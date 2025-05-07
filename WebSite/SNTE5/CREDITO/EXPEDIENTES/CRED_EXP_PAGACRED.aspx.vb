Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_EXP_PAGACRED
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Préstamos por pagar", "Préstamos por pagar")
        If Not Me.IsPostBack Then

            LlenaInstituciones()
            'LlenarExpXPagar()
            CargaTipoProd()

        End If

    End Sub

    Private Sub LlenaInstituciones()

        ddl_Instituciones.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        ddl_Instituciones.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ENTIDADES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Instituciones.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CargaTipoProd()

        cmb_producto.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_producto.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRODUCTOS_ACTIVOS_COTIZADOR"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("MSTPRODUCTOS_NOMBRE").Value.ToString + " (" + Session("rs").Fields("DESTINO").Value.ToString + ")", Session("rs").Fields("MSTPRODUCTOS_ID_PROD").Value.ToString)
            cmb_producto.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_producto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_producto.SelectedIndexChanged
        'Metodo que llena el droplist con los expedientes
        If cmb_producto.SelectedItem.Value.ToString <> -1 And ddl_Instituciones.SelectedItem.Value.ToString <> -1 Then
            LlenarExpXPagar()

        End If

    End Sub

    Protected Sub cmb_dependencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Instituciones.SelectedIndexChanged
        'Metodo que llena el droplist con los expedientes
        If cmb_producto.SelectedItem.Value.ToString <> -1 And ddl_Instituciones.SelectedItem.Value.ToString <> -1 Then
            LlenarExpXPagar()

        End If
    End Sub

    Private Sub LlenarExpXPagar()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXPEDIENTES_PORPAGAR"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTI", Session("adVarChar"), Session("adParamInput"), 10, ddl_Instituciones.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()
        If DAG_Analisis.Rows.Count > 0 Then
            btn_pagar.Visible = True
            btn_pagos.Visible = True
        Else
            btn_pagar.Visible = False
            btn_pagos.Visible = False
        End If

    End Sub

    Private Sub DAG_Analisis_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_Analisis.SelectedIndexChanged

        Session("FOLIO") = e.Item.Cells(0).Text
        Session("PROSPECTO") = e.Item.Cells(1).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PRODUCTO") = e.Item.Cells(4).Text
        Session("MONTO") = e.Item.Cells(5).Text

    End Sub

    Protected Sub Suma(sender As Object, e As EventArgs)
        Dim data As String = ""
        Dim monto As Decimal = 0.00
        For Each row As GridViewRow In DAG_Analisis.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chk_Aplicado"), CheckBox)
                If chkRow.Checked Then

                    monto = monto + (CDec(row.Cells(6).Text))
                    data = (Convert.ToString(monto))
                Else

                End If
            End If
        Next

    End Sub

    Private Sub LimpiaVariables()
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("PRODUCTO") = Nothing

    End Sub

    Protected Sub btn_bancos_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagos.Click
        DescargaCSVDescuentos()
    End Sub

    Protected Sub btn_GetArchivoDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagar.Click
        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then

            PagaCreditos()

            PagoCreditosCorreo()
        Else
            lbl_status.Text = "Error: No ha elegido préstamos para pagar"
        End If

    End Sub

    Private Sub PagoCreditosCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "REPCREDPAG")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Pagos de préstamos autorizados"

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Se le informa que se ha realizado el pago de préstamo(s).</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Favor de descargar órdenes de descuento y verificar.</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

    Private Sub DescargaCSVDescuentos()
        'lbl_status.Text = "" +
        'Periodos Activos
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim NombreCSV As String = "pagos_"

        Dim result As String = "OK"


        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("SEL_PAGO_A_BANCOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("INSTI", SqlDbType.Int)
                Session("parm").Value = ddl_Instituciones.SelectedItem.Value
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = cmb_producto.SelectedItem.Value
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()

                    dtMovimientosDescuentos.Load(myReader)

                    Dim context As HttpContext = HttpContext.Current
                    context.Response.Clear()
                    context.Response.ContentEncoding = System.Text.Encoding.Default
                    Dim iRow As Integer = 0
                    Dim i As Integer

                    For Each Renglon As Data.DataRow In dtMovimientosDescuentos.Rows

                        For i = 1 To dtMovimientosDescuentos.Columns.Count - 1

                            context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + "")

                        Next
                        context.Response.Write(Environment.NewLine)
                    Next

                    context.Response.ContentType = "text/csv"
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreCSV + Session("FechaSis").ToString + ".txt")
                    context.Response.End()
                End While
                myReader.Close()

            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarExpXPagar()
        End Try

    End Sub

    Private Sub PagaCreditos()
        'lbl_status.Text = "" +
        'Periodos Activos
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim NombreCSV As String = "DESCUENTOS"

        Dim result As String = "OK"
        Dim dtDescuentos As New Data.DataTable()

        dtDescuentos.Columns.Add("FOLIO", GetType(Integer))
        dtDescuentos.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtDescuentos.Rows.Add(CInt(DAG_Analisis.Rows(i).Cells(0).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("INS_PAGOS_CONFIRMADOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("CONFIRMA", SqlDbType.Structured)
                Session("parm").Value = dtDescuentos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lbl_status.Text = "Préstamos entregados correctamente"
            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarExpXPagar()

        End Try

    End Sub


End Class