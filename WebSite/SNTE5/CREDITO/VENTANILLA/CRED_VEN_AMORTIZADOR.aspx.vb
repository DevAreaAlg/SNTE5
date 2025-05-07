Imports WnvWordToPdf
Public Class CRED_VEN_AMORTIZADOR
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Amortizador", "Amortizador de Préstamo")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            crea_tablas()
        End If

        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica(1)")
        ViewState("Monto") = 0.00
        ViewState("CAP_F") = 0.00
        'ViewState("INT_F") = 0.00
        'ViewState("ADELANTO") = 0.00
        ViewState("Res") = ""


        If Session("idperbusca") <> "" Then
            tbx_rfc.Text = Session("idperbusca")
            Session("idperbusca") = ""
        End If

        If Session("idperbusca_Usuario") <> Nothing Then
            Session("idperbusca_Usuario") = Nothing
        End If

        HiddenPrinterName.Value = Session("IMPRESORA_TICKET")

    End Sub

    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_seleccionar.Click
        obtieneId()
        lbl_info.Text = ""
        lbl_estatus.Text = ""
        Session("PERSONAID") = txt_IdCliente.Text
        limpia_datos()
        Llenadatos()
        limpiaforma()
        cmb_folio.Enabled = True
    End Sub

    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            lbl_alerta.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_folio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_folio.SelectedIndexChanged
        lbl_estatus.Text = ""
        'lnk_verplanpago.Visible = False
        lnk_recibo.Visible = False
        If cmb_folio.SelectedValue = -1 Then
            btn_pagar.Enabled = False
            'cmb_tipo_reduccion.Enabled = False
            lbl_alerta.Text = ""
            limpiaforma()
        Else
            llena_informacion_folio()
            btn_pagar.Enabled = True
            'cmb_tipo_reduccion.Enabled = True
        End If

    End Sub

    Protected Sub limpiaforma()
        lbl_monto_liq_txt.Text = ""
        'txt_saldo_anterior.Text = ""
        'txt_desc_pend.Text = ""
        'txt_desc_sob.Text = ""
        'txt_adelanto_pend.Text = ""
        'txt_inte_falta.Text = ""
        txt_fecha.Text = ""
        txt_monto.Text = ""
        txt_total.Text = ""
        txt_recalculado.Text = ""
        txt_descontado.Text = ""

    End Sub

    Protected Sub btn_pagar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagar.Click
        Session("RES") = "0"
        Dim sefectivo As String, efectivo As Decimal
        sefectivo = txt_cajaori.Text
        If sefectivo <> "" Then
            efectivo = CDec(sefectivo)
        Else
            efectivo = 0.0
        End If

        If validapago() = True Then
            aplica_pago()
            lbl_alerta.Text = ""
        Else
            aplica_pago()
            lbl_alerta.Text = ""
        End If

    End Sub

    Private Function validapago() As Boolean
        Dim efectivo As Decimal
        If txt_cajaori.Text = "" Then
            efectivo = 0.00
        Else
            efectivo = CDec(txt_cajaori.Text)
        End If

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 15, efectivo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_PLAN_PAGOS_PFSI"
        Session("rs") = Session("cmd").Execute()
        Dim RESULTADO As Boolean
        If Session("rs").Fields("RESPUESTA").value.ToString = "SI" Then
            RESULTADO = True
        ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "NO" Then
            RESULTADO = False
        End If
        Session("Con").Close()
        Return RESULTADO
    End Function


    Private Sub aplica_pago()

        lbl_info.Text = ""

        Dim sefectivo As String
        'Dim sadelanto As String
        'Dim sinteres As String
        Dim scapital As String

        sefectivo = txt_cajaori.Text
        'sadelanto = txt_adelanto_pend.Text
        'sinteres = txt_inte_falta.Text


        Dim efectivo As Decimal
        'Dim adelanto As Decimal
        'Dim interes As Decimal
        Dim capital As Decimal

        If sefectivo <> "" Then
            efectivo = CDec(sefectivo)
            'adelanto = CDec(sadelanto)
            'interes = CDec(sinteres)
            capital = CDec(scapital)
        Else
            efectivo = 0.0
        End If

        Dim money_in As Decimal = efectivo
        Session("efectivo") = efectivo

        If money_in > 0.00 Then
            If txt_refpago.Text = "" Then
                lbl_estatus.Text = "Error: No ha capturado referencia de pago"

            Else

                Amortizacion(efectivo, capital)
                btn_pagar.Enabled = False
                LimpiaTodo_Deposito()
                cmb_folio.Items.Clear()
            End If
        Else
            lbl_estatus.Text = "Error: Debe ingresar una cantidad mayor a 0"

        End If

    End Sub

    Private Sub Amortizacion(ByVal efectivo As Decimal, cap As Decimal)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, CInt(cmb_folio.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EFECTIVO", Session("adVarChar"), Session("adParamInput"), 20, efectivo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 20, "MONTO")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REFPAG", Session("adVarChar"), Session("adParamInput"), 20, txt_refpago.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 20, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PAGO_CREDITO"
        Session("rs") = Session("cmd").Execute()

        Session("RES") = Session("rs").Fields("RES").Value.ToString
        ViewState("TransCred") = Session("rs").Fields("CRED").Value.ToString
        ViewState("TransCap") = Session("rs").Fields("CAP").Value.ToString

        Session("Con").Close()


        ViewState("monto") = efectivo
        ViewState("CAP_F") = cap
        'ViewState("INT_F") = Int()
        'ViewState("ADELANTO") = adelanto

        'If CDec(ViewState("monto")) + CDec(ViewState("ADELANTO")) <= CDec(ViewState("CAP_F")) + CDec(ViewState("INT_F")) Then
        '    ViewState("res") = "pago_atraso"
        'Else
        ViewState("res") = ""
        'End If

        datos()

        If Session("RES") = "-2" Then
            lbl_estatus.Text = "Se intentó realizar un pago idéntico a este en un periodo de tiempo muy corto. Favor de esperar 5 minutos para realizar la operación de nuevo."
        Else 'If Session("RES") = "0" Then
            '    If lbl_monto_liq_txt.Text <= 0.00 Then
            '        ViewState("tipo") = "LIQUIDACIÓN"
            '        lbl_estatus.Text = "Guardado correctamente. Su préstamo se encuentra liquidado"
            '    Else
            '        ViewState("tipo") = "ABONO"

            '        lbl_estatus.Text = "Guardado correctamente. Descargue su nuevo plan de pagos"


            '    End If
            'ElseIf Session("RES") = "1" Then
            '    ViewState("tipo") = "LIQUIDACIÓN"
            lbl_estatus.Text = "El préstamo se ha liquidado virtualmente. Los ajustes se realizarán al final del ciclo."


        End If

        'If ViewState("tipo") = "LIQUIDACIÓN" Then
        'lnk_verplanpago.Visible = False
        lnk_recibo.Visible = True
        ' Else
        'lnk_verplanpago.Visible = True
        '_recibo.Visible = True
        'End If

    End Sub

#Region "EMAIL"


    Private Sub Motor_On_off()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, "PAGREC")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONFIGURACION_ENVIOS_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("MOTOR") = Session("rs").fields("GRANTED").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Private Sub Enviar_Correo()

        Motor_On_off()
        If Session("MOTOR") = "1" Then
            envioemail()
        End If

        Session("MOTOR") = Nothing
    End Sub

    Private Sub envioemail()
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim user As String = String.Empty
        Dim fecha As String = String.Empty
        Dim cant As String = String.Empty
        Dim cap As String = String.Empty
        Dim ord As String = String.Empty
        Dim mora As String = String.Empty
        Dim comisiones As String = String.Empty
        Dim impuestos As String = String.Empty
        Dim folio As String = String.Empty
        Dim correo As String = String.Empty
        Session("IDENVIO") = "0"

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
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_PAGO_RECIBIDO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_LOG_PAGO_RECIBIDO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            user = Session("rs").Fields("NOMBRE").Value.ToString
            Session("rs").Fields("MONTO_APLICADO").Value.ToString()
            correo = Session("rs").Fields("EMAIL").Value.ToString()
            cap = Session("rs").Fields("CAPITAL").Value.ToString()
            ord = Session("rs").Fields("ORDINARIOS").Value.ToString()
            mora = Session("rs").Fields("MORATORIOS").Value.ToString()
            Session("rs").Fields("COMISIONES").Value.ToString()

            folio = Session("rs").Fields("FOLIO").Value.ToString()
            Session("IDENVIO") = Session("rs").Fields("IDENVIO").Value.ToString
            Session("rs").Fields("FECHAPAGO_APLICADO").Value.ToString()
            subject = "Pago Recibido"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td width='25%'>Su pago ha sido recibido:</td></td></tr>")
            sbhtml.Append("<tr><td>Estimado (a):  " + user + "</td></tr>")
            sbhtml.Append("<tr><td> Le informamos que el pago realizado a su préstamo: " + folio + "ha quedado debidamente registrado </td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td width='75%'>Fecha de pago :</td><td>" + "<b>" + fecha + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Cantidad aplicada :</td>" + "<b>" + cant + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='25%'>Desglose:</td></td></tr>")
            sbhtml.Append("<tr><td width='30%'>Capital :</td>" + "<b>" + cap + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Intereses ordinarios :</td>" + "<b>" + ord + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Intereses moratorios :</td>" + "<b>" + mora + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Comisiones :</td>" + "<b>" + comisiones + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Impuestos :</td>" + "<b>" + impuestos + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='250'>Para cualquier duda o aclaración puede comunicarse a: Soporte Técnico del Sistema correspondiente.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString(), subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
        End If


        Session("Con").Close()
        If Session("IDENVIO") <> "0" Then
            actualizarEnvio()
        End If

    End Sub

    Private Sub actualizarEnvio()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDENVIO", Session("adVarChar"), Session("adParamInput"), 20, Session("IDENVIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_LOG_PAGO_RECIBIDO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub
#End Region

#Region "Recalculo"

    Private Sub Recalculo_plan()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RECALCULO_VENTANILLA_VALIDACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            If Session("rs").fields("RESPUESTA").value.ToString = "ADELANTO" Then
                ModalPopupExtender_recalculo.Show()
            Else
                ModalPopupExtender_recalculo.Hide()
            End If
        End If
        Session("Con").Close()

    End Sub

#End Region


#Region "llenado"

    Private Sub Llenainfocredito()
        Dim salAnterior As Decimal
        Dim adelanto_Ped As Decimal
        Dim descuento_Sob As Decimal
        Dim cap_Desc_prog As Decimal
        Dim inte_Falta As Decimal
        Dim cap_Falta As Decimal
        Dim montoLiquidar As Decimal
        Dim res As Integer
        Dim fecha As String
        Dim montoPres As Decimal
        Dim totalPagar As Decimal
        Dim descontado As Decimal
        Dim capintori As Decimal

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VENTANILLA_MONTO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            salAnterior = Session("rs").Fields("SALDO").Value
            adelanto_Ped = Session("rs").Fields("ADELANTOS_PENDIENTES").Value
            descuento_Sob = Session("rs").Fields("DESCUENTOS_SOBRANTES").Value
            cap_Desc_prog = Session("rs").Fields("DESCUENTO_PROG").Value
            inte_Falta = Session("rs").Fields("INT_F").Value
            cap_Falta = Session("rs").Fields("CAP_F").Value
            montoLiquidar = Session("rs").Fields("TOTAL").Value
            res = Session("rs").Fields("RES").Value
            fecha = Session("rs").Fields("FECHA_APLICACION").Value
            montoPres = Session("rs").Fields("MONTO_PRESTADO").Value
            capintori = Session("rs").Fields("CAPINT_ORI").Value
            totalPagar = Session("rs").Fields("TOTAL_PAGAR").Value
            descontado = Session("rs").Fields("DESCONTADO").Value


            'txt_saldo_anterior.Text = salAnterior.ToString("C")
            'txt_adelanto_pend.Text = adelanto_Ped.ToString("C")
            'txt_desc_sob.Text = descuento_Sob.ToString("C")
            'txt_desc_pend.Text = cap_Desc_prog.ToString("C")
            'txt_inte_falta.Text = inte_Falta.ToString("C")


            txt_fecha.Text = fecha.ToString
            txt_monto.Text = montoPres.ToString("C")
            txt_total.Text = capintori.ToString("C")
            txt_recalculado.Text = totalPagar.ToString("C")
            txt_descontado.Text = descontado.ToString("C")


            lbl_monto_liq_txt.Text = montoLiquidar.ToString("C")
            If res = 0 Then
                lbl_alerta.Text = ""
            Else
                lbl_alerta.Text = ""
            End If

        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenadatos()
        cmb_folio.Items.Clear()
        cmb_folio.Items.Add(New ListItem("ELIJA", "-1"))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FOLIOS_X_CLIENTE"
        Session("rs") = Session("cmd").Execute()
        ViewState("CLIENTE") = Session("rs").Fields("NOMBRE").Value

        lbl_cliente.Text = ViewState("CLIENTE")
        Session("PROSPECTO") = Session("rs").Fields("NOMBRE").Value

        If Session("rs").Fields("COND").Value.ToString = "0" Then
            lbl_info.Text = "El cliente no cuenta con préstamos activos"
            Session("Con").Close()
            Exit Sub
        End If
        If Session("rs").Fields("NOMBRE").Value.ToString = "-1" Then
            lbl_info.Text = "El cliente introducido no existe"
            lbl_cliente.Text = ""
            Session("Con").Close()
            Exit Sub
        End If

        Do While Not Session("rs").EOF
            cmb_folio.Items.Add(New ListItem(Session("rs").Fields("CLAVE").Value, Session("rs").Fields("FOLIO").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Private Sub datos()
        Llenainfocredito()

        borra_tablas()
        crea_tablas()

        txt_cajaori.Text = ""
        Session("FOLIO") = cmb_folio.SelectedItem.Value
        Session("CLAVE") = cmb_folio.SelectedItem.Text

    End Sub

    'Carga la información del folio en la ventana de pagos
    Private Sub llena_informacion_folio()

        If cmb_folio.SelectedItem.Value > -1 Then

            datos()

            'Mostar los datos generales de un expediente: folio, nombre de cliente y producto
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"
            Session("rs") = Session("cmd").Execute()
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value
            Session("Con").Close()

            lbl_info_disp.Text = ""


            txt_cajaori.Enabled = True

        End If


    End Sub

#End Region 'Fin de Region "llenado"

#Region "Creación Tablas y Limpiar Controles"

    Private Sub crea_tablas()
        Dim tabla_bancosori As New DataTable, tabla_chequesori As New DataTable

        tabla_bancosori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO", GetType(String))
        tabla_bancosori.Columns.Add("MONTO", GetType(Decimal))
        tabla_bancosori.Columns.Add("ID_ORIGEN", GetType(Integer))
        tabla_bancosori.Columns.Add("ORIGEN", GetType(String))
        tabla_bancosori.Columns.Add("ID_BANCO_DESTINO", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO_DESTINO", GetType(String))
        tabla_bancosori.Columns.Add("NUM_CTA_DESTINO", GetType(String))
        tabla_bancosori.Columns.Add("TITULAR_DESTINO", GetType(String))

        Session("tabla_bancosori") = tabla_bancosori

        tabla_chequesori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_chequesori.Columns.Add("ID_BANCO", GetType(Integer))
        tabla_chequesori.Columns.Add("BANCO", GetType(String))
        tabla_chequesori.Columns.Add("NUMCUENTA", GetType(String))
        tabla_chequesori.Columns.Add("CHEQUE", GetType(String))
        tabla_chequesori.Columns.Add("MONTO", GetType(Decimal))
        tabla_chequesori.Columns.Add("ESTATUS", GetType(String))

        Session("tabla_chequesori") = tabla_chequesori

    End Sub

    Private Sub borra_tablas()
        Session("tabla_bancosori").Clear()

        Session("tabla_chequesori").Clear()

    End Sub

    Private Sub limpia_datos()

        txt_cajaori.Text = ""
        btn_pagar.Enabled = False

    End Sub


    Private Sub LimpiaTodo_Deposito()

        Session("IFE") = Nothing
        Session("GUADAR_USUARIO") = Nothing
        Session("efectivo") = Nothing

        LimpiaGenerales()
        LimpiaEfectivo()
        btn_pagar.Enabled = False

        'Se recarga información de folio de préstamo
        llena_informacion_folio()

    End Sub

    Private Sub LimpiaGenerales()

        borra_tablas()
        lbl_cliente.Text = ""
    End Sub


    Private Sub LimpiaEfectivo()
        txt_cajaori.Enabled = False
        txt_cajaori.Text = ""
    End Sub

#End Region 'Fin de region "Creación Tablas y Limpiar Controles"


#Region "Validaciones"

    Function val_idPersona_existente(ByVal idpersona As Integer, idpersona_agregar As Integer) As String

        Dim Respuesta As String = ""

        Dim nombre1 As String, nombre2 As String, paterno As String, materno As String, pais As String, nac As String, fechanac As String,
           sexo As String

        'Significa que hicieron busqueda de persona
        If idpersona_agregar <> -1 Then
            nombre1 = ""
            nombre2 = ""
            materno = ""
            paterno = ""
            pais = 0
            nac = 0
            fechanac = ""
            sexo = ""

        Else
            idpersona_agregar = -1


        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 25, idpersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA_AGREGAR", Session("adVarChar"), Session("adParamInput"), 25, idpersona_agregar)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, nombre1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, nombre2)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, paterno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, materno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LUGARNAC", Session("adVarChar"), Session("adParamInput"), 50, pais)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_NAC", Session("adVarChar"), Session("adParamInput"), 50, nac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 50, fechanac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 5, sexo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPOSITO_VALIDACION_PERSONA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Respuesta = Session("rs").Fields("RESPUESTA").value
        End If

        Session("Con").Close()
        Return Respuesta
    End Function


    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Function val_rangos_Efectivo(ByVal monto As Decimal) As String
        Dim Respuesta As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 25, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPOSITO_VALIDACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Respuesta = Session("rs").Fields("RESPUESTA").value
            Session("IFE") = Session("rs").Fields("IFE").value
        End If

        Session("Con").Close()
        Return Respuesta
    End Function

#End Region 'Fin de Region "Validaciones"

#Region "Recibo"
    Protected Sub lnk_recibo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_recibo.Click
        GeneraRecibo()
    End Sub

    Private Sub GeneraRecibo()

        Dim url As String = "Recibo.DOCX"

        If Not url Like (-1).ToString Then

            Dim NewDocName As String = Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
            Dim ResultDocName As String = "ReciboAdelanto(" + CStr(Session("CLAVE")) + ").pdf"

            Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "DocPlantillas\Solicitudes\" + url)
                ReemplazarEtiquetas(worddoc)
                worddoc.SaveAs(Session("APPATH") + "\Word\" + NewDocName + ".docx")
            End Using

            Dim result As String = ""
            ' Dim objNewWord As New Microsoft.Office.Interop.Word.Application()

            Try
                Dim winKey As String = ConfigurationManager.AppSettings.[Get]("WINKEY")
                Dim wordToPdfConverter As New WordToPdfConverter()
                wordToPdfConverter.LicenseKey = winKey
                ' Dim wordToPdfConverter As New WordToPdfConverter()
                ' wordToPdfConverter.LicenseKey = "DYOSgpaRgpKClIySgpGTjJOQjJubm5uCkg=="
                wordToPdfConverter.ConvertWordFileToFile(Session("APPATH") + "\Word\" + NewDocName + ".docx", Session("APPATH") + "\Word\" + NewDocName + ".pdf")


                'Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(Session("APPATH") + "\Word\" + NewDocName + ".docx")
                'objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", Session("APPATH") + "\Word\"), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False,
                '                                       Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
                'objNewDoc.Close()
                'Elimina el Documento WORD ya Prellenado
                System.IO.File.Delete(Session("APPATH") + "\Word\" + NewDocName + ".docx")

                ' Se genera el PDF
                Dim Filename As String = NewDocName + ".pdf"
                Dim FilePath As String = Session("APPATH") + "\Word\"
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
                'objNewWord.Quit()
            End Try

            'objNewWord = Nothing

        End If


    End Sub

    Private Sub ReemplazarEtiquetas(ByRef doc As Novacode.DocX)

        Dim persona As String = ""
        Dim cve_exp As String = ""
        Dim montoCred As Decimal = 0.00
        Dim montoCredLetra As String = String.Empty
        Dim adscripcion As String = String.Empty
        Dim fecha_sis As String = ""
        Dim dia As String = ""
        Dim mes As String = ""
        Dim anio As String = ""
        Dim tipo As String = ViewState("tipo")
        Dim interes As Decimal = 0.00
        Dim interes_atr As Decimal = 0.00
        Dim categ As String = ""
        Dim tipocred As String = ""
        Dim nocontrol As Integer
        Dim clave As String = ""
        Dim RFC As String = ""

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_RECIBO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TRANSCRED", Session("adVarChar"), Session("adParamInput"), 11, ViewState("TransCred"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TRANSCAP", Session("adVarChar"), Session("adParamInput"), 11, ViewState("TransCap"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            montoCred = Session("rs").Fields("MONTO").Value.ToString()
            montoCredLetra = Session("rs").Fields("MONTO_LETRA").Value.ToString()
            adscripcion = Session("rs").Fields("ADSCRIPCION").Value.ToString()
            persona = Session("rs").Fields("NOMBRE").Value.ToString()
            cve_exp = Session("rs").Fields("FOLIO").Value.ToString()
            interes = Session("rs").Fields("INTERES").Value.ToString()
            interes_atr = Session("rs").Fields("INTERES_ABONADO_RETRASO").Value.ToString()
            dia = Session("rs").Fields("DIA").Value.ToString()
            mes = Session("rs").Fields("MES").Value.ToString()
            anio = Session("rs").Fields("ANIO").Value.ToString()
            categ = Session("rs").Fields("CATEG").Value.ToString()
            nocontrol = Session("rs").Fields("NO_CONTROL").Value.ToString()
            clave = Session("rs").Fields("CLAVE").Value.ToString()
            RFC = Session("rs").Fields("RFC").Value.ToString()
        End If

        If categ = "PA" Then
            tipocred = "AHORRO"
        ElseIf categ = "PD" Then
            tipocred = "AHORRO DEUDOR"
        End If

        Session("Con").Close()

        doc.ReplaceText("[CLIENTE]", persona, False, RegexOptions.None)
        doc.ReplaceText("[FOLIO]", clave, False, RegexOptions.None)
        doc.ReplaceText("[MONTO]", FormatCurrency(CStr(montoCred)), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_LETRA]", montoCredLetra, False, RegexOptions.None)
        doc.ReplaceText("[INTERES]", FormatCurrency(CStr(interes + interes_atr)), False, RegexOptions.None)
        doc.ReplaceText("[SECTOR]", adscripcion, False, RegexOptions.None)
        doc.ReplaceText("[TIPO]", "ABONO", False, RegexOptions.None)
        doc.ReplaceText("[DIA]", dia, False, RegexOptions.None)
        doc.ReplaceText("[MES]", mes, False, RegexOptions.None)
        doc.ReplaceText("[ANIO]", anio, False, RegexOptions.None)
        doc.ReplaceText("[TIPO_CRED]", tipocred, False, RegexOptions.None)
        doc.ReplaceText("[NO_CONTROL]", RFC, False, RegexOptions.None)
        doc.ReplaceText("[DEPENDENCIA]", adscripcion, False, RegexOptions.None)

    End Sub

#End Region

#Region "Convierte a PDF" 'IHG 2017-07-26

    Private Sub ObtenerProducto()

        'Mostar los datos generales de un expediente: folio, nombre de cliente y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"

        Session("rs") = Session("cmd").Execute()

        Session("PRODUCTO") = Session("rs").fields("PRODUCTO").value.ToString
        Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString
        Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub

#End Region

End Class