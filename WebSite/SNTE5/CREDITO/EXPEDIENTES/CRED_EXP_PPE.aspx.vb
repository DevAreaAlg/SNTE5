Public Class CRED_EXP_PPE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lbl_Folio.Text = "Persona Políticamente Expuesta"
        lbl_Cliente1.Text = Session("PROSPECTO") + " (" + CStr(Session("PERSONAID")) + ")"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPPE As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_HISTORIAL_PPE_X_PERSONA"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtPPE, Session("rs"))

        If dtPPE.Rows.Count > 0 Then
            dag_PPE.Visible = True
            dag_PPE.DataSource = dtPPE
            dag_PPE.DataBind()
        Else
            dag_PPE.Visible = True
        End If

        Session("Con").Close()

    End Sub

    Protected Sub dag_PPE_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_PPE.ItemCommand

        If (e.CommandName = "VER") Then

            If e.Item.Cells(0).Text = "1" Then
                Session("DOCUMENTO_DIGITALIZADO") = e.Item.Cells(0).Text
                Session("FOLIO_AUX") = e.Item.Cells(5).Text
                Response.Redirect("~/DIGITALIZADOR/DIGI_MOSTRAR.aspx")
            Else
                Session("DOCUMENTO_DIGITALIZADO") = Nothing
                Session("FOLIO_AUX") = Nothing
                lbl_Status.Text = "No existe documento digitalizado de este reporte"
            End If

        End If

    End Sub


End Class