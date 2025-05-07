Public Class PEN_PROC_SALDOS_TRABAJADOR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Consulta de Saldo", "Consulta de saldo de Trabajadores")
        If Not Me.IsPostBack Then

        End If

        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_Continuar.ClientID + "')")
        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            folderA(panel_saldos, "up")
        End If
    End Sub

    Private Sub BuscarIDCliente()

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_NombrePersonaBusqueda.Text = ""
            lbl_statusc.Text = "Error: No existe el número de cliente."
            txt_IdCliente.Text = ""
        Else
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
        End If
    End Sub

    Protected Sub btn_BusquedaPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BusquedaPersona.Click

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Busqueda", "busquedapersonafisica('" + btn_BusquedaPersona.ID + "');", True)
    End Sub

    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click

        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: Ingrese un número de cliente."

            Else
                'Metodo que llena el droplist con los tipos de productos
                Session("CLIENTE") = Session("PROSPECTO")
                Session("PROSPECTO") = Nothing
                lbl_NombrePersonaBusqueda.Text = "CLIENTE: " + Session("CLIENTE").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True

            End If
        Else
            folderA(panel_saldos, "down")
            lbl_statusc.Text = " "
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
            LlenaSaldos()
            LlenaCtaPrin()
        End If

    End Sub

    Private Sub LlenaSaldos()
        Dim custDS As New System.Data.OleDb.OleDbDataAdapter()
        Dim dsaldos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_CTAS_SALDOS"
        Session("rs") = Session("cmd").Execute()

        custDS.Fill(dsaldos, Session("rs"))
        If dsaldos.Rows.Count > 0 Then
            dag_Saldos.Visible = True
            dag_Saldos.DataSource = dsaldos
            dag_Saldos.DataBind()
        Else
            dag_Saldos.Visible = False
        End If
        Session("Con").Close()
    End Sub

    Private Sub LlenaCtaPrin()
        Dim custDC As New System.Data.OleDb.OleDbDataAdapter()
        Dim dcta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_CTA_PRINCIPAL"
        Session("rs") = Session("cmd").Execute()

        custDC.Fill(dcta, Session("rs"))
        If dcta.Rows.Count > 0 Then
            dag_Cuenta.Visible = True
            dag_Cuenta.DataSource = dcta
            dag_Cuenta.DataBind()
        Else
            dag_Cuenta.Visible = False
        End If
        Session("Con").Close()
    End Sub

    Protected Sub dag_Saldos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Saldos.ItemCommand
        Session("CLAVE") = e.Item.Cells(0).Text
        If (e.CommandName = "MOVIMIENTOS") Then
            folderA(panel_saldos, "up")
            folderA(panel_mov, "down")
            LlenaMovimientos()
        End If
    End Sub

    Protected Sub dag_Cuenta_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Cuenta.ItemCommand
        If (e.CommandName = "HISTORIAL") Then
            folderA(panel_saldos, "up")
            folderA(panel_historial, "down")
        End If
    End Sub

    Private Sub LlenaMovimientos()
        Dim custDM As New System.Data.OleDb.OleDbDataAdapter()
        Dim dmov As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, Session("CLAVE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_MOVIMIENTOS"
        Session("rs") = Session("cmd").Execute()

        custDM.Fill(dmov, Session("rs"))
        If dmov.Rows.Count > 0 Then
            dag_movimientos.Visible = True
            dag_movimientos.DataSource = dmov
            dag_movimientos.DataBind()
        Else
            dag_movimientos.Visible = False
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_EXCEL_Click() Handles btn_EXCEL.Click

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtDivisas As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, Session("CLAVE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_MOVIMIENTOS"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtDivisas, Session("rs"))

        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtDivisas.Columns.Count - 1
            context.Response.Write(dtDivisas.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtDivisas.Rows

            For i = 0 To dtDivisas.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Movimientos" + Session("CLAVE") + ".csv")
        context.Response.End()

    End Sub

    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)

        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "none")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #C0CDD5")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub


End Class