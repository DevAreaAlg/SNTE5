Public Class UNI_REFERENCIAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Referencias", "Asignación de Referencias")
        If Not Me.IsPostBack Then

            'LLENO COMBOS CORRESPONDIENTES
            LlenaInstituciones()
            Llenareferencias()
            LlenaVialidad()
            Llenatiporelacion()

            Session("idperbusca") = Nothing

            'Datos Generales de Expediente: Folio, Nombre de Cliente y producto
            'lbl_subtitulo.Text = Session("APARTADO").ToString + "(Referencias)"

            lbl_Folio.Text = "Datos del Expediente: " + Session("CVEEXPE")

            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")

            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))

            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")
            IniciaContador()
            expbloqueado()

        End If

        'Declaro las funciones necesarias que se mandan llamar por medio de Java Script
        txt_cp.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cp.ClientID + "','" + btn_buscadat.ClientID + "')")
        lnk_BusquedaCP.Attributes.Add("OnClick", "busquedaCP()")
        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente1.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If


    End Sub

    '----------------------------VERIFICA SI EL EXPEDIENTE ESTÁ BLOQUEADO----------------------------------------
    Private Sub expbloqueado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("BLOQUEADO") = Session("rs").fields("BLOQUEADO").value.ToString()

            If Session("BLOQUEADO") = "1" Then 'Si está bloqueado el expediente se deshabilitan los botones
                btn_guardar.Enabled = False
                btn_agregar.Enabled = False

            End If

        End If
        Session("Con").Close()


    End Sub

    'Llena las referencias que tiene agregadas esa persona.
    Private Sub Llenareferencias()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtreferencias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_REFERENCIAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtreferencias, Session("rs"))
        DAG_referencias.DataSource = dtreferencias
        DAG_referencias.DataBind()
        Session("Con").Close()

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
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Instituciones.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'Procedimiento que obtiene el catálogo de tipo de relación  y las despliega en el combo correspondiente
    Private Sub Llenatiporelacion()

        cmb_tiporel.Items.Clear()
        cmb_relacion.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "")
        cmb_tiporel.Items.Add(elija)
        cmb_relacion.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_tiporel.Items.Add(item)
            cmb_relacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    'Procedimiento que obtiene el catálogo de vialidades y las despliega en el combo correspondiente
    Private Sub LlenaVialidad()

        cmb_tipo_via.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VIALIDAD"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "")

        cmb_tipo_via.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
            'Se llena combo de tipo de vialidad de la pestaña de dirección personal
            cmb_tipo_via.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'Procedimiento que guarda una dirección
    Private Function GuardaDireccion(ByVal personaid As String, ByVal tipodir As String, ByVal idasenta As String, ByVal idmuni As String,
                                     ByVal idedo As String, ByVal idvi As String, ByVal calle As String, ByVal numext As String,
                                     ByVal numint As String, ByVal cp As String, ByVal referencia As String, ByVal tipoviv As String,
                                     ByVal antig As String, ByVal latitud As String, ByVal longitud As String, ByVal zoom As String) As String

        Dim iddireccion As String
        iddireccion = ""

        'SE VERIFICA QUE EXISTAN LOS DATOS MINIMOS PARA CREAR EL REGISTRO DE DIRECCION
        If cp <> "" And calle <> "" Then

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, personaid)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 2, tipodir)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 10, idasenta)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, idmuni)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, idedo)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, idvi)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, calle)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, numext)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, numint)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, cp)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, referencia)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOVIV", Session("adVarChar"), Session("adParamInput"), 3, tipoviv)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ANTIG", Session("adVarChar"), Session("adParamInput"), 2, antig)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LATITUD", Session("adVarChar"), Session("adParamInput"), 30, latitud) 'latitud)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LONGITUD", Session("adVarChar"), Session("adParamInput"), 30, longitud) 'longitud)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ZOOM", Session("adVarChar"), Session("adParamInput"), 5, zoom)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_DIRECCION"
            Session("rs") = Session("cmd").Execute()
            iddireccion = Session("rs").Fields("IDDIRECCION").Value.ToString
            Session("Con").Close()
        End If

        Return iddireccion

    End Function

    Private Sub DAG_referencias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_referencias.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            lbl_statusnuevaref.Text = ""
            lbl_statusgeneral.Text = ""
            Elimina_referencia(e.Item.Cells(0).Text)
            Llenareferencias()
        End If

    End Sub

    Protected Sub dag_referencias_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_referencias.ItemDataBound

        e.Item.Cells(0).Visible = False
        If Session("BLOQUEADO") = "1" Then
            e.Item.Cells(4).Visible = False ' Se pone invisible la columna eliminar referencia

        End If
    End Sub

    'Elimina completamente la referencia de la BD
    Private Sub Elimina_referencia(ByVal cvereferencia As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVEREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, cvereferencia)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_REFERENCIAS"
        Session("cmd").Execute()

        Session("CONTADOR") = Session("CONTADOR") - 1
        Session("DIFERENCIA") = CInt(Session("MINREFERENCIA")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        lbl_refer.Text = "Faltan: " + Session("DIFERENCIA").ToString + " referencia(s)"

        Session("Con").Close()

        Session("idperbusca") = Nothing

    End Sub

    Private Sub LimpiaForma()

        cmb_colonia.Items.Clear()
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_tipo_via.SelectedIndex = 0
        cmb_tiporel.SelectedIndex = 0
        txt_calle.Text = ""
        txt_conocer.Text = ""
        txt_cp.Text = ""
        txt_ladamov.Text = ""
        txt_materno.Text = ""
        txt_nombres.Text = ""
        txt_paterno.Text = ""
        txt_referencia.Text = ""
        txt_telmov.Text = ""
        lbl_status.Text = ""

    End Sub

    Private Sub LimpiaForma2()
        cmb_relacion.SelectedIndex = 0
        txt_conocer2.Text = ""
        txt_IdCliente1.Text = ""
        txt_IdCliente.Text = ""
        Session("idperbusca") = Nothing
    End Sub

    ''Agrega una nueva referencia
    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click

        lbl_status.Text = ""  'Limpio el lbl status
        lbl_statusgeneral.Text = ""

        obtieneId()


        If Session("CONTADOR") < Session("MAXREFERENCIA") And Session("MAXREFERENCIA") >= Session("MINREFERENCIA") Then

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIEMPOCONOCER", Session("adVarChar"), Session("adParamInput"), 10, txt_conocer2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRES", Session("adVarChar"), Session("adParamInput"), 200, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 5, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 15, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 2, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIO", Session("adVarChar"), Session("adParamInput"), 4, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 5, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CALLENUM", Session("adVarChar"), Session("adParamInput"), 200, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFEXP_REFERENCIA"
            Session("rs") = Session("cmd").Execute()

            Dim Existe As Integer
            Existe = 0

            If Not Session("rs").eof Then

                Existe = Session("rs").fields("NOEXISTE").value.ToString

                If Existe = 1 Then
                    lbl_status.Text = "Error: No existe el número de cliente: " + txt_IdCliente.Text
                Else
                    Select Case Session("rs").Fields("EXISTE").value.ToString

                        Case "ELMISMO"
                            lbl_status.Text = "Error: No puede asignarse a sí mismo"
                        Case "YAEXISTE"
                            lbl_status.Text = "Error: Esta persona ya fue asignada como referencia"
                        Case "NOEXISTE"

                            Select Case Session("rs").Fields("RESPUESTA").value.ToString

                                Case "PERSONAINCOMPLETA"
                                    lbl_status.Text = "Error: Persona con datos incompletos"

                                Case Else
                                    lbl_status.Text = ""

                            End Select
                        Case Else
                            lbl_status.Text = ""
                    End Select

                    'Se inserta la referencia y modifico el contador
                    If Session("rs").Fields("FLAG").value.ToString = "CONTAR" Then
                        Contador()
                        lbl_status.Text = "Guardado correctamente"
                    End If

                End If



            End If

            Session("Con").Close()
            LimpiaForma2()
            Llenareferencias()

        Else
            lbl_statusgeneral.Text = "Error: Ya cumple con el máximo de referencias establecido"
            LimpiaForma2()
        End If

    End Sub



    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDEPEN", Session("adVarChar"), Session("adParamInput"), 10, ddl_Instituciones.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString
        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""


        Else

            txt_IdCliente.Text = CStr(idp)


        End If

        Session("Con").Close()
    End Sub



    'Botón que guarda una nueva referencia creada
    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click


        If Session("CONTADOR") < Session("MAXREFERENCIA") And Session("MAXREFERENCIA") >= Session("MINREFERENCIA") Then
            Dim MaxlengthRefBene As Integer
            MaxlengthRefBene = CInt(txt_referencia.Text.Length)


            If MaxlengthRefBene <= 300 Then
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, -1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporel.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TIEMPOCONOCER", Session("adVarChar"), Session("adParamInput"), 10, txt_conocer.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NOMBRES", Session("adVarChar"), Session("adParamInput"), 100, txt_nombres.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_paterno.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, txt_materno.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 6, txt_ladamov.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 15, txt_telmov.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 2, cmb_estado.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIO", Session("adVarChar"), Session("adParamInput"), 4, cmb_municipio.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 5, cmb_colonia.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CALLENUM", Session("adVarChar"), Session("adParamInput"), 200, txt_calle.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, txt_referencia.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                'AGREGO LOS NUEVOS PARAMETROS CREADOS
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_CNFEXP_REFERENCIA"
                Session("rs") = Session("cmd").Execute()

                Dim Existe As Integer
                Existe = 0
                Existe = Session("rs").fields("NOEXISTE").value.ToString
                Session("Con").Close()

                If Existe = 0 Then
                    Llenareferencias()
                    Contador()
                    LimpiaForma()

                    lbl_statusnuevaref.Text = "Guardado correctamente"
                End If
            Else
                lbl_statusnuevaref.Text = "Error: Excede el número de caracteres permitidos en el campo referencias"
            End If


        Else
            lbl_statusnuevaref.Text = "Error: Ya cumple con el máximo de referencias establecido"
            LimpiaForma()

        End If






    End Sub

    'Trae de la BD los valores minimos y máximos de referencias que tiene esa persona
    Protected Sub IniciaContador()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MIN_MAX_REQ_ADICIONALES"
        Session("rs") = Session("cmd").Execute()

        Session("MINREFERENCIA") = Session("rs").Fields("MINREF").value.ToString
        Session("MAXREFERENCIA") = Session("rs").Fields("MAXREF").value.ToString
        Session("CONTADOR") = Session("rs").Fields("CONTADORREFERENCIA").value.ToString

        Session("DIFERENCIA") = CInt(Session("MINREFERENCIA")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        Session("Con").Close()

        lbl_refer.Text = "Faltan: " + Session("DIFERENCIA").ToString + " referencia(s)"
    End Sub

    Private Sub Contador()

        Session("CONTADOR") = Session("CONTADOR") + 1
        Session("DIFERENCIA") = CInt(Session("MINREFERENCIA")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If

        lbl_refer.Text = "Faltan: " + Session("DIFERENCIA").ToString + " referencia(s)"
    End Sub


    Public Function busquedaCP_Global(ByVal CP As String) As DropDownList()

        Dim cmb_estado, cmb_municipio, cmb_colonia As New DropDownList

        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()
        If CP = "" Then
            Exit Function
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, CP)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        Dim idedo As String = ""
        Dim idmuni As String = ""

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP

            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString
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

        Return {cmb_estado, cmb_municipio, cmb_colonia}
    End Function


    Private Sub busquedaCP(ByVal CP As String)
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()
        If txt_cp.Text = "" Then
            Exit Sub
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, CP)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        Dim idedo As String = ""
        Dim idmuni As String = ""

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP

            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString
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
    End Sub

    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        busquedaCP(txt_cp.Text)
    End Sub

    Protected Sub lnk_Regresar_Click()

        If Session("IDENTIF") = "CRED" Then
            Response.Redirect("/CREDITO/EXPEDIENTES/CRED_EXP_APARTADO4.aspx")
        ElseIf Session("IDENTIF") = "CAP" Then
            Response.Redirect("/CAPTACION/EXPEDIENTES/CAP_EXP_APARTADO2.aspx")
        End If
    End Sub

End Class