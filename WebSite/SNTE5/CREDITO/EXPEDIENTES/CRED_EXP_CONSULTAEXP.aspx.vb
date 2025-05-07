Imports WnvWordToPdf
Imports System.Data
Public Class CRED_EXP_CONSULTAEXP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Información del Agremiado", "Información del Agremiado")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Session("idperbusca") = Nothing
            Empresa()
            TipoPago()

            If Session("VENGODE") = "CRED_EXP_GENERAL" And Not Session("PERSONAID") Is Nothing Then
                txt_IdCliente.Text = Session("PERSONAID").ToString
                tbx_rfc.Text = Session("NUMTRAB").ToString
                Session("CLIENTE") = Session("PROSPECTO").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                MuestraExpedientes()
                CargaAniosAportaciones()
                MuestraAportaciones(0)
                MuestraNomina()
                MuestraPensionAlim()
            End If

            If Session("VENGODE") = "ConsultaExp.aspx" Or Session("VENGODE") = "UNI_RED_SOCIAL.aspx" Or Session("VENGODE") = "CRED_EXP_EXP_DIGITAL.aspx" And Not Session("PERSONAID") Is Nothing Then

                txt_IdCliente.Text = Session("PERSONAID").ToString
                tbx_rfc.Text = Session("NUMTRAB").ToString
                Session("CLIENTE") = Session("PROSPECTO").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                MuestraExpedientes()
                CargaAniosAportaciones()
                MuestraAportaciones(0)
                MuestraNomina()
                MuestraPensionAlim()
                'CargaAniosIncapacidades()
                'MuestraIncapacidades(0)
                'CargaAniosPermisos()
                'MuestraPermisos(0)
                pnl_expedientes.Visible = True
                stn_aportaciones.Visible = True
                stn_nomina.Visible = True
                'stn_incapacidades.Visible = True
                'stn_permisos.Visible = True
                pnl_cnfexp.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "UP")
                folderA(pnl_cnfexp, "down")

                If tipo_exp(Session("FOLIO")) = "INV" Then
                    pnl_info_Captacion.Visible = False
                    pnl_info_Credito.Visible = False
                    pnl_info_Inversion.Visible = True
                    dag_Beneficiarios.Visible = True
                    Llenabeneficiario()

                ElseIf tipo_exp(Session("FOLIO")) = "CAP" Then
                    pnl_info_Inversion.Visible = False
                    pnl_info_Credito.Visible = False
                    pnl_info_Captacion.Visible = True
                    DAG_BENE_CAPB.Visible = True
                    Llenabeneficiario_Cap()

                ElseIf tipo_exp(Session("FOLIO")) = "CRED" Then
                    pnl_info_Inversion.Visible = False
                    pnl_info_Captacion.Visible = False
                    pnl_info_Credito.Visible = True

                End If

                DetalleExpediente()

                Session("idperbusca") = Nothing
                Session("VENGODE") = Nothing
            End If
        End If

        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + lnk_Continuar.ClientID + "')")
        lnk_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            validaPersona()
        End If


        lbl_statusc.Text = ""
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptPaneles", "$('.panel_folder_toogle').click(function(event) { var folder_content=$(this).parent().siblings('.panel-body').children('.panel-body_content');if($(this).hasClass('up')){$(this).removeClass('up');$(this).addClass('down');folder_content.show('6666',null);$(this).parent().css({'background': '#696462 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px' });}else if($(this).hasClass('down')){$(this).removeClass('down');folder_content.hide('333',null);$(this).addClass('up');$(this).parent().css({ 'background': '#696462 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px'  });}});", True)
    End Sub

    Private Sub BuscarIDCliente()
        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If
        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""
            folderA(div_selCliente, "down")
            pnl_expedientes.Visible = False
            stn_aportaciones.Visible = False
            stn_nomina.Visible = False
            'stn_incapacidades.Visible = False
            'stn_permisos.Visible = False
            stn_movimientos.Visible = False
            folderA(pnl_expedientes, "up")
            pnl_cnfexp.Visible = False
            folderA(pnl_cnfexp, "up")
            sectionAhorro.Visible = False

        ElseIf Existe = -1 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de trabajador"
            lbl_NombrePersonaBusqueda.Text = ""
            folderA(div_selCliente, "down")
            pnl_expedientes.Visible = False
            stn_aportaciones.Visible = False
            stn_nomina.Visible = False
            'stn_incapacidades.Visible = False
            'stn_permisos.Visible = False
            stn_movimientos.Visible = False
            folderA(pnl_expedientes, "up")
            pnl_cnfexp.Visible = False
            folderA(pnl_cnfexp, "up")
            Session("PERSONAID") = txt_IdCliente.Text
            sectionAhorro.Visible = False
        Else
            lbl_statusc.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            MuestraExpedientes()
            CargaAniosAportaciones()
            MuestraAportaciones(0)
            MuestraNomina()
            MuestraPensionAlim()
            consultarInformacionDeCierreAgremiado()
            'CargaAniosIncapacidades()
            'MuestraIncapacidades(0)
            'CargaAniosPermisos()
            'MuestraPermisos(0)
            lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
            pnl_expedientes.Visible = True
            stn_aportaciones.Visible = True
            stn_nomina.Visible = True
            'stn_incapacidades.Visible = True
            'stn_permisos.Visible = True
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "down")
            folderA(pnl_cnfexp, "up")
            sectionAhorro.Visible = True
        End If
    End Sub

    Private Sub TipoPago()
        ' Carga cátalogo de bancos disponibles
        cmb_tipo.Items.Clear()
        cmb_tipo.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_METODO_PAGO"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_tipo.Items.Add(New ListItem(Session("rs").Fields("TEXT").Value, Session("rs").Fields("VALUE").Value))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

    Private Sub CargaTipoPago()
        Try
            Dim h As Hashtable = New Hashtable()
            h.Add("@IDPER", Session("PERSONAID").ToString)

            Dim da As New DataAccess()
            Dim a As String = da.RegresaUnaCadena("SEL_METODO_PAGO_AGREMIADO", h)

            cmb_tipo.SelectedValue = a
        Catch ex As Exception
        End Try


    End Sub

    Protected Sub guardatipo(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        guardaTipo()
    End Sub

    Private Sub guardaTipo()

        Try

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOPAGO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_MEDIO_PAGO_AHORRO"
            Session("cmd").Execute()
            lbl_status.Text = "Guardado correctamente"
        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally
            Session("Con").Close()
            CargaTipoPago()
        End Try
    End Sub

    'DBGRID Muestra documentos digitalizados
    Private Sub MuestraExpedientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_EXPEDIENTES"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtexpedientes, Session("rs"))
        dag_Expendientes.DataSource = dtexpedientes
        dag_Expendientes.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub MuestraReest()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REESTRUCTURAS_PLANPAGO"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtexpedientes, Session("rs"))
        dag_reest.DataSource = dtexpedientes
        dag_reest.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub DAG_Reest_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_reest.ItemCommand
        If (e.CommandName = "GENERAR") Then
            ViewState("FOLIO") = e.Item.Cells(0).Text
            ViewState("CVEEXP") = e.Item.Cells(1).Text
            ViewState("TRANSCRED") = e.Item.Cells(3).Text
            ViewState("TRANSCAP") = e.Item.Cells(4).Text
            ViewState("ABONOOLIQ") = e.Item.Cells(5).Text
            Session("PROSPECTO") = Session("CLIENTE")

            GeneraRecibo()

        End If
    End Sub

    Private Sub DetalleExpediente()

        lbl_folioa.Text = Session("CVEEXP")

        If tipo_exp(Session("FOLIO")) = "CRED" Then

            lnk_persona.Enabled = True
            lnk_docsexp.Enabled = True
            lnk_notas.Enabled = True
            lnk_solicitud.Enabled = True

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()


            lbl_ProductoDetalleB.Text = Session("rs").Fields("PRODUCTO").value.ToString()
            lbl_MontoB.Text = Session("rs").Fields("MONTO").value.ToString()
            Dim Plazo As Integer = Convert.ToInt32(Session("rs").Fields("PLAZO").value.ToString()) / 15
            lbl_PlazoB.Text = Plazo.ToString() + " QNA"
            lbl_TasaNormalB.Text = Session("rs").Fields("TASA_NORMAL").value.ToString()
            lbl_TasaMoraB.Text = Session("rs").Fields("TASA_MORA").value.ToString()
            lbl_fechaliberaB.Text = Session("rs").Fields("AUX_FECHA_LIBERA").value.ToString()
            lbl_estatusB.Text = Session("rs").fields("ESTATUS").value.ToString()
            lbl_fechaV.Text = Session("rs").fields("FECHA_EST_VENC").value.ToString()


            Session("CLASIFICACION") = Session("rs").Fields("CLASIFICACION").value.ToString
            Session("TIPOPLANPAGO") = Session("rs").Fields("TIPOPLAN").value.ToString()
            Select Case Session("TIPOPLANPAGO")
                Case "SI"
                    lbl_tipoplanB.Text = "SALDOS INSOLUTOS"
                    lnk_PlanPagos.Visible = True

                Case "PFSI"
                    lbl_tipoplanB.Text = "PAGOS FIJOS SI"
                    lnk_PlanPagos.Visible = True

                Case "ES"
                    lbl_tipoplanB.Text = "PLAN ESPECIAL"
                    If Session("CLASIFICACION") = "PLAN ESPECIAL" Then
                        lnk_PlanPagos.Visible = True

                    ElseIf Session("CLASIFICACION") = "CUENTA CORRIENTE" Then

                        lnk_PlanPagos.Visible = True

                    End If
                Case Else
                    lbl_tipoplanB.Text = ""
                    lnk_PlanPagos.Visible = False

            End Select

            If Session("rs").Fields("CONTRATO_SIA").Value.ToString <> "" Then

                lbl_folioSIA.Visible = True
                Label3.Visible = True
                lbl_folioSIA.Text = Session("rs").Fields("CONTRATO_SIA").Value.ToString
            Else

                lbl_folioSIA.Visible = False
                Label3.Visible = False

            End If

            Session("Con").Close()

        ElseIf tipo_exp(Session("FOLIO")) = "INV" Or tipo_exp(Session("FOLIO")) = "INVPER" Then


            lnk_persona.Enabled = True
            lnk_docsexp.Enabled = True
            lnk_notas.Enabled = True
            lnk_solicitud.Enabled = True

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_EXP_INV_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                lbl_num_exp_InvB.Text = Session("FOLIO").ToString
                lbl_nom_prod_InvB.Text = Session("rs").Fields("NOMBRE_PRODUCTO").value.ToString()

                Lbl_Fechas_InvB.Text = "Del " + Session("rs").Fields("INICIO_INVERSION").value.ToString().Substring(0, 10) + " Al " + Session("rs").Fields("VENCIMIENTO_INVERSION").value.ToString().Substring(0, 10)
                Lbl_Fecha_Ultimo_Pago_InvB.Text = Session("rs").Fields("FECHA_PAGO").value.ToString()
                lbl_Plazo_INVB.Text = Session("rs").fields("PLAZO").value.ToString()
                lbl_status_INVB.Text = Session("rs").fields("ESTATUS").value.ToString()
                Lbl_Monto_intgen_InvB.Text = FormatCurrency(Session("rs").fields("INTERES_GENERADO").value.ToString())
                Lbl_monto_InvB.Text = FormatCurrency(Session("rs").Fields("INV_MONTO").value.ToString())
                If Session("rs").Fields("TASA_ESPECIAL").value.ToString() = 1 Then

                    lbl_tasa_InvB.Text = Session("rs").Fields("TASA").value.ToString() + " *Tasa Especial"
                Else
                    lbl_tasa_InvB.Text = Session("rs").Fields("TASA").value.ToString()

                End If

                lbl_TasaNormalB.Text = Session("rs").Fields("TASA").value.ToString()
                lbl_fechaliberaB.Text = Session("rs").Fields("FECHA_LIBERACION").value.ToString()
                lbl_inst_InvB.Text = Session("rs").Fields("INST_INV").value.ToString()

                If Session("rs").Fields("CONTRATO_SIA").Value.ToString <> "" Then

                    lbl_num_cte_SIA_InvA.Visible = True
                    lbl_num_cte_SIA_InvB.Visible = True
                    lbl_num_cte_SIA_InvB.Text = Session("rs").Fields("CONTRATO_SIA").Value.ToString

                End If

                If Session("rs").Fields("CONTRATO_SIA").Value.ToString = 0 Then

                    lbl_num_cte_SIA_InvA.Visible = False
                    lbl_num_cte_SIA_InvB.Visible = False

                End If

            End If
            Session("Con").Close()


        ElseIf tipo_exp(Session("FOLIO")) = "CAP" Then
            lnk_persona.Enabled = True
            lnk_docsexp.Enabled = True
            lnk_notas.Enabled = True
            lnk_solicitud.Enabled = True

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE_CAP"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                lbl_exp_CapB.Text = Session("FOLIO")
                lbl_producto_CapB.Text = Session("rs").fields("PRODUCTO").value.ToString()
                lbl_fecha_dep_CapB.Text = Session("rs").fields("FECHA_ULT_DEP").value.ToString()
                lbl_fecha_Retiro_CapB.Text = Session("rs").fields("FECHA_ULT_RET").value.ToString()
                lbl_Saldo_Actual_CapB.Text = Session("rs").fields("SALDO_ACTUAL").value.ToString()
                lbl_plazo_CapB.Text = Session("rs").fields("PLAZO").value.ToString()
                lbl_tasa_CapB.Text = Session("rs").fields("TASA").value.ToString()
                lbl_statusCAPB.Text = Session("rs").fields("ESTATUS").value.ToString()
                If Session("rs").Fields("CONTRATO_SIA").Value.ToString <> "" Then

                    lbl_num_sia_CapA.Visible = True
                    lbl_num_sia_CapB.Visible = True
                    lbl_num_sia_CapB.Text = Session("rs").Fields("CONTRATO_SIA").Value.ToString

                End If

                If Session("rs").Fields("CONTRATO_SIA").Value.ToString = 0 Then

                    lbl_num_sia_CapA.Visible = False
                    lbl_num_sia_CapB.Visible = False

                End If

            End If
            Session("Con").Close()
        End If
    End Sub

    Function tipo_exp(ByVal folio As Integer) As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIPO_EXP"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("CLAVE").value.ToString
        Session("Con").Close()
        Return tipo
    End Function

    Private Sub mostrar_datos_exp(ByVal Tipo As String)
        If Tipo = "INV" Then
            lbl_folio.Visible = True
            Label3.Visible = True
            lbl_ProductoDetalleA.Visible = False
            lbl_MontoA.Visible = False
            lbl_PlazoA.Visible = False
            lbl_TasaNormalA.Visible = False
            lbl_TasaMoraA.Visible = False
            lbl_fechaliberaA.Visible = False
            lbl_tipoplanA.Visible = False
            lbl_estatus.Visible = False

            lbl_folioa.Visible = True
            lbl_folioSIA.Visible = True
            lbl_ProductoDetalleB.Visible = False
            lbl_MontoB.Visible = False
            lbl_PlazoB.Visible = False
            lbl_TasaNormalB.Visible = False
            lbl_TasaMoraB.Visible = False
            lbl_fechaliberaB.Visible = False
            lbl_tipoplanB.Visible = False
            lbl_estatusB.Visible = False

        ElseIf Tipo = "CAP" Then

        ElseIf Tipo = "CRED" Then

        End If

    End Sub

    Private Sub consultarInformacionDeCierreAgremiado(Optional ByVal plaza_agremiado As String = "")

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Dim variables As String = ""
        Dim contador As Integer = 0

        Dim num_control As String = String.Empty
        Dim saldo_ahorro As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim tipo_pago As String = String.Empty ''13
        Dim clave_rastreo As String = String.Empty ''16
        Dim num_paquete As String = String.Empty ''17
        Dim fecha_cveRastreo As String = String.Empty ''18
        Dim Estatus_SPEI As String = String.Empty ''15

        Dim contador2 As Integer = 0

        Try


            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_INFO_DE_CIERRE_CICLO_AGREMIADO"
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            If plaza_agremiado <> "" Then
                Session("parm") = Session("cmd").CreateParameter("TIPO_PLAZA", Session("adVarChar"), Session("adParamInput"), 20, plaza_agremiado)
                Session("cmd").Parameters.Append(Session("parm"))
            End If

            Session("rs") = Session("cmd").Execute()

            'se agregan los expedientes a una tabla en memoria
            custDA.Fill(dtAnalisis, Session("rs"))

            'se vacian los expedientes al formulario
            DAG_Analisis.DataSource = dtAnalisis
            DAG_Analisis.DataBind()

        Catch ex As Exception

        Finally
            Session("Con").Close()
        End Try

        Try

            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then

                    tipo_pago = Fila.Cells(12).Text.ToString() ''13
                    clave_rastreo = Fila.Cells(16).Text.ToString() ''16
                    num_paquete = Fila.Cells(17).Text.ToString() ''17
                    fecha_cveRastreo = Fila.Cells(18).Text.ToString() ''18
                    Estatus_SPEI = Fila.Cells(15).Text.ToString() ''15

                    If tipo_pago.Equals("CHQ") Then


                    ElseIf tipo_pago.Equals("SPEI") Then

                    ElseIf tipo_pago.Equals("SIN ASIGNAR") Then

                    End If



                    If Fila.Cells(8).Text.ToString().Equals("&nbsp;") Or Fila.Cells(8).Text.ToString().Equals("") Or Fila.Cells(8).Text.ToString().Equals("0") Then

                    Else

                    End If

                Else

                    Exit Sub
                End If
            Next



        Catch ex As Exception

        Finally

            Try

                If no_cheque <> "" Then

                End If

            Catch ex As Exception

            End Try

        End Try

    End Sub
    Private Sub DAG_Expedientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Expendientes.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            Session("FOLIO") = e.Item.Cells(0).Text
            Session("CVEEXP") = e.Item.Cells(1).Text
            Session("PRODUCTO") = e.Item.Cells(2).Text
            Session("PROSPECTO") = Session("CLIENTE")

            If tipo_exp(Session("FOLIO")) = "INV" Then

                pnl_info_Captacion.Visible = False
                pnl_info_Credito.Visible = False
                pnl_info_Inversion.Visible = True
                dag_Beneficiarios.Visible = True
                Llenabeneficiario()

            ElseIf tipo_exp(Session("FOLIO")) = "CAP" Then

                pnl_info_Inversion.Visible = False
                pnl_info_Credito.Visible = False
                pnl_info_Captacion.Visible = True
                DAG_BENE_CAPB.Visible = True
                Llenabeneficiario_Cap()

            ElseIf tipo_exp(Session("FOLIO")) = "CRED" Then
                pnl_info_Inversion.Visible = False
                pnl_info_Captacion.Visible = False
                pnl_info_Credito.Visible = True
                MuestraReest()

            End If
            pnl_cnfexp.Visible = True
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "up")
            folderA(pnl_cnfexp, "down")

            DetalleExpediente()
            stn_movimientos.Visible = True
            CargaAniosMovimientos()
            CargaEstatusMovimientos()
            MuestraMovimientos(0, -1)
            ResumenMovimientos()
            verificafacultad()


            lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")

        End If
    End Sub

    Protected Sub lnk_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Continuar.Click
        validaPersona()
        CargaTipoPago()
    End Sub

    Private Sub validaPersona()
        lbl_statusc.Text = ""
        dgd_movimientos.DataSource = Nothing
        dgd_movimientos.DataBind()
        stn_movimientos.Visible = False
        pnl_cnfexp.Visible = False
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: Validar el RFC debido a que el agremiado no existe en la base de datos o es incorrecto."
            Else
                Session("CLIENTE") = Session("PROSPECTO")
                Session("PROSPECTO") = Nothing
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString

                div_NombrePersonaBusqueda.Visible = True
            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()

        End If
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
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text

        End If

        Session("Con").Close()

    End Sub

    'Obtengo el nombre de la empresa
    Private Sub Empresa()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_EMPRESA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("EMPRESA") = Session("rs").fields("RAZON").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Private Sub Limpiavariables()
        Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona
        Session("PROSPECTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("TIPOPER") = Nothing
        Session("TASA") = Nothing
        Session("INDICE") = Nothing
        Session("MONTO") = Nothing
        Session("PLAZO") = Nothing
        Session("UPLAZO") = Nothing
        Session("PERIODO") = Nothing
        Session("UPERIODO") = Nothing
        Session("FECHA") = Nothing
        Session("OPCION") = Nothing
        Session("CLASIFICACION") = Nothing
        Session("TIPOPLANPAGO") = Nothing
        Session("CAT") = Nothing
        Session("OPCION") = Nothing
        Session("C_COBRO") = Nothing
        Session("C_TIEMPO") = Nothing
        Session("C_TOTAL") = Nothing
        Session("C_IVA") = Nothing
    End Sub

    Protected Sub lnk_docsexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_docsexp.Click
        Session("VENGODE") = "CRED_EXP_EXP_DIGITAL.aspx"
        Response.Redirect("CRED_EXP_EXP_DIGITAL.aspx")
    End Sub

    '------------------------------------PLANES DE PAGOS-----------------------------------
    Protected Sub lnk_PlanPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_PlanPagos.Click
        Session("VENGODE") = "CRED_EXP_EXP_DIGITAL.aspx"
        Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
    End Sub

    Protected Sub btn_cancelar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar1.Click
        ModalPopupExtender_bloqueo.Hide()
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click

        opcionbloqueo()

        If Session("OPCION") = "B" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("OPCION", Session("adVarChar"), Session("adParamInput"), 10, Session("OPCION"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_BLOQUEO_EXPEDIENTES"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                If Session("rs").fields("RESPUESTA").value.ToString = 0 Then
                    lbl_status_modal.Text = "Error: No puede bloquear el expediente, verifique el estatus del mismo"
                Else
                    lbl_status_modal.Text = "Se ha bloqueado el expediente"
                End If
            End If
            Session("Con").Close()

        Else 'DESBLOQUEAR

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("OPCION", Session("adVarChar"), Session("adParamInput"), 10, Session("OPCION"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_BLOQUEO_EXPEDIENTES"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                If Session("rs").fields("RESPUESTA").value.ToString = 0 Then
                    lbl_status_modal.Text = "Error: No puede desbloquear el expediente, verifique el estatus del mismo"
                Else
                    lbl_status_modal.Text = "Se ha desbloqueado el expediente"
                End If
            End If
            Session("Con").Close()
        End If
        limpiamodal()
        DetalleExpediente()
        MuestraExpedientes()
        CargaAniosAportaciones()
        MuestraAportaciones(0)
        MuestraNomina()
        MuestraPensionAlim()
        'CargaAniosIncapacidades()
        'MuestraIncapacidades(0)
        ModalPopupExtender_bloqueo.Show()
        opcionbloqueo()

    End Sub

    Private Sub verificafacultad()

        Dim facultad As Integer

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_FACULTAD_BLOQUEO_EXPEDIENTE"
        Session("rs") = Session("cmd").Execute()

        facultad = Session("rs").fields("FACULTAD").VALUE.ToString

        Session("Con").close()

    End Sub

    Private Sub opcionbloqueo()
        Session("OPCION") = Nothing

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPCION_BLOQUEO_EXPEDIENTE"
        Session("rs") = Session("cmd").Execute()
        Session("OPCION") = Session("rs").Fields("GRANTED").Value.ToString
        Session("Con").Close()

        If Session("OPCION") = "D" Then
            Me.btn_guardar.Text = "DESBLOQUEAR"
        Else
            Me.btn_guardar.Text = "BLOQUEAR"
        End If

    End Sub

    Private Sub limpiamodal()
        Session("OPCION") = Nothing
    End Sub

    Private Sub Llenabeneficiario_Cap()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtbeneficiario As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_BENEFICIARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtbeneficiario, Session("rs"))
        DAG_BENE_CAPB.DataSource = dtbeneficiario
        DAG_BENE_CAPB.DataBind()
        DAG_BENE_CAPB.Visible = True
        Session("Con").Close()
    End Sub

    Private Sub Llenabeneficiario()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtbeneficiario As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_BENEFICIARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtbeneficiario, Session("rs"))
        dag_Beneficiarios.DataSource = dtbeneficiario
        dag_Beneficiarios.DataBind()

        Session("Con").Close()
    End Sub

    Protected Sub lnk_notas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_notas.Click

        ModalPopupExtender1.Show()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_CNFEXP_VALIDADOR_NOTAS]"
        Session("rs") = Session("cmd").Execute()

        Dim nota As String = ""
        Do While Not Session("rs").EOF

            nota = nota + vbCrLf + Session("rs").Fields("TIPO").Value.ToString + vbCrLf + Session("rs").Fields("NOTAS").Value.ToString + vbCrLf
            Session("rs").movenext()

        Loop
        lbl_notasexp.Text = nota

        Session("Con").Close()

    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        ModalPopupExtender1.Hide()
        lbl_notasexp.Text = ""
    End Sub

    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)

        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#696462 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#696462 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion

    End Sub

    Protected Sub lnk_garantias(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busqueda_gtia.Click
        Response.Redirect("CRED_EXP_GARANTIA.ASPX")
    End Sub

#Region "Recibo"
    Private Sub GeneraRecibo()

        Dim url As String = "Recibo.DOCX"

        If Not url Like (-1).ToString Then

            Dim NewDocName As String = Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
            Dim ResultDocName As String = "ReciboAdelanto(" + CStr(Session("CVEEXP")) + ").pdf"

            Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "DocPlantillas\Solicitudes\" + url)
                ReemplazarEtiquetas(worddoc)
                worddoc.SaveAs(Session("APPATH") + "\Word\" + NewDocName + ".docx")
            End Using

            Dim result As String = ""

            Try

                Dim wordToPdfConverter As New WordToPdfConverter()
                wordToPdfConverter.LicenseKey = "DYOSgpaRgpKClIySgpGTjJOQjJubm5uCkg=="
                wordToPdfConverter.ConvertWordFileToFile(Session("APPATH") + "\Word\" + NewDocName + ".docx", Session("APPATH") + "\Word\" + NewDocName + ".pdf")

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

            End Try

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
        Dim tipo As String = ViewState("ABONOOLIQ")
        Dim interes As Decimal = 0.00
        Dim interes_atr As Decimal = 0.00
        Dim categ As String = ""
        Dim tipocred As String = ""
        Dim nocontrol As Integer

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_RECIBO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, ViewState("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TRANSCRED", Session("adVarChar"), Session("adParamInput"), 11, ViewState("TRANSCRED"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TRANSCAP", Session("adVarChar"), Session("adParamInput"), 11, ViewState("TRANSCAP"))
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
        End If

        If categ = "PC" Or categ = "PD" Then
            tipocred = "DE AHORRRO"
        ElseIf categ = "PC" Then
            tipocred = "COMPLEMENTARIO"
        End If

        Session("Con").Close()

        doc.ReplaceText("[CLIENTE]", persona, False, RegexOptions.None)
        doc.ReplaceText("[FOLIO]", cve_exp, False, RegexOptions.None)
        doc.ReplaceText("[MONTO]", FormatCurrency(CStr(montoCred)), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_LETRA]", montoCredLetra, False, RegexOptions.None)
        doc.ReplaceText("[INTERES]", FormatCurrency(CStr(interes + interes_atr)), False, RegexOptions.None)
        doc.ReplaceText("[SECTOR]", adscripcion, False, RegexOptions.None)
        doc.ReplaceText("[TIPO]", tipo, False, RegexOptions.None)
        doc.ReplaceText("[DIA]", dia, False, RegexOptions.None)
        doc.ReplaceText("[MES]", mes, False, RegexOptions.None)
        doc.ReplaceText("[ANIO]", anio, False, RegexOptions.None)
        doc.ReplaceText("[TIPO_CRED]", tipocred, False, RegexOptions.None)
        doc.ReplaceText("[NO_CONTROL]", nocontrol, False, RegexOptions.None)
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

#Region "Aportaciones"

    Private Sub MuestraAportaciones(ByVal Anio As Integer)

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtaportaciones As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, Anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_APORTACIONES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtaportaciones, Session("rs"))
        ViewState("dtaportaciones") = dtaportaciones
        Session("Con").Close()

        If dtaportaciones.Rows.Count > 0 Then

            dgd_aportaciones.DataSource = dtaportaciones
            dgd_aportaciones.DataBind()
            dgd_aportaciones.Visible = True
            InformacionAportaciones("SIMPLE")

        Else

            dgd_aportaciones.DataSource = Nothing
            dgd_aportaciones.DataBind()
            dgd_aportaciones.Visible = False
            ViewState("dtCalendars") = Nothing

        End If

    End Sub

    Private Sub CargaAniosAportaciones()

        ddl_anios_aportaciones.Items.Clear()
        Dim elija As New ListItem("ELIJA", "")
        ddl_anios_aportaciones.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_APORTACIONES_ANIOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ANIO").Value.ToString, Session("rs").Fields("ANIO").Value.ToString)
            ddl_anios_aportaciones.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub ddl_anios_aportaciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_anios_aportaciones.SelectedIndexChanged

        If ddl_anios_aportaciones.SelectedItem.Value = "" Then
            MuestraAportaciones(0)
        Else
            MuestraAportaciones(ddl_anios_aportaciones.SelectedItem.Value)
        End If

    End Sub

    Private Sub InformacionAportaciones(ByVal Tipo As String)

        If Tipo = "SIMPLE" Then

            dgd_aportaciones.Columns(4).Visible = False
            dgd_aportaciones.Columns(5).Visible = False

        Else

            dgd_aportaciones.Columns(4).Visible = True
            dgd_aportaciones.Columns(5).Visible = True

        End If

        dgd_aportaciones.DataSource = ViewState("dtaportaciones")
        dgd_aportaciones.DataBind()

    End Sub

    Protected Sub btn_info_aportaciones_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_info_aportaciones.Click

        If dgd_aportaciones.Columns(4).Visible = False Then
            InformacionAportaciones("COMPLETA")
            btn_info_aportaciones.Text = "Ocultar Porcentajes"
        Else
            InformacionAportaciones("SIMPLE")
            btn_info_aportaciones.Text = "Mostrar Porcentajes"
        End If

    End Sub

    Protected Sub dgd_aportaciones_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles dgd_aportaciones.PageIndexChanging

        dgd_aportaciones.PageIndex = e.NewPageIndex
        dgd_aportaciones.DataSource = ViewState("dtaportaciones")
        dgd_aportaciones.DataBind()

    End Sub

#End Region

#Region "Nomina"
    Private Sub MuestraNomina()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtnomina As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_NOMINA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtnomina, Session("rs"))
        ViewState("dtnomina") = dtnomina
        Session("Con").Close()

        If dtnomina.Rows.Count > 0 Then

            dag_nomina.DataSource = dtnomina
            dag_nomina.DataBind()
            dag_nomina.Visible = True
            'InformacionAportaciones("SIMPLE")

        Else

            dag_nomina.DataSource = Nothing
            dag_nomina.DataBind()
            dag_nomina.Visible = False
            'ViewState("dtCalendars") = Nothing

        End If

    End Sub
#End Region

#Region "Pensiones"
    Private Sub MuestraPensionAlim()

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtpension As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INFO_PENSION_ALIMENTICIA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtpension, Session("rs"))
        ViewState("dtpension") = dtpension
        Session("Con").Close()

        If dtpension.Rows.Count > 0 Then
            pnl_pensiones.Visible = True
            dag_pensiones.DataSource = dtpension
            dag_pensiones.DataBind()
            dag_pensiones.Visible = True
            'InformacionAportaciones("SIMPLE")

        Else
            pnl_pensiones.Visible = False
            dag_pensiones.DataSource = Nothing
            dag_pensiones.DataBind()
            dag_pensiones.Visible = False
            'ViewState("dtCalendars") = Nothing

        End If

    End Sub
#End Region

#Region "Movimientos"

    Private Sub MuestraMovimientos(ByVal Anio As Integer, ByVal Estatus As Integer)

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dtmovimientos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("cmd").CommandTimeout = 1000000
        Session("parm") = Session("cmd").CreateParameter("IDFOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, Anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, Estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_MOVIMIENTOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtmovimientos, Session("rs"))
        ViewState("dtmovimientos") = dtmovimientos
        Session("Con").Close()

        If dtmovimientos.Rows.Count > 0 Then

            dgd_movimientos.DataSource = dtmovimientos
            dgd_movimientos.DataBind()
            dgd_movimientos.Visible = True

        Else

            dgd_movimientos.DataSource = Nothing
            dgd_movimientos.DataBind()
            dgd_movimientos.Visible = False
            ViewState("dtmovimientos") = Nothing

        End If

    End Sub

    Private Sub CargaAniosMovimientos()

        ddl_anios_movimiento.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        ddl_anios_movimiento.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDFOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_MOVIMIENTOS_ANIOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ANIO").Value.ToString, Session("rs").Fields("ANIO").Value.ToString)
            ddl_anios_movimiento.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CargaEstatusMovimientos()

        ddl_estatus_movimiento.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_estatus_movimiento.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDFOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_MOVIMIENTOS_ESTATUS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ESTATUS").Value.ToString, Session("rs").Fields("IDESTATUS").Value.ToString)
            ddl_estatus_movimiento.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub ddl_anios_movimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_anios_movimiento.SelectedIndexChanged

        MuestraMovimientos(ddl_anios_movimiento.SelectedItem.Value, ddl_estatus_movimiento.SelectedItem.Value)

    End Sub

    Protected Sub ddl_estatus_movimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_estatus_movimiento.SelectedIndexChanged

        MuestraMovimientos(ddl_anios_movimiento.SelectedItem.Value, ddl_estatus_movimiento.SelectedItem.Value)

    End Sub

    Protected Sub dgd_movimientos_pageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles dgd_movimientos.PageIndexChanging

        dgd_movimientos.PageIndex = e.NewPageIndex
        dgd_movimientos.DataSource = ViewState("dtmovimientos")
        dgd_movimientos.DataBind()

    End Sub

    Private Sub ResumenMovimientos()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_MOVIMIENTOS_RESUMEN"
        Session("rs") = Session("cmd").Execute()

        txt_pagadas.Text = Session("rs").Fields("DESCUENTOS_CONFIRMADOS").value.ToString()
        txt_montoPagado.Text = Session("rs").Fields("MONTO_PAGADO").value.ToString()
        txt_incompletas.Text = Session("rs").Fields("DESCUENTOS_INCOMPLETOS").value.ToString()
        'txt_montoAtrasado.Text = Session("rs").Fields("MONTO_ATRASADO").value.ToString()
        txt_atrasadas.Text = Session("rs").Fields("DESCUENTOS_ATRASADOS").value.ToString()
        'txt_montoxPagar.Text = Session("rs").Fields("MONTO_FALTANTE_PLAN_PAGOS").value.ToString()
        txt_faltantes.Text = Session("rs").fields("DESCUENTOS_FALTANTES").value.ToString()
        'txt_montoxLiquidar.Text = Session("rs").fields("MONTO_LIQUIDACION").value.ToString()

        Session("Con").Close()

    End Sub

#End Region



#Region "Generar Solicitud"
    Protected Sub lnk_solicitud_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_solicitud.Click
        validaSolicitud()
    End Sub

    Private Sub validaSolicitud()

        Dim destino As String = ""
        Dim estatustrab As String = ""
        Dim claveprod As String = ""
        Dim nombreprod As String = ""
        Dim renoProd As Integer = 0
        'OBTENER EL COUNT TIPO PAGO CAP
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CONF_PRELLENADO_CARTA"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            estatustrab = Session("rs").Fields("ESTATUSTRAB").Value.ToString()
            destino = Session("rs").Fields("DESTINO").Value.ToString()
            claveprod = Session("rs").Fields("CLAVEPROD").Value.ToString()
            nombreprod = Session("rs").Fields("NOMBREPROD").Value.ToString()
            renoProd = Session("rs").Fields("RESTRUCTURA").Value.ToString()
        End If
        Session("Con").Close()

        generadDocs(claveprod, estatustrab, destino, renoProd)

    End Sub

    Private Sub generadDocs(ByVal cveProd As String, ByVal estatusTrab As String, ByVal destino As String, ByVal renoProd As Integer)

        Dim extencion As String = ".DOCX"

        'If renoProd = 0 Then
        '    extencion = ".DOCX"
        'Else
        '    extencion = "_RENO.DOCX"
        'End If

        Dim url As String = cveProd + "_" + estatusTrab + "_" + destino + extencion

        If Not url Like (-1).ToString Then

            Dim NewDocName As String = cveProd + "_" + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
            Dim ResultDocName As String

            If renoProd = 0 Then
                ResultDocName = cveProd + "_" + estatusTrab + "_" + destino + "(" + CStr(Session("CVEEXPE")) + ").pdf"
            Else
                ResultDocName = cveProd + "_" + estatusTrab + "_" + destino + "_RENO(" + CStr(Session("CVEEXPE")) + ").pdf"
            End If


            Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "\DocPlantillas\Solicitudes\" + url)
                ReemplazarEtiquetasSol(worddoc)
                worddoc.SaveAs(Session("APPATH") + "\Word\" + NewDocName + extencion)
            End Using

            Dim result As String = ""
            'Dim objNewWord As New Microsoft.Office.Interop.Word.Application()

            Try
                Dim winKey As String = ConfigurationManager.AppSettings.[Get]("WINKEY")
                Dim wordToPdfConverter As New WordToPdfConverter()
                wordToPdfConverter.LicenseKey = winKey
                wordToPdfConverter.ConvertWordFileToFile(Session("APPATH") + "\Word\" + NewDocName + extencion, Session("APPATH") + "\Word\" + NewDocName + ".pdf")

                'Elimina el Documento WORD ya Prellenado
                System.IO.File.Delete(Session("APPATH") + "\Word\" + NewDocName + extencion)

                ' Se genera el PDF
                Dim Filename As String = NewDocName + ".pdf"
                Dim FilePath As String = Session("APPATH") + "\Word\"
                Dim fs As System.IO.FileStream
                fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
                Dim bytBytes(fs.Length) As Byte
                fs.Read(bytBytes, 0, fs.Length)
                fs.Close()

                'se va a actualzar?
                'ActualizaExtatusContratoImpreso()

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

    Private Sub ReemplazarEtiquetasSol(ByRef doc As Novacode.DocX)
        Dim anio_factor As Integer = 0
        Dim persona As String = String.Empty
        Dim nomprod As String = String.Empty
        Dim dia As String = String.Empty
        Dim mes As String = String.Empty
        Dim año As String = String.Empty
        Dim cve_exp As String = String.Empty
        Dim montoCred As Decimal = 0.00
        Dim montoCredLetra As String = String.Empty
        Dim montoMaxCred As Decimal = 0.00
        Dim montoMaxCredLetra As String = String.Empty
        Dim direccion As String = String.Empty
        Dim calle As String = String.Empty
        Dim numInt As String = String.Empty
        Dim numExt As String = String.Empty
        Dim poblacion As String = String.Empty
        Dim estadoTrab As String = String.Empty
        Dim muniTrab As String = String.Empty
        Dim tipoProd As String = String.Empty
        Dim tipoCred As String = String.Empty
        Dim adscripcion As String = String.Empty
        Dim seguro As Decimal = 0.00
        Dim monto_transf As Decimal = 0.00
        Dim pagoFijo As Decimal = 0.00
        Dim adeudo As Decimal = 0.00
        Dim pagoFijo_Letra As String = ""
        Dim PagoQuin As Decimal = 0.00
        Dim pagoMen As Decimal = 0.00
        Dim pagoAnual As Decimal = 0.00
        Dim costoAn As Decimal = 0.00
        Dim costoSeg As Decimal = 0.00
        Dim costoQuin As Decimal = 0.00
        Dim porcentaje_seg As String = ""
        Dim fecha_sis As String = ""
        Dim sueldo_quin As String = ""
        Dim saldo_ant As Decimal = 0.00
        Dim cve_exp_rigen As String = ""
        Dim monto_transf_reno As Decimal = 0.00
        Dim nom_suc As String = ""
        Dim curp As String = ""
        Dim rfc As String = ""
        Dim clabeBanco As String = ""
        Dim banco As String = ""
        Dim fnac As String = ""
        Dim edad As String = ""
        Dim sexo As String = ""
        Dim edociv As String = ""
        Dim int_sal_ant As Decimal = 0.00
        Dim telefono As String = String.Empty
        Dim emailTrab As String = String.Empty
        Dim cargoTrab As String = String.Empty
        Dim numCtrl As String = String.Empty
        Dim numPagos As String = String.Empty
        Dim nomInst As String = String.Empty
        Dim iniOpe As String = String.Empty
        Dim intereses As Decimal = 0.0

        Dim cct As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty

        Dim categoria As String = String.Empty
        Dim anios_servicio As String = String.Empty

        Dim nomAval As String = String.Empty
        Dim direccionAval As String = String.Empty
        Dim calleAval As String = String.Empty
        Dim numIntAval As String = String.Empty
        Dim numExtAval As String = String.Empty
        Dim poblacionAval As String = String.Empty
        Dim estadoAval As String = String.Empty
        Dim muniAval As String = String.Empty


        Dim medio_pago As String = String.Empty
        Dim val1 As String = String.Empty
        Dim medio As String = String.Empty
        Dim val2 As String = String.Empty

        'PARA GARANTES
        Dim nomGarante1 As String = String.Empty
        Dim sexoGarante1 As String = String.Empty
        Dim edadGarante1 As String = String.Empty
        Dim rfcGarante1 As String = String.Empty
        Dim domicilioGarante1 As String = String.Empty
        Dim coloniaGarante1 As String = String.Empty
        Dim estadoGarante1 As String = String.Empty
        Dim telGarante1 As String = String.Empty
        Dim edocivilGarante1 As String = String.Empty
        Dim nomconyugeGarante1 As String = String.Empty
        Dim valorcatGarante1 As String = String.Empty
        Dim avaluocomGarante1 As String = String.Empty


        Dim nomGarante2 As String = String.Empty
        Dim sexoGarante2 As String = String.Empty
        Dim edadGarante2 As String = String.Empty
        Dim rfcGarante2 As String = String.Empty
        Dim domicilioGarante2 As String = String.Empty
        Dim coloniaGarante2 As String = String.Empty
        Dim estadoGarante2 As String = String.Empty
        Dim telGarante2 As String = String.Empty
        Dim edocivilGarante2 As String = String.Empty
        Dim nomconyugeGarante2 As String = String.Empty
        Dim valorcatGarante2 As String = String.Empty
        Dim avaluocomGarante2 As String = String.Empty
        Dim finalidadCred As String = String.Empty

        Dim quincenaPagar As String = String.Empty
        Dim anioPagar As Integer = 0
        Dim numcontrol_aval As String = String.Empty
        Dim conyuge As String = String.Empty

        ' SE OBTIENEN LOS DATOS PARA LAS SOLICITUDES DEL PRELLENADO
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_SOLICITUDES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            persona = Session("rs").Fields("NOMBRE").Value.ToString()
            dia = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2)
            mes = Session("rs").Fields("MES").Value.ToString()
            año = Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
            año = año.Replace("/", "")
            cve_exp = Session("rs").Fields("CVE_EXP").Value.ToString()

            calle = Session("rs").Fields("CALLE").Value.ToString()
            numExt = Session("rs").Fields("NUMEXT").Value.ToString()
            numInt = Session("rs").Fields("NUMINT").Value.ToString()

            curp = Session("rs").Fields("CURP").Value.ToString()
            rfc = Session("rs").Fields("RFC").Value.ToString()

            fnac = Session("rs").Fields("FNAC").Value.ToString()
            fnac = fnac.Replace("/", "")
            edad = Session("rs").Fields("EDAD").Value.ToString()
            sexo = Session("rs").Fields("SEXO").Value.ToString()
            edociv = Session("rs").Fields("EDOCIV").Value.ToString()
            conyuge = Session("rs").Fields("CONYUGE").Value.ToString()

            cct = Session("rs").Fields("CCT").Value.ToString()
            region = Session("rs").Fields("REGION").Value.ToString()
            delegacion = Session("rs").Fields("DELEGACION").Value.ToString()

            categoria = Session("rs").Fields("CATEGORIA").Value.ToString()
            anios_servicio = Session("rs").Fields("ANIOS_SERVICIO").Value.ToString()

            direccion = calle

            If numExt <> "" And numExt <> "0" And numInt = "" Then
                direccion += " No." + numExt
            ElseIf numExt <> "" And numInt <> "" And numInt <> "0" Then
                direccion += "  No." + numExt + " " + numExt
            ElseIf numExt = "" And numExt <> "0" And numInt <> "" And numInt <> "0" Then
                direccion += "No." + numInt
            End If

            poblacion = Session("rs").Fields("ASENTAMIENTO").Value.ToString()
            estadoTrab = Session("rs").Fields("ESTADO").Value.ToString()
            muniTrab = Session("rs").Fields("MUNICIPIO").Value.ToString()
            nom_suc = Session("rs").Fields("SUCURSAL").Value.ToString()
            emailTrab = Session("rs").Fields("CORREO").Value.ToString()
            cargoTrab = Session("rs").Fields("OCUPACION").Value.ToString()
            numCtrl = Session("rs").Fields("NUMTRAB").Value.ToString()
            numPagos = Session("rs").Fields("NUMPAGOS").Value.ToString()
            nomInst = Session("rs").Fields("NOMINST").Value.ToString()
            iniOpe = Session("rs").Fields("FECHAINI").Value.ToString()
            numcontrol_aval = Session("rs").Fields("NUMCONTROLAVAL").Value.ToString()

            medio_pago = Session("rs").Fields("MEDIOPAGO").Value.ToString()
            val1 = Session("rs").Fields("VALOR").Value.ToString()
            medio = Session("rs").Fields("MEDIO").Value.ToString()
            val2 = Session("rs").Fields("VALOR2").Value.ToString()
            telefono = Session("rs").Fields("TELEFONO2").Value.ToString()
        End If

        Session("Con").Close()


        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            montoCred = Session("rs").Fields("MONTO").Value.ToString()
            montoCredLetra = Session("rs").Fields("MONTO_LETRA").Value.ToString()
            montoMaxCred = Session("rs").Fields("MONTO_MAX_CRED").Value.ToString()
            montoMaxCredLetra = Session("rs").Fields("MONTO_MAX_CRED_LETRA").Value.ToString()
            tipoProd = Session("rs").Fields("TIPO_PROD").Value.ToString()
            tipoCred = Session("rs").Fields("PRODUCTO").Value.ToString()
            adscripcion = Session("rs").Fields("ADSCRIPCION").Value.ToString()
            pagoFijo = Session("rs").Fields("PAGOFIJO").Value.ToString()
            pagoMen = Session("rs").Fields("PAGOMEN").Value.ToString()
            pagoAnual = Session("rs").Fields("PAGOAN").Value.ToString()
            adeudo = Session("rs").Fields("SUBTOTAL").value.ToString()
            pagoMen = Session("rs").Fields("PAGOMEN").Value.ToString()
            costoSeg = Session("rs").Fields("COSTOSEG").Value.ToString()
            costoAn = Session("rs").Fields("COSTOAN").Value.ToString()
            costoQuin = Session("rs").Fields("COSTOQUIN").value.ToString()
            pagoFijo_Letra = Session("rs").Fields("PAGOFIJO_LETRA").Value.ToString()
            seguro = Session("rs").Fields("SEGURO").Value.ToString()
            monto_transf = Session("rs").Fields("CAP_ENTREGAR").Value.ToString()
            porcentaje_seg = Session("rs").Fields("PORCENTAJE_COMISION").Value.ToString()
            fecha_sis = Session("rs").Fields("FECHA_SISTEMA").Value.ToString()
            sueldo_quin = Session("rs").Fields("SUELDO_QUIN").Value.ToString()
            saldo_ant = Session("rs").Fields("SALDO_ANTERIOR").Value.ToString()
            cve_exp_rigen = Session("rs").Fields("CVE_EXP_ORIGEN_REEST").Value.ToString()
            monto_transf_reno = Session("rs").Fields("MONTO_TRANSF_RENO").Value.ToString()
            clabeBanco = Session("rs").Fields("CLABE_BANCARIA").Value.ToString()
            banco = Session("rs").Fields("BANCO").Value.ToString()
            int_sal_ant = Session("rs").Fields("INT_ORDI").Value.ToString()
            intereses = Session("rs").Fields("INTERESES").Value.ToString()

            nomGarante1 = Session("rs").Fields("NOM_GARANTE1").Value.ToString()
            sexoGarante1 = Session("rs").Fields("SEXO_GH1").Value.ToString()
            edadGarante1 = Session("rs").Fields("EDAD_GH1").Value.ToString()
            rfcGarante1 = Session("rs").Fields("RFC_GH1").Value.ToString()
            domicilioGarante1 = Session("rs").Fields("DOMI_GH1").Value.ToString()
            coloniaGarante1 = Session("rs").Fields("POBLACION_GH1").Value.ToString()
            estadoGarante1 = Session("rs").Fields("ESTADO_GH1").Value.ToString()
            telGarante1 = Session("rs").Fields("TELGH1").Value.ToString()
            edocivilGarante1 = Session("rs").Fields("EDOCIVIL_GH1").Value.ToString()
            nomconyugeGarante1 = Session("rs").Fields("NOMCONYUGE_GH1").Value.ToString()
            valorcatGarante1 = Session("rs").Fields("VALOR_CAT_GH1").Value.ToString()
            avaluocomGarante1 = Session("rs").Fields("AVALUO_COM_GH1").Value.ToString()

            nomGarante2 = Session("rs").Fields("NOM_GARANTE2").Value.ToString()
            sexoGarante2 = Session("rs").Fields("SEXO_GH2").Value.ToString()
            edadGarante2 = Session("rs").Fields("EDAD_GH2").Value.ToString()
            rfcGarante2 = Session("rs").Fields("RFC_GH2").Value.ToString()
            domicilioGarante2 = Session("rs").Fields("DOMI_GH2").Value.ToString()
            coloniaGarante2 = Session("rs").Fields("POBLACION_GH2").Value.ToString()
            estadoGarante2 = Session("rs").Fields("ESTADO_GH2").Value.ToString()
            telGarante2 = Session("rs").Fields("TELGH2").Value.ToString()
            edocivilGarante2 = Session("rs").Fields("EDOCIVIL_GH2").Value.ToString()
            nomconyugeGarante2 = Session("rs").Fields("NOMCONYUGE_GH2").Value.ToString()
            valorcatGarante2 = Session("rs").Fields("VALOR_CAT_GH2").Value.ToString()
            avaluocomGarante2 = Session("rs").Fields("AVALUO_COM_GH2").Value.ToString()
            finalidadCred = Session("rs").Fields("FINALIDAD_CRED").Value.ToString()

            quincenaPagar = Session("rs").Fields("QNA_PAGAR").Value.ToString()
            anioPagar = Session("rs").Fields("ANIO_PAGAR").Value.ToString()
            anio_factor = Session("rs").Fields("ANIO_FACTOR").Value.ToString()

        End If

        Session("Con").Close()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_AVALES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            nomAval = Session("rs").Fields("NOMBRE").Value.ToString()


            calleAval = Session("rs").Fields("CALLE").Value.ToString()
            numExtAval = Session("rs").Fields("NUMEXT").Value.ToString()
            numIntAval = Session("rs").Fields("NUMINT").Value.ToString()

            'If numIntAval = "" Then
            direccionAval = calleAval ' + " Num. Ext. #" + numExtAval

            If numExtAval <> "" And numExtAval <> "0" And numIntAval = "" Then
                direccion += " No." + numExtAval
            ElseIf numExtAval <> "" And numIntAval <> "" And numIntAval <> "0" Then
                direccion += "  No." + numExtAval + " " + numIntAval
            ElseIf numExtAval = "" And numExtAval <> "0" And numIntAval <> "" And numIntAval <> "0" Then
                direccion += "No." + numIntAval
            End If

            poblacionAval = Session("rs").Fields("ASENTAMIENTO").Value.ToString()
            estadoAval = Session("rs").Fields("ESTADO").Value.ToString()
            muniAval = Session("rs").Fields("MUNICIPIO").Value.ToString()
        End If

        Session("Con").Close()
        doc.ReplaceText("[NOM_TRABAJADOR]", persona, False, RegexOptions.None)
        doc.ReplaceText("[NOM_PROD]", Session("PRODUCTO").ToString, False, RegexOptions.None)
        doc.ReplaceText("[DIAS]", dia, False, RegexOptions.None)
        doc.ReplaceText("[MES]", mes, False, RegexOptions.None)
        doc.ReplaceText("[ANIO]", año, False, RegexOptions.None)
        doc.ReplaceText("[CVE_EXP]", cve_exp, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_CREDITO]", FormatCurrency(CStr(montoCred)), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_CREDITO_LETRA]", montoCredLetra, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_MAX_CREDITO]", FormatCurrency(CStr(montoMaxCred)), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_MAX_CREDITO_LETRA]", montoMaxCredLetra, False, RegexOptions.None)
        doc.ReplaceText("[ESTADO_TRABAJADOR]", estadoTrab, False, RegexOptions.None)
        doc.ReplaceText("[DOMI_TRABAJADOR]", direccion, False, RegexOptions.None)
        doc.ReplaceText("[POBLACION_TRAB]", poblacion, False, RegexOptions.None)
        doc.ReplaceText("[MUNI_TRABAJADOR]", muniTrab, False, RegexOptions.None)
        doc.ReplaceText("[MUNI_TRABAJADOR]", muniTrab, False, RegexOptions.None)
        doc.ReplaceText("[TIPO_PROD]", tipoProd, False, RegexOptions.None)
        doc.ReplaceText("[ADSCRIPCION]", adscripcion, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_SEG]", FormatCurrency(seguro), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_TRANS]", FormatCurrency(monto_transf), False, RegexOptions.None)
        doc.ReplaceText("[PAGO_FIJO]", FormatCurrency(pagoFijo), False, RegexOptions.None)
        doc.ReplaceText("[PAGO_MEN]", FormatCurrency(pagoMen), False, RegexOptions.None)
        doc.ReplaceText("[COSTO_SEG]", FormatCurrency(costoSeg), False, RegexOptions.None)
        doc.ReplaceText("[TOTAL_MENS]", FormatCurrency(costoAn), False, RegexOptions.None)
        doc.ReplaceText("[TOTAL_QNAL]", FormatCurrency(costoQuin), False, RegexOptions.None)
        doc.ReplaceText("[ADEUDO]", FormatCurrency(adeudo), False, RegexOptions.None)
        doc.ReplaceText("[PAGO_FIJO_LETRA]", pagoFijo_Letra, False, RegexOptions.None)
        doc.ReplaceText("[PORCENTAJE_COMI]", porcentaje_seg, False, RegexOptions.None)
        doc.ReplaceText("[PORCENTAJE_SEG]", seguro, False, RegexOptions.None)
        doc.ReplaceText("[FECHASIS]", fecha_sis, False, RegexOptions.None)

        doc.ReplaceText("[CCT]", cct, False, RegexOptions.None)
        doc.ReplaceText("[REGION]", region, False, RegexOptions.None)
        doc.ReplaceText("[DELEGACION]", delegacion, False, RegexOptions.None)

        doc.ReplaceText("[CATEGORIA]", categoria, False, RegexOptions.None)
        doc.ReplaceText("[ANIOS_SERVICIO]", anios_servicio, False, RegexOptions.None)

        doc.ReplaceText("[CVE_EXP_ORIGEN]", cve_exp_rigen, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_TRANS_RENO]", FormatCurrency(monto_transf_reno), False, RegexOptions.None)
        doc.ReplaceText("[NOM_SUC]", nom_suc, False, RegexOptions.None)
        'doc.ReplaceText("[CBE_BANCARIA]", clabeBanco, False, RegexOptions.None)
        'doc.ReplaceText("[NOM_BANCO]", bncSolicitud, False, RegexOptions.None)

        doc.ReplaceText("[MEDIO_PAGO]", medio_pago, False, RegexOptions.None)
        doc.ReplaceText("[VALOR]", val1, False, RegexOptions.None)
        doc.ReplaceText("[MEDIO]", medio, False, RegexOptions.None)
        doc.ReplaceText("[VALOR2]", val2, False, RegexOptions.None)
        doc.ReplaceText("[INT_SAL_ANT]", FormatCurrency(int_sal_ant), False, RegexOptions.None)
        doc.ReplaceText("[TELTRAB]", telefono, False, RegexOptions.None)
        doc.ReplaceText("[EMAILTRAB]", emailTrab, False, RegexOptions.None)
        doc.ReplaceText("[CARGOTRAB]", cargoTrab, False, RegexOptions.None)
        doc.ReplaceText("[NUMTRAB]", numCtrl, False, RegexOptions.None)
        doc.ReplaceText("[NUMPAGOS]", numPagos, False, RegexOptions.None)
        doc.ReplaceText("[NOMINST]", nomInst, False, RegexOptions.None)
        doc.ReplaceText("[FECHAINI]", iniOpe, False, RegexOptions.None)
        doc.ReplaceText("[CURP]", curp, False, RegexOptions.None)
        doc.ReplaceText("[RFC]", rfc, False, RegexOptions.None)
        doc.ReplaceText("[FNAC]", fnac, False, RegexOptions.None)
        doc.ReplaceText("[SEXO]", sexo, False, RegexOptions.None)
        doc.ReplaceText("[EDOCIVIL]", edociv, False, RegexOptions.None)
        doc.ReplaceText("[EDAD]", edad, False, RegexOptions.None)
        doc.ReplaceText("[INTERES]", FormatCurrency(intereses), False, RegexOptions.None)


        doc.ReplaceText("[NOM_AVAL]", nomAval, False, RegexOptions.None)
        doc.ReplaceText("[DOMI_AVAL]", direccionAval, False, RegexOptions.None)
        doc.ReplaceText("[POBLACION_AVAL]", poblacionAval, False, RegexOptions.None)
        doc.ReplaceText("[MUNI_AVAL]", muniAval, False, RegexOptions.None)
        doc.ReplaceText("[ESTADO_AVAL]", estadoAval, False, RegexOptions.None)
        doc.ReplaceText("[CTRLAVAL]", numcontrol_aval, False, RegexOptions.None)
        doc.ReplaceText("[NOM_CONYUGE]", conyuge, False, RegexOptions.None)
        doc.ReplaceText("[ANIO_FAC]", anio_factor, False, RegexOptions.None)


        'GARANTES HIPOTECARIOS
        If nomGarante1 <> "" Then
            doc.ReplaceText("[SAL_QUIN_TRAB]", FormatCurrency(sueldo_quin), False, RegexOptions.None)
            doc.ReplaceText("[MENOS_SAL_ANT]", FormatCurrency(saldo_ant), False, RegexOptions.None)
            doc.ReplaceText("[NOM_GARANTE1]", nomGarante1, False, RegexOptions.None)
            doc.ReplaceText("[SEXO_GH1]", sexoGarante1, False, RegexOptions.None)
            doc.ReplaceText("[EDAD_GH1]", edadGarante1, False, RegexOptions.None)
            doc.ReplaceText("[RFC_GH1]", rfcGarante1, False, RegexOptions.None)
            doc.ReplaceText("[DOMI_GH1]", domicilioGarante1, False, RegexOptions.None)
            doc.ReplaceText("[POBLACION_GH1]", coloniaGarante1, False, RegexOptions.None)
            doc.ReplaceText("[ESTADO_GH1]", estadoGarante1, False, RegexOptions.None)
            doc.ReplaceText("[TELGH1]", telGarante1, False, RegexOptions.None)
            doc.ReplaceText("[EDOCIVIL_GH1]", edocivilGarante1, False, RegexOptions.None)
            doc.ReplaceText("[NOMCONYUGE_GH1]", nomconyugeGarante1, False, RegexOptions.None)
            doc.ReplaceText("[VALOR_CAT_GH1]", valorcatGarante1, False, RegexOptions.None)
            doc.ReplaceText("[AVALUO_COM_GH1]", avaluocomGarante1, False, RegexOptions.None)

            doc.ReplaceText("[NOM_GARANTE2]", nomGarante2, False, RegexOptions.None)
            doc.ReplaceText("[SEXO_GH2]", sexoGarante2, False, RegexOptions.None)
            doc.ReplaceText("[EDAD_GH2]", edadGarante2, False, RegexOptions.None)
            doc.ReplaceText("[RFC_GH2]", rfcGarante2, False, RegexOptions.None)
            doc.ReplaceText("[DOMI_GH2]", domicilioGarante2, False, RegexOptions.None)
            doc.ReplaceText("[POBLACION_GH2]", coloniaGarante2, False, RegexOptions.None)
            doc.ReplaceText("[ESTADO_GH2]", estadoGarante2, False, RegexOptions.None)
            doc.ReplaceText("[TELGH2]", telGarante2, False, RegexOptions.None)
            doc.ReplaceText("[EDOCIVIL_GH2]", edocivilGarante2, False, RegexOptions.None)
            doc.ReplaceText("[NOMCONYUGE_GH2]", nomconyugeGarante2, False, RegexOptions.None)
            doc.ReplaceText("[VALOR_CAT_GH2]", valorcatGarante2, False, RegexOptions.None)
            doc.ReplaceText("[AVALUO_COM_GH2]", avaluocomGarante2, False, RegexOptions.None)
            doc.ReplaceText("[FINALIDAD_CRED]", finalidadCred, False, RegexOptions.None)
            doc.ReplaceText("[PAGO_MEN]", FormatCurrency(pagoMen), False, RegexOptions.None)
            doc.ReplaceText("[PAGO_ANUAL]", FormatCurrency(pagoAnual), False, RegexOptions.None)
            doc.ReplaceText("[QNA_PAGAR]", quincenaPagar, False, RegexOptions.None)
            doc.ReplaceText("[ANIO_PAGAR]", anioPagar, False, RegexOptions.None)


        End If
    End Sub
#End Region

    Protected Sub btn_generaExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_generaExcel.Click

        GeneraExcelMovimientos(0, -1)
    End Sub


#Region "Excel Movimientos"
    Private Sub GeneraExcelMovimientos(ByVal Anio As Integer, ByVal Estatus As Integer)

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dteExcelmovimientos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDFOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, Anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, Estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_MOVIMIENTOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteExcelmovimientos, Session("rs"))
        ViewState("dtmovimientos") = dteExcelmovimientos
        Session("Con").Close()


        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dteExcelmovimientos.Columns.Count - 1
            context.Response.Write(dteExcelmovimientos.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As DataRow In dteExcelmovimientos.Rows
            For i = 0 To dteExcelmovimientos.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim NombreExcel As String
        Dim FechaSis As Date = Session("FechaSis")


        NombreExcel = "Reporte Movimientos " + lbl_folioa.Text.ToString + " " + FechaSis.ToString("dd-MM-yyyy")



        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreExcel.ToString + ".csv")
        context.Response.End()

    End Sub

#End Region

End Class