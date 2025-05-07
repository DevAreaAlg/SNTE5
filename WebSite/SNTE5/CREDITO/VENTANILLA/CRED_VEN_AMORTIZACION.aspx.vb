Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class CRED_VEN_AMORTIZACION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Amortizador", "Amortizador de Préstamo")
        If Not Me.IsPostBack Then
            crea_tablas()
            LlenaInstituciones()
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

        If Session("PERSONAID") <> Nothing And cmb_folio.Items.Count > 1 Then
            Llenactascapori()
        End If

        HiddenPrinterName.Value = Session("IMPRESORA_TICKET")
        'HiddenPrinterName.Value = "Canon MF4800 Series UFRII LT"

    End Sub

    Private Sub LlenaInstituciones()

        ddl_Instituciones.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        ddl_Instituciones.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Instituciones.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_seleccionar.Click
        lbl_info.Text = ""
        lbl_estatus.Text = ""
        Session("PERSONAID") = CInt(txt_cliente.Text)
        limpia_datos()
        Llenadatos()
        cmb_folio.Enabled = True
    End Sub

    Protected Sub cmb_folio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_folio.SelectedIndexChanged
        If cmb_folio.SelectedValue = -1 Then
            btn_pagar.Enabled = False
        Else
            llena_informacion_folio()
        End If

    End Sub

    Protected Sub OnTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fechacorte.TextChanged
        If txt_fechacorte.Text <> "" Then
            If CDate(txt_fechacorte.Text) > CDate(Session("FECHA_SISTEMA")) Then
                Dim custDA As New System.Data.OleDb.OleDbDataAdapter(), dtPendientes As New Data.DataTable()
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("FECHA_AD_V", Session("adVarChar"), Session("adParamInput"), 10, txt_fechacorte.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_PAGO_AD"
                Session("rs") = Session("cmd").Execute()
                custDA.Fill(dtPendientes, Session("rs"))
                grd_pago.DataSource = dtPendientes
                grd_pago.DataBind()
                Session("Con").Close()
                lbl_info_fecha_corte.Visible = False
            Else
                lbl_info_fecha_corte.Visible = True
            End If
        End If
    End Sub

    Protected Sub btn_pagar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagar.Click
        Session("RES") = "0"
        Dim sefectivo As String, efectivo As Decimal
        sefectivo = txt_cajaori.Text
        If sefectivo <> "" Then
            efectivo = CDec(sefectivo)
        Else
            efectivo = 0.0
        End If

        aplica_pago()

    End Sub

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
        Session("parm") = Session("cmd").CreateParameter("TIPO_OPERACION", Session("adVarChar"), Session("adParamInput"), 50, "AMORTIZACION")
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

    Private Sub cambia_modal_autorizacion(mode As String)
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

    Protected Sub lnk_ProvRec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ProvRec.Click

        ClientScript.RegisterStartupScript(GetType(String), "Proveedores de Recurso", "window.open(""CRED_VEN_PROV_REC.aspx"", ""RP"", ""width=650px,height=450,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)

    End Sub

    Private Sub aplica_pago()

        lbl_info.Text = ""
        Dim idpersona_busqueda As Integer

        If txt_IdCliente.Text = "" Then
            idpersona_busqueda = -1
        Else
            idpersona_busqueda = CInt(txt_IdCliente.Text)
        End If

        If txt_nombre1.Text = "" And txt_nombre2.Text = "" And txt_paterno.Text = "" And txt_materno.Text = "" Then
            lbl_info.Text = ""
        Else
            'Tener como mínimo la referencia primer nombre y Paterno
            If (txt_nombre1.Text = "" And txt_paterno.Text = "") Or (txt_nombre1.Text <> "" And txt_paterno.Text = "") Or (txt_nombre1.Text = "" And txt_paterno.Text <> "") Then
                lbl_info.Text = "Agregue como mínimo Primer Nombre y Paterno"
                Exit Sub
            End If
        End If

        'máximo 3000 caracteres
        If txt_notas.Text.Length > 3000 Then
            lbl_info.Text = "Error: Máximo 3000 caracteres en las notas"
            Exit Sub
        End If

        If Not (rad_usuario.Checked) And Not (rad_cliente.Checked) Then
            lbl_info.Text = "Error: Seleccione quién está realizando el pago"
            Exit Sub
        End If

        Dim sefectivo As String, sctasori As String, sbancos As String, scheques As String, sEfectivoBanco As String

        sefectivo = txt_cajaori.Text
        sctasori = (ctas_origen().Compute("Sum(MONTO)", "MONTO<>0")).ToString
        sbancos = (Session("tabla_bancosori").Compute("Sum(MONTO)", "MONTO<>0")).ToString
        sEfectivoBanco = (Session("tabla_bancosori").Compute("Sum(MONTO)", "ORIGEN='EFECTIVO' AND MONTO<>0")).ToString
        scheques = (Session("tabla_chequesori").Compute("Sum(MONTO)", "MONTO<>0")).ToString
        Dim efectivo, cheques, ctasori, bancos, efectivoBanco As Decimal

        If sefectivo <> "" Or sctasori <> "" Or sbancos <> "" Or scheques <> "" Then

            If sefectivo <> "" Then
                efectivo = CDec(sefectivo)
            Else
                efectivo = 0.0
            End If

            If scheques <> "" Then
                cheques = CDec(scheques)
            Else
                cheques = 0.0
            End If

            If sbancos <> "" Then
                bancos = CDec(sbancos)
            Else
                bancos = 0.0
            End If

            If sctasori <> "" Then
                ctasori = CDec(sctasori)
            Else
                ctasori = 0.0
            End If

            If sEfectivoBanco <> "" Then
                efectivoBanco = CDec(sEfectivoBanco)
            Else
                efectivoBanco = 0.0
            End If

            Dim money_in As Decimal = efectivo + cheques + bancos + ctasori
            Session("efectivo") = efectivoBanco + efectivo

            If money_in >= Session("INT_NOR") + Session("IVA_INT_NOR") + Session("COM") + Session("IVA_COM") Then

                If money_in < Session("INT_NOR") + Session("IVA_INT_NOR") + Session("COM") + Session("IVA_COM") Then
                    lbl_info.Text = "La cantidad restante despues de cubrir el adeudo de intereses, no es suficiente para cubrir el pago minimo (interes ordinario)" +
                        "requerido. Depositar el restante a cuenta eje."
                    datos()
                    Exit Sub
                End If

                If rad_usuario.Checked Then
                    ' VERIFICA SI REQUIERE CAPTURAR EL CUESTIONARIO Y TENER LOS DATOS CAPTURADOS
                    If val_cuestionario_Efectivo(val_rangos_Efectivo(Session("efectivo"))) = "OK" Then
                        ' SOLO SE GUARDARAN LOS DATOS EN CASO DE SER REQUERIDOS
                        If val_rangos_Efectivo(Session("efectivo")) = "REGISTRAR" Then
                            If val_idPersona_existente(txt_cliente.Text, idpersona_busqueda) <> "OK" Then
                                lbl_info.Text = val_idPersona_existente(txt_cliente.Text, idpersona_busqueda)
                                Exit Sub
                            Else
                                Session("GUADAR_USUARIO") = "SI"
                            End If
                        Else
                            Session("GUADAR_USUARIO") = "NO"
                        End If
                    Else
                        lbl_info.Text = "Debido a la suma acumulada en efectivo es obligatorio capturar la información del Usuario"
                        Exit Sub
                    End If
                Else ' Es Cliente
                    Session("GUADAR_USUARIO") = "NO"
                End If

                If Session("tabla_chequesori").Select("ESTATUS='COB'").Length <> 0 Then
                    If revisa_facultad_cheques() Then
                        If AutorizacionActiva(efectivo) = 1 Then
                            SolicitudAutorizacion(efectivo)
                            cambia_modal_autorizacion("PLD")
                            Session("AUTOR_EFECTIVO") = efectivo
                            Session("AUTOR_BANCOS") = bancos
                            Session("AUTOR_CHEQUES") = cheques
                            ModalPopupExtender2.Show()
                        Else

                            Amortizacion(efectivo, cheques, bancos, txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_notas.Text)

                            If Session("GUADAR_USUARIO") = "SI" Then
                                GuardaUsuario(cmb_folio.SelectedItem.Value, Session("SERIE"), Session("FOLIO_IMP"), Session("IFE"))
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
                    If AutorizacionActiva(efectivo) = 1 Then
                        SolicitudAutorizacion(efectivo)
                        cambia_modal_autorizacion("PLD")
                        Session("AUTOR_EFECTIVO") = efectivo
                        Session("AUTOR_BANCOS") = bancos
                        Session("AUTOR_CHEQUES") = cheques
                        ModalPopupExtender2.Show()
                    Else
                        Amortizacion(efectivo, cheques, bancos, txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_notas.Text)

                        If Session("GUADAR_USUARIO") = "SI" Then
                            GuardaUsuario(cmb_folio.SelectedItem.Value, Session("SERIE"), Session("FOLIO_IMP"), Session("IFE"))
                        End If

                        If Session("IFE") = "REGISTRAR" Then
                            lbl_ife.Text = "Recuerde solicitar IFE para posteriormente digitalizarla"
                        Else
                            lbl_ife.Text = ""
                        End If
                        LimpiaTodo_Deposito()

                    End If
                End If

            Else
                lbl_info.Text = "El monto introducido es insuficiente para cubrir el pago minimo (interes ordinario) requerido."
            End If
        Else
            lbl_info.Text = "No ha introducido ningun monto como origen del pago"
        End If

    End Sub

    Private Sub Amortizacion(ByVal efectivo As Decimal, ByVal cheques As Decimal, ByVal bancos As Decimal, ByVal nombre1 As String, ByVal nombre2 As String, ByVal paterno As String, ByVal materno As String, ByVal notas As String)

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("INS_VENTANILLA_PAGO", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("FOLIO", SqlDbType.Int)
            Session("parm").Value = CInt(cmb_folio.SelectedItem.Value)
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
            lbl_estatus.Text = "Guardado correctamente"
        End Using

        If Session("RES") = "1" Then

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CHEQUE", Session("adVarChar"), Session("adParamInput"), 20, cheques)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("BANCO", Session("adVarChar"), Session("adParamInput"), 20, bancos)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EFECTIVO", Session("adVarChar"), Session("adParamInput"), 20, efectivo)
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

            If Session("IDALERTA") >= 0 Then
                ClientScript.RegisterStartupScript(GetType(String), "AlertaOpercionInusual", "window.open(""CRED_VEN_ALERTAPLD.aspx"", ""PLD"", ""width=650,height=550,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1"");", True)
            End If

            'Se llama tira de efectivo
            If efectivo > 0.0 Then
                Session("MONTO_EFECTIVO") += efectivo
                Session("ENTRADASALIDA") = "ENTRADA"

                LlamaTiraEfectivo()

            End If

            HiddenRawData.Value = Session("MascoreG").impresion_ticket_CRED(Session("SERIE"), Session("FOLIO_IMP"), Session("USERID"), Session("EMPRESA"))

            Recalculo_plan()
            'Enviar correo de confirmación
            Enviar_Correo()


        Else
            If Session("RES") = "0" Then
                lbl_estatus.Text = "El número de cheque capturado ya existe en la base de datos"
            ElseIf Session("RES") = "-2" Then
                lbl_estatus.Text = "Se intentó realizar un pago idéntico a este en un periodo de tiempo muy corto. Favor de esperar 5 minutos para realizar la operación de nuevo."
            Else
                lbl_estatus.Text = "La amortización no se efectuó. Verifique pago mínimo."
            End If
        End If
        datos()
    End Sub

    Function ctas_origen() As DataTable
        Dim ctas As New DataTable, txtcapori(100) As TextBox, dato As String
        Dim i As Integer = 0
        ctas.Columns.Add("FOLIO", GetType(Integer))
        ctas.Columns.Add("MONTO", GetType(Decimal))
        If Not Session("txtcapori") Is Nothing Then
            txtcapori = Session("txtcapori")
        End If

        Do While Not txtcapori(i) Is Nothing
            'Calculos de ingresos y egresos.
            dato = txtcapori(i).Text
            If dato <> "" Then
                ctas.Rows.Add(CInt(txtcapori(i).ID.ToString), CDec(dato))
            End If
            i += 1
        Loop
        Return ctas
    End Function

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
        ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)

        Return True

    End Function

    Private Sub dag_bancosori_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_bancosori.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then

            lbl_infobanco.Text = ""
            Session("tabla_bancosori").Rows(e.Item.ItemIndex).Delete()
            dag_bancosori.DataSource = Session("tabla_bancosori")
            dag_bancosori.DataBind()
            Llenobancosori()
        End If

    End Sub

    Private Sub dag_chequesori_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_chequesori.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then

            lbl_infocheques.Text = ""
            Session("tabla_chequesori").Rows(e.Item.ItemIndex).Delete()
            dag_chequesori.DataSource = Session("tabla_chequesori")
            dag_chequesori.DataBind()
            Llenobancoschequesori()
        End If

    End Sub

    Protected Sub btn_ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok.Click
        ModalPopupExtender1.Hide()
        aplica_pago()
    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        ModalPopupExtender1.Hide()
    End Sub

    '--------------------   ESCALAMIENTO DE OPERACIONES (PLD)   --------------------
#Region "PLD"

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

    Private Sub SolicitudAutorizacion(ByVal efectivo As Decimal)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_OPERACION", Session("adVarChar"), Session("adParamInput"), 50, "AMORTIZACION")
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

    Protected Sub lnk_ACT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ACT.Click
        If Not Session("ID_AUT") Is Nothing Then
            ver_aut_remota_cheques()
        Else
            VerificaAutorRemota()
        End If
        ModalPopupExtender2.Show()
    End Sub

    Private Sub llena_lista_user_aut()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter(), dt_lst As New Data.DataTable()
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
                            If AutorizacionActiva(Session("caja")) = 1 Then
                                SolicitudAutorizacion(Session("caja"))
                                cambia_modal_autorizacion("PLD")
                                ModalPopupExtender2.Show()
                            Else
                                Amortizacion(Session("AUTOR_EFECTIVO"), Session("AUTOR_CHEQUES"), Session("AUTOR_BANCOS"), txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_notas.Text)

                                actualiza_folio_imp_aut_cheques()
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
                            Amortizacion(Session("AUTOR_EFECTIVO"), Session("AUTOR_CHEQUES"), Session("AUTOR_BANCOS"), txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_notas.Text)
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
                    cmb_Acc.ClearSelection()
                    cmb_Acc.Items.FindByValue("-1").Selected = True

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
                    Amortizacion(Session("AUTOR_EFECTIVO"), Session("AUTOR_CHEQUES"), Session("AUTOR_BANCOS"), txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_notas.Text)
                End If
                Session("ID_AUT") = Nothing
                Session("AUTOR_USR_CHEQUES") = Nothing
            Else
                Amortizacion(Session("AUTOR_EFECTIVO"), Session("AUTOR_CHEQUES"), Session("AUTOR_BANCOS"), txt_nombre1.Text, txt_nombre2.Text, txt_paterno.Text, txt_materno.Text, txt_notas.Text)
                If Not Session("ID_AUT") Is Nothing Then
                    actualiza_folio_imp_aut_cheques()
                Else
                    FolioImpAutorizacion()
                    Session("IDAUTORIZACION") = Nothing
                End If
                FolioImpAutorizacion()
                Session("IDAUTORIZACION") = Nothing
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

#End Region 'fin de regoin PLD

#Region "EMAIL"


    Private Sub Motor_On_off()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, "PAGREC")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_CONFIGURACION_ENVIOS_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("MOTOR") = Session("rs").fields("GRANTED").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Private Sub Enviar_Correo()

        Motor_On_off()
        If Session("MOTOR") = "1" Then
            envioemail()
        End If

        Session("MOTOR") = Nothing
    End Sub

    Private Sub envioemail()
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim user As String = String.Empty
        Dim fecha As String = String.Empty
        Dim cant As String = String.Empty
        Dim cap As String = String.Empty
        Dim ord As String = String.Empty
        Dim mora As String = String.Empty
        Dim comisiones As String = String.Empty
        Dim impuestos As String = String.Empty
        Dim folio As String = String.Empty
        Dim correo As String = String.Empty
        Session("IDENVIO") = "0"

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_LOG_CONFIGURACION_GLOBAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Session("BCC_ENVIADOS") = Session("rs").Fields("BCC_ENVIADOS").Value.ToString
            Session("BCC_ENVIADOS_EMAIL1") = Session("rs").Fields("BCC_ENVIADOS_EMAIL1").Value.ToString
            Session("BCC_ENVIADOS_EMAIL2") = Session("rs").Fields("BCC_ENVIADOS_EMAIL2").Value.ToString
            Session("REMITENTE_ALIAS") = Session("rs").Fields("REMITENTE_ALIAS").Value.ToString

        End If

        Session("Con").Close()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_LOG_PAGO_RECIBIDO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_LOG_PAGO_RECIBIDO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            user = Session("rs").Fields("NOMBRE").Value.ToString
            Session("rs").Fields("MONTO_APLICADO").Value.ToString()
            correo = Session("rs").Fields("EMAIL").Value.ToString()
            cap = Session("rs").Fields("CAPITAL").Value.ToString()
            ord = Session("rs").Fields("ORDINARIOS").Value.ToString()
            mora = Session("rs").Fields("MORATORIOS").Value.ToString()
            Session("rs").Fields("COMISIONES").Value.ToString()

            folio = Session("rs").Fields("FOLIO").Value.ToString()
            Session("IDENVIO") = Session("rs").Fields("IDENVIO").Value.ToString
            Session("rs").Fields("FECHAPAGO_APLICADO").Value.ToString()
            subject = "Pago Recibido"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td width='25%'>Su pago ha sido recibido:</td></td></tr>")
            sbhtml.Append("<tr><td>Estimado (a):  " + user + "</td></tr>")
            sbhtml.Append("<tr><td> Le informamos que el pago realizado a su préstamo: " + folio + "ha quedado debidamente registrado </td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td width='75%'>Fecha de pago :</td><td>" + "<b>" + fecha + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Cantidad aplicada :</td>" + "<b>" + cant + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='25%'>Desglose:</td></td></tr>")
            sbhtml.Append("<tr><td width='30%'>Capital :</td>" + "<b>" + cap + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Intereses ordinarios :</td>" + "<b>" + ord + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Intereses moratorios :</td>" + "<b>" + mora + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Comisiones :</td>" + "<b>" + comisiones + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Impuestos :</td>" + "<b>" + impuestos + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='250'>Para cualquier duda o aclaración puede comunicarse a: Soporte Técnico del Sistema correspondiente.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString(), subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
        End If


        Session("Con").Close()
        If Session("IDENVIO") <> "0" Then
            actualizarEnvio()
        End If

    End Sub

    Private Sub actualizarEnvio()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDENVIO", Session("adVarChar"), Session("adParamInput"), 20, Session("IDENVIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_LOG_PAGO_RECIBIDO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub
#End Region

#Region "Recalculo"

    Private Sub Recalculo_plan()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RECALCULO_VENTANILLA_VALIDACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            If Session("rs").fields("RESPUESTA").value.ToString = "ADELANTO" Then
                ModalPopupExtender_recalculo.Show()
            Else
                ModalPopupExtender_recalculo.Hide()
            End If
        End If
        Session("Con").Close()

    End Sub

    Protected Sub btn_aceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aceptar.Click
        ModalPopupExtender_recalculo.Hide()
    End Sub

#End Region


#Region "llenado"

    Private Sub Llenainfocredito()
        Dim montoLiquidar As Decimal
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VENTANILLA_INFO_CREDITO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_montocreditotxt.Text = Session("rs").Fields("MONTO").Value.ToString
            lbl_saldocreditotxt.Text = Session("rs").Fields("SALDO").Value.ToString
            lbl_fechaliberaciontxt.Text = Session("rs").Fields("FECHA_L").Value.ToString
            lbl_fechaultimopagotxt.Text = Session("rs").Fields("FECHA_ULT").Value.ToString
            lbl_intnortxt.Text = Session("rs").Fields("TASA_NOR").Value.ToString
            lbl_intmortxt.Text = Session("rs").Fields("TASA_MOR").Value.ToString
            lbl_diasultimopagotxt.Text = Session("rs").Fields("DIAS_ULT").Value.ToString
            lbl_vencidostxt.Text = Session("rs").Fields("VENCIDOS").Value.ToString
            lbl_fechavenctxt.Text = Session("rs").Fields("FECHA_VENC").Value.ToString
            Session("CLASIFICACION") = Session("rs").Fields("CLASCRED").Value.ToString
            Session("FECHA_SISTEMA") = Session("rs").Fields("FECHA_ACTUAL").Value.ToString
            Session("FECHA_PROX") = Session("rs").Fields("FECHA_LIM").Value.ToString
            Session("INT_NOR") = Session("rs").Fields("INT_NOR").Value
            Session("IVA_INT_NOR") = Session("rs").Fields("IVA_INT_NOR").Value
            Session("MONTO_TOTAL_CRED") = Session("rs").Fields("MONTO_TOTAL_CRED").Value
            Session("SALDO_CTA_EJE") = Session("rs").Fields("SALDO_CTA_EJE").Value
            montoLiquidar = Session("MONTO_TOTAL_CRED") - Session("SALDO_CTA_EJE")
            lbl_monto_liq_txt.Text = IIf(montoLiquidar < 0.0, 0.0, montoLiquidar)
            Session("COM") = Session("rs").Fields("COM").Value
            Session("IVA_COM") = Session("rs").Fields("IVA_COM").Value
            Session("MONTO_ADEUDO_CV") = Session("rs").Fields("MONTO_ADEUDO_CV").Value
        End If
        Session("Con").Close()
    End Sub

    Private Sub Llenadatospago()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPendientes As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VENTANILLA_DATOS_PAGO"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtPendientes, Session("rs"))
        grd_pago.DataSource = dtPendientes
        grd_pago.DataBind()
        Session("Con").Close()
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

    Private Sub Llenactascapori()

        pnl_ctascapori.Controls.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 25, "AMORTIZADOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_CRED", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DEPRET_CTAS_CAPTACION_ORIGEN"
        Session("rs") = Session("cmd").Execute()


        Dim i As Integer = 0
        Dim descapori(100), saldocapori(100) As Label, txtcapori(100) As TextBox, cr(100) As Literal, filtro(100) As AjaxControlToolkit.FilteredTextBoxExtender, reg(100) As RegularExpressionValidator
        Do While Not Session("rs").EOF

            'Declaro los arreglos
            descapori(i) = New Label
            txtcapori(i) = New TextBox
            saldocapori(i) = New Label
            filtro(i) = New AjaxControlToolkit.FilteredTextBoxExtender
            reg(i) = New RegularExpressionValidator
            cr(i) = New Literal
            descapori(i).Text = Session("rs").Fields("DESCRIPCION").Value.ToString
            descapori(i).CssClass = "texto"
            cr(i).Text = "<br />"
            descapori(i).Width = 250
            txtcapori(i).Width = 130
            txtcapori(i).MaxLength = 22
            txtcapori(i).CssClass = "textocajas"
            txtcapori(i).ID = Session("rs").Fields("FOLIO").Value.ToString
            saldocapori(i).Text = ("Saldo: $" + Session("rs").Fields("SALDO").Value.ToString)
            saldocapori(i).CssClass = "texto"
            saldocapori(i).Width = 155
            If Session("FACULTAD_SALDOS") = "1" Then
                saldocapori(i).Visible = True
            Else
                saldocapori(i).Visible = False
            End If
            filtro(i).ID = "fil_" + Session("rs").Fields("FOLIO").Value.ToString
            filtro(i).TargetControlID = txtcapori(i).ID
            filtro(i).FilterType = AjaxControlToolkit.FilterTypes.Custom
            filtro(i).ValidChars = ".1234567890"
            reg(i).ID = "RegularExpressionValidator_" + Session("rs").Fields("FOLIO").Value.ToString
            reg(i).CssClass = "textogris"
            reg(i).ControlToValidate = txtcapori(i).ID
            reg(i).ErrorMessage = "Monto incorrecto!"
            reg(i).ValidationExpression = "^[0-9]+(\.[0-9]{1}[0-9]?)?$"
            pnl_ctascapori.Controls.Add(descapori(i))
            pnl_ctascapori.Controls.Add(cr(i))
            pnl_ctascapori.Controls.Add(saldocapori(i))
            pnl_ctascapori.Controls.Add(txtcapori(i))
            pnl_ctascapori.Controls.Add(filtro(i))
            pnl_ctascapori.Controls.Add(reg(i))
            pnl_ctascapori.Controls.Add(cr(i))
            Session("rs").movenext()
            i += 1
        Loop
        Session("Con").Close()

        Session("txtcapori") = txtcapori
        Session("saldocapori") = saldocapori

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

    Private Sub Llenadatos()
        cmb_folio.Items.Clear()
        cmb_folio.Items.Add(New ListItem("ELIJA", "-1"))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_cliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FOLIOS_X_CLIENTE"
        Session("rs") = Session("cmd").Execute()

        lbl_cliente.Text = Session("rs").Fields("NOMBRE").Value
        Session("PROSPECTO") = Session("rs").Fields("NOMBRE").Value

        If Session("rs").Fields("COND").Value.ToString = "0" Then
            lbl_info.Text = "El cliente no cuenta con préstamos activos"
            Session("Con").Close()
            Exit Sub
        End If
        If Session("rs").Fields("NOMBRE").Value.ToString = "-1" Then
            lbl_info.Text = "El cliente introducido no existe"
            lbl_cliente.Text = ""
            Session("Con").Close()
            Exit Sub
        End If

        Do While Not Session("rs").EOF
            cmb_folio.Items.Add(New ListItem(Session("rs").Fields("FOLIO").Value, Session("rs").Fields("FOLIO").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Private Sub bancos()
        cmb_bancoori.Enabled = True
        txt_montobancosori.Enabled = True
        cmb_origen_dep.Enabled = True
        btn_agregarbancosori.Enabled = True
    End Sub

    Private Sub cheques()
        cmb_bancochequesori.Enabled = True
        txt_ctachequesori.Enabled = True
        txt_montochequesori.Enabled = True
        txt_numchequesori.Enabled = True
        btn_agregarchequesori.Enabled = True
        cmb_modo_rec.Enabled = True
    End Sub

    Private Sub datos()
        Llenainfocredito()
        grd_pago.Enabled = True
        grd_pago.Visible = True
        borra_tablas()
        crea_tablas()
        Llenadatospago()
        Llenobancosori()
        Llenobancoschequesori()
        Llenobancosdes()
        pnl_ctascapori.Enabled = True
        bancos()
        cheques()
        Llena_Origen()
        txt_cajaori.Text = ""
        btn_pagar.Enabled = True
        Session("FOLIO") = cmb_folio.SelectedItem.Value
        Llenactascapori()
        txt_nombre1.Text = ""
        txt_nombre2.Text = ""
        txt_paterno.Text = ""
        txt_materno.Text = ""
        txt_notas.Text = ""
    End Sub

    Private Sub Llenobancosdes()

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
                cmb_banco_des_dep.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    'Carga la información del folio en la ventana de pagos
    Private Sub llena_informacion_folio()

        If cmb_folio.SelectedItem.Value > -1 Then
            'Validacion para amortizar en expedientes de amortizacion
            validacionDisposiciones()
            datos()

            'Mostar los datos generales de un expediente: folio, nombre de cliente y producto
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"
            Session("rs") = Session("cmd").Execute()
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value
            Session("Con").Close()

            'Bandera que se inicia al momento de verificar si existen disposiciones pendientes
            If Session("AMORTIZAR") = 0 Then
                btn_pagar.Enabled = False
                lbl_info_disp.Text = "Error: No puede realizar ninguna amortización para este expediente, el expediente cuenta con disposiciones pendientes."

            Else
                lbl_info_disp.Text = ""
                btn_pagar.Enabled = True
                lnk_ProvRec.Visible = True

                lbl_accion.Visible = True
                rad_cliente.Visible = True
                rad_usuario.Visible = True
                rad_cliente.Checked = False
                rad_usuario.Checked = False

                'SE VERIFICA SI CUENTA CON LA FACULTAD DE VER SALDOS, Levantando la variable de session ("FACULTAD_SALDOS")
                revisa_facultad_saldos(CInt(Session("USERID")))

            End If

            txt_fechacorte.Text = ""
            txt_cajaori.Enabled = True
            lbl_info_fecha_corte.Visible = False

        End If


    End Sub

#End Region 'Fin de Region "llenado"

#Region "Creación Tablas y Limpiar Controles"

    Private Sub crea_tablas()
        Dim tabla_bancosori As New DataTable, tabla_chequesori As New DataTable

        tabla_bancosori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO", GetType(String))
        tabla_bancosori.Columns.Add("MONTO", GetType(Decimal))
        tabla_bancosori.Columns.Add("ID_ORIGEN", GetType(Integer))
        tabla_bancosori.Columns.Add("ORIGEN", GetType(String))
        tabla_bancosori.Columns.Add("ID_BANCO_DESTINO", GetType(Integer))
        tabla_bancosori.Columns.Add("BANCO_DESTINO", GetType(String))
        tabla_bancosori.Columns.Add("NUM_CTA_DESTINO", GetType(String))
        tabla_bancosori.Columns.Add("TITULAR_DESTINO", GetType(String))

        Session("tabla_bancosori") = tabla_bancosori

        tabla_chequesori.Columns.Add("ID_CTA", GetType(Integer))
        tabla_chequesori.Columns.Add("ID_BANCO", GetType(Integer))
        tabla_chequesori.Columns.Add("BANCO", GetType(String))
        tabla_chequesori.Columns.Add("NUMCUENTA", GetType(String))
        tabla_chequesori.Columns.Add("CHEQUE", GetType(String))
        tabla_chequesori.Columns.Add("MONTO", GetType(Decimal))
        tabla_chequesori.Columns.Add("ESTATUS", GetType(String))

        Session("tabla_chequesori") = tabla_chequesori

    End Sub

    Private Sub borra_tablas()
        Session("tabla_bancosori").Clear()
        dag_bancosori.DataSource = Session("tabla_bancosori")
        dag_bancosori.DataBind()
        Session("tabla_chequesori").Clear()
        dag_chequesori.DataSource = Session("tabla_chequesori")
        dag_chequesori.DataBind()
    End Sub

    Private Sub limpia_datos()
        lbl_montocreditotxt.Text = ""
        lbl_saldocreditotxt.Text = ""
        lbl_fechaliberaciontxt.Text = ""
        lbl_fechaultimopagotxt.Text = ""
        lbl_intnortxt.Text = ""
        lbl_intmortxt.Text = ""
        lbl_diasultimopagotxt.Text = ""
        lbl_vencidostxt.Text = ""
        lbl_fechavenctxt.Text = ""
        grd_pago.Enabled = False
        pnl_ctascapori.Enabled = False
        cmb_bancoori.Enabled = False
        txt_montobancosori.Enabled = False
        btn_agregarbancosori.Enabled = False
        cmb_bancochequesori.Enabled = False
        txt_ctachequesori.Enabled = False
        txt_montochequesori.Enabled = False
        txt_numchequesori.Enabled = False
        btn_agregarchequesori.Enabled = False
        txt_cajaori.Text = ""
        btn_pagar.Enabled = False
        grd_pago.Visible = False
        txt_nombre1.Text = ""
        txt_nombre2.Text = ""
        txt_paterno.Text = ""
        txt_materno.Text = ""
        txt_notas.Text = ""
        lnk_ProvRec.Visible = False
    End Sub

    Private Sub LimpiaTransferencia()
        txt_montobancosori.Text = ""
        cmb_origen_dep.SelectedIndex = 0
        cmb_banco_des_dep.SelectedIndex = 0

        txt_cta_dep.Text = ""
        txt_titular_cta_dep.Text = ""
    End Sub

    Private Sub limpiaReferencia()
        txt_nombre1.Text = ""
        txt_nombre2.Text = ""
        txt_paterno.Text = ""
        txt_materno.Text = ""
        txt_notas.Text = ""
    End Sub

    Private Sub Habilitar_Who(ByVal semaforo As Boolean)
        rad_cliente.Visible = semaforo
        rad_usuario.Visible = semaforo

        If semaforo = True Then
            rad_cliente.Checked = False
            rad_usuario.Checked = False
            rad_cliente.CssClass = "texto"
            rad_usuario.CssClass = "texto"
        End If

        pnl_usuario_pf.Visible = False
        lbl_accion.Visible = semaforo
    End Sub

    Private Sub LimpiaTodo_Deposito()
        'Session("bancos") = Nothing
        'Session("cheques") = Nothing
        'Session("caja") = Nothing
        'Session("ctasori") = Nothing

        Session("IFE") = Nothing
        Session("GUADAR_USUARIO") = Nothing
        Session("efectivo") = Nothing

        LimpiaGenerales()
        LimpiaCtasDep()
        LimpiaEfectivo()
        LimpiaBancos()
        LimpiaCheques()
        Limpia_pf()
        btn_pagar.Enabled = False

        Habilitar_Who(0)

        'Se recarga información de folio de préstamo
        llena_informacion_folio()

    End Sub

    Private Sub LimpiaGenerales()

        borra_tablas()
        lbl_cliente.Text = ""
        lbl_accion.Visible = False
        rad_usuario.Visible = False
        rad_cliente.Visible = False
        limpiaReferencia()
        Habilitar_Who(0)
    End Sub

    Private Sub LimpiaCtasDep()
        'lbl_ctascapori.Visible = False
        pnl_ctascapori.Visible = False
    End Sub

    Private Sub LimpiaEfectivo()
        txt_cajaori.Enabled = False
        txt_cajaori.Text = ""
    End Sub

    Private Sub LimpiaBancos()

        cmb_bancoori.Items.Clear()
        cmb_bancoori.Enabled = False
        txt_montobancosori.Enabled = False
        txt_montobancosori.Text = ""
        cmb_origen_dep.Items.Clear()
        cmb_origen_dep.Enabled = False
        txt_cta_dep.Text = ""
        txt_titular_cta_dep.Text = ""
        btn_agregarbancosori.Enabled = False

        lbl_infobanco.Text = ""

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

        lbl_infocheques.Text = ""

    End Sub


#End Region 'Fin de region "Creación Tablas y Limpiar Controles"

#Region "Agregar"

    Protected Sub btn_agregarbancosori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarbancosori.Click

        lbl_infobanco.Text = ""

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
                lbl_infobanco.Text = "Debe de seleccionar un banco de origen!"
                Exit Sub
            Else
                banco_destino = cmb_banco_des_dep.Text
            End If

            num_cta_destino = txt_cta_dep.Text
            If num_cta_destino = "" Then
                lbl_infobanco.Text = "Debe de escribir una cuenta de origen!"
                Exit Sub
            End If
            titular_destino = txt_titular_cta_dep.Text.ToUpper
        End If

        If Validamonto(txt_montobancosori.Text) Then
            If val_cheques(num_cta_destino, "TRA", id_banco_destino) = "EXISTE" And origen(0) = "CHE" Then
                lbl_infobanco.Text = "Error: Ya está registrado ese número de cheque, verifique."
            Else
                Session("tabla_bancosori").Rows.Add(idCta, banco, monto, origen(1), Origen_Nombre, id_banco_destino, banco_destino, num_cta_destino, titular_destino)
                dag_bancosori.DataSource = Session("tabla_bancosori")
                dag_bancosori.DataBind()
                Llenobancosori()
                pnl_forma_transferencia.Visible = False
                LimpiaTransferencia()
            End If

        Else
            lbl_infobanco.Text = " Monto incorrecto!"
        End If

    End Sub

    Protected Sub btn_agregarchequesori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregarchequesori.Click
        lbl_infocheques.Text = ""
        If Validamonto(txt_montochequesori.Text) = True Then

            If val_cheques(txt_numchequesori.Text, "CHE", CInt(cmb_bancochequesori.SelectedItem.Value)) = "EXISTE" Then
                lbl_infocheques.Text = "Error: Ya está registrado ese número de cheque, verifique."
            Else
                Session("tabla_chequesori").Rows.Add(0, CInt(cmb_bancochequesori.SelectedItem.Value), cmb_bancochequesori.SelectedItem.Text, txt_ctachequesori.Text, txt_numchequesori.Text, CDec(txt_montochequesori.Text), cmb_modo_rec.SelectedItem.Value)
                dag_chequesori.DataSource = Session("tabla_chequesori")
                dag_chequesori.DataBind()
                Llenobancoschequesori()
                txt_ctachequesori.Text = ""
                txt_numchequesori.Text = ""
                txt_montochequesori.Text = ""
                cmb_modo_rec.ClearSelection()
                cmb_modo_rec.Items.FindByValue("-1").Selected = True
            End If
        Else
            lbl_infocheques.Text = " Monto incorrecto!<br /><br/ >"
        End If

    End Sub

#End Region 'Fin de region "Agregar"

#Region "Validaciones"

    Function val_cheques(ByVal numcheque As String, ByVal seccion As String, ByVal id_banco As Integer) As String
        Dim Respuesta As String = ""

        If id_banco <> -1 Then

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
        Else
            Respuesta = ""
        End If

        Return Respuesta
    End Function

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
        If Not Session("rs").eof Then
            If Session("rs").Fields("RES").value = 1 Then
                res = True
            End If
        End If
        Session("Con").Close()
        Return res
    End Function

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
        'lbl_info.Text = Session("SUCID").ToString + " hol " + Session("ID_EQ").ToString
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

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    'Validacion de disposiciones 
    Private Sub validacionDisposiciones()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_folio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_DISPOSICIONES_VALIDACION_AMORTIZACION]"
        Session("rs") = Session("cmd").Execute()
        Session("AMORTIZAR") = Session("rs").fields("AMORTIZAR").value
        Session("Con").Close()


    End Sub

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


#End Region 'Fin de Region "Validaciones"

#Region "USUARIOS" 'Alta de usuarios que realizan movimientos

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
        Session("parm") = Session("cmd").CreateParameter("FECHANAC", Session("adVarChar"), Session("adParamInput"), 20, txt_fechanac.Text)
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

    Protected Sub cmb_origen_dep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_origen_dep.SelectedIndexChanged

        Dim origen
        origen = Split(cmb_origen_dep.SelectedItem.Value.ToString, "-")

        Select Case origen(0)
            Case "CHE"
                pnl_forma_transferencia.Visible = True
                lbl_tit_dep_origen.Text = "*Núm. cheque: "
            Case "TRA"
                pnl_forma_transferencia.Visible = True
                lbl_tit_dep_origen.Text = "*Núm. cuenta: "
            Case Else
                pnl_forma_transferencia.Visible = False
                lbl_tit_dep_origen.Text = ""
        End Select
    End Sub

    Protected Sub rad_usuario_CheckedChanged(sender As Object, e As EventArgs) Handles rad_usuario.CheckedChanged

        If rad_usuario.Checked = True Then
            pnl_usuario_pf.Visible = True
            Limpia_pf()
            LlenaPaises(cmb_pais.ID)
            llena_nac()
            llena_vialidad(cmb_tipo_via.ID)
        Else
            pnl_usuario_pf.Visible = False

        End If

    End Sub

    Protected Sub rad_cliente_CheckedChanged(sender As Object, e As EventArgs) Handles rad_cliente.CheckedChanged
        Panels(0)
    End Sub

    Private Sub Panels(ByVal semaforo As Boolean)
        pnl_usuario_pf.Visible = semaforo

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

    Protected Sub lnk_BusquedaCP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_BusquedaCP.Click
        ScriptManager.RegisterStartupScript(Me, GetType(String), "Busqueda", "busquedaCP();", True)
    End Sub

    Protected Sub txt_cp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp.TextChanged
        'Response.Redirect("AltaModPersonaF.aspx")
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()
        cmb_localidad.Items.Clear()
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

#End Region

End Class