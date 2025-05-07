Public Class PLD_OPE_ALERTASCCC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Comité de Comunicación y Control", "COMITÉ DE COMUNICACIÓN Y CONTROL")
        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Login.aspx")
            End If

            lbl_subtitulo.Text = "Operaciones PLD pendientes"
            LlenaSesionesPendientes()

            lnk_EntrevistaPLD.Attributes.Add("OnClick", "det_EntrevistaPLD()")
            lnk_PersonaPolitica.Attributes.Add("OnClick", "his_PPE()")

        End If

        If Session("PNL") = 2 Then

            lbl_subtitulo.Text = "Información de Alerta"
            pnl_OpePend.Visible = False
            pnl_DatosOpe.Visible = True
            pnl_SesionesPLD.Visible = False

            lbl_Folio.Text = "Datos del expediente: " + Session("FOLIO")
            lbl_Prospecto.Text = Session("PROSPECTO") + " (" + Session("PERSONAID").ToString + ")"

            DetalleOperacion()

            If Session("OPERACION") <> "OPERACION PREOCUPANTE" Then

                DetalleExpediente()
                Empresa()
                If tipo_persona() = "F" Then
                    lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
                Else
                    lnk_persona.Attributes.Add("OnClick", "ResumenPersonaM()")
                End If

                '  lnk_restructura.Attributes.Add("OnClick", "det_restructura()")

                pnl_general.Visible = True
            Else
                pnl_general.Visible = False
            End If

            pnl_foro.Visible = True

            MuestraComentarios()
            Dim res As String = YaVoto()
            Votacion()

            ValidarDictaminaSesion()

            If Session("DICTAMEN_AUX") = "SI" Then
                If Session("MAYORVOTOS_AUX") = "SI" Then
                    pnl_dictamen.Visible = True
                Else
                    pnl_dictamen.Visible = False
                End If
            Else
                pnl_dictamen.Visible = False
            End If

            Session("PNL") = Nothing
        End If

    End Sub

    Protected Sub btn_ActualizarSesiones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ActualizarSesiones.Click
        Response.Redirect("PLD_OPE_ALERTASCCC.aspx")
    End Sub

    Private Sub LimpiaOpePend()
        Session("IDALERTA") = Nothing
        Session("PERSONAID") = Nothing
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("OPERACION") = Nothing
        Session("SUCURSAL") = Nothing
        Session("FECHAALERTA") = Nothing
        Session("IDOPERACION") = Nothing
        Session("TIPOPRODUCTO") = Nothing
        Session("SESION_COMITE") = Nothing
        Session("PERTENECE_AUX") = Nothing
        Session("DICTAMEN_AUX") = Nothing
        Session("IDALEOPE") = Nothing
        btn_votar.Enabled = True
        cmb_voto.Enabled = True
        txt_observacion.Text = ""
        cmb_Justificado.SelectedValue = "-1"
    End Sub

    Private Sub Limpia()
        Session("IDALERTA") = Nothing
        Session("PERSONAID") = Nothing
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("OPERACION") = Nothing
        Session("SUCURSAL") = Nothing
        Session("FECHAALERTA") = Nothing
        Session("IDOPERACION") = Nothing
        Session("TIPOPRODUCTO") = Nothing
        Session("IDALEOPE") = Nothing
        btn_votar.Enabled = True
        cmb_voto.Enabled = True
        txt_observacion.Text = ""
        cmb_Justificado.SelectedValue = "-1"
        lbl_StatusVoto.Text = ""
        cmb_voto.SelectedValue = "0"
    End Sub

    Private Sub LlenaSesionesPendientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtOpePend As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SESIONES_PLD_PENDIENTES_CCC"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtOpePend, Session("rs"))

        Session("Con").Close()

        If dtOpePend.Rows.Count > 0 Then
            dag_Sesiones.Visible = True
            dag_Sesiones.DataSource = dtOpePend
            dag_Sesiones.DataBind()
        Else
            dag_Sesiones.Visible = True
        End If

    End Sub

    Protected Sub dag_Sesiones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Sesiones.ItemCommand

        Session("SESION_COMITE") = e.Item.Cells(0).Text

        If (e.CommandName = "ABRIR") Then
            PerteneceComite()
            If Session("PERTENECE_AUX") = "SI" Then
                pnl_SesionesPLD.Visible = False
                pnl_OpePend.Visible = True
                pnl_DatosOpe.Visible = False
                lbl_subtitulo.Text = "Operaciones PLD pendientes"
                LlenaAlertasPLD()
                lbl_StatusSesiones.Text = ""
            Else
                lbl_StatusSesiones.Text = "Error: No es miembro de este comité"
            End If
        End If
        If (e.CommandName = "ACTA") Then
            If e.Item.Cells(2).Text = "PENDIENTE" Then
                lbl_StatusSesiones.Text = "Error: Aún no se han generado los dictamenes de todas la alertas de esta sesión"
            Else
                lbl_StatusSesiones.Text = ""
                GenerarActaCCC()
            End If
        End If
        If (e.CommandName = "DIGITALIZAR") Then
            If e.Item.Cells(2).Text = "PENDIENTE" Then
                lbl_StatusSesiones.Text = "Error: Aún no se han generado los dictamenes de todas la alertas de esta sesión"
            Else
                lbl_StatusSesiones.Text = ""
                Session("VENGODE") = "PLD_OPE_ALERTASCCC.aspx"
                Response.Redirect("../../DIGITALIZADOR/DIGI_GLOBAL.aspx")
            End If
        End If

    End Sub

    Private Sub PerteneceComite()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTA_PLD_PERTENECE_CCC"
        Session("rs") = Session("cmd").Execute()

        Session("PERTENECE_AUX") = Session("rs").Fields("PERTENECE").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub ValidarDictaminaSesion()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTA_PLD_VALIDADICT_CCC"
        Session("rs") = Session("cmd").Execute()

        Session("DICTAMEN_AUX") = Session("rs").Fields("DICTAMEN").value.ToString
        Session("MAYORVOTOS_AUX") = Session("rs").Fields("MAYORVOTOS").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub LlenaAlertasPLD()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtOpePend As New Data.DataTable()

        dag_OpePend.DataSource = dtOpePend
        dag_OpePend.DataBind()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_OPERACIONES_PLD_PENDIENTES_X_SESION"
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtOpePend, Session("rs"))

        Session("Con").Close()

        If dtOpePend.Rows.Count > 0 Then
            dag_OpePend.Visible = True
            dag_OpePend.DataSource = dtOpePend
            dag_OpePend.DataBind()
        Else
            dag_OpePend.Visible = True
        End If

    End Sub

    Protected Sub dag_OpePend_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_OpePend.ItemCommand

        Session("IDALERTA") = e.Item.Cells(0).Text
        Session("OPERACION") = e.Item.Cells(1).Text
        Session("SUCURSAL") = e.Item.Cells(2).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PROSPECTO") = e.Item.Cells(4).Text
        Session("FOLIO") = e.Item.Cells(5).Text
        Session("FECHAALERTA") = e.Item.Cells(6).Text
        Session("IDOPERACION") = e.Item.Cells(7).Text
        Session("TIPOPRODUCTO") = e.Item.Cells(8).Text
        Session("IDALEOPE") = e.Item.Cells(9).Text

        If (e.CommandName = "ABRIR") Then

            lbl_subtitulo.Text = "Información de Alerta"
            pnl_OpePend.Visible = False
            pnl_DatosOpe.Visible = True

            lbl_Folio.Text = "Datos del expediente: " + Session("FOLIO")
            lbl_Prospecto.Text = Session("PROSPECTO") + " (" + Session("PERSONAID").ToString + ")"

            DetalleOperacion()

            If Session("PERTENECE_AUX") = "SI" Then
                'muestro los comentarios del foro con respecto al expediente
                MuestraComentarios()
                Dim res As String = YaVoto()
                Votacion()

                ValidarDictaminaSesion()

                pnl_foro.Visible = True
                If Session("DICTAMEN_AUX") = "SI" Then

                    If Session("MAYORVOTOS_AUX") = "SI" Then
                        pnl_dictamen.Visible = True
                    Else
                        pnl_dictamen.Visible = False
                    End If
                Else
                    pnl_dictamen.Visible = False
                End If
            Else
                pnl_foro.Visible = False
                pnl_dictamen.Visible = False
            End If

            If Session("OPERACION") <> "OPERACION PREOCUPANTE" Then

                DetalleExpediente()
                Empresa()
                If tipo_persona() = "F" Then
                    lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
                Else
                    lnk_persona.Attributes.Add("OnClick", "ResumenPersonaM()")
                End If

                lnk_restructura.Attributes.Add("OnClick", "det_restructura()")

                pnl_ope.Visible = True
                pnl_general.Visible = True
                'pnl_dictamen.Visible = True
            Else
                pnl_ope.Visible = True
                pnl_general.Visible = False
                pnl_dictamen.Visible = True
            End If

            'TabContainer1.ActiveTabIndex = 2

        End If

    End Sub

    Function tipo_persona() As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_TIPO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("TIPO").value.ToString
        Session("Con").Close()
        Return tipo

    End Function

    Private Sub DetalleOperacion()

        lbl_Status.Text = ""
        lbl_IDAlertaM.Text = Session("IDALERTA")
        lbl_TipoOpeM.Text = Session("OPERACION")
        lbl_SucursalM.Text = Session("SUCURSAL")
        lbl_FechaAlertaM.Text = Session("FECHAALERTA")

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DETALLE_OPERACION_PLD"
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        lbl_FolioImpM.Text = Session("rs").Fields("FOLIO_IMP").value.ToString
        lbl_MontoOpePLDM.Text = FormatCurrency(Session("rs").Fields("MONTO").value.ToString)
        lbl_NotaOpePLDM.Text = Session("rs").Fields("NOTA").value.ToString

        If Session("OPERACION") = "OPERACION PREOCUPANTE" Then
            lbl_RealizoEntM.Text = "-"
            lnk_EntrevistaPLD.Enabled = False
            lnk_HistorialAlertas.Enabled = False
            lnk_PersonaPolitica.Enabled = False
            lbl_TipoPerfilM.Text = "-"
            lbl_PerfilPersonaM.Text = "-"
        Else
            If Session("rs").Fields("REALIZO_ENT").value.ToString = "-1" Then
                lbl_RealizoEntM.Text = "ENTREVISTA AUN NO CAPTURADA"
                lnk_EntrevistaPLD.Enabled = False
                lnk_HistorialAlertas.Enabled = False
                lnk_PersonaPolitica.Enabled = False
            ElseIf Session("rs").Fields("REALIZO_ENT").value.ToString = "1" Then
                lbl_RealizoEntM.Text = "ENTREVISTA REALIZADA CON EXITO"
                lnk_EntrevistaPLD.Enabled = True
                lnk_HistorialAlertas.Enabled = True
                lnk_PersonaPolitica.Enabled = True
            Else
                lbl_RealizoEntM.Text = "ENTREVISTA NO REALIZADA"
                lnk_EntrevistaPLD.Enabled = True
                lnk_HistorialAlertas.Enabled = True
                lnk_PersonaPolitica.Enabled = True
            End If
            lbl_TipoPerfilM.Text = Session("rs").Fields("TIPO_PERFIL").value.ToString
            lbl_PerfilPersonaM.Text = FormatCurrency(Session("rs").Fields("PERFIL").value.ToString)
        End If

        Session("Con").Close()

        DetalleDictamenOC()

    End Sub

    Private Sub DetalleDictamenOC()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DETALLE_DICTAMEN_OC"
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        lbl_DictamenOC1.Text = Session("rs").Fields("DICTAMEN_OC").value.ToString
        lbl_ObservacionesOC1.Text = Session("rs").Fields("OBSERVACIONES_OC").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub DetalleExpediente()

        If Session("TIPOPRODUCTO") = "1" Then

            pnl_DatosCred.Visible = True
            pnl_DatosCaptacion.Visible = False

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            lbl_ProductoDetalleB.Text = Session("rs").Fields("PRODUCTO").value.ToString
            lbl_MontoB.Text = Session("rs").Fields("MONTO").value.ToString
            lbl_PlazoB.Text = Session("rs").Fields("PLAZO").value.ToString
            lbl_PeriodicidadB.Text = Session("rs").Fields("PERIODICIDAD").value.ToString
            lbl_TasaNormalB.Text = Session("rs").Fields("TASA_NORMAL").value.ToString
            lbl_TasaMoraB.Text = Session("rs").Fields("TASA_MORA").value.ToString
            lbl_fechaliberaB.Text = Session("rs").Fields("AUX_FECHA_LIBERA").value.ToString


            Session("TIPOPLANPAGO") = Session("rs").Fields("TIPOPLAN").value
            Select Case Session("TIPOPLANPAGO")
                Case "SI"
                    lbl_tipoplanB.Text = "SALDOS INSOLUTOS"
                Case "PFSI"
                    lbl_tipoplanB.Text = "PAGOS FIJOS SI"
                Case "ES"
                    lbl_tipoplanB.Text = "PLAN ESPECIAL"
            End Select

            Session("Con").Close()

            lnk_garantias.Visible = True
            lnk_gastos.Visible = True
            lnk_restructura.Visible = True

        ElseIf Session("TIPOPRODUCTO") = "2" Then

            pnl_DatosCred.Visible = False
            pnl_DatosCaptacion.Visible = True

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DETALLE_EXPEDIENTE_CAPTACION"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            lbl_ProductoCap1.Text = Session("rs").Fields("PRODUCTO").value.ToString
            lbl_TasaCap1.Text = Session("rs").Fields("TASA_CAP").value.ToString
            lbl_SaldoCap1.Text = FormatCurrency(Session("rs").Fields("SALDO").value.ToString)
            lbl_UltFechaDepCap1.Text = Session("rs").Fields("ULT_FECHA_DEP").value.ToString
            lbl_UltFechaRetCap1.Text = Session("rs").Fields("ULT_FECHA_RET").value.ToString

            Session("Con").Close()

            lnk_garantias.Visible = False
            lnk_gastos.Visible = False
            lnk_restructura.Visible = False

        Else
            lnk_garantias.Visible = False
            lnk_gastos.Visible = False
            lnk_restructura.Visible = False
        End If

    End Sub

    Private Sub Empresa()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_EMPRESA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("EMPRESA") = Session("rs").fields("RAZON").value
        End If
        Session("Con").Close()

    End Sub

    Protected Sub lnk_garantias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_garantias.Click
        Session("VENGODE") = "AdmAlertasCCC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_GARANTIA.aspx")
    End Sub

    Protected Sub lnk_historial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_historial.Click
        Session("VENGODE") = "AdmAlertasCCC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_HISTORIAL.aspx")
    End Sub

    Protected Sub lnk_docsexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_docsexp.Click
        Session("VENGODE") = "AdmAlertasCCC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_EXP_DIGITAL.aspx")
    End Sub

    Protected Sub lnk_HistorialAlertas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_HistorialAlertas.Click
        Session("VENGODE") = "AdmAlertasCCC.aspx"
        Session("PNL") = 2
        Response.Redirect("../ALERTAS/PLD_ALE_HISTORIAL.aspx")
    End Sub

    Protected Sub lnk_redsocial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_redsocial.Click
        Session("VENGODE") = "AdmAlertasCCC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../UNIVERSAL/UNI_RED_SOCIAL.aspx")
    End Sub

    Protected Sub lnk_gastos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_gastos.Click
        Session("VENGODE") = "AdmAlertasCCC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_INVESTIGACIONES.aspx")
    End Sub

    '------------------------------------PLANES DE PAGOS-----------------------------------
    Protected Sub lnk_PlanPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_PlanPagos.Click
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_PLAN_GENERAL.aspx")
    End Sub

    Private Sub MuestraComentarios()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcomentarios As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESFORO", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTAS_PLD_COMENTARIOS_FORO"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtcomentarios, Session("rs"))
        DAG_post.DataSource = dtcomentarios
        DAG_post.DataBind()

        Session("Con").Close()
    End Sub

    Private Function YaVoto() As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTAS_PLD_YA_VOTO"
        Session("rs") = Session("cmd").Execute()

        Dim res As String = Session("rs").Fields("YAVOTO").value.ToString()
        Session("Con").Close()

        If res = "SI" Then 'ya voto
            cmb_voto.Enabled = False
            btn_votar.Enabled = False
        Else
            res = "NO"
        End If

        Return res
    End Function

    Private Sub Votacion()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTAS_PLD_VOTACION_SESION"
        Session("rs") = Session("cmd").Execute()

        lbl_votosf.Text = "Justificado: " + Session("rs").Fields("FAVOR").value.ToString()
        lbl_votosc.Text = "No Justificado: " + Session("rs").Fields("CONTRA").value.ToString()
        lbl_miembrosc.Text = "Miembros comite: " + Session("rs").Fields("MIEMBROS").value.ToString()

        Session("Con").Close()
    End Sub

    Private Sub CambiaPage(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DAG_post.PageIndexChanged
        DAG_post.CurrentPageIndex = e.NewPageIndex
        MuestraComentarios()
    End Sub

    Protected Sub btn_votar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_votar.Click
        Dim res As String = YaVoto()


        txt_comentario.Text = ""
        If res = "NO" Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION_COMITE"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VOTO", Session("adVarChar"), Session("adParamInput"), 10, cmb_voto.SelectedItem.Value.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_ALERTAS_PLD_VOTO_COMITE"
            Session("cmd").Execute()
            Session("Con").Close()

            lbl_StatusVoto.Text = "Voto emitido correctamente."
            cmb_voto.Enabled = False
            btn_votar.Enabled = False
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 15, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTAS_PLD_OBTIENE_SIGUIENTE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("Con").Close()
            ModalPopupExtender1.Show()
        Else
            Session("Con").Close()
            Votacion()
            ValidarDictaminaSesion()
            If Session("DICTAMEN_AUX") = "SI" Then
                If Session("MAYORVOTOS_AUX") = "SI" Then
                    pnl_dictamen.Visible = True
                Else
                    pnl_dictamen.Visible = False
                End If
            Else
                pnl_dictamen.Visible = False
            End If
        End If
    End Sub

    Protected Sub btn_SigAlerta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SigAlerta.Click

        lbl_StatusVoto.Text = ""
        cmb_voto.Enabled = True
        btn_votar.Enabled = True

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 15, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTAS_PLD_OBTIENE_SIGUIENTE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("SESION_COMITE") = Session("rs").Fields("IDSESION").value.ToString()
            Session("IDALERTA") = Session("rs").Fields("IDALERTA").value.ToString()
            Session("OPERACION") = Session("rs").Fields("OPERACION").value.ToString()
            Session("SUCURSAL") = Session("rs").Fields("SUCURSAL").value.ToString()
            Session("PERSONAID") = Session("rs").Fields("PERSONAID").value.ToString()
            Session("PROSPECTO") = Session("rs").Fields("PROSPECTO").value.ToString()
            Session("FOLIO") = Session("rs").Fields("FOLIO").value.ToString()
            Session("FECHAALERTA") = Session("rs").Fields("FECHAALERTA").value.ToString()
            Session("IDOPERACION") = Session("rs").Fields("IDOPERACION").value.ToString()
            Session("TIPOPRODUCTO") = Session("rs").Fields("TIPOPRODUCTO").value.ToString()
            Session("IDALEOPE") = Session("rs").Fields("IDALEOPE").value.ToString()
        End If

        Session("Con").Close()

        lbl_subtitulo.Text = "Información de Alerta"
        lbl_StatusSesiones.Text = ""
        pnl_SesionesPLD.Visible = False
        pnl_OpePend.Visible = False
        pnl_DatosOpe.Visible = True
        LlenaAlertasPLD()

        lbl_Folio.Text = "Datos del expediente: " + Session("FOLIO")
        lbl_Prospecto.Text = Session("PROSPECTO") + " (" + Session("PERSONAID").ToString + ")"

        DetalleOperacion()

        'muestro los comentarios del foro con respecto al expediente
        MuestraComentarios()
        Dim res As String = YaVoto()
        Votacion()

        ValidarDictaminaSesion()

        pnl_foro.Visible = True
        If Session("DICTAMEN_AUX") = "SI" Then
            If Session("MAYORVOTOS_AUX") = "SI" Then
                pnl_dictamen.Visible = True
            Else
                pnl_dictamen.Visible = False
            End If
        Else
            pnl_dictamen.Visible = False
        End If

        If Session("OPERACION") <> "OPERACION PREOCUPANTE" Then
            DetalleExpediente()
            Empresa()
            If tipo_persona() = "F" Then
                lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
            Else
                lnk_persona.Attributes.Add("OnClick", "ResumenPersonaM()")
            End If

            lnk_restructura.Attributes.Add("OnClick", "det_restructura()")

            pnl_ope.Visible = True
            pnl_general.Visible = True
            'pnl_dictamen.Visible = True
        Else
            pnl_ope.Visible = True
            pnl_general.Visible = False
            pnl_dictamen.Visible = True
        End If
        'TabContainer1.ActiveTabIndex = 2
        ModalPopupExtender1.Hide()

    End Sub

    Protected Sub btn_NoSigAlerta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_NoSigAlerta.Click

        Votacion()
        ValidarDictaminaSesion()
        If Session("DICTAMEN_AUX") = "SI" Then
            If Session("MAYORVOTOS_AUX") = "SI" Then
                pnl_dictamen.Visible = True
            Else
                pnl_dictamen.Visible = False
            End If
        Else
            pnl_dictamen.Visible = False
        End If

        ModalPopupExtender1.Hide()

    End Sub

    Protected Sub btn_enviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_enviar.Click
        If txt_comentario.Text.Length > 1000 Then
            lbl_StatusVoto.Text = "Error: Los comentarios deben contener un maximo de 1000 caracteres."
            txt_comentario.Text = ""
            Exit Sub
        End If
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESFORO", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("COMENTARIO", Session("adVarChar"), Session("adParamInput"), 1000, txt_comentario.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 15, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_ALERTAS_PLD_COMENTARIO_FORO"
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_StatusVoto.Text = ""
        MuestraComentarios()
        txt_comentario.Text = ""

    End Sub

    Protected Sub btn_Resultado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Resultado.Click

        If txt_observacion.Text.Length > 5000 Then
            lbl_statusresultado.Text = "Error: Las observaciones deben contener un máximo de 5000 caracteres."
            txt_observacion.Text = ""
            Exit Sub
        End If
        lbl_statusresultado.Text = ""
        TerminarRevision()
        Limpia()

    End Sub

    Private Sub TerminarRevision()

        Dim Estatus As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RESULTADO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Justificado.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("OBSERVACIONES", Session("adVarChar"), Session("adParamInput"), 5000, txt_observacion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_DICTAMEN_CCC"
        Session("rs") = Session("cmd").Execute()

        Estatus = Session("rs").fields("ESTATUS").value.ToString
        Session("Con").Close()

        If Estatus = "PENDIENTES" Then
            Limpia()
            lbl_Status.Text = "Dictamen final sobre alerta se ha guardado."
            lbl_StatusSesiones.Text = ""
            pnl_DatosOpe.Visible = False
            pnl_OpePend.Visible = True
            pnl_SesionesPLD.Visible = False
            LlenaAlertasPLD()
        Else
            LimpiaOpePend()
            lbl_Status.Text = ""
            lbl_StatusSesiones.Text = "Ha finalizado con las alertas de esta sesión. Favor de generar el acta, firmarla y digitalizarla."
            pnl_DatosOpe.Visible = False
            pnl_OpePend.Visible = False
            pnl_SesionesPLD.Visible = True
            LlenaSesionesPendientes()
        End If
    End Sub

    Private Sub GenerarActaCCC()

        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaSesionCCC1.pdf")

        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        'CREACION DE UN DOCUMENTO

        Dim document As New iTextSharp.text.Document(psize, 60, 60, 108, 108)
        With document
            .AddAuthor("Desarrollo - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Reporte Denominación")
            .AddSubject("Reporte Denominación")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Reporte Denominación")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Reporte Denominación")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO

        Dim writer As iTextSharp.text.pdf.PdfWriter
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        ' step 3: we open the document
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Solicitud As iTextSharp.text.pdf.PdfImportedPage

        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

        cb.BeginText()

        Dim bf As iTextSharp.text.pdf.BaseFont
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        Dim X, Y As Single
        X = 425
        Y = 685

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_ACTA_CCC"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 460
            Y = Y - 53
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "10:00", X, Y, 0)
            X = X + 95
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 95
            Y = Y - 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 130
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_REL").Value.ToString, X, Y, 0)
            X = X - 200
            Y = Y - 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TRIMESTRE").Value.ToString, X, Y, 0)
            X = X + 250
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_REL").Value.ToString, X, Y, 0)
            X = X - 170
            Y = Y - 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 85
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_INU").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 103
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 215
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_INU").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_PRE").Value.ToString, X, Y, 0)

            cb.EndText()

            document.NewPage()
            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaSesionCCC2.pdf")

            cb = writer.DirectContent
            Solicitud = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
            cb.BeginText()
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            X = 80
            Y = 650

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 210
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_PRE").Value.ToString, X, Y, 0)
            X = X + 265
            Y = Y - 35
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 520
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 80
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)

        End If

        Y = Y - 50
        Dim lado As Integer = 0

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ALERTAPLD_MIEMBROS_COMITE_ACTA"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF

                If lado = 0 Then
                    X = 165
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________________", X, Y, 0)
                    Y = Y - 12
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("USUARIO").Value.ToString, X, Y, 0)
                    Y = Y + 12
                    lado = 1
                Else
                    X = 445
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________________", X, Y, 0)
                    Y = Y - 12
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("USUARIO").Value.ToString, X, Y, 0)
                    Y = Y + 12
                    lado = 0
                End If

                If Y < 30 And lado = 0 Then
                    cb.EndText()
                    X = 300
                    Y = 750

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

                    cb = writer.DirectContent
                    Solicitud = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "MIEMBROS DE SESIÓN", X, Y, 0)

                    Y = Y - 50
                End If

                Y = Y - 50

                Session("rs").movenext()
            Loop
        End If

        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

        cb = writer.DirectContent
        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        X = 300
        Y = 750

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

        X = 50
        Y = Y - 30

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ALERTAPLD_X_SESION_ACTA"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF
                If (Y - 80) < 20 Then
                    cb.EndText()
                    X = 300
                    Y = 750

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

                    cb = writer.DirectContent
                    Solicitud = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

                    X = 50
                    Y = Y - 30
                End If

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Operación: " + Session("rs").Fields("OPERACION").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Len(Session("rs").Fields("NOTA").Value.ToString) > 100 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Left(Session("rs").Fields("NOTA").Value.ToString, 100), X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                     " + Mid(Session("rs").Fields("NOTA").Value.ToString, 101), X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Session("rs").Fields("NOTA").Value.ToString, X, Y, 0)
                End If

                Y = Y - 13
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Session("rs").Fields("FOLIO").Value.ToString = "-1" Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: ", X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: " + Session("rs").Fields("FOLIO").Value.ToString, X, Y, 0)
                End If

                X = X + 250
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha: " + Left(Session("rs").Fields("FECHA").Value.ToString, 10), X, Y, 0)

                X = X - 250
                Y = Y - 13
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Dictamen: " + Session("rs").Fields("ESTATUS").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Len(Session("rs").Fields("OBSERVACIONES").Value.ToString) > 100 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Left(Session("rs").Fields("OBSERVACIONES").Value.ToString, 100), X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                       " + Mid(Session("rs").Fields("OBSERVACIONES").Value.ToString, 101), X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Session("rs").Fields("OBSERVACIONES").Value.ToString, X, Y, 0)
                End If

                Y = Y - 20

                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

        cb.EndText()

        document.Close()

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ReporteAlertasPLD(" + Session("SESION_COMITE") + ").pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With

    End Sub

    Private Sub lnk_restructura_Click(sender As Object, e As EventArgs) Handles lnk_restructura.Click

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

        modal_port.Show()
    End Sub

    Private Sub lnk_actualiza_Click(sender As Object, A As EventArgs) Handles lnk_actualiza.Click
        MuestraComentarios()
    End Sub

End Class