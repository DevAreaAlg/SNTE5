Imports System.Data.SqlClient
Imports System.IO

Public Class CORE_PER_PAGA_PENSIONES
    Inherits Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Pago de pensiones por transferencia", "Pago de pensiones por Transferencia")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            EstatusAhorro()
            CargaCiclos()
        End If

    End Sub

#Region "Carga Inicial"

    Private Sub LlenarExpXPagar(estatus As Integer, ciclo As Integer)

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PENSION_SPEI"
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 1000, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CICLO", Session("adVarChar"), Session("adParamInput"), 1000, ciclo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtAnalisis, Session("rs"))
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()
        Session("Con").Close()

        If DAG_Analisis.Rows.Count > 0 Then
            btn_layout_bancos.Visible = True
            lbl_registros_tol.Text = "Total de Pensiones: " + DAG_Analisis.Rows.Count.ToString
            lbl_layout_bancos.Visible = True
            fud_layout_bancos.Visible = True
            btn_pagar.Visible = True
        Else
            btn_layout_bancos.Visible = False
            lbl_registros_tol.Text = "Total de Pensiones: 0"
            lbl_layout_bancos.Visible = False
            fud_layout_bancos.Visible = False
            btn_pagar.Visible = False
        End If

    End Sub



#End Region

    Protected Sub Suma(sender As Object, e As EventArgs)

        Dim monto As Decimal = 0.00
        Dim registros As Integer = 0


        For Each row As GridViewRow In DAG_Analisis.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chk_Aplicado"), CheckBox)
                If chkRow.Checked Then

                    monto += (CDec(row.Cells(2).Text))


                    registros += 1
                End If
            End If
        Next


        If monto >= 0.00 Then
            btn_layout_bancos.Visible = True
        Else
            btn_layout_bancos.Visible = False
        End If
        lbl_registros_sel.Text = "Registros Seleccionados: " + registros.ToString
        lbl_acumulado.Text = monto
    End Sub

#Region "Genera Layout Bancos"

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
                dtBancos.Rows.Add(0,
                                  Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("NUMCLIENTE"), Label).Text),
                                  "",
                                  CDec(DAG_Analisis.Rows(i).Cells(2).Text))
            End If
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()

                Dim insertCommand As New SqlCommand("SEL_PAGO_A_BANCOS_PENSION", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("LAYOUT_PRESTAMOS", SqlDbType.Structured)
                Session("parm").Value = dtBancos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SECUENCIA", SqlDbType.Int)
                Session("parm").Value = txt_secuencia.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("USERID", SqlDbType.Int)
                Session("parm").Value = Session("USERID").ToString
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar, 20)
                Session("parm").Value = Session("Sesion").ToString
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("CICLO", SqlDbType.Int)
                Session("parm").Value = cmb_ciclo.SelectedItem.Value.ToString
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



#Region "Evento Correo"

    'Private Sub PagoCreditosCorreo()

    '    Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
    '    Dim correo As New Correo 'Variable para la clase de correo
    '    Dim sbhtml As New StringBuilder

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "REPCREDPAG")
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
    '    Session("rs") = Session("cmd").Execute()

    '    Dim subject As String = "Pagos de préstamos autorizados"

    '    Do While Not Session("rs").EOF

    '        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
    '        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
    '        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
    '        sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
    '        sbhtml.Append("<br/>")
    '        sbhtml.Append("<tr><td>Se le informa que se han realizado el pago de préstamo(s).</td></tr>")
    '        sbhtml.Append("<br/>")
    '        sbhtml.Append("<tr><td>Favor de descargar órdenes de descuento y verificar.</td></tr>")
    '        sbhtml.Append("</table>")
    '        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
    '        sbhtml.Append("<br/><br/><br/>")
    '        sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
    '        sbhtml.Append("</table>")

    '        'Envio de Correo
    '        correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

    '        sbhtml.Clear()

    '        Session("rs").movenext()

    '    Loop

    '    Session("Con").Close()

    'End Sub

#End Region

#Region "Procesa Layout Bancos"

    Protected Sub btn_pagar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_pagar.Click

        If fud_layout_bancos.HasFile = False Then
            lbl_status.Text = "Error: Debe seleccionar un archivo."
            Exit Sub
        End If

        If Path.GetExtension(fud_layout_bancos.FileName.ToUpper()) = ".TXT" Then
            PagaCreditos()
            'PagoCreditosCorreo()
        Else
            lbl_status.Text = "Error: Debe seleccionar un archivo tipo TXT."
            Exit Sub
        End If

    End Sub

    Private Sub PagaCreditos()

        Dim FechaSistema As Date = Convert.ToDateTime(Session("FechaSis").ToString)
        Dim NoRandom As Random = New Random()
        Dim NombreArchivo As String = fud_layout_bancos.FileName.ToUpper.ToString
        NombreArchivo = FechaSistema.Year.ToString + FechaSistema.Month.ToString + FechaSistema.Day.ToString + "_" + NoRandom.Next().ToString + "_PA_" + NombreArchivo.Replace(".TXT", "_PROCESADO.txt").ToString

        Dim SaveFile As String = Server.MapPath("/LAYOUTS_DEFINITIVOS/" + NombreArchivo.ToString)
        fud_layout_bancos.SaveAs(SaveFile)

        Dim FilePath As String = Server.MapPath("/tmp/" + fud_layout_bancos.FileName.ToString)
        fud_layout_bancos.SaveAs(FilePath)

        Try

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FILEPATH", Session("adVarChar"), Session("adParamInput"), 1000, FilePath)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FILENAME", Session("adVarChar"), Session("adParamInput"), 1000, NombreArchivo)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PROCESA_LAYOUT_BANCOS_PENSION"
            Session("rs") = Session("cmd").Execute()

            lbl_status.Text = "Se ha procesado con éxito el layout de bancos y se entregaron correctamente los préstamos."

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally

            Session("Con").Close()

            DelHDFile(FilePath)
            'LlenarExpXPagar()

            lbl_acumulado.Text = ""
            lbl_registros_sel.Text = "Registros Seleccionados: 0"
            txt_secuencia.Text = ""

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

    Private Sub DAG_Analisis_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles DAG_Analisis.PageIndexChanging
        DAG_Analisis.PageIndex = e.NewPageIndex
        DAG_Analisis.PageSize = 1000
    End Sub



    Private Sub EstatusAhorro()
        ahorro.Items.Clear()
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_ESTATUS_AHORRO"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                ahorro.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value, Session("rs").Fields("ID").Value.ToString))

                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub Filtro_Click(sender As Object, e As EventArgs) Handles Filtro.Click
        Dim est As Integer = ahorro.SelectedItem.Value.ToString
        Dim idciclo As Integer = cmb_ciclo.SelectedItem.Value.ToString

        LlenarExpXPagar(est, idciclo)
    End Sub

    Private Sub CargaCiclos()


        cmb_ciclo.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CICLOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TEXT").Value.ToString, Session("rs").Fields("VALUE").Value.ToString)
            cmb_ciclo.Items.Add(item)
            Session("rs").movenext()
        Loop



        Session("Con").Close()
    End Sub
End Class