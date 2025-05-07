Public Class PLD_OPE_OPERACIONES_24HORAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Operaciones 24 horas", "OPERACIONES 24 HORAS")
        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Login.aspx")
            End If
            'Acciones generales
            lbl_busqueda.Text = ""
            lbl_status.Text = ""

        End If


    End Sub

    Protected Sub lnk_Aceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Aceptar.Click
        lbl_status.Text = ""
        lbl_busqueda.Text = ""
        RegularExpressionValidator_folio_imp.Text = ""
        dag_Movimientos.Visible = False
        pnl_Observaciones.Visible = False
        llenaGrid()
        txt_Notas.Text = ""
    End Sub

    Protected Sub lnk_limpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_limpiafiltro.Click
        dag_Movimientos.Visible = False
        pnl_Observaciones.Visible = False
        lbl_status.Text = ""
        lbl_busqueda.Text = ""
        txt_folio.Text = ""
        txt_FolioImp.Text = ""
        txt_Notas.Text = ""
    End Sub

    Protected Sub llenaGrid()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter(), dtMovimientos As New Data.DataTable()

        If (txt_FolioImp.Text = "" And txt_folio.Text = "") Then
            lbl_busqueda.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"

        Else
            lbl_busqueda.Text = ""
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            If (txt_folio.Text = "") Then
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, 0)
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, txt_folio.Text)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If (txt_FolioImp.Text = "") Then
                Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 20, "")
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 20, txt_FolioImp.Text)
                Session("cmd").Parameters.Append(Session("parm"))

            End If
            Session("cmd").CommandText = "SEL_24H_MOVIMENTOS"
            Session("rs") = Session("cmd").Execute()

            custDA.Fill(dtMovimientos, Session("rs"))
            Session("Con").Close()

            If (dtMovimientos.Rows.Count > 0) Then
                dag_Movimientos.DataSource = dtMovimientos
                dag_Movimientos.DataBind()
                dag_Movimientos.Visible = True

            Else
                lbl_busqueda.Text = "No hay resultados para la búsqueda"
                dag_Movimientos.Visible = False
                pnl_Observaciones.Visible = False
            End If

        End If


    End Sub

    Private Sub dag_Movimientos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Movimientos.ItemCommand

        lbl_status.Text = ""
        If (e.CommandName = "REPORTAR") Then
            pnl_Observaciones.Visible = True
            txt_Notas.Text = ""
            btn_Cancela.Enabled = True
            Session("ID_PER_GRID") = e.Item.Cells(0).Text
            Session("NOM_PER_GRID") = e.Item.Cells(1).Text
            Session("FOLIO_GRID") = e.Item.Cells(2).Text
            Session("FOLIO_IMP_GRID") = e.Item.Cells(3).Text
            Session("FECHA_OP_GRID") = e.Item.Cells(5).Text
            Session("MONTO_GRID") = e.Item.Cells(4).Text
        End If
    End Sub

    Protected Sub btn_Confirma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Confirma.Click

        If (txt_Notas.Text.Length > 200) Then

            lbl_status.Text = "Error: Máximo 200 caracteres permitidos"
        Else
            lbl_status.Text = ""
            reportaOperacion()
        End If
    End Sub

    Protected Sub btn_Cancela_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancela.Click
        txt_folio.Text = ""
        txt_FolioImp.Text = ""
        txt_Notas.Text = ""
        dag_Movimientos.Visible = False
        pnl_Observaciones.Visible = False
    End Sub

    Protected Sub Empresa()
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_EMPRESA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("RAZONEMPRESA") = Session("rs").fields("RAZON").value
        End If
        Session("Con").Close()
    End Sub

    Private Sub reportaOperacion()
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        'SE INSERTA EL REPORTE
        Empresa()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_PER_GRID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO_GRID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 25, Session("MONTO_GRID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO_IMP_GRID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("OBSERVACION", Session("adVarChar"), Session("adParamInput"), 200, txt_Notas.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_OPERACION_ALERTA_24_HORAS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "ALERTAPLD")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()
        lbl_status.Text = "" 'IHG 2017-07-10 

        Do While Not Session("rs").EOF
            subject = "Alerta: Operaciones inusuales, REPORTE DE 24 HORAS"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) Oficial :  " + Session("rs").Fields("NOMBRE").Value.ToString + "</td></tr>")
            sbhtml.Append("<tr><td>Se le informa que se ha generado una alerta de operación inusual de 24 HORAS y se encuentra en lista de espera para su revisión.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("</table>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("RAZONEMPRESA").ToString + "</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
            Session("rs").movenext()
        Loop
        pnl_Observaciones.Visible = False

        Session("Con").Close()
        If lbl_status.Text = "" Then
            lbl_status.Text = "Se ha generado una alerta"
        End If

        txt_Notas.Text = " "
        'txt_Notas.Text = "Se Reporto la operacion inusual con informacion: " + "ID PERSONA " + Session("ID_PER_GRID") + "FOLIO PERSONA " + Session("FOLIO_GRID") + " MONTO " + Session("MONTO_GRID") + "  folio imp " + Session("FOLIO_IMP_GRID") + "USERID " + Session("USERID").ToString + "SESION" + Session("Sesion").ToString + "SUCID" + Session("SUCID")
    End Sub


End Class