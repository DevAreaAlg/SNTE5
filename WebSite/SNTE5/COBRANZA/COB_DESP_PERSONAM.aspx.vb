Imports System.IO
Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Public Class COB_DESP_PERSONAM
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Alta Despacho", "CONFIGURACIÓN DE DESPACHO")

        If Not Me.IsPostBack Then

            Session("DEPENDENCIAID") = Nothing
            Session("idperbusca") = Nothing
            Session("CONYUGEID") = Nothing
            Session("PROSPECTO") = Nothing
            Session("YAEXISTE") = Nothing
            Session("AUXILIAR") = "-1"
            llena_vialidad(cmb_vialidad.ID)

            LlenaUsuarios(Session("DESPACHOID_M").ToString)
            txt_despacho_id.Text = "Nueva Despacho"

            If Session("DESPACHOID_M") > 0 Then
                txt_despacho_id.Text = Session("DESPACHOID_M")
                carga_personales(Session("DESPACHOID_M"))
                carga_domicilio()
                carga_contacto(Session("DESPACHOID_M"))
                LlenaUsuarios(Session("DESPACHOID_M").ToString)
                define_avance()
            End If
        End If

        If Session("DESPACHOID_M") > 0 Then
            define_avance()
        End If

    End Sub
    Private Sub LlenaUsuarios(ByVal iddespacho As String)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt As New Data.DataTable()

        lbl_status.Text = ""
        'Lst_usuarios_asignados.Items.Clear()
        'Lst_usuarios_disponibles.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, iddespacho)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_USUARIOS_EXTERNOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt, Session("rs"))
        Session("Con").Close()
        If dt.Rows.Count > 0 Then

            dag_users.Visible = True
            dag_users.DataSource = dt
            dag_users.DataBind()
        Else
            dag_users.Visible = False
        End If

    End Sub
    Private Sub carga_personales(ByVal despachoid As Integer)


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_GENERALES_PM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then


            If Session("rs").fields("RES").value.ToString = "1" Then

                Dim notas As String
                notas = Session("rs").Fields("NOTAS").Value


                If Not Session("DESPACHOID_M") Is Nothing Then
                    txt_despacho_id.Text = Session("DESPACHOID_M").ToString
                    txt_razonsocial.Text = Session("rs").Fields("RAZONSOCIAL").Value
                    txt_comercial.Text = Session("rs").Fields("NOMBRECOMERCIAL").Value
                    txt_nombre1.Text = Session("rs").Fields("NOMBRE1").Value
                    txt_nombre2.Text = Session("rs").Fields("NOMBRE2").Value
                    txt_paterno.Text = Session("rs").Fields("PATERNO").Value
                    txt_materno.Text = Session("rs").Fields("MATERNO").Value
                    txt_rfc.Text = Session("rs").Fields("RFC").Value

                    txt_notas.Text = Session("rs").Fields("NOTAS").Value

                    If Session("ESTATUS_P") Then
                        'cmb_estatus_persona.ClearSelection()
                        'cmb_estatus_persona.Items.FindByValue(Session("rs").Fields("ESTATUS").Value.ToString).Selected = True
                    End If

                End If

            End If


        End If
        Session("Con").Close()
    End Sub

    Private Sub define_avance()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AVANCE_ABOGADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Dim Avance As Integer = CInt(Session("rs").Fields("AVANCE").Value)
            If Avance > 1 Then
                Semaforo1_v.Visible = True
                Semaforo1_r.Visible = False
            End If
            If Avance > 2 Then
                ' Semaforo2_v.Visible = True
                ' Semaforo2_r.Visible = False
            End If
            If Avance > 3 Then
                Semaforo3_v.Visible = True
                Semaforo3_r.Visible = False
            End If
            If Avance > 4 Then
                Semaforo4_v.Visible = True
                Semaforo4_r.Visible = False
            End If
            If Avance > 5 Then
                Semaforo5_v.Visible = True
                Semaforo5_r.Visible = False
            End If
            If Avance > 6 Then
                'Semaforo6_v.Visible = True
                'Semaforo6_r.Visible = False
            End If
        End If
        Session("Con").Close()
        up_semaforos.Update()
    End Sub
    '----------------------------------AVANCE-----------------------------------------

    Private Sub actualiza_avance(ByVal avance As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AVANCE", Session("adVarChar"), Session("adParamInput"), 10, avance)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_DESP_AVANCE_PM"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        'toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        'toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        ' toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub

    Protected Sub lnk_semafor_Click(sender As LinkButton, e As EventArgs)
        Dim up_panel_id As String = sender.ID.Replace("lnk_to_", "upnl_")
        Dim panel_id As String = sender.ID.Replace("lnk_to_", "panel_")

        Try
            Dim up_panel As UpdatePanel = TryCast(FindControlRecursive(Me.Page, up_panel_id), UpdatePanel)
            Dim panel As HtmlGenericControl = TryCast(FindControlRecursive(Me.Page, panel_id), HtmlGenericControl)

            'folderA(panel, "down")
            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptScrollTo", " jQuery('.wrapper').getNiceScroll(0).doScrollTop($('#" & TryCast(FindControlRecursive(Me.Page, "content_" & panel_id), HtmlGenericControl).ClientID & "').position().top, 666);", True)
            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptFolder" & panel.ClientID, "$('#" & panel.ClientID & " .panel_header_folder').click(function(event) {var folder_content =$(this).siblings('.panel-body').children('.panel-body_content'); var toogle = $(this).children('.panel_folder_toogle'); if (toogle.hasClass('up')){toogle.removeClass('up');toogle.addClass('down');folder_content.show('6666', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.parent().css({ 'background': '#FB7A68 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px'});} else if (toogle.hasClass('down')){toogle.removeClass('down');folder_content.hide('333', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.addClass('up');toogle.parent().css({ 'background': '#F8F9F9 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px' });}});", True)

            up_panel.Update()
        Catch ex As Exception

            '  Label1.Text = Label1.Text & "ññ" & panel_id
            ' Label1.Text = Label1.Text & "{--}" & ex.ToString
        End Try

    End Sub
    Public Shared Function FindControlRecursive(root As Control, id As String) As Control
        If root.ID = id Then
            Return root
        End If
        Return root.Controls.Cast(Of Control)().[Select](Function(c) FindControlRecursive(c, id)).FirstOrDefault(Function(c) c IsNot Nothing)
    End Function
    'Protected Sub btn_guardar_l_Click(sender As Object, e As EventArgs) Handles btn_guardar_l.Click
    '    actualiza_avance(6)

    '    define_avance()
    '    lbl_status_lab.Text = "Guardado correctamente"
    'End Sub

    Protected Sub txt_cp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp.TextChanged
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
    End Sub
#Region "Domicilio"
    Private Sub llena_vialidad(ByVal id As String)
        'Procedimiento que obtiene el catálogo de vialidades y las despliega en el combo correspondiente
        Dim elija As New ListItem("ELIJA", "-1")
        If id = "cmb_vialidad" Then
            cmb_vialidad.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_VIALIDAD"
            Session("rs") = Session("cmd").Execute()
            cmb_vialidad.Items.Add(elija)
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
                cmb_vialidad.Items.Add(item)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
        If id = "cmb_tipoviaemp" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_VIALIDAD"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If


    End Sub
#End Region
#Region "Personales"
    '------------------------------VALIDACION DE RFC ---------------------------
    Private Function ValidaRFC(ByVal rfc As String) As Boolean
        Return Regex.IsMatch(rfc, ("^[a-zA-Z]{4}(\d{6})((\D|\d){3})?$"))
    End Function
    Private Sub actualiza_personales()
        Try
            If txt_notas.Text.Length > 2000 Then
                lbl_status.Text = "Error: el número de caracteres en notas no puede exceder 2000"
                Exit Sub
            End If

            lbl_status.Text = ""
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("RAZONSOCIAL", Session("adVarChar"), Session("adParamInput"), 300, txt_razonsocial.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRECOMERCIAL", Session("adVarChar"), Session("adParamInput"), 300, txt_comercial.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, Left(txt_notas.Text, 2000))
            Session("cmd").Parameters.Append(Session("parm"))
            'If Session("ESTATUS_P") Then
            '    Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_estatus_persona.SelectedItem.Value))
            'Else
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 1)
            'End If
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_COB_DESP_GENERALES_PM"
            Session("rs") = Session("cmd").Execute()

            If Session("rs").Fields("RES").Value = 1 Then
                lbl_status.Text = "Guardado correctamente"
            Else
                lbl_status.Text = "Error: la persona editada ya esta dada de alta en el sistema.verifique."
            End If
            Session("Con").Close()
        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally

        End Try

    End Sub
    Private Sub guarda_personales()
        Try
            If txt_notas.Text.Length > 2000 Then
                lbl_status.Text = "Error: el número de caracteres en notas no puede exceder 2000"
                Exit Sub
            End If
            lbl_status.Text = ""
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("RAZONSOCIAL", Session("adVarChar"), Session("adParamInput"), 300, txt_razonsocial.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRECOMERCIAL", Session("adVarChar"), Session("adParamInput"), 300, txt_comercial.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, Left(txt_notas.Text, 2000))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_COB_DESP_GENERALES_PM"
            Session("rs") = Session("cmd").Execute()
            Session("YAEXISTE") = Session("rs").Fields("YAEXISTE").Value

            If Session("YAEXISTE") = "NO" Then

                Session("DESPACHOID_M") = CInt(Session("rs").Fields("IDDESPACHO").Value.ToString)
                Session("PROSPECTO") = Session("rs").Fields("PROSPECTO").Value.ToString
                txt_despacho_id.Text = Session("DESPACHOID_M")
                upd_id.Update()
                lbl_status.Text = "Guardado correctamente"
            Else

                'YA FUE CAPTURADA ESTA PERSONA POR LO QUE NO LA DEJAMOS CONTINUAR
                lbl_status.Text = "Error: el despacho con ese rfc ya fue capturado en el sistema."

                Exit Sub

            End If
        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally
            Session("Con").Close()
        End Try

    End Sub

#End Region
    Private Sub btn_guardar_p_Click(sender As Object, e As EventArgs) Handles btn_guardar_p.Click
        If Session("DESPACHOID_M") > 0 Then
            Try

                actualiza_personales()
                actualiza_avance(2)
            Catch ex As Exception
                lbl_status.Text = ex.ToString
            Finally
                carga_personales(Session("DESPACHOID_M"))
            End Try
        Else
            Try
                If ValidaRFC(txt_rfc.Text) Then
                    guarda_personales()
                    actualiza_avance(2)
                    If Session("YAEXISTE") <> "SI" Then

                    End If
                Else
                    lbl_status.Text = "Error: rfc incorrecto"
                End If

            Catch ex As Exception
                lbl_status.Text = ex.ToString()
            Finally
                carga_personales(Session("DESPACHOID_M"))
            End Try
        End If

    End Sub
#Region "Domicilio"
    Private Sub carga_domicilio()

        Dim id_asen As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("DESPACHOID_M") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID"))
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("cmd").CommandText = "SEL_COB_DESP_DIRECCION_PM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            If Not Session("rs").Fields("CP").Value.ToString = Nothing Then
                txt_cp.Text = Session("rs").Fields("CP").Value
            End If
            id_asen = Session("rs").Fields("ID_ASEN").Value.ToString
            cmb_asentamiento.ClearSelection()
            ' cmb_asentamiento.Items.FindByValue(Session("rs").Fields("ID_ASEN").Value.ToString).Selected = True
            If Not Session("rs").Fields("CALLE").Value.ToString = Nothing Then

                txt_calle.Text = Session("rs").Fields("CALLE").Value
            End If
            If Not Session("rs").Fields("ID_VIAL").Value.ToString = Nothing Then

                cmb_vialidad.ClearSelection()
                cmb_vialidad.Items.FindByValue(Session("rs").Fields("ID_VIAL").Value.ToString).Selected = True
            End If
            If Not Session("rs").Fields("NUM_EXT").Value.ToString = Nothing Then

                txt_exterior.Text = Session("rs").Fields("NUM_EXT").Value
            End If
            If Not Session("rs").Fields("NUM_INT").Value.ToString = Nothing Then

                txt_interior.Text = Session("rs").Fields("NUM_INT").Value
            End If
            If Not Session("rs").Fields("REF").Value.ToString = Nothing Then

                txt_referencias.Text = Session("rs").Fields("REF").Value
            End If

        End If
        Session("Con").Close()

        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()

        If txt_cp.Text = "" Then
            Exit Sub
        End If

        Dim idedo As String, idmuni As String

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP
            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value

            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, idedo)
            cmb_estado.Items.Add(item_edo)
            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            cmb_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)
                cmb_asentamiento.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

        ''si se encontraron estado y municipio validos para el cp ingresado entonces busco las localidades correspondientes
        'If Not idedo Is Nothing And Not idmuni Is Nothing Then

        '    Session("Con").Open()
        '    Session("rs") = CreateObject("ADODB.Recordset")
        '    Session("cmd") = New ADODB.Command()
        '    Session("cmd").ActiveConnection = Session("Con")
        '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        '    Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, idedo)
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("parm") = Session("cmd").CreateParameter("IDMUN", Session("adVarChar"), Session("adParamInput"), 10, idmuni)
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("cmd").CommandText = "SEL_LOCALIDAD_MUNI_EDO"
        '    Session("rs") = Session("cmd").Execute()
        '    If Not Session("rs").EOF Then
        '        Do While Not Session("rs").EOF
        '            'Dim item As New ListItem(Session("rs").Fields("LOCALIDAD").Value.ToString, Session("rs").Fields("IDLOC").Value.ToString)
        '            'cmb_localidad.Items.Add(item)
        '            'Session("rs").movenext()
        '        Loop
        '    End If
        '    Session("Con").Close()
        'End If
        ''
        'cmb_asentamiento.Items.FindByValue(id_asen).Selected = True
        ' cmb_localidad.Items.FindByValue(id_loc).Selected = True

    End Sub
    Private Sub guarda_domicilio()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("DESPACHOID_M") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID"))
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 10, cmb_asentamiento.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, cmb_vialidad.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, txt_calle.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, txt_exterior.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, txt_interior.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, txt_referencias.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_DESP_DIRECCION_PM"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        If Session("DESPACHOID_M") Is Nothing Then
            carga_domicilio()

        Else
            actualiza_avance(3)
            lbl_status_dom.Text = "Guardado correctamente"

        End If
    End Sub

    Private Sub actualiza_domcilio()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("DESPACHOID_M") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID"))
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 10, cmb_asentamiento.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, cmb_vialidad.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, txt_calle.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, txt_exterior.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, txt_interior.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, txt_referencias.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_DESP_DIRECCION_PM"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        If Session("DESPACHOID_M") Is Nothing Then
            carga_domicilio()

        Else
            actualiza_avance(3)
            lbl_status_dom.Text = "Guardado correctamente"
            carga_domicilio()
        End If
    End Sub
#End Region

    Private Sub btn_guardar_domicilio_Click(sender As Object, e As EventArgs) Handles btn_guardar_domicilio.Click
        If Session("DESPACHOID_M") > 0 Then
            Try

                actualiza_domcilio()
                actualiza_avance(3)


            Catch ex As Exception
                lbl_status_dom.Text = ex.ToString
            Finally
                carga_domicilio()
            End Try
        Else
            Try

                guarda_domicilio()
                If Session("YAEXISTE") <> "SI" Then
                    llena_vialidad(cmb_vialidad.ID)
                    actualiza_avance(3)
                End If


            Catch ex As Exception
                lbl_status_dom.Text = ex.ToString()
            Finally
                carga_domicilio()
            End Try
        End If
    End Sub
#Region "Codigo Postal"
    Private Sub busquedaCP(ByVal CP As String)
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
        If txt_cp.Text = "" Then
            Exit Sub
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, CP)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        Dim idedo As String = ""
        Dim idmuni As String = ""

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP

            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString
            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, idedo)
            cmb_estado.Items.Add(item_edo)
            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            cmb_municipio.Items.Add(item_mun)
            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                cmb_asentamiento.Items.Add(item)
                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()

    End Sub
#End Region
    Private Sub btn_buscadat_Click(sender As Object, e As ImageClickEventArgs) Handles btn_buscadat.Click
        busquedaCP(txt_cp.Text)
    End Sub


#Region "Contacto"

    Function validacion_info_contacto() As Boolean
        lbl_statustel.Text = ""

        Dim res As Boolean = True
        'REVISO SI SE CAPTURARON LOS DATOS  INDISPENSABLES PARA GUARDAR LOS TELEFONOS Y LOS GUARDO (7 digitos minimo tel  y 2 de lada)
        'PARTICULAR
        Dim c1_cel As Integer, c1_ofi As Integer, c1_lada As Integer, c1_lada_ofi As Integer
        Dim c2_cel As Integer, c2_ofi As Integer, c2_lada As Integer, c2_lada_ofi As Integer


        c1_cel = txt_c1_tel.Text.Length
        c1_ofi = txt_c1_tel_ofi.Text.Length
        c1_lada = txt_c1_lada.Text.Length
        c1_lada_ofi = txt_c1_lada_ofi.Text.Length

        c2_cel = txt_c2_tel.Text.Length
        c2_ofi = txt_c2_tel_ofi.Text.Length
        c2_lada = txt_c2_lada.Text.Length
        c2_lada_ofi = txt_c2_lada_ofi.Text.Length


        'MOVIL
        If c1_cel > 0 Then
            If c1_cel >= 7 And c1_lada >= 2 Then
                res = True
            Else
                lbl_statustel.Text = "Error: clave lada o teléfono incompletos."
                res = False
            End If
        End If

        'TRABAJO
        If c1_ofi > 0 Then
            If c1_ofi >= 7 And c1_lada_ofi >= 2 Then
                res = True
            Else
                lbl_statustel.Text = "Error: clave lada o teléfono incompletos."
                res = False
            End If
        End If

        'CONTACTO 2

        'MOVIL
        If c2_cel > 0 Then
            If c2_cel >= 7 And c2_lada >= 2 Then
                res = True
            Else
                lbl_statustel.Text = "Error: clave lada o teléfono incompletos."
                res = False
            End If
        End If

        'TRABAJO
        If c2_ofi > 0 Then
            If c2_ofi >= 7 And c2_lada_ofi >= 2 Then
                res = True
            Else
                lbl_statustel.Text = "Error: clave lada o teléfono incompletos."
                res = False
            End If
        End If


        Return res
    End Function

    Private Sub carga_contacto(ByVal despachoid As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 15, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_CONTACTOS_PM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            'lbl_c1_tel.Text = "(" + Session("rs").Fields("C1_LADA").Value.ToString + ") " + Session("rs").Fields("C1_TEL").Value.ToString
            'lbl_c1_tel_ofi.Text = IIf(Session("rs").Fields("C1_LADA_OFI").Value.ToString = "", "-", "(" + Session("rs").Fields("C1_LADA_OFI").Value.ToString + ") " + Session("rs").Fields("C1_TEL_OFI").Value.ToString) +
            '    IIf(Session("rs").Fields("C1_EXT_OFI").Value.ToString = "", "", "  EXT: " + Session("rs").Fields("C1_EXT_OFI").Value.ToString)

            'lbl_c2_tel.Text = IIf(Session("rs").Fields("C2_LADA").Value.ToString = "", "-", "(" + Session("rs").Fields("C2_LADA").Value.ToString + ") " + Session("rs").Fields("C2_TEL").Value.ToString)
            'lbl_c2_tel_ofi.Text = IIf(Session("rs").Fields("C2_LADA_OFI").Value.ToString = "", "-", "(" + Session("rs").Fields("C2_LADA_OFI").Value.ToString + ") " + Session("rs").Fields("C2_TEL_OFI").Value.ToString) +
            '    IIf(Session("rs").Fields("C2_EXT_OFI").Value.ToString = "", "", "  EXT: " + Session("rs").Fields("C2_EXT_OFI").Value.ToString)

            'lbl_c1_email.Text = Session("rs").Fields("C1_EMAIL").Value.ToString
            'lbl_c2_email.Text = IIf(Session("rs").Fields("C2_EMAIL").Value.ToString = "", "-", Session("rs").Fields("C2_EMAIL").Value.ToString)


            'lbl_c1_nombre.Text = Session("rs").Fields("C1_NOMBRE1").Value + " " + Session("rs").Fields("C1_NOMBRE2").Value + " " + Session("rs").Fields("C1_PATERNO").Value + " " + Session("rs").Fields("C1_MATERNO").Value
            'lbl_c2_nombre.Text = IIf(Session("rs").Fields("C2_NOMBRE1").Value.ToString = "", "-", Session("rs").Fields("C2_NOMBRE1").Value + " " + Session("rs").Fields("C2_NOMBRE2").Value + " " + Session("rs").Fields("C2_PATERNO").Value + " " + Session("rs").Fields("C2_MATERNO").Value)


            If Not Session("DESPACHOID_M") Is Nothing Then
                txt_c1_lada.Text = Session("rs").fields("C1_LADA").value.ToString
                txt_c1_tel.Text = Session("rs").fields("C1_TEL").value.ToString
                txt_c1_lada_ofi.Text = Session("rs").fields("C1_LADA_OFI").value.ToString
                txt_c1_tel_ofi.Text = Session("rs").fields("C1_TEL_OFI").value.ToString
                txt_c1_ext_ofi.Text = Session("rs").fields("C1_EXT_OFI").value.ToString
                txt_c1_email.Text = Session("rs").fields("C1_EMAIL").value.ToString

                txt_c1_materno.Text = Session("rs").fields("C1_MATERNO").value.ToString
                txt_c1_paterno.Text = Session("rs").fields("C1_PATERNO").value.ToString
                txt_c1_nombre.Text = Session("rs").fields("C1_NOMBRE1").value.ToString
                txt_c1_seg_nombre.Text = Session("rs").fields("C1_NOMBRE2").value.ToString

                txt_c2_lada.Text = Session("rs").fields("C2_LADA").value.ToString
                txt_c2_tel.Text = Session("rs").fields("C2_TEL").value.ToString
                txt_c2_lada_ofi.Text = Session("rs").fields("C2_LADA_OFI").value.ToString
                txt_c2_tel_ofi.Text = Session("rs").fields("C2_TEL_OFI").value.ToString
                txt_c2_ext_ofi.Text = Session("rs").fields("C2_EXT_OFI").value.ToString
                txt_c2_email.Text = Session("rs").fields("C2_EMAIL").value.ToString


                txt_c2_materno.Text = Session("rs").fields("C2_MATERNO").value.ToString
                txt_c2_paterno.Text = Session("rs").fields("C2_PATERNO").value.ToString
                txt_c2_nombre.Text = Session("rs").fields("C2_NOMBRE1").value.ToString
                txt_c2_seg_nombre.Text = Session("rs").fields("C2_NOMBRE2").value.ToString


            End If



        End If
        Session("Con").Close()
    End Sub



    Private Sub guardacontactos()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("DESPACHOID_M") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID"))
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("C1_NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_c1_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_c1_seg_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_c1_paterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_c1_materno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_LADA", Session("adVarChar"), Session("adParamInput"), 5, txt_c1_lada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_TEL", Session("adVarChar"), Session("adParamInput"), 15, txt_c1_tel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_LADA_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c1_lada_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_TEL_OFI", Session("adVarChar"), Session("adParamInput"), 15, txt_c1_tel_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_EXT_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c1_ext_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_EMAIL", Session("adVarChar"), Session("adParamInput"), 100, txt_c1_email.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_c2_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_c2_seg_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_c2_paterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_c2_materno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_LADA", Session("adVarChar"), Session("adParamInput"), 5, txt_c2_lada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_TEL", Session("adVarChar"), Session("adParamInput"), 15, txt_c2_tel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_LADA_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c2_lada_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_TEL_OFI", Session("adVarChar"), Session("adParamInput"), 15, txt_c2_tel_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_EXT_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c2_ext_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_EMAIL", Session("adVarChar"), Session("adParamInput"), 15, txt_c2_email.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_DESP_CONTACTOS_PM"
        Session("cmd").Execute()
        Session("Con").Close()
        If Session("DESPACHOID_M") Is Nothing Then
            carga_contacto(Session("DESPACHOID_M"))

        Else
            lbl_statustel.Text = "Guardado correctamente"
            carga_contacto(Session("DESPACHOID_M"))
            actualiza_avance(4)
        End If

    End Sub

    Private Sub actualiza_contacto()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("DESPACHOID_M") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID"))
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("C1_NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_c1_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_c1_seg_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_c1_paterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_c1_materno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_LADA", Session("adVarChar"), Session("adParamInput"), 5, txt_c1_lada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_TEL", Session("adVarChar"), Session("adParamInput"), 15, txt_c1_tel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_LADA_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c1_lada_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_TEL_OFI", Session("adVarChar"), Session("adParamInput"), 15, txt_c1_tel_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_EXT_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c1_ext_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C1_EMAIL", Session("adVarChar"), Session("adParamInput"), 100, txt_c1_email.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_c2_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_c2_seg_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_c2_paterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_c2_materno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_LADA", Session("adVarChar"), Session("adParamInput"), 5, txt_c2_lada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_TEL", Session("adVarChar"), Session("adParamInput"), 15, txt_c2_tel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_LADA_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c2_lada_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_TEL_OFI", Session("adVarChar"), Session("adParamInput"), 15, txt_c2_tel_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_EXT_OFI", Session("adVarChar"), Session("adParamInput"), 5, txt_c2_ext_ofi.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("C2_EMAIL", Session("adVarChar"), Session("adParamInput"), 100, txt_c2_email.Text)

        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_DESP_CONTACTOS_PM"
        Session("cmd").Execute()
        Session("Con").Close()
        If Session("DESPACHOID_M") Is Nothing Then
            carga_contacto(Session("DESPACHOID_M"))
        Else
            carga_contacto(Session("DESPACHOID_M"))
            actualiza_avance(4)
            lbl_statustel.Text = "Guardado correctamente"
        End If

    End Sub

    Private Sub btn_guardar_c_Click(sender As Object, e As EventArgs) Handles btn_guardar_c.Click
        If Session("DESPACHOID_M") > 0 Then
            Try
                Dim ESTATUS As Integer
                select_avance(ESTATUS)
                If ESTATUS >= 3 Then
                    actualiza_contacto()
                    actualiza_avance(4)
                Else
                    lbl_statustel.Text = "No puede elegir esta opción hasta que tenga registrado los apartados anteriores"
                End If
            Catch ex As Exception
                lbl_status.Text = ex.ToString
            Finally
                carga_contacto(Session("DESPACHOID_M"))
            End Try
        Else
            Try

                lbl_statustel.Text = ""
                Dim ESTATUS As Integer
                If ESTATUS >= 3 Then
                    guardacontactos()
                    actualiza_avance(4)
                Else
                    lbl_statustel.Text = "No puede elegir esta opción hasta que tenga registrado los apartados anteriores"
                End If

                If Session("YAEXISTE") <> "SI" Then
                    actualiza_avance(4)
                End If

            Catch ex As Exception
                lbl_status.Text = ex.ToString()
            Finally
                carga_contacto(Session("DESPACHOID_M"))
            End Try
        End If
    End Sub
    Private Sub AsignaUsuarios(ByVal IDDESPACHO As String, ByVal iduser As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, IDDESPACHO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIOEXTERNO", Session("adVarChar"), Session("adParamInput"), 10, iduser)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_DESP_USUARIOS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub
    Public Sub select_avance(ByRef ESTATUS As Integer)
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPendientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()

        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, Session("DESPACHOID_M"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 1, "M")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_ESTATUS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPendientes, Session("rs"))

        ESTATUS = dtPendientes.Rows(0)("ESTATUS").ToString

        Session("Con").Close()



    End Sub
    Private Sub btn_adduser_Click(sender As Object, e As EventArgs) Handles btn_adduser.Click
        Dim ESTATUS As Integer
        select_avance(ESTATUS)
        If ESTATUS >= 4 Then
            'Data table que se llena con los contenidos de los datagrids
            Dim dt As New Data.DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("USUARIO", GetType(String))
            dt.Columns.Add("ASIGNADO", GetType(Integer))

            Dim idusuario As String
            If Not Session("DESPACHOID_M") Is Nothing Then
                idusuario = Session("DESPACHOID_M").ToString
            Else
                idusuario = Session("DESPACHOID").ToString
            End If
            For i As Integer = 0 To dag_users.Rows.Count() - 1
                dt.Rows.Add(CInt(dag_users.Rows(i).Cells(0).Text), dag_users.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_users.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked))
            Next
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_COB_DESP_USUARIOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDEVENTO", SqlDbType.Int)
                Session("parm").Value = idusuario
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("ASIG", SqlDbType.Structured)
                Session("parm").Value = dt
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))
                '  Execute the command.
                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()
                    Session("AUXILIAR") = myReader.GetInt32(0).ToString
                    lbl_status.Text = Session("AUXILIAR").ToString

                End While
                myReader.Close()

            End Using

            Try


            Catch ex As Exception

            Finally
                Session("AUXILIAR") = Nothing
                lbl_status_user.Text = "Guardado correctamente"
                LlenaUsuarios(idusuario)
                actualiza_avance(5)
                define_avance()
            End Try

        Else
            lbl_status_user.Text = "No puede elegir esta opción hasta que tenga registrado los apartados anteriores"
        End If


    End Sub


#End Region

End Class