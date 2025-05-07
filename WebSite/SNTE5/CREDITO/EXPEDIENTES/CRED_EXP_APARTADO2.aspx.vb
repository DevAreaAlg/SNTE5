Public Class CRED_EXP_APARTADO2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Plan de pagos", "Plan de Pagos Simple")

        If Not Me.IsPostBack Then
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")
            Session("VENGODE") = "CRED_EXP_APARTADO2.aspx"
            'LLENO COMBO CON LOS TIPOS DE PLAN DE PAGO DISPONIBLES PARA EL PRODUCTO

            Clasificacion()

            TraeMontoPlazo() 'Muestra el monto, el plazo y unidad
            muestrafechaliberacion() 'Muestra la fecha de liberación
            Llenatasas() 'Llena los combos de tasas del plan especial

            tipoplan() 'Habilita o deshabilita el link de ver el plan de pagos general
            Facultad()
            expbloqueado()
        End If


    End Sub

    Private Sub LIMPIAVARIABLES()

        Session("MONTO") = Nothing
        Session("FACULTAD") = Nothing
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
                cmb_tipoplan.Enabled = False
            End If

        End If
        Session("Con").Close()


    End Sub


    '-------------- INFORMACIÓN GENERAL-------------------
    'Combo que llena los tipos de plan existentes en la BD
    Private Sub LlenaTipoPlan()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPOS_PLAN_PAGO_PRODUCTO"

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

    'Habilita los Tabs de acuerdo al plan elegido
    Protected Sub cmb_tipoplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipoplan.SelectedIndexChanged

        lbl_status.Text = ""
        lbl_status_pfsi.Text = ""

        Select Case cmb_tipoplan.SelectedItem.Value.ToString
            Case "SI"

                Periodicidad()
                LimpiaForma()
                upd_pnl_si.Visible = True
                upd_pnl_pfsi.Visible = False

                Select Case Session("TIPO_LINEA")

                    Case "FACT"
                        LlenaTasasFactoraje()
                    Case "INV"
                        LlenaTasasInversion()
                    Case Else

                End Select

            Case "PFSI"

                InsumosPlan()
                Periodicidad()
                LimpiaForma2()
                upd_pnl_pfsi.Visible = True
                upd_pnl_si.Visible = False


                If Session("TIPO_LINEA") = "INV" Then
                    LlenaTasasInversion_PFSI()
                End If


            Case "0"
                upd_pnl_pfsi.Visible = False
                upd_pnl_si.Visible = False

        End Select

    End Sub

    Private Sub Monto_plazo()

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
            lbl_plazo.Text = Session("rs").Fields("PLAZO").Value.ToString
            lbl_lplazo.Text = "*Plazo (" + Session("rs").Fields("UNIDAD").Value.ToString + "):"
            Session("Con").Close()
        End If
    End Sub



    'Muestra el Monto y el Plazo que trae el producto desde el configurador de Productos
    Private Sub TraeMontoPlazo()

        Monto_plazo()
        LlenaTipoPlan()
        muestrafechaliberacion()


    End Sub

    'Valida que la fecha de pago sea menor a la fecha del sistema
    Public Function validacionfecha() As Boolean
        lbl_status.Text = ""
        lbl_status.Text = ""

        Dim resp As Boolean = True

        If cmb_tipoplan.SelectedItem.Value.ToString = "PFSI" Then
            If CDate(txt_fechaliberacion.Text) < CDate(Session("FechaSis")) Then
                lbl_status.Text = "Error:La fecha de pago del préstamo no puede ser anterior a la fecha del sistema"
                resp = False
            End If
        End If

        If cmb_tipoplan.SelectedItem.Value.ToString = "SI" Then

            If CDate(txt_fechaliberacion.Text) < CDate(Session("FechaSis")) Then
                lbl_status.Text = "Error:La fecha de pago del préstamo no puede ser anterior a la fecha del sistema"
                resp = False
            End If
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
                    cmb_tipoplan.Enabled = True

                End If
            End If

            Session("Con").close()

        End If

    End Sub

    'Datos de cualquier plan (MIN Y MAX DE LOS INDICES Y TASAS)
    Private Sub InsumosPlan()
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
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

                lbl_indicenorsi.Text = "UDI +"
            End If
            If Session("rs").fields("INDNORIND").value.ToString = "2" Then  'INDICE ES 2 ES TIIE

                lbl_indicenorsi.Text = "TIIE 28 +"
            End If

            If Session("rs").fields("INDNORIND").value.ToString = "3" Then  'INDICE ES 3 ES CETES

                lbl_indicenorsi.Text = "CETES 28 +"
            End If
            '-------------INDICES MORATORIOS INDIZADOS---------------
            If Session("rs").fields("INDMORIND").value.ToString = "1" Then

                lbl_indicemorsi.Text = "UDI +"
            End If
            If Session("rs").fields("INDMORIND").value.ToString = "2" Then

                lbl_indicemorsi.Text = "TIIE 28 +"
            End If

            If Session("rs").fields("INDMORIND").value.ToString = "3" Then

                lbl_indicemorsi.Text = "CETES 28 +"
            End If


            lbl_tasanorpfsi.Text = "*Tasa ordinaria: (Desde " + Session("TASAMINNORFIJ") + "% hasta " + Session("TASAMAXNORFIJ") + "%)"
            lbl_tasamorpfsi.Text = "*Tasa moratoria: (Desde " + Session("TASAMINMORFIJ") + "% hasta " + Session("TASAMAXMORFIJ") + "%)"


        End If
        Session("Con").Close()

    End Sub

    '---------------CODIGO DE PLAN DE PAGOS SALDOS INSOLUTOS---------------

    'Combo de tasas normales
    Protected Sub cmb_tipotasasi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipotasasi.SelectedIndexChanged
        lbl_status.Text = ""

        Select Case cmb_tipotasasi.SelectedItem.Value.ToString
            Case "FIJ"
                InsumosPlan()
                lbl_indicenorsi.Visible = False
                lbl_tasasi.Text = "(Desde " + Session("TASAMINNORFIJ") + "% hasta " + Session("TASAMAXNORFIJ") + "%)"
                txt_tasasi.Enabled = True
                txt_tasasi.Text = ""

            Case "IND"
                InsumosPlan()

                lbl_indicenorsi.Visible = True
                lbl_tasasi.Text = "Puntos (Desde " + Session("TASAMINNORIND") + " hasta " + Session("TASAMAXNORIND") + ")"
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

    'combo de tasas moratorias
    Protected Sub cmb_tipotasamorsi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipotasamorsi.SelectedIndexChanged

        Select Case cmb_tipotasamorsi.SelectedItem.Value.ToString
            Case "FIJ"
                InsumosPlan()

                lbl_indicemorsi.Visible = False
                lbl_tasamorsi.Text = "(Desde " + Session("TASAMINMORFIJ") + "% hasta " + Session("TASAMAXMORFIJ") + "%)"
                txt_tasamorsi.Enabled = True
                txt_tasamorsi.Text = ""
            Case "IND"

                InsumosPlan()
                lbl_indicemorsi.Visible = True
                lbl_tasamorsi.Text = "Puntos (Desde " + Session("TASAMINMORIND") + " hasta " + Session("TASAMAXMORIND") + ")"
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

        cmb_tipopersi.Items.Clear()
        cmb_tipoperiodicidad.Items.Clear()


        Dim elija As New ListItem("ELIJA", "0")
        cmb_tipopersi.Items.Add(elija)
        cmb_tipoperiodicidad.Items.Add(elija)


        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("TIPOPERIODO").Value.ToString)
            cmb_tipopersi.Items.Add(item)
            cmb_tipoperiodicidad.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub InsumosSI()

        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPPERIODO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipopersi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TASA_PERIODO_SI"
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
        InsumosSI() 'Obtiene los numeros de periodicidad de acuerdo a los configurados
        Monto_plazo()
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
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CNFEXP_MAX_RECURRENCIA"
                Session("rs") = Session("cmd").Execute()
                Session("maxdiasmes") = Session("rs").fields("MAXIMO").value.ToString
                Session("Con").close()
                Session("cuentadiasx") = 0
                'lleno el combo con el numero de dias en el mes
                LlenaCmbXdia()
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
                lbl_errortasasi.Text = "Error: Los puntos de la tasa ordinaria no están en los límites establecidos "
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
                lbl_errortasamorsi.Text = "Error: Los puntos de la tasa moratoria no están en los límites establecidos "
                COUNT = 0
            End If

        End If

        Return COUNT
    End Function

    Private Function Validatasa(ByVal tasa As String) As Boolean
        Return Regex.IsMatch(tasa, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Function Validatasa_renovacion(ByVal FOLIO As String, ByVal tasa As String, ByVal Indice As String, ByVal tasa_mora As String, ByVal Indice_mora As String) As Boolean

        Dim validacion As Boolean

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, tasa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, Indice)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA_MORA", Session("adVarChar"), Session("adParamInput"), 20, tasa_mora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICE_MORA", Session("adVarChar"), Session("adParamInput"), 20, Indice_mora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RENOVACION_TASAS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            If Session("rs").fields("VALIDACION").value.ToString = "1" Then
                validacion = True
            Else
                validacion = False
            End If

        End If

        Session("Con").close()

        Return validacion
    End Function

    Private Sub GUARDAR_PLAN_SI()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_SI"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasasi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipotasasi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))


        If cmb_tipotasasi.SelectedItem.Value.ToString = "FIJ" Then

            Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, Session("INDNORIND").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasasi.Text))
            Session("cmd").Parameters.Append(Session("parm"))
        End If



        Session("parm") = Session("cmd").CreateParameter("TASAMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASAMOR", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipotasamorsi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        If cmb_tipotasamorsi.SelectedItem.Value.ToString = "FIJ" Then
            Session("parm") = Session("cmd").CreateParameter("IDINDICEMOR", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOSMOR", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDINDICEMOR", Session("adVarChar"), Session("adParamInput"), 20, Session("INDMORIND").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOSMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorsi.Text))
            Session("cmd").Parameters.Append(Session("parm"))

        End If

        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "SI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERIODO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipopersi.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 20, cmb_periodicidadSI.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("Con").close()
    End Sub

    Private Sub GUARDAR_PLAN_SI_DIAX()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_SI_DIAX"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasasi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipotasasi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_tipotasasi.SelectedItem.Value.ToString = "FIJ" Then
            Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
        Else

            Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, Session("INDNORIND").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasasi.Text))
            Session("cmd").Parameters.Append(Session("parm"))
        End If



        Session("parm") = Session("cmd").CreateParameter("TASAMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASAMOR", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipotasamorsi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_tipotasamorsi.SelectedItem.Value.ToString = "FIJ" Then
            Session("parm") = Session("cmd").CreateParameter("IDINDICEMOR", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOSMOR", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDINDICEMOR", Session("adVarChar"), Session("adParamInput"), 20, Session("INDMORIND").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOSMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorsi.Text))
            Session("cmd").Parameters.Append(Session("parm"))

        End If

        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "SI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CADENA", Session("adVarChar"), Session("adParamInput"), 50, Session("diaspago"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()


    End Sub

    Private Sub GUARDAR_PLAN_SI_FACTORAJE()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_FACT_CNFEXP_PLAN_PAGOS_SI"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasasi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipotasasi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_tipotasasi.SelectedItem.Value.ToString = "FIJ" Then
            Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
        Else

            Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, Session("INDNORIND").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasasi.Text))
            Session("cmd").Parameters.Append(Session("parm"))
        End If



        Session("parm") = Session("cmd").CreateParameter("TASAMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASAMOR", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipotasamorsi.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_tipotasamorsi.SelectedItem.Value.ToString = "FIJ" Then
            Session("parm") = Session("cmd").CreateParameter("IDINDICEMOR", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOSMOR", Session("adVarChar"), Session("adParamInput"), 20, "0")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDINDICEMOR", Session("adVarChar"), Session("adParamInput"), 20, Session("INDMORIND").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUNTOSMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorsi.Text))
            Session("cmd").Parameters.Append(Session("parm"))

        End If

        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "SI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERIODO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipopersi.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 20, cmb_periodicidadSI.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

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

    '----------------CODIGO DE PLAN DE PAGOS FIJOS---------------------------------

    'Datos del plan de Pagos Fijos Saldos insolutos
    Private Sub InsumosPFSI()
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipoperiodicidad.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TASA_PERIODO_PFSI"
        Session("rs") = Session("cmd").Execute()

        cmb_periodicidad.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_periodicidad.Items.Add(elija)


        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").value.ToString + " " + Session("rs").Fields("UNIDAD").Value.ToString, Session("rs").Fields("PERIODO").Value.ToString)
            cmb_periodicidad.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'De acuerdo al tipo de periodicidad muestra en el cmb_periodicidad las periodicidades permitidas
    Protected Sub cmb_tipoperiodicidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipoperiodicidad.SelectedIndexChanged
        lbl_status_pfsi.Text = ""
        lbl_errortasanorpfsi.Text = ""
        lbl_errortasamorpfsi.Text = ""
        Monto_plazo()
        InsumosPFSI()

        'Agrega al combo de Periodicidad los periodos permitidos según sea el Caso
        Select Case cmb_tipoperiodicidad.SelectedItem.Value

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
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CNFEXP_MAX_RECURRENCIA"
                Session("rs") = Session("cmd").Execute()
                Session("maxdiasmes") = Session("rs").fields("MAXIMO").value.ToString
                Session("Con").close()
                Session("cuentadiasx") = 0
                'lleno el combo con el numero de dias en el mes
                LlenaCmbXdia()
        End Select

    End Sub

    'Llena los dias a elegir del plan especial
    Private Sub LlenaCmbXdia()

        cmb_periodicidad.Items.Clear()
        cmb_periodicidadSI.Items.Clear()
        Dim z As String
        'Ciclo que me permite agregar al combo del 1 al 31
        For count = 1 To 28

            z = "DIA " + count.ToString + " DEL MES"
            Dim y As New ListItem(z, count.ToString)
            cmb_periodicidad.Items.Add(y)
            cmb_periodicidadSI.Items.Add(y)
        Next
        'Agrega al combo el mensaje Dia ultimo de mes
        Dim ultimo As New ListItem("DIA ULTIMO DE MES", 32)
        cmb_periodicidad.Items.Add(ultimo)
        cmb_periodicidadSI.Items.Add(ultimo)

        'Si la sesion dias pago no esta vacia quito el dia del combo
        If Session("DIASPAGO").ToString.Length <> 0 Then

            Dim arre = Split(Session("DIASPAGO").ToString, "-")
            Dim Contadornumeros As Integer = 0

            Do While arre(Contadornumeros) <> Nothing
                'Permite remover del combo el index seleccionado del combo de periodicidades encontrando primero el valor y posteriormente su index
                cmb_periodicidad.Items.RemoveAt(cmb_periodicidad.Items.IndexOf(cmb_periodicidad.Items.FindByValue(arre(Contadornumeros).ToString)))
                cmb_periodicidadSI.Items.RemoveAt(cmb_periodicidadSI.Items.IndexOf(cmb_periodicidadSI.Items.FindByValue(arre(Contadornumeros).ToString)))

                'Incremento variable
                Contadornumeros = Contadornumeros + 1
            Loop


        End If

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

    Private Sub GUARDA_PFSI()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_PFSI"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasanorpfsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(txt_tasamorpfsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "PFSI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERIODO", Session("adVarChar"), Session("adParamInput"), 10, Left(cmb_tipoperiodicidad.SelectedItem.Value, 4))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 20, CInt(cmb_periodicidad.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub GUARDA_PFSI_DIAX()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_PAGOS_PFSI_DIAX"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, CDec(txt_tasanorpfsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMOR", Session("adVarChar"), Session("adParamInput"), 10, CDec(txt_tasamorpfsi.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CADENA", Session("adVarChar"), Session("adParamInput"), 50, Session("diaspago"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 10, "PFSI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub


    'Llama las funciones validación y generarImprimirPlanPagoFijo
    Protected Sub lnk_generar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar.Click
        Dim count As Integer
        lbl_status_pfsi.Text = ""

        Dim tasaNorpfsi As String
        Dim tasaMorpfsi As String
        Dim TipoPerpfsi As String
        tasaNorpfsi = txt_tasanorpfsi.Text
        tasaMorpfsi = txt_tasamorpfsi.Text
        TipoPerpfsi = cmb_tipoperiodicidad.SelectedItem.Value.ToString

        If validacion_fecha_pago(txt_fechaliberacion.Text) = "ACTUALIZAR" Then



            If Validatasa(tasaNorpfsi) = True Or Validatasa(tasaMorpfsi) = True Then

                count = validacion(tasaNorpfsi, tasaMorpfsi) 'Primero Verifica que la tasa y la periodicidad este en el margen establecido

                If count = 1 And validacionfecha() = True Then
                    If Validatasa_renovacion(Session("FOLIO"), tasaNorpfsi, 0, tasaMorpfsi, 0) = True Then

                        'Si es especial (DIAX) entonces se genera un SP
                        If TipoPerpfsi = "DIAX" Then
                            If lbl_diaspago.Text <> "" Then
                                GUARDA_PFSI_DIAX()
                                Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
                                lnk_verplanpago.Enabled = True
                            Else
                                lbl_status_pfsi.Text = "Error: Capture días de pago en el mes"
                            End If

                        Else ' Es pagos fijos
                            GUARDA_PFSI()
                            Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
                            lnk_verplanpago.Enabled = True
                        End If
                    Else
                        lbl_status_pfsi.Text = "Error: Las tasas capturadas son diferentes al préstamo que dio origen la renovación"
                    End If
                End If
            Else
                lbl_status_pfsi.Text = "Error: Verifique la tasa"
            End If
        Else
            lbl_status_pfsi.Text = "Error:Fecha anterior a la fecha de sistema ó excede la fecha de expiración del expediente"
        End If
    End Sub


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


    '-------------VALIDACION DE TASA NORMAL Y MORATORIA QUE ESTE EN EL LIMITE CONFIGURADO EN EL CONFIGURADOR DE PRODUCTOS


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

        cmb_tipotasasi.Items.Clear()
        cmb_tipotasamorsi.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        Dim elija2 As New ListItem("ELIJA", "0")

        cmb_tipotasasi.Items.Add(elija)
        cmb_tipotasamorsi.Items.Add(elija2)



        Do While Not Session("rs").eof

            If Session("rs").Fields("TIPO").Value.ToString = "NOR" Then 'normal
                If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                    Dim item As New ListItem("FIJA", Session("rs").Fields("CLASIFICACION").Value.ToString)

                    cmb_tipotasasi.Items.Add(item)
                    Session("VARIABLE") = Session("rs").Fields("VARIABLE").Value.ToString
                Else
                    Dim item As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)

                    cmb_tipotasasi.Items.Add(item)
                    Session("VARIABLEIND") = Session("rs").Fields("VARIABLE").value.ToString
                End If
                If Not Session("rs").EOF() Then
                    Session("VARIABLEIND") = Session("rs").Fields("VARIABLE").value.ToString
                End If
            Else 'moratorio
                If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                    Dim itemmor As New ListItem("FIJA", Session("rs").Fields("CLASIFICACION").value.ToString)

                    cmb_tipotasamorsi.Items.Add(itemmor)
                    Session("VARIABLEMORA") = Session("rs").Fields("VARIABLE").Value.ToString

                Else
                    Dim itemmor As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)

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


    Private Function validacion_fecha_pago(ByVal FechaPago As String) As String


        Dim resultado As String = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAPAGO", Session("adVarChar"), Session("adParamInput"), 10, FechaPago)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFEXP_FECHA_LIBERACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            resultado = Session("rs").Fields("RESULTADO").value.ToString()
        End If
        Session("Con").Close()


        Return resultado

    End Function


    '***********************************************PDF GENERAL
    'Obtiene que tipo de plan es
    Private Sub tipoplan()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_PLANPAGO"
        Session("rs") = Session("cmd").Execute()

        'SI EXISTE UN PLAN DE PAGOS SE HABILITA EL LINK PARA VER EL PLAN DE PAGOS
        If Session("rs").Fields("RESPUESTA").value.ToString = "EXISTE" Then
            lnk_verplanpago.Enabled = True

        End If
        Session("Con").Close()
    End Sub

    'lnk general del pdf
    Protected Sub lnk_verplanpago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_verplanpago.Click
        Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
    End Sub

    Protected Sub LlenaTasasFactoraje()

        Dim TipoTasaOrd As String
        Dim TipoTasaMora As String
        Dim TasaOrd As Decimal
        Dim TasaMora As Decimal
        Dim IndOrd As Integer
        Dim IndMora As Integer
        Dim PuntosOrd As Decimal
        Dim PuntosMora As Decimal

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO_LINEA"))

        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FACT_DATOS_TASAS"
        Session("rs") = Session("cmd").Execute()

        TipoTasaOrd = Session("rs").fields("TIPOTASAORD").value.ToString
        TipoTasaMora = Session("rs").fields("TIPOTASAMORA").value.ToString
        TasaOrd = Session("rs").fields("TASA_ORD").value.ToString
        TasaMora = Session("rs").fields("TASAMORA").value.ToString
        IndOrd = Session("rs").fields("INDORD").value.ToString
        IndMora = Session("rs").fields("INDMORA").value.ToString
        PuntosOrd = Session("rs").fields("PUNTOSORD").value.ToString
        PuntosMora = Session("rs").fields("PUNTOSMORA").value.ToString

        cmb_tipotasasi.SelectedItem.Value = TipoTasaOrd
        cmb_tipotasasi.Items.FindByValue(TipoTasaOrd).Selected = True

        cmb_tipotasamorsi.SelectedItem.Value = TipoTasaMora
        cmb_tipotasamorsi.Items.FindByValue(TipoTasaMora).Selected = True

        Session("Con").Close()

        Select Case cmb_tipotasasi.SelectedItem.Value.ToString
            Case "FIJ"

                cmb_tipotasasi.SelectedItem.Text = "FIJA"
                InsumosPlan()
                lbl_indicenorsi.Visible = False
                lbl_tasasi.Text = "(Desde " + Session("TASAMINNORFIJ") + "% hasta " + Session("TASAMAXNORFIJ") + "%)"
                txt_tasasi.Enabled = False
                txt_tasasi.Text = TasaOrd

            Case "IND"

                cmb_tipotasasi.SelectedItem.Text = "INDIZADA"
                InsumosPlan()

                lbl_indicenorsi.Visible = True
                lbl_tasasi.Text = "Puntos (Desde " + Session("TASAMINNORIND") + " hasta " + Session("TASAMAXNORIND") + ")"
                txt_tasasi.Enabled = True
                txt_tasasi.Text = ""
                txt_tasasi.Text = PuntosOrd


            Case "0"
                lbl_tasasi.Text = "(Desde - % hasta - %)"
                lbl_indicenorsi.Text = ""
                lbl_indicenorsi.Visible = False
                txt_tasasi.Enabled = False
                txt_tasasi.Text = ""
        End Select


        Select Case cmb_tipotasamorsi.SelectedItem.Value.ToString
            Case "FIJ"

                cmb_tipotasamorsi.SelectedItem.Text = "FIJA"
                InsumosPlan()

                lbl_indicemorsi.Visible = False
                lbl_tasamorsi.Text = "(Desde " + Session("TASAMINMORFIJ") + "% hasta " + Session("TASAMAXMORFIJ") + "%)"
                txt_tasamorsi.Enabled = False
                txt_tasamorsi.Text = TasaMora
            Case "IND"
                cmb_tipotasamorsi.SelectedItem.Text = "INDIZADA"
                InsumosPlan()
                lbl_indicemorsi.Visible = True
                lbl_tasamorsi.Text = "Puntos (Desde " + Session("TASAMINMORIND") + " hasta " + Session("TASAMAXMORIND") + ")"
                txt_tasamorsi.Enabled = False
                txt_tasamorsi.Text = PuntosMora

            Case "0"
                lbl_tasamorsi.Text = "(Desde - % hasta - %)"
                lbl_indicemorsi.Text = ""
                lbl_indicemorsi.Visible = False
                txt_tasamorsi.Enabled = False
                txt_tasamorsi.Text = ""


        End Select


        cmb_tipotasasi.Enabled = False
        txt_tasasi.Enabled = False

        cmb_tipotasamorsi.Enabled = False
        txt_tasamorsi.Enabled = False


    End Sub

    Protected Sub LlenaTasasInversion()

        Dim TipoTasaOrd As String
        Dim TasaOrd As Decimal

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INVPER_EXP_OTORCRED_TASAS"
        Session("rs") = Session("cmd").Execute()

        TipoTasaOrd = Session("rs").fields("TIPOTASAORD").value.ToString
        TasaOrd = Session("rs").fields("TASA_ORD").value.ToString

        cmb_tipotasasi.SelectedItem.Value = TipoTasaOrd
        cmb_tipotasasi.Items.FindByValue(TipoTasaOrd).Selected = True

        Session("Con").Close()

        Select Case cmb_tipotasasi.SelectedItem.Value.ToString
            Case "FIJ"
                cmb_tipotasasi.SelectedItem.Text = "FIJA"
                InsumosPlan()
                lbl_indicenorsi.Visible = False
                lbl_tasasi.Text = "(Desde " + Session("TASAMINNORFIJ") + "% hasta " + Session("TASAMAXNORFIJ") + "%)"
                txt_tasasi.Enabled = False
                txt_tasasi.Text = TasaOrd

            Case "IND"
                cmb_tipotasasi.SelectedItem.Text = "INDIZADA"
                InsumosPlan()

                lbl_indicenorsi.Visible = True
                lbl_tasasi.Text = "Puntos (Desde " + Session("TASAMINNORIND") + " hasta " + Session("TASAMAXNORIND") + ")"
                txt_tasasi.Enabled = False
                txt_tasasi.Text = ""
                txt_tasasi.Text = TasaOrd
            Case "0"
                lbl_tasasi.Text = "(Desde - % hasta - %)"
                lbl_indicenorsi.Text = ""
                lbl_indicenorsi.Visible = False
                txt_tasasi.Enabled = False
                txt_tasasi.Text = ""
        End Select


        cmb_tipotasasi.Enabled = False
        txt_tasasi.Enabled = False

    End Sub

    Protected Sub LlenaTasasInversion_PFSI()

        Dim TipoTasaOrd As String
        Dim TasaOrd As Decimal

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INVPER_EXP_OTORCRED_TASAS"
        Session("rs") = Session("cmd").Execute()

        TipoTasaOrd = Session("rs").fields("TIPOTASAORD").value.ToString
        TasaOrd = Session("rs").fields("TASA_ORD").value.ToString

        Session("Con").Close()

        If TipoTasaOrd = "FIJ" Then
            txt_tasanorpfsi.Text = TasaOrd
            txt_tasanorpfsi.Enabled = False
        Else
            lbl_status_pfsi.Text = "Error: No se puede usar este tipo de plan, La inversión cuenta con tasas indizadas, Genere un Plan de Pagos Saldos Insolutos."
            lnk_generar.Enabled = False
        End If

    End Sub


    Protected Sub lnk_generar_si_Click(sender As Object, e As EventArgs) Handles lnk_generar_si.Click

        Dim count As Integer
        Dim tasa As String
        Dim indice As String
        Dim tasa_mora As String
        Dim indice_mora As String
        Dim Tipotasa As String
        Dim TipotasaMor As String
        Dim TipoPeriodicidad As String

        tasa = txt_tasasi.Text
        tasa_mora = txt_tasamorsi.Text
        Tipotasa = cmb_tipotasasi.SelectedItem.Value.ToString
        TipotasaMor = cmb_tipotasamorsi.SelectedItem.Value.ToString
        TipoPeriodicidad = cmb_tipopersi.SelectedItem.Value.ToString
        lbl_status.Text = ""
        lbl_status.Visible = True

        If validacion_fecha_pago(txt_fechaliberacion.Text) = "ACTUALIZAR" Then

            If Validatasa(tasa) = True And Validatasa(tasa_mora) = True Then

                count = validacionSI(Tipotasa, tasa, TipotasaMor, tasa_mora) 'Primero Verifica que la tasa y la periodicidad este en el margen establecido

                If Tipotasa = "FIJ" Then
                    indice = 0
                    indice_mora = 0
                Else
                    indice = Session("INDNORIND").ToString
                    indice_mora = Session("INDMORIND").ToString

                End If


                If Validatasa_renovacion(Session("FOLIO"), tasa, indice, tasa_mora, indice_mora) = True Then

                    If count = 1 And validacionfecha() = True Then
                        If TipoPeriodicidad = "DIAX" Then
                            If lbl_diaspago_SI.Text <> "" Then
                                GUARDAR_PLAN_SI_DIAX()
                                Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
                                lnk_verplanpago.Enabled = True
                            Else
                                lbl_status.Text = "Error: Capture días de pago en el mes"
                            End If
                        Else
                            If Session("TIPO_LINEA") = "FACT" Then
                                GUARDAR_PLAN_SI_FACTORAJE()
                                Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
                                lnk_verplanpago.Enabled = True
                            Else
                                GUARDAR_PLAN_SI()
                                Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
                                lnk_verplanpago.Enabled = True
                            End If
                        End If
                    End If
                Else
                    lbl_status.Text = "Error: Las tasas capturadas son diferentes al préstamo que dio origen la renovación"
                End If
            Else
                lbl_status.Text = "Error: Verifique la tasa"
            End If
        Else
            lbl_status.Text = "Error:Fecha anterior a la fecha de sistema ó excede la fecha de expiración del expediente"
        End If

    End Sub

End Class