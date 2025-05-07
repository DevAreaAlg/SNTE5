Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class CRED_VEN_PAGO_CRED
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            If Session("VENGODE") = "ExpedientesFACT.aspx" Then
                pnl_Credito.Visible = False
                pnl_DevAforo.Visible = True
                FactirajeFormasPago()
                CreaTablas()
                LlenaDatosPagFact()
            Else
                pnl_Credito.Visible = True
                pnl_DevAforo.Visible = False
                Clasificacion()
                ObtieneDatos()
                VerificaFormasPago()
                If Session("CLASIFICACION") <> "ARFIN" Then
                    CreaTablas()
                    LlenoComisiones()
                Else
                    get_info_arfin()
                    lbl_pago_anticipo.Visible = True
                    btn_Aplicar.Enabled = True
                    sw_ctrl_arfin(False)
                    crea_tablas_arfin()
                End If

                lbl_Cliente.Text = Session("PROSPECTO").ToString + " (" + Session("PERSONAID").ToString + ")"
                lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO").ToString
                lbl_Producto.Text = Session("PRODUCTO").ToString
            End If

            If Session("CLASIFICACION") = "ARFIN" Then
                Llenobancoschequesori()
            Else
                Llenobancoscheques()
            End If
            Llenobancos()
        End If

        If Session("VENGODE") <> "ExpedientesFACT.aspx" Then
            Llenactascap()
        End If
        HiddenPrinterName.Value = "EPSON TM-U220 Receipt"
        'HiddenPrinterName.Value = "Canon MF4800 Series UFRII LT"
        TryCast(Me.Master, MasterMascore).CargaASPX("Pago de Préstamo", "Pago de Préstamo")
    End Sub

    Private Sub crea_tablas_arfin()
        Dim tabla_bancosori, tabla_chequesori As New DataTable
        tabla_bancosori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO", GetType(String))
        tabla_bancosori.Columns.Add("MONTO", GetType(Decimal))
        tabla_chequesori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_chequesori.Columns.Add("ID_BANCO", GetType(Integer))
        tabla_chequesori.Columns.Add("BANCO", GetType(String))
        tabla_chequesori.Columns.Add("NUMCUENTA", GetType(String))
        tabla_chequesori.Columns.Add("CHEQUE", GetType(String))
        tabla_chequesori.Columns.Add("MONTO", GetType(Decimal))
        tabla_chequesori.Columns.Add("ESTATUS", GetType(String))
        Session("tabla_bancosori") = tabla_bancosori
        Session("tabla_chequesori") = tabla_chequesori
    End Sub

    Private Sub Llenobancoschequesori()
        cmb_BancoCheques.Items.Clear()
        cmb_BancoCheques.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                cmb_BancoCheques.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub sw_ctrl_arfin(status As Boolean)
        lbl_BancoCliente.Visible = status
        cmb_BancoCliente.Visible = status
        RequiredFieldValidator_BancoCliente.Enabled = status
        lbl_ClabeCliente.Visible = status
        txt_ClabeCliente.Visible = status
        FilteredTextBoxExtender_ClabeCliente.Enabled = status
        RequiredFieldValidator_ClabeCliente.Enabled = status
        RegularExpressionValidator_ClabeCliente.Enabled = status
        lbl_NumCtaCliente.Visible = status
        txt_NumCtaCliente.Visible = status
        RequiredFieldValidator_NumCtaCliente.Enabled = status
        dag_Bancos.Visible = status
        dag_Cheques.Visible = status
        dag_bancosori.Visible = Not status
        dag_chequesori.Visible = Not status
        lbl_ctachequesori.Visible = Not status
        txt_ctachequesori.Visible = Not status
        FilteredTextBoxExtender_ctachequesori.Enabled = Not status
        RequiredFieldValidator_ctachequesori.Enabled = Not status
        lbl_modo_rec.Visible = Not status
        cmb_modo_rec.Visible = Not status
        rfv_modo_rec.Enabled = Not status
    End Sub

    Private Sub get_info_arfin()
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ARFIN"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Session("anticipoArfin") = Session("rs").Fields("ANTICIPO").Value
            lbl_pago_anticipo.Text = "Es necesario pagar la cantidad de: " + FormatCurrency(Session("anticipoArfin")).ToString + " por concepto de ANTICIPO. Este anticipo incluye la renta por"
            lbl_pago_anticipo.Text += " adelantado, una renta como depostio, asi como culquier costo de seguro o gasto de administrativo que pudiera aplicar.<br />"
        End If
        Session("Con").Close()
    End Sub

    Private Sub Clasificacion()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CLASIF_CRED"
        Session("rs") = Session("cmd").Execute()
        Session("CLASIFICACION") = Session("rs").Fields("CLAVE").Value.ToString
        Session("TIPO_LINEA") = Session("rs").Fields("TIPO_LINEA").Value.ToString
        Session("FOLIO_LINEA") = Session("rs").Fields("FOLIO_LINEA").Value.ToString
        Session("Con").Close()
    End Sub

    Private Sub ObtieneDatos()
        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        Session("MONTO") = Session("rs").fields("MONTO").value.ToString
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_USUARIO"
        Session("rs") = Session("cmd").Execute()
        Session("USUARIO") = Session("rs").fields("NOMBRE").value.ToString
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_SUCURSAL_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        Session("SUCURSAL") = Session("rs").fields("NOMBRE").value.ToString

        If Session("TIPO_LINEA") = "FACT" Then
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FACT_DATOS_PAGO"
            Session("rs") = Session("cmd").Execute()
            Session("MONTO_AFORO") = Session("rs").fields("MONTO_AFORO").value.ToString
            Session("MONTO_REAL") = Session("MONTO")
            Session("MONTO") = Session("MONTO") - Session("MONTO_AFORO")
            lbl_Monto.Text = "Monto a Pagar: " + FormatCurrency(Session("MONTO").ToString) + " (Monto Total: " + FormatCurrency(Session("MONTO_REAL")) + " - Monto de Aforo: " + FormatCurrency(Session("MONTO_AFORO")) + ")"
        Else
            lbl_Monto.Text = FormatCurrency(Session("MONTO").ToString)
        End If
        Session("Con").Close()
    End Sub

    Private Sub FactirajeFormasPago()
        'CUENTAS DE CAPTACION
        pnl_CtasCap.Enabled = False
        tit_cuentas.Text = ""

        'CHEQUES
        cmb_BancoCheques.Enabled = True
        txt_MontoCheques.Enabled = True
        txt_NumCheques.Enabled = True
        btn_AgregarCheques.Enabled = True

        'EFECTIVO
        txt_CajaMonto.Enabled = False

        'BANCOS
        cmb_Banco.Enabled = True
        txt_MontoBanco.Enabled = True
        btn_AgregarBanco.Enabled = True
        cmb_BancoCliente.Enabled = True
        txt_NumCtaCliente.Enabled = True
        txt_ClabeCliente.Enabled = True

        'COMISIONES
        Chk_FinanCom.Enabled = False
        Chk_FinanCom.Visible = False
        lbl_TotalCom.Visible = False
        dag_Comisiones.Visible = False
        'lbl_InfoCom.Text = ""
        lbl_TotalComMonto.Text = ""
        lbl_PagaCom.Visible = False

    End Sub

    Private Sub VerificaFormasPago()
        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_FORMAS_PAGO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Select Case (Session("rs").fields("FORMAPAGO").value.ToString)

                Case "1" 'DEPOSITO 
                    pnl_CtasCap.Enabled = True
                    tit_cuentas.Text = "CUENTAS"
                Case "2" 'CHEQUE A NOMBRE DEL CLIENTE
                    cmb_BancoCheques.Enabled = True
                    txt_MontoCheques.Enabled = True
                    txt_NumCheques.Enabled = True
                    btn_AgregarCheques.Enabled = True
                Case "4" 'EFECTIVO EN SUCURSAL
                    txt_CajaMonto.Enabled = True
                Case "5" 'TRANSFERENCIA BANCARIA A CUENTA DE CLEINTE
                    cmb_Banco.Enabled = True
                    txt_MontoBanco.Enabled = True
                    btn_AgregarBanco.Enabled = True
                    cmb_BancoCliente.Enabled = True
                    txt_NumCtaCliente.Enabled = True
                    txt_ClabeCliente.Enabled = True
            End Select

            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CreaTablas()

        Dim tabla_bancos As New DataTable
        Dim tabla_bancos_cliente As New DataTable
        Dim tabla_cheques As New DataTable

        tabla_bancos.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancos.Columns.Add("BANCO", GetType(String))
        tabla_bancos.Columns.Add("MONTO", GetType(Decimal))
        tabla_bancos_cliente.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancos_cliente.Columns.Add("BANCO", GetType(String))
        tabla_bancos_cliente.Columns.Add("MONTO", GetType(Decimal))
        tabla_bancos_cliente.Columns.Add("ID_BANCO_CLIENTE", GetType(Integer))
        tabla_bancos_cliente.Columns.Add("BANCO_CLIENTE", GetType(String))
        tabla_bancos_cliente.Columns.Add("CLABE", GetType(String))
        tabla_bancos_cliente.Columns.Add("NUM_CTA_CLIENTE", GetType(String))

        tabla_cheques.Columns.Add("ID_CTA", GetType(Integer))
        tabla_cheques.Columns.Add("ID_BANCO", GetType(Integer))
        tabla_cheques.Columns.Add("BANCO", GetType(String))
        tabla_cheques.Columns.Add("NUMCUENTA", GetType(String))
        tabla_cheques.Columns.Add("CHEQUE", GetType(String))
        tabla_cheques.Columns.Add("MONTO", GetType(Decimal))
        tabla_cheques.Columns.Add("ESTATUS", GetType(String))

        Session("tabla_bancos") = tabla_bancos
        Session("tabla_bancos_cliente") = tabla_bancos_cliente
        Session("tabla_cheques") = tabla_cheques

    End Sub

    Protected Sub LlenaDatosPagFact()

        'Se llena una tabla con los lotes pagares pendientes por ser recibidos
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtLnCredAct As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDFACTPAGO", Session("adVarChar"), Session("adParamInput"), 10, Session("IDFACTPAGO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FACT_DATOS_PAGO_OPERAR"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF() Then
            lbl_FactProv.Text = Session("rs").fields("PROVEEDOR").value.ToString
            Session("MONTO_AFORO") = Session("rs").fields("MONTO_AFORO").value.ToString
            Session("INT_X_COB") = Session("rs").fields("INT_X_COB").value.ToString
            Session("IVA_X_COB") = Session("rs").fields("IVA_X_COB").value.ToString
            Session("RESTANTE_AFORO") = (CDec(Session("MONTO_AFORO")) - (CDec(Session("INT_X_COB")) + CDec(Session("IVA_X_COB"))))

            lbl_FactMontoAforo.Text = FormatCurrency(Session("rs").fields("MONTO_AFORO").value.ToString)
            lbl_FactInteres.Text = FormatCurrency(Session("rs").fields("INT_X_COB").value.ToString)
            lbl_FactIva.Text = FormatCurrency(Session("rs").fields("IVA_X_COB").value.ToString)
            lbl_FactTotalPago.Text = FormatCurrency(CDec(Session("INT_X_COB")) + CDec(Session("IVA_X_COB")))
            lbl_FactRestanteAforo.Text = FormatCurrency(Session("RESTANTE_AFORO"))

            If Session("rs").fields("AFORO_ESTATUS").value.ToString = "0" And Session("rs").fields("PERMISO_DEV_AFORO").value.ToString = "1" Then
                btn_Aplicar.Enabled = True
            Else
                btn_Aplicar.Enabled = False
            End If

        End If

        Session("Con").Close()

    End Sub

    Private Sub LlenoComisiones()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtComisiones As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMAPE_PAGO_CRED"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtComisiones, Session("rs"))
        Session("Con").Close()
        If dtComisiones.Rows.Count > 0 Then
            pnl_comision.Visible = True
            dag_Comisiones.Visible = True
            dag_Comisiones.DataSource = dtComisiones
            dag_Comisiones.DataBind()
        Else
            dag_Comisiones.Visible = False
        End If

    End Sub

    Protected Sub dag_Comisiones_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_Comisiones.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim total As Decimal = Decimal.Parse(DataBinder.Eval(e.Item.DataItem, "TOTAL").ToString())

            If total > 0.0 Then
                Session("TOTAL_COM") = total
                Chk_FinanCom.Enabled = True
                Chk_FinanCom.Checked = True
                Chk_FinanCom.Text = "Activado"
                lbl_InfoCom.Text = ""
            Else
                Session("TOTAL_COM") = 0.0
                Chk_FinanCom.Enabled = False
                Chk_FinanCom.Checked = False
                Chk_FinanCom.Text = "Desactivado"
                lbl_InfoCom.Text = ""
            End If
        End If

        lbl_TotalComMonto.Text = FormatCurrency(Session("TOTAL_COM"))

    End Sub

    Protected Sub Chk_FinanCom_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk_FinanCom.CheckedChanged

        If Chk_FinanCom.Checked Then
            Chk_FinanCom.Text = "Activado"
            lbl_TotalComMonto.Text = FormatCurrency(Session("TOTAL_COM"))
            lbl_InfoCom.Text = ""
        Else
            Chk_FinanCom.Text = "Desactivado"
            lbl_TotalComMonto.Text = FormatCurrency(0.0)
            lbl_InfoCom.Text = "Alerta: No se financiará la comision de apertura con una parte del credito."
        End If

        lbl_Info.Text = ""

    End Sub

    Private Sub Llenactascap()

        pnl_CtasCap.Controls.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, CInt(Session("PERSONAID")))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 25, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPRET_CTAS_CAPTACION_DESTINO"
        Session("rs") = Session("cmd").Execute()

        Dim i As Integer
        Dim descap(100) As Label
        Dim txtcap(100) As TextBox
        Dim cr(100) As Literal
        i = 0

        Do While Not Session("rs").EOF

            'Declaro los arreglos
            descap(i) = New Label
            txtcap(i) = New TextBox
            cr(i) = New Literal

            descap(i).Text = Session("rs").Fields("DESCRIPCION").Value.ToString
            descap(i).CssClass = "texto"
            cr(i).Text = "<br />"
            descap(i).Width = 350

            'txtcap(i).Width = 100
            txtcap(i).MaxLength = 10
            txtcap(i).CssClass = "text_input_nice_input w_100"
            txtcap(i).ID = ("des_" + Session("rs").Fields("FOLIO").Value.ToString)

            pnl_CtasCap.Controls.Add(descap(i))
            pnl_CtasCap.Controls.Add(cr(i))
            pnl_CtasCap.Controls.Add(txtcap(i))
            pnl_CtasCap.Controls.Add(cr(i))

            Session("rs").movenext()
            i = i + 1
        Loop

        Session("Con").Close()

        Session("txtcap") = txtcap

    End Sub

    Private Sub Llenobancos()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_Banco.Items.Clear()
        cmb_Banco.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                cmb_Banco.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

        Dim elija2 As New ListItem("ELIJA", "-1")
        cmb_BancoCliente.Items.Clear()
        cmb_BancoCliente.Items.Add(elija2)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value.ToString, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value.ToString)
                cmb_BancoCliente.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Private Sub Llenobancoscheques()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_BancoCheques.Items.Clear()
        cmb_BancoCheques.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                cmb_BancoCheques.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Private Function Validamonto(ByVal monto As String) As Boolean

        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))

    End Function

    Protected Sub btn_AgregarBanco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarBanco.Click

        lbl_InfoBanco.Text = ""

        If Validamonto(txt_MontoBanco.Text) Then
            If Session("CLASIFICACION") = "ARFIN" Then
                Session("tabla_bancosori").Rows.Add(CInt(cmb_Banco.SelectedItem.Value), cmb_Banco.SelectedItem.Text, CDec(txt_MontoBanco.Text))
                dag_bancosori.DataSource = Session("tabla_bancosori")
                dag_bancosori.DataBind()
            Else
                Session("tabla_bancos").Rows.Add(cmb_Banco.SelectedItem.Value, cmb_Banco.SelectedItem.Text, CDec(txt_MontoBanco.Text))
                Session("tabla_bancos_cliente").Rows.Add(cmb_Banco.SelectedItem.Value, cmb_Banco.SelectedItem.Text, CDec(txt_MontoBanco.Text), cmb_BancoCliente.SelectedItem.Value, cmb_BancoCliente.SelectedItem.Text, txt_ClabeCliente.Text, txt_NumCtaCliente.Text)
                dag_Bancos.DataSource = Session("tabla_bancos_cliente")
                dag_Bancos.DataBind()
            End If

            Llenobancos()
            txt_MontoBanco.Text = ""
            txt_ClabeCliente.Text = ""
            txt_NumCtaCliente.Text = ""
        Else
            lbl_InfoBanco.Text = " Monto incorrecto!"
        End If

        lbl_Info.Text = ""

    End Sub

    Private Sub dag_bancosori_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_bancosori.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_InfoBanco.Text = ""

            Session("tabla_bancosori").Rows(e.Item.ItemIndex).Delete()
            dag_bancosori.DataSource = Session("tabla_bancosori")
            dag_bancosori.DataBind()
            Llenobancos()
            txt_MontoBanco.Text = ""
            txt_ClabeCliente.Text = ""
            txt_NumCtaCliente.Text = ""
        End If

    End Sub

    Private Sub dag_Bancos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Bancos.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_InfoBanco.Text = ""

            Session("tabla_bancos").Rows(e.Item.ItemIndex).Delete()
            Session("tabla_bancos_cliente").Rows(e.Item.ItemIndex).Delete()
            dag_Bancos.DataSource = Session("tabla_bancos_cliente")
            dag_Bancos.DataBind()
            Llenobancos()
            txt_MontoBanco.Text = ""
            txt_ClabeCliente.Text = ""
            txt_NumCtaCliente.Text = ""
        End If

    End Sub

    Protected Sub btn_AgregarCheques_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarCheques.Click

        lbl_InfoCheques.Text = ""

        If Validamonto(txt_MontoCheques.Text) Then
            If Session("CLASIFICACION") = "ARFIN" Then
                Session("tabla_chequesori").Rows.Add(0, CInt(cmb_BancoCheques.SelectedItem.Value), cmb_BancoCheques.SelectedItem.Text, txt_ctachequesori.Text, txt_NumCheques.Text, CDec(txt_MontoCheques.Text), cmb_modo_rec.SelectedItem.Value)
                dag_chequesori.DataSource = Session("tabla_chequesori")
                dag_chequesori.DataBind()
                Llenobancoschequesori()
                txt_ctachequesori.Text = ""
                cmb_modo_rec.ClearSelection()
                cmb_modo_rec.Items.FindByValue("SBC").Selected = True
            Else
                Session("tabla_cheques").Rows.Add(CInt(cmb_BancoCheques.SelectedItem.Value), 0, cmb_BancoCheques.SelectedItem.Text, "", txt_NumCheques.Text, CDec(txt_MontoCheques.Text), "PAG")
                dag_Cheques.DataSource = Session("tabla_cheques")
                dag_Cheques.DataBind()
                Llenobancoscheques()
            End If

            txt_NumCheques.Text = ""
            txt_MontoCheques.Text = ""
        Else
            lbl_InfoCheques.Text = "Monto incorrecto!"
        End If

        lbl_Info.Text = ""

    End Sub

    Private Sub dag_chequesori_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_chequesori.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_InfoCheques.Text = ""

            Session("tabla_chequesori").Rows(e.Item.ItemIndex).Delete()
            dag_chequesori.DataSource = Session("tabla_chequesori")
            dag_chequesori.DataBind()

            Llenobancoschequesori()
            txt_ctachequesori.Text = ""
            cmb_modo_rec.ClearSelection()
            cmb_modo_rec.Items.FindByValue("SBC").Selected = True
            txt_NumCheques.Text = ""
            txt_MontoCheques.Text = ""
        End If

    End Sub

    Private Sub dag_Cheques_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Cheques.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor
        If (e.CommandName = "ELIMINAR") Then
            lbl_InfoCheques.Text = ""

            Session("tabla_cheques").Rows(e.Item.ItemIndex).Delete()
            dag_Cheques.DataSource = Session("tabla_cheques")
            dag_Cheques.DataBind()
            Llenobancoscheques()
            txt_NumCheques.Text = ""
            txt_MontoCheques.Text = ""
        End If

    End Sub

    Protected Sub btn_Aplicar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Aplicar.Click
        ' Ticket
        Dim bancos, cheques, ctas, caja As Decimal, sbancos, scheques, sctas, scaja As String

        If Session("CLASIFICACION") <> "ARFIN" Then
            sbancos = (Session("tabla_bancos").Compute("Sum(MONTO)", "MONTO<>0")).ToString
            scheques = (Session("tabla_cheques").Compute("Sum(MONTO)", "MONTO<>0")).ToString
            scaja = txt_CajaMonto.Text
            sctas = (ctas_cap().Compute("Sum(MONTO)", "MONTO<>0")).ToString

            If scaja <> "" Or sctas <> "" Or sbancos <> "" Or scheques <> "" Then
                If sbancos <> "" Then
                    bancos = CDec(sbancos)
                Else
                    bancos = 0.0
                End If

                If scheques <> "" Then
                    cheques = CDec(scheques)
                Else
                    cheques = 0.0
                End If

                If scaja <> "" Then
                    caja = CDec(scaja)
                Else
                    caja = 0.0
                End If

                If sctas <> "" Then
                    ctas = CDec(sctas)
                Else
                    ctas = 0.0
                End If

                If Session("VENGODE") = "ExpedientesFACT.aspx" Then
                    If Session("RESTANTE_AFORO") = (bancos + cheques + caja + ctas) Then

                        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                            'Create a DataTable with the modified rows.

                            connection.Open()
                            'Configure the SqlCommand and SqlParameter.
                            Dim insertCommand As New SqlCommand("INS_FACT_DEVAFORO_COBINTERES", connection)
                            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                            Session("parm") = New SqlParameter("IDFACTPAGO", SqlDbType.Int)
                            Session("parm").Value = Session("IDFACTPAGO")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("MONTO_AFORO", SqlDbType.Decimal)
                            Session("parm").Value = Session("MONTO_AFORO")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("INT_X_COB", SqlDbType.Decimal)
                            Session("parm").Value = Session("INT_X_COB")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("IVA_X_COB", SqlDbType.Decimal)
                            Session("parm").Value = Session("IVA_X_COB")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("RESTANTE_AFORO", SqlDbType.Decimal)
                            Session("parm").Value = Session("RESTANTE_AFORO")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("BANCOS", SqlDbType.Structured)
                            Session("parm").Value = Session("tabla_bancos")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("BANCOS_CTA_CLIENTE", SqlDbType.Structured)
                            Session("parm").Value = Session("tabla_bancos_cliente")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("CHEQUES", SqlDbType.Structured)
                            Session("parm").Value = Session("tabla_cheques")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("EFECTIVO", SqlDbType.Decimal)
                            Session("parm").Value = caja
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("IDSUC", SqlDbType.Int)
                            Session("parm").Value = Session("SUCID")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                            Session("parm").Value = Session("USERID")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("Sesion", SqlDbType.VarChar)
                            Session("parm").Value = Session("Sesion")
                            insertCommand.Parameters.Add(Session("parm"))

                            'Execute the command.
                            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                            While myReader.Read()
                                Session("RES") = myReader.GetString(0)
                                Session("SERIE") = myReader.GetString(1)
                                Session("FOLIO_IMP") = myReader.GetString(2)
                            End While
                            myReader.Close()

                        End Using

                        If Session("RES") = "1" Then
                            Limpiar()
                            lbl_Info.Text = ""
                            'Response.Write("<script language='javascript'> {var wbusf =window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, wbusf, ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1""); } </script>")
                            'HiddenRawData.Value = impresion_ticket()
                            HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))
                            borra_tablas()
                            CreaTablas()
                            'Llenactascap()
                            LimpiarFactoraje()
                            Llenobancoscheques()
                            Llenobancos()

                        Else
                            lbl_Info.Text = "El número de cheque capturado ya existe en la base de datos"
                            Exit Sub
                        End If
                    Else
                        lbl_Info.Text = "Error: El monto acumulado no coincide con el Monto Restante de Aforo."
                    End If
                Else
                    If (Chk_FinanCom.Checked = False And Session("MONTO") = (bancos + cheques + caja + ctas) = True) Or (Chk_FinanCom.Checked = True And (Session("MONTO") - Session("TOTAL_COM")) = (bancos + cheques + caja + ctas) = True) Then

                        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                            'Create a DataTable with the modified rows.

                            connection.Open()
                            'Configure the SqlCommand and SqlParameter.
                            Dim insertCommand As New SqlCommand("INS_CREDITO_PAGO_INICIAL", connection)
                            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                            Session("parm") = New SqlParameter("FOLIO", SqlDbType.Int)
                            Session("parm").Value = Session("FOLIO")
                            insertCommand.Parameters.Add(Session("parm"))

                            If Chk_FinanCom.Checked = True Then
                                Session("parm") = New SqlParameter("COMISION", SqlDbType.Decimal)
                                Session("parm").Value = Session("TOTAL_COM")
                                insertCommand.Parameters.Add(Session("parm"))
                            Else
                                Session("parm") = New SqlParameter("COMISION", SqlDbType.Decimal)
                                Session("parm").Value = 0.0
                                insertCommand.Parameters.Add(Session("parm"))
                            End If

                            Session("parm") = New SqlParameter("CTAS_CAP", SqlDbType.Structured)
                            Session("parm").Value = ctas_cap()
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("BANCOS", SqlDbType.Structured)
                            Session("parm").Value = Session("tabla_bancos")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("BANCOS_CTA_CLIENTE", SqlDbType.Structured)
                            Session("parm").Value = Session("tabla_bancos_cliente")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("CHEQUES", SqlDbType.Structured)
                            Session("parm").Value = Session("tabla_cheques")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("EFECTIVO", SqlDbType.Decimal)
                            Session("parm").Value = caja
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("IDSUC", SqlDbType.Int)
                            Session("parm").Value = Session("SUCID")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("IDPERSONA", SqlDbType.Int)
                            Session("parm").Value = Session("PERSONAID")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                            Session("parm").Value = Session("USERID")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("ID_EQ", SqlDbType.Int)
                            Session("parm").Value = Session("ID_EQ")
                            insertCommand.Parameters.Add(Session("parm"))

                            Session("parm") = New SqlParameter("Sesion", SqlDbType.VarChar)
                            Session("parm").Value = Session("Sesion")
                            insertCommand.Parameters.Add(Session("parm"))

                            'Execute the command.
                            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                            While myReader.Read()
                                Session("RES") = myReader.GetString(0)
                                Session("SERIE") = myReader.GetString(1)
                                Session("FOLIO_IMP") = myReader.GetString(2)
                            End While
                            myReader.Close()

                        End Using

                        If Session("RES") = "1" Then

                            If bancos > 0.0 Or cheques > 0.0 Then
                                ObtieneCorreosFinanzas()
                            End If

                            Limpiar()
                            'Response.Write("<script language='javascript'> {var wbusf =window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, wbusf, ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1""); } </script>")
                            'HiddenRawData.Value = impresion_ticket()
                            HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))

                            If caja > 0.0 Then

                                Session("MONTO_EFECTIVO") = caja
                                Session("ENTRADASALIDA") = "SALIDA"

                                LlamaTiraEfectivo()

                            End If

                            'Enviar correo de confirmación
                            Enviar_Correo()

                            borra_tablas()
                            CreaTablas()
                            Llenactascap()
                            Llenobancoscheques()
                            Llenobancos()
                            lbl_Info.Text = "Pago realizado con éxito"
                        Else
                            lbl_Info.Text = "El número de cheque capturado ya existe en la base de datos"
                            Exit Sub
                        End If
                    Else
                        If Chk_FinanCom.Checked = True Then
                            lbl_Info.Text = "Error: El monto acumulado debe ser de: " + FormatCurrency(Session("MONTO") - Session("TOTAL_COM")) + " (Monto de credito menos la Comision de Apertura)"
                        Else
                            lbl_Info.Text = "Error: El monto acumulado no coincide con el monto de préstamo autorizado."
                        End If
                        Llenactascap()
                    End If
                End If
            Else
                lbl_Info.Text = "Error: No ha introducido ningun monto"
                Llenactascap()
            End If


        Else

            scaja = txt_CajaMonto.Text
            sbancos = (Session("tabla_bancosori").Compute("Sum(MONTO)", "MONTO<>0")).ToString
            scheques = (Session("tabla_chequesori").Compute("Sum(MONTO)", "MONTO<>0")).ToString

            If scaja <> "" Or sbancos <> "" Or scheques <> "" Then
                If scaja <> "" Then
                    caja = CDec(scaja)
                Else
                    caja = 0.0
                End If

                If scheques <> "" Then
                    cheques = CDec(scheques)
                Else
                    cheques = 0.0
                End If

                If sbancos <> "" Then
                    bancos = CDec(sbancos)
                Else
                    bancos = 0.0
                End If

            Else
                lbl_Info.Text = "Error: No ha introducido ningun monto"
            End If


            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                'Create a DataTable with the modified rows.

                connection.Open()
                'Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("UPD_CNFEXP_PAGA_ARFIN", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("FOLIO", SqlDbType.Int)
                Session("parm").Value = Session("FOLIO")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("BANCOS", SqlDbType.Structured)
                Session("parm").Value = Session("tabla_bancos")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("CHEQUES", SqlDbType.Structured)
                Session("parm").Value = Session("tabla_cheques")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("ID_EQ", SqlDbType.Int)
                Session("parm").Value = Session("ID_EQ")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("ID_USER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                'Execute the command.
                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()
                    Session("RES") = myReader.GetString(0)
                    Session("SERIE") = myReader.GetString(1)
                    Session("FOLIO_IMP") = myReader.GetString(2)
                End While
                myReader.Close()

            End Using

            Limpiar()
            lbl_Info.Text = ""
            'Response.Write("<script language='javascript'> {var wbusf =window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, wbusf, ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1""); } </script>")
            'HiddenRawData.Value = impresion_ticket()
            HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))
        End If
    End Sub

    Function ctas_cap() As DataTable

        Dim ctas As New DataTable
        ctas.Columns.Add("FOLIO", GetType(Integer))
        ctas.Columns.Add("MONTO", GetType(Decimal))

        Dim i As Integer
        Dim txtcap(100) As TextBox
        Dim dato As String

        If Not Session("txtcap") Is Nothing Then
            txtcap = Session("txtcap")
        End If

        i = 0

        Do While Not txtcap(i) Is Nothing
            'Calculos de ingresos y egresos.
            dato = txtcap(i).Text
            If dato <> "" Then
                ctas.Rows.Add(CInt(Mid(txtcap(i).ID.ToString, 5)), CDec(dato))
            End If

            i = i + 1
        Loop
        Return ctas

    End Function

    Private Sub borra_tablas()

        Session("tabla_bancos").Clear()
        Session("tabla_bancos_cliente").Clear()
        dag_Bancos.DataSource = Session("tabla_bancos_cliente")
        dag_Bancos.DataBind()
        Session("tabla_cheques").Clear()
        dag_Cheques.DataSource = Session("tabla_cheques")
        dag_Cheques.DataBind()

    End Sub

    Private Sub Limpiar()
        txt_ctachequesori.Text = ""
        txt_CajaMonto.Text = ""
        cmb_Banco.SelectedIndex = "0"
        txt_MontoBanco.Text = ""
        cmb_BancoCliente.SelectedIndex = "0"
        txt_NumCtaCliente.Text = ""
        cmb_BancoCheques.SelectedIndex = "0"
        txt_MontoCheques.Text = ""
        txt_NumCheques.Text = ""
        Session("MONTO") = 0.0
        lbl_Monto.Text = "Monto: $0.00"
        Chk_FinanCom.Checked = False
        Chk_FinanCom.Enabled = False
        dag_Comisiones.Visible = False
        'lbl_InfoCom.Text = ""
        lbl_TotalComMonto.Text = ""
        Session("TOTAL_COM") = 0.0

    End Sub

    Private Sub LimpiarFactoraje()

        txt_CajaMonto.Text = ""
        cmb_Banco.SelectedIndex = "0"
        txt_MontoBanco.Text = ""
        cmb_BancoCliente.SelectedIndex = "0"
        txt_NumCtaCliente.Text = ""
        cmb_BancoCheques.SelectedIndex = "0"
        txt_MontoCheques.Text = ""
        txt_NumCheques.Text = ""
        Session("MONTO_AFORO") = 0.0
        Session("INT_X_COB") = 0.0
        Session("INT_X_COB") = 0.0
        Session("RESTANTE_AFORO") = 0.0
        lbl_FactMontoAforo.Text = ""
        lbl_FactInteres.Text = ""
        lbl_FactIva.Text = ""
        lbl_FactTotalPago.Text = ""
        lbl_FactRestanteAforo.Text = ""

    End Sub

    Protected Sub ObtieneCorreosFinanzas()


        Dim Operaciones As Integer = 0
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim nombre As String = String.Empty
        Dim sucursal As String = String.Empty
        Dim folio As String = String.Empty
        Dim prospecto As String = String.Empty
        Dim personaid As String = String.Empty
        Dim usuario As String = String.Empty
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 20, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TESORERIA_CHEQUETRANS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Operaciones += 1

            If Session("rs").Fields("TIPO").Value.ToString = "TRANSBANC" Then
                'CuerpoCorreo = CuerpoCorreo + "Transferencia Bancaria " + vbCrLf + vbCrLf +
                '"Monto: " + Session("rs").Fields("MONTO").Value.ToString + vbCrLf +
                '"Cuenta Origen: " + Session("rs").Fields("CTA_ORIGEN").Value.ToString + vbCrLf +
                '"Banco Destino: " + Session("rs").Fields("BANCO_DESTINO").Value.ToString + vbCrLf +
                '"Clabe: " + Session("rs").Fields("CLABE_DESTINO").Value.ToString + vbCrLf +
                '"Numero de Cuenta: " + Session("rs").Fields("NUMCTA_DESTINO").Value.ToString + vbCrLf + vbCrLf + ""
                subject = "Aviso de transferencia / cheque por pago de credito."
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td width='25%'>Transferencia Bancaria</td></td></tr>")
                sbhtml.Append("<tr><td width='25%'>Se le avisa que debe realizar la transferencia bancaria y/o expedición de cheque por el pago de préstamo con los siguientes datos:</td></td></tr>")
                sbhtml.Append("<tr><td width='75%'>Monto: </td><td>" + "<b>" + Session("rs").Fields("MONTO").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='30%'>Cuenta Origen: </td>" + "<b>" + Session("rs").Fields("CTA_ORIGEN").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Banco Destino: </td>" + "<b>" + Session("rs").Fields("BANCO_DESTINO").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Clabe: </td>" + "<b>" + Session("rs").Fields("CLABE_DESTINO").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Numero de Cuenta: </td>" + "<b>" + Session("rs").Fields("NUMCTA_DESTINO").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<br></br></table>")
            End If
            If Session("rs").Fields("TIPO").Value.ToString = "CHEQUE" Then
                'CuerpoCorreo = CuerpoCorreo + "Cheque " + vbCrLf + vbCrLf +
                '"Monto: " + Session("rs").Fields("MONTO").Value.ToString + vbCrLf +
                '"Cuenta Origen: " + Session("rs").Fields("CTA_ORIGEN").Value.ToString + vbCrLf + vbCrLf + ""

                subject = "Aviso de transferencia / cheque por pago de credito."
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td width='25%'>Cheque</td></td></tr>")
                sbhtml.Append("<tr><td width='25%'>Se le avisa que debe realizar la transferencia bancaria y/o expedición de cheque por el pago de préstamo con los siguientes datos:</td></td></tr>")
                sbhtml.Append("<tr><td width='75%'>Monto: </td><td>" + "<b>" + Session("rs").Fields("MONTO").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='30%'>Cuenta Origen: </td>" + "<b>" + Session("rs").Fields("CTA_ORIGEN").Value.ToString + "</b>" + "</td></tr>")
                sbhtml.Append("<br></br></table>")

            End If

            Session("rs").movenext()
        Loop

        Session("Con").Close()

        If Operaciones > 0 Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "SOLDISPTES")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
            Session("rs") = Session("cmd").Execute()

            usuario = Session("USUARIO").ToString
            sucursal = Session("SUCURSAL").ToString
            folio = Session("FOLIO").ToString
            prospecto = Session("PROSPECTO").ToString
            personaid = Session("PERSONAID").ToString
            Do While Not Session("rs").EOF
                nombre = Session("rs").Fields("NOMBRE").Value.ToString
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
                sbhtml.Append("<tr><td width='75%'>Estimado(a): </td><td>" + "<b>" + nombre + "</b>" + "</td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br />")
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
                sbhtml.Append("<tr><td width='75%'>Usuario: </td><td>" + "<b>" + usuario + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='30%'>Sucursal: </td>" + "<b>" + sucursal + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Contrato: </td>" + "<b>" + folio + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='50%'>Cliente: </td>" + "<b>" + prospecto + " (" + personaid + ")" + "</b>" + "</td></tr>")
                sbhtml.Append("<tr><td width='250'>Favor de aplicar el movimiento y confirmar en el sistema por medio de los modulos de conciliación.</td></tr>")
                sbhtml.Append("<br></br>")
                sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br></br>")
                clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
                Session("rs").movenext()

            Loop

            Session("Con").Close()
        End If

    End Sub

    Private Sub EnviaMail(ByVal destinatario As String, ByVal nombre As String, ByVal CuerpoCorreo As String)

        Const ConfigNamespace As String =
                   "http://schemas.microsoft.com/cdo/configuration/"
        Dim oMsg As New CDO.Message()
        Dim iConfig As New CDO.Configuration()
        Dim Flds As ADODB.Fields = iConfig.Fields
        With iConfig.Fields
            .Item(ConfigNamespace & "smtpserver").Value = Session("MAIL_SERVER")
            .Item(ConfigNamespace & "smtpserverport").Value = Session("MAIL_SERVER_PORT")
            .Item(ConfigNamespace & "sendusing").Value =
                CDO.CdoSendUsing.cdoSendUsingPort
            If Session("MAIL_SERVER_SSL") = 1 Then
                .Item(ConfigNamespace & "smtpusessl").Value = True
            End If
            .Item(ConfigNamespace & "sendusername").Value = Session("MAIL_SERVER_USER")
            .Item(ConfigNamespace & "sendpassword").Value = Session("MAIL_SERVER_PWD")
            .Item(ConfigNamespace & "smtpauthenticate").Value =
                CDO.CdoProtocolsAuthentication.cdoBasic
            .Update()
        End With

        With oMsg
            .Configuration = iConfig
            .From = Session("MAIL_SERVER_FROM")
            .To = destinatario
            .Subject = "Aviso de transferencia / cheque por pago de credito."
            .TextBody = "Estimado(a) " + nombre + vbCrLf + vbCrLf +
                "Se le avisa que debe realizar la transferencia bancaria y/o expedición de cheque por el pago de préstamo con los siguientes datos:" + vbCrLf + vbCrLf +
                "Usuario: " + Session("USUARIO").ToString + vbCrLf +
                "Sucursal: " + Session("SUCURSAL").ToString + vbCrLf +
                "Contrato: " + Session("FOLIO").ToString + vbCrLf +
                "Cliente: " + Session("PROSPECTO").ToString + " (" + Session("PERSONAID").ToString + ")" + vbCrLf + vbCrLf +
                CuerpoCorreo + vbCrLf +
                "Favor de aplicar el movimiento y confirmar en el sistema por medio de los modulos de conciliación." + vbCrLf +
                "Atentamente" + vbCrLf + vbCrLf + "MAS.Core" + vbCrLf +
                Session("EMPRESA")
            .Send()
        End With
        oMsg = Nothing
        iConfig = Nothing

    End Sub

    Function LlamaTiraEfectivo() As Boolean

        'Se agrega el movimiento pendiente en la base de datos
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ENTRADASALIDA", Session("adVarChar"), Session("adParamInput"), 15, Session("ENTRADASALIDA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 15, Session("MONTO_EFECTIVO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 15, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_TIRAEFEC_OPERACION_PENDIENTE"
        Session("rs") = Session("cmd").Execute()

        Session("UNIR") = Session("rs").Fields("UNIR").Value.ToString

        Session("Con").Close()

        'Se llama el ASPX de Tira de Efectivo
        ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)

        Return True

    End Function


#Region "EMAIL"


    Private Sub Motor_On_off()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, "INI")
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
        Dim clase_Correo As New Correo
        Dim subject As String = String.Empty
        Dim sbhtml As New StringBuilder
        Dim cc As String = String.Empty
        Dim cuentas As String = String.Empty
        Dim folio As String = String.Empty
        Dim monto As String = String.Empty
        Dim plazo As String = String.Empty
        Dim tasa As String = String.Empty
        Dim mora As String = String.Empty
        Dim cliente As String = String.Empty
        Dim nombre As String = String.Empty
        Dim capital As String = String.Empty
        Dim ord As String = String.Empty
        Dim comisiones As String = String.Empty
        Dim formasp As String = String.Empty
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
            Session("FORMAPAGO1") = Session("rs").Fields("FORMAPAGO1").Value.ToString
            Session("FORMAPAGO2") = Session("rs").Fields("FORMAPAGO2").Value.ToString
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
        Session("cmd").CommandText = "INS_COB_LOG_BIENVENIDA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_LOG_BIENVENIDA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Session("IDENVIO") = Session("rs").Fields("IDENVIO").Value.ToString

            nombre = Session("rs").Fields("NOMBRE").Value.ToString()
            folio = Session("rs").Fields("FOLIO").Value.ToString()
            cliente = Session("rs").Fields("IDCLIENTE").Value.ToString()

            capital = Session("rs").Fields("CAPITAL").Value.ToString()
            ord = Session("rs").Fields("ORDINARIOS").Value.ToString()
            mora = Session("rs").Fields("MORATORIOS").Value.ToString()
            comisiones = Session("rs").Fields("COMISIONES").Value.ToString()
            plazo = Session("rs").Fields("PLAZO").Value.ToString()
            formasp = Session("FORMAPAGO1") + "," + Session("FORMAPAGO2")

            subject = ""
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td width='25%'>Bienvenid@:</td></td></tr>")
            sbhtml.Append("<tr><td width='75%'>Estimado(a): </td><td>" + "<b>" + cliente + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td> Te damos la más cordial bienvenida, próximamente te estaremos enviando mensualmente vía electrónica tus estados de cuenta, recordatorios de pago y confirmaciones de pago.</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td>Aquí detallamos las características de tu préstamo:  </td></tr>")

            sbhtml.Append("<tr><td width='75%'>Monto :</td><td>" + "<b>" + monto + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Plazo :</td>" + "<b>" + plazo + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Tasa :</td>" + "<b>" + tasa + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Te recordamos que los pagos tardíos generan intereses moratorios sobre el :</td>" + "<b>" + mora + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Es muy importante poner en la referencia de pagos tu número de cliente :</td>" + "<b>" + cliente + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='70%'>Folio :</td>" + "<b>" + folio + "</b>" + " para que sean localizados y aplicados en tiempo.</td></tr>")
            sbhtml.Append("<tr><td width='250'>Para cualquier comentario o duda contacta a tu ejecutivo .</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>¡Agradecemos la confianza y compromiso que nos brindas! </b></td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'> Los pagos realizados con cheque y por medio de pago en línea son salvo buen cobro. Te sugerimos validar con tu banco que el monto entregado sea debitado de tu cuenta bancaria. Por favor toma en cuenta que la información mostrada en la página de Internet es actualizada una vez al día. Es posible que encuentres información diferente a la que se menciona en este mensaje hasta el momento de la siguiente actualización.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='30%'>Cuentas para depósitos :</td>" + "<b>" + cuentas + "</b>" + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
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
        Session("cmd").CommandText = "UPD_COB_LOG_BIENVENIDA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

#End Region

End Class