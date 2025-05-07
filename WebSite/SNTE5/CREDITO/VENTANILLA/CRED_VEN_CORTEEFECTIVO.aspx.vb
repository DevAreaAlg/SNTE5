Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_VEN_CORTEEFECTIVO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Manejo de Efectivo", "Manejo de Efectivo")

        If Not Me.IsPostBack Then

            TabPanel1.Enabled = False
            TabPanel2.Enabled = False
            TabPanel3.Enabled = False
            TabPanel4.Enabled = False
            TabPanel5.Enabled = False
            TabPanel6.Enabled = False
            TabPanel7.Enabled = False
            LlenaCajaEquipo()
            Session("FP") = verifica_biometrico(Session("ID_EQ"), Session("USERID"), "ENVCAJCAJ")
        End If

        If Session("IDCAJA_USR") <> Nothing Then
            LlenaMovPend()

            If Session("PBTIRA") = "1" Then
                VerificaCaja()
                txt_Monto.Text = ""
                txt_MontoTE.Text = ""
                txt_MontoCambDenom.Text = ""
                txt_MontoCrr.Text = ""
                btn_Aplicar.Enabled = False
                btn_AplicarTE.Enabled = False
                btn_AplicaCrr.Enabled = False
                Session("PBTIRA") = Nothing
            End If

            If Session("VENGODEEFECTIVO") = "~/DIGITALIZADOR/DIGI_GLOBAL.aspx" Then
                TabContainer1.ActiveTab = TabPanel2
                Session("VENGODEEFECTIVO") = Nothing
            End If
        End If

        If Not Session("VAL_BIO") Is Nothing Then
            If Session("VAL_BIO") Then
                Session("VAL_BIO") = Nothing
                ObtieneNuevaSerie()
                ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=550,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
            Else
                lbl_InfoTE.Text = "Error: Validacion de huella incorrecta"
                Exit Sub
            End If
        End If

    End Sub

    Private Sub LlenaCajaEquipo()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_CajaPropia.Items.Clear()
        cmb_CajaPropia.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CAJAS_X_EQUIPO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            If Session("rs").Fields("RES").Value.ToString = "0" Then
                lbl_Info0.Text = "Su computadora no tiene permisos para utilizar este modulo!"
            Else
                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("CAJA_DESC").Value.ToString, Session("rs").Fields("IDCAJA").Value.ToString)
                    cmb_CajaPropia.Items.Add(item)
                    Session("rs").movenext()
                Loop
            End If
        Else
            lbl_Info0.Text = "Su computadora no tiene permisos para utilizar este modulo!"
        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_CajaPropia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_CajaPropia.SelectedIndexChanged

        lbl_Info0.Text = ""
        CreaTablas()
        LlenaDenominacion()

        If cmb_CajaPropia.SelectedItem.Value <> "0" Then
            Session("IDCAJA_USR") = cmb_CajaPropia.SelectedItem.Value
            VerificaCaja()
            LlenaMovPend()
            TabContainer1.ActiveTab = TabPanel1
            Session("ALERTA") = Nothing
        Else
            Session("IDCAJA_USR") = Nothing
            Session("ALERTA") = Nothing

            TabPanel1.Enabled = False
            TabPanel2.Enabled = False
            TabPanel3.Enabled = False
            TabPanel4.Enabled = False
            TabPanel5.Enabled = False
            TabPanel6.Enabled = False
            TabPanel7.Enabled = False
        End If

    End Sub

    Private Sub CreaTablas()

        Dim DenominacionTipo(50) As String
        Dim Acumulados(50) As Decimal
        Dim CantAcumulados(50) As Integer

        For i = 1 To 16
            Acumulados(i) = 0.0
            CantAcumulados(i) = 0
        Next

        Session("DenominacionTipo") = DenominacionTipo
        Session("Acumulados1") = Acumulados
        Session("CantAcumulados1") = CantAcumulados

    End Sub

    Private Sub LlenaDenominacion()

        Dim DenominacionTipo(50) As String
        Dim i As Integer = 1
        Dim valor As Decimal
        Dim tipo As String

        DenominacionTipo = Session("DenominacionTipo")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIRAEFEC_DENOMINACIONES_PESOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF

                valor = Session("rs").Fields("VALOR").Value.ToString
                tipo = Session("rs").Fields("TIPO").Value.ToString

                DenominacionTipo(i) = (CStr(valor) + tipo)

                i = i + 1
                Session("rs").movenext()
            Loop

        End If

        Session("Con").Close()

        Session("DenominacionTipo") = DenominacionTipo

    End Sub

    Private Sub VerificaCaja()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CAJA_CORTES"
        Session("rs") = Session("cmd").Execute()

        Session("RES") = Session("rs").Fields("RES").Value.ToString

        Session("Con").Close()

        If Session("RES") = "0" Or Session("RES") = "3" Then
            LlenaCajasCI()
            LlenaCajasCF()
            LlenaCajasFlujo()
            LlenaCajasCR()

            TabPanel1.Enabled = True
            TabPanel2.Enabled = True
            TabPanel3.Enabled = False
            TabPanel4.Enabled = True
            TabPanel5.Enabled = True
            TabPanel6.Enabled = True
            TabPanel7.Enabled = True

            TabContainer1.TabIndex = 0

        ElseIf Session("RES") = "1" Then

            TabPanel1.Enabled = False
            TabPanel2.Enabled = False
            TabPanel7.Enabled = False

            If VerificaCorteInicial() = "1" Then
                LlenaCajasFlujo()
                TabPanel3.Enabled = True
                TabPanel4.Enabled = True
                TabPanel5.Enabled = True
                TabPanel6.Enabled = True
                lbl_Info0.Text = ""
                VerificaCorteFinal()
            Else
                TabPanel3.Enabled = False
                TabPanel4.Enabled = False
                TabPanel5.Enabled = False
                TabPanel6.Enabled = False
                TabPanel7.Enabled = False
                lbl_Info0.Text = "No se ha realizado su corte inicial."
            End If

            TabContainer1.TabIndex = 2

        Else

            lbl_Info0.Text = "Su computadora no tiene permisos para utilizar este modulo!"
            TabPanel1.Enabled = False
            TabPanel2.Enabled = False
            TabPanel3.Enabled = False
            TabPanel4.Enabled = False
            TabPanel5.Enabled = False
            TabPanel6.Enabled = False
            TabPanel7.Enabled = False

        End If

    End Sub

    Private Function VerificaCorteInicial() As String

        Dim Corte As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CORTEINICIAL_CAJA"
        Session("rs") = Session("cmd").Execute()

        Corte = Session("rs").Fields("RES").Value.ToString

        Session("Con").Close()

        Return Corte

    End Function

    Private Sub VerificaCorteFinal()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CORTEFINAL_CAJA"
        Session("rs") = Session("cmd").Execute()

        Dim res As String
        res = Session("rs").Fields("RES").Value.ToString

        If res = "1" Then
            lbl_Info0.Text = "El registro de su corte final se ha enviado al jefe de sucursal."
            TabPanel3.Enabled = False
            TabPanel4.Enabled = False
            TabPanel5.Enabled = False
            TabPanel6.Enabled = False
        Else
            lbl_Info0.Text = ""
            TabPanel3.Enabled = True
            TabPanel4.Enabled = True
            TabPanel5.Enabled = True
            TabPanel6.Enabled = True
        End If

        Session("Con").Close()

    End Sub

    Private Sub ObtieneNuevaSerie()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_OBTIENE_SERIE"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            Session("SERIE") = Session("rs").Fields("SERIE").Value.ToString
            Session("FOLIO_IMP") = Session("rs").Fields("FOLIO_IMP").Value.ToString

        End If

        Session("Con").Close()

    End Sub

    Protected Sub ObtieneIP_Caja(ByVal IdCaja As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, IdCaja)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MAC_X_CAJA"
        Session("rs") = Session("cmd").Execute()

        Session("MACTira") = Session("rs").Fields("MAC").Value.ToString
        Session("ID_EQTira") = Session("rs").Fields("ID_EQ").Value.ToString

        Session("Con").Close()

    End Sub

    ' ------------------------------- CORTE INICIAL (JEFE DE SUCURSAL) -------------------------------

    Private Sub LlenaCajasCI()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_Cajas.Items.Clear()
        cmb_Cajas.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_CAJAS_SUC"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CAJADESC").Value.ToString, Session("rs").Fields("CAJA").Value.ToString)
                cmb_Cajas.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_Cajas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Cajas.SelectedIndexChanged

        If cmb_Cajas.SelectedItem.Value <> "0" Then
            btn_Aplicar.Enabled = True
            txt_Monto.Focus()
        Else
            btn_Aplicar.Enabled = False
        End If

    End Sub

    Protected Sub btn_Aplicar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Aplicar.Click

        If txt_Monto.Text <> "" Then
            lbl_Info.Text = ""
            Session("SERIE") = "CI"
            Session("CAJA_DEST") = cmb_Cajas.SelectedItem.Value
            Session("MONTO_EFECTIVO") = CDec(txt_Monto.Text)
            Session("ENTRADASALIDA") = "SALIDA"
            ObtieneNuevaSerie()
            ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=550,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
            'If Session("ExitoCI") = 1 Then
            '    lbl_Info.Text = "Corte de caja inicial realizado con éxito"
            'Else
            '    lbl_Info.Text = ""
            'End If
        Else
            lbl_Info.Text = "Falta monto"
        End If

    End Sub

    Protected Sub lnk_ReportesCI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ReportesCI.Click

        Session("SERIE_REPORTES") = "CI"
        ClientScript.RegisterStartupScript(GetType(String), "RepotesDenom", "window.open(""CRED_VEN_REP_DENOMINACION.aspx"", ""RP"", ""width=500,height=250,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)

    End Sub

    ' ------------------------------- TRASPASO EFECTIVO -------------------------------

    Private Sub LlenaCajasFlujo()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_CajasTE.Items.Clear()
        cmb_CajasTE.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SURTIR_CAJAS_SUC"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CAJADESC").Value.ToString, Session("rs").Fields("CAJA").Value.ToString)
                cmb_CajasTE.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_CajasTE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_CajasTE.SelectedIndexChanged

        If cmb_CajasTE.SelectedItem.Value <> "0" Then
            btn_AplicarTE.Enabled = True
            txt_MontoTE.Focus()
        Else
            btn_AplicarTE.Enabled = False
        End If

    End Sub

    Private Function verifica_biometrico(ByVal id_equipo As Integer, ByVal id_fp As Integer, ByVal tipo_val As String) As String
        Dim fp As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_FP", Session("adVarChar"), Session("adParamInput"), 10, id_fp)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_VAL", Session("adVarChar"), Session("adParamInput"), 10, tipo_val)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQUIPO", Session("adVarChar"), Session("adParamInput"), 10, id_equipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_BIOMETRICO"
        Session("rs") = Session("cmd").Execute()
        fp = Session("rs").Fields("FP").Value
        Session("Con").Close()
        Return fp
    End Function

    Protected Sub btn_AplicarTE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AplicarTE.Click

        If txt_MontoTE.Text <> "" Then
            lbl_InfoTE.Text = ""
            Session("SERIE") = "TE"
            Session("CAJA_DEST") = cmb_CajasTE.SelectedItem.Value
            Session("MONTO_EFECTIVO") = CDec(txt_MontoTE.Text)
            Session("ENTRADASALIDA") = "SALIDA"
            If Session("FP") <> "0" Then
                If Session("FP") = "-1" Then
                    lbl_InfoTE.Text = "Error: El usuario no tiene huella digital registrada"
                    Exit Sub
                Else
                    ClientScript.RegisterStartupScript(GetType(String), "Biometrico", "biometric('v');", True)
                End If
            Else
                ObtieneNuevaSerie()
                ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=550,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
            End If
        Else
            lbl_InfoTE.Text = "Falta monto"
        End If

    End Sub

    Protected Sub lnk_ReportesTE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ReportesTE.Click

        Session("SERIE_REPORTES") = "TE"
        ClientScript.RegisterStartupScript(GetType(String), "RepotesDenom", "window.open(""CRED_VEN_REP_DENOMINACION.aspx"", ""RP"", ""width=500,height=250,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)

    End Sub

    ' ------------------------------- CORTE FINAL (JEFE DE SUCURSAL) -------------------------------

    Private Sub LlenaCajasCF()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_Cajas2.Items.Clear()
        cmb_Cajas2.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_FINAL_CAJAS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CAJADESC").Value.ToString, Session("rs").Fields("CAJA").Value.ToString)
                cmb_Cajas2.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_Cajas2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Cajas2.SelectedIndexChanged

        If cmb_Cajas2.SelectedItem.Value <> 0 Then
            pnl_registroefect.Visible = True
            LlenaDenomCorteFinal()
            btn_Imprimir.Enabled = True
            btn_Digitalizar.Enabled = True
            TabPanel3.Enabled = False
        Else
            LlenaCajasCR()
            btn_AplicaCrr.Enabled = False
            btn_CorteFinal.Enabled = False
            btn_Imprimir.Enabled = False
            btn_Digitalizar.Enabled = False
            btn_Corregir.Enabled = False
            pnl_registroefect.Visible = False
            lbl_TotalFlujo.Text = ""
            lbl_TotalCaja.Text = ""
            lbl_AlertaTotal.Text = ""
            TabPanel3.Enabled = False
        End If

    End Sub

    Private Sub LlenaDenomCorteFinal()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_FINAL_COMP"
        Session("rs") = Session("cmd").Execute()

        Session("SERIE") = Session("rs").Fields("SERIE").Value.ToString
        Session("FOLIO_IMP") = Session("rs").Fields("FOLIO_IMP").Value.ToString
        lbl_TotalCaja.Text = FormatCurrency(Session("rs").Fields("TOTAL_REGIS").Value.ToString)
        Session("TOTAL_REGIS") = Session("rs").Fields("TOTAL_REGIS").Value.ToString
        lbl_TotalFlujo.Text = FormatCurrency(Session("rs").Fields("TOTAL_FLUJO").Value.ToString)
        Session("TOTAL_FLUJO") = Session("rs").Fields("TOTAL_FLUJO").Value.ToString
        lbl_AlertaTotal.Text = Session("rs").Fields("ALERTA_TOTAL").Value.ToString

        Dim i As Integer
        Dim AuxTipoDenom As Integer
        Dim Denom(100) As Label
        Dim CantFlujo(100) As Label
        Dim SubFlujo(100) As Label
        Dim CantRegis(100) As Label
        Dim SubRegis(100) As Label
        Dim Alerta(100) As Label
        Dim TipoDenom As Label

        Dim TableSup As Literal
        Dim TableInf As Literal
        Dim TableRowSup As Literal
        Dim TableRowInf As Literal
        Dim TableColSup As Literal
        Dim TableColInf As Literal

        Dim TCantFlujo As Label
        Dim TSubFlujo As Label
        Dim TCantRegis As Label
        Dim TSubRegis As Label

        TCantFlujo = New Label
        TSubFlujo = New Label
        TCantRegis = New Label
        TSubRegis = New Label
        TipoDenom = New Label

        TCantFlujo.Text = "Cant"
        TCantFlujo.CssClass = "subtitulos"
        TCantFlujo.Width = 50
        TSubFlujo.Text = "Monto"
        TSubFlujo.CssClass = "subtitulos"
        TSubFlujo.Width = 100
        TCantRegis.Text = "Cant"
        TCantRegis.CssClass = "subtitulos"
        TCantRegis.Width = 50
        TSubRegis.Text = "Monto"
        TSubRegis.CssClass = "subtitulos"
        TSubRegis.Width = 100
        TipoDenom.Text = "BILLETE"
        TipoDenom.CssClass = "texto"
        TipoDenom.Width = 100

        TableSup = New Literal
        TableInf = New Literal
        TableSup.Text = "<table>"
        TableInf.Text = "</table>"
        pnl_Denom.Controls.Add(TableSup)

        TableRowSup = New Literal
        TableRowInf = New Literal
        TableRowSup.Text = "<tr>"
        TableRowInf.Text = "</tr>"
        pnl_Denom.Controls.Add(TableRowSup)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""100"" align=""right"" style=""border-style: solid; border-width: 2px; border-color: #054B66 #FFFFFF #054B66 #FFFFFF;"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""50"" align=""center"" style=""border-style: solid; border-width: 2px; border-color: #054B66 #FFFFFF #054B66 #FFFFFF;"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TCantFlujo)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""100"" align=""center"" style=""border-style: solid; border-width: 2px; border-color: #054B66 #FFFFFF #054B66 #FFFFFF;"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TSubFlujo)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""50"" align=""center"" style=""border-style: solid; border-width: 2px; border-color: #054B66 #FFFFFF #054B66 #FFFFFF;"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TCantRegis)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""100"" align=""center"" style=""border-style: solid; border-width: 2px; border-color: #054B66 #FFFFFF #054B66 #FFFFFF;"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TSubRegis)
        pnl_Denom.Controls.Add(TableColInf)
        ' ---------------------------------------------
        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""325"" align=""center"" style=""border-style: solid; border-width: 2px; border-color: #054B66 #FFFFFF #054B66 #FFFFFF;"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)

        pnl_Denom.Controls.Add(TableRowInf)

        TableRowSup = New Literal
        TableRowInf = New Literal
        TableRowSup.Text = "<tr>"
        TableRowInf.Text = "</tr>"
        pnl_Denom.Controls.Add(TableRowSup)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""100"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TipoDenom)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""50"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""100"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""50"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)

        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""100"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)
        ' ---------------------------------------------
        TableColSup = New Literal
        TableColInf = New Literal
        TableColSup.Text = "<td width=""325"" align=""center"">"
        TableColInf.Text = "</td>"
        pnl_Denom.Controls.Add(TableColSup)
        pnl_Denom.Controls.Add(TableColInf)

        pnl_Denom.Controls.Add(TableRowInf)

        i = 1
        AuxTipoDenom = 0

        Do While Not Session("rs").EOF

            'Declaro los arreglos
            Denom(i) = New Label
            CantFlujo(i) = New Label
            SubFlujo(i) = New Label
            CantRegis(i) = New Label
            SubRegis(i) = New Label
            Alerta(i) = New Label

            'filtro(i) = New AjaxControlToolkit.FilteredTextBoxExtender

            Denom(i).Text = FormatCurrency(Session("rs").Fields("VALOR").Value.ToString)
            Denom(i).CssClass = "texto"
            Denom(i).Width = 100

            CantFlujo(i).Text = Session("rs").Fields("CANT_FLUJO").Value.ToString
            CantFlujo(i).CssClass = "texto"
            CantFlujo(i).Width = 50

            SubFlujo(i).Text = FormatCurrency(Session("rs").Fields("SUB_FLUJO").Value.ToString)
            SubFlujo(i).CssClass = "texto"
            SubFlujo(i).Width = 100

            CantRegis(i).Text = Session("rs").Fields("CANT_REGIS").Value.ToString
            CantRegis(i).CssClass = "texto"
            CantRegis(i).Width = 50

            SubRegis(i).Text = FormatCurrency(Session("rs").Fields("SUB_REGIS").Value.ToString)
            SubRegis(i).CssClass = "texto"
            SubRegis(i).Width = 100

            If Session("rs").Fields("ALERTA").Value.ToString <> "" Then
                Session("ALERTA") = 1
                btn_Corregir.Enabled = True
            End If

            Alerta(i).Text = Session("rs").Fields("ALERTA").Value.ToString
            Alerta(i).CssClass = "textogris"
            Alerta(i).ForeColor = Drawing.Color.Red
            Alerta(i).Width = 325

            If Session("rs").Fields("TIPO_DENOM").Value.ToString = "MONEDA" And AuxTipoDenom = 0 Then
                TipoDenom = New Label
                TipoDenom.Text = "MONEDA"
                TipoDenom.CssClass = "texto"
                TipoDenom.Width = 100

                TableRowSup = New Literal
                TableRowInf = New Literal
                TableRowSup.Text = "<tr>"
                TableRowInf.Text = "</tr>"
                pnl_Denom.Controls.Add(TableRowSup)

                TableColSup = New Literal
                TableColInf = New Literal
                TableColSup.Text = "<td align=""left"" >"
                TableColInf.Text = "</td>"
                pnl_Denom.Controls.Add(TableColSup)
                pnl_Denom.Controls.Add(TipoDenom)
                pnl_Denom.Controls.Add(TableColInf)

                pnl_Denom.Controls.Add(TableRowInf)
                AuxTipoDenom = 1
            End If

            ' Superior del Row
            TableRowSup = New Literal
            TableRowInf = New Literal
            TableRowSup.Text = "<tr>"
            TableRowInf.Text = "</tr>"
            pnl_Denom.Controls.Add(TableRowSup)

            'Denominacion
            TableColSup = New Literal
            TableColInf = New Literal
            TableColSup.Text = "<td align=""right"">"
            TableColInf.Text = "</td>"
            pnl_Denom.Controls.Add(TableColSup)
            pnl_Denom.Controls.Add(Denom(i))
            pnl_Denom.Controls.Add(TableColInf)

            'Flujo(Cantidad)
            TableColSup = New Literal
            TableColInf = New Literal
            TableColSup.Text = "<td align=""center"">"
            TableColInf.Text = "</td>"
            pnl_Denom.Controls.Add(TableColSup)
            pnl_Denom.Controls.Add(CantFlujo(i))
            pnl_Denom.Controls.Add(TableColInf)

            'Flujo(SubTotal)
            TableColSup = New Literal
            TableColInf = New Literal
            TableColSup.Text = "<td align=""right"">"
            TableColInf.Text = "</td>"
            pnl_Denom.Controls.Add(TableColSup)
            pnl_Denom.Controls.Add(SubFlujo(i))
            pnl_Denom.Controls.Add(TableColInf)

            'Cajero(Cantidad)
            TableColSup = New Literal
            TableColInf = New Literal
            TableColSup.Text = "<td align=""center"">"
            TableColInf.Text = "</td>"
            pnl_Denom.Controls.Add(TableColSup)
            pnl_Denom.Controls.Add(CantRegis(i))
            pnl_Denom.Controls.Add(TableColInf)

            'Flujo(SubTotal)
            TableColSup = New Literal
            TableColInf = New Literal
            TableColSup.Text = "<td align=""right"">"
            TableColInf.Text = "</td>"
            pnl_Denom.Controls.Add(TableColSup)
            pnl_Denom.Controls.Add(SubRegis(i))
            pnl_Denom.Controls.Add(TableColInf)

            'Alertas
            TableColSup = New Literal
            TableColInf = New Literal
            TableColSup.Text = "<td align=""center"">"
            TableColInf.Text = "</td>"
            pnl_Denom.Controls.Add(TableColSup)
            pnl_Denom.Controls.Add(Alerta(i))
            pnl_Denom.Controls.Add(TableColInf)

            ' Inferior del Row
            pnl_Denom.Controls.Add(TableRowInf)

            Session("rs").movenext()
            i = i + 1
        Loop

        pnl_Denom.Controls.Add(TableInf)

        Session("Con").Close()

        Session("CantRegis") = CantRegis

        If Session("ALERTA") = 1 Then
            'btn_Corregir.Enabled = True
            btn_CorteFinal.Enabled = False
        Else
            btn_Corregir.Enabled = False

            If VerificaRepDigit() = "NO" Then
                btn_CorteFinal.Enabled = False
            Else
                btn_CorteFinal.Enabled = True
            End If

        End If

    End Sub

    Private Function VerificaRepDigit() As String

        Dim Digit As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 15, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_REP_DENOM_DIGIT"
        Session("rs") = Session("cmd").Execute()

        Digit = Session("rs").Fields("RES").Value.ToString

        Session("Con").Close()

        Return Digit

    End Function

    Protected Sub btn_Imprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Imprimir.Click

        LlenaDenomCorteFinal()
        LlenaReporteCorteFinal(cmb_Cajas2.SelectedItem.Value)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= CorteFinal.pdf")
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

    Private Sub LlenaReporteCorteFinal(ByVal idcaja As Integer)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\CorteFinal.pdf")

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
            .AddAuthor("MasCore")
            .AddCreationDate()
            .AddCreator("MasCore")
            .AddSubject("MasCore")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("MasCore")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("MasCore")
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
        Dim X, Y As Single

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_FINAL_COMP"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Dim TotalRegis As Decimal
            Dim TotalFlujo As Decimal
            Dim AlertaTotal As String
            Dim aux As Integer

            TotalRegis = Session("rs").Fields("TOTAL_REGIS").Value.ToString
            TotalFlujo = Session("rs").Fields("TOTAL_FLUJO").Value.ToString
            AlertaTotal = Session("rs").Fields("ALERTA_TOTAL").Value.ToString
            aux = 0

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            X = 60
            Y = 665

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Folio: " + Session("rs").Fields("SERIE").Value.ToString + "-" + Session("rs").Fields("FOLIO_IMP").Value.ToString(), X, Y, 0)

            Dim fecha As String
            fecha = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHASIS").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "Fecha: " + fecha, X + 200, Y, 0)

            X = 170
            Y = Y - 30

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Flujo de Efectivo", X, Y, 0)
            X = X + 140
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Registro de Cajero", X, Y, 0)

            X = 60
            Y = Y - 15

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Denom.", X, Y, 0)
            X = X + 55
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Cantidad", X, Y, 0)
            X = X + 55
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Subtotal", X, Y, 0)
            X = X + 55
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Cantidad", X, Y, 0)
            X = X + 55
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Subtotal", X, Y, 0)
            X = X + 140
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Alertas", X, Y, 0)
            Y = Y - 7
            X = X - 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "_____________________________________________________________________________________________________________________", X, Y, 0)

            Y = Y - 22

            X = 35
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TIPO_DENOM").Value.ToString, X, Y, 0)

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            Do While Not Session("rs").EOF

                Y = Y - 15
                X = 80

                If Session("rs").Fields("TIPO_DENOM").Value.ToString = "MONEDA" And aux = 0 Then
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)
                    X = 35
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TIPO_DENOM").Value.ToString, X, Y, 0)
                    aux = 1
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)
                    Y = Y - 15
                    X = 80
                End If
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("VALOR").Value.ToString), X, Y, 0)
                X = X + 35
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("CANT_FLUJO").Value.ToString, X, Y, 0)
                X = X + 75
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SUB_FLUJO").Value.ToString), X, Y, 0)
                X = X + 35
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("CANT_REGIS").Value.ToString, X, Y, 0)
                X = X + 75
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SUB_REGIS").Value.ToString), X, Y, 0)
                X = X + 30
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ALERTA").Value.ToString, X, Y, 0)

                Session("rs").movenext()
            Loop

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)
            Y = Y - 15
            X = 305

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "_____________________________________________________________________________________________________________________", X, Y, 0)

            X = X - 270

            Y = Y - 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "TOTAL", X, Y, 0)

            X = X + 155
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(TotalFlujo), X, Y, 0)
            X = X + 110
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(TotalRegis), X, Y, 0)
            X = X + 20
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, AlertaTotal, X, Y, 0)

        End If

        Session("Con").Close()

        cb.EndText()
        document.Close()

    End Sub

    Protected Sub btn_CorteFinal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CorteFinal.Click

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_TRANSACCION", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CORTE_FINAL_EFECTIVO"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

        LlenaCajasCF()
        lbl_Info0.Text = ""
        lbl_TotalFlujo.Text = ""
        lbl_TotalCaja.Text = ""
        lbl_AlertaTotal.Text = ""
        lbl_Info0.Text = "Corte de caja final se ha realizado."
        btn_Imprimir.Enabled = False
        btn_CorteFinal.Enabled = False
        btn_Digitalizar.Enabled = False
        pnl_registroefect.Visible = False
    End Sub

    Protected Sub btn_Digitalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Digitalizar.Click

        Session("IDCAJA") = cmb_Cajas2.SelectedItem.Value
        Session("VENGODE") = "CRED_VEN_CORTEEFECTIVO.aspx"
        Response.Redirect("../../DIGITALIZADOR/DIGI_GLOBAL.aspx")

    End Sub

    Protected Sub btn_Corregir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Corregir.Click

        cmb_CajasCrr.Items.Clear()
        Dim item As New ListItem(cmb_Cajas2.SelectedItem.Text, cmb_Cajas2.SelectedItem.Value)
        cmb_CajasCrr.Items.Add(item)
        btn_AplicaCrr.Enabled = True

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 10, "CORTE")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 50, Session("TOTAL_FLUJO") - Session("TOTAL_REGIS"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CAJAS_DIFERENCIA_CORTE"
        Session("cmd").Execute()
        Session("Con").Close()

        Session("CORREGIR") = "CORREGIR"

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CORREGIR_FINAL_EFECTIVO"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

        TabPanel3.Enabled = True
        TabContainer1.ActiveTab = TabPanel3

        lbl_CorregirTiraEfectivo.Visible = True
        LlenaDenomCorregir()

    End Sub

    Private Sub LlenaDenomCorregir()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Cajas2.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORREGIR_EFECTIVO_REGIS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            lbl_MontoAcum.Text = Session("rs").Fields("TOTAL").Value.ToString

            Do While Not Session("rs").EOF
                Select Case Session("rs").Fields("ID").Value.ToString

                    Case 1
                        txt_Cant16.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont16.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("16", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 2
                        txt_Cant15.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont15.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("15", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 3
                        txt_Cant14.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont14.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("14", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 4
                        txt_Cant13.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont13.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("13", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 5
                        txt_Cant12.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont12.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("12", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 6
                        txt_Cant11.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont11.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("11", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 7
                        txt_Cant10.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont10.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("10", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 8
                        txt_Cant9.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont9.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("9", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 9
                        txt_Cant8.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont8.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("8", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 10
                        txt_Cant7.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont7.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("7", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 11
                        txt_Cant6.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont6.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("6", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 12
                        txt_Cant5.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont5.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("5", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 13
                        txt_Cant4.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont4.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("4", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 14
                        txt_Cant3.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont3.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("3", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 15
                        txt_Cant2.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont2.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("2", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)
                    Case 16
                        txt_Cant1.Text = Session("rs").Fields("CANTIDAD").Value.ToString
                        txt_Mont1.Text = Session("rs").Fields("SUBTOTAL").Value.ToString
                        ActualizaSubMontos("1", Session("rs").Fields("SUBTOTAL").Value.ToString, Session("rs").Fields("CANTIDAD").Value.ToString)

                End Select

                Session("rs").movenext()

            Loop

        End If

        Session("Con").Close()

    End Sub

    Private Sub ActualizaCorregirDenom()

        Dim i As Integer
        Dim ModDenominacion As New DataTable
        Dim DenominacionTipo(50) As String
        Dim CantAcumulados(50) As Integer

        ModDenominacion.Columns.Add("DENOMTIPO", GetType(String))
        ModDenominacion.Columns.Add("CANTIDAD", GetType(Integer))

        DenominacionTipo = Session("DenominacionTipo")
        CantAcumulados = Session("CantAcumulados1")

        For i = 1 To 16
            ModDenominacion.Rows.Add(DenominacionTipo(i), CantAcumulados(i))
        Next

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            Dim insertCommand As New SqlCommand("UPD_CORREGIR_DENOM_CAJERO", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("IDCAJA", SqlDbType.Int)
            Session("parm").Value = cmb_Cajas2.SelectedItem.Value
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("DENOM", SqlDbType.Structured)
            Session("parm").Value = ModDenominacion
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SERIE", SqlDbType.VarChar)
            Session("parm").Value = Session("SERIE")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("FOLIO_IMP", SqlDbType.VarChar)
            Session("parm").Value = Session("FOLIO_IMP")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
            Session("parm").Value = Session("Sesion")
            insertCommand.Parameters.Add(Session("parm"))

            insertCommand.ExecuteNonQuery()
            connection.Close()

        End Using

    End Sub

    ' ------------------------------- CORTE FINAL (CAJERO) -------------------------------

    Private Sub ActualizaSubMontos(ByVal NumID As String, ByVal SubMonto As Decimal, ByVal Cant As Integer)

        Dim Acumulados(50) As Decimal
        Dim CantAcumulados(50) As Integer

        Acumulados = Session("Acumulados1")
        CantAcumulados = Session("CantAcumulados1")

        Acumulados(CInt(NumID)) = SubMonto
        CantAcumulados(CInt(NumID)) = Cant

        Session("Acumulados1") = Acumulados
        Session("CantAcumulados1") = CantAcumulados

        ActualizaAcumulados()

    End Sub

    Private Sub ActualizaAcumulados()

        Dim Acumulados(50) As Decimal
        Dim i As Integer

        Acumulados = Session("Acumulados1")
        Session("ACUMULADO") = 0.0

        For i = 1 To 16
            Session("ACUMULADO") = Session("ACUMULADO") + Acumulados(i)
        Next

        lbl_MontoAcum.Text = FormatCurrency(Session("ACUMULADO").ToString)

    End Sub

    Protected Sub txt_Cant1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant1.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant1.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant1.Text)
        End If
        denom = CDec(lbl_Deno1.Text)

        monto = cantidad * denom
        ActualizaSubMontos("1", monto, cantidad)

        txt_Cant1.Text = cantidad
        txt_Mont1.Text = monto

        txt_Cant2.Focus()

    End Sub

    Protected Sub txt_Cant2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant2.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant2.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant2.Text)
        End If
        denom = CDec(lbl_Deno2.Text)

        monto = cantidad * denom
        ActualizaSubMontos("2", monto, cantidad)

        txt_Cant2.Text = cantidad
        txt_Mont2.Text = monto

        txt_Cant3.Focus()

    End Sub

    Protected Sub txt_Cant3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant3.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant3.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant3.Text)
        End If
        denom = CDec(lbl_Deno3.Text)

        monto = cantidad * denom
        ActualizaSubMontos("3", monto, cantidad)

        txt_Cant3.Text = cantidad
        txt_Mont3.Text = monto

        txt_Cant4.Focus()

    End Sub

    Protected Sub txt_Cant4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant4.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant4.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant4.Text)
        End If
        denom = CDec(lbl_Deno4.Text)

        monto = cantidad * denom
        ActualizaSubMontos("4", monto, cantidad)

        txt_Cant4.Text = cantidad
        txt_Mont4.Text = monto

        txt_Cant5.Focus()

    End Sub

    Protected Sub txt_Cant5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant5.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant5.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant5.Text)
        End If
        denom = CDec(lbl_Deno5.Text)

        monto = cantidad * denom
        ActualizaSubMontos("5", monto, cantidad)

        txt_Cant5.Text = cantidad
        txt_Mont5.Text = monto

        txt_Cant6.Focus()

    End Sub

    Protected Sub txt_Cant6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant6.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant6.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant6.Text)
        End If
        denom = CDec(lbl_Deno6.Text)

        monto = cantidad * denom
        ActualizaSubMontos("6", monto, cantidad)

        txt_Cant6.Text = cantidad
        txt_Mont6.Text = monto

        txt_Cant7.Focus()

    End Sub

    Protected Sub txt_Cant7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant7.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant7.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant7.Text)
        End If
        denom = CDec(lbl_Deno7.Text)

        monto = cantidad * denom
        ActualizaSubMontos("7", monto, cantidad)

        txt_Cant7.Text = cantidad
        txt_Mont7.Text = monto

        txt_Cant8.Focus()

    End Sub

    Protected Sub txt_Cant8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant8.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant8.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant8.Text)
        End If
        denom = CDec(lbl_Deno8.Text)

        monto = cantidad * denom
        ActualizaSubMontos("8", monto, cantidad)

        txt_Cant8.Text = cantidad
        txt_Mont8.Text = monto

        txt_Cant9.Focus()

    End Sub

    Protected Sub txt_Cant9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant9.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant9.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant9.Text)
        End If
        denom = CDec(lbl_Deno9.Text)

        monto = cantidad * denom
        ActualizaSubMontos("9", monto, cantidad)

        txt_Cant9.Text = cantidad
        txt_Mont9.Text = monto

        txt_Cant10.Focus()

    End Sub

    Protected Sub txt_Cant10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant10.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant10.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant10.Text)
        End If
        denom = CDec(lbl_Deno10.Text)

        monto = cantidad * denom
        ActualizaSubMontos("10", monto, cantidad)

        txt_Cant10.Text = cantidad
        txt_Mont10.Text = monto

        txt_Cant11.Focus()

    End Sub

    Protected Sub txt_Cant11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant11.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant11.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant11.Text)
        End If
        denom = CDec(lbl_Deno11.Text)

        monto = cantidad * denom
        ActualizaSubMontos("11", monto, cantidad)

        txt_Cant11.Text = cantidad
        txt_Mont11.Text = monto

        txt_Cant12.Focus()

    End Sub

    Protected Sub txt_Cant12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant12.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant12.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant12.Text)
        End If
        denom = CDec(lbl_Deno12.Text)

        monto = cantidad * denom
        ActualizaSubMontos("12", monto, cantidad)

        txt_Cant12.Text = cantidad
        txt_Mont12.Text = monto
        txt_Cant13.Focus()

    End Sub

    Protected Sub txt_Cant13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant13.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant13.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant13.Text)
        End If
        denom = CDec(lbl_Deno13.Text)

        monto = cantidad * denom
        ActualizaSubMontos("13", monto, cantidad)

        txt_Cant13.Text = cantidad
        txt_Mont13.Text = monto

        txt_Cant14.Focus()

    End Sub

    Protected Sub txt_Cant14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant14.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant14.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant14.Text)
        End If
        denom = CDec(lbl_Deno14.Text)

        monto = cantidad * denom
        ActualizaSubMontos("14", monto, cantidad)

        txt_Cant14.Text = cantidad
        txt_Mont14.Text = monto

        txt_Cant15.Focus()

    End Sub

    Protected Sub txt_Cant15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant15.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant15.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant15.Text)
        End If
        denom = CDec(lbl_Deno15.Text)

        monto = cantidad * denom
        ActualizaSubMontos("15", monto, cantidad)

        txt_Cant15.Text = cantidad
        txt_Mont15.Text = monto

        txt_Cant16.Focus()

    End Sub

    Protected Sub txt_Cant16_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant16.TextChanged

        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal

        If txt_Cant16.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant16.Text)
        End If
        denom = CDec(lbl_Deno16.Text)

        monto = cantidad * denom
        ActualizaSubMontos("16", monto, cantidad)

        txt_Cant16.Text = cantidad
        txt_Mont16.Text = monto

    End Sub

    Protected Sub btn_GuardaDenomCaja_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardaDenomCaja.Click

        If Session("CORREGIR") = "CORREGIR" Then

            ActualizaCorregirDenom()

            Session("CORREGIR") = Nothing

            LlenaCajasCF()
            lbl_TotalFlujo.Text = ""
            lbl_TotalCaja.Text = ""
            lbl_AlertaTotal.Text = ""
            lbl_Info0.Text = "Corrección guardada"
            btn_Imprimir.Enabled = False
            btn_CorteFinal.Enabled = False
            btn_Digitalizar.Enabled = False
            'btn_Corregir.Visible = False
            btn_Corregir.Enabled = False
            TabContainer1.ActiveTab = TabPanel2
            TabPanel3.Enabled = False

        Else

            If VerificaMovPend() = "1" Then
                lbl_Info0.Text = "Error: Aún existen movimientos pendientes para esta caja"
                TabPanel3.Enabled = False
                TabContainer1.ActiveTab = TabPanel6
            Else
                If VerificaDigPend() = "1" Then
                    lbl_Info0.Text = "Error: Aún existen digitalizaciones pendientes para esta caja, Favor de digitalizarlos en el módulo Operaciones Efectivo"
                    TabPanel3.Enabled = False
                    TabPanel4.Enabled = False
                    TabPanel5.Enabled = False
                    TabPanel6.Enabled = False
                Else

                    If VerificaCorteFinalCajero() = "0" Then
                        'Label1.Text = ""
                        Session("SERIE") = "CF"
                        ObtieneNuevaSerie()
                        RegistraDenomCajero()
                        lbl_Info0.Text = "El registro de corte final se ha enviado al jefe de sucursal."
                        TabPanel3.Enabled = False
                        TabPanel4.Enabled = False
                        TabPanel5.Enabled = False
                        TabPanel6.Enabled = False
                    Else
                        lbl_Info0.Text = "El registro de corte final ya fue enviado al jefe de sucursal."
                        TabPanel3.Enabled = False
                        TabPanel4.Enabled = False
                        TabPanel5.Enabled = False
                        TabPanel6.Enabled = False
                    End If

                End If

            End If

        End If

    End Sub


    Private Function VerificaDigPend() As String

        Dim DigPend As String = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPEEFE_DIG_PENDIENTES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            DigPend = Session("rs").Fields("RES").Value.ToString
        End If

        Session("Con").Close()

        Return DigPend

    End Function

    Private Function VerificaCorteFinalCajero() As String

        Dim MovPend As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CORTE_CAJA"
        Session("rs") = Session("cmd").Execute()
        MovPend = Session("rs").Fields("RES").Value.ToString
        Session("Con").Close()

        Return MovPend

    End Function



    Private Function VerificaMovPend() As String

        Dim MovPend As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_MOV_PEND"
        Session("rs") = Session("cmd").Execute()

        MovPend = Session("rs").Fields("RES").Value.ToString

        Session("Con").Close()

        Return MovPend

    End Function

    Private Sub RegistraDenomCajero()

        Dim i As Integer
        Dim ModDenominacion As New DataTable
        Dim DenominacionTipo(50) As String
        Dim CantAcumulados(50) As Integer

        ModDenominacion.Columns.Add("DENOMTIPO", GetType(String))
        ModDenominacion.Columns.Add("CANTIDAD", GetType(Integer))

        DenominacionTipo = Session("DenominacionTipo")
        CantAcumulados = Session("CantAcumulados1")

        For i = 1 To 16
            ModDenominacion.Rows.Add(DenominacionTipo(i), CantAcumulados(i))
        Next

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            Dim insertCommand As New SqlCommand("INS_CORTE_DENOM_CAJERO", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("ID_EQ", SqlDbType.Int)
            Session("parm").Value = Session("ID_EQ")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDCAJA", SqlDbType.Int)
            Session("parm").Value = Session("IDCAJA_USR")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("DENOM", SqlDbType.Structured)
            Session("parm").Value = ModDenominacion
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SERIE", SqlDbType.VarChar)
            Session("parm").Value = Session("SERIE")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("FOLIO_IMP", SqlDbType.VarChar)
            Session("parm").Value = Session("FOLIO_IMP")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
            Session("parm").Value = Session("Sesion")
            insertCommand.Parameters.Add(Session("parm"))

            insertCommand.ExecuteNonQuery()
            connection.Close()

        End Using

    End Sub

    ' ------------------------------- CAMBIO DE DENOMINACION -------------------------------

    Protected Sub btn_CambDenom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CambDenom.Click

        If txt_MontoCambDenom.Text <> "" Then
            lbl_Info1.Text = ""
            Session("SERIE") = "CD"
            Session("MONTO_EFECTIVO") = CDec(txt_MontoCambDenom.Text)
            Session("ENTRADASALIDA") = "ENTRADA"
            ObtieneNuevaSerie()
            ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=550,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
        Else
            lbl_Info1.Text = "Falta monto"
        End If


    End Sub

    ' ------------------------------- MOVIMIENTOS PENDIENTES -------------------------------

    Private Sub LlenaMovPend()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtMovPend As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MOV_PEND"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtMovPend, Session("rs"))
        'se vacian los expedientes al formulario
        dag_MovPend.DataSource = dtMovPend
        dag_MovPend.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub dag_MovPend_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_MovPend.ItemCommand

        If (e.CommandName = "REALIZAR") Then
            Session("ENTRADASALIDA") = e.Item.Cells(2).Text
            Session("MONTO_EFECTIVO") = e.Item.Cells(1).Text
            Session("SERIE") = Mid(e.Item.Cells(0).Text, 1, 3)
            Session("FOLIO_IMP") = Mid(e.Item.Cells(0).Text, 5)
            Session("UNIR") = "SI"
            Session("PENDIENTE") = "SI"
            Session("CAMBIO") = ""
            ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=550,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
            LlenaMovPend()
        End If

    End Sub

    ' ------------------------------- CORRECCION DE FLUJO DE EFECTIVO -------------------------------

    Private Sub LlenaCajasCR()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_CajasCrr.Items.Clear()
        cmb_CajasCrr.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SURTIR_CAJAS_SUC"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CAJADESC").Value.ToString, Session("rs").Fields("CAJA").Value.ToString)
                cmb_CajasCrr.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_CajasCrr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_CajasCrr.SelectedIndexChanged

        If cmb_CajasCrr.SelectedItem.Value <> "0" Then
            btn_AplicaCrr.Enabled = True
            txt_MontoCrr.Focus()
        Else
            btn_AplicaCrr.Enabled = False
        End If

    End Sub

    Protected Sub btn_AplicaCrr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AplicaCrr.Click

        If txt_MontoCrr.Text <> "" Then
            If CDec(txt_MontoCrr.Text) > 0.0 Then
                lbl_InfoCrr.Text = ""
                Session("SERIE") = "CR"
                ObtieneIP_Caja(cmb_CajasCrr.SelectedItem.Value)
                Session("CAJA_DEST") = cmb_CajasCrr.SelectedItem.Value
                Session("MONTO_EFECTIVO") = CDec(txt_MontoCrr.Text)
                Session("ENTRADASALIDA") = cmb_TipoOpe.SelectedItem.Value
                If Session("FP") <> "0" Then
                    ClientScript.RegisterStartupScript(GetType(String), "Biometrico", "biometric('v');", True)
                Else
                    ObtieneNuevaSerie()
                    ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=550,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
                End If
            Else
                lbl_InfoCrr.Text = "Falta monto"
            End If
        Else
            lbl_InfoCrr.Text = "Falta monto"
        End If

    End Sub

End Class