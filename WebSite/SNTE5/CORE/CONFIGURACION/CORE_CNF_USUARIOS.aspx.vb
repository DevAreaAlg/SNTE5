Public Class CORE_CNF_USUARIOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            llena_sucursales("cmb_suc")
            llena_usuarios()
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Usuarios", "Usuarios")
    End Sub

    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click

        lbl_estatus.Text = ""
        txt_nombre.Text = ""
        txt_paterno.Text = ""
        txt_usuario.Text = ""

        llena_sucursales("cmb_suc")

        cmb_estatus.Items.Clear()
        Dim elijaEst As New ListItem("ELIJA", "-1")
        cmb_estatus.Items.Add(elijaEst)
        Dim elijaEst1 As New ListItem("INACTIVO", "0")
        cmb_estatus.Items.Add(elijaEst1)
        Dim elijaEst2 As New ListItem("ACTIVO", "1")
        cmb_estatus.Items.Add(elijaEst2)

        llena_usuarios()
    End Sub

    Private Sub llena_sucursales(ByVal i As String)
        cmb_sucursal.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_sucursal.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_sucursal.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llena_usuarios()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_usuario.Visible = True
            dag_usuario.DataSource = dteq
            dag_usuario.DataBind()
        Else
            dag_usuario.Visible = False
        End If
    End Sub

    Protected Sub btn_busca_usuario_Click(sender As Object, e As EventArgs) Handles btn_busca_usuario.Click
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        If txt_nombre.Text = "" And txt_paterno.Text = "" And txt_usuario.Text = "" And cmb_sucursal.SelectedValue = "-1" And cmb_estatus.SelectedValue = "-1" Then

            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"
        Else

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 50, txt_usuario.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 50, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 10, cmb_sucursal.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, cmb_estatus.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FILTRO_USUARIO"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dteq, Session("rs"))
            Session("Con").Close()
            If dteq.Rows.Count > 0 Then
                dag_usuario.Visible = True
                dag_usuario.DataSource = dteq
                dag_usuario.DataBind()
            Else
                dag_usuario.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If

        End If



    End Sub

    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        Session("ID") = "-1"
        Response.Redirect("CORE_CNF_USUARIOS_CREAR.aspx")
    End Sub

    Protected Sub dag_usuario_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_usuario.ItemCommand
        If (e.CommandName = "EDITAR") Then
            Session("ID") = e.Item.Cells(0).Text
            Response.Redirect("CORE_CNF_USUARIOS_CREAR.aspx")
        End If
    End Sub


End Class