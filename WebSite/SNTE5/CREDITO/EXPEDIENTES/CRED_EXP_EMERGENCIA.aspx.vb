Imports System.Data.SqlClient
Imports System.Data.DataRow
Imports System.Data
Class CRED_EXP_EMERGENCIA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Autorización de Emergencia Médica", "Autorización de Emergencia Médica")
        If Not Me.IsPostBack Then

            LlenaInstituciones()

        End If

    End Sub

    Private Sub LlenaInstituciones()

        ddl_Instituciones.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        ddl_Instituciones.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ENTIDADES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Instituciones.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_dependencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Instituciones.SelectedIndexChanged
        'Metodo que llena el droplist con los expedientes
        If ddl_Instituciones.SelectedItem.Value.ToString <> -1 Then
            lbl_status.Text = ""
            AutorizarEmergencia()

        End If
    End Sub

    Public Sub AutorizarEmergencia()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXP_AUTO_EMERGENCIA_MED"

        Session("parm") = Session("cmd").CreateParameter("ID_INSTI", Session("adVarChar"), Session("adParamInput"), 10, ddl_Instituciones.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub btnEmergenciaMedica_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_emergencia.Click

        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            CambiaEmergenciaMedica()
        Else
            lbl_status.Text = "Error: No ha elegido préstamos para autorizar"
        End If

    End Sub

    Private Sub CambiaEmergenciaMedica()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()

        Dim result As String = "OK"
        Dim dtDescuentos As New Data.DataTable()

        dtDescuentos.Columns.Add("FOLIO", GetType(Integer))
        dtDescuentos.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtDescuentos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("UPD_EMERGENCIA_MEDICA", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("CONFIRMA", SqlDbType.Structured)
                Session("parm").Value = dtDescuentos
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lbl_status.Text = "Guardado correctamente"
            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            AutorizarEmergenciaMed()
            AutorizarEmergencia()


        End Try

    End Sub

    Private Sub AutorizarEmergenciaMed()

        Dim dtDescuentos As New Data.DataTable()
        dtDescuentos.Columns.Add("FOLIO", GetType(String))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                dtDescuentos.Rows.Add(DAG_Analisis.Rows(i).Cells(1).Text)
            End If
        Next


        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "REPCREDPAG")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Autoriza emergencias médicas"


        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<tr><td></td></tr>")
            sbhtml.Append("<tr><td>Se le informa que se ha realizado la autorización de emergencia médica. </td></tr>")
            sbhtml.Append("<tr><td></td></tr>")
            sbhtml.Append("</table>")

            'Table start.
            sbhtml.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>")
            'Adding HeaderRow.
            sbhtml.Append("<tr>")
            For Each column As DataColumn In dtDescuentos.Columns
                sbhtml.Append(("<th style='background-color: #113964;border: 1px solid #ccc'>" _
                            + (column.ColumnName + "</th>")))
            Next
            sbhtml.Append("</tr>")
            'Adding DataRow.
            For Each row As DataRow In dtDescuentos.Rows
                sbhtml.Append("<tr>")
                For Each column As DataColumn In dtDescuentos.Columns
                    sbhtml.Append(("<td style='width:200px;border: 1px solid #ccc'>" _
                                + (row(column.ColumnName).ToString + "</td>")))
                Next
                sbhtml.Append("</tr>")
            Next
            'Table end.
            sbhtml.Append("</table>")
            'ltTable.Text = sbhtml.ToString

            sbhtml.Append("<table>")
            sbhtml.Append("<tr><td></td></tr>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("<tr><td></td></tr>")
            sbhtml.Append("<tr><td></td></tr>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

End Class