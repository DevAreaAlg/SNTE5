Public Class CRED_PROD_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            LlenarDestinos()
            llenaclasificacion()
            llenaclaves()
            facultad()
            LlenarProductos()

            If Not (Session("PRODID") Is Nothing) Then

                lbl_Alertas.Text = Nothing
                ddl_productos.Visible = True
                btn_cancelar.Visible = True
                btn_crear.Enabled = False

                Session("CONFCOMPLETO") = 0
                ddl_productos.Items.FindByValue(Session("PRODID")).Selected = True
                LlenarDatosProducto(Session("PRODID").ToString)
                ConfiguracionProducto(Session("PRODID"))
                Producto_Activo_Inactivo()
                If Session("FACULTAD") = "1" Then
                    btn_eliminar.Visible = True
                End If
                txt_nombre.Enabled = True
                txt_DescripcionProducto.Enabled = True
                ddl_clasificacion.Enabled = False
                ddl_tipo_persona.Enabled = True
                btn_guarda_cambios.Visible = True
                dag_ConfProductos.Visible = True
                lnk_resumen.Visible = True

            End If
        End If

        TryCast(Me.Master, MasterMascore).CargaASPX("Productos de Crédito", "Producto de Crédito")
    End Sub

    Private Sub LlenarProductos()

        'Lleno el combo con los productos respecto al tipo de producto elegido
        ddl_productos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        ddl_productos.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_PRODUCTOS_CONF"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem((Session("rs").Fields("MSTPRODUCTOS_NOMBRE").Value.ToString + " (" + Session("rs").Fields("DESTINO").Value.ToString + ")"), Session("rs").Fields("MSTPRODUCTOS_ID_PROD").Value.ToString)
            ddl_productos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub ddl_productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_productos.SelectedIndexChanged

        If ddl_productos.SelectedItem.Value <> 0 Then

            Session("CONFCOMPLETO") = 0
            Session("PRODID") = ddl_productos.SelectedItem.Value
            Session("IDPRODRES") = ddl_productos.SelectedItem.Value

            lbl_Alertas.Text = Nothing
            txt_nombre.Enabled = True
            txt_cveProd.Enabled = True
            txt_DescripcionProducto.Enabled = True
            ddl_clasificacion.Enabled = False
            ddl_tipo_persona.Enabled = True
            cmb_cvedscto.Enabled = True
            VerificaCambioDestino(Session("PRODID"))

            lnk_resumen.Visible = True
            LlenarDatosProducto(Session("PRODID").ToString)

            ConfiguracionProducto(Session("PRODID"))
            Producto_Activo_Inactivo()

            btn_guarda_cambios.Visible = True
            btn_guarda_cambios.Enabled = True
            dag_ConfProductos.Visible = True

            If Session("FACULTAD") = "1" Then
                btn_eliminar.Visible = True
            End If
        Else
            lbl_Alertas.Text = Nothing
            btn_guarda_cambios.Enabled = False
            txt_nombre.Text = ""
            txt_DescripcionProducto.Text = ""
            ddl_clasificacion.Text = 0
            ddl_destino.Text = 0
            dag_ConfProductos.Visible = False
            Chk_ActivaDesactivar.Checked = False
            Chk_ActivaDesactivar.Enabled = False
            Session("IDPRODRES") = Nothing
            lnk_resumen.Visible = False
        End If

    End Sub

    Private Sub facultad()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_FACULTAD_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        Session("FACULTAD") = Session("rs").Fields("RES").Value.ToString

        Session("Con").Close()
    End Sub

    Protected Sub btn_crear_Click(sender As Object, e As EventArgs)
        txt_nombre.Enabled = True
        txt_cveProd.Enabled = True
        ddl_clasificacion.Enabled = True
        ddl_tipo_persona.Enabled = True
        ddl_destino.Enabled = True
        cmb_cvedscto.Enabled = True
        txt_DescripcionProducto.Enabled = True
        btn_crear.Enabled = False
        btn_editar.Enabled = False
        btn_generarP.Visible = True
        btn_cancelar.Visible = True
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click

        Session("PRODID") = Nothing
        Response.Redirect("CRED_PROD_GENERAL.aspx")

    End Sub

    Protected Sub btn_editar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_editar.Click

        lbl_Alertas.Text = Nothing
        txt_nombre.Text = ""
        txt_DescripcionProducto.Text = ""
        ddl_clasificacion.Text = 0
        ddl_tipo_persona.Text = -1
        ddl_destino.Text = 0
        ddl_productos.Visible = True
        btn_cancelar.Visible = True
        btn_crear.Enabled = False
        LlenarProductos()

    End Sub

    Protected Sub btn_generarP_Click(sender As Object, e As EventArgs)
        If txt_DescripcionProducto.Text.Length > 300 Then
            lbl_Alertas.Text = "Error: Sólo 300 caracteres o menos en la descripción"
        Else
            nuevoProducto()
        End If
    End Sub

    Private Sub nuevoProducto()
        'Lleno el combo con los productos respecto al tipo de producto elegido
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBREPROD", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVEPROD", Session("adVarChar"), Session("adParamInput"), 50, txt_cveProd.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCPROD", Session("adVarChar"), Session("adParamInput"), 300, txt_DescripcionProducto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASIFICACION", Session("adVarChar"), Session("adParamInput"), 10, ddl_clasificacion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("OBJPROD", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPROD", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, ddl_tipo_persona.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESTINO", Session("adVarChar"), Session("adParamInput"), 10, ddl_destino.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, cmb_cvedscto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_NUEVO_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        Session("PRODID") = Session("rs").fields("IDPROD").value.ToString

        Session("Con").Close()

        If Session("PRODID").ToString > -1 Then

            lbl_Alertas.Text = "Guardado correctamente"
            ConfiguracionProducto(Session("PRODID").ToString)
            LlenarDatosProducto(Session("PRODID").ToString)
            btn_generarP.Visible = False
            btn_guarda_cambios.Visible = True
            ddl_clasificacion.Enabled = False
            Producto_Activo_Inactivo()
            LlenarProductos()

        Else
            lbl_Alertas.Text = "Error: Ya existe un producto con el mismo nombre asigando a este destino. (Cambiar nombre de producto o asignar a otro destino)"
        End If
    End Sub

    Private Sub LlenarDatosProducto(ByVal idproducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONFIGURACION_DATOS_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        txt_nombre.Text = Session("rs").fields("NOMBRE").value.ToString
        txt_cveProd.Text = Session("rs").fields("CLAVEPROD").value.ToString
        Session("PROD_NOMBRE") = Session("rs").fields("NOMBRE").value.ToString
        txt_DescripcionProducto.Text = Session("rs").fields("DESCRIPCION").value.ToString
        ddl_clasificacion.Text = Session("rs").fields("CLASIFICACION").value.ToString
        'ddl_Institucion.Text = Session("rs").fields("INSTITUCION").value.ToString
        ddl_tipo_persona.Text = Session("rs").fields("TIPOP").value.ToString
        ddl_destino.Text = Session("rs").fields("IDDESTINO").value.ToString
        cmb_cvedscto.Text = Session("rs").fields("CLAVE").value.ToString
        ViewState("BANDERA") = "1"
        ViewState("NOMBREPROD") = Session("rs").fields("CLAVE").value.ToString
        Session("Con").Close()
        LlenarProductos()

        ddl_productos.Items.FindByValue(Session("PRODID")).Selected = True
        VerificaCambioDestino(idproducto)


    End Sub

    Private Sub lnk_resumen_Click(sender As Object, e As EventArgs) Handles lnk_resumen.Click
        Session("IDPRODRES") = ddl_productos.SelectedItem.Value
        Response.Redirect("CRED_PROD_RESUMEN.aspx")
    End Sub

    Private Sub VerificaCambioDestino(ByVal idproducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_VERIFICA_CAMBIO_DESTINO"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").fields("ESTATUS").value.ToString = "1" Then
            ddl_destino.Enabled = True
        Else
            ddl_destino.Enabled = False
        End If
        Session("Con").Close()

    End Sub

#Region "Configuracion Apartados"

    Private Sub ConfiguracionProducto(ByVal idproducto As Integer)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtConfProductos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_CONFIGURACION_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtConfProductos, Session("rs"))
        'se vacian los expedientes al formulario
        dag_ConfProductos.DataSource = dtConfProductos
        dag_ConfProductos.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub dag_ConfProductos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_ConfProductos.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforo"), Image)
            Dim terminado As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Estatus").ToString())

            If terminado = 1 Then
                imagen.ImageUrl = "~\img\SemaforoVERDE.png"
            Else
                imagen.ImageUrl = "~\img\SemaforoROJO.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False

    End Sub

    Private Sub dag_ConfProductos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_ConfProductos.ItemCommand

        Dim idconf As Integer
        Dim aspx As String

        Dim apartado

        If (e.CommandName = "MODIFICAR") Then
            'Selecciona el nombre que tiene el dbgrid y lo guarda en una variable de sesión
            apartado = Split(e.Item.Cells(3).Text, "-")
            Session("APARTADO") = apartado(1)
            Session("VENGODE") = "ConfProducto.aspx"


            idconf = e.Item.Cells(0).Text
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDCONF", Session("adVarChar"), Session("adParamInput"), 10, idconf)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_ASPX_APARTADO_CONF_PROD"
            Session("rs") = Session("cmd").Execute()
            aspx = Session("rs").fields("ASPX").value.ToString
            Session("Con").close()
            Response.Redirect(aspx)
        End If

    End Sub

#End Region

    Private Sub Producto_Activo_Inactivo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ESTATUS_PRODUCTO_ACTIVADO_DESACTIVADO"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("ESTATUS").Value.ToString() = 0 Then
            Chk_ActivaDesactivar.Checked = False

        ElseIf Session("rs").Fields("ESTATUS").Value.ToString() = 1 Then

            Chk_ActivaDesactivar.Checked = True
            Chk_ActivaDesactivar.Enabled = True
        End If

        If Session("CONFCOMPLETO").ToString > 0 Then
            Chk_ActivaDesactivar.Enabled = False
        ElseIf Session("CONFCOMPLETO").ToString = 0 Then
            Chk_ActivaDesactivar.Enabled = True
        End If

        Session("Con").Close()

    End Sub

    Protected Sub Chk_ActivaDesactivar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk_ActivaDesactivar.CheckedChanged

        If Chk_ActivaDesactivar.Checked = True Then
            ProductoActivar()
        Else
            ProductoDesactivar()
        End If

    End Sub

    Private Sub ProductoActivar()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMPROD", Session("adVarChar"), Session("adParamInput"), 100, Session("PROD_NOMBRE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONFIGURACION_ACTIVAR_PRODUCTO"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Sub ProductoDesactivar()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMPROD", Session("adVarChar"), Session("adParamInput"), 100, Session("PROD_NOMBRE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONFIGURACION_DESACTIVAR_PRODUCTO"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub btn_eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminar.Click
        ProductoEliminar()
    End Sub

    Private Sub ProductoEliminar()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONFIGURACION_ELIMINAR_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("RESPUESTA").Value.ToString() = "ERROR" Then
            lbl_Alertas.Text = "Error: No puede eliminar el producto, debido a que existen expedientes pendientes"
            Session("Con").Close()
        Else
            Session("Con").Close()
            Session("PRODID") = Nothing
            Response.Redirect("CRED_PROD_GENERAL.aspx")
        End If

    End Sub

#Region "Catalogos iniciales"

    Private Sub llenaclasificacion()

        ddl_clasificacion.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        ddl_clasificacion.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_CLASIF_CRED"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_clasificacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub llenaclaves()

        cmb_cvedscto.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_cvedscto.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_CLAVES_DSCTO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_cvedscto.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub LlenarDestinos()

        ddl_destino.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        ddl_destino.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("cmd").CommandText = "SEL_CNFPCR_DESTINOS"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESTINO").Value.ToString, Session("rs").Fields("IDDESTINO").Value.ToString)
            ddl_destino.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

#End Region

    Protected Sub btn_guarda_cambios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guarda_cambios.Click

        If txt_DescripcionProducto.Text.Length > 300 Then
            lbl_Alertas.Text = "Error: Solo 300 caracteres o menos en la descripción!"
        Else
            EditarProducto()
        End If

    End Sub

    Private Sub EditarProducto()

        Dim edito As Integer
        Dim cambioDestino As Integer

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVEPROD", Session("adVarChar"), Session("adParamInput"), 50, txt_cveProd.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 300, txt_DescripcionProducto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASIFICACION", Session("adVarChar"), Session("adParamInput"), 10, ddl_clasificacion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("POBOBJ", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOP", Session("adVarChar"), Session("adParamInput"), 10, ddl_tipo_persona.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESTINO", Session("adVarChar"), Session("adParamInput"), 10, ddl_destino.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, cmb_cvedscto.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONFIGURACION_DATOS_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        edito = Session("rs").fields("EDITO").value.ToString
        cambioDestino = Session("rs").fields("DESTINO").value.ToString

        Session("Con").Close()

        If edito <> 0 Then
            lbl_Alertas.Text = "Guardado correctamente"
            ConfiguracionProducto(Session("PRODID").ToString)
            btn_guarda_cambios.Visible = False
            btn_guarda_cambios.Visible = True
            Producto_Activo_Inactivo()
            LlenarProductos()
            ddl_productos.Items.FindByValue(Session("PRODID").ToString).Selected = True
            LlenarDatosProducto(Session("PRODID").ToString)


            If cambioDestino = "1" Then
                lbl_AlertaDestino.Text = "El destino del producto fue actualizado. La cuenta de capital y datos de comisiones fueron eliminados y se deben volver a capturar."
            Else
                lbl_AlertaDestino.Text = ""
            End If

        Else
            lbl_Alertas.Text = "Error: Ya existe un producto con el mismo nombre asigando a este destino. (Cambiar nombre de producto o asignar a otro destino)"
        End If

    End Sub


End Class