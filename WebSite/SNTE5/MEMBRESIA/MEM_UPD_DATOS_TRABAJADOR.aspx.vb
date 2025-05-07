Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class MEM_UPD_DATOS_TRABAJADOR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Datos Trabajador", "ACTUALIZACIÓN DE DATOS")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            bancos()
            cargaRegiones()
            cargaDelegaciones()
            cargaCCT()
            cargaMotivos()
            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona
            lnk_vermanual.Attributes.Add("onclick", "window.open('/DocPlantillas/MANUAL.pdf" + "');")

            If Session("VENGODE") = "CRED_EXP_APARTADO1.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO2.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO2_PLAN_INSTITUCIONAL.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO3.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO4.aspx" Or Session("VENGODE") = "ADMDIGITALIZADOR.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO6.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO5.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO7.aspx" Or Session("VENGODE") = "PRELLENADOSOLICITUDAPCUENTA.ASPX" Or Session("VENGODE") = "CNFEXP_CAP_APARTADO1.ASPX" Or Session("VENGODE") = "CRED_EXP_APARTADO8.aspx" Or Session("VENGODE") = "CNFEXP_PPE.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO9.aspx" Or Session("VENGODE") = "CRED_EXP_AVALES.aspx" Or Session("VENGODE") = "UNI_REFERENCIAS.aspx" Then



                tbx_rfc.Text = Session("NUMTRAB").ToString

                'LlenaComites()
                txt_IdCliente.Text = Session("PERSONAID").ToString
                lbl_nompros.Text = Session("CLIENTE").ToString
                ' lbl_subtitdom.Text = Session("DOMICILIO").ToString

                pnl_expedientes.Visible = True
                Estatus_panel.Visible = True

                folderA(pnl_expedientes, "down")
                'habilito tabs y asigno focus al tab de configuracion
                folderA(div_selCliente, "up")

            ElseIf Session("VENGODE") = "DetalleCartaCredito.aspx" Then

                txt_IdCliente.Text = Session("PERSONAID")
                Session("CLIENTE") = Session("PROSPECTO")
                'Session("PROSPECTO") = Nothing
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "down")
            ElseIf Session("VENGODE") = "ExpedientesFACT.aspx" Then

                txt_IdCliente.Text = Session("PERSONAID")
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "down")
            ElseIf Session("VENGODE") = "AdmPlazoFijo.aspx" Then

                txt_IdCliente.Text = Session("PERSONAID")
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "down")
            ElseIf Session("VENGODE") = "DIGI_GLOBAL.aspx" Then



                tbx_rfc.Text = Session("NUMTRAB").ToString
                div_NombrePersonaBusqueda.Visible = True
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                'LlenaComites()
                txt_IdCliente.Text = Session("PERSONAID").ToString
                lbl_nompros.Text = Session("CLIENTE").ToString

                ' lbl_subtitdom.Text = Session("DOMICILIO").ToString

                pnl_expedientes.Visible = True
                Estatus_panel.Visible = True
                folderA(pnl_expedientes, "down")
                'habilito tabs y asigno focus al tab de configuracion
                folderA(div_selCliente, "up")

                'Regreso valores de número de trabajador e nstitución cuando salga de configurador'
            ElseIf Session("VENGODE") = "CRED_EXP_GENERAL" Or Session("VENGODE") = "CORE_PER_PERSONA" Then
                tbx_rfc.Text = Session("NUMTRAB").ToString

                obtieneId()

            Else
                'limpio variables
                LimpiaVariables()

            End If

        End If
        ' txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_Continuar.ClientID + "')")
        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            validaNumPersona()
        End If

        lbliduser.Text = Session("USERID")
        lblidsesion.Text = Session("Sesion")
        lblidsucu.Text = Session("SUCID")

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptPaneles", "$('.panel_folder_toogle').click(function(event) { var folder_content=$(this).parent().siblings('.panel-body').children('.panel-body_content');if($(this).hasClass('up')){$(this).removeClass('up');$(this).addClass('down');folder_content.show('6666',null);$(this).parent().css({'background': '#696462	 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px' });}else if($(this).hasClass('down')){$(this).removeClass('down');folder_content.hide('333',null);$(this).addClass('up');$(this).parent().css({ 'background': '#696462 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px'  });}});", True)

    End Sub

    Private Sub LimpiaDatosExpedientes()
        lbl_nompros.Text = ""
    End Sub

    'LIMPIA VARIABLES CREADAS
    Private Sub LimpiaVariables()
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("CLIENTE") = Nothing
        Session("VENGODE") = Nothing
        Session("FOLIOCCRED") = Nothing
        Session("PRODUCTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("CONFCOMPLETO") = Nothing
        Session("idperbusca") = Nothing
        Session("TIPOPROD") = Nothing
        Session("APARTADO") = Nothing
        Session("TIPOPER") = Nothing
        Session("CLASIFICACION") = Nothing
        Session("ASPX") = Nothing
        Session("ID") = Nothing
        Session("GENERAR") = Nothing
        Session("IDCARTA") = Nothing
        Session("IDFACTPAGO") = Nothing
        Session("FOLIOLINEA") = Nothing

    End Sub

    Private Sub Mayoredad()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_VERIFICACION_EDAD"
        Session("rs") = Session("cmd").Execute()
        Session("RESPUESTA") = Session("rs").fields("RESPUESTA").value.ToString
        Session("Con").Close()


    End Sub

    'FUNCION QUE VERIFICA SI EL CLIENTE TIENE MORA
    Private Function TieneMora() As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 15, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_MORA"
        Session("rs") = Session("cmd").Execute()

        Dim RESULTADO As Boolean
        If Session("rs").Fields("RESULTADO").value.ToString = "SI" Then
            RESULTADO = True
        Else
            RESULTADO = False
        End If
        Session("Con").Close()

        Return RESULTADO
    End Function



    Private Sub validaNumPersona()
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: RFC incorrecto."
                pnl_expedientes.Visible = False
                Estatus_panel.Visible = False
                folderA(div_selCliente, "down")
                div_NombrePersonaBusqueda.Visible = False
            Else
                ValidaPersona(txt_IdCliente.Text)
                'Metodo que llena el droplist con los tipos de productos
                Session("CLIENTE") = Session("PROSPECTO")
                Session("PROSPECTO") = Nothing
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                tbx_clabe_conf.Text = ""
            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
            ValidaPersona(txt_IdCliente.Text)
        End If
    End Sub

    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        'Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
            folderA(div_selCliente, "down")
        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Public Sub BuscarIDCliente()

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA_DELEGACIONES"
        Session("rs") = Session("cmd").Execute()


        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
            Session("SALARIO") = Session("rs").fields("SALARIO").value.ToString
            Session("CAPACIDAD") = Session("rs").fields("CAPACIDAD").value.ToString
            Session("OTROSCRED") = Session("rs").fields("OTROSCRED").value.ToString
            Session("PORPLAZORENO") = Session("rs").fields("PLAZORENO").value.ToString
            Session("NUMCTRL") = Session("rs").Fields("NUMCONT").value.ToString
            Session("NUMINST") = Session("rs").Fields("INSTI").value.ToString
            Session("ANTIGUEDAD") = Session("rs").Fields("ANTIGUEDAD").value.ToString
            Session("AGR_REGION") = Session("rs").Fields("REGION").value.ToString
            Session("AGR_DELEGACION") = Session("rs").Fields("DELEGACION").value.ToString
            Session("AGR_CT") = Session("rs").Fields("CCT").value.ToString
            Session("APORT") = Session("rs").Fields("APORTACION").value.ToString
            Session("PEN") = Session("rs").Fields("PENSION").value.ToString


            ddl_cct.SelectedValue = Session("rs").Fields("CCT").value.ToString
            ddl_region.SelectedValue = Session("rs").Fields("REGION").value.ToString

            ddl_delegacion.SelectedValue = Session("rs").Fields("DELEGACION").value.ToString
            txt_correo.Text = Session("rs").Fields("CORREO").value.ToString
            txt_num.Text = Session("rs").Fields("TELEFONO").value.ToString

            If Session("rs").Fields("ID_P").value.ToString >= 110 Or Session("rs").Fields("ID_P").value.ToString = -1 Then
                cmb_banco.SelectedValue = -1
                text_cmb_banco.Text = "Banco:"
                rfv_ddl_banco.Enabled = False
            Else
                cmb_banco.SelectedValue = Session("rs").Fields("ID_P").value.ToString
                text_cmb_banco.Text = "*Banco:"
                rfv_ddl_banco.Enabled = True
            End If

            If Session("rs").Fields("CLABE").value.ToString <> "" Then
                txt_clabe.Text = Session("rs").Fields("CLABE").value.ToString
                text_clabe_verifi.Text = "*CLABE:"
                rfv_txt_clabe.Enabled = True
            Else
                text_clabe_verifi.Text = "CLABE:"
                rfv_txt_clabe.Enabled = False
                txt_clabe.Text = ""
            End If


            If Session("rs").Fields("CLABE").value.ToString <> "" Then
                tbx_clabe_conf.Text = Session("rs").Fields("CLABE").value.ToString
                text_clabe_conf_verifi.Text = "*CONFIRMA CLABE:"
                rfv_tbx_clabe_conf.Enabled = True
            Else
                text_clabe_conf_verifi.Text = "CONFIRMA CLABE:"
                rfv_tbx_clabe_conf.Enabled = False
                tbx_clabe_conf.Text = ""
            End If

            Combo_Estatus.SelectedValue = Session("rs").Fields("MOTIVO").value.ToString
            Notas_baja.Text = Session("rs").Fields("NOTAS").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""

        ElseIf Existe = -1 Then
            pnl_expedientes.Visible = True
            Estatus_panel.Visible = True
            folderA(div_selCliente, "up")
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de control"
            lbl_NombrePersonaBusqueda.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            LimpiaDatosExpedientes()
        Else
            lbl_statusc.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lblidperson.Text = Session("PERSONAID")
            lbl_nompros.Text = Session("CLIENTE").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True



        End If

        ValidaPersona(txt_IdCliente.Text)

    End Sub


    Public Sub ValidaPersona(idcliente As Integer)
        ViewState("ALERTAS") = ""
        ViewState("NUM") = 0
        ViewState("NUMC") = 0
        ViewState("RES") = ""
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("SEL_VALIDACION_PERSONA", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("IDCLIENTE", SqlDbType.Int)
            Session("parm").Value = idcliente
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                ViewState("RES") = myReader.GetString(0)
                ViewState("EXISTE") = myReader.GetInt32(1)
                ViewState("NUM") = myReader.GetInt32(2)
                ViewState("NUMC") = myReader.GetInt32(3)
                ViewState("ALERTAS") = ViewState("ALERTAS") + "<br />" + myReader.GetString(4)
            End While
            myReader.Close()
        End Using

        If ViewState("RES") = "NO" Then
            'If ViewState("NUM") <> "5" Then
            pnl_expedientes.Visible = True
            Estatus_panel.Visible = True

            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "down")
            lbl_maxreest.Text = ""


        Else

            pnl_expedientes.Visible = True
            Estatus_panel.Visible = True

            folderA(div_selCliente, "down")
            folderA(pnl_expedientes, "down")
            lbl_maxreest.Text = ""
            'llena droplist con los expedientes pendientes del cliente
        End If

        If ViewState("EXISTE") = 1 Then
            pnl_expedientes.Visible = True
            Estatus_panel.Visible = True

        Else
            pnl_expedientes.Visible = False
            Estatus_panel.Visible = False
        End If

    End Sub


    'folder close or open
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#696462	 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#696462	 !important")
            head.Attributes.CssStyle.Add("color", "fff")
            head.Attributes.CssStyle.Add("border", "solid 1px #fff")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion

    End Sub
    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        tbx_clabe_conf.Text = ""
        lbl_estatus.Text = ""
        Alerta_estatus.Text = ""
        validaNumPersona()
    End Sub


    Private Sub bancos()

        cmb_banco.Items.Clear()
        cmb_banco.Items.Add(New ListItem("ELIJA", "-1"))
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
        Session("Con").Close()
    End Sub

    Private Sub CargaInfoBanco()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CLABE_BANCO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            If Session("rs").Fields("TIPO_CUENTA").Value.ToString = 1 Then

                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                'txt_cat.Text = Session("rs").Fields("CATEGORIA").Value.ToString
                'txt_anios.Text = Session("rs").Fields("ANIOS_SERV").Value.ToString
                txt_correo.Text = Session("rs").Fields("CORREO").Value.ToString
                txt_num.Text = Session("rs").Fields("TELEFONO").Value.ToString

            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 2 Then

                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                'txt_cat.Text = Session("rs").Fields("CATEGORIA").Value.ToString
                'txt_anios.Text = Session("rs").Fields("ANIOS_SERV").Value.ToString
                txt_correo.Text = Session("rs").Fields("CORREO").Value.ToString
                txt_num.Text = Session("rs").Fields("TELEFONO").Value.ToString

            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 3 Then

                cmb_banco.SelectedIndex = "-1"
                cmb_banco.Enabled = False
                'rfv_ddl_banco.Enabled = False
                txt_clabe.Text = ""
                txt_clabe.Enabled = False
                rfv_txt_clabe.Enabled = False
                tbx_clabe_conf.Text = ""
                tbx_clabe_conf.Enabled = False
                rfv_tbx_clabe_conf.Enabled = False
                'txt_cat.Text = Session("rs").Fields("CATEGORIA").Value.ToString
                'txt_anios.Text = Session("rs").Fields("ANIOS_SERV").Value.ToString
                txt_correo.Text = Session("rs").Fields("CORREO").Value.ToString
                txt_num.Text = Session("rs").Fields("TELEFONO").Value.ToString
            End If

        End If

        Session("Con").close()

    End Sub


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
                ddl_delegacion.Items.Add(New ListItem("R" + Session("rs").Fields("REGION").Value + " " + Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value.ToString))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
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
            lbl_estatus.Text = "Error: El digito verificador no coincide"
            Return False
        End If



    End Function

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

    Private Sub btn_guardaBanco_Click(sender As Object, e As EventArgs) Handles btn_guardaBanco.Click
        lbl_estatus.Text = ""
        If Len(txt_num.Text) > 0 Then
            If Len(txt_num.Text) <> 10 Then
                lbl_estatus.Text = "Error: El número de teléfono no puede ser menor a 10 dígitos."
                Exit Sub
            End If
        End If

        If txt_correo.Text <> "" Then
            If ValidaCorreo(txt_correo.Text) = False Then
                lbl_estatus.Text = "Error: El correo tiene formato invalido."
                Exit Sub
            End If
        End If

        Try

            If ddl_region.SelectedValue = 0 Then
                GuardaBancos()
                'pnl_modal_confirmar.Visible = True
                'modal_confirmar.Show()
            Else

                If rfv_tbx_clabe_conf.Enabled = False And txt_clabe.Text = "" And tbx_clabe_conf.Text = "" And cmb_banco.SelectedItem.Value = -1 Then
                    pnl_modal_confirmar.Visible = True
                    modal_confirmar.Show()

                Else
                    Dim Validacion As Boolean = ValidacionBancosVb()
                    If Validacion = True Then
                        Dim Validaciondigito As Boolean = ValidaDigitoValidador(tbx_clabe_conf.Text.ToString)
                        If Validaciondigito = True Then
                            ' GuardaBancos()
                            pnl_modal_confirmar.Visible = True
                            modal_confirmar.Show()
                        End If
                    End If


                End If

            End If

        Catch ex As Exception
            lbl_estatus.Text = ex.ToString
        End Try
    End Sub

    Protected Sub btn_confirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_confirmar.Click
        AvisoP(1)
        GuardaBancos()

    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_cancelar.Click
        AvisoP(0)
    End Sub

    Private Function ValidaCorreo(ByVal correo As String) As Boolean
        Return Regex.IsMatch(correo, ("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$"))
    End Function

    Private Function ValidacionBancosVb() As Boolean

        If txt_clabe.Text <> tbx_clabe_conf.Text Then
            lbl_estatus.Text = "Error: La CLABE no coincide con el campo de confirmación."
            Return False
        End If

        If Len(txt_clabe.Text) <> 18 Then
            lbl_estatus.Text = "Error: El número de CLABE INTERBANCARIA debe ser de 18 posiciones."
            Return False
            'End If
        End If

        If Len(txt_clabe.Text) = 18 Then
            Dim Validacion As String = ValidaCLABE()
            If Validacion <> "OK" Then
                If Validacion = "FALSE" Then
                    lbl_estatus.Text = "Error: La CLABE no coincide con el Banco seleccionado."
                    Return False
                Else

                    lbl_estatus.Text = "Error: CLABE ya registrada para el agremiado: " + Validacion.ToString
                    Return False
                End If
            End If
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

    Protected Sub GuardaBancos()

        Dim Respuesta_UPD As String = ""
        Dim valor As Integer
        Dim correo As String
        Dim nombre_j As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINSTFINAN", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_banco.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 50, txt_clabe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 150, txt_correo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 50, txt_num.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOVAL", Session("adVarChar"), Session("adParamInput"), 1, 2)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_REGION", Session("adVarChar"), Session("adParamInput"), 100, CInt(ddl_region.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_DELEGACION", Session("adVarChar"), Session("adParamInput"), 100, CInt(ddl_delegacion.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_CCT", Session("adVarChar"), Session("adParamInput"), 100, CInt(ddl_cct.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 25, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_INSTFINAN_CLABE_DELEGADOS"
        Session("rs") = Session("cmd").Execute()
        Respuesta_UPD = Session("rs").fields("VALIDACION").value.ToString
        valor = Session("rs").fields("BANDERA").value.ToString
        correo = Session("rs").fields("CORREO").value.ToString
        nombre_j = Session("rs").fields("NOMBRE").value.ToString

        Session("Con").Close()

        If Respuesta_UPD = "TRUE" Then
            If valor = 1 And correo <> "" Then
                EnviaCorreo(correo, nombre_j)
            End If
            lbl_estatus.Text = "Guardado correctamente"
        Else
            lbl_estatus.Text = "Error: Solo se pueden actualizar datos para la delegación asignada, no puede guardar datos para otras delegaciones"
        End If

    End Sub


    Protected Sub AvisoP(Valor As Integer)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 18, tbx_rfc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 50, Valor)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 25, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_AVISO_P"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub
    Private Sub ddl_region_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_region.SelectedIndexChanged
        If ddl_region.SelectedItem.Value = -1 Then
            lbl_estatus.Text = ""
        End If
    End Sub

    Private Sub ddl_delegacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_delegacion.SelectedIndexChanged
        If ddl_delegacion.SelectedItem.Value = -1 Then
            lbl_estatus.Text = ""
        End If
    End Sub

    Private Sub ddl_cct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_cct.SelectedIndexChanged
        If ddl_cct.SelectedItem.Value = -1 Then
            lbl_estatus.Text = ""
        End If
    End Sub

    Private Sub cmb_banco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_banco.SelectedIndexChanged
        If cmb_banco.SelectedItem.Value = -1 Then
            lbl_estatus.Text = ""
        End If
    End Sub

    'Private Sub btn_reporte_Click(sender As Object, e As EventArgs) Handles btn_reporte.Click

    '    ' Se Crea spreadsheet en la ruta deseada.
    '    Dim spreadsheetDocument As SpreadsheetDocument = SpreadsheetDocument.Create("C:\hola.xlsx", SpreadsheetDocumentType.Workbook)
    '    ' Add a WorkbookPart to the document.
    '    Dim workbookpart As WorkbookPart = spreadsheetDocument.AddWorkbookPart
    '    workbookpart.Workbook = New Workbook
    '    ' Add a WorksheetPart to the WorkbookPart.
    '    Dim worksheetPart As WorksheetPart = workbookpart.AddNewPart(Of WorksheetPart)()
    '    worksheetPart.Worksheet = New Worksheet(New SheetData())

    '    ' Add Sheets to the Workbook.
    '    Dim sheets As Sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(Of Sheets)(New Sheets())

    '    ' Append a new worksheet and associate it with the workbook.
    '    Dim sheet As Sheet = New Sheet
    '    sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart)
    '    sheet.SheetId = 1
    '    sheet.Name = "Hoja 1"
    '    sheets.Append(sheet)

    '    Dim worksheet As New Worksheet()
    '    Dim sheetData As New SheetData()

    '    Dim row1 As New Row() With {.RowIndex = 1}

    '    Dim row7 As New Row() With {.RowIndex = 3}

    '    Dim cell5 As New Cell() With {.CellReference = "E1", .DataType = CellValues.[String], .CellValue = New CellValue("REPORTE DE DELEGACION")}

    '    Dim cell7_1 As New Cell() With {.CellReference = "A3", .DataType = CellValues.[String], .CellValue = New CellValue("REGION")}
    '    Dim cell7_2 As New Cell() With {.CellReference = "B3", .DataType = CellValues.[String], .CellValue = New CellValue("DELEGACION")}
    '    Dim cell7_3 As New Cell() With {.CellReference = "C3", .DataType = CellValues.[String], .CellValue = New CellValue("CCT")}
    '    Dim cell7_4 As New Cell() With {.CellReference = "D3", .DataType = CellValues.[String], .CellValue = New CellValue("RFC")}
    '    Dim cell7_5 As New Cell() With {.CellReference = "E3", .DataType = CellValues.[String], .CellValue = New CellValue("NOMBRE")}
    '    Dim cell7_6 As New Cell() With {.CellReference = "F3", .DataType = CellValues.[String], .CellValue = New CellValue("CORREO")}
    '    Dim cell7_7 As New Cell() With {.CellReference = "G3", .DataType = CellValues.[String], .CellValue = New CellValue("TELEFONO")}
    '    Dim cell7_8 As New Cell() With {.CellReference = "H3", .DataType = CellValues.[String], .CellValue = New CellValue("BANCO")}
    '    Dim cell7_9 As New Cell() With {.CellReference = "I3", .DataType = CellValues.[String], .CellValue = New CellValue("CLABE")}


    '    row1.Append(cell5)
    '    row7.Append(cell7_1)
    '    row7.Append(cell7_2)
    '    row7.Append(cell7_3)
    '    row7.Append(cell7_4)
    '    row7.Append(cell7_5)
    '    row7.Append(cell7_6)
    '    row7.Append(cell7_7)
    '    row7.Append(cell7_8)
    '    row7.Append(cell7_9)


    '    sheetData.Append(row1)
    '    sheetData.Append(row7)

    '    worksheet.Append(sheetData)

    '    Session("Con").Open()
    '    Session("rs") = CreateObject("ADODB.Recordset")
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = CommandType.StoredProcedure
    '    Session("cmd").CommandTimeout = 10000000
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 25, Session("USERID"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "REP_MEMBRESIA_DELEGACIONES"
    '    Session("rs") = Session("cmd").Execute()

    '    Dim count As Integer = Session("rs").Fields.Count()

    '    Dim COORDX As Integer
    '    COORDX = 4

    '    If Not Session("rs").eof Then
    '        Do While Not Session("rs").EOF

    '            Dim row8 As New Row With {.RowIndex = COORDX}

    '            Dim index As Integer = 0
    '            While index <= count - 1
    '                Dim letraux As String = LetraColumna(index)
    '                Dim cell8_1 As New Cell() With {.CellReference = letraux + CStr(COORDX), .DataType = CellValues.[String], .CellValue = New CellValue(Session("rs").Fields(index).Value.ToString())}
    '                row8.Append(cell8_1)
    '                index += 1
    '            End While
    '            sheetData.Append(row8)
    '            COORDX += 1
    '            Session("rs").movenext()

    '        Loop
    '    End If

    '    Session("Con").Close()

    '    worksheetPart.Worksheet = worksheet
    '    ' Close the document.
    '    spreadsheetDocument.Close()

    '    Response.Clear()
    '    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '    Response.AddHeader("content-disposition", "attachment;filename=Reporte Delegacion.xlsx")
    '    Response.WriteFile("C:\hola.xlsx")
    '    Response.End()


    'End Sub

    Private Sub DelHDFile(ByVal File1 As String)
        If File.Exists(File1) Then
            Dim fi As New System.IO.FileInfo(File1)
            If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
                fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
            End If
        Else
            lbl_estatus.Text = "Alerta: El archivo ha sido movido o eliminado"
        End If
        System.IO.File.Delete(File1)
    End Sub

    Private Sub Btn_Guardar_Estatus_Click(sender As Object, e As EventArgs) Handles Btn_Guardar_Estatus.Click
        Dim Respuesta_Act As String = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINSTFINAN", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_banco.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 25, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ACTIVO_GUAR", Session("adVarChar"), Session("adParamInput"), 100, CInt(Combo_Estatus.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOTIVOS", Session("adVarChar"), Session("adParamInput"), 2000, Notas_baja.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_ACTIVO_MEMBRESIA"
        Session("rs") = Session("cmd").Execute()
        Respuesta_Act = Session("rs").fields("VALIDACION").value.ToString
        Session("Con").Close()

        If Respuesta_Act = "TRUE" Then
            Alerta_estatus.Text = "Guardado correctamente"
        Else
            Alerta_estatus.Text = "Solo se pueden actualizar datos para personas asignadas a tu delegación, no puede actualizar datos para personas en diferentes delegaciones"
        End If
    End Sub



    Private Sub EnviaCorreo(valor_correo As String, valor_nombre As String)

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Dim subject As String = "Actualización de Ubicación de Agremiado"

        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE Sección 5</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a): " + valor_nombre + "</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<tr><td>Le informamos que se presentó una actualización de datos en el sistema de membresías de la Sección 5, para el agremiado: " + lbl_NombrePersonaBusqueda.Text + " con RFC " + tbx_rfc.Text + "." + "</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<tr><td>Dicho agremidado perteneciente a su delegación ha sido reasignado a una nueva ubicación (región:" + ddl_region.SelectedItem.ToString + ", delegación: " + ddl_delegacion.SelectedItem.ToString + ")." + "</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("</table>")
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<br/><br/><br/>")
        sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
        sbhtml.Append("</table>")

        Try

            Dim dirsmtp As String = Session("MAIL_SERVER")
            Dim portsmtp As String = Session("MAIL_SERVER_PORT")
            Dim correoSSL As Boolean = Session("MAIL_SERVER_SSL")
            Dim correoUser As String = Session("MAIL_SERVER_USER")
            Dim correoPass As String = Session("MAIL_SERVER_PWD")
            Dim usuarioDom As String = AppSettings("correoUserDom")
            Dim objSMTP As New Net.Mail.SmtpClient(dirsmtp)
            Dim mailFrom As String = Session("MAIL_SERVER_FROM")
            Dim mail As New Net.Mail.MailMessage
            Dim envio As String = Session("MAIL_SERVER_ENVIO")

            If envio = 1 Then

                mail.Subject = subject
                mail.IsBodyHtml = True
                mail.From = New System.Net.Mail.MailAddress(mailFrom)
                mail.To.Add(valor_correo)
                objSMTP.Port = portsmtp

                If Not (cc.Trim().Equals(String.Empty)) Then
                    If cc.Split(";").Count > 1 Then
                        For i As Integer = 0 To cc.Split(";").Count - 1
                            mail.Bcc.Add(cc.Split(";")(i))
                        Next
                    Else
                        mail.Bcc.Add(cc)
                    End If
                End If
                'cuerpo de correo
                mail.Body = sbhtml.ToString
                'credenciales de autentication               
                objSMTP.Credentials = New System.Net.NetworkCredential(correoUser, correoPass, usuarioDom)
                'ssl para envio de correo
                objSMTP.EnableSsl = correoSSL
                'envio de correo
                objSMTP.Send(mail)
            End If


        Catch ex As Exception
        End Try
        sbhtml.Clear()

    End Sub

    Private Function LetraColumna(Numero As Integer) As String

        Dim colLetter As String = String.Empty

        If Numero <= 25 Then
            colLetter = Chr(65 + Numero)
        Else
            Dim valor1 As Integer = System.Math.Truncate((Numero / 26)) - 1
            Dim primero As String = String.Empty
            Dim segundo As String = String.Empty

            Dim valor2 As Integer = Numero - (26 * System.Math.Truncate((Numero / 26)))
            primero = Chr(65 + valor1)
            segundo = Chr(65 + valor2)

            colLetter = primero + segundo
        End If

        Return colLetter

    End Function

End Class