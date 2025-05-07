Public Class PEN_CNF_INSTITUCIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            llena_Oficinas()
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Instituciones", "Instituciones")
    End Sub

    Protected Sub btn_Agregar_Institucion_Click(sender As Object, e As EventArgs) Handles btn_Agregar_Institucion.Click
        Session("IdInsti") = "0"
        Response.Redirect("PEN_CNF_INSTITUCIONES_CREACION.aspx")
    End Sub

    Private Sub llena_Oficinas()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_instituciones.Visible = True
            dag_instituciones.DataSource = dteq
            dag_instituciones.DataBind()
        Else
            dag_instituciones.Visible = False
        End If
    End Sub

    Protected Sub dag_instituciones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_instituciones.ItemCommand

        If (e.CommandName = "EDITAR") Then
            Session("IdInsti") = e.Item.Cells(0).Text
            Response.Redirect("PEN_CNF_INSTITUCIONES_CREACION.aspx")
        End If
    End Sub

    Protected Sub btn_busca_institucion_Click(sender As Object, e As EventArgs) Handles btn_busca_institucion.Click


        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 50, ddl_estatus.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FILTRO_INSTITUCION"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))

        If dteq.Rows.Count > 0 Then
            dag_instituciones.Visible = True
            dag_instituciones.DataSource = dteq
            dag_instituciones.DataBind()
        Else
            dag_instituciones.Visible = False
        End If
        Session("Con").Close()
    End Sub


End Class