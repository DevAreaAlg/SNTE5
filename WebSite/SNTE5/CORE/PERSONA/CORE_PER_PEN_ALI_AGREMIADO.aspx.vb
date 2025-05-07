Public Class CORE_PER_PEN_ALI_AGREMIADO
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("ID_PENSEION") = 0 Then
            TryCast(Master, MasterMascore).CargaASPX("Agregar Pensión Alimenticia", "Agregar Pensión Alimenticia")
        Else
            TryCast(Master, MasterMascore).CargaASPX("Editar Pensión Alimenticia", "Editar Pensión Alimenticia")
        End If

        If Not IsPostBack Then
            If Session("ID_PENSION") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If

            CargarBancos()
            Permisos()
            CargaPeriodo()
            CargarTipoSegumientos()
            CargarSeguimientos()
            tbx_id_persona.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + tbx_id_persona.ClientID + "','" + btn_buscar.ClientID + "')")
            btn_buscar_agremiado.Attributes.Add("OnClick", "busquedapersonafisica(1)")
        End If

        If Session("idperbusca") <> "" Then
            tbx_rfc.Text = Session("idperbusca")
            Session("idperbusca") = ""
            lbl_estatus_rfc.Text = ""
        End If

    End Sub

    Private Sub Permisos()

        If Session("ID_PENSION") = 0 Then
            stn_buscar_agremiado.Visible = True
            tbx_id_registro.Text = 0
            btn_guardar.Enabled = False
            btn_digitalizar.Enabled = False
            btn_guardar_segumiento.Enabled = False
        Else
            stn_buscar_agremiado.Visible = False
            tbx_id_registro.Text = Session("ID_PENSION").ToString
            btn_guardar.Enabled = True
            btn_digitalizar.Enabled = True
            btn_guardar_segumiento.Enabled = True
            CargaPensionAlimenticia()
        End If

    End Sub

    Private Sub CargarBancos()

        ddl_banco.Items.Clear()
        ddl_banco.Items.Add(New ListItem("ELIJA", "-1"))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            ddl_banco.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CargaPeriodo()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERIODO_PEN_ALI"
        Session("rs") = Session("cmd").Execute()
        lbl_periodo_porc_bene.Text = "*% de retención para el beneficiario (Periodo " + Session("rs").fields("PERIODO").value.ToString + "):"
        lbl_periodo_porc_fondo.Text = "*% de retención para el fondo de contingencia (Periodo " + Session("rs").fields("PERIODO").value.ToString + "):"
        Session("Con").Close()

    End Sub

    Private Sub CargarTipoSegumientos()

        ddl_seguimiento.Items.Clear()
        ddl_seguimiento.Items.Add(New ListItem("ELIJA", ""))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPO_SEGUIMIENTOS_PEN_ALI"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            ddl_seguimiento.Items.Add(New ListItem(Session("rs").Fields("TEXT").Value, Session("rs").Fields("VALUE").Value))
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CargarSeguimientos()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtSeguimientos As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PENSION", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PEN_ALI_SEGUIMIENTOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtSeguimientos, Session("rs"))
        Session("Con").Close()

        If DtSeguimientos.Rows.Count > 0 Then
            gvw_segumientos.DataSource = DtSeguimientos
            ViewState("Seguimientos") = DtSeguimientos
            gvw_segumientos.DataBind()
        Else
            gvw_segumientos.DataSource = Nothing
            gvw_segumientos.DataBind()
        End If

    End Sub

    Private Sub gvw_segumientos_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvw_segumientos.PageIndexChanging

        gvw_segumientos.PageIndex = e.NewPageIndex
        gvw_segumientos.DataSource = ViewState("Seguimientos")
        gvw_segumientos.DataBind()

    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_buscar.Click
        BuscarAgremiado()
    End Sub

    Private Sub BuscarAgremiado()

        Limpiar()
        IdAgremiado()

        If tbx_id_persona.Text = "" Then
            lbl_estatus_rfc.Text = "Error: Validar el RFC debido a que el agremiado no existe en la base de datos o es incorrecto."
        Else
            BuscarDemandado()
            btn_guardar.Enabled = True
        End If

    End Sub

    Private Sub Limpiar()

        lbl_estatus_rfc.Text = ""
        lbl_estatus.Text = ""
        lbl_estatus_segumiento.Text = ""

        tbx_id_persona.Text = ""
        tbx_rfc_agremiado.Text = ""
        tbx_nombre_agremiado.Text = ""
        tbx_id_registro.Text = "0"
        tbx_region.Text = ""
        tbx_delegacion.Text = ""
        tbx_ct.Text = ""

        tbx_beneficiario.Text = ""
        tbx_rfc_beneficiario.Text = ""
        tbx_telefono.Text = ""
        tbx_correo.Text = ""
        tbx_cp.Text = ""
        ddl_estado.Items.Clear()
        ddl_municipio.Items.Clear()
        ddl_colonia.Items.Clear()
        tbx_calle.Text = ""
        tbx_numero_ext.Text = ""
        tbx_numero_int.Text = ""
        ddl_banco.SelectedValue = "-1"
        tbx_clabe.Text = ""
        tbx_clabe_confirma.Text = ""
        tbx_porcentaje.Text = ""
        tbx_porcentaje_fondo.Text = ""
        tbx_porcentaje_oficio.Text = ""
        tbx_fecha_vencimiento.Text = ""
        tbx_fecha_oficio.Text = ""

        btn_digitalizar.Enabled = False
        btn_guardar.Enabled = False
        btn_guardar_segumiento.Enabled = False

        gvw_segumientos.DataSource = Nothing
        gvw_segumientos.DataBind()

    End Sub

    Private Sub IdAgremiado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("EXISTE").value.ToString = -1 Then
            tbx_id_persona.Text = ""
        Else
            tbx_id_persona.Text = Session("rs").fields("IDPERSONA").value.ToString
        End If

        Session("Con").Close()

    End Sub

    Private Sub BuscarDemandado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_persona.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_PEN_ALI", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PEN_ALI_AGREMIADO"
        Session("rs") = Session("cmd").Execute()
        tbx_rfc_agremiado.Text = Session("rs").fields("RFC").value.ToString
        tbx_nombre_agremiado.Text = Session("rs").fields("AGREMIADO").value.ToString
        tbx_region.Text = Session("rs").fields("REGION").value.ToString
        tbx_delegacion.Text = Session("rs").fields("DELEGACION").value.ToString
        tbx_ct.Text = Session("rs").fields("CT").value.ToString
        btn_guardar.Enabled = True
        Session("Con").Close()

    End Sub

    Private Sub CargaPensionAlimenticia()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_persona.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_PEN_ALI", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PEN_ALI_AGREMIADO"
        Session("rs") = Session("cmd").Execute()
        tbx_rfc_agremiado.Text = IIf(Session("rs").fields("RFC").value IsNot DBNull.Value, Session("rs").fields("RFC").value, "")
        tbx_nombre_agremiado.Text = IIf(Session("rs").fields("AGREMIADO").value IsNot DBNull.Value, Session("rs").fields("AGREMIADO").value, "")
        tbx_region.Text = IIf(Session("rs").fields("REGION").value IsNot DBNull.Value, Session("rs").fields("REGION").value, "")
        tbx_delegacion.Text = IIf(Session("rs").fields("DELEGACION").value IsNot DBNull.Value, Session("rs").fields("DELEGACION").value, "")
        tbx_ct.Text = IIf(Session("rs").fields("CT").value IsNot DBNull.Value, Session("rs").fields("CT").value, "")
        tbx_rfc_beneficiario.Text = IIf(Session("rs").fields("RFC_BENEFICIARIO").value IsNot DBNull.Value, Session("rs").fields("RFC_BENEFICIARIO").value, "")
        tbx_beneficiario.Text = IIf(Session("rs").fields("BENEFICIARIO").value IsNot DBNull.Value, Session("rs").fields("BENEFICIARIO").value, "")
        tbx_telefono.Text = IIf(Session("rs").fields("TELEFONO").value IsNot DBNull.Value, Session("rs").fields("TELEFONO").value, "")
        tbx_correo.Text = IIf(Session("rs").fields("CORREO").value IsNot DBNull.Value, Session("rs").fields("CORREO").value, "")
        Dim cp As String = IIf(Session("rs").fields("CP").value IsNot DBNull.Value, Session("rs").fields("CP").value, "")
        'txb_notas.Text = IIf(Session("rs").fields("NOTAS").value IsNot DBNull.Value, Session("rs").fields("NOTAS").value, "")
        Dim state As String = Session("rs").fields("ID_ESTADO").value.ToString
        Dim mpio As String = Session("rs").fields("ID_MUNICIPIO").value.ToString
        Dim col As String = Session("rs").fields("ID_COLONIA").value.ToString
        Dim ID_BANCO As String = IIf(Session("rs").fields("ID_BANCO").value IsNot DBNull.Value, Session("rs").fields("ID_BANCO").value, "-1")
        tbx_calle.Text = IIf(Session("rs").fields("CALLE").value IsNot DBNull.Value, Session("rs").fields("CALLE").value, "")
        tbx_numero_ext.Text = IIf(Session("rs").fields("NO_EXT").value IsNot DBNull.Value, Session("rs").fields("NO_EXT").value, "")
        tbx_numero_int.Text = IIf(Session("rs").fields("NO_INT").value IsNot DBNull.Value, Session("rs").fields("NO_INT").value, "")
        tbx_clabe.Text = IIf(Session("rs").fields("CLABE").value IsNot DBNull.Value, Session("rs").fields("CLABE").value, "")
        tbx_clabe_confirma.Text = IIf(Session("rs").fields("CLABE").value IsNot DBNull.Value, Session("rs").fields("CLABE").value, "")
        tbx_porcentaje.Text = IIf(Session("rs").fields("PORCENTAJE").value IsNot DBNull.Value, Session("rs").fields("PORCENTAJE").value, "")
        tbx_porcentaje_fondo.Text = IIf(Session("rs").fields("PORCENTAJE_FONDO").value IsNot DBNull.Value, Session("rs").fields("PORCENTAJE_FONDO").value, "")
        tbx_porcentaje_oficio.Text = IIf(Session("rs").fields("PORCENTAJE_OFICIO").value IsNot DBNull.Value, Session("rs").fields("PORCENTAJE_OFICIO").value, "")
        tbx_fecha_vencimiento.Text = Session("rs").fields("FECHA_VENCIMIENTO").value.ToString
        tbx_fecha_oficio.Text = Session("rs").fields("FECHA_OFICIO").value.ToString
        Session("Con").Close()

        If cp <> "" Then
            tbx_cp.Text = cp
            CargarEstadoMunicipioColonia()
            ddl_estado.SelectedValue = state
            ddl_municipio.SelectedValue = mpio
            ddl_colonia.SelectedValue = col
        End If

        ddl_banco.SelectedValue = ID_BANCO

    End Sub

    Private Sub CargarEstadoMunicipioColonia()

        Dim uni_ref As New UNI_REFERENCIAS
        Dim DropsDireccion_by_cp As DropDownList()
        DropsDireccion_by_cp = uni_ref.busquedaCP_Global(tbx_cp.Text)

        ddl_estado.Items.Clear()
        ddl_estado.Items.Add(DropsDireccion_by_cp(0).Items(0))

        ddl_municipio.Items.Clear()
        ddl_municipio.Items.Add(DropsDireccion_by_cp(1).Items(0))

        ddl_colonia.Items.Clear()
        For Each item As ListItem In DropsDireccion_by_cp(2).Items
            ddl_colonia.Items.Add(item)
        Next

    End Sub

    Protected Sub btn_buscar_cp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar_cp.Click

        BuscarCP(tbx_cp.Text)

    End Sub

    Private Sub BuscarCP(ByVal cp As String)

        Dim uni_ref As New UNI_REFERENCIAS
        Dim DropsDireccion_by_cp As DropDownList()
        DropsDireccion_by_cp = uni_ref.busquedaCP_Global(cp)

        ddl_estado.Items.Clear()
        ddl_estado.Items.Add(DropsDireccion_by_cp(0).Items(0))

        ddl_municipio.Items.Clear()
        ddl_municipio.Items.Add(DropsDireccion_by_cp(1).Items(0))

        ddl_colonia.Items.Clear()
        For Each item As ListItem In DropsDireccion_by_cp(2).Items
            ddl_colonia.Items.Add(item)
        Next

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar.Click

        If ValidacionBancos() = True Then
            If tbx_correo.Text <> "" Then
                If ValidarCorreo(tbx_correo.Text) = True Then
                    If tbx_rfc_beneficiario.Text <> "" Then
                        If ValidarRFCBeneficiario(tbx_rfc_beneficiario.Text) = True Then
                            GuardarPensionAlimenticia()
                        End If
                    Else
                        GuardarPensionAlimenticia()
                    End If
                End If
            Else
                If tbx_rfc_beneficiario.Text <> "" Then
                    If ValidarRFCBeneficiario(tbx_rfc_beneficiario.Text) = True Then
                        GuardarPensionAlimenticia()
                    End If
                Else
                    GuardarPensionAlimenticia()
                End If
            End If
        End If

    End Sub

    Private Function ValidacionBancos() As Boolean

        Dim Validacion As Boolean

        If ddl_banco.SelectedValue = "-1" AndAlso tbx_clabe.Text = "" Then
            Validacion = True
        ElseIf ddl_banco.SelectedValue <> "-1" AndAlso tbx_clabe.Text = "" Then
            Validacion = False
            lbl_estatus.Text = "Error: No se ha ingresado la CLABE INTERBANCARIA."
        ElseIf ddl_banco.SelectedValue = "-1" AndAlso tbx_clabe.Text <> "" Then
            Validacion = False
            lbl_estatus.Text = "Error: Se requiere seleccionar un BANCO."
        Else
            If tbx_clabe.Text <> tbx_clabe_confirma.Text Then
                Validacion = False
                lbl_estatus.Text = "Error: La CLABE no coincide con el campo de confirmación."
            Else
                If Len(tbx_clabe.Text) <> 18 Then
                    Validacion = False
                    lbl_estatus.Text = "Error: El número de CLABE INTERBANCARIA debe ser de 18 posiciones."
                Else
                    If ValidaCLABE() = "OK" Then
                        Validacion = True
                    Else
                        Validacion = False
                        lbl_estatus.Text = "Error: La CLABE no coincide con el Banco seleccionado."
                    End If
                End If
            End If
        End If

        Return Validacion

    End Function

    Private Function ValidaCLABE() As String

        Dim Respuesta As String = "FALSE"
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_BANCO", Session("adVarChar"), Session("adParamInput"), 10, ddl_banco.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLABE_BANCO", Session("adVarChar"), Session("adParamInput"), 20, tbx_clabe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_CLABE_BANCO"
        Session("rs") = Session("cmd").Execute()
        Respuesta = Session("rs").fields("VALIDACION").value.ToString
        Session("Con").close()
        Return Respuesta

    End Function

    Private Function ValidarCorreo(Correo As String) As Boolean

        Dim Validar As Boolean = Regex.IsMatch(Correo, "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")

        If Validar = False Then
            lbl_error_correo.Visible = True
            tbx_correo.Focus()
            Return False
        Else
            lbl_error_correo.Visible = False
            Return True
        End If

    End Function

    Private Function ValidarRFCBeneficiario(RFC As String) As Boolean

        Dim Validar As Boolean = Regex.IsMatch(RFC, "^[a-zA-Z]{4}(\d{6})((\D|\d){3})?$")

        If Validar = False Then
            lbl_error_rfc.Visible = True
            tbx_rfc_beneficiario.Focus()
            Return False
        Else
            lbl_error_rfc.Visible = False
            Return True
        End If

    End Function

    Private Sub GuardarPensionAlimenticia()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PENSION", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_DEMAN", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_persona.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC_DEMAN", Session("adVarChar"), Session("adParamInput"), 15, tbx_rfc_agremiado.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("BENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 500, tbx_beneficiario.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC_BENEF", Session("adVarChar"), Session("adParamInput"), 15, tbx_rfc_beneficiario.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TEL_BENEF", Session("adVarChar"), Session("adParamInput"), 20, tbx_telefono.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CORREO_BENEF", Session("adVarChar"), Session("adParamInput"), 50, tbx_correo.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP_BENEF", Session("adVarChar"), Session("adParamInput"), 10, tbx_cp.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_ESTADO", Session("adVarChar"), Session("adParamInput"), 10, ddl_estado.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_municipio.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_COLONIA", Session("adVarChar"), Session("adParamInput"), 10, ddl_colonia.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLE_BENEF", Session("adVarChar"), Session("adParamInput"), 100, tbx_calle.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NO_EXT_BENEF", Session("adVarChar"), Session("adParamInput"), 10, tbx_numero_ext.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NO_INT_BENEF", Session("adVarChar"), Session("adParamInput"), 10, tbx_numero_int.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_BANCO", Session("adVarChar"), Session("adParamInput"), 10, ddl_banco.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLABE_BENEF", Session("adVarChar"), Session("adParamInput"), 20, tbx_clabe.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PORC_OFICIO", Session("adVarChar"), Session("adParamInput"), 50, tbx_porcentaje_oficio.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PORC_BENEF", Session("adVarChar"), Session("adParamInput"), 50, tbx_porcentaje.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PORC_FONDO", Session("adVarChar"), Session("adParamInput"), 50, tbx_porcentaje_fondo.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If tbx_fecha_oficio.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("FECHA_OFICIO", Session("adVarChar"), Session("adParamInput"), 10, DBNull.Value)
        Else
            Session("parm") = Session("cmd").CreateParameter("FECHA_OFICIO", Session("adVarChar"), Session("adParamInput"), 10, CType(tbx_fecha_oficio.Text, Date))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        If tbx_fecha_vencimiento.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("FECHA_VENCIMIENTO", Session("adVarChar"), Session("adParamInput"), 10, DBNull.Value)
        Else
            Session("parm") = Session("cmd").CreateParameter("FECHA_VENCIMIENTO", Session("adVarChar"), Session("adParamInput"), 10, CType(tbx_fecha_vencimiento.Text, Date))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PENSION_ALIMENTICIA"
        Session("rs") = Session("cmd").Execute()
        tbx_id_registro.Text = Session("rs").fields("ID_PENSION").value.ToString
        Session("Con").Close()

        lbl_estatus.Text = "Éxito: Pensión alimenticia guardada correctamente."
        btn_digitalizar.Enabled = True
        btn_guardar_segumiento.Enabled = True
        CargaPensionAlimenticia()

    End Sub

    Protected Sub btn_digitalizar_Click(sender As Object, e As EventArgs) Handles btn_digitalizar.Click

        Session("IDPENSION") = tbx_id_registro.Text.ToString
        Response.Redirect("CORE_PER_PEN_ALI_DIGI.aspx")

    End Sub

    Protected Sub btn_guardar_segumiento_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar_segumiento.Click

        GuardarSeguimiento()

    End Sub

    Private Sub GuardarSeguimiento()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PENSION", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SEGUIMIENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_seguimiento.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TEXTO", Session("adVarChar"), Session("adParamInput"), 2000, tbx_seguimiento.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PEN_ALI_SEGUIMIENTO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        ddl_seguimiento.SelectedValue = ""
        tbx_seguimiento.Text = ""
        lbl_estatus_segumiento.Text = "Éxito: Se ha guardado correctamente el registro."
        CargarSeguimientos()

    End Sub

End Class