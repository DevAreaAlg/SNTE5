Public Class PLD_ALE_HISTORIAL
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Historial de alertas", "Historial de alertas PLD")
        If Not Me.IsPostBack Then

        End If

        Session("VENGODE_HIST_ALERT_PLD") = "PLD_ALE_HISTORIAL.aspx"

        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + lnk_Continuar.ClientID + "')")
        lnk_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")
        lnk_EntrevistaPLD.Attributes.Add("OnClick", "det_EntrevistaPLD()")

        If Not Session("idperbusca") Is Nothing Then
            lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If

        If Session("VENGODE") = "AdmExpedientesOC.aspx" Or Session("VENGODE") = "AdmAlertasCCC.aspx" Or Session("VENGODE") = "PLD_ALE_PERFIL.aspx" Then
            lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString

            pnl_cliente.Visible = False
            LlenaHistorial()
        End If

    End Sub

    Protected Sub lnk_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Continuar.Click

        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") Is Nothing Then
                lbl_statusc.Text = "Error: número de cliente incorrecto."
            Else
                lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
                LlenaHistorial()
            End If
        Else
            BuscarIDCliente(txt_IdCliente.Text)
        End If

    End Sub

    Private Sub BuscarIDCliente(ByVal idcliente As String)

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, idcliente)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = Nothing
            lbl_statusc.Text = "Error: no existe el número de cliente."
            txt_IdCliente.Text = ""
        Else
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
            LlenaHistorial()
        End If

    End Sub

    Private Sub LlenaHistorial()

        'TabPanel2.Visible = True
        'pnl_detalle.Visible = True

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAlertPLD As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_HISTORIAL_ALERTAS_PLD_X_PERSONA"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtAlertPLD, Session("rs"))

        If dtAlertPLD.Rows.Count > 0 Then
            dag_AlertPLD.Visible = True
            dag_AlertPLD.DataSource = dtAlertPLD
            dag_AlertPLD.DataBind()
        Else
            dag_AlertPLD.Visible = True
        End If

        Session("Con").Close()

    End Sub

    Protected Sub dag_AlertPLD_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_AlertPLD.ItemCommand

        If (e.CommandName = "DETALLE") Then
            lnk_Cerrar.Visible = True
            Session("IDALERTA_AUX") = e.Item.Cells(0).Text
            Session("IDOPERACION_AUX") = e.Item.Cells(1).Text
            Session("OPERACION_AUX") = e.Item.Cells(2).Text
            Session("SUCURSAL_AUX") = e.Item.Cells(4).Text
            Session("FECHAALERTA_AUX") = e.Item.Cells(5).Text
            Session("IDALEOPE_AUX") = e.Item.Cells(8).Text

            DetalleOperacion()
            DetalleDictamenOC()
            DetalleDictamenCCC()

            pnl_Lista.Visible = False
            pnl_detalle.Visible = True
        End If

    End Sub

    Private Sub DetalleOperacion()

        lbl_Status.Text = ""
        lbl_IDAlertaM.Text = Session("IDALERTA_AUX")
        lbl_TipoOpeM.Text = Session("OPERACION_AUX")
        lbl_SucursalM.Text = Session("SUCURSAL_AUX")
        lbl_FechaAlertaM.Text = Session("FECHAALERTA_AUX")

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DETALLE_OPERACION_PLD"
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        lbl_FolioImpM.Text = Session("rs").Fields("FOLIO_IMP").value.ToString
        lbl_MontoOpePLDM.Text = FormatCurrency(Session("rs").Fields("MONTO").value.ToString)
        lbl_NotaOpePLDM.Text = Session("rs").Fields("NOTA").value.ToString

        If Session("OPERACION") = "OPERACION PREOCUPANTE" Then
            lbl_RealizoEntM.Text = "-"
            lnk_EntrevistaPLD.Visible = False
            lbl_TipoPerfilM.Text = "-"
            lbl_PerfilPersonaM.Text = "-"
        Else
            If Session("rs").Fields("REALIZO_ENT").value.ToString = "-1" Then
                lbl_RealizoEntM.Text = "ENTREVISTA AUN NO CAPTURADA"
                lnk_EntrevistaPLD.Visible = False
            ElseIf Session("rs").Fields("REALIZO_ENT").value.ToString = "1" Then
                lbl_RealizoEntM.Text = "ENTREVISTA REALIZADA CON EXITO"
                lnk_EntrevistaPLD.Visible = True
            Else
                lbl_RealizoEntM.Text = "ENTREVISTA NO REALIZADA"
                lnk_EntrevistaPLD.Visible = True
            End If
            lbl_TipoPerfilM.Text = Session("rs").Fields("TIPO_PERFIL").value.ToString
            lbl_PerfilPersonaM.Text = FormatCurrency(Session("rs").Fields("PERFIL").value.ToString)
        End If

        Session("Con").Close()

    End Sub

    Private Sub DetalleDictamenOC()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DETALLE_DICTAMEN_OC"
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").Fields("DICTAMEN_OC").value.ToString <> Nothing Then
                lbl_DictamenOC1.Text = Session("rs").Fields("DICTAMEN_OC").value.ToString
                lbl_ObservacionesOC1.Text = Session("rs").Fields("OBSERVACIONES_OC").value.ToString
            Else
                lbl_DictamenOC1.Text = ""
                lbl_ObservacionesOC1.Text = ""
            End If
        End If

        Session("Con").Close()

    End Sub

    Private Sub DetalleDictamenCCC()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DETALLE_DICTAMEN_CCC"
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE_AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            If Session("rs").Fields("DICTAMEN_CCC").value.ToString <> Nothing Then
                lbl_DictamenCCC1.Text = Session("rs").Fields("DICTAMEN_CCC").value.ToString
                lbl_ObservacionesCCC1.Text = Session("rs").Fields("OBSERVACIONES_CCC").value.ToString
            Else
                lbl_DictamenCCC1.Text = ""
                lbl_ObservacionesCCC1.Text = ""
            End If
        End If

        Session("Con").Close()

    End Sub

    Protected Sub lnk_Cerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Cerrar.Click
        pnl_detalle.Visible = False
        pnl_Lista.Visible = True
        LlenaHistorial()
    End Sub


End Class