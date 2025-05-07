Public Class CAP_EXP_BENEFICIARIOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Beneficiarios", "Asignación de Beneficiarios")

        If Not Me.IsPostBack Then
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("FOLIO"))

            Llenabeneficiario()
            LlenaVialidad()
            Llenatiporelacion()
            IniciaContador()

        End If


        'txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + lnk_Continuar.ClientID + "')")
        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")
        'Declaro las funciones necesarias que se mandan llamar por medio de Java Script
        txt_cp.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cp.ClientID + "','" + btn_buscadat.ClientID + "')")
        lnk_BusquedaCP.Attributes.Add("OnClick", "busquedaCP()")


        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If

        If Session("TIPOPROD") = "3" Then
            lnk_copiar_beneficiario.Visible = True
        Else
            lnk_copiar_beneficiario.Visible = False
        End If

    End Sub

    'Llena las referencias que tiene agregadas esa persona.
    Private Sub Llenabeneficiario()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtbeneficiario As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_BENEFICIARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtbeneficiario, Session("rs"))
        DAG_BEN.DataSource = dtbeneficiario
        DAG_BEN.DataBind()


        Session("Con").Close()

    End Sub

    Private Sub DAG_BEN_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_BEN.ItemCommand


        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            lbl_statusnuevoben.Text = ""
            lbl_count.Text = ""
            Elimina_beneficiario(e.Item.Cells(0).Text)
            Llenabeneficiario()
        End If

    End Sub

    Protected Sub dag_beneficiario_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_BEN.ItemDataBound
        e.Item.Cells(0).Visible = False
    End Sub

    'Elimina completamente la referencia de la BD
    Private Sub Elimina_beneficiario(ByVal cvebeneficiario As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVEBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, cvebeneficiario)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "DEL_CNFEXP_BENEFICIARIO"
        Session("cmd").Execute()


        Session("CONTADOR") = Session("CONTADOR") - 1
        Session("DIFERENCIA") = CInt(Session("MINBENEFICIARIO")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        lbl_beneficiario.Text = "Faltan: " + Session("DIFERENCIA").ToString + " beneficiario(s)"


        Session("Con").Close()
        Session("idperbusca") = Nothing

    End Sub

    ''CP
    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_colonia.Items.Clear()

        If (txt_cp.Text) = "" Then
            Exit Sub
        End If

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
            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString)
            cmb_estado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
            cmb_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                cmb_colonia.Items.Add(item)

                Session("rs").movenext()
            Loop
        Else 'NO SE ENCONTRO EL CP
            'lbl_cpnoenc.Visible = True
        End If
        Session("Con").Close()
    End Sub


    'Procedimiento que obtiene el catálogo de tipo de relación  y las despliega en el combo correspondiente
    Private Sub Llenatiporelacion()

        ' cmb_tiporel.Items.Clear()
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

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click

        If Session("CONTADOR") < Session("MAXBENEFICIARIO") And Session("MAXBENEFICIARIO") >= Session("MINBENEFICIARIO") Then
            'Permite calcular la suma de la columna del porcentaje evaluando que no pase del 100%
            Dim suma As Integer
            Dim MaxlengthRefBene As Integer
            If txt_porcentaje.Text <> "" Then
                suma = CInt(txt_porcentaje.Text)
            Else
                suma = 0
            End If

            For Each DataGriditem In DAG_BEN.Items

                suma = suma + CInt(DataGriditem.cells(3).Text)

            Next

            'lbl_count.Text = CStr(suma)
            MaxlengthRefBene = CInt(txt_referencia.Text.Length)
            If MaxlengthRefBene <= 300 Then
                If (suma) <= 100 Then

                    Session("cmd") = New ADODB.Command()
                    Session("Con").Open()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("IDBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, -1)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tiporel.SelectedItem.Value)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("PORCENTAJE", Session("adVarChar"), Session("adParamInput"), 10, txt_porcentaje.Text)
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
                    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    'AGREGO LOS NUEVOS PARAMETROS CREADOS
                    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))

                    Session("cmd").CommandText = "INS_CNFEXP_BENEFICIARIO"
                    Session("rs") = Session("cmd").Execute()

                    'Se inserta el beneficiario y modifico el contador
                    If Session("rs").Fields("FLAG").value.ToString = "CONTAR" Then
                        Contador()
                        lbl_statusnuevoben.Text = "Guardado correctamente"
                    End If

                    Session("Con").Close()
                    LimpiaForma()
                    Llenabeneficiario()
                Else
                    lbl_statusnuevoben.Text = "Error: Excede el 100%"
                    LimpiaForma3()
                End If
            Else
                lbl_statusnuevoben.Text = "Error: Excede el número de caracteres permitidos en el campo (Referencias)"
            End If
            'Verifico que no pase del 100%

        Else
            lbl_statusnuevoben.Text = "Error: Ya cumple con el máximo de beneficiarios establecidos"
            LimpiaForma()
        End If
    End Sub

    Private Sub LimpiaForma()
        Session("idperbusca") = Nothing
        cmb_colonia.Items.Clear()
        cmb_estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_tipo_via.SelectedIndex = 0
        cmb_tiporel.SelectedIndex = 0
        txt_calle.Text = ""
        txt_porcentaje.Text = ""
        txt_cp.Text = ""
        txt_ladamov.Text = ""
        txt_materno.Text = ""
        txt_nombres.Text = ""
        txt_paterno.Text = ""
        txt_referencia.Text = ""
        txt_telmov.Text = ""
        txt_IdCliente.Text = ""

    End Sub

    Private Sub LimpiaForma2()
        cmb_relacion.SelectedIndex = 0
        txt_porcentaje.Text = ""
        txt_porcentajebus.Text = ""
        Session("idperbusca") = Nothing
        txt_IdCliente.Text = ""
    End Sub

    Private Sub LimpiaForma3()

        txt_porcentaje.Text = ""
        Session("idperbusca") = Nothing
        LimpiaForma2()
    End Sub



    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click

        lbl_status.Text = ""  'Limpio el lbl status


        If Session("CONTADOR") < Session("MAXBENEFICIARIO") And Session("MAXBENEFICIARIO") >= Session("MINBENEFICIARIO") Then


            Dim suma As Integer

            If txt_porcentajebus.Text <> "" Then
                suma = CInt(txt_porcentajebus.Text)
            Else
                suma = 0
            End If

            For Each DataGriditem In DAG_BEN.Items

                suma = (suma + CInt(DataGriditem.cells(3).Text))

            Next


            If ((suma) <= 100) Then

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PORCENTAJE", Session("adVarChar"), Session("adParamInput"), 10, txt_porcentajebus.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NOMBRES", Session("adVarChar"), Session("adParamInput"), 100, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 6, "")
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
                Session("cmd").CommandText = "INS_CNFEXP_BENEFICIARIO"
                Session("rs") = Session("cmd").Execute()

                If Not Session("rs").eof Then

                    Select Case Session("rs").Fields("EXISTE").value.ToString
                        Case "NOEXISTEID"
                            lbl_status.Text = "Error: No existe el número de cliente " + txt_IdCliente.Text
                        Case "INCOMPLETO"
                            lbl_status.Text = "Error: Persona con datos incompletos"

                        Case "ELMISMO"
                            lbl_status.Text = "Error: No puede asignarse a sí mismo"
                        Case "YAEXISTE"
                            lbl_status.Text = "Error: Esta persona ya fue asignada como beneficiario"
                        Case "NOEXISTE"
                            Select Case Session("rs").Fields("RESPUESTA").value.ToString

                                Case "PERSONAINCOMPLETA"
                                    lbl_status.Text = "Error: Persona con datos incompletos"
                                Case Else
                                    lbl_status.Text = ""
                            End Select
                    End Select

                    'Se inserta la referencia y modifico el contador
                    If Session("rs").Fields("FLAG").value.ToString = "CONTAR" Then
                        Contador()
                        lbl_status.Text = "Guardado correctamente"
                    End If

                End If


                Session("Con").Close()
                LimpiaForma2()
                Llenabeneficiario()
            Else
                If suma > 100 Then
                    lbl_status.Text = "Error: Excede del 100%"
                    LimpiaForma2()
                End If

            End If


        Else
            lbl_status.Text = "Error: Ya cumple con el máximo de beneficiarios establecidos"
        End If
    End Sub

    'Trae de la BD los valores minimos y máximos de beneficiarios
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

        Session("MINBENEFICIARIO") = Session("rs").Fields("MINBEN").value.ToString
        Session("MAXBENEFICIARIO") = Session("rs").Fields("MAXBEN").value.ToString
        Session("CONTADOR") = Session("rs").Fields("CONTADORBENEFICIARIO").value.ToString

        Session("DIFERENCIA") = CInt(Session("MINBENEFICIARIO")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        Session("Con").Close()

        lbl_beneficiario.Text = "Faltan: " + Session("DIFERENCIA").ToString + " beneficiario(s)"
    End Sub


    Private Sub Contador()

        Session("CONTADOR") = Session("CONTADOR") + 1
        Session("DIFERENCIA") = CInt(Session("MINBENEFICIARIO")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If

        lbl_beneficiario.Text = "Faltan: " + Session("DIFERENCIA").ToString + " beneficiario(s)"
    End Sub

    Protected Sub lnk_copiar_beneficiario_Click(sender As Object, e As System.EventArgs) Handles lnk_copiar_beneficiario.Click

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
        Session("cmd").CommandText = "INS_CNFEXP_COPIAR_BENEFICIARIO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("RES") = Session("rs").Fields("RESPUESTA").value.ToString

        End If

        Session("Con").Close()

        If Session("RES") = "NO COPIAR" Then
            lbl_count.Text = "No existen beneficiarios agregados en la cuenta origen de la inversión."
        Else
            lbl_count.Text = ""
            Llenabeneficiario()
            IniciaContador()
        End If

    End Sub

End Class