Public Class PLD_ALE_OPERACIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Alertas PLD", "Alertas de Operaciones PLD")
        If Not Me.IsPostBack Then

            lbl_IDPersona.Text = "Num. de Cliente: " + CStr(Session("PERSONAID"))
            lbl_Persona.Text = "Cliente: " + CStr(Session("PROSPECTO"))
            lbl_Folio.Text = "Folio: " + CStr(Session("FOLIO"))

            LlenaAlertas()
            AvisoPLDCorreo()

        End If

    End Sub

    Private Sub LlenaAlertas()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAlertasPLD As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_ALERTA_PLD"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAlertasPLD, Session("rs"))
        'se vacian los expedientes al formulario
        dag_Operacion.DataSource = dtAlertasPLD
        dag_Operacion.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub AvisoPLDCorreo()

        Dim periodicidad As String
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim operacion As String = String.Empty
        Dim monto As String = String.Empty
        Dim nota As String = String.Empty
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_ALERTA_PLD_EVENTO"
        Session("rs") = Session("cmd").Execute()

        periodicidad = Session("rs").Fields("PERIODICIDAD").Value.ToString

        If periodicidad = "EVENTO" Then

            Dim correo As String

            correo = "Estimado(a) Oficial" + vbCrLf + vbCrLf + "Se informa que las siguientes alertas de operaciones inusuales y/o relevantes se han agregado a la lista de espera para su revisión." + vbCrLf
            correo = correo + "CLIENTE: " + Session("rs").Fields("PERSONA").Value.ToString + vbCrLf + "EXPEDIENTE: " + Session("rs").Fields("FOLIO").Value.ToString + vbCrLf
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) Oficial : </td></tr>")
            sbhtml.Append("<tr><td>Se informa que las siguientes alertas de operaciones inusuales y/o relevantes se han agregado a la lista de espera para su revisión.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("</table>")
            Do While Not Session("rs").EOF

                operacion = Session("rs").Fields("OPERACION").Value.ToString
                monto = Session("rs").Fields("MONTO").Value.ToString
                nota = Session("rs").Fields("NOTA").Value.ToString

                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
                sbhtml.Append("<tr><td width='30%'>Operación: </td>" + "<b>" + operacion + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Monto: </td>" + "<b>" + monto + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Descripciòn: </td>" + "<b>" + nota + "</b>" + "</td></tr>")
                sbhtml.Append("<br></br>")
                Session("rs").movenext()
            Loop

            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")


            Session("Con").Close()

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "ALERTAPLD")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

                Session("rs").movenext()
            Loop

            Session("Con").Close()
        Else
            Session("Con").Close()
        End If



    End Sub

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click

        ClientScript.RegisterStartupScript(GetType(String), "Close", "window.close();", True)

    End Sub
End Class