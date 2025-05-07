Public Class CRED_CNF_PAGARES_RECEPCION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            TryCast(Me.Master, MasterMascore).CargaASPX("Recepción de Pagarés", "Recepción de Pagarés")
            LlenaLotesEnviados()

        End If
    End Sub

    'Llena los lotes de pagare que aun no han sido confirmados a entrega por parte d ela sucursal
    Protected Sub LlenaLotesEnviados()

        'Se llena una tabla con los lotes pagares pendientes por ser recibidos

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtLotesPagare As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_LOTES_PAGARE_PEND_SUC"

        Session("rs") = Session("cmd").Execute()

        'se agregan los lotes a una tabla en memoria
        custDA.Fill(dtLotesPagare, Session("rs"))
        'se vacian los lotes al formulario
        dag_LotesPend.DataSource = dtLotesPagare
        dag_LotesPend.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub dag_LotesPend_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_LotesPend.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "RECIBE") Then

            Dim FOLIOINI As String
            Dim FOLIOFIN As String
            Dim SUCDEST As String
            Dim USERDEST As String

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDLOTE", Session("adVarChar"), Session("adParamInput"), 10, e.Item.Cells(0).Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_RECIBE_LOTES"
            Session("rs") = Session("cmd").Execute()

            FOLIOINI = Session("rs").Fields("FOLIOINI").Value.ToString
            FOLIOFIN = Session("rs").Fields("FOLIOFIN").Value.ToString
            SUCDEST = Session("rs").Fields("SUCDEST").Value.ToString
            USERDEST = Session("rs").Fields("USERDEST").Value.ToString

            Session("Con").Close()

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "LOTESPAG")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF

                EnviaMail(Session("rs").Fields("EMAIL").Value.ToString, Session("rs").Fields("NOMBRE").Value.ToString, FOLIOINI, FOLIOFIN, SUCDEST, USERDEST)

                Session("rs").movenext()
            Loop

            Session("Con").Close()

            LlenaLotesEnviados()
            lbl_Alerta.Text = "Se ha enviado un email de confirmación a corporativo"

        End If

    End Sub

    Private Sub EnviaMail(ByVal destinatario As String, ByVal nombre As String, ByVal folioini As String, ByVal foliofin As String, ByVal Sucursal As String, ByVal UserDest As String)
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder

        subject = "Confirma de Recibido Lote de Pagarés"
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a) : " + nombre + "</td></tr>")
        sbhtml.Append("<tr><td>Se confirma de recibido por el usuario: " + "<b>" + UserDest + "</b>" + " el lote de pagarés enviado a la sucursal  " + "<b>" + Sucursal + "</b>" + " con folio inicial: " + "<b>" + folioini + "</b>" + " y folio final " + "<b>" + foliofin + "</b>" + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br />")
        sbhtml.Append("<br></br>")
        sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br></br>")
        clase_Correo.Envio_email(sbhtml.ToString, subject, destinatario, cc)

    End Sub

End Class