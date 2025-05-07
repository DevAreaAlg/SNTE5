Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class MEM_ESTADOS_CTA_TRABAJADOR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Estados de Cuenta", "ESTADOS DE CUENTA")
        If Not Me.IsPostBack Then
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

        txt_correo.Enabled = False
        txt_num.Enabled = False
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
                pnl_expedientes.Visible = False
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
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC_DELEGADOS"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
            folderA(div_selCliente, "down")
            lbl_statusc.Text = "Error: RFC incorrecto."

        ElseIf Existe = 2 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
            folderA(div_selCliente, "down")
            lbl_statusc.Text = "El agremiado no pertenece a tu región/delegación"
        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

        cmb_sistema.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SISTEMAS_X_RFC"
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("SISTEMA").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_sistema.Items.Add(item)
            Session("rs").movenext()
        Loop

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


            Combo_Estatus.SelectedValue = Session("rs").Fields("MOTIVO").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""

        ElseIf Existe = -1 Then
            pnl_expedientes.Visible = True
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

            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "down")
            lbl_maxreest.Text = ""


        Else

            pnl_expedientes.Visible = True

            folderA(div_selCliente, "down")
            folderA(pnl_expedientes, "down")
            lbl_maxreest.Text = ""
            'llena droplist con los expedientes pendientes del cliente
        End If

        If ViewState("EXISTE") = 1 Then
            pnl_expedientes.Visible = True

        Else
            pnl_expedientes.Visible = False
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
        lbl_estatus.Text = ""
        validaNumPersona()
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



    Private Sub EnviaCorreo(email As String, ruta1 As String, ruta2 As String, ruta3 As String, ruta4 As String)


        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Dim subject As String = "Estado de cuenta"

        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE Sección 5</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a): " + lbl_NombrePersonaBusqueda.Text + "</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<tr><td>Se realiza la entrega de su estado de cuenta solicitado por medio de su representante de región/delegación.</td></tr>")
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
                If ruta1 <> "" Then
                    Dim File As String = ruta1
                    Dim Data As Net.Mail.Attachment = New Net.Mail.Attachment(File)
                    Data.Name = "Estado de Cuenta Ahorro.pdf"
                    mail.Attachments.Add(Data)

                End If
                If ruta2 <> "" Then
                    Dim File2 As String = ruta2
                    Dim Data2 As Net.Mail.Attachment = New Net.Mail.Attachment(File2)
                    Data2.Name = "Estado de Cuenta Prestamo.pdf"
                    mail.Attachments.Add(Data2)

                End If
                If ruta3 <> "" Then
                    Dim File3 As String = ruta3
                    Dim Data3 As Net.Mail.Attachment = New Net.Mail.Attachment(File3)
                    Data3.Name = "Estado de Cuenta Aportacion Homologada.pdf"
                    mail.Attachments.Add(Data3)

                End If
                If ruta4 <> "" Then
                    Dim File4 As String = ruta4
                    Dim Data4 As Net.Mail.Attachment = New Net.Mail.Attachment(File4)
                    Data4.Name = "Estado de Cuenta RFC Anterior.pdf"
                    mail.Attachments.Add(Data4)

                End If



                mail.Subject = subject
                mail.IsBodyHtml = True
                mail.From = New System.Net.Mail.MailAddress(mailFrom)
                mail.To.Add(email)
                'mail.Attachments.Add(data2)
                'mail.Attachments.Add(data3)
                'mail.Attachments.Add(data4)



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

            lbl_estatus.Text = "Éxito: Se envió el correo."

        Catch ex As Exception
            lbl_estatus.Text = ex.Message
        End Try
        sbhtml.Clear()

    End Sub

    Private Sub cmb_resultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_sistema.SelectedIndexChanged
        If cmb_sistema.SelectedItem.Value = 2 Then
            btn_genera_estado_p.Enabled = "False"
        Else
            btn_genera_estado_p.Enabled = "True"
        End If

        obtieneIdSis()
    End Sub

    Private Sub obtieneIdSis()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SISTEMA", Session("adVarChar"), Session("adParamInput"), 20, cmb_sistema.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC_SISTEMA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            txt_IdCliente.Text = CStr(idp)
            Session("PERSONAID") = txt_IdCliente.Text
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Private Sub btn_genera_estado_a_Click(sender As Object, e As EventArgs) Handles btn_genera_estado_a.Click
        Session("RFC_PERSONA") = tbx_rfc.Text
        VER_ESTADOCTA(Session("PERSONAID"), Session("RFC_PERSONA"))
    End Sub


    Private Sub VER_ESTADOCTA(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String)

        EstadoCuentaAportaciones(ID_PERSONA, RFC_PERSONA, "", False)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ESTADO_CUENTA_APORTACIONES(RFC:" + Session("NUMTRAB") + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)

            .End()
            .Flush()
        End With
    End Sub

    Private Sub EstadoCuentaAportaciones(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String, ByVal SISTEMA As String, ByVal EnvioMasivo As Boolean)

        Dim periodo As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty
        Dim clave_CT As String = String.Empty
        Dim municipio As String = String.Empty
        Dim rfc As String = String.Empty
        Dim nombre As String = String.Empty

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            periodo = Session("rs").Fields("PERIODO").Value.ToString()
            region = Session("rs").Fields("REGION").Value.ToString()
            delegacion = Session("rs").Fields("DELEGACION").Value.ToString()
            clave_CT = Session("rs").Fields("CLAVE_CT").Value.ToString()
            municipio = Session("rs").Fields("MUNICIPIO").Value.ToString()
            rfc = Session("rs").Fields("RFC").Value.ToString()
            nombre = Session("rs").Fields("NOMBRE").Value.ToString()


        End If
        Session("Con").Close()

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuenta.pdf")
        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        With document
            .AddAuthor("SNTE -  SNTE")
            .AddCreationDate()
            .AddCreator("SNTE - Estado de Cuenta")
            .AddSubject("Estado de Cuentas")
            .AddTitle("Estado de Cuenta")
            .AddKeywords("Estado de Cuenta")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter
        'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + RFC_PERSONA + "_" + SISTEMA + "_APORTACIONES.pdf", FileMode.Create))
        End If

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim EstadoCuenta As iTextSharp.text.pdf.PdfImportedPage

        EstadoCuenta = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)


        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 450  'X empieza de izquierda a derecha
        Y = 672 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer


        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

        Y = Y - 15
        X = 450

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)
        Y = Y - 15
        X = 450

        'Delegación
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
        Y = Y - 15
        X = 450

        'Municipio
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

        Y = Y - 15
        X = 450

        'Clave CT
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, clave_CT, X, Y, 0)

        Y = Y - 15
        X = 450


        'Región
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

        Y = 648
        X = 80

        'Nombre
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, nombre.ToString, X, Y, 0)

        Y = Y - 15
        X = 65



        Dim prestamosAhorro As Decimal = 0.00
        Dim aportacionTrab As Decimal = 0.00
        Dim interesesAhorro As Decimal = 0.00
        Dim aportacionEntidad As Decimal = 0.00
        Dim totalPrestamo As Decimal = 0.00
        Dim SubTotalAhorro As Decimal = 0.00
        Dim totalDescuento As Decimal = 0.00
        Dim prestComplementarios As Decimal = 0.00
        Dim pagosAnticipados As Decimal = 0.00
        Dim interesesComplementaros As Decimal = 0.00
        Dim totalPagado As Decimal = 0.00
        Dim total As Decimal = 0.00
        Dim restoPrestamo As Decimal = 0.00
        Dim ajustePrestamo As Decimal = 0.00
        Dim ajustePension As Decimal = 0.00
        Dim ajustePeriodoAnterior As Decimal = 0.00
        Dim saldoAhorro As Decimal = 0.00
        Dim saldoAFavor As Decimal = 0.00
        Dim totalPrestamoComplemenatario As Decimal = 0.00
        Dim InteresesInversiones As Decimal = 0.00
        Dim InteresesPrestamos As Decimal = 0.00



        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ESTADO_CTA"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            prestamosAhorro = Session("rs").Fields("PRESTAMO_AHORRO").Value.ToString()
            interesesAhorro = Session("rs").Fields("INTERESES_AHORRO").Value.ToString()
            totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
            totalDescuento = Session("rs").Fields("TOTAL_DESCUENTOS").Value.ToString()
            pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
            totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
            restoPrestamo = Session("rs").Fields("RESTO_PRESTAMO").Value.ToString()
            saldoAFavor = Session("rs").Fields("SALDO_A_FAVOR").Value.ToString()

            aportacionTrab = Session("rs").Fields("APORTACION_TRAB").Value.ToString()
            aportacionEntidad = Session("rs").Fields("APORTACION_ENTIDAD").Value.ToString()
            InteresesInversiones = Session("rs").Fields("INTERESES_INVERSIONES").Value.ToString()
            InteresesPrestamos = Session("rs").Fields("INTERESES_PRESTAMO").Value.ToString()
            SubTotalAhorro = Session("rs").Fields("SUBTOTAL_AHORRO").Value.ToString()
            prestComplementarios = Session("rs").Fields("PRESTAMOS_COMPLEMENTARIOS").Value.ToString()
            interesesComplementaros = Session("rs").Fields("INTERESES_COMPLEMENTARIOS").Value.ToString()
            totalPrestamoComplemenatario = Session("rs").Fields("TOTAL_PRESTAMO_COMPLEMENTARIOS").Value.ToString()
            ajustePrestamo = Session("rs").Fields("AJUSTE_PRESTAMO").Value.ToString()
            ajustePension = Session("rs").Fields("AJUSTE_PENSION").Value.ToString()
            ajustePeriodoAnterior = Session("rs").Fields("AJUSTE_SALDO_PRESTAMO_ANTERIOR").Value.ToString()
            saldoAhorro = Session("rs").Fields("SALDO_AHORRO").Value.ToString()

        End If
        Session("Con").Close()

        X = 170  'X empieza de izquierda a derecha
        Y = 558 'Y empieza de abajo hacia arriba

        'Prestamo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestamosAhorro), X, Y, 0)


        X = 170
        Y = 538
        'Intereses Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesAhorro), X, Y, 0)

        X = 170
        Y = 517
        'Total Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        X = 170
        Y = 499
        'Total Descuentos 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalDescuento), X, Y, 0)


        X = 170
        Y = 480
        'Pagos Anticipados 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


        X = 170
        Y = 461
        'Total Pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)


        X = 170
        Y = 442
        'Resto Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(restoPrestamo), X, Y, 0)


        X = 170
        Y = 423
        'Saldo a Favor 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAFavor), X, Y, 0)


        Y = 565
        X = 470
        'Aportaciones Trabajador
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionTrab), X, Y, 0)


        Y = Y - 20
        X = 470
        'Aportaciones Entidad
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionEntidad), X, Y, 0)


        Y = Y - 18
        X = 470
        'Intereses por inversiones
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesInversiones), X, Y, 0)

        Y = Y - 20
        X = 470
        'Intereses por préstamos
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesPrestamos), X, Y, 0)


        Y = Y - 19
        X = 470
        'SubTotal Ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(SubTotalAhorro), X, Y, 0)


        Y = Y - 19
        X = 470
        'Préstamo Complementarios
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestComplementarios), X, Y, 0)


        Y = Y - 19
        X = 470
        'Intereses
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesComplementaros), X, Y, 0)


        Y = Y - 19
        X = 470
        'Total Préstamo Complementario
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamoComplemenatario), X, Y, 0)

        Y = Y - 19
        X = 470
        'Ajuste Préstamo de ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePrestamo), X, Y, 0)


        Y = Y - 20
        X = 470
        'Ajuste Pension 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePension), X, Y, 0)


        Y = Y - 20
        X = 470
        'Ajuste Saldo Préstamos periodo anterior
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePeriodoAnterior), X, Y, 0)


        Y = Y - 39
        X = 470
        'Saldo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAhorro), X, Y, 0)


        Y = Y - 52
        X = 30


        'ESTADO DE CUENTA 

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)

        'XT = X
        'YT = Y - 6

        'QUINCENA
        'XT = XT + 50

        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Quincena", XT, YT, 0)

        ''AÑO
        'XT = XT + 130
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Año", XT, YT, 0)


        ''APORTACION TRABAJADOR 
        'XT = XT + 125
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "AP Trabajador", XT, YT, 0)


        ''APORTACION ENTIDAD
        'XT = XT + 110
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "AP Entidad", XT, YT, 0)

        ''TOTAL
        'XT = XT + 100
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)
        Y = Y - 18
        X = 30

        Session("Con").Open()

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        X = 70

        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF
                If Y < 90 Then
                    Y = 645
                    X = 65
                    cb.EndText()

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuenta.pdf")

                    cb = writer.DirectContent
                    EstadoCuenta = writer.GetImportedPage(Reader, 2)
                    cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    cb.SetFontAndSize(bf, 9)


                    Y = 710
                    X = 30

                    'XT = X
                    'YT = Y + 35
                Else
                    Y = Y - 14
                    X = 60
                End If
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 8)

                X = 65
                'QUINCENA
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("QUINCENA").Value.ToString, X, Y, 0)

                'AÑO
                X = X + 100
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("ANIO").Value.ToString, X, Y, 0)

                'APORTACION TRABAJADOR 
                X = X + 130
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("AP_TRABAJADOR").Value.ToString), X, Y, 0)

                'APORTACION ENTIDAD
                X = X + 170
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("AP_ENTIDAD").Value.ToString), X, Y, 0)

                'TOTAL
                X = X + 96
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("TOTAL").Value.ToString), X, Y, 0)


                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()
        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuenta.pdf")

        cb = writer.DirectContent
        EstadoCuenta = writer.GetImportedPage(Reader, 3)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        cb.SetFontAndSize(bf, 9)


        Y = 675
        X = 75

        cb.EndText()
        document.Close()

    End Sub

    Private Sub btn_genera_estado_p_Click(sender As Object, e As EventArgs) Handles btn_genera_estado_p.Click
        Session("RFC_PERSONA") = tbx_rfc.Text
        VER_ESTADOCTA_PRESTAMOS(Session("PERSONAID"), Session("RFC_PERSONA"))
    End Sub

    Private Sub VER_ESTADOCTA_PRESTAMOS(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String)

        EstadoCuentaPrestamos(ID_PERSONA, RFC_PERSONA, "", False)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ESTADO_CUENTA_PRESTAMOS(RFC:" + Session("NUMTRAB") + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With
    End Sub

    Private Sub EstadoCuentaPrestamos(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String, ByVal SISTEMAS As String, ByVal EnvioMasivo As Boolean)

        Dim periodo As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty
        Dim clave_CT As String = String.Empty
        Dim municipio As String = String.Empty
        Dim rfc As String = String.Empty
        Dim nombre As String = String.Empty


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            periodo = Session("rs").Fields("PERIODO").Value.ToString()
            region = Session("rs").Fields("REGION").Value.ToString()
            delegacion = Session("rs").Fields("DELEGACION").Value.ToString()
            clave_CT = Session("rs").Fields("CLAVE_CT").Value.ToString()
            municipio = Session("rs").Fields("MUNICIPIO").Value.ToString()
            rfc = Session("rs").Fields("RFC").Value.ToString()
            nombre = Session("rs").Fields("NOMBRE").Value.ToString()


        End If
        Session("Con").Close()

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")
        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        With document
            .AddAuthor("SNTE -  SNTE")
            .AddCreationDate()
            .AddCreator("SNTE - Estado de Cuenta")
            .AddSubject("Estado de Cuentas")
            .AddTitle("Estado de Cuenta")
            .AddKeywords("Estado de Cuenta")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + RFC_PERSONA + "_" + SISTEMAS + "_PRESTAMOS.pdf", FileMode.Create))
        End If


        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim EstadoCuenta As iTextSharp.text.pdf.PdfImportedPage

        EstadoCuenta = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 440  'X empieza de izquierda a derecha
        Y = 674 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

        Y = Y - 15
        X = 440

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)

        Y = Y - 15
        X = 440

        'Delegación
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
        Y = Y - 15
        X = 440

        'Municipio
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

        Y = Y - 15
        X = 440

        'Clave CT
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, clave_CT, X, Y, 0)

        Y = Y - 15
        X = 440


        'Región
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

        Y = 648
        X = 80

        'Nombre
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, nombre.ToString, X, Y, 0)

        Y = Y - 15
        X = 65


        Dim producto As String = String.Empty
        Dim fechaPrestamo As String = String.Empty
        Dim plazo As String = String.Empty
        Dim autorizado As Decimal = 0.00
        Dim interes As Decimal = 0.00
        Dim totalPrestamo As Decimal = 0.00
        Dim pagosAnticipados As Decimal = 0.00
        Dim totalPagado As Decimal = 0.00
        Dim resto As Decimal = 0.00


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADOCTA_PRESTAMOS"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then


            producto = Session("rs").Fields("PRODUCTO").Value.ToString()
            fechaPrestamo = Session("rs").Fields("FECHA_PRESTAMO").Value.ToString()
            plazo = Session("rs").Fields("PLAZO").Value.ToString()
            autorizado = Session("rs").Fields("AUTORIZADO").Value.ToString()
            interes = Session("rs").Fields("INTERES").Value.ToString()
            totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
            pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
            totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
            resto = Session("rs").Fields("RESTO").Value.ToString()

        End If
        Session("Con").Close()



        X = 440  'X empieza de izquierda a derecha
        Y = 557 'Y empieza de abajo hacia arriba


        'Producto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, producto, X, Y, 0)


        Y = 542
        X = 440
        'Fecha Préstamo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, fechaPrestamo, X, Y, 0)


        Y = Y - 15
        X = 440
        'Plazo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, plazo, X, Y, 0)


        Y = Y - 15
        X = 440
        'Autorizado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(autorizado), X, Y, 0)

        Y = Y - 14
        X = 440
        'Intereses
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interes), X, Y, 0)


        Y = Y - 17
        X = 440
        'Total Préstamo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        Y = Y - 334
        X = 462
        'Total Préstamo 2
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        Y = Y - 19
        X = 462
        'Pagos Anticipados
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


        Y = Y - 18
        X = 462
        'Total pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)

        Y = Y - 56
        X = 462
        'Resto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(resto), X, Y, 0)


        Y = 400
        X = 40

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)


        Session("Con").Open()

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA_PRESTAMOS"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        X = 70

        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF
                If Y < 90 Then
                    Y = 645
                    X = 65
                    cb.EndText()

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")

                    cb = writer.DirectContent
                    EstadoCuenta = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    cb.SetFontAndSize(bf, 9)


                    Y = 710
                    X = 30

                    'XT = X
                    'YT = Y + 35
                Else
                    Y = Y - 14
                    X = 60
                End If
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 8)

                X = 90
                'QUINCENA
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("QUINCENA").Value.ToString, X, Y, 0)

                'AÑO
                X = X + 120
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("ANIO").Value.ToString, X, Y, 0)

                'DESCUENTO 
                X = X + 138
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("DESCUENTO").Value.ToString), X, Y, 0)

                'SALDO
                X = X + 175
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SALDO").Value.ToString), X, Y, 0)

                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

        'GLOSARIO TERMINOS
        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")

        cb = writer.DirectContent
        EstadoCuenta = writer.GetImportedPage(Reader, 2)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        'Se cambia el tamaño y el tipo de letra para agregar la nota al PDF
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = 65
        Y = 675


        cb.EndText()
        document.Close()


    End Sub

    Private Sub bnt_enviar_correo_Click(sender As Object, e As EventArgs) Handles bnt_enviar_correo.Click
        If txt_correo.Text = "" Then
            lbl_estatus.Text = "No cuenta con un correo registrato para envio de estados de cuenta"
        Else
            Validarutas()
        End If

    End Sub

    Private Sub Validarutas()

        Dim Valor As String = ""
        Dim Valor2 As String = ""
        Dim Valor3 As String = ""
        Dim Valor4 As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_RUTAS_ESTADO_CTA"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESSION", Session("adVarChar"), Session("adParamInput"), 20, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Valor = Session("rs").Fields("APORTACIONES").Value.ToString()
            Valor2 = Session("rs").Fields("PRESTAMO").Value.ToString()
            Valor3 = Session("rs").Fields("HOMOLOGADO").Value.ToString()
            Valor4 = Session("rs").Fields("RFCANTERIOR").Value.ToString()

        End If
        Session("Con").Close()

        EnviaCorreo(txt_correo.Text, Valor, Valor2, Valor3, Valor4)

    End Sub
End Class