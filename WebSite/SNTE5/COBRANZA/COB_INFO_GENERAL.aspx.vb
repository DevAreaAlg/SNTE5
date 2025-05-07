Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class COB_INFO_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Información general", "Información general")
        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If
        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
            Session("idperbusca") = Nothing
        End If
    End Sub

    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_seleccionar.Click
        Session("PERSONAID") = CInt(txt_cliente.Text)
        LimpiarControles()
        limpiapanels()
        Llenadatos()
    End Sub

    Private Sub LlenaFoliosDespacho()

        cmb_folio.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_folio.Items.Add(elija)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_cliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_FOLIOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            If Session("rs").Fields("COND").value.ToString = "0" Then
                cmb_folio.Enabled = False
                lbl_Info.Text = "El cliente no cuenta con expedientes activos o liquidados"
                Session("Con").Close()

                Exit Sub
            End If

            If Session("rs").Fields("NOMBRE").value.ToString = "-1" Then
                lbl_Info.Text = "El número de cliente introducido no existe"
                cmb_folio.Enabled = False
                Session("Con").Close()
                Exit Sub
            End If

            ' lbl_clienteB.Text = "" + Session("rs").Fields("NOMBRE").Value.value.ToString
            cmb_folio.Enabled = True
            lbl_Info.Text = ""

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("FOLIO").value.ToString, Session("rs").Fields("FOLIO").value.ToString)
                cmb_folio.Items.Add(item)
                Session("rs").movenext()
            Loop
        Else
            cmb_folio.Enabled = False
            lbl_Info.Text = "El cliente no cuenta con expedientes activos o liquidados"

        End If

        Session("Con").Close()
    End Sub

    Private Sub Llenadatos()
        pnl_info.Visible = False
        pnl_dom.Visible = False
        pnl_aval.Visible = False
        pnl_ref.Visible = False
        pnl_abog.Visible = False
        pnl_bit.Visible = False
        pnl_calc.Visible = False
        pnl_rep.Visible = False
        LlenaFoliosDespacho()

    End Sub

    Protected Sub cmb_folio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_folio.SelectedIndexChanged
        If cmb_folio.SelectedIndex = "0" Then
            pnl_info.Visible = False
            pnl_dom.Visible = False
            pnl_aval.Visible = False
            pnl_ref.Visible = False
            pnl_abog.Visible = False
            pnl_bit.Visible = False
            pnl_calc.Visible = False
            pnl_rep.Visible = False
        Else
            pnl_info.Visible = True
            pnl_dom.Visible = True
            pnl_aval.Visible = True
            pnl_ref.Visible = True
            pnl_abog.Visible = True
            pnl_bit.Visible = True
            pnl_calc.Visible = True
            pnl_rep.Visible = True
            Dim Folio = cmb_folio.SelectedItem.Value

            InfoCta(Folio)
            Domicilio(Folio)
            AvalesGtias(Folio, "AVAL")
            Referencias(Folio, "REF")
            Abogados(Folio)

            'Controles Bitácora
            pnl_cobranza.Visible = False
            pnl_evento_cita.Visible = False
            pnl_agendar.Visible = False
            pnl_seg.Visible = False
            pnl_seguimiento.Visible = False
            pnl_llamada.Visible = False
            pnl_aviso.Visible = False
            pnl_seguimiento_aviso.Visible = False
            pnl_citatorio.Visible = False
            pnl_seguimiento_citatorio.Visible = False
            pnl_reg_juicio.Visible = False
            lbl_Info.Text = ""
            lbl_status.Text = ""

            If Folio <> 0 Then
                Session("FOLIO") = Folio
                Llenaeventos()
                DatosExpediente()

            Else
                Session("FOLIO") = Nothing

            End If

            'Controles de Calculador

            datos()
            txt_fechacorte.Text = ""
            lbl_info_fecha_corte.Visible = False

            'Controles de Asignacion
            LLENAEXPEDIENTES(1, Folio)
        End If

    End Sub

    Private Sub LimpiarControles()
        Limpia_InfoCta()
        LimpiaAvGtia()
        LimpiaDomicilio()
        LimpiaREF()
        LimpiaAbogados()
    End Sub

    Private Sub Limpia_InfoCta()

        lbl_folio.Text = ""
        lbl_actualizacion.Text = ""
        lbl_num_cliente.Text = ""
        lbl_nombre.Text = ""
        lbl_producto.Text = ""
        lbl_plazo.Text = ""
        lbl_unidad_plazo.Text = ""
        lbl_periodicidad.Text = ""
        lbl_modalidad.Text = ""
        lbl_monto.Text = ""
        lbl_tasa_ord.Text = ""
        lbl_tasa_mor.Text = ""
        lbl_sucursal.Text = ""
        lbl_fecha_liberacion.Text = ""
        lbl_fecha_vencimiento.Text = ""

        lbl_saldo_insoluto.Text = ""

        lbl_int_ord_gravado.Text = ""
        lbl_int_ord_exento.Text = ""
        lbl_total_int_ord.Text = ""
        lbl_iva_int_ord.Text = ""
        lbl_int_mor_gravado.Text = ""
        lbl_int_mor_exento.Text = ""
        lbl_total_int_mor.Text = ""

        lbl_iva_int_mor.Text = ""
        lbl_comision.Text = ""
        lbl_iva_comision.Text = ""

        lbl_int_ccartera.Text = ""
        lbl_iva_int_ccartera.Text = ""
        lbl_fecha_ultimo_pago.Text = ""
        lbl_monto_ultimo_pago.Text = ""
        lbl_fecha_prox_pago.Text = ""
        lbl_monto_prox_pago.Text = ""
        lbl_dias_vencer.Text = ""
        lbl_capital_vencido.Text = ""
        lbl_monto_regularizacion.Text = ""

        lbl_monto_liquidar.Text = ""
        lbl_abonos_vencidos.Text = ""
        lbl_dias_mora.Text = ""
        lbl_num_reestructura.Text = ""
        lbl_folio_reestructura.Text = ""
        lbl_folio_cta_eje.Text = ""
        lbl_saldo_cta_eje.Text = ""
        lbl_fecha_ult_mov_cta_eje.Text = ""
        lbl_saldo_ult_mov_cta_eje.Text = ""
    End Sub

    Private Sub LimpiaDomicilio()

        lbl_tipo_persona.Text = ""
        lbl_domicilio.Text = ""
        lbl_tel_casa.Text = ""
        lbl_tel_cel.Text = ""
        lbl_tel_ofi.Text = ""
        'lbl_ext_ofi.Text = ""
        lbl_destino.Text = ""
        lbl_id_suc.Text = ""

    End Sub

    Private Sub LimpiaREF()
        lbl_id_persona_ref1.Text = ""
        lbl_nombre_ref1.Text = ""
        lbl_direccion_ref1.Text = ""
        lbl_tel_casa_ref1.Text = ""
        lbl_tel_ofi_ref1.Text = ""
        lbl_ext_ofi_ref1.Text = ""
        lbl_tel_cel_ref1.Text = ""
        lbl_id_persona_ref2.Text = ""
        lbl_nombre_ref2.Text = ""
        lbl_direccion_ref2.Text = ""
        lbl_tel_casa_ref2.Text = ""
        lbl_tel_ofi_ref2.Text = ""
        lbl_ext_ofi_ref2.Text = ""
        lbl_tel_cel_ref2.Text = ""

        lbl_id_persona_ref3.Text = ""
        lbl_nombre_ref3.Text = ""
        lbl_direccion_ref3.Text = ""
        lbl_tel_Casa_ref3.Text = ""
        lbl_tel_ofi_ref3.Text = ""
        lbl_ext_ofi_ref3.Text = ""
        lbl_tel_cel_ref3.Text = ""
    End Sub

    Private Sub LimpiaAvGtia()

        lbl_tipo_garantias.Text = ""
        lbl_valor_gtias.Text = ""
        lbl_descripcion_gtias.Text = ""

        lbl_id_persona_aval1.Text = ""
        lbl_nombre_aval1.Text = ""
        lbl_direccion_aval1.Text = ""
        lbl_tel_Casa_aval1.Text = ""
        lbl_tel_ofi_aval1.Text = ""
        lbl_ext_tel_ofi_aval1.Text = ""
        lbl_tel_movil_aval1.Text = ""

        lbl_id_persona_aval2.Text = ""
        lbl_nombre_aval2.Text = ""
        lbl_direccion_aval2.Text = ""
        lbl_tel_casa_aval2.Text = ""
        lbl_tel_ofi_aval2.Text = ""
        lbl_ext_tel_ofi_aval2.Text = ""
        lbl_tel_movil_aval2.Text = ""
    End Sub

    Private Sub LimpiaAbogados()
        lbl_estatus_juicio.Text = ""
        lbl_fecha_diligencia.Text = ""
        lbl_juzgado.Text = ""
        lbl_exhorto.Text = ""
        lbl_juzgado_exhortado.Text = ""
        lbl_fecha_prom_pago.Text = ""
        lbl_monto_prom_pago.Text = ""
        lbl_gestor.Text = ""
        lbl_cita.Text = ""
        txt_detalle.Text = ""

    End Sub

    Private Sub InfoCta(ByVal Folio As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_CTA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            lbl_estatus_Cob.Text = Session("rs").fields("ESTATUS_COB").value.ToString
            lbl_folio.Text = Session("rs").fields("FOLIO").value.ToString
            lbl_actualizacion.Text = Session("rs").fields("FECHA_GENERACION").value.ToString
            lbl_num_cliente.Text = Session("rs").fields("NUM_CLIENTE").value.ToString
            lbl_nombre.Text = Session("rs").fields("NOMBRE").value.ToString
            lbl_producto.Text = Session("rs").fields("PRODUCTO").value.ToString
            lbl_plazo.Text = Session("rs").fields("PLAZO").value.ToString
            lbl_unidad_plazo.Text = Session("rs").fields("UNIDAD_PLAZO").value.ToString
            lbl_periodicidad.Text = Session("rs").fields("PERIODICIDAD").value.ToString
            lbl_modalidad.Text = Session("rs").fields("MODALIDAD_PAGO").value.ToString
            lbl_monto.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("MONTO_CREDITO").value.ToString)
            lbl_tasa_ord.Text = Session("rs").fields("TASA_ORDINARIA").value.ToString + " % Anual"
            lbl_tasa_mor.Text = Session("rs").fields("TASA_MORATORIA").value.ToString + " % Mensual"
            lbl_sucursal.Text = Session("rs").fields("SUCURSAL").value.ToString

            If Session("rs").fields("FECHA_LIBERACION").value.ToString = "" Then
                lbl_fecha_liberacion.Text = "-"
            Else
                lbl_fecha_liberacion.Text = Session("rs").fields("FECHA_LIBERACION").value.ToString
            End If

            If Session("rs").fields("FECHA_VENCIMIENTO").value.ToString = "" Then
                lbl_fecha_vencimiento.Text = "-"
            Else
                lbl_fecha_vencimiento.Text = Session("rs").fields("FECHA_VENCIMIENTO").value.ToString
            End If

            lbl_saldo_insoluto.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("SALDO_INSOLUTO").value.ToString)

            lbl_int_ord_gravado.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("INTERES_ORDINARIO_GRAVADO").value.ToString)
            lbl_int_ord_exento.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("INTERES_ORDINARIO_EXENTO").value.ToString)
            lbl_total_int_ord.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("TOTAL_INTERES_ORDINARIO").value.ToString)
            lbl_iva_int_ord.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("IVA_INTERES_ORDINARIO").value.ToString)
            lbl_int_mor_gravado.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("INTERES_MORATORIO_GRAVADO").value.ToString)
            lbl_int_mor_exento.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("INTERES_MORATORIO_EXENTO").value.ToString)
            lbl_total_int_mor.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("TOTAL_INTERES_MORATORIO").value.ToString)

            lbl_iva_int_mor.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("IVA_INTERES_MORATORIO").value.ToString)
            lbl_comision.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("COMISION").value.ToString)
            lbl_iva_comision.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("IVA_COMISION").value.ToString)

            lbl_int_ccartera.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("INTERESES_CCARTERA").value.ToString)
            lbl_iva_int_ccartera.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("IVA_INTERESES_CCARTERA").value.ToString)
            If Session("rs").fields("FECHA_ULTIMO_PAGO").value.ToString = "" Then
                lbl_fecha_ultimo_pago.Text = "-"
            Else
                lbl_fecha_ultimo_pago.Text = Session("rs").fields("FECHA_ULTIMO_PAGO").value.ToString
            End If

            lbl_monto_ultimo_pago.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("MONTO_ULTIMO_PAGO").value.ToString)
            lbl_fecha_prox_pago.Text = Session("rs").fields("FECHA_PROXIMO_PAGO").value.ToString
            lbl_monto_prox_pago.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("MONTO_PROXIMO_PAGO").value.ToString)
            lbl_dias_vencer.Text = Session("rs").fields("DIAS_POR_VENCER").value.ToString
            lbl_capital_vencido.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("CAPITAL_VENCIDO").value.ToString)
            lbl_monto_regularizacion.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("MONTO_REGULARIZACION").value.ToString)

            lbl_monto_liquidar.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("MONTO_LIQUIDAR").value.ToString)
            lbl_abonos_vencidos.Text = Session("rs").fields("ABONOS_VENCIDOS").value.ToString
            lbl_dias_mora.Text = Session("rs").fields("DIAS_MORA").value.ToString
            lbl_num_reestructura.Text = Session("rs").fields("NUMERO_REESTRUCTURA").value.ToString
            lbl_folio_reestructura.Text = Session("rs").fields("FOLIO_REESTRUCTURA").value.ToString
            lbl_folio_cta_eje.Text = Session("rs").fields("FOLIO_CTA_EJE").value.ToString
            lbl_saldo_cta_eje.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("SALDO_CTA_EJE").value.ToString)
            If Session("rs").fields("FECHA_ULTIMO_MOV_CTA_EJE").value.ToString = "" Then
                lbl_fecha_ult_mov_cta_eje.Text = "-"
            Else
                lbl_fecha_ult_mov_cta_eje.Text = Session("rs").fields("FECHA_ULTIMO_MOV_CTA_EJE").value.ToString
            End If

            lbl_saldo_ult_mov_cta_eje.Text = "$ " + Session("MASCOREG").FormatNumberCurr(Session("rs").fields("SALDO_ULTIMO_MOV_CTA_EJE").value.ToString)

        End If
        Session("Con").Close()
    End Sub

    Private Sub Domicilio(ByVal Folio As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_DOMICILIO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            lbl_tipo_persona.Text = Session("rs").fields("TIPO_PERSONA").value.ToString
            lbl_domicilio.Text = Session("rs").fields("DOMICILIO").value.ToString
            lbl_tel_casa.Text = Session("rs").fields("TCASA").value.ToString
            lbl_tel_cel.Text = Session("rs").fields("TCEL").value.ToString


            If (Session("rs").fields("EXT_OFI").value.ToString) = "" Then
                lbl_tel_ofi.Text = Session("rs").fields("TOFI").value.ToString
            Else
                lbl_tel_ofi.Text = Session("rs").fields("TOFI").value.ToString + " Extensión: " + Session("rs").fields("EXT_OFI").value.ToString
                'lbl_ext_ofi.Text = Session("rs").fields("EXT_OFI").value.ToString

            End If
            lbl_destino.Text = Session("rs").fields("DESTINO").value.ToString
            lbl_id_suc.Text = Session("rs").fields("IDSUC").value.ToString

        End If
        Session("Con").Close()
    End Sub

    Private Sub AvalesGtias(ByVal Folio As String, ByVal BANDERA As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("BANDERA", Session("adVarChar"), Session("adParamInput"), 20, BANDERA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_REFAVCOD"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            lbl_tipo_garantias.Text = Session("rs").fields("TIPO_GARANTIAS").value.ToString
            lbl_valor_gtias.Text = Session("rs").fields("VALOR_GARANTIAS").value.ToString
            lbl_descripcion_gtias.Text = Session("rs").fields("DESCRIPCION_GARANTIAS").value.ToString

            lbl_id_persona_aval1.Text = Session("rs").fields("IDPERSONA_AVAL1").value.ToString
            lbl_nombre_aval1.Text = Session("rs").fields("NOMBRE_AVAL1").value.ToString
            lbl_direccion_aval1.Text = Session("rs").fields("DIRECCION_AVAL1").value.ToString

            If Session("rs").fields("TEL_CASA_AVAL1").value.ToString = "" Then
                lbl_tel_Casa_aval1.Text = "-"
            Else
                lbl_tel_Casa_aval1.Text = Session("rs").fields("TEL_CASA_AVAL1").value.ToString
            End If

            If Session("rs").fields("TEL_OFI_AVAL1").value.ToString = "" Then
                lbl_tel_ofi_aval1.Text = "-"
            Else
                lbl_tel_ofi_aval1.Text = Session("rs").fields("TEL_OFI_AVAL1").value.ToString
            End If

            If Session("rs").fields("EXT_OFI_AVAL1").value.ToString = "" Then
                lbl_ext_tel_ofi_aval1.Text = "-"
            Else
                lbl_ext_tel_ofi_aval1.Text = Session("rs").fields("EXT_OFI_AVAL1").value.ToString
            End If
            If Session("rs").fields("TEL_CEL_AVAL1").value.ToString = "" Then
                lbl_tel_movil_aval1.Text = "-"
            Else
                lbl_tel_movil_aval1.Text = Session("rs").fields("TEL_CEL_AVAL1").value.ToString
            End If

            lbl_id_persona_aval2.Text = Session("rs").fields("IDPERSONA_AVAL2").value.ToString
            lbl_nombre_aval2.Text = Session("rs").fields("NOMBRE_AVAL2").value.ToString
            lbl_direccion_aval2.Text = Session("rs").fields("DIRECCION_AVAL2").value.ToString

            If Session("rs").fields("TEL_CASA_AVAL2").value.ToString = "" Then
                lbl_tel_casa_aval2.Text = "-"
            Else
                lbl_tel_casa_aval2.Text = Session("rs").fields("TEL_CASA_AVAL2").value.ToString
            End If
            If Session("rs").fields("TEL_OFI_AVAL2").value.ToString = "" Then
                lbl_tel_ofi_aval2.Text = "-"
            Else
                lbl_tel_ofi_aval2.Text = Session("rs").fields("TEL_OFI_AVAL2").value.ToString
            End If

            If Session("rs").fields("EXT_OFI_AVAL2").value.ToString = "" Then
                lbl_ext_tel_ofi_aval2.Text = "-"
            Else
                lbl_ext_tel_ofi_aval2.Text = Session("rs").fields("EXT_OFI_AVAL2").value.ToString
            End If

            If Session("rs").fields("TEL_CEL_AVAL2").value.ToString = "" Then
                lbl_tel_movil_aval2.Text = "-"
            Else
                lbl_tel_movil_aval2.Text = Session("rs").fields("TEL_CEL_AVAL2").value.ToString

            End If
        End If

        Session("Con").Close()
    End Sub

    Private Sub Referencias(ByVal Folio As String, ByVal BANDERA As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("BANDERA", Session("adVarChar"), Session("adParamInput"), 20, BANDERA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_REFAVCOD"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            lbl_id_persona_ref1.Text = Session("rs").fields("IDPERSONA_REFERENCIA1").value.ToString
            lbl_nombre_ref1.Text = Session("rs").fields("NOMBRE_REFERENCIA1").value.ToString
            lbl_direccion_ref1.Text = Session("rs").fields("DIRECCION_REFERENCIA1").value.ToString

            If Session("rs").fields("TEL_CASA_REFERENCIA1").value.ToString = "" Then
                lbl_tel_casa_ref1.Text = "-"
            Else
                lbl_tel_casa_ref1.Text = Session("rs").fields("TEL_CASA_REFERENCIA1").value.ToString
            End If

            If Session("rs").fields("TEL_OFI_REFERENCIA1").value.ToString = "" Then
                lbl_tel_ofi_ref1.Text = "-"
            Else
                lbl_tel_ofi_ref1.Text = Session("rs").fields("TEL_OFI_REFERENCIA1").value.ToString

            End If

            If Session("rs").fields("EXT_OFI_REFERENCIA1").value.ToString = "" Then
                lbl_ext_ofi_ref1.Text = "-"
            Else
                lbl_ext_ofi_ref1.Text = Session("rs").fields("EXT_OFI_REFERENCIA1").value.ToString
            End If

            If Session("rs").fields("TEL_CEL_REFERENCIA1").value.ToString = "" Then
                lbl_tel_cel_ref1.Text = "-"
            Else
                lbl_tel_cel_ref1.Text = Session("rs").fields("TEL_CEL_REFERENCIA1").value.ToString

            End If

            lbl_id_persona_ref2.Text = Session("rs").fields("IDPERSONA_REFERENCIA2").value.ToString
            lbl_nombre_ref2.Text = Session("rs").fields("NOMBRE_REFERENCIA2").value.ToString
            lbl_direccion_ref2.Text = Session("rs").fields("DIRECCION_REFERENCIA2").value.ToString

            If Session("rs").fields("TEL_CASA_REFERENCIA2").value.ToString = "" Then
                lbl_tel_casa_ref2.Text = "-"
            Else
                lbl_tel_casa_ref2.Text = Session("rs").fields("TEL_CASA_REFERENCIA2").value.ToString
            End If
            If Session("rs").fields("TEL_OFI_REFERENCIA2").value.ToString = "" Then
                lbl_tel_ofi_ref2.Text = "-"
            Else
                lbl_tel_ofi_ref2.Text = Session("rs").fields("TEL_OFI_REFERENCIA2").value.ToString
            End If
            If Session("rs").fields("EXT_OFI_REFERENCIA2").value.ToString = "" Then
                lbl_ext_ofi_ref2.Text = "-"
            Else
                lbl_ext_ofi_ref2.Text = Session("rs").fields("EXT_OFI_REFERENCIA2").value.ToString
            End If

            If Session("rs").fields("TEL_CEL_REFERENCIA2").value.ToString = "" Then
                lbl_tel_cel_ref2.Text = "-"
            Else
                lbl_tel_cel_ref2.Text = Session("rs").fields("TEL_CEL_REFERENCIA2").value.ToString

            End If

            lbl_id_persona_ref3.Text = Session("rs").fields("IDPERSONA_REFERENCIA3").value.ToString
            lbl_nombre_ref3.Text = Session("rs").fields("NOMBRE_REFERENCIA3").value.ToString
            lbl_direccion_ref3.Text = Session("rs").fields("DIRECCION_REFERENCIA3").value.ToString

            If Session("rs").fields("TEL_CASA_REFERENCIA3").value.ToString = "" Then
                lbl_tel_Casa_ref3.Text = "-"
            Else
                lbl_tel_Casa_ref3.Text = Session("rs").fields("TEL_CASA_REFERENCIA3").value.ToString
            End If

            If Session("rs").fields("TEL_OFI_REFERENCIA3").value.ToString = "" Then
                lbl_tel_ofi_ref3.Text = "-"
            Else
                lbl_tel_ofi_ref3.Text = Session("rs").fields("TEL_OFI_REFERENCIA3").value.ToString
            End If

            If Session("rs").fields("EXT_OFI_REFERENCIA3").value.ToString = "" Then
                lbl_ext_ofi_ref3.Text = "-"
            Else
                lbl_ext_ofi_ref3.Text = Session("rs").fields("EXT_OFI_REFERENCIA3").value.ToString
            End If

            If Session("rs").fields("TEL_CEL_REFERENCIA3").value.ToString = "" Then
                lbl_tel_cel_ref3.Text = "-"
            Else
                lbl_tel_cel_ref3.Text = Session("rs").fields("TEL_CEL_REFERENCIA3").value.ToString

            End If

        End If
        Session("Con").Close()
    End Sub

    Private Sub Abogados(ByVal Folio As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_ABOGADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            lbl_estatus_juicio.Text = Session("rs").fields("ESTATUS").value.ToString
            lbl_fecha_diligencia.Text = Session("rs").fields("FECHA").value.ToString
            lbl_juzgado.Text = Session("rs").fields("JUZGADO").value.ToString
            lbl_exhorto.Text = Session("rs").fields("EXHORTO").value.ToString
            lbl_juzgado_exhortado.Text = Session("rs").fields("JUICIO_EXH").value.ToString
            lbl_fecha_prom_pago.Text = Session("rs").fields("PROMPAGO").value.ToString
            lbl_monto_prom_pago.Text = Session("rs").fields("MONTOPROMPAGO").value.ToString
            txt_ddetalle.Text = Session("rs").fields("DETALLE").value.ToString
            lbl_gestor.Text = Session("rs").fields("GESTOR").value.ToString
            lbl_cita.Text = Session("rs").fields("CITA").value.ToString
            lbl_f_emp_aval.Text = Session("rs").fields("FECHAEMPAVAL").value.ToString
            lbl_f_emp_tit.Text = Session("rs").fields("FECHAEMPTIT").value.ToString
        Else
            lbl_estatus_juicio.Text = "-"
            lbl_fecha_diligencia.Text = "-"
            lbl_juzgado.Text = "-"
            lbl_exhorto.Text = "-"
            lbl_juzgado_exhortado.Text = "-"
            lbl_fecha_prom_pago.Text = "-"
            lbl_monto_prom_pago.Text = "-"
            lbl_cita.Text = "-"
            lbl_gestor.Text = "-"
            txt_ddetalle.Text = ""
            lbl_f_emp_aval.Text = "-"
            lbl_f_emp_tit.Text = "-"
        End If
        Session("Con").Close()
    End Sub

    'Bitácora
#Region "bitacora"
    '------------------ LLENA COMBOS-----------------------------------------------

    Private Sub Llenaeventos()
        cmb_evento.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")

        cmb_evento.Items.Add(elija)
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("BANDERA", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_EVENTOS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString + "-" + Session("rs").Fields("CLAVE").Value.ToString)
            cmb_evento.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub Llenaresultado(ByVal EVENTO As String)

        cmb_resultado.Items.Clear()
        cmb_resultado_atencion.Items.Clear()
        cmb_resultado_aviso.Items.Clear()
        cmb_resultado_citatorio.Items.Clear()
        cmb_estatus_juicio.Items.Clear()
        cmb_estatus_cob.Items.Clear()
        cmb_resultado_visita.Items.Clear()


        Dim elija As New ListItem("ELIJA", "0")
        cmb_resultado.Items.Add(elija)
        cmb_resultado_atencion.Items.Add(elija)
        cmb_resultado_aviso.Items.Add(elija)
        cmb_resultado_citatorio.Items.Add(elija)
        cmb_estatus_juicio.Items.Add(elija)
        cmb_estatus_cob.Items.Add(elija)
        cmb_resultado_visita.Items.Add(elija)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 20, EVENTO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_RESULTADOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("ID").Value.ToString + "-" + Session("rs").Fields("CLAVE").Value.ToString)
            cmb_resultado.Items.Add(item)
            cmb_resultado_atencion.Items.Add(item)
            cmb_resultado_aviso.Items.Add(item)
            cmb_resultado_citatorio.Items.Add(item)
            cmb_estatus_juicio.Items.Add(item)
            cmb_estatus_cob.Items.Add(item)
            cmb_resultado_visita.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'TIPO RELACION
    Private Sub Llenatiporelacion()

        cmb_tiporel.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_tiporel.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_tiporel.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    'TIPO TELEFONO
    Private Sub Llenatipotelefono()

        cmb_tipo_tel.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_tipo_tel.Items.Add(elija)

        Dim casa As New ListItem("PARTICULAR", "PAR")
        cmb_tipo_tel.Items.Add(casa)
        Dim cel As New ListItem("MOVIL", "MOV")
        cmb_tipo_tel.Items.Add(cel)
        Dim trabajo As New ListItem("TRABAJO", "TRA")
        cmb_tipo_tel.Items.Add(trabajo)

    End Sub

    'EVENTO AL HACER UNA CITA
    Private Sub Llenaeventocita()

        cmb_evento_cita.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_evento_cita.Items.Add(elija)

        Dim casa As New ListItem("AGENDAR", "AG")
        cmb_evento_cita.Items.Add(casa)
        Dim cel As New ListItem("SEGUIMIENTO", "SEG")
        cmb_evento_cita.Items.Add(cel)

    End Sub

    'CITAS AGENDADAS
    Private Sub cita()
        cmb_cita_seguimiento.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_cita_seguimiento.Items.Add(elija)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_CITAS_AGENDADAS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("FECHA").Value.ToString + " (" + Session("rs").Fields("SUCURSAL").Value.ToString + ") ", Session("rs").Fields("NUM").Value.ToString)
            cmb_cita_seguimiento.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'DESTINATARIOS
    Private Sub LlenaDestinatarios()
        cmb_destinatario.Items.Clear()
        cmb_destinatario_llamada.Items.Clear()
        cmb_destinatario_visita.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_destinatario.Items.Add(elija)
        cmb_destinatario_llamada.Items.Add(elija)
        cmb_destinatario_visita.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESTINATARIOS_FOLIO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            If Session("rs").Fields("CLAVE").Value.ToString <> "REF" And Session("rs").Fields("CLAVE").Value.ToString <> "CON" Then
                Dim item As New ListItem(Session("rs").Fields("NOMBREDESTINO").Value.ToString, Session("rs").Fields("IDDESTINO").Value.ToString + "-" + Session("rs").Fields("CLAVE").Value.ToString)
                cmb_destinatario.Items.Add(item)
                cmb_destinatario_visita.Items.Add(item)
            End If
            Dim item2 As New ListItem(Session("rs").Fields("NOMBREDESTINO").Value.ToString, Session("rs").Fields("IDDESTINO").Value.ToString + "-" + Session("rs").Fields("CLAVE").Value.ToString)
            cmb_destinatario_llamada.Items.Add(item2)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'NOMBRE DESTINATARIOS
    Private Sub LlenaNombreDestinatarios(ByVal destinatario As String)

        cmb_persona_cita.Items.Clear()
        cmb_nombre_destinatario.Items.Clear()
        cmb_persona_visita.Items.Clear()


        Dim elija As New ListItem("ELIJA", "0")
        cmb_persona_cita.Items.Add(elija)
        cmb_nombre_destinatario.Items.Add(elija)
        cmb_persona_visita.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESTINATARIO", Session("adVarChar"), Session("adParamInput"), 20, destinatario)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_NOMBRE_DESTINATARIOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_persona_cita.Items.Add(item)
            cmb_nombre_destinatario.Items.Add(item)
            cmb_persona_visita.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'USUARIOS
    Private Sub LlenaUsuarios()
        cmb_usuario_atendio.Items.Clear()
        cmb_usuario_seg_visita.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_usuario_atendio.Items.Add(elija)
        cmb_usuario_seg_visita.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_usuario_atendio.Items.Add(item)
            cmb_usuario_seg_visita.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    '-------------------------------VALIDACIONES-----------------------
    Private Function date_val(ByVal fecha As String) As Boolean
        Dim correcto As Boolean = True

        If fecha <> "" Then
            If DateDiff(DateInterval.Year, CDate(fecha), Now()) > 150 Then
                correcto = False
            End If
        End If
        Return correcto
    End Function

    Private Function horas(ByVal hora As String) As Boolean
        Return Regex.IsMatch(hora, "(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)")
    End Function

    Private Sub DatosExpediente()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INFO_X_FOLIO"
        Session("rs") = Session("cmd").Execute()

        Session("TIPO_PROD") = Session("rs").Fields("TIPO_PROD").Value.ToString
        Session("Con").Close()

        If Session("TIPO_PROD").ToString = "1" Then
            pnl_cobranza.Visible = True

        Else
            pnl_cobranza.Visible = False
        End If

    End Sub

    Protected Sub txt_cliente_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cliente.TextChanged
        pnl_cobranza.Visible = False
        lbl_status.Text = ""

    End Sub

    Protected Sub cmb_evento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_evento.SelectedIndexChanged
        lbl_status.Text = ""

        If cmb_evento.SelectedIndex <> "0" Then

            Dim CLAVE
            CLAVE = Split(cmb_evento.SelectedItem.Value.ToString, "-")

            Select Case CLAVE(1)
                Case "LLAM"
                    LIMPIALLAMADAS()
                    Llenatiporelacion()
                    Llenatipotelefono()
                    LlenaDestinatarios()
                    Llenaresultado(CLAVE(0))

                    lbl_realizo.Text = Session("NombreUsr")
                    pnl_llamada.Visible = True
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False
                    pnl_evento_cita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_evento_visita.Visible = False

                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                Case "CITA"
                    limpiacitas()
                    Llenaeventocita()
                    pnl_evento_cita.Visible = True
                    pnl_llamada.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_evento_visita.Visible = False

                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                Case "AVIS"
                    limpiaavisos()
                    avisos(CLAVE(0))
                    pnl_aviso.Visible = True
                    lnk_guardar_aviso.Visible = True
                    lnk_guardar_cancelar.Visible = True
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False
                    pnl_llamada.Visible = False
                    pnl_evento_cita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_evento_visita.Visible = False

                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                Case "CTRO"
                    LIMPIACITATORIO()
                    citatorios(CLAVE(0))
                    pnl_citatorio.Visible = True
                    lnk_agregar_Citatorio.Visible = True
                    lnk_cancelar_citatorio.Visible = True
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False
                    pnl_llamada.Visible = False
                    pnl_evento_cita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_evento_visita.Visible = False

                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                Case "JUIC"
                    limpiaRegistroJuicio()
                    Llenaresultado(CLAVE(0))
                    pnl_reg_juicio.Visible = True
                    lbl_user_despacho.Text = Session("NombreUsr")
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False
                    pnl_llamada.Visible = False
                    pnl_evento_cita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_evento_visita.Visible = False

                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                Case "COB"
                    limpiaEstatusCobranza()
                    Llenaresultado(CLAVE(0))
                    pnl_estatus.Visible = True
                    lbl_user_estatus.Text = Session("NombreUsr")
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False
                    pnl_llamada.Visible = False
                    pnl_evento_cita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_evento_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                Case "VISI"
                    limpiavisita()
                    Llenaeventovisita()
                    pnl_evento_visita.Visible = True
                    pnl_llamada.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False

                Case Else
                    pnl_seg.Visible = False
                    pnl_seguimiento.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_llamada.Visible = False
                    pnl_evento_cita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_aviso.Visible = False
                    pnl_seguimiento_aviso.Visible = False
                    pnl_citatorio.Visible = False
                    pnl_seguimiento_citatorio.Visible = False
                    pnl_reg_juicio.Visible = False
                    pnl_estatus.Visible = False
                    pnl_evento_visita.Visible = False

            End Select
        End If
    End Sub

    Protected Sub cmb_tipo_tel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipo_tel.SelectedIndexChanged
        Select Case cmb_tipo_tel.SelectedItem.Value.ToString
            Case "PAR"
                lbl_extofi.Visible = False
                txt_ext.Visible = False
            Case "MOV"
                lbl_extofi.Visible = False
                txt_ext.Visible = False
            Case "TRA"
                lbl_extofi.Visible = True
                txt_ext.Visible = True
            Case Else
                lbl_extofi.Visible = False
                txt_ext.Visible = False
        End Select
    End Sub

    '-------------------------------------LLAMADAS-------------------------------------------

    Private Sub EventoLlamada()

        Dim resultado
        resultado = Split(cmb_resultado.SelectedItem.Value.ToString, "-")

        Dim evento
        evento = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        Dim Iddestinatario
        Iddestinatario = Split(cmb_destinatario_llamada.SelectedItem.Value.ToString, "-")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 20, evento(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESTINATARIO", Session("adVarChar"), Session("adParamInput"), 20, Iddestinatario(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, cmb_nombre_destinatario.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAEJECUCION", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaejecucion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DETALLE", Session("adVarChar"), Session("adParamInput"), 3000, txt_detacc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIEMPO", Session("adVarChar"), Session("adParamInput"), 5, txt_tiempo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RESULTADO", Session("adVarChar"), Session("adParamInput"), 15, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RELACION", Session("adVarChar"), Session("adParamInput"), 15, cmb_tiporel.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_paterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_materno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombres.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre2.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 15, cmb_tipo_tel.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 6, txt_lada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TEL", Session("adVarChar"), Session("adParamInput"), 15, txt_tel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EXT", Session("adVarChar"), Session("adParamInput"), 3, txt_ext.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMPAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_prom_pago_llamada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOPROMPAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto_prom_pago_llamada.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_LLAMADA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Protected Sub lnk_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_guardar.Click

        lbl_Info.Text = ""
        lbl_status.Text = ""

        If txt_prom_pago_llamada.Text <> "" Then
            If (CDate(txt_prom_pago_llamada.Text) < CDate(Session("FechaSis"))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago anterior a la fecha de sistema"
                Exit Sub
            End If
        End If

        If date_val(txt_fechaejecucion.Text) = True Then

            If (txt_prom_pago_llamada.Text = "" And txt_monto_prom_pago_llamada.Text <> "") Or (txt_prom_pago_llamada.Text <> "" And txt_monto_prom_pago_llamada.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago_llamada.Text) = True Then

                    If Validamonto(txt_monto_prom_pago_llamada.Text) = True Then
                        EventoLlamada()
                        lbl_status.Text = "Se ha guardado correctamente el registro"
                        LIMPIAREGISTRO()
                        LIMPIALLAMADAS()
                    Else
                        lbl_status.Text = "Error: Monto incorrecto"
                    End If

                Else
                    lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
                End If

            End If

        Else
            lbl_status.Text = "Error: Fecha inválida"
        End If

    End Sub

    Protected Sub cmb_resultado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_resultado.SelectedIndexChanged
        Dim resultado
        resultado = Split(cmb_resultado.SelectedItem.Value.ToString, "-")

        If cmb_resultado.SelectedIndex <> "0" Then
            Select Case resultado(1)
                Case "LOCCLIE"
                    pnl_personas.Visible = False
                Case "NOLOCCLIE"
                    pnl_personas.Visible = True
                Case Else
                    pnl_personas.Visible = False
            End Select
        End If

    End Sub

    '-------------------------------------LIMPIA CONTROLES------------------------------------
    Private Sub limpiapanels()
        pnl_cobranza.Visible = False
        pnl_agendar.Visible = False
        pnl_evento_cita.Visible = False
        pnl_personas.Visible = False
        pnl_llamada.Visible = False
        pnl_seg.Visible = False
        pnl_seguimiento.Visible = False
        pnl_estatus.Visible = False
        pnl_agendar_visita.Visible = False
        pnl_evento_visita.Visible = False
        pnl_citatorio.Visible = False

    End Sub

    Private Sub LIMPIAREGISTRO()
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"

    End Sub

    Private Sub LIMPIALLAMADAS()
        lbl_realizo.Text = ""
        txt_fechaejecucion.Text = ""
        cmb_tipo_tel.Items.Clear()
        txt_lada.Text = ""
        txt_tel.Text = ""
        txt_ext.Text = ""
        txt_detacc.Text = ""
        txt_tiempo.Text = ""
        cmb_resultado.Items.Clear()
        cmb_destinatario_llamada.Items.Clear()
        cmb_nombre_destinatario.Items.Clear()
        cmb_tiporel.Items.Clear()
        txt_paterno.Text = ""
        txt_materno.Text = ""
        txt_nombre2.Text = ""
        txt_nombres.Text = ""
        pnl_llamada.Visible = False
        txt_monto_prom_pago_llamada.Text = ""
        txt_prom_pago_llamada.Text = ""

    End Sub

    Private Sub limpiacitas()
        cmb_evento_cita.Items.Clear()
        lbl_c_usuario.Text = ""
        cmb_sucursal.Items.Clear()
        txt_cita_fecha.Text = ""
        txt_hora.Text = ""
        txt_cita_notas.Text = ""
        pnl_evento_cita.Visible = False
        pnl_agendar.Visible = False
    End Sub

    Private Sub limpiaseguimiento()
        txt_fecha_atencion.Text = ""
        txt_hora_atencion.Text = ""
        txt_duracion_atencion.Text = ""
        cmb_resultado_atencion.Items.Clear()
        cmb_usuario_atendio.Items.Clear()
        cmb_sucursal_atendio.Items.Clear()
        txt_motivo_atencion.Text = ""
        txt_monto_prom_pago_cita.Text = ""
        txt_prom_pago_cita.Text = ""
    End Sub


    Private Sub limpiaRegistroJuicio()

        txt_detalle.Text = ""
        txt_gestor.Text = ""
        cmb_estatus_juicio.Items.Clear()
        txt_cita.Text = ""
        txt_juzgado.Text = ""
        txt_juzgado_exhorto.Text = ""
        txt_exhorto.Text = ""
        txt_prom_pago.Text = ""
        txt_monto_prom_pago.Text = ""
        txt_fecha_ingreso.Text = ""
        lbl_user_despacho.Text = ""
        pnl_reg_juicio.Visible = False
        txt_fecha_emp_Aval.Text = ""
        txt_fecha_emp_tit.Text = ""
    End Sub

    '----------------------------------REGISTRO Y SEGUIMIENTO DE CITAS-----------------

    Protected Sub cmb_evento_cita_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_evento_cita.SelectedIndexChanged

        Select Case cmb_evento_cita.SelectedItem.Value

            Case "AG"
                pnl_seg.Visible = False
                pnl_seguimiento.Visible = False
                pnl_agendar.Visible = True
                txt_cita_fecha.Text = ""
                txt_hora.Text = ""
                txt_cita_notas.Text = ""
                LlenaSucursales()
                LlenaDestinatarios()
                cmb_persona_cita.Items.Clear()
                lbl_c_usuario.Text = Session("NombreUsr")
            Case "SEG"
                pnl_agendar.Visible = False
                pnl_seg.Visible = True
                cita()
            Case Else
                pnl_agendar.Visible = False
                pnl_seg.Visible = False
        End Select

    End Sub

    Protected Sub lnk_guardar_cita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_guardar_cita.Click
        lbl_Info.Text = ""
        lbl_status.Text = ""

        If CDate(txt_cita_fecha.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha de cita es anterior a la fecha de sistema"
            Exit Sub
        End If

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_cita_fecha.Text) = True And horas(txt_hora.Text) = True Then
            Eventocita()
            lbl_status.Text = "Se ha registrado correctamente la cita"
            LIMPIAREGISTRO()
            limpiacitas()
            pnl_evento_cita.Visible = False
            ' pnl_cobranza.Visible = False
            pnl_seg.Visible = False
            pnl_seguimiento.Visible = False
        Else
            lbl_status.Text = "Error: Verifique formato de Fecha y hora"
        End If
    End Sub

    Private Sub Eventocita()

        Dim evento
        evento = Split(cmb_evento.SelectedItem.Value.ToString, "-")


        Dim Iddestinatario
        Iddestinatario = Split(cmb_destinatario.SelectedItem.Value.ToString, "-")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 20, evento(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESTINATARIO", Session("adVarChar"), Session("adParamInput"), 20, Iddestinatario(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, cmb_persona_cita.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHACITA", Session("adVarChar"), Session("adParamInput"), 10, txt_cita_fecha.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 3000, txt_cita_notas.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORAS", Session("adVarChar"), Session("adParamInput"), 5, txt_hora.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, cmb_sucursal.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_CITA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub Eventocita_Seguimiento()

        Dim resultado
        resultado = Split(cmb_resultado_atencion.SelectedItem.Value.ToString, "-")
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCITA", Session("adVarChar"), Session("adParamInput"), 20, cmb_cita_seguimiento.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, cmb_sucursal_atendio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID_A", Session("adVarChar"), Session("adParamInput"), 20, cmb_usuario_atendio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHACITA", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_atencion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORA", Session("adVarChar"), Session("adParamInput"), 15, txt_hora_atencion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIEMPO", Session("adVarChar"), Session("adParamInput"), 15, txt_duracion_atencion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDRESULTADO", Session("adVarChar"), Session("adParamInput"), 15, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOTIVO", Session("adVarChar"), Session("adParamInput"), 3000, txt_motivo_atencion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_prom_pago_cita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOPROMPAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto_prom_pago_cita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_LOG_CITA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Protected Sub cmb_cita_seguimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_cita_seguimiento.SelectedIndexChanged
        If cmb_cita_seguimiento.SelectedIndex = "0" Then
            lbl_status.Text = "Error: Seleccione una cita para dar seguimiento"
        Else
            lbl_status.Text = ""
            Dim CLAVE
            CLAVE = Split(cmb_evento.SelectedItem.Value.ToString, "-")
            pnl_seguimiento.Visible = True
            Llenaresultado(CLAVE(0))
            LlenaSucursales()
            LlenaUsuarios()
            LlenaDestinatarios()
        End If
    End Sub

    Protected Sub lnk_agregar_seguimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_agregar_seguimiento.Click
        lbl_Info.Text = ""
        lbl_status.Text = ""

        If CDate(txt_fecha_atencion.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha anterior a la fecha de sistema"
            Exit Sub
        End If

        If txt_prom_pago_cita.Text <> "" Then
            If (txt_prom_pago_cita.Text <> "" And (CDate(txt_prom_pago_cita.Text) < CDate(Session("FechaSis")))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
                Exit Sub
            End If

        End If

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_fecha_atencion.Text) = True And horas(txt_hora_atencion.Text) = True Then


            If (txt_prom_pago_cita.Text = "" And txt_monto_prom_pago_cita.Text <> "") Or (txt_prom_pago_cita.Text <> "" And txt_monto_prom_pago_cita.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago_cita.Text) = True Then

                    If Validamonto(txt_monto_prom_pago_cita.Text) = True Then
                        Eventocita_Seguimiento()
                        lbl_status.Text = "Se ha guardado correctamente el seguimiento"
                        LIMPIAREGISTRO()
                        limpiaseguimiento()
                        pnl_seguimiento.Visible = False
                        pnl_evento_cita.Visible = False
                        pnl_seg.Visible = False

                    Else
                        lbl_status.Text = "Error: Monto incorrecto"
                    End If

                Else
                    lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
                End If

            End If

        Else
            lbl_status.Text = "Error: Verifique el formato de fecha y hora"
        End If

    End Sub

    Protected Sub cmb_destinatario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_destinatario.SelectedIndexChanged
        If cmb_destinatario.SelectedIndex <> "0" Then

            Dim CLAVE
            CLAVE = Split(cmb_destinatario.SelectedItem.Value.ToString, "-")
            LlenaNombreDestinatarios(CLAVE(0))
            If CLAVE(1) = "CLI" Then
                LBL_nombre_destinatario.Visible = False
                cmb_persona_cita.Visible = False
                cmb_persona_cita.Items.RemoveAt(0)
                cmb_persona_cita.Items.FindByValue(txt_cliente.Text).Selected = True

            Else
                LBL_nombre_destinatario.Visible = True
                cmb_persona_cita.Visible = True
            End If

        End If
    End Sub

    Protected Sub cmb_destinatario_llamada_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_destinatario_llamada.SelectedIndexChanged

        If cmb_destinatario_llamada.SelectedIndex <> "0" Then

            Dim CLAVE
            CLAVE = Split(cmb_destinatario_llamada.SelectedItem.Value.ToString, "-")

            LlenaNombreDestinatarios(CLAVE(0))

            If CLAVE(1) = "CLI" Then
                lbl_nombre_dest_llamada.Visible = False
                cmb_nombre_destinatario.Visible = False
                cmb_nombre_destinatario.Items.RemoveAt(0)
                cmb_nombre_destinatario.Items.FindByValue(txt_cliente.Text).Selected = True
            Else
                lbl_nombre_dest_llamada.Visible = True
                cmb_nombre_destinatario.Visible = True
            End If
        End If

    End Sub


    Protected Sub dag_aviso_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_aviso.ItemDataBound

        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False

    End Sub

    Private Sub avisos(ByVal EVENTO As String)


        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtavisos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDEVENTO", Session("adVarChar"), Session("adParamInput"), 15, EVENTO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_AVISOS_FOLIO"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtavisos, Session("rs"))
        If dtavisos.Rows.Count > 0 Then
            lbl_avisos_gen.Visible = True
            dag_aviso.Visible = True
            dag_aviso.DataSource = dtavisos
            dag_aviso.DataBind()
        Else
            lbl_avisos_gen.Visible = False
            dag_aviso.Visible = False
            lbl_status.Text = "No existen avisos generados para este evento"
        End If

        Session("Con").Close()
    End Sub

    Private Sub DAG_avisos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_aviso.ItemCommand

        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        Session("IDAVISO") = e.Item.Cells(0).Text
        Session("DESTINO_AVISO") = e.Item.Cells(3).Text
        Session("NOMBRE_DESTINO_AVISO") = e.Item.Cells(4).Text


        If (e.CommandName = "SEGUIMIENTO") Then
            pnl_cobranza.Visible = True
            pnl_seguimiento_aviso.Visible = True
            pnl_aviso.Visible = False
            lbl_nombre_usuario_aviso.Text = Session("NombreUsr")
            lbl_nombre_destinatario_aviso.Text = Session("NOMBRE_DESTINO_AVISO")
            lbl_nombre_destino_aviso.Text = Session("DESTINO_AVISO")
            Llenaresultado(EVENTO(0))
        End If

    End Sub


    Protected Sub lnk_guardar_aviso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_guardar_aviso.Click
        lbl_Info.Text = ""
        lbl_status.Text = ""


        If CDate(txt_fecha_aviso.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha anterior a la fecha de sistema"
            Exit Sub
        End If


        If txt_prom_pago_aviso.Text <> "" Then
            If (txt_prom_pago_aviso.Text <> "" And (CDate(txt_prom_pago_aviso.Text) < CDate(Session("FechaSis")))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
                Exit Sub
            End If

        End If

        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_fecha_aviso.Text) = True Then

            If (txt_prom_pago_aviso.Text = "" And txt_monto_prom_pago_aviso.Text <> "") Or (txt_prom_pago_aviso.Text <> "" And txt_monto_prom_pago_aviso.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago_aviso.Text) = True Then

                    If Validamonto(txt_monto_prom_pago_aviso.Text) = True Then
                        Eventoaviso_Seguimiento(Session("IDAVISO"))
                        Session("CVECOB") = "AVIS"
                        Session("IDEVENTO") = Session("IDAVISO")
                        lnk_guardar_aviso.Visible = False
                        lnk_guardar_cancelar.Visible = False
                        pnl_digitalizar_aviso.Visible = True
                    Else
                        lbl_status.Text = "Error: Monto incorrecto"
                    End If

                Else
                    lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
                End If

            End If

        Else
            lbl_status.Text = "Error: Verifique el formato de fecha y hora"
        End If



    End Sub

    Private Sub Eventoaviso_Seguimiento(ByVal IDAVISO As String)

        Dim resultado
        resultado = Split(cmb_resultado_aviso.SelectedItem.Value.ToString, "-")
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDAVISO", Session("adVarChar"), Session("adParamInput"), 20, IDAVISO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAAVISO", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_aviso.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDRESULTADO", Session("adVarChar"), Session("adParamInput"), 15, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOTIVO", Session("adVarChar"), Session("adParamInput"), 3000, txt_notas_aviso.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_prom_pago_aviso.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOPROMPAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto_prom_pago_aviso.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_LOG_AVISO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub limpiaavisos()
        txt_notas_aviso.Text = ""
        cmb_resultado_aviso.Items.Clear()
        txt_fecha_aviso.Text = ""
        lbl_nombre_usuario_aviso.Text = ""
        lbl_nombre_destinatario_aviso.Text = ""
        lbl_nombre_destino_aviso.Text = ""
        txt_prom_pago_aviso.Text = ""
        txt_monto_prom_pago.Text = ""

    End Sub

    Protected Sub lnk_guardar_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_guardar_cancelar.Click

        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        limpiaavisos()
        pnl_seguimiento_aviso.Visible = False
        pnl_aviso.Visible = True
        avisos(EVENTO(0))
    End Sub
    Private Sub LIMPIAVARIABLESAVISO()

        Session("IDAVISO") = Nothing
        Session("DESTINO_AVISO") = Nothing
        Session("NOMBRE_DESTINO_AVISO") = Nothing
    End Sub

    '---------------------------------CITATORIOS-----------------------------
    Protected Sub dag_citatorios_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_citatorios.ItemDataBound

        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False

    End Sub

    Private Sub citatorios(ByVal EVENTO As String)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcitatorios As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDEVENTO", Session("adVarChar"), Session("adParamInput"), 15, EVENTO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CITATORIOS_FOLIO"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtcitatorios, Session("rs"))
        If dtcitatorios.Rows.Count > 0 Then
            lbl_citatorios_sub.Visible = True
            dag_citatorios.Visible = True
            dag_citatorios.DataSource = dtcitatorios
            dag_citatorios.DataBind()
        Else
            lbl_citatorios_sub.Visible = False
            dag_citatorios.Visible = False
            lbl_status.Text = "No existen citatorios generados para este evento"
        End If

        Session("Con").Close()

    End Sub

    Private Sub DAG_citatorios_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_citatorios.ItemCommand

        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        Session("IDCITATORIO") = e.Item.Cells(0).Text
        Session("DESTINO_CITATORIO") = e.Item.Cells(3).Text
        Session("NOMBRE_DESTINO_CITATORIO") = e.Item.Cells(4).Text

        If (e.CommandName = "SEGUIMIENTO") Then
            pnl_cobranza.Visible = True
            pnl_seguimiento_citatorio.Visible = True
            pnl_citatorio.Visible = False
            lbl_usuario_citatorio.Text = Session("NombreUsr")
            lbl_nombre_destinatario_cit.Text = Session("NOMBRE_DESTINO_CITATORIO")
            lbl_nombre_destino_cit.Text = Session("DESTINO_CITATORIO")
            Llenaresultado(EVENTO(0))
        End If
    End Sub

    Protected Sub lnk_guardar_citatorio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_agregar_Citatorio.Click
        lbl_Info.Text = ""
        lbl_status.Text = ""

        If CDate(txt_fecha_citatorio.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha anterior a la fecha de sistema"
            Exit Sub
        End If

        If txt_prom_pago_citatorio.Text <> "" Then
            If (txt_prom_pago_citatorio.Text <> "" And (CDate(txt_prom_pago_citatorio.Text) < CDate(Session("FechaSis")))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
                Exit Sub
            End If

        End If

        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_fecha_citatorio.Text) = True Then

            If (txt_prom_pago_citatorio.Text = "" And txt_monto_pago_citatorio.Text <> "") Or (txt_prom_pago_citatorio.Text <> "" And txt_monto_pago_citatorio.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago_citatorio.Text) = True Then

                    If Validamonto(txt_monto_pago_citatorio.Text) = True Then
                        Eventocitatorio_Seguimiento(Session("IDCITATORIO"))
                        Session("CVECOB") = "CTRO"
                        Session("IDEVENTO") = Session("IDCITATORIO")
                        lnk_agregar_Citatorio.Visible = False
                        lnk_cancelar_citatorio.Visible = False
                        pnl_digitalizar_citatorio.Visible = True
                    Else
                        lbl_status.Text = "Error: Monto incorrecto"
                    End If

                Else
                    lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
                End If

            End If

        Else
            lbl_status.Text = "Error: Verifique el formato de fecha y hora"
        End If
    End Sub

    Private Sub Eventocitatorio_Seguimiento(ByVal IDCITATORIO As String)

        Dim resultado
        resultado = Split(cmb_resultado_citatorio.SelectedItem.Value.ToString, "-")
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCITATORIO", Session("adVarChar"), Session("adParamInput"), 20, IDCITATORIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHACITATORIO", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_citatorio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDRESULTADO", Session("adVarChar"), Session("adParamInput"), 15, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOTIVO", Session("adVarChar"), Session("adParamInput"), 3000, txt_notas_citatorio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_prom_pago_citatorio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOPROMPAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto_pago_citatorio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_LOG_CITATORIO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub LIMPIACITATORIO()
        cmb_resultado_citatorio.Items.Clear()
        txt_fecha_citatorio.Text = ""
        txt_notas_citatorio.Text = ""
        lbl_nombre_destinatario_cit.Text = ""
        lbl_nombre_destino_cit.Text = ""
        lbl_usuario_citatorio.Text = ""
        txt_prom_pago_citatorio.Text = ""
        txt_monto_pago_citatorio.Text = ""

    End Sub

    Private Sub LIMPIAVARIABLESCITATORIOS()

        Session("IDCITATORIO") = Nothing
        Session("NOMBRE_DESTINO_CITATORIO") = Nothing
        Session("DESTINO_CITATORIO") = Nothing

    End Sub


    Protected Sub lnk_cancelar_citatorio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_cancelar_citatorio.Click
        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        LIMPIACITATORIO()
        LIMPIAVARIABLESCITATORIOS()
        pnl_seguimiento_citatorio.Visible = False
        pnl_citatorio.Visible = True
        citatorios(EVENTO(0))
    End Sub

    Protected Sub lnk_cancelar_llamda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_cancelar_llamda.Click
        LIMPIALLAMADAS()
        pnl_llamada.Visible = False
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"
    End Sub

    Protected Sub lnk_cancelar_cita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_cancelar_cita.Click
        limpiacitas()
        pnl_agendar.Visible = False
        pnl_evento_cita.Visible = True
        cmb_evento_cita.SelectedIndex = "0"
    End Sub

    Protected Sub lnk_cancelar_cita_seg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_cancelar_cita_seg.Click
        limpiaseguimiento()
        pnl_seguimiento.Visible = False
        pnl_seg.Visible = False
        pnl_evento_cita.Visible = True
        cmb_evento_cita.SelectedIndex = "0"
    End Sub

    Protected Sub lnk_no_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_no.Click

        pnl_cobranza.Visible = True
        ' cmb_folio.Items.Clear()
        ' txt_cliente.Text = ""
        LIMPIACITATORIO()
        LIMPIAVARIABLESCITATORIOS()
        pnl_citatorio.Visible = False
        pnl_seguimiento_citatorio.Visible = False
        pnl_digitalizar_citatorio.Visible = False

    End Sub

    Protected Sub lnk_si_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_si.Click
        Session("VENGODE") = "COB_LOG.aspx"
        Response.Redirect("DigitalizadorGlobal.aspx")
    End Sub

    Protected Sub lnk_no_aviso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_no_aviso.Click
        pnl_cobranza.Visible = True
        '  cmb_folio.Items.Clear()
        ' txt_cliente.Text = ""
        limpiaavisos()
        LIMPIAVARIABLESAVISO()
        pnl_aviso.Visible = False
        pnl_seguimiento_aviso.Visible = False
        pnl_digitalizar_aviso.Visible = False


    End Sub

    Protected Sub lnl_si_aviso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnl_si_aviso.Click
        Session("VENGODE") = "COB_LOG.aspx"
        Response.Redirect("DigitalizadorGlobal.aspx")
    End Sub

    Private Function Validamonto(ByVal monto As String) As Boolean

        Dim Valor As Boolean = True

        If monto <> "" Then
            Valor = Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))

        End If

        Return Valor
    End Function

    Protected Sub lnk_agregar_reg_juicio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_agregar_reg_juicio.Click

        lbl_Info.Text = ""
        lbl_status.Text = ""


        If txt_detalle.Text.Length > 8000 Then
            lbl_status.Text = "Error: Sólo 8000 caracteres o menos en el detalle."
            Exit Sub
        End If

        If txt_cita.Text <> "" Then
            If CDate(txt_cita.Text) < CDate(Session("FechaSis")) Then
                lbl_status.Text = "Error: Fecha de cita anterior a la fecha de sistema"
                Exit Sub
            End If
        End If

        If txt_prom_pago.Text <> "" Then
            If (txt_prom_pago.Text <> "" And (CDate(txt_prom_pago.Text) < CDate(Session("FechaSis")))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
                Exit Sub
            End If

        End If

        If date_val(txt_fecha_ingreso.Text) = True Then

            If (txt_prom_pago.Text = "" And txt_monto_prom_pago.Text <> "") Or (txt_prom_pago.Text <> "" And txt_monto_prom_pago.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago.Text) = True Then

                    If Validamonto(txt_monto_prom_pago.Text) = True Then
                        EventoRegJuicio()
                        lbl_status.Text = "Se ha guardado correctamente el registro"
                        Abogados(Session("FOLIO"))
                        LIMPIAREGISTRO()
                        limpiaRegistroJuicio()

                    Else
                        lbl_status.Text = "Error: Monto incorrecto"
                    End If

                Else
                    lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
                End If

            End If

        Else
            lbl_status.Text = "Error: Verifique el formato de fecha de diligencia"
        End If

    End Sub

    Private Sub EventoRegJuicio()


        Dim evento
        evento = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        Dim resultado
        resultado = Split(cmb_estatus_juicio.SelectedItem.Value.ToString, "-")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 20, evento(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_INGRESO", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_ingreso.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDRESULTADO", Session("adVarChar"), Session("adParamInput"), 50, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("JUZGADO", Session("adVarChar"), Session("adParamInput"), 300, txt_juzgado.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EXHORTO", Session("adVarChar"), Session("adParamInput"), 100, txt_exhorto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("JUZGADO_EXHORTADO", Session("adVarChar"), Session("adParamInput"), 300, txt_juzgado_exhorto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("GESTOR", Session("adVarChar"), Session("adParamInput"), 300, txt_gestor.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DETALLE", Session("adVarChar"), Session("adParamInput"), 8000, txt_detalle.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CITA", Session("adVarChar"), Session("adParamInput"), 10, txt_cita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROM_PAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_prom_pago.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO_PROM_PAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto_prom_pago.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_EMP_TIT", Session("adVarChar"), Session("adParamInput"), 15, txt_fecha_emp_tit.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_EMP_AVAL", Session("adVarChar"), Session("adParamInput"), 15, txt_fecha_emp_Aval.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_JUICIO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Protected Sub lnk_cancelar_reg_juicio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_cancelar_reg_juicio.Click
        limpiaRegistroJuicio()
        pnl_reg_juicio.Visible = False
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"
    End Sub


#End Region

#Region "Calculadora Credito"
    Private Sub Llenainfocredito()
        Dim montoLiquidar As Decimal
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VENTANILLA_INFO_CREDITO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_montocreditotxt.Text = Session("rs").Fields("MONTO").Value.ToString
            lbl_saldocreditotxt.Text = Session("rs").Fields("SALDO").Value.ToString
            lbl_fechaliberaciontxt.Text = Session("rs").Fields("FECHA_L").Value.ToString
            lbl_fechaultimopagotxt.Text = Session("rs").Fields("FECHA_ULT").Value.ToString
            lbl_intnortxt.Text = Session("rs").Fields("TASA_NOR").Value.ToString
            lbl_intmortxt.Text = Session("rs").Fields("TASA_MOR").Value.ToString
            lbl_diasultimopagotxt.Text = Session("rs").Fields("DIAS_ULT").Value.ToString
            lbl_vencidostxt.Text = Session("rs").Fields("VENCIDOS").Value.ToString
            lbl_fechavenctxt.Text = Session("rs").Fields("FECHA_VENC").Value.ToString
            'Session("CLASIFICACION") = Session("rs").Fields("CLASCRED").Value.ToString
            Session("FECHA_SISTEMA") = Session("rs").Fields("FECHA_ACTUAL").Value.ToString
            Session("FECHA_PROX") = Session("rs").Fields("FECHA_LIM").Value.ToString
            Session("INT_NOR") = Session("rs").Fields("INT_NOR").Value
            Session("IVA_INT_NOR") = Session("rs").Fields("IVA_INT_NOR").Value
            Session("MONTO_TOTAL_CRED") = Session("rs").Fields("MONTO_TOTAL_CRED").Value
            Session("SALDO_CTA_EJE") = Session("rs").Fields("SALDO_CTA_EJE").Value
            lbl_saldo_cta_eje_txt.Text = Session("SALDO_CTA_EJE")
            montoLiquidar = Session("MONTO_TOTAL_CRED") - Session("SALDO_CTA_EJE")
            lbl_monto_liq_txt.Text = IIf(montoLiquidar < 0.0, 0.0, montoLiquidar)
            Session("COM") = Session("rs").Fields("COM").Value
            Session("IVA_COM") = Session("rs").Fields("IVA_COM").Value
            Session("MONTO_ADEUDO_CV") = Session("rs").Fields("MONTO_ADEUDO_CV").Value
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenadatospago()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter(), dtPendientes As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_PAGO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPendientes, Session("rs"))
        grd_pago.DataSource = dtPendientes
        grd_pago.DataBind()
        Session("Con").Close()
    End Sub

    Private Sub datos()
        Llenainfocredito()
        grd_pago.Enabled = True
        grd_pago.Visible = True
        Llenadatospago()

        Session("FOLIO") = cmb_folio.SelectedItem.Value
    End Sub

    Protected Sub OnTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fechacorte.TextChanged
        If txt_fechacorte.Text <> "" Then
            If CDate(txt_fechacorte.Text) > CDate(Session("FECHA_SISTEMA")) Then
                lbl_info_fecha_corte.Text = ""
                Dim custDA As New System.Data.OleDb.OleDbDataAdapter(), dtPendientes As New Data.DataTable()
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("FECHA_AD_V", Session("adVarChar"), Session("adParamInput"), 10, txt_fechacorte.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_PAGO_AD"
                Session("rs") = Session("cmd").Execute()
                custDA.Fill(dtPendientes, Session("rs"))
                grd_pago.DataSource = dtPendientes
                grd_pago.DataBind()
                Session("Con").Close()
                lbl_info_fecha_corte.Visible = False
            Else
                lbl_info_fecha_corte.Visible = True
            End If
        End If
    End Sub

#End Region


#Region "ASIGNACION DESPACHO"
    'DESPACHO/ABOGADO
    Private Sub Llenafase()

        cmb_fase.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")

        cmb_fase.Items.Add(elija)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_FASE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_fase.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'SUCURSALES
    Private Sub LlenaSucursales()

        cmb_sucursal.Items.Clear()
        cmb_sucursal_atendio.Items.Clear()
        cmb_sucursal_visita.Items.Clear()
        cmb_sucursal_seg_visita.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_sucursal.Items.Add(elija)
        cmb_sucursal_atendio.Items.Add(elija)
        cmb_sucursal_visita.Items.Add(elija)
        cmb_sucursal_seg_visita.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_sucursal.Items.Add(item)
            cmb_sucursal_atendio.Items.Add(item)
            cmb_sucursal_visita.Items.Add(item)
            cmb_sucursal_seg_visita.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'DESPACHO/ABOGADO

    Private Sub Llenadespacho()

        cmb_despacho_asignar.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_despacho_asignar.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_DESP_EXP_DESP"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_despacho_asignar.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub dag_expedientes_pageIndexChanged(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs) Handles dag_expedientes.PageIndexChanged

        dag_expedientes.CurrentPageIndex = e.NewPageIndex
        LLENAEXPEDIENTES(0, Session("FOLIO"))

    End Sub

    Protected Sub dag_expedientes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_expedientes.ItemDataBound
        'se oculta la columna de ID abogado
        e.Item.Cells(8).Visible = False
    End Sub

    Private Sub LLENAEXPEDIENTES(ByVal Filtro As Integer, ByVal FOLIO As Integer)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dthistorial As New Data.DataTable()
        dag_expedientes.Visible = True
        If Filtro = 1 Then
            dag_expedientes.CurrentPageIndex = 0
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_EXP_X_FOLIO"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dthistorial, Session("rs"))

        If dthistorial.Rows.Count > 0 Then
            pnl_expedientes.Visible = True
            'se vacian los expedientes al formulario
            dag_expedientes.DataSource = dthistorial
            dag_expedientes.DataBind()
            Session("CONSULTA") = dthistorial
        Else
            'lbl_status.Text = "No existen registros"
            dag_expedientes.Visible = False
        End If

        Session("Con").Close()

    End Sub

    Private Sub DAG_expediente_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_expedientes.ItemCommand
        lbl_status.Text = ""

        If (e.CommandName = "MODIFICAR") Then

            limpiamodal()
            Session("FOLIO") = e.Item.Cells(0).Text
            Llenadespacho()
            Llenafase()
            ModalPopupExtender_asignacion.Show()
            cargarAsignados(Session("FOLIO"))

        End If

    End Sub

    Private Sub cargarAsignados(ByVal FOLIO As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_EXP_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            cmb_despacho_asignar.Items.RemoveAt(0)
            cmb_despacho_asignar.Items.FindByValue(Session("rs").Fields("IDDESPACHO").Value.ToString).Selected = True
            cmb_fase.Items.RemoveAt(0)
            cmb_fase.Items.FindByValue(Session("rs").Fields("IDFASE").Value.ToString).Selected = True
            txt_Valor.Text = Session("rs").Fields("PCTJE").Value.ToString
            txt_Objetivo.Text = Session("rs").Fields("NOTAS").Value.ToString

        End If
        Session("Con").Close()
    End Sub

    Private Sub Asignar(ByVal FOLIO As String, ByVal IDDESPACHO As String, ByVal IDFASE As String, ByVal PCTJE As String, ByVal NOTAS As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 20, IDDESPACHO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDFASE", Session("adVarChar"), Session("adParamInput"), 20, IDFASE)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("COMISION", Session("adVarChar"), Session("adParamInput"), 20, PCTJE)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 8000, NOTAS)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_DESP_EXP"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub limpiamodal()
        cmb_fase.SelectedIndex = "0"
        cmb_despacho_asignar.SelectedIndex = "0"
        txt_Valor.Text = ""
        txt_Objetivo.Text = ""
        lbl_status_modal.Text = ""
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        lbl_status_modal.Visible = True
        If txt_Objetivo.Text.Length > 3000 Then
            ModalPopupExtender_asignacion.Show()
            lbl_status_modal.Text = "Error: Sólo 3000 caracteres o menos en las notas"
            Exit Sub
        End If

        If Validamonto(txt_Valor.Text) = False Then
            ModalPopupExtender_asignacion.Show()
            lbl_status_modal.Text = "Error: Porcentaje incorrecto"
            Exit Sub
        End If

        If txt_Valor.Text <> "" Then
            If CDec(txt_Valor.Text) > 100 Then
                ModalPopupExtender_asignacion.Show()
                lbl_status_modal.Text = "Error: Excede del 100 %"
                Exit Sub
            End If
        End If

        If cmb_despacho_asignar.SelectedItem.Value = "-1" And cmb_fase.SelectedItem.Text <> "AL CORRIENTE" Then
            ModalPopupExtender_asignacion.Show()
            lbl_status_modal.Text = "Error: Selecione un abogado/despacho"
            Exit Sub
        Else
            Asignar(Session("FOLIO"), cmb_despacho_asignar.SelectedItem.Value.ToString, cmb_fase.SelectedItem.Value.ToString, txt_Valor.Text, txt_Objetivo.Text)
            LLENAEXPEDIENTES(1, Session("FOLIO"))
            Enviar_Correo(Session("FOLIO"), cmb_despacho_asignar.SelectedItem.Value.ToString)
        End If

    End Sub

    Protected Sub btn_cerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar.Click
        ModalPopupExtender_asignacion.Hide()
    End Sub

    Private Sub LIMPIADAG()
        lbl_status.Text = ""
        dag_expedientes.CurrentPageIndex = 0
        dag_expedientes.Visible = False
        Dim dtconsulta As New Data.DataTable()
        dtconsulta.Clear()
        dag_expedientes.DataSource = dtconsulta
        dag_expedientes.DataBind()
    End Sub

    Private Sub Motor_On_off()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, "DEXP")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONFIGURACION_ENVIOS_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("MOTOR") = Session("rs").fields("GRANTED").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Private Sub Enviar_Correo(ByVal Folio As String, ByVal iddespacho As String)

        Motor_On_off()
        If Session("MOTOR") = "1" Then
            envioemail(Folio, iddespacho)
        End If

        Session("MOTOR") = Nothing
    End Sub

    Private Sub envioemail(ByVal Folio As String, ByVal iddespacho As String)
        Dim mail As New Correo
        Dim cuerpo As String = ""
        Dim Abogado As String = ""
        Dim Email_Abogado As String = ""  'correo para enviar 
        'variables para el armado del html para tomar valor desde sql 
        Dim Fecha As String = ""
        Dim Info_cliente As String = ""
        Dim Info_gtias As String = ""
        Dim Info_bitacora As String = ""
        Dim mailerror As String = ""
        Dim CLIENTE As String = ""
        Dim Folio_ As String = ""
        Dim Fecha_Ven As String = ""
        Dim Monto_Cr As String = ""
        Dim Saldo_Cu As String = ""
        Dim Saldo_Ins As String = ""
        Dim int_Ord As String = ""
        Dim iva_Int As String = ""
        Dim In_Morat As String = ""
        Dim IVA_Int_mor As String = ""
        Dim Fecha_lib As String = ""
        Dim name_cliente As String = ""
        Dim int_mora As String = ""
        Dim date_Cita As String = ""
        Dim Sucursal_Cita As String = ""
        Dim Nombre_Dest As String = ""
        Dim Notas As String = ""
        Dim date_Reg As String = ""
        Dim Usuario As String = ""
        Dim date_Real_registro As String = ""
        Dim Resultado As String = ""
        Dim datetime_Atención As String = ""
        Dim Sucursal_at As String = ""
        Dim duracion As String = ""
        Dim Notas_Seg As String = ""
        Dim date_real_Reg As String = ""
        Dim tipo_garantia As String = String.Empty
        Dim descrip_garantia As String = String.Empty
        Dim valor_garantia As String = String.Empty
        lbl_status.Text = "" 'se obtiene msj
        Dim sbHtml As StringBuilder = New StringBuilder ' variable para armado de html
        Dim stringb As StringBuilder = New StringBuilder 'variable para obtener el resultado del envio
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_LOG_CONFIGURACION_GLOBAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Session("BCC_ENVIADOS") = Session("rs").Fields("BCC_ENVIADOS").Value.ToString
            Session("BCC_ENVIADOS_EMAIL1") = Session("rs").Fields("BCC_ENVIADOS_EMAIL1").Value.ToString
            Session("BCC_ENVIADOS_EMAIL2") = Session("rs").Fields("BCC_ENVIADOS_EMAIL2").Value.ToString
            Session("REMITENTE_ALIAS") = Session("rs").Fields("REMITENTE_ALIAS").Value.ToString

        End If

        Session("Con").Close()



        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 20, iddespacho)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_CTA_ENVIO_CORREO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF

                If Session("rs").Fields("ETIQUETA").Value.ToString = "INFO DESPACHO" Then

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "DESPACHO" Then
                        Abogado = Session("rs").Fields("DATO").Value.ToString
                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "EMAIL" Then
                        Email_Abogado = Session("rs").Fields("DATO").Value.ToString
                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "FECHA" Then
                        Fecha = Session("rs").Fields("DATO").Value.ToString
                    End If


                End If


                ' se verifica por un que se INFO CLIENTE para asi obtener los valores en cada variable para enviarlo en el cuerpo de correo 
                'buscando por descripcion y asi asiganarle valor a la variable
                If Session("rs").Fields("ETIQUETA").Value.ToString = "INFO CLIENTE" Then
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Núm. Cliente: " Then
                        CLIENTE = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Folio: " Then
                        Folio = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Cliente: " Then
                        name_cliente = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha Liberación: " Then
                        Fecha_lib = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha Vencimiento: " Then
                        Fecha_Ven = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Monto Préstamo: " Then
                        Monto_Cr = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Saldo Cuenta Eje:  " Then
                        Saldo_Cu = Session("rs").Fields("DATO").Value.ToString
                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Saldo Insoluto: " Then
                        Saldo_Ins = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Interés Ordinario:  " Then
                        int_Ord = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "IVA Interés Ordinario: " Then
                        iva_Int = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Interés Moratorio: " Then
                        int_mora = Session("rs").Fields("DATO").Value.ToString
                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "IVA Interés Moratorio:" Then
                        IVA_Int_mor = Session("rs").Fields("DATO").Value.ToString
                    End If
                End If

                ' se verifica por un que se INFO GTIAS para asi obtener los valores en cada variable para enviarlo en el cuerpo de correo 
                'buscando por descripcion y asi asiganarle valor a la variable

                If Session("rs").Fields("ETIQUETA").Value.ToString = "INFO GTIAS" Then
                    Info_gtias = Info_gtias + Session("rs").Fields("DESCRIPCION").Value.ToString + Session("rs").Fields("DATO").Value.ToString + vbCrLf
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Tipo Garantía: " Then
                        tipo_garantia = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Tipo Garantía: " Then
                        descrip_garantia = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Descripción Garantías:  " Then
                        tipo_garantia = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Valor Garantías: " Then
                        valor_garantia = Session("rs").Fields("DATO").Value.ToString
                    End If

                End If

                If Session("rs").Fields("ETIQUETA").Value.ToString = "INFO BITACORA" Then
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha y Hora Cita: " Then
                        date_Cita = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Sucursal Cita: " Then
                        Sucursal_Cita = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Nombre Destinatario: " Then
                        Nombre_Dest = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Notas: " Then
                        Notas = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha Registro: " Then
                        date_Reg = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Usuario: " Then
                        Usuario = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha Real de Registro:: " Then
                        date_real_Reg = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Resultado:" Then
                        Resultado = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha y Hora Atención: " Then
                        datetime_Atención = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Sucursal Atención: " Then
                        Sucursal_at = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Duración: " Then
                        duracion = Session("rs").Fields("DATO").Value.ToString

                    End If

                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Notas del Seguimiento: " Then
                        Notas_Seg = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha Registro: " Then
                        date_Reg = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Usuario: " Then
                        Usuario = Session("rs").Fields("DATO").Value.ToString

                    End If
                    If Session("rs").Fields("DESCRIPCION").Value.ToString = "Fecha Real Registro: " Then
                        date_real_Reg = Session("rs").Fields("DATO").Value.ToString

                    End If

                End If

                Session("rs").movenext()
            Loop

            ' SOLO SE ENVIA CORREO AQUEL DESPACHO/ABOGADO QUE TENGA CORREO
            If Email_Abogado <> "" Then
                'asunto de correo por variable
                subject = "Asignación de Expediente"
                'armado de html para cuerpo de correo
                sbHtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbHtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
                sbHtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbHtml.Append("<tr><td>Estimado Despacho/Abogado:  " + "<b>" + Abogado + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td>Por medio de la presente se le informa que se le ha asignado un nuevo expediente de la empresa:  " + Session("EMPRESA") + " con el objetivo de liquidar la cuenta.</td></tr>")
                sbHtml.Append("<tr><td>A continuación se describe la información del expediente:</td></tr>")
                sbHtml.Append("</table>")
                sbHtml.Append("<br />")
                sbHtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
                sbHtml.Append("<tr><td width='25%'>Información del expediente</td></td></tr>")
                sbHtml.Append("<tr><td width='25%'>Número Cliente</b></td><td>" + CLIENTE + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Folio:</td>" + "<b>" + Folio_ + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Cliente:</td>" + "<b>" + name_cliente + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha Liberación:</td>" + "<b>" + Fecha_lib + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha Vencimiento:</td>" + "<b>" + Fecha_Ven + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Monto Préstamo:</td>" + "<b>" + Monto_Cr + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Saldo Cuenta Eje:</td>" + "<b>" + Saldo_Cu + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Saldo Insoluto:</td>" + "<b>" + Saldo_Ins + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Interés Ordinario:</td>" + "<b>" + int_Ord + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>IVA Interés Ordinario:</td>" + "<b>" + iva_Int + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Interés Moratorio:</td>" + "<b>" + int_mora + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>IVA Interés Moratorio:</td>" + "<b>" + IVA_Int_mor + "</b>" + "</td></tr>")
                sbHtml.Append("<br></br>")
                sbHtml.Append("<tr><td width='150'>Información de Garantías</td>" + "<b>" + Info_gtias + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Tipo Garantía: </b></td><td>" + "<b>" + "</b>" + tipo_garantia + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Descripción Garantías: </b></td><td>" + "<b>" + "</b>" + descrip_garantia + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Valor Garantías: </b></td><td>" + "<b>" + "</b>" + valor_garantia + "</td></tr>")
                sbHtml.Append("<br></br>")
                sbHtml.Append("<tr><td>Información de la última acción</td></tr>")
                sbHtml.Append("<br></br>")
                sbHtml.Append("<tr><td width='25%'>Fecha y Hora Cita:</b></td><td>" + "<b>" + "</b>" + date_Cita + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Sucursal Cita:</td>" + "<b>" + Sucursal_Cita + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Nombre Destinatario:</td>" + "<b>" + Nombre_Dest + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Notas:</td>" + "<b>" + Notas + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha Registro:</td>" + "<b>" + date_Reg + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Usuario:</td>" + "<b>" + Usuario + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha Real de Registro:</td>" + "<b>" + date_Real_registro + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Resultado:</td>" + "<b>" + Resultado + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha y Hora Atención:</td>" + "<b>" + datetime_Atención + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Sucursal Atención:</td>" + "<b>" + Sucursal_at + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Duración:</td>" + "<b>" + duracion + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Notas del Seguimiento:</td>" + "<b>" + Notas_Seg + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha Registro:</td>" + "<b>" + Notas_Seg + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Usuario:</td>" + "<b>" + Usuario + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='25%'>Fecha Real Registro:</td>" + "<b>" + date_real_Reg + "</b>" + "</td></tr>")
                sbHtml.Append("<tr><td width='150'>Fecha</b></td><td>" + "<b>" + Date.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</b>" + "</td></tr>")
                sbHtml.Append("<br></br>")
                sbHtml.Append("<tr><td width='350'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
                sbHtml.Append("</table>")
                sbHtml.Append("<br></br>")

            Else
                'msj en caso de no exista un correo para envio
                lbl_status.Text = "No se ha enviado correo , no existe una cuenta de correo registrada"
            End If
            'se hace condicion para saber si se obtiene error de la clase de correo
            'se obtiene el error del msj si viene vacio esto indica que el correo se envio correctamente
            If Not (mail.Envio_email(sbHtml.ToString, subject, Email_Abogado, cc)) Then
                stringb.Append("Descripción:<br>" + mail._mailError + "<br>")
                lbl_status.Text = "Clase Correo " + stringb.ToString()
                'Else
                '    lbl_status.Text = "Envío de correo exitoso"
            End If
        End If
    End Sub



#End Region

#Region "ESTATUS COBRANZA"
    Protected Sub lnk_cancelar_estatus_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_estatus.Click
        limpiaEstatusCobranza()
        pnl_estatus.Visible = False
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"
    End Sub

    Private Sub limpiaEstatusCobranza()

        cmb_estatus_cob.Items.Clear()
        lbl_user_estatus.Text = ""
        pnl_estatus.Visible = False

    End Sub

    Private Sub EventoEstatusCobranza()
        Dim evento
        Dim resultado

        evento = Split(cmb_evento.SelectedItem.Value.ToString, "-")
        resultado = Split(cmb_estatus_cob.SelectedItem.Value.ToString, "-")


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 20, evento(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDRESULTADO", Session("adVarChar"), Session("adParamInput"), 50, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_ESTATUS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Protected Sub lnk_agregar_estatus_Click(sender As Object, e As EventArgs) Handles lnk_agregar_estatus.Click
        EventoEstatusCobranza()
        lbl_status.Text = "Se ha guardado correctamente el registro"
        InfoCta(Session("FOLIO"))
        LIMPIAREGISTRO()
        limpiaEstatusCobranza()
    End Sub
#End Region

#Region "VISITAS"
    'EVENTO AL HACER UNA VISITA
    Private Sub Llenaeventovisita()

        cmb_evento_visita.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_evento_visita.Items.Add(elija)

        Dim agendar As New ListItem("AGENDAR", "AG")
        cmb_evento_visita.Items.Add(agendar)
        Dim seguimiento As New ListItem("SEGUIMIENTO", "SEG")
        cmb_evento_visita.Items.Add(seguimiento)

    End Sub

    'VISITAS AGENDADAS
    Private Sub visita()
        cmb_visita_seguimiento.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_visita_seguimiento.Items.Add(elija)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_VISITAS_AGENDADAS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("FECHA").Value.ToString + " (" + Session("rs").Fields("SUCURSAL").Value.ToString + ") ", Session("rs").Fields("NUM").Value.ToString)
            cmb_visita_seguimiento.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()


    End Sub


    Protected Sub cmb_evento_visita_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_evento_visita.SelectedIndexChanged

        Select Case cmb_evento_visita.SelectedItem.Value

            Case "AG"
                pnl_seg_visita.Visible = False
                pnl_seguimiento_visita.Visible = False
                pnl_agendar_visita.Visible = True
                txt_fecha_visita.Text = ""
                txt_hora_visita.Text = ""
                txt_noras_visita.Text = ""
                LlenaSucursales()
                LlenaDestinatarios()
                cmb_persona_visita.Items.Clear()
                lbl_usuario_visita.Text = Session("NombreUsr")
            Case "SEG"
                pnl_agendar_visita.Visible = False
                pnl_seg_visita.Visible = True
                visita()
            Case Else
                pnl_agendar_visita.Visible = False
                pnl_seg_visita.Visible = False
        End Select
    End Sub

    Protected Sub cmb_visita_seguimiento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_visita_seguimiento.SelectedIndexChanged
        If cmb_visita_seguimiento.SelectedIndex = "0" Then
            lbl_status.Text = "Error: Seleccione una visita para dar seguimiento"
        Else
            Dim CLAVE
            CLAVE = Split(cmb_evento.SelectedItem.Value.ToString, "-")
            pnl_seguimiento_visita.Visible = True
            Llenaresultado(CLAVE(0))
            LlenaSucursales()
            LlenaUsuarios()
            LlenaDestinatarios()
        End If
    End Sub

    Protected Sub lnk_agregar_visita_Click(sender As Object, e As EventArgs) Handles lnk_agregar_visita.Click
        lbl_Info.Text = ""
        lbl_status.Text = ""

        If CDate(txt_fecha_visita.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha de visita es anterior a la fecha de sistema"
            Exit Sub
        End If

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_fecha_visita.Text) = True And horas(txt_hora_visita.Text) = True Then
            EventoVISITA()
            lbl_status.Text = "Se ha registrado correctamente la visita"
            LIMPIAREGISTRO()
            limpiavisita()
            pnl_evento_visita.Visible = False
            pnl_seg_visita.Visible = False
            pnl_seguimiento_visita.Visible = False
        Else
            lbl_status.Text = "Error: Verifique formato de Fecha y hora"
        End If
    End Sub


    Private Sub limpiavisita()
        cmb_evento_visita.Items.Clear()
        lbl_usuario_visita.Text = ""
        cmb_sucursal_visita.Items.Clear()
        txt_fecha_visita.Text = ""
        txt_hora_visita.Text = ""
        txt_noras_visita.Text = ""
        pnl_evento_visita.Visible = False
        pnl_agendar_visita.Visible = False
    End Sub

    Private Sub limpiaseguimientovisita()
        txt_fecha_seg_visita.Text = ""
        txt_hora_seg_visita.Text = ""
        txt_duracion_visita.Text = ""
        cmb_resultado_visita.Items.Clear()
        cmb_usuario_seg_visita.Items.Clear()
        cmb_sucursal_seg_visita.Items.Clear()
        txt_seg_notas.Text = ""
        txt_monto_prompago_visita.Text = ""
        txt_prom_pago_visita.Text = ""
    End Sub

    Protected Sub lnk_cancelar_visita_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_visita.Click
        limpiavisita()
        pnl_agendar_visita.Visible = False
        pnl_evento_visita.Visible = True
        cmb_evento_visita.SelectedIndex = "0"
    End Sub


    Private Sub EventoVISITA()

        Dim evento
        evento = Split(cmb_evento.SelectedItem.Value.ToString, "-")


        Dim Iddestinatario
        Iddestinatario = Split(cmb_destinatario_visita.SelectedItem.Value.ToString, "-")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 20, evento(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESTINATARIO", Session("adVarChar"), Session("adParamInput"), 20, Iddestinatario(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, cmb_persona_visita.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAVISITA", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 3000, txt_noras_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORAS", Session("adVarChar"), Session("adParamInput"), 5, txt_hora_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, cmb_sucursal_visita.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_VISITA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub Eventovisita_Seguimiento()

        Dim resultado
        resultado = Split(cmb_resultado_visita.SelectedItem.Value.ToString, "-")
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDVISITA", Session("adVarChar"), Session("adParamInput"), 20, cmb_visita_seguimiento.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, cmb_sucursal_seg_visita.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID_A", Session("adVarChar"), Session("adParamInput"), 20, cmb_usuario_seg_visita.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAVISITA", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_seg_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORA", Session("adVarChar"), Session("adParamInput"), 15, txt_hora_seg_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIEMPO", Session("adVarChar"), Session("adParamInput"), 15, txt_duracion_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDRESULTADO", Session("adVarChar"), Session("adParamInput"), 15, resultado(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOTIVO", Session("adVarChar"), Session("adParamInput"), 3000, txt_seg_notas.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_prom_pago_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOPROMPAGO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto_prompago_visita.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_LOG_VISITA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub
    Protected Sub lnk_guardar_seg_visita_Click(sender As Object, e As EventArgs) Handles lnk_guardar_seg_visita.Click
        lbl_Info.Text = ""
        lbl_status.Text = ""

        If CDate(txt_fecha_seg_visita.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha anterior a la fecha de sistema"
            Exit Sub
        End If


        If txt_prom_pago_visita.Text <> "" Then
            If (txt_prom_pago_visita.Text <> "" And (CDate(txt_prom_pago_visita.Text) < CDate(Session("FechaSis")))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
                Exit Sub
            End If

        End If



        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_fecha_seg_visita.Text) = True And horas(txt_hora_seg_visita.Text) = True Then


            If (txt_prom_pago_visita.Text = "" And txt_monto_prompago_visita.Text <> "") Or (txt_prom_pago_visita.Text <> "" And txt_monto_prompago_visita.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago_visita.Text) = True Then

                    If Validamonto(txt_monto_prompago_visita.Text) = True Then
                        Eventovisita_Seguimiento()
                        lbl_status.Text = "Se ha guardado correctamente el seguimiento"
                        LIMPIAREGISTRO()
                        limpiaseguimientovisita()
                        pnl_seguimiento_visita.Visible = False
                        pnl_evento_visita.Visible = False
                        pnl_seg_visita.Visible = False

                    Else
                        lbl_status.Text = "Error: Monto incorrecto"
                    End If

                Else
                    lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
                End If

            End If

        Else
            lbl_status.Text = "Error: Verifique el formato de fecha y hora"
        End If


    End Sub

    Protected Sub lnk_cancelar_seg_visita_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_seg_visita.Click
        limpiaseguimientovisita()
        pnl_seguimiento_visita.Visible = False
        pnl_seg_visita.Visible = False
        pnl_evento_visita.Visible = True
        cmb_evento_visita.SelectedIndex = "0"
    End Sub

    Protected Sub cmb_destinatario_visita_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_destinatario_visita.SelectedIndexChanged
        If cmb_destinatario_visita.SelectedIndex <> "0" Then

            Dim CLAVE
            CLAVE = Split(cmb_destinatario_visita.SelectedItem.Value.ToString, "-")
            LlenaNombreDestinatarios(CLAVE(0))
            If CLAVE(1) = "CLI" Then
                lbl_tit_nombre_visita.Visible = False
                cmb_persona_visita.Visible = False
                cmb_persona_visita.Items.RemoveAt(0)
                cmb_persona_visita.Items.FindByValue(txt_cliente.Text).Selected = True

            Else
                lbl_tit_nombre_visita.Visible = True
                cmb_persona_visita.Visible = True
            End If

        End If
    End Sub
#End Region



    Protected Sub btn_ver_Aviso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ver_Aviso.Click
        Dim cPath As String = Session("APPATH") + "\DocPlantillas\"
        Dim NewDocName As String = "Solicitud" + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        Dim cNewDocC As String = NewDocName + ".docx"
        Dim cNewDocA As String = cNewDocC + ".docx"
        Dim cPathNewDocC As String = cPath + NewDocName + ".docx"
        CrearTablas()
        LlenaEventosFolio(Session("FOLIO"))
        GeneraDocumento(cPathNewDocC)
        ConviertePDF(NewDocName, cPath)
    End Sub

    Private Sub CrearTablas()

        Session("Eventos_Asignados") = Nothing

        Dim Eventos_Asignados As New Data.DataTable

        Eventos_Asignados.Columns.Add("ID", GetType(Integer)) '0
        Eventos_Asignados.Columns.Add("IDEVENTO", GetType(Integer)) '1
        Eventos_Asignados.Columns.Add("FOLIO", GetType(Integer)) '2
        Eventos_Asignados.Columns.Add("IDPLANTILLA", GetType(Integer)) '3
        Eventos_Asignados.Columns.Add("IDPERSONA", GetType(Integer)) '4
        Eventos_Asignados.Columns.Add("EVENTO", GetType(String)) '5

        Session("Eventos_Asignados") = Eventos_Asignados

    End Sub

    Private Sub LlenaEventosFolio(ByVal Folio As Integer)

        Dim Cont As Integer = 0
        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_COB_EVENTOSDOC_X_FOLIO"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                Cont = Cont + 1
                Session("Eventos_Asignados").Rows.Add(Cont, Session("rs").Fields("IDEVENTO").Value.ToString, Session("rs").Fields("FOLIO").Value.ToString, Session("rs").Fields("IDPLANTILLA").Value.ToString, Session("rs").Fields("IDOTRA").Value.ToString, Session("rs").Fields("EVENTO").Value.ToString)
                Session("rs").movenext()
            Loop

            Session("Con").Close()

        Catch ex As Exception
            lbl_documento.Text = ex.ToString
        End Try
    End Sub

    Private Sub GeneraDocumento(ByVal NewDocName As String)
        Dim lcUrl As String = "AVISO.docx"

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los roles a un usuario
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("SEL_DATOS_AVISO_PRELLENADO", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("EVENTOS", SqlDbType.Structured)
                Session("parm").Value = Session("Eventos_Asignados")
                insertCommand.Parameters.Add(Session("parm"))


                Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "\DocPlantillas\" + lcUrl)
                    Try

                        Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                        While myReader.Read()
                            worddoc.ReplaceText("{*DIRECCION*}", myReader.GetString(0), False, RegexOptions.None)
                            worddoc.ReplaceText("{*ID_CLIENTE*}", myReader.GetInt32(1), False, RegexOptions.None)
                            worddoc.ReplaceText("{*CLIENTE*}", myReader.GetString(2), False, RegexOptions.None)
                            worddoc.ReplaceText("{*DIR_CLIENTE*}", myReader.GetString(3), False, RegexOptions.None)
                            worddoc.ReplaceText("{*REF_CLIENTE*}", myReader.GetString(4), False, RegexOptions.None)
                            worddoc.ReplaceText("{*MONTO_CREDITO*}", myReader.GetString(5), False, RegexOptions.None)
                            worddoc.ReplaceText("{*SALDO_CREDITO*}", myReader.GetString(6), False, RegexOptions.None)
                            worddoc.ReplaceText("{*PAGOS_ATR*}", myReader.GetInt32(7), False, RegexOptions.None)
                            worddoc.ReplaceText("{*PAGOS_PACTADOS*}", myReader.GetInt32(8), False, RegexOptions.None)
                            worddoc.ReplaceText("{*MONTO_CORRIENTE*}", myReader.GetString(9), False, RegexOptions.None)
                            worddoc.ReplaceText("{*INT_NORMAL*}", myReader.GetString(10), False, RegexOptions.None)
                            worddoc.ReplaceText("{*IVA_INT_NORMAL*}", myReader.GetString(11), False, RegexOptions.None)
                            worddoc.ReplaceText("{*INT_MOR*}", myReader.GetString(12), False, RegexOptions.None)
                            worddoc.ReplaceText("{*IVA_INT_MOR*}", myReader.GetString(13), False, RegexOptions.None)
                            worddoc.ReplaceText("{*SALDO_TOTAL*}", myReader.GetString(14), False, RegexOptions.None)
                            worddoc.ReplaceText("{*PERIODICIDAD*}", myReader.GetString(15), False, RegexOptions.None)
                            worddoc.ReplaceText("{*DIAS_MORA*}", myReader.GetInt32(16), False, RegexOptions.None)
                            worddoc.ReplaceText("{*ULT_PAGO*}", myReader.GetString(17), False, RegexOptions.None)
                            worddoc.ReplaceText("{*ID_CODEUDOR*}", myReader.GetString(18), False, RegexOptions.None)
                            worddoc.ReplaceText("{*NOMBRE_CODEUDOR*}", myReader.GetString(19), False, RegexOptions.None)
                            worddoc.ReplaceText("{*DIR_CODEUDOR*}", myReader.GetString(20), False, RegexOptions.None)
                            worddoc.ReplaceText("{*REF_CODEUDOR*}", myReader.GetString(21), False, RegexOptions.None)
                            worddoc.ReplaceText("{*ID_AVAL1*}", myReader.GetString(22), False, RegexOptions.None)
                            worddoc.ReplaceText("{*NOMBRE_AVAL1*}", myReader.GetString(23), False, RegexOptions.None)
                            worddoc.ReplaceText("{*DIR_AVAL1*}", myReader.GetString(24), False, RegexOptions.None)
                            worddoc.ReplaceText("{*REF_AVAL1*}", myReader.GetString(25), False, RegexOptions.None)
                            worddoc.ReplaceText("{*ID_AVAL2*}", myReader.GetString(26), False, RegexOptions.None)
                            worddoc.ReplaceText("{*NOMBRE_AVAL2*}", myReader.GetString(27), False, RegexOptions.None)
                            worddoc.ReplaceText("{*DIR_AVAL2*}", myReader.GetString(28), False, RegexOptions.None)
                            worddoc.ReplaceText("{*REF_AVAL2*}", myReader.GetString(29), False, RegexOptions.None)
                            worddoc.ReplaceText("{*SUCURSAL*}", myReader.GetString(30), False, RegexOptions.None)
                        End While
                        myReader.Close()

                        '            worddoc.ReplaceText("{*DIRECCION*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*ID_CLIENTE*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*CLIENTE*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*DIR_CLIENTE*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*REF_CLIENTE*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*MONTO_CREDITO*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*SALDO_CREDITO*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*PAGOS_ATR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*PAGOS_PACTADOS*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*MONTO_CORRIENTE*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*INT_NORMAL*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*IVA_INT_NORMAL*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*INT_MOR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*IVA_INT_MOR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*SALDO_TOTAL*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*PERIODICIDAD*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*DIAS_MORA*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*ULT_PAGO*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*ID_CODEUDOR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*NOMBRE_CODEUDOR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*DIR_CODEUDOR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*REF_CODEUDOR*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*ID_AVAL1*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*NOMBRE_AVAL1*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*DIR_AVAL1*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*REF_AVAL1*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*ID_AVAL2*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*NOMBRE_AVAL2*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*DIR_AVAL2*}", " ", False, RegexOptions.None)
                        '            worddoc.ReplaceText("{*REF_AVAL2*}", " ", False, RegexOptions.None)

                        worddoc.SaveAs(NewDocName)
                        lbl_documento.Text = ""
                    Catch ex As Exception
                        lbl_documento.Text = "Error al crear el documento"
                    End Try

                End Using

            End Using
        Catch ex As Exception
            lbl_documento.Text = "Error de conexión"

        Finally

        End Try

    End Sub

    Protected Sub lnk_PlanPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_PlanPagos.Click
        Session("VENGODE") = "COB_INFO_GENERAL.aspx"
        Response.Redirect("../CREDITO/EXPEDIENTES/CRED_EXP_PLAN_GENERAL.aspx")
    End Sub

    Private Sub ConviertePDF(ByVal NewDocName As String, ByVal cPath As String)
        Dim result As String = ""
        Dim objNewWord As New Microsoft.Office.Interop.Word.Application()
        Dim ResultDocName As String = NewDocName + ".pdf"

        Try
            Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(cPath + NewDocName + ".docx")
            objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", cPath), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
            objNewDoc.Close()
            'Elimina el Documento WORD ya Prellenado
            System.IO.File.Delete(cPath + NewDocName + ".docx")


            ' Se genera el PDF
            Dim Filename As String = NewDocName + ".pdf"
            Dim FilePath As String = cPath
            Dim fs As System.IO.FileStream
            fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
            Dim bytBytes(fs.Length) As Byte
            fs.Read(bytBytes, 0, fs.Length)
            fs.Close()

            'Borra el archivo creado en memoria
            DelHDFile(FilePath + Filename)
            Response.Buffer = True
            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", ResultDocName))
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(bytBytes)
            Response.End()

        Catch ex As Exception
            result = (ex.Message)
        Finally
            objNewWord.Quit()
        End Try

        objNewWord = Nothing
    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub

End Class