Imports System.Runtime.InteropServices.WindowsRuntime

Public Class CORE_PER_PERSONA
    Inherits System.Web.UI.Page
    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        TryCast(Master, MasterMascore).CargaASPX("Edición de información del Agremiado", "Edición de Agremiados")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            cargaRegiones()
            cargaDelegaciones()
            cargaCCT()
            cargaMotivos()


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
            bancos()

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

    'Private Sub carga_datos_conyuge_busqueda()
    '    ' Carga los datos de la persona que se buscó para declararse como cónyuge

    '    Try
    '        Session("Con").Open()
    '        Session("cmd") = New ADODB.Command()
    '        Session("cmd").ActiveConnection = Session("Con")
    '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, Session("CONYUGEID"))
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 50, Session("insti"))
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("cmd").CommandText = "SEL_BUSQUEDA_IDCON"
    '        Session("rs") = Session("cmd").Execute()
    '        If Not Session("rs").EOF Then
    '            control_institu.Visible = True
    '            txt_conyuge.Text = Session("rs").Fields("NOMBRE1").Value.ToString
    '            txt_conyuge2.Text = Session("rs").Fields("NOMBRE2").Value.ToString
    '            txt_paternoc.Text = Session("rs").Fields("PATERNO").Value.ToString
    '            txt_maternoc.Text = Session("rs").Fields("MATERNO").Value.ToString
    '            txt_fechaconyuge.Text = Session("rs").Fields("FECHA_NAC").Value.ToString
    '            txt_CURPc.Text = Session("rs").Fields("CURP").Value.ToString
    '            txt_RFCc.Text = Session("rs").Fields("RFC").Value.ToString
    '            txt_control_conyuge.Text = Session("rs").Fields("NUMCONT").Value.ToString
    '            txt_inst_conyuge.Text = Session("rs").Fields("INSTIT").Value.ToString
    '            If Session("rs").Fields("SEXO").Value = "H" Then
    '                conyuge_sexo1.Checked = True
    '                conyuge_sexo2.Checked = False
    '            ElseIf Session("rs").Fields("SEXO").Value = "M" Then
    '                conyuge_sexo1.Checked = False
    '                conyuge_sexo2.Checked = True
    '            Else
    '                conyuge_sexo1.Checked = False
    '                conyuge_sexo2.Checked = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '    Finally
    '        Session("Con").Close()
    '    End Try
    'End Sub

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
            upnl_generales.Visible = False
            'upnl_adicionales.Visible = False
            upnl_domicilio.Visible = False
            upnl_contacto.Visible = False
            'upnl_laborales.Visible = False
            upnl_bancarios.Visible = False
            upnl_estatus_trab.Visible = False
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
        ' Oculta los botones de guardado si no se tiene la facultad de edición de infomración
        btn_guardar_g.Visible = False
        'btn_guardar_a.Visible = False
        btn_guardar_domicilio.Visible = False
        btn_guardar_contactos.Visible = False
        'btn_guardar_l.Visible = False
        btn_guarda_bank.Visible = False
        Btn_Guardar_Estatus.Visible = False
    End Sub

    Private Sub muestra_botones_facultad()
        ' Muestra los botones de guardado si se tiene la facultad de edición de infomración
        btn_guardar_g.Visible = True
        'btn_guardar_a.Visible = True
        btn_guardar_domicilio.Visible = True
        btn_guardar_contactos.Visible = True
        'btn_guardar_l.Visible = True
        btn_guarda_bank.Visible = True
        Btn_Guardar_Estatus.Visible = True
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
            Session("NUMTRAB") = txt_rfc.Text
            upnl_generales.Visible = True
            ' upnl_adicionales.Visible = True
            upnl_domicilio.Visible = True
            upnl_contacto.Visible = True
            'upnl_laborales.Visible = True
            upnl_bancarios.Visible = True
            upnl_estatus_trab.Visible = True
            carga_generales()
            'carga_Adicionales()
            carga_Domiciliarios()
            CargaInfoContacto()
            'carga_info_plaza()
            carga_telefonos()
            'carga_edo_civil()
            'carga_conyuges_externos()
            CargaBancos()
        End If

    End Sub

    Private Sub validaTrabajador()
        lbl_statusc.Text = ""
        lbl_status.Text = ""
        lbl_status_dom.Text = ""
        'lbl_status_adi.Text = ""
        'lbl_status_lab.Text = ""
        lbl_estatus_contacto.Text = ""
        txt_notas.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: RFC incorrecto"
                lbl_NombrePersonaBusqueda.Text = ""
                div_NombrePersonaBusqueda.Visible = False
                upnl_generales.Visible = False
                'upnl_adicionales.Visible = False
                upnl_domicilio.Visible = False
                upnl_contacto.Visible = False
                'upnl_laborales.Visible = False
                upnl_bancarios.Visible = False
                upnl_estatus_trab.Visible = False
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
        txt_clabe.Text = ""
        cmb_banco.ClearSelection()
        txt_clabe_conf.Text = ""
        lbl_estatus_bank.Text = ""
        txt_medio_paga.Text = ""
        folderA(panel_generales, "up")
        'folderA(panel_adicionales, "up")
        folderA(panel_domicilio, "up")
        folderA(panel_contacto, "up")
        'folderA(panel_laborales, "up")
        folderA(panel_bancarios, "up")
        folderA(panel_estatus, "up")
        lbl_cambio_estatus.Text = ""

    End Sub

    Private Sub bancos()
        ' Carga cátalogo de bancos disponibles
        cmb_banco.Items.Clear()
        cmb_banco.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_banco.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub cmb_banco_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmb_banco.SelectedIndexChanged

        If cmb_banco.SelectedItem.Text = "SCOTIABANK" Then
            txt_medio_paga.Text = "NUMERO DE CUENTA"
            txt_clabe.Text = ""
            lbl_estatus_bank.Text = ""
        Else
            txt_medio_paga.Text = "CLABE INTERBANCARIA"
            txt_clabe.Text = ""
            lbl_estatus_bank.Text = ""
        End If

        If cmb_banco.SelectedValue = -1 Then
            txt_clabe.Text = ""
            txt_medio_paga.Text = ""
            lbl_estatus_bank.Text = ""
        End If

    End Sub
    'Private Sub cargaPeriodos()
    '    ' Carga cátalogo de periodicidad de pago y lo muestra en cmb_persuel
    '    cmb_persuel.Items.Clear()
    '    cmb_persuel.Items.Add(New ListItem("ELIJA", "-1"))
    '    Try
    '        Session("cmd") = New ADODB.Command()
    '        Session("Con").Open()
    '        Session("cmd").ActiveConnection = Session("Con")
    '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '        Session("cmd").CommandText = "SEL_PERIODICIDADES"
    '        Session("rs") = Session("cmd").Execute()

    '        Do While Not Session("rs").EOF
    '            cmb_persuel.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("CLAVE").Value.ToString))
    '            Session("rs").movenext()
    '        Loop
    '    Catch ex As Exception
    '    Finally
    '        Session("Con").Close()
    '    End Try
    'End Sub

    Private Sub cargaRegiones()
        ' Carga cátalogo de periodicidad de pago y lo muestra en cmb_persuel
        ddl_region.Items.Clear()
        ddl_region.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_REGIONES"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                ddl_region.Items.Add(New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value.ToString))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub cargaDelegaciones()
        ' Carga cátalogo de periodicidad de pago y lo muestra en cmb_persuel
        ddl_delegacion.Items.Clear()
        ddl_delegacion.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DELEGACIONES"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                ddl_delegacion.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value.ToString))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub cargaCCT()
        ' Carga cátalogo de periodicidad de pago y lo muestra en cmb_persuel
        ddl_cct.Items.Clear()
        ddl_cct.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CENTROS_TRABAJO"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                ddl_cct.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value.ToString))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub carga_generales()

        Try

            Session("Con") = CreateObject("ADODB.Connection")
            Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Session("Con").ConnectionTimeout = 240
            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_GENERALES_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then

                txt_agremiado.Text = Session("rs").FIELDS("AGREMIADO").value.ToString
                txt_curp.Text = Session("rs").FIELDS("CURP").Value.ToString
                txt_rfc.Text = Session("rs").FIELDS("RFC").value.ToString
                txt_fecha.Text = Session("rs").FIELDS("FECHANAC").value.ToString
                If Session("rs").FIELDS("SEXO").Value = "H" Then
                    rad_sexcon2.Checked = False
                    rad_sexcon1.Checked = True
                ElseIf Session("rs").FIELDS("SEXO").Value = "M" Then
                    rad_sexcon1.Checked = False
                    rad_sexcon2.Checked = True
                Else
                    rad_sexcon1.Checked = False
                    rad_sexcon2.Checked = False
                End If
                txt_notas.Text = Session("rs").FIELDS("NOTAS").value.ToString
                ddl_cct.SelectedValue = Session("rs").Fields("CCT").value.ToString
                ddl_region.SelectedValue = Session("rs").Fields("REGION").value.ToString
                ddl_delegacion.SelectedValue = Session("rs").Fields("DELEGACION").value.ToString
                Combo_Estatus.SelectedValue = Session("rs").Fields("ESTATUS").value.ToString
                Notas_baja.Text = Session("rs").Fields("NOTAS").value.ToString

            End If

        Catch ex As Exception
        Finally

            Session("Con").Close()

        End Try

    End Sub

    Private Sub carga_Domiciliarios()
        ' Carga datos de domicilio del trabajador
        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_DOMICILIARIOS_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                txt_calle.Text = Session("rs").FIELDS("CALLE").value.ToString
                txt_municipio.Text = Session("rs").FIELDS("MUNICIPIO").value.ToString
                txt_estado.Text = Session("rs").FIELDS("ESTADO").value.ToString
                txt_asentamiento.Text = Session("rs").FIELDS("ASENTAMIENTO").value.ToString
                txt_exterior.Text = Session("rs").FIELDS("NUMEXT").value.ToString
                txt_interior.Text = Session("rs").FIELDS("NUMINT").value.ToString
                txt_cp.Text = Session("rs").FIELDS("CP").value.ToString
                txt_referencias.Text = Session("rs").FIELDS("REFERENCIA").value.ToString
            End If
        Catch ex As Exception
        Finally
            Session("Con").close()
        End Try
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
            If Curp = "" Or fecha = "" Then
                correcto = True
            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
        Return correcto
    End Function

    Private Sub guarda_generales()
        ' Guarda datos generales del trabajador
        Try
            'lbl_status.Text = "IDPERSONA: " + txt_IdCliente.Text + "USERID: " + Session("USERID").ToString + "SESION: " + Session("SESION").ToString + "INSTI: " + ddl_institucion.SelectedValue + "NOMBRE1:  " + txt_nombre1.Text + "NOMBRE2: " + txt_nombre2.Text + "PATERNO: " + txt_paterno.Text + "MATERNO: " + txt_materno.Text + "RFC" + txt_rfc.Text + "CURP: " + txt_curp.Text + "FECHANAC: " + txt_fecha.Text + "SEXO:" + rad_sexcon1.ToString + rad_sexcon2.ToString + "NOTAS: " + txt_notas.Text'
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INSTI", Session("adVarChar"), Session("adParamInput"), 5, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("AGREMIADO", Session("adVarChar"), Session("adParamInput"), 300, txt_agremiado.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CURP", Session("adVarChar"), Session("adParamInput"), 18, txt_curp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            If rad_sexcon1.Checked = True Then
                Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 1, "H")
                Session("cmd").Parameters.Append(Session("parm"))
            ElseIf rad_sexcon2.Checked = True Then
                Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 1, "M")
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 1, "")
                Session("cmd").Parameters.Append(Session("parm"))
            End If
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, txt_notas.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CCT", Session("adVarChar"), Session("adParamInput"), 2000, ddl_cct.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("REGION", Session("adVarChar"), Session("adParamInput"), 2000, ddl_region.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DELEGACION", Session("adVarChar"), Session("adParamInput"), 2000, ddl_delegacion.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_DATOS_GENERALES_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
        Catch ex As Exception
        Finally
            Session("Con").close()
        End Try
    End Sub

    Protected Sub btn_guardar_g_Click(sender As Object, e As EventArgs)
        ' Botón para guardar información del trabajador 

        'If rad_sexcon1.Checked = False And rad_sexcon2.Checked = False Then
        '    lbl_status.Text = "Error, debe seleccionar una opción en el requisito 'Sexo', por favor verifique"
        '    Exit Sub
        'End If

        If Len(txt_rfc.Text) > 0 Then
            If Len(txt_rfc.Text) < 13 Then
                lbl_status.Text = "Error: La longitud del RFC debe ser de 13 caracteres,por favor, verifique"
                Exit Sub
            End If
        End If

        'If Len(txt_curp.Text) < 18 Then
        '    lbl_status.Text = "Error: La longitud de la CURP debe ser de 18 caracteres, por favor, verifique"
        '    Exit Sub
        'End If

        If txt_notas.Text.Length > 2000 Then
            lbl_status.Text = "Error: El número de caracteres en notas no puede exceder 2000"
            Exit Sub
        End If

        If Len(txt_agremiado.Text) > 300 Then
            lbl_status.Text = "Error: La longitud del Nombre sobrepasa el limite de carácteres, verifique"
            Exit Sub
        End If

        If VerificaFechaNac(txt_fecha.Text) Then
            If Verifica_FechaNac_Curp_RFC(txt_fecha.Text, txt_curp.Text, txt_rfc.Text) = True Then
                Try
                    guarda_generales()
                    carga_generales()
                    lbl_status.Text = "Información guardada correctamente"
                Catch ex As Exception
                    lbl_status.Text = ex.ToString
                End Try
            Else
                lbl_status.Text = "Error: No coincide la fecha de nacimiento con la CURP o la CURP no coincide con el RFC, verifique"
            End If
        Else
            lbl_status.Text = "Error: La fecha de nacimiento es incorrecta"
        End If

    End Sub

    Private Sub guarda_domiciliarios()
        ' Guarda información de domicilio del trabajador
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 220, txt_calle.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NUM_EXT", Session("adVarChar"), Session("adParamInput"), 40, txt_exterior.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NUM_INT", Session("adVarChar"), Session("adParamInput"), 40, txt_interior.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTADO", Session("adVarChar"), Session("adParamInput"), 100, txt_estado.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 100, txt_municipio.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 200, txt_asentamiento.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, txt_referencias.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_DATOS_DOMICILIO_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
        Catch ex As Exception
            lbl_status_dom.Text = ex.ToString
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Protected Sub btn_guardar_domicilio_Click(sender As Object, e As EventArgs)
        ' Botón para guardar información de domicilio del trabajador
        If Len(txt_cp.Text) < 5 Then
            lbl_status_dom.Text = "Error: La longitud del Código Postal es incorrecta, verifique"
            Exit Sub
        End If

        Try
            guarda_domiciliarios()
            carga_Domiciliarios()
            lbl_status_dom.Text = "Información guardada correctamente"
        Catch ex As Exception
            lbl_status_dom.Text = ex.ToString
        End Try

    End Sub

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
        'carga_telefonos()
        guarda_telefonos()
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

    Private Sub guarda_telefonos()
        ' Guarda los teléfonos del afiliado a partir del SP "INS_TELEFONO_AFILIADO"
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            'Session("parm") = Session("cmd").CreateParameter("TELE_MOV", Session("adVarChar"), Session("adParamInput"), 10, txt_telmov.Text)
            'Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TELE_PAR", Session("adVarChar"), Session("adParamInput"), 10, txt_telcasa.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_TELEFONO_AFILIADO"
            Session("cmd").Execute()
            Session("Con").Close()

        Catch ex As Exception
        Finally

        End Try
    End Sub

    'Private Sub guarda_Plaza()
    '    ' Guarda información de datos laborales del trabajador a partir del SP "UPD_DATOS_LABORALES_TRABAJADOR"
    '    Try
    '        Session("cmd") = New ADODB.Command()
    '        Session("Con").Open()
    '        Session("cmd").ActiveConnection = Session("Con")
    '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("IDPLAZA", Session("adVarChar"), Session("adParamInput"), 15, txt_plaza.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("SUELDO", Session("adVarChar"), Session("adParamInput"), 25, txt_sueldo.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("SUELDONETO", Session("adVarChar"), Session("adParamInput"), 25, txt_sueldo_neto.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("SALARIOCOT", Session("adVarChar"), Session("adParamInput"), 25, txt_sueldo_cot.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("PERIODOPAGO", Session("adVarChar"), Session("adParamInput"), 1, cmb_persuel.SelectedValue)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("FECHAINGRESO", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaIniOpe.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("TIPOTRAB", Session("adVarChar"), Session("adParamInput"), 5, cmb_tipotrab.SelectedValue)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("GENERACION", Session("adVarChar"), Session("adParamInput"), 5, cmb_generacion.SelectedValue)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 250, txt_puesto.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 1, cmb_estatus.SelectedValue)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 250, txt_user_afiliado.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("CONTRASEÑA", Session("adVarChar"), Session("adParamInput"), 200, txt_pass_afiliado.Text)
    '        Session("cmd").Parameters.Append(Session("parm"))

    '        Session("cmd").CommandText = "UPD_DATOS_LABORALES_TRABAJADOR"
    '        Session("rs") = Session("cmd").Execute()
    '    Catch ex As Exception
    '    Finally
    '        Session("Con").close()
    '    End Try
    'End Sub

    'Protected Sub btn_guardar_l_Click(sender As Object, e As EventArgs) Handles btn_guardar_l.Click
    '    ' Botón para guardar información laboral del trabajador
    '    Try
    '        guarda_Plaza()
    '        carga_info_plaza()
    '        lbl_status_lab.Text = "Información guardada correctamente"
    '    Catch ex As Exception
    '        lbl_status_lab.Text = ex.ToString
    '    End Try

    'End Sub

    Private Sub limpia_datos()
        'folderA(panel_adicionales, "up")
        folderA(panel_generales, "up")
        'folderA(panel_adicionales, "up")
        folderA(panel_domicilio, "up")
        folderA(panel_contacto, "up")
        'folderA(panel_laborales, "up")
        folderA(panel_bancarios, "up")
        folderA(panel_estatus, "up")

        'Datos generales del afiliado'
        txt_agremiado.Text = ""
        txt_curp.Text = ""
        txt_rfc.Text = ""
        txt_fecha.Text = ""
        rad_sexcon1.Checked = False
        rad_sexcon2.Checked = False
        txt_notas.Text = ""
        'Datos adicionales del afiliado'
        'txt_nss.Text = ""
        'ddl_tipo_conyuge.ClearSelection()
        'cmb_edo_civil.ClearSelection()
        'txt_conyuge.Text = ""
        'txt_conyuge2.Text = ""
        'txt_paternoc.Text = ""
        'txt_maternoc.Text = ""
        'txt_CURPc.Text = ""
        'txt_RFCc.Text = ""
        'txt_fechaconyuge.Text = ""
        'conyuge_sexo1.Checked = False
        'conyuge_sexo2.Checked = False
        'Datos domiciliarios del afiliado'
        txt_calle.Text = ""
        txt_exterior.Text = ""
        txt_interior.Text = ""
        txt_cp.Text = ""
        txt_estado.Text = ""
        txt_municipio.Text = ""
        txt_asentamiento.Text = ""
        txt_referencias.Text = ""
        'Datos de contacto del afiliado'
        txt_telcasa.Text = ""
        txt_correo.Text = ""
        'Datos laborales del afiliado'
        'txt_plaza.Text = ""
        'txt_puesto.Text = ""
        'txt_sueldo.Text = ""
        'txt_sueldo_neto.Text = ""
        'txt_sueldo_cot.Text = ""
        'cmb_persuel.ClearSelection()
        'txt_fechaIniOpe.Text = ""
        'cmb_estatus.ClearSelection()
        'cmb_tipotrab.ClearSelection()
        'txt_antiguedad.Text = ""
        'cmb_generacion.ClearSelection()
        'txt_user_afiliado.Text = ""
        'txt_pass_afiliado.Text = ""
        'Datos bancarios del afiliado'
        txt_medio_paga.Text = ""
        cmb_banco.ClearSelection()
        txt_clabe.Text = ""
        txt_clabe_conf.Text = ""
        lbl_estatus_bank.Text = ""
        'txt_control_conyuge.Text = ""
        'txt_inst_conyuge.Text = ""
        lbl_status.Text = ""
        'lbl_status_adi.Text = ""
        lbl_estatus_contacto.Text = ""
        lbl_status_dom.Text = ""
        'lbl_status_lab.Text = ""
        lbl_statusc.Text = ""
        lbl_estatus_bank.Text = ""
        'control_institu.Visible = False
        lbl_cambio_estatus.Text = ""

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


    'Private Sub lnk_busqueda_coyuge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busqueda_coyuge.Click
    '    ' Linklabel para buscar datos de personas para declararlo cónyuge externo
    '    hdn_origen_busquedas.Value = "conyuge"
    '    up_invisible.Update()
    '    lnk_busqueda_coyuge.Attributes.Add("OnClick", "busquedapersonafisica()")
    'End Sub


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

#Region "Datos Bancarios"

    Private Sub CargaBancos()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CLABE_BANCO_DEFAULT"
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            If Session("rs").Fields("TIPO_CUENTA").Value.ToString = 1 Then
                txt_medio_paga.Text = "*NUMERO DE CUENTA"
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 2 Then
                txt_medio_paga.Text = "CLABE INTERBANCARIA"
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 3 Then
                txt_medio_paga.Text = "CHEQUE"
            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 0 Then
                txt_clabe.Text = ""
                cmb_banco.SelectedValue = "-1"
                txt_clabe.Enabled = False
                cmb_banco.Enabled = False
            End If
        End If
        Session("Con").close()

    End Sub

    Protected Sub btn_guarda_bank_Click(sender As Object, e As EventArgs) Handles btn_guarda_bank.Click

        Try
            Dim Validacion As Boolean = ValidacionBancosVb()
            If Validacion = True Then
                Dim Validaciondigito As Boolean = ValidaDigitoValidador(txt_clabe.Text.ToString)
                If Validaciondigito = True Then
                    lbl_estatus_bank.Text = "Validaciones con éxito."
                    GuardaBancos()
                    CargaBancos()

                End If
            End If
        Catch ex As Exception
            lbl_estatus_bank.Text = ex.ToString
        End Try

    End Sub

    Private Function ValidacionBancosVb() As Boolean

        If txt_clabe.Text <> txt_clabe_conf.Text Then
            lbl_estatus_bank.Text = "Error: La CLABE no coincide con el campo de confirmación."
            Return False
        End If

        If Len(txt_clabe.Text) = 18 Then
            Dim Validacion As String = ValidaCLABE()
            If Validacion <> "OK" Then
                If Validacion = "FALSE" Then
                    lbl_estatus_bank.Text = "Error: La CLABE no coincide con el Banco seleccionado."
                    Return False
                Else

                    lbl_estatus_bank.Text = "Error: CLABE ya registrada para el agremiado: " + Validacion.ToString
                    Return False
                End If
            End If
        Else
            lbl_estatus_bank.Text = "Error: Ingrese los 18 dígitos de la CLABE"
            Return False
        End If


        Return True

    End Function

    Private Function ValidaCLABE() As String

        Dim Respuesta As String = "FALSE"
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_BANCO", Session("adVarChar"), Session("adParamInput"), 10, cmb_banco.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLABE_BANCO", Session("adVarChar"), Session("adParamInput"), 20, txt_clabe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_CLABE_BANCO"
        Session("rs") = Session("cmd").Execute()
        Respuesta = Session("rs").fields("VALIDACION").value.ToString
        Session("Con").close()
        Return Respuesta

    End Function

    Private Sub GuardaBancos()


        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("BANCO", Session("adVarChar"), Session("adParamInput"), 20, CInt(cmb_banco.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MEDIO", Session("adVarChar"), Session("adParamInput"), 20, txt_clabe.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 1, 2)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_DATOS_BANCARIOS_TRABAJADOR"
            Session("rs") = Session("cmd").Execute()
            Session("Con").close()
        Catch ex As Exception
        Finally
            lbl_estatus_bank.Text = "Información guardada correctamente."
        End Try

    End Sub

#End Region

    Private Sub cargaMotivos()
        ' Carga cátalogo de periodicidad de pago y lo muestra en cmb_persuel
        Combo_Estatus.Items.Clear()
        Combo_Estatus.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_MOTIVO_BAJA"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                Combo_Estatus.Items.Add(New ListItem(Session("rs").Fields("MOTIVO").Value, Session("rs").Fields("ID").Value.ToString))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Protected Sub Btn_Guardar_Estatus_Click(sender As Object, e As EventArgs) Handles Btn_Guardar_Estatus.Click
        Dim Respuesta_Act As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 25, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ACTIVO_GUAR", Session("adVarChar"), Session("adParamInput"), 100, CInt(Combo_Estatus.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOTIVOS", Session("adVarChar"), Session("adParamInput"), 2000, Notas_baja.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_ACTIVO_GENERAL"
        Session("rs") = Session("cmd").Execute()
        Respuesta_Act = Session("rs").fields("VALIDACION").value.ToString
        Session("Con").Close()

        If Respuesta_Act = "TRUE" Then
            lbl_cambio_estatus.Text = "Guardado correctamente"
        Else
            lbl_cambio_estatus.Text = "Error"
        End If
    End Sub


    Private Function ValidaDigitoValidador(Clabe As String) As Boolean

        Dim Clabe_Aux As String = Clabe.Substring(0, 17)

        Dim Factor_De_Peso As New DataTable
        Factor_De_Peso.Columns.Add("VALOR", GetType(Integer))
        Factor_De_Peso.Rows.Add(3)
        Factor_De_Peso.Rows.Add(7)
        Factor_De_Peso.Rows.Add(1)
        Factor_De_Peso.Rows.Add(3)
        Factor_De_Peso.Rows.Add(7)
        Factor_De_Peso.Rows.Add(1)
        Factor_De_Peso.Rows.Add(3)
        Factor_De_Peso.Rows.Add(7)
        Factor_De_Peso.Rows.Add(1)
        Factor_De_Peso.Rows.Add(3)
        Factor_De_Peso.Rows.Add(7)
        Factor_De_Peso.Rows.Add(1)
        Factor_De_Peso.Rows.Add(3)
        Factor_De_Peso.Rows.Add(7)
        Factor_De_Peso.Rows.Add(1)
        Factor_De_Peso.Rows.Add(3)
        Factor_De_Peso.Rows.Add(7)

        Dim Producto As New DataTable
        Producto.Columns.Add("VALORP", GetType(Integer))

        Dim contador As Integer = 0
        Dim cont_prod As Integer = 0

        For Each row As DataRow In Factor_De_Peso.Rows
            Dim valor_clabe As Integer = Clabe_Aux.Substring(contador, 1)
            Dim valor_factor As String = CInt(row("VALOR"))
            Dim valor_producto As Integer = ((valor_clabe * valor_factor) Mod 10)
            Producto.Rows.Add(valor_producto)
            contador += 1
        Next

        For Each row2 As DataRow In Producto.Rows
            cont_prod += CInt(row2("VALORP"))
        Next

        Dim cont_prod_mod10 As Integer = cont_prod Mod 10

        Dim digito_recibido As Integer = Clabe.Substring(17, 1)
        Dim digito_calculado As Integer

        If cont_prod_mod10 = 0 Then
            digito_calculado = 0
        Else
            digito_calculado = 10 - cont_prod_mod10
        End If

        If digito_recibido = digito_calculado Then

            Return True
        Else
            lbl_estatus_bank.Text = "Error: El digito verificador no coincide"
            Return False
        End If



    End Function
End Class