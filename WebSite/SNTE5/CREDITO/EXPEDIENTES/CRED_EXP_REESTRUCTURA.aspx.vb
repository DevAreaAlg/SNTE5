Public Class CRED_EXP_REESTRUCTURA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RESTRUCTURA_DETALLE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_folio_origenB.Text = Session("rs").Fields("FOLIOORIG").Value.ToString
            lbl_tiporesB.Text = Session("rs").Fields("TIPORES").Value.ToString
            lbl_emprobB.Text = Session("rs").Fields("RAZON").Value

        End If
        Session("Con").Close()
    End Sub

End Class