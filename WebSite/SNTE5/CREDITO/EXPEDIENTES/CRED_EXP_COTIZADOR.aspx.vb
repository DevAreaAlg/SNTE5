Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_COTIZADOR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Cotizador", "Cotizador")

        If Not Me.IsPostBack Then
            'LLENO COMBO CON LOS TIPOS DE PLAN DE PAGO DISPONIBLES PARA EL PRODUCTO
            LlenarProductos()
        End If
    End Sub

    Private Sub LimpiaGenerales()
        lbl_lmonto.Text = "*Monto: "
        lbl_fechaliberacion.Text = "*Fecha pago:"
        txt_monto.Text = ""
        txt_fechaliberacion.Text = ""
        txt_plazo.Text = ""
        cmb_tipoplan.Items.Clear()
        lbl_status_general.Text = ""
    End Sub

    Private Sub LIMPIAVARIABLES()

        Session("IDPROD") = Nothing
        Session("TIPOPLAN") = Nothing
        Session("MONTO") = Nothing
        Session("TIPOTASA") = Nothing
        Session("TASA") = Nothing
        Session("IDINDICE") = Nothing
        Session("TIPOTASAMORA") = Nothing
        Session("TASAMORA") = Nothing
        Session("IDINDICEMORA") = Nothing
        Session("SUCURSAL") = Nothing
        Session("TIPOPERIODO") = Nothing
        Session("PERIODO") = Nothing
        Session("CADENA") = Nothing
        Session("FECHAPAGO") = Nothing
        Session("UNIDADPLAZO") = Nothing
        Session("PLAZO") = Nothing
        Session("CLASIFICACION") = Nothing
    End Sub

    Private Sub LlenarProductos()

        cmb_Producto.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_Producto.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRODUCTOS_ACTIVOS_COTIZADOR"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("MSTPRODUCTOS_NOMBRE").Value.ToString + " (" + Session("rs").Fields("DESTINO").Value.ToString + ")", Session("rs").Fields("MSTPRODUCTOS_ID_PROD").Value.ToString)
            cmb_Producto.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_Producto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Producto.SelectedIndexChanged

        If cmb_Producto.SelectedItem.Value.ToString <> 0 Then

            LimpiaGenerales()

            Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString
            Clasificacion(idProducto)
            Rangosmonto(idProducto)
            RangosPlazo(idProducto, "DIAS")
            Llenatasas(idProducto)

            Select Case Session("CLASIFICACION")
                Case "SIM"
                    lbl_tipoplan.Visible = True
                    cmb_tipoplan.Visible = True
                    lbl_fechaliberacion.Text = "*Fecha pago: "
                    lbl_lmonto.Text = "*Monto: "
                    pnl_arfin.Visible = False
                    pnl_srev.Visible = False
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    upd_especial.Visible = False
                    LlenaTipoPlan(idProducto)
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                    LLENAID()
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                    lnk_verplanespecial.Enabled = False
                Case "PESP"
                    lbl_tipoplan.Visible = True
                    cmb_tipoplan.Visible = True
                    lbl_fechaliberacion.Text = "*Fecha pago: "
                    lbl_lmonto.Text = "*Monto: "
                    pnl_arfin.Visible = False
                    pnl_srev.Visible = False
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    upd_especial.Visible = False
                    LlenaTipoPlan(idProducto)
                    LLENAID()
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                    lnk_verplanespecial.Enabled = False
                Case "SREV"
                    pnl_arfin.Visible = False
                    pnl_srev.Visible = True
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    upd_especial.Visible = False
                    lnk_generar_plan_pagos_srev.Visible = True
                    lbl_fechaliberacion.Text = "*Fecha pago: "
                    lbl_lmonto.Text = "*Línea de Préstamo: "
                    lbl_tipoplan.Visible = False
                    cmb_tipoplan.Visible = False

                    pnl_tasas_fija.Visible = False
                    pnl_tasas_indizadas.Visible = False

                    get_tasas_arfin()
                    get_periodicidad_SREV_CTAC()
                    LimpiaForma3()
                    pnl_Carta_credito.Visible = False
                    pnl_disp_srev.Visible = True

                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                Case "CTAC"
                    pnl_srev.Visible = True
                    pnl_arfin.Visible = False
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    upd_especial.Visible = False
                    lbl_fechaliberacion.Text = "*Fecha pago: "
                    lbl_lmonto.Text = "*Línea de Préstamo: "
                    lbl_tipoplan.Visible = False
                    cmb_tipoplan.Visible = False
                    lnk_generar_plan_pagos_srev.Visible = True
                    pnl_tasas_fija.Visible = False
                    pnl_tasas_indizadas.Visible = False
                    get_tasas_arfin()
                    get_periodicidad_SREV_CTAC()
                    LimpiaForma3()
                    pnl_Carta_credito.Visible = True
                    pnl_disp_srev.Visible = True
                    lbl_dias_pago_srev.Visible = False
                    lbl_pagomes_srev.Visible = False
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False

                Case "ARFIN"

                    upd_especial.Visible = False
                    lbl_tipoplan.Visible = False
                    cmb_tipoplan.Visible = False
                    pnl_arfin.Visible = True
                    lbl_fechaliberacion.Text = "*Fecha de inicio de Arrendamiento: "
                    pnl_srev.Visible = False
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    get_divisas()
                    get_tasas_arfin()
                    get_periodicidad_arfin()
                    LimpiaArrendamiento()
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                Case Else
                    pnl_srev.Visible = False
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    pnl_arfin.Visible = False
                    upd_especial.Visible = False
                    LimpiaGenerales()
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
            End Select


        Else
            pnl_srev.Visible = False
            upd_pnl_pfsi.Visible = False
            upd_pnl_si.Visible = False
            pnl_arfin.Visible = False
            LimpiaGenerales()
            lnk_eliminar.Enabled = False
            lnk_eliminar.Visible = False
        End If



    End Sub


    Private Sub Rangosmonto(ByVal idProducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_RANGOS_MONTOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            lbl_rango.Text = "(De: " + FormatCurrency(Session("rs").Fields("MINIMO").value.ToString) + " a: " + FormatCurrency(Session("rs").Fields("MAXIMO").value.ToString) + ")"

            Session("MONTOMIN") = Session("rs").Fields("MINIMO").value.ToString
            Session("MONTOMAX") = Session("rs").Fields("MAXIMO").value.ToString

        End If


        Session("Con").Close()


    End Sub

    Private Sub RangosPlazo(ByVal idProducto As Integer, ByVal unidadPlazo As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, unidadPlazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_PLAZOS_PRODUCTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            lbl_rangoPlazo.Text = "(De: " + Session("rs").Fields("MINIMO").value.ToString + " a: " + Session("rs").Fields("MAXIMO").value.ToString + " Días)"

            Session("PLAZOMIN") = Session("rs").Fields("MINIMO").value.ToString
            Session("PLAZOMAX") = Session("rs").Fields("MAXIMO").value.ToString

        End If
        Session("Con").Close()

    End Sub

    Private Sub LlenaTipoPlan(ByVal idProducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TIPOS_PLAN_PAGO_PRODUCTO"
        Session("rs") = Session("cmd").Execute()
        'SE MUESTRAN LOS TIPOS DE PLAN DE PAGOS DISPONIBLES
        cmb_tipoplan.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_tipoplan.Items.Add(elija)

        Do While Not Session("rs").eof
            Dim item As New ListItem(Session("rs").Fields("TIPOPAGO").Value.ToString, Session("rs").Fields("CVEPAGO").Value.ToString)
            cmb_tipoplan.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub Clasificacion(ByVal idProducto As Integer)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 20, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CLASIF_CRED"
        Session("rs") = Session("cmd").Execute()
        Session("CLASIFICACION") = Session("rs").Fields("CLAVE").Value.ToString
        Session("Con").Close()
    End Sub

    'Datos de cualquier plan (MIN Y MAX DE LOS INDICES Y TASAS)
    Private Sub InsumosPlan(ByVal idProducto As Integer)
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TASA_PERIODO_SI"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            'MIN Y MAXIMOS DE TASA NORMAL Y TASA MORATORIO
            Session("TASAMINNORFIJ") = Session("rs").fields("TASAMINNORFIJ").value.ToString
            Session("TASAMAXNORFIJ") = Session("rs").fields("TASAMAXNORFIJ").value.ToString
            Session("TASAMINMORFIJ") = Session("rs").fields("TASAMINMORFIJ").value.ToString
            Session("TASAMAXMORFIJ") = Session("rs").fields("TASAMAXMORFIJ").value.ToString
            Session("TASAMINNORIND") = Session("rs").fields("TASAMINNORIND").value.ToString
            Session("TASAMAXNORIND") = Session("rs").fields("TASAMAXNORIND").value.ToString
            Session("TASAMINMORIND") = Session("rs").fields("TASAMINMORIND").value.ToString
            Session("TASAMAXMORIND") = Session("rs").fields("TASAMAXMORIND").value.ToString

            Session("INDNORIND") = Session("rs").fields("INDNORIND").value.ToString
            Session("INDMORIND") = Session("rs").fields("INDMORIND").value.ToString

            '---------------INDICES NORMALES INDIZADOS--------------
            If Session("rs").fields("INDNORIND").value.ToString = "1" Then  'INDICE ES 1 ES UDI
                lbl_indicenorsi.Text = "UDI + "
                lbl_indice_tasa_nor_srev.Text = "UDI + "
                lbl_indicenormal.Text = "UDI + "
            End If
            If Session("rs").fields("INDNORIND").value.ToString = "2" Then  'INDICE ES 2 ES TIIE
                lbl_indicenorsi.Text = "TIIE 28 +"
                lbl_indice_tasa_nor_srev.Text = "TIIE 28 + "
                lbl_indicenormal.Text = "TIIE 28 + "
            End If

            If Session("rs").fields("INDNORIND").value.ToString = "3" Then  'INDICE ES 3 ES CETES
                lbl_indice_tasa_nor_srev.Text = "CETES 28 + "
                lbl_indicenorsi.Text = "CETES 28 + "
                lbl_indicenormal.Text = "CETES 28 + "
            End If

            '-------------INDICES MORATORIOS INDIZADOS---------------
            If Session("rs").fields("INDMORIND").value.ToString = "1" Then
                lbl_indice_tasa_mora_srev.Text = "UDI + "
                lbl_indicemorsi.Text = "UDI + "
                lbl_indicemora.Text = "UDI + "
            End If
            If Session("rs").fields("INDMORIND").value.ToString = "2" Then
                lbl_indice_tasa_mora_srev.Text = "TIIE 28 + "
                lbl_indicemorsi.Text = "TIIE 28 + "
                lbl_indicemora.Text = "TIIE 28 + "
            End If

            If Session("rs").fields("INDMORIND").value.ToString = "3" Then
                lbl_indice_tasa_mora_srev.Text = "CETES 28 + "
                lbl_indicemorsi.Text = "CETES 28 + "
                lbl_indicemora.Text = "CETES 28 + "
            End If


            lbl_tasanorpfsi.Text = "*Tasa Ordinaria (De" + Session("TASAMINNORFIJ") + "% a " + Session("TASAMAXNORFIJ") + "%):"
            lbl_tasamorpfsi.Text = "*Tasa Moratoria (De " + Session("TASAMINMORFIJ") + "% a " + Session("TASAMAXMORFIJ") + "%):"


        End If
        Session("Con").Close()

    End Sub

    'Habilita los Tabs de acuerdo al plan elegido
    Protected Sub cmb_tipoplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipoplan.SelectedIndexChanged

        lbl_status.Text = ""
        lbl_status_pfsi.Text = ""
        lbl_status_general.Text = ""

        Dim tipoPlan As String = cmb_tipoplan.SelectedItem.Value
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value
        Dim unidadPlazo As String = "DIAS"
        Dim plazo As String = txt_plazo.Text
        Dim fechaPago As String = txt_fechaliberacion.Text
        Dim monto As String = txt_monto.Text

        If fechaPago = "" Or monto = "" Or plazo = "" Or idProducto = "0" Or tipoPlan = "0" Then
            lbl_status_general.Text = "Error: Faltan datos por capturar"
            upd_pnl_si.Visible = False
            upd_pnl_pfsi.Visible = False
            upd_especial.Visible = False
            Exit Sub
        End If

        If validacionfecha(tipoPlan, fechaPago) Then

            Select Case tipoPlan
                Case "SI"

                    Periodicidad(idProducto, unidadPlazo, tipoPlan)
                    LimpiaForma()
                    upd_pnl_si.Visible = True
                    upd_pnl_pfsi.Visible = False
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                    upd_especial.Visible = False
                Case "PFSI"

                    InsumosPlan(idProducto)
                    Periodicidad(idProducto, unidadPlazo, tipoPlan)
                    LimpiaForma2()
                    upd_pnl_pfsi.Visible = True
                    upd_pnl_si.Visible = False
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                    lnk_verplanespecial.Enabled = False
                    upd_especial.Visible = False
                Case "ES"
                    Accordion1.Enabled = True
                    Accordion1.SelectedIndex = "0"
                    Periodicidad(idProducto, unidadPlazo, tipoPlan)
                    upd_pnl_si.Visible = False
                    upd_pnl_pfsi.Visible = False
                    upd_especial.Visible = True
                    FECHAMAX(CDec(plazo), unidadPlazo, fechaPago)
                    lnk_eliminar.Enabled = True
                    lnk_eliminar.Visible = True
                    AccordionPane1.Enabled = True
                    Accordion1.SelectedIndex = 0
                    lnk_verplanespecial.Enabled = False
                Case "0"
                    upd_pnl_pfsi.Visible = False
                    upd_pnl_si.Visible = False
                    upd_especial.Visible = False
                    lnk_eliminar.Enabled = False
                    lnk_eliminar.Visible = False
                    lnk_verplanespecial.Enabled = False
            End Select
        Else
            txt_fechaliberacion.Text = ""
            cmb_tipoplan.SelectedIndex = "0"
        End If



    End Sub

    'Combo de tasas normales
    Protected Sub cmb_tipotasasi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipotasasi.SelectedIndexChanged
        lbl_status.Text = ""
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString
        Select Case cmb_tipotasasi.SelectedItem.Value.ToString
            Case "FIJ"
                InsumosPlan(idProducto)
                lbl_indicenorsi.Visible = False
                lbl_tasasi.Text = "(De " + Session("TASAMINNORFIJ") + "% a " + Session("TASAMAXNORFIJ") + "%)"
                txt_tasasi.Enabled = True
                txt_tasasi.Text = ""

            Case "IND"
                InsumosPlan(idProducto)

                lbl_indicenorsi.Visible = True
                lbl_tasasi.Text = "Puntos (De " + Session("TASAMINNORIND") + "% a " + Session("TASAMAXNORIND") + "%)"
                txt_tasasi.Enabled = True
                txt_tasasi.Text = ""

            Case "0"
                lbl_tasasi.Text = "(Desde - % hasta - %)"
                lbl_indicenorsi.Text = ""
                lbl_indicenorsi.Visible = False
                txt_tasasi.Enabled = False
                txt_tasasi.Text = ""
        End Select
    End Sub

    ''Llena las tasas del plan especial
    Private Sub Llenatasas(ByVal idProducto As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")

        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_CLASIFICACION_TASAS"
        Session("rs") = Session("cmd").Execute()

        cmb_tipotasasi.Items.Clear()
        cmb_tipotasamorsi.Items.Clear()
        cmb_tipo_tasa_srev.Items.Clear()
        cmb_tasamora.Items.Clear()
        cmb_tasanormal.Items.Clear()


        Dim elija As New ListItem("ELIJA", "0")
        Dim elija2 As New ListItem("ELIJA", "0")

        cmb_tipotasamorsi.Items.Add(elija)
        cmb_tipotasasi.Items.Add(elija)
        cmb_tipo_tasa_srev.Items.Add(elija)
        cmb_tasamora.Items.Add(elija)
        cmb_tasanormal.Items.Add(elija)



        Do While Not Session("rs").eof

            If Session("rs").Fields("TIPO").Value.ToString = "NOR" Then 'normal
                If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                    Dim item As New ListItem("FIJA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                    cmb_tipotasasi.Items.Add(item)
                    cmb_tipo_tasa_srev.Items.Add(item)
                    cmb_tasanormal.Items.Add(item)
                    Session("VARIABLE") = Session("rs").Fields("VARIABLE").Value.ToString
                Else
                    Dim item As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                    cmb_tipotasasi.Items.Add(item)
                    cmb_tipo_tasa_srev.Items.Add(item)
                    cmb_tasanormal.Items.Add(item)
                    Session("VARIABLEIND") = Session("rs").Fields("VARIABLE").value.ToString
                End If
                If Not Session("rs").EOF() Then
                    Session("VARIABLEIND") = Session("rs").Fields("VARIABLE").value.ToString
                End If
            Else 'moratorio
                If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                    Dim itemmor As New ListItem("FIJA", Session("rs").Fields("CLASIFICACION").value.ToString)
                    cmb_tasamora.Items.Add(itemmor)
                    cmb_tipotasamorsi.Items.Add(itemmor)
                    Session("VARIABLEMORA") = Session("rs").Fields("VARIABLE").Value.ToString

                Else
                    Dim itemmor As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                    cmb_tasamora.Items.Add(itemmor)
                    cmb_tipotasamorsi.Items.Add(itemmor)

                    Session("VARIABLEINDMORA") = Session("rs").Fields("VARIABLE").Value.ToString
                End If
                If Not Session("rs").EOF() Then
                    Session("VARIABLEINDMORA") = Session("rs").Fields("VARIABLE").value.ToString
                End If

            End If
            Session("rs").movenext()
        Loop


        Session("Con").Close()
    End Sub

    Private Sub Periodicidad(ByVal idProducto As Integer, ByVal UnidadPlazo As String, ByVal tipoPlan As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, UnidadPlazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPLAN", Session("adVarChar"), Session("adParamInput"), 10, tipoPlan)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_PERIODICIDAD"
        Session("rs") = Session("cmd").Execute()

        cmb_tipopersi.Items.Clear()
        cmb_tipoperiodicidad.Items.Clear()
        cmb_pagorecu.Items.Clear()
        cmb_tiporecurrencia.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_tipopersi.Items.Add(elija)
        cmb_tipoperiodicidad.Items.Add(elija)
        cmb_pagorecu.Items.Add(elija)
        cmb_tiporecurrencia.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("TIPOPERIODO").Value.ToString)
            cmb_tipopersi.Items.Add(item)
            cmb_tipoperiodicidad.Items.Add(item)
            cmb_pagorecu.Items.Add(item)
            cmb_tiporecurrencia.Items.Add(item)
            Session("rs").movenext()
        Loop


        Session("Con").Close()
    End Sub

    'Llena los dias a elegir del plan especial
    Private Sub LlenaCmbXdia()

        cmb_per.Items.Clear()
        cmb_periodicidad.Items.Clear()
        cmb_periodicidadSI.Items.Clear()
        Dim z As String
        'Ciclo que me permite agregar al combo del 1 al 31
        For count = 1 To 28

            z = "DIA " + count.ToString + " DEL MES"
            Dim y As New ListItem(z, count.ToString)
            cmb_periodicidad.Items.Add(y)
            cmb_periodicidadSI.Items.Add(y)
            cmb_per.Items.Add(y)
        Next
        'Agrega al combo el mensaje Dia ultimo de mes
        Dim ultimo As New ListItem("DIA ULTIMO DE MES", 32)
        cmb_periodicidad.Items.Add(ultimo)
        cmb_periodicidadSI.Items.Add(ultimo)
        cmb_per.Items.Add(ultimo)
        'Si la sesion dias pago no esta vacia quito el dia del combo
        If Session("DIASPAGO").ToString.Length <> 0 Then

            Dim arre = Split(Session("DIASPAGO").ToString, "-")
            Dim Contadornumeros As Integer = 0

            Do While arre(Contadornumeros) <> Nothing
                'Permite remover del combo el index seleccionado del combo de periodicidades encontrando primero el valor y posteriormente su index
                cmb_periodicidad.Items.RemoveAt(cmb_periodicidad.Items.IndexOf(cmb_periodicidad.Items.FindByValue(arre(Contadornumeros).ToString)))
                cmb_periodicidadSI.Items.RemoveAt(cmb_periodicidadSI.Items.IndexOf(cmb_periodicidadSI.Items.FindByValue(arre(Contadornumeros).ToString)))
                cmb_per.Items.RemoveAt(cmb_per.Items.IndexOf(cmb_per.Items.FindByValue(arre(Contadornumeros).ToString)))

                'Incremento variable
                Contadornumeros = Contadornumeros + 1
            Loop


        End If

    End Sub




#Region "VALIDACIONES_GENERALES"
    'Valida que la fecha de pago sea menor a la fecha del sistema
    Public Function validacionfecha(ByVal tipoPlan As String, ByVal fechaPago As String) As Boolean
        lbl_status.Text = ""
        lbl_status_pfsi.Text = ""
        lbl_status_general.Text = ""
        Dim resp As Boolean = True

        If tipoPlan = "PFSI" Then
            If CDate(fechaPago) < CDate(Session("FechaSis")) Then
                lbl_status_pfsi.Text = "Error:La fecha de pago del préstamo no puede ser anterior a la fecha del sistema"
                resp = False
            End If
        End If

        If tipoPlan = "SI" Then

            If CDate(fechaPago) < CDate(Session("FechaSis")) Then
                lbl_status.Text = "Error:La fecha de pago del préstamo no puede ser anterior a la fecha del sistema"
                resp = False
            End If
        End If

        If tipoPlan = "ES" Then

            If CDate(fechaPago) < CDate(Session("FechaSis")) Then
                lbl_status_general.Text = "Error:La fecha de pago del préstamo no puede ser anterior a la fecha del sistema"
                resp = False
            End If
        End If

        Return resp
    End Function

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Function Validatasa(ByVal tasa As String) As Boolean
        Return Regex.IsMatch(tasa, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

#End Region



#Region "PAGOS FIJOS"
    '----------------CODIGO DE PLAN DE PAGOS FIJOS---------------------------------

    'Datos del plan de Pagos Fijos Saldos insolutos
    Private Sub InsumosPFSI(ByVal idproducto As Integer, ByVal tipoPeriodicidad As String)
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, tipoPeriodicidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TASA_PERIODO_PFSI"
        Session("rs") = Session("cmd").Execute()
        cmb_per.Items.Clear()
        cmb_periodicidad.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_periodicidad.Items.Add(elija)
        cmb_per.Items.Add(elija)
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").value.ToString + " " + Session("rs").Fields("UNIDAD").Value.ToString, Session("rs").Fields("PERIODO").Value.ToString)
            cmb_periodicidad.Items.Add(item)
            cmb_per.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'De acuerdo al tipo de periodicidad muestra en el cmb_periodicidad las periodicidades permitidas
    Protected Sub cmb_tipoperiodicidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipoperiodicidad.SelectedIndexChanged
        lbl_status_pfsi.Text = ""
        lbl_errortasanorpfsi.Text = ""
        lbl_errortasamorpfsi.Text = ""

        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString
        Dim tipoPeriodicidad As String = cmb_tipoperiodicidad.SelectedItem.Value

        InsumosPFSI(idProducto, tipoPeriodicidad)

        'Agrega al combo de Periodicidad los periodos permitidos según sea el Caso
        Select Case tipoPeriodicidad

            Case "NDIA"
                lnk_generar0.Visible = False
                lbl_periodos.Visible = False
                lbl_diaspago.Text = ""
                lnk_generar0.Visible = False


            Case "NSEM"
                lnk_generar0.Visible = False
                lbl_periodos.Visible = False
                lbl_diaspago.Text = ""

            Case "NMES"
                lnk_generar0.Visible = False
                lbl_periodos.Visible = False
                lbl_diaspago.Text = ""


            Case "DIAX" 'Número de pagos en el mes

                lbl_periodos.Visible = True
                lnk_generar0.Visible = True
                lbl_diaspago.Visible = True
                lbl_diaspago.Text = ""
                Session("diaspago") = ""

                'obtengo el maximo de dias recurrentes a asignar en el mes
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_COTIZADOR_MAX_RECURRENCIA"
                Session("rs") = Session("cmd").Execute()
                Session("maxdiasmes") = Session("rs").fields("MAXIMO").value.ToString
                Session("Con").close()
                Session("cuentadiasx") = 0
                'lleno el combo con el numero de dias en el mes
                LlenaCmbXdia()




        End Select

    End Sub

    'Verifica que la tasa y el periodo esté en el límite establecido
    Public Function validacion(ByVal TasaNorPfsi As String, ByVal TasaMorPfsi As String) As Integer

        lbl_errortasanorpfsi.Text = ""
        lbl_errortasamorpfsi.Text = ""
        Dim COUNT As Integer
        COUNT = 1


        If CDec(TasaNorPfsi) < CDec(Session("TASAMINNORFIJ").ToString) Or CDec(TasaNorPfsi) > CDec(Session("TASAMAXNORFIJ").ToString) Then
            lbl_errortasanorpfsi.Text = "Error: La tasa ordinaria no está en los límites establecidos "
            COUNT = 0
        End If

        If CDec(TasaMorPfsi) < CDec(Session("TASAMINMORFIJ").ToString) Or CDec(TasaMorPfsi) > CDec(Session("TASAMAXMORFIJ").ToString) Then
            lbl_errortasamorpfsi.Text = "Error: La tasa moratoria no está en los límites establecidos "
            COUNT = 0
        End If

        Return COUNT

    End Function

    Private Sub LimpiaForma2()

        txt_tasanorpfsi.Text = ""
        cmb_tipoperiodicidad.SelectedIndex = "0"
        cmb_periodicidad.Items.Clear()

        lbl_errortasanorpfsi.Text = ""
        lbl_errortasamorpfsi.Text = ""
        txt_tasamorpfsi.Text = ""

        lnk_generar0.Visible = False
        lbl_periodos.Visible = False
        lbl_diaspago.Text = ""
    End Sub

    Protected Sub lnk_generar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar0.Click
        lbl_status_pfsi.Text = ""

        If CInt(Session("CUENTADIASX")) < CInt(Session("MAXDIASMES")) Then

            Session("DIASPAGO") = Session("DIASPAGO").ToString + cmb_periodicidad.SelectedItem.Value.ToString + "-"

            If lbl_diaspago.Text.Length = 0 Then 'La primera vez que agrega un dia del mes
                If cmb_periodicidad.SelectedItem.Value.ToString = "32" Then
                    lbl_diaspago.Text = "ULTIMO DIA"
                Else
                    lbl_diaspago.Text = cmb_periodicidad.SelectedItem.Value.ToString
                End If

            Else

                If cmb_periodicidad.SelectedItem.Value.ToString = "32" Then
                    lbl_diaspago.Text = lbl_diaspago.Text + "," + "ULTIMO DIA"
                Else
                    lbl_diaspago.Text = lbl_diaspago.Text + "," + cmb_periodicidad.SelectedItem.Value.ToString
                End If



            End If
            'Se llena el combo de dias del mes
            LlenaCmbXdia()

            Session("CUENTADIASX") = CInt(Session("CUENTADIASX")) + 1

        Else 'ya no permito
            lbl_status_pfsi.Text = "Error: Ya no puede agregar más pagos recurrentes"

        End If
    End Sub

    'Llama las funciones validación y generarImprimirPlanPagoFijo
    Protected Sub lnk_generar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar.Click

        lbl_status_pfsi.Text = ""

        Dim count As Integer
        Dim monto As String
        Dim tipoTasa As String
        Dim tasa As Decimal
        Dim idIndice As Integer
        Dim idproducto As Integer
        Dim tipoPlan As String

        Dim tipoTasaMora As String
        Dim tasaMora As Decimal
        Dim idIndiceMora As Integer
        Dim idSuc As Integer
        Dim tipoPeriodo As String
        Dim periodo As Integer
        Dim cadena As String
        Dim fechaPago As String
        Dim plazo As String
        Dim unidadPlazo As String
        Dim rango As Integer = 0
        fechaPago = txt_fechaliberacion.Text
        plazo = txt_plazo.Text
        unidadPlazo = "DIAS"

        tipoPeriodo = cmb_tipoperiodicidad.SelectedItem.Value.ToString
        If tipoPeriodo = "DIAX" Then
            periodo = 0
            cadena = Session("diaspago")
        Else
            periodo = cmb_periodicidad.SelectedItem.Value
            cadena = ""
        End If

        tipoTasaMora = "FIJ"

        If tipoTasaMora = "FIJ" Then
            tasaMora = txt_tasamorpfsi.Text
            idIndiceMora = 0
        Else
            tasaMora = txt_tasamorpfsi.Text
            idIndiceMora = 0
        End If

        tipoPlan = "PFSI"
        idproducto = cmb_Producto.SelectedItem.Value.ToString
        monto = txt_monto.Text
        tipoTasa = "FIJ"
        idSuc = CInt(Session("SUCID"))

        If tipoTasa = "FIJ" Then
            tasa = txt_tasanorpfsi.Text
            idIndice = 0
        Else
            tasa = txt_tasanorpfsi.Text
            idIndice = 0
        End If

        If plazo = "" Or monto = "" Or fechaPago = "" Then
            lbl_status_pfsi.Text = "Error: Capture plazo, monto y fecha pago del préstamo"
            Exit Sub
        End If


        If tipoPeriodo = "DIAX" And lbl_diaspago.Text = "" Then
            lbl_status_pfsi.Text = "Error: Capture días de pago en el mes"
        Else
            If Validamonto(monto) = True Then
                If CDec(monto) >= CDec(Session("MONTOMIN").ToString) And CDec(monto) <= CDec(Session("MONTOMAX").ToString) Then
                    If CInt(plazo) >= CInt(Session("PLAZOMIN").ToString) And CInt(plazo) <= CInt(Session("PLAZOMAX").ToString) Then

                        If Validatasa(tasa) = True And Validatasa(tasaMora) = True Then

                            count = validacion(tasa, tasaMora)

                            If count = 1 And validacionfecha(tipoPlan, fechaPago) = True Then


                                Session("IDPROD") = idproducto
                                Session("TIPOPLAN") = tipoPlan
                                Session("MONTO") = monto
                                Session("TIPOTASA") = tipoTasa
                                Session("TASA") = tasa
                                Session("IDINDICE") = idIndice
                                Session("TIPOTASAMORA") = tipoTasaMora
                                Session("TASAMORA") = tasaMora
                                Session("IDINDICEMORA") = idIndiceMora
                                Session("SUCURSAL") = idSuc
                                Session("TIPOPERIODO") = tipoPeriodo
                                Session("PERIODO") = periodo
                                Session("CADENA") = cadena
                                Session("FECHAPAGO") = fechaPago
                                Session("UNIDADPLAZO") = unidadPlazo
                                Session("PLAZO") = plazo
                                Session("RANGO") = rango
                                Session("ID") = 0
                                Response.Redirect("CRED_EXP_COTIZADOR_PLAN.ASPX")

                            End If

                        Else
                            lbl_status_pfsi.Text = "Error: Verifique la tasa"
                        End If
                    Else
                        lbl_status_pfsi.Text = "Error: Plazo fuera de los limites establecidos"
                    End If
                Else
                    lbl_status_pfsi.Text = "Error: Monto fuera de los limites establecidos"
                End If
            Else
                lbl_status_pfsi.Text = "Error: Monto incorrecto"
            End If

        End If


    End Sub


#End Region



#Region "SaldosInsolutos"

    Private Sub InsumosSI(ByVal idProducto As Integer, ByVal tipoPeriodicidad As String)

        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, tipoPeriodicidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_PERIODICIDAD_SI"
        Session("rs") = Session("cmd").Execute()
        cmb_periodicidadSI.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_periodicidadSI.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").value.ToString + " " + Session("rs").Fields("UNIDAD").Value.ToString, Session("rs").Fields("PERIODO").Value.ToString)
            cmb_periodicidadSI.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()


    End Sub

    'Muestra los diferentes periodos que hay de acuerdo al tipo de periodicidad 
    Protected Sub cmb_tipopersi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipopersi.SelectedIndexChanged

        Dim tipoPeriodicidad As String = cmb_tipopersi.SelectedItem.Value.ToString
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString

        InsumosSI(idProducto, tipoPeriodicidad) 'Obtiene los numeros de periodicidad de acuerdo a los configurados

        lbl_status.Text = ""
        lbl_errortasasi.Text = ""
        lbl_errortasamorsi.Text = ""
        'Agrega al combo de Periodicidad los periodos permitidos según sea el Caso
        Select Case cmb_tipopersi.SelectedItem.Value

            Case "NDIA"
                lnk_generar1.Visible = False
                lbl_periodos_SI.Visible = False
                lbl_diaspago_SI.Text = ""
                lnk_generar0.Visible = False


            Case "NSEM"
                lnk_generar1.Visible = False
                lbl_periodos_SI.Visible = False
                lbl_diaspago_SI.Text = ""

            Case "NMES"
                lnk_generar1.Visible = False
                lbl_periodos_SI.Visible = False
                lbl_diaspago_SI.Text = ""


            Case "DIAX" 'Número de pagos en el mes

                lbl_periodos_SI.Visible = True
                lnk_generar1.Visible = True
                lbl_diaspago_SI.Visible = True
                lbl_diaspago_SI.Text = ""
                Session("diaspago") = ""

                'obtengo el maximo de dias recurrentes a asignar en el mes
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_COTIZADOR_MAX_RECURRENCIA"
                Session("rs") = Session("cmd").Execute()
                Session("maxdiasmes") = Session("rs").fields("MAXIMO").value.ToString
                Session("Con").close()
                Session("cuentadiasx") = 0
                'lleno el combo con el numero de dias en el mes
                LlenaCmbXdia()



        End Select




    End Sub

    'combo de tasas moratorias
    Protected Sub cmb_tipotasamorsi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipotasamorsi.SelectedIndexChanged
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString

        Select Case cmb_tipotasamorsi.SelectedItem.Value.ToString
            Case "FIJ"
                InsumosPlan(idProducto)

                lbl_indicemorsi.Visible = False
                lbl_tasamorsi.Text = "(De " + Session("TASAMINMORFIJ") + "% a " + Session("TASAMAXMORFIJ") + "%)"
                txt_tasamorsi.Enabled = True
                txt_tasamorsi.Text = ""
            Case "IND"

                InsumosPlan(idProducto)
                lbl_indicemorsi.Visible = True
                lbl_tasamorsi.Text = "Puntos (De " + Session("TASAMINMORIND") + "% a " + Session("TASAMAXMORIND") + "%)"
                txt_tasamorsi.Enabled = True
                txt_tasamorsi.Text = ""

            Case "0"
                lbl_tasamorsi.Text = "(Desde - % hasta - %)"
                lbl_indicemorsi.Text = ""
                lbl_indicemorsi.Visible = False
                txt_tasamorsi.Enabled = False
                txt_tasamorsi.Text = ""


        End Select

    End Sub

    'Verifica que la tasa y el periodo esté en el límite establecido
    Public Function validacionSI(ByVal tipotasasi As String, ByVal tasaSi As String, ByVal tipotasamorsi As String, tasamorsi As String) As Integer

        lbl_errortasasi.Text = ""
        lbl_errortasamorsi.Text = ""
        Dim COUNT As Integer
        COUNT = 1

        '---------------VALIDACION DEL COMBO DE TASA NORMAL-----------------
        If tipotasasi = "FIJ" Then

            If CDec(tasaSi) < CDec(Session("TASAMINNORFIJ").ToString) Or CDec(tasaSi) > CDec(Session("TASAMAXNORFIJ").ToString) Then
                lbl_errortasasi.Text = "Error: La tasa ordinaria no está en los límites establecidos "
                COUNT = 0
            End If
        Else

            If CDec(tasaSi) < CDec(Session("TASAMINNORIND").ToString) Or CDec(tasaSi) > CDec(Session("TASAMAXNORIND").ToString) Then
                lbl_errortasasi.Text = "Error: Los puntos % de la tasa ordinaria no están en los límites establecidos "
                COUNT = 0
            End If

        End If

        '-------------------VALIDACION DEL COMBO DE TASA MORATORIA-------------------

        If tipotasamorsi = "FIJ" Then

            If CDec(tasamorsi) < CDec(Session("TASAMINMORFIJ").ToString) Or CDec(tasamorsi) > CDec(Session("TASAMAXMORFIJ").ToString) Then
                lbl_errortasamorsi.Text = "Error: La tasa moratoria no está en los límites establecidos "
                COUNT = 0
            End If

        Else

            If CDec(tasamorsi) < CDec(Session("TASAMINMORIND").ToString) Or CDec(tasamorsi) > CDec(Session("TASAMAXMORIND").ToString) Then
                lbl_errortasamorsi.Text = "Error: Los puntos % de la tasa moratoria no están en los límites establecidos "
                COUNT = 0
            End If

        End If

        Return COUNT
    End Function

    'Agregar dia X
    Protected Sub lnk_generar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar1.Click

        lbl_status.Text = ""

        If CInt(Session("CUENTADIASX")) < CInt(Session("MAXDIASMES")) Then

            Session("DIASPAGO") = Session("DIASPAGO").ToString + cmb_periodicidadSI.SelectedItem.Value.ToString + "-"

            If lbl_diaspago_SI.Text.Length = 0 Then 'La primera vez que agrega un dia del mes
                If cmb_periodicidadSI.SelectedItem.Value.ToString = "32" Then
                    lbl_diaspago_SI.Text = "ULTIMO DIA"
                Else
                    lbl_diaspago_SI.Text = cmb_periodicidadSI.SelectedItem.Value.ToString
                End If

            Else

                If cmb_periodicidadSI.SelectedItem.Value.ToString = "32" Then
                    lbl_diaspago_SI.Text = lbl_diaspago_SI.Text + "," + "ULTIMO DIA"
                Else
                    lbl_diaspago_SI.Text = lbl_diaspago_SI.Text + "," + cmb_periodicidadSI.SelectedItem.Value.ToString
                End If



            End If
            'Se llena el combo de dias del mes
            LlenaCmbXdia()

            Session("CUENTADIASX") = CInt(Session("CUENTADIASX")) + 1

        Else 'ya no permito
            lbl_status.Text = "Error: Ya no puede agregar más pagos recurrentes"

        End If
    End Sub

    Protected Sub lnk_generar_si0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar_si0.Click
        Dim count As Integer
        Dim monto As String
        Dim tipoTasa As String
        Dim tasa As Decimal
        Dim idIndice As Integer
        Dim idproducto As Integer
        Dim tipoPlan As String

        Dim tipoTasaMora As String
        Dim tasaMora As Decimal
        Dim idIndiceMora As Integer
        Dim idSuc As Integer
        Dim tipoPeriodo As String
        Dim periodo As Integer
        Dim cadena As String
        Dim fechaPago As String
        Dim plazo As String
        Dim unidadPlazo As String
        Dim rango As Integer = 0

        idproducto = cmb_Producto.SelectedItem.Value.ToString

        InsumosPlan(idproducto)


        fechaPago = txt_fechaliberacion.Text
        plazo = txt_plazo.Text
        unidadPlazo = "DIAS"

        tipoPeriodo = cmb_tipopersi.SelectedItem.Value
        If tipoPeriodo = "DIAX" Then
            periodo = 0
            cadena = Session("diaspago")
        Else
            periodo = cmb_periodicidadSI.SelectedItem.Value
            cadena = ""
        End If

        tipoTasaMora = cmb_tipotasamorsi.SelectedItem.Value.ToString


        If tipoTasaMora = "FIJ" Then
            tasaMora = txt_tasamorsi.Text
            idIndiceMora = 0
        Else
            tasaMora = txt_tasamorsi.Text
            idIndiceMora = Session("INDMORIND").ToString
        End If

        tipoPlan = "SI"
        monto = txt_monto.Text
        tipoTasa = cmb_tipotasasi.SelectedItem.Value.ToString
        idSuc = CInt(Session("SUCID"))

        If tipoTasa = "FIJ" Then
            tasa = txt_tasasi.Text
            idIndice = 0
        Else
            tasa = txt_tasasi.Text
            idIndice = Session("INDNORIND").ToString
        End If


        If plazo = "" Or monto = "" Or fechaPago = "" Then
            lbl_status.Text = "Error: Capture plazo, monto y fecha pago del préstamo"
            Exit Sub
        End If



        If tipoPeriodo = "DIAX" And lbl_diaspago_SI.Text = "" Then
            lbl_status.Text = "Error: Capture días de pago en el mes"
        Else
            If Validamonto(monto) = True Then
                If CDec(monto) >= CDec(Session("MONTOMIN").ToString) And CDec(monto) <= CDec(Session("MONTOMAX").ToString) Then
                    If CInt(plazo) >= CInt(Session("PLAZOMIN").ToString) And CInt(plazo) <= CInt(Session("PLAZOMAX").ToString) Then

                        If Validatasa(tasa) = True And Validatasa(tasaMora) = True Then

                            count = validacionSI(tipoTasa, tasa, tipoTasaMora, tasaMora) 'Primero Verifica que la tasa y la periodicidad este en el margen establecido

                            If count = 1 And validacionfecha(tipoPlan, fechaPago) = True Then


                                Session("IDPROD") = idproducto
                                Session("TIPOPLAN") = tipoPlan
                                Session("MONTO") = monto
                                Session("TIPOTASA") = tipoTasa
                                Session("TASA") = tasa
                                Session("IDINDICE") = idIndice
                                Session("TIPOTASAMORA") = tipoTasaMora
                                Session("TASAMORA") = tasaMora
                                Session("IDINDICEMORA") = idIndiceMora
                                Session("SUCURSAL") = idSuc
                                Session("TIPOPERIODO") = tipoPeriodo
                                Session("PERIODO") = periodo
                                Session("CADENA") = cadena
                                Session("FECHAPAGO") = fechaPago
                                Session("UNIDADPLAZO") = unidadPlazo
                                Session("PLAZO") = plazo
                                Session("RANGO") = rango
                                Session("ID") = 0
                                Response.Redirect("CRED_EXP_COTIZADOR_PLAN.ASPX")

                            End If

                        Else
                            lbl_status.Text = "Error: Verifique la tasa"
                        End If
                    Else
                        lbl_status.Text = "Error: Plazo fuera de los limites establecidos"
                    End If
                Else
                    lbl_status.Text = "Error: Monto fuera de los limites establecidos"
                End If
            Else
                lbl_status.Text = "Error: Monto incorrecto"
            End If

        End If

    End Sub

    Private Sub LimpiaForma()
        cmb_tipotasasi.SelectedIndex = "0"
        cmb_tipopersi.SelectedIndex = "0"
        cmb_periodicidadSI.Items.Clear()
        cmb_tipotasamorsi.SelectedIndex = "0"

        lbl_errortasamorsi.Text = ""
        lbl_tasasi.Text = "(Desde - % hasta - %)"
        lbl_indicenorsi.Text = ""
        lbl_indicenorsi.Visible = False
        txt_tasasi.Enabled = False
        txt_tasasi.Text = ""

        lbl_tasamorsi.Text = "(Desde - % hasta - %)"
        lbl_indicemorsi.Text = ""
        lbl_indicemorsi.Visible = False
        txt_tasamorsi.Enabled = False
        txt_tasamorsi.Text = ""

        lnk_generar1.Visible = False
        lbl_diaspago_SI.Text = ""
        lbl_periodos_SI.Visible = False


    End Sub
#End Region

#Region "ARRENDAMIENTO FINANCIERO"

    Private Sub get_divisas()
        cmb_divisa.Items.Clear()
        cmb_divisa.Items.Add(New ListItem("ELIJA", "0"))

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_DIVISAS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            cmb_divisa.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("RELPRODDIVISA_ID_DIVISA").Value.ToString))

            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub get_tasas_arfin()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TASA_PERIODO_SI"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            'MIN Y MAXIMOS DE TASA NORMAL Y TASA MORATORIO
            Session("TASAMINNORFIJ") = Session("rs").fields("TASAMINNORFIJ").value.ToString
            Session("TASAMAXNORFIJ") = Session("rs").fields("TASAMAXNORFIJ").value.ToString

            Session("TASAMINMORFIJ") = Session("rs").fields("TASAMINMORFIJ").value.ToString
            Session("TASAMAXMORFIJ") = Session("rs").fields("TASAMAXMORFIJ").value.ToString

            Session("TASAMINMORIND") = Session("rs").fields("TASAMINMORIND").value.ToString 'TASAMINMORIND	TASAMAXMORIND
            Session("TASAMAXMORIND") = Session("rs").fields("TASAMAXMORIND").value.ToString

            Session("TASAMINNORIND") = Session("rs").fields("TASAMINNORIND").value.ToString
            Session("TASAMAXNORIND") = Session("rs").fields("TASAMAXNORIND").value.ToString

            Session("TASAMINMORFIJ") = Session("rs").fields("TASAMINMORFIJ").value.ToString
            Session("TASAMAXMORFIJ") = Session("rs").fields("TASAMAXMORFIJ").value.ToString 'TASAMAXMORIND TASAMINMORIND

            Session("INDNORIND") = Session("rs").fields("INDNORIND").value.ToString


            lbl_tasa_ord.Text = "*Tasa Normal (De " + Session("TASAMINNORFIJ") + "% a " + Session("TASAMAXNORFIJ") + "%):"
            lbl_tasa_mor.Text = "*Tasa Moratoria (De " + Session("TASAMINMORFIJ") + "% a " + Session("TASAMAXMORFIJ") + "%):"
            lbl_tasa_ord_srev.Text = "*Tasa Normal (De " + Session("TASAMINNORFIJ") + "% a " + Session("TASAMAXNORFIJ") + "%):"
            lbl_tasa_mor_srev.Text = "*Tasa Moratoria (De " + Session("TASAMINMORFIJ") + "% a " + Session("TASAMAXMORFIJ") + "%):"
            lbl_tasa_ind_srev.Text = "*Puntos (De " + Session("TASAMINNORIND") + "% a " + Session("TASAMAXNORIND") + "%):"
            lbl_tasa_ind_mora_srev.Text = "*Puntos (De " + Session("TASAMINMORIND") + "% a " + Session("TASAMAXMORIND") + "%):"
        End If

        Session("Con").Close()
    End Sub

    Private Sub get_periodicidad_arfin()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 11, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, "DIAS")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPLAN", Session("adVarChar"), Session("adParamInput"), 10, "PFSI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_PERIODICIDAD"
        Session("rs") = Session("cmd").Execute()

        cmb_tipo_per.Items.Clear()
        cmb_tipo_per.Items.Add(New ListItem("ELIJA", "0"))

        Do While Not Session("rs").EOF

            cmb_tipo_per.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("TIPOPERIODO").Value.ToString))

            Session("rs").movenext()
        Loop

        Session("Con").Close()



    End Sub

    Protected Sub cmb_tipo_per_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmb_tipo_per.SelectedIndexChanged
        Dim idproducto As Integer
        Dim tipoPeriodicidad As String

        idproducto = cmb_Producto.SelectedItem.Value.ToString
        tipoPeriodicidad = cmb_tipo_per.SelectedItem.Value

        InsumosPFSI(idproducto, tipoPeriodicidad)
    End Sub

    Private Sub LimpiaArrendamiento()
        txt_tasa_mor.Text = ""
        txt_tasa_ord.Text = ""
        cmb_per.Items.Clear()

    End Sub

    Protected Sub lnk_generar_plan_pagos_Click(sender As Object, e As EventArgs) Handles lnk_generar_plan_pagos.Click

        Dim monto As String
        Dim tasa As Decimal
        Dim idproducto As Integer
        Dim tipoPlan As String
        Dim tasaMora As Decimal
        Dim idSuc As Integer
        Dim tipoPeriodo As String
        Dim periodo As Integer
        Dim fechaPago As String
        Dim plazo As String
        Dim unidadPlazo As String
        Dim nombreProducto As String

        fechaPago = txt_fechaliberacion.Text
        plazo = txt_plazo.Text
        unidadPlazo = "DIAS"
        tipoPeriodo = Left(cmb_tipo_per.SelectedItem.Value, 4)
        periodo = cmb_per.SelectedItem.Value
        tasaMora = CDec(txt_tasa_mor.Text)
        tipoPlan = "PFSI"

        idproducto = cmb_Producto.SelectedItem.Value.ToString
        nombreProducto = cmb_Producto.SelectedItem.Text
        monto = txt_monto.Text
        idSuc = CInt(Session("SUCID"))
        tasa = CDec(txt_tasa_ord.Text)


        If plazo = "" Or monto = "" Or fechaPago = "" Then
            lbl_status_srev.Text = "Error: Capture plazo, monto y fecha inicio arrendamiento"
            Exit Sub
        Else

            Session("IDPROD") = idproducto
            Session("TIPOPLAN") = tipoPlan
            Session("MONTO") = monto
            Session("TIPOTASA") = "FIJ"
            Session("TASA") = tasa
            Session("IDINDICE") = 0
            Session("TIPOTASAMORA") = "FIJ"
            Session("TASAMORA") = tasaMora
            Session("IDINDICEMORA") = 0
            Session("SUCURSAL") = idSuc
            Session("TIPOPERIODO") = tipoPeriodo
            Session("PERIODO") = periodo
            Session("CADENA") = ""
            Session("FECHAPAGO") = fechaPago
            Session("UNIDADPLAZO") = unidadPlazo
            Session("PLAZO") = plazo
            Session("RANGO") = 0
            Session("ID") = 0

            Response.Redirect("CRED_EXP_COTIZADOR_PLAN.ASPX")
        End If





    End Sub
#End Region

#Region "SIMPLE REVOLVENTE"

    Protected Sub cmb_tipo_tasa_srev_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipo_tasa_srev.SelectedIndexChanged
        Dim idProducto As Integer
        idProducto = cmb_Producto.SelectedItem.Value

        LimpiaTasas()
        InsumosPlan(idProducto)
        Select Case cmb_tipo_tasa_srev.SelectedItem.Value

            Case "0"
                txt_tasa_ord_srev.Enabled = False
                txt_tasa_mor_srev.Enabled = False
                txt_pts_srev.Enabled = False
                txt_tasa_mor_srev.Enabled = False

            Case "FIJ"
                pnl_tasas_fija.Visible = True
                pnl_tasas_indizadas.Visible = False
                lbl_tasa_ord_srev.Enabled = True
                txt_tasa_ord_srev.Enabled = True
                txt_pts_srev.Enabled = False
                txt_tasa_mor_srev.Enabled = True
                txt_tasa_ind_mora_rev.Enabled = False

                lbl_tasa_ord_srev.Text = "*Tasa Normal (De " + Session("TASAMINNORFIJ") + "% a " + Session("TASAMAXNORFIJ") + "%):"

            Case "IND"
                pnl_tasas_indizadas.Visible = True
                pnl_tasas_fija.Visible = False
                lbl_tasa_ind_srev.Text = "(De " + Session("TASAMINNORIND") + "% a " + Session("TASAMAXNORIND") + "%):"
                lbl_tasa_ind_mora_srev.Text = " (De " + Session("TASAMINMORIND") + "% a " + Session("TASAMAXMORIND") + "%):"
                txt_tasa_mor_srev.Enabled = True
                txt_tasa_ind_mora_rev.Enabled = True
                txt_tasa_ord_srev.Enabled = False
                txt_tasa_mor_srev.Enabled = False
                txt_pts_srev.Text = ""
                txt_pts_srev.Enabled = True
                RequiredFieldValidator3.Visible = False
            Case Else
                LimpiaTasas()

        End Select
    End Sub

    Private Sub LimpiaTasas()
        txt_tasa_ord_srev.Text = ""
        txt_tasa_mor_srev.Text = ""
        txt_pts_srev.Text = ""
        txt_tasa_ind_mora_rev.Text = ""
        lbl_error_tasa_srev0.Text = ""
        lbl_error_mora_srev1.Text = ""
        lbl_error_pts_srev.Text = ""
        lbl_error_puntos_srev2.Text = ""
        lbl_error_tasa_srev0.Text = ""
    End Sub

    Private Sub get_periodicidad_SREV_CTAC()
        cmb_per_srev.Items.Clear()
        Dim Elija As New ListItem("ELIJA", 0)
        cmb_per_srev.Items.Add(Elija)
        Dim z As String
        'Ciclo que me permite agregar al combo del 1 al 31
        For count = 1 To 28

            z = "DIA " + count.ToString + " DEL MES"
            Dim y As New ListItem(z, count.ToString)
            cmb_per_srev.Items.Add(y)

        Next
        'Agrega al combo el mensaje Dia ultimo de mes
        Dim ultimo As New ListItem("DIA ULTIMO DE MES", 32)
        cmb_per_srev.Items.Add(ultimo)

    End Sub

    Public Sub LimpiaForma3()

        txt_rango_cptl_cc.Text = ""
        txt_monto_disp_srev.Text = ""
        rev_tasa_ord_srev.Text = ""
        lbl_error_tasa_srev.Text = ""
        rev_tasa_mor_srev.Text = ""
        txt_tasa_ord_srev.Text = ""
        RegularExpressionValidator3.Text = ""
        RegularExpressionValidator4.Text = ""
        txt_tasa_mor_srev.Text = ""
        txt_pts_srev.Text = ""
        txt_tasa_ind_mora_rev.Text = ""
        lbl_pagomes_srev.Text = ""
        lbl_status_srev.Text = ""
    End Sub

    Public Function validacionMorSI(ByVal tasaMoratoria As String, ByVal tipoTasa As String) As Integer
        lbl_error_puntos_srev2.Text = ""
        lbl_error_tasa_srev0.Text = ""
        lbl_errortasasi.Text = ""
        Dim COUNTMor As Integer
        COUNTMor = 1

        If tipoTasa = "FIJ" Then

            If CDec(tasaMoratoria) < CDec(Session("TASAMINMORFIJ").ToString) Or CDec(tasaMoratoria) > CDec(Session("TASAMAXMORFIJ").ToString) Then
                lbl_error_tasa_srev0.Text = "Error: La tasa no está en los límites establecidos "
                COUNTMor = 0
            End If

        Else

            If CDec(tasaMoratoria) < CDec(Session("TASAMINMORIND").ToString) Or CDec(tasaMoratoria) > CDec(Session("TASAMAXMORIND").ToString) Then
                lbl_error_puntos_srev2.Text = "Error: Los puntos % no están en los límites establecidos "
                COUNTMor = 0
            End If

        End If

        Return COUNTMor

    End Function


    Protected Sub lnk_generar_plan_pagos_srev_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnk_generar_plan_pagos_srev.Click
        Dim count As Integer
        Dim countmor As Integer

        Dim monto As String
        Dim tipoTasa As String
        Dim tasa As Decimal
        Dim idIndice As Integer
        Dim idproducto As Integer
        Dim tipoPlan As String

        Dim tipoTasaMora As String
        Dim tasaMora As Decimal
        Dim idIndiceMora As Integer
        Dim idSuc As Integer
        Dim tipoPeriodo As String
        Dim periodo As Integer
        Dim cadena As String
        Dim fechaPago As String
        Dim plazo As String
        Dim unidadPlazo As String
        Dim montoDisposicion As String
        Dim rango As Integer
        Dim id_cotizacion As Integer


        fechaPago = txt_fechaliberacion.Text
        plazo = txt_plazo.Text
        tipoPlan = "SI"
        idproducto = cmb_Producto.SelectedItem.Value.ToString
        monto = txt_monto.Text
        montoDisposicion = txt_monto_disp_srev.Text
        idSuc = CInt(Session("SUCID"))


        If plazo = "" Or monto = "" Or fechaPago = "" Then
            lbl_status_srev.Text = "Error: Capture plazo, monto de la línea de préstamo y fecha pago del préstamo"
            Exit Sub
        End If

        InsumosPlan(idproducto)


        unidadPlazo = "DIAS"
        tipoPeriodo = "DIAX"
        periodo = cmb_per_srev.SelectedItem.Value.ToString
        cadena = cmb_per_srev.SelectedItem.Value.ToString + "-"


        tipoTasaMora = cmb_tipo_tasa_srev.SelectedItem.Value.ToString

        If tipoTasaMora = "FIJ" Then
            tasaMora = txt_tasa_mor_srev.Text
            idIndiceMora = 0
        Else
            tasaMora = txt_tasa_ind_mora_rev.Text
            idIndiceMora = Session("INDMORIND")
        End If


        tipoTasa = cmb_tipo_tasa_srev.SelectedItem.Value.ToString

        If tipoTasa = "FIJ" Then
            tasa = txt_tasa_ord_srev.Text
            idIndice = 0
        Else
            tasa = txt_pts_srev.Text
            idIndice = Session("INDNORIND")
        End If




        If Validamonto(monto) = True Then
            If CDec(monto) >= CDec(Session("MONTOMIN").ToString) And CDec(monto) <= CDec(Session("MONTOMAX").ToString) Then
                If CInt(plazo) >= CInt(Session("PLAZOMIN").ToString) And CInt(plazo) <= CInt(Session("PLAZOMAX").ToString) Then
                    If Session("CLASIFICACION") = "SREV" Then

                        If montoDisposicion.Length <> 0 Then

                            If CDec(montoDisposicion) <= CDec(monto) Then

                                If (CStr(tasa) <> "" And CStr(tasaMora) <> "") Then

                                    If Validatasa(CStr(tasa)) = True Then
                                        count = validacionSI(tipoTasa, CStr(tasa), tipoTasaMora, CStr(tasaMora)) 'Primero Verifica que la tasa y la periodicidad este en el margen establecido
                                        countmor = validacionMorSI(CStr(tasaMora), tipoTasaMora)

                                        If count = 1 And countmor = 1 And validacionfecha(tipoPlan, fechaPago) = True Then
                                            id_cotizacion = 0
                                            rango = 0
                                            Session("IDPROD") = idproducto
                                            Session("TIPOPLAN") = tipoPlan
                                            Session("MONTO") = monto
                                            Session("TIPOTASA") = tipoTasa
                                            Session("TASA") = tasa
                                            Session("IDINDICE") = idIndice
                                            Session("TIPOTASAMORA") = tipoTasaMora
                                            Session("TASAMORA") = tasaMora
                                            Session("IDINDICEMORA") = idIndiceMora
                                            Session("SUCURSAL") = idSuc
                                            Session("TIPOPERIODO") = tipoPeriodo
                                            Session("PERIODO") = periodo
                                            Session("CADENA") = cadena
                                            Session("FECHAPAGO") = fechaPago
                                            Session("UNIDADPLAZO") = unidadPlazo
                                            Session("PLAZO") = plazo
                                            Session("RANGO") = rango
                                            Session("MONTODISPOSICION") = montoDisposicion
                                            Session("ID") = 0
                                            Response.Redirect("CRED_EXP_COTIZADOR_PLAN.ASPX")

                                        End If
                                    Else
                                        lbl_status_srev.Text = "Error: Verifique la tasa"
                                    End If

                                Else
                                    lbl_status_srev.Text = "Error: Verifique la tasa"
                                End If

                            Else
                                lbl_status_srev.Text = "Error: Verifique monto de disposición"
                            End If
                        Else
                            lbl_status_srev.Text = "Error: Verifique monto de disposición"
                        End If

                    ElseIf Session("CLASIFICACION") = "CTAC" Then

                        If CStr(rango).Length <> 0 Then

                            If montoDisposicion.Length <> 0 Then

                                If CDec(montoDisposicion) <= CDec(monto) Then

                                    If (CStr(tasa) <> "" And CStr(tasaMora) <> "") Then

                                        If Validatasa(CStr(tasa)) = True Then
                                            count = validacionSI(tipoTasa, CStr(tasa), tipoTasaMora, CStr(tasaMora)) 'Primero Verifica que la tasa y la periodicidad este en el margen establecido
                                            countmor = validacionMorSI(CStr(tasaMora), tipoTasaMora)

                                            If count = 1 And countmor = 1 And validacionfecha(tipoPlan, fechaPago) = True Then
                                                rango = txt_rango_cptl_cc.Text
                                                Session("IDPROD") = idproducto
                                                Session("TIPOPLAN") = tipoPlan
                                                Session("MONTO") = monto
                                                Session("TIPOTASA") = tipoTasa
                                                Session("TASA") = tasa
                                                Session("IDINDICE") = idIndice
                                                Session("TIPOTASAMORA") = tipoTasaMora
                                                Session("TASAMORA") = tasaMora
                                                Session("IDINDICEMORA") = idIndiceMora
                                                Session("SUCURSAL") = idSuc
                                                Session("TIPOPERIODO") = tipoPeriodo
                                                Session("PERIODO") = periodo
                                                Session("CADENA") = cadena
                                                Session("FECHAPAGO") = fechaPago
                                                Session("UNIDADPLAZO") = unidadPlazo
                                                Session("PLAZO") = plazo
                                                Session("RANGO") = rango
                                                Session("MONTODISPOSICION") = montoDisposicion
                                                Session("ID") = 0
                                                Response.Redirect("CRED_EXP_COTIZADOR_PLAN.ASPX")
                                            End If
                                        Else
                                            lbl_status_srev.Text = "Error: Verifique la tasa"
                                        End If

                                    Else
                                        lbl_status_srev.Text = "Error: Verifique la tasa"
                                    End If

                                Else
                                    lbl_status_srev.Text = "Error: Verifique monto de disposición"
                                End If

                            Else
                                lbl_status_srev.Text = "Error: Verifique monto de disposición"
                            End If
                        Else
                            lbl_status_srev.Text = "Error: Verifique rango"
                        End If

                    End If
                Else
                    lbl_status_srev.Text = "Error: Plazo fuera de los limites establecidos"
                End If
            Else
                lbl_status_srev.Text = "Error: Monto fuera de los limites establecidos"
            End If
        Else
            lbl_status_srev.Text = "Error: Monto incorrecto"
        End If


    End Sub
#End Region

#Region "ESPECIAL"

    Private Sub FECHAMAX(ByVal plazo As Integer, ByVal unidadPlazo As String, ByVal fechaPago As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, unidadPlazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, plazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 10, fechaPago)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_FECHA_MAXIMA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_max_fecha.Text = "Fecha máxima de la Cotización: " + (Session("rs").Fields("FECHA").value.ToString)
        End If

        Session("Con").Close()


    End Sub
    Private Sub PermitePeriodosGracia()

        Dim permite As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_PERIODOS_GRACIA_INT"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").Eof Then
            permite = Session("rs").fields("PERMITE").value.ToString()
        End If

        Session("Con").Close()

        If permite = "1" Then 'si permite periodo de gracias
            RevisaFacultadPeriodos()
        End If


    End Sub

    Private Sub RevisaFacultadPeriodos()
        Dim permiso As String = ""

        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FACULTAD_PERIODOS_GRACIA_INTERES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").Eof Then
            permiso = Session("rs").fields("PERMISO").value.ToString
        End If
        Session("Con").Close()

        If permiso = "0" Then
            txt_fechaini.Enabled = False
            txt_fechafin.Enabled = False
            btn_guardapgracia.Enabled = False
        Else

            txt_fechaini.Enabled = True
            txt_fechafin.Enabled = True
            btn_guardapgracia.Enabled = True
            Req_fechafin.Enabled = True
            Req_fechafin.Enabled = True
            LlenaPeriodosG()

        End If


    End Sub

    Private Sub limpiaPeriodosG()
        txt_fechaini.Text = ""
        txt_fechafin.Text = ""
        lbl_statuspgracia.Text = ""
    End Sub

    Private Sub limpiaComisiones()
        lbl_status_comision.Text = ""
    End Sub

    Private Sub LlenaPeriodosG()

        limpiaPeriodosG()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtperiodg As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_PERIODOS_GRACIA_INT_ID"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtperiodg, Session("rs"))
        dag_periodos.DataSource = dtperiodg
        dag_periodos.DataBind()
        Session("Con").Close()
    End Sub

    Protected Sub lnk_eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_eliminar.Click

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_COTIZADOR_ID"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        Accordion1.SelectedIndex = -1
        Accordion1.Enabled = False

        lnk_verplanespecial.Enabled = False
        lbl_fechamax.Text = ""
        muestramontoscapital()
        muestrapagointeres()
        muestraintereses()
        muestrainteresesmoratorios()
        LlenaComisiones()
        LlenaPeriodosG()
        muestrafechaliberacion()
        lbl_suma.Text = ""
        txt_fechafinnormal.Text = ""
        txt_fechafinmoratorio.Text = ""
        lbl_fechainiperiodonormal.Text = ""
        lbl_fechainiperiodomoratorio.Text = ""

        txt_tasamora.Text = ""
        txt_tasanormal.Text = ""
        cmb_tasanormal.SelectedIndex = "0"
        cmb_tasamora.SelectedIndex = "0"
        cmb_tipoplan.SelectedIndex = "0"

    End Sub

    '    '-------------------COMISIONES-----------------------------
    Public Sub muestrafechaliberacion()

        PermitePeriodosGracia()
        LlenaComisiones()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, "DIAS")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_plazo.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_FECHA_LIBERACION"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            If Session("rs").Fields("FECHAPAGO").value.ToString <> "" Then
                'Muestra la fecha máxima de pago de acuerdo a la fecha agregada
                lbl_fechamax.Text = "Fecha máxima de Pago: " + Left(Session("rs").fields("FECHAMAX").Value.ToString, 10)

            End If

            '------------Verifico que paneles ya tiene completos y los habilito
            If Session("rs").Fields("HABILITARINTERES").value.ToString = "HABILITAR PAGO CAPITAL" Then
                AccordionPane2.Enabled = True
            Else
                AccordionPane2.Enabled = False
            End If

            If Session("rs").Fields("HABILITARTASA").value.ToString = "HABILITAR TASA NORMAL" Then
                AccordionPane3.Enabled = True
            Else
                AccordionPane3.Enabled = False
            End If

            If Session("rs").Fields("HABILITARMORA").value.ToString = "HABILITAR TASA MORA" Then
                AccordionPane4.Enabled = True
            Else
                AccordionPane4.Enabled = False
            End If

            If Session("rs").Fields("HABILITARPERIODOG").value.ToString = "HABILITAR PERIODO GRACIA" Then
                AccordionPane5.Enabled = True
            Else
                AccordionPane5.Enabled = False
            End If


            If Session("rs").Fields("HABILITARCOMISION").value.ToString = "HABILITAR COMISION" Then
                AccordionPane6.Enabled = True
            Else
                AccordionPane6.Enabled = False
            End If

            '-------------Si la suma del capital es igual al monto ya puede generar el plan especial
            If Session("rs").Fields("RESPUESTA").value.ToString = "HABILITAR" Then
                lnk_verplanespecial.Enabled = True

            Else ' Aun no completa el total de los campos 
                lnk_verplanespecial.Enabled = False

            End If

            Session("FECHALIBERACION") = Left(Session("rs").Fields("FECHAMAX").value.ToString, 10)
            Session("FECHALIBERACIONCREDITO") = Session("rs").Fields("FECHAPAGO").value.ToString
        End If
        Session("Con").Close()


        lbl_fechainiperiodonormal.Text = Left(Session("FECHALIBERACIONCREDITO").ToString, 10)
        lbl_fechainiperiodomoratorio.Text = Left(Session("FECHALIBERACIONCREDITO").ToString, 10)



    End Sub

    Private Sub LlenaComisiones()

        limpiaComisiones()

        lst_ComAsg.Items.Clear()
        lst_ComDsp.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_COMISIONES_ASIGNADAS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("IDC").Value.ToString)
            If Session("rs").Fields("GRANTED").Value.ToString = "1" Then
                lst_ComAsg.Items.Add(item)
            Else
                lst_ComDsp.Items.Add(item)
            End If

            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click

        lbl_status_comision.Text = ""

        If lst_ComDsp.SelectedItem Is Nothing Then
            lbl_status_comision.Text = "Error: Seleccione una Comisión."
        Else
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDCOMISION", Session("adVarChar"), Session("adParamInput"), 10, lst_ComDsp.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CREADOX", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_COTIZADOR_COMISIONES_ASIGNADAS"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()
            LlenaComisiones()
            lbl_status_comision.Text = "Comisión asignada."
            muestrafechaliberacion()
        End If

    End Sub

    Protected Sub btn_rem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_rem.Click

        If lst_ComAsg.SelectedItem Is Nothing Then
            lbl_status.Text = "Error: Seleccione una Comisión."
        Else
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDCOMISION", Session("adVarChar"), Session("adParamInput"), 10, lst_ComAsg.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_COTIZADOR_COMISIONES_ASIGNADAS"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()
            LlenaComisiones()
            lbl_status_comision.Text = "Comisión eliminada."
            muestrafechaliberacion()
        End If
        lbl_status_comision.Visible = True

    End Sub

    '------------CAPITAL------------
    Protected Sub cmb_capporcentaje_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_capporcentaje.SelectedIndexChanged

        If cmb_capporcentaje.SelectedItem.Value.ToString = "0" Then
            Forma_capital("OFF")
        End If

        If cmb_capporcentaje.SelectedItem.Value.ToString = "MONTO" Then
            Forma_capital("ON")
            lbl_cantidad.Text = "*Monto Capital: $"
        End If

        If cmb_capporcentaje.SelectedItem.Value.ToString = "PORCENTAJE" Then
            Forma_capital("ON")
            lbl_cantidad.Text = "*Porcentaje Capital: %"
        End If


    End Sub


    Private Sub Forma_capital(ByVal switch As String)


        If switch = "ON" Then
            lbl_cantidad.Text = ""
            txt_capital.Text = ""
            lbl_cantidad.Visible = True
            txt_capital.Visible = True
            'lbl_capitalstatus.Text = ""
            RequiredFieldValidator_cantidad.Enabled = True
        Else
            lbl_cantidad.Text = ""
            txt_capital.Text = ""
            lbl_cantidad.Visible = False
            txt_capital.Visible = False
            ' lbl_capitalstatus.Text = ""
            RequiredFieldValidator_cantidad.Enabled = False
        End If


    End Sub

    Protected Sub cmb_pagocapital_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_pagocapital.SelectedIndexChanged
        lbl_capitalstatus.Text = ""
        If cmb_pagocapital.SelectedItem.Value.ToString = "0" Then
            LimpiaMontos()

        End If


        limpiar_controles()
        Forma_capital("OFF")

        If cmb_pagocapital.SelectedItem.Value.ToString = "UNOXUNO" Then

            lbl_fechainiperiodo.Text = "*Fecha de pago: "
            Visible_invisible("OFF")

        End If

        If cmb_pagocapital.SelectedItem.Value = "RECURRENTE" Then
            limpiar_controles()
            Forma_capital("OFF")

            lbl_fechainiperiodo.Text = "*Fecha inicio periodo: "
            lbl_error.Text = ""
            Visible_invisible("ON")

        End If

        If cmb_pagocapital.SelectedItem.Value = "COPIA" Then
            limpiar_controles()
            updpnl_capital.Visible = False

        End If



    End Sub

    Private Sub Habilitar_Deshabilitar(ByVal switch As String)

        If switch = "ON" Then

            RequiredFieldValidator_fechafinperiodo.Enabled = True
            MaskedEditExtender_fechafinperiodo.Enabled = True
            RequiredFieldValidator_pagorecu.Enabled = True
            RequiredFieldValidator_tiporecurrenciacapital.Enabled = True
            RequiredFieldValidator_dia.Enabled = True


        Else
            RequiredFieldValidator_fechafinperiodo.Enabled = False
            MaskedEditExtender_fechafinperiodo.Enabled = False
            RequiredFieldValidator_pagorecu.Enabled = False
            RequiredFieldValidator_tiporecurrenciacapital.Enabled = False
            RequiredFieldValidator_dia.Enabled = False

        End If

    End Sub

    Private Sub Visible_invisible(ByVal switch As String)


        updpnl_capital.Visible = True
        Dim tipoPlan As String = cmb_tipoplan.SelectedItem.Value
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value
        Dim unidadPlazo As String = "DIAS"

        If switch = "ON" Then

            Periodicidad(idProducto, unidadPlazo, tipoPlan)
            lbl_fechainiperiodo.Visible = True
            txt_fechainiperiodo.Visible = True
            lbl_formato_fecha_ini.Visible = True
            lbl_fechafinperiodo.Visible = True
            txt_fechafinperiodo.Visible = True
            txt_fechafinperiodo.Text = ""
            lbl_formato_fecha_fin.Visible = True
            lbl_pagorecu.Visible = True
            cmb_pagorecu.Visible = True
            cmb_tiporecurrenciacapital.Visible = False
            lbl_dia.Visible = False
            cmb_dia.Visible = False
            updpnl_capital.Visible = True
            updpnl_periodo.Visible = True
            Habilitar_Deshabilitar("ON")


        Else
            lbl_fechainiperiodo.Visible = True
            txt_fechainiperiodo.Visible = True
            lbl_formato_fecha_ini.Visible = True
            lbl_fechafinperiodo.Visible = False
            txt_fechafinperiodo.Visible = False
            lbl_formato_fecha_fin.Visible = False
            lbl_pagorecu.Visible = False
            cmb_pagorecu.Visible = False
            cmb_tiporecurrenciacapital.Visible = False
            lbl_dia.Visible = False
            cmb_dia.Visible = False
            updpnl_capital.Visible = True
            updpnl_periodo.Visible = False
            Habilitar_Deshabilitar("OFF")
        End If



    End Sub

    Private Sub Tipo_dia(ByVal Tipo As String)

        If Tipo = "DIA DE LA SEMANA" Or Tipo = "DIA DEL MES" Then

            lbl_dia.Visible = True
            cmb_dia.Visible = True
            RequiredFieldValidator_dia.Enabled = True
            cmb_dia.Items.Clear()


            If Tipo = "DIA DE LA SEMANA" Then

                Dim Elija As New ListItem("ELIJA", 0)
                cmb_dia.Items.Add(Elija)
                'Agrega al combo los dias de la semana
                Dim Lunes As New ListItem("LUNES", 2)
                cmb_dia.Items.Add(Lunes)
                Dim Martes As New ListItem("MARTES", 3)
                cmb_dia.Items.Add(Martes)
                Dim Miercoles As New ListItem("MIERCOLES", 4)
                cmb_dia.Items.Add(Miercoles)
                Dim Jueves As New ListItem("JUEVES", 5)
                cmb_dia.Items.Add(Jueves)
                Dim Viernes As New ListItem("VIERNES", 6)
                cmb_dia.Items.Add(Viernes)
                Dim Sabado As New ListItem("SABADO", 7)
                cmb_dia.Items.Add(Sabado)
                Dim Domingo As New ListItem("DOMINGO", 1)
                cmb_dia.Items.Add(Domingo)


            Else

                Dim Elija As New ListItem("ELIJA", 0)
                cmb_dia.Items.Add(Elija)
                Dim z As String
                'Ciclo que me permite agregar al combo del 1 al 31
                For count = 1 To 28

                    z = "DIA " + count.ToString + " DEL MES"
                    Dim y As New ListItem(z, count.ToString)
                    cmb_dia.Items.Add(y)

                Next
                'Agrega al combo el mensaje Dia ultimo de mes
                Dim ultimo As New ListItem("DIA ULTIMO DE MES", 32)
                cmb_dia.Items.Add(ultimo)

            End If

        End If

    End Sub

    Private Sub limpiar_controles()
        cmb_pagorecu.Items.Clear()
        cmb_pagorecu.Visible = False
        cmb_tiporecurrenciacapital.Items.Clear()
        cmb_dia.Items.Clear()
        txt_fechainiperiodo.Text = ""
        txt_fechafinperiodo.Text = ""
        lbl_error.Text = ""
        lbl_capitalstatus.Text = ""
        cmb_capporcentaje.SelectedIndex = "0"


    End Sub

    Protected Sub btn_agregarcapital_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarcapital.Click

        lbl_suma.Text = ""
        lbl_capitalstatus.Visible = True
        If txt_capital.Text = "" Then
            lbl_capitalstatus.Text = "Error: Falta cantidad a pagar"
            Exit Sub
        End If

        lbl_capitalstatus.Text = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 30, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 30, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 30, txt_monto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD_PLAZO", Session("adVarChar"), Session("adParamInput"), 10, "DIAS")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 30, txt_plazo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHALIBERACION", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaliberacion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_pagocapital.SelectedItem.Value.ToString = "UNOXUNO" Then
            Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_fechainiperiodo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NA")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
        ElseIf cmb_pagocapital.SelectedItem.Value.ToString = "RECURRENTE" Then '----CAPTURARON RECURRENTE
            Dim arre

            Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, txt_fechainiperiodo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, txt_fechafinperiodo.Text)
            Session("cmd").Parameters.Append(Session("parm"))


            arre = cmb_pagorecu.SelectedItem.Value

            If arre = "NDIA" Then
                Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NDIA")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrenciacapital.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If arre = "NSEM" Then
                Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NSEM")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrenciacapital.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If arre = "NMES" Then
                Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NMES")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrenciacapital.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If arre = "DIAX" Then

                If cmb_tiporecurrenciacapital.SelectedItem.Value = "DIA DEL MES" Then '--- Dia del mes
                    Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "DMES")
                    Session("cmd").Parameters.Append(Session("parm"))
                End If

                If cmb_tiporecurrenciacapital.SelectedItem.Value = "DIA DE LA SEMANA" Then '--- Dia del mes
                    Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "DSEM")
                    Session("cmd").Parameters.Append(Session("parm"))

                End If
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_dia.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))



            End If
        Else
            Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "COPIA")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))

        End If



        Session("parm") = Session("cmd").CreateParameter("TIPOCAPITAL", Session("adVarChar"), Session("adParamInput"), 15, cmb_capporcentaje.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CAPITAL", Session("adVarChar"), Session("adParamInput"), 10, txt_capital.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COTIZADOR_MONTO_PESP"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            '---- Muestra las diferentes validaciones que puede tener el plan especial
            If Session("rs").Fields("RESPUESTA").value.ToString <> "AGREGAR" Then

                lbl_capitalstatus.Text = Session("rs").Fields("RESPUESTA").value.ToString

            End If

            If Session("rs").Fields("HABILITAR").VALUE.ToString = "HABILITAR TASA NORMAL" Then
                AccordionPane3.Enabled = True
                lbl_fechamax.Text = "Fecha de liberación del préstamo:" + Left(Session("rs").Fields("NUEVAFECHA").value.ToString, 10)
            End If

            lbl_suma.Text = "Suma Capital: " + FormatCurrency(Session("rs").Fields("SUMA").value.ToString)
        End If

        Session("Con").Close()

        muestramontoscapital()
        muestrafechaliberacion()
        LimpiaMontos()
        'LimpiaPagointeres()
    End Sub

    'Llena iD
    Private Sub LLENAID()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COTIZADOR_ID"
        Session("rs") = Session("cmd").Execute()

        Session("ID") = Session("rs").Fields("ID").value.ToString

        Session("Con").Close()
    End Sub

    '    'Combo que permite agregar el tipo de pago
    Protected Sub cmb_pagorecu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_pagorecu.SelectedIndexChanged

        Dim tipoPeriodicidad As String = cmb_pagorecu.SelectedItem.Value
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value

        Muestraperiodicidadcapital(idProducto, tipoPeriodicidad)
        cmb_tiporecurrenciacapital.Enabled = True
        cmb_tiporecurrenciacapital.Visible = True

        'Agrega al combo de Periodicidad los periodos permitidos según sea el Caso
        Select Case cmb_pagorecu.SelectedItem.Value

            Case "NDIA"
                DiaX("OFF")
            Case "NSEM"
                DiaX("OFF")
            Case "NMES"
                DiaX("OFF")
            Case "DIAX"
                DiaX("ON")
            Case Else
                DiaX("OFF")
        End Select

    End Sub

    Private Sub DiaX(ByVal Switch As String)

        If Switch = "ON" Then
            cmb_tiporecurrenciacapital.AutoPostBack = True
            lbl_dia.Visible = False
            cmb_dia.Items.Clear()
            cmb_dia.Visible = False
        Else
            cmb_tiporecurrenciacapital.AutoPostBack = False
            lbl_dia.Visible = False
            cmb_dia.Items.Clear()
            cmb_dia.Visible = False
        End If


    End Sub

    'Llena el combo de tipo de recurrencia para el día x
    Protected Sub cmb_tiporecurrenciacapital_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmb_tiporecurrenciacapital.SelectedIndexChanged

        Dim TIPO As String

        TIPO = cmb_tiporecurrenciacapital.SelectedItem.Value.ToString

        Tipo_dia(TIPO)


    End Sub

    Private Sub LimpiaMontos()

        Visible_invisible("OFF")
        Forma_capital("OFF")
        cmb_pagocapital.SelectedIndex = "0"
        cmb_capporcentaje.SelectedIndex = "0"

        updpnl_capital.Visible = False
        updpnl_periodo.Visible = False


    End Sub



    '    '------------INTERES------------

    Protected Sub btn_agregarinteres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarinteres.Click
        lbl_statusinteres.Text = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 30, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 30, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 30, txt_monto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, "DIAS")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, txt_plazo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHALIBERACION", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaliberacion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_pagoint.SelectedItem.Value.ToString = "UNOXUNO" Then

            Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_fechainicialperiodointeres.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))


        Else '----CAPTURARON RECURRENTE

            Dim arre

            Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, txt_fechainicialperiodointeres.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, txt_fechafinalinteres.Text)
            Session("cmd").Parameters.Append(Session("parm"))

            arre = Left(cmb_tiporecurrencia.SelectedItem.Value, 4)

            If arre = "NDIA" Then
                Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NDIA")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrenciainteres.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If arre = "NSEM" Then
                Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NSEM")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrenciainteres.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If arre = "NMES" Then
                Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "NMES")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrenciainteres.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If arre = "DIAX" Then

                If cmb_tiporecurrenciainteres.SelectedItem.Value = "DIA DEL MES" Then '--- Dia del mes
                    Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "DMES")
                    Session("cmd").Parameters.Append(Session("parm"))
                End If

                If cmb_tiporecurrenciainteres.SelectedItem.Value = "DIA DE LA SEMANA" Then '--- Dia del mes
                    Session("parm") = Session("cmd").CreateParameter("TIPOC", Session("adVarChar"), Session("adParamInput"), 5, "DSEM")
                    Session("cmd").Parameters.Append(Session("parm"))

                End If
                Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_diainteres.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))

            End If


        End If

        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COTIZADOR_PAGO_INTERES_PESP"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            '---- Muestra las diferentes validaciones que puede tener el plan especial
            If Session("rs").Fields("RESPUESTA").value.ToString <> "AGREGAR" Then
                lbl_statusinteres.Visible = True
                lbl_statusinteres.Text = Session("rs").Fields("RESPUESTA").value.ToString
            End If

            '--Si el ultimo pago de interes es igual al ultimo pago de capital habilito el siguiente panel
            If Session("rs").Fields("HABILITAR").value.ToString = "HABILITAR PAGO CAPITAL" Then
                AccordionPane2.Enabled = True

            End If
        End If
        Session("Con").Close()

        muestrapagointeres()
        muestrafechaliberacion()
        LimpiaPagointeres()

    End Sub



    Protected Sub cmb_tiporecurrencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tiporecurrencia.SelectedIndexChanged

        Dim idproducto As Integer = cmb_Producto.SelectedItem.Value
        Dim tipoPeriodicidad As String = cmb_tiporecurrencia.SelectedItem.Value

        Muestraperiodicidadinteres(idproducto, tipoPeriodicidad)
        cmb_tiporecurrenciainteres.Enabled = True
        cmb_tiporecurrenciainteres.Visible = True

        'Agrega al combo de Periodicidad los periodos permitidos según sea el Caso
        Select Case cmb_tiporecurrencia.SelectedItem.Value

            Case "NDIA"
                DiaX_INTERES("OFF")
            Case "NSEM"
                DiaX_INTERES("OFF")
            Case "NMES"
                DiaX_INTERES("OFF")
            Case "DIAX"
                DiaX_INTERES("ON")
            Case Else
                DiaX_INTERES("OFF")
        End Select

    End Sub

    Private Sub LimpiaPagointeres()

        Visible_invisible_interes("OFF")
        cmb_pagoint.SelectedIndex = "0"
        updpnl_interes.Visible = False

    End Sub

    Private Sub limpiar_controles_INTERES()
        cmb_tiporecurrencia.Items.Clear()
        cmb_tiporecurrencia.Visible = False
        cmb_tiporecurrenciainteres.Items.Clear()
        cmb_diainteres.Items.Clear()
        txt_fechainicialperiodointeres.Text = ""
        txt_fechafinalinteres.Text = ""
        lbl_statusinteres.Text = ""



    End Sub


    Protected Sub cmb_pagoint_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_pagoint.SelectedIndexChanged

        lbl_statusinteres.Text = ""
        If cmb_pagoint.SelectedItem.Value.ToString = "0" Then
            LimpiaPagointeres()
        End If


        limpiar_controles_INTERES()

        If cmb_pagoint.SelectedItem.Value.ToString = "UNOXUNO" Then

            lbl_fechainicialperiodointeres.Text = "*Fecha de pago: "
            Visible_invisible_interes("OFF")

        End If

        If cmb_pagoint.SelectedItem.Value = "RECURRENTE" Then

            lbl_fechainicialperiodointeres.Text = "*Fecha inicio periodo: "
            Visible_invisible_interes("ON")

        End If



    End Sub


    Private Sub Habilitar_Deshabilitar_interes(ByVal switch As String)

        If switch = "ON" Then

            RequiredFieldValidator_fechafinalinteres.Enabled = True
            MaskedEditExtender_fechafinalinteres.Enabled = True
            RequiredFieldValidator_tiporecurrencia.Enabled = True
            RequiredFieldValidator_tiporecurrenciainteres.Enabled = True
            RequiredFieldValidator_diasemanainteres.Enabled = True


        Else
            RequiredFieldValidator_fechafinalinteres.Enabled = False
            MaskedEditExtender_fechafinalinteres.Enabled = False
            RequiredFieldValidator_tiporecurrencia.Enabled = False
            RequiredFieldValidator_tiporecurrenciainteres.Enabled = False
            RequiredFieldValidator_diasemanainteres.Enabled = False

        End If

    End Sub

    Private Sub Visible_invisible_interes(ByVal switch As String)



        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value
        Dim unidadPlazo As String = "DIAS"
        Dim tipoPlan As String = "ES"
        If switch = "ON" Then

            Periodicidad(idProducto, unidadPlazo, tipoPlan)

            lbl_fechainicialperiodointeres.Visible = True
            txt_fechainicialperiodointeres.Visible = True
            lbl_formato_fini_interes.Visible = True
            lbl_fechafinalinteres.Visible = True
            txt_fechafinalinteres.Visible = True
            txt_fechafinalinteres.Text = ""
            lbl_formato_ffinal_int.Visible = True
            lbl_pago_rec_interes.Visible = True
            cmb_tiporecurrencia.Visible = True
            cmb_tiporecurrenciainteres.Visible = False
            lbl_diaper_interes.Visible = False
            cmb_diainteres.Visible = False
            updpnl_interes.Visible = True
            Habilitar_Deshabilitar_interes("ON")


        Else
            lbl_fechainicialperiodointeres.Visible = True
            txt_fechainicialperiodointeres.Visible = True
            lbl_formato_fini_interes.Visible = True
            lbl_fechafinalinteres.Visible = False
            txt_fechafinalinteres.Visible = False
            txt_fechafinalinteres.Text = ""
            lbl_formato_ffinal_int.Visible = False
            lbl_pago_rec_interes.Visible = False
            cmb_tiporecurrencia.Visible = False
            cmb_tiporecurrenciainteres.Visible = False
            lbl_diaper_interes.Visible = False
            cmb_diainteres.Visible = False
            updpnl_interes.Visible = True
            Habilitar_Deshabilitar_interes("OFF")
        End If



    End Sub

    Private Sub Tipo_dia_interes(ByVal Tipo As String)

        If Tipo = "DIA DE LA SEMANA" Or Tipo = "DIA DEL MES" Then

            lbl_diaper_interes.Visible = True
            cmb_diainteres.Visible = True
            RequiredFieldValidator_diasemanainteres.Enabled = True
            cmb_diainteres.Items.Clear()


            If Tipo = "DIA DE LA SEMANA" Then

                Dim Elija As New ListItem("ELIJA", 0)
                cmb_diainteres.Items.Add(Elija)
                'Agrega al combo los dias de la semana
                Dim Lunes As New ListItem("LUNES", 2)
                cmb_diainteres.Items.Add(Lunes)
                Dim Martes As New ListItem("MARTES", 3)
                cmb_diainteres.Items.Add(Martes)
                Dim Miercoles As New ListItem("MIERCOLES", 4)
                cmb_diainteres.Items.Add(Miercoles)
                Dim Jueves As New ListItem("JUEVES", 5)
                cmb_diainteres.Items.Add(Jueves)
                Dim Viernes As New ListItem("VIERNES", 6)
                cmb_diainteres.Items.Add(Viernes)
                Dim Sabado As New ListItem("SABADO", 7)
                cmb_diainteres.Items.Add(Sabado)
                Dim Domingo As New ListItem("DOMINGO", 1)
                cmb_diainteres.Items.Add(Domingo)


            Else
                Dim z As String
                'Agrega al combo el mensaje Dia ultimo de mes
                Dim Elija As New ListItem("ELIJA", 0)
                cmb_diainteres.Items.Add(Elija)

                'Ciclo que me permite agregar al combo del 1 al 31
                For count = 1 To 28

                    z = "DIA " + count.ToString + " DEL MES"
                    Dim y As New ListItem(z, count.ToString)
                    cmb_diainteres.Items.Add(y)

                Next
                'Agrega al combo el mensaje Dia ultimo de mes
                Dim ultimo As New ListItem("DIA ULTIMO DE MES", 32)
                cmb_diainteres.Items.Add(ultimo)

            End If

        End If

    End Sub

    Private Sub DiaX_INTERES(ByVal Switch As String)

        If Switch = "ON" Then
            cmb_tiporecurrenciainteres.AutoPostBack = True
            lbl_diaper_interes.Visible = False
            cmb_diainteres.Items.Clear()
            cmb_diainteres.Visible = False
        Else
            cmb_tiporecurrenciainteres.AutoPostBack = False
            lbl_diaper_interes.Visible = False
            cmb_diainteres.Items.Clear()
            cmb_diainteres.Visible = False
        End If


    End Sub


    Protected Sub cmb_tiporecurrenciainteres_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmb_tiporecurrenciainteres.SelectedIndexChanged

        Dim TIPO As String
        TIPO = cmb_tiporecurrenciainteres.SelectedItem.Value.ToString
        Tipo_dia_interes(TIPO)
    End Sub
    '    '---------------------ELIMINACION DE CAPITAL Y DE PERIODO DE INTERES------------
    'Elimina todo el capital del plan de pago
    Protected Sub btn_eliminarplanpago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminarplanpago.Click

        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value

        lbl_capitalstatus.Text = ""
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_COTIZADOR_MONTO_PESp"
        Session("cmd").Execute()
        Session("Con").Close()

        lbl_suma.Text = "Suma Capital: $0.00 "
        LimpiaPagointeres()
        LimpiaMontos()
        muestramontoscapital()
        muestrafechaliberacion()

    End Sub

    'Elimina todo el pago de interes del plan de pago
    Protected Sub Btn_eliminarinteres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_eliminarinteres.Click
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_COTIZADOR_PERIODO_PAGOINT_PESP"
        Session("cmd").Execute()
        Session("Con").Close()

        LimpiaPagointeres()
        muestrapagointeres()
        muestrafechaliberacion()


    End Sub


    'COMBO DE TASA FIJA
    Protected Sub cmb_tasanormal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tasanormal.SelectedIndexChanged
        lbl_statusnormal.Text = ""
        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString

        TASAS()
        InsumosPlan(idProducto)
        Select Case cmb_tasanormal.SelectedItem.Value.ToString
            Case "FIJ"
                updpnl_tipo_tasas.Visible = True
                txt_tasanormal.Visible = True
                lbl_tasanor.Visible = True
                lbl_indicenormal.Visible = False
                lbl_puntosnormal.Visible = False
                lbl_tasanor.Text = "*(De " + Session("TASAMINNORFIJ") + "% a " + Session("TASAMAXNORFIJ") + "%):"
                txt_tasanormal.Text = ""

                If Session("VARIABLE") = 0 Then
                    txt_fechafinnormal.Text = Session("FECHALIBERACION").ToString
                    txt_fechafinnormal.Enabled = False
                Else
                    txt_fechafinnormal.Text = ""
                    txt_fechafinnormal.Enabled = True

                End If

            Case "IND"
                updpnl_tipo_tasas.Visible = True
                txt_tasanormal.Visible = True
                lbl_tasanor.Visible = False
                lbl_indicenormal.Visible = True
                lbl_puntosnormal.Visible = True
                lbl_puntosnormal.Text = "*Puntos (De " + Session("TASAMINNORIND") + "% a " + Session("TASAMAXNORIND") + "%):"
                txt_tasanormal.Text = ""


                If Session("VARIABLEIND") = 0 Then

                    '--se agrega un dia a la fecha de liberacion del préstamo.. debido a que no puede ser la misma fecha
                    txt_fechafinnormal.Text = Session("FECHALIBERACION").ToString
                    txt_fechafinnormal.Enabled = False
                Else


                    txt_fechafinnormal.Text = ""
                    txt_fechafinnormal.Enabled = True

                End If

            Case "0"
                '  updpnl_tipo_tasas.Visible = False
                lbl_tasanor.Visible = False
                lbl_indicenormal.Visible = False
                lbl_puntosnormal.Visible = False
        End Select



    End Sub

    'COMBO DE TASA MORATORIO
    Protected Sub cmb_tasamora_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tasamora.SelectedIndexChanged

        Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString
        InsumosPlan(idProducto)
        Select Case cmb_tasamora.SelectedItem.Value.ToString
            Case "FIJ"

                updpnl_moratorio.Visible = True
                txt_tasamora.Visible = True
                lbl_tasamora.Visible = True
                lbl_indicemora.Visible = False
                lbl_puntosmora.Visible = False
                lbl_tasamora.Text = "*(De " + Session("TASAMINMORFIJ") + "% a " + Session("TASAMAXMORFIJ") + "%):"
                txt_tasamora.Text = ""


                If Session("VARIABLEMORA") = 0 Then

                    txt_fechafinmoratorio.Text = Session("FECHALIBERACION").ToString
                    txt_fechafinmoratorio.Enabled = False
                Else
                    txt_fechafinmoratorio.Text = ""
                    txt_fechafinmoratorio.Enabled = True

                End If


            Case "IND"

                updpnl_moratorio.Visible = True
                txt_tasamora.Visible = True
                lbl_tasamora.Visible = False
                lbl_indicemora.Visible = True
                lbl_puntosmora.Visible = True
                txt_tasamora.Text = ""
                lbl_puntosmora.Text = "*(De " + Session("TASAMINMORIND") + "% a " + Session("TASAMAXMORIND") + "%):"


                If Session("VARIABLEINDMORA") = 0 Then

                    '--se agrega un dia a la fecha de liberacion del préstamo.. debido a que no puede ser la misma fecha
                    txt_fechafinmoratorio.Text = Session("FECHALIBERACION").ToString
                    txt_fechafinmoratorio.Enabled = False
                Else

                    txt_fechafinmoratorio.Text = ""
                    txt_fechafinmoratorio.Enabled = True

                End If
            Case "0"
                updpnl_moratorio.Visible = False
                lbl_tasamora.Text = ""
                lbl_puntosmora.Text = ""
                lbl_indicemora.Text = ""
        End Select

    End Sub

    '    'Inserta periodo de tasas normales
    Private Sub InsertaPeriodoTasa()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 30, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 30, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 10, "NOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, lbl_fechainiperiodonormal.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, txt_fechafinnormal.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASIFICACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tasanormal.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))

        If cmb_tasanormal.SelectedItem.Value = "FIJ" Then

            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, CDec(txt_tasanormal.Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INDICE", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
        Else

            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, CDec(txt_tasanormal.Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INDICE", Session("adVarChar"), Session("adParamInput"), 10, Session("INDNORIND"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "INS_COTIZADOR_TASAS_PESP"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            '---- Muestra las diferentes validaciones que puede tener LA TASA NORMAL
            If Session("rs").Fields("RESPUESTA").value.ToString <> "AGREGAR" Then

                lbl_statusnormal.Text = Session("rs").Fields("RESPUESTA").value.ToString
            Else
                '-------- SE LE AGREGA UN DIA A LA FECHA ULTIMA AGREGADA PARA SER LA FECHA INICIAL A LA SIGUIENTE INSERCION
                lbl_fechainiperiodonormal.Text = CStr(CDate(txt_fechafinnormal.Text).AddDays(1))
                '------------ SE ASIGNA LA FECHA ULTIMA COMO LA FECHA INICIAL AL SIGUIENTE INSERTADA
                Session("FECHAINIFINPERIODO") = lbl_fechainiperiodonormal.Text
            End If

            If Session("rs").fields("HABILITAR").value.ToString = "HABILITAR TASA MORATORIA" Then
                AccordionPane4.Enabled = True
            End If
        End If
        Session("Con").Close()
        muestraintereses()
        limpiatasanormal()
        limpiatasamoratorio()
    End Sub


    Public Function validacionPESP() As Integer

        lbl_error_tasanormal.Text = ""
        lbl_error_tasamora.Text = ""
        Dim COUNT As Integer
        COUNT = 1

        '-----------VERIFICO POR LA TASA NORMAL FIJA
        If cmb_tasanormal.SelectedItem.Value = "FIJ" Then

            If CDec(txt_tasanormal.Text) < CDec(Session("TASAMINNORFIJ").ToString) Or CDec(txt_tasanormal.Text) > CDec(Session("TASAMAXNORFIJ").ToString) Then
                lbl_error_tasanormal.Text = "Error: La tasa no está en los límites establecidos "
                COUNT = 0
            End If

        End If


        Return COUNT
    End Function

    '    'Botón Agregar de tasas normales PESP
    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregartasanormal.Click
        Dim count As Integer

        If Validatasa(txt_tasanormal.Text) = True Then
            count = validacionPESP()
            If count = 1 Then
                InsertaPeriodoTasa()

            End If
        Else
            lbl_statusnormal.Text = "Error: Verifique su tasa"

        End If


    End Sub

    'Botón Agregar tasa moratoria PESP
    Protected Sub btn_agregarmora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarmora.Click
        Dim count As Integer
        lbl_statusmora.Text = ""
        If Validatasa(txt_tasamora.Text) = True Then
            count = validaciontasamora()
            If count = 1 Then
                InsertaPeriodoTasaMora()

            End If
        Else
            lbl_statusmora.Text = "Error: Verifique su tasa"
        End If


        'liberarplan() 'Verifica si puede generar el plan
    End Sub

    'Inserta periodo de tasas moratorias
    Private Sub InsertaPeriodoTasaMora()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 30, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 30, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 10, "MOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAI", Session("adVarChar"), Session("adParamInput"), 10, lbl_fechainiperiodomoratorio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAF", Session("adVarChar"), Session("adParamInput"), 10, txt_fechafinmoratorio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASIFICACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tasamora.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))

        If cmb_tasamora.SelectedItem.Value = "FIJ" Then

            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, CDec(txt_tasamora.Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INDICE", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
        Else

            Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, CDec(txt_tasamora.Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INDICE", Session("adVarChar"), Session("adParamInput"), 10, Session("INDMORIND"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COTIZADOR_TASAS_PESP"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            '-*----- SI LA FECHA INICIAL DEL PERIODO ES MAYOR O IGUAL A LA FECHA DE LIBERACION SE DESHABILITA EL BOTÓN
            If Session("rs").Fields("RESPUESTA").value.ToString = "Ya cubre con el total de periodo establecido" Then
                btn_agregarmora.Enabled = False
            End If


            '---- Muestra las diferentes validaciones que puede tener el plan especial
            If Session("rs").Fields("RESPUESTA").value.ToString <> "AGREGAR" Then

                lbl_statusmora.Text = Session("rs").Fields("RESPUESTA").value.ToString
            Else
                '-------- SE LE AGREGA UN DIA A LA FECHA ULTIMA AGREGADA PARA SER LA FECHA INICIAL A LA SIGUIENTE INSERCION
                lbl_fechainiperiodomoratorio.Text = CStr(CDate(txt_fechafinmoratorio.Text).AddDays(1))
                '------------ SE ASIGNA LA FECHA ULTIMA COMO LA FECHA INICIAL AL SIGUIENTE INSERTADA
                Session("FECHAINIFINMORA") = lbl_fechainiperiodomoratorio.Text

            End If

            If Session("rs").fields("HABILITAR").value.ToString = "HABILITAR" Then
                AccordionPane5.Enabled = True
                AccordionPane6.Enabled = True
                lnk_verplanespecial.Enabled = True
            End If

        End If
        Session("Con").Close()
        muestrainteresesmoratorios()
        limpiatasamoratorio()

    End Sub

    '    '-------SE LIMPIAN LOS RESPECTIVOS CONTROLES DE TASAS NORMALES Y MORATORIAS

    Private Sub limpiatasanormal()
        cmb_tasanormal.SelectedIndex = 0
        lbl_tasanor.Text = ""
        txt_tasanormal.Text = ""
        lbl_indicenormal.Text = ""
        lbl_puntosnormal.Text = ""
        txt_tasanormal.Visible = False
        updpnl_tipo_tasas.Visible = False

    End Sub

    Private Sub limpiatasamoratorio()
        cmb_tasamora.SelectedIndex = 0
        lbl_tasamora.Text = ""
        txt_tasamora.Text = ""
        lbl_indicemora.Text = ""
        lbl_puntosmora.Text = ""
        txt_tasamora.Visible = False
        updpnl_moratorio.Visible = False

    End Sub

    '    '------------------ELIMINACION DE TASAS-------------
    'Elimina la ultima tasa normal
    Protected Sub btn_eliminarultimo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminarultimo.Click
        Dim TIPOTASA As String
        TIPOTASA = "NOR"
        ELIMINARTASAS(TIPOTASA)
        limpiatasanormal()
        muestraintereses()
        TASAS() '--mando a llamar para que me muestre el lbl de inicio del periodo de la tasa
        muestrainteresesmoratorios()

        '---DESHABILITO LOS OTROS PLANELES---
        AccordionPane4.Enabled = False
    End Sub

    'Elimina la ultima tasa moratoria
    Protected Sub btn_eliminarultimomora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminarultimomora.Click

        lbl_capitalstatus.Text = ""
        Dim TIPOTASA As String
        TIPOTASA = "MOR"
        ELIMINARTASAS(TIPOTASA) 'le envio el tipo de tasa que deseo eliminar
        muestrainteresesmoratorios()
        muestrafechaliberacion()

        'liberarplan()
        TASAS() '--mando a llamar para que me muestre el lbl de inicio del periodo de la tasa
    End Sub

    'Elimina las tasas
    Private Sub ELIMINARTASAS(ByVal TIPOTASA As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 30, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 30, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 10, TIPOTASA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[DEL_COTIZADOR_TASA_PESP]"
        Session("rs") = Session("cmd").Execute()

        If TIPOTASA = "NOR" Then
            '---- Muestra La fecha ultima después de haber eliminado
            lbl_fechainiperiodonormal.Text = Left(Session("rs").Fields("FECHAFINAL").value.ToString, 10)
        End If
        If TIPOTASA = "MOR" Then
            '---- Muestra La fecha ultima después de haber eliminado
            lbl_fechainiperiodomoratorio.Text = Left(Session("rs").Fields("FECHAFINAL").value.ToString, 10)
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_guardapgracia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardapgracia.Click


        If CDate(txt_fechafin.Text) < CDate(txt_fechaini.Text) Then

            lbl_statuspgracia.Text = "Error: La fecha final del periodo es menor a la fecha inicial"
        Else

            lbl_statuspgracia.Text = ""
            Dim exito As String = ""

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAINI", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaini.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAFIN", Session("adVarChar"), Session("adParamInput"), 10, txt_fechafin.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHASIS", Session("adVarChar"), Session("adParamInput"), 10, Session("FechaSis").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_COTIZADOR_PERIODO_GRACIA"
            Session("rs") = Session("cmd").Execute()

            exito = Session("rs").fields("EXITO").value.ToString()
            Session("Con").Close()

            If exito = "1" Then 'inserto correctamente
                LlenaPeriodosG()
                muestrafechaliberacion()
            Else
                lbl_statuspgracia.Text = "Error: Fechas incorrectas"
            End If

        End If


    End Sub

    Private Sub EliminaPeriodo(ByVal idper As String)
        lbl_statuspgracia.Text = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPER", Session("adVarChar"), Session("adParamInput"), 10, idper)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_COTIZADOR_PERIODO_GRACIA"
        Session("cmd").Execute()
        Session("Con").Close()

        lbl_statuspgracia.Text = "Periodo de gracia eliminado exitosamente"
        LlenaPeriodosG()

    End Sub

    Private Sub dag_periodos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_periodos.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            EliminaPeriodo(e.Item.Cells(0).Text)

        End If

    End Sub

    Protected Sub dag_periodos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_periodos.ItemDataBound
        e.Item.Cells(0).Visible = False

    End Sub

    Private Sub TASAS()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 30, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 30, cmb_Producto.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TASAS_PESP"

        Session("rs") = Session("cmd").Execute()

        lbl_fechainiperiodonormal.Text = Left(Session("rs").Fields("FECHANORMAL").value.ToString, 10)
        lbl_fechainiperiodomoratorio.Text = Left(Session("rs").Fields("FECHAMORA").value.ToString, 10)


        Session("Con").Close()

    End Sub


    '    '----------------------------------PLAN ESPECIAL-----------------------------------

    Private Sub Muestraperiodicidadcapital(ByVal idProducto As Integer, ByVal tipoPeriodicidad As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, tipoPeriodicidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TIPO_PERIODO_INTERES_PESP"
        Session("rs") = Session("cmd").Execute()

        'SE MUESTRAN LOS TIPOS DE PLAN DE PAGOS DISPONIBLES
        cmb_tiporecurrenciacapital.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_tiporecurrenciacapital.Items.Add(elija)


        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").value.ToString + " " + Session("rs").Fields("UNIDAD").Value.ToString, Session("rs").Fields("PERIODO").Value.ToString)
            cmb_tiporecurrenciacapital.Items.Add(item)
            Session("rs").movenext()
        Loop


        Session("Con").Close()
    End Sub

    Private Sub Muestraperiodicidadinteres(ByVal idProducto As Integer, ByVal tipoPeriodicidad As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, tipoPeriodicidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TIPO_PERIODO_INTERES_PESP"
        Session("rs") = Session("cmd").Execute()

        'SE MUESTRAN LOS TIPOS DE PLAN DE PAGOS DISPONIBLES
        cmb_tiporecurrenciainteres.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_tiporecurrenciainteres.Items.Add(elija)


        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").value.ToString + " " + Session("rs").Fields("UNIDAD").Value.ToString, Session("rs").Fields("PERIODO").Value.ToString)
            cmb_tiporecurrenciainteres.Items.Add(item)
            Session("rs").movenext()
        Loop


        Session("Con").Close()
    End Sub

    '    '-------------VALIDACION DE TASA NORMAL Y MORATORIA QUE ESTE EN EL LIMITE CONFIGURADO EN EL CONFIGURADOR DE PRODUCTOS

    'Verifica que la tasa moratoria este en el limite establecido
    Public Function validaciontasamora() As Integer

        lbl_error_tasamora.Text = ""
        Dim COUNT As Integer
        COUNT = 1

        '---------------VERIFICO POR LA TASA MORATORIA FIJA
        If cmb_tasamora.SelectedItem.Value = "FIJ" Then

            If CDec(txt_tasamora.Text) < CDec(Session("TASAMINMORFIJ").ToString) Or CDec(txt_tasamora.Text) > CDec(Session("TASAMAXMORFIJ").ToString) Then
                lbl_error_tasamora.Text = "Error: La tasa no está en los límites establecidos "
                COUNT = 0
            End If

        End If

        Return COUNT
    End Function

    '-------------------DB GRID--------------------------

    'Muestra los registros de tasas normales que existen en el  plan especial de acuerdo al folio en el DB GRID
    Private Sub muestraintereses()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtplaninteres As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_INTERESES_PESP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtplaninteres, Session("rs"))
        dag_normal.DataSource = dtplaninteres
        dag_normal.DataBind()
        Session("Con").Close()
    End Sub

    ''Muestra los registros de tasas moratorias que existen en el  plan especial de acuerdo al folio en el DB GRID
    Private Sub muestrainteresesmoratorios()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtplaninteres As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_INTERESES_MORATORIO_PESP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtplaninteres, Session("rs"))
        dag_moratorio.DataSource = dtplaninteres
        dag_moratorio.DataBind()
        Session("Con").Close()
    End Sub

    'Muestra los registros que existen  en el panel de montos del plan especial de acuerdo al folio en el DB GRID
    Private Sub muestramontoscapital()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtplan As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_MONTOS_PESP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtplan, Session("rs"))
        dagcapital.DataSource = dtplan
        dagcapital.DataBind()
        Session("Con").Close()
    End Sub

    'Muestra los pagos de interes que existen en el panel de pago de interes de acuerod al folio en el DB GRID
    Private Sub muestrapagointeres()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtplan As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Producto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_COTIZADOR_PAGO_INTERES_PESP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtplan, Session("rs"))
        dag_pagointeres.DataSource = dtplan
        dag_pagointeres.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub lnk_verplanespecial_Click(sender As Object, e As System.EventArgs) Handles lnk_verplanespecial.Click

        Dim monto As String
        Dim idproducto As Integer
        Dim tipoPlan As String

        Dim idSuc As Integer
        Dim fechaPago As String
        Dim plazo As String
        Dim unidadPlazo As String = "DIAS"
        Dim id_cotizacion As Integer

        fechaPago = txt_fechaliberacion.Text
        plazo = txt_plazo.Text
        tipoPlan = "ES"
        idproducto = cmb_Producto.SelectedItem.Value.ToString
        monto = txt_monto.Text

        idSuc = CInt(Session("SUCID"))
        id_cotizacion = Session("ID")

        Session("IDPROD") = idproducto
        Session("TIPOPLAN") = tipoPlan
        Session("MONTO") = monto
        Session("TIPOTASA") = ""
        Session("TASA") = 0
        Session("IDINDICE") = 0
        Session("TIPOTASAMORA") = ""
        Session("TASAMORA") = 0
        Session("IDINDICEMORA") = 0
        Session("SUCURSAL") = idSuc
        Session("TIPOPERIODO") = ""
        Session("PERIODO") = 0
        Session("CADENA") = ""
        Session("FECHAPAGO") = fechaPago
        Session("UNIDADPLAZO") = unidadPlazo
        Session("PLAZO") = plazo
        Session("RANGO") = 0
        Session("ID") = id_cotizacion

        Response.Redirect("CRED_EXP_COTIZADOR_PLAN.ASPX")

    End Sub
#End Region

End Class