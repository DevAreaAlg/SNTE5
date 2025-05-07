Public Class UNI_BUSQUEDA_CP
    Inherits System.Web.UI.Page

    Protected Sub btn_cancelarbusqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelarbusqueda.Click
        Response.Write("<script language='javascript'> { window.returnValue=""""; window.close();}</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

        End If

        lbl_status.Text = ""

    End Sub


    Protected Sub btn_buscarCP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscarCP.Click

        lst_encontrados.Items.Clear()

        If txt_cp.Text <> "" Or txt_estado.Text <> "" Or txt_municipio.Text <> "" Or txt_asentamiento.Text <> "" Then

            Dim resultado As String

            'Datos laborales
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EDO", Session("adVarChar"), Session("adParamInput"), 40, txt_estado.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MUN", Session("adVarChar"), Session("adParamInput"), 100, txt_municipio.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ASEN", Session("adVarChar"), Session("adParamInput"), 100, txt_asentamiento.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_BUSQUEDA_CATCP"
            Session("rs") = Session("cmd").Execute()
            'Se cargan los datos que arroja la consulta
            lst_encontrados.Items.Clear()
            Do While Not Session("rs").EOF
                resultado = Session("rs").Fields("CP").Value.ToString + " / " + Session("rs").Fields("ESTADO").Value.ToString +
                        " / " + Session("rs").Fields("MUNICIPIO").Value.ToString + " / " + Session("rs").Fields("ASENTAMIENTO").Value.ToString

                Dim item As New ListItem(resultado, Session("rs").Fields("CP").Value.ToString)
                lst_encontrados.Items.Add(item)
                Session("rs").movenext()
            Loop
            Session("Con").Close()

            If lst_encontrados.Items.Count = 0 Then
                lbl_status.Text = "No existen coincidencias"
            End If

        Else
            lbl_status.Text = "Ingrese por lo menos un parámetro de búsqueda."

        End If
    End Sub


End Class