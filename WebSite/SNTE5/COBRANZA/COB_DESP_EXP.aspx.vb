Public Class COB_DESP_EXP
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Asignación de expedientes", "ASIGNACIÓN DE EXPEDIENTES")
        If Not Me.IsPostBack Then
            Llenafase()
            LlenaSucursales()
            Llenadespacho()
        End If
    End Sub

    Private Sub LLENAEXP()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_EXP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt, Session("rs"))
        Session("Con").Close()
        If dt.Rows.Count > 0 Then
            dag_expedientes.Visible = True
            dag_expedientes.DataSource = dt
            dag_expedientes.DataBind()
        Else
            dag_expedientes.Visible = False
        End If
    End Sub
    'SUCURSALES
    Private Sub LlenaSucursales()

        cmb_sucursal.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_sucursal.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_sucursal.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub


    'DESPACHO/ABOGADO

    Private Sub Llenadespacho()

        cmb_despacho.Items.Clear()
        ' cmb_despacho_asignar.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        'cmb_despacho_asignar.Items.Add(elija)
        cmb_despacho.Items.Add(elija)


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_DESP_EXP_DESP"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            '   cmb_despacho_asignar.Items.Add(item)
            cmb_despacho.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub
    Private Sub Llenadespachomodal()


        cmb_despacho_asignar.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
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
    Private Sub limpiafiltro()
        cmb_sucursal.SelectedIndex = "0"
        cmb_despacho.SelectedIndex = "0"
        txt_minimo.Text = ""
        txtmaximo.Text = ""
        txtfolio.Text = ""
        txtnumcliente.Text = ""
        lbl_status.Text = ""
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
    Public Sub btn_consultar_Click(sender As Object, e As EventArgs) Handles btn_consultar.Click
        LIMPIADAG()


        If (txtmaximo.Text <> "" And txt_minimo.Text = "") Or (txtmaximo.Text = "" And txt_minimo.Text <> "") Then
            lbl_status.Text = "Error: Debe de agregar ambos límites de días de mora"
            Exit Sub

        End If

        Dim SUCURSAL As Integer
        Dim MINMORA As Integer
        Dim MAXMORA As Integer
        Dim ABOGADO As Integer
        Dim Folio As Integer
        Dim NumCliente As Integer

        If cmb_sucursal.SelectedItem.Value.ToString = "0" Then
            SUCURSAL = -1
        Else
            SUCURSAL = cmb_sucursal.SelectedItem.Value.ToString
        End If

        If txtmaximo.Text = "" And txt_minimo.Text = "" Then
            MINMORA = -1
            MAXMORA = -1
        Else
            MINMORA = txt_minimo.Text
            MAXMORA = txtmaximo.Text

        End If

        If cmb_despacho.SelectedItem.Value.ToString = "0" Then
            ABOGADO = -1
        Else
            ABOGADO = cmb_despacho.SelectedItem.Value.ToString
        End If

        If txtfolio.Text = "" Then
            Folio = 0
        Else
            Folio = txtfolio.Text
        End If

        If txtnumcliente.Text = "" Then

            NumCliente = 0
        Else
            NumCliente = txtnumcliente.Text
        End If


        LLENAEXPEDIENTES(SUCURSAL, MINMORA, MAXMORA, ABOGADO, 0, NumCliente, Folio)
    End Sub
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
    Private Sub limpiamodal()
        cmb_fase.SelectedIndex = "0"
        cmb_despacho_asignar.SelectedIndex = "0"
        txt_valor.Text = ""
        txt_Objetivo.Text = ""
        lbl_status_modal.Text = ""
    End Sub
    Private Sub dag_expedientes_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dag_expedientes.ItemCommand
        lbl_status.Text = ""

        If (e.CommandName = "EDITAR") Then

            limpiamodal()

            modal_port.Show()
            Session("FOLIO") = e.Item.Cells(0).Text
            Llenadespachomodal()
            Llenafase()

            cargarAsignados(Session("FOLIO"))

        End If
    End Sub
    Private Sub LLENAEXPEDIENTES(ByVal IDSUC As String, ByVal INFMORA As String, ByVal SUPMORA As String, ByVal IDDESPACHO As String, ByVal Filtro As Integer, ByVal NUMCLIENTE As Integer, ByVal FOLIO As Integer)

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
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, IDSUC)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INFMORA", Session("adVarChar"), Session("adParamInput"), 15, INFMORA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUPMORA", Session("adVarChar"), Session("adParamInput"), 15, SUPMORA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, IDDESPACHO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMCLIENTE", Session("adVarChar"), Session("adParamInput"), 10, NUMCLIENTE)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_EXP"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dthistorial, Session("rs"))

        If dthistorial.Rows.Count > 0 Then
            dag_expedientes.Visible = True
            'se vacian los expedientes al formulario
            dag_expedientes.DataSource = dthistorial
            dag_expedientes.DataBind()
        Else
            lbl_status.Text = "No existen registros"
            dag_expedientes.Visible = False
        End If

        Session("Con").Close()

    End Sub
    Private Sub dag_expedientes_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dag_expedientes.ItemDataBound
        'se oculta la columna de ID abogado
        e.Item.Cells(8).Visible = False
    End Sub

    Private Sub dag_expedientes_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dag_expedientes.PageIndexChanged

        dag_expedientes.CurrentPageIndex = e.NewPageIndex

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
            txt_valor.Text = Session("rs").Fields("PCTJE").Value.ToString
            txt_Objetivo.Text = Session("rs").Fields("NOTAS").Value.ToString

        End If

        Session("Con").Close()



    End Sub

    Public Sub btneliminar_Click(sender As Object, e As EventArgs)
        limpiafiltro()
        LIMPIADAG()
    End Sub



    Private Sub btn_evalxcerrar_Click(sender As Object, e As EventArgs) Handles btn_evalxcerrar.Click
        modal_port.Hide()
        limpiamodal()
    End Sub
    Private Function Validamonto(ByVal monto As String) As Boolean

        Dim Valor As Boolean = True

        If monto <> "" Then
            Valor = Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))

        End If

        Return Valor
    End Function
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
                sbHtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>MASCORE</td></tr>")
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
    Private Sub Enviar_Correo(ByVal Folio As String, ByVal iddespacho As String)

        Motor_On_off()
        If Session("MOTOR") = "1" Then
            envioemail(Folio, iddespacho)
        End If

        Session("MOTOR") = Nothing
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

        'Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 10, (Session("FOLIO")))
        'Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_DESP_EXP"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()
        lbl_status_modal.Text = "Guardado Correctamente"

    End Sub
    Private Sub consulta()
        LIMPIADAG()


        If (txtmaximo.Text <> "" And txt_minimo.Text = "") Or (txtmaximo.Text = "" And txt_minimo.Text <> "") Then
            lbl_status.Text = "Error: Debe de agregar ambos límites de días de mora"
            Exit Sub

        End If

        Dim SUCURSAL As Integer
        Dim MINMORA As Integer
        Dim MAXMORA As Integer
        Dim ABOGADO As Integer
        Dim Folio As Integer
        Dim NumCliente As Integer

        If cmb_sucursal.SelectedItem.Value.ToString = "0" Then
            SUCURSAL = -1
        Else
            SUCURSAL = cmb_sucursal.SelectedItem.Value.ToString
        End If

        If txtmaximo.Text = "" And txt_minimo.Text = "" Then
            MINMORA = -1
            MAXMORA = -1
        Else
            MINMORA = txt_minimo.Text
            MAXMORA = txtmaximo.Text

        End If

        If cmb_despacho.SelectedItem.Value.ToString = "0" Then
            ABOGADO = -1
        Else
            ABOGADO = cmb_despacho.SelectedItem.Value.ToString
        End If

        If txtfolio.Text = "" Then
            Folio = 0
        Else
            Folio = txtfolio.Text
        End If

        If txtnumcliente.Text = "" Then

            NumCliente = 0
        Else
            NumCliente = txtnumcliente.Text
        End If


        LLENAEXPEDIENTES(SUCURSAL, MINMORA, MAXMORA, ABOGADO, 0, NumCliente, Folio)
    End Sub
    Private Sub btn_guarda_modal_Click(sender As Object, e As EventArgs) Handles btn_guarda_modal.Click
        lbl_status_modal.Visible = True
        If txt_Objetivo.Text.Length > 3000 Then
            modal_port.Show()
            lbl_status_modal.Text = "Error: Sólo 3000 caracteres o menos en las notas"
            Exit Sub
        End If

        If Validamonto(txt_valor.Text) = False Then
            modal_port.Show()
            lbl_status_modal.Text = "Error: Porcentaje incorrecto"
            Exit Sub
        End If

        If txt_valor.Text <> "" Then
            If CDec(txt_valor.Text) > 100 Then
                modal_port.Show()
                lbl_status_modal.Text = "Error: Excede del 100 %"
                Exit Sub
            End If
        End If


        Asignar(Session("FOLIO"), cmb_despacho_asignar.SelectedItem.Value.ToString, cmb_fase.SelectedItem.Value.ToString, txt_valor.Text, txt_Objetivo.Text)
        LLENAEXPEDIENTES(1, -1, -1, -1, 0, 0, 0)
        'Se verifica si asignaron despacho para enviar correo, de lo contrario no envía correo. (Se quita candado de forzar a elegir un abogado cuando cambie la fase de cobranza
        If cmb_despacho_asignar.SelectedItem.Value <> "-1" Then
            Enviar_Correo(Session("FOLIO"), cmb_despacho_asignar.SelectedItem.Value.ToString)
        End If


    End Sub

End Class