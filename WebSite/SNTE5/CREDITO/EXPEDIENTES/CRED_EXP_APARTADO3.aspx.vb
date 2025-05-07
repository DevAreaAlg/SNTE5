Imports System.Data.SqlClient
Imports System.Data
Public Class CRED_EXP_APARTADO3
    Inherits System.Web.UI.Page

    '---EL SEMAFORO PRENDE CUANDO  EL TIPO DE PRODUCTO  DE CREDITO CUMPLE CON EL MINIMO DE AVALES,REFERENCIAS Y CODEUDORES
    '--Y EL TIPO DE PRODUCTO DE CAPTACION CUMPLE CON EL MINIMO DE REFERENCIA Y BENEFICIARIO CON SU 100%
    Dim egresos As New List(Of TextBox)()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Estudio Socioeconómico", "Estudio Socioeconómico")

        If Not Me.IsPostBack Then

            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))
            Session("VENGODE") = "CRED_EXP_APARTADO3.aspx"
            LlenaActivosEfectivo() 'llena Grid Activos efectivo
            LlenaActivosCXCNODOC() 'llena grid Activos CXCNoDoc
            LlenaActivosCXCDOC() 'llena grid Activos CXCDoc
            LlenaActivosHIPFID() 'llena grid Activos hipoteca fideicomiso
            LlenaActivosINVACC() 'llena grid Activos Inversiones en Acciones
            LlenaPasivosCXPNODOC() 'llena grid pasivos CXP No documentadas
            LlenaPasivosCXPDOC() ''llena grid pasivos CXP documentos
            LlenaPasivosIXP() 'llena grid pasivos impuestos por pagar
            LlenaPasivosOtros() 'llena grid pasivos otros
            LlenaPasivosContingente() 'llena grid pasivos contingentes
            LlenaGravamen()
            LlenaBienesin()
            LlenaBienesmu()
            LlenaPromocion()
            LlenaProductServ()
            LlenaCreditosAd()
        End If

        DatosIngreso()
        LlenaGastos()
        calGanancias()
    End Sub

#Region "Ingresos y egresos"

    Private Sub DatosIngreso()

        Dim SUBTOTAL As Integer = 0
        Dim SUBTOTAL_co As Integer = 0
        Dim TOTAL As Integer

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_INGRESO_PERSONA"
        Session("rs") = Session("cmd").Execute()

        'SI LA PERSONA NO TIENE CODEUDOR SOLO MUESTRA SUS PROPIOS INGRESOS
        If Not Session("rs").EOF Then
            lbl_montoempleo.Text = Session("rs").fields("EMPLEO").value.ToString
            lbl_montoadicional.Text = Session("rs").fields("ADICIONALES").value.ToString
            lbl_montootros.Text = Session("rs").fields("OTRO").value.ToString
            Session("rs").movenext()
            SUBTOTAL = CInt(lbl_montoempleo.Text) + CInt(lbl_montoadicional.Text) + CInt(lbl_montootros.Text)
            lbl_montosub.Text = FormatCurrency(SUBTOTAL)
            TOTAL = SUBTOTAL
        End If

        'SI LA PERSONA TIENE CODEUDOR
        If Not Session("rs").EOF Then
            lbl_mempleocony.Text = Session("rs").fields("EMPLEO").value.ToString
            lbl_montoadicionalcony.Text = Session("rs").fields("ADICIONALES").value.ToString
            lbl_montootroscony.Text = Session("rs").fields("OTRO").value.ToString

            '--suma de ingresos del conyuge
            SUBTOTAL_co = CInt(lbl_mempleocony.Text) + CInt(lbl_montoadicionalcony.Text) + CInt(lbl_montootroscony.Text)
            lbl_subcony.Text = FormatCurrency(SUBTOTAL_co)
            '---Total de la suma
            TOTAL = SUBTOTAL_co + SUBTOTAL
            pnl_codeudor.Visible = True
        End If

        lbl_montototal.Text = FormatCurrency(TOTAL)
        Session("Con").Close()

    End Sub

    Private Sub LlenaGastos()
        'limpia el panel
        pnl_egresosInputs.Controls.Clear()
        Dim sumaGastos As Double

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_EGRESOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            'declaro variables
            Dim nombreGasto As String
            Dim idGasto As String
            Dim montoGasto As String
            'verfico si ya expiró el monto
            'If Session("rs").Fields("AVISO").value.ToString = 1 Then
            montoGasto = Session("rs").Fields("MONTO").Value.ToString
            'Else
            '    montoGasto = "0.0"
            'End If
            'asigno varibles
            nombreGasto = Session("rs").Fields("CATCONCEPGTOS_GASTO").Value.ToString.ToLower
            Dim charC As String = nombreGasto.First.ToString.ToUpper()
            nombreGasto = nombreGasto.Remove(0, 1).Insert(0, charC)
            idGasto = Session("rs").Fields("IDGASTO").Value.ToString

            'creo el control dinamicamente
            Dim controlGasto As HtmlGenericControl
            controlGasto = newEgresosField(idGasto, nombreGasto, montoGasto)
            'agrego el control
            pnl_egresosInputs.Controls.Add(controlGasto)
            Session("rs").movenext()

            'sumo el monto actual a la suma de los gastos mensules
            sumaGastos += montoGasto
        Loop
        lbl_gastosMensuales.Text = "$" + Session("MascoreG").FormatNumberCurr(sumaGastos)
        Session("Con").Close()

    End Sub

    Sub calGanancias()
        lbl_gananciasMesuales.Text = "$" + Session("MascoreG").FormatNumberCurr((CType(lbl_montototal.Text.ToString, Double) - CType(lbl_gastosMensuales.Text, Double)))
    End Sub

    Protected Sub btn_guardarEgresos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarEgresos.Click

        Dim idGasto As Integer = 1
        Dim dtgastos As New Data.DataTable

        dtgastos.Columns.Add("IDCONCEPTO", GetType(String))
        dtgastos.Columns.Add("VALOR", GetType(Decimal))

        For Each inputE As TextBox In egresos
            dtgastos.Rows.Add(idGasto, CType(inputE.Text, Decimal))
            idGasto += 1
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFEXP_GASTOS_PERSONA", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("GASTOS", SqlDbType.Structured)
                Session("parm").Value = dtgastos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("PERSONAID", SqlDbType.Int)
                Session("parm").Value = Session("PERSONAID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("USERID", SqlDbType.VarChar)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDSESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("FOLIO", SqlDbType.VarChar)
                Session("parm").Value = Session("FOLIO")
                insertCommand.Parameters.Add(Session("parm"))
                insertCommand.ExecuteNonQuery()
                connection.Close()
                lbl_mensageEstado.Text = "Guardado correctamente"

            End Using
            LlenaGastos()
            calGanancias()

        Catch ex As Exception
            lbl_mensageEstado.Text = "Error al guardar egresos."
        Finally

        End Try

    End Sub

    'función que regresa un input con las caracteristicas necesarias para representar un gasto
    Function newEgresosField(ByVal id As String, ByVal title As String, ByVal monto As String) As HtmlGenericControl
        ' se declaran variables
        Dim outer_div As New HtmlGenericControl
        Dim content_div As New HtmlGenericControl
        Dim input As New TextBox
        Dim labels_div As New HtmlGenericControl
        Dim title_label As New Label
        Dim requiredFieldValidator As New RequiredFieldValidator
        Dim filtro As New AjaxControlToolkit.FilteredTextBoxExtender
        Dim filtroE As New RegularExpressionValidator
        'se establezen las caracterizticas del control
        outer_div.TagName = "div"
        outer_div.Attributes("class") = "module_subsec_elements"

        content_div.TagName = "div"
        content_div.Attributes("class") = "module_subsec_elements_content text_input_nice_div "

        input.CssClass = "text_input_nice_input"
        input.ID = "txt_g" & id
        input.Text = monto

        labels_div.TagName = "div"
        labels_div.Attributes("class") = "text_input_nice_labels"

        title_label.ID = "lbl_g" & id
        title_label.CssClass = "text_input_nice_label"
        title_label.Text = title

        requiredFieldValidator.ControlToValidate = input.ID
        requiredFieldValidator.CssClass = "alertaValidator bold"
        requiredFieldValidator.Text = "Falta Dato"
        requiredFieldValidator.Display = ValidatorDisplay.Dynamic

        filtroE.ControlToValidate = input.ID
        filtroE.CssClass = "alertaValidator bold"
        filtroE.Text = "Formato Erroneo"
        filtroE.Display = ValidatorDisplay.Dynamic
        filtroE.ValidationExpression = "[0-9]{0,17}(\.[0-9][0-9]?)?"

        filtro.TargetControlID = input.ID
        filtro.FilterType = AjaxControlToolkit.FilterTypes.Custom
        filtro.ValidChars = ".0123456789"

        'se agregan las características al control
        labels_div.Controls.Add(title_label)
        labels_div.Controls.Add(requiredFieldValidator)
        labels_div.Controls.Add(filtroE)

        content_div.Controls.Add(input)
        content_div.Controls.Add(filtro)
        content_div.Controls.Add(labels_div)

        outer_div.Controls.Add(content_div)
        'guarda el textbox del control en una lista de los text box
        egresos.Add(input)
        'regresa el control
        Return outer_div
    End Function

#End Region

    'ACTIVOS
#Region "Efectivo"

    Protected Sub btn_guarda_AcEfectivo_Click(sender As Object, e As EventArgs) Handles btn_guarda_AcEfectivo.Click
        Guarda_AcEfectivo()
    End Sub

    Protected Sub Guarda_AcEfectivo()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 100, txt_institucion_Efe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUM_CTA", Session("adVarChar"), Session("adParamInput"), 20, txt_nocta_efe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 15, txt_saldo_efe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_EFECTIVO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_status_AcEfectivo.Text = "Guardado correctamente"
        LlenaActivosEfectivo()
        txt_institucion_Efe.Text = ""
        txt_nocta_efe.Text = ""
        txt_saldo_efe.Text = ""
    End Sub

    Private Sub LlenaActivosEfectivo() ' SEL_CNFEXP_ACTIVOS_CXCNODOC
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtEfectivo As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_EFECTIVO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtEfectivo, Session("rs"))
        DAG_efectivo.DataSource = dtEfectivo
        DAG_efectivo.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub DAG_efectivo_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_efectivo.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcEfectivo.Text = ""
            Elimina_AcEfectivo(e.Item.Cells(1).Text)
            LlenaActivosEfectivo()
        End If
    End Sub

    Private Sub Elimina_AcEfectivo(ByVal Cve_Efectivo As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_DEUDOR", Session("adVarChar"), Session("adParamInput"), 10, Cve_Efectivo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_EFECTIVO"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub


#End Region

#Region "Cuentas x Cobrar"

    Protected Sub btn_guarda_AcCXCnoDOC_Click(sender As Object, e As EventArgs) Handles btn_guarda_AcCXCnoDOC.Click
        Guarda_AcCxCNoDoc()
    End Sub

    Protected Sub Guarda_AcCxCNoDoc()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DEUDOR", Session("adVarChar"), Session("adParamInput"), 200, txt_deudorCXCNoDoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAVENC", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaVencCXCNoDoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_SaldoAcCXCnoDOC.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_CxCNODOC"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_status_AcCXCnoDOC.Text = "Guardado correctamente"
        LlenaActivosCXCNODOC()
        txt_deudorCXCNoDoc.Text = ""
        txt_fechaVencCXCNoDoc.Text = ""
        txt_SaldoAcCXCnoDOC.Text = ""
    End Sub

    Private Sub LlenaActivosCXCNODOC() ' SEL_CNFEXP_ACTIVOS_CXCNODOC
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtCXCnoDOC As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_CXCNODOC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtCXCnoDOC, Session("rs"))
        DAG_CXCnoDOC.DataSource = dtCXCnoDOC
        DAG_CXCnoDOC.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub DAG_CXCnoDOC_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_CXCnoDOC.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcCXCnoDOC.Text = ""
            Elimina_AcCxcnoDoc(e.Item.Cells(1).Text)
            LlenaActivosCXCNODOC()
        End If
    End Sub

    Private Sub Elimina_AcCxcnoDoc(ByVal Cve_deudor As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_EFECTIVO", Session("adVarChar"), Session("adParamInput"), 10, Cve_deudor)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_CxCNODOC"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

#Region "Documentos x Cobrar"

    Protected Sub btn_guarda_AcCXCDoc_Click(sender As Object, e As EventArgs) Handles btn_guarda_AcCXCDoc.Click
        Guarda_AcCxCDoc()
    End Sub

    Protected Sub Guarda_AcCxCDoc()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DEUDOR", Session("adVarChar"), Session("adParamInput"), 200, txt_DedudorCXCDoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAVENC", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaVencCXCDoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_SaldoAcCXCDOC.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_CXCDOC"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_status_AcCXCDoc.Text = "Guardado correctamente"
        LlenaActivosCXCDOC()
        txt_DedudorCXCDoc.Text = ""
        txt_fechaVencCXCDoc.Text = ""
        txt_SaldoAcCXCDOC.Text = ""

    End Sub

    Private Sub LlenaActivosCXCDOC() ' SEL_CNFEXP_ACTIVOS_HIPFID
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtCXCDOC As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_CXCDOC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtCXCDOC, Session("rs"))
        DAG_CXCDoc.DataSource = dtCXCDOC
        DAG_CXCDoc.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub DAG_cxcdoc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_CXCDoc.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcCXCDoc.Text = ""
            Elimina_AcCxCDoc(e.Item.Cells(1).Text)
            LlenaActivosCXCDOC()
        End If

    End Sub

    Private Sub Elimina_AcCxCDoc(ByVal CVE_DEUDORCXCDOC As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_DEUDOR", Session("adVarChar"), Session("adParamInput"), 10, CVE_DEUDORCXCDOC)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_CXCDOC"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub


#End Region

#Region "Hipotecas y fideicomisos"

    Protected Sub btn_guarda_AcHipoFid_Click(sender As Object, e As EventArgs) Handles btn_guarda_AcHipFid.Click
        Guarda_AcHipofid()
    End Sub

    Private Sub Guarda_AcHipofid()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DEUDOR", Session("adVarChar"), Session("adParamInput"), 200, txt_DedudorHipFid.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PCTPROP", Session("adVarChar"), Session("adParamInput"), 5, txt_porcPropHipFid.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_SaldoHipFid.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_HIPOFID"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_status_AcHipFid.Text = "Guardado correctamente"
        LlenaActivosHIPFID()
        txt_DedudorHipFid.Text = ""
        txt_porcPropHipFid.Text = ""
        txt_SaldoHipFid.Text = ""

    End Sub

    Private Sub LlenaActivosHIPFID() ' SEL_CNFEXP_ACTIVOS_INVACC
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtHIPFID As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_HIPFID"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtHIPFID, Session("rs"))
        DAG_HipFid.DataSource = dtHIPFID
        DAG_HipFid.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub DAG_hipfid_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_HipFid.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcHipFid.Text = ""
            Elimina_AcHipoFid(e.Item.Cells(1).Text)
            LlenaActivosHIPFID()
        End If
    End Sub

    Private Sub Elimina_AcHipoFid(ByVal cve_hipfid As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_HIP", Session("adVarChar"), Session("adParamInput"), 10, cve_hipfid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_HIPOFID"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub


#End Region

#Region "Inversiones"

    Protected Sub btn_guarda_InvAcc_Click(sender As Object, e As EventArgs) Handles btn_guarda_AcInvAcc.Click
        Guarda_AcInvAcc()
    End Sub

    Private Sub Guarda_AcInvAcc()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EMPRESA", Session("adVarChar"), Session("adParamInput"), 200, txt_DedudorInvAcc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PCTPROP", Session("adVarChar"), Session("adParamInput"), 5, txt_PorcInvAcc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_saldoInvAcc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_INVACC"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcInvAcc.Text = "Guardado correctamente"
        LlenaActivosINVACC()
        txt_DedudorInvAcc.Text = ""
        txt_PorcInvAcc.Text = ""
        txt_saldoInvAcc.Text = ""
    End Sub

    Private Sub LlenaActivosINVACC() ' SEL_CNFEXP_ACTIVOS_INVACC
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtINVACC As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_INVACC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtINVACC, Session("rs"))
        DAG_InvAcc.DataSource = dtINVACC
        DAG_InvAcc.DataBind()
        Session("Con").Close()
    End Sub

    Protected Sub DAG_InvAcc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_InvAcc.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcInvAcc.Text = ""
            Elimina_AcInvAcc(e.Item.Cells(1).Text)
            LlenaActivosINVACC()
        End If
    End Sub

    Private Sub Elimina_AcInvAcc(ByVal cve_invacc As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_EMP", Session("adVarChar"), Session("adParamInput"), 10, cve_invacc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_INVACC"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

    'PASIVOS
#Region "Cuentas por pagar"

    Protected Sub btn_guarda_CXPnoDOC_Click(sender As Object, e As EventArgs) Handles btn_guarda_CXPnoDOC.Click
        GuardaPasivoCXPNODOC()
    End Sub

    Private Sub GuardaPasivoCXPNODOC()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 200, txt_acreedorCXPNoDoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCRED", Session("adVarChar"), Session("adParamInput"), 200, txt_tipoCredCXPNoDoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 18, txt_SaldoCXPnoDOC.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PASIVOS_CXPNODOC"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcCXPnoDOC.Text = "Guardado correctamente"
        LlenaPasivosCXPNODOC()
        txt_acreedorCXPNoDoc.Text = ""
        txt_tipoCredCXPNoDoc.Text = ""
        txt_SaldoCXPnoDOC.Text = ""

    End Sub

    Protected Sub DAG_CXPnoDOC_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_CXPnoDOC.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcCXPnoDOC.Text = ""
            Elimina_CXPNODOC(e.Item.Cells(1).Text)
            LlenaPasivosCXPNODOC()
        End If
    End Sub

    Private Sub LlenaPasivosCXPNODOC()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPasCXPNODOC As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PASIVOS_CXPNODOC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPasCXPNODOC, Session("rs"))
        DAG_CXPnoDOC.DataSource = dtPasCXPNODOC
        DAG_CXPnoDOC.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_CXPNODOC(ByVal cveinst As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PASIVOS_CXPNODOC"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

#Region "Documentos por pagar"

    Protected Sub btn_guarda_CXPdoC_Click(sender As Object, e As EventArgs) Handles btn_guarda_CXPdoC.Click
        GuardaPasivoCXPDOC()
    End Sub

    Private Sub GuardaPasivoCXPDOC()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 200, txt_acreedorCXPDoC.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCRED", Session("adVarChar"), Session("adParamInput"), 200, txt_tipoCredCXPDoC.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 18, txt_SaldoCXPdoC.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PASIVOS_CXPDOC"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcCXPdoC.Text = "Guardado correctamente"
        LlenaPasivosCXPDOC()
        txt_acreedorCXPDoC.Text = ""
        txt_tipoCredCXPDoC.Text = ""
        txt_SaldoCXPdoC.Text = ""

    End Sub

    Protected Sub DAG_CXPdoC_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_CXPdoC.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_AcCXPdoC.Text = ""
            Elimina_CXPDOC(e.Item.Cells(1).Text)
            LlenaPasivosCXPDOC()
        End If
    End Sub

    Private Sub LlenaPasivosCXPDOC()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPasCXPDOC As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PASIVOS_CXPDOC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPasCXPDOC, Session("rs"))
        DAG_CXPdoC.DataSource = dtPasCXPDOC
        DAG_CXPdoC.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_CXPDOC(ByVal cveinst As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PASIVOS_CXPDOC"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

#Region "Impuesto por pagar"

    Protected Sub btn_guardaIxP_Click(sender As Object, e As EventArgs) Handles btn_guardaIxP.Click
        GuardaPasivoIXP()
    End Sub

    Private Sub GuardaPasivoIXP()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 200, txt_acreedorIxP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCRED", Session("adVarChar"), Session("adParamInput"), 200, txt_tipocredIxP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_saldoIxP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PASIVOS_IXP"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusIxP.Text = "Guardado correctamente"
        LlenaPasivosIXP()
        txt_acreedorIxP.Text = ""
        txt_tipocredIxP.Text = ""
        txt_saldoIxP.Text = ""

    End Sub

    Protected Sub DAG_IxP_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_IxP.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_statusIxP.Text = ""
            Elimina_IXP(e.Item.Cells(1).Text)
            LlenaPasivosIXP()
        End If
    End Sub

    Private Sub LlenaPasivosIXP()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPasIXP As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PASIVOS_IXP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPasIXP, Session("rs"))
        DAG_IxP.DataSource = dtPasIXP
        DAG_IxP.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_IXP(ByVal cveinst As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PASIVOS_IXP"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

#Region "Pasivo contingente"

    Protected Sub btn_guardaCnt_Click(sender As Object, e As EventArgs) Handles btn_guardaCnt.Click
        GuardaPasivosCNT()
    End Sub

    Private Sub GuardaPasivosCNT()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCRED", Session("adVarChar"), Session("adParamInput"), 200, txt_tipoCnt.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_saldoCnt.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PASIVOS_CNTGNT"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusCnt.Text = "Guardado correctamente"
        LlenaPasivosContingente()
        txt_tipoCnt.Text = ""
        txt_saldoCnt.Text = ""

    End Sub

    Protected Sub DAG_cntgnt_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_cntgnt.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_statusCnt.Text = ""
            Elimina_CNTGNT(e.Item.Cells(1).Text)
            LlenaPasivosContingente()
        End If
    End Sub

    Private Sub LlenaPasivosContingente()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPasCntgnt As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PASIVOS_CNTGNT"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPasCntgnt, Session("rs"))
        DAG_cntgnt.DataSource = dtPasCntgnt
        DAG_cntgnt.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_CNTGNT(ByVal cve As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_CNTGNT", Session("adVarChar"), Session("adParamInput"), 10, cve)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PASIVOS_CNTGNT"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

#Region "otros pasivos"

    Protected Sub btn_guardaOtros_Click(sender As Object, e As EventArgs) Handles btn_guardaOtros.Click
        GuardaPasivoOtros()
    End Sub

    Private Sub GuardaPasivoOtros()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 200, txt_institucionOtros.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCRED", Session("adVarChar"), Session("adParamInput"), 200, txt_tipocredOtros.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_saldoOtros.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PASIVOS_OTROS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusOtros.Text = "Guardado correctamente"
        LlenaPasivosOtros()
        txt_institucionOtros.Text = ""
        txt_tipocredOtros.Text = ""
        txt_saldoOtros.Text = ""

    End Sub

    Protected Sub DAG_Otros_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_Otros.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_statusOtros.Text = ""
            Elimina_OTROS(e.Item.Cells(1).Text)
            LlenaPasivosOtros()
        End If
    End Sub

    Private Sub LlenaPasivosOtros()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPasOtros As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PASIVOS_OTROS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPasOtros, Session("rs"))
        DAG_Otros.DataSource = dtPasOtros
        DAG_Otros.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_OTROS(ByVal cveinst As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PASIVOS_OTROS"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

    'BIENES INMUEBLES
#Region "bien inmueble"

    Protected Sub cmb_gravamen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_gravamen.SelectedIndexChanged

        If cmb_gravamen.SelectedItem.Value = "1" Then
            lbl_acree1.Visible = True
            txt_acree1.Visible = True
            lbl_acree2.Visible = True
            txt_acree2.Visible = True
            lbl_plazo.Visible = True
            txt_plazo.Visible = True
            lbl_plazores.Visible = True
            txt_plazores.Visible = True
            lbl_monto.Visible = True
            txt_monto.Visible = True
            lbl_abono.Visible = True
            txt_abono.Visible = True
            lbl_tasa.Visible = True
            txt_tasa.Visible = True
            lbl_fechav.Visible = True
            txt_fechav.Visible = True
            lbl_saldo.Visible = True
            txt_saldo.Visible = True

            txt_acree1.ValidationGroup = "val_BienesIn"
            txt_plazo.ValidationGroup = "val_BienesIn"
            txt_plazores.ValidationGroup = "val_BienesIn"
            txt_monto.ValidationGroup = "val_BienesIn"
            txt_abono.ValidationGroup = "val_BienesIn"
            txt_tasa.ValidationGroup = "val_BienesIn"
            txt_fechav.ValidationGroup = "val_BienesIn"
            txt_saldo.ValidationGroup = "val_BienesIn"


            RequiredFieldValidator39.Visible = True
            RequiredFieldValidator40.Visible = True
            RequiredFieldValidator41.Visible = True
            RequiredFieldValidator42.Visible = True
            RequiredFieldValidator43.Visible = True
            RequiredFieldValidator44.Visible = True
            RequiredFieldValidator45.Visible = True
            RequiredFieldValidator46.Visible = True

            'vuelve
        Else
            lbl_acree1.Visible = False
            txt_acree1.Visible = False
            lbl_acree2.Visible = False
            txt_acree2.Visible = False
            lbl_plazo.Visible = False
            txt_plazo.Visible = False
            lbl_plazores.Visible = False
            txt_plazores.Visible = False
            lbl_monto.Visible = False
            txt_monto.Visible = False
            lbl_abono.Visible = False
            txt_abono.Visible = False
            lbl_tasa.Visible = False
            txt_tasa.Visible = False
            lbl_fechav.Visible = False
            txt_fechav.Visible = False
            lbl_saldo.Visible = False
            txt_saldo.Visible = False

            txt_acree1.ValidationGroup = ""
            txt_plazo.ValidationGroup = ""
            txt_plazores.ValidationGroup = ""
            txt_monto.ValidationGroup = ""
            txt_abono.ValidationGroup = ""
            txt_tasa.ValidationGroup = ""
            txt_fechav.ValidationGroup = ""
            txt_saldo.ValidationGroup = ""


            RequiredFieldValidator39.Visible = False
            RequiredFieldValidator40.Visible = False
            RequiredFieldValidator41.Visible = False
            RequiredFieldValidator42.Visible = False
            RequiredFieldValidator43.Visible = False
            RequiredFieldValidator44.Visible = False
            RequiredFieldValidator45.Visible = False
            RequiredFieldValidator46.Visible = False

        End If

    End Sub

    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()
        If txt_cp.Text = "" Then
            Exit Sub
        End If
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        Dim idedo As String = ""
        Dim idmuni As String = ""

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP

            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString

            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, idedo)
            cmb_estado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            cmb_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                cmb_colonia.Items.Add(item)
                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()

    End Sub


    Protected Sub btn_guarda_Bienesin_Click(sender As Object, e As EventArgs) Handles btn_guarda_bienin.Click
        Guarda_Bienesin()
    End Sub

    Private Sub Guarda_Bienesin()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 100, txt_bienin_tipo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, txt_calle.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, txt_num_ext.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, txt_num_int.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_ESTADO", Session("adVarChar"), Session("adParamInput"), 2, cmb_estado.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 4, cmb_municipio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_ASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 5, cmb_colonia.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CIUDAD", Session("adVarChar"), Session("adParamInput"), 100, txt_ciudad.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        If txt_pctprop.Text <> "" Then
            Session("parm") = Session("cmd").CreateParameter("PCTPROP", Session("adVarChar"), Session("adParamInput"), 6, txt_pctprop.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("PCTPROP", Session("adVarChar"), Session("adParamInput"), 6, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If


        If txt_ingrenta.Text <> "" Then
            Session("parm") = Session("cmd").CreateParameter("INGRENTA", Session("adVarChar"), Session("adParamInput"), 19, txt_ingrenta.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("INGRENTA", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        If txt_valorm.Text <> "" Then

            Session("parm") = Session("cmd").CreateParameter("VALORM", Session("adVarChar"), Session("adParamInput"), 19, txt_valorm.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("VALORM", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("SUPT", Session("adVarChar"), Session("adParamInput"), 19, txt_supert.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUPC", Session("adVarChar"), Session("adParamInput"), 19, txt_superc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RPPC", Session("adVarChar"), Session("adParamInput"), 500, txt_rppc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("GRAVADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_gravamen.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_gravamen.SelectedItem.Value = 1 Then
            Session("parm") = Session("cmd").CreateParameter("ACREEDOR1", Session("adVarChar"), Session("adParamInput"), 200, txt_acree1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ACREEDOR2", Session("adVarChar"), Session("adParamInput"), 200, txt_acree2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, txt_plazo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RESTANTE", Session("adVarChar"), Session("adParamInput"), 10, txt_plazores.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 19, txt_monto.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ABONO", Session("adVarChar"), Session("adParamInput"), 19, txt_abono.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, txt_tasa.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAV", Session("adVarChar"), Session("adParamInput"), 10, txt_fechav.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, txt_saldo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("ACREEDOR1", Session("adVarChar"), Session("adParamInput"), 200, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ACREEDOR2", Session("adVarChar"), Session("adParamInput"), 200, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RESTANTE", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ABONO", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAV", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("VALORN", Session("adVarChar"), Session("adParamInput"), 19, txt_valorn.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_BIENESIN"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_status_bienin.Text = "Guardado correctamente"
        LlenaBienesin()
        txt_bienin_tipo.Text = ""
        txt_calle.Text = ""
        txt_num_ext.Text = ""
        txt_num_int.Text = ""
        cmb_estado.Items.Clear()
        cmb_colonia.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_gravamen.Items.Clear()
        LlenaGravamen()

        txt_cp.Text = ""
        txt_ciudad.Text = ""
        txt_pctprop.Text = ""
        txt_ingrenta.Text = ""
        txt_valorm.Text = ""
        txt_superc.Text = ""
        txt_supert.Text = ""
        txt_rppc.Text = ""
        txt_acree1.Text = ""
        txt_acree2.Text = ""
        txt_plazo.Text = ""
        txt_plazores.Text = ""
        txt_monto.Text = ""
        txt_abono.Text = ""
        txt_tasa.Text = ""
        txt_fechav.Text = ""
        txt_saldo.Text = ""
        txt_valorn.Text = ""
    End Sub

    Protected Sub DAG_Bienesin_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_BienesIn.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_bienin.Text = ""
            Elimina_Bienesin(e.Item.Cells(1).Text)
            LlenaBienesin()
        End If
    End Sub

    Private Sub LlenaGravamen()

        cmb_gravamen.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_gravamen.Items.Add(elija)

        Dim si As New ListItem("SI", "1")
        cmb_gravamen.Items.Add(si)

        Dim no As New ListItem("NO", "0")
        cmb_gravamen.Items.Add(no)

    End Sub

    Private Sub Elimina_Bienesin(ByVal Cve_bien As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_BIEN", Session("adVarChar"), Session("adParamInput"), 10, Cve_bien)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_BIENESIN"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub LlenaBienesin()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtbienesin As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_BIENESIN"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtbienesin, Session("rs"))
        DAG_BienesIn.DataSource = dtbienesin
        DAG_BienesIn.DataBind()
        Session("Con").Close()

    End Sub

#End Region

#Region "Bien mueble"

    Protected Sub btn_guarda_Bienesmu_Click(sender As Object, e As EventArgs) Handles btn_guardabienmu.Click
        Guarda_Bienesmu()
    End Sub

    Private Sub Guarda_Bienesmu()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MARCA", Session("adVarChar"), Session("adParamInput"), 100, txt_marca.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODELO", Session("adVarChar"), Session("adParamInput"), 100, txt_modelo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANO", Session("adVarChar"), Session("adParamInput"), 4, txt_ano.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALORC", Session("adVarChar"), Session("adParamInput"), 19, txt_valorbienmu.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ACTIVOS_BIENESMU"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusbienmu.Text = "Guardado correctamente"
        LlenaBienesmu()
        txt_marca.Text = ""
        txt_modelo.Text = ""
        txt_ano.Text = ""
        txt_valorbienmu.Text = ""

    End Sub

    Protected Sub DAG_Bienesmu_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_bienesmu.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_statusbienmu.Text = ""
            Elimina_Bienesmu(e.Item.Cells(1).Text)
            LlenaBienesmu()
        End If
    End Sub

    Private Sub LlenaBienesmu()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtbienesmu As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTIVOS_BIENESMU"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtbienesmu, Session("rs"))
        DAG_bienesmu.DataSource = dtbienesmu
        DAG_bienesmu.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_Bienesmu(ByVal Cve_bien As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_BIEN", Session("adVarChar"), Session("adParamInput"), 10, Cve_bien)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_ACTIVOS_BIENESMU"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

#Region "productos y servicios"

    Private Sub LlenaPromocion()
        cmb_promo.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_promo.Items.Add(elija)

        Dim si As New ListItem("SI", "1")
        cmb_promo.Items.Add(si)

        Dim no As New ListItem("NO", "0")
        cmb_promo.Items.Add(no)
    End Sub

    Protected Sub cmb_promo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_promo.SelectedIndexChanged

        If cmb_promo.SelectedItem.Value = "1" Then
            lbl_promotipo.Visible = True
            txt_promotipo.Visible = True
            lbl_promomedios.Visible = True
            txt_promomedios.Visible = True
            lbl_promocosto.Visible = True
            txt_promocosto.Visible = True

            txt_promotipo.ValidationGroup = "val_Prod"
            txt_promomedios.ValidationGroup = "val_Prod"
            txt_promocosto.ValidationGroup = "val_Prod"

            RequiredFieldValidator57.Visible = True
            RequiredFieldValidator58.Visible = True
            RequiredFieldValidator59.Visible = True

        Else
            lbl_promotipo.Visible = False
            txt_promotipo.Visible = False
            lbl_promomedios.Visible = False
            txt_promomedios.Visible = False
            lbl_promocosto.Visible = False
            txt_promocosto.Visible = False

            txt_promotipo.ValidationGroup = ""
            txt_promomedios.ValidationGroup = ""
            txt_promocosto.ValidationGroup = ""

            RequiredFieldValidator57.Visible = False
            RequiredFieldValidator58.Visible = False
            RequiredFieldValidator59.Visible = False
        End If

    End Sub


    Protected Sub btn_guarda_prodserv_Click(sender As Object, e As EventArgs) Handles btn_guarda_prodserv.Click
        Guarda_ProductServ()
        LlenaProductServ()
    End Sub

    Private Sub Guarda_ProductServ()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROD1", Session("adVarChar"), Session("adParamInput"), 200, txt_prod1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VENTASPROD1", Session("adVarChar"), Session("adParamInput"), 19, txt_prod1vta.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        If txt_prod2.Text <> "" And txt_prod2vta.Text <> "" Then
            Session("parm") = Session("cmd").CreateParameter("PROD2", Session("adVarChar"), Session("adParamInput"), 200, txt_prod2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VENTASPROD2", Session("adVarChar"), Session("adParamInput"), 19, txt_prod2vta.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("PROD2", Session("adVarChar"), Session("adParamInput"), 200, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VENTASPROD2", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If


        If txt_prod3.Text <> "" And txt_prod3vta.Text <> "" Then
            Session("parm") = Session("cmd").CreateParameter("PROD3", Session("adVarChar"), Session("adParamInput"), 200, txt_prod3.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VENTASPROD3", Session("adVarChar"), Session("adParamInput"), 19, txt_prod3vta.Text)
            Session("cmd").Parameters.Append(Session("parm"))

        Else
            Session("parm") = Session("cmd").CreateParameter("PROD3", Session("adVarChar"), Session("adParamInput"), 200, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VENTASPROD3", Session("adVarChar"), Session("adParamInput"), 19, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("PROCPROD", Session("adVarChar"), Session("adParamInput"), 500, txt_procprod.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUEVOSPROD", Session("adVarChar"), Session("adParamInput"), 500, txt_nuevosp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCMERC", Session("adVarChar"), Session("adParamInput"), 500, txt_mercado.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMO", Session("adVarChar"), Session("adParamInput"), 200, cmb_promo.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        If cmb_promo.SelectedItem.Value = 1 Then
            Session("parm") = Session("cmd").CreateParameter("PROMOTIPO", Session("adVarChar"), Session("adParamInput"), 500, txt_promotipo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PROMOMEDIO", Session("adVarChar"), Session("adParamInput"), 500, txt_promomedios.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PROMOCOST", Session("adVarChar"), Session("adParamInput"), 10, txt_promocosto.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("PROMOTIPO", Session("adVarChar"), Session("adParamInput"), 500, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PROMOMEDIO", Session("adVarChar"), Session("adParamInput"), 500, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PROMOCOST", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "INS_CNFEXP_PRODSERPROM"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_status_prodserv.Text = "Guardado correctamente"

    End Sub

    Private Sub LlenaProductServ()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PRODSERPROM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_prod1.Text = Session("rs").fields("PROD1").value.ToString
            txt_prod1vta.Text = Session("rs").fields("VTAPROD1").value.ToString
            txt_prod2.Text = Session("rs").fields("PROD2").value.ToString
            txt_prod2vta.Text = Session("rs").fields("VTAPROD2").value.ToString
            txt_prod3.Text = Session("rs").fields("PROD3").value.ToString
            txt_prod3vta.Text = Session("rs").fields("VTAPROD3").value.ToString
            txt_procprod.Text = Session("rs").fields("PROCPROD").value.ToString
            txt_nuevosp.Text = Session("rs").fields("NUEVOSP").value.ToString
            txt_mercado.Text = Session("rs").fields("MERCADO").value.ToString

            If Session("rs").fields("PROMOCION").value.ToString = "1" Then

                txt_promotipo.Text = Session("rs").fields("PROMOTIPO").value.ToString
                txt_promomedios.Text = Session("rs").fields("PROMOMED").value.ToString
                txt_promocosto.Text = Session("rs").fields("PROMOCOST").value.ToString
                lbl_promotipo.Visible = True
                txt_promotipo.Visible = True
                lbl_promomedios.Visible = True
                txt_promomedios.Visible = True
                lbl_promocosto.Visible = True
                txt_promocosto.Visible = True
            Else
                lbl_promotipo.Visible = False
                txt_promotipo.Visible = False
                lbl_promomedios.Visible = False
                txt_promomedios.Visible = False
                lbl_promocosto.Visible = False
                txt_promocosto.Visible = False

            End If

            cmb_promo.ClearSelection()
            cmb_promo.Items.FindByValue(Session("rs").Fields("PROMOCION").Value.ToString).Selected = True

        End If

        Session("Con").Close()

    End Sub

#End Region

#Region "otros"

    Protected Sub btn_guarda_credito_Click(sender As Object, e As EventArgs) Handles btn_guarda_credito.Click
        Guarda_CreditosAd()
    End Sub

    Private Sub Guarda_CreditosAd()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCRED", Session("adVarChar"), Session("adParamInput"), 200, txt_credito_tipo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTI", Session("adVarChar"), Session("adParamInput"), 200, txt_credito_insti.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 19, txt_credito_monto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If txt_credito_plazo.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 9, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 9, txt_credito_plazo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        If txt_credito_tasa.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 6, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 6, txt_credito_tasa.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        If txt_credito_Fecha.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 10, txt_credito_Fecha.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("GTIAS", Session("adVarChar"), Session("adParamInput"), 500, txt_credito_gtias.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_CREDADI"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_credito.Text = "Guardado correctamente"
        LlenaCreditosAd()
        txt_credito_tipo.Text = ""
        txt_credito_insti.Text = ""
        txt_credito_monto.Text = ""
        txt_credito_plazo.Text = ""
        txt_credito_tasa.Text = ""
        txt_credito_Fecha.Text = ""
        txt_credito_gtias.Text = ""

    End Sub

    Protected Sub DAG_creditos_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_creditos.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            lbl_status_credito.Text = ""
            Elimina_CreditoAd(e.Item.Cells(1).Text)
            LlenaCreditosAd()
        End If
    End Sub

    Private Sub LlenaCreditosAd()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcreditos As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CREDADI"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcreditos, Session("rs"))
        DAG_creditos.DataSource = dtcreditos
        DAG_creditos.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub Elimina_CreditoAd(ByVal Cve_cred As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_CRED", Session("adVarChar"), Session("adParamInput"), 10, Cve_cred)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_CREDADI"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

End Class