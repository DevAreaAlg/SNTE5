Public Class COB_LOG
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Registro de Bitácora", "REGISTRO DE BITÁCORA DE COBRANZA")
        If Not Me.IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If

            'Llenafase()
            Llenaeventos()

        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
            limpia()
            cmb_folio.Items.Clear()
            pnl_cobranza.Visible = False
            Session("idperbusca") = Nothing
        End If

    End Sub

    Private Sub Llenaeventos()
        cmb_evento.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")

        cmb_evento.Items.Add(elija)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("BANDERA", Session("adVarChar"), Session("adParamInput"), 10, 0)
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



    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_seleccionar.Click
        Session("PERSONAID") = CInt(txt_cliente.Text)
        Llenadatos()
    End Sub

    Private Sub Llenadatos()

        cmb_folio.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_folio.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_cliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FOLIOS_PAGADOS_CLIENTE"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            If Session("rs").Fields("COND").Value.ToString = "0" Then
                limpia()
                lbl_clienteA.Visible = True
                lbl_clienteB.Text = "" + Session("rs").Fields("NOMBRE").Value.ToString
                lbl_info.Text = "El cliente no cuenta con expedientes activos o liquidados"
                Session("Con").Close()
                Exit Sub
            End If

            If Session("rs").Fields("NOMBRE").Value.ToString = "-1" Then
                lbl_info.Text = "El número de cliente introducido no existe"
                lbl_clienteA.Visible = False
                lbl_clienteB.Visible = False
                limpia()
                Session("Con").Close()
                Exit Sub
            End If
            lbl_clienteA.Visible = True
            lbl_clienteB.Visible = True
            lbl_clienteB.Text = "" + Session("rs").Fields("NOMBRE").Value.ToString
            cmb_folio.Enabled = True
            lbl_info.Text = ""

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("FOLIO").Value.ToString, Session("rs").Fields("FOLIO").Value.ToString)
                cmb_folio.Items.Add(item)
                Session("rs").movenext()
            Loop

        Else
            lbl_info.Text = "El cliente no cuenta con expedientes activos o liquidados"

        End If
        Session("Con").Close()
    End Sub

    Protected Sub cmb_folio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_folio.SelectedIndexChanged
        lbl_status.Text = ""

        pnl_cobranza.Visible = False
        Dim dtconsulta As New Data.DataTable()
        dtconsulta.Clear()

        If cmb_folio.SelectedItem.Value <> 0 Then
            Session("FOLIO") = cmb_folio.SelectedItem.Value
            lbl_ProductoA.Visible = True
            lbl_ProductoB.Visible = True
            DatosExpediente()
            Llenaeventos()
            pnl_agendar.Visible = False
            pnl_agendar_visita.Visible = False
            pnl_aviso.Visible = False
            pnl_citatorio.Visible = False
            pnl_estatus.Visible = False
            pnl_llamada.Visible = False
            pnl_evento_cita.Visible = False
            pnl_evento_visita.Visible = False
            pnl_personas.Visible = False
            pnl_reg_juicio.Visible = False
            pnl_seg.Visible = False
            pnl_seguimiento.Visible = False
            pnl_seguimiento_aviso.Visible = False
            pnl_seguimiento_citatorio.Visible = False
            pnl_seg_visita.Visible = False
            pnl_seguimiento_visita.Visible = False
        Else
            Session("FOLIO") = Nothing
            lbl_ProductoB.Text = ""
            lbl_ProductoA.Visible = False
            lbl_ProductoB.Visible = False
        End If
    End Sub

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
        lbl_ProductoB.Text = "" + Session("rs").Fields("PRODUCTO").Value.ToString

        Session("Con").Close()

        If Session("TIPO_PROD").ToString = "1" Then
            pnl_cobranza.Visible = True

        Else
            pnl_cobranza.Visible = False
        End If

    End Sub

    Protected Sub txt_cliente_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cliente.TextChanged
        pnl_cobranza.Visible = False
    End Sub

    Private Sub limpiafiltros()

        cmb_evento.SelectedIndex = "0"
        ' lbl_status_dag.Text = ""


    End Sub

    Private Function date_val(ByVal fecha As String) As Boolean
        Dim correcto As Boolean = True

        If fecha <> "" Then
            If DateDiff(DateInterval.Year, CDate(fecha), Now()) > 150 Then
                correcto = False
            End If
        End If
        Return correcto
    End Function


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
    Private Sub VerDocumentoDigitalizado(ByVal idLog As String, ByVal tipo As String)


        Dim strConnString As String 'Ruta de la BD
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_COBRANZA", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@IDLOG", idLog)
        sqlCmdObj.Parameters.AddWithValue("@TIPO", tipo)


        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()


        Dim iByteLength As Long
        Try
            iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)
        Catch ex As Exception
            iByteLength = 0
        End Try

        If iByteLength <> 0 Then

            Dim byFileData(iByteLength) As Byte
            sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

            sdrRecordset.Close()
            sqlConnection.Close()

            'Obtiene los bytes (imagen) y las despliega con Adobe Reader por el formato PDF 
            Dim ms As New System.IO.MemoryStream()
            With Response
                .BufferOutput = True
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/octet-stream"
                If tipo = "AVIS" Then 'ES UN AVISO
                    .AddHeader("Content-disposition", "attachment; filename=Aviso.pdf")
                Else 'ES UN CITATORIO
                    .AddHeader("Content-disposition", "attachment; filename=Citatorio.pdf")
                End If

                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.SetNoServerCaching()
                Response.Cache.SetNoStore()
                Response.Cache.SetMaxAge(System.TimeSpan.Zero)

                .OutputStream.Write(byFileData, 0, byFileData.Length)
                .End()
            End With
        Else
            sdrRecordset.Close()
            sqlConnection.Close()
            Exit Sub
        End If

    End Sub



    'RESULTADOS
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
    Private Sub Juicio()

        lbl_info.Text = ""
        lbl_status.Text = ""


        If txt_cita.Text <> "" Then
            If CDate(txt_cita.Text) < CDate(Session("FechaSis")) Then
                lbl_status.Text = "Error: Fecha de cita anterior a la fecha de sistema"
                Exit Sub
            End If
        End If


        'If CDate(txt_fecha_ingreso.Text) < CDate(Session("FechaSis")) Then
        '    lbl_status.Text = "Error: Fecha de ingreso de Demanda anterior a la fecha de sistema"
        '    Exit Sub
        'End If


        If txt_prom_pago.Text <> "" Then
            If (txt_prom_pago.Text <> "" And (CDate(txt_prom_pago.Text) < CDate(Session("FechaSis")))) Then
                lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
                Exit Sub
            End If

        End If

        'If txt_fecha_emp_Aval.Text <> "" Then
        '    If CDate(txt_fecha_emp_Aval.Text) < CDate(Session("FechaSis")) Then
        '        lbl_status.Text = "Error: Fecha de Emplazamiento Aval anterior a la fecha de sistema"
        '        Exit Sub
        '    End If
        'End If

        'If txt_fecha_emp_tit.Text <> "" Then
        '    If CDate(txt_fecha_emp_tit.Text) < CDate(Session("FechaSis")) Then
        '        lbl_status.Text = "Error: Fecha de Emplazamiento Titular anterior a la fecha de sistema"
        '        Exit Sub
        '    End If
        'End If



        ' If date_val(txt_fechaejecucion.Text) = True Then

        If (txt_prom_pago.Text = "" And txt_monto_prom_pago.Text <> "") Or (txt_prom_pago.Text <> "" And txt_monto_prom_pago.Text = "") Then
            lbl_status.Text = "Error: Debe de capturar el campo de PromeEventoRegJuiciosa de Pago y Monto Promesa de Pago"

        Else

            If date_val(txt_prom_pago.Text) = True Then

                If Validamonto(txt_monto_prom_pago.Text) = True Then
                    EventoRegJuicio()
                    lbl_status.Text = "Guardado correctamente"


                Else
                    lbl_status.Text = "Error: Monto incorrecto"
                End If

            Else
                lbl_status.Text = "Error: La fecha Promesa Pago es inválida"
            End If

        End If

        ' Else
        'lbl_status.Text = "Error: Verifique el formato de fecha de diligencia"
        'End If


    End Sub
    Private Sub EstatusCobranza()
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
    Private Sub limpiaEstatusCobranza()

        cmb_estatus_cob.Items.Clear()
        lbl_user_estatus.Text = ""

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


    Private Sub limpia()
        limpiafiltros()
        cmb_folio.Enabled = False
        lbl_ProductoA.Visible = False
        lbl_ProductoB.Visible = False
    End Sub
    'Aviso 
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
    Private Function Validamonto(ByVal monto As String) As Boolean

        Dim Valor As Boolean = True

        If monto <> "" Then
            Valor = Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))

        End If

        Return Valor
    End Function
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
    Public Sub guardarclick()
        lbl_info.Text = ""
        lbl_status.Text = ""


        'If CDate(txt_fecha_aviso.Text) < CDate(Session("FechaSis")) Then
        '    lbl_status.Text = "Error: Fecha anterior a la fecha de sistema"
        '    Exit Sub
        'End If


        'If txt_prom_pago_aviso.Text <> "" Then
        '    If (txt_prom_pago_aviso.Text <> "" And (CDate(txt_prom_pago_aviso.Text) < CDate(Session("FechaSis")))) Then
        '        lbl_status.Text = "Error: Fecha Promesa Pago es anterior a la fecha de sistema"
        '        Exit Sub
        '    End If

        'End If

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
    Public Sub btn_guardar_aviso_Click(sender As Object, e As EventArgs)

        guardarclick()

    End Sub

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
                lbl_status.Text = ""
            Case "SEG"
                pnl_agendar.Visible = False
                pnl_seg.Visible = True
                cita()
            Case Else
                pnl_agendar.Visible = False
                pnl_seg.Visible = False
        End Select

    End Sub
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

    'DESTINATARIOS
    Private Sub LlenaDestinatarios()


        cmb_destinatario.Items.Clear()
        cmb_destinatario_llamada.Items.Clear()
        cmb_destinatario_visita.Items.Clear()


        Dim elija As New ListItem("ELIJA", "0")
        cmb_destinatario.Items.Add(elija)
        cmb_destinatario_llamada.Items.Add(elija)
        cmb_destinatario_visita.Items.Add(elija)
        '
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
    'LIMPIAR

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
        txt_fecha_emp_Aval.Text = ""
        txt_fecha_emp_tit.Text = ""


    End Sub
    'citatorios

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

    'visita
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
    Private Sub Llenaeventovisita()

        cmb_evento_visita.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_evento_visita.Items.Add(elija)

        Dim agendar As New ListItem("AGENDAR", "AG")
        cmb_evento_visita.Items.Add(agendar)
        Dim seguimiento As New ListItem("SEGUIMIENTO", "SEG")
        cmb_evento_visita.Items.Add(seguimiento)


    End Sub
    Private Sub cmb_evento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_evento.SelectedIndexChanged
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
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_evento_cita.Visible = False
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
                    pnl_agendar.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
                    pnl_seguimiento_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_agendar_visita.Visible = False
                    pnl_seg_visita.Visible = False
            End Select
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

    Private Function horas(ByVal hora As String) As Boolean
        Return Regex.IsMatch(hora, "(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)")
    End Function

    Private Sub lnk_agregar_visita_Click(sender As Object, e As EventArgs) Handles lnk_agregar_visita.Click
        lbl_info.Text = ""
        lbl_status.Text = ""
        lbl_status.Text = ""
        If CDate(txt_fecha_visita.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha de visita es anterior a la fecha de sistema"
            Exit Sub
        End If

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_fecha_visita.Text) = True And horas(txt_hora_visita.Text) = True Then
            EventoVISITA()
            lbl_status.Text = "Guardado correctamente"
            LIMPIAREGISTRO()
            limpiavisita()
            pnl_evento_visita.Visible = False
            pnl_seg_visita.Visible = False
            pnl_seguimiento_visita.Visible = False
        Else
            lbl_status.Text = "Error: Verifique formato de Fecha y hora"
        End If
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

    Private Sub lnk_guardar_cita_Click(sender As Object, e As EventArgs) Handles lnk_guardar_cita.Click
        lbl_info.Text = ""
        lbl_status.Text = ""
        lbl_status.Text = ""

        If CDate(txt_cita_fecha.Text) < CDate(Session("FechaSis")) Then
            lbl_status.Text = "Error: Fecha de cita es anterior a la fecha de sistema"
            Exit Sub
        End If

        'Valida que la fecha no tenga formato incorrecto
        If date_val(txt_cita_fecha.Text) = True And horas(txt_hora.Text) = True Then
            Eventocita()
            lbl_status.Text = "Guardado correctamente"
            LIMPIAREGISTRO()
            limpiacitas()
            pnl_agendar.Visible = False
            pnl_evento_cita.Visible = False
            ' pnl_cobranza.Visible = False
            pnl_seg.Visible = False
            pnl_seguimiento.Visible = False
        Else
            lbl_status.Text = "Error: Verifique formato de fecha y hora"
        End If
    End Sub

    Private Sub lnk_cancelar_cita_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_cita.Click
        limpiacitas()
        pnl_agendar.Visible = False
        pnl_evento_cita.Visible = True
        cmb_evento_cita.SelectedIndex = "0"
    End Sub

    Private Sub cmb_cita_seguimiento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_cita_seguimiento.SelectedIndexChanged
        If cmb_cita_seguimiento.SelectedIndex = "0" Then
            lbl_status.Text = "Error: Seleccione una cita para dar seguimiento"
        Else
            Dim CLAVE
            CLAVE = Split(cmb_evento.SelectedItem.Value.ToString, "-")
            pnl_seguimiento.Visible = True
            Llenaresultado(CLAVE(0))
            LlenaSucursales()
            LlenaUsuarios()
            LlenaDestinatarios()
        End If
    End Sub

    Private Sub lnk_agregar_seguimiento_Click(sender As Object, e As EventArgs) Handles lnk_agregar_seguimiento.Click
        lbl_info.Text = ""
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
                        lbl_status.Text = "Guardado correctamente"
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

    Private Sub lnk_cancelar_cita_seg_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_cita_seg.Click
        limpiaseguimiento()
        pnl_seguimiento.Visible = False
        pnl_seg.Visible = False
        pnl_evento_cita.Visible = True
        ' cmb_evento_cita.SelectedIndex = "0"

    End Sub

    Private Sub cmb_destinatario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_destinatario.SelectedIndexChanged
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

    Private Sub lnk_guardar_seg_visita_Click(sender As Object, e As EventArgs) Handles lnk_guardar_seg_visita.Click
        lbl_info.Text = ""
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
                        lbl_status.Text = "Guardado correctamente"
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

    Private Sub lnk_guardar_Click(sender As Object, e As EventArgs) Handles lnk_guardar.Click
        lbl_info.Text = ""
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
                        lbl_status.Text = "Guardado correctamente"
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

    Private Sub cmb_destinatario_llamada_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_destinatario_llamada.SelectedIndexChanged
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
    'Aviso
    Private Sub dag_aviso_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dag_aviso.ItemCommand
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

    Private Sub dag_aviso_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dag_aviso.ItemDataBound
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False
    End Sub

    Private Sub lnk_guardar_aviso_Click(sender As Object, e As EventArgs) Handles lnk_guardar_aviso.Click
        lbl_info.Text = ""
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

    Private Sub lnl_si_aviso_Click(sender As Object, e As EventArgs) Handles lnl_si_aviso.Click
        Session("VENGODE") = "COB_LOG.aspx"
        Response.Redirect("../DIGITALIZADOR/DIGI_GLOBAL.aspx")
    End Sub
    Private Sub LIMPIAVARIABLESAVISO()

        Session("IDAVISO") = Nothing
        Session("DESTINO_AVISO") = Nothing
        Session("NOMBRE_DESTINO_AVISO") = Nothing
    End Sub
    Private Sub lnk_no_aviso_Click(sender As Object, e As EventArgs) Handles lnk_no_aviso.Click
        pnl_cobranza.Visible = False
        cmb_folio.Items.Clear()
        txt_cliente.Text = ""
        lbl_clienteB.Text = ""
        lbl_ProductoB.Text = ""
        limpiaavisos()
        LIMPIAVARIABLESAVISO()
        pnl_aviso.Visible = False
        pnl_seguimiento_aviso.Visible = False
        pnl_digitalizar_aviso.Visible = False
    End Sub
    'Estatus
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

    Private Sub lnk_agregar_estatus_Click(sender As Object, e As EventArgs) Handles lnk_agregar_estatus.Click
        EventoEstatusCobranza()
        lbl_status.Text = "Guardado correctamente"
        LIMPIAREGISTRO()
        pnl_estatus.Visible = False
        limpiaEstatusCobranza()
    End Sub

    Private Sub lnk_cancelar_estatus_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_estatus.Click
        limpiaEstatusCobranza()
        pnl_estatus.Visible = False
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"
    End Sub
    'Juicio
    Private Sub lnk_agregar_reg_juicio_Click(sender As Object, e As EventArgs) Handles lnk_agregar_reg_juicio.Click
        lbl_info.Text = ""
        lbl_status.Text = ""
        lbl_status.Text = ""


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


        If date_val(txt_fechaejecucion.Text) = True Then

            If (txt_prom_pago.Text = "" And txt_monto_prom_pago.Text <> "") Or (txt_prom_pago.Text <> "" And txt_monto_prom_pago.Text = "") Then
                lbl_status.Text = "Error: Debe de capturar el campo de Promesa de Pago y Monto Promesa de Pago"

            Else

                If date_val(txt_prom_pago.Text) = True Then

                    If Validamonto(txt_monto_prom_pago.Text) = True Then
                        EventoRegJuicio()
                        LIMPIAREGISTRO()
                        limpiaRegistroJuicio()
                        pnl_reg_juicio.Visible = False
                        lbl_status.Text = "Guardado correctamente"

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

    Private Sub lnk_cancelar_reg_juicio_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_reg_juicio.Click
        limpiaRegistroJuicio()
        pnl_reg_juicio.Visible = False
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"
    End Sub

    Private Sub dag_citatorios_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dag_citatorios.ItemDataBound
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False

    End Sub

    Private Sub dag_citatorios_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dag_citatorios.ItemCommand
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

    Private Sub cmb_evento_visita_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_evento_visita.SelectedIndexChanged

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
                lbl_status.Text = ""
                pnl_agendar_visita.Visible = False
                pnl_seg_visita.Visible = True
                visita()
            Case Else
                pnl_agendar_visita.Visible = False
                pnl_seg_visita.Visible = False
        End Select
    End Sub

    Private Sub lnk_cancelar_visita_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_visita.Click
        limpiavisita()
        pnl_agendar_visita.Visible = False
        pnl_evento_visita.Visible = False
        cmb_evento.SelectedIndex = "0"
    End Sub

    Private Sub cmb_destinatario_visita_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_destinatario_visita.SelectedIndexChanged
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
    Private Sub lnk_cancelar_seg_visita_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_seg_visita.Click
        lbl_status.Text = ""
        lbl_status.Text = ""
        limpiaseguimientovisita()
        pnl_evento_visita.Visible = False
        cmb_evento.SelectedIndex = "0"
        pnl_seguimiento_visita.Visible = False
        pnl_seg_visita.Visible = False

    End Sub

    Private Sub cmb_visita_seguimiento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_visita_seguimiento.SelectedIndexChanged
        lbl_status.Text = ""

        If cmb_visita_seguimiento.SelectedIndex = "0" Then
            lbl_status.Text = "Error: Seleccione una visita para dar seguimiento"
        Else
            Dim CLAVE
            CLAVE = Split(cmb_evento.SelectedItem.Value.ToString, "-")
            lbl_status.Text = ""
            pnl_seguimiento_visita.Visible = True
            Llenaresultado(CLAVE(0))
            LlenaSucursales()
            LlenaUsuarios()
            LlenaDestinatarios()
        End If
    End Sub
    Private Sub LIMPIAVARIABLESCITATORIOS()

        Session("IDCITATORIO") = Nothing
        Session("NOMBRE_DESTINO_CITATORIO") = Nothing
        Session("DESTINO_CITATORIO") = Nothing

    End Sub
    Private Sub lnk_cancelar_citatorio_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_citatorio.Click
        Dim EVENTO
        EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")

        LIMPIACITATORIO()
        LIMPIAVARIABLESCITATORIOS()
        pnl_seguimiento_citatorio.Visible = False
        pnl_citatorio.Visible = True
        citatorios(EVENTO(0))
    End Sub

    Private Sub lnk_agregar_Citatorio_Click(sender As Object, e As EventArgs) Handles lnk_agregar_Citatorio.Click
        lbl_info.Text = ""
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
                        lbl_status.Text = "Guardado correctamente"
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

    Private Sub lnk_cancelar_llamda_Click(sender As Object, e As EventArgs) Handles lnk_cancelar_llamda.Click
        LIMPIALLAMADAS()
        pnl_llamada.Visible = False
        pnl_cobranza.Visible = True
        cmb_evento.SelectedIndex = "0"
    End Sub

    Private Sub cmb_resultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_resultado.SelectedIndexChanged
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

End Class