Public Class CORE_PER_PERSONAEXT_BUSQUEDA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            llena_sucursales("cmb_suc")
            llena_personas()
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Personas", "Personas Externas")
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

    Private Sub llena_personas()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtper As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERSONASEXT"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtper, Session("rs"))
        Session("Con").Close()
        If dtper.Rows.Count > 0 Then
            dag_persona.Visible = True
            dag_persona.DataSource = dtper
            dag_persona.DataBind()
        Else
            dag_persona.Visible = False
        End If
    End Sub

    Protected Sub btn_busca_persona_Click(sender As Object, e As EventArgs) Handles btn_busca_persona.Click
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtper As New Data.DataTable()
        If txt_id.Text = "" And txt_nombre.Text = "" And txt_paterno.Text = "" And cmb_sucursal.SelectedValue = "-1" Then
            dag_persona.Visible = False
            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"
        Else
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, txt_id.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 50, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 10, cmb_sucursal.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FILTRO_PERSONAEXT"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtper, Session("rs"))
            Session("Con").Close()
            If dtper.Rows.Count > 0 Then
                dag_persona.Visible = True
                dag_persona.DataSource = dtper
                dag_persona.DataBind()
                lbl_estatus.Text = ""
            Else
                dag_persona.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
        End If
    End Sub

    Protected Sub btn_nuevo_Click(sender As Object, e As EventArgs) Handles btn_nuevo.Click
        Session("PERSONAID") = "-1"
        Response.Redirect("CORE_PER_PERSONAEXT.aspx")
    End Sub

    Protected Sub dag_persona_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_persona.ItemCommand
        If (e.CommandName = "EDITAR") Then
            Session("PERSONAID") = e.Item.Cells(0).Text
            Response.Redirect("CORE_PER_PERSONAEXT.aspx")
        End If
    End Sub

    Protected Sub btn_elimina_filtro_Click(sender As Object, e As EventArgs) Handles btn_elimina_filtro.Click
        llena_personas()
        lbl_estatus.Text = ""
        txt_id.Text = ""
        txt_nombre.Text = ""
        txt_paterno.Text = ""
        cmb_sucursal.SelectedIndex = "0"
    End Sub

End Class