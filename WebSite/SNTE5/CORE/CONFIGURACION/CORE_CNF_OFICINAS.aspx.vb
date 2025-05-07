Public Class CORE_CNF_OFICINAS
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
            llena_Estados()

        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Oficinas", "Oficinas")
    End Sub

    Protected Sub btn_Agregar_Politica_Click(sender As Object, e As EventArgs) Handles btn_Agregar_Politica.Click
        Session("ID_SUCURSAL") = "0"
        Response.Redirect("CORE_CNF_OFICINAS_CREAR.aspx")
    End Sub

    Private Sub llena_Estados()
        ddl_estado.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_estado.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ESTADO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ESTADO").Value, Session("rs").Fields("IDESTADO").Value.ToString)
            ddl_estado.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llena_Oficinas()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_OFICINAS"
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
            Response.Redirect("CORE_CNF_OFICINAS_CREAR.aspx")
        End If
    End Sub

    Protected Sub btn_busca_oficina_Click(sender As Object, e As EventArgs) Handles btn_busca_oficina.Click


        If txt_nombre.Text = "" And ddl_estado.SelectedValue = "-1" And cmb_estatus.SelectedValue = "-1" Then
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
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 50, cmb_estatus.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FILTRA_SUCURSAL"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dteq, Session("rs"))

            If dteq.Rows.Count > 0 Then
                dag_oficinas.Visible = True
                dag_oficinas.DataSource = dteq
                dag_oficinas.DataBind()
            Else
                dag_oficinas.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
            Session("Con").Close()
        End If


    End Sub


    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click
        txt_nombre.Text = ""

        llena_Estados()

        cmb_estatus.Items.Clear()
        Dim elijaEst As New ListItem("ELIJA", "-1")
        cmb_estatus.Items.Add(elijaEst)
        Dim elijaEst1 As New ListItem("INACTIVO", "0")
        cmb_estatus.Items.Add(elijaEst1)
        Dim elijaEst2 As New ListItem("ACTIVO", "1")
        cmb_estatus.Items.Add(elijaEst2)
        lbl_estatus.Text = ""
        llena_Oficinas()


    End Sub

End Class