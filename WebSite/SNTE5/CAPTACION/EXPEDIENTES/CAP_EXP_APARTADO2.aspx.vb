Public Class CAP_EXP_APARTADO2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Beneficiarios y Referencias", "BENEFICIARIOS Y REFERENCIAS")

        If Not Me.IsPostBack Then

            Session("VENGODE") = "CAP_EXP_APARTADO2.ASPX"
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("FOLIO"))

        End If
        MuestraCaptacion()
    End Sub

    'links dinámicos de tipo de producto: Préstamo
    Private Sub MuestraCredito()

        Dim lnk_aval As New LinkButton
        lnk_aval.Text = "Avales"
        lnk_aval.CssClass = "textogris"
        lnk_aval.PostBackUrl = "CNFEXP_AVALES.ASPX"
        Dim ret1 = New Literal
        ret1.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_aval)
        pnl_links.Controls.Add(ret1)



        Dim lnk_cod As New LinkButton
        lnk_cod.Text = "Codeudores"
        lnk_cod.CssClass = "textogris"
        lnk_cod.PostBackUrl = "CNFEXP_CODEUDORES.ASPX"
        Dim ret3 = New Literal
        ret3.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_cod)
        pnl_links.Controls.Add(ret3)

        Dim lnk_ref As New LinkButton
        lnk_ref.Text = "Referencias"
        lnk_ref.CssClass = "textogris"
        Session("IDENTIF") = "CRED"
        lnk_ref.PostBackUrl = "CNFEXP_REFERENCIAS.ASPX"
        Dim ret4 = New Literal
        ret4.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ref)
        pnl_links.Controls.Add(ret4)




    End Sub

    'links dinámicos de tipo de producto: Captación(VISTA)
    Private Sub MuestraCaptacion()


        Dim lnk_ben As New LinkButton
        lnk_ben.Text = "Beneficiarios"
        lnk_ben.CssClass = "textogris"
        lnk_ben.PostBackUrl = "CAP_EXP_BENEFICIARIOS.ASPX"
        Dim ret3 = New Literal
        ret3.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ben)
        pnl_links.Controls.Add(ret3)

        Dim lnk_ref As New LinkButton
        lnk_ref.Text = "Referencias"
        lnk_ref.CssClass = "textogris"
        Session("IDENTIF") = "CAP"
        lnk_ref.PostBackUrl = "/UNIVERSAL/UNI_REFERENCIAS.ASPX"
        Dim ret4 = New Literal
        ret4.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ref)
        pnl_links.Controls.Add(ret4)

        Dim lnk_prr As New LinkButton
        lnk_prr.Text = "Propietario real de los recursos (PRR)"
        lnk_prr.CssClass = "textogris"
        Session("IDENTIF") = "CAP"
        lnk_prr.PostBackUrl = "CAP_EXP_PROPIETARIORECURSOS.ASPX"
        Dim ret5 = New Literal
        ret5.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_prr)
        pnl_links.Controls.Add(ret5)

        Dim lnk_preq As New LinkButton
        lnk_preq.Text = "Proveedor de los recursos"
        lnk_preq.CssClass = "textogris"
        Session("IDENTIF") = "CAP"
        lnk_preq.PostBackUrl = "CAP_EXP_PROVEEDORREQ.ASPX"
        Dim ret6 = New Literal
        ret6.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_preq)
        pnl_links.Controls.Add(ret6)


    End Sub

    'links dinámicos de tipo de producto: Captación(PLAZO FIJO)
    Private Sub MuestraPlazoFijo()

        Dim lnk_ben As New LinkButton
        lnk_ben.Text = "Beneficiarios"
        lnk_ben.CssClass = "textogris"
        lnk_ben.PostBackUrl = "CNFEXP_BENEFICIARIOS.ASPX"
        Dim ret3 = New Literal
        ret3.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ben)
        pnl_links.Controls.Add(ret3)

    End Sub

End Class