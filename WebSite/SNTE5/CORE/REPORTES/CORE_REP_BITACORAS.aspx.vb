Public Class CORE_REP_BITACORAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            LlenaReportes()
            LlenaUsuarios()
            LlenaEventos()
            LlenaModulos()

        End If

        TryCast(TryCast(Master, MasterMascore).FindControl("lbl_tituloASPX"), Label).Text = "Sistema de Bitácoras"

    End Sub

    Private Sub LlenaReportes()

        ddl_logs.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_logs.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_BITACORAS_PERMISOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("REPORTE").Value, Session("rs").Fields("CVE_REPORTE").Value.ToString)
                ddl_logs.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

    End Sub

    Private Sub LlenaUsuarios()

        ddl_users.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_users.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_TOTAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("USUARIO").Value, Session("rs").Fields("IDUSUARIO").Value.ToString)
                ddl_users.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

    End Sub

    Private Sub LlenaEventos()

        ddl_events.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_events.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_BITACORAS_EVENTOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("EVENTO").Value, Session("rs").Fields("IDEVENTO").Value.ToString)
                ddl_events.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

    End Sub

    Private Sub LlenaModulos()

        ddl_modules.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_modules.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_BITACORAS_MODULOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("MODULO").Value, Session("rs").Fields("IDMODULO").Value.ToString)
                ddl_modules.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

    End Sub

    Protected Sub ddl_logs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_logs.SelectedIndexChanged

        lbl_estatus.Text = ""
        Limpiar()

        Select Case ddl_logs.SelectedItem.Value

            Case "REPLOGTRAN" ' REPORTE BITACORA DE TRANSACCIONES
                txt_folio.Enabled = False
                ddl_users.Enabled = True
                ddl_events.Enabled = True
                ddl_modules.Enabled = False
                btn_search.Enabled = True

            Case "REPLOGISES" ' REPORTE BITACORA DE INICIO DE SESION
                txt_folio.Enabled = False
                ddl_users.Enabled = True
                ddl_events.Enabled = True
                ddl_modules.Enabled = True
                btn_search.Enabled = True

            Case "REPLOGEEXP" ' REPORTE BITACORA ESTATUS DE EXPEDIENTES
                txt_folio.Enabled = True
                ddl_users.Enabled = True
                ddl_events.Enabled = False
                ddl_modules.Enabled = False
                btn_search.Enabled = True

            Case "REPLOGPUSR"  ' REPORTE PERMISOS DE USUARIOS
                txt_folio.Enabled = False
                txt_enddate.Enabled = False
                txt_initdate.Enabled = False
                RequiredFieldValidator_txt_enddate.Enabled = False
                RequiredFieldValidator_initdate.Enabled = False
                ddl_users.Enabled = True
                ddl_events.Enabled = False
                ddl_modules.Enabled = False
                btn_search.Enabled = True

            Case "REPLOGCDIA" ' REPORTE BITACORA DE CIERRES DE DIA
                txt_folio.Enabled = False
                ddl_users.Enabled = False
                ddl_events.Enabled = False
                ddl_modules.Enabled = False
                btn_search.Enabled = True

            Case Else
                txt_folio.Enabled = False
                ddl_users.Enabled = False
                ddl_events.Enabled = False
                ddl_modules.Enabled = False
                btn_search.Enabled = False

        End Select

    End Sub

    Private Sub Limpiar()

        DAG_LOGSESION.Visible = False
        DAG_LOGTRANS.Visible = False
        DAG_PERMISOS.Visible = False
        DAG_CIERREDIAS.Visible = False
        DAG_ESTEXP.Visible = False

        ddl_users.SelectedValue = -1
        ddl_modules.SelectedValue = -1
        ddl_events.SelectedValue = -1

        txt_folio.Text = ""
        txt_initdate.Text = ""
        txt_enddate.Text = ""

        RequiredFieldValidator_txt_enddate.Enabled = True
        RequiredFieldValidator_initdate.Enabled = True
        txt_enddate.Enabled = True
        txt_initdate.Enabled = True
        txt_folio.Enabled = False
        btn_EXCEL.Visible = False

    End Sub

    'CONSULTA MOSTAR DATOS EN DATA GRIDS
    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click

        Select Case ddl_logs.SelectedItem.Value

            Case "REPLOGTRAN" ' REPORTE BITACORA DE TRANSACCIONES
                If CDate(txt_initdate.Text) > CDate(txt_enddate.Text) Then
                    lbl_estatus.Text = "Error: La fecha inicial debe ser menor o igual a la fecha final."
                    Exit Sub
                Else
                    lbl_estatus.Text = ""
                    ConsultaBitacoraTrans(0)
                End If

            Case "REPLOGISES" ' REPORTE BITACORA DE INICIO DE SESION
                If CDate(txt_initdate.Text) > CDate(txt_enddate.Text) Then
                    lbl_estatus.Text = "Error: La fecha inicial debe ser menor o igual a la fecha final."
                    Exit Sub
                Else
                    lbl_estatus.Text = ""
                    ConsultaBitacoraSesion(0)
                End If

            Case "REPLOGEEXP" ' REPORTE BITACORA ESTATUS DE EXPEDIENTES
                If CDate(txt_initdate.Text) > CDate(txt_enddate.Text) Then
                    lbl_estatus.Text = "Error: La fecha inicial debe ser menor o igual a la fecha final."
                    Exit Sub
                Else
                    lbl_estatus.Text = ""
                    If txt_folio.Text = "" Then
                        ConsultaBitacoraEstatusExp(0)
                    Else
                        If ValidaFolio(txt_folio.Text) = True Then
                            ConsultaBitacoraEstatusExp(0)
                        Else
                            lbl_estatus.Text = "Error: El folio del expediente tiene el formato invalido."
                        End If
                    End If

                End If

            Case "REPLOGCDIA" ' REPORTE BITACORA DE CIERRES DE DIA
                If CDate(txt_initdate.Text) > CDate(txt_enddate.Text) Then
                    lbl_estatus.Text = "Error: La fecha inicial debe ser menor o igual a la fecha final."
                    Exit Sub
                Else
                    lbl_estatus.Text = ""
                    ConsultaBitacoraCierreDia(0)
                End If

            Case "REPLOGPUSR"  ' REPORTE PERMISOS DE USUARIOS
                lbl_estatus.Text = ""
                ConsultaBitacoraPermisos(0)

            Case Else

        End Select

    End Sub

    Private Sub ConsultaBitacoraPermisos(ByVal Pagina As Integer)

        If Pagina = 0 Then
            DAG_PERMISOS.PageIndex = 0
        End If

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_PERMISOS_USUARIOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()
        If dtConsulta.Rows.Count > 0 Then
            btn_EXCEL.Visible = True
            DAG_PERMISOS.Visible = True
            DAG_PERMISOS.DataSource = dtConsulta
            ViewState("DAG_PERMISOS") = dtConsulta
            DAG_PERMISOS.DataBind()
        Else
            btn_EXCEL.Visible = False
            DAG_PERMISOS.Visible = False
        End If

    End Sub

    Private Sub ConsultaBitacoraTrans(ByVal Pagina As Integer)

        If Pagina = 0 Then
            DAG_LOGTRANS.PageIndex = 0
        End If

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_events.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_LOGTRANS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()
        If dtConsulta.Rows.Count > 0 Then
            btn_EXCEL.Visible = True
            DAG_LOGTRANS.Visible = True
            DAG_LOGTRANS.DataSource = dtConsulta
            ViewState("DAG_LOGTRANS") = dtConsulta
            DAG_LOGTRANS.DataBind()
        Else
            btn_EXCEL.Visible = False
            DAG_LOGTRANS.Visible = False
        End If

    End Sub

    Private Sub ConsultaBitacoraSesion(ByVal Pagina As Integer)

        If Pagina = 0 Then
            DAG_LOGSESION.PageIndex = 0
        End If

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_events.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODULO", Session("adVarChar"), Session("adParamInput"), 10, ddl_modules.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_LOGSESION"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()
        If dtConsulta.Rows.Count > 0 Then
            btn_EXCEL.Visible = True
            DAG_LOGSESION.Visible = True
            DAG_LOGSESION.DataSource = dtConsulta
            ViewState("DAG_LOGSESION") = dtConsulta
            DAG_LOGSESION.DataBind()
        Else
            btn_EXCEL.Visible = False
            DAG_LOGSESION.Visible = False
        End If

    End Sub

    Private Sub ConsultaBitacoraCierreDia(ByVal Pagina As Integer)

        If Pagina = 0 Then
            DAG_CIERREDIAS.PageIndex = 0
        End If

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INICIO", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FINAL", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_CIERRE_DIA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()
        If dtConsulta.Rows.Count > 0 Then
            btn_EXCEL.Visible = True
            DAG_CIERREDIAS.Visible = True
            DAG_CIERREDIAS.DataSource = dtConsulta
            ViewState("DAG_CIERREDIAS") = dtConsulta
            DAG_CIERREDIAS.DataBind()
        Else
            btn_EXCEL.Visible = False
            DAG_CIERREDIAS.Visible = False
        End If

    End Sub

    Private Sub ConsultaBitacoraEstatusExp(ByVal Pagina As Integer)

        If Pagina = 0 Then
            DAG_ESTEXP.PageIndex = 0
        End If

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If txt_folio.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, "-1")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, txt_folio.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_ESTATUS_EXP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()
        If dtConsulta.Rows.Count > 0 Then
            btn_EXCEL.Visible = True
            DAG_ESTEXP.Visible = True
            DAG_ESTEXP.DataSource = dtConsulta
            ViewState("DAG_ESTEXP") = dtConsulta
            DAG_ESTEXP.DataBind()
        Else
            btn_EXCEL.Visible = False
            DAG_ESTEXP.Visible = False
        End If

    End Sub

    'Private Sub ConsultaBitacoraError(ByVal Pagina As Integer)

    '    If Pagina = 0 Then
    '        '  DAG_LOGERROR.CurrentPageIndex = 0
    '        DAG_LOGERROR.PageIndex = 0
    '    End If

    '    Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    '    Dim dtConsulta As New Data.DataTable()

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_events.SelectedItem.Value)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "REP_BITACORAS_ERROR"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dtConsulta, Session("rs"))
    '    Session("Con").Close()
    '    If dtConsulta.Rows.Count > 0 Then
    '        btn_EXCEL.Visible = True
    '        DAG_LOGERROR.Visible = True
    '        DAG_LOGERROR.DataSource = dtConsulta
    '        ViewState("DAG_LOGERROR") = dtConsulta
    '        DAG_LOGERROR.DataBind()
    '    Else
    '        btn_EXCEL.Visible = False
    '        DAG_LOGERROR.Visible = False
    '    End If

    'End Sub

    ' REPORTES EN EXCEL

    Protected Sub btn_EXCEL_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_EXCEL.Click

        Select Case ddl_logs.SelectedItem.Value

            Case "REPLOGCDIA" ' REPORTE BITACORA DE CIERRES DE DIA
                ExcelBitacoraCierreDia()

            Case "REPLOGISES" ' REPORTE BITACORA DE INICIO DE SESION
                ExcelBitacoraSesion()

            Case "REPLOGTRAN" ' REPORTE BITACORA DE TRANSACCIONES
                ExcelBitacoraTrans()

            Case "REPLOGEEXP" ' REPORTE BITACORA ESTATUS DE EXPEDIENTES
                ExcelBitacoraEstatusExp()

            Case "REPLOGPUSR"  ' REPORTE PERMISOS DE USUARIOS
                ExcelBitacoraPermisos()

                'Case "ERRORLOG" ' REPORTE BITACORA DE ERRORES
                '    ExcelBitacoraError()

            Case Else

        End Select

    End Sub

#Region "Excel"

    Private Sub ExcelBitacoraCierreDia()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INICIO", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FINAL", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_CIERRE_DIA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As Data.DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Reporte Bitacora Cierres Dia " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    Private Sub ExcelBitacoraSesion()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_events.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODULO", Session("adVarChar"), Session("adParamInput"), 10, ddl_modules.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_LOGSESION"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As Data.DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Reporte Bitacora Inicio Sesion " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    Private Sub ExcelBitacoraTrans()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_events.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_LOGTRANS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As Data.DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Reporte Bitacora Transacciones " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    Private Sub ExcelBitacoraEstatusExp()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If txt_folio.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, "-1")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, txt_folio.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_BITACORAS_ESTATUS_EXP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As Data.DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Reporte Bitacora Estatus Expedientes " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    Private Sub ExcelBitacoraPermisos()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_PERMISOS_USUARIOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As Data.DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Reporte Permisos Usuarios " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    'Private Sub ExcelBitacoraError()

    '    Dim custDA As New Data.OleDb.OleDbDataAdapter()
    '    Dim dtConsulta As New Data.DataTable()

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_initdate.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_enddate.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_users.SelectedItem.Value)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_events.SelectedItem.Value)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "REP_BITACORAS_ERROR"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dtConsulta, Session("rs"))
    '    Session("Con").Close()

    '    Dim context As HttpContext = HttpContext.Current
    '    context.Response.Clear()
    '    context.Response.ContentEncoding = Encoding.Default

    '    Dim i As Integer
    '    For i = 0 To dtConsulta.Columns.Count - 1
    '        context.Response.Write(dtConsulta.Columns(i).Caption + ",")
    '    Next

    '    context.Response.Write(Environment.NewLine)

    '    For Each Renglon As Data.DataRow In dtConsulta.Rows
    '        For i = 0 To dtConsulta.Columns.Count - 1
    '            context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
    '        Next
    '        context.Response.Write(Environment.NewLine)
    '    Next

    '    context.Response.ContentType = "text/csv"
    '    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Output" + ".csv")
    '    context.Response.End()

    'End Sub

#End Region

#Region "Paginadores de GridViews"

    'Protected Sub dag_LogError_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DAG_LOGERROR.PageIndexChanging

    '    DAG_LOGERROR.PageIndex = e.NewPageIndex
    '    DAG_LOGERROR.DataSource = ViewState("DAG_LOGERROR")
    '    DAG_LOGERROR.DataBind()

    'End Sub

    Protected Sub dag_logsesion_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DAG_LOGSESION.PageIndexChanging

        DAG_LOGSESION.PageIndex = e.NewPageIndex
        DAG_LOGSESION.DataSource = ViewState("DAG_LOGSESION")
        DAG_LOGSESION.DataBind()

    End Sub

    Protected Sub dag_LogTrans_pageIndexChnging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DAG_LOGTRANS.PageIndexChanging

        DAG_LOGTRANS.PageIndex = e.NewPageIndex
        DAG_LOGTRANS.DataSource = ViewState("DAG_LOGTRANS")
        DAG_LOGTRANS.DataBind()

    End Sub

    Protected Sub dag_permisos_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DAG_PERMISOS.PageIndexChanging

        DAG_PERMISOS.PageIndex = e.NewPageIndex
        DAG_PERMISOS.DataSource = ViewState("DAG_PERMISOS")
        DAG_PERMISOS.DataBind()

    End Sub

    Protected Sub dag_cierredias_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DAG_CIERREDIAS.PageIndexChanging

        DAG_CIERREDIAS.PageIndex = e.NewPageIndex
        DAG_CIERREDIAS.DataSource = ViewState("DAG_CIERREDIAS")
        DAG_CIERREDIAS.DataBind()

    End Sub

    Protected Sub dag_estexp_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DAG_ESTEXP.PageIndexChanging

        DAG_ESTEXP.PageIndex = e.NewPageIndex
        DAG_ESTEXP.DataSource = ViewState("DAG_ESTEXP")
        DAG_ESTEXP.DataBind()

    End Sub

#End Region

#Region "Herramientas"

    Private Function ValidaFolio(ByVal folio As String) As Boolean
        Return Regex.IsMatch(folio, ("^([pa|pc|pd|PA|PC|PD]{2}[-]{1}[\d]{4}[-]{1}[\d]{6})$"))
    End Function

#End Region

End Class