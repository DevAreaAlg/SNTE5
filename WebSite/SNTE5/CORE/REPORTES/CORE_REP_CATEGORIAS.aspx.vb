Public Class CORE_REP_CATEGORIAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load() Handles Me.Init

        Dim arbol As New Categ_Ctrl()
        AddHandler arbol.SelectedCat, AddressOf catCick
        ayuda.Controls.Add(arbol)


    End Sub

    Private Sub catCick(ByVal sender As LinkButton, ByVal e As System.EventArgs)

    End Sub


End Class