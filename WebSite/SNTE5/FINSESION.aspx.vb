Public Class FINSESION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("MAC") Is Nothing And Session("USERID") Is Nothing And Session("Sesion") Is Nothing Then

            Else

                Dim mac As String = Session("MAC").ToString
                Dim user As String = Session("USERID").ToString
                Dim ses As String = Session("Sesion").ToString
                'SE INSERTA EN LA BD EL CORRESPONDIENTE LOG DE FIN DE SESION POR INACTIVIDAD
                FinSesion("FNSESINA", Session("USERID").ToString, Session("MAC").ToString, Session("Sesion").ToString)
            End If
        End If
    End Sub

    Protected Sub btn_iniciar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_iniciar.Click
        Response.Redirect("/LOGIN.aspx")
    End Sub

    Private Sub FinSesion(ByVal tipo_fin As String, ByVal userid As Integer, ByVal mac As String, ByVal sesion As String)

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO_FIN", Session("adVarChar"), Session("adParamInput"), 15, tipo_fin)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, userid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 50, mac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SESION", Session("adVarChar"), Session("adParamInput"), 15, sesion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_FIN_SESION"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        Session("LoggedIn") = False
        Session.Abandon()


    End Sub


End Class