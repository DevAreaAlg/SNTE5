Imports System.IO
Imports System.Windows.Forms
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports WnvWordToPdf
Public Class CRED_EXP_COTIZADOR_TAB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Cotizador", "COTIZADOR INSTITUCIONAL")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + btn_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica(1)")

        If Session("idperbusca") <> "" Then
            tbx_rfc.Text = Session("idperbusca")
            Session("idperbusca") = ""
        End If
        lnk_resumen.Attributes.Add("OnClick", "ResumenPersona()")


    End Sub

    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_seleccionar.Click
        lbl_status_pfsi.Text = ""
        lbl_status_general.Text = ""
        obtieneId()
        If txt_cliente.Text = "" Then
            lbl_status_general.Text = "Error: Validar el RFC debido a que el agremiado no existe en la base de datos o es incorrecto."
            cmb_Producto.Enabled = False
        Else
            Session("PERSONAID") = CInt(txt_cliente.Text)
            Llenadatos(CInt(txt_cliente.Text))
            cmb_Producto.Enabled = True
            LlenarProductos()
        End If

    End Sub


    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_cliente.Text = ""
        Else
            txt_cliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Private Sub LimpiaGenerales()

        txt_plazo.Text = ""
        txt_tasa.Text = ""
        txt_monto.Text = ""
        txt_sueldo.Text = ""
        txt_tipo_trabajador.Text = ""
        cmb_Producto.ClearSelection()
        'lnk_descargar.Visible = False
        dag_sugeridor.DataSource = Nothing
        dag_sugeridor.DataBind()
        lbl_rango_plazo.Text = ""
        lbl_rango_monto.Text = ""
        txt_cappago.Text = ""
        txb_saldo_restante.Text = ""

    End Sub

    Private Sub LlenarProductos()

        cmb_Producto.Items.Clear()

        Dim elija As New WebControls.ListItem("ELIJA", "0")
        cmb_Producto.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDESTINO", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, "F")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRODUCTOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            Dim item As New WebControls.ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_Producto.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub Llenadatos(ByVal idPersona As Integer)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, idPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_GNRAL_CLIENTE"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("NOMBRE").Value.ToString = "-1" Then
            lbl_status_general.Text = "El cliente introducido no existe"
            lbl_sueldo.Text = "*Sueldo neto quincenal:"
            lbl_cliente.Text = ""
            txt_monto.Text = ""
            txt_sueldo.Text = ""
            txt_tipo_trabajador.Text = ""
            txt_tasa.Text = ""

            cmb_Producto.ClearSelection()
            dag_sugeridor.Visible = False
            dag_sugeridor.DataSource = Nothing
            dag_sugeridor.DataBind()
            'lnk_descargar.Visible = False
            lnk_resumen.Enabled = False
            Session("Con").Close()
            Exit Sub
        Else

            Session("CLIENTE") = Session("rs").Fields("NOMBRE").Value
            lbl_cliente.Text = Session("rs").Fields("NOMBRE").Value

            lnk_resumen.Enabled = True
        End If

        Session("Con").Close()


        pnl_cotizador.Visible = True
        LimpiaGenerales()

    End Sub

    Protected Sub cmb_Producto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Producto.SelectedIndexChanged

        LimpiarCampos()



        If ValidarListaNegra() = "OK" Then

            If cmb_Producto.SelectedItem.Value.ToString <> 0 Then
                If cmb_Producto.SelectedItem.Value.ToString = "2" Then
                    lbl_status_pfsi.Text = "Error: Periodo para PAs no activo"
                    Exit Sub
                End If

                Dim idProducto As Integer = cmb_Producto.SelectedItem.Value.ToString
                Dim idPlaza As Integer = Session("PERSONAID").ToString

                If ValidarPrestamo(idProducto, idPlaza) = "OK" Then

                    TasaPFSI(idProducto, idPlaza)

                    Dim monto As Decimal
                    Dim tasa As Decimal
                    Dim tipoPersonal As String
                    Dim idSuc As Integer
                    Dim sueldo As Decimal

                    sueldo = CDec(txt_sueldo.Text)
                    tasa = CDec(txt_tasa.Text)
                    tipoPersonal = txt_tipo_trabajador.Text

                    idSuc = CInt(Session("SUCID"))
                    monto = 0.00

                    If Validaciones(idProducto, sueldo, tipoPersonal, tasa, idSuc, monto) = "OK" Then
                        btn_cotizar.Enabled = True
                        RangosPlazo(idProducto, CInt(txt_cliente.Text))
                        RangosMonto(idProducto, CInt(txt_cliente.Text))


                    Else
                        lbl_status_pfsi.Text = ViewState("RESPUESTA")
                        txt_monto.Text = ""
                        txt_plazo.Text = ""
                        lbl_rango_plazo.Text = ""
                        lbl_rango_monto.Text = ""
                        btn_cotizar.Enabled = False
                    End If

                ElseIf ValidarPrestamo(idProducto, idPlaza) = "PD_AGRE" Then
                    lbl_status_pfsi.Text = "Error: El Agremiado tiene un PRESTAMO DE AHORRO DEUDOR."
                Else
                    lbl_status_pfsi.Text = "Error: El Agremiado ya cuenta con un " + cmb_Producto.SelectedItem.Text.ToString + "."
                End If

            Else
                LimpiaGenerales()
                End If

            Else
                If ValidarListaNegra() = "PREJUBILADO" Then
                    lbl_status_pfsi.Text = "Error: El Agremiado no puede cotizar debido a que está en Licencia Prejubilatoria."
                ElseIf ValidarListaNegra() = "PLAZOPA" Then
                    lbl_status_pfsi.Text = "Error: Periodo para PAs no activo"
                Else
                    lbl_status_pfsi.Text = "Error: El Agremiado no tiene información suficiente para cotizar."
                End If

        End If
    End Sub

    Private Sub LimpiarCampos()

        If cmb_Producto.SelectedItem.Text = "PRESTAMO COMPLEMENTARIO" Then
            lbl_sueldo.Text = "*Aportaciones totales:"
            lbl_cappago.Text = "Pensión Alimentacia:"
        Else
            lbl_sueldo.Text = "*Sueldo neto quincenal:"
            lbl_cappago.Text = "*Capacidad de pago:"
        End If

        lbl_rango_monto.Text = ""
        lbl_rango_plazo.Text = ""

        txt_sueldo.Text = ""
        txt_tipo_trabajador.Text = ""
        txt_tasa.Text = ""
        txt_monto.Text = ""
        txt_plazo.Text = ""
        txt_cappago.Text = ""
        txb_saldo_restante.Text = ""

        dag_sugeridor.DataSource = Nothing
        dag_sugeridor.DataBind()

    End Sub

    Private Function ValidarListaNegra() As String

        Dim Resultado As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_BUSCAR_AGREMIADO_BLACK_LIST"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESPUESTA").value.ToString
        Session("Con").Close()

        Return Resultado

    End Function

    Private Function ValidarPrestamo(ByVal IdProducto As Integer, ByVal IdPersona As Integer) As String

        Dim Resultado As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, IdProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, IdPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_BUSCAR_PRESTAMO_AGREMIADO"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESULTADO").value.ToString
        Session("Con").Close()

        Return Resultado

    End Function

    'Rango de los montos
    Private Sub RangosPlazo(ByVal idprod As Integer, ByVal idPersona As Integer)
        'LBL_ESTA.Text = " IDPROD: " + CStr(idprod) + " IDPERSONA: " + CStr(idPersona) + " IDSUC: " + Session("SUCID").ToString + " IDUSER: " + Session("USERID").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idprod)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, idPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RANGOS_PLAZO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_rango_plazo.Text = "(Desde: " + (CDec(Session("rs").Fields("PLAZO_MIN").value) / 15).ToString + " hasta: " + (CDec(Session("rs").Fields("PLAZO_MAX").value) / 15).ToString + " Quincenas)"
            ViewState("PLAZOMIN") = (CDec(Session("rs").Fields("PLAZO_MIN").value) / 15).ToString
            ViewState("PLAZOMAX") = (CDec(Session("rs").Fields("PLAZO_MAX").value) / 15).ToString
        End If
        Session("Con").Close()
    End Sub

    Private Sub RangosMonto(ByVal idprod As Integer, ByVal idPersona As Integer)
        'LBL_ESTA.Text = " IDPROD: " + CStr(idprod) + " IDPERSONA: " + CStr(idPersona) + " IDSUC: " + Session("SUCID").ToString + " IDUSER: " + Session("USERID").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idprod)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, idPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RANGOS_MONTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            If CDec(Session("rs").Fields("MONTO_MIN").value) <= CDec(Session("rs").Fields("MONTO_MAX").value) And Session("SUELDO") >= 1000 Then
                txt_monto.Enabled = True
                txt_plazo.Enabled = True
                lbl_rango_monto.Text = "(Desde: " + FormatCurrency(CDec(Session("rs").Fields("MONTO_MIN").value)).ToString + " hasta: " + FormatCurrency(CDec(Session("rs").Fields("MONTO_MAX").value)).ToString + ")"
                ViewState("MONTOMIN") = (CDec(Session("rs").Fields("MONTO_MIN").value)).ToString
                ViewState("MONTOMAX") = (CDec(Session("rs").Fields("MONTO_MAX").value)).ToString
                lbl_status_pfsi.Text = "Error: El plazo está cerca de fecha de corte o fecha de término"
                lbl_status_pfsi.Text = ""
            Else
                lbl_rango_monto.Text = "Desde $0.00 hasta $0.00"
                If ViewState("PLAZOMAX") = 0 Then

                Else
                    lbl_status_pfsi.Text = "Error: No cumple con la liquidez mínima requerida"
                End If

                txt_monto.Enabled = False
                txt_plazo.Enabled = False
                btn_cotizar.Enabled = False
            End If
        End If
        Session("Con").Close()
    End Sub

    'Datos de cualquier plan (MIN Y MAX DE LOS INDICES Y TASAS)
    Private Sub TasaPFSI(ByVal idProducto As Integer, ByVal idPlaza As Integer)

        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, idProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPLAZA", Session("adVarChar"), Session("adParamInput"), 10, idPlaza)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COTIZADOR_TASA_PERIODO_SI"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            Session("TIPONORIND") = Session("rs").fields("TIPONORIND").value.ToString
            Session("TASAMINNORIND") = Session("rs").fields("TASAMINNORIND").value.ToString
            Session("TASAMAXNORIND") = Session("rs").fields("TASAMAXNORIND").value.ToString
            Session("VALORNORIND") = Session("rs").fields("VALORNORIND").value.ToString

            If Session("rs").fields("TASAMAXNORFIJ").value.ToString <> 0.00 Then
                txt_tasa.Text = Session("rs").fields("TASAMAXNORFIJ").value.ToString
            Else
                txt_tasa.Text = Session("rs").fields("TASAMAXNORIND").value.ToString
                lbl_tasa.Text = "*Tasa ordinaria: (Desde " + Session("TIPONORIND") + " +" + Session("TASAMINNORIND") + "% hasta " + Session("TIPONORIND") + " +" + Session("TASAMAXNORIND") + "%)"
            End If

            Session("SUELDO") = CDec((Session("rs").fields("SUELDO").value.ToString))
            txt_sueldo.Text = FormatCurrency(Session("rs").fields("SUELDO").value.ToString)
            txt_tipo_trabajador.Text = Session("rs").fields("TIPO_PER").value.ToString
            txt_cappago.Text = FormatCurrency(Session("rs").fields("CAPPAGO").value.ToString)
            txb_saldo_restante.Text = FormatCurrency(Session("rs").fields("SALDOINSOLUTO").value.ToString)

        End If

        Session("Con").Close()

    End Sub

#Region "VALIDACIONES_GENERALES"

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

#End Region

#Region "PAGOS FIJOS"

    Private Function Validaciones(ByVal idprod As Integer, ByVal sueldo As Decimal, ByVal tipopersona As String, ByVal tasa_ord As Decimal, ByVal idsuc As Integer, ByVal Monto_solicitar As Decimal) As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VALIDACION_COTIZACION"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, CInt(Session("PERSONAID")))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 20, idprod)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUELDO", Session("adVarChar"), Session("adParamInput"), 20, sueldo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERSONAL", Session("adVarChar"), Session("adParamInput"), 800, tipopersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA_ORD", Session("adVarChar"), Session("adParamInput"), 20, tasa_ord)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, idsuc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO_SOLICITAR", Session("adVarChar"), Session("adParamInput"), 20, Monto_solicitar)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        ViewState("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString
        Session("Con").Close()

        Return ViewState("RESPUESTA")
    End Function

    Private Sub Cotizacion(ByVal idprod As Integer, ByVal sueldo As Decimal, ByVal tipopersona As String, ByVal tasa_ord As Decimal, ByVal idsuc As Integer, ByVal Monto_solicitar As Decimal, ByVal Plazo As Integer)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtgarantias As New Data.DataTable()

        lbl_status_general.Text = ""
        'lnk_descargar.Visible = True

        If Validaciones(idprod, sueldo, tipopersona, tasa_ord, idsuc, Monto_solicitar) = "OK" Then

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_COTIZADOR_TABULADOR"
            Session("parm") = Session("cmd").CreateParameter("IDTRAB", Session("adVarChar"), Session("adParamInput"), 20, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 20, idprod)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SUELDO", Session("adVarChar"), Session("adParamInput"), 20, sueldo)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOPERSONAL", Session("adVarChar"), Session("adParamInput"), 800, tipopersona)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TASA_ORD", Session("adVarChar"), Session("adParamInput"), 20, Left(CStr(CDec(txt_tasa.Text) + CDec(Session("VALORNORIND"))), InStr(CStr(CDec(txt_tasa.Text) + CDec(Session("VALORIND"))), ".") + 2))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, idsuc)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO_SOLICITAR", Session("adVarChar"), Session("adParamInput"), 20, Monto_solicitar)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 20, Plazo)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtgarantias, Session("rs"))

            If dtgarantias.Rows.Count > 0 Then
                dag_sugeridor.Visible = True
                dag_sugeridor.DataSource = dtgarantias
                dag_sugeridor.DataBind()
            Else
                dag_sugeridor.Visible = False
                lbl_status_pfsi.Text = "Error: No cuenta con capacidad de pago para el monto elegido"
            End If

            Session("Con").Close()
        Else
            lbl_status_pfsi.Text = ViewState("RESPUESTA")
        End If

    End Sub

    Protected Sub btn_cotizar_Click(sender As Object, e As EventArgs) Handles btn_cotizar.Click

        dag_sugeridor.DataSource = Nothing
        dag_sugeridor.DataBind()
        'lnk_descargar.Visible = False
        lbl_status_pfsi.Text = ""

        Dim monto As Decimal
        Dim tasa As Decimal
        Dim idproducto As Integer
        Dim tipoPersonal As String
        Dim idSuc As Integer
        Dim sueldo As Decimal
        Dim Plazo As Integer

        sueldo = CDec(txt_sueldo.Text)
        tasa = CDec(txt_tasa.Text)

        tipoPersonal = txt_tipo_trabajador.Text

        idSuc = CInt(Session("SUCID"))

        If cmb_Producto.SelectedItem.Value <> "0" Then
            idproducto = cmb_Producto.SelectedItem.Value
        Else
            lbl_status_pfsi.Text = "Error: Seleccione producto"
            Exit Sub
        End If
        If Validamonto(txt_monto.Text) = True Then

            If txt_monto.Text <> "" Then
                monto = CDec(txt_monto.Text)
            Else
                lbl_status_pfsi.Text = "Error: Seleccione monto a solicitar"
                Exit Sub
            End If

            If txt_plazo.Text <> "" Then
                Plazo = CInt(txt_plazo.Text)
            Else
                lbl_status_pfsi.Text = "Error: Seleccione plazo a solicitar"
                Exit Sub
            End If

            If CDec(txt_monto.Text) > CDec(ViewState("MONTOMAX")) Then
                lbl_status_pfsi.Text = "Error: Seleccione un monto igual o menor al máximo"
                Exit Sub
            ElseIf CDec(txt_monto.Text) < CDec(ViewState("MONTOMIN")) Then
                lbl_status_pfsi.Text = "Error: Monto Min es de " + ViewState("MONTOMIN").ToString
                Exit Sub
            End If


            If (Plazo >= ViewState("PLAZOMIN") And Plazo <= ViewState("PLAZOMAX")) Then
                Cotizacion(idproducto, sueldo, tipoPersonal, tasa, idSuc, monto, (Plazo * 15))
            Else
                lbl_status_pfsi.Text = "Error: Plazo fuera de los rangos permitidos"
            End If

        Else
            lbl_status_pfsi.Text = "Error: Formato de monto incorrecto "
        End If



    End Sub

    Protected Sub btn_limpiar_Click(sender As Object, e As EventArgs) Handles btn_limpiar.Click
        LimpiarFormulario()
    End Sub

    Protected Sub LimpiarFormulario()

        'lnk_descargar.Visible = False

        lbl_status_general.Text = ""
        lbl_status_pfsi.Text = ""
        lbl_rango_plazo.Text = ""
        lbl_rango_monto.Text = ""

        txt_sueldo.Text = ""
        txt_tipo_trabajador.Text = ""
        txt_monto.Text = ""
        txt_cappago.Text = ""
        txt_tasa.Text = ""
        txt_plazo.Text = ""
        txb_saldo_restante.Text = ""

        dag_sugeridor.DataSource = Nothing
        dag_sugeridor.DataBind()

        LlenarProductos()

    End Sub

    Private Sub GenerarCotizacion(ByVal Cpath As String, ByVal NewDocName As String, ByVal idProd As Integer, ByVal sueldo As Decimal, ByVal tipopersona As String, ByVal tasa_ord As Decimal, ByVal idsuc As Integer, ByVal Monto_solicitar As Decimal, ByVal plazo As Integer)
        Dim Url As String = "COTIZACION.docx"


        Dim Monto As Decimal
        Dim Plazo_Completo As String
        Dim tasa As Decimal
        Dim pagofijo As Decimal
        Dim count As Integer = 1

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(Cpath + Url)

            Try

                NewDocName = Cpath + NewDocName + ".docx"
                worddoc.SaveAs(NewDocName)

                Dim NumTrabajador As String = ""
                NumTrabajador = txt_cliente.Text
                NumTrabajador = NumTrabajador.PadLeft(5, "0")

                worddoc.ReplaceText("[NOMBRE_CLIENTE]", lbl_cliente.Text, False, RegexOptions.None)
                worddoc.ReplaceText("[NUM_EMPLEADO]", NumTrabajador, False, RegexOptions.None)
                worddoc.ReplaceText("[FECHA]", Session("FechaSis"), False, RegexOptions.None)
                worddoc.ReplaceText("[PRODUCTO]", cmb_Producto.SelectedItem.Text, False, RegexOptions.None)
                worddoc.Save()

                ObtieneDatosDocumentosFijos(NewDocName)

                'prueba.Text = "IDPROD: " + CStr(idProd) + " MONTO: " + CStr(Monto_solicitar) + " TASA: " + CStr(tasa_ord) + " TIPO_PERSONAL: " + tipopersona + " ID_SUC: " + CStr(idsuc) + " SUELDO: " + CStr(sueldo) + " PLAZO: " + CStr(plazo) + " NUMTRAB: " + NumTrabajador
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_COTIZADOR_TABULADOR"
                Session("parm") = Session("cmd").CreateParameter("IDTRAB", Session("adVarChar"), Session("adParamInput"), 20, NumTrabajador)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 20, idProd)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SUELDO", Session("adVarChar"), Session("adParamInput"), 20, sueldo)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TIPOPERSONAL", Session("adVarChar"), Session("adParamInput"), 800, tipopersona)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TASA_ORD", Session("adVarChar"), Session("adParamInput"), 20, tasa_ord)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, idsuc)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MONTO_SOLICITAR", Session("adVarChar"), Session("adParamInput"), 20, Monto_solicitar)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 20, plazo)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                Do While Not Session("rs").eof
                    Monto = Session("rs").Fields("MONTO").Value
                    Plazo_Completo = Session("rs").Fields("PLAZO").Value
                    tasa = Session("rs").Fields("TASA_ORD").Value
                    pagofijo = Session("rs").Fields("PAGO").Value

                    ObtieneTablaCotizacion(count, NewDocName, Monto, Plazo_Completo, tasa, pagofijo)
                    count = count + 1

                    Session("rs").MoveNext()
                Loop
                Session("Con").Close()

            Catch ex As Exception
                lbl_status_general.Text = ex.ToString
            Finally

            End Try

        End Using
    End Sub

    Private Sub ObtieneDatosDocumentosFijos(ByVal NewDocName As String)


        Using worddoc As Novacode.DocX = Novacode.DocX.Load(NewDocName)
            Try
                Dim Paragraph As Novacode.Paragraph = worddoc.InsertParagraph

                Paragraph.InsertText("REQUISITOS: " + vbLf + vbLf)
                Paragraph.InsertText("*COMPROBANTE DE DOMICILIO(1)" + vbLf)
                Paragraph.InsertText("*IDENTIFICACIÓN OFICIAL(1)" + vbLf)
                Paragraph.InsertText("*DOCUMENTO QUE CONTENGA CLABE INTERBANCARIA" + vbLf)
                Paragraph.InsertText("*COMPROBANTE DE INGRESOS" + vbLf)
                Paragraph.InsertText("*LIQUIDEZ DE PAGO" + vbLf)
                worddoc.Save()
            Catch ex As Exception
                lbl_status_general.Text = ex.ToString
            End Try
        End Using


    End Sub

    Private Sub ObtieneTablaCotizacion(ByVal Contador As Integer, ByVal NewDocName As String, ByVal monto As Decimal, ByVal plazo As String, ByVal tasa As Integer, ByVal pagofijo As Decimal)

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(NewDocName)
            Try
                Dim Paragraph As Novacode.Paragraph = worddoc.InsertParagraph

                Paragraph.InsertText("Opción:" + CStr(Contador) + vbLf + vbLf)
                Paragraph.InsertText("Monto: " + FormatCurrency(CStr(monto)) + vbLf)
                Paragraph.InsertText("Plazo: " + plazo + vbLf)
                Paragraph.InsertText("Tasa ordinaria: " + CStr(tasa) + "% Anual" + vbLf)
                Paragraph.InsertText("Pago Fijo: " + FormatCurrency(CStr(pagofijo)) + vbLf)
                worddoc.Save()

            Catch ex As Exception
            End Try
        End Using

    End Sub

    Private Sub DAG_sugeridor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_sugeridor.ItemCommand

        If (e.CommandName = "PLANPAGO") Then

            Session("MONTO") = (e.Item.Cells(1).Text)

            Session("PLAZO") = Replace(e.Item.Cells(2).Text, "QUINCENA", "")
            Session("PLAZO") = Replace(Session("PLAZO"), "S", "")
            Session("TASA") = e.Item.Cells(3).Text

            PlanPagos(Session("MONTO"), CInt(Session("PLAZO")), Session("TASA"))

            With Response
                .BufferOutput = True
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/octet-stream"
                .AddHeader("Content-disposition",
                           "attachment; filename= PLANPAGOS(FOLIO:" + Session("CLAVE") + ").pdf")
                Response.Cache.SetNoServerCaching()
                Response.Cache.SetNoStore()
                Response.Cache.SetMaxAge(System.TimeSpan.Zero)

                Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

                .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
                .End()
                .Flush()
            End With
        End If

    End Sub

    Private Sub PlanPagos(ByVal monto As Decimal, ByVal plazo As String, ByVal tasa As String)

        Dim folioCadena As String = ""
        Dim idpersona As String = ""
        Dim titulo As String = ""
        Dim producto As String = ""
        Dim fechaPago As String = ""
        Dim unidadPeriodicidad As String = ""
        Dim periodicidad As String = ""
        Dim tipotasa As String = ""

        Dim indice As String = ""
        Dim tasa_mora As String = ""
        Dim indice_mora As String = ""
        Dim c_pcjte As String = ""
        Dim ctotal As String = ""
        Dim c_iva As String = ""
        Dim c_comision As String = ""
        Dim c_cobro_fra As String = ""
        Dim c_tiempo_fra As String = ""
        Dim c_iva_fra As String = ""
        Dim c_total_fra As String = ""
        Dim capital_total As String = ""
        Dim interes_iva As String = ""
        Dim cat As String = ""
        Dim tipoPlan As String = ""
        Dim clasificacion As String = ""
        Dim idDisposicion As String = ""
        Dim fechaDisposicion As String = ""
        Dim montoDisposicion As String = ""
        Dim c_pctje_dis As String = ""
        Dim c_comision_dis As String = ""
        Dim c_iva_dis As String = ""
        Dim monto_ant_dis As String = ""
        Dim saldo_insoluto_dis As String = ""
        Dim interes_dis As String = ""
        Dim opcionComFra As Integer = 0
        Dim clave_comision As String = ""
        Dim no_qui As Integer = 0
        Dim rfc As String = ""

        titulo = "Plan de Pagos Fijos Saldos Insolutos"


        'Comienza seccion de escritura del pdf 

        idpersona = Session("PERSONAID")
        rfc = tbx_rfc.Text
        producto = cmb_Producto.SelectedItem.Text
        monto = FormatCurrency(CDec(monto))

        no_qui = txt_plazo.Text
        unidadPeriodicidad = "DIAS"
        periodicidad = "QUINCENAL"
        'tasa = txt_tasa.Text



        If tipoPlan = "PFSI" Then
            titulo = "Plan de Pagos Fijos Saldos Insolutos"
        ElseIf tipoPlan = "SI" Then
            titulo = "Plan de Pagos Saldos Insolutos"
        End If




        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")
        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        With document
            .AddAuthor("SNTE - SNTE")
            .AddCreationDate()
            .AddCreator("SNTE - Plan de Pagos")
            .AddSubject("Plan de Pagos")
            .AddTitle("Plan de Pagos")
            .AddKeywords("Plan de Pagos")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT
        Dim writer As iTextSharp.text.pdf.PdfWriter
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim PlanPagos As iTextSharp.text.pdf.PdfImportedPage

        PlanPagos = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(PlanPagos, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 65  'X empieza de izquierda a derecha
        Y = 670 'Y empieza de abajo hacia arriba


        Dim fecalt As String

        XT = X
        YT = Y + 5
        'encabezado

        'Producto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Producto: " + producto, X, Y, 0)

        X = 305

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "RFC: " + rfc, X, Y, 0)
        Y = Y - 15
        X = 65
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        'Monto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto:", X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(monto), X + 40, Y, 0)

        X = 305

        'Monto maximo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto máximo: ", X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ViewState("MONTOMAX")), X + 70, Y, 0)

        Y = Y - 15
        X = 65
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        'Plazo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo: " + no_qui.ToString + " QUINCENAS", X, Y, 0)

        X = 305

        'Plazo maximo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo máximo: ", X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, ViewState("PLAZOMAX") + " QUINCENAS", X + 60, Y, 0)


        Y = Y - 15
        X = 65

        'Nombre
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Nombre: " + Session("CLIENTE").ToString, X, Y, 0)

        X = 305
        ' Y = 655

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa: ", X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, tasa + "%", X + 30, Y, 0)

        Y = Y - 15
        X = 305 'Muestro el plazo con su respectiva unidad




        'Declaro de nuevo los títulos para el encabezado de la tabla
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)
        X = 70
        Y = Y - 65
        'Declaras XT y YT para conservar los valores iniciales de X y Y para utilizarlos posteriormente

        XT = X
        YT = Y + 25




        'NO. PAGO
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

        'CAPITAL
        XT = XT + 120
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

        'INTERES
        XT = XT + 140
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)

        ''IVA
        'XT = XT + 60
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA", XT, YT, 0)

        'TOTAL
        XT = XT + 120
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)

        'SALDO
        XT = XT + 90
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo", XT, YT, 0)


        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SIMULA_PLAN_PAGOS_COTIZA"
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 10, CDec(monto))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMERO_PAGOS", Session("adVarChar"), Session("adParamInput"), 10, CInt(plazo))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_Producto.SelectedItem.Value.ToString))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, CDec(tasa))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF

                If Y < 90 Then
                    Y = 645
                    X = 65
                    cb.EndText()

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

                    cb = writer.DirectContent
                    PlanPagos = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(PlanPagos, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    cb.SetFontAndSize(bf, 9)

                    XT = X
                    YT = Y + 35
                Else
                    Y = Y - 15
                    X = 65
                End If
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 8)

                X = 70
                'NO. PAGO
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("NUM_PAGO").Value.ToString, X, Y, 0)

                'FECHA DE PAGO
                ' X = X + 100

                fecalt = Session("rs").Fields("FECHAPAGO").Value.ToString
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)

                'CAPITAL
                X = X + 135
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("CAPITAL").Value.ToString, X, Y, 0)

                'INTERES
                X = X + 135
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("INTERES").Value.ToString, X, Y, 0)



                'TOTAL
                X = X + 130
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("TOTAL").Value.ToString, X, Y, 0)

                X = X + 90
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("SALDO").Value.ToString, X, Y, 0)

                Session("rs").movenext()
            Loop

        End If


        Session("Con").Close()

        cb.EndText()
        document.Close()

    End Sub

#End Region

#Region "Convierte a PDF"

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub

#End Region

End Class