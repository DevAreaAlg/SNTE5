Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class CRED_EXP_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Préstamo", "Configuración de Expedientes de Préstamo")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona

            TIPORENOVACION()

            If Session("VENGODE") = "CRED_EXP_APARTADO1.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO2.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO2_PLAN_INSTITUCIONAL.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO3.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO4.aspx" Or Session("VENGODE") = "ADMDIGITALIZADOR.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO6.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO5.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO7.aspx" Or Session("VENGODE") = "PRELLENADOSOLICITUDAPCUENTA.ASPX" Or Session("VENGODE") = "CNFEXP_CAP_APARTADO1.ASPX" Or Session("VENGODE") = "CRED_EXP_APARTADO8.aspx" Or Session("VENGODE") = "CNFEXP_PPE.aspx" Or Session("VENGODE") = "CRED_EXP_APARTADO9.aspx" Or Session("VENGODE") = "CRED_EXP_AVALES.aspx" Or Session("VENGODE") = "UNI_REFERENCIAS.aspx" Then

                ConfiguracionExpediente()

                LlenaPendientes()

                tbx_rfc.Text = Session("NUMTRAB").ToString

                'LlenaComites()
                txt_IdCliente.Text = Session("PERSONAID").ToString
                lbl_subtitfolio.Text = Session("CVEEXPE").ToString
                lbl_subtitprod.Text = Session("PRODUCTO").ToString
                lbl_subtitcli.Text = Session("CLIENTE").ToString

                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)

                lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
                lbl_otroscred.Text = FormatCurrency(Session("OTROSCRED").ToString)
                tbx_region.Text = Session("AGR_REGION").ToString
                tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
                tbx_ct.Text = Session("AGR_CT").ToString
                tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
                tbx_pension.Text = FormatCurrency(Session("PEN").ToString)

                ' lbl_subtitdom.Text = Session("DOMICILIO").ToString

                pnl_expedientes.Visible = True
                pnl_cnfexp.Visible = True
                folderA(pnl_expedientes, "down")
                'habilito tabs y asigno focus al tab de configuracion
                folderA(div_selCliente, "up")

                folderA(pnl_cnfexp, "down")
                LlenarProductos()
            ElseIf Session("VENGODE") = "DetalleCartaCredito.aspx" Then

                'Metodo que llena el droplist con los tipos de productos
                LlenarTipoProd()
                'llena droplist con los expedientes pendientes del cliente
                LlenaPendientes()
                txt_IdCliente.Text = Session("PERSONAID")
                Session("CLIENTE") = Session("PROSPECTO")
                'Session("PROSPECTO") = Nothing
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)

                lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
                lbl_otroscred.Text = FormatCurrency(Session("OTROSCRED").ToString)
                tbx_region.Text = Session("AGR_REGION").ToString
                tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
                tbx_ct.Text = Session("AGR_CT").ToString
                tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
                tbx_pension.Text = FormatCurrency(Session("PEN").ToString)
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "down")
                folderA(pnl_cnfexp, "up")
                Limpiarenovacion()
                LlenarProductos()
            ElseIf Session("VENGODE") = "ExpedientesFACT.aspx" Then

                LlenarTipoProd()
                LlenaPendientes()
                txt_IdCliente.Text = Session("PERSONAID")
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)
                lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
                lbl_otroscred.Text = FormatCurrency(Session("OTROSCRED").ToString)
                tbx_region.Text = Session("AGR_REGION").ToString
                tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
                tbx_ct.Text = Session("AGR_CT").ToString
                tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
                tbx_pension.Text = FormatCurrency(Session("PEN").ToString)
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "down")
                folderA(pnl_cnfexp, "up")
                Limpiarenovacion()
                LlenarProductos()
            ElseIf Session("VENGODE") = "AdmPlazoFijo.aspx" Then

                LlenarTipoProd()
                LlenaPendientes()
                txt_IdCliente.Text = Session("PERSONAID")
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)
                lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
                lbl_otroscred.Text = FormatCurrency(Session("OTROSCRED").ToString)
                tbx_region.Text = Session("AGR_REGION").ToString
                tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
                tbx_ct.Text = Session("AGR_CT").ToString
                tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
                tbx_pension.Text = FormatCurrency(Session("PEN").ToString)
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "down")
                folderA(pnl_cnfexp, "up")
                Limpiarenovacion()
                LlenarProductos()
            ElseIf Session("VENGODE") = "DIGI_GLOBAL.aspx" Then

                ConfiguracionExpediente()

                LlenaPendientes()

                tbx_rfc.Text = Session("NUMTRAB").ToString
                div_NombrePersonaBusqueda.Visible = True
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                'LlenaComites()
                txt_IdCliente.Text = Session("PERSONAID").ToString
                lbl_subtitfolio.Text = Session("CVEEXPE").ToString
                lbl_subtitprod.Text = Session("PRODUCTO").ToString
                lbl_subtitcli.Text = Session("CLIENTE").ToString

                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)
                lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
                lbl_otroscred.Text = FormatCurrency(Session("OTROSCRED").ToString)
                tbx_region.Text = Session("AGR_REGION").ToString
                tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
                tbx_ct.Text = Session("AGR_CT").ToString
                tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
                tbx_pension.Text = FormatCurrency(Session("PEN").ToString)

                ' lbl_subtitdom.Text = Session("DOMICILIO").ToString

                pnl_expedientes.Visible = True
                folderA(pnl_expedientes, "down")
                'habilito tabs y asigno focus al tab de configuracion
                folderA(div_selCliente, "up")

                folderA(pnl_cnfexp, "down")
                LlenarProductos()
                'Regreso valores de número de trabajador e nstitución cuando salga de configurador'
            ElseIf Session("VENGODE") = "CRED_EXP_GENERAL" Or Session("VENGODE") = "CORE_PER_PERSONA" Then
                tbx_rfc.Text = Session("NUMTRAB").ToString

                obtieneId()

            Else
                'limpio variables
                LimpiaVariables()

            End If
            LlenarTipoProd()

        End If
        ' txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_Continuar.ClientID + "')")
        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            validaNumPersona()
        End If

        lbl_statusconf.Text = ""

        lbliduser.Text = Session("USERID")
        lblidsesion.Text = Session("Sesion")
        lblidsucu.Text = Session("SUCID")

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptPaneles", "$('.panel_folder_toogle').click(function(event) { var folder_content=$(this).parent().siblings('.panel-body').children('.panel-body_content');if($(this).hasClass('up')){$(this).removeClass('up');$(this).addClass('down');folder_content.show('6666',null);$(this).parent().css({'background': '#696462	 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px' });}else if($(this).hasClass('down')){$(this).removeClass('down');folder_content.hide('333',null);$(this).addClass('up');$(this).parent().css({ 'background': '#696462 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px'  });}});", True)

    End Sub

    '----------------------------------- LIMPIA CONTROLES -----------------------------------
    Private Sub Limpiarenovacion()
        'LIMPA RENOVACION
        ' cmb_tipo.SelectedIndex = "0"
        ' pnl_general.Visible = False
        ' Panel_Renovacion.Visible = False
        ' avisos.Visible = False
        ' lbl_FoliosRestrRenov.Visible = False
        'cmb_FoliosRestrRenov.Visible = False
        'cmb_FoliosRestrRenov.Enabled = False
        Session("NOMBRE_PRODUCTO") = Nothing
    End Sub

    Private Sub LimpiaDatosExpedientes()
        lbl_nompros.Text = ""
        lbl_salario.Text = ""
        lbl_capacidad.Text = ""
        lbl_otroscred.Text = ""
        tbx_region.Text = ""
        tbx_delegacion.Text = ""
        tbx_ct.Text = ""
        tbx_aportaciones.Text = ""
        tbx_pension.Text = ""
    End Sub

    'LIMPIA VARIABLES CREADAS
    Private Sub LimpiaVariables()
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("CLIENTE") = Nothing
        Session("VENGODE") = Nothing
        Session("FOLIOCCRED") = Nothing
        Session("PRODUCTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("CONFCOMPLETO") = Nothing
        Session("idperbusca") = Nothing
        Session("TIPOPROD") = Nothing
        Session("APARTADO") = Nothing
        Session("TIPOPER") = Nothing
        Session("CLASIFICACION") = Nothing
        Session("ASPX") = Nothing
        Session("ID") = Nothing
        Session("GENERAR") = Nothing
        Session("IDCARTA") = Nothing
        Session("IDFACTPAGO") = Nothing
        Session("FOLIOLINEA") = Nothing

    End Sub

    '----------------------------------- LLENA CATALOGOS Y COMBOS -----------------------------------
    Private Sub LlenarTipoProd()

        ' cmb_plaza.Items.Clear()
        ' chk_emergencia.Checked = False
        'cmb_folioori.Visible = False
        'lbl_folioori.Visible = False


    End Sub

    Private Sub LlenarProductos()
        ' lbl_productos.Text = "TIPO: 1" + " DESTINO: cmb_destino.SelectedItem.Value " + " SUCID: " + Session("SUCID").ToString + " TIPOPER: F " + " PERSONAID: " + Session("PERSONAID")
        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmb_Productos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_Productos.Items.Add(elija)
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
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString + "-" + Session("rs").Fields("DESCRIPCION").Value.ToString)
            cmb_Productos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub LlenarProductosCaptacion()

        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmb_Productos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_Productos.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOPER").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRODUCTOS_CAPTACION_VISTA"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString + "-" + Session("rs").Fields("DESCRIPCION").Value.ToString)
            cmb_Productos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub LlenaPendientes()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPendientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        'Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXPEDIENTES_PENDIENTES"

        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtPendientes, Session("rs"))
        dag_Expendientes.DataSource = dtPendientes
        dag_Expendientes.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub lnk_editar_agremiado_Click(sender As Object, e As EventArgs) Handles lnk_editar_agremiado.Click
        Session("PERSONAID") = Session("numcontrol")
        Session("VENGODE") = "CRED_EXP_GENERAL"
        Session("numcontrol") = tbx_rfc.Text
        Response.Redirect("../../CORE/PERSONA/CORE_PER_PERSONA.aspx")
    End Sub

    Private Sub lnk_info_agremiado_Click(sender As Object, e As EventArgs) Handles lnk_info_agremiado.Click
        Session("PERSONAID") = Session("PERSONAID")
        Session("VENGODE") = "CRED_EXP_GENERAL"
        Session("PROSPECTO") = Session("CLIENTE")
        Session("NUMTRAB") = tbx_rfc.Text
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_CONSULTAEXP.aspx")
    End Sub

    'FUNCION QUE HABILITA O DESHABILITA EL COMBO DE TIPO DE RENOVACION, DEPENDIENDO DEL TIPO DE CLASIFICACION DEL PRODUCTO, UNICAMENTE SE PUEDE ELEGIR TIPO DE RENOVACION CON CLASIFICACION SIMPLE
    Private Sub ClasCred()

        Dim cad
        cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CLASIF_CRED"
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, cad(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Session("CLASIFICACION") = Session("rs").Fields("CLAVE").Value.ToString
        End If
        Session("Con").Close()
    End Sub

    'LLENO EL COMBO DE ACUERDO A LOS 3 TIPOS DE RENOVACION QUE HAY
    Private Sub TIPORENOVACION()
        ' cmb_tipo.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        'cmb_tipo.Items.Add(elija)
        ' SI ES DE CARTA DE CREDITO.. UNICAMENTE APARECERÁ LA OPCION CARTA DE CREDITO SINO APARECERÁ LA OPCION REESTRUCTURA
        If Session("FOLIOCCRED") <> "" Then
            Dim item2 As New ListItem("CARTA CREDITO", "CCRED")
            ' cmb_tipo.Items.Add(item2)
        ElseIf (Session("IDFACTPAGO") <> "") Then
            Dim item2 As New ListItem("PAGO A PROVEEDOR", "FACT")


        ElseIf (Session("FOLIO_ORIGEN") <> "") Then

            Dim item2 As New ListItem("GARANTIA INVERSION", "INVPER")

        Else
            Dim item As New ListItem("REESTRUCTURA", "REEST")
            ' cmb_tipo.Items.Add(item)
            Dim item2 As New ListItem("RENOVACION", "RENOV")
            ' cmb_tipo.Items.Add(item2)
        End If


    End Sub

    Private Sub LenaFoliosActivosXPersona()

        Dim elija As New ListItem("ELIJA", "0")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FOLIOS_X_CLIENTE"
        Session("rs") = Session("cmd").Execute()

        Session("COND") = Session("rs").Fields("COND").Value.ToString

        If Session("rs").Fields("COND").Value.ToString = "0" Then
            lbl_info.Text = "El trabajador no cuenta con préstamos activos"
            lbl_status.Text = "El trabajador no cuenta con préstamos activos"

            Session("Con").Close()
            Exit Sub
        End If

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("FOLIO").Value.ToString, Session("rs").Fields("FOLIO").Value.ToString)

            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'valida que el plazo anterior de la renovación no exceda del limite permitido por la CUACP
    Private Function validarenovacion(ByVal folio As String) As Boolean

        Dim RESULTADO As Boolean
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIOORIGEN", Session("adVarChar"), Session("adParamInput"), 15, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_RENOVACION_PLAZO_MAXIMO]"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            If Session("rs").Fields("RESPUESTA").value.ToString = "1" Then
                RESULTADO = True
            Else
                RESULTADO = False
            End If
        End If

        Session("Con").Close()

        Return RESULTADO
    End Function

    'valida que el plazo anterior de la renovación no exceda del limite permitido por la CUACP
    Private Function Validaproducto_renovacion(ByVal folio As String, ByVal idprod As String) As Boolean

        Dim RESULTADO As Boolean
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIOORIGEN", Session("adVarChar"), Session("adParamInput"), 15, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, idprod)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_RENOVACION_PRODUCTO]"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            Session("NOMBRE_PRODUCTO") = Session("rs").Fields("PRODUCTO").value.ToString


            If Session("rs").Fields("RESPUESTA").value.ToString = "1" Then
                RESULTADO = True
            Else
                RESULTADO = False
            End If
        End If

        Session("Con").Close()

        Return RESULTADO
    End Function

    '----------------------------CREACION DEL EXPEDIENTE------------------------------
    Private Sub CrearExpediente()
        'crea expediente nuevo con: producto, e ID de cliente

        'SE OBTIENE EL ID DEL PRODUCTO
        Dim cad
        cad = Split(cmb_Productos.SelectedItem.Value, "-")

        validacionexp(cad(0))

        If Session("GENERAR") = "OK" Then

            'clasificacion_credito(cad(0)) 'se levanta la session de clasificacion
            NuevoExpediente()
            lbl_FolioCreado.Text = "Se ha generado un nuevo expediente con FOLIO: " + Session("FOLIO")
            pnl_ExpedienteNuevo.Visible = True
            ModalPopupExtender1.Show()


        Else

            If Session("GENERAR") = "OKPREJ" Then

                lbl_status.Text = "Alerta: El agremiado es un prejubilado."
                NuevoExpediente()
                lbl_FolioCreado.Text = "Se ha generado un nuevo expediente con FOLIO: " + Session("FOLIO")
                pnl_ExpedienteNuevo.Visible = True
                ModalPopupExtender1.Show()

            ElseIf Session("GENERAR") = "FALTAINDICEUMA" Then

                lbl_status.Text = "Error: Falta cargar valor de UMA"

            ElseIf Session("GENERAR") = "BLACKLIST" Then

                lbl_status.Text = "Error: El Agremiado no tiene información suficiente para prospectar"

            ElseIf Session("GENERAR") = "PREJUBILADO" Then

                lbl_status.Text = "Error: No puede prospectar un préstamo para el agremiado debido a que se encuentra en licencia prejubilatoria"


            ElseIf Session("GENERAR") = "TIPOTRAB" Then

                lbl_status.Text = "Error: El agremiado es un jubilado."

            ElseIf Session("GENERAR") = "ESTATUSPREJ" Then

                lbl_status.Text = "Error: El agremiado ha finalizado su proceso de prejubilación."

            ElseIf Session("GENERAR") = "NOAPORAGREM" Then

                lbl_status.Text = "Error: El agremiado no tiene aportaciones para el ciclo actual."

            ElseIf Session("GENERAR") = "ERRORCTA" Then

                lbl_status.Text = "Error: No cuenta con una cuenta eje creada. Verifique el configurador de productos de captación"

            ElseIf Session("GENERAR") = "ANTIG" Then
                lbl_status.Text = "Error: No cuenta con antigüedad suficiente para solicitar el producto seleccionado"

            ElseIf Session("GENERAR") = "NOCORRESPONDE" Then
                lbl_status.Text = "Error: El producto elegido no corresponde a su perfil"

            ElseIf Session("GENERAR") = "DIFPROD" Then
                lbl_status.Text = "Error: La reestructura no puede hacerse sobre un producto distinto al del préstamo vigente"

            ElseIf Session("GENERAR") = "CREDINC" Then
                lbl_status.Text = "Error: Aún posee un préstamo en prospección o en proceso de autorización, favor de completar o cancelar"

            ElseIf Session("GENERAR") = "MAXREEST" Then
                lbl_status.Text = "Error: Ya cumple con el máximo de reestructuras"

            ElseIf Session("GENERAR") = "MINREES" Then
                lbl_status.Text = "Error: No ha cubierto el 50% mínimo de su préstamo anterior"

            ElseIf Session("GENERAR") = "DIFPLAZA" Then
                lbl_status.Text = "Error: El expediente de origen no corresponde a la plaza elegida"

            ElseIf Session("GENERAR") = "CREDMAX" Then
                lbl_status.Text = "Error: El máximo de préstamos activos que puede poseer son 2"

            ElseIf Session("GENERAR") = "SALDO" Then
                lbl_status.Text = "Error: El sueldo neto es inferior al salario mínimo"

            ElseIf Session("GENERAR") = "CAPACIDADPAGO" Then
                lbl_status.Text = "Error: No cuenta con capacidad de pago"

            ElseIf Session("GENERAR") = "NOEXISTSFACTOR" Then
                lbl_status.Text = "Error: No se ha guardado el Factor de Seguro Hipotecario para el año vigente."

            ElseIf Session("GENERAR") = "HIP" Then
                lbl_status.Text = "Error: Ya posee un préstamo activo"

            ElseIf Session("GENERAR") = "ERRORBENE" Then

                lbl_status.Text = "Error: No se puede otorgar préstamos a beneficiarios"

            ElseIf Session("GENERAR") = "SINAPORT" Then

                lbl_status.Text = "Error: Existen quincenas atrasadas de carga de layout de la institución"

            ElseIf Session("GENERAR") = "FECHAPA" Then

                lbl_status.Text = "Error: excede la fecha límite de colocación de PA's"
            ElseIf Session("GENERAR") = "FECHAPC" Then

                lbl_status.Text = "Error: PC's no habilitados todavía"

            ElseIf Session("GENERAR") = "PRESATRASO" Then

                lbl_status.Text = "Error: El trabajador tienes descuentos atrasados de un préstamo activo"

            Else
                lbl_status.Text = "Error: No cuenta con una cuenta eje asignada a la sucursal. Verifique el configurador de productos de captación"

            End If

        End If
        'lbl_status.Text = Session("GENERAR").ToString
    End Sub

    Private Sub Validacion_Tabulador(ByVal Idprod As Integer, ByVal idpersona As Integer, ByVal idsuc As Integer)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Idprod)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, idpersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, idsuc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TABULADOR_VALIDACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            ViewState("CANDADO_TABULADOR") = Session("rs").fields("RESPUESTA").value.ToString
        End If

        Session("Con").Close()
    End Sub


    'CREACION DEL NUEVO EXPEDIENTE
    Private Sub NuevoExpediente()
        Dim cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")
        Dim Estatus As Integer
        Estatus = 0

        'Inserta un nuevo expediente y se genera un nuevo FOLIO
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPOPROD", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, cad(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_RENOV_REESTR", Session("adVarChar"), Session("adParamInput"), 50, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON_EMPROB", Session("adVarChar"), Session("adParamInput"), 1000, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_RENOVACION", Session("adVarChar"), Session("adParamInput"), 15, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EMERGENCIA_MEDICA", Session("adVarChar"), Session("adParamInput"), 10, Estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_ORIGEN_REEST", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZA", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EXPEDIENTE_FOLIO", Session("adVarChar"), Session("adParamInput"), 15, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_NUEVO_EXPEDIENTE"
        Session("rs") = Session("cmd").Execute()
        Session("FOLIO") = Session("rs").fields("FOLIO").value.ToString
        Session("Con").Close()

    End Sub

    '-----------------------------VALIDACIONES--------------------------------
    Private Sub validacionexp(ByVal IDPROD As String)
        Dim Estatus As Integer
        Estatus = 0

        'lbl_info.Text = CStr(cmb_folioori.SelectedItem.Value) + " " + Session("TIPOPER").ToString
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, IDPROD)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZA", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_ORI", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERSONA", Session("adVarChar"), Session("adParamInput"), 15, Session("TIPOPER").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EMERGENCIA_MEDICA", Session("adVarChar"), Session("adParamInput"), 10, Estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_VALIDACION_EXP"
        Session("rs") = Session("cmd").Execute()
        Session("GENERAR") = Session("rs").fields("RESPUESTA").value.ToString
        Session("Con").Close()


    End Sub

    Private Sub Mayoredad()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_VERIFICACION_EDAD"
        Session("rs") = Session("cmd").Execute()
        Session("RESPUESTA") = Session("rs").fields("RESPUESTA").value.ToString
        Session("Con").Close()


    End Sub

    Private Sub clasificacion_credito(ByVal idprod As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, idprod)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_VERIFICACION_CLASIFICACION_CREDITO"
        Session("rs") = Session("cmd").Execute()


        Session("CLASIFICACION") = Session("rs").fields("CLASIFICACION").value.ToString
        Session("ID") = Session("rs").fields("ID").value.ToString
        Session("ASPX") = Session("rs").fields("ASPX").value.ToString
        Session("Con").Close()


    End Sub

    Private Sub tipoderenovacion(ByVal FOLIO As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RENOVACION_TIPO"
        Session("rs") = Session("cmd").Execute()

        Session("TIPORENOVACION") = Session("rs").fields("TIPO").value.ToString

        Session("Con").Close()
    End Sub

    'VALIDA EL NUMERO DE RENOVACIONES QUE TIENE EL FOLIO ORIGEN A RENOVAR
    Private Function validanumrenovacion(ByVal FOLIORENOVAR As String) As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, FOLIORENOVAR)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RENOVACION_NUM"
        Session("rs") = Session("cmd").Execute()

        Dim RESULTADO As Boolean
        If Session("rs").Fields("NUM").value.ToString = "0" Then
            RESULTADO = True
        Else
            RESULTADO = False
        End If
        Session("Con").Close()

        Return RESULTADO
    End Function

    'FUNCION QUE VALIDA SI YA LIQUIDO EL TOTAL DE SUS INTERESES
    Private Function validaintereses_renovacion(ByVal FOLIORENOVAR As String) As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, FOLIORENOVAR)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_RENOVACION_INTERESES]"
        Session("rs") = Session("cmd").Execute()

        Dim RESULTADO As Boolean
        If Session("rs").Fields("RESPUESTA").value.ToString = "1" Then
            RESULTADO = True
        Else
            RESULTADO = False
        End If
        Session("Con").Close()

        Return RESULTADO
    End Function

    'FUNCION QUE VALIDA SI EL EXPEDIENTE ORIGEN A UNA RENOVACION O REESTRUCTURA NO EXISTE EN OTRO EXPEDIENTE
    Private Function validaexpedienteorigen(ByVal FOLIORENOVAR As String, ByVal DESTINO As String) As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO_ORIGEN", Session("adVarChar"), Session("adParamInput"), 20, FOLIORENOVAR)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESTINO", Session("adVarChar"), Session("adParamInput"), 20, DESTINO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_CNFEXP_EXPEDIENTE_ORIGEN]"
        Session("rs") = Session("cmd").Execute()

        Dim RESULTADO As Boolean
        If Session("rs").Fields("EXISTE").value.ToString = "SI" Then
            RESULTADO = False
        Else
            RESULTADO = True
        End If
        Session("Con").Close()

        Return RESULTADO
    End Function

    'RANGOS DEL PLAZO PARA HACER EFECTIVA UNA CARTA DE CREDITO
    Private Function Rangosmonto_plazo() As Boolean

        Dim RESULTADO As Boolean
        RESULTADO = False

        Dim cad
        cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 15, cad(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIOCCRED", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIOCCRED"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_CARTA", Session("adVarChar"), Session("adParamInput"), 15, Session("IDCARTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_CNFEXP_CCRED_RANGOS]"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            If Session("rs").Fields("APLICA").value.ToString = "1" Then 'SI ESTA EL PLAZO DE HACER EFECTIVA LA CARTA DENTRO DE LOS LIMITES CONFIGURADOS Y EL MONTO DE LA CARTA TAMBN
                RESULTADO = True
            Else
                RESULTADO = False
            End If

        End If


        Session("Con").Close()
        Return RESULTADO

    End Function

    'FUNCION QUE VERIFICA SI EL CLIENTE TIENE MORA
    Private Function TieneMora() As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 15, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_MORA"
        Session("rs") = Session("cmd").Execute()

        Dim RESULTADO As Boolean
        If Session("rs").Fields("RESULTADO").value.ToString = "SI" Then
            RESULTADO = True
        Else
            RESULTADO = False
        End If
        Session("Con").Close()

        Return RESULTADO
    End Function

    'REVISA LOS SEMAFOROS
    Private Function RevisaSemaforos() As Boolean

        Dim resultado As Boolean = True

        For Each DataGriditem In dag_ConfExpediente.Items
            If DataGriditem.cells(1).Text = 0 Then
                resultado = False
            End If
        Next

        Return resultado

    End Function

    'Validación del miembro del consejo 
    Private Sub validacionmiembroconsejo()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 15, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDACION_RELACION_CONSEJO"
        Session("rs") = Session("cmd").Execute()
        Session("ALERTA") = Session("rs").fields("ALERTA").value.ToString()
        Session("Con").Close()
    End Sub

    Private Function ExpedienteLiberado() As String
        Dim res As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EXPEDIENTE_LIBERADO"
        Session("rs") = Session("cmd").Execute()
        res = Session("rs").fields("RESULTADO").value.ToString
        Session("Con").Close()

        Return res

    End Function

    Private Sub RevisaEstatusExp()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EXP_ANALISIS_FASE1"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("RESULTADO").value.ToString() = "SI" Then
            ' lnk_comitepet.Enabled = True
        Else
            ' lnk_comitepet.Enabled = False
        End If

        Session("Con").Close()

    End Sub

    '------------------------------ CONTROLES ------------------------------
    Private Sub ConfiguracionExpediente()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtConfExpediente As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOPROD").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONFIGURACION_EXPEDIENTE"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtConfExpediente, Session("rs"))
        'se vacian los expedientes al formulario
        dag_ConfExpediente.DataSource = dtConfExpediente
        dag_ConfExpediente.DataBind()

        Session("Con").Close()

        'reviso si esta en analisis fase 1 para mostrar el envio directo a comite de credito
        RevisaEstatusExp()
    End Sub

    Protected Sub dag_ConfExpediente_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_ConfExpediente.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforo"), Image)
            Dim terminado As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Estatus").ToString())

            If terminado = 1 Then
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(6).Visible = False 'aspx
        e.Item.Cells(7).Visible = False 'multiple

    End Sub

    Private Sub dag_Expendientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Expendientes.ItemCommand

        Session("FOLIO") = e.Item.Cells(1).Text
        Session("CVEEXPE") = e.Item.Cells(2).Text
        Session("PRODUCTO") = e.Item.Cells(3).Text
        'Session("DOMICILIO") = e.Item.Cells(11).Text

        If e.Item.Cells(11).Text = "CRED" Then
            Session("TIPOPROD") = 1
            clasificacion_credito(e.Item.Cells(0).Text)

        End If

        If e.Item.Cells(11).Text = "CAP" Then
            Session("TIPOPROD") = 2

        End If

        If (e.CommandName = "ELIMINAR") Then
            CancelaExpediente()

            lbl_status.Text = "Expediente cancelado"
        End If

        If (e.CommandName = "CONTINUAR") Then

            pnl_cnfexp.Visible = True
            ConfiguracionExpediente()

            lbl_subtitfolio.Text = Session("CVEEXPE").ToString
            lbl_subtitprod.Text = Session("PRODUCTO").ToString
            lbl_subtitcli.Text = Session("CLIENTE").ToString
            'lbl_subtitdom.Text = Session("DOMICILIO").ToString
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "up")
            folderA(pnl_cnfexp, "down")
        End If

    End Sub

    Private Sub dag_ConfExpediente_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_ConfExpediente.ItemCommand

        Dim apartado
        Dim aspx As String

        If (e.CommandName = "MODIFICAR") And e.Item.Cells(4).Text = "-" Then
            apartado = Split(e.Item.Cells(3).Text, "-")
            Session("APARTADO") = apartado(0)
            aspx = e.Item.Cells(6).Text
            Session("ESTATUS_EXPEDIENTE") = e.Item.Cells(9).Text

            If e.Item.Cells(7).Text = "1" Then
                If Session("CLASIFICACION") = "CCRED" Or Session("CLASIFICACION") = "FACT" Then
                    lbl_statusconf.Text = "Este tipo de producto no cuenta con un plan de pagos, continue con el siguiente semáforo"
                Else
                    Response.Redirect(Session("ASPX"))

                End If
            Else
                If Session("CLASIFICACION") = "ARFIN" And e.Item.Cells(3).Text = "5. GARANTIAS / SEGURO - CAPTURA DE GARANTIAS Y SEGUROS" Then
                    Response.Redirect("inventario_arfin.aspx")
                End If
                Response.Redirect(aspx)
            End If

        Else
            lbl_statusconf.Text = "Error: Faltan apartados por configurar antes que el seleccionado"
        End If

    End Sub

    Protected Sub cmb_Productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Productos.SelectedIndexChanged
        lbl_status.Text = ""
        ClasCred()
        'chk_emergencia.Checked = False
        If cmb_Productos.SelectedItem.Value.ToString = "0" Then
            Limpiarenovacion() 'Renovacion / Reestructura
            'cmb_plaza.Items.Clear()
        Else
            Dim cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")
            'lbl_descripcion.Text = cad(1)
            lblidproducto.Text = cad(0)
            Limpiarenovacion() 'Renovacion / Reestructura

        End If

        folderA(div_selCliente, "up")
        folderA(pnl_cnfexp, "up")
        folderA(pnl_expedientes, "down")
    End Sub

    Private Sub paga_credito_arfin()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_LIBERA_CREDITO_ARFIN"
            Session("rs") = Session("cmd").Execute()

            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString
            Session("Con").Close()
        End If

        If Session("RESPUESTA") = "ERROR" Then
            lbl_statusconf.Text = "Error: Debe actualizar el plan de pagos."
        Else
            LlenaPendientes()
            lbl_statusconf.Text = "El expediente ha sido liberado con éxito"
        End If

    End Sub

    'Rango de los montos
    Private Function Rangosmonto() As Decimal
        Dim montocapneto As Decimal

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_RANGOS_MONTOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            montocapneto = Session("rs").Fields("MONTO_CAP_NETO").value.ToString

        End If

        Session("Con").Close()

        Return montocapneto

    End Function

    Private Function ValidaCapitalNeto() As Integer

        Dim MontoCredito As Decimal
        Dim Monto_Creditos As Decimal
        Dim MontoCapNeto As Decimal

        MontoCapNeto = Rangosmonto()

        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        MontoCredito = Session("rs").fields("MONTO").value.ToString

        Session("Con").Close()

        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MONTOS_ACUMULADOS_CREDITO"
        Session("rs") = Session("cmd").Execute()

        Monto_Creditos = Session("rs").fields("MONTO_CREDITOS").value.ToString

        Session("Con").Close()

        If (Monto_Creditos + MontoCredito) > MontoCapNeto Then
            Return 0
        Else
            Return 1
        End If

    End Function

    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        validaNumPersona()
        pnl_cnfexp.Visible = False
    End Sub

    Private Sub validaNumPersona()
        lbl_status.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: RFC incorrecto."
                pnl_cnfexp.Visible = False
                pnl_expedientes.Visible = False
                folderA(div_selCliente, "down")
                div_NombrePersonaBusqueda.Visible = False
                Limpiarenovacion()
            Else
                ValidaPersona(txt_IdCliente.Text)
                'Metodo que llena el droplist con los tipos de productos
                Session("CLIENTE") = Session("PROSPECTO")
                Session("PROSPECTO") = Nothing
                lbl_nompros.Text = Session("CLIENTE").ToString
                lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)
                lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
                lbl_otroscred.Text = "PAGO POR OTROS PRÉSTAMOS: " + FormatCurrency(Session("OTROSCRED").ToString)
                tbx_region.Text = Session("AGR_REGION").ToString
                tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
                tbx_ct.Text = Session("AGR_CT").ToString
                tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
                tbx_pension.Text = FormatCurrency(Session("PEN").ToString)
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                Limpiarenovacion()

            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
            ValidaPersona(txt_IdCliente.Text)
            Limpiarenovacion()
        End If
    End Sub

    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
            folderA(div_selCliente, "down")
        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Private Sub BuscarIDCliente()

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
            Session("SALARIO") = Session("rs").fields("SALARIO").value.ToString
            Session("CAPACIDAD") = Session("rs").fields("CAPACIDAD").value.ToString
            Session("OTROSCRED") = Session("rs").fields("OTROSCRED").value.ToString
            Session("PORPLAZORENO") = Session("rs").fields("PLAZORENO").value.ToString
            Session("NUMCTRL") = Session("rs").Fields("NUMCONT").value.ToString
            Session("NUMINST") = Session("rs").Fields("INSTI").value.ToString
            Session("ANTIGUEDAD") = Session("rs").Fields("ANTIGUEDAD").value.ToString
            Session("AGR_REGION") = Session("rs").Fields("REGION").value.ToString
            Session("AGR_DELEGACION") = Session("rs").Fields("DELEGACION").value.ToString
            Session("AGR_CT") = Session("rs").Fields("CT").value.ToString
            Session("APORT") = Session("rs").Fields("APORTACION").value.ToString
            Session("PEN") = Session("rs").Fields("PENSION").value.ToString

        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""

        ElseIf Existe = -1 Then
            pnl_expedientes.Visible = True
            folderA(div_selCliente, "up")
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de control"
            lbl_NombrePersonaBusqueda.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            LimpiaDatosExpedientes()
        Else
            lbl_statusc.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lblidperson.Text = Session("PERSONAID")
            lbl_nompros.Text = Session("CLIENTE").ToString
            lbl_salario.Text = FormatCurrency(Session("SALARIO").ToString)
            lbl_capacidad.Text = FormatCurrency(Session("CAPACIDAD").ToString)
            lbl_otroscred.Text = FormatCurrency(Session("OTROSCRED").ToString)
            tbx_region.Text = Session("AGR_REGION").ToString
            tbx_delegacion.Text = Session("AGR_DELEGACION").ToString
            tbx_ct.Text = Session("AGR_CT").ToString
            tbx_aportaciones.Text = FormatCurrency(Session("APORT").ToString)
            tbx_pension.Text = FormatCurrency(Session("PEN").ToString)
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
        End If

        ValidaPersona(txt_IdCliente.Text)

    End Sub

    Protected Sub btn_ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok.Click
        'Confirma generacion de expedientre nuevo
        ModalPopupExtender1.Hide()

        LlenaPendientes()
        LlenarProductos()
    End Sub

    Public Sub ValidaPersona(idcliente As Integer)
        ViewState("ALERTAS") = ""
        ViewState("NUM") = 0
        ViewState("NUMC") = 0
        ViewState("RES") = ""
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("SEL_VALIDACION_PERSONA", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("IDCLIENTE", SqlDbType.Int)
            Session("parm").Value = idcliente
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                ViewState("RES") = myReader.GetString(0)
                ViewState("EXISTE") = myReader.GetInt32(1)
                ViewState("NUM") = myReader.GetInt32(2)
                ViewState("NUMC") = myReader.GetInt32(3)
                ViewState("ALERTAS") = ViewState("ALERTAS") + "<br />" + myReader.GetString(4)
            End While
            myReader.Close()
        End Using

        If ViewState("RES") = "NO" Then
            'If ViewState("NUM") <> "5" Then
            lbl_alertas.Text = ViewState("ALERTAS")
            pnl_alertas.Visible = True
            modal_alertas.Show()
            pnl_expedientes.Visible = True
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "down")
            folderA(pnl_cnfexp, "up")
            lbl_maxreest.Text = ""
            LlenaPendientes()
            LlenarTipoProd()
            LlenarProductos()

        Else
            LlenarTipoProd()
            pnl_alertas.Visible = True
            pnl_expedientes.Visible = True
            folderA(div_selCliente, "down")
            folderA(pnl_expedientes, "down")
            folderA(pnl_cnfexp, "up")
            lbl_maxreest.Text = ""
            'llena droplist con los expedientes pendientes del cliente
            LlenaPendientes()
            LlenarProductos()
        End If

        If ViewState("EXISTE") = 1 Then
            pnl_expedientes.Visible = True
            pnl_cnfexp.Visible = True
        Else
            pnl_expedientes.Visible = False
            pnl_cnfexp.Visible = False
        End If

    End Sub

    'Protected Sub LlenaPlazas()
    'Dim cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")
    '  cmb_plaza.Items.Clear()
    'Dim elija As New ListItem("ELIJA", "0")
    ' cmb_plaza.Items.Add(elija)
    '  Session("cmd") = New ADODB.Command()
    ' Session("Con").Open()
    ' Session("cmd").ActiveConnection = Session("Con")
    '   Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '  Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
    '  Session("cmd").Parameters.Append(Session("parm"))
    ' Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, cad(0))
    ' Session("cmd").Parameters.Append(Session("parm"))
    ' Session("cmd").CommandText = "SEL_PLAZAS_PERSONA"
    ' Session("rs") = Session("cmd").Execute()
    'Do While Not Session("rs").EOF
    'Dim item As New ListItem(Session("rs").Fields("NOMBRAMIENTO").Value.ToString, Session("rs").Fields("IDPLAZA").Value.ToString)
    '  cmb_plaza.Items.Add(item)
    ' Session("rs").movenext()
    'Loop
    ' Session("Con").Close()
    'End Sub

    Protected Sub lnk_GeneraExp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_GeneraExp.Click
        lbl_status.Text = ""

        ''-------------------------------------------------VALIDACIONES PRINCIPALES--------------------------------------------------

        'If cmb_TipoProductos.SelectedItem.Value.ToString = "1" Then
        'If cmb_destino.SelectedItem.Value.ToString = "0" Then
        '    lbl_status.Text = "Error: Elija un destino."
        '    Exit Sub
        'End If
        'End If
        If cmb_Productos.SelectedItem.Value.ToString = "0" Then
            lbl_status.Text = "Error: Elija un producto."
            Exit Sub
        End If
        'If cmb_plaza.SelectedItem.Value.ToString = "0" Then
        'lbl_status.Text = "Error: Elija una plaza."
        'Exit Sub
        'End If
        Dim numtrab As Integer = 0
        If txt_IdCliente.Text = "" Then
            numtrab = 0
        Else
            numtrab = CInt(txt_IdCliente.Text)
        End If

        'Dim actualizainfo As New Act_InfoTrab

        ' actualizainfo.ActualizaInfo(numtrab)

        CrearExpediente()
        LlenarTipoProd()
        LlenarProductos()
        Limpiarenovacion()
        'cmb_Productos.Items.Clear()
        'cmb_plaza.Items.Clear()
        'chk_emergencia.Checked = False
        folderA(div_selCliente, "up")
        folderA(pnl_expedientes, "down")
        folderA(pnl_cnfexp, "up")

    End Sub

    Protected Sub btn_okcomite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_okcomite.Click
        Dim razon As String
        If Len(txt_razoncom.Text) > 2000 Then
            razon = Left(txt_razoncom.Text, 2000)
        Else
            razon = txt_razoncom.Text
        End If

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 2000, razon)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCOMIT", Session("adVarChar"), Session("adParamInput"), 15, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_EXP_ANALISIS_COMITE"
        Session("cmd").Execute()

        Session("Con").Close()

        txt_razoncom.Text = ""

        ModalPopupExtender_comite.Hide()

    End Sub

    Protected Sub btn_cancelcomite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelcomite.Click
        ModalPopupExtender_comite.Hide()
    End Sub

    Private Sub SaldoInicialCapVista()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "INS_CNFEXP_SALDO_INICIAL_CAPTACION_VISTA"
            Session("cmd").Execute()

            Session("Con").Close()
        End If

        LlenaPendientes()
        lbl_statusconf.Text = "Se ha liberado correctamente"

    End Sub

    'LIBERACION EXPEDIENTE CREDITO
    Private Sub LiberarCredito()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_LIBERA_CREDITO"
            Session("rs") = Session("cmd").Execute()

            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString
            Session("Con").Close()
        End If

        Select Case Session("RESPUESTA")
            Case "ERROR"
                lbl_statusconf.Text = "Error: Debe actualizar el plan de pagos, su fecha de pago es menor a la fecha de sistema."
                Exit Sub

            Case "AGREGAR"
                LlenaPendientes()
                lbl_statusconf.Text = "El expediente ha sido liberado con éxito"
            Case Else
                lbl_statusconf.Text = ""
        End Select

    End Sub

#Region "Libera Credito X Descuentos"

    'Liberar expedientes por medio de descuentos de nomina
    Private Sub LiberarCreditoDescuentos()

        Dim MensajeLiberar As String = VerificaCaja()
        Dim RES As String = ""
        Dim RESDESC As String = ""

        If MensajeLiberar = "OK" Then

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDEQUIPO", Session("adVarChar"), Session("adParamInput"), 15, Session("ID_EQ").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CREDITO_PAGO_INICIAL_DESCUENTOS"
            Session("rs") = Session("cmd").Execute()

            RES = Session("rs").Fields("RES").value.ToString
            RESDESC = Session("rs").Fields("RESDESC").value.ToString

            Session("Con").Close()

            Select Case RES

                Case "0"
                    MensajeLiberar = RESDESC
                Case "1"
                    MensajeLiberar = RESDESC
                    LlenaPendientes()
                Case Else
                    MensajeLiberar = ""

            End Select

        End If

        lbl_statusconf.Text = MensajeLiberar

    End Sub

    Private Function VerificaCaja() As String

        Dim resultado As String = ""

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
        ViewState("AUXILIAR") = Session("rs").Fields("RES").Value.ToString
        ViewState("IDCAJA_USR") = Session("rs").Fields("IDCAJA").Value.ToString
        Session("Con").Close()

        If ViewState("AUXILIAR") = "0" Then
            resultado = "Tu equipo no esta dado de alta para operar como caja, por lo tanto no puedes usar este apartado"
        Else
            resultado = "OK"
        End If

        ViewState("AUXILIAR") = Nothing
        ViewState("IDCAJA_USR") = Nothing

        Return resultado

    End Function

#End Region

    'LIBERACION CREDITO RENOVADO
    Private Sub LIBERACREDITO_RENOVADO()


        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 15, Session("ID_EQ").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_RENOVACION_LIQUIDA_CREDITO"
            Session("rs") = Session("cmd").Execute()

            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString
            Session("Con").Close()
        End If


        Select Case Session("RESPUESTA")

            Case "ERROR"
                lbl_statusconf.Text = "Error: Debe actualizar el plan de pagos, su fecha de pago es menor a la fecha de sistema."
                Exit Sub
            Case "AGREGAR"
                LlenaPendientes()
                lbl_statusconf.Text = "El expediente ha sido liberado con éxito"
            Case Else
                lbl_statusconf.Text = ""
        End Select




    End Sub

    'LIBERACION CUENTA CORRIENTE
    Private Sub PagaCreditoCTACorriente()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_LIBERA_CREDITO_CTACORRIENTE"
            Session("rs") = Session("cmd").Execute()

            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString
            Session("Con").Close()
        End If

        Select Case Session("RESPUESTA")

            Case "ERROR"
                lbl_statusconf.Text = "Error: Debe actualizar el plan de pagos, su fecha de pago es menor a la fecha de sistema."
                Exit Sub
            Case "NOCOINCIDIR"
                lbl_statusconf.Text = "Error: No coincide la fecha de pago con la fecha de sistema"
                Exit Sub
            Case "AGREGAR"
                LlenaPendientes()
                lbl_statusconf.Text = "El expediente ha sido liberado con éxito"
            Case Else
                lbl_statusconf.Text = ""
        End Select



    End Sub

    'LIBERACION CREDITO SIMPLE REVOLVENTE
    Private Sub PagaCreditoSREV()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_SREV_LIBERA_CREDITO"
            Session("rs") = Session("cmd").Execute()
            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").value.ToString
            Session("Con").Close()
        End If

        Select Case Session("RESPUESTA")

            Case "ERROR"
                lbl_statusconf.Text = "Error: Debe actualizar el plan de pagos, su fecha de pago es menor a la fecha de sistema."
                Exit Sub
            Case "NOCOINCIDIR"
                lbl_statusconf.Text = "Error: No coincide la fecha de pago con la fecha de sistema"
                Exit Sub
            Case "AGREGAR"
                LlenaPendientes()
                lbl_statusconf.Text = "El expediente ha sido liberado con éxito"
            Case Else
                lbl_statusconf.Text = ""
        End Select


    End Sub

    'LIBERACION CARTA CREDITO
    Private Sub PagaCreditoCCRED()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_LIBERA_CREDITO_CARTA_CREDITO"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()
        End If

        LlenaPendientes()
        lbl_statusconf.Text = "El expediente ha sido liberado con éxito"

    End Sub

    'LIBERACION CARTA CREDITO
    Private Sub PagaCreditoFACT()

        Dim res As String = ExpedienteLiberado()

        If res = "NO" Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_LIBERA_CREDITO_FACTORAJE"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()

            lbl_statusconf.Text = "El expediente ha sido liberado con éxito"
        Else
            lbl_statusconf.Text = "Error: El expediente ya fue liberado."
        End If

        LlenaPendientes()

    End Sub

    '---------------------------------CANCELAR EXPEDIENTE--------------
    Private Sub CancelaExpediente()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_CNFEXP_CANCELA_EXPEDIENTE"
        Session("cmd").Execute()

        Session("Con").Close()
        LlenaPendientes()

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
            '  Create a DataTable with the modified rows.

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("SEL_VALIDACION_PERSONA", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = New SqlParameter("IDCLIENTE", SqlDbType.Int)
            Session("parm").Value = Session("PERSONAID").ToString
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While myReader.Read()
                ViewState("RES") = myReader.GetString(0)
                ViewState("EXISTE") = myReader.GetInt32(1)
                ViewState("NUM") = myReader.GetInt32(2)
                ViewState("NUMC") = myReader.GetInt32(3)
                ViewState("ALERTAS") = ViewState("ALERTAS") + "<br />" + myReader.GetString(4)
            End While
            myReader.Close()
        End Using
    End Sub

    Private Sub AvisoCambioEstatus()

        Dim correo As String

        'Insertar a la Cola de validacion para la fase 2
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AVISOCORREO_ESTATUS_USUARIO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            correo = "Estimado(a) " + Session("rs").Fields("USUARIO").Value.ToString + vbCrLf + vbCrLf + Session("rs").Fields("CONTENIDO").Value.ToString + vbCrLf
            correo = correo + vbCrLf + "Atentamente" + vbCrLf + vbCrLf + "MAS.Core" + vbCrLf + Session("EMPRESA").ToString

            Const ConfigNamespace As String = "http://schemas.microsoft.com/cdo/configuration/"
            Dim oMsg As New CDO.Message()
            Dim iConfig As New CDO.Configuration()
            Dim Flds As ADODB.Fields = iConfig.Fields
            With iConfig.Fields
                .Item(ConfigNamespace & "smtpserver").Value = Session("MAIL_SERVER")
                .Item(ConfigNamespace & "smtpserverport").Value = Session("MAIL_SERVER_PORT")
                .Item(ConfigNamespace & "sendusing").Value = CDO.CdoSendUsing.cdoSendUsingPort
                If Session("MAIL_SERVER_SSL") = 1 Then
                    .Item(ConfigNamespace & "smtpusessl").Value = True
                End If
                .Item(ConfigNamespace & "sendusername").Value = Session("MAIL_SERVER_USER")
                .Item(ConfigNamespace & "sendpassword").Value = Session("MAIL_SERVER_PWD")
                .Item(ConfigNamespace & "smtpauthenticate").Value = CDO.CdoProtocolsAuthentication.cdoBasic
                .Update()
            End With

            With oMsg
                .Configuration = iConfig
                .From = Session("MAIL_SERVER_FROM")
                .To = Session("rs").Fields("EMAIL").Value.ToString
                .Subject = "CAMBIO DE ESTATUS DE EXPEDIENTE (" + CStr(Session("FOLIO")) + ")"
                .TextBody = correo
                .Send()
            End With
            oMsg = Nothing
            iConfig = Nothing

            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub lnk_pas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_pas.Click

        HistoricoSalario()
        pnl_modal_hist.Visible = True
        mpe_confirmar.Show()

    End Sub

    Private Sub HistoricoSalario()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtHistorial As New Data.DataTable()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 20, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DESCUENTOS_PAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtHistorial, Session("rs"))
        dag_Historico.DataSource = dtHistorial
        dag_Historico.DataBind()

        Session("Con").Close()
    End Sub

    'folder close or open
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#696462	 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#696462	 !important")
            head.Attributes.CssStyle.Add("color", "fff")
            head.Attributes.CssStyle.Add("border", "solid 1px #fff")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion

    End Sub

End Class