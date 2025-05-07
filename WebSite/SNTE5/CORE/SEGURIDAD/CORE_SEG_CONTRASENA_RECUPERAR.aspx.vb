Public Class CORE_SEG_CONTRASENA_RECUPERAR
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

    End Sub

    Protected Sub btn_Enviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Enviar.Click

        lbl_Results.Text = ""
        If txt_Mail.Text = "" Then
            lbl_Results.Text = "Error: Campo(s) Vacío(s)."
            Exit Sub
        End If

        'EJECUTO SP QUE OBTIENE LA PREGUNTA SECRETA DADO EL EMAIL Y QUE LA CUENTA ESTE HABILITADA
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 150, txt_Mail.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PREGUNTA_SECRETA"
        Session("rs") = Session("cmd").Execute()

        'MUESTRO PANEL CON PREGUNTA SI EL CORREO ESTA REGISTRADO Y LA CUENTA HABILITADA
        If Session("rs").Fields("ESTATUS").Value.ToString = "NOK" Then 'cuenta deshabilitada o correo no registrado
            lbl_Results.Text = "Error: Dirección de correo no registrada o cuenta deshabilitada"
        Else 'la cuenta si esta habilitada y el correo registrado

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("USUARIO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
                cmb_usuarios.Items.Add(item)
                Session("rs").movenext()
            Loop

            pnl_usr.Visible = True
            txt_Mail.Enabled = False
            btn_Enviar.Enabled = False

        End If

        Session("Con").Close()

    End Sub



    Protected Sub btn_EnviarR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_EnviarR.Click

        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        'VALIDO LA RESPUESTA A LA PREGUNTA SECRETA Y OBTENGO EL NUEVO PASSWORD EN CASO DE SER CORRECTA
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RESP", Session("adVarChar"), Session("adParamInput"), 50, txt_respuesta.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 17, Session("MAC_PSWD"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 17, cmb_usuarios.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONTRASEÑA"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("EMAIL").Value.ToString = "" Then 'RESPUESTA INCORRECTA
            lbl_Results.Text = "Error: La respuesta es incorrecta"
            'pnl_pregunta.Visible = False
            'pnl_usr.Visible = False
            'txt_Mail.Text = ""
            'cmb_usuarios.SelectedIndex = "0"
            'txt_respuesta.Text = ""
            'txt_Mail.Enabled = True
            'btn_Enviar.Enabled = True
        Else 'RESPUESTA CORRECTA
            lbl_Results.Text = "" 'IHG 2017-07-10
            'EnviaMail(Session("rs").Fields("USUARIO").Value.ToString, Session("rs").Fields("CONTRASEÑA").Value.ToString, Session("rs").Fields("EMAIL").Value.ToString)

            Dim LOGIN As String = Session("rs").Fields("USUARIO").Value.ToString
            Dim PWD As String = Session("rs").Fields("CONTRASEÑA").Value.ToString
            'VALIDO LA RESPUESTA A LA PREGUNTA SECRETA Y OBTENGO EL NUEVO PASSWORD EN CASO DE SER CORRECTA
            Dim subject As String = "Contraseña del Sistema"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) usuario:  " + LOGIN + "</td></tr>")
            sbhtml.Append("<tr><td>A continuación se le muestran sus datos de acceso al sistema: </td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td width='25%'>Información del usuario:</td></td></tr>")
            sbhtml.Append("<tr><td width='75%'>Usuario:</td><td>" + "<b>" + LOGIN + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Contraseña:</td>" + "<b>" + PWD + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='250'>Para cualquier duda o aclaración puede comunicarse a: Soporte Técnico del Sistema correspondiente.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            If Not (clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)) Then
                lbl_Results.Text = "Sus datos de acceso no han sido enviados a su correo electrónico"

            Else
                lbl_Results.Text = "Sus datos de acceso han sido enviados a su correo electrónico"

            End If
            pnl_pregunta.Visible = False
            pnl_usr.Visible = False
            txt_Mail.Text = ""
            cmb_usuarios.SelectedIndex = "0"
            txt_respuesta.Text = ""
            btn_Enviar.Enabled = True
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_usuario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_usuario.Click

        lbl_Results.Text = ""

        'EJECUTO SP PARA VALIDAR SI EL USUARIO TIENE BYPASS O LA MAC ESTA PERMITIDA
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("MACS", Session("adVarChar"), Session("adParamInput"), 500, hdn_mcs.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 150, cmb_usuarios.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_MAC"
        Session("rs") = Session("cmd").Execute()
        Session("MAC_PSWD") = Session("rs").Fields("MAC").Value

        If Session("MAC_PSWD") <> "0" Then

            'EJECUTO SP QUE OBTIENE LA PREGUNTA SECRETA DADO EL EMAIL Y QUE LA CUENTA ESTE HABILITADA
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 150, txt_Mail.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 150, cmb_usuarios.SelectedItem.Value.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_PREGUNTA_SECRETA_USR"
            Session("rs") = Session("cmd").Execute()

            'MUESTRO PANEL CON PREGUNTA SI EL CORREO ESTA REGISTRADO Y LA CUENTA HABILITADA
            If Session("rs").Fields("ESTATUS").Value.ToString = "NOK" Then 'cuenta deshabilitada o correo no registrado
                lbl_Results.Text = "Error: cuenta deshabilitada"
            Else 'la cuenta si esta habilitada y el correo registrado
                cmb_usuarios.Enabled = False
                btn_usuario.Enabled = False
                pnl_pregunta.Visible = True
                lbl_pregunta.Text = Session("rs").fields("PREGUNTA").value.ToString
            End If

        Else
            Session("MAC_PSWD") = Nothing
            lbl_Results.Text = "Error: Equipo no autorizado."
        End If

        Session("Con").Close()

    End Sub

    Protected Sub lnk_Regresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Regresar.Click
        Response.Redirect("~/LOGIN.aspx")
    End Sub


End Class