Public Class CRED_VEN_REEST_PRES
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Reestructurar Préstamo", "Reestructurar Préstamo")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            tbx_id_persona.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + tbx_id_persona.ClientID + "','" + btn_buscar_prestamo.ClientID + "')")
            btn_buscar_agremiado.Attributes.Add("OnClick", "busquedapersonafisica(1)")
        End If

        If Session("idperbusca") <> "" Then
            tbx_rfc.Text = Session("idperbusca")
            Session("idperbusca") = ""
        End If

    End Sub

    Protected Sub btn_buscar_prestamo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_buscar_prestamo.Click

        BuscarAgremiado()

    End Sub

    Private Sub BuscarAgremiado()

        Limpiar()
        IdAgremiado()

        If tbx_id_persona.Text = "" Then
            lbl_estatus.Text = "Error: Validar el RFC debido a que el agremiado no existe en la base de datos o es incorrecto."
        Else
            InformacionAgremiado()
        End If

    End Sub

    Private Sub Limpiar()

        lbl_estatus.Text = ""
        tbx_agremiado.Text = ""
        tbx_folio.Text = ""
        tbx_id_folio.Text = 0
        tbx_fecha_pago.Text = ""
        tbx_monto.Text = ""
        tbx_intereses.Text = ""
        tbx_total.Text = ""
        tbx_monto_insoluto.Text = ""
        tbx_intereses_insoluto.Text = ""
        tbx_total_insoluto.Text = ""
        tbx_tasa.Text = ""
        tbx_plazo.Text = ""
        lbl_plazo.Text = "Plazo Préstamo:"
        tbx_descuento.Text = ""
        dgd_descuentos.DataSource = Nothing
        dgd_descuentos.DataBind()

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

    Private Sub InformacionAgremiado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRESTAMO_AGREMIADO"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("RESULTADO").value.ToString = "NO_ID_FOLIO" Then
            lbl_estatus.Text = "Error: El Agremiado no posee ningún préstamo que se pueda REESTRUCTURAR."
        Else

            If Session("rs").fields("RESULTADO").value.ToString = "DESC_APLI" Then
                tbx_plazo.Enabled = False
                tbx_tasa.Enabled = False
                btn_reestructurar.Enabled = False
                tbx_id_folio.Text = 0
                lbl_estatus.Text = "Error: El Agremiado ya cuenta con DESCUENTOS aplicados que impiden una REESTRUCTURA."
            Else
                tbx_tasa.Enabled = False
                tbx_plazo.Enabled = True
                btn_reestructurar.Enabled = True
                tbx_id_folio.Text = Session("rs").fields("ID_FOLIO").value.ToString
            End If

            tbx_agremiado.Text = Session("rs").fields("AGREMIADO").value.ToString
            tbx_folio.Text = Session("rs").fields("FOLIO").value.ToString
            tbx_fecha_pago.Text = Session("rs").fields("FECHA_PAGO").value.ToString
            tbx_monto.Text = Session("rs").fields("CAPITAL").value.ToString
            tbx_intereses.Text = Session("rs").fields("INTERES").value.ToString
            tbx_total.Text = Session("rs").fields("SALDO").value.ToString
            tbx_monto_insoluto.Text = Session("rs").fields("CAPITAL_INSOLUTO").value.ToString
            tbx_intereses_insoluto.Text = Session("rs").fields("INTERES_INSOLUTO").value.ToString
            tbx_total_insoluto.Text = Session("rs").fields("SALDO_INSOLUTO").value.ToString
            tbx_tasa.Text = Session("rs").fields("TASA").value.ToString
            tbx_plazo.Text = Session("rs").fields("PLAZO").value.ToString
            lbl_plazo.Text = "Plazo Préstamo: (Hasta " + Session("rs").fields("NO_DESCUENTOS").value.ToString + " quincenas - Fin de ciclo)"
            tbx_descuento.Text = Session("rs").fields("DESCUENTO").value.ToString

        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_reestructurar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_reestructurar.Click

        If ValidaTasa(tbx_tasa.Text) = True Then
            RecalculaPrestamo()
        Else
            lbl_estatus.Text = "Error: La tasa para el recalculo tiene el formato incorrecto."
        End If

    End Sub

    Private Function ValidaTasa(ByVal tasa As String) As Boolean
        Return Regex.IsMatch(tasa, ("^[0-9]{1,3}(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Sub RecalculaPrestamo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_FOLIO", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_folio.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_PAGO_VAR", Session("adVarChar"), Session("adParamInput"), 10, tbx_fecha_pago.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA_NUEVA", Session("adVarChar"), Session("adParamInput"), 10, tbx_tasa.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO_NUEVO", Session("adVarChar"), Session("adParamInput"), 10, tbx_plazo.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_RECALCULO_PRESTAMO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_estatus.Text = "Éxito: Se ha REESTRUCTURADO el préstamo."
        InformacionAgremiado()
        CargaNuevosDescuentos()

    End Sub

    Private Sub CargaNuevosDescuentos()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtInversiones As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_FOLIO", Session("adVarChar"), Session("adParamInput"), 20, tbx_id_folio.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DESCUENTOS_PRESTAMO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtInversiones, Session("rs"))
        Session("Con").Close()

        If DtInversiones.Rows.Count > 0 Then
            dgd_descuentos.DataSource = DtInversiones
            dgd_descuentos.DataBind()
        Else
            dgd_descuentos.DataSource = Nothing
            dgd_descuentos.DataBind()
        End If

    End Sub

End Class