Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Imports RestSharp.Extensions

Public Class CRED_EXP_PORPAGARPER
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Préstamos de ahorro pendientes de aprobación", "Préstamos de ahorro pendientes de aprobación")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            'LlenaInstituciones()
            LlenarExpComite()
            CargaSaldoActual()
        End If
    End Sub


    Private Sub LlenarExpComite()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXPEDIENTES_COLA"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDEPEN", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, "PA")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

        If DAG_Analisis.Rows.Count > 0 Then
            btn_aprobar.Visible = True
            lbl_registros_tol.Text = "Total de préstamos: " + DAG_Analisis.Rows.Count.ToString
        Else
            btn_aprobar.Visible = False
            lbl_registros_tol.Text = "Total de préstamos: 0"
        End If

    End Sub

    Private Sub DAG_Analisis_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_Analisis.SelectedIndexChanged

        Session("PROSPECTO") = e.Item.Cells(1).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PRODUCTO") = e.Item.Cells(4).Text
        Session("MONTO") = e.Item.Cells(5).Text
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                ObtieneSuma()
            End If

        Next

    End Sub

    Private Sub ObtieneSuma()
        lbl_acumulado.Text = Session("MONTO").ToString
    End Sub



    Protected Sub Suma(sender As Object, e As EventArgs)
        Dim data As String = ""
        Dim monto As Decimal = 0.00
        Dim registros As Integer = 0
        Dim presuFin As Decimal = CDec(lbl_presactual.Text)
        Dim dataFin As String = ""
        For Each row As GridViewRow In DAG_Analisis.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chk_Aplicado"), CheckBox)
                If chkRow.Checked Then
                    If (DirectCast(row.FindControl("CLAVE_PRODUCTO"), Label).Text).ToString <> "PD" Then
                        monto += (CDec(row.Cells(6).Text))
                        data = monto.ToString("C")
                        presuFin -= (CDec(row.Cells(6).Text))
                        dataFin = presuFin.ToString("C")
                        dataFin = presuFin.ToString("C")
                    End If
                    registros += 1
                End If
            End If
        Next

        lbl_registros_sel.Text = "Registros Seleccionados: " + registros.ToString
        lbl_acumulado.Text = data
        lbl_presfinal.Text = dataFin
    End Sub

    Private Sub LimpiaVariables()

        Session("PROSPECTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("PRODUCTO") = Nothing
    End Sub

    Protected Sub btn_GetArchivoDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aprobar.Click
        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            AutorizaCreditos()

        Else
            lbl_status.Text = "Error: No ha elegido préstamos para autorizar"
        End If

    End Sub

    Private Sub AutorizaCreditos()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New DataTable()
        Dim dtDescuentos As New DataTable()

        dtDescuentos.Columns.Add("FOLIO", GetType(Integer))
        dtDescuentos.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtDescuentos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("UPD_PAGOS_CONFIRMADOS", connection)
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
                lbl_status.Text = "Se autorizaron correctamente los préstamos"
                AutorizarCreditosCorreo()

            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarExpComite()
            lbl_acumulado.Text = ""
            lbl_registros_sel.Text = "Registros Seleccionados: 0"
        End Try

    End Sub


    Private Sub CargaSaldoActual()

        Dim SaldoActual As Decimal

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SALDO_ACTUAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            SaldoActual = Session("rs").Fields("SALDO_ACTUAL").value
            lbl_presactual.Text = SaldoActual.ToString("C")

        End If
        Session("Con").Close()

    End Sub

    Private Sub AutorizarCreditosCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "APROBCRED")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Se han autorizado préstamos pendientes"

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Se le informa que se ha(n) autorizado el(los) préstamo(s) pendiente(s).</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Favor de ir al módulo pago de préstamos para finalizar el proceso de prospección.</td></tr>")
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

    Private Sub ckb_todos_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_todos.CheckedChanged

        If ckb_todos.Checked Then
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox)
                chkRow.Checked = 0
            Next
        End If

    End Sub

End Class