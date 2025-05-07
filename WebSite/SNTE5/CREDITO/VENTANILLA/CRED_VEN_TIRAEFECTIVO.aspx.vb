Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_VEN_TIRAEFECTIVO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If

            If Session("SERIE") <> "CR" Then
                Session("ID_EQTira") = Session("ID_EQ")
                Session("IDCAJA_Tira") = Session("IDCAJA_USR")
            Else
                Session("IDCAJA_Tira") = Session("CAJA_DEST")
            End If

            lbl_MontoReq.Text = FormatCurrency(Session("MONTO_EFECTIVO").ToString)
            lbl_EntradaSalida.Text = Session("ENTRADASALIDA").ToString

            If Session("IDTRANSACCION") Is Nothing Then
                Session("IDTRANSACCION") = 0
            End If

            Empresa()
            CreaTablas()
            Limpia()
            ConfiguracionInicial()

            If Session("SERIE") = "CI" Or Session("SERIE") = "TE" Or Session("SERIE") = "ED" Or Session("SERIE") = "CF" Or Session("SERIE") = "CD" Or Session("SERIE") = "CR" Then
                lnk_Union.Enabled = False
                rad_Indepen.Enabled = False
                rad_Union.Enabled = False
            End If

            If Session("CAMBIO") Is Nothing Then
                Session("CAMBIO") = "NO"
            End If

        End If

        If Session("CAMBIO") Is Nothing Then
            Session("CAMBIO") = "NO"
        End If

        txt_Cant1.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant1.ClientID + "','','" + txt_Cant2.ClientID + "',event)")
        txt_Cant2.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant2.ClientID + "','" + txt_Cant1.ClientID + "','" + txt_Cant3.ClientID + "',event)")
        txt_Cant3.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant3.ClientID + "','" + txt_Cant2.ClientID + "','" + txt_Cant4.ClientID + "',event)")
        txt_Cant4.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant4.ClientID + "','" + txt_Cant3.ClientID + "','" + txt_Cant5.ClientID + "',event)")
        txt_Cant5.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant5.ClientID + "','" + txt_Cant4.ClientID + "','" + txt_Cant6.ClientID + "',event)")
        txt_Cant6.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant6.ClientID + "','" + txt_Cant5.ClientID + "','" + txt_Cant7.ClientID + "',event)")
        txt_Cant7.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant7.ClientID + "','" + txt_Cant6.ClientID + "','" + txt_Cant8.ClientID + "',event)")
        txt_Cant8.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant8.ClientID + "','" + txt_Cant7.ClientID + "','" + txt_Cant9.ClientID + "',event)")
        txt_Cant9.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant9.ClientID + "','" + txt_Cant8.ClientID + "','" + txt_Cant10.ClientID + "',event)")
        txt_Cant10.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant10.ClientID + "','" + txt_Cant9.ClientID + "','" + txt_Cant11.ClientID + "',event)")
        txt_Cant11.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant11.ClientID + "','" + txt_Cant10.ClientID + "','" + txt_Cant12.ClientID + "',event)")
        txt_Cant12.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant12.ClientID + "','" + txt_Cant11.ClientID + "','" + txt_Cant13.ClientID + "',event)")
        txt_Cant13.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant13.ClientID + "','" + txt_Cant12.ClientID + "','" + txt_Cant14.ClientID + "',event)")
        txt_Cant14.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant14.ClientID + "','" + txt_Cant13.ClientID + "','" + txt_Cant15.ClientID + "',event)")
        txt_Cant15.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant15.ClientID + "','" + txt_Cant14.ClientID + "','" + txt_Cant16.ClientID + "',event)")
        txt_Cant16.Attributes.Add("onkeydown", "presionaTecla('" + txt_Cant16.ClientID + "','" + txt_Cant15.ClientID + "','',event)")

        txt_Cant1.Focus()

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

    Private Sub CreaTablas()

        Dim DenominacionTipo(50) As String
        Dim Cantidad(50) As Integer
        Dim Acumulados(50) As Decimal
        Dim CantAcumulados(50) As Integer
        Dim ValorDenom(50) As Decimal
        Dim tabla_folios As New DataTable

        For i = 1 To 16
            Acumulados(i) = 0.0
            CantAcumulados(i) = 0
        Next

        Session("DenominacionTipoUser") = Nothing
        Session("CantidadUser") = Nothing
        Session("Acumulados") = Nothing
        Session("CantAcumulados") = Nothing
        Session("ValorDenom") = Nothing
        Session("tabla_folios") = Nothing

        Session("DenominacionTipoUser") = DenominacionTipo
        Session("CantidadUser") = Cantidad
        Session("Acumulados") = Acumulados
        Session("CantAcumulados") = CantAcumulados
        Session("ValorDenom") = ValorDenom

        tabla_folios.Columns.Add("ID_CAJA", GetType(Integer))
        tabla_folios.Columns.Add("SERIE", GetType(String))
        tabla_folios.Columns.Add("FOLIO_IMP", GetType(String))
        tabla_folios.Columns.Add("MONTO", GetType(Decimal))

        Session("tabla_folios") = tabla_folios

    End Sub

    Private Sub LimpiarTablas()

        Session("DenominacionTipoUser") = Nothing
        Session("CantidadUser") = Nothing
        Session("Acumulados") = Nothing
        Session("CantAcumulados") = Nothing
        Session("ValorDenom") = Nothing
        Session("tabla_folios") = Nothing

    End Sub

    Private Sub Limpia()

        txt_Cant1.Text = ""
        txt_Cant2.Text = ""
        txt_Cant3.Text = ""
        txt_Cant4.Text = ""
        txt_Cant5.Text = ""
        txt_Cant6.Text = ""
        txt_Cant7.Text = ""
        txt_Cant8.Text = ""
        txt_Cant9.Text = ""
        txt_Cant10.Text = ""
        txt_Cant11.Text = ""
        txt_Cant12.Text = ""
        txt_Cant13.Text = ""
        txt_Cant14.Text = ""
        txt_Cant15.Text = ""
        txt_Cant16.Text = ""

        txt_Mont1.Text = "0.00"
        txt_Mont2.Text = "0.00"
        txt_Mont3.Text = "0.00"
        txt_Mont4.Text = "0.00"
        txt_Mont5.Text = "0.00"
        txt_Mont6.Text = "0.00"
        txt_Mont7.Text = "0.00"
        txt_Mont8.Text = "0.00"
        txt_Mont9.Text = "0.00"
        txt_Mont10.Text = "0.00"
        txt_Mont11.Text = "0.00"
        txt_Mont12.Text = "0.00"
        txt_Mont13.Text = "0.00"
        txt_Mont14.Text = "0.00"
        txt_Mont15.Text = "0.00"
        txt_Mont16.Text = "0.00"

        lbl_MontoAcum.Text = FormatCurrency("0.00")
        lbl_MontoFalt.Text = FormatCurrency(Session("MONTO_EFECTIVO").ToString)

    End Sub

    Private Sub ConfiguracionInicial()

        Session("CONFIRMA_UNION") = "NO"
        LlenaDenominacionUser()

        'If Session("PENDIENTE") <> "SI" Then

        If Session("CAMBIO") <> "SI" Then


            If Session("UNIR") = "SI" Then

                rad_Indepen.Visible = True
                rad_Union.Visible = True
                rad_Indepen.Checked = True

            End If

            'Session("Con").Close()

            Session("CAMBIO") = "NO"

        End If

        If Session("PENDIENTE") <> "SI" Then
            Session("PENDIENTE") = ""
        End If
        'Else
        '
        'End If

        Session("PENDIENTE") = ""

        'Se carga el listado de folios contables que se incluyen en el movimiento de efectivo
        If Session("ENTRADASALIDA") = "ENTRADA" Then
            Session("tabla_folios").Rows.Add(Session("IDCAJA_Tira"), Session("SERIE"), Session("FOLIO_IMP"), Session("MONTO_EFECTIVO"))
        Else
            Session("tabla_folios").Rows.Add(Session("IDCAJA_Tira"), Session("SERIE"), Session("FOLIO_IMP"), (Session("MONTO_EFECTIVO") * (-1)))
        End If

    End Sub

    Private Sub LlenaDenominacionUser()

        Dim DenominacionTipo(50) As String
        Dim ValorDenom(50) As Decimal
        Dim Cantidad(50) As Integer
        Dim i As Integer = 1
        Dim valor As Decimal
        Dim tipo As String

        DenominacionTipo = Session("DenominacionTipoUser")
        ValorDenom = Session("ValorDenom")
        Cantidad = Session("CantidadUser")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQTira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_Tira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIRAEFEC_DENOMINACIONES_USER"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF

                valor = Session("rs").Fields("VALOR").Value.ToString
                tipo = Session("rs").Fields("TIPO").Value.ToString

                ValorDenom(i) = valor
                DenominacionTipo(i) = (CStr(valor) + tipo)
                Cantidad(i) = Session("rs").Fields("CANTIDAD").Value.ToString

                i = i + 1
                Session("rs").movenext()
            Loop

        End If

        Session("Con").Close()

        Session("DenominacionTipoUser") = DenominacionTipo
        Session("ValorDenom") = ValorDenom
        Session("CantidadUser") = Cantidad

    End Sub

    Private Sub ActualizaSubMontos(ByVal NumID As String, ByVal SubMonto As Decimal, ByVal Cant As Integer)

        Dim Acumulados(50) As Decimal
        Dim CantAcumulados(50) As Integer

        Acumulados = Session("Acumulados")
        CantAcumulados = Session("CantAcumulados")

        Acumulados(CInt(NumID)) = SubMonto
        CantAcumulados(CInt(NumID)) = Cant

        Session("Acumulados") = Acumulados
        Session("CantAcumulados") = CantAcumulados

        ActualizaAcumulados()

    End Sub

    Private Sub ActualizaAcumulados()

        Dim Acumulados(50) As Decimal
        Dim i As Integer

        Acumulados = Session("Acumulados")
        Session("ACUMULADO") = 0.0

        For i = 1 To 16
            Session("ACUMULADO") = Session("ACUMULADO") + Acumulados(i)
        Next

        lbl_MontoAcum.Text = FormatCurrency(Session("ACUMULADO").ToString)
        lbl_MontoFalt.Text = FormatCurrency(Session("MONTO_EFECTIVO") - Session("ACUMULADO").ToString)

    End Sub

    Private Sub MontoOperacionesUnion(ByVal IndependUnion As Integer)

        Dim id_caja As Integer
        Dim serie_folio As String
        Dim folio_impreso As String
        Dim monto_folio As Decimal
        Dim registros As Integer
        Dim Fila As DataRow
        Dim Acumulado As Decimal
        Dim existeRegistro As Int16

        If IndependUnion = 1 Then
            'Se agregan folios unidos a la tabla que contiene los folios contables incluidos en el mov. de efectivo
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_Tira"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_TIRAEFEC_FOLIOS_OP_UNION"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").EOF Then
                Do While Not Session("rs").EOF

                    id_caja = Session("rs").Fields("ID_CAJA").Value.ToString
                    serie_folio = Session("rs").Fields("SERIE").Value.ToString
                    folio_impreso = Session("rs").Fields("FOLIO_IMP").Value.ToString
                    monto_folio = Session("rs").Fields("MONTO").Value.ToString
                    existeRegistro = 0

                    'Se carga el listado de folios contables que se incluyen en el movimiento de efectivo
                    existeRegistro = Convert.ToInt16(Session("tabla_folios").Compute("Count(FOLIO_IMP)", "SERIE = '" + serie_folio + "' AND FOLIO_IMP = '" + folio_impreso + "'"))

                    If existeRegistro = 0 Then
                        Session("tabla_folios").Rows.Add(id_caja, serie_folio, folio_impreso, monto_folio)
                    End If

                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
        Else
            'Se eliminan los registros
            registros = Session("tabla_folios").Rows.Count()
            For contador = (registros - 1) To 1 Step -1
                Session("tabla_folios").Rows(contador).Delete()
            Next
        End If

        'Se obtiene el acumulado
        Acumulado = 0
        registros = Session("tabla_folios").Rows.Count()
        For contador = 0 To (registros - 1) Step 1
            Fila = Session("tabla_folios").Rows(contador)
            Acumulado = Acumulado + Fila("MONTO")
        Next


        Session("MONTO_EFECTIVO") = Acumulado

        If Session("MONTO_EFECTIVO") < 0.0 Then
            Session("ENTRADASALIDA") = "SALIDA"
            Session("MONTO_EFECTIVO") = Session("MONTO_EFECTIVO") * (-1)
        Else
            Session("ENTRADASALIDA") = "ENTRADA"
        End If

        'Session("Con").Close()

        LlenaDenominacionUser()
        lbl_MontoReq.Text = FormatCurrency(Session("MONTO_EFECTIVO").ToString)
        lbl_EntradaSalida.Text = Session("ENTRADASALIDA").ToString

    End Sub

    Public Function VerificaDenominacionRestanteUser(ByVal CantSol As Integer, ByVal Denom As Decimal, ByVal Tipo As String) As Integer

        Dim RealizaCambio As Integer
        Dim DenominacionTipo(50) As String
        Dim Cantidad(50) As Integer
        Dim i As Integer
        Dim Aux As Integer

        RealizaCambio = 1
        DenominacionTipo = Session("DenominacionTipoUser")
        Cantidad = Session("CantidadUser")

        For i = 1 To 16
            If DenominacionTipo(i) = (CStr(Denom) + Tipo) Then
                Aux = i
            End If

            If CantSol > Cantidad(Aux) Then
                RealizaCambio = 0
            Else
                RealizaCambio = 1
            End If
        Next
        Return RealizaCambio

    End Function

    Protected Sub txt_Cant1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant1.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "1"
        If txt_Cant1.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant1.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant1.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant1.Text = cantidad
        txt_Mont1.Text = monto

        txt_Cant2.Focus()

    End Sub

    Protected Sub txt_Cant2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant2.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "2"

        If txt_Cant2.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant2.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant2.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant2.Text = cantidad
        txt_Mont2.Text = monto

        txt_Cant3.Focus()

    End Sub

    Protected Sub txt_Cant3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant3.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "3"

        If txt_Cant3.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant3.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant3.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant3.Text = cantidad
        txt_Mont3.Text = monto

        txt_Cant4.Focus()

    End Sub

    Protected Sub txt_Cant4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant4.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "4"

        If txt_Cant4.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant4.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant4.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant4.Text = cantidad
        txt_Mont4.Text = monto

        txt_Cant5.Focus()

    End Sub

    Protected Sub txt_Cant5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant5.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "5"

        If txt_Cant5.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant5.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant5.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant5.Text = cantidad
        txt_Mont5.Text = monto

        txt_Cant6.Focus()

    End Sub

    Protected Sub txt_Cant6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant6.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "6"

        If txt_Cant6.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant6.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant6.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant6.Text = cantidad
        txt_Mont6.Text = monto

        txt_Cant7.Focus()

    End Sub

    Protected Sub txt_Cant7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant7.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "7"

        If txt_Cant7.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant7.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant7.ToolTip

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant7.Text = cantidad
        txt_Mont7.Text = monto

        txt_Cant8.Focus()

    End Sub

    Protected Sub txt_Cant8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant8.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "8"

        If txt_Cant8.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant8.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant8.ToolTip


        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant8.Text = cantidad
        txt_Mont8.Text = monto

        txt_Cant9.Focus()

    End Sub

    Protected Sub txt_Cant9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant9.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "9"

        If txt_Cant9.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant9.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant9.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant9.Text = cantidad
        txt_Mont9.Text = monto

        txt_Cant10.Focus()

    End Sub

    Protected Sub txt_Cant10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant10.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "10"

        If txt_Cant10.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant10.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant10.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant10.Text = cantidad
        txt_Mont10.Text = monto

        txt_Cant11.Focus()

    End Sub

    Protected Sub txt_Cant11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant11.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "11"

        If txt_Cant11.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant11.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant11.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant11.Text = cantidad
        txt_Mont11.Text = monto

        txt_Cant12.Focus()

    End Sub

    Protected Sub txt_Cant12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant12.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "12"

        If txt_Cant12.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant12.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant12.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant12.Text = cantidad
        txt_Mont12.Text = monto

        txt_Cant13.Focus()

    End Sub

    Protected Sub txt_Cant13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant13.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "13"

        If txt_Cant13.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant13.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant13.ToolTip


        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant13.Text = cantidad
        txt_Mont13.Text = monto

        txt_Cant14.Focus()

    End Sub

    Protected Sub txt_Cant14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant14.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "14"

        If txt_Cant14.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant14.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant14.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant14.Text = cantidad
        txt_Mont14.Text = monto

        txt_Cant15.Focus()

    End Sub

    Protected Sub txt_Cant15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant15.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "15"

        If txt_Cant15.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant15.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant15.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant15.Text = cantidad
        txt_Mont15.Text = monto

        txt_Cant16.Focus()

    End Sub

    Protected Sub txt_Cant16_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Cant16.TextChanged

        Dim IDNum As String
        Dim cantidad As Integer
        Dim denom As Decimal
        Dim monto As Decimal
        Dim tipodenom As String
        Dim ValorDenom(50) As Decimal

        ValorDenom = Session("ValorDenom")

        IDNum = "16"

        If txt_Cant16.Text = "" Then
            cantidad = 0
        Else
            cantidad = CInt(txt_Cant16.Text)
        End If

        denom = ValorDenom(CInt(IDNum))
        tipodenom = txt_Cant16.ToolTip



        If Session("ENTRADASALIDA") = "SALIDA" Then
            If VerificaDenominacionRestanteUser(cantidad, denom, tipodenom) = 1 Then
                monto = cantidad * denom
                ActualizaSubMontos(IDNum, monto, cantidad)
            Else
                cantidad = monto / denom
            End If
        Else
            monto = cantidad * denom
            ActualizaSubMontos(IDNum, monto, cantidad)
        End If

        txt_Cant16.Text = cantidad
        txt_Mont16.Text = monto

        lnk_Confirmar.Focus()

    End Sub

    Protected Sub lnk_Limpia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Limpia.Click
        Limpia()
    End Sub

    Protected Sub lnk_Confirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Confirmar.Click

        Dim id_trans As Int64
        Dim SerieInterna As Integer
        SerieInterna = RevisaSerieInterna(Session("SERIE"))
        InsumosEfectivo()

        If Session("ENTRADASALIDA") = "SALIDA" Then
            If (SerieInterna = 1 And CDec(lbl_MontoFalt.Text) > 0) Or (SerieInterna = 0 And CDec(lbl_MontoFalt.Text) > Session("MARGEN_EFECTIVO")) Then
                lbl_Info.Text = "Error: No cumple con el monto total solicitado"
            ElseIf (SerieInterna = 1 And CDec(lbl_MontoFalt.Text) < 0) Then
                lbl_Info.Text = "Error: No cumple con el monto total solicitado"
            Else

                Session("FALTANTE_SOBRANTE") = CDec(lbl_MontoFalt.Text)

                If CDec(lbl_MontoFalt.Text) < CDec(Session("MARGEN_EFECTIVO")) * -1.0 Then
                    Session("CAMBIO") = "SI"
                    id_trans = ActualizarEfectivoUser()
                    Session("IDTRANSACCION") = id_trans
                    Session("ENTRADASALIDA") = "ENTRADA"
                    Session("MONTO_EFECTIVO") = CDec(lbl_MontoFalt.Text) * -1
                    Session("ACUMULADO") = Nothing
                    Session("UNIR") = Nothing
                    Session("FALTANTE_SOBRANTE") = Nothing
                    LimpiarTablas()
                    Response.Redirect("CRED_VEN_TIRAEFECTIVO.aspx")
                Else
                    Session("CAMBIO") = "NO"
                    ActualizarEfectivoUser()
                    Session("IDTRANSACCION") = Nothing
                    Session("MONTO_EFECTIVO") = Nothing
                    Session("ENTRADASALIDA") = Nothing
                    Session("ACUMULADO") = Nothing
                    Session("UNIR") = Nothing
                    Session("FALTANTE_SOBRANTE") = Nothing
                    Session("PBTIRA") = "1"
                    LimpiarTablas()
                    AlertasEfectivo()
                    Response.Write("<script language='javascript'> {if(window.opener.ActivatePostBack){window.opener.ActivatePostBack();}}</script>")
                    ClientScript.RegisterStartupScript(GetType(String), "Close", "window.close();", True)
                End If
            End If
        End If

        If Session("ENTRADASALIDA") = "ENTRADA" Then

            If Session("SERIE") = "CD" Then

                Session("FALTANTE_SOBRANTE") = 0.0

                If CDec(lbl_MontoFalt.Text) <> 0 Then
                    lbl_Info.Text = "Error: Aun no coincide el monto solicidato!"
                Else
                    Session("CAMBIO") = "SI"
                    id_trans = ActualizarEfectivoUser()
                    Session("IDTRANSACCION") = id_trans
                    Session("ENTRADASALIDA") = "SALIDA"
                    Session("ACUMULADO") = Nothing
                    Session("UNIR") = Nothing
                    Session("FALTANTE_SOBRANTE") = Nothing
                    LimpiarTablas()
                    Response.Redirect("CRED_VEN_TIRAEFECTIVO.aspx")
                End If

            Else
                If (SerieInterna = 1 And CDec(lbl_MontoFalt.Text) > 0) Or (SerieInterna = 0 And CDec(lbl_MontoFalt.Text) > Session("MARGEN_EFECTIVO")) Then
                    lbl_Info.Text = "Error: El monto a depositar es menor al monto total"
                ElseIf CDec(lbl_MontoFalt.Text) <= CDec(Session("MARGEN_EFECTIVO")) Then

                    Session("FALTANTE_SOBRANTE") = CDec(lbl_MontoFalt.Text)

                    If CDec(lbl_MontoFalt.Text) < CDec(Session("MARGEN_EFECTIVO")) * -1.0 Then
                        Session("CAMBIO") = "SI"
                        id_trans = ActualizarEfectivoUser()
                        Session("IDTRANSACCION") = id_trans
                        Session("ENTRADASALIDA") = "SALIDA"
                        Session("MONTO_EFECTIVO") = CDec(lbl_MontoFalt.Text) * -1
                        Session("ACUMULADO") = Nothing
                        Session("UNIR") = Nothing
                        Session("FALTANTE_SOBRANTE") = Nothing
                        LimpiarTablas()
                        Response.Redirect("CRED_VEN_TIRAEFECTIVO.aspx")
                    Else
                        Session("CAMBIO") = "NO"
                        ActualizarEfectivoUser()
                        Session("IDTRANSACCION") = Nothing
                        Session("MONTO_EFECTIVO") = Nothing
                        Session("ENTRADASALIDA") = Nothing
                        Session("ACUMULADO") = Nothing
                        Session("UNIR") = Nothing
                        Session("FALTANTE_SOBRANTE") = Nothing
                        Session("PBTIRA") = "1"
                        LimpiarTablas()
                        AlertasEfectivo()
                        Response.Write("<script language='javascript'> {if(window.opener.ActivatePostBack){window.opener.ActivatePostBack();}}</script>")
                        ClientScript.RegisterStartupScript(GetType(String), "Close", "window.close();", True)
                    End If
                End If
            End If
        End If


    End Sub

    Protected Sub lnk_Union_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Union.Click

        UnirOperaciones()
        Session("IDTRANSACCION") = Nothing
        Session("MONTO_EFECTIVO") = Nothing
        Session("ENTRADASALIDA") = Nothing
        Session("ACUMULADO") = Nothing
        Session("UNIR") = Nothing
        Session("IDTRANSACCION") = Nothing
        LimpiarTablas()

        AlertasEfectivo()

        Response.Write("<script language='javascript'> {if(window.opener.ActivatePostBack){window.opener.ActivatePostBack();}}</script>")
        ClientScript.RegisterStartupScript(GetType(String), "Close", "window.close();", True)
        'Response.Write("<script language='javascript'> {window.returnValue=""OK"";window.close();}</script>")

    End Sub

    Private Sub UnirOperaciones()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQTira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_Tira"))
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
        Session("cmd").CommandText = "UPD_TIRAEFEC_UNION_OPERACION"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Function ActualizarEfectivoUser() As Int64

        Dim i As Integer
        Dim ModDenominacion As New DataTable
        Dim DenominacionTipo(50) As String
        Dim CantAcumulados(50) As Integer
        Dim id_transaccion As Int64
        Dim FoliosEfectivo As New DataTable

        If Session("IDTRANSACCION") = Nothing Then
            id_transaccion = 0
        Else
            id_transaccion = Session("IDTRANSACCION")
        End If

        ModDenominacion.Columns.Add("DENOMTIPO", GetType(String))
        ModDenominacion.Columns.Add("CANTIDAD", GetType(Integer))

        FoliosEfectivo.Columns.Add("ID_CAJA", GetType(Integer))
        FoliosEfectivo.Columns.Add("SERIE", GetType(String))
        FoliosEfectivo.Columns.Add("FOLIO_IMP", GetType(String))
        FoliosEfectivo.Columns.Add("MONTO", GetType(Decimal))

        DenominacionTipo = Session("DenominacionTipoUser")
        CantAcumulados = Session("CantAcumulados")

        FoliosEfectivo = Session("tabla_folios")

        For i = 1 To 16
            ModDenominacion.Rows.Add(DenominacionTipo(i), CantAcumulados(i))
        Next

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            Dim insertCommand As New SqlCommand("UPD_TIRAEFEC_DENOMIACIONES_USER", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("ID_EQ", SqlDbType.VarChar)
            Session("parm").Value = Session("ID_EQTira")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDCAJA", SqlDbType.VarChar)
            Session("parm").Value = Session("IDCAJA_Tira")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("ENTRADASALIDA", SqlDbType.VarChar)
            Session("parm").Value = Session("ENTRADASALIDA")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("DENOM", SqlDbType.Structured)
            Session("parm").Value = ModDenominacion
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("FOLIOS", SqlDbType.Structured)
            Session("parm").Value = FoliosEfectivo
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("UNIR", SqlDbType.VarChar)
            Session("parm").Value = Session("CONFIRMA_UNION")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CAMBIO", SqlDbType.VarChar)
            Session("parm").Value = Session("CAMBIO")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
            Session("parm").Value = Session("Sesion")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("FALTANTE_SOBRANTE", SqlDbType.Decimal)
            Session("parm").Value = Session("FALTANTE_SOBRANTE")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("ID_TRANSACCION", SqlDbType.BigInt)
            Session("parm").Value = id_transaccion
            insertCommand.Parameters.Add(Session("parm"))

            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                id_transaccion = myReader.GetInt64(0)
            End While
            myReader.Close()

            'insertCommand.ExecuteNonQuery()
            'connection.Close()

        End Using

        If Session("SERIE") = "CI" Or Session("SERIE") = "TE" Then
            RecepcionTrasEfectivo(id_transaccion)
        End If

        Return id_transaccion

    End Function

    Protected Sub lnk_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Cancelar.Click

        BorrarOperacion()
        AlertasEfectivo()
        Session("MONTO_EFECTIVO") = Nothing
        Session("ENTRADASALIDA") = Nothing
        Response.Write("<script language='javascript'> {if(window.opener.ActivatePostBack){window.opener.ActivatePostBack();}}</script>")
        ClientScript.RegisterStartupScript(GetType(String), "Close", "window.close();", True)

    End Sub

    Private Sub BorrarOperacion()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQTira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_Tira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 15, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_OPERACION_EFECTIVO"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub rad_Union_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_Union.CheckedChanged

        Session("CONFIRMA_UNION") = "SI"
        MontoOperacionesUnion(1)
        ActualizaAcumulados()

    End Sub

    Protected Sub rad_Indepen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_Indepen.CheckedChanged

        Session("CONFIRMA_UNION") = "NO"
        MontoOperacionesUnion(0)
        ActualizaAcumulados()

    End Sub

    Private Sub RecepcionTrasEfectivo(ByVal id_trans As Int64)

        Dim i As Integer
        Dim ModDenominacion As New DataTable
        Dim DenominacionTipo(50) As String
        Dim CantAcumulados(50) As Integer

        ModDenominacion.Columns.Add("DENOMTIPO", GetType(String))
        ModDenominacion.Columns.Add("CANTIDAD", GetType(Integer))

        DenominacionTipo = Session("DenominacionTipoUser")
        CantAcumulados = Session("CantAcumulados")

        For i = 1 To 16
            ModDenominacion.Rows.Add(DenominacionTipo(i), CantAcumulados(i))
        Next

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            Dim insertCommand As New SqlCommand("UPD_CORTE_DENOM_CAJA", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("IDCAJA", SqlDbType.Int)
            Session("parm").Value = Session("CAJA_DEST")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("ENTRADASALIDA", SqlDbType.VarChar)
            Session("parm").Value = "ENTRADA"
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

            Session("parm") = New SqlParameter("ID_TRANSACCION", SqlDbType.BigInt)
            Session("parm").Value = id_trans
            insertCommand.Parameters.Add(Session("parm"))

            insertCommand.ExecuteNonQuery()
            connection.Close()

        End Using
        Session("ExitoCI") = 1
    End Sub

    Private Function RevisaSerieInterna(ByVal Serie As String) As Integer

        Dim SerieInterna As Integer

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 10, Serie)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REVISA_SERIE_INTERNA"
        Session("rs") = Session("cmd").Execute()

        SerieInterna = Session("rs").Fields("RESULTADO").Value.ToString()

        Session("Con").Close()

        Return SerieInterna

    End Function

    Private Sub InsumosEfectivo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSUMOS_TIRAEFECTIVO"
        Session("rs") = Session("cmd").Execute()

        Session("MARGEN_EFECTIVO") = Session("rs").Fields("MARGEN_EFECTIVO").Value.ToString()

        Session("Con").Close()

    End Sub

    '-------------------------------- REVISION DE EFECTIVO -----------------------------------------------------

    Public Sub AlertasEfectivo()

        Dim ESTATUS_ALERTA, CAJAF, IDCAJA As Integer
        Dim ALERTA, USUARIO, SUCURSAL As String
        Dim MONTO_CAJA, LIMITE As Decimal
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQTira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_Tira"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ALERTA_LIM_EFECTIVO"
        Session("rs") = Session("cmd").Execute()

        ESTATUS_ALERTA = Session("rs").Fields("ESTATUS_ALERTA").Value.ToString
        CAJAF = Session("rs").Fields("CAJAF").Value.ToString
        IDCAJA = Session("rs").Fields("IDCAJA").Value.ToString
        ALERTA = Session("rs").Fields("ALERTA").Value.ToString
        USUARIO = Session("rs").Fields("USUARIO").Value.ToString
        SUCURSAL = Session("rs").Fields("SUCURSAL").Value.ToString
        MONTO_CAJA = Session("rs").Fields("MONTO_CAJA").Value.ToString
        LIMITE = Session("rs").Fields("LIMITE").Value.ToString

        Session("Con").Close()

        If ESTATUS_ALERTA <> 0 Then

            '-------------------------------------- MANDO CORREO --------------------------------------
            Dim Email As String
            Email = ""

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CORREO_JEFE_SUCURSAL_X_SUC"
            Session("rs") = Session("cmd").Execute()

            Email = Session("rs").Fields("EMAIL").Value.ToString

            Session("Con").Close()
            'armado de html
            subject = "Alerta: Aviso de efectivo (" + SUCURSAL + ")."
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>A quien corresponda:  </td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td width='35%'>La caja: " + "<b>" + CStr(IDCAJA) + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='60%'>Usuario: " + "<b>" + USUARIO + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='80%'>Perteneciente a la sucursal: " + "<b>" + SUCURSAL + "</b>" + " ha alcanzado su limite: " + "<b>" + ALERTA + "</b>" + " de efectivo permitido  <b>(" + FormatCurrency(LIMITE) + ")</b> " + "</td></tr>")
            sbhtml.Append("<tr><td width='60%'>Teniendo un saldo de: " + "<b>" + FormatCurrency(MONTO_CAJA) + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='250'>Favor de tomar las acciones pertinentes.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            'envio de correo
            clase_Correo.Envio_email(sbhtml.ToString, subject, Email, cc)


        End If

    End Sub

End Class