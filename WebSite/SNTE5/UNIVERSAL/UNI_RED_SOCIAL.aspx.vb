Public Class UNI_RED_SOCIAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Red Social", "Red Social")

        If Not Me.IsPostBack Then
            Dim menuPanel As Panel
            menuPanel = CType(Master.FindControl("modulos_menu"), Panel)
            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona

            'vengo de modulo comite de credito O DIRECCION
            If (Session("VENGODE") = "CRED_EXP_EXPEDIENTE.ASPX" Or Session("VENGODE") = "AdmAlertasCCC.aspx" Or Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX" Or Session("VENGODE") = "UNI_RED_SOCIAL.aspx" And Not Session("PERSONAID") Is Nothing) Then
                lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
                MuestraConyuges()
                MuestraConyugesPor()
                MuestraAvales()
                MuestraAvalesPor()
                MuestraCodeudores()
                MuestraCodeudoresPor()
                MuestraReferencias()
                MuestraReferenciasPor()
                MuestraDependientes()
                MuestraDependientesPor()
                MuestraConsejo()
                MuestraExpuesta()
                muestra_creditos_dependientes()
                muestra_creditos_depende_de()
                pnl_cliente.Visible = False
                pnl_redsocial.Visible = True

            ElseIf (Session("VENGODE") = "CRED_EXP_EXP_DIGITAL.aspx") Then
                Session("id") = Session("id")
                txt_IdCliente1.Text = Session("NUMTRAB").ToString
                txt_IdCliente.Text = Session("id").ToString
                BuscarIDCliente(Session("id").ToString, 2)
                Session("VENGODE") = "UNI_RED_SOCIAL.aspx"
            End If
        End If

        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + lnk_Continuar.ClientID + "')")
        lnk_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        'vengo de una busqueda
        If Not Session("idperbusca") Is Nothing Then
            txt_IdCliente1.Text = Session("idperbusca").ToString
            lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
        End If
    End Sub



    Private Sub BuscarIDCliente(ByVal idcliente As String, ByVal tab As Integer)

        'Busca el ID de Cliente que el usuario ingresó y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, idcliente)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString
        End If

        Session("Con").Close()

        If Existe = -1 Then
            Session("idperbusca") = Nothing
            lbl_statusc.Text = "Error: no existe el número de trabajador"
            txt_IdCliente.Text = ""
            pnl_informacion.Visible = False
        Else
            lbl_statusc.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
            MuestraConyuges()
            MuestraConyugesPor()
            MuestraAvales()
            MuestraAvalesPor()
            MuestraCodeudores()
            MuestraCodeudoresPor()
            MuestraReferencias()
            MuestraReferenciasPor()
            MuestraDependientes()
            MuestraDependientesPor()
            MuestraConsejo()
            MuestraExpuesta()
            muestra_creditos_dependientes()
            muestra_creditos_depende_de()
            pnl_informacion.Visible = True

        End If

    End Sub
    'DBGRID Muestra conyuges
    Private Sub MuestraConyuges()
        obtieneId()
        lbl_conyuges.Text = "Cónyuges de: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtconyuges As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_CONYUGES_PERSONA"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtconyuges, Session("rs"))
        Session("Con").Close()
        If dtconyuges.Rows.Count > 0 Then
            dag_conyuges.Visible = True
            dag_conyuges.DataSource = dtconyuges
            dag_conyuges.DataBind()
        Else
            dag_conyuges.Visible = False
        End If
    End Sub

    Private Sub MuestraConyugesPor()

        lbl_conyugespor.Text = Session("PROSPECTO").ToString + " ha sido declarado como cónyuge por:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtconyugespor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_PERSONA_CONYUGES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtconyugespor, Session("rs"))
        Session("Con").Close()
        If dtconyugespor.Rows.Count > 0 Then
            dag_conyugespor.Visible = True
            dag_conyugespor.DataSource = dtconyugespor
            dag_conyugespor.DataBind()
        Else
            dag_conyugespor.Visible = False
        End If
    End Sub
    'DBGRID Muestra AVALES
    Private Sub MuestraAvalesPor()

        lbl_avalespor.Text = Session("PROSPECTO").ToString + " es aval de:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtavalespor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_PERSONA_AVALES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtavalespor, Session("rs"))
        Session("Con").Close()
        If dtavalespor.Rows.Count > 0 Then
            dag_avalespor.Visible = True
            dag_avalespor.DataSource = dtavalespor
            dag_avalespor.DataBind()
        Else
            dag_avalespor.Visible = False
        End If
    End Sub
    'DBGRID Muestra AVALES
    Private Sub MuestraAvales()

        lbl_avales.Text = "Avales de: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtavales As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_AVALES_PERSONA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtavales, Session("rs"))
        Session("Con").Close()
        If dtavales.Rows.Count > 0 Then
            dag_avales.Visible = True
            dag_avales.DataSource = dtavales
            dag_avales.DataBind()
        Else
            dag_avales.Visible = False
        End If

    End Sub

    Protected Sub dag_avales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_avales.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Protected Sub dag_avalespor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_avalespor.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Protected Sub dag_codeudores_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_codeudores.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Protected Sub dag_codeudorespor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_codeudorespor.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    'DBGRID Muestra referencias del cliente
    Private Sub MuestraReferencias()

        lbl_referencias.Text = "Referencias de: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtreferencias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_REFERENCIAS_PERSONA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtreferencias, Session("rs"))
        Session("Con").Close()
        If dtreferencias.Rows.Count > 0 Then
            dag_referencias.Visible = True
            dag_referencias.DataSource = dtreferencias
            dag_referencias.DataBind()
        Else
            dag_referencias.Visible = False
        End If
    End Sub

    'DBGRID Muestra las personas que han asignado como referencia al cliente
    Private Sub MuestraReferenciasPor()

        lbl_referenciaspor.Text = Session("PROSPECTO").ToString + " ha sido asignado como referencia por:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtreferenciaspor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_PERSONA_REFERENCIAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtreferenciaspor, Session("rs"))
        Session("Con").Close()
        If dtreferenciaspor.Rows.Count > 0 Then
            dag_referenciaspor.Visible = True
            dag_referenciaspor.DataSource = dtreferenciaspor
            dag_referenciaspor.DataBind()
        Else
            dag_referenciaspor.Visible = False
        End If
    End Sub

    'DBGRID Muestra CODEUDORES
    Private Sub MuestraCodeudores()

        lbl_codeudores.Text = "Codeudores de: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcodeudores As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_CODEUDORES_PERSONA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcodeudores, Session("rs"))
        Session("Con").Close()
        If dtcodeudores.Rows.Count > 0 Then
            dag_codeudores.Visible = True
            dag_codeudores.DataSource = dtcodeudores
            dag_codeudores.DataBind()
        Else
            dag_codeudores.Visible = False
        End If
    End Sub

    'DBGRID Muestra CODEUDORES
    Private Sub MuestraCodeudoresPor()

        lbl_codeudorespor.Text = Session("PROSPECTO").ToString + " es codeudor de:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcodeudorespor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_PERSONA_CODEUDORES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcodeudorespor, Session("rs"))
        Session("Con").Close()
        If dtcodeudorespor.Rows.Count > 0 Then
            dag_codeudorespor.Visible = True
            dag_codeudorespor.DataSource = dtcodeudorespor
            dag_codeudorespor.DataBind()
        Else
            dag_codeudorespor.Visible = False
        End If
    End Sub

    Protected Sub lnk_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Continuar.Click
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") Is Nothing Then
                lbl_statusc.Text = "Error: número de control incorrecto"
            Else
                lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString

                MuestraAvales()
                MuestraAvalesPor()
                MuestraCodeudores()
                MuestraCodeudoresPor()
                MuestraReferencias()
                MuestraReferenciasPor()
                MuestraConyuges()
                MuestraConyugesPor()
                MuestraDependientes()
                MuestraDependientesPor()
                MuestraConsejo()
                MuestraExpuesta()
                muestra_creditos_dependientes()
                muestra_creditos_depende_de()
                pnl_redsocial.Visible = True


            End If
        Else
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente(txt_IdCliente.Text, 2)
        End If

    End Sub


    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDEPEN", Session("adVarChar"), Session("adParamInput"), 10, 34)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA"
        Session("rs") = Session("cmd").Execute()

        'Session("NO_TRABAJADOR") = txt_IdCliente1.Text

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString
        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""


        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)


        End If

        Session("Con").Close()
    End Sub

    Private Sub DAG_Conyuges_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_conyuges.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(0).Text
            BuscarIDCliente(e.Item.Cells(0).Text, 3)
        End If
    End Sub

    Private Sub DAG_Conyugespor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_conyugespor.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(0).Text
            BuscarIDCliente(e.Item.Cells(0).Text, 3)
        End If
    End Sub

    Private Sub DAG_Avales_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_avales.ItemCommand

        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If

            txt_IdCliente.Text = e.Item.Cells(4).Text
            BuscarIDCliente(e.Item.Cells(4).Text, 3)
        End If

    End Sub

    Private Sub DAG_Avalespor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_avalespor.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(4).Text
            BuscarIDCliente(e.Item.Cells(4).Text, 3)
        End If
    End Sub

    Private Sub DAG_Codeudores_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_codeudores.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(4).Text
            BuscarIDCliente(e.Item.Cells(4).Text, 3)
        End If
    End Sub

    Private Sub DAG_Codeudorespor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_codeudorespor.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(4).Text
            BuscarIDCliente(e.Item.Cells(4).Text, 3)
        End If
    End Sub

    Private Sub DAG_Referencias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_referencias.ItemCommand
        'solo muestro los datos de la referencia si la columna de idpersona tiene un dato distinto a espacio
        If (e.CommandName = "CONSULTAR") And e.Item.Cells(1).Text <> "&nbsp;" Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(1).Text
            BuscarIDCliente(e.Item.Cells(1).Text, 3)
        End If
    End Sub

    Private Sub DAG_Referenciaspor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_referenciaspor.ItemCommand
        'solo muestro los datos de la referencia si la columna de idpersona tiene un dato distinto a espacio
        If (e.CommandName = "CONSULTAR") And e.Item.Cells(1).Text <> "&nbsp;" Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If
            txt_IdCliente.Text = e.Item.Cells(1).Text
            BuscarIDCliente(e.Item.Cells(1).Text, 3)
        End If
    End Sub

    Protected Sub lnk_regorig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_regorig.Click

        If Not Session("REDSOCIAL") Is Nothing Then
            Dim arre = Split(Session("REDSOCIAL"), "-")
            Session("REDSOCIAL") = Nothing
            txt_IdCliente.Text = arre(0)
            BuscarIDCliente(arre(0), 3)
        End If

    End Sub

    Protected Sub lnk_regniv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_regniv.Click

        If Not Session("REDSOCIAL") Is Nothing Then
            Dim destino As String
            Dim arre = Split(Session("REDSOCIAL").ToString, "-")
            destino = arre(arre.Length - 2)

            Dim i As Integer = 0
            Session("REDSOCIAL") = Nothing
            Do While arre(i) <> destino
                Session("REDSOCIAL") = Session("REDSOCIAL") + arre(i) + "-"
                i = i + 1
            Loop
            txt_IdCliente.Text = destino
            BuscarIDCliente(destino, 3)
        End If
    End Sub

    Private Sub MuestraDependientes()

        lbl_depeco.Text = "Dependientes económicos de: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdepeco As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_DEPECO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtdepeco, Session("rs"))
        Session("Con").Close()
        If dtdepeco.Rows.Count > 0 Then
            dag_depeco.Visible = True
            dag_depeco.DataSource = dtdepeco
            dag_depeco.DataBind()
        Else
            dag_depeco.Visible = False
        End If
    End Sub

    Private Sub MuestraDependientesPor()
        lbl_depecopor.Text = Session("PROSPECTO").ToString + " es dependiente económico de:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdepecopor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_PERSONA_DEPECO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtdepecopor, Session("rs"))
        Session("Con").Close()
        If dtdepecopor.Rows.Count > 0 Then
            dag_depecopor.Visible = True
            dag_depecopor.DataSource = dtdepecopor
            dag_depecopor.DataBind()
        Else
            dag_depecopor.Visible = False
        End If
    End Sub

    Private Sub DAG_depeco_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_depeco.ItemCommand

        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If

            txt_IdCliente.Text = e.Item.Cells(4).Text
            BuscarIDCliente(e.Item.Cells(4).Text, 3)
        End If

    End Sub

    Protected Sub dag_depeco_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_depeco.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Private Sub DAG_depecopor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_depecopor.ItemCommand

        If (e.CommandName = "CONSULTAR") Then
            If Session("REDSOCIAL") Is Nothing Then
                Session("REDSOCIAL") = Session("PERSONAID") + "-"
            Else
                Session("REDSOCIAL") = Session("REDSOCIAL") + Session("PERSONAID") + "-"
            End If

            txt_IdCliente.Text = e.Item.Cells(4).Text
            BuscarIDCliente(e.Item.Cells(4).Text, 3)
        End If

    End Sub

    Protected Sub dag_depecopor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_depecopor.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Private Sub MuestraConsejo()
        lbl_consejo.Text = Session("PROSPECTO").ToString + " tiene relación con los miembros:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtconsejo As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_CONSEJO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtconsejo, Session("rs"))
        Session("Con").Close()
        If dtconsejo.Rows.Count > 0 Then
            dag_consejo.Visible = True
            dag_consejo.DataSource = dtconsejo
            dag_consejo.DataBind()
        Else
            dag_consejo.Visible = False
        End If
    End Sub

    Protected Sub dag_consejo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_consejo.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Private Sub MuestraExpuesta()
        lbl_expuesta.Text = Session("PROSPECTO").ToString + " tiene relación con las siguientes personas:"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpuesta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_PPE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtexpuesta, Session("rs"))
        Session("Con").Close()
        If dtexpuesta.Rows.Count > 0 Then
            dag_expuesta.Visible = True
            dag_expuesta.DataSource = dtexpuesta
            dag_expuesta.DataBind()
        Else
            dag_expuesta.Visible = False
        End If
    End Sub

    Protected Sub dag_expuesta_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_expuesta.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim imagen As Image = CType(e.Item.FindControl("Semaforoimg"), Image)
            Dim alerta As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "alerta").ToString())
            'Dim semaforo As String = DataBinder.Eval(e.Item.DataItem, "semaforo").ToString()
            'imagen.ImageUrl = "sysimages\Semaforo" + semaforo + ".png"
            If alerta = 1 Then
                imagen.ImageUrl = "~/img\SemaforoROJO.png"
            Else
                imagen.ImageUrl = "~/img\SemaforoVERDE.png"
                Session("CONFCOMPLETO") = Session("CONFCOMPLETO") + 1
            End If
        End If
        e.Item.Cells(0).Visible = False

    End Sub

    Private Sub muestra_creditos_dependientes()
        lbl_cred_rel_dep.Text = "Préstamos de dependientes económicos de: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcrde As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CRED_REL_DEP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcrde, Session("rs"))
        Session("Con").Close()
        If dtcrde.Rows.Count > 0 Then
            dag_cred_rel_dep.Visible = True
            dag_cred_rel_dep.DataSource = dtcrde
            dag_cred_rel_dep.DataBind()
            exp_calc.Enabled = True
        Else
            dag_cred_rel_dep.Visible = False
        End If
    End Sub

    Private Sub muestra_creditos_depende_de()
        lbl_cred_rel_dep_de.Text = "Préstamos de quienes depende: " + Session("PROSPECTO").ToString

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcrde As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CRED_REL_DEP_DE"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcrde, Session("rs"))
        Session("Con").Close()
        If dtcrde.Rows.Count > 0 Then
            dag_cred_rel_dep_de.Visible = True
            dag_cred_rel_dep_de.DataSource = dtcrde
            dag_cred_rel_dep_de.DataBind()
            exp_calc.Enabled = True
        Else
            dag_cred_rel_dep_de.Visible = False
        End If
    End Sub

    Protected Sub exp_calc_Click() Handles exp_calc.Click
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dttxt As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CRED_REL_CSV"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dttxt, Session("rs"))
        Session("Con").Close()


        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For Each Renglon As Data.DataRow In dttxt.Rows

            For i = 0 To dttxt.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString)

            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename= Reporte de creditos atrasados.csv")
        context.Response.End()
    End Sub

End Class