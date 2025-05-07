Public Class CORE_CNF_POLITICAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Políticas", "Políticas")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            trae_politicas()
        End If
    End Sub


    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click


        txt_nombre.Text = ""

        ddl_estatus.Items.Clear()
        Dim elijaEst As New ListItem("ELIJA", "-1")
        ddl_estatus.Items.Add(elijaEst)
        Dim elijaEst1 As New ListItem("INACTIVO", "0")
        ddl_estatus.Items.Add(elijaEst1)
        Dim elijaEst2 As New ListItem("ACTIVO", "1")
        ddl_estatus.Items.Add(elijaEst2)
        lbl_estatus.Text = ""
        trae_politicas()


    End Sub


    Protected Sub btn_busca_politica_Click(sender As Object, e As EventArgs) Handles btn_busca_politica.Click

        If txt_nombre.Text = "" And ddl_estatus.SelectedValue = "-1" Then
            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"

        Else
            lbl_estatus.Text = ""

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
            Session("cmd").CommandText = "SEL_FILTRO_POLITICA"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dteq, Session("rs"))

            If dteq.Rows.Count > 0 Then
                dag_Politicas.Visible = True
                dag_Politicas.DataSource = dteq
                dag_Politicas.DataBind()
            Else
                dag_Politicas.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
            Session("Con").Close()
        End If



    End Sub

    Private Sub trae_politicas()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_POLITICAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then

            dag_Politicas.Visible = True
            dag_Politicas.DataSource = dteq
            dag_Politicas.DataBind()
        Else
            dag_Politicas.Visible = False
        End If

    End Sub

    Protected Sub dag_Politicas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Politicas.ItemCommand

        If (e.CommandName = "EDITAR") Then
            Session("ID_POLITICA") = e.Item.Cells(0).Text
            Response.Redirect("CORE_CNF_POLITICAS_CREAR.aspx")
        End If
    End Sub


    Protected Sub btn_Agregar_Politica_Click(sender As Object, e As EventArgs) Handles btn_Agregar_Politica.Click
        Session("ID_POLITICA") = "0"

        Response.Redirect("CORE_CNF_POLITICAS_CREAR.aspx")
    End Sub

End Class