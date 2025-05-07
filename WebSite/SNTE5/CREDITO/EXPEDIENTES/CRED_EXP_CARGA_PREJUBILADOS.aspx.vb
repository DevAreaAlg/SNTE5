Public Class CRED_EXP_CARGA_PREJUBILADOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Carga de Prejubilados", "Carga de Prejubilados")
        If Not Me.IsPostBack Then


        End If
    End Sub

End Class