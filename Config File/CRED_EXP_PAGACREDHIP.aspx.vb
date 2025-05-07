Imports System.Data.SqlClient
Imports System.IO

Public Class CRED_EXP_PAGACREDHIP
    Inherits Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Préstamos complementarios por pagar", "Préstamos complementarios por pagar")

        If Not IsPostBack Then
            LlenarExpXPagar()
            CargaSaldoActual()
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
        Session("cmd").CommandText = "SEL_EXPEDIENTES_PORPAGAR"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTI", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, "PC")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

        If DAG_Analisis.Rows.Count > 0 Then
            btn_layout_bancos.Visible = True
            lbl_registros_tol.Text = "Total de préstamos: " + DAG_Analisis.Rows.Count.ToString
            lbl_layout_bancos.Visible = True
            fud_layout_bancos.Visible = True
            btn_pagar.Visible = True
        Else
            btn_layout_bancos.Visible = False
            lbl_registros_tol.Text = "Total de préstamos: 0"
            lbl_layout_bancos.Visible = False
            fud_layout_bancos.Visible = False
            btn_pagar.Visible = False
        End If

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

#End Region

    Private Sub DAG_Analisis_RowCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles DAG_Analisis.SelectedIndexChanged

        Session("PROSPECTO") = e.Item.Cells(1).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PRODUCTO") = e.Item.Cells(4).Text
        Session("MONTO") = e.Item.Cells(5).Text

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
                    registros += 1
                    monto += (CDec(row.Cells(4).Text))
                    data = monto.ToString("C")
                    presuFin -= (CDec(row.Cells(4).Text))
                    dataFin = presuFin.ToString("C")
                End If
            End If
        Next

        If monto > 0.00 Then
            btn_layout_bancos.Visible = True
        Else
            btn_layout_bancos.Visible = False
        End If

        lbl_registros_sel.Text = "Registros Seleccionados: " + registros.ToString
        lbl_acumulado.Text = data
        lbl_presfinal.Text = dataFin

    End Sub

#Region "Genera Layout de Bancos"

    Protected Sub btn_layout_bancos_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_layout_bancos.Click

        lbl_status.Text = ""
        If txt_secuencia.Text <> "" Then
            DescargaTXTLayout()
        Else
            lbl_status.Text = "Error: Capture número de secuencia."
        End If

    End Sub

    Private Sub DescargaTXTLayout()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtRegistros As New DataTable()
        Dim NombreTXT As String = "pagos_"

        Dim dtBancos As New DataTable()
        dtBancos.Columns.Add("FOLIO", GetType(Integer))
        dtBancos.Columns.Add("ID_PERSONA", GetType(Integer))
        dtBancos.Columns.Add("CLAVE_EXPEDIENTE", GetType(String))
        dtBancos.Columns.Add("MONTO", GetType(Decimal))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                dtBancos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("NUMCLIENTE"), Label).Text), DAG_Analisis.Rows(i).Cells(0).Text, CDec(DAG_Analisis.Rows(i).Cells(4).Text))
            End If
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()

                Dim insertCommand As New SqlCommand("SEL_PAGO_A_BANCOS", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("LAYOUT_PRESTAMOS", SqlDbType.Structured)
                Session("parm").Value = dtBancos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SECUENCIA", SqlDbType.Int)
                Session("parm").Value = txt_secuencia.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("USERID", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dtRegistros.Load(myReader)

                Dim context As HttpContext = HttpContext.Current
                context.Response.Clear()
                context.Response.ContentEncoding = Encoding.Default

                For Each Renglon As DataRow In dtRegistros.Rows
                    context.Response.Write(Renglon.Item(0).ToString)
                    context.Response.Write(Environment.NewLine)
                Next

                context.Response.ContentType = "text/csv"
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreTXT + Session("FechaSis").ToString + ".txt")
                context.Response.End()

                myReader.Close()

            End Using

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            lbl_status.Text = "Se ha generado el layout de bancos."
        End Try

    End Sub

#End Region

    Private Sub LimpiaVariables()

        Session("PROSPECTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("PRODUCTO") = Nothing

    End Sub

#Region "Procesa Layout Bancos"

    Protected Sub btn_pagar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_pagar.Click

        If fud_layout_bancos.HasFile = False Then
            lbl_status.Text = "Error: Debe seleccionar un archivo."
            Exit Sub
        End If

        If Path.GetExtension(fud_layout_bancos.FileName.ToUpper()) = ".TXT" Then
            PagaCreditos()
            PagoCreditosCorreo()
        Else
            lbl_status.Text = "Error: Debe seleccionar un archivo tipo TXT."
            Exit Sub
        End If

    End Sub

    Private Sub PagaCreditos()

        Try

            Dim FilePath As String = Server.MapPath("/tmp/" + fud_layout_bancos.FileName.ToString)
            fud_layout_bancos.SaveAs(FilePath)

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FILEPATH", Session("adVarChar"), Session("adParamInput"), 1000, FilePath)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PROCESA_LAYOUT_BANCOS"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()

            DelHDFile(FilePath)

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarExpXPagar()
            CargaSaldoActual()
            lbl_acumulado.Text = ""
            lbl_presfinal.Text = ""
            lbl_registros_sel.Text = "Registros Seleccionados: 0"
            txt_secuencia.Text = ""
            lbl_status.Text = "Se ha procesado con exito el layout de bancos y se entregaron correctamente los préstamos."
        End Try

    End Sub

    Private Sub DelHDFile(ByVal File1 As String)
        If File.Exists(File1) Then
            Dim fi As New FileInfo(File1)
            If (fi.Attributes And FileAttributes.ReadOnly) <> 0 Then
                fi.Attributes = fi.Attributes Xor FileAttributes.ReadOnly
            End If
        Else
            lbl_status.Text = "Alerta: El archivo ha sido movido o eliminado"
        End If
        File.Delete(File1)
    End Sub

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
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
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