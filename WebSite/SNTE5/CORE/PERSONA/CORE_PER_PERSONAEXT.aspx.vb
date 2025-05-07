Imports System.IO
Public Class CORE_PER_PERSONAEXT
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Alta Persona", "Configuración de Persona")
        If Not Me.IsPostBack Then

            Session("DEPENDENCIAID") = Nothing
            Session("idperbusca") = Nothing
            Session("CONYUGEID") = Nothing
            Session("PROSPECTO") = Nothing
            Session("DEPENDIENTEID") = Nothing
            LlenaPaises(cmb_pais_nac.ID)
            llena_nac()
            llena_estatus()
            llena_sectores()
            LlenaPeriodos()
            llena_vialidad(cmb_vialidad.ID)
            llena_vialidad("cmb_vialidad_empresa")
            txt_id_persona.Text = "Nueva Persona"

            If Session("PERSONAID") > 0 Then

                txt_id_persona.Text = Session("PERSONAID")
                carga_personales()
                carga_ad()
                carga_domicilio()
                carga_contacto()
                carga_laborales()
                llena_dependencias()
                llena_dependientes()
                define_avance()
                MostrarDocumentos()
                MostrarDocumentosDigitalizados()
                VerificarDocsCompletos()
                If cmb_edo_civil.SelectedValue = "SOL" Or cmb_edo_civil.SelectedValue = "DIV" Or cmb_edo_civil.SelectedValue = "VIU" Then
                    RequiredFieldValidator_regimen.Enabled = False
                End If
            End If

        End If

        If Not Session("idperbusca") Is Nothing Then

            If hdn_origen_busquedas.Value = "conyuge" Then
                Session("CONYUGEID") = CInt(Session("idperbusca"))
                lbl_datosconyugebuscar2.Visible = True
                lbl_datosconyugebuscar2.Text = Session("PROSPECTO")
                Session("idperbusca") = Nothing
                Session("PROSPECTO") = Nothing
                hdn_origen_busquedas.Value = ""
                upnl_adicionales.Update()
                lbl_status_adi.Text = ""
                Session("CONYUGE_ED") = 0
            End If
            If hdn_origen_busquedas.Value = "dependencia" Then

                Session("DEPENDENCIAID") = CInt(Session("idperbusca"))
                lbl_nombre_de_quien_depende.Text = Session("PROSPECTO")
                lbl_dependencia_parentesco.Visible = True
                cmb_dependencia_parentesco.Visible = True
                req_dependencia_parentesco.Enabled = True
                lnk_agregar_dependencia.Visible = True
                llena_parentesco(cmb_dependencia_parentesco.ID)
                Session("idperbusca") = Nothing
                Session("PROSPECTO") = Nothing
                hdn_origen_busquedas.Value = ""
                lbl_estatus_dependencia.Text = ""
                upnl_dependientes.Update()

            End If
            If hdn_origen_busquedas.Value = "dependiente" Then

                Session("DEPENDIENTEID") = CInt(Session("idperbusca"))
                lbl_nombre_dependiente.Text = Session("PROSPECTO")
                lbl_dependiente_parentesco.Visible = True
                cmb_dependiente_parentesco.Visible = True
                req_dependiente_parentesco.Enabled = True
                lnk_agregar_dependiente.Visible = True
                llena_parentesco(cmb_dependiente_parentesco.ID)
                Session("idperbusca") = Nothing
                Session("PROSPECTO") = Nothing
                hdn_origen_busquedas.Value = ""
                lbl_estatus_dependiente.Text = ""
                upnl_dependientes.Update()
                Dofocus(cmb_dependiente_parentesco)

            End If
            If hdn_origen_busquedas.Value = "empresa" Then
                Session("EMPRESAID") = CInt(Session("idperbusca"))
                Session("idperbusca") = Nothing
                Session("PROSPECTO") = Nothing
                hdn_origen_busquedas.Value = ""
            End If
        End If
        If Session("PERSONAID") > 0 Then
            define_avance()
        End If
        If Me.IsPostBack Then
            If ScriptManager.GetCurrent(Me).IsInAsyncPostBack Then

                Dim smUniqueId As String = ScriptManager.GetCurrent(Page).UniqueID
                Dim smFieldValue As String = Request.Form(smUniqueId)
                Dim id As String = ""
                If (Not String.IsNullOrEmpty(smFieldValue)) And smFieldValue.Contains("|") Then

                    If smFieldValue.Split("|")(0).Contains("upnl_") Then
                        id = smFieldValue.Split("|")(0).Replace("upnl_", "panel_")
                        id = id.Replace("$", "_")

                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptFolder" & id, "$('#" & id & " .panel_header_folder').click(function(event) {var folder_content =$(this).siblings('.panel-body').children('.panel-body_content'); var toogle = $(this).children('.panel_folder_toogle'); if (toogle.hasClass('up')){toogle.removeClass('up');toogle.addClass('down');folder_content.show('6666', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.parent().css({ 'background': '#113964 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px'});} else if (toogle.hasClass('down')){toogle.removeClass('down');folder_content.hide('333', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.addClass('up');toogle.parent().css({ 'background': '#886F4F !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px' });}});", True)
                        folderA(FindControlRecursive(Me.Page, id.Substring(id.IndexOf("panel_"))), "down", False)

                    End If


                    'If smFieldValue.Split("|")(1).Contains("lnk_busqueda_coyuge") Then
                    '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "salertas", "__doPostBack('" & lbl_datosconyugebuscar2.ClientID & "','');", True)

                    'End If

                End If

            End If
        End If
    End Sub

    Protected Sub chk_desempleo_Click(sender As Object, e As EventArgs) Handles chk_desempleo.CheckedChanged

        If chk_desempleo.Checked = True Then
            panel_empresa.Visible = False
            upnl_empleo.Visible = False
            folderA(panel_ingresos_ad, "down")
        ElseIf chk_desempleo.Checked = False Then
            panel_empresa.Visible = True
            upnl_empleo.Visible = True
            folderA(panel_empresa, "down")
        End If
    End Sub

    Private Sub define_avance()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AVANCE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Dim Avance As Integer = CInt(Session("rs").Fields("AVANCE").Value)
            If Avance > 1 Then
                Semaforo1_v.Visible = True
                Semaforo1_r.Visible = False
            End If
            If Avance > 2 Then
                Semaforo2_v.Visible = True
                Semaforo2_r.Visible = False
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
                Semaforo6_v.Visible = True
                Semaforo6_r.Visible = False
            End If
        End If
        Session("Con").Close()
        up_semaforos.Update()
    End Sub

    Private Sub actualiza_avance(ByVal avance As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AVANCE", Session("adVarChar"), Session("adParamInput"), 10, avance)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AVANCE_PERSONA_FISICA"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub llena_estatus()
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento
        cmb_estatus.Items.Clear()
        cmb_estatus.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ESTATUSTRAB"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_estatus.Items.Add(New ListItem(Session("rs").Fields("ESTATUS").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaPaises(ByVal id As String)
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento
        If id = "cmb_pais_nac" Then
            cmb_pais_nac.Items.Clear()
            cmb_pais_nac.Items.Add(New ListItem("ELIJA", "-1"))
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_PAISES"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_pais_nac.Items.Add(New ListItem(Session("rs").Fields("CATPAIS_PAIS").Value, Session("rs").Fields("CATPAIS_ID_PAIS").Value.ToString))
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
        If id = "cmb_lugarnaccon" Then
            cmb_lugarnaccon.Items.Clear()
            cmb_lugarnaccon.Items.Add(New ListItem("ELIJA", "-1"))
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_PAISES"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_lugarnaccon.Items.Add(New ListItem(Session("rs").Fields("CATPAIS_PAIS").Value, Session("rs").Fields("CATPAIS_ID_PAIS").Value.ToString))
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
    End Sub

    Private Sub llena_nac()
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento
        cmb_nacionalidad.Items.Clear()
        cmb_nacionalidad.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_NACIONALIDADES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_nacionalidad.Items.Add(New ListItem(Session("rs").Fields("NAC").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaPeriodos()
        cmb_periodo_pago.Items.Clear()
        cmb_periodo_pago.Items.Add(New ListItem("ELIJA", "-1"))
        cmb_periodo_dividendos.Items.Clear()
        cmb_periodo_dividendos.Items.Add(New ListItem("ELIJA", "-1"))
        cmb_periodo_otros.Items.Clear()
        cmb_periodo_otros.Items.Add(New ListItem("ELIJA", "-1"))
        cmb_periodo_intereses.Items.Clear()
        cmb_periodo_intereses.Items.Add(New ListItem("ELIJA", "-1"))
        cmb_periodo_adicional.Items.Clear()
        cmb_periodo_adicional.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_periodo_pago.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("CLAVE").Value.ToString))
            cmb_periodo_adicional.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("CLAVE").Value.ToString))
            cmb_periodo_dividendos.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("CLAVE").Value.ToString))
            cmb_periodo_otros.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("CLAVE").Value.ToString))
            cmb_periodo_intereses.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("CLAVE").Value.ToString))
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

#Region "verificaFechas"
    Protected Sub btn_guardar_g_Click(sender As Object, e As EventArgs)
        Try

            If VerificaFechaNac(txt_fecha.Text) Then

                If Verifica_FechaNac_Curp_RFC(txt_fecha.Text, txt_curp.Text, txt_rfc.Text, cmb_pais_nac.SelectedItem.Value) = True Then
                    guarda_personales()
                    carga_personales()
                    actualiza_avance(1)
                Else
                    lbl_status.Text = "Error: No coincide la fecha de Nacimiento con la CURP o la CURP no coincide con el RFC, Verifique"
                End If
            Else
                lbl_status.Text = "Error: La fecha de nacimiento es incorrecta"
                Exit Sub
            End If
            folderA(panel_generales, "down")
            upd_pnl_digitalizacion.Update()
            MostrarDocumentos()

        Catch ex As Exception
            lbl_status.Text = ex.ToString
        End Try
    End Sub

#End Region


#Region "VerificaFechaNac"
    Private Function VerificaFechaNac(ByVal fecha As String) As Boolean
        Dim correcto As Boolean = True
        Try
            If DateDiff(DateInterval.Year, CDate(fecha), Now()) > 150 Or (DateAdd(DateInterval.Day, -1, Now()) <= CDate(fecha)) Then
                correcto = False
            End If
        Catch ex As Exception
        End Try
        Return correcto
    End Function
#End Region

#Region "Verifica Fecha Nac CURP/RFC"
    Private Function Verifica_FechaNac_Curp_RFC(ByVal fecha As String, ByVal Curp As String, ByVal rfc As String, ByVal idpais As Integer) As Boolean
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
            Session("parm") = Session("cmd").CreateParameter("ID_PAIS", Session("adVarChar"), Session("adParamInput"), 13, idpais)
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
#End Region

#Region "Guarda Generales"
    Private Sub guarda_personales()
        If txt_notas.Text.Length > 2000 Then
            lbl_status.Text = "Error: El número de caracteres en notas no puede exceder 2000"
            Exit Sub
        End If

        If Session("PERSONAID") > 0 Then
            lbl_status.Text = ""
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CURP", Session("adVarChar"), Session("adParamInput"), 18, txt_curp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LUGARNAC", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_pais_nac.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, Left(txt_notas.Text, 2000))
            Session("cmd").Parameters.Append(Session("parm"))
            If rad_sexcon1.Checked = True Then 'seleccionaron hombre
                Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 10, "H")
                Session("cmd").Parameters.Append(Session("parm"))
            Else 'seleccionaron mujer
                Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 10, "M")
                Session("cmd").Parameters.Append(Session("parm"))
            End If
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDOFICIAL", Session("adVarChar"), Session("adParamInput"), 25, txt_id_oficial.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_NAC", Session("adVarChar"), Session("adParamInput"), 11, CInt(cmb_nacionalidad.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            'Session("parm") = Session("cmd").CreateParameter("ESTATUSPER", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_estatus.SelectedItem.Value))
            'Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUSTRAB", Session("adVarChar"), Session("adParamInput"), 10, 5)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_PERSONA_EXTERNA_PERSONALES"
            Session("rs") = Session("cmd").Execute()
            If Session("rs").Fields("RES").Value = 1 Then
                lbl_status.Text = "Guardado correctamente"
            Else
                lbl_status.Text = "Error: La persona editada ya está dada de alta en el sistema. Verifique"
            End If
            Session("Con").Close()
        Else

            lbl_status.Text = ""
            Dim sexo1 As String
            If rad_sexcon1.Checked = True Then 'seleccionaron hombre
                sexo1 = "H"
            Else 'seleccionaron mujer
                sexo1 = "M"
            End If
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CURP", Session("adVarChar"), Session("adParamInput"), 18, txt_curp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LUGARNAC", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_pais_nac.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 1, sexo1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, Left(txt_notas.Text, 2000))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 2)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDOFICIAL", Session("adVarChar"), Session("adParamInput"), 25, txt_id_oficial.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_NAC", Session("adVarChar"), Session("adParamInput"), 11, CInt(cmb_nacionalidad.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUSPER", Session("adVarChar"), Session("adParamInput"), 10, 5)
            Session("cmd").Parameters.Append(Session("parm"))
            'Session("parm") = Session("cmd").CreateParameter("ESTATUSPER", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_estatus.SelectedItem.Value))
            'Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PERSONA_EXTERNA"
            Session("rs") = Session("cmd").Execute()
            Session("YAEXISTE") = Session("rs").Fields("YAEXISTE").Value
            Session("PERSONAID") = CInt(Session("rs").Fields("IDPERSONA").Value.ToString)
            If Session("YAEXISTE") = "NO" Then 'SI ES UNA PERSONA NUEVA
                lbl_status.Text = "Guardado correctamente"
                upd_id.Update()
                txt_id_persona.Text = Session("PERSONAID")
            Else
                Session("Con").Close()
                If Session("YAEXISTE") = "CONYUGE" Then
                    lbl_status.Text = "La persona ya existe como Cónyuge,       ¿Desea continuar su registro?"
                    Exit Sub
                ElseIf Session("YAEXISTE") = "REP_LEG" Then
                    lbl_status.Text = "La persona ya existe como Representante legal,       ¿Desea continuar su registro?"
                    Exit Sub
                ElseIf Session("YAEXISTE") = "ACC_CON" Then
                    lbl_status.Text = "La persona ya existe como Accionista o Miembro del consejo de alguna persona Moral,       ¿Desea continuar su registro?"
                    Exit Sub
                Else
                    'YA FUE CAPTURADA ESTA PERSONA POR LO QUE NO LA DEJAMOS CONTINUAR
                    lbl_status.Text = "Error: La persona ya fue capturada en el sistema"
                    Exit Sub
                End If
            End If
            Session("Con").Close()
            'QuieEsQuien("F", txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_rfc.Text, "")
        End If
        'define_avance()

    End Sub

#End Region

#Region "Llena Sectores"
    Private Sub llena_sectores()
        cmb_sector.Items.Clear()
        cmb_sector.Items.Add(New ListItem("ELIJA", -1))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ACTECO_SECTOR"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_sector.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub
#End Region

#Region "Llena Actividades Economicas"
    Private Sub llena_actividades_economicas()
        'Procedimiento que obtiene el catálogo de actividades económicas y las despliega en el combo correspondiente
        cmb_actividad.Items.Clear()
        cmb_actividad.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_SEC", Session("adVarChar"), Session("adParamInput"), 11, cmb_sector.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ACTIVIDAD_ECO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_actividad.Items.Add(New ListItem(Session("rs").Fields("ACT").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub



    Protected Sub cmb_sector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_sector.SelectedIndexChanged
        If cmb_sector.SelectedItem.Value = -1 Then
            cmb_actividad.Items.Clear()
            cmb_actividad.Enabled = False
        Else
            cmb_actividad.Enabled = True
            llena_actividades_economicas()
        End If
    End Sub
#End Region

    Protected Sub txt_cp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp.TextChanged
        'Response.Redirect("AltaModPersonaF.aspx")
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
    End Sub

    Protected Sub cmb_edo_civil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_edo_civil.SelectedIndexChanged
        If cmb_edo_civil.SelectedValue = "CAS" Or cmb_edo_civil.SelectedValue = "UNL" Then
            cmb_regimen.ClearSelection()
            lbl_datosconyugebuscar2.Text = "-"

        End If

        If cmb_edo_civil.SelectedValue = "CAS" Or cmb_edo_civil.SelectedValue = "UNL" Then
            Session("idperbusca") = Nothing
            lbl_regimen.Visible = True
            cmb_regimen.Visible = True
            lnk_conyuge.Visible = True
            lbl_odatoscompletos.Visible = True
            lbl_buscarpersonas1.Visible = True
            lnk_busqueda_coyuge.Visible = True
            lbl_datosconyugebuscar1.Visible = True
            lbl_datosconyugebuscar2.Visible = True
            RequiredFieldValidator_regimen.Enabled = True

        Else
            Session("idperbusca") = Nothing
            lbl_regimen.Visible = False
            cmb_regimen.Visible = False
            lnk_conyuge.Visible = False
            lbl_odatoscompletos.Visible = False
            lbl_buscarpersonas1.Visible = False
            lnk_busqueda_coyuge.Visible = False
            lbl_datosconyugebuscar1.Visible = False
            lbl_datosconyugebuscar2.Visible = False
            RequiredFieldValidator_regimen.Enabled = False
        End If
    End Sub

    Protected Sub lnk_conyuge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_conyuge.Click
        lbl_status_adi.Text = ""
        txt_nombrecon1.Text = ""
        txt_nombrecon2.Text = ""
        txt_paternocon.Text = ""
        txt_maternocon.Text = ""
        txt_fechanaccon.Text = ""
        rad_sexcon1.Checked = True
        txt_curpcon.Text = ""
        txt_rfccon.Text = ""
        lbl_status_con.Text = ""

        Session("CONYUGEID") = Nothing
        pnl_alta_mod_conyuge.Visible = True
        ModalPopupExtender_alta_mod_conyuge.Enabled = True
        LlenaPaises(cmb_lugarnaccon.ID)
        ModalPopupExtender_alta_mod_conyuge.Show()
        Dofocus(txt_nombrecon1)
        'If Not Session("PERSONAID") Is Nothing Then
        Session("CONYUGE_ED") = 0
        '    txt_nombrecon1.Text = ""
        '    txt_nombrecon2.Text = ""
        '    txt_paternocon.Text = ""
        '    txt_maternocon.Text = ""
        '    txt_fechanaccon.Text = ""
        '    rad_sexcon1.Checked = True
        '    txt_curpcon.Text = ""
        '    txt_rfccon.Text = ""
        'End If
    End Sub

    Private Function ValidaCURP(ByVal curp As String) As Boolean
        Return Regex.IsMatch(curp, ("^[a-zA-Z]{3,4}(\d{6})[hmHM]{1}[a-zA-Z]{4,5}(\d{2})?$"))
    End Function

    Protected Sub btn_guardacon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardacon.Click
        lbl_fechacerr.Text = ""

        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptFolder" & ID, "$('#" & panel_adicionales.ClientID & " .panel_header_folder').click(function(event) {var folder_content =$(this).siblings('.panel-body').children('.panel-body_content'); var toogle = $(this).children('.panel_folder_toogle'); if (toogle.hasClass('up')){toogle.removeClass('up');toogle.addClass('down');folder_content.show('6666', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.parent().css({ 'background': '#113964 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px'});} else if (toogle.hasClass('down')){toogle.removeClass('down');folder_content.hide('333', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.addClass('up');toogle.parent().css({ 'background': '#F8F9F9 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px' });}});", True)

        If VerificaFechaNac(txt_fechanaccon.Text) Then

            If Verifica_FechaNac_Curp_RFC(txt_fechanaccon.Text, txt_curpcon.Text, txt_rfccon.Text, cmb_lugarnaccon.SelectedItem.Value) = True Then
                lbl_curpwrong2.Visible = False
                lbl_status_con.Text = ""
                actualiza_avance(1)
                lbl_datosconyugebuscar2.Text = UCase(txt_nombrecon1.Text) + " " + UCase(txt_nombrecon2.Text) + " " + UCase(txt_paternocon.Text) + " " + UCase(txt_maternocon.Text)
                ModalPopupExtender_alta_mod_conyuge.Hide()
            Else
                lbl_status_con.Text = "Error: No coincide la fecha de Nacimiento con la CURP o la CURP no coincide con el RFC, Verifique"
                ModalPopupExtender_alta_mod_conyuge.Show()
                Exit Sub
            End If
        Else
            lbl_status_con.Text = "Error: La fecha de nacimiento es incorrecta"
            ModalPopupExtender_alta_mod_conyuge.Show()
            Exit Sub
        End If
        Dofocus(txt_dependientes)
        upnl_adicionales.Update()
        folderA(panel_laborales, "up", False)
        folderA(panel_adicionales, "down", False)
        folderA(panel_contacto, "up", False)
        folderA(panel_dependientes, "up", False)
        folderA(panel_domicilio, "up", False)
        folderA(panel_generales, "up", False)
        folderA(panel_digitalizacion, "up", False)
    End Sub

    Protected Sub btn_cancelarcon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelarcon.Click
        ModalPopupExtender_alta_mod_conyuge.Hide()
        pnl_alta_mod_conyuge.Visible = False
        ModalPopupExtender_alta_mod_conyuge.Enabled = False

        Session("CONYUGE_ED") = Nothing

        upnl_adicionales.Update()
        folderA(panel_laborales, "up", False)
        folderA(panel_adicionales, "down", False)
        folderA(panel_contacto, "up", False)
        folderA(panel_dependientes, "up", False)
        folderA(panel_domicilio, "up", False)
        folderA(panel_generales, "up", False)
        folderA(panel_digitalizacion, "up", False)
        verifica_conyuge()
    End Sub

    Private Sub verifica_conyuge()
        lbl_datosconyugebuscar2.Text = ""
        Session("CONYUGE_ED") = 0
        Session("CONYUGEID") = Nothing
        lnk_editar_conyuge.Visible = False
        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_PERSONA_FISICA_DATOS_CONYUGE"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then
                Session("CONYUGEID") = 0
                lbl_datosconyugebuscar2.Text = Session("rs").Fields("NOMBRE1").Value + " " + Session("rs").Fields("NOMBRE2").Value + " " + Session("rs").Fields("PATERNO").Value + " " + Session("rs").Fields("MATERNO").Value
                Session("CONYUGE_ED") = 1
                lnk_editar_conyuge.Visible = True
            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub lnk_busqueda_coyuge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busqueda_coyuge.Click
        'lbl_status_ad_civil.Text = ""

        hdn_origen_busquedas.Value = "conyuge"
        up_invisible.Update()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Busqueda", "busquedapersonafisica('" + lnk_busqueda_coyuge.ID + "');", True)



    End Sub

    Private Sub guarda_ad_economicos()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("IDACTIVIDAD", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_ACT"))
        Session("parm") = Session("cmd").CreateParameter("IDACTIVIDAD", Session("adVarChar"), Session("adParamInput"), 11, cmb_actividad.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NSS", Session("adVarChar"), Session("adParamInput"), 11, txt_nss.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("GRADAC", Session("adVarChar"), Session("adParamInput"), 3, cmb_grado.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDFIEL", Session("adVarChar"), Session("adParamInput"), 30, txt_fiel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAALTASAT", Session("adVarChar"), Session("adParamInput"), 30, txt_fecha_sat.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PERSONA_FISICA_ADICIONALES"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub guarda_personales_conyuge()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombrecon1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_nombrecon2.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_paternocon.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_maternocon.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfccon.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CURP", Session("adVarChar"), Session("adParamInput"), 18, txt_curpcon.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LUGARNAC", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_lugarnaccon.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 20, txt_fechanaccon.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If RadioButton1.Checked = True Then 'seleccionaron hombre
            Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 1, "H")
            Session("cmd").Parameters.Append(Session("parm"))
        Else 'seleccionaron mujer
            Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 1, "M")
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 3)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOFICIAL", Session("adVarChar"), Session("adParamInput"), 25, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_NAC", Session("adVarChar"), Session("adParamInput"), 11, 273)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUSPER", Session("adVarChar"), Session("adParamInput"), 10, 5)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PERSONA_EXTERNA"
        Session("rs") = Session("cmd").Execute()

        Session("YAEXISTE") = Session("rs").Fields("YAEXISTE").Value
        Session("CONYUGEID") = CInt(Session("rs").Fields("IDPERSONA").Value.ToString)

        If Session("rs").Fields("YAEXISTE").Value = "NO" Then 'SI ES UNA PERSONA NUEVA
            lbl_status_adi.Text = ""
        Else
            lbl_status_adi.Text = "El conyuge ya se encuentra registrado"
        End If
        Session("Con").Close()
    End Sub

    Private Sub guarda_ad_civiles()
        Dim nIdConyuge As Integer
        nIdConyuge = 0
        If Not (Session("CONYUGEID") Is Nothing) Then
            nIdConyuge = Session("CONYUGEID")
        End If
        If Session("CONYUGE_ED") = 0 Then
            Try
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("EDOCIV", Session("adVarChar"), Session("adParamInput"), 3, cmb_edo_civil.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("REGIMEN", Session("adVarChar"), Session("adParamInput"), 3, cmb_regimen.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DEPENDIENTES", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_dependientes.Text))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDCONYUGE", Session("adVarChar"), Session("adParamInput"), 10, nIdConyuge)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_PERSONA_FISICA_CIVILES"
                Session("cmd").Execute()
            Catch ex As Exception
            Finally
                Session("Con").Close()
            End Try
        Else
            Try
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("EDOCIV", Session("adVarChar"), Session("adParamInput"), 3, cmb_edo_civil.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("REGIMEN", Session("adVarChar"), Session("adParamInput"), 3, cmb_regimen.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DEPENDIENTES", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_dependientes.Text))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDCONYUGE", Session("adVarChar"), Session("adParamInput"), 10, nIdConyuge)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_PERSONA_FISICA_CIVILES"
                Session("cmd").Execute()
            Catch ex As Exception
            Finally
                Session("Con").Close()
            End Try
        End If

    End Sub

    Private Sub carga_datos_conyuge()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_DATOS_CONYUGE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            txt_nombrecon1.Text = Session("rs").Fields("NOMBRE1").Value
            txt_nombrecon2.Text = Session("rs").Fields("NOMBRE2").Value
            txt_paternocon.Text = Session("rs").Fields("PATERNO").Value
            txt_maternocon.Text = Session("rs").Fields("MATERNO").Value
            cmb_lugarnaccon.ClearSelection()
            cmb_lugarnaccon.Items.FindByValue(Session("rs").Fields("IDPAIS").Value.ToString).Selected = True
            txt_fechanaccon.Text = Session("rs").Fields("FECHA_NAC").Value
            If Session("rs").Fields("SEXO").Value = "H" Then
                rad_sexcon1.Checked = True
            Else
                rad_sexcon2.Checked = True
            End If
            txt_curpcon.Text = Session("rs").Fields("CURP").Value
            txt_rfccon.Text = Session("rs").Fields("RFC").Value
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_guardar_a_Click(sender As Object, e As EventArgs)
        'Session("ID_ACT") = Nothing
        lbl_status_adi.Text = ""
        If cmb_edo_civil.SelectedValue.ToString = "CAS" Or cmb_edo_civil.SelectedValue.ToString = "UNL" Then
            If lbl_datosconyugebuscar2.Text = "-" Or lbl_datosconyugebuscar2.Text = "" Then
                lbl_status_adi.Text = "Error: Debe seleccionar o dar de alta una persona como Cónyuge"
                Exit Sub
            End If
            If Session("CONYUGEID") = Nothing And Session("CONYUGE_ED") = 0 Then
                guarda_personales_conyuge()
            End If

        End If
        If lbl_status_adi.Text = "" Then

            guarda_ad_economicos()
            guarda_ad_civiles()
            actualiza_avance(2)
            folderA(panel_adicionales, "down")
            define_avance()
            carga_ad()
            lbl_status_adi.Text = "Guardado correctamente"
        Else
            lbl_status_adi.Text = "Error:El conyuge ya se encuentra registrado"
        End If
    End Sub

    Private Sub llena_parentesco(ByVal id As String)
        Dim elija As New ListItem("ELIJA", "-1")
        If id = "cmb_dependencia_parentesco" Then
            cmb_dependencia_parentesco.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
            Session("rs") = Session("cmd").Execute()
            cmb_dependencia_parentesco.Items.Add(elija)
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
                cmb_dependencia_parentesco.Items.Add(item)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
        If id = "cmb_dependiente_parentesco" Then
            cmb_dependiente_parentesco.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
            Session("rs") = Session("cmd").Execute()
            cmb_dependiente_parentesco.Items.Add(elija)
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
                cmb_dependiente_parentesco.Items.Add(item)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
    End Sub

    Private Sub llena_dependencias()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_dependencias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("PERSONAID") > 0 Then
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_DEPENDENCIAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_dependencias, Session("rs"))
        Session("Con").Close()
        If dt_dependencias.Rows.Count > 0 Then
            dag_dependencia.Visible = True
            dag_dependencia.DataSource = dt_dependencias
            dag_dependencia.DataBind()
        Else
            dag_dependencia.Visible = False
        End If
    End Sub

    Protected Sub lnk_busca_dependencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busca_dependencia.Click
        folderA(panel_dependientes, "down", False)
        hdn_origen_busquedas.Value = "dependencia"
        up_invisible.Update()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Busqueda", "busquedapersonafisica('" + lnk_busca_dependencia.ID + "');", True)
    End Sub

    Private Sub guardar_dependencia()
        If Session("DEPENDENCIAID") = Session("PERSONAID") Then
            lbl_estatus_dependencia.Text = "Error: No puede agregarse a sí mismo como dependencia"
            Exit Sub
        End If

        If Not Session("DEPENDENCIAID") Is Nothing Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If Session("PERSONAID_F") Is Nothing Then
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Else
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID_F"))
            End If
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDDEPENDENCIA", Session("adVarChar"), Session("adParamInput"), 10, Session("DEPENDENCIAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPARENTESCO", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_dependencia_parentesco.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PERSONA_FISICA_DEPENDENCIAS"
            Session("rs") = Session("cmd").Execute()
            If Session("rs").Fields("RES").Value = "1" Then
                Session("Con").Close()
                lbl_estatus_dependencia.Text = ""
                llena_dependencias()
            Else
                Session("Con").Close()
                lbl_estatus_dependencia.Text = "Error: Ya existe una relación de dependencia con esta persona, verifique"
            End If
            Session("DEPENDENCIAID") = Nothing
        Else
            Exit Sub
        End If
    End Sub

    Protected Sub lnk_agregar_dependencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_agregar_dependencia.Click
        guardar_dependencia()
        cmb_dependencia_parentesco.Visible = False
        req_dependencia_parentesco.Enabled = False
        lnk_agregar_dependencia.Visible = False
        lbl_dependencia_parentesco.Visible = False
        lbl_nombre_de_quien_depende.Text = ""
    End Sub

    Protected Sub dag_dependencia_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_dependencia.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If Session("PERSONAID") Is Nothing Then
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Else
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            End If
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDDEPENDENCIA", Session("adVarChar"), Session("adParamInput"), 10, CInt(e.Item.Cells(0).Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_BAJA_DEPENDENCIA"
            Session("cmd").Execute()
            Session("Con").Close()
            llena_dependencias()
            lbl_estatus_dependencia.Text = ""
        End If
    End Sub

    Private Sub llena_dependientes()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_dependientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("PERSONAID_F") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID_F"))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_DEPENDIENTES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_dependientes, Session("rs"))
        Session("Con").Close()
        If dt_dependientes.Rows.Count > 0 Then
            dag_dependientes.Visible = True
            dag_dependientes.DataSource = dt_dependientes
            dag_dependientes.DataBind()
        Else
            dag_dependientes.Visible = False
        End If
    End Sub

    Protected Sub lnk_busca_dependientes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busca_dependientes.Click
        folderA(panel_dependientes, "down", False)
        hdn_origen_busquedas.Value = "dependiente"
        up_invisible.Update()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Busqueda", "busquedapersonafisica('" + lnk_busca_dependientes.ID + "');", True)
    End Sub

    Private Sub guardar_dependiente()
        If Session("DEPENDIENTEID") = Session("PERSONAID") Then
            lbl_estatus_dependiente.Text = "Error: No puede agregarse a sí mismo como dependiente"
            Exit Sub
        End If
        If Not Session("DEPENDIENTEID") Is Nothing Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If Session("PERSONAID_F") Is Nothing Then
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Else
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID_F"))
            End If
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDDEPENDIENTE", Session("adVarChar"), Session("adParamInput"), 10, Session("DEPENDIENTEID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPARENTESCO", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_dependiente_parentesco.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PERSONA_FISICA_DEPENDIENTES"
            Session("rs") = Session("cmd").Execute()
            If Session("rs").Fields("RES").Value = "1" Then
                Session("Con").Close()
                lbl_estatus_dependiente.Text = ""
                llena_dependientes()
            Else
                Session("Con").Close()
                lbl_estatus_dependiente.Text = "Error: Ya existe una relación de dependencia con esta persona, verifique"
            End If
            Session("DEPENDIENTEID") = Nothing
        Else
            Exit Sub
        End If
    End Sub

    Protected Sub lnk_agregar_dependiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_agregar_dependiente.Click
        guardar_dependiente()
        cmb_dependiente_parentesco.Visible = False
        req_dependiente_parentesco.Enabled = False
        lnk_agregar_dependiente.Visible = False
        lbl_dependiente_parentesco.Visible = False
        lbl_nombre_dependiente.Text = ""
    End Sub

    Protected Sub dag_dependientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_dependientes.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If Session("PERSONAID_F") Is Nothing Then
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Else
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID_F"))
            End If
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDDEPENDENCIA", Session("adVarChar"), Session("adParamInput"), 10, CInt(e.Item.Cells(0).Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_BAJA_DEPENDIENTE"
            Session("cmd").Execute()
            Session("Con").Close()
            llena_dependientes()
            lbl_estatus_dependiente.Text = ""
        End If
    End Sub

    Protected Sub btn_guardar_d_Click(sender As Object, e As EventArgs)
        actualiza_avance(3)
        folderA(panel_dependientes, "down")
        define_avance()
    End Sub

    Private Sub busquedaCP(ByVal CP As String, ByVal cApartado As String, ByVal cIdAsen As String, ByVal cIdLoca As String)
        Dim cIdEdo As String = ""
        Dim cIdMuni As String = ""

        Select Case cApartado
            Case "D"
                cmb_estado.Items.Clear()
                cmb_municipio.Items.Clear()
                cmb_asentamiento.Items.Clear()
                If txt_cp.Text = "" Then
                    Exit Sub
                End If
            Case "E"
                cmb_estado_empresa.Items.Clear()
                cmb_municipio_empresa.Items.Clear()
                cmb_asentamiento_empresa.Items.Clear()
                If txt_cp_empresa.Text = "" Then
                    Exit Sub
                End If
        End Select

        Try
            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, CP)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_x_CP"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP
                cIdEdo = Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString
                cIdMuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString
                Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, cIdEdo)
                Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, cIdMuni)

                Select Case cApartado
                    Case "D"
                        cmb_municipio.Items.Add(item_mun)
                        cmb_estado.Items.Add(item_edo)
                        Do While Not Session("rs").EOF
                            Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)
                            cmb_asentamiento.Items.Add(item)
                            Session("rs").movenext()
                        Loop
                    Case "E"
                        cmb_municipio_empresa.Items.Add(item_mun)
                        cmb_estado_empresa.Items.Add(item_edo)
                        Do While Not Session("rs").EOF
                            Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)
                            cmb_asentamiento_empresa.Items.Add(item)
                            Session("rs").movenext()
                        Loop
                    Case Else
                End Select

            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try

    End Sub

    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        busquedaCP(txt_cp.Text, "D", "", "")
    End Sub

    Protected Sub btn_buscadat_empresa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat_empresa.Click
        busquedaCP(txt_cp_empresa.Text, "E", "", "")
    End Sub

    Private Sub llena_vialidad(ByVal id As String)
        'Procedimiento que obtiene el catálogo de vialidades y las despliega en el combo correspondiente
        Dim elija As New ListItem("ELIJA", "-1")
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_VIALIDAD"
            Session("rs") = Session("cmd").Execute()

            If id = "cmb_vialidad" Then
                cmb_vialidad.Items.Clear()
                cmb_vialidad.Items.Add(elija)
                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
                    cmb_vialidad.Items.Add(item)
                    Session("rs").movenext()
                Loop
            End If

            If id = "cmb_vialidad_empresa" Then
                cmb_vialidad_empresa.Items.Clear()
                cmb_vialidad_empresa.Items.Add(elija)

                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
                    cmb_vialidad_empresa.Items.Add(item)
                    Session("rs").movenext()
                Loop
            End If

        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub GuardaDireccion(ByVal tipodir As Integer, ByVal idasenta As String, ByVal idloca As String, ByVal idmuni As String, ByVal idedo As String, ByVal idvi As String, ByVal calle As String, ByVal numext As String, ByVal numint As String, ByVal cp As String, ByVal referencia As String, ByVal tipoviv As String, ByVal antig As String, ByVal latitud As String, ByVal longitud As String, ByVal zoom As String, ByVal fechaven As String, ByVal residentes As Integer, ByVal renta As Integer)
        If Session("PERSONAID") > 0 Then
            Try
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                If Session("PERSONAID_F") Is Nothing Then
                    Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Else
                    Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID_F"))
                End If
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, tipodir)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 10, idasenta)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDLOCA", Session("adVarChar"), Session("adParamInput"), 10, idloca)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, idmuni)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, idedo)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, idvi)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, calle)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, numext)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, numint)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, cp)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, referencia)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TIPOVIV", Session("adVarChar"), Session("adParamInput"), 3, tipoviv)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ANTIG", Session("adVarChar"), Session("adParamInput"), 2, antig)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LATITUD", Session("adVarChar"), Session("adParamInput"), 30, latitud) 'latitud)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LONGITUD", Session("adVarChar"), Session("adParamInput"), 30, longitud) 'longitud)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ZOOM", Session("adVarChar"), Session("adParamInput"), 5, zoom)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TOTRESI", Session("adVarChar"), Session("adParamInput"), 15, residentes)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_DIRECCION"
                Session("cmd").Execute()
                lbl_status_dom.Text = "Guardado correctamente"
            Catch ex As Exception
                lbl_status_dom.Text = ex.ToString
            Finally
                Session("Con").Close()
            End Try
        End If

    End Sub

    Protected Sub btn_guardar_domicilio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_domicilio.Click
        Dim nResidentes As Integer
        nResidentes = 0
        If txt_residentes.Text <> "" Then
            nResidentes = CInt(txt_residentes.Text)
        End If

        If txt_referencias.Text.Length > 300 Then
            lbl_status_dom.Text = "Error: El número de caracteres en referencias no puede exceder a 300"
            Exit Sub
        End If

        If Session("PERSONAID") > 0 Then
            GuardaDireccion(1, cmb_asentamiento.SelectedItem.Value, "", cmb_municipio.SelectedItem.Value, cmb_estado.SelectedItem.Value, cmb_vialidad.SelectedItem.Value, txt_calle.Text, txt_exterior.Text, txt_interior.Text, txt_cp.Text, txt_referencias.Text, cmb_tipoviv.SelectedValue, txt_tiempo.Text, "", "", "", "", nResidentes, 0)
            actualiza_avance(4)
            carga_domicilio()
            define_avance()
        Else
            lbl_status_dom.Text = "Error: Falta capturar apartados anteriores"
        End If
    End Sub

    Private Sub GuardaTelefono(ByVal lada As String, ByVal tele As String, ByVal ext As String, ByVal tipo As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("PERSONAID") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 6, lada)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELE", Session("adVarChar"), Session("adParamInput"), 15, tele)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EXT", Session("adVarChar"), Session("adParamInput"), 5, ext)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODO", Session("adVarChar"), Session("adParamInput"), 11, IIf(Session("PERSONAID") Is Nothing, 1, 2))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PERSONA_TELEFONO"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub GuardaCorreo(ByVal email As String, ByVal tipo As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("PERSONAID") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 100, email)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODO", Session("adVarChar"), Session("adParamInput"), 11, IIf(Session("PERSONAID") Is Nothing, 1, 2))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PERSONA_CORREO"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Function guarda_info_contacto() As Boolean
        lbl_statustel.Text = ""
        Dim res As Boolean = True
        'REVISO SI SE CAPTURARON LOS DATOS  INDISPENSABLES PARA GUARDAR LOS TELEFONOS Y LOS GUARDO (7 digitos minimo tel  y 2 de lada)
        'PARTICULAR
        Dim casa As Integer, cel As Integer, ofi As Integer, rec As Integer
        casa = txt_telcasa.Text.Length
        cel = txt_telmov.Text.Length
        ofi = txt_telofi.Text.Length
        rec = txt_telrec.Text.Length

        If casa > 0 Then
            If casa >= 7 And txt_ladacasa.Text.Length >= 2 Then
                GuardaTelefono(txt_ladacasa.Text, txt_telcasa.Text, "", "PAR")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos"
                res = False
            End If
        End If


        'MOVIL
        If cel > 0 Then
            If cel >= 7 And txt_ladamov.Text.Length >= 2 Then
                GuardaTelefono(txt_ladamov.Text, txt_telmov.Text, "", "MOV")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos"
                res = False
            End If
        End If

        'TRABAJO
        If ofi > 0 Then
            If ofi >= 7 And txt_ladaofi.Text.Length >= 2 Then
                GuardaTelefono(txt_ladaofi.Text, txt_telofi.Text, txt_extofi.Text, "TRA")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos"
                res = False
            End If
        End If

        'RECADOS
        If rec > 0 Then
            If rec >= 7 And txt_ladarec.Text.Length >= 2 Then
                GuardaTelefono(txt_ladarec.Text, txt_telrec.Text, "", "REC")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos"
                res = False
            End If
        End If

        'REVISO SI TIENE CORREOS Y LOS GUARDO
        If txt_correoper.Text <> "" Then
            GuardaCorreo(txt_correoper.Text, "PAR")
        End If
        If txt_correoofi.Text <> "" Then
            GuardaCorreo(txt_correoofi.Text, "TRA")
        End If
        Return res
    End Function

    Protected Sub btn_guardar_contacto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_c.Click

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_PERSONA_FISICA_CONTACTO"
        Session("cmd").Execute()
        Session("Con").Close()
        If guarda_info_contacto() Then
            actualiza_avance(5)
            folderA(panel_contacto, "down")
            define_avance()
            lbl_statustel.Text = "Guardado correctamente"
        Else
            lbl_statustel.Text = "Error: Lada o Teléfono inválido"
        End If

    End Sub

    Private Sub guardar_laborales()
        If Session("PERSONAID") > 0 Then

            If chk_desempleo.Checked Then
                LimpiarLaborales()
                Try
                    Session("cmd") = New ADODB.Command()
                    Session("COn").Open()
                    Session("cmd").ActiveConnection = Session("COn")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").CommandText = "UPD_PERSONA_FISICA_LABORAL_DES"
                    Session("cmd").Execute()
                Catch ex As Exception
                Finally
                    Session("Con").Close()
                End Try
            Else
                If Session("EX_LAB") Is Nothing Then
                    GuardaDireccion(2, cmb_asentamiento_empresa.SelectedItem.Value, "", cmb_municipio_empresa.SelectedItem.Value, cmb_estado_empresa.SelectedItem.Value, cmb_vialidad_empresa.SelectedItem.Value, txt_calle_empresa.Text, txt_exterior_empresa.Text, txt_interior_empresa.Text, txt_cp_empresa.Text, txt_Referencia_empresa.Text, "", "0", "", "", "", "", 0, 0)
                Else
                    Try
                        Session("cmd") = New ADODB.Command()
                        Session("COn").Open()
                        Session("cmd").ActiveConnection = Session("COn")
                        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 2)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 5, cmb_asentamiento_empresa.SelectedItem.Value)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDLOCA", Session("adVarChar"), Session("adParamInput"), 10, "")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio_empresa.SelectedItem.Value)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado_empresa.SelectedItem.Value)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, cmb_vialidad_empresa.SelectedItem.Value)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, txt_calle_empresa.Text)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, txt_exterior_empresa.Text)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, txt_interior_empresa.Text)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp_empresa.Text)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, txt_Referencia_empresa.Text)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("TIPOVIV", Session("adVarChar"), Session("adParamInput"), 3, "")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("ANTIG", Session("adVarChar"), Session("adParamInput"), 2, "0")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("LATITUD", Session("adVarChar"), Session("adParamInput"), 30, "")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("LONGITUD", Session("adVarChar"), Session("adParamInput"), 30, "")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("ZOOM", Session("adVarChar"), Session("adParamInput"), 4, "")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("FECHAVENC", Session("adVarChar"), Session("adParamInput"), 10, "")
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("TOTRESI", Session("adVarChar"), Session("adParamInput"), 15, 0)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("parm") = Session("cmd").CreateParameter("MESRENTA", Session("adVarChar"), Session("adParamInput"), 15, 0)
                        Session("cmd").Parameters.Append(Session("parm"))
                        Session("cmd").CommandText = "UPD_DIRECCION"
                        Session("cmd").Execute()
                    Catch ex As Exception

                    Finally
                        Session("Con").Close()
                    End Try
                End If
                guardar_empresa()
            End If
            guardar_ingreso_ad()
        End If

    End Sub
    Private Sub guardar_empresa()
        If Session("EMPRESAID") Is Nothing Then
            Session("EMPRESAID") = 0
        End If

        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If Session("EX_LAB") Is Nothing Then
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("EMPRESA", Session("adVarChar"), Session("adParamInput"), 600, txt_empresa.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDEMPRESA", Session("adVarChar"), Session("adParamInput"), 10, Session("EMPRESAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFENOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFEPATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_paterno_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFEMATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFELADA", Session("adVarChar"), Session("adParamInput"), 6, txt_lada_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFETEL", Session("adVarChar"), Session("adParamInput"), 15, txt_telefono_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFEEXT", Session("adVarChar"), Session("adParamInput"), 5, txt_ext_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ANTIG", Session("adVarChar"), Session("adParamInput"), 2, txt_antiguedad_empresa.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 100, txt_puesto.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("INGRESO", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_sueldo.Text))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PERINGRESO", Session("adVarChar"), Session("adParamInput"), 3, cmb_periodo_pago.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("RELLAB", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion_laboral.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CORREOJEFE", Session("adVarChar"), Session("adParamInput"), 200, txt_correo_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_PERSONA_FISICA_LABORAL"
                Session("cmd").Execute()
            Else
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("EMPRESA", Session("adVarChar"), Session("adParamInput"), 600, txt_empresa.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDEMPRESA", Session("adVarChar"), Session("adParamInput"), 10, Session("EMPRESAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFENOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFEPATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_paterno_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFEMATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFELADA", Session("adVarChar"), Session("adParamInput"), 6, txt_lada_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFETEL", Session("adVarChar"), Session("adParamInput"), 15, txt_telefono_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("JEFEEXT", Session("adVarChar"), Session("adParamInput"), 5, txt_ext_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ANTIG", Session("adVarChar"), Session("adParamInput"), 2, txt_antiguedad_empresa.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 100, txt_puesto.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("INGRESO", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_sueldo.Text))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PERINGRESO", Session("adVarChar"), Session("adParamInput"), 3, cmb_periodo_pago.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("RELLAB", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion_laboral.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CORREOJEFE", Session("adVarChar"), Session("adParamInput"), 200, txt_correo_jefe.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_PERSONA_FISICA_LABORAL"
                Session("cmd").Execute()
            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub LimpiarLaborales()
        txt_empresa.Text = ""
        txt_cp_empresa.Text = ""
        cmb_estado_empresa.ClearSelection()
        cmb_municipio_empresa.ClearSelection()
        cmb_asentamiento_empresa.ClearSelection()
        cmb_vialidad_empresa.ClearSelection()
        txt_calle_empresa.Text = ""
        txt_exterior_empresa.Text = ""
        txt_interior_empresa.Text = ""
        txt_antiguedad_empresa.Text = ""
        txt_sueldo.Text = ""
        txt_puesto.Text = ""
        txt_Referencia_empresa.Text = ""
        txt_nombre_jefe.Text = ""
        txt_paterno_jefe.Text = ""
        txt_materno_jefe.Text = ""
        txt_correo_jefe.Text = ""
        txt_telefono_jefe.Text = ""
        txt_lada_jefe.Text = ""
        txt_ext_jefe.Text = ""
    End Sub


    Private Sub guardar_ingreso_ad()
        Dim cPerNeg As String, cPerOtro As String, cPerDiv As String, cPerInt As String
        Dim cDescNeg As String
        Dim cOrigenIng As String
        Dim nIngneg As Integer, nOtroing As Integer, nDivi As Integer, nIntereses As Integer

        nIngneg = 0
        If txt_adicionales.Text <> "" Then
            nIngneg = txt_adicionales.Text
        End If

        nOtroing = 0
        If txt_otros_ingresos.Text <> "" Then
            nOtroing = txt_otros_ingresos.Text
        End If

        nDivi = 0
        If txt_dividendos.Text <> "" Then
            nDivi = txt_dividendos.Text
        End If

        nIntereses = 0
        If txt_intereses.Text <> "" Then
            nIntereses = txt_intereses.Text
        End If

        cDescNeg = ""
        If txt_descripcion.Text <> "" Then
            cDescNeg = txt_descripcion.Text
        End If

        cPerNeg = ""
        If (cmb_periodo_adicional.SelectedItem.Value <> "-1") Then
            cPerNeg = cmb_periodo_adicional.SelectedItem.Value
        End If

        cPerOtro = ""
        If (cmb_periodo_otros.SelectedItem.Value <> "-1") Then
            cPerOtro = cmb_periodo_otros.SelectedItem.Value
        End If

        cPerDiv = ""
        If (cmb_periodo_dividendos.SelectedItem.Value <> "-1") Then
            cPerDiv = cmb_periodo_dividendos.SelectedItem.Value
        End If

        cPerInt = ""
        If (cmb_periodo_intereses.SelectedItem.Value <> "-1") Then
            cPerInt = cmb_periodo_intereses.SelectedItem.Value
        End If
        cOrigenIng = ""
        If txt_origen.Text <> "" Then
            cOrigenIng = txt_origen.Text
        End If

        Try
            'If Session("EX_ADING") = Nothing Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If Session("PERSONAID_F") Is Nothing Then
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Else
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID_F"))
            End If
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INGNEG", Session("adVarChar"), Session("adParamInput"), 10, nIngneg)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PERING", Session("adVarChar"), Session("adParamInput"), 3, cPerNeg)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DESCNEG", Session("adVarChar"), Session("adParamInput"), 300, cDescNeg)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("OTROSING", Session("adVarChar"), Session("adParamInput"), 10, nOtroing)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PEROTRO", Session("adVarChar"), Session("adParamInput"), 3, cPerOtro)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 100, cOrigenIng)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DIVIDENDOS", Session("adVarChar"), Session("adParamInput"), 10, nDivi)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PER_DIVIDENDOS", Session("adVarChar"), Session("adParamInput"), 15, cPerDiv)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INTERESES", Session("adVarChar"), Session("adParamInput"), 10, nIntereses)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PER_INTERESES", Session("adVarChar"), Session("adParamInput"), 15, cPerInt)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PERSONA_FISICA_INGRESOAD"
            Session("cmd").Execute()
            'Else
            '    Session("cmd") = New ADODB.Command()
            '    Session("Con").Open()
            '    Session("cmd").ActiveConnection = Session("Con")
            '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            '    Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("INGNEG", Session("adVarChar"), Session("adParamInput"), 10, nIngneg)
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("PERING", Session("adVarChar"), Session("adParamInput"), 3, cPerNeg)
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("DESCNEG", Session("adVarChar"), Session("adParamInput"), 300, cDescNeg)
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("OTROSING", Session("adVarChar"), Session("adParamInput"), 10, nOtroing)
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("PEROTRO", Session("adVarChar"), Session("adParamInput"), 3, cPerOtro)
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 100, cOrigenIng)
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            '    Session("cmd").Parameters.Append(Session("parm"))
            '    'Session("parm") = Session("cmd").CreateParameter("DIVIDENDOS", Session("adVarChar"), Session("adParamInput"), 10, nDivi)
            '    'Session("cmd").Parameters.Append(Session("parm"))
            '    'Session("parm") = Session("cmd").CreateParameter("PER_DIVIDENDOS", Session("adVarChar"), Session("adParamInput"), 15, cPerDiv)
            '    'Session("cmd").Parameters.Append(Session("parm"))
            '    'Session("parm") = Session("cmd").CreateParameter("INTERESES", Session("adVarChar"), Session("adParamInput"), 10, nIntereses)
            '    'Session("cmd").Parameters.Append(Session("parm"))
            '    'Session("parm") = Session("cmd").CreateParameter("PER_INTERESES", Session("adVarChar"), Session("adParamInput"), 15, cPerInt)
            '    'Session("cmd").Parameters.Append(Session("parm"))

            '    Session("cmd").CommandText = "UPD_PERSONA_FISICA_INGRESOAD"
            '    Session("cmd").Execute()
            'End If
        Catch ex As Exception
            lbl_status_lab.Text = ex.ToString
        Finally
            Session("Con").Close()
        End Try
        lbl_status_lab.Text = "Guardado correctamente"
    End Sub

    Private Sub carga_personales()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_EXTERNA_PERSONALES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            txt_nombre1.Text = Session("rs").Fields("NOMBRE1").Value
            txt_nombre2.Text = Session("rs").Fields("NOMBRE2").Value
            txt_paterno.Text = Session("rs").Fields("PATERNO").Value
            txt_materno.Text = Session("rs").Fields("MATERNO").Value
            txt_curp.Text = Session("rs").Fields("CURP").Value
            txt_rfc.Text = Session("rs").Fields("RFC").Value
            If Session("rs").Fields("SEXO").Value = "H" Then
                rad_sexcon1.Checked = True
            Else
                rad_sexcon2.Checked = True
            End If
            txt_fecha.Text = Left(Session("rs").Fields("FECHANAC").Value, 10)
            txt_notas.Text = Session("rs").Fields("NOTAS").Value
            cmb_pais_nac.ClearSelection()
            cmb_pais_nac.Items.FindByValue(Session("rs").Fields("IDPAIS").Value.ToString).Selected = True
            txt_id_oficial.Text = Session("rs").Fields("IDOFICIAL").Value
            cmb_nacionalidad.ClearSelection()
            cmb_nacionalidad.Items.FindByValue(Session("rs").Fields("ID_NAC").Value).Selected = True
            'cmb_estatus.Items.FindByValue(Session("rs").Fields("ESTTRAB").Value).Selected = True
        End If
        Session("Con").Close()
    End Sub

    Private Sub carga_ad()
        Dim edo_civ As String, num_rel As Integer, id_act As Integer, datos As Integer = 0
        Dim fecha_sat As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_GENERALES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            datos = 1
            'txt_actividad.Text = Session("rs").Fields("ACT").Value
            'cmb_actividad.ClearSelection()
            id_act = Session("rs").Fields("ID_ACT").Value
            cmb_sector.ClearSelection()
            cmb_sector.Items.FindByValue(Session("rs").Fields("ID_SEC").Value).Selected = True
            txt_nss.Text = Session("rs").Fields("NSS").Value
            txt_fiel.Text = Session("rs").Fields("FIEL").Value
            fecha_sat = Session("rs").Fields("FECHA_ALTA_SAT").Value
            If fecha_sat = "01/01/1900" Then
                txt_fecha_sat.Text = ""
            Else
                txt_fecha_sat.Text = fecha_sat
            End If
            cmb_grado.ClearSelection()
            cmb_grado.Items.FindByValue(Session("rs").Fields("GRA_ACA").Value.ToString).Selected = True
            edo_civ = Session("rs").Fields("EDO_CIVIL").Value.ToString
            cmb_edo_civil.ClearSelection()
            cmb_edo_civil.Items.FindByValue(edo_civ).Selected = True
            If edo_civ = "CAS" Or edo_civ = "UNL" Then
                lbl_regimen.Visible = True
                cmb_regimen.Visible = True
                lnk_conyuge.Visible = True
                lbl_odatoscompletos.Visible = True
                lbl_buscarpersonas1.Visible = True
                lnk_busqueda_coyuge.Visible = True
                lbl_datosconyugebuscar1.Visible = True
                lbl_datosconyugebuscar2.Visible = True
                cmb_regimen.ClearSelection()
                cmb_regimen.Items.FindByValue(Session("rs").Fields("REG_CON").Value).Selected = True
                lbl_datosconyugebuscar2.Text = Session("rs").Fields("NOM_CON").Value
                lbl_odatoscompletos.Text = "Si desea agregar un nuevo cónyuge capture su información "
                RequiredFieldValidator_regimen.Enabled = True
                Session("CONYUGE_ED") = 1
            Else
                Session("CONYUGE_ED") = 0
            End If
            txt_dependientes.Text = Session("rs").Fields("NUM_DEP").Value.ToString
            num_rel = Session("rs").Fields("NUM_REL").Value
        End If
        Session("Con").Close()
        If (num_rel > 0 And edo_civ <> "CAS") Or (num_rel > 1 And edo_civ = "CAS") Or (num_rel > 0 And edo_civ <> "UNL") Or (num_rel > 1 And edo_civ = "UNL") Then
            llena_conyuges_anteriores()
        End If
        Session("CONYUGEID") = 0
        cmb_actividad.Enabled = True
        llena_actividades_economicas()
        If (datos = 1) Then
            cmb_actividad.Items.FindByValue(id_act).Selected = True
        End If
    End Sub

    Private Sub llena_conyuges_anteriores()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtconyuges_anteriores As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_CONYUGES_ANTERIORES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtconyuges_anteriores, Session("rs"))
        Session("Con").Close()
        If dtconyuges_anteriores.Rows.Count > 0 Then
            dag_conyuge.Visible = True
            dag_conyuge.DataSource = dtconyuges_anteriores
            dag_conyuge.DataBind()
        Else
            dag_conyuge.Visible = False
        End If
    End Sub

    Private Sub carga_domicilio()
        Dim id_asen As String = ""
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_DIRECCION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            txt_cp.Text = Session("rs").Fields("CP").Value
            id_asen = Session("rs").Fields("ID_ASEN").Value.ToString
            txt_tiempo.Text = Session("rs").Fields("ANTIG").Value.ToString
            txt_calle.Text = Session("rs").Fields("CALLE").Value
            cmb_vialidad.Items.FindByValue(Session("rs").Fields("ID_VIAL").Value.ToString).Selected = True
            txt_exterior.Text = Session("rs").Fields("NUM_EXT").Value
            txt_interior.Text = Session("rs").Fields("NUM_INT").Value
            txt_referencias.Text = Session("rs").Fields("REF").Value
            txt_residentes.Text = Session("rs").Fields("TOTAL_RESIDENTES").Value
            If Session("rs").Fields("TIPO_VIV").Value <> "" Then
                cmb_tipoviv.Items.FindByValue(Session("rs").Fields("TIPO_VIV").Value).Selected = True
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
        cmb_asentamiento.Items.FindByValue(id_asen).Selected = True
    End Sub

    Private Sub carga_contacto()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_CONTACTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            txt_correoper.Text = Session("rs").Fields("MAIL_PAR").Value
            txt_correoofi.Text = Session("rs").Fields("MAIL_TRA").Value
            Do While Not Session("rs").EOF
                Select Case Session("rs").Fields("TIPO").Value
                    Case "PAR"
                        txt_ladacasa.Text = Session("rs").Fields("LADA").Value
                        txt_telcasa.Text = Session("rs").Fields("TEL").Value
                        Exit Select
                    Case "MOV"
                        txt_ladamov.Text = Session("rs").Fields("LADA").Value
                        txt_telmov.Text = Session("rs").Fields("TEL").Value
                        Exit Select
                    Case "REC"
                        txt_ladarec.Text = Session("rs").Fields("LADA").Value
                        txt_telrec.Text = Session("rs").Fields("TEL").Value
                        Exit Select
                    Case "TRA"
                        txt_ladaofi.Text = Session("rs").Fields("LADA").Value
                        txt_telofi.Text = Session("rs").Fields("TEL").Value
                        txt_extofi.Text = Session("rs").Fields("EXT").Value
                        Exit Select
                End Select
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_guardar_laboral_Click(sender As Object, e As EventArgs)
        Dim nAdicionales, nOtros, nDiv, nInt As Integer

        lbl_status_lab.Text = ""
        If Session("PERSONAID") > 0 Then

            If txt_Referencia_empresa.Text.Length > 300 Then
                lbl_status_lab.Text = "Error: El número de caracteres en referencias no puede exceder a 300"
                Exit Sub
            End If

            nAdicionales = 0
            If (txt_adicionales.Text.ToString) <> "" Then
                nAdicionales = CInt(txt_adicionales.Text)
            End If

            nOtros = 0
            If (txt_otros_ingresos.Text.ToString) <> "" Then
                nOtros = CInt(txt_otros_ingresos.Text)
            End If

            nDiv = 0
            If (txt_dividendos.Text.ToString) <> "" Then
                nDiv = CInt(txt_dividendos.Text)
            End If

            nInt = 0
            If (txt_intereses.Text.ToString) <> "" Then
                nInt = CInt(txt_intereses.Text)
            End If

            If (nAdicionales > 0 And cmb_periodo_adicional.SelectedValue = "-1" And txt_descripcion.Text = "") Or
               (nAdicionales > 0 And cmb_periodo_adicional.SelectedValue = "-1" And txt_descripcion.Text <> "") Or
               (nAdicionales > 0 And cmb_periodo_adicional.SelectedValue <> "-1" And txt_descripcion.Text = "") Or
               (nAdicionales = 0 And cmb_periodo_adicional.SelectedValue <> "-1" And txt_descripcion.Text = "") Or
               (nAdicionales = 0 And cmb_periodo_adicional.SelectedValue <> "-1" And txt_descripcion.Text <> "") Or
               (nAdicionales = 0 And cmb_periodo_adicional.SelectedValue = "-1" And txt_descripcion.Text <> "") Then
                lbl_status_lab.Text = "Error: Necesita capturar monto, periodicidad y descripción del negocio"
                AbrePestaña()
                Exit Sub
            End If

            If (nOtros > 0 And cmb_periodo_otros.SelectedValue = "-1" And txt_origen.Text = "") Or
               (nOtros > 0 And cmb_periodo_otros.SelectedValue = "-1" And txt_origen.Text <> "") Or
               (nOtros > 0 And cmb_periodo_otros.SelectedValue <> "-1" And txt_origen.Text = "") Or
               (nOtros = 0 And cmb_periodo_otros.SelectedValue <> "-1" And txt_origen.Text = "") Or
               (nOtros = 0 And cmb_periodo_otros.SelectedValue <> "-1" And txt_origen.Text <> "") Or
               (nOtros = 0 And cmb_periodo_otros.SelectedValue = "-1" And txt_origen.Text <> "") Then
                lbl_status_lab.Text = "Error: Necesita capturar monto, periodicidad y origen para otros ingresos"
                AbrePestaña()
                Exit Sub
            End If

            If (nDiv = 0 And cmb_periodo_dividendos.SelectedValue <> "-1") Or
                (nDiv > 0 And cmb_periodo_dividendos.SelectedValue = "-1") Then
                lbl_status_lab.Text = "Error: Necesita capturar monto y periodicidad para dividendos"
                AbrePestaña()
                Exit Sub
            End If

            If (nInt = 0 And cmb_periodo_intereses.SelectedValue <> "-1") Or
               (nInt > 0 And cmb_periodo_intereses.SelectedValue = "-1") Then
                lbl_status_lab.Text = "Error: Necesita capturar monto y periodicidad para intereses"
                AbrePestaña()
                Exit Sub
            End If

            folderA(panel_laborales, "down")
            folderA(panel_laborales, "down")
            guardar_laborales()
            actualiza_avance(6)
            carga_laborales()
            define_avance()


        End If
    End Sub
    Private Sub AbrePestaña()
        folderA(panel_laborales, "down")
        folderA(panel_laborales, "down")
        If chk_desempleo.Checked = True Then
            folderA(panel_ingresos_ad, "down")
            folderA(panel_ingresos_ad, "down")
        ElseIf chk_desempleo.Checked = False Then
            folderA(panel_empresa, "down")
            folderA(panel_empresa, "down")
            folderA(panel_empleo, "down")
            folderA(panel_empleo, "down")
        End If
    End Sub

    Private Sub carga_laborales()
        Dim nIdAsen As Integer

        Session("EX_LAB") = Nothing
        Session("EX_ADING") = Nothing
        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_PERSONA_FISICA_LABORALES_EDICION"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then
                If Session("rs").Fields("EX_LAB").Value = "1" Then
                    Session("EX_LAB") = "1"
                    txt_empresa.Text = Session("rs").Fields("EMPRESA").Value
                    txt_cp_empresa.Text = Session("rs").Fields("CP").Value
                    nIdAsen = Session("rs").Fields("ID_ASEN").Value
                    cmb_vialidad_empresa.ClearSelection()
                    If Session("rs").Fields("ID_VIA").Value > 0 Then
                        cmb_vialidad_empresa.Items.FindByValue(Session("rs").Fields("ID_VIA").Value).Selected = True
                    End If
                    txt_calle_empresa.Text = Session("rs").Fields("CALLE").Value
                    txt_exterior_empresa.Text = Session("rs").Fields("NUM_EXT").Value
                    txt_interior_empresa.Text = Session("rs").Fields("NUM_INT").Value
                    txt_Referencia_empresa.Text = Session("rs").Fields("REF").Value
                    txt_puesto.Text = Session("rs").Fields("PUESTO").Value
                    txt_antiguedad_empresa.Text = Session("rs").Fields("ANTIG").Value.ToString
                    txt_sueldo.Text = Session("rs").Fields("INGRESO").Value.ToString
                    cmb_relacion_laboral.ClearSelection()
                    If Session("rs").Fields("REL_LAB").Value <> "" Then
                        cmb_relacion_laboral.Items.FindByValue(Session("rs").Fields("REL_LAB").Value).Selected = True
                    End If
                    txt_nombre_jefe.Text = Session("rs").Fields("JEFE_NOMBRE").Value
                    txt_paterno_jefe.Text = Session("rs").Fields("JEFE_PATERNO").Value
                    txt_materno_jefe.Text = Session("rs").Fields("JEFE_MATERNO").Value
                    txt_lada_jefe.Text = Session("rs").Fields("JEFE_LADA").Value
                    txt_telefono_jefe.Text = Session("rs").Fields("JEFE_TEL").Value
                    txt_ext_jefe.Text = Session("rs").Fields("JEFE_EXT").Value
                    txt_sueldo.Text = Session("rs").Fields("INGRESO").Value
                    txt_correo_jefe.Text = Session("rs").Fields("CORREOJEFE").Value
                    If Session("rs").Fields("PER_ING").Value.ToString <> "-1" Then
                        cmb_periodo_pago.Items.FindByValue(Session("rs").Fields("PER_ING").Value.ToString).Selected = True
                    End If
                    chk_desempleo.Checked = False
                    panel_empresa.Visible = True
                    upnl_empleo.Visible = True
                    folderA(panel_empresa, "down")
                    folderA(panel_empresa, "down")
                    folderA(panel_empleo, "down")
                    folderA(panel_empleo, "down")
                End If

                ' INGRESOS ADICIONALES 
                If Session("rs").Fields("EX_ADING").Value = "1" Then
                    Session("EX_ADING") = "1"
                    txt_adicionales.Text = ""
                    If Not IsDBNull(Session("rs").Fields("ING_NEG").Value) Then
                        If (Session("rs").Fields("ING_NEG").Value > 0) Then
                            txt_adicionales.Text = Session("rs").Fields("ING_NEG").Value
                        End If
                    End If
                    txt_descripcion.Text = ""
                    If Not IsDBNull(Session("rs").Fields("DES_NEG").Value) Then
                        txt_descripcion.Text = Session("rs").Fields("DES_NEG").Value
                    End If
                    txt_otros_ingresos.Text = ""
                    If Not IsDBNull(Session("rs").Fields("OTR_ING").Value) Then
                        If (Session("rs").Fields("OTR_ING").Value > 0) Then
                            txt_otros_ingresos.Text = Session("rs").Fields("OTR_ING").Value
                        End If
                    End If
                    txt_dividendos.Text = ""
                    If Not IsDBNull(Session("rs").Fields("DIVIDENDOS").Value) Then
                        If (Session("rs").Fields("DIVIDENDOS").Value > 0) Then
                            txt_dividendos.Text = Session("rs").Fields("DIVIDENDOS").Value
                        End If
                    End If
                    txt_intereses.Text = ""
                    If Not IsDBNull(Session("rs").Fields("INTERESES").Value) Then
                        If (Session("rs").Fields("INTERESES").Value > 0) Then
                            txt_intereses.Text = Session("rs").Fields("INTERESES").Value
                        End If
                    End If
                    txt_origen.Text = ""
                    If Not IsDBNull(Session("rs").Fields("ORIG").Value) Then
                        txt_origen.Text = Session("rs").Fields("ORIG").Value
                    End If
                    If Not IsDBNull(Session("rs").Fields("AD_PER_ING").Value) Then
                        If Session("rs").Fields("AD_PER_ING").Value.ToString <> "-1" Then
                            cmb_periodo_adicional.Items.FindByValue(Session("rs").Fields("AD_PER_ING").Value.ToString).Selected = True
                        End If
                    End If
                    If Not IsDBNull(Session("rs").Fields("PER_OTR").Value.ToString) Then
                        If Session("rs").Fields("PER_OTR").Value.ToString <> "-1" Then
                            cmb_periodo_otros.Items.FindByValue(Session("rs").Fields("PER_OTR").Value.ToString).Selected = True
                        End If
                    End If

                    If Not IsDBNull(Session("rs").Fields("PERIODICIDAD_DIVIDENDOS").Value.ToString) Then
                        If Session("rs").Fields("PERIODICIDAD_DIVIDENDOS").Value.ToString <> "-1" Then
                            cmb_periodo_dividendos.Items.FindByValue(Session("rs").Fields("PERIODICIDAD_DIVIDENDOS").Value.ToString).Selected = True
                        End If
                    End If

                    If Not IsDBNull(Session("rs").Fields("PERIODICIDAD_INTERESES").Value.ToString) Then
                        If (Session("rs").Fields("PERIODICIDAD_INTERESES").Value.ToString) <> "-1" Then
                            cmb_periodo_intereses.Items.FindByValue(Session("rs").Fields("PERIODICIDAD_INTERESES").Value.ToString).Selected = True
                        End If
                    End If
                    folderA(panel_ingresos_ad, "down")
                    folderA(panel_ingresos_ad, "down")
                End If
                If Session("rs").Fields("EX_LAB").Value = "0" And Session("rs").Fields("EX_ADING").Value = "1" Then
                    chk_desempleo.Checked = True
                    panel_empresa.Visible = False
                    upnl_empleo.Visible = False
                    folderA(panel_ingresos_ad, "down")
                    folderA(panel_ingresos_ad, "down")
                    folderA(panel_empresa, "up")
                    folderA(panel_empresa, "up")
                    folderA(panel_empleo, "up")
                    folderA(panel_empleo, "up")
                    Dofocus(txt_adicionales)
                End If
                If Session("rs").Fields("EX_LAB").Value = "0" And Session("rs").Fields("EX_ADING").Value = "0" Then
                    chk_desempleo.Checked = False
                    panel_empresa.Visible = True
                    upnl_empleo.Visible = True
                    folderA(panel_empresa, "down")
                    folderA(panel_empresa, "down")
                    folderA(panel_empleo, "down")
                    folderA(panel_empleo, "down")
                    Dofocus(txt_empresa)
                End If
            Else
                folderA(panel_empresa, "down")
                folderA(panel_empresa, "down")
                folderA(panel_empleo, "down")
                folderA(panel_empleo, "down")
                Dofocus(txt_empresa)
            End If
        Catch ex As Exception
            lbl_status_lab.Text = ex.ToString
        Finally
            Session("Con").Close()
        End Try

        If txt_cp_empresa.Text <> "" Then
            busquedaCP(txt_cp_empresa.Text, "E", "", "")
        End If


    End Sub

    Private Sub MostrarDocumentos()
        lst_DocNoDigi.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPER", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DOCUMENTOS_TIPOPER"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCDOC").Value.ToString, Session("rs").Fields("TIPODOC").Value.ToString)
            lst_DocNoDigi.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub btn_Digitalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Digitalizar.Click

        Dim Posicion As Integer

        'Primero habilitara el dropdown para elegir un documento especifico para digitalizarlo (IFE, Pasaporte, Recibo de Luz, etc..)
        If lst_DocNoDigi.SelectedItem Is Nothing Then
            lbl_AlertaDigitaliza.Text = "Error: Seleccione un tipo de documento"
            lbl_AlertaDigitaliza.Visible = True
            lbl_AlertaNoBorrar.Text = ""
            lbl_AlertaVerBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
            lbl_FechaExp.Visible = False
        Else
            Posicion = lst_DocNoDigi.SelectedItem.Value().IndexOf(";")
            Session("CLAVE_DOCUMENTO") = Mid(lst_DocNoDigi.SelectedItem.Value(), Posicion + 2, 100)
            lbl_AlertaDigitaliza.Visible = False
            lst_DocumentosEspecificos.Visible = True
            txt_fechadoc.Text = ""
            LlenarDocumentos()
            lbl_FechaExp.Visible = False
        End If

    End Sub

    Private Sub LlenarDocumentos()

        'Llena la lista de Docuemntos (IFE, Recibo de Luz, etc...)
        lst_DocumentosEspecificos.Items.Clear()
        Dim elija As New ListItem("DOCUMENTO A ESCANEAR", "0")
        lst_DocumentosEspecificos.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPODOCUMENTO", Session("adVarChar"), Session("adParamInput"), 22, lst_DocNoDigi.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXP", Session("adVarChar"), Session("adParamInput"), 30, Session("ESTATUS_EXPEDIENTE"))
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 30, Session("FOLIO"))
        'Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DOCUMENTOS_X_TIPO"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value.ToString, Session("rs").Fields("CATDOCTOS_ID_DOCUMENTO").Value.ToString)
            lst_DocumentosEspecificos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub lst_DocumentosEspecificos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lst_DocumentosEspecificos.SelectedIndexChanged

        'Revisa si algun Documento fue seleccionado
        If lst_DocumentosEspecificos.SelectedItem.Value <> "0" Then
            btn_ElegirDocumento.Visible = True
            ' Variable de sesion para guardar el id del documento seleccionado
            Session("DOCUMENTOID") = lst_DocumentosEspecificos.SelectedItem.Value
            btn_Subir.Enabled = True
            FileUpload1.Enabled = True
            lbl_AlertaDigitaliza.Text = ""
            lbl_AlertaNoBorrar.Text = ""
            lbl_AlertaVerBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
            lbl_AlertaDigitaliza.Visible = True
        End If

    End Sub

    Private Sub VerificarDocsCompletos()

        'Metodo para saber si todos los documentos de un expediente estan completos
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCUMENTACION_COMPLETA_INCOMPLETA"
        Session("rs") = Session("cmd").Execute()
        Dim AUX As Integer = Session("rs").Fields("CONT").Value
        If AUX = 0 Then
            btn_Insertar_ColaValidacion.Enabled = True 'Si esta completo el expediente habilita el boton de insertar a la cola de validacion
        Else
            btn_Insertar_ColaValidacion.Enabled = False 'No esta completo el expediente no se habilita el boton
        End If
        Session("Con").Close()

    End Sub

    Private Sub MostrarDocumentosDigitalizados()

        ' Muestra los Documentos que ya han sido Digitalizados
        lst_DocDigi.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCUMENTOS_PER_DIGITALIZADOS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem((Session("rs").Fields("CATTIPDOC_DESCRIPCION").Value.ToString + " - " + Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value.ToString), Session("rs").Fields("MSTDOCPERSONA_CLAVE_DOCUMENTO").Value.ToString)
            lst_DocDigi.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Protected Sub btn_subir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Subir.Click
        Try
            ' Read the file and convert it to Byte Array
            Dim filePath As String = FileUpload1.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim ext As String = Path.GetExtension(filename)
            Dim contenttype As String = String.Empty

            'Set the contenttype based on File Extension
            Select Case ext
                Case ".pdf"
                    contenttype = "application/pdf"
                    Exit Select
            End Select

            If contenttype = String.Empty Then
                lbl_UploadEstatus.Text = "El tipo de archivo no es reconocido (solo se aceptan *.PDF)"
                lbl_UploadEstatus.Visible = True
            Else

                Dim size As Integer = FileUpload1.PostedFile.ContentLength
                Dim Tamdig As Integer = Session("TAMDIG")

                'consulta Session("DOCUMENTOID").ToString para saber el tamaño Max

                'Stored Procedure 
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDDOCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTOID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_TAMANO_MAX_DIGITAL_X_DOCUMENTO"
                Session("rs") = Session("cmd").Execute()

                If Not Session("rs").EOF Then
                    If Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value = "EXPEDIENTE" Then
                        'Nuevo Valor
                        Tamdig = Session("rs").Fields("MAX_TAMANO_DIGIT").Value
                    Else
                        Tamdig = Session("TAMDIG")
                    End If
                    Session("Con").Close()
                    'lbl_titulo.Text = FileUpload1.PostedFile.ContentLength.ToString + " " + Session("DOCUMENTOID") + " " + (20170 * 1024).ToString
                End If

                If size >= (Tamdig * 1024) Then '20170
                    lbl_UploadEstatus.Text = "El archivo excede el maximo permitido para este documento"
                    lbl_UploadEstatus.Visible = True
                    lst_DocumentosEspecificos.Visible = False
                    btn_ElegirDocumento.Visible = False
                    btn_Subir.Enabled = False
                    FileUpload1.Enabled = False
                    Exit Sub
                End If

                If size <= (Tamdig * 1024) Then
                    Dim fs As Stream = FileUpload1.PostedFile.InputStream
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(fs.Length)

                    'lbl_titulo.Text = "" + CStr(size)
                    Dim strConnString As String 'Ruta de la BD
                    strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                    Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

                    Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("INS_MST_DOCPERSONA", sqlConnection)
                    'Stored Procedure 
                    sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

                    'Parametros para la incesrion del Stored Procedure
                    sqlCmdObj.Parameters.AddWithValue("@IDDOCUMENTO", Session("DOCUMENTOID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@IDPERSONA", Session("PERSONAID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@ARCHIVO", bytes)
                    sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@SESION", Session("Sesion").ToString)
                    'sqlCmdObj.Parameters.AddWithValue("@ESTATUS_EXP", Session("ESTATUS_EXPEDIENTE").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@CLAVE_DOCUMENTO", Session("CLAVE_DOCUMENTO").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@FECHA_DOC", txt_fechadoc.Text)

                    sqlConnection.Open()
                    sqlCmdObj.ExecuteNonQuery()
                    sqlConnection.Close()

                    lbl_UploadEstatus.Text = "El archivo ha sido guardado correctamente"
                    lbl_UploadEstatus.Visible = True

                End If
                folderA(panel_laborales, "up", False)
                folderA(panel_adicionales, "up", False)
                folderA(panel_contacto, "up", False)
                folderA(panel_dependientes, "up", False)
                folderA(panel_domicilio, "up", False)
                folderA(panel_generales, "up", False)
                folderA(panel_digitalizacion, "down", False)

            End If
        Catch ex As Exception
        Finally
            MostrarDocumentos() 'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
            MostrarDocumentosDigitalizados() 'Mostrara los documentos ya Dititalizados
            VerificarDocsCompletos() 'Verifica si ya estan digitalizados todos los requerimientos
            lst_DocumentosEspecificos.Visible = False
            btn_ElegirDocumento.Visible = False
            btn_Subir.Enabled = False
            FileUpload1.Enabled = False
            Dofocus(lst_DocNoDigi)
        End Try

    End Sub

    Protected Sub btn_Ver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Ver.Click

        'Boton para ver un documento ya digitalizado y poder revisar un documento con mas detalle
        If lst_DocDigi.SelectedItem Is Nothing Then
            lbl_AlertaVerBorrar.Text = "Error: Seleccione un documento"
            lbl_AlertaVerBorrar.Visible = True
            lbl_AlertaDigitaliza.Text = ""
            lbl_AlertaNoBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
        Else
            Session("DOCUMENTO_DIGITALIZADO") = lst_DocDigi.SelectedItem.Value() 'Variable de sesion para tener el docuemnto elegido
            lbl_AlertaVerBorrar.Text = ""

            Response.Redirect("~/DIGITALIZADOR/DIGI_MOSTRAR.aspx")

        End If

    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        'Boton para eliminar un Documento de la lista de Documentos Digitalizados
        If lst_DocDigi.SelectedItem Is Nothing Then
            lbl_AlertaVerBorrar.Text = "Error: Seleccione un documento"
            lbl_AlertaVerBorrar.Visible = True
            lbl_AlertaDigitaliza.Text = ""
            lbl_AlertaNoBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
        Else
            Session("DOCUMENTO_DIGITALIZADO") = lst_DocDigi.SelectedItem.Value() 'Variable de sesion para tener el docuemnto elegido
            lbl_AlertaVerBorrar.Visible = False
            EliminarDocumentoDigitalizado() 'Metodo para eliminar el documento seleccionado
            MostrarDocumentos() 'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
            MostrarDocumentosDigitalizados() 'Mostrara los documentos Dititalizados
            VerificarDocsCompletos() 'Verifica si los requisitos estan completos
        End If
    End Sub

    Private Sub EliminarDocumentoDigitalizado()

        'Metodo para eliminar el Documento Seleccionado
        'No permite eliminar un documento que ya halla sido aprobado por validadores
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTO_DIGITALIZADO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXPEDIENTE", Session("adVarChar"), Session("adParamInput"), 30, Session("ESTATUS_EXPEDIENTE").ToString)
        'Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_DOCUMENTOS_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim AUX As Integer = Session("rs").Fields("ALERTA").Value
        If AUX = 0 Then
            lbl_AlertaNoBorrar.Text = "Error: El Documento ya esta Aceptado por validador, no puede ser borrado"
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_Insertar_ColaValidacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Insertar_ColaValidacion.Click

        EstatusEnDigitalizacion()
        'lbl_TerminoDigitFolio.Text = "Expediente listo para validacion con FOLIO: " + Session("FOLIO")
        'ModalPopupExtender.Show()
        Dim menuPanel As Panel
        menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

        If Not menuPanel Is Nothing Then
            menuPanel.Enabled = True
        End If

        'EstatusEnDigitalizacion()
        Session("VENGODE") = "ALTAPERSONA.aspx"
        Response.Redirect("CORE_PER_PERSONAEXT_BUSQUEDA.aspx")

    End Sub

    Private Sub EstatusEnDigitalizacion()

        Try
            'Bandera para mostrar si el exepdiente esta en uso
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_ESTATUS_DIGIT_PERSONA"
            Session("cmd").Execute()

        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try

    End Sub


    'folder close or open
    Private Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            If toogle.Attributes("class").IndexOf("down") >= 0 Then
                content.Attributes.CssStyle.Add("display", "block")
            Else
                head.Attributes.CssStyle.Add("background", "#113964 !important")
                head.Attributes.CssStyle.Add("color", "#fff")
                head.Attributes.CssStyle.Add("border", "solid 1px transparent")
                head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptS" & pnl.ClientID, "$('#" & content.ClientID & "').show('6666',null);", True)
            End If

        ElseIf accion.Equals("up") Then
            If toogle.Attributes("class").IndexOf("up") >= 0 Then
                content.Attributes.CssStyle.Add("display", "none")
            Else
                head.Attributes.CssStyle.Add("background", "#F8F9F9 !important")
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
                head.Attributes.CssStyle.Add("background", "#113964 !important")
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
                head.Attributes.CssStyle.Add("background", "#113964 !important")
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


    Protected Sub lnk_semafor_Click(sender As LinkButton, e As EventArgs)
        Dim up_panel_id As String = sender.ID.Replace("lnk_to_", "upnl_")
        Dim panel_id As String = sender.ID.Replace("lnk_to_", "panel_")

        Try
            Dim up_panel As UpdatePanel = TryCast(FindControlRecursive(Me.Page, up_panel_id), UpdatePanel)
            Dim panel As HtmlGenericControl = TryCast(FindControlRecursive(Me.Page, panel_id), HtmlGenericControl)

            folderA(panel, "down")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptScrollTo", " jQuery('.wrapper').getNiceScroll(0).doScrollTop($('#" & TryCast(FindControlRecursive(Me.Page, "content_" & panel_id), HtmlGenericControl).ClientID & "').position().top, 666);", True)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptFolder" & panel.ClientID, "$('#" & panel.ClientID & " .panel_header_folder').click(function(event) {var folder_content =$(this).siblings('.panel-body').children('.panel-body_content'); var toogle = $(this).children('.panel_folder_toogle'); if (toogle.hasClass('up')){toogle.removeClass('up');toogle.addClass('down');folder_content.show('6666', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.parent().css({ 'background': '#113964 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px'});} else if (toogle.hasClass('down')){toogle.removeClass('down');folder_content.hide('333', function () { jQuery('.wrapper').getNiceScroll().resize(); });toogle.addClass('up');toogle.parent().css({ 'background': '#F8F9F9 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px' });}});", True)

            up_panel.Update()
        Catch ex As Exception


            Label1.Text = Label1.Text & "ññ" & panel_id
            Label1.Text = Label1.Text & "{--}" & ex.ToString
        End Try

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


End Class