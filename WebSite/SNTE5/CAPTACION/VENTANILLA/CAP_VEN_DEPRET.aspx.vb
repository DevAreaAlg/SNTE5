Imports System.Data
Imports System.Data.SqlClient
Public Class CAP_VEN_DEPRET
    Inherits System.Web.UI.Page

    Dim CuentasCaptacionOrigen As New List(Of TextBox)()
    Dim CuentasCaptacionDestino As New List(Of TextBox)()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Depósitos y Retiros", "Depósitos y Retiros")

        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If
            pnl_origenDeposito.Visible = False
            pnl_destinoDeposito.Visible = False
            pnl_origenRetiro.Visible = False
            pnl_destinoRetiro.Visible = False
            pnl_referencia.Visible = False

            crea_tablas()
        End If

        If Not Session("VAL_BIO") Is Nothing Then
            If Session("VAL_BIO") Then
                Session("VAL_BIO") = Nothing
                aplica_ret()
                If Session("RES") = "1" Then
                    If chk_pdf.Checked = True Then
                        'ClientScript.RegisterStartupScript(GetType(String), "FolioImpreso", "window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, ""FI"", ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
                    Else
                        HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))

                    End If
                    If Session("caja") > 0.0 Then
                        Session("MONTO_EFECTIVO") = Session("caja")
                        Session("ENTRADASALIDA") = "SALIDA"
                        LlamaTiraEfectivo()
                    End If
                    retiro()
                Else
                    If Session("RES") = "0" Then
                        lbl_info.Text = "El número de cheque capturado ya existe en la base de datos"
                        Exit Sub
                    Else
                        lbl_info.Text = "El monto a retirar excede al monto disponible debido a una retención de IDE por aplicar"
                        Exit Sub
                    End If
                End If
                Session("bancos") = Nothing
                Session("cheques") = Nothing
                Session("caja") = Nothing
                Session("ctasdes") = Nothing
            Else
                Exit Sub
            End If
        Else
            RequiredFieldValidator_cliente.Enabled = True
            RequiredFieldValidator_cmbbancochequesdes.Enabled = True
            RequiredFieldValidator_cmbbancochequesori.Enabled = True
            RequiredFieldValidator_cmbbancodes.Enabled = True
            RequiredFieldValidator_cmbbancodes_des.Enabled = True
            RequiredFieldValidator_cmbbancosori.Enabled = True
            RequiredFieldValidator_ctachequesori.Enabled = True
            RequiredFieldValidator_montochequesori.Enabled = True
            RequiredFieldValidator_numchequesori.Enabled = True
            RequiredFieldValidator_txt_clabe_banco_des.Enabled = True
            RequiredFieldValidator_txtmontobancodes.Enabled = True
            RequiredFieldValidator_txtmontobancosori.Enabled = True
            RequiredFieldValidator_txtmontochequesdes.Enabled = True
            RequiredFieldValidator_txtnumchequesdes.Enabled = True
        End If

        VerificaInsumos()
        txt_cp.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cp.ClientID + "','" + btn_buscadat.ClientID + "')")
        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + btn_seleccionar.ClientID + "')")

        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica(1)")
        lnk_BusquedaCP.Attributes.Add("OnClick", "busquedaCP()")
        btn_buscapersona_ori.Attributes.Add("OnClick", "busquedapersonafisica(2)")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
            Session("idperbusca") = ""
        End If

        If Session("idperbusca_Usuario") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca_Usuario").ToString
            Session("idperbusca_Usuario") = Nothing
            '  lbl_nombre_cliente_ori.Text = Session("PROSPECTO_Usuario")
        End If

        ''btn_aplicar.Attributes.Add("OnClick", "generafolio_imp()")
        'If Session("idperbusca") <> "" Then
        '    txt_cliente.Text = Session("idperbusca")
        'End If

        If Session("PERSONAID") <> Nothing Then
            Llenactascapori()
            Llenactascapdes()
        End If

        HiddenPrinterName.Value = Session("IMPRESORA_TICKET")

    End Sub

    'función que mantiene el foco
    Sub Dofocus(ByVal control As Control)
        Dim scm As ScriptManager
        scm = ScriptManager.GetCurrent(Page)
        scm.SetFocus(control)
    End Sub

#Region "Valida y realiza operacion"

    Protected Sub btn_aplicar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aplicar.Click

        Session("RES") = "0"
        Session("InfoTicket") = Nothing
        Dim idpersona_busqueda As Integer

        If txt_IdCliente.Text = "" Then
            idpersona_busqueda = -1
        Else
            idpersona_busqueda = CInt(txt_IdCliente.Text)
        End If

        If txt_nombre1.Text = "" And txt_nombre2.Text = "" And txt_paterno.Text = "" And txt_materno.Text = "" Then
            lbl_info0.Text = ""
        Else
            'Tener como mínimo la referencia primer nombre y Paterno
            If (txt_nombre1.Text = "" And txt_paterno.Text = "") Or (txt_nombre1.Text <> "" And txt_paterno.Text = "") Or (txt_nombre1.Text = "" And txt_paterno.Text <> "") Then
                lbl_info0.Text = "Agregue como mínimo Primer Nombre y Paterno"
                Exit Sub
            End If
        End If

        'máximo 3000 caracteres
        If txt_notas.Text.Length > 3000 Then
            lbl_info0.Text = "Error: Máximo 3000 caracteres en las notas"
            Exit Sub
        End If

        'Session("inv_per") = cmb_inv_per.Visible
        Dim sbancos As String, scheques As String, sctasori As String, sctasdes As String, scaja As String, sEfectivo As String

        If rad_deposito.Checked Then 'DEPOSITO

            If rad_cliente.Checked = False And rad_usuario.Checked = False Then
                lbl_info0.Text = "Seleccione quién está realizando el movimiento del depósito"
                Exit Sub
            Else
                If cmb_ctascapdes.SelectedItem.Value <> "-1" Then 'CTA DEP SELECCIONADA
                    scaja = txt_cajaori.Text

                    If scaja <> "" Or (ctas_origen().Compute("Sum(MONTO)", "MONTO<>0")).ToString <> "" Or (Session("tabla_bancosori").Compute("Sum(MONTO)", "MONTO<>0")).ToString <> "" Or (Session("tabla_chequesori").Compute("Sum(MONTO)", "MONTO<>0")).ToString <> "" Then 'MONTOS INDICADOS

                        sctasori = (ctas_origen().Compute("Sum(MONTO)", "MONTO<>0")).ToString
                        sbancos = (Session("tabla_bancosori").Compute("Sum(MONTO)", "MONTO<>0")).ToString
                        sEfectivo = (Session("tabla_bancosori").Compute("Sum(MONTO)", "ORIGEN='EFECTIVO' AND MONTO<>0")).ToString
                        scheques = (Session("tabla_chequesori").Compute("Sum(MONTO)", "MONTO<>0")).ToString

                        If sctasori <> "" Then
                            Session("ctasori") = CDec(sctasori)
                        Else
                            Session("ctasori") = 0
                        End If

                        If sbancos <> "" Then
                            Session("bancos") = CDec(sbancos)
                        Else
                            Session("bancos") = 0
                        End If

                        If scheques <> "" Then
                            Session("cheques") = CDec(scheques)
                        Else
                            Session("cheques") = 0.0
                        End If

                        If scaja <> "" Then
                            Session("caja") = CDec(scaja)
                        Else
                            Session("caja") = 0
                        End If

                        If sEfectivo <> "" Then
                            Session("efectivo") = CDec(sEfectivo)
                        Else
                            Session("efectivo") = 0
                        End If

                        Dim monto As Decimal = Session("ctasori") + Session("bancos") + Session("cheques") + Session("caja")
                        Session("efectivo") = Session("efectivo") + Session("caja")

                        'Se crean nuevas variables para utilizarlas en los modales
                        Session("caja_AUTO") = Session("caja")
                        Session("cheques_AUTO") = Session("cheques")
                        Session("bancos_AUTO") = Session("bancos")
                        Session("ctasori_AUTO") = Session("ctasori")

                        If validacion_bloqueo_general_deposito() Then
                            If validad_saldo_suficiente_dep() Then
                                If valida_cuentas_iguales_dep() Then
                                    If val_cap_net(monto) = monto Then
                                        If rad_usuario.Checked Then
                                            ' VERIFICA SI REQUIERE CAPTURAR EL CUESTIONARIO Y TENER LOS DATOS CAPTURADOS
                                            If val_cuestionario_Efectivo(val_rangos_Efectivo(Session("efectivo"))) = "OK" Then
                                                ' SOLO SE GUARDARAN LOS DATOS EN CASO DE SER REQUERIDOS
                                                If val_rangos_Efectivo(Session("efectivo")) = "REGISTRAR" Then
                                                    'Session("GUADAR_USUARIO") = "SI"
                                                    If val_idPersona_existente(txt_cliente.Text, idpersona_busqueda) <> "OK" Then
                                                        lbl_info0.Text = val_idPersona_existente(txt_cliente.Text, idpersona_busqueda)
                                                    Else
                                                        Session("GUADAR_USUARIO") = "SI"
                                                    End If
                                                Else
                                                    Session("GUADAR_USUARIO") = "NO"
                                                End If
                                            Else
                                                lbl_info0.Text = "Debido a la suma acumulada en efectivo es obligatorio capturar la información del Usuario"
                                                Exit Sub
                                            End If
                                        Else ' Es Cliente
                                            Session("GUADAR_USUARIO") = "NO"
                                        End If

                                        If Session("tabla_chequesori").Select("ESTATUS='COB'").Length <> 0 Then
                                            If revisa_facultad_cheques() Then
                                                If AutorizacionActiva(Session("caja")) = 1 Then
                                                    SolicitudAutorizacion(Session("caja"))
                                                    cambia_modal_autorizacion("PLD")
                                                    ModalPopupExtender2.Show()
                                                Else
                                                    aplica_dep()
                                                    aplica_dep_after(Session("caja"), Session("cheques"), Session("bancos"), Session("ctasori"))

                                                    If Session("GUADAR_USUARIO") = "SI" Then
                                                        GuardaUsuario(Session("CTA_DESTINO"), Session("SERIE"), Session("FOLIO_IMP"), Session("IFE"))
                                                    End If

                                                    If Session("IFE") = "REGISTRAR" Then
                                                        lbl_ife.Text = "Recuerde solicitar IFE para posteriormente digitalizarla"
                                                    Else
                                                        lbl_ife.Text = ""
                                                    End If
                                                    LimpiaTodo_Deposito()
                                                End If
                                            Else
                                                solicitud_autorizacion_cheques()
                                                cambia_modal_autorizacion("CHEQUES")
                                                ModalPopupExtender2.Show()
                                            End If
                                        Else
                                            If AutorizacionActiva(Session("caja")) = 1 Then
                                                SolicitudAutorizacion(Session("caja"))
                                                ModalPopupExtender2.Show()
                                            Else
                                                aplica_dep()

                                                aplica_dep_after(Session("caja"), Session("cheques"), Session("bancos"), Session("ctasori"))
                                                If Session("GUADAR_USUARIO") = "SI" Then
                                                    GuardaUsuario(Session("CTA_DESTINO"), Session("SERIE"), Session("FOLIO_IMP"), Session("IFE"))
                                                End If

                                                If Session("IFE") = "REGISTRAR" Then
                                                    lbl_ife.Text = "Recuerde solicitar IFE para posteriormente digitalizarla"
                                                Else
                                                    lbl_ife.Text = ""
                                                End If
                                                'SE LIMPIAN LOS COMPONENTES
                                                LimpiaTodo_Deposito()
                                            End If
                                        End If

                                    Else
                                        lbl_info0.Text = "No se permite depositar la cantidad indicada. El monto permitido es de: $" + val_cap_net(monto).ToString
                                    End If
                                Else
                                    lbl_info0.Text = "No puede elegir la misma cuenta de captación como origen y destino"
                                End If
                            Else
                                lbl_info0.Text = "No puede retirar más de lo disponible en alguna de sus cuentas"
                            End If
                        Else
                            lbl_info0.Text = "Error: El Expediente esta bloqueado"
                        End If

                    Else
                        lbl_info0.Text = "No ha introducido ningún monto a depositar"
                    End If
                Else
                    lbl_info0.Text = "No ha seleccionado la cuenta destino de depósito"
                End If
            End If
        Else 'Retiro
            If cmb_ctascapori.SelectedItem.Value = "-1" Then
                lbl_info0.Text = "No ha seleccionado la cuenta origen de retiro"
                Exit Sub
            Else
                If txt_cajades.Text <> "" Or (ctas_destino().Compute("Sum(MONTO)", "MONTO<>0")).ToString <> "" Or (Session("tabla_bancosdes").Compute("Sum(MONTO)", "MONTO<>0")).ToString <> "" Or (Session("tabla_chequesdes").Compute("Sum(MONTO)", "MONTO<>0")).ToString <> "" Then
                    sbancos = (Session("tabla_bancosdes").Compute("Sum(MONTO)", "MONTO<>0")).ToString
                    scheques = (Session("tabla_chequesdes").Compute("Sum(MONTO)", "MONTO<>0")).ToString
                    scaja = txt_cajades.Text
                    sctasdes = (ctas_destino().Compute("Sum(MONTO)", "MONTO<>0")).ToString

                    If sbancos <> "" Then
                        Session("bancos") = CDec(sbancos)
                    Else
                        Session("bancos") = 0.0
                    End If
                    If scheques <> "" Then
                        Session("cheques") = CDec(scheques)
                    Else
                        Session("cheques") = 0.0
                    End If
                    If scaja <> "" Then
                        Session("caja") = CDec(scaja)
                    Else
                        Session("caja") = 0.0
                    End If
                    If sctasdes <> "" Then
                        Session("ctasdes") = CDec(sctasdes)
                    Else
                        Session("ctasdes") = 0.0
                    End If

                    'Primero se verifica si el saldo no está bloqueado de manera total
                    If validacion_bloqueo_general_retiro(Session("bancos") + Session("cheques") + Session("caja") + Session("ctasdes")) = True Then

                        If validad_saldo_suficiente_ret(Session("bancos") + Session("cheques") + Session("caja") + Session("ctasdes"), CInt(Split(cmb_ctascapori.SelectedItem.Value, "_")(1))) = True Then


                            If valida_cuentas_iguales_ret() Then
                                Session("FP") = verifica_biometrico(Session("ID_EQ"), Session("PERSONAID"), "RETCAP")
                                If Session("FP") <> "0" Then 'Esta habilitada la validación por biométrico
                                    If Session("FP") = "-1" Then 'el  cliente no tiene su huella registrada
                                        lbl_info0.Text = "El cliente no ha registrado su huella digital"
                                        Exit Sub
                                    Else 'si tiene su huella registrada
                                        RequiredFieldValidator_cliente.Enabled = False
                                        RequiredFieldValidator_cmbbancochequesdes.Enabled = False
                                        RequiredFieldValidator_cmbbancochequesori.Enabled = False
                                        RequiredFieldValidator_cmbbancodes.Enabled = False
                                        RequiredFieldValidator_cmbbancodes_des.Enabled = False
                                        RequiredFieldValidator_cmbbancosori.Enabled = False
                                        RequiredFieldValidator_ctachequesori.Enabled = False
                                        RequiredFieldValidator_montochequesori.Enabled = False
                                        RequiredFieldValidator_numchequesori.Enabled = False
                                        RequiredFieldValidator_txt_clabe_banco_des.Enabled = False
                                        RequiredFieldValidator_txtmontobancodes.Enabled = False
                                        RequiredFieldValidator_txtmontobancosori.Enabled = False
                                        RequiredFieldValidator_txtmontochequesdes.Enabled = False
                                        RequiredFieldValidator_txtnumchequesdes.Enabled = False
                                        rfv_modo_rec.Enabled = False
                                        ClientScript.RegisterStartupScript(GetType(String), "Biometrico", "biometric('v');", True)
                                    End If

                                Else
                                    aplica_ret()
                                    If Session("RES") = "1" Then
                                        'ClientScript.RegisterStartupScript(GetType(String), "FolioImpreso", "window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, ""FI"", ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
                                        'HiddenRawData.Value = impresion_ticket()

                                        If Session("caja") > 0.0 Then
                                            Session("MONTO_EFECTIVO") = Session("caja")
                                            Session("ENTRADASALIDA") = "SALIDA"

                                            LlamaTiraEfectivo()

                                            'ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""TiraEfectivo.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
                                        End If

                                        If chk_pdf.Checked = True Then
                                            ClientScript.RegisterStartupScript(GetType(String), "FolioImpreso", "window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, ""FI"", ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
                                        Else
                                            HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))
                                        End If

                                        retiro()
                                    Else
                                        If Session("RES") = "0" Then
                                            lbl_info0.Text = "El número de cheque capturado ya existe en la base de datos"
                                            Exit Sub
                                            'Else
                                            '    lbl_info.Text = "El monto a retirar excede al monto disponible debido a una retención de IDE por aplicar"
                                            '    Exit Sub
                                        End If
                                    End If
                                    LimpiaTodo_Retiro()
                                End If
                            Else
                                lbl_info0.Text = "No puede elegir la misma cuenta de captación como origen y destino"
                                Exit Sub
                            End If
                        Else
                            lbl_info0.Text = "No puede retirar más de lo disponible en alguna de sus cuentas"
                            Exit Sub
                        End If
                    Else

                        lbl_info0.Text = "Error: Cuenta/Saldo Bloqueado, sólo puede retirar hasta: $  " + Session("MASCOREG").FormatNumberCurr(Session("SALDO_PUEDE_RETIRAR").ToString)

                    End If

                Else
                    lbl_info0.Text = "No ha introducido ningún monto a retirar"
                    Exit Sub
                End If


            End If

        End If


    End Sub

    Private Sub aplica_dep()
        'lbl_titulo.Text = "It Works"
        Dim efectivo As Decimal, caja As String
        caja = txt_cajaori.Text

        If caja <> "" Then
            efectivo = CDec(caja)
        Else
            efectivo = 0.0
        End If

        Dim nombre1 As String, nombre2 As String, paterno As String, materno As String, notas As String
        nombre1 = txt_nombre1.Text
        nombre2 = txt_nombre2.Text
        paterno = txt_paterno.Text
        materno = txt_materno.Text
        notas = txt_notas.Text


        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.
            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("INS_DEPRET_APLICA_DEPOSITO", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("CTA_DES", SqlDbType.Int)
            Session("parm").Value = CInt(Split(cmb_ctascapdes.SelectedItem.Value, "_")(1))
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CTAS_ORI", SqlDbType.Structured)
            Session("parm").Value = ctas_origen()
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("BANCOS", SqlDbType.Structured)
            Session("parm").Value = Session("tabla_bancosori")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CHEQUES", SqlDbType.Structured)
            Session("parm").Value = Session("tabla_chequesori")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("EFECTIVO", SqlDbType.Decimal)
            Session("parm").Value = efectivo
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

            Session("parm") = New SqlParameter("NOMBRE1", SqlDbType.VarChar)
            Session("parm").Value = nombre1
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("NOMBRE2", SqlDbType.VarChar)
            Session("parm").Value = nombre2
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("PATERNO", SqlDbType.VarChar)
            Session("parm").Value = paterno
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("MATERNO", SqlDbType.VarChar)
            Session("parm").Value = materno
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("NOTAS", SqlDbType.VarChar)
            Session("parm").Value = notas
            insertCommand.Parameters.Add(Session("parm"))
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                Session("RES") = myReader.GetString(0)
                Session("SERIE") = myReader.GetString(1)
                Session("FOLIO_IMP") = myReader.GetString(2)
                Session("CTA_DESTINO") = CInt(Split(cmb_ctascapdes.SelectedItem.Value, "_")(1))
            End While
            myReader.Close()

        End Using

        'If Session("inv_per") Then
        '    Session("Con").Open()
        '    Session("cmd") = New ADODB.Command()
        '    Session("cmd").ActiveConnection = Session("Con")
        '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        '    Session("parm") = Session("cmd").CreateParameter("CHEQUES", Session("adVarChar"), Session("adParamInput"), 5000, Array)
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("parm") = Session("cmd").CreateParameter("TIPO_OPERACION", Session("adVarChar"), Session("adParamInput"), 50, "DEPOSITO")
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("parm") = Session("cmd").CreateParameter("ID_SUC", Session("adVarChar"), Session("adParamInput"), 20, Session("SUCID"))
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        '    Session("cmd").Parameters.Append(Session("parm"))
        '    Session("cmd").CommandText = "INS_AUTORIZACION_CHEQUES"
        '    Session("rs") = Session("cmd").Execute()
        '    Session("ID_AUT") = Session("rs").Fields("ID_AUT").Value
        '    Session("Con").Close()
        'End If

    End Sub

    Private Sub aplica_ret()
        'lbl_titulo.Text = "It Works"
        Dim efectivo As Decimal, caja As String
        caja = txt_cajades.Text

        If caja <> "" Then
            efectivo = CDec(caja)
        Else
            efectivo = 0.0
        End If

        Dim nombre1 As String, nombre2 As String, paterno As String, materno As String, notas As String
        nombre1 = txt_nombre1.Text
        nombre2 = txt_nombre2.Text
        paterno = txt_paterno.Text
        materno = txt_materno.Text
        notas = txt_notas.Text

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("INS_DEPRET_APLICA_RETIRO", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("CTAS_DES", SqlDbType.Structured)
            Session("parm").Value = ctas_destino()
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CTA_ORI", SqlDbType.Int)
            Session("parm").Value = CInt(Split(cmb_ctascapori.SelectedItem.Value, "_")(1))
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("BANCOS", SqlDbType.Structured)
            Session("parm").Value = Session("tabla_bancosdes")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CHEQUES", SqlDbType.Structured)
            Session("parm").Value = Session("tabla_chequesdes")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("EFECTIVO", SqlDbType.Decimal)
            Session("parm").Value = efectivo
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

            Session("parm") = New SqlParameter("NOMBRE1", SqlDbType.VarChar)
            Session("parm").Value = nombre1
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("NOMBRE2", SqlDbType.VarChar)
            Session("parm").Value = nombre2
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("PATERNO", SqlDbType.VarChar)
            Session("parm").Value = paterno
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("MATERNO", SqlDbType.VarChar)
            Session("parm").Value = materno
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("NOTAS", SqlDbType.VarChar)
            Session("parm").Value = notas
            insertCommand.Parameters.Add(Session("parm"))

            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                Session("RES") = myReader.GetString(0)
                Session("SERIE") = myReader.GetString(1)
                Session("FOLIO_IMP") = myReader.GetString(2)
            End While
            myReader.Close()

            LlenaDatosGenerales()
            ObtieneCorreosFinanzas()

        End Using

    End Sub

    Private Sub aplica_dep_after(ByVal caja As Decimal, ByVal cheques As Decimal, ByVal bancos As Decimal, ByVal ctasori As Decimal)

        If Session("RES") <> "0" Then

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CHEQUE", Session("adVarChar"), Session("adParamInput"), 20, cheques)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("BANCO", Session("adVarChar"), Session("adParamInput"), 20, bancos)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EFECTIVO", Session("adVarChar"), Session("adParamInput"), 20, caja)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 20, Session("SERIE"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO_IMP"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDDIVISA", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_OPERACION_ALERTA_PLD"
            Session("rs") = Session("cmd").Execute()

            Session("IDALERTA") = Session("rs").Fields("ALERTA").Value.ToString

            Session("Con").Close()

            'lbl_titulo.text = "resultado " + CStr(Session("IDALERTA"))

            If Session("IDALERTA") >= 0 Then
                ClientScript.RegisterStartupScript(GetType(String), "AlertaOpercionInusual", "window.open(""AlertasOperacionPLD.aspx"", ""PLD"", ""width=650,height=550,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
            End If

            'ClientScript.RegisterStartupScript(GetType(String), "FolioImpreso", "window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, ""IF"", ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
            'HiddenRawData.Value = impresion_ticket()

            'Se llama la tira de efectivo
            If caja > 0.0 Then
                Session("MONTO_EFECTIVO") = caja
                Session("ENTRADASALIDA") = "ENTRADA"

                'Función que llama tira de efectivo
                LlamaTiraEfectivo()

                'ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""TiraEfectivo.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
            End If

            If chk_pdf.Checked = True Then
                ClientScript.RegisterStartupScript(GetType(String), "FolioImpreso", "window.open(""FolioImpreso.aspx?serie=" + Session("SERIE") + "&folio_imp=" + Session("FOLIO_IMP") + """, ""FI"", ""width=610,height=482,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
            Else
                HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))

            End If

            deposito()
        Else
            lbl_info.Text = "El número de cheque capturado ya existe en la base de datos"
        End If
    End Sub

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

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


    Function val_cap_net(ByVal monto As Decimal) As Decimal
        Dim monto_valido As Decimal
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 25, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_DEP_CAPNET"
        Session("rs") = Session("cmd").Execute()
        monto_valido = Session("rs").Fields("MONTO_ACEPTAR").value
        Session("Con").Close()
        Return monto_valido
    End Function

    Function revisa_facultad_cheques() As Boolean
        Dim res As Boolean = False
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REVISA_FACULTAD_CHEQUES"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("RES").value = 1 Then
            res = True
        End If
        Session("Con").Close()
        Return res
    End Function

    Private Sub llena_cheques_x_autorizar()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_cheques_aut As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_AUT", Session("adVarChar"), Session("adParamInput"), 11, Session("ID_AUT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES_X_AUTORIZAR"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_cheques_aut, Session("rs"))
        Session("Con").Close()
        dag_cheques_aut.Visible = True
        dag_cheques_aut.DataSource = dt_cheques_aut
        dag_cheques_aut.DataBind()
    End Sub

    Private Sub llena_lista_user_aut()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_lst As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_AUTORIZACION_CHEQUES_USUARIOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_lst, Session("rs"))
        Session("Con").Close()
        dag_lst.DataSource = dt_lst
        dag_lst.DataBind()
    End Sub

    Private Sub solicitud_autorizacion_cheques()
        Dim rsrows() As DataRow
        Dim array As String = ""
        rsrows = Session("tabla_chequesori").Select("ESTATUS='COB'")
        For Each row As DataRow In rsrows
            array += row(1).ToString + ":" + row(4).ToString + ":" + row(5).ToString + ","
        Next
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CHEQUES", Session("adVarChar"), Session("adParamInput"), 5000, array)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_OPERACION", Session("adVarChar"), Session("adParamInput"), 50, "DEPOSITO")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SUC", Session("adVarChar"), Session("adParamInput"), 20, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_AUTORIZACION_CHEQUES"
        Session("rs") = Session("cmd").Execute()
        Session("ID_AUT") = Session("rs").Fields("ID_AUT").Value
        Session("Con").Close()
        lbl_IDAutor2.Text = Session("ID_AUT").ToString
        llena_cheques_x_autorizar()
        llena_lista_user_aut()
    End Sub

    Private Sub cambia_modal_autorizacion(ByVal mode As String)
        If mode = "CHEQUES" Then
            lbl_AutorPLD_Titulo.Text = "Autorización de Cheques"
            lbl_IDAutor.Visible = False
            lbl_IDAutor2.Visible = False
            dag_cheques_aut.Visible = True
        Else
            lbl_AutorPLD_Titulo.Text = "Autorización de Movimiento"
            lbl_IDAutor.Visible = True
            lbl_IDAutor2.Visible = True
            dag_cheques_aut.Visible = False
        End If
        cmb_Acc.ClearSelection()
        cmb_Acc.Items.FindByValue("-1").Selected = True
        txt_Usr.Text = ""
        txt_Pwd.Text = ""
        txt_Mtv.Text = ""
    End Sub

    Function valida_cuentas_iguales_dep() As Boolean
        Dim i As Integer = 0
        Dim res As Boolean = True
        Dim txtcapori(100) As TextBox
        If Not Session("txtcapori") Is Nothing Then
            txtcapori = Session("txtcapori")
        End If
        Do While Not txtcapori(i) Is Nothing
            'Calculos de ingresos y egresos.
            If txtcapori(i).Text <> "" And Split(txtcapori(i).ID.ToString, "_")(1) = Split(cmb_ctascapdes.SelectedItem.Value, "_")(1) Then
                res = False
                Exit Do
            End If
            i += 1
        Loop
        Return res
    End Function

    Function valida_cuentas_iguales_ret() As Boolean
        Dim i As Integer = 0
        Dim res As Boolean = True
        Dim txtcapdes(100) As TextBox
        If Not Session("txtcapdes") Is Nothing Then
            txtcapdes = Session("txtcapdes")
        End If
        Do While Not txtcapdes(i) Is Nothing
            'Calculos de ingresos y egresos.
            If txtcapdes(i).Text <> "" And Split(txtcapdes(i).ID.ToString, "_")(1) = Split(cmb_ctascapori.SelectedItem.Value, "_")(1) Then
                res = False
                Exit Do
            End If
            i += 1
        Loop
        Return res
    End Function

    Function validad_saldo_suficiente_dep() As Boolean
        Dim i As Integer = 0
        Dim res As Boolean = True
        Dim txtcapori(100) As TextBox, saldocapori(100) As Label, dato As String
        If Not Session("txtcapori") Is Nothing Then
            txtcapori = Session("txtcapori")
        End If

        If Not Session("saldocapori") Is Nothing Then
            saldocapori = Session("saldocapori")
        End If

        Do While Not txtcapori(i) Is Nothing
            'Calculos de ingresos y egresos.
            dato = txtcapori(i).Text
            If dato <> "" Then

                If CDec(dato) > CDec(Mid(saldocapori(i).Text.ToString, 8)) Then
                    res = False
                    Exit Do
                End If

                If res = False Then
                    Exit Do
                End If
            End If
            i += 1
        Loop
        Return res
    End Function

    Function validacion_bloqueo_general_retiro(ByVal saldo As Decimal) As Boolean
        Dim res As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDOARETIRAR", Session("adVarChar"), Session("adParamInput"), 20, saldo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_BLOQUEO_EXP_RETIRO"
        Session("rs") = Session("cmd").Execute()

        Session("ESTATUS") = Session("rs").fields("ESTATUS").value.ToString
        Session("SALDO_PUEDE_RETIRAR") = Session("rs").fields("SALDO").value.ToString

        Session("Con").close()
        res = IIf(Session("ESTATUS") = "OK", True, False)
        Return res
    End Function

    Function validacion_bloqueo_general_deposito() As Boolean
        Dim res As Boolean
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_BLOQUEO_EXP_DEPOSITO"
        Session("rs") = Session("cmd").Execute()
        Session("BLOQUEADO") = Session("rs").fields("ESTATUS").value.ToString
        Session("Con").close()
        res = IIf(Session("BLOQUEADO") = "SI", False, True)
        Return res
    End Function

    Function validad_saldo_suficiente_ret(ByVal monto As Decimal, ByVal FolioCap As Integer) As Boolean
        'Dim res As Boolean = True
        'Dim i As Integer = 0
        'Dim saldocapdes(100) As Label
        'If Not Session("saldocapdes") Is Nothing Then
        '    saldocapdes = Session("saldocapdes")
        'End If

        'Do While Not saldocapdes(i) Is Nothing
        '    'Calculos de ingresos y egresos.
        '    If saldocapdes(i).ID = Split(cmb_ctascapori.SelectedItem.Value, "_")(1) And (monto > CDec(Mid(saldocapdes(i).Text.ToString, 8))) Then
        '        res = False
        '        Exit Do
        '    End If
        '    i += 1
        'Loop
        'Return res

        Dim res As Boolean
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, FolioCap)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RETIRO", Session("adVarChar"), Session("adParamInput"), 10, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_RETIRO1"
        Session("rs") = Session("cmd").Execute()
        Session("RETIRAR") = Session("rs").fields("ESTATUS").value.ToString
        Session("Con").close()
        res = IIf(Session("RETIRAR") = "NO", False, True)
        Return res

    End Function


    Protected Sub btn_agregarbancosori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarbancosori.Click

        Dim idCta As Integer, banco As String, monto As Decimal, id_banco_destino As Integer, banco_destino As String, num_cta_destino As String, titular_destino As String
        Dim Origen_Nombre As String

        idCta = CInt(cmb_bancoori.SelectedItem.Value)
        banco = cmb_bancoori.SelectedItem.Text
        monto = CDec(txt_montobancosori.Text)

        Dim origen
        origen = Split(cmb_origen_dep.SelectedItem.Value.ToString, "-")
        Origen_Nombre = cmb_origen_dep.SelectedItem.Text

        If origen(0) = "EFE" Or origen(0) = "REM" Then
            id_banco_destino = -1
            banco_destino = ""
            num_cta_destino = ""
            titular_destino = ""
        Else
            id_banco_destino = cmb_banco_des_dep.SelectedItem.Value
            If id_banco_destino = -1 Then
                lbl_infobancoori.Text = "Debe de seleccionar un banco de destino!"
                Exit Sub
            Else
                banco_destino = cmb_banco_des_dep.SelectedItem.Text
            End If

            num_cta_destino = txt_cta_dep.Text
            If num_cta_destino = "" Then
                lbl_infobancoori.Text = "Debe de escribir una cuenta de destino!"
                Exit Sub
            End If
            titular_destino = txt_titular_cta_dep.Text

        End If

        If Validamonto(txt_montobancosori.Text) Then
            If val_cheques(num_cta_destino, "TRA", id_banco_destino) = "EXISTE" And origen(0) = "CHE" Then
                lbl_infobancoori.Text = "Error: Ya está registrado ese número de cheque, verifique."
            Else
                Session("tabla_bancosori").Rows.Add(idCta, banco, monto, origen(1), Origen_Nombre, id_banco_destino, banco_destino, num_cta_destino, titular_destino)
                dag_bancosori.DataSource = Session("tabla_bancosori")
                dag_bancosori.DataBind()
                Llenobancosori()
                pnl_forma_transferencia.Visible = False
                LimpiaTransferencia()
            End If


        Else
            lbl_infobancoori.Text = " Monto incorrecto!"
        End If


    End Sub


    Protected Sub btn_agregarchequesori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarchequesori.Click
        lbl_infochequesori.Text = ""
        If Validamonto(txt_montochequesori.Text) Then

            Session("tabla_chequesori").Rows.Add(0, CInt(cmb_bancochequesori.SelectedItem.Value), cmb_bancochequesori.SelectedItem.Text, txt_ctachequesori.Text, txt_numchequesori.Text, CDec(txt_montochequesori.Text), cmb_modo_rec.SelectedItem.Value)
            dag_chequesori.DataSource = Session("tabla_chequesori")
            dag_chequesori.DataBind()
            div_dag_chequesori.Visible = True
            Llenobancoschequesori()
            txt_ctachequesori.Text = ""
            txt_numchequesori.Text = ""
            txt_montochequesori.Text = ""
            cmb_modo_rec.ClearSelection()
            cmb_modo_rec.Items.FindByValue("-1").Selected = True
        Else
            lbl_infochequesori.Text = " Monto incorrecto!"
        End If
    End Sub

    Protected Sub btn_agreagrbancosdes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agreagrbancosdes.Click
        lbl_infobancodes.Text = ""
        If Validamonto(txt_montobancodes.Text) Then
            Session("tabla_bancosdes").Rows.Add(cmb_bancodes.SelectedItem.Value, cmb_bancodes.SelectedItem.Text, CDec(txt_montobancodes.Text), cmb_bancodes_des.SelectedItem.Value, cmb_bancodes_des.SelectedItem.Text, txt_clabe_banco_des.Text, txt_cta_banco_des.Text)
            dag_bancosdes.DataSource = Session("tabla_bancosdes")
            dag_bancosdes.DataBind()
            Llenobancosdes()
            txt_montobancodes.Text = ""
            txt_clabe_banco_des.Text = ""
            txt_cta_banco_des.Text = ""
        Else
            lbl_infobancodes.Text = " Monto incorrecto!"
        End If
    End Sub

    Protected Sub btn_agregarchequesdes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarchequesdes.Click
        lbl_infochequesdes.Text = ""
        If Validamonto(txt_montochequesdes.Text) Then
            Session("tabla_chequesdes").Rows.Add(CInt(cmb_bancochequesdes.SelectedItem.Value), 0, cmb_bancochequesdes.SelectedItem.Text, "", txt_numchequesdes.Text, CDec(txt_montochequesdes.Text), "PAG")
            dag_chequesdes.DataSource = Session("tabla_chequesdes")
            dag_chequesdes.DataBind()
            Llenobancoschequesdes()
            txt_numchequesdes.Text = ""
            txt_montochequesdes.Text = ""
        Else
            lbl_infochequesdes.Text = " Monto incorrecto!"
        End If
    End Sub


    Function ctas_destino() As DataTable
        Dim ctas As New DataTable, txtcapdes(100) As TextBox, dato As String
        Dim i As Integer = 0
        ctas.Columns.Add("FOLIO", GetType(Integer))
        ctas.Columns.Add("MONTO", GetType(Decimal))

        If Not Session("txtcapdes") Is Nothing Then
            txtcapdes = Session("txtcapdes")
        End If

        Do While Not txtcapdes(i) Is Nothing
            'Calculos de ingresos y egresos.
            dato = txtcapdes(i).Text
            If dato <> "" Then
                ctas.Rows.Add(Split(txtcapdes(i).ID.ToString, "_")(1), CDec(dato))
                'ctas.Rows.Add(CInt(Mid(txtcapdes(i).ID.ToString, 5)), CDec(dato))
            End If

            i += 1
        Loop
        Return ctas
    End Function

    Function ctas_origen() As DataTable
        Dim i As Integer = 0
        Dim ctas As New DataTable, txtcapori(100) As TextBox, dato As String
        ctas.Columns.Add("FOLIO", GetType(Integer))
        ctas.Columns.Add("MONTO", GetType(Decimal))

        If Not Session("txtcapori") Is Nothing Then
            txtcapori = Session("txtcapori")
        End If

        Do While Not txtcapori(i) Is Nothing
            'Calculos de ingresos y egresos.
            dato = txtcapori(i).Text
            If dato <> "" Then
                ctas.Rows.Add(Split(txtcapori(i).ID.ToString, "_")(1), CDec(dato))
                'ctas.Rows.Add(CInt(Mid(txtcapori(i).ID.ToString, 5)), CDec(dato))
            End If

            i += 1
        Loop
        Return ctas
    End Function


    Private Sub dag_bancosori_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_bancosori.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then

            lbl_infobancoori.Text = ""
            Session("tabla_bancosori").Rows(e.Item.ItemIndex).Delete()
            dag_bancosori.DataSource = Session("tabla_bancosori")
            dag_bancosori.DataBind()
            Llenobancosori()
        End If

    End Sub

    Private Sub dag_bancosdes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_bancosdes.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then

            lbl_infobancoori.Text = ""
            Session("tabla_bancosdes").Rows(e.Item.ItemIndex).Delete()
            dag_bancosdes.DataSource = Session("tabla_bancosdes")
            dag_bancosdes.DataBind()
            Llenobancosdes()
        End If

    End Sub

    Private Sub dag_chequesdes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_chequesdes.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then

            lbl_infochequesdes.Text = ""
            Session("tabla_chequesdes").Rows(e.Item.ItemIndex).Delete()
            dag_chequesdes.DataSource = Session("tabla_chequesdes")
            dag_chequesdes.DataBind()
            Llenobancoschequesdes()
        End If

    End Sub

    Private Sub dag_chequesori_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_chequesori.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then
            lbl_infochequesdes.Text = ""
            Session("tabla_chequesori").Rows(e.Item.ItemIndex).Delete()
            dag_chequesori.DataSource = Session("tabla_chequesori")
            dag_chequesori.DataBind()
            div_dag_chequesori.Visible = True
            Llenobancoschequesori()
        End If

    End Sub

    Protected Sub cmb_ctascapori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_ctascapori.SelectedIndexChanged

        If cmb_ctascapori.SelectedItem.Value <> "-1" Then
            lnk_RepOp.Enabled = True
            lnk_PerAut.Enabled = True
            Session("FOLIO") = CInt(Split(cmb_ctascapori.SelectedItem.Value, "_")(1))
            'Mostar los datos generales de un expediente: folio, nombre de cliente y producto
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"
            Session("rs") = Session("cmd").Execute()
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
            Session("Con").Close()
        Else
            lnk_RepOp.Enabled = False
            lnk_PerAut.Enabled = True
            Session("FOLIO") = Nothing
        End If

    End Sub

    Protected Sub cmb_ctascapdes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_ctascapdes.SelectedIndexChanged
        If cmb_ctascapdes.SelectedItem.Value <> "-1" Then
            lnk_RepOp.Enabled = True
            lnk_PerAut.Enabled = False
            Session("FOLIO") = CInt(Split(cmb_ctascapdes.SelectedItem.Value, "_")(1))
            Dim inv_per As Integer
            'Mostar los datos generales de un expediente: folio, nombre de cliente y producto
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"
            Session("rs") = Session("cmd").Execute()
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
            inv_per = Session("rs").fields("INV_PER").value
            Session("Con").Close()
            If inv_per = 1 Then
                lbl_inv_per.Visible = True
                cmb_inv_per.Visible = True
                'llena_inv_per(Session("FOLIO"))
            End If
        Else
            lnk_RepOp.Enabled = True
            lnk_PerAut.Enabled = False
            Session("FOLIO") = Nothing
            lbl_inv_per.Visible = False
            cmb_inv_per.Visible = False
        End If
    End Sub


    Protected Sub btn_ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok.Click
        ModalPopupExtender1.Hide()
        Dim cheques As Decimal, caja As Decimal, ctasori As Decimal, bancos As Decimal
        Dim scheques As String, scaja As String, sctasori As String, sbancos As String

        scheques = (Session("tabla_chequesori").Compute("Sum(MONTO)", "MONTO<>0")).ToString
        scaja = txt_cajaori.Text

        sctasori = (ctas_origen().Compute("Sum(MONTO)", "MONTO<>0")).ToString
        sbancos = (Session("tabla_bancosori").Compute("Sum(MONTO)", "MONTO<>0")).ToString


        If scheques <> "" Then
            cheques = CDec(scheques)
        Else
            cheques = 0.0
        End If
        If scaja <> "" Then
            caja = CDec(scaja)
        Else
            caja = 0
        End If
        If sctasori <> "" Then
            ctasori = CDec(sctasori)
        Else
            ctasori = 0.0
        End If
        If sbancos <> "" Then
            bancos = CDec(sbancos)
        Else
            bancos = 0.0
        End If

        If Session("tabla_chequesori").Select("ESTATUS='COB'").Length <> 0 Then
            If revisa_facultad_cheques() Then
                If AutorizacionActiva(caja) = 1 Then
                    SolicitudAutorizacion(caja)
                    cambia_modal_autorizacion("PLD")
                    ModalPopupExtender2.Show()
                Else
                    aplica_dep()
                    aplica_dep_after(caja, cheques, bancos, sctasori)
                    bancos = Nothing
                    cheques = Nothing
                    caja = Nothing
                    sctasori = Nothing
                    LimpiaTodo_Deposito()
                End If
            Else
                solicitud_autorizacion_cheques()
                cambia_modal_autorizacion("CHEQUES")
                ModalPopupExtender2.Show()
            End If
        Else
            If AutorizacionActiva(caja) = 1 Then
                SolicitudAutorizacion(caja)
                ModalPopupExtender2.Show()
            Else
                aplica_dep()
                aplica_dep_after(caja, cheques, bancos, sctasori)
                bancos = Nothing
                cheques = Nothing
                caja = Nothing
                sctasori = Nothing
                LimpiaTodo_Deposito()
            End If
        End If

        'pnl_ide.Visible = False
    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        ModalPopupExtender1.Hide()
        'pnl_ide.Visible = False
    End Sub

    Private Sub SolicitudAutorizacion(ByVal efectivo As Decimal)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_OPERACION", Session("adVarChar"), Session("adParamInput"), 50, "DEPOSITO")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EFECTIVO", Session("adVarChar"), Session("adParamInput"), 20, efectivo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 20, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_AUTORIZACION_OPERACION_PLD"
        Session("rs") = Session("cmd").Execute()

        Session("IDAUTORIZACION") = Session("rs").Fields("IDAUTORIZACION").Value.ToString
        lbl_IDAutor2.Text = Session("IDAUTORIZACION").ToString

        Session("Con").Close()

    End Sub

    Protected Sub lnk_ACT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ACT.Click
        If Not Session("ID_AUT") Is Nothing Then
            ver_aut_remota_cheques()
        Else
            VerificaAutorRemota()
        End If
        ModalPopupExtender2.Show()
    End Sub

    Function ver_aut_remota_cheques() As Boolean
        Dim res As Boolean
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_AUT", Session("adVarChar"), Session("adParamInput"), 11, Session("ID_AUT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_AUTORIZACION_CHEQUE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            res = IIf(Session("rs").fields("RES").value = 1, True, False)
            If res Then
                txt_Mtv.Text = Session("rs").fields("RAZON").value.ToString
                cmb_Acc.SelectedValue = Session("rs").fields("ESTATUS").value.ToString
                lbl_InfoAutoriza.Text = "Se ha realizado la autorización de cheques remotamente."

                txt_Usr.Enabled = False
                txt_Pwd.Enabled = False
                txt_Mtv.Enabled = False
                cmb_Acc.Enabled = False
                btn_AutorPLD_AUTO.Enabled = False
                btn_AutorPLD_CAN.Enabled = False
                lnk_ACT.Enabled = False
                btn_AUTOCerrar.Visible = True
            Else
                txt_Usr.Enabled = True
                txt_Pwd.Enabled = True
                txt_Mtv.Enabled = True
                cmb_Acc.Enabled = True
                btn_AutorPLD_AUTO.Enabled = True
                btn_AutorPLD_CAN.Enabled = True
                lnk_ACT.Enabled = True
                btn_AUTOCerrar.Visible = False
            End If
        End If
        Session("Con").Close()

        ModalPopupExtender2.Show()

        Return res
    End Function

    Function val_user_aut_cheques() As String
        Dim res As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("USER", Session("adVarChar"), Session("adParamInput"), 20, txt_Usr.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PASSWORD", Session("adVarChar"), Session("adParamInput"), 20, txt_Pwd.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_USUARIO_AUTORIZACION_CHEQUE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            res = Session("rs").fields("MENSAJE").value
            If res = "OK" Then
                Session("AUTOR_USR_CHEQUES") = Session("rs").fields("ID_USER").value
            End If
        End If
        Session("Con").Close()
        Return res
    End Function

    Private Sub autoriza_recepcion_cheques(estatus As String, autor_user As Integer, razon As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_AUT", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_AUT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AUTOR_ID_USER", Session("adVarChar"), Session("adParamInput"), 11, autor_user)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 1000, razon)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AUTORIZACION_CHEQUES"
        Session("cmd").Execute()
        Session("Con").Close()
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
        ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""../../CREDITO/VENTANILLA/CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)


        Return True

    End Function

    Function val_cheques(ByVal numcheque As String, ByVal seccion As String, ByVal id_banco As Integer) As String
        Dim Respuesta As String = ""


        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.
            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("SEL_DEPOSITO_CHEQUES", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("NUM_CHEQUE", SqlDbType.NVarChar)
            Session("parm").Value = numcheque
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SECCION", SqlDbType.NVarChar)
            Session("parm").Value = seccion
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("ID_BANCO", SqlDbType.Int)
            Session("parm").Value = id_banco
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CHEQUES", SqlDbType.Structured)
            Session("parm").Value = Session("tabla_chequesori")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("BANCOS", SqlDbType.Structured)
            Session("parm").Value = Session("tabla_bancosori")
            insertCommand.Parameters.Add(Session("parm"))
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                Respuesta = myReader.GetString(0)
            End While
            myReader.Close()

        End Using

        Return Respuesta
    End Function

    Protected Sub cmb_origen_dep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_origen_dep.SelectedIndexChanged

        Dim origen
        origen = Split(cmb_origen_dep.SelectedItem.Value.ToString, "-")

        Select Case origen(0)
            Case "CHE"
                pnl_forma_transferencia.Visible = True
                lbl_tit_dep_origen.Text = "*Núm. Cheque: "
            Case "TRA"
                pnl_forma_transferencia.Visible = True
                lbl_tit_dep_origen.Text = "*Núm. Cuenta: "
            Case Else
                pnl_forma_transferencia.Visible = False
                lbl_tit_dep_origen.Text = ""
        End Select
    End Sub

    Private Sub revisa_facultad_saldos(ByVal iduser As Integer)

        Session("FACULTAD_SALDOS") = "0"

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 11, iduser)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REVISA_FACULTAD_SALDOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").Fields("RES").value = 1 Then
                Session("FACULTAD_SALDOS") = "1"
            Else
                Session("FACULTAD_SALDOS") = "0"
            End If
        End If

        Session("Con").Close()

    End Sub

#End Region 'Region Valida y realiza operacion


#Region "Cargar y Limpiar Entorno"

    Private Sub VerificaInsumos()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VERIFICA_INSUMOS_MASCORE"
        Session("rs") = Session("cmd").Execute()

        Dim res As String
        res = Session("rs").Fields("RES").Value.ToString

        If res <> "" Then
            lbl_info.Text = res
            btn_seleccionar.Enabled = False
        Else
            res = "1"
        End If
        Session("Con").Close()

        If res = "1" Then
            verifica_caja()
        End If

    End Sub

    Private Sub verifica_caja()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CAJA"
        Session("rs") = Session("cmd").Execute()

        Dim res As String
        res = Session("rs").Fields("RES").Value.ToString

        If Session("rs").Fields("RES").Value.ToString = "0" Then
            lbl_info.Text = "Tu equipo no esta dado de alta para operar como caja, por lo tanto no puedes usar este apartado"
            lbl_info.Visible = True
            btn_seleccionar.Enabled = False
        Else
            lbl_info.Text = ""
            btn_seleccionar.Enabled = True
            Session("IDCAJA_USR") = Session("rs").Fields("IDCAJA").Value.ToString

        End If
        Session("Con").Close()

        If res = "1" Then
            verifica_corte_inicial()
        End If
    End Sub

    Private Sub verifica_corte_inicial()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CORTE_INICIAL"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("RES").Value = "0" Then
            lbl_info.Text = "Esta caja no ha ejecutado corte inicial, por lo tanto no puede operar."
            btn_seleccionar.Enabled = False
        Else
            lbl_info.Text = ""
            btn_seleccionar.Enabled = True
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenadatos()
        'Lleno información del cliente
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FOLIOS_X_CLIENTE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            If Session("rs").Fields("NOMBRE").Value.ToString = "-1" Then
                lbl_info.Text = "El cliente introducido no existe"

                Session("Con").Close()
                Exit Sub
            End If

            lbl_cliente.Text = "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString
            Session("PROSPECTO") = Session("rs").Fields("NOMBRE").Value.ToString
            Session("MONTO_IDE") = CDec(Session("rs").Fields("MONTO").Value.ToString)
            'Session("MONTO_LIM") = CDec(Session("rs").Fields("MONTO_LIM").Value.ToString)
        End If
        Session("Con").Close()

        'If Session("MONTO_IDE") >= Session("MONTO_LIM") Then
        '    lbl_info_ide.Text = "Nota: Este cliente excede el límite de depositos y amortizaciones en efectivo libre de retención de IDE, cualquier deposito será motivo de retención."
        'End If
        If cmb_ctascapori.Items.Count = 1 And cmb_ctascapdes.Items.Count = 1 Then
            lbl_info.Text = "El cliente no cuenta con cuentas de captación activas o disponibles"
            lbl_depret.Visible = False
            rad_deposito.Visible = False
            rad_retiro.Visible = False
            rad_deposito.Enabled = False
            rad_retiro.Enabled = False
        Else
            lbl_info.Text = ""
            'Se muestran los componentes para decidir el movimiento a realizar
            lbl_depret.Visible = True
            rad_deposito.Visible = True
            rad_retiro.Visible = True
            rad_deposito.Enabled = True
            rad_retiro.Enabled = True

            'Lleno datos de cheques del lado del origen
            Llenobancoschequesori()
            'Lleno información de depositos y transferencias en bancos, del lado del origen
            Llenobancosori()
            Llena_Origen()
            'Lleno las cuentas de captacion del cliente del lado del origen
            Llenactascapori()
            'Lleno la información de cheques del lado del destino
            Llenobancoschequesdes()
            'Lleno la información de transferencias y depositos en bancos, del lado del destino
            Llenobancosdes()
            'Lleno las cuentas de captacion del cliente del lado del destino
            Llenactascapdes()
            Llenacmbctasdes()
            Llenacmbctasori()

        End If


    End Sub

    Private Sub Llenobancosori()

        cmb_bancoori.Items.Clear()
        cmb_bancoori.Items.Add(New ListItem("ELIJA", "-1"))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                cmb_bancoori.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value, Session("rs").Fields("CTA").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

    End Sub

    Private Sub Llenobancoschequesori()

        cmb_bancochequesori.Items.Clear()
        cmb_bancochequesori.Items.Add(New ListItem("ELIJA", "-1"))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                cmb_bancochequesori.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenobancosdes()
        cmb_bancodes.Items.Clear()
        cmb_bancodes.Items.Add(New ListItem("ELIJA", "-1"))


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                cmb_bancodes.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value, Session("rs").Fields("CTA").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

        cmb_bancodes_des.Items.Clear()
        cmb_bancodes_des.Items.Add(New ListItem("ELIJA", "-1"))
        cmb_banco_des_dep.Items.Clear()
        cmb_banco_des_dep.Items.Add(New ListItem("ELIJA", "-1"))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                cmb_bancodes_des.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                cmb_banco_des_dep.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenobancoschequesdes()
        cmb_bancochequesdes.Items.Clear()
        cmb_bancochequesdes.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                cmb_bancochequesdes.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value, Session("rs").Fields("CTA").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llena_Origen()
        cmb_origen_dep.Items.Clear()
        cmb_origen_dep.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ORIGEN_DEPOSITO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            Do While Not Session("rs").EOF
                cmb_origen_dep.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CLAVE").Value.ToString + "-" + Session("rs").Fields("ID").Value.ToString))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()


    End Sub

    Private Sub Llenactascapori()

        pnl_ctascapori.Controls.Clear()
        CuentasCaptacionOrigen.Clear()

        Dim nombreCuenta As String
        Dim idCuenta As Integer

        Dim i As Integer = 0
        'Dim descapori(100) As Label, txtcapori(100) As TextBox, saldocapori(100) As Label, filtrocapori(100) As AjaxControlToolkit.FilteredTextBoxExtender,
        '    regcapori(100) As RegularExpressionValidator, cr(100) As Literal

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 25, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_CRED", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPRET_CTAS_CAPTACION_ORIGEN"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            'Asigno varibles
            nombreCuenta = Session("rs").Fields("DESCRIPCION").Value.ToString

            If Session("FACULTAD_SALDOS") = "1" Then
                nombreCuenta = nombreCuenta + (" -  Saldo: $" + Session("rs").Fields("SALDO").Value.ToString)
            End If

            idCuenta = Session("rs").Fields("FOLIO").Value.ToString

            'creo el control dinamicamente
            Dim controlCuenta As HtmlGenericControl
            controlCuenta = newCuentaOrigenField(idCuenta, nombreCuenta, "")
            'agrego el control
            pnl_ctascapori.Controls.Add(controlCuenta)

            ''Declaro los arreglos
            'descapori(i) = New Label
            'txtcapori(i) = New TextBox
            'saldocapori(i) = New Label
            'filtrocapori(i) = New AjaxControlToolkit.FilteredTextBoxExtender
            'regcapori(i) = New RegularExpressionValidator
            'cr(i) = New Literal

            'descapori(i).Text = Session("rs").Fields("DESCRIPCION").Value.ToString
            'descapori(i).CssClass = "text_input_nice_label"
            'cr(i).Text = "<br />"
            'descapori(i).Width = 250
            'txtcapori(i).Width = 130
            'txtcapori(i).MaxLength = 22
            'txtcapori(i).CssClass = "module_subsec_elements module_subsec_medium-elements text_input_nice_input"
            'txtcapori(i).ID = "ori_" + Session("rs").Fields("FOLIO").Value.ToString + "_" + i.ToString
            'saldocapori(i).Text = ("Saldo: $" + Session("rs").Fields("SALDO").Value.ToString)
            'saldocapori(i).CssClass = "text_input_nice_label"
            'saldocapori(i).Width = 155
            'filtrocapori(i).ID = "fil_ori_" + Session("rs").Fields("FOLIO").Value.ToString
            'filtrocapori(i).TargetControlID = txtcapori(i).ID
            'filtrocapori(i).FilterType = AjaxControlToolkit.FilterTypes.Custom
            'filtrocapori(i).ValidChars = ".1234567890"
            'regcapori(i).ID = "RegularExpressionValidator_ori_" + Session("rs").Fields("FOLIO").Value.ToString
            'regcapori(i).CssClass = "textogris"
            'regcapori(i).ControlToValidate = txtcapori(i).ID
            'regcapori(i).ErrorMessage = "Monto incorrecto!"
            'regcapori(i).ValidationExpression = "^[0-9]+(\.[0-9]{1}[0-9]?)?$"
            'pnl_ctascapori.Controls.Add(descapori(i))
            'pnl_ctascapori.Controls.Add(New LiteralControl("<br />"))
            'pnl_ctascapori.Controls.Add(cr(i))
            'pnl_ctascapori.Controls.Add(saldocapori(i))
            'pnl_ctascapori.Controls.Add(txtcapori(i))
            'pnl_ctascapori.Controls.Add(filtrocapori(i))
            'pnl_ctascapori.Controls.Add(regcapori(i))
            'pnl_ctascapori.Controls.Add(cr(i))
            Session("rs").movenext()
            i += 1
        Loop
        Session("Con").Close()

        'Session("txtcapori") = txtcapori
        'Session("saldocapori") = saldocapori

    End Sub

    Private Sub Llenactascapdes()
        pnl_ctascapdes.Controls.Clear()
        CuentasCaptacionOrigen.Clear()

        Dim nombreCuenta As String
        Dim idCuenta As Integer

        Dim i As Integer = 0
        'Dim descapdes(100) As Label, txtcapdes(100) As TextBox, saldocapdes(100) As Label, filtrocapdes(100) As AjaxControlToolkit.FilteredTextBoxExtender,
        '    regcapdes(100) As RegularExpressionValidator, cr(100) As Literal
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 25, "DEPRET")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPRET_CTAS_CAPTACION_DESTINO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            'Asigno varibles
            nombreCuenta = Session("rs").Fields("DESCRIPCION").Value.ToString

            If Session("FACULTAD_SALDOS") = "1" Then
                nombreCuenta = nombreCuenta + (" -  Saldo: $" + Session("rs").Fields("SALDO").Value.ToString)
            End If

            idCuenta = Session("rs").Fields("FOLIO").Value.ToString

            'creo el control dinamicamente
            Dim controlCuenta As HtmlGenericControl
            controlCuenta = newCuentaDestinoField(idCuenta, nombreCuenta, "")
            'agrego el control
            pnl_ctascapdes.Controls.Add(controlCuenta)

            'Declaro los arreglos
            'Dim sb As New StringBuilder()
            'descapdes(i) = New Label
            'txtcapdes(i) = New TextBox
            'saldocapdes(i) = New Label
            'filtrocapdes(i) = New AjaxControlToolkit.FilteredTextBoxExtender
            'regcapdes(i) = New RegularExpressionValidator
            'cr(i) = New Literal
            ''filtro(i) = New AjaxControlToolkit.FilteredTextBoxExtender
            'descapdes(i).Text = Session("rs").Fields("DESCRIPCION").Value.ToString
            'descapdes(i).CssClass = "module_subsec_elements module_subsec_medium-elements title_tag"
            'cr(i).Text = "<br />"
            'descapdes(i).Width = 340
            'txtcapdes(i).Width = 205
            'txtcapdes(i).MaxLength = 22
            'txtcapdes(i).CssClass = "text_input_nice_input"
            'txtcapdes(i).ID = "des_" + Session("rs").Fields("FOLIO").Value.ToString + "_" + i.ToString
            'saldocapdes(i).ID = Session("rs").Fields("FOLIO").Value.ToString
            'saldocapdes(i).Text = ("Saldo: $" + Session("rs").Fields("SALDO").Value.ToString)
            'saldocapdes(i).CssClass = "module_subsec_elements module_subsec_medium-elements title_tag"
            'saldocapdes(i).Width = 100
            'filtrocapdes(i).ID = "fil_des_" + Session("rs").Fields("FOLIO").Value.ToString
            'filtrocapdes(i).TargetControlID = txtcapdes(i).ID
            'filtrocapdes(i).FilterType = AjaxControlToolkit.FilterTypes.Custom
            'filtrocapdes(i).ValidChars = ".1234567890"
            'regcapdes(i).ID = "RegularExpressionValidator_des_" + Session("rs").Fields("FOLIO").Value.ToString
            'regcapdes(i).CssClass = "textogris"
            'regcapdes(i).ControlToValidate = txtcapdes(i).ID
            'regcapdes(i).ErrorMessage = "Monto incorrecto!"
            'regcapdes(i).ValidationExpression = "^[0-9]+(\.[0-9]{1}[0-9]?)?$"
            'pnl_ctascapdes.Controls.Add(descapdes(i))
            'pnl_ctascapdes.Controls.Add(New LiteralControl("<br />"))
            'pnl_ctascapdes.Controls.Add(saldocapdes(i))
            'pnl_ctascapdes.Controls.Add(txtcapdes(i))
            'pnl_ctascapdes.Controls.Add(filtrocapdes(i))
            'pnl_ctascapdes.Controls.Add(regcapdes(i))
            'pnl_ctascapdes.Controls.Add(cr(i))
            Session("rs").movenext()
            i += 1
        Loop
        Session("Con").Close()

        'Session("txtcapdes") = txtcapdes
        'Session("saldocapdes") = saldocapdes

    End Sub

    Private Sub Llenacmbctasdes()
        cmb_ctascapdes.Items.Clear()
        cmb_ctascapdes.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 25, "DEPRET")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPRET_CTAS_CAPTACION_DESTINO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Dim counter As Integer = 0
            Do While Not Session("rs").EOF
                cmb_ctascapdes.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString + (" Saldo: $" + Session("rs").Fields("SALDO").Value.ToString),
                                         "descmb_" + Session("rs").Fields("FOLIO").Value.ToString + "_" + counter.ToString))
                counter += 1
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenacmbctasori()
        cmb_ctascapori.Items.Clear()
        cmb_ctascapori.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 25, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_CRED", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPRET_CTAS_CAPTACION_ORIGEN"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Dim counter As Integer = 0
            Do While Not Session("rs").EOF
                cmb_ctascapori.Items.Add(New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString + (" Saldo: $" + Session("rs").Fields("SALDO").Value.ToString),
                                         "oricmb_" + Session("rs").Fields("FOLIO").Value.ToString + "_" + counter.ToString))
                counter += 1
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub deposito()
        lbl_info.Text = ""
        lbl_info0.Text = ""
        If rad_deposito.Checked = True Then
            'rad_deposito.CssClass = "textonegritasgrande"
            'rad_retiro.CssClass = "texto"
            txt_cajaori.Enabled = True
            txt_cajaori.Text = ""
            pnl_ctascapori.Enabled = True
            pnl_Usuario.Visible = True
            txt_cajades.Enabled = False
            pnl_ctascapdes.Enabled = True
            'lbl_operacion.Text = "DEPOSITO"
            bancos()
            cheques()
            pnl_ctascapdes.Visible = False
            lbl_ctascapdes.Visible = False
            div_cmbctascapdes.Visible = True
            'Req_ctascapdes.Enabled = True
            pnl_ctascapori.Visible = True
            lbl_ctascapori.Visible = True
            div_cmbctascapori.Visible = False
            'Req_ctascapori.Enabled = False
            Llenacmbctasdes()
            limpiaReferencia()
            cmb_modo_rec.Enabled = True
            Llena_Origen()
            LimpiaTransferencia()
            pnl_forma_transferencia.Visible = False
            Habilitar_Who(1)
            btn_aplicar.Text = "Aplicar depósito"
        Else
            'rad_retiro.CssClass = "textonegritasgrande"
            'rad_deposito.CssClass = "texto"
            txt_cajaori.Enabled = False
            pnl_ctascapori.Enabled = True
            txt_cajades.Enabled = True
            txt_cajades.Text = ""
            pnl_ctascapdes.Enabled = True
            pnl_Usuario.Visible = False
            'lbl_operacion.Text = "RETIRO"
            bancos()
            cheques()
            pnl_ctascapdes.Visible = True
            lbl_ctascapdes.Visible = True
            div_cmbctascapdes.Visible = False
            'Req_ctascapdes.Enabled = False
            pnl_ctascapori.Visible = False
            lbl_ctascapori.Visible = False
            div_cmbctascapori.Visible = True
            'Req_ctascapori.Enabled = True
            Llenacmbctasori()
            limpiaReferencia()
            cmb_modo_rec.Enabled = False
            cmb_origen_dep.Enabled = False
            LimpiaTransferencia()
            pnl_forma_transferencia.Visible = False
            'No habilita el panel de preguntar quien es el que hace la acción
            Habilitar_Who(0)
            btn_aplicar.Text = "Aplicar retiro"
        End If
        borra_tablas()
        crea_tablas()
        Llenadatos()
        btn_aplicar.Enabled = True
    End Sub

    Private Sub retiro()
        lbl_info.Text = ""
        lbl_info0.Text = ""
        If rad_retiro.Checked Then
            'rad_retiro.CssClass = "textonegritasgrande"
            'rad_deposito.CssClass = "texto"
            txt_cajaori.Enabled = False
            pnl_ctascapori.Enabled = True
            txt_cajades.Enabled = True
            txt_cajades.Text = ""
            pnl_ctascapdes.Enabled = True
            pnl_Usuario.Visible = False
            'lbl_operacion.Text = "RETIRO"
            bancos()
            cheques()
            pnl_ctascapdes.Visible = True
            lbl_ctascapdes.Visible = True
            div_cmbctascapdes.Visible = False
            'Req_ctascapdes.Enabled = False
            pnl_ctascapori.Visible = False
            lbl_ctascapori.Visible = False
            div_cmbctascapori.Visible = True
            'Req_ctascapori.Enabled = True
            Llenacmbctasori()
            limpiaReferencia()
            cmb_modo_rec.Enabled = False
            cmb_origen_dep.Enabled = False
            LimpiaTransferencia()
            pnl_forma_transferencia.Visible = False
            'No habilita el panel de preguntar quien es el que hace la acción
            Habilitar_Who(0)
            btn_aplicar.Text = "Aplicar retiro"
        Else
            'rad_deposito.CssClass = "textonegritasgrande"
            'rad_retiro.CssClass = "texto"
            txt_cajaori.Enabled = True
            txt_cajaori.Text = ""
            pnl_ctascapori.Enabled = True
            pnl_Usuario.Visible = True
            txt_cajades.Enabled = False
            pnl_ctascapdes.Enabled = True
            'lbl_operacion.Text = "DEPOSITO"
            bancos()
            cheques()
            pnl_ctascapdes.Visible = False
            lbl_ctascapdes.Visible = False
            div_cmbctascapdes.Visible = True
            'Req_ctascapdes.Enabled = True
            pnl_ctascapori.Visible = True
            lbl_ctascapori.Visible = True
            div_cmbctascapori.Visible = False
            'Req_ctascapori.Enabled = False
            Llenacmbctasdes()
            limpiaReferencia()
            cmb_modo_rec.Enabled = True
            Llena_Origen()
            LimpiaTransferencia()
            pnl_forma_transferencia.Visible = False
            Habilitar_Who(1)
            btn_aplicar.Text = "Aplicar depósito"
        End If
        borra_tablas()
        crea_tablas()
        Llenadatos()
        btn_aplicar.Enabled = True
    End Sub

    Private Sub bancos()
        If rad_deposito.Checked Then
            cmb_bancoori.Enabled = True
            txt_montobancosori.Enabled = True
            cmb_origen_dep.Enabled = True
            btn_agregarbancosori.Enabled = True
            cmb_bancodes.Enabled = False
            txt_montobancodes.Enabled = False
            btn_agreagrbancosdes.Enabled = False
            cmb_bancodes_des.Enabled = False
            txt_clabe_banco_des.Enabled = False
            txt_cta_banco_des.Enabled = False
        Else
            cmb_bancoori.Enabled = False
            txt_montobancosori.Enabled = False
            cmb_origen_dep.Enabled = False
            btn_agregarbancosori.Enabled = False
            cmb_bancodes.Enabled = True
            txt_montobancodes.Enabled = True
            btn_agreagrbancosdes.Enabled = True
            cmb_bancodes_des.Enabled = True
            txt_clabe_banco_des.Enabled = True
            txt_cta_banco_des.Enabled = True
        End If
    End Sub

    Private Sub cheques()
        If rad_deposito.Checked Then
            cmb_bancochequesori.Enabled = True
            txt_ctachequesori.Enabled = True
            txt_montochequesori.Enabled = True
            txt_numchequesori.Enabled = True
            btn_agregarchequesori.Enabled = True
            cmb_bancochequesdes.Enabled = False
            txt_montochequesdes.Enabled = False
            txt_numchequesdes.Enabled = False
            btn_agregarchequesdes.Enabled = False
        Else
            cmb_bancochequesori.Enabled = False
            txt_ctachequesori.Enabled = False
            txt_montochequesori.Enabled = False
            txt_numchequesori.Enabled = False
            btn_agregarchequesori.Enabled = False
            cmb_bancochequesdes.Enabled = True
            txt_montochequesdes.Enabled = True
            txt_numchequesdes.Enabled = True
            btn_agregarchequesdes.Enabled = True
        End If
    End Sub

    Private Sub limpiaReferencia()
        txt_nombre1.Text = ""
        txt_nombre2.Text = ""
        txt_paterno.Text = ""
        txt_materno.Text = ""
        txt_notas.Text = ""
    End Sub

    Private Sub LimpiaTransferencia()
        txt_montobancosori.Text = ""
        cmb_origen_dep.SelectedIndex = 0
        cmb_banco_des_dep.SelectedIndex = 0
        txt_cta_dep.Text = ""
        txt_titular_cta_dep.Text = ""
    End Sub

    Private Sub LimpiaTodo_Deposito()
        Session("bancos") = Nothing
        Session("cheques") = Nothing
        Session("caja") = Nothing
        Session("ctasori") = Nothing
        Session("IFE") = Nothing
        Session("GUADAR_USUARIO") = Nothing
        Session("efectivo") = Nothing

        LimpiaGenerales()
        LimpiaCtasDep()
        LimpiaEfectivo()
        LimpiaBancos()
        LimpiaCheques()

        btn_aplicar.Text = "Aplicar"
        btn_aplicar.Enabled = False

    End Sub

    Private Sub LimpiaTodo_Retiro()
        Session("bancos") = Nothing
        Session("cheques") = Nothing
        Session("caja") = Nothing
        Session("ctasdes") = Nothing

        LimpiaGenerales()
        LimpiaCtasDep()
        LimpiaEfectivo()
        LimpiaBancos()
        LimpiaCheques()
        btn_aplicar.Text = "Aplicar"
        btn_aplicar.Enabled = False
    End Sub

    Private Sub LimpiaGenerales()

        borra_tablas()

        ' Session("PERSONAID") = Nothing
        lnk_RepOp.Enabled = False
        lnk_PerAut.Enabled = False

        'Sección de Cliente y seleccion de depósito o retiro
        txt_cliente.Text = ""
        lbl_cliente.Text = ""
        'rad_usuario.Visible = False
        'rad_cliente.Visible = False
        lbl_depret.Visible = False 'Acción (depósito/retiro)
        rad_deposito.Visible = False
        rad_retiro.Visible = False

        limpiaReferencia()
        pnl_Usuario.Visible = False
        Habilitar_Who(0)
    End Sub

    Private Sub Habilitar_Who(ByVal semaforo As Boolean)

        If semaforo = True Then
            rad_cliente.Checked = False
            rad_usuario.Checked = False
            'rad_cliente.CssClass = "texto"
            'rad_usuario.CssClass = "texto"
        End If

        pnl_Usuario.Visible = semaforo

        pnl_usuario_pf.Visible = False 'Búsqueda de usuario/Nuevo Usuario

    End Sub

    'Función que oculta páneles de movimientos
    Private Sub LimpiaCtasDep()

        pnl_origenDeposito.Visible = False
        pnl_origenRetiro.Visible = False
        pnl_destinoRetiro.Visible = False
        pnl_destinoDeposito.Visible = False

        lbl_ctascapdes.Visible = False
        pnl_ctascapdes.Visible = False

        div_cmbctascapori.Visible = False
        div_cmbctascapdes.Visible = False

        'Inversión periódica
        lbl_inv_per.Visible = False
        cmb_inv_per.Visible = False
        pnl_ctascapori.Visible = False

    End Sub

    Private Sub LimpiaEfectivo()
        txt_cajaori.Enabled = False
        txt_cajaori.Text = ""
        txt_cajades.Enabled = False
        txt_cajades.Text = ""
    End Sub

    Private Sub LimpiaBancos()

        'Bancos origen
        cmb_bancoori.Items.Clear()
        cmb_bancoori.Enabled = False
        txt_montobancosori.Enabled = False
        txt_montobancosori.Text = ""
        cmb_origen_dep.Items.Clear()
        cmb_origen_dep.Enabled = False
        cmb_banco_des_dep.Items.Clear()
        'cmb_banco_des_dep.Enabled = False
        txt_cta_dep.Text = ""
        txt_titular_cta_dep.Text = ""
        btn_agregarbancosori.Enabled = False

        'Bancos destino
        cmb_bancodes.Items.Clear()
        cmb_bancodes.Enabled = False
        txt_montobancodes.Enabled = False
        txt_montobancodes.Text = ""
        cmb_bancodes_des.Items.Clear()
        'cmb_bancodes_des.Enabled = False
        txt_clabe_banco_des.Text = ""
        txt_cta_banco_des.Text = ""
        btn_agreagrbancosdes.Enabled = False

        lbl_infobancoori.Text = ""
        lbl_infobancodes.Text = ""

    End Sub

    Private Sub LimpiaCheques()
        cmb_bancochequesori.Enabled = False
        cmb_bancochequesori.Items.Clear()

        txt_ctachequesori.Enabled = False
        txt_ctachequesori.Text = ""
        txt_numchequesori.Enabled = False
        txt_numchequesori.Text = ""
        txt_montochequesori.Enabled = False
        txt_montochequesori.Text = ""
        cmb_modo_rec.Enabled = False
        cmb_modo_rec.SelectedIndex = "0"
        btn_agregarchequesori.Enabled = False

        cmb_bancochequesdes.Enabled = False
        cmb_bancochequesdes.Items.Clear()
        txt_numchequesdes.Text = ""
        txt_montochequesdes.Text = ""
        btn_agregarchequesdes.Enabled = False

        lbl_infochequesdes.Text = ""
        lbl_infochequesori.Text = ""

    End Sub

    Private Sub borra_tablas()
        Session("tabla_bancosori").Clear()
        dag_bancosori.DataSource = Session("tabla_bancosori")
        dag_bancosori.DataBind()
        Session("tabla_chequesori").Clear()
        dag_chequesori.DataSource = Session("tabla_chequesori")
        dag_chequesori.DataBind()
        div_dag_chequesori.Visible = True
        Session("tabla_bancosdes").Clear()
        dag_bancosdes.DataSource = Session("tabla_bancosdes")
        dag_bancosdes.DataBind()
        Session("tabla_chequesdes").Clear()
        dag_chequesdes.DataSource = Session("tabla_chequesdes")
        dag_chequesdes.DataBind()
    End Sub

    Private Sub crea_tablas()
        Dim tabla_bancosori As New DataTable
        Dim tabla_chequesori As New DataTable
        Dim tabla_bancosdes As New DataTable
        Dim tabla_chequesdes As New DataTable

        tabla_bancosori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO", GetType(String))
        tabla_bancosori.Columns.Add("MONTO", GetType(Decimal))
        tabla_bancosori.Columns.Add("ID_ORIGEN", GetType(Integer))
        tabla_bancosori.Columns.Add("ORIGEN", GetType(String))
        tabla_bancosori.Columns.Add("ID_BANCO_DESTINO", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO_DESTINO", GetType(String))
        tabla_bancosori.Columns.Add("NUM_CTA_DESTINO", GetType(String))
        tabla_bancosori.Columns.Add("TITULAR_DESTINO", GetType(String))

        tabla_chequesori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_chequesori.Columns.Add("ID_BANCO", GetType(Integer))
        tabla_chequesori.Columns.Add("BANCO", GetType(String))
        tabla_chequesori.Columns.Add("NUMCUENTA", GetType(String))
        tabla_chequesori.Columns.Add("CHEQUE", GetType(String))
        tabla_chequesori.Columns.Add("MONTO", GetType(Decimal))
        tabla_chequesori.Columns.Add("ESTATUS", GetType(String))

        tabla_bancosdes.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancosdes.Columns.Add("BANCO", GetType(String))
        tabla_bancosdes.Columns.Add("MONTO", GetType(Decimal))
        tabla_bancosdes.Columns.Add("ID_BANCO_CLIENTE", GetType(Integer))
        tabla_bancosdes.Columns.Add("BANCO_CLIENTE", GetType(String))
        tabla_bancosdes.Columns.Add("CLABE", GetType(String))
        tabla_bancosdes.Columns.Add("NUM_CTA_CLIENTE", GetType(String))

        tabla_chequesdes.Columns.Add("ID_CTA", GetType(Integer))
        tabla_chequesdes.Columns.Add("ID_BANCO", GetType(Integer))
        tabla_chequesdes.Columns.Add("BANCO", GetType(String))
        tabla_chequesdes.Columns.Add("NUMCUENTA", GetType(String))
        tabla_chequesdes.Columns.Add("CHEQUE", GetType(String))
        tabla_chequesdes.Columns.Add("MONTO", GetType(Decimal))
        tabla_chequesdes.Columns.Add("ESTATUS", GetType(String))

        Session("tabla_bancosori") = tabla_bancosori
        Session("tabla_chequesori") = tabla_chequesori
        Session("tabla_bancosdes") = tabla_bancosdes
        Session("tabla_chequesdes") = tabla_chequesdes
    End Sub

    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_seleccionar.Click
        lbl_info.Text = ""
        'lbl_depret.Text = ""

        Session("idperbusca") = Nothing

        'SE VERIFICA SI CUENTA CON LA FACULTAD DE VER SALDOS, Levantando la variable de session ("FACULTAD_SALDOS")
        revisa_facultad_saldos(CInt(Session("USERID")))

        Session("PERSONAID") = CInt(txt_cliente.Text)
        lnk_RepOp.Enabled = False
        borra_tablas()
        crea_tablas()
        Llenadatos()
        limpiaReferencia()
        rad_deposito.Checked = False
        rad_retiro.Checked = False
        rad_deposito.CssClass = "texto"
        rad_retiro.CssClass = "texto"

        'Se limpian todos los componentes del ASPX
        'Paneles de movimientos
        If pnl_origenDeposito.Visible Or pnl_ctascapdes.Visible Then
            LimpiaCtasDep()
        End If

        'Panel de selección de usuario/cliente
        If pnl_Usuario.Visible Then
            Habilitar_Who(0)
        End If

        'Botón de aplicar
        If btn_aplicar.Enabled Then
            btn_aplicar.Text = "Aplicar"
            btn_aplicar.Enabled = False
        End If

    End Sub

    Protected Sub btn_seleccionar_ori_Click(sender As Object, e As EventArgs) Handles btn_seleccionar_ori.Click

        lbl_nombre_cliente_ori.Text = ""
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_USUARIO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            lbl_nombre_cliente_ori.Text = Session("rs").fields("PROSPECTO").value.ToString
        Else
            lbl_info.Text = "Error: No existe el número de cliente."
        End If
        Session("Con").Close()

    End Sub

    Protected Sub rad_deposito_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_deposito.CheckedChanged
        pnl_origenRetiro.Visible = False
        pnl_destinoRetiro.Visible = False
        pnl_origenDeposito.Visible = True
        pnl_destinoDeposito.Visible = True
        lnk_RepOp.Enabled = False
        lnk_PerAut.Enabled = False
        Session("FOLIO") = Nothing
        deposito()
    End Sub


    Protected Sub rad_retiro_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_retiro.CheckedChanged
        pnl_origenDeposito.Visible = False
        pnl_destinoDeposito.Visible = False
        pnl_origenRetiro.Visible = True
        pnl_destinoRetiro.Visible = True

        lnk_RepOp.Enabled = False
        lnk_PerAut.Enabled = False
        Session("FOLIO") = Nothing
        retiro()
    End Sub

    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()
        cmb_localidad.Items.Clear()

        If txt_cp.Text = "" Then
            Exit Sub
        End If

        Dim idedo As String, idmuni As String

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP
            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value

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

        'si se encontraron estado y municipio validos para el cp ingresado entonces busco las localidades correspondientes
        If idedo <> "" And idmuni <> "" Then

            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, idedo)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDMUN", Session("adVarChar"), Session("adParamInput"), 10, idmuni)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_LOCALIDAD_MUNI_EDO"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then
                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("LOCALIDAD").Value.ToString, Session("rs").Fields("IDLOC").Value.ToString)
                    cmb_localidad.Items.Add(item)
                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
        End If

    End Sub

    'función que regresa un input con las caracteristicas necesarias para representar una cuenta de Captaciones (Origen)
    Function newCuentaOrigenField(ByVal id As String, ByVal title As String, ByVal monto As String) As HtmlGenericControl
        ' se declaran variables
        Dim outer_div As New HtmlGenericControl
        Dim outer_2_div As New HtmlGenericControl
        Dim content_div As New HtmlGenericControl
        Dim input As New TextBox
        'Dim labels_div As New HtmlGenericControl
        Dim title_label As New Label
        'Dim requiredFieldValidator As New RequiredFieldValidator
        Dim filtro As New AjaxControlToolkit.FilteredTextBoxExtender
        Dim filtroE As New RegularExpressionValidator
        'se establezen las caracterizticas del control
        outer_div.TagName = "div"
        outer_div.Attributes("class") = "module_subsec_elements"

        outer_2_div.TagName = "div"
        outer_2_div.Attributes("class") = "module_subsec_elements vertical flex_1"

        content_div.TagName = "div"
        'content_div.Attributes("class") = "module_subsec_elements_content text_input_nice_div "
        content_div.Attributes("class") = "module_subsec columned no_m align_items_flex_center "

        input.CssClass = "module_subsec_elements flex_1 text_input_nice_input" '"text_input_nice_input"
        input.ID = "txt_cuentaOrigen" & id
        input.Text = monto

        'labels_div.TagName = "div"
        'labels_div.Attributes("class") = "text_input_nice_labels"

        title_label.ID = "lbl_cuentaOrigen" & id
        title_label.CssClass = "text_input_nice_label"
        title_label.Text = title

        'requiredFieldValidator.ControlToValidate = input.ID
        'requiredFieldValidator.CssClass = "alertaValidator bold"
        'requiredFieldValidator.Text = "Falta Dato"
        'requiredFieldValidator.Display = ValidatorDisplay.Dynamic

        filtroE.ControlToValidate = input.ID
        filtroE.CssClass = "alertaValidator bold"
        filtroE.Text = "Formato Erroneo"
        filtroE.Display = ValidatorDisplay.Dynamic
        filtroE.ValidationExpression = "[0-9]{0,17}(\.[0-9][0-9]?)?"

        filtro.TargetControlID = input.ID
        filtro.FilterType = AjaxControlToolkit.FilterTypes.Custom
        filtro.ValidChars = ".0123456789"

        'se agregan las características al control
        content_div.Controls.Add(title_label)
        'labels_div.Controls.Add(requiredFieldValidator)
        content_div.Controls.Add(filtroE)

        content_div.Controls.Add(input)
        content_div.Controls.Add(filtro)
        'content_div.Controls.Add(labels_div)

        outer_2_div.Controls.Add(content_div)
        outer_div.Controls.Add(outer_2_div)

        'guarda el textbox del control en una lista de los text box
        CuentasCaptacionOrigen.Add(input)
        'regresa el control
        Return outer_div
    End Function

    'función que regresa un input con las caracteristicas necesarias para representar una cuenta de Captaciones (Destino)
    Function newCuentaDestinoField(ByVal id As String, ByVal title As String, ByVal monto As String) As HtmlGenericControl
        ' se declaran variables
        Dim outer_div As New HtmlGenericControl
        Dim outer_2_div As New HtmlGenericControl
        Dim content_div As New HtmlGenericControl
        Dim input As New TextBox
        'Dim labels_div As New HtmlGenericControl
        Dim title_label As New Label
        'Dim requiredFieldValidator As New RequiredFieldValidator
        Dim filtro As New AjaxControlToolkit.FilteredTextBoxExtender
        Dim filtroE As New RegularExpressionValidator
        'se establezen las caracterizticas del control
        outer_div.TagName = "div"
        outer_div.Attributes("class") = "module_subsec_elements"

        outer_2_div.TagName = "div"
        outer_2_div.Attributes("class") = "module_subsec_elements vertical flex_1"

        content_div.TagName = "div"
        'content_div.Attributes("class") = "module_subsec_elements_content text_input_nice_div "
        content_div.Attributes("class") = "module_subsec columned no_m align_items_flex_center "

        input.CssClass = "module_subsec_elements flex_1 text_input_nice_input" '"text_input_nice_input"
        input.ID = "txt_cuentaDestino" & id
        input.Text = monto

        'labels_div.TagName = "div"
        'labels_div.Attributes("class") = "text_input_nice_labels"

        title_label.ID = "lbl_cuentaDestino" & id
        title_label.CssClass = "text_input_nice_label"
        title_label.Text = title

        'requiredFieldValidator.ControlToValidate = input.ID
        'requiredFieldValidator.CssClass = "alertaValidator bold"
        'requiredFieldValidator.Text = "Falta Dato"
        'requiredFieldValidator.Display = ValidatorDisplay.Dynamic

        filtroE.ControlToValidate = input.ID
        filtroE.CssClass = "alertaValidator bold"
        filtroE.Text = "Formato Erroneo"
        filtroE.Display = ValidatorDisplay.Dynamic
        filtroE.ValidationExpression = "[0-9]{0,17}(\.[0-9][0-9]?)?"

        filtro.TargetControlID = input.ID
        filtro.FilterType = AjaxControlToolkit.FilterTypes.Custom
        filtro.ValidChars = ".0123456789"

        'se agregan las características al control
        content_div.Controls.Add(title_label)
        'labels_div.Controls.Add(requiredFieldValidator)
        content_div.Controls.Add(filtroE)

        content_div.Controls.Add(input)
        content_div.Controls.Add(filtro)
        'content_div.Controls.Add(labels_div)

        outer_2_div.Controls.Add(content_div)
        outer_div.Controls.Add(outer_2_div)

        'guarda el textbox del control en una lista de los text box
        CuentasCaptacionDestino.Add(input)
        'regresa el control
        Return outer_div
    End Function

#End Region 'Region Cargar y Limpiar Entorno


#Region "PLD"

    Protected Sub lnk_RepOp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_RepOp.Click
        ClientScript.RegisterStartupScript(GetType(String), "Operacion Preocupante", "window.open(""RegistroOperacionPLD.aspx"", ""RP"", ""width=650px,height=450,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
    End Sub

    Protected Sub lnk_PerAut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_PerAut.Click
        ClientScript.RegisterStartupScript(GetType(String), "Personas Autorizadas para Retiro", "window.open(""PersonasAutorizadas.aspx"", ""RP"", ""width=650px,height=450,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
    End Sub

    '--------------------   ESCALAMIENTO DE OPERACIONES (PLD)   --------------------

    Function AutorizacionActiva(ByVal efectivo As Decimal) As Integer
        Dim Autoriza As Integer, LimitePF As Decimal, LimitePM As Decimal
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFLAVADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Autoriza = Session("rs").fields("AUTOR_OPE").value
            LimitePF = Session("rs").fields("LIM_OPEREL_PESOS_FISICA").value
            LimitePM = Session("rs").fields("LIM_OPEREL_PESOS_MORAL").value
        End If
        Session("Con").Close()
        Autoriza = IIf(Autoriza = 1, IIf(Session("TIPOPER") = "F", IIf(efectivo >= LimitePF, 1, 0), IIf(efectivo >= LimitePM, 1, 0)), 0)
        Return Autoriza
    End Function


    Protected Sub btn_AutorPLD_AUTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AutorPLD_AUTO.Click
        Dim valida As String
        If Not Session("ID_AUT") Is Nothing Then
            If ver_aut_remota_cheques() = False Then
                valida = val_user_aut_cheques()
                If Len(txt_Mtv.Text) > 1000 Then
                    lbl_InfoAutoriza.Text = "Error: El campo de motivo o razón debe ser menor o igual a 1000 caracteres."
                    ModalPopupExtender2.Show()
                Else
                    If valida = "OK" Then
                        lbl_InfoAutoriza.Text = ""
                        autoriza_recepcion_cheques(cmb_Acc.SelectedItem.Value, Session("AUTOR_USR_CHEQUES"), txt_Mtv.Text)
                        ModalPopupExtender2.Hide()
                        If cmb_Acc.SelectedItem.Value = "AUTORIZADO" Then
                            dag_cheques_aut.Visible = False
                            If AutorizacionActiva(Session("caja")) = 1 Then
                                SolicitudAutorizacion(Session("caja"))
                                cambia_modal_autorizacion("PLD")
                                ModalPopupExtender2.Show()
                            Else
                                aplica_dep()
                                aplica_dep_after(Session("caja"), Session("cheques"), Session("bancos"), Session("ctasori"))
                            End If
                            Session("ID_AUT") = Nothing
                            Session("AUTOR_USR_CHEQUES") = Nothing
                        End If
                    Else
                        lbl_InfoAutoriza.Text = valida
                        ModalPopupExtender2.Show()
                    End If
                End If
            End If
        Else
            If VerificaAutorRemota() = 0 Then

                valida = VerificaUsrPwd()

                If valida = "OK" Then
                    If Len(txt_Mtv.Text) > 1000 Then
                        lbl_InfoAutoriza.Text = "Error: El campo de motivo o razón debe ser menor o igual a 1000 caracteres."
                        ModalPopupExtender2.Show()
                    Else
                        lbl_InfoAutoriza.Text = ""
                        AutorizacionOperacion(cmb_Acc.SelectedItem.Value, Session("AUTOR_USR"), txt_Mtv.Text)
                        ModalPopupExtender2.Hide()
                        If cmb_Acc.SelectedItem.Value = "AUTORIZADO" Then
                            aplica_dep()
                            aplica_dep_after(Session("caja"), Session("cheques"), Session("bancos"), Session("ctasori"))
                            FolioImpAutorizacion()
                            Session("IDAUTORIZACION") = Nothing
                        End If
                    End If

                    Session("AUTOR_EFECTIVO") = Nothing
                    Session("AUTOR_CHEQUES") = Nothing
                    Session("AUTOR_BANCOS") = Nothing
                    Session("AUTOR_USR") = Nothing

                    txt_Usr.Text = ""
                    txt_Pwd.Text = ""
                    txt_Mtv.Text = ""
                    cmb_Acc.SelectedValue = "0"

                Else
                    lbl_InfoAutoriza.Text = valida
                    ModalPopupExtender2.Show()
                End If

            End If
        End If

    End Sub

    Protected Sub btn_AutorPLD_CAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AutorPLD_CAN.Click
        If Not Session("ID_AUT") Is Nothing Then
            If ver_aut_remota_cheques() = False Then
                autoriza_recepcion_cheques("CANCELADO", -1, "")
                Session("ID_AUT") = Nothing
                Session("AUTOR_USR_CHEQUES") = Nothing
                ModalPopupExtender2.Hide()
            End If
        Else
            If VerificaAutorRemota() = 0 Then
                AutorizacionOperacion("CANCELADO", -1, "")
                Session("IDAUTORIZACION") = Nothing
                ModalPopupExtender2.Hide()
            End If
        End If
    End Sub

    Protected Sub btn_AUTOCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AUTOCerrar.Click

        lbl_InfoAutoriza.Text = ""
        ModalPopupExtender2.Hide()
        If cmb_Acc.SelectedItem.Value = "AUTORIZADO" Then
            If Not Session("ID_AUT") Is Nothing Then
                If AutorizacionActiva(Session("caja")) = 1 Then
                    SolicitudAutorizacion(Session("caja"))
                    cambia_modal_autorizacion("PLD")
                    ModalPopupExtender2.Show()
                Else
                    aplica_dep()
                    aplica_dep_after(Session("caja"), Session("cheques"), Session("bancos"), Session("ctasori"))
                End If
                Session("ID_AUT") = Nothing
                Session("AUTOR_USR_CHEQUES") = Nothing

            Else
                aplica_dep()
                aplica_dep_after(Session("caja"), Session("cheques"), Session("bancos"), Session("ctasori"))
                If Not Session("ID_AUT") Is Nothing Then
                    actualiza_folio_imp_aut_cheques()
                Else
                    FolioImpAutorizacion()
                    Session("IDAUTORIZACION") = Nothing
                End If

            End If

        End If

        Session("AUTOR_EFECTIVO") = Nothing
        Session("AUTOR_CHEQUES") = Nothing
        Session("AUTOR_BANCOS") = Nothing
        Session("AUTOR_USR") = Nothing

        txt_Usr.Enabled = True
        txt_Pwd.Enabled = True
        txt_Mtv.Enabled = True
        cmb_Acc.Enabled = True
        btn_AutorPLD_AUTO.Enabled = True
        btn_AutorPLD_CAN.Enabled = True
        lnk_ACT.Enabled = True
        btn_AUTOCerrar.Visible = False

        txt_Usr.Text = ""
        txt_Pwd.Text = ""
        txt_Mtv.Text = ""
        cmb_Acc.ClearSelection()
        cmb_Acc.Items.FindByValue("-1").Selected = True

    End Sub

    Private Sub AutorizacionOperacion(ByVal estatus As String, ByVal usuario As Integer, ByVal razon As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDAUTORIZACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDAUTORIZACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 50, usuario)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 1000, razon)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AUTORIZACION_OPERACION_PLD"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Function VerificaUsrPwd() As String

        Dim Valida As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("USER", Session("adVarChar"), Session("adParamInput"), 20, txt_Usr.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PASSWORD", Session("adVarChar"), Session("adParamInput"), 20, txt_Pwd.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AUTORIZACION_OPERACION_PLD_USRPWD"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Valida = Session("rs").fields("VALIDACION").value.ToString
            If Session("rs").fields("VALIDACION").value.ToString = "OK" Then
                Session("AUTOR_USR") = Session("rs").fields("IDUSUARIO").value.ToString
            End If
        End If
        Session("Con").Close()

        Return Valida

    End Function

    Private Sub actualiza_folio_imp_aut_cheques()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDAUTORIZACION", Session("adVarChar"), Session("adParamInput"), 11, Session("ID_AUT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AUTORIZACION_CHEQUES_FOLIO_IMP"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub FolioImpAutorizacion()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDAUTORIZACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDAUTORIZACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 10, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AUTORIZACION_OPERACION_PLD_FOLIOIMP"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Function VerificaAutorRemota() As Integer

        Dim Verifica As Integer

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDAUTORIZACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDAUTORIZACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AUTORIZACION_OPERACION_VER_REMOTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Verifica = Session("rs").fields("VERIFICA").value.ToString

            If Verifica = 1 Then
                txt_Mtv.Text = Session("rs").fields("RAZON").value.ToString
                cmb_Acc.SelectedValue = Session("rs").fields("ESTATUS").value.ToString
                lbl_InfoAutoriza.Text = "Se ha realizado el dictámen de la operación remotamente."

                txt_Usr.Enabled = False
                txt_Pwd.Enabled = False
                txt_Mtv.Enabled = False
                cmb_Acc.Enabled = False
                btn_AutorPLD_AUTO.Enabled = False
                btn_AutorPLD_CAN.Enabled = False
                lnk_ACT.Enabled = False
                btn_AUTOCerrar.Visible = True
            Else
                txt_Usr.Enabled = True
                txt_Pwd.Enabled = True
                txt_Mtv.Enabled = True
                cmb_Acc.Enabled = True
                btn_AutorPLD_AUTO.Enabled = True
                btn_AutorPLD_CAN.Enabled = True
                lnk_ACT.Enabled = True
                btn_AUTOCerrar.Visible = False
            End If

        End If
        Session("Con").Close()

        ModalPopupExtender2.Show()

        Return Verifica

    End Function

    'IDENTIFICACIÓN DE USUARIOS

    Protected Sub txt_cp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp.TextChanged
        'Response.Redirect("AltaModPersonaF.aspx")
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()
        cmb_localidad.Items.Clear()
    End Sub

    Function val_cuestionario_Efectivo(ByVal aplica As String) As String

        Dim Respuesta As String = ""

        Dim nombre1 As String, paterno As String, pais As String, nac As String, fechanac As String,
            idoficial As String, cp As String, estado As Integer, municipio As Integer, localidad As Integer, colonia As Integer, tipovialidad As Integer,
            calle As String, numext As String, idpersona As String, sexo As String

        idpersona = txt_IdCliente.Text
        nombre1 = txt_nombre1_u.Text
        paterno = txt_paterno_u.Text
        pais = cmb_pais.SelectedItem.Value
        nac = cmb_nac.SelectedItem.Value
        fechanac = txt_fechanac.Text
        idoficial = txt_id_oficial.Text
        cp = txt_cp.Text
        If cp = "" Then
            estado = 0
            municipio = 0
            localidad = 0
            colonia = 0
        Else
            estado = cmb_estado.SelectedItem.Value
            municipio = cmb_municipio.SelectedItem.Value
            localidad = cmb_localidad.SelectedItem.Value
            colonia = cmb_colonia.SelectedItem.Value
        End If

        tipovialidad = cmb_tipo_via.SelectedItem.Value
        calle = txt_calle.Text
        numext = txt_num_ext.Text

        If rad_sexo0.Checked = True Then
            sexo = "H"
        Else
            sexo = "M"
        End If

        ' SI EXCEDIERA DE LOS LIMITES, HAY QUE REGISTRAR
        If aplica = "REGISTRAR" Then
            ' VERIFICA QUE TENGA CAPTURADO LOS CAMPOS COMO OBLIGATORIOS
            If idpersona <> "" Or (nombre1 <> "" And paterno <> "" And pais <> "-1" And nac <> "-1" And fechanac <> "" And idoficial <> "" And cp <> "" And estado <> "-1" And municipio <> "-1" And localidad <> "-1" And colonia <> "-1" And tipovialidad <> "-1" And calle <> "" And numext <> "" And sexo <> "") Then
                Respuesta = "OK"
            Else
                Respuesta = "ERROR"
            End If
        Else
            Respuesta = "OK"
        End If

        Return Respuesta
    End Function

    Function val_rangos_Efectivo(ByVal monto As Decimal) As String
        Dim Respuesta As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 25, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPOSITO_VALIDACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Respuesta = Session("rs").Fields("RESPUESTA").value
            Session("IFE") = Session("rs").Fields("IFE").value
        End If

        Session("Con").Close()
        Return Respuesta
    End Function

    Function val_idPersona_existente(ByVal idpersona As Integer, idpersona_agregar As Integer) As String

        Dim Respuesta As String = ""

        Dim nombre1 As String, nombre2 As String, paterno As String, materno As String, pais As String, nac As String, fechanac As String,
           sexo As String

        'Significa que hicieron busqueda de persona
        If idpersona_agregar <> -1 Then
            nombre1 = ""
            nombre2 = ""
            materno = ""
            paterno = ""
            pais = 0
            nac = 0
            fechanac = ""
            sexo = ""

        Else
            idpersona_agregar = -1
            nombre1 = txt_nombre1_u.Text
            nombre2 = txt_nombre2_u.Text
            materno = txt_materno_u.Text
            paterno = txt_paterno_u.Text
            pais = cmb_pais.SelectedItem.Value
            nac = cmb_nac.SelectedItem.Value
            fechanac = txt_fechanac.Text

            If rad_sexo0.Checked = True Then 'seleccionaron hombre
                sexo = "H"
            Else 'seleccionaron mujer
                sexo = "M"
            End If

        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 25, idpersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA_AGREGAR", Session("adVarChar"), Session("adParamInput"), 25, idpersona_agregar)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, nombre1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, nombre2)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, paterno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, materno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LUGARNAC", Session("adVarChar"), Session("adParamInput"), 50, pais)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_NAC", Session("adVarChar"), Session("adParamInput"), 50, nac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 50, fechanac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 5, sexo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPOSITO_VALIDACION_PERSONA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Respuesta = Session("rs").Fields("RESPUESTA").value
        End If

        Session("Con").Close()
        Return Respuesta
    End Function

    Private Sub GuardaUsuario(ByVal folio As Integer, ByVal serie As String, ByVal folio_imp As String, ByVal IFE As String)

        Dim nombre1 As String, nombre2 As String, paterno As String, materno As String, pais As String, nac As String, fechanac As String,
            idoficial As String, cp As String, estado As Integer, municipio As Integer, localidad As Integer, colonia As Integer, tipovialidad As Integer,
            calle As String, numext As String, idpersona As String, numint As String, sexo As String

        idpersona = txt_IdCliente.Text

        'Significa que hicieron busqueda de persona
        If idpersona <> "" Then
            nombre1 = ""
            nombre2 = ""
            materno = ""
            paterno = ""
            pais = 0
            nac = 0
            fechanac = ""
            idoficial = ""
            cp = ""
            estado = 0
            municipio = 0
            localidad = 0
            colonia = 0
            tipovialidad = 0
            calle = ""
            numext = ""
            numint = ""

        Else
            idpersona = -1
            nombre1 = txt_nombre1_u.Text
            nombre2 = txt_nombre2_u.Text
            materno = txt_materno_u.Text
            paterno = txt_paterno_u.Text
            pais = cmb_pais.SelectedItem.Value
            nac = cmb_nac.SelectedItem.Value
            fechanac = txt_fechanac.Text
            idoficial = txt_id_oficial.Text
            cp = txt_cp.Text


            estado = cmb_estado.SelectedItem.Value
            municipio = cmb_municipio.SelectedItem.Value
            localidad = cmb_localidad.SelectedItem.Value
            colonia = cmb_colonia.SelectedItem.Value
            tipovialidad = cmb_tipo_via.SelectedItem.Value
            calle = txt_calle.Text
            numext = txt_num_ext.Text
            numint = txt_num_int.Text

        End If


        If rad_sexo0.Checked = True Then 'seleccionaron hombre
            sexo = "H"
        Else 'seleccionaron mujer
            sexo = "M"
        End If
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, idpersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, nombre1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, nombre2)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, paterno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, materno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LUGARNAC", Session("adVarChar"), Session("adParamInput"), 50, pais)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_NAC", Session("adVarChar"), Session("adParamInput"), 50, nac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 50, fechanac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 5, sexo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOFICIAL", Session("adVarChar"), Session("adParamInput"), 50, idoficial)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 10, colonia)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDLOCA", Session("adVarChar"), Session("adParamInput"), 10, localidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, municipio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, estado)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, tipovialidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, calle)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, numext)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, numint)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, cp)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 5, serie)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 10, folio_imp)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SUC", Session("adVarChar"), Session("adParamInput"), 20, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IFE", Session("adVarChar"), Session("adParamInput"), 20, IFE)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_DEPOSITO_USUARIO"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()
    End Sub

    Private Sub rad_cliente_CheckedChanged(sender As Object, e As EventArgs) Handles rad_cliente.CheckedChanged
        If rad_cliente.Checked Then
            pnl_usuario_pf.Visible = False
        End If
    End Sub

    Private Sub rad_usuario_CheckedChanged(sender As Object, e As EventArgs) Handles rad_usuario.CheckedChanged
        If rad_usuario.Checked Then
            pnl_usuario_pf.Visible = True

            Limpia_pf()
            LlenaPaises(cmb_pais.ID)
            llena_nac()
            llena_vialidad(cmb_tipo_via.ID)
        Else
            pnl_usuario_pf.Visible = False

        End If
    End Sub

    Private Sub Limpia_pf()
        txt_IdCliente.Text = ""
        txt_nombre1_u.Text = ""
        txt_nombre2_u.Text = ""
        txt_paterno_u.Text = ""
        txt_materno_u.Text = ""
        cmb_pais.Items.Clear()
        cmb_nac.Items.Clear()
        txt_fechanac.Text = ""
        txt_id_oficial.Text = ""
        txt_cp.Text = ""
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_localidad.Items.Clear()
        cmb_colonia.Items.Clear()
        cmb_tipo_via.Items.Clear()
        txt_calle.Text = ""
        txt_num_ext.Text = ""
        txt_num_int.Text = ""
        rad_sexo0.Checked = True
        rad_sexo1.Checked = False
        lbl_nombre_cliente_ori.Text = ""
    End Sub

    Private Sub LlenaPaises(ByVal id As String)
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento
        If id = "cmb_pais" Then
            cmb_pais.Items.Clear()
            cmb_pais.Items.Add(New ListItem("ELIJA", "-1"))
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_PAISES"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_pais.Items.Add(New ListItem(Session("rs").Fields("CATPAIS_PAIS").Value, Session("rs").Fields("CATPAIS_ID_PAIS").Value.ToString))
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If

    End Sub

    Private Sub llena_vialidad(ByVal id As String)
        'Procedimiento que obtiene el catálogo de vialidades y las despliega en el combo correspondiente
        Dim elija As New ListItem("ELIJA", "-1")
        If id = "cmb_tipo_via" Then
            cmb_tipo_via.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_VIALIDAD"
            Session("rs") = Session("cmd").Execute()
            cmb_tipo_via.Items.Add(elija)
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
                cmb_tipo_via.Items.Add(item)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If

    End Sub

    Private Sub llena_nac()
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento

        cmb_nac.Items.Clear()
        cmb_nac.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_NACIONALIDADES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_nac.Items.Add(New ListItem(Session("rs").Fields("NAC").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub
#End Region 'Region PLD


#Region "Correos Tesoreria"
    Protected Sub LlenaDatosGenerales()

        Session("Con").Open()
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

        Session("Con").Close()

    End Sub

    Protected Sub ObtieneCorreosFinanzas()

        Dim CuerpoCorreo As String = ""
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
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
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
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td width='25%'>Cheque</td></td></tr>")
                sbhtml.Append("<tr><td width='25%'>Se le avisa que debe realizar la transferencia bancaria y/o expedición de cheque por el retiro a cuenta de captación con los siguientes datos:</td></td></tr>")
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
            subject = "Aviso de transferencia / cheque por retiro de cuenta de captación."
            usuario = Session("USUARIO").ToString
            sucursal = Session("SUCURSAL").ToString
            folio = Session("FOLIO").ToString
            prospecto = Session("PROSPECTO").ToString
            personaid = Session("PERSONAID").ToString
            Do While Not Session("rs").EOF
                nombre = Session("rs").Fields("NOMBRE").Value.ToString
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td>Estimado(a):  " + nombre + "</td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br />")
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
                sbhtml.Append("<tr><td width='25%'>Se le avisa que debe realizar la transferencia bancaria y/o expedición de cheque por el retiro a cuenta de captación con los siguientes datos:</td></td></tr>")
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



#End Region


    Protected Sub Ticket_PDF()

        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TEMPORAL.pdf")

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
            .AddAuthor("Ticket - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Ticket ")
            .AddSubject("Ticket")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Ticket")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Ticket")
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        Dim X, Y As Single
        X = 300
        Y = 675

        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, infoTicket.ToString, X, Y, 0)

        Dim ctas As Boolean = False
        Dim displayString As String, contrato As String, nombre_producto As String, saldo_total As String, saldo_dis As String, fecha As String, sub_mov As String
        Dim len As Integer, minus As Integer, lft As Integer
        Dim id_persona As Integer

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "         CAJA DE LA SIERRA GORDA        ", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           S.A. DE C.V. S.F.P.          ", X, Y, 0)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_FICHA_GRAL"
        Session("rs") = Session("cmd").Execute()

        Dim RefNombreCap As String = ""

        If Not Session("rs").EOF Then
            id_persona = Session("rs").Fields("IDPERSONA").Value

            If rad_deposito.Checked Then
                RefNombreCap = "Ref: " + Session("rs").Fields("NOMBRE_OP").Value

            Else

                RefNombreCap = ""
            End If

            If id_persona <> -1 Then
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                      ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                      ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("DIR1").Value.ToString + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("DIR2").Value.ToString + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("FECHA_HORA").Value.ToString + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("NOMBRE").Value.ToString + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("FECHA_HORA").Value.ToString + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "FOLIO: " + Session("SERIE") + Session("FOLIO_IMP") + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "NUM CLIENTE: " + Session("rs").Fields("IDPERSONA").Value.ToString + "         ", X, Y, 0)

                contrato = Session("rs").Fields("FOLIO").Value.ToString
                nombre_producto = Session("rs").Fields("NOMBRE_PROD").Value
                saldo_total = Session("rs").Fields("SALDO_TOTAL").Value.ToString
                saldo_dis = Session("rs").Fields("SALDO_DIS").Value.ToString
                fecha = Left(Session("rs").Fields("FECHA_HORA").Value.ToString, 10)
                If Session("rs").Fields("CTAS_CAP").Value.ToString = "1" Then
                    ctas = True
                End If
            Else
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           FOLIO:        " + Session("SERIE") + Session("FOLIO_IMP") + "         ", X, Y, 0)

                len = Session("rs").Fields("CONCEPTO").Value.Length
                If len > 26 Then
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "CONCEPTO:     " + Mid(Session("rs").Fields("CONCEPTO").Value, 1, 26) + "         ", X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "              " + Mid(Session("rs").Fields("CONCEPTO").Value, 27, len) + "         ", X, Y, 0)

                Else
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "CONCEPTO:     " + Session("rs").Fields("CONCEPTO").Value + "         ", X, Y, 0)

                End If

                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "CUENTA CARGO: " + Session("rs").Fields("CTA_CARGO").Value + "         ", X, Y, 0)

                len = Session("rs").Fields("DESC_CARGO").Value.Length

                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, IIf(len > 26, "              " + Mid(Session("rs").Fields("DESC_CARGO").Value, 1, 26)  x,y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Mid(Session("rs").Fields("DESC_CARGO").Value, 27, len) + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("DESC_CARGO").Value + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "CUENTA ABONO: " + Session("rs").Fields("CTA_ABONO").Value + "         ", X, Y, 0)

                len = Session("rs").Fields("DESC_ABONO").Value.Length

                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           "+  IIf(len > 26, "              " + Mid(Session("rs").Fields("DESC_ABONO").Value, 1, 26)  +"         ", x,y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Mid(Session("rs").Fields("DESC_ABONO").Value, 27, len) + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Mid(Session("rs").Fields("DESC_ABONO").Value, 27, len) + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + Session("rs").Fields("DESC_ABONO").Value + "         ", X, Y, 0)

                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "MONTO:        " + Session("rs").Fields("MONTO").ValuE.ToString + "         ", X, Y, 0)
                Y = Y - 10
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "FECHA:        " + Left(Session("rs").Fields("FECHA").Value.ToString, 10) + "         ", X, Y, 0)

            End If

        End If
        Session("Con").Close()
        If id_persona <> -1 Then
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                    ", X, Y, 0)
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "Detalle de movimientos       Movimientos" + "         ", X, Y, 0)
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "           " + "----------------------------------------" + "         ", X, Y, 0)

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_FICHA"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then
                Do While Not Session("rs").EOF

                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("DESCRIPCION_MOV").Value.ToString, X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("MONTO").Value.ToString), X, Y, 0)


                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "         ----------------------------------------           ", X, Y, 0)
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, nombre_producto + " (NO. CTA.: " + contrato + ")     ", X, Y, 0)

        End If

        If ctas = True Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_FICHA_OTRAS_CTAS"
            Session("rs") = Session("cmd").Execute()

            'If Not Session("rs").EOF Then
            If Not Session("rs").EOF Then
                Do While Not Session("rs").EOF
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "         ----------------------------------           ", X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("PRODUCTO").Value + " (NO. CTA.: " + Session("MascoreG").FormatNumberCurr(Session("rs").Fields("FOLIO").Value.ToString) + ")", X, Y, 0)

                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
        End If

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                   ", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                   ", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "         -          ", X, Y, 0)

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "             ---------------              ", X, Y, 0)

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "              Firma del Cajero              ", X, Y, 0)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_USUARIO"
        Session("rs") = Session("cmd").Execute()

        Y = Y - 10
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "             " + Session("rs").Fields("USUARIO").Value.ToString + "              ", X, Y, 0)

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "             ---------------              ", X, Y, 0)

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "              Firma del  Cliente              ", X, Y, 0)

        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                           ", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "                           ", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "     * No Valido sin Sello y Firma del Cajero           ", X, Y, 0)

        displayString += vbCrLf + "* No Valido sin Sello y Firma del Cajero" & vbCrLf

        cb.EndText()

        document.Close()

    End Sub

End Class