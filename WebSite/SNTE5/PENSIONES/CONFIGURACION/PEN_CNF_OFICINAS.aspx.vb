Public Class PEN_CNF_OFICINAS
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
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Oficinas", "Oficinas Institucionales")
    End Sub

    Protected Sub btn_Agregar_Politica_Click(sender As Object, e As EventArgs) Handles btn_Agregar_Politica.Click
        Session("ID_SUCURSAL") = "0"
        Response.Redirect("PEN_CNF_OFICINAS_CREAR.aspx")
    End Sub

    Private Sub llena_Oficinas()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_OFICINAS_INST"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_oficinas.Visible = True
            dag_oficinas.DataSource = dteq
            dag_oficinas.DataBind()
        Else
            dag_oficinas.Visible = False
        End If
    End Sub

    Protected Sub dag_oficinas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_oficinas.ItemCommand

        If (e.CommandName = "EDITAR") Then
            Session("ID_SUCURSAL") = e.Item.Cells(0).Text
            Response.Redirect("PEN_CNF_OFICINAS_CREAR.aspx")
        End If
    End Sub

    Protected Sub btn_busca_oficina_Click(sender As Object, e As EventArgs) Handles btn_busca_oficina.Click


        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 50, ddl_estatus.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FILTRA_OFICINA_INST"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))

        If dteq.Rows.Count > 0 Then
            dag_oficinas.Visible = True
            dag_oficinas.DataSource = dteq
            dag_oficinas.DataBind()
        Else
            dag_oficinas.Visible = False
        End If
        Session("Con").Close()
    End Sub

End Class