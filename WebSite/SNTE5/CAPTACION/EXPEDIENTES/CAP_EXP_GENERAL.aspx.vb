Public Class CAP_EXP_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Captación", "Configuración de Expedientes de Captación")
        If Not Me.IsPostBack Then

            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona

            If Session("VENGODE") = "CAP_EXP_APARTADO1.ASPX" Or Session("VENGODE") = "CAP_EXP_APARTADO2.ASPX" Or Session("VENGODE") = "CAP_EXP_APARTADO3.ASPX" Or Session("VENGODE") = "CAP_EXP_APARTADO4.ASPX" Then
                ConfiguracionExpediente()
                LlenaPendientes()
                lbl_subtitfolio.Text = Session("FOLIO").ToString
                lbl_subtitprod.Text = Session("PRODUCTO").ToString
                lbl_subtitcli.Text = Session("CLIENTE").ToString
                pnl_cnfexp.Visible = True
                pnl_expedientes.Visible = True
                'habilito tabs y asigno focus al tab de configuracion
                folderA(div_selCliente, "up")
                folderA(pnl_expedientes, "up")
                folderA(pnl_cnfexp, "down")

            Else
                'limpio variables
                LimpiaVariables()

            End If
            LlenarTipoProd()


        End If
        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_Continuar.ClientID + "')")
        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            folderA(div_selCliente, "down")
            folderA(pnl_expedientes, "up")
            folderA(pnl_cnfexp, "up")
            pnl_cnfexp.Visible = False
            pnl_expedientes.Visible = False
            Session("idperbusca") = Nothing
        End If

        lbl_statusconf.Text = ""
    End Sub

    '----------------------------------- LIMPIA CONTROLES -----------------------------------
    Private Sub Limpiarenovacion()
        Session("NOMBRE_PRODUCTO") = Nothing
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
        LlenarProductosCaptacion()
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
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 2)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, "F")
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
        Session("cmd").CommandText = "SEL_CNFEXP_EXPEDIENTES_CAP_PENDIENTES"

        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtPendientes, Session("rs"))
        dag_Expendientes.DataSource = dtPendientes
        dag_Expendientes.DataBind()

        Session("Con").Close()
    End Sub

    '----------------------------CREACION DEL EXPEDIENTE------------------------------
    Private Sub CrearExpediente()
        'crea expediente nuevo con: producto, e ID de cliente

        'SE OBTIENE EL ID DEL PRODUCTO
        Dim cad = Split(cmb_Productos.SelectedItem.Value, "-")

        Mayoredad()

        validacionexp(cad(0))

        'CAPTACION
        NuevoExpediente_Captacion()
        lbl_descripcion.Text = ""
        lbl_FolioCreado.Text = "Se ha generado un nuevo expediente con FOLIO: " + Session("FOLIO")

        ModalPopupExtender1.Show()

        'End If

    End Sub

    'CREACION DEL NUEVO EXPEDIENTE
    Private Sub NuevoExpediente_Captacion()

        Dim cad
        cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")


        'Inserta un nuevo expediente y se genera un nuevo FOLIO
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPOPROD", Session("adVarChar"), Session("adParamInput"), 10, 2)
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
        Session("parm") = Session("cmd").CreateParameter("EXPEDIENTE_FOLIO", Session("adVarChar"), Session("adParamInput"), 15, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_NUEVO_EXPEDIENTE_CAP"
        Session("rs") = Session("cmd").Execute()
        Session("FOLIO") = Session("rs").fields("FOLIO").value.ToString

        Session("Con").Close()

    End Sub

    '-----------------------------VALIDACIONES--------------------------------
    Private Sub validacionexp(ByVal IDPROD As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, IDPROD)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERSONA", Session("adVarChar"), Session("adParamInput"), 15, Session("TIPOPER").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID").ToString)
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
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "2")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONFIGURACION_EXPEDIENTE"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtConfExpediente, Session("rs"))
        'se vacian los expedientes al formulario

        dag_ConfExpediente.DataSource = dtConfExpediente
        dag_ConfExpediente.DataBind()
        Session("Con").Close()
        'evaluo si ya estan todos los semaforos en verde, si es así habilito el boton de liberar expediente
        btn_liberar.Enabled = RevisaSemaforos()

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

        'e.Item es la fila sobre la cual dio click el cursor
        Session("FOLIO") = e.Item.Cells(1).Text
        Session("PRODUCTO") = e.Item.Cells(2).Text

        If (e.CommandName = "ELIMINAR") Then
            CancelaExpediente()

            lbl_status.Text = "Expediente cancelado"
        End If

        If (e.CommandName = "CONTINUAR") Then

            pnl_cnfexp.Visible = True
            ConfiguracionExpediente()
            lbl_subtitfolio.Text = Session("FOLIO").ToString
            lbl_subtitprod.Text = Session("PRODUCTO").ToString
            lbl_subtitcli.Text = Session("CLIENTE").ToString
            'habilito tabs y asigno focus al tab de configuracion
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "up")
            folderA(pnl_cnfexp, "down")

        End If

    End Sub

    Private Sub dag_ConfExpediente_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_ConfExpediente.ItemCommand
        Session("TIPOP") = "CAP"
        Dim apartado
        Dim aspx As String, enuso As String
        Session("idperbusca") = Nothing
        Session("idperbusca_Usuario") = Nothing
        If (e.CommandName = "MODIFICAR") And e.Item.Cells(4).Text = "-" Then
            apartado = Split(e.Item.Cells(3).Text, "-")
            Session("APARTADO") = apartado(0)
            aspx = e.Item.Cells(6).Text
            enuso = e.Item.Cells(8).Text
            Session("ESTATUS_EXPEDIENTE") = e.Item.Cells(9).Text

            If ((Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Or Session("ESTATUS_EXPEDIENTE") = "DIGITALIZACION FASE 2" Or Session("ESTATUS_EXPEDIENTE") = "DIGITALIZACION FASE 3") And e.Item.Cells(6).Text = "9") Or e.Item.Cells(6).Text <> "9" Then

                If enuso = "NO" Then
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
                    lbl_statusconf.Text = "Error: Este apartado está en uso y no se puede acceder a él por el momento."
                End If
            Else
                lbl_statusconf.Text = "Error: No se puede acceder a este apartado durante el estatus actual"
            End If
        Else
            lbl_statusconf.Text = "Error: Faltan apartados por configurar antes que el seleccionado"
        End If

    End Sub

    Protected Sub cmb_Productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Productos.SelectedIndexChanged
        lbl_status.Text = ""

        If cmb_Productos.SelectedItem.Value.ToString = "0" Then
            Limpiarenovacion() 'Renovacion / Reestructura
        Else
            Dim cad = Split(cmb_Productos.SelectedItem.Value.ToString, "-")

            lbl_DescProducto.Visible = True
            lbl_descripcion.Text = cad(1)

            Limpiarenovacion() 'Renovacion / Reestructura

        End If

        folderA(div_selCliente, "up")
        folderA(pnl_cnfexp, "up")
        folderA(pnl_expedientes, "down")
    End Sub

    Protected Sub btn_liberar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_liberar.Click

        SaldoInicialCapVista()

    End Sub


    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click

        lbl_status.Text = ""

        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: número de cliente incorrecto."
                Limpiarenovacion()
            Else
                'Metodo que llena el droplist con los tipos de productos
                LlenarTipoProd()
                'llena droplist con los expedientes pendientes del cliente
                LlenaPendientes()
                Session("CLIENTE") = Session("PROSPECTO")
                Session("PROSPECTO") = Nothing
                lbl_nompros.Text = "CLIENTE: " + Session("CLIENTE").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                lbl_descripcion.Text = ""
                Limpiarenovacion()
            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
            Limpiarenovacion()
        End If

    End Sub


    Private Sub BuscarIDCliente()

        Dim Existe As Integer
        Existe = 0

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()


        If Not Session("rs").eof Then
            Existe = Session("rs").fields("EXISTE").value.ToString
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Cliente con datos incompletos"
            folderA(div_selCliente, "down")
            pnl_expedientes.Visible = False
            folderA(pnl_expedientes, "up")
            pnl_cnfexp.Visible = False
            folderA(pnl_cnfexp, "up")

            Session("PERSONAID") = txt_IdCliente.Text
            LlenaPendientes()
            'ConfiguracionExpediente()
        ElseIf Existe = -1 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de cliente"
            folderA(div_selCliente, "down")
            pnl_expedientes.Visible = False
            folderA(pnl_expedientes, "up")
            pnl_cnfexp.Visible = False
            folderA(pnl_cnfexp, "up")

            Session("PERSONAID") = txt_IdCliente.Text
            LlenaPendientes()
        Else


            Session("PERSONAID") = txt_IdCliente.Text
            LlenaPendientes()
            lbl_nompros.Text = "CLIENTE: " + Session("CLIENTE").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString

            'Metodo que llena el droplist con los tipos de productos
            LlenarTipoProd()
            lbl_descripcion.Text = ""


            folderA(div_selCliente, "up")
            pnl_expedientes.Visible = True
            folderA(pnl_expedientes, "down")
            folderA(pnl_cnfexp, "up")
            pnl_cnfexp.Visible = False
        End If





    End Sub


    Protected Sub btn_ok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok.Click
        'Confirma generacion de expedientre nuevo
        ' pnl_ExpedienteNuevo.Visible = False
        ModalPopupExtender1.Hide()
        LlenarTipoProd()
        LlenaPendientes()

    End Sub


    Protected Sub lnk_GeneraExp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_GeneraExp.Click
        lbl_status.Text = ""
        '-------------------------------------------------VALIDACIONES PRINCIPALES--------------------------------------------------

        If cmb_Productos.SelectedItem.Value.ToString = "0" Then
            lbl_status.Text = "Error: Elija un producto"
        Else
            CrearExpediente()
            LlenarTipoProd()
            Limpiarenovacion()
            lbl_descripcion.Text = ""
            cmb_Productos.Items.Clear()
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "down")
            folderA(pnl_cnfexp, "up")
        End If


    End Sub


    Protected Sub btn_cancelcomite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelcomite.Click
        ModalPopupExtender_comite.Hide()
    End Sub

    '----------------------------------------------------------LIBERACION DE EXPEDIENTES--------------------------------------------
    'LIBERACION EXPEDIENTE CAPTACION
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
        folderA(div_selCliente, "up")
        folderA(pnl_expedientes, "down")
        folderA(pnl_cnfexp, "up")
        pnl_cnfexp.Visible = False
        LlenaPendientes()
        lbl_statusconf.Text = "Se ha liberado correctamente"
        btn_liberar.Enabled = False

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
    End Sub

    Private Sub AvisoCambioEstatus()

        Dim correo As String
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim usuario As String = String.Empty
        Dim contenido As String = String.Empty
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

            usuario = Session("rs").Fields("USUARIO").Value.ToString
            contenido = Session("rs").Fields("CONTENIDO").Value.ToString
            subject = "CAMBIO DE ESTATUS DE EXPEDIENTE (" + CStr(Session("FOLIO")) + ")"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a):  " + usuario + +contenido + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
        Loop

        Session("Con").Close()
    End Sub

#Region "Sub para abrir y cerrar folders"
    'folder close or open
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#FB7A68 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "none")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#F8F9F9 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #C0CDD5")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion
    End Sub
#End Region

End Class