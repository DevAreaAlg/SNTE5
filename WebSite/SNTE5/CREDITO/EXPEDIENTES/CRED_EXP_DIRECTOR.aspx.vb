Public Class CRED_EXP_DIRECTOR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Dirección de Préstamo", "Dirección de Préstamo")
        If Not Me.IsPostBack Then

            'Metodo que llena el droplist con los expedientes
            LlenarExp()
            Session("VENGODEDIGGLOBAL") = Nothing
        End If

    End Sub

    Private Sub LlenarExp()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_EXPEDIENTES_DIRECTOR"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub btn_actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_actualizar.Click
        Response.Redirect("CRED_EXP_DIRECTOR.aspx")
    End Sub

    Private Sub DAG_Analisis_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_Analisis.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        Session("FOLIO") = e.Item.Cells(0).Text
        Session("CVEEXPE") = e.Item.Cells(1).Text
        Session("PROSPECTO") = e.Item.Cells(2).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PRODUCTO") = e.Item.Cells(4).Text

        If (e.CommandName = "Detalle") Then
            ObtieneIDsesion()
            Response.Redirect("CRED_EXP_ANA_DIRECTOR.ASPX")
        End If


    End Sub

    Private Sub ObtieneIDsesion()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCOMITE", Session("adVarChar"), Session("adParamInput"), 10, 2)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_ANAEXP_CREA_SESION"
        Session("rs") = Session("cmd").Execute()

        Session("IDSESCOMIT") = Session("rs").Fields("IDSESCOMIT").value.ToString()

        Session("Con").Close()
    End Sub

    Private Sub LimpiaVariables()
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("PRODUCTO") = Nothing

    End Sub

End Class