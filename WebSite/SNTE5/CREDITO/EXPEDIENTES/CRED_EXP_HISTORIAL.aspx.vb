Public Class CRED_EXP_HISTORIAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Historial de Expedientes", "Historial")
        If Not Me.IsPostBack Then

            Dim menuPanel As Panel
            menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("CVEEXP")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("PROSPECTO")
            MuestraExpedientes()

            Session("VENGODE") = "ConsultaExp.aspx"

        End If

    End Sub

    'Muestra los expedientes de una persona (Historial Crediticio)
    Private Sub MuestraExpedientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_EXPEDIENTES_PERSONA"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtexpedientes, Session("rs"))
        DAG_expedientes.DataSource = dtexpedientes
        DAG_expedientes.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub DAG_expedientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_expedientes.ItemCommand

        'Se habilita el acordion de detalle
        If (e.CommandName = "DETALLE") Then

            If e.Item.Cells(2).Text = "CREDITO" Then

                folderA(panel_1, "up")
                panel_2.Visible = True
                panel_3.Visible = False

                DetalleExpediente(e.Item.Cells(0).Text)
            ElseIf e.Item.Cells(2).Text = "CAPTACION" Then

                folderA(panel_1, "up")
                panel_2.Visible = False
                panel_3.Visible = True

                DetalleExpedienteCaptacion(e.Item.Cells(0).Text)
            End If

        End If

    End Sub

    Private Sub DetalleExpediente(ByVal Folio As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        lbl_FolioDetalleB.Text = CStr(Folio) + ""
        lbl_ProductoDetalleB.Text = Session("rs").Fields("PRODUCTO").value.ToString()
        lbl_MontoB.Text = Session("rs").Fields("MONTO").value.ToString()
        lbl_PlazoB.Text = Session("rs").Fields("PLAZO").value.ToString()
        lbl_PeriodicidadB.Text = Session("rs").Fields("PERIODICIDAD").value.ToString()
        lbl_NumPagoAtrB.Text = Session("rs").Fields("NUM_PAGO_ATRAS").value.ToString()
        lbl_EstatusB.Text = Session("rs").Fields("ESTATUS").value.ToString()
        lbl_TasaNormalB.Text = Session("rs").Fields("TASA_NORMAL").value.ToString()
        lbl_TasaMoraB.Text = Session("rs").Fields("TASA_MORA").value.ToString()
        lbl_MaxDiasMoraB.Text = Session("rs").Fields("DIAS_MAX_MORA").value.ToString()
        lbl_SaldoB.Text = Session("rs").Fields("SALDO_ACTUAL").value.ToString()
        lbl_FechaUltB.Text = Session("rs").Fields("FECHA_ULT").value.ToString()
        lbl_FechaLiberaB.Text = Session("rs").Fields("AUX_FECHA_LIBERA").value.ToString()

        Session("Con").Close()

    End Sub

    Private Sub DetalleExpedienteCaptacion(ByVal Folio As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE_CAP"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        lbl_FolioCaptB.Text = CStr(Folio) + ""
        lbl_ProductoCaptB.Text = Session("rs").Fields("PRODUCTO").value.ToString()
        lbl_FechaUltDepositoB.Text = Session("rs").Fields("FECHA_ULT_DEP").value.ToString()
        lbl_FechaUltRetiroB.Text = Session("rs").Fields("FECHA_ULT_RET").value.ToString()
        lbl_SaldoCaptB.Text = Session("rs").Fields("SALDO_ACTUAL").value.ToString()
        lbl_PlazoCaptB.Text = Session("rs").Fields("PLAZO").value.ToString()
        lbl_EstausCaptB.Text = Session("rs").Fields("ESTATUS").value.ToString()
        lbl_TasaCaptB.Text = Session("rs").Fields("TASA").value.ToString()

        Session("Con").Close()

    End Sub
    Private Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)

        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            If toogle.Attributes("class").IndexOf("down") >= 0 Then
                content.Attributes.CssStyle.Add("display", "block")
            Else
                head.Attributes.CssStyle.Add("background", "#113964 !important")
                head.Attributes.CssStyle.Add("color", "#fff")
                head.Attributes.CssStyle.Add("border", "solid 1px transparent")
                head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptS" & pnl.ClientID, "$('#" & content.ClientID & "').show('6666',null);", True)
            End If

        ElseIf accion.Equals("up") Then
            If toogle.Attributes("class").IndexOf("up") >= 0 Then
                content.Attributes.CssStyle.Add("display", "none")
            Else
                head.Attributes.CssStyle.Add("background", "#113964 !important")
                head.Attributes.CssStyle.Add("color", "inherit")
                head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
                head.Attributes.CssStyle.Add("border-radius", "4px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptH" & pnl.ClientID, "$('#" & content.ClientID & "').hide('6666',null);", True)
            End If
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion

    End Sub

End Class