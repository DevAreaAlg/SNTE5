Public Class COB_CONSULTA_BIT
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Consulta de Bitácora", "Consulta de bitácora de cobranza")
        If Not Me.IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If

            Llenafase()
            Llenaeventos()

        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
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

    Private Sub Llenafase()

        cmb_estatus.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_estatus.Items.Add(elija)
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_FASE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_estatus.Items.Add(item)
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
        dag_historial.CurrentPageIndex = 0

        dag_historial.Visible = False
        Dim dtconsulta As New Data.DataTable()
        dtconsulta.Clear()
        dag_historial.DataSource = dtconsulta
        dag_historial.DataBind()

        If cmb_folio.SelectedItem.Value <> 0 Then
            Session("FOLIO") = cmb_folio.SelectedItem.Value
            lbl_ProductoA.Visible = True
            lbl_ProductoB.Visible = True
            DatosExpediente()

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

    Protected Sub lnk_limpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_limpiar.Click
        limpiafiltros()

        lbl_info.Text = ""
        lbl_status.Text = ""
        dag_historial.CurrentPageIndex = 0

        dag_historial.Visible = False
        Dim dtconsulta As New Data.DataTable()
        dtconsulta.Clear()
        dag_historial.DataSource = dtconsulta
        dag_historial.DataBind()

    End Sub

    Private Sub limpiafiltros()
        cmb_estatus.SelectedIndex = "0"
        cmb_evento.SelectedIndex = "0"
        lbl_status_dag.Text = ""
        txt_FechaFin.Text = ""
        txt_FechaIni.Text = ""
        Session("IDLOG") = Nothing
    End Sub

    Protected Sub lnk_Consultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Consultar.Click

        lbl_info.Text = ""
        lbl_status.Text = ""
        dag_historial.CurrentPageIndex = 0
        dag_historial.Visible = False
        Dim dtconsulta As New Data.DataTable()
        dtconsulta.Clear()
        dag_historial.DataSource = dtconsulta
        dag_historial.DataBind()

        'Asignacion de variables

        Dim fase As String
        If cmb_estatus.SelectedIndex = "0" Then
            fase = 0
        Else
            fase = cmb_estatus.SelectedItem.Value.ToString
        End If
        Dim EVENTO
        Dim eventos As String

        If cmb_evento.SelectedIndex = "0" Then
            eventos = 0
        Else
            EVENTO = Split(cmb_evento.SelectedItem.Value.ToString, "-")
            eventos = EVENTO(0)
        End If

        Dim fechaini As String = ""
        Dim fechafin As String = ""

        If (txt_FechaFin.Text <> "" And txt_FechaIni.Text = "") Or (txt_FechaIni.Text <> "" And txt_FechaFin.Text = "") Then
            lbl_status.Text = "Error: Necesita capturar ambas fechas"

            Exit Sub
        End If

        ' Filtro de FEchas
        If txt_FechaIni.Text <> "" And txt_FechaFin.Text <> "" And (txt_FechaIni.Text <> "01/01/1900" And txt_FechaFin.Text <> "01/01/1900" And txt_FechaFin.Text <> "01/01/0000" And txt_FechaIni.Text <> "01/01/0000") Then
            'verifica que el formato de la fecha sea correcto
            If date_val(txt_FechaIni.Text) = True And date_val(txt_FechaFin.Text) = True Then
                'verifica que la fecha inicial  sea menor a la fecha final
                If CDate(txt_FechaIni.Text) <= CDate(txt_FechaFin.Text) Then
                    fechaini = txt_FechaIni.Text
                    fechafin = txt_FechaFin.Text
                Else
                    lbl_status.Text = "Error: La fecha inicial debe ser menor o igual a la fecha final"

                    Exit Sub
                End If
            Else
                lbl_status.Text = "Error: Verifique fechas"
                Exit Sub
            End If
        Else
            fechaini = ""
            fechafin = ""
        End If
        LLENAREPORTE(fase, eventos, fechaini, fechafin, 1)
    End Sub

    Protected Sub dag_historial_pageIndexChanged(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs) Handles dag_historial.PageIndexChanged

        dag_historial.CurrentPageIndex = e.NewPageIndex
        LLENAREPORTE(0, 0, "", "", 0)

    End Sub

    Private Sub LLENAREPORTE(ByVal FASE As String, ByVal EVENTO As String, ByVal FECHAI As String, ByVal FECHAF As String, ByVal Filtro As Integer)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dthistorial As New Data.DataTable()
        dag_historial.Visible = True
        If Filtro = 1 Then
            dag_historial.CurrentPageIndex = 0
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, cmb_folio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FASE", Session("adVarChar"), Session("adParamInput"), 15, FASE)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EVENTO", Session("adVarChar"), Session("adParamInput"), 15, EVENTO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, FECHAI)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, FECHAF)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("BANDERA", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dthistorial, Session("rs"))

        If dthistorial.Rows.Count > 0 Then
            'se vacian los expedientes al formulario
            dag_historial.DataSource = dthistorial
            dag_historial.DataBind()
            Session("CONSULTA") = dthistorial
        Else
            lbl_status.Text = "No existen registros"
            dag_historial.Visible = False
        End If

        Session("Con").Close()

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

    Protected Sub dag_consulta_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_historial.ItemDataBound

        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(5).Visible = False

    End Sub

    Private Sub DAG_consulta_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_historial.ItemCommand

        If (e.CommandName = "DETALLE") Then

            Session("IDLOG") = e.Item.Cells(0).Text
            Session("CLAVE_EVENTO") = e.Item.Cells(5).Text

            Select Case Session("CLAVE_EVENTO")
                Case "LLAM"
                    llamada(Session("IDLOG"))
                    ModalPopupExtender_llamada.Show()
                Case "CITA"
                    cita(Session("IDLOG"))
                    ModalPopupExtender_cita.Show()
                Case "CTRO"
                    citatorio(Session("IDLOG"))
                    ModalPopupExtender_citatorios.Show()
                Case "AVIS"
                    avisos(Session("IDLOG"))
                    ModalPopupExtender_avisos.Show()

                Case "JUIC"
                    Juicio(Session("IDLOG"))
                    ModalPopupExtender_juicio.Show()

                Case "DEXP"
                    Despachos(Session("IDLOG"))
                    ModalPopupExtender_detalle_asignacion.Show()

                Case "COB"
                    EstatusCobranza(Session("IDLOG"))
                    ModalPopupExtender_detalle_estatus.Show()

                Case "VISI"
                    visita(Session("IDLOG"))
                    ModalPopupExtender_visita.Show()
                Case Else
                    ModalPopupExtender_llamada.Hide()
                    ModalPopupExtender_cita.Hide()
                    ModalPopupExtender_avisos.Hide()
                    ModalPopupExtender_citatorios.Hide()
                    ModalPopupExtender_juicio.Hide()
                    ModalPopupExtender_detalle_asignacion.Hide()
                    ModalPopupExtender_detalle_estatus.Hide()
                    ModalPopupExtender_visita.Hide()

            End Select

        End If

        If (e.CommandName = "VER") Then
            If (e.Item.Cells(5).Text = "AVIS") Or (e.Item.Cells(5).Text = "CTRO") Then
                VerDocumentoDigitalizado(e.Item.Cells(0).Text, e.Item.Cells(5).Text)
            Else
                lbl_status_dag.Text = "No existe un documento para el evento elegido"
            End If

        End If
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

    Private Sub llamada(ByVal idlog As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_LLAMADA"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_id_llamada.Text = Session("rs").fields("ID_LLAMADA").value.ToString
            lbl_destino_llamada.Text = Session("rs").fields("DESTINATARIO").value.ToString
            lbl_nombre_destino_llamada.Text = Session("rs").fields("NOMBRE_DESTINATARIO").value.ToString + " ( ID:  " + Session("rs").fields("ID_PERSONA").value.ToString + " )"
            txt_notas_llamada.Text = Session("rs").fields("NOTAS").value.ToString
            lbl_tel_llamada.Text = Session("rs").fields("TELEFONO").value.ToString
            lbl_duracion_llamada.Text = Session("rs").fields("DURACION").value.ToString
            txt_resultado_llamada.Text = Session("rs").fields("RESULTADO").value.ToString
            lbl_persona_resp_llamada.Text = Session("rs").fields("PERSONA_RESPONDIO").value.ToString
            lbl_fecha_llamada.Text = Session("rs").fields("FECHA_REGISTRO").value.ToString
            lbl_usuario_llamada.Text = Session("rs").fields("CREADO").value.ToString
            lbl_fecha_creado_llamada.Text = Session("rs").fields("FECHA_CREADO").value.ToString

            lbl_prom_pago_llamada.Text = Session("rs").fields("PROM_PAGO").value.ToString
            lbl_monto_pago_llamada.Text = Session("rs").fields("MONTO_PAGO").value.ToString

        Else
            lbl_id_llamada.Text = ""
            lbl_destino_llamada.Text = ""
            lbl_nombre_destino_llamada.Text = ""
            txt_notas_llamada.Text = ""
            lbl_tel_llamada.Text = ""
            lbl_duracion_llamada.Text = ""
            txt_resultado_llamada.Text = ""
            lbl_persona_resp_llamada.Text = ""
            lbl_fecha_llamada.Text = ""
            lbl_usuario_llamada.Text = ""
            lbl_fecha_creado_llamada.Text = ""
            lbl_prom_pago_llamada.Text = ""
            lbl_monto_pago_llamada.Text = ""
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_cerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar.Click
        ModalPopupExtender_llamada.Hide()
    End Sub

    Private Sub cita(ByVal idlog As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_CITA"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_id_cita.Text = Session("rs").fields("ID_CITAS").value.ToString
            lbl_hora_Fecha_cita.Text = Session("rs").fields("HORA_FECHA_CITA").value.ToString
            lbl_suc_cita.Text = Session("rs").fields("SUCURSAL_CITA").value.ToString
            lbl_destinatario_cita.Text = Session("rs").fields("DESTINATARIO").value.ToString
            lbl_nombre_destinatario_cita.Text = Session("rs").fields("NOMBRE_DESTINATARIO").value.ToString + " ( ID:  " + Session("rs").fields("ID_PERSONA").value.ToString + " )"
            txt_notas_cita.Text = Session("rs").fields("NOTAS").value.ToString
            lbl_fecha_registro_cita.Text = Session("rs").fields("FECHA_REGISTRO_CITA").value.ToString
            lbl_creado_cita.Text = Session("rs").fields("CREADO_CITA").value.ToString
            lbl_fecha_creado_cita.Text = Session("rs").fields("FECHA_CREADO_CITA").value.ToString

            lbl_resultado_cita.Text = Session("rs").fields("RESULTADO").value.ToString
            lbl_fecha_hora_seg_cita.Text = Session("rs").fields("HORA_FECHA_ASISTIO").value.ToString
            lbl_suc_seg_cita.Text = Session("rs").fields("SUCURSAL_ASISTIO").value.ToString
            lbl_duracion_seg_cita.Text = Session("rs").fields("DURACION").value.ToString
            txt_notas_seg_cita.Text = Session("rs").fields("NOTAS_RESULTADO").value.ToString
            lbl_fecha_registro_seg.Text = Session("rs").fields("FECHA_REGISTRO_ASISTIO").value.ToString
            lbl_usuario_seg_cita.Text = Session("rs").fields("CREADO_ASISTIO").value.ToString
            lbl_fecha_real_seg_cita.Text = Session("rs").fields("FECHA_CREADO_ASISTIO").value.ToString

            lbl_prom_pago_cita.Text = Session("rs").fields("PROM_PAGO").value.ToString
            lbl_monto_pago_cita.Text = Session("rs").fields("MONTO_PAGO").value.ToString
        Else
            lbl_id_cita.Text = ""
            lbl_hora_Fecha_cita.Text = ""
            lbl_suc_cita.Text = ""
            lbl_destinatario_cita.Text = ""
            lbl_nombre_destinatario_cita.Text = ""
            txt_notas_cita.Text = ""
            lbl_fecha_registro_cita.Text = ""
            lbl_creado_cita.Text = ""
            lbl_fecha_creado_cita.Text = ""

            lbl_resultado_cita.Text = ""
            lbl_fecha_hora_seg_cita.Text = ""
            lbl_suc_seg_cita.Text = ""
            lbl_duracion_seg_cita.Text = ""
            txt_notas_seg_cita.Text = ""
            lbl_fecha_registro_seg.Text = ""
            lbl_usuario_seg_cita.Text = ""
            lbl_fecha_real_seg_cita.Text = ""

            lbl_prom_pago_cita.Text = ""
            lbl_monto_pago_cita.Text = ""
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_cerrar_cita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar_cita.Click
        ModalPopupExtender_cita.Hide()
    End Sub

    Private Sub citatorio(ByVal idlog As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_CITATORIOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            lbl_citatorio.Text = Session("rs").fields("ID_CITATORIO").value.ToString
            lbl_destinatario_desti.Text = Session("rs").fields("DESTINATARIO").value.ToString
            lbl_nombre_destinatario_desti.Text = Session("rs").fields("NOMBRE_DESTINATARIO").value.ToString + " ( ID:  " + Session("rs").fields("ID_PERSONA").value.ToString + " )"
            txt_notas_desti.Text = Session("rs").fields("NOTAS").value.ToString
            lbl_fecha_registro_desti.Text = Session("rs").fields("FECHA_REGISTRO").value.ToString
            lbl_usuario_desti.Text = Session("rs").fields("CREADO").value.ToString
            lbl_fecha_desti.Text = Session("rs").fields("FECHA_CREADO").value.ToString
            lbl_plantilla_desti.Text = Session("rs").fields("PLANTILLA").value.ToString
            lbl_resultado_desti.Text = Session("rs").fields("RESULTADO").value.ToString
            lbl_fecha_seg_desti.Text = Session("rs").fields("FECHA_REGISTRO_MODIFICADO").value.ToString
            lbl_usuario_seg_desti.Text = Session("rs").fields("MODIFICADO").value.ToString
            lbl_fecha_Real_seg_desti.Text = Session("rs").fields("FECHA_REAL_MODIFICADO").value.ToString
            lbl_prom_pago_ctro.Text = Session("rs").fields("PROM_PAGO").value.ToString
            lbl_monto_pago_ctro.Text = Session("rs").fields("MONTO_PAGO").value.ToString

        Else
            lbl_citatorio.Text = ""
            lbl_destinatario_desti.Text = ""
            lbl_nombre_destinatario_desti.Text = ""
            txt_notas_desti.Text = ""
            lbl_fecha_registro_desti.Text = ""
            lbl_usuario_desti.Text = ""
            lbl_fecha_desti.Text = ""
            lbl_plantilla_desti.Text = ""
            lbl_resultado_desti.Text = ""
            lbl_fecha_seg_desti.Text = ""
            lbl_usuario_seg_desti.Text = ""
            lbl_fecha_Real_seg_desti.Text = ""
            lbl_prom_pago_ctro.Text = ""
            lbl_monto_pago_ctro.Text = ""
        End If
        Session("Con").Close()

    End Sub

    Protected Sub btn_cerrar_citatorios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar_citatorios.Click
        ModalPopupExtender_citatorios.Hide()
    End Sub

    Private Sub avisos(ByVal idlog As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_AVISOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            lbl_aviso.Text = Session("rs").fields("ID_AVISOS").value.ToString
            lbl_destinatario_aviso.Text = Session("rs").fields("DESTINATARIO").value.ToString
            lbl_nombre_destinatario_aviso.Text = Session("rs").fields("NOMBRE_DESTINATARIO").value.ToString + " ( ID:  " + Session("rs").fields("ID_PERSONA").value.ToString + " )"
            txt_notas_seg_aviso.Text = Session("rs").fields("NOTAS").value.ToString
            lbl_fecha_registro_aviso.Text = Session("rs").fields("FECHA_REGISTRO").value.ToString
            lbl_usuario_aviso.Text = Session("rs").fields("CREADO").value.ToString
            lbl_fecha_real_aviso.Text = Session("rs").fields("FECHA_CREADO").value.ToString
            lbl_plantilla_aviso.Text = Session("rs").fields("PLANTILLA").value.ToString
            lbl_resultado_aviso.Text = Session("rs").fields("RESULTADO").value.ToString
            lbl_fecha_registro_seg_aviso.Text = Session("rs").fields("FECHA_REGISTRO_MODIFICADO").value.ToString
            lbl_usuario_seg_aviso.Text = Session("rs").fields("MODIFICADO").value.ToString
            lbl_fecha_real_seg_aviso.Text = Session("rs").fields("FECHA_REAL_MODIFICADO").value.ToString

            lbl_prom_pago_aviso.Text = Session("rs").fields("PROM_PAGO").value.ToString
            lbl_monto_pago_aviso.Text = Session("rs").fields("MONTO_PAGO").value.ToString

        Else
            lbl_aviso.Text = ""
            lbl_destinatario_aviso.Text = ""
            lbl_nombre_destinatario_aviso.Text = ""
            lbl_fecha_registro_aviso.Text = ""
            lbl_usuario_aviso.Text = ""
            lbl_fecha_real_aviso.Text = ""
            lbl_plantilla_aviso.Text = ""
            lbl_resultado_aviso.Text = ""
            lbl_fecha_registro_seg_aviso.Text = ""
            lbl_usuario_seg_aviso.Text = ""
            lbl_fecha_real_seg_aviso.Text = ""
            lbl_prom_pago_aviso.Text = ""
            lbl_monto_pago_aviso.Text = ""
        End If
        Session("Con").Close()

    End Sub

    Protected Sub btn_cerrar_avisos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar_avisos.Click
        ModalPopupExtender_avisos.Hide()
    End Sub

    Private Sub Juicio(ByVal idlog As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_JUICIO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            lbl_estatus_juicio.Text = Session("rs").fields("ESTATUS").value.ToString
            lbl_fecha_diligencia.Text = Session("rs").fields("FECHA").value.ToString
            lbl_juzgado.Text = Session("rs").fields("JUZGADO").value.ToString
            lbl_exhorto.Text = Session("rs").fields("EXHORTO").value.ToString
            lbl_juzgado_exhortado.Text = Session("rs").fields("JUICIO_EXH").value.ToString
            lbl_fecha_prom_pago.Text = Session("rs").fields("PROMPAGO").value.ToString
            lbl_monto_prom_pago.Text = Session("rs").fields("MONTOPROMPAGO").value.ToString
            lbl_user.Text = Session("rs").fields("USERID").value.ToString

            lbl_fecha_sistema.Text = Session("rs").fields("FECHA_SISTEMA").value.ToString
            lbl_fecha_Real.Text = Session("rs").fields("FECHA_CREADO").value.ToString
            lbl_f_emp_aval.Text = Session("rs").fields("FECHAEMPAVAL").value.ToString
            lbl_f_emp_tit.Text = Session("rs").fields("FECHAEMPTIT").value.ToString
            lbl_Gestor.Text = Session("rs").fields("GESTOR").value.ToString
            lbl_detalle.Text = Session("rs").fields("DETALLE").value.ToString()
            lbl_cita.Text = Session("rs").fields("CITA").value.ToString

        Else
            lbl_estatus_juicio.Text = "-"
            lbl_fecha_diligencia.Text = "-"
            lbl_juzgado.Text = "-"
            lbl_exhorto.Text = "-"
            lbl_juzgado_exhortado.Text = "-"
            lbl_fecha_prom_pago.Text = "-"
            lbl_monto_prom_pago.Text = "-"
            lbl_user.Text = "-"
            lbl_fecha_sistema.Text = "-"
            lbl_fecha_Real.Text = "-"
            lbl_f_emp_aval.Text = "-"
            lbl_f_emp_tit.Text = "-"
            lbl_Gestor.Text = "-"
            lbl_detalle.Text = "-"
            lbl_cita.Text = "-"
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_cerrar_juicio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar_juicio.Click
        ModalPopupExtender_juicio.Hide()
    End Sub

    Private Sub Despachos(ByVal idlog As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_DESPACHOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            lbl_user_dexp.Text = Session("rs").fields("USERID_REG").value.ToString
            lbl_fase_dexp.Text = Session("rs").fields("FASE").value.ToString
            lbl_com_dexp.Text = Session("rs").fields("COMISION").value.ToString
            txt_notas_dexp.Text = Session("rs").fields("NOTAS").value.ToString
            lbl_despacho_dexp.Text = Session("rs").fields("DESPACHO").value.ToString
            lbl_fr_dexp.Text = Session("rs").fields("FECHA_SISTEMA").value.ToString
            lbl_frr_dexp.Text = Session("rs").fields("FECHA_CREADO").value.ToString
        Else

            lbl_user_dexp.Text = ""
            lbl_fase_dexp.Text = ""
            lbl_com_dexp.Text = ""
            txt_notas_dexp.Text = ""
            lbl_despacho_dexp.Text = ""
            lbl_fr_dexp.Text = ""
            lbl_frr_dexp.Text = ""
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_ok_dexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok_dexp.Click
        ModalPopupExtender_detalle_asignacion.Hide()
    End Sub

    Private Sub EstatusCobranza(ByVal idlog As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_ESTATUS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            lbl_user_estatus.Text = Session("rs").fields("USERID").value.ToString
            lbl_estatus_cob.Text = Session("rs").fields("ESTATUS").value.ToString
            lbl_estatus_fecha_sistema.Text = Session("rs").fields("FECHA_SISTEMA").value.ToString
            lbl_estatus_fecha_creado.Text = Session("rs").fields("FECHA_CREADO").value.ToString

        Else

            lbl_user_estatus.Text = "-"
            lbl_estatus_cob.Text = "-"
            lbl_estatus_fecha_sistema.Text = "-"
            lbl_estatus_fecha_creado.Text = "-"
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_CERRAR_ESTATUS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar_estatus.Click
        ModalPopupExtender_detalle_estatus.Hide()
    End Sub


    Private Sub visita(ByVal idlog As String)


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDLOG", Session("adVarChar"), Session("adParamInput"), 20, idlog)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONSULTA_VISITA"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_id_visita.Text = Session("rs").fields("ID_VISITA").value.ToString
            lbl_fecha_visita.Text = Session("rs").fields("HORA_FECHA_VISITA").value.ToString
            lbl_suc_visita.Text = Session("rs").fields("SUCURSAL_VISITA").value.ToString
            lbl_destinatario_visita.Text = Session("rs").fields("DESTINATARIO").value.ToString
            lbl_nombre_destinatario_visita.Text = Session("rs").fields("NOMBRE_DESTINATARIO").value.ToString + " ( ID:  " + Session("rs").fields("ID_PERSONA").value.ToString + " )"
            txt_notas_visita.Text = Session("rs").fields("NOTAS").value.ToString
            lbl_fecha_registro_Visita.Text = Session("rs").fields("FECHA_REGISTRO_VISITA").value.ToString
            lbl_user_visita.Text = Session("rs").fields("CREADO_VISITA").value.ToString
            lbl_fecha_real_visita.Text = Session("rs").fields("FECHA_CREADO_VISITA").value.ToString

            lbl_resultado_Visita.Text = Session("rs").fields("RESULTADO").value.ToString
            lbl_hora_seg_visita.Text = Session("rs").fields("HORA_FECHA_ASISTIO").value.ToString
            lbl_suc_seg_visita.Text = Session("rs").fields("SUCURSAL_ASISTIO").value.ToString
            lbl_duracion_Visita.Text = Session("rs").fields("DURACION").value.ToString
            txt_notas_Seg_visita.Text = Session("rs").fields("NOTAS_RESULTADO").value.ToString
            lbl_fecha_reg_seg_visita.Text = Session("rs").fields("FECHA_REGISTRO_ASISTIO").value.ToString
            lbl_user_seg_visita.Text = Session("rs").fields("CREADO_ASISTIO").value.ToString
            lbl_fecha_Real_seg_visita.Text = Session("rs").fields("FECHA_CREADO_ASISTIO").value.ToString

            lbl_prompago_visita.Text = Session("rs").fields("PROM_PAGO").value.ToString
            lbl_monto_pago_visita.Text = Session("rs").fields("MONTO_PAGO").value.ToString

        Else

            lbl_id_visita.Text = ""
            lbl_fecha_visita.Text = ""
            lbl_suc_visita.Text = ""
            lbl_destinatario_visita.Text = ""
            lbl_nombre_destinatario_visita.Text = ""
            txt_notas_visita.Text = ""
            lbl_fecha_registro_Visita.Text = ""
            lbl_user_visita.Text = ""
            lbl_fecha_real_visita.Text = ""

            lbl_resultado_Visita.Text = ""
            lbl_hora_seg_visita.Text = ""
            lbl_suc_seg_visita.Text = ""
            lbl_duracion_Visita.Text = ""
            txt_notas_Seg_visita.Text = ""
            lbl_fecha_reg_seg_visita.Text = ""
            lbl_user_seg_visita.Text = ""
            lbl_fecha_Real_seg_visita.Text = ""
            lbl_prompago_visita.Text = ""
            lbl_monto_pago_visita.Text = ""
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_ok_visita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok_visita.Click
        ModalPopupExtender_visita.Hide()
    End Sub

    Private Sub limpia()
        limpiafiltros()
        cmb_folio.Enabled = False
        dag_historial.Visible = False
        lbl_ProductoA.Visible = False
        lbl_ProductoB.Visible = False
    End Sub


End Class