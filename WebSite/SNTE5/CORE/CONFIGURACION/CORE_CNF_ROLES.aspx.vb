Public Class CORE_CNF_ROLES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            llena_roles()
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Roles", "Roles")
    End Sub

    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        Session("IDROL") = "-1"
        Response.Redirect("CORE_CNF_ROLES_CREAR.aspx")
    End Sub


    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click


        txt_nombre.Text = ""

        cmb_status_eq_filter.Items.Clear()
        Dim elijaEst As New ListItem("ELIJA", "-1")
        cmb_status_eq_filter.Items.Add(elijaEst)
        Dim elijaEst1 As New ListItem("INACTIVO", "0")
        cmb_status_eq_filter.Items.Add(elijaEst1)
        Dim elijaEst2 As New ListItem("ACTIVO", "1")
        cmb_status_eq_filter.Items.Add(elijaEst2)
        lbl_estatus.Text = ""
        llena_roles()


    End Sub

    Private Sub llena_roles()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ROLES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_Roles.Visible = True
            dag_Roles.DataSource = dteq
            dag_Roles.DataBind()
        Else
            dag_Roles.Visible = False
        End If
    End Sub

    Protected Sub dag_Roles_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Roles.ItemCommand
        If (e.CommandName = "EDITAR") Then
            Session("IDROL") = e.Item.Cells(0).Text
            Response.Redirect("CORE_CNF_ROLES_CREAR.aspx")
        End If
    End Sub

    Protected Sub btn_busca_rol_Click()
        If txt_nombre.Text = "" And cmb_status_eq_filter.SelectedValue = "-1" Then
            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"

        Else
            lbl_estatus.Text = ""
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim drol As New Data.DataTable()
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, cmb_status_eq_filter.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FILTRO_ROLES"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(drol, Session("rs"))
            Session("Con").Close()
            If drol.Rows.Count > 0 Then
                dag_Roles.Visible = True
                dag_Roles.DataSource = drol
                dag_Roles.DataBind()
            Else
                dag_Roles.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
        End If



    End Sub


End Class