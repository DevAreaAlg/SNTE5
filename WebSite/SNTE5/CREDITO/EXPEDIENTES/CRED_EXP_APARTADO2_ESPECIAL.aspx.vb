Public Class CRED_EXP_APARTADO2_ESPECIAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX(Session("APARTADO").ToString, "Plan de Pagos Especial")

        If Not Me.IsPostBack Then
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")

            'LLENO COMBO CON LOS TIPOS DE PLAN DE PAGO DISPONIBLES PARA EL PRODUCTO

            TraeMontoPlazo() 'Muestra el monto, el plazo y unidad
            muestrafechaliberacion() 'Muestra la fecha de liberación
            Llenatasas() 'Llena los combos de tasas del plan especial

            TASAS() 'Muestra en los lbls la fecha inicial del periodo
            muestramontoscapital() 'Muestra los pagos a capital
            muestrapagointeres() 'Muestra el pago a interes
            muestraintereses() ' Muestra los registros que va teniendo el plan especial de acuerdo al folio
            muestrainteresesmoratorios() 'Muestra las tasas moratorias
            Facultad()
            expbloqueado()
        End If
    End Sub

    Private Sub LIMPIAVARIABLES()
        'Se matan las variables de sesión.

        Session("MONTO") = Nothing
        Session("PLAZO") = Nothing
        Session("UNIDAD") = Nothing
        Session("DIAS") = Nothing
        Session("MESES") = Nothing
        Session("TASAMIN") = Nothing
        Session("TASAMAX") = Nothing
        Session("NUMPAGOS") = Nothing
        Session("GENERAPAGO") = Nothing
        Session("TIPOTASA") = Nothing
        Session("TIPOPLANPAGO") = Nothing
        Session("TASA") = Nothing
        Session("PERIODICIDAD") = Nothing
        Session("UNIDADPERIODO") = Nothing
        Session("FACULTAD") = Nothing
        Session("FECHALIBERACION") = Nothing
        Session("FECHALIBERACIONCREDITO") = Nothing
        Session("TASAMINNORFIJ") = Nothing
        Session("TASAMAXNORFIJ") = Nothing
        Session("TASAMINMORFIJ") = Nothing
        Session("TASAMAXMORFIJ") = Nothing
        Session("TASAMINNORIND") = Nothing
        Session("TASAMAXNORIND") = Nothing
        Session("TASAMINMORIND") = Nothing
        Session("TASAMAXMORIND") = Nothing
        Session("INDNORIND") = Nothing
        Session("INDMORIND") = Nothing
        Session("VARIABLE") = Nothing
        Session("VARIABLEIND") = Nothing
        Session("VARIABLEMORA") = Nothing
        Session("OPCION_FRA") = Nothing
        Session("C_COBRO_FRA") = Nothing
        Session("C_TIEMPO_FRA") = Nothing
        Session("C_TOTAL_FRA") = Nothing
        Session("C_IVA_FRA") = Nothing
        Session("OPCION") = Nothing
        Session("C_PCTJE") = Nothing
        Session("C_TOTAL") = Nothing
        Session("C_IVA") = Nothing
        Session("C_COMISION") = Nothing
    End Sub

    Private Sub Facultad()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FACULTAD_PLANPAGO_PAGARES"
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("FACULTAD") = Session("rs").Fields("FACULTAD").value.ToString
        Session("Con").Close()
    End Sub


    '----------------------------VERIFICA SI EL EXPEDIENTE ESTÁ BLOQUEADO----------------------------------------
    Private Sub expbloqueado()

        Dim bloqueado As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            bloqueado = Session("rs").fields("BLOQUEADO").value.ToString()

            If bloqueado = "1" And Session("FACULTAD") = "0" Then 'Si está bloqueado el expediente se deshabilitan los botones
                lnk_fecha.Enabled = False
            End If

        End If
        Session("Con").Close()
    End Sub

    'Muestra el Monto y el Plazo que trae el producto desde el configurador de Productos
    Private Sub TraeMontoPlazo()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MONTO_PLAZO_CREDITO"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("MONTO").Value.ToString = "" Then 'no los ha capturado por lo que no lo dejo seguir usando el apartado
            lbl_statusapartado.Text = "Error: el monto y plazo no han sido capturados."
            Session("Con").Close()
            Exit Sub
        Else 'ya capturo el monto y plazo en el apartado 1
            Session("MONTO") = Session("rs").Fields("MONTO").Value.ToString
            lbl_monto.Text = FormatCurrency(Session("MONTO"))
            Session("PLAZO") = Session("rs").Fields("PLAZO").Value.ToString
            Session("UNIDAD") = Session("rs").Fields("UNIDAD").Value.ToString
            lbl_plazo.Text = Session("PLAZO")
            lbl_lplazo.Text = "Plazo (" + Session("UNIDAD") + "):"
            Session("Con").Close()
            muestrafechaliberacion()
        End If

    End Sub

    'Valida que la fecha de pago sea menor a la fecha del sistema
    Public Function validacionfecha() As Boolean

        lbl_statusnormal.Text = ""
        Dim resp As Boolean = True
        If CDate(txt_fechaliberacion.Text) < CDate(Session("FechaSis")) Then
            lbl_statusnormal.Text = "Error:La fecha de pago del préstamo no puede ser anterior a la fecha del sistema"
            resp = False
        End If

        Return resp
    End Function

    'Muestra la fecha de liberación
    Public Sub muestrafechaliberacion()

        If Session("MONTO") = "" Then
            lbl_statusapartado.Text = "Error: el monto y plazo no han sido capturados."
        Else

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_FECHA_LIBERACION"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then


                If Session("rs").Fields("FECHAPAGO").value.ToString <> "" Then
                    txt_fechaliberacion.Text = Session("rs").Fields("FECHAPAGO").value.ToString
                    'Muestra la fecha máxima de pago de acuerdo a la fecha agregada
                    lbl_fechamax.Text = "Fecha de liquidación del préstamo: " + Left(Session("rs").fields("FECHAMAX").Value.ToString, 10)

                End If

                '-------------Si la suma del capital es igual al monto ya puede generar el plan especial
                If Session("rs").Fields("RESPUESTA").value.ToString = "HABILITAR" Then
                    lnk_verplanespecial.Enabled = True
                    lnk_verplanpago.Enabled = True

                Else ' Aun no completa el total de los campos 
                    lnk_verplanespecial.Enabled = False
                    lnk_verplanpago.Enabled = False
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

                Session("FECHALIBERACION") = Left(Session("rs").Fields("FECHAMAX").value.ToString, 10)
                Session("FECHALIBERACIONCREDITO") = Session("rs").Fields("FECHAPAGO").value.ToString
                lbl_fechainiperiodonormal.Text = Left(Session("FECHALIBERACIONCREDITO").ToString, 10)
                lbl_fechainiperiodomoratorio.Text = Left(Session("FECHALIBERACIONCREDITO").ToString, 10)

            End If
            Session("Con").Close()

        End If

    End Sub

    Private Sub liberarplan()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FECHA_LIBERACION"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            '--Si la suma del capital es igual al monto ya puede generar el plan especial
            If Session("rs").Fields("RESPUESTA").value.ToString = "HABILITAR" Then
                lnk_verplanespecial.Enabled = True
                lnk_verplanpago.Enabled = True
            Else ' Aun no completa el total de los campos 
                lnk_verplanespecial.Enabled = False
                lnk_verplanpago.Enabled = False
            End If
        End If

        Session("Con").Close()
    End Sub

    'Datos de cualquier plan (MIN Y MAX DE LOS INDICES Y TASAS)
    Private Sub InsumosPlan()
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TASA_PERIODO_PESP"
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
                lbl_indicenormal.Text = "UDI"

            End If
            If Session("rs").fields("INDNORIND").value.ToString = "2" Then  'INDICE ES 2 ES TIIE
                lbl_indicenormal.Text = "TIIE 28"

            End If

            If Session("rs").fields("INDNORIND").value.ToString = "3" Then  'INDICE ES 3 ES CETES
                lbl_indicenormal.Text = "CETES 28"

            End If
            '-------------INDICES MORATORIOS INDIZADOS---------------
            If Session("rs").fields("INDMORIND").value.ToString = "1" Then
                lbl_indicemora.Text = "UDI"

            End If
            If Session("rs").fields("INDMORIND").value.ToString = "2" Then
                lbl_indicemora.Text = "TIIE 28"

            End If

            If Session("rs").fields("INDMORIND").value.ToString = "3" Then
                lbl_indicemora.Text = "CETES 28"

            End If




        End If
        Session("Con").Close()

    End Sub

    'MUESTRA EN LOS LBL DE PERIODO INICIAL LA FECHA DE  LAS TASAS 
    Private Sub TASAS()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TASAS_PESP"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_fechainiperiodonormal.Text = Left(Session("rs").Fields("FECHANORMAL").value.ToString, 10)
            lbl_fechainiperiodomoratorio.Text = Left(Session("rs").Fields("FECHAMORA").value.ToString, 10)
        End If

        Session("Con").Close()

    End Sub


    '---------------CODIGO DE PLAN DE PAGOS SALDOS INSOLUTOS---------------

    'Muestra los periodos que fueron configurados en el conf de productos.
    Private Sub Periodicidad()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PERIODICIDAD"
        Session("rs") = Session("cmd").Execute()

        cmb_pagorecu.Items.Clear()
        cmb_tiporecurrencia.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")

        cmb_pagorecu.Items.Add(elija)
        cmb_tiporecurrencia.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("TIPOPERIODO").Value.ToString)
            cmb_pagorecu.Items.Add(item)
            cmb_tiporecurrencia.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Function Validatasa(ByVal tasa As String) As Boolean
        Return Regex.IsMatch(tasa, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function


    '----------------------------------PLAN ESPECIAL-----------------------------------

    Private Sub Muestraperiodicidadcapital()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, cmb_pagorecu.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_PERIODO_INTERES_PESP"
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

    Private Sub Muestraperiodicidadinteres()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporecurrencia.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_PERIODO_INTERES_PESP"
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


    '-------------VALIDACION DE TASA NORMAL Y MORATORIA QUE ESTE EN EL LIMITE CONFIGURADO EN EL CONFIGURADOR DE PRODUCTOS

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

    'Verifica que la tasa normal  esté en el límite establecido
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

    'Llena las tasas del plan especial
    Private Sub Llenatasas()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")

        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CLASIFICACION_TASAS"
        Session("rs") = Session("cmd").Execute()

        cmb_tasamora.Items.Clear()
        cmb_tasanormal.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        Dim elija2 As New ListItem("ELIJA", "0")

        cmb_tasanormal.Items.Add(elija)
        cmb_tasamora.Items.Add(elija2)

        Do While Not Session("rs").eof

            If Session("rs").Fields("TIPO").Value.ToString = "NOR" Then 'normal
                If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                    Dim item As New ListItem("FIJA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                    cmb_tasanormal.Items.Add(item)

                    Session("VARIABLE") = Session("rs").Fields("VARIABLE").Value.ToString
                Else
                    Dim item As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)
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

                    Session("VARIABLEMORA") = Session("rs").Fields("VARIABLE").Value.ToString

                Else
                    Dim itemmor As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                    cmb_tasamora.Items.Add(itemmor)


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


    '-------------------DB GRID--------------------------

    'Muestra los registros de tasas normales que existen en el  plan especial de acuerdo al folio en el DB GRID
    Private Sub muestraintereses()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtplaninteres As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_INTERESES_PESP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtplaninteres, Session("rs"))
        dag_normal.DataSource = dtplaninteres
        dag_normal.DataBind()
        Session("Con").Close()
    End Sub

    'Muestra los registros de tasas moratorias que existen en el  plan especial de acuerdo al folio en el DB GRID
    Private Sub muestrainteresesmoratorios()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtplaninteres As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_INTERESES_MORATORIO_PESP"
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
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MONTOS_PESP"
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
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PAGO_INTERES_PESP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtplan, Session("rs"))
        dag_pagointeres.DataSource = dtplan
        dag_pagointeres.DataBind()
        Session("Con").Close()

    End Sub

    'Actualiza fecha de liberación del préstamo
    Protected Sub lnk_fecha_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_fecha.Click

        lbl_statusapartado.Text = ""
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaliberacion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFEXP_FECHA_LIBERACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            Select Case Session("rs").Fields("RESULTADO").value.ToString
                Case "NOACTUALIZAR"
                    lbl_statusapartado.Text = "Error:Fecha anterior a la fecha de sistema ó excede la fecha de expiración del expediente"
                Case "ACTUALIZAR"
                    lnk_fecha.Enabled = False
                    lnk_verplanpago.Enabled = False
                    Accordion1.Enabled = True
                    Accordion1.SelectedIndex = "0"
                Case "NOACTUALIZARDISP"
                    lbl_statusapartado.Text = "Error:Existen disposiciones generadas, necesita cancelar las disposiciones para cambiar la fecha de pago"
                Case Else
                    lbl_statusapartado.Text = ""
            End Select
        End If

        Session("Con").Close()
        '----------SE MUESTRAN LOS DIFERENTES DB GRID Y SE ACTUALIZA LA FECHA DE LIBERACION

        muestrafechaliberacion()
        muestramontoscapital()
        muestrapagointeres()
        muestraintereses()
        muestrainteresesmoratorios()

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

        If switch = "ON" Then

            Periodicidad()
            lbl_fechainiperiodo.Visible = True
            txt_fechainiperiodo.Visible = True
            'lbl_formato_fecha_ini.Visible = True
            lbl_fechafinperiodo.Visible = True
            txt_fechafinperiodo.Visible = True
            txt_fechafinperiodo.Text = ""
            'lbl_formato_fecha_fin.Visible = True
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
            'lbl_formato_fecha_ini.Visible = True
            lbl_fechafinperiodo.Visible = False
            txt_fechafinperiodo.Visible = False
            'lbl_formato_fecha_fin.Visible = False
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
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
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
            Session("parm") = Session("cmd").CreateParameter("DIABUSCADO", Session("adVarChar"), Session("adParamInput"), 10, "")
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
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 15, "ES")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_ES_CAPITAL"
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

            lbl_suma.Text = "Suma Capital: $ " + Session("PENSIONESG").FormatNumberCurr(Session("rs").Fields("SUMA").value.ToString)
        End If

        Session("Con").Close()

        muestramontoscapital()
        muestrafechaliberacion()
        LimpiaMontos()
        'LimpiaPagointeres()
    End Sub

    'Combo que permite agregar el tipo de pago
    Protected Sub cmb_pagorecu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_pagorecu.SelectedIndexChanged

        Muestraperiodicidadcapital()
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
    Protected Sub cmb_tiporecurrenciacapital_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_tiporecurrenciacapital.SelectedIndexChanged

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



    '------------INTERES------------


    Protected Sub btn_agregarinteres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarinteres.Click
        lbl_statusinteres.Text = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
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
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 15, "ES")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_ES_INTERES"
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

        Muestraperiodicidadinteres()
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


        If switch = "ON" Then

            Periodicidad()
            lbl_fechainicialperiodointeres.Visible = True
            txt_fechainicialperiodointeres.Visible = True
            'lbl_formato_fini_interes.Visible = True
            lbl_fechafinalinteres.Visible = True
            txt_fechafinalinteres.Visible = True
            txt_fechafinalinteres.Text = ""
            'lbl_formato_ffinal_int.Visible = True
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
            'lbl_formato_fini_interes.Visible = True
            lbl_fechafinalinteres.Visible = False
            txt_fechafinalinteres.Visible = False
            txt_fechafinalinteres.Text = ""
            'lbl_formato_ffinal_int.Visible = False
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


    Protected Sub cmb_tiporecurrenciainteres_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_tiporecurrenciainteres.SelectedIndexChanged

        Dim TIPO As String

        TIPO = cmb_tiporecurrenciainteres.SelectedItem.Value.ToString

        Tipo_dia_interes(TIPO)
    End Sub
    '---------------------ELIMINACION DE CAPITAL Y DE PERIODO DE INTERES------------
    'Elimina todo el capital del plan de pago
    Protected Sub btn_eliminarplanpago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminarplanpago.Click
        lbl_capitalstatus.Text = ""
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_MONTO_PESP"
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_suma.Text = "Suma Capital: $0.00 "
        LimpiaPagointeres()
        LimpiaMontos()
        muestramontoscapital()
        muestrafechaliberacion()
        '---DESHABILITO LOS OTROS PLANELES---
        AccordionPane3.Enabled = False
        AccordionPane4.Enabled = False

    End Sub

    'Elimina todo el pago de interes del plan de pago
    Protected Sub Btn_eliminarinteres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_eliminarinteres.Click
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PERIODO_PAGOINT_PESP"
        Session("cmd").Execute()
        Session("Con").Close()
        LimpiaPagointeres()
        muestrapagointeres()
        '---DESHABILITO LOS OTROS PLANELES---

        AccordionPane2.Enabled = False
        AccordionPane3.Enabled = False
        AccordionPane4.Enabled = False

    End Sub

    '-----------------INSERCIÓN DE TASAS NORMAL Y MORATORIA------------
    'COMBO DE TASA FIJA
    Protected Sub cmb_tasanormal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tasanormal.SelectedIndexChanged
        lbl_statusnormal.Text = ""
        InsumosPlan()


        Select Case cmb_tasanormal.SelectedItem.Value.ToString
            Case "FIJ"
                updpnl_tipo_tasas.Visible = True
                txt_tasanormal.Visible = True
                lbl_tasanor.Visible = True
                lbl_indicenormal.Visible = False
                lbl_puntosnormal.Visible = False
                lbl_tasanor.Text = "*(Desde " + Session("TASAMINNORFIJ") + "% hasta " + Session("TASAMAXNORFIJ") + "%):"
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
                lbl_puntosnormal.Text = "*Puntos (Desde " + Session("TASAMINNORIND") + "% hasta " + Session("TASAMAXNORIND") + "%):"
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


        InsumosPlan()
        Select Case cmb_tasamora.SelectedItem.Value.ToString
            Case "FIJ"

                updpnl_moratorio.Visible = True
                txt_tasamora.Visible = True
                lbl_tasamora.Visible = True
                lbl_indicemora.Visible = False
                lbl_puntosmora.Visible = False
                lbl_tasamora.Text = "*(Desde " + Session("TASAMINMORFIJ") + "% hasta " + Session("TASAMAXMORFIJ") + "%):"
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
                lbl_puntosmora.Text = "*(Desde " + Session("TASAMINMORIND") + "% hasta " + Session("TASAMAXMORIND") + "%):"


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


    'Inserta periodo de tasas normales
    Private Sub InsertaPeriodoTasa()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
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
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "ES")
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_ES_TASAS"
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

    'Botón Agregar de tasas normales PESP
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


        liberarplan() 'Verifica si puede generar el plan
    End Sub

    'Inserta periodo de tasas moratorias
    Private Sub InsertaPeriodoTasaMora()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
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
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "ES")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_ES_TASAS"
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

        End If
        Session("Con").Close()
        muestrainteresesmoratorios()
        limpiatasamoratorio()

    End Sub

    '-------SE LIMPIAN LOS RESPECTIVOS CONTROLES DE TASAS NORMALES Y MORATORIAS

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

    '------------------ELIMINACION DE TASAS-------------
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
        liberarplan()
        TASAS() '--mando a llamar para que me muestre el lbl de inicio del periodo de la tasa
    End Sub

    'Elimina las tasas
    Private Sub ELIMINARTASAS(ByVal TIPOTASA As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 10, TIPOTASA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_TASA_PESP"
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

    '-------------LINK VER PLAN Y GUARDAR PLAN
    Protected Sub lnk_verplanespecial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_verplanespecial.Click

        GUARDAR_PLAN_ES()

        If Session("RESPUESTA").ToString = "AGREGAR" Then
            Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
        Else
            lbl_statusfinal.Text = Session("RESPUESTA").ToString
        End If

    End Sub


    Private Sub GUARDAR_PLAN_ES()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_ES"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 50, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString()
        End If

        Session("Con").Close()

    End Sub


    'lnk general del pdf
    Protected Sub lnk_verplanpago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_verplanpago.Click
        Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
    End Sub

End Class