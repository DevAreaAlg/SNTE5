Public Class CAP_VEN_PERSONAS_AUTORIZADAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Llenareferencias()
        End If
    End Sub

    Private Sub Llenareferencias()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtreferencias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FIRMAS_AUTORIZADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtreferencias, Session("rs"))
        DAG_PerAcep.DataSource = dtreferencias
        DAG_PerAcep.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub DAG_PerAcep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DAG_PerAcep.SelectedIndexChanged

    End Sub

End Class