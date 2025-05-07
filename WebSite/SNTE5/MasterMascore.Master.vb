Public Class MasterMascore
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Not Session("LoggedIn") Then
            Response.Redirect("~/LOGIN.aspx")
        End If

        Try
            String.IsNullOrEmpty(Session("BREADCRUMB"))
            Session("BREADCRUMB") = New List(Of LinkButton)
        Catch ex As Exception

        End Try

        'busca el número de notificaciones activas

        ModulosPermitidos()
        notifRead()

    End Sub

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.MaintainScrollPositionOnPostBack = True
        If Not Me.IsPostBack Then

            Dim resp As Date = (Session("FechaSis"))
            username.InnerHtml = Session("USERNOM")
            lbl_fecha.Text = "Fecha de Sistema: " + (Session("FechaSis"))

        End If

    End Sub

    Protected Sub lnk_salir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_salir.Click
        'Session("MascoreG").Salir_sistema(Session("USERID").ToString, Session("MAC").ToString, Session("Sesion").ToString)
        Session("LoggedIn") = False
        Session.Abandon()
        Response.Redirect("~/LOGIN.aspx")
    End Sub

    Private Sub ModulosPermitidos()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtModulos As New Data.DataTable()
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_MODULOS_PERMITIDOS"
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtModulos, Session("rs"))
        Session("Con").Close()
        PopulateMenu(dtModulos, 0, Nothing)

    End Sub

    Private Sub PopulateMenu(dt As DataTable, parentMenuId As Integer, parentMenuItem As MenuItem)

        'Dim currentPage As String = Path.GetFileName(Request.Url.AbsolutePath)
        Dim sub_modulo As LinkButton
        Dim modulo As HtmlGenericControl
        Dim modulo_cont As HtmlGenericControl
        Dim nombre_mod As String = ""
        Dim contador As Integer = 1

        For Each row As DataRow In dt.Rows

            If Not row.Field(Of String)("CATNOMBRE").Equals(nombre_mod) Then

                If nombre_mod IsNot "" Then
                    modulo.Controls.Add(modulo_cont)
                    modulos_menu.Controls.Add(modulo)
                End If

                nombre_mod = row.Field(Of String)("CATNOMBRE")

                modulo = New HtmlGenericControl
                modulo.TagName = "li"
                modulo.Attributes("class") = "sub-menu"

                Dim titulo_modulo As New HtmlGenericControl
                titulo_modulo.TagName = "a"
                titulo_modulo.Attributes("href") = "javascript:;"
                titulo_modulo.Attributes("title") = row.Field(Of String)("CATDESCRIPCION")


                Dim icono_titulo_modulo As New HtmlGenericControl
                icono_titulo_modulo.TagName = "i"
                icono_titulo_modulo.Attributes("class") = row.Field(Of String)("CATICON")

                Dim span_titulo_modulo As New HtmlGenericControl
                span_titulo_modulo.TagName = "span"
                span_titulo_modulo.InnerText = nombre_mod


                Dim span_caret_modulo As New HtmlGenericControl
                span_caret_modulo.TagName = "span"
                span_caret_modulo.Attributes("class") = "menu-arrow arrow_carrot-right"


                titulo_modulo.Controls.Add(icono_titulo_modulo)
                titulo_modulo.Controls.Add(span_titulo_modulo)
                titulo_modulo.Controls.Add(span_caret_modulo)


                modulo.Controls.Add(titulo_modulo)

                modulo_cont = New HtmlGenericControl
                modulo_cont.TagName = "ul"
                modulo_cont.Attributes("class") = "sub"

            End If

            Dim lisobrante As New HtmlGenericControl
            lisobrante.TagName = "li"

            sub_modulo = New LinkButton
            AddHandler sub_modulo.Click, AddressOf limpiarVariblesS
            'If row.Field(Of String)("MODURL") = "~/INVERSIONES/EXPEDIENTES/INV_EXP_CALCULADORA.ASPX" Then
            '    sub_modulo.Attributes.Add("OnClick", "autorizacion()")
            '    sub_modulo.Attributes("direc") = row.Field(Of String)("MODURL")
            'ClientScript.RegisterStartupScript(GetType(String), "INVERSION", "window.open(""/INVERSIONES/EXPEDIENTES/INV_EXP_CALCULADORA_INV.ASPX"", ""INV"", ""width=500,height=550,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)

            sub_modulo.Attributes("direc") = row.Field(Of String)("MODURL")


            sub_modulo.Attributes("title") = row.Field(Of String)("MODDESCRIPCION")
            sub_modulo.Text = row.Field(Of String)("MODNOMBRE")

            lisobrante.Controls.Add(sub_modulo)
            modulo_cont.Controls.Add(lisobrante)

            contador += 1

        Next
        If contador > 1 Then
            modulo.Controls.Add(modulo_cont)
            modulos_menu.Controls.Add(modulo)
        End If


    End Sub

#Region "notificaciones"

    Sub notifRead()


        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtnotif As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_NOTIFICACIONES"
        Session("rs") = Session("cmd").Execute()



        custDA.Fill(dtnotif, Session("rs"))
        lbl_notif.Text = dtnotif.Rows.Count


        'Dim sw As New System.IO.StringWriter
        'Dim h As New HtmlTextWriter(sw)
        'notif_list.RenderControl(h)

        Dim cont3 As Integer = 0




        Dim flechaArriba As New HtmlGenericControl
        flechaArriba.TagName = "div"
        flechaArriba.Attributes("class") = "notify-arrow notify-arrow-blue"


        notif_list.Controls.Add(flechaArriba)



        For Each row As Data.DataRow In dtnotif.Rows
            cont3 = cont3 + 1
            Dim not_txt As String
            If row.Item("TEXTO").ToString().Length > 30 Then
                not_txt = row.Item("TEXTO").ToString().Remove(30)
                not_txt = not_txt & "..."
            Else
                not_txt = row.Item("TEXTO").ToString()
            End If
            Dim colorEx As String = ""
            Select Case row.Item("EXP_STATUS")
                Case 2
                    colorEx = "#fed189"
                Case 1
                    colorEx = "#113964"
                Case 0
                    colorEx = "#9e9e9e"
            End Select



            Dim notifN As New HtmlGenericControl
            notifN.TagName = "li"

            Dim linkNNotif As New LinkButton
            linkNNotif.Attributes("direc") = "~/NOTIFICACIONES.aspx"
            linkNNotif.Attributes.CssStyle.Add("display", "flex")
            linkNNotif.Attributes.CssStyle.Add("align-items", "center")
            AddHandler linkNNotif.Click, AddressOf limpiarVariblesS
            linkNNotif.Text = "<div style="" border-radius:4px; width: 16px; height: 14px; opacity: 0.7; background-color:" & colorEx & ";display:inline-block; margin-right:17px;""></div><span> " & not_txt & " </span>"

            notifN.Controls.Add(linkNNotif)
            notif_list.Controls.Add(notifN)

            If cont3 = 3 Then
                Exit For
            End If
        Next

        Dim verTodo As New HtmlGenericControl
        verTodo.TagName = "li"
        verTodo.Attributes.CssStyle.Add("padding", "0")

        verTodo.Attributes.CssStyle.Add("padding-right", "10.5px")

        Dim verTodoLink As New LinkButton
        verTodoLink.Attributes.CssStyle.Add("display", "flex")
        verTodoLink.Attributes.CssStyle.Add("justify-content", "flex-end")
        verTodoLink.Attributes.CssStyle.Add("padding", "0")
        AddHandler verTodoLink.Click, AddressOf limpiarVariblesS
        verTodoLink.Attributes("direc") = "~/NOTIFICACIONES.aspx"

        Dim flecha_vertodo As New HtmlGenericControl
        flecha_vertodo.TagName = "i"
        flecha_vertodo.Attributes.CssStyle.Add("font-size", "23px")
        flecha_vertodo.Attributes.CssStyle.Add("padding-top", "0px")
        flecha_vertodo.Attributes("class") = "arrow_carrot-right"


        Dim texto_vertodo As New HtmlGenericControl
        texto_vertodo.TagName = "span"
        texto_vertodo.InnerText = "Ver Todo"
        texto_vertodo.Attributes.CssStyle.Add("padding", "1.5px 0")

        verTodoLink.Controls.Add(texto_vertodo)
        verTodoLink.Controls.Add(flecha_vertodo)

        verTodo.Controls.Add(verTodoLink)

        notif_list.Controls.Add(verTodo)

        Session("Con").Close()




    End Sub


#End Region

#Region "Limpieza de variables de sesión."

    Protected Sub limpiarVariblesS(ByVal sender As LinkButton, ByVal eargs As EventArgs)
        TryCast(Session("BREADCRUMB"), List(Of LinkButton)).Clear()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtVAR As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_APPVARSESION_ELIMINAR"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtVAR, Session("rs"))
        Session("Con").Close()
        For Each row As Data.DataRow In dtVAR.Rows
            Session.Remove(row("VARIABLE"))
        Next
        Response.Redirect(sender.Attributes("direc"))
    End Sub

#End Region

#Region "craga breadcrumb"

    Public Sub CargaASPX(ByVal titulo_link As String, ByVal titulo_apartado As String)

        Dim lbl_slash As New Label
        lbl_slash.Text = "/"
        lbl_slash.Attributes.CssStyle.Add("margin", "0 10px")

        lbl_tituloASPX.Text = titulo_apartado
        Dim url_link As String
        Dim num_segments As Integer = CInt(Request.Url.Segments.Count)
        If num_segments = 3 Then
            url_link = Request.Url.Segments(1).ToString + Request.Url.Segments(2).ToString
        ElseIf num_segments = 4 Then
            url_link = Request.Url.Segments(1).ToString + Request.Url.Segments(2).ToString + Request.Url.Segments(3).ToString
        ElseIf num_segments = 2 Then
            url_link = Request.Url.Segments(1).ToString
        End If
        breadcrumb.Controls.Clear()

        Dim count As Integer = 0
        While count < TryCast(Session("BREADCRUMB"), List(Of LinkButton)).Count

            Dim lbl_slash2 As New Label
            lbl_slash2.Text = "/"
            lbl_slash2.Attributes.CssStyle.Add("margin", "0 10px")
            breadcrumb.Controls.Add(lbl_slash2)

            breadcrumb.Controls.Add(TryCast(Session("BREADCRUMB"), List(Of LinkButton))(count))

            If TryCast(Session("BREADCRUMB"), List(Of LinkButton))(count).PostBackUrl.Equals(url_link, StringComparison.CurrentCultureIgnoreCase) Then
                Dim count_links As Integer = count
                count = TryCast(Session("BREADCRUMB"), List(Of LinkButton)).Count - 1
                While count_links < count

                    TryCast(Session("BREADCRUMB"), List(Of LinkButton)).RemoveAt(count)
                    count = count - 1
                End While

                Exit Sub
            End If


            count = count + 1
        End While



        Dim link As New LinkButton
        link.PostBackUrl = url_link

        link.Text = titulo_link
        link.CssClass = "module_subsec_elements module_subsec_small-elements"

        breadcrumb.Controls.Add(lbl_slash)
        breadcrumb.Controls.Add(link)


        TryCast(Session("BREADCRUMB"), List(Of LinkButton)).Add(link)
    End Sub
#End Region

End Class