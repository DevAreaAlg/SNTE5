Public Class RespuestaContraseña
    Public Property responseCode As String
    Public Property responseMessage As String
    Public Property data As List(Of dataContraseña)
End Class

Public Class dataContraseña
    Public Property Result As String
    Public Property MailUsr As String
    Public Property CodeMail As String
    Public Property MessageMail As String
End Class
