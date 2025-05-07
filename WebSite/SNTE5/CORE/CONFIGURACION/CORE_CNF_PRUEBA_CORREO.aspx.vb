Imports System.Configuration.ConfigurationManager
Public Class CORE_CNF_PRUEBA_CORREO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Prueba de correo", "Prueba de correo")

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btn_enviar_correo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_enviar_correo.Click
        EnviaCorreo()
    End Sub

    Protected Sub btn_enviar_correo_del_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_del.Click

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ComisionesGeneral As New Data.DataTable()

        Dim dtComisionesAsignadas As New Data.DataTable()
        dtComisionesAsignadas.Columns.Add("CORREO", GetType(Integer))
        dtComisionesAsignadas.Columns.Add("PASS", GetType(String))
        dtComisionesAsignadas.Columns.Add("USUARIO", GetType(Integer))

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CREDENCIALES_DELEGACIONARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ComisionesGeneral, Session("rs"))
        Session("Con").Close()

        For Each row As Data.DataRow In ComisionesGeneral.Rows()
            'dtComisionesAsignadas.ImportRow(row)
            EnviaCorreoDel(row(0).ToString, row(1).ToString, row(2).ToString)
        Next


    End Sub

    Private Sub EnviaCorreoDel(ByVal correopara As String, ByVal pass As String, ByVal usuario As String)


        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Dim subject As String = "Credenciales de acceso"

        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE Sección 5</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a) representante de delegación</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<tr><td>Usted podrá acceder al sistema de actualización de membresías con las siguientes credenciales:</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<tr><td width='250'><b>Usuario: " + usuario + "</td></tr>")
        sbhtml.Append("<tr><td width='250'><b>Contraseña: " + pass + "</td></tr>")
        sbhtml.Append("<tr><td width='250'><b>URL: https://snte5.algoritmos.com.mx/ </td></tr>")
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
                mail.To.Add(correopara)
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

            lbl_estatus_correo.Text = "Éxito: Se envió el correo."

        Catch ex As Exception
            lbl_estatus_correo.Text = ex.Message
        End Try
        sbhtml.Clear()

    End Sub

#Region "Evento Correo"

    Private Sub EnviaCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Dim subject As String = "Prueba de correo"

        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE Sección 5</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
        sbhtml.Append("<br/>")
        sbhtml.Append("<tr><td>Se realizo una prueba de envió de correo.</td></tr>")
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
                mail.To.Add("gcuatepotzo@algoritmos.com.mx")
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

            lbl_estatus_correo.Text = "Éxito: Se envió el correo."

        Catch ex As Exception
            lbl_estatus_correo.Text = ex.Message
        End Try
        sbhtml.Clear()

    End Sub

#End Region

End Class