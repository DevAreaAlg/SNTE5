Public Class CRED_EXP_APARTADO4
    Inherits System.Web.UI.Page

    '---EL SEMAFORO PRENDE CUANDO  EL TIPO DE PRODUCTO  DE CREDITO CUMPLE CON EL MINIMO DE AVALES,REFERENCIAS Y CODEUDORES
    '--Y EL TIPO DE PRODUCTO DE CAPTACION CUMPLE CON EL MINIMO DE REFERENCIA Y BENEFICIARIO CON SU 100%

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Requisitos Generales", "Requisitos Generales")

        If Not Me.IsPostBack Then

            Session("VENGODE") = "CRED_EXP_APARTADO4.aspx"

            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))

        End If

        MuestraCredito()

    End Sub

    'links dinámicos de tipo de producto: Préstamo
    Private Sub MuestraCredito()

        Dim lnk_aval As New LinkButton
        lnk_aval.Text = "Avales"
        lnk_aval.CssClass = "textogris"
        lnk_aval.PostBackUrl = "CRED_EXP_AVALES.ASPX"
        Dim ret1 = New Literal
        ret1.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_aval)
        pnl_links.Controls.Add(ret1)


        ' Dim lnk_cod As New LinkButton
        'lnk_cod.Text = "Codeudores"
        'lnk_cod.CssClass = "textogris"
        'lnk_cod.PostBackUrl = "CRED_EXP_CODEUDORES.ASPX"
        'Dim ret3 = New Literal
        'ret3.Text = "<br /><br />"
        'pnl_links.Controls.Add(lnk_cod)
        'pnl_links.Controls.Add(ret3)


        Dim lnk_ref As New LinkButton
        lnk_ref.Text = "Referencias"
        lnk_ref.CssClass = "textogris"
        Session("IDENTIF") = "CRED"
        lnk_ref.PostBackUrl = "/UNIVERSAL/UNI_REFERENCIAS.ASPX"
        Dim ret4 = New Literal
        ret4.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ref)
        pnl_links.Controls.Add(ret4)


        'Dim lnk_prr As New LinkButton
        'lnk_prr.Text = "Propietario Real de los Recursos(PRR)"
        'lnk_prr.CssClass = "textogris"
        'Session("IDENTIF") = "CRED"
        'lnk_prr.PostBackUrl = "CRED_EXP_PROPIETARIO.ASPX"
        'Dim ret5 = New Literal
        'ret5.Text = "<br /><br />"
        'pnl_links.Controls.Add(lnk_prr)
        'pnl_links.Controls.Add(ret5)


        'Dim lnk_prec As New LinkButton
        'lnk_prec.Text = "Proveedor de los Recursos"
        'lnk_prec.CssClass = "textogris"
        'Session("IDENTIF") = "CRED"
        'lnk_prec.PostBackUrl = "CRED_EXP_PROV_REC.ASPX"
        'Dim ret6 = New Literal
        'ret6.Text = "<br /><br />"
        'pnl_links.Controls.Add(lnk_prec)
        'pnl_links.Controls.Add(ret6)


        'Dim lnk_refcom As New LinkButton
        'lnk_refcom.Text = "Referencias Comerciales"
        'lnk_refcom.CssClass = "textogris"
        'Session("IDENTIF") = "CRED"
        'lnk_refcom.PostBackUrl = "CRED_EXP_REF_COM.ASPX"
        'Dim ret7 = New Literal
        'ret7.Text = "<br /><br />"
        'pnl_links.Controls.Add(lnk_refcom)
        'pnl_links.Controls.Add(ret7)

    End Sub

    'links dinámicos de tipo de producto: Captación(VISTA)
    Private Sub MuestraCaptacion()


        Dim lnk_ben As New LinkButton
        lnk_ben.Text = "Beneficiarios"
        lnk_ben.CssClass = "textogris"
        lnk_ben.PostBackUrl = "CRED_EXP_BENEFICIARIOS.ASPX"
        Dim ret3 = New Literal
        ret3.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ben)
        pnl_links.Controls.Add(ret3)

        Dim lnk_ref As New LinkButton
        lnk_ref.Text = "Referencias"
        lnk_ref.CssClass = "textogris"
        Session("IDENTIF") = "CAP"
        lnk_ref.PostBackUrl = "CRED_EXP_REFERENCIAS.ASPX"
        Dim ret4 = New Literal
        ret4.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ref)
        pnl_links.Controls.Add(ret4)


    End Sub

    'links dinámicos de tipo de producto: Captación(PLAZO FIJO)
    Private Sub MuestraPlazoFijo()

        Dim lnk_ben As New LinkButton
        lnk_ben.Text = "Beneficiarios"
        lnk_ben.CssClass = "textogris"
        lnk_ben.PostBackUrl = "CRED_EXP_BENEFICIARIOS.ASPX"
        Dim ret3 = New Literal
        ret3.Text = "<br /><br />"
        pnl_links.Controls.Add(lnk_ben)
        pnl_links.Controls.Add(ret3)

    End Sub


End Class