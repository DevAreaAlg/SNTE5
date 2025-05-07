Public Class COB_DESP_ALTA
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Alta de abogado/despacho", "CONFIGURACIÓN DE ABOGADO/DESPACHO")
        If Not Me.IsPostBack Then

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Login.aspx")
            End If
            folderA(div_selCliente, "down")
            LlenaReportes()
        End If

    End Sub
    Private Sub LlenaReportes()

        cmb_despacho.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_despacho.Items.Add(elija)

        Dim ITEM As New ListItem("ABOGADO", "ABO")
        cmb_despacho.Items.Add(ITEM)

        Dim ITEM2 As New ListItem("DESPACHO", "DES")
        cmb_despacho.Items.Add(ITEM2)


    End Sub

    Private Sub cmb_despacho_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_despacho.SelectedIndexChanged
        Select Case cmb_despacho.SelectedItem.Text

            Case "DESPACHO"
                Response.Redirect("COB_BUSQUEDA_DESP.aspx")
            Case "ABOGADO"

                Response.Redirect("COB_BUSQUEDA.aspx")

            Case Else


        End Select
    End Sub
    'folder close or open
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub

End Class