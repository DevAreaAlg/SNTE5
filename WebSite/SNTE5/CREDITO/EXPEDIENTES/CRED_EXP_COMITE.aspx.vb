Public Class CRED_EXP_COMITE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Comité de Préstamo", "Comité de Préstamo")
        If Not Me.IsPostBack Then

            Try
                'Metodo que llena el droplist con los expedientes
                LlenarExpComite()
                Session("VENGODEDIGGLOBAL") = Nothing
            Catch
                lbl_AlertaNoAcceso.Text = "Uno de los expedientes en que emitió voto falta por finalizar, póngase en contacto con el comité"
            End Try
        End If

    End Sub

    Private Sub LlenarExpComite()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_EXPEDIENTES_COMITE"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub btn_actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_actualizar.Click
        Response.Redirect("CRED_EXP_COMITE.aspx")
    End Sub

    Private Sub DAG_Analisis_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_Analisis.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        Session("FOLIO") = e.Item.Cells(0).Text
        Session("PROSPECTO") = e.Item.Cells(1).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PRODUCTO") = e.Item.Cells(4).Text

        If (e.CommandName = "Detalle") Then
            ObtieneIDsesion()
            Response.Redirect("CRED_EXP_EXPEDIENTE.ASPX")
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
        Session("parm") = Session("cmd").CreateParameter("IDCOMITE", Session("adVarChar"), Session("adParamInput"), 10, 1)
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