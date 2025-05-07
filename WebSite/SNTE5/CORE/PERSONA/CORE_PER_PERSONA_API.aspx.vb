Imports System.Runtime.InteropServices.WindowsRuntime
Imports Newtonsoft.Json

Public Class CORE_PER_PERSONA_API
    Inherits System.Web.UI.Page
    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        TryCast(Master, MasterMascore).CargaASPX("Activación de Cuenta Movil", "ACTIVACIÓN DE APP MOVIL")

        If Not IsPostBack Then

            If Session("VENGODE") = "CRED_EXP_GENERAL" Then
                Session("VENGODE") = "CORE_PER_PERSONA"
                tbx_rfc.Text = Session("NUMTRAB").ToString()
                obtieneId()
                'lnk_busqueda_coyuge.Attributes.Add("OnClick", "busquedapersonafisica()")
                div_NombrePersonaBusqueda.Visible = True
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            End If

            Verifica_facultad()
            Session("idperbusca") = Nothing
            Session("CONYUGEID") = Nothing
            Session("INSTCON") = Nothing
            Session("PROSPECTO") = Nothing
            Session("insti") = Nothing
            'carga_estatus()       '<-- Carga ddl con catálogo de estatus del afiliado
            'cargaPeriodos()       '<-- Carga ddl con catálogo de periodicidades de pago

            btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        End If

        If Me.IsPostBack Then
            If Session("idperbusca") <> Nothing Then
                If hdn_origen_busquedas.Value = "conyuge" Then
                    Session("CONYUGEID") = CInt(Session("idperbusca"))
                    Session("insti") = CInt(Session("INSTITUCION"))
                    'carga_datos_conyuge_busqueda()
                    hdn_origen_busquedas.Value = ""
                    Session("idperbusca") = Nothing
                    Session("INSTITUCION") = Nothing
                Else
                    tbx_rfc.Text = Session("idperbusca").ToString
                    Session("CLIENTE") = Session("PROSPECTO").ToString
                    lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                    div_NombrePersonaBusqueda.Visible = True
                    Session("idperbusca") = Nothing
                    Session("INSTITUCION") = Nothing
                    validaTrabajador()
                End If
            End If

            'If hdn_origen_busquedas.Value = Nothing Then
            '    Session("INSTITUCION") = ddl_InstitucionesMAIN.SelectedValue
            'End If

            'lnk_busqueda_coyuge.Attributes.Add("OnClick", "busquedapersonafisica()")

            If ScriptManager.GetCurrent(Me).IsInAsyncPostBack Then

                Dim smUniqueId As String = ScriptManager.GetCurrent(Page).UniqueID
                Dim smFieldValue As String = Request.Form(smUniqueId)
                Dim id As String = ""
                If (Not String.IsNullOrEmpty(smFieldValue)) And smFieldValue.Contains("|") Then

                    If smFieldValue.Split("|")(0).Contains("upnl_") Then
                        id = smFieldValue.Split("|")(0).Replace("upnl_", "panel_")
                        id = id.Replace("$", "_")

                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptFolder" & id, "$('#" & id & " .panel_header_folder').click(function(event) {var folder_content =$(this).siblings('.panel-body').children('.panel-body_content'); var toogle = $(this).children('.panel_folder_toogle'); if (toogle.hasClass('up')){toogle.removeClass('up');toogle.addClass('down');folder_content.show('6666', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.parent().css({ 'background': '#696462 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px'});} else if (toogle.hasClass('down')){toogle.removeClass('down');folder_content.hide('333', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.addClass('up');toogle.parent().css({ 'background': '#696462 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px' });}});", True)
                        folderA(FindControlRecursive(Me.Page, id.Substring(id.IndexOf("panel_"))), "down", False)

                    End If

                End If

            End If


        End If


    End Sub

    Private Sub BuscarIDCliente()
        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If
        Session("Con").Close()

        If Existe = -1 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de control"
            lbl_NombrePersonaBusqueda.Text = ""
            div_NombrePersonaBusqueda.Visible = False
            upnl_contacto.Visible = False
            Session("PERSONAID") = txt_IdCliente.Text
        Else
            lbl_statusc.Text = ""
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("PERSONAID") = txt_IdCliente.Text
        End If

    End Sub

    Private Sub Verifica_facultad()
        ' Verifica si el usuario tiene la facultad de edición de información (ID 116) para poder editar la información de los afiliados
        Dim facultad As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ANAEXP_PERMITE_EDICION_AFILIADOS"
        Session("rs") = Session("cmd").Execute()

        facultad = Session("rs").Fields("FACULTAD").value.ToString()
        Session("Con").Close()

        If facultad <> "1" Then
            oculta_botones_facultad()
        Else
            muestra_botones_facultad()
        End If
    End Sub

    Private Sub oculta_botones_facultad()
        btn_guardar_contactos.Visible = False
    End Sub

    Private Sub muestra_botones_facultad()
        btn_guardar_contactos.Visible = True
    End Sub


    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        Session("Con").Close()

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            upnl_contacto.Visible = True
            CargaInfoContacto()
            carga_telefonos()
        End If

    End Sub

    Private Sub validaTrabajador()
        lbl_statusc.Text = ""
        lbl_estatus_contacto.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: RFC incorrecto"
                lbl_NombrePersonaBusqueda.Text = ""
                div_NombrePersonaBusqueda.Visible = False
                upnl_contacto.Visible = False
            Else
                Session("CLIENTE") = Session("ID")
                Session("PROSPECTO") = Nothing
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
        End If
    End Sub

    Protected Sub lnk_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        limpia_datos()
        validaTrabajador()

    End Sub

    Protected Sub btn_busqueda_persona(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BusquedaPersona.Click
        'folderA(panel_contacto, "up")
    End Sub

    Private Function Verifica_FechaNac_Curp_RFC(ByVal fecha As String, ByVal Curp As String, ByVal rfc As String) As Boolean
        ' Verifica fecha de nacimiento, CURP y RFC 
        Dim correcto As Boolean
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FECHA_NAC", Session("adVarChar"), Session("adParamInput"), 10, fecha)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CURP", Session("adVarChar"), Session("adParamInput"), 18, Curp)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, rfc)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_VALIDACION_FNAC_CURP_RFC"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                If Session("rs").Fields("RESPUESTA").value.ToString = "OK" Then
                    correcto = True
                Else
                    correcto = False
                End If
            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
        Return correcto
    End Function

#Region "Información de Contacto"

    Protected Sub btn_guardar_contactos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar_contactos.Click

        If Len(txt_telcasa.Text) > 0 Then
            If Len(txt_telcasa.Text) <> 10 Then
                lbl_estatus_contacto.Text = "Error: El número de teléfono no puede ser menor a 10 dígitos."
                Exit Sub
            End If
        End If

        If txt_correo.Text <> "" Then
            If ValidaCorreo(txt_correo.Text) = False Then
                lbl_estatus_contacto.Text = "Error: El correo tiene formato invalido."
                Exit Sub
            End If
        End If

        GuardarInfoContacto()
        CargaInfoContacto()

    End Sub

    Protected Sub btn_activar_app_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_activar_app.Click

        '''''Creación de cuenta de la app
        Dim model As CrearCuentaAppModel = New CrearCuentaAppModel()
        model.ID_PROCEDENCIA = "1"
        model.ID_PERSONA = txt_IdCliente.Text
        model.USER_NAME = tbx_rfc.Text
        model.TELEFONO = txt_telcasa.Text
        model.CORREO = txt_correo.Text
        model.CONTRASENIA = String.Empty
        model.USER_TRANS = Session("USERID")

        Dim cw As New ControlsWeb()
        Dim respAPI As String = cw.PostCreaCtaApp(model)
        Dim notf As RespuestaCrearCta = JsonConvert.DeserializeObject(Of RespuestaCrearCta)(respAPI)
        Dim responseCode As String = notf.responseCode
        Dim responseMessage As String = notf.responseMessage

        If responseCode = "200" Then
            lbl_estatus_contacto.Text = "Creación de cuenta exitosa."
        Else
            lbl_estatus_contacto.Text = "Error: " + responseMessage
        End If


        ''''Contraseña
        Dim jsonAPIPwd As String = cw.PostContraseña(tbx_rfc.Text)
        Dim respAPIPwd As RespuestaContraseña = JsonConvert.DeserializeObject(Of RespuestaContraseña)(jsonAPIPwd)
        Dim responseCodePwd As String = respAPIPwd.responseCode
        Dim responseMessagePwd As String = respAPIPwd.responseMessage
        Dim dataPwd As List(Of dataContraseña) = respAPIPwd.data.ToList()

        Dim Result As String, MailUsr As String, CodeMail As String, MessageMail As String

        For i As Integer = 0 To dataPwd.Count - 1
            Result = dataPwd(i).Result
            MailUsr = dataPwd(i).MailUsr
            CodeMail = dataPwd(i).CodeMail
            MessageMail = dataPwd(i).MessageMail
        Next

    End Sub


    Private Sub GuardarInfoContacto()

        Try

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 150, txt_correo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 10, txt_telcasa.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_DATOS_CONTACTO_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()

        Catch ex As Exception

            lbl_estatus_contacto.Text = "1.- " + ex.ToString

        Finally

            lbl_estatus_contacto.Text = "Información guardada correctamente"

        End Try

    End Sub

    Private Sub CargaInfoContacto()

        Try

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CORREO_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                txt_correo.Text = Session("rs").FIELDS("EMAIL").value.ToString
            End If
            Session("Con").close()

        Catch ex As Exception
            lbl_estatus_contacto.Text = "2.- " + ex.ToString
        Finally

        End Try

    End Sub

    Private Sub carga_telefonos()
        ' Carga télefonos del afiliado
        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_TELEFONOS_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                Do While Not Session("rs").EOF

                    txt_telcasa.Text = Session("rs").Fields("TEL").Value

                    Session("rs").movenext()
                Loop
            End If
        Catch ex As Exception
            lbl_estatus_contacto.Text = "3.- " + ex.ToString
        Finally
            Session("Con").close()
        End Try
    End Sub

#End Region


    Private Sub limpia_datos()
        'folderA(panel_contacto, "up")
        'Datos de contacto del afiliado'
        txt_telcasa.Text = ""
        txt_correo.Text = ""
        lbl_estatus_contacto.Text = ""
        lbl_statusc.Text = ""

    End Sub

    Private Function ValidaCURP(ByVal curp As String) As Boolean
        ' Valida el formato de la CURP
        Return Regex.IsMatch(curp, ("^[a-zA-Z]{3,4}(\d{6})[hmHM]{1}[a-zA-Z]{4,5}[0-9A-Z]{1}[0-9A-Z]{1}"))
    End Function

    Private Function ValidaRFC(ByVal cRfc As String) As Boolean
        ' Valida el formato del RFC
        Return Regex.IsMatch(cRfc, ("^[a-zA-Z]{4}(\d{6})((\D|\d){3})?$"))
    End Function

    Private Function VerificaFechaNac(ByVal fecha As String) As Boolean
        ' Valida la fecha de nacimiento
        Dim correcto As Boolean = True
        Try
            If DateDiff(DateInterval.Year, CDate(fecha), Now()) > 150 Or (DateAdd(DateInterval.Day, -1, Now()) <= CDate(fecha)) Then
                correcto = False
            End If
        Catch ex As Exception
        End Try
        Return correcto
    End Function

    Private Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            If toogle.Attributes("class").IndexOf("down") >= 0 Then
                content.Attributes.CssStyle.Add("display", "block")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "#fff")
                head.Attributes.CssStyle.Add("border", "solid 1px transparent")
                head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptS" & pnl.ClientID, "$('#" & content.ClientID & "').show('6666',null);", True)
            End If

        ElseIf accion.Equals("up") Then
            If toogle.Attributes("class").IndexOf("up") >= 0 Then
                content.Attributes.CssStyle.Add("display", "none")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "inherit")
                head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
                head.Attributes.CssStyle.Add("border-radius", "4px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptH" & pnl.ClientID, "$('#" & content.ClientID & "').hide('6666',null);", True)
            End If
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub
    Private Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String, ByVal efecto As Boolean)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            If toogle.Attributes("class").IndexOf("down") >= 0 Then
                content.Attributes.CssStyle.Add("display", "block")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "#fff")
                head.Attributes.CssStyle.Add("border", "solid 1px transparent")
                head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
                If efecto Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptS" & pnl.ClientID, "$('#" & content.ClientID & "').show('6666',null);", True)
                Else
                    content.Attributes.CssStyle.Add("display", "block")
                End If
            End If

        ElseIf accion.Equals("up") Then
            If toogle.Attributes("class").IndexOf("up") >= 0 Then
                content.Attributes.CssStyle.Add("display", "none")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "inherit")
                head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
                head.Attributes.CssStyle.Add("border-radius", "4px")
                If efecto Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptH" & pnl.ClientID, "$('#" & content.ClientID & "').hide('6666',null);", True)
                Else
                    content.Attributes.CssStyle.Add("display", "none")
                End If
            End If
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion
    End Sub


    Public Shared Function FindControlRecursive(root As Control, id As String) As Control
        If root.ID = id Then
            Return root
        End If

        Return root.Controls.Cast(Of Control)().[Select](Function(c) FindControlRecursive(c, id)).FirstOrDefault(Function(c) c IsNot Nothing)
    End Function

    Sub Dofocus(ByVal control As Control)
        Dim scm As ScriptManager
        scm = ScriptManager.GetCurrent(Page)
        scm.SetFocus(control)
    End Sub

#Region "Herramientas"

    Private Function ValidaCorreo(ByVal correo As String) As Boolean
        Return Regex.IsMatch(correo, ("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$"))
    End Function

#End Region


End Class