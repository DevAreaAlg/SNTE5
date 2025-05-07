Imports System.Data.SqlClient
Imports System.IO

Public Class CRED_EXP_AUTORIZAEXCEDENTE
    Inherits Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Autorización especial", "Autorización de préstamos con excedente")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            LlenarExpXPagar()
        End If
    End Sub

#Region "Carga Inicial"

    Private Sub LlenarExpXPagar()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXPEDIENTES_AUTOESP"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

        If DAG_Analisis.Rows.Count > 0 Then

            btn_autorizar.Visible = True
            btn_rechazar.Visible = True
        Else

            btn_autorizar.Visible = False
            btn_rechazar.Visible = False
        End If

    End Sub

#End Region


#Region "Evento Correo"

    Private Sub PagoCreditosCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "REPCREDPAG")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Pagos de préstamos autorizados"

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Se le informa que se han realizado el pago de préstamo(s).</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Favor de descargar órdenes de descuento y verificar.</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

#End Region


    Protected Sub btn_Autoriza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_autorizar.Click

        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            'PagoCreditosCorreo()
            Autoriza()
        Else
            lbl_status.Text = "Error: No ha elegido préstamos para pagar"
        End If

    End Sub


    Protected Sub btn_Rechaza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_rechazar.Click

        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            'PagoCreditosCorreo()
            Rechaza()
        Else
            lbl_status.Text = "Error: No ha elegido préstamos para pagar"
        End If

    End Sub

    Private Sub Autoriza()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim dtDescuentos As New DataTable()

        dtDescuentos.Columns.Add("FOLIO", GetType(Integer))
        dtDescuentos.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtDescuentos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("UPD_CONFIRMA_EXCEDENTES", connection)
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

                Session("parm") = New SqlParameter("BANDERA", SqlDbType.VarChar)
                Session("parm").Value = "AUTORIZADO"
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lbl_status.Text = "Préstamos Autorizados correctamente"
            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarExpXPagar()
            lbl_registros_sel.Text = "Registros Seleccionados: 0"

        End Try

    End Sub

    Private Sub Rechaza()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim dtDescuentos As New DataTable()

        dtDescuentos.Columns.Add("FOLIO", GetType(Integer))
        dtDescuentos.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtDescuentos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("UPD_CONFIRMA_EXCEDENTES", connection)
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


                Session("parm") = New SqlParameter("BANDERA", SqlDbType.VarChar)
                Session("parm").Value = "CANCELADO"
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lbl_status.Text = "Préstamos Rechazados correctamente"
            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarExpXPagar()
            lbl_registros_sel.Text = "Registros Seleccionados: 0"

        End Try

    End Sub


End Class