Public Class PROHIBIDO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Protected Sub btn_Volver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Volver.Click
        Response.Redirect("~/LOGIN.aspx")
    End Sub

End Class