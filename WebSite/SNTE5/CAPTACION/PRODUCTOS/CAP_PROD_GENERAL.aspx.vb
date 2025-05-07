Public Class CAP_PROD_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Productos de Captación", "Producto de Captación")
        If Not Me.IsPostBack Then


            TipoProducto()

            If (Session("PRODID") IsNot Nothing) Then
                Dim objecto As Object = Session("PRODID")
                If (objecto IsNot Nothing) Then
                    lbl_Alertas.Text = Nothing
                    cmb_Productos.Visible = True
                    btn_Cancelar.Visible = True
                    lnk_ProductoNuevo.Enabled = False

                    Session("CONFCOMPLETO") = 0
                    LlenarProductos()

                    LlenarDatosProducto(Session("PRODID"))

                    Dim tipo
                    tipo = cmb_tipo.SelectedValue.ToString
                    cmb_Productos.Items.FindByValue(Session("PRODID") + "-" + tipo).Selected = True
                    Dim producto
                    producto = Split(cmb_Productos.SelectedItem.Value.ToString, "-")
                    Session("IDPRODRES") = producto(0)

                    ConfiguracionProducto(Session("PRODID"))
                    Producto_Activo_Inactivo()

                    If Session("FACULTAD") = "1" Then
                        btn_eliminar.Visible = True
                    End If
                    ''Muestro la liga de resumen con su variable de session

                    txt_NombreProducto.Enabled = True
                    txt_DescripcionProducto.Enabled = True
                    cmb_tipoper.Enabled = True
                    cmb_tipo.Enabled = False

                    btn_GuardarProducto.Visible = True
                    btn_GuardarProducto.Enabled = True
                    dag_ConfProductos.Visible = True

                End If

            End If

        End If


    End Sub

    '***********************LLENA COMBO DE TIPO DE PRODUCTO********************
    Private Sub TipoProducto()

        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmb_tipo.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_tipo.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("cmd").CommandText = "SEL_PRODUCTOS_CAPTACION_TIPO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_tipo.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub



    '**********************NUEVO PRODUCTO*******************
    'btn Generar producto nuevo
    Protected Sub btn_GenerarProducto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GenerarProducto.Click
        If txt_DescripcionProducto.Text.Length > 300 Then
            lbl_Alertas.Text = "Error: Excede el máximo de 300 caracteres en la descripción"
        Else
            'se crea el nuevo producto
            NuevoProducto()
        End If

    End Sub

    'Creación de un nuevo producto
    Private Sub NuevoProducto()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPOPROD", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBREPROD", Session("adVarChar"), Session("adParamInput"), 50, txt_NombreProducto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipoper.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCPROD", Session("adVarChar"), Session("adParamInput"), 300, txt_DescripcionProducto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_NUEVO_PRODUCTO_CAPTACION"
        Session("rs") = Session("cmd").Execute()

        Session("PRODID") = Session("rs").fields("IDPROD").value.ToString

        Session("Con").Close()

        If Session("PRODID").ToString > -1 Then

            lbl_Alertas.Text = "Guardado correctamente"
            ConfiguracionProducto(Session("PRODID").ToString)
            LlenarDatosProducto(Session("PRODID").ToString)
            btn_GenerarProducto.Visible = False
            btn_GuardarProducto.Visible = True

            Producto_Activo_Inactivo()

        Else 'YA EXISTE UN PRODUCTO CON EL MISMO NOMBRE
            lbl_Alertas.Text = "Error: Ya existe un producto con el mismo nombre asigando"
        End If

    End Sub

    'lnk nuevo producto
    Protected Sub lnk_ProductoNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ProductoNuevo.Click

        lbl_Alertas.Text = Nothing
        txt_NombreProducto.Enabled = True
        txt_DescripcionProducto.Enabled = True
        cmb_tipoper.Enabled = True
        cmb_tipo.Enabled = True
        btn_GenerarProducto.Visible = True
        btn_Cancelar.Visible = True
        lnk_ProductoEditar.Enabled = False
        lnk_ProductoNuevo.Enabled = False

    End Sub

    '*************************EDICION DEL PRODUCTO***************

    'lnk editar
    Protected Sub lnk_ProductoEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_ProductoEditar.Click
        lbl_Alertas.Text = Nothing
        txt_NombreProducto.Text = ""
        txt_DescripcionProducto.Text = ""
        cmb_tipoper.Text = -1
        cmb_Productos.Visible = True
        btn_Cancelar.Visible = True
        lnk_ProductoNuevo.Enabled = False

        LlenarProductos()
    End Sub

    'Llena los productos creados para ser editados
    Private Sub LlenarProductos()

        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmb_Productos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_Productos.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("cmd").CommandText = "SEL_PRODUCTOS_CONF_CAPTACION"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("IDPROD").Value.ToString + "-" + Session("rs").Fields("TIPO").Value.ToString)
            cmb_Productos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_Productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Productos.SelectedIndexChanged

        Dim productos
        productos = Split(cmb_Productos.SelectedItem.Value.ToString, "-")


        If productos(0) <> 0 Then
            lbl_Alertas.Text = Nothing
            txt_NombreProducto.Enabled = True
            txt_DescripcionProducto.Enabled = True
            cmb_tipoper.Enabled = True
            cmb_tipo.Enabled = False

            Session("CONFCOMPLETO") = 0
            Session("PRODID") = productos(0)
            Session("IDPRODRES") = productos(0)
            Session("TIPO") = productos(1) 'TIPO DE PRODUCTO 2 ES CAPTACION 3 ES INVERSION
            lnk_resumen.Visible = True
            LlenarDatosProducto(Session("PRODID"))
            ConfiguracionProducto(Session("PRODID"))
            Producto_Activo_Inactivo()

            btn_GuardarProducto.Visible = True
            btn_GuardarProducto.Enabled = True
            dag_ConfProductos.Visible = True

            If Session("FACULTAD") = "1" Then
                btn_eliminar.Visible = True
            End If
        Else
            lbl_Alertas.Text = Nothing
            btn_GuardarProducto.Enabled = False
            txt_NombreProducto.Text = ""
            txt_DescripcionProducto.Text = ""
            'cmb_tipoper.Text = 0
            cmb_tipo.Text = 0

            dag_ConfProductos.Visible = False
            Chk_ActivaDesactivar.Checked = False
            Chk_ActivaDesactivar.Enabled = False
            Session("IDPRODRES") = Nothing
            lnk_resumen.Visible = False
        End If
    End Sub

    'Muestra los valores que fueron registrados para mostrarlos.
    Private Sub LlenarDatosProducto(ByVal idproducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONFIGURACION_DATOS_PRODUCTO_CAPTACION"
        Session("rs") = Session("cmd").Execute()

        txt_NombreProducto.Text = Session("rs").fields("NOMBRE").value.ToString
        Session("PROD_NOMBRE") = Session("rs").fields("NOMBRE").value.ToString
        txt_DescripcionProducto.Text = Session("rs").fields("DESCRIPCION").value.ToString
        cmb_tipoper.Text = Session("rs").fields("TIPOP").value.ToString
        cmb_tipo.Text = Session("rs").fields("TIPOPROD").value.ToString




        Session("Con").Close()

    End Sub

    'Btn que permite editar el producto creado
    Protected Sub btn_GuardarProducto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardarProducto.Click
        If txt_DescripcionProducto.Text.Length > 300 Then
            lbl_Alertas.Text = "Error: Excede el máximo de 300 caracteres en la descripción"
        Else
            EditarProducto()
        End If
    End Sub

    'Editar el producto
    Private Sub EditarProducto()

        Dim edito As Integer

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_NombreProducto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 300, txt_DescripcionProducto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOP", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipoper.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCAP", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONFIGURACION_DATOS_PRODUCTO_CAPTACION"
        Session("rs") = Session("cmd").Execute()

        edito = Session("rs").fields("EDITO").value.ToString

        Session("Con").Close()

        If edito <> 0 Then
            lbl_Alertas.Text = "Guardado correctamente"
            ConfiguracionProducto(Session("PRODID").ToString)
            LlenarDatosProducto(Session("PRODID").ToString)
            btn_GenerarProducto.Visible = False
            btn_GuardarProducto.Visible = True
            Producto_Activo_Inactivo()

        Else
            lbl_Alertas.Text = "Error: Ya existe un producto con el mismo nombre asigando"
        End If

    End Sub


    '*******************DATA GRID*****************************

    'Data Grid que muestra los apartados que tiene el producto
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


        'Si desea modificar el producto
        If (e.CommandName = "MODIFICAR") Then
            'Selecciona el nombre que tiene el dbgrid y lo guarda en una variable de sesión
            apartado = Split(e.Item.Cells(3).Text, "-")
            Session("APARTADO") = apartado(1)
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

    'Btn Cancelar
    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click

        Session("PRODID") = Nothing
        Response.Redirect("CAP_PROD_GENERAL.aspx")

    End Sub



    '****************ACTIVACION Y DESACTIVACION DEL PRODUCTO

    Private Sub ProductoActivar()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMPROD", Session("adVarChar"), Session("adParamInput"), 50, Session("PROD_NOMBRE").ToString)
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
        Session("parm") = Session("cmd").CreateParameter("NOMPROD", Session("adVarChar"), Session("adParamInput"), 50, Session("PROD_NOMBRE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONFIGURACION_DESACTIVAR_PRODUCTO"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

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
            'lbl_ActivarDesactivar.Text = "Activar Producto"
        ElseIf Session("rs").Fields("ESTATUS").Value.ToString() = 1 Then
            Chk_ActivaDesactivar.Checked = True
            Chk_ActivaDesactivar.Enabled = True
        End If

        'Session("CONFCOMPLETO") = 0

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
            'lbl_ActivarDesactivar.Text = "Activar Producto"
        Else
            ProductoDesactivar()
            'lbl_ActivarDesactivar.Text = "Activar Producto"
        End If

    End Sub



    Private Sub lnk_resumen_Click(sender As Object, e As EventArgs) Handles lnk_resumen.Click
        Response.Redirect("CAP_PROD_RESUMEN.aspx")
    End Sub

    '***************SALIR Y REGRESAR*******************
    Private Sub LIMPIAVARIABLES()
        Session("TIPO") = Nothing
        Session("PROD_NOMBRE") = Nothing
        Session("PRODID") = Nothing
        Session("APARTADO") = Nothing
        Session("VENGODE") = Nothing
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
            Response.Redirect("ConfProductoCap.aspx")
        End If




    End Sub
    Protected Sub btn_eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminar.Click
        ProductoEliminar()
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

        Session("FACULTAD") = Session("rs").Fields("FACULTAD").Value.ToString

        Session("Con").Close()
    End Sub

End Class