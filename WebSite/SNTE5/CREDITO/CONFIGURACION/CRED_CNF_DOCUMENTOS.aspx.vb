Public Class CRED_CNF_DOCUMENTOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda Contratos y Pagarés", "Contratos y Pagarés")

        If Not Me.IsPostBack Then
            buscarDocs()
        End If
        Session.Remove("ID_DOC")

    End Sub
#Region "MÉTODO BUSQUEDA"
    Private Sub buscarDocs()

        Dim dt_res As New Data.DataTable
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO_ARCH", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipo_doc.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, cmb_estatus.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONTRATOS_PAGARES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_res, Session("rs"))
        Session("Con").Close()

        grdVw_busqueda.DataSource = dt_res
        grdVw_busqueda.DataBind()
        grdVw_busqueda.Visible = True
    End Sub

    Private Sub buscarDocs1()

        If cmb_tipo_doc.SelectedValue = "-1" And txt_nombre.Text = "" And cmb_estatus.SelectedValue = "-1" Then
            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"
        Else
            Dim dt_res As New Data.DataTable
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("TIPO_ARCH", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipo_doc.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, cmb_estatus.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CONTRATOS_PAGARES"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dt_res, Session("rs"))
            Session("Con").Close()


            If dt_res.Rows.Count > 0 Then
                grdVw_busqueda.Visible = True
                grdVw_busqueda.DataSource = dt_res
                grdVw_busqueda.DataBind()

            Else
                grdVw_busqueda.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If


        End If


    End Sub


    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click
        txt_nombre.Text = ""


        'cmb_tipo_doc.Items.Clear()
        'cmb_tipo_doc.Items.Clear()
        'Dim elijaDoc As New ListItem("ELIJA", "-1")
        'cmb_tipo_doc.Items.Add(elijaDoc)
        'Dim elijaDoc1 As New ListItem("PAGARÉ", "PAGARE")
        'cmb_tipo_doc.Items.Add(elijaDoc1)
        'Dim elijaDoc2 As New ListItem("CONTRATO", "CONTRATO")
        'cmb_tipo_doc.Items.Add(elijaDoc2)

        'cmb_estatus.Items.Clear()
        'Dim elijaEst As New ListItem("ELIJA", "-1")
        'cmb_estatus.Items.Add(elijaEst)
        'Dim elijaEst1 As New ListItem("INACTIVO", "0")
        'cmb_estatus.Items.Add(elijaEst1)
        'Dim elijaEst2 As New ListItem("ACTIVO", "1")
        'cmb_estatus.Items.Add(elijaEst2)
        'Dim elijaEst3 As New ListItem("EN EDICION", "2")
        'cmb_estatus.Items.Add(elijaEst3)


        lbl_estatus.Text = ""

        buscarDocs()


    End Sub


#End Region

#Region "EVENTOS"
    Protected Sub btn_crearDoc_Click(sender As Object, e As EventArgs)
        Session("ID_DOC") = New String() {"-1", "-1"}
        Response.Redirect("CRED_CNF_DOCUMENTOS_CREACION.aspx")
    End Sub
    Protected Sub btn_buscarDoc_Click(sender As Object, e As EventArgs)
        buscarDocs1()
    End Sub
    Protected Sub grdVw_busqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName.Equals("EDITAR") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdVw_busqueda.Rows(index)
            Session("ID_DOC") = New String() {row.Cells(0).Text, row.Cells(2).Text}
            Response.Redirect("CRED_CNF_DOCUMENTOS_CREACION.aspx")
        End If
    End Sub
#End Region

End Class