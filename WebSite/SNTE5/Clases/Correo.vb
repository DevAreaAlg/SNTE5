Imports System.Net.Mail 'referencia
Imports Microsoft.VisualBasic
Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports System.Text
Imports System.Net
Public Class Correo
    Inherits System.Web.UI.Page
    ' variable para tomar el error del envio de correo
    Public _mailError As String = String.Empty
    'Clase de envio de correo obteniendo el error del envio atraves del mailerror para envio de msj si se llega a necesitar
    'la captura del error



    Public Function Envio_email(ByVal html As String, ByVal asunto As String, ByVal para As String, ByVal cc As String) As Boolean
        Try
            'se toman valores de session para envio de correo , puerto , ssl etc desde el web.config
            Dim dirsmtp As String = Session("MAIL_SERVER")
            Dim portsmtp As String = Session("MAIL_SERVER_PORT")
            Dim correoSSL As Boolean = Session("MAIL_SERVER_SSL")
            Dim correoUser As String = Session("MAIL_SERVER_USER")
            Dim correoPass As String = Session("MAIL_SERVER_PWD")
            Dim usuarioDom As String = AppSettings("correoUserDom")
            Dim objSMTP As New Net.Mail.SmtpClient(dirsmtp)
            Dim mailFrom As String = Session("MAIL_SERVER_FROM")
            Dim mail As New Net.Mail.MailMessage
            Dim envio As String = String.Empty
            envio = Session("MAIL_SERVER_ENVIO")
            _mailError = String.Empty
            If envio = 1 Then
                mail.Subject = asunto
                mail.IsBodyHtml = True
                mail.From = New System.Net.Mail.MailAddress(mailFrom)
                para = para.Replace(";", ",")
                mail.To.Add(para)
                objSMTP.Port = portsmtp
                'se agrega el correo de copia para el envio 
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
                mail.Body = html
                'credenciales de autentication               
                objSMTP.Credentials = New System.Net.NetworkCredential(correoUser, correoPass, usuarioDom)
                'ssl para envio de correo
                objSMTP.EnableSsl = correoSSL
                'envio de correo
                objSMTP.Send(mail)
            End If
            Return True
        Catch ex As Exception
            'error de correo en caso de que exista 
            _mailError = ex.Message
            Return False
        End Try
    End Function
End Class
