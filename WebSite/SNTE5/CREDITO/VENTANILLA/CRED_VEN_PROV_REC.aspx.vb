Public Class CRED_VEN_PROV_REC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            LlenaProveedores()
            LlenaPropietarios()
        End If

    End Sub

    Private Sub LlenaProveedores()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtProveedores As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PROVEEDORREC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtProveedores, Session("rs"))

        dag_Proveedor.DataSource = dtProveedores
        dag_Proveedor.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub LlenaPropietarios()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPropietarios As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PROPIETARIOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPropietarios, Session("rs"))

        dag_Propietario.DataSource = dtPropietarios
        dag_Propietario.DataBind()

        Session("Con").Close()

    End Sub

End Class