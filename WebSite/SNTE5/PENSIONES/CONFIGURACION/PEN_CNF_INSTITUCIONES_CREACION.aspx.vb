Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class PEN_CNF_INSTITUCIONES_CREACION
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Alta Institución", "Configuración de Institución")

        If Not Me.IsPostBack Then
            LlenaTipos()
            LlenaTipo()
            LlenaPaises(cmb_estado.ID)
            llena_vialidad(cmb_vialidad.ID)
            If Session("IdInsti") > 0 Then
                txt_id_institucion.Text = Session("IdInsti")
                carga_datos()
                carga_domicilio()
                carga_contacto()
                LlenaOficinas()
                'llena_Plazas()
                define_avance()
            Else
                txt_id_institucion.Text = "Nueva institución"
                chk_estatus.Enabled = True
            End If
        End If
    End Sub

    Private Sub LlenaTipos()
        cmb_tipo.Items.Clear()
        cmb_tipo.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPOS_INSTITUCION"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_tipo.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID_TIPO").Value.ToString))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaTipo()
        ddl_tipo.Items.Clear()
        ddl_tipo.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPOS_OFI_INST"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_tipo.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID_TIPO").Value.ToString))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaPaises(ByVal id As String)
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento
        cmb_estado.Items.Clear()
        cmb_estado.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PAISES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_estado.Items.Add(New ListItem(Session("rs").Fields("CATPAIS_PAIS").Value, Session("rs").Fields("CATPAIS_ID_PAIS").Value.ToString))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub



    Private Sub llena_vialidad(ByVal id As String)
        'Procedimiento que obtiene el catálogo de vialidades y las despliega en el combo correspondiente
        Dim elija As New ListItem("ELIJA", "-1")

        cmb_vialidad.Items.Clear()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VIALIDAD"
        Session("rs") = Session("cmd").Execute()
        cmb_vialidad.Items.Add(elija)
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATVIALIDAD_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVIALIDAD_ID_VIALIDAD").Value.ToString)
            cmb_vialidad.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Private Sub LlenaOficinas()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtOfi As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 20, Session("IdInsti"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OFICINA_INSTITUCION"

        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtOfi, Session("rs"))
        Session("Con").Close()

        If dtOfi.Rows.Count > 0 Then
            dag_oficinas.Visible = True
            dag_oficinas.DataSource = dtOfi
            dag_oficinas.DataBind()

        Else
            dag_oficinas.Visible = False
        End If


    End Sub

    Private Sub define_avance()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AVANCE_PERSONAAUX"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Dim Avance As Integer = CInt(Session("rs").Fields("AVANCE").Value)

        End If
    End Sub

    Private Sub carga_datos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_INSTITUCION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            cmb_tipo.ClearSelection()
            cmb_tipo.Items.FindByValue(Session("rs").Fields("ID_TIPO").Value.ToString).Selected = True
            txt_clave.Text = Session("rs").Fields("CLAVE").Value
            txt_nombre.Text = Session("rs").Fields("NOMBRE").Value
            txt_rfc.Text = Session("rs").Fields("RFC").Value
            txt_director.Text = Session("rs").Fields("DIRECTOR").Value
            txt_fecha.Text = Left(Session("rs").Fields("FECHAOPE").Value, 10)
            cmb_estado.ClearSelection()
            cmb_estado.Items.FindByValue(Session("rs").Fields("IDPAIS").Value.ToString).Selected = True
            If Session("rs").Fields("ESTATUS").value.ToString = "1" Then
                chk_estatus.Checked = True
            ElseIf Session("rs").Fields("ESTATUS").value.ToString = "0" Then
                chk_estatus.Checked = False
            End If

        End If
        Session("Con").Close()
    End Sub

    Private Sub carga_domicilio()
        Dim id_asen As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONAAUX_DIRECCION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            txt_cp.Text = Session("rs").Fields("CP").Value
            id_asen = Session("rs").Fields("ID_ASEN").Value.ToString
            txt_calle.Text = Session("rs").Fields("CALLE").Value
            cmb_vialidad.Items.FindByValue(Session("rs").Fields("ID_VIAL").Value.ToString).Selected = True
            txt_exterior.Text = Session("rs").Fields("NUM_EXT").Value
            txt_interior.Text = Session("rs").Fields("NUM_INT").Value
            txt_referencias.Text = Session("rs").Fields("REF").Value
        End If
        Session("Con").Close()
        cmb_estado1.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
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
            cmb_estado1.Items.Add(item_edo)
            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            cmb_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)
                cmb_asentamiento.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

        'si se encontraron estado y municipio validos para el cp ingresado entonces busco las localidades correspondientes
        If Not idedo Is Nothing And Not idmuni Is Nothing Then

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
                    cmb_asentamiento.Items.Add(item)
                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
        End If
        'cmb_colonia.ClearSelection()
        cmb_asentamiento.Items.FindByValue(id_asen).Selected = True

    End Sub

    Private Sub carga_contacto()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONAAUX_CONTACTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Select Case Session("rs").Fields("TIPO").Value
                    Case "PAR"
                        txt_lada1.Text = Session("rs").Fields("LADA").Value
                        txt_tel1.Text = Session("rs").Fields("TEL").Value
                        txt_ext1.Text = Session("rs").Fields("EXT").Value
                        Exit Select
                    Case "MOV"
                        txt_lada2.Text = Session("rs").Fields("LADA").Value
                        txt_tel2.Text = Session("rs").Fields("TEL").Value
                        txt_ext2.Text = Session("rs").Fields("EXT").Value
                        Exit Select
                    Case "REC"
                        txt_lada3.Text = Session("rs").Fields("LADA").Value
                        txt_tel3.Text = Session("rs").Fields("TEL").Value
                        txt_ext3.Text = Session("rs").Fields("EXT").Value
                        Exit Select
                    Case "TRA"
                        txt_lada4.Text = Session("rs").Fields("LADA").Value
                        txt_tel4.Text = Session("rs").Fields("TEL").Value
                        txt_ext4.Text = Session("rs").Fields("EXT").Value
                        Exit Select
                End Select
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub guarda_general()
        Dim Estatus As Integer
        If chk_estatus.Checked = False Then
            Estatus = 0
        Else
            Estatus = 1
        End If

        If Session("IdInsti") > 0 Then
            lbl_status.Text = ""
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 12, txt_clave.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DIRECTOR", Session("adVarChar"), Session("adParamInput"), 100, txt_director.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PAIS", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_estado.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHA_OPE", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, Estatus)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_INSTITUCION"
            Session("rs") = Session("cmd").Execute()
            If Session("rs").Fields("RES").Value = 1 Then
                lbl_status.Text = "Guardado correctamente"
            Else
                lbl_status.Text = "Error: Ya existe una institución con el RFC que intenta ingresar. Verifique"
            End If
            Session("Con").Close()
            carga_datos()
            carga_domicilio()

        Else
            Dim si_calend As String = "NO"

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 12, txt_clave.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_tipo.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PAIS", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_estado.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHA_OPE", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("AVANCE", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, Estatus)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIENECALEND", Session("adVarChar"), Session("adParamInput"), 15, si_calend)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_INSTITUCION"
            Session("rs") = Session("cmd").Execute()

            Session("YAEXISTE") = Session("rs").Fields("YAEXISTE").Value
            Session("IdInsti") = CInt(Session("rs").Fields("IDPERSONA").Value.ToString)
            If Session("YAEXISTE") = "NO" Then 'SI ES UNA PERSONA NUEVA
                lbl_status.Text = "Guardado correctamente"
                txt_id_institucion.Text = Session("IdInsti")

            Else
                Session("Con").Close()
                'YA FUE CAPTURADA ESTA PERSONA POR LO QUE NO LA DEJAMOS CONTINUAR
                lbl_status.Text = "Error: La institución ya fue capturada en el sistema."
                Exit Sub

            End If
            Session("Con").Close()
            carga_datos()
            carga_domicilio()
        End If
    End Sub

    Private Sub actualiza_avance(ByVal avance As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AVANCE", Session("adVarChar"), Session("adParamInput"), 10, avance)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AVANCE_PERSONAAUX"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs)
        guarda_general()

        actualiza_avance(1)

    End Sub



    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        busquedaCP(txt_cp.Text)
    End Sub




    Protected Sub btn_buscacp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscacp.Click
        busquedaCPOFi(txt_cp_ofi.Text)

    End Sub


    Private Sub busquedaCP(ByVal CP As String)
        cmb_estado1.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
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
            cmb_estado1.Items.Add(item_edo)
            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            cmb_municipio.Items.Add(item_mun)
            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                cmb_asentamiento.Items.Add(item)
                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()
    End Sub

    Private Sub busquedaCPOFi(ByVal CP As String)
        ddl_Estado.Items.Clear()
        ddl_municipio.Items.Clear()
        ddl_asentamiento.Items.Clear()

        If txt_cp_ofi.Text = "" Then
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
            ddl_Estado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            ddl_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                ddl_asentamiento.Items.Add(item)

                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()
    End Sub

    Protected Sub txt_cp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp.TextChanged
        'Response.Redirect("AltaModPersonaF.aspx")
        cmb_estado1.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
    End Sub

    Private Sub GuardaDireccion()

        Dim contReferencias As Integer

        contReferencias = CInt(txt_referencias.Text.Length)

        If contReferencias <= 2000 Then
            If Session("IdInsti") > 0 Then
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 3)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDASENTA", Session("adVarChar"), Session("adParamInput"), 5, cmb_asentamiento.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDMUNI", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDEDO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado1.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDVI", Session("adVarChar"), Session("adParamInput"), 10, cmb_vialidad.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 100, txt_calle.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 10, txt_exterior.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 10, txt_interior.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 300, txt_referencias.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LATITUD", Session("adVarChar"), Session("adParamInput"), 30, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LONGITUD", Session("adVarChar"), Session("adParamInput"), 30, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ZOOM", Session("adVarChar"), Session("adParamInput"), 5, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_DIRECCIONAUX"
                Session("cmd").Execute()
                Session("Con").Close()
                lbl_status_dom.Text = "Guardado correctamente"
                carga_domicilio()
            Else
                lbl_status_dom.Text = "No se ha agregado una institución"
            End If

        Else
            lbl_status_dom.Text = "Error: Excede el número de caracteres permitidos en referencias"
        End If


    End Sub

    Protected Sub btn_guardar_domicilio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_domicilio.Click
        GuardaDireccion()
        actualiza_avance(2)
    End Sub

    Private Sub GuardaTelefono(ByVal lada As String, ByVal tele As String, ByVal ext As String, ByVal tipo As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("IdInsti") Is Nothing Then
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
        End If
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 6, lada)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELE", Session("adVarChar"), Session("adParamInput"), 15, tele)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EXT", Session("adVarChar"), Session("adParamInput"), 5, ext)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODO", Session("adVarChar"), Session("adParamInput"), 11, IIf(Session("PERSONAID") Is Nothing, 1, 2))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PERSONAAUX_TELEFONO"
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_statustel.Text = "Guardado correctamente"
    End Sub

    Function guarda_info_contacto() As Boolean
        lbl_statustel.Text = ""
        Dim res As Boolean = True
        'REVISO SI SE CAPTURARON LOS DATOS  INDISPENSABLES PARA GUARDAR LOS TELEFONOS Y LOS GUARDO (7 digitos minimo tel  y 2 de lada)
        'PARTICULAR
        Dim casa As Integer, cel As Integer, ofi As Integer, rec As Integer
        casa = txt_tel1.Text.Length
        cel = txt_tel2.Text.Length
        ofi = txt_tel3.Text.Length
        rec = txt_tel4.Text.Length

        If casa > 0 Then
            If casa >= 7 And txt_lada1.Text.Length >= 2 Then
                GuardaTelefono(txt_lada1.Text, txt_tel1.Text, txt_ext1.Text, "PAR")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos."
                res = False
            End If
        End If


        'MOVIL
        If cel > 0 Then
            If cel >= 7 And txt_lada2.Text.Length >= 2 Then
                GuardaTelefono(txt_lada2.Text, txt_tel2.Text, txt_ext2.Text, "MOV")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos."
                res = False
            End If
        End If

        'TRABAJO
        If ofi > 0 Then
            If ofi >= 7 And txt_lada3.Text.Length >= 2 Then
                GuardaTelefono(txt_lada3.Text, txt_tel3.Text, txt_ext3.Text, "TRA")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos."
                res = False
            End If
        End If

        'RECADOS
        If rec > 0 Then
            If rec >= 7 And txt_lada4.Text.Length >= 2 Then
                GuardaTelefono(txt_lada4.Text, txt_tel4.Text, txt_ext4.Text, "REC")
            Else
                lbl_statustel.Text = "Error: Clave lada o teléfono incompletos."
                res = False
            End If
        End If
        Return res
    End Function

    Protected Sub btn_guardar_contacto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_c.Click


        If Session("IdInsti") > 0 Then
            If txt_tel1.Text.Length = 0 And txt_tel2.Text.Length = 0 And txt_tel3.Text.Length = 0 And txt_tel4.Text.Length = 0 Then
                lbl_statustel.Text = "Es necesario guardar al menos un teléfono"
            Else
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "DEL_PERSONAAUX_CONTACTO"
                Session("cmd").Execute()
                Session("Con").Close()
                If guarda_info_contacto() Then
                    actualiza_avance(3)
                    define_avance()
                End If

            End If
        Else
            lbl_statustel.Text = "No se ha agregado una institución"
        End If


    End Sub



    Protected Sub btn_guardar_oficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_oficina.Click
        Dim Estatus As Integer
        If ckb_Activo.Checked = False Then
            Estatus = 0
        Else
            Estatus = 1
        End If

        If Session("IdInsti") > 0 Then
            Session("Con") = CreateObject("ADODB.Connection")
            Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Session("Con").ConnectionTimeout = 240
            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 9, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDINSTITUCION", Session("adVarChar"), Session("adParamInput"), 9, Session("IdInsti"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ABREVIATURA", Session("adVarChar"), Session("adParamInput"), 10, txt_nombre_abreviatura.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre_ofi.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CALLENUM", Session("adVarChar"), Session("adParamInput"), 100, txt_calle_num.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 20, ddl_Estado.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIO", Session("adVarChar"), Session("adParamInput"), 20, ddl_municipio.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 20, ddl_asentamiento.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOOFI", Session("adVarChar"), Session("adParamInput"), 3, ddl_tipo.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 6, txt_lada.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 15, txt_telefono.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EXTENSION", Session("adVarChar"), Session("adParamInput"), 15, txt_extension.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, Estatus)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_OFICINA_INST"
            Session("rs") = Session("cmd").Execute()
            Session("ID_SUCURSAL") = Session("rs").fields("ID").value.ToString
            lbl_statusofi.Text = Session("rs").fields("MENSAJE").value.ToString

            Session("Con").Close()
            lbl_statusofi.Text = "Guardado correctamente"
            limpiaFormaOfi()
            LlenaOficinas()
        Else
            lbl_statusofi.Text = "No se ha agregado una institución"
        End If

    End Sub



    Protected Sub limpiaFormaOfi()
        ckb_Activo.Checked = False
        LlenaTipo()
        txt_nombre_ofi.Text = ""
        txt_nombre_abreviatura.Text = ""
        txt_calle_num.Text = ""
        txt_cp_ofi.Text = ""
        ddl_Estado.Items.Clear()
        ddl_municipio.Items.Clear()
        ddl_asentamiento.Items.Clear()
        txt_lada.Text = ""
        txt_telefono.Text = ""
        txt_extension.Text = ""

    End Sub



    ' Protected Sub btn_carga_layPlz_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_carga_layPlz.Click

    '    ' AsyncFileUpload2.Enabled = False
    '    ' btn_carga_layPlz.Enabled = False
    '   lbl_plaza_lay.Text = ""
    '   CargarArchPlzCSV()
    '' LlenaOficinas()

    '  End Sub

    ' Private Sub CargarArchPlzCSV()
    'If Session("IdInsti") > 0 Then
    '' that the FileUpload control contains a file.
    '  If (AsyncFileUpload2.HasFile) Then
    '' Get the name of the file to upload.
    '  ' Dim fileName As String = Server.HtmlEncode(AsyncFileUpload1.FileName)
    ' Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload2.FileName.ToString)

    'Dim name As String = AsyncFileUpload2.FileName.ToString
    '' Get the extension of the uploaded file.
    '  Dim extension As String = System.IO.Path.GetExtension(fileName)


    '     ' Call the SaveAs method to save                
    '     AsyncFileUpload2.SaveAs(fileName)





    'Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    'Dim PersonaGeneral As New Data.DataTable()
    '   PersonaGeneral.Columns.Add("IDRESPUESTA", GetType(Integer))
    '   PersonaGeneral.Columns.Add("RESPUESTA", GetType(String))
    '   PersonaGeneral.Columns.Add("OBLIGATORIO", GetType(Integer))
    '   PersonaGeneral.Columns.Add("ORDEN", GetType(String))
    '  PersonaGeneral.Columns.Add("NOMBRE", GetType(String))
    '  PersonaGeneral.Columns.Add("EXPRESION_REGULAR", GetType(String))

    'Dim dtPersonasEx As New Data.DataTable()
    '   dtPersonasEx.Columns.Add("TIPO", GetType(String))
    '   dtPersonasEx.Columns.Add("EVENTO", GetType(String))
    '  dtPersonasEx.Columns.Add("PLAZA", GetType(String))
    ' dtPersonasEx.Columns.Add("CUENTA", GetType(String))
    '  dtPersonasEx.Columns.Add("DESCRIPCION", GetType(String))
    '  dtPersonasEx.Columns.Add("FECHACREADO", GetType(String))





    '   Session("cmd") = New ADODB.Command()
    '   Session("Con").Open()
    '   Session("cmd").ActiveConnection = Session("Con")
    '   Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '   Session("parm") = Session("cmd").CreateParameter("NOMBRE_ARCHIVO", Session("adVarChar"), Session("adParamInput"), 1000, name)
    '   Session("cmd").Parameters.Append(Session("parm"))
    '   Session("parm") = Session("cmd").CreateParameter("EXTENSION", Session("adVarChar"), Session("adParamInput"), 15, extension)
    '  Session("cmd").Parameters.Append(Session("parm"))
    ' Session("parm") = Session("cmd").CreateParameter("RUTA_ARCHIVO", Session("adVarChar"), Session("adParamInput"), 1000, fileName)
    '   Session("cmd").Parameters.Append(Session("parm"))
    '  Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti"))
    ' Session("cmd").Parameters.Append(Session("parm"))
    'Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
    '  Session("cmd").Parameters.Append(Session("parm"))
    ' Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
    'Session("cmd").Parameters.Append(Session("parm"))
    '  Session("cmd").CommandText = "INS_PLAZA_CARGA_MULTIPLE"
    ' Session("rs") = Session("cmd").Execute()
    'custDA.Fill(dtPersonasEx, Session("rs"))
    ' 'custDA.Fill(PersonaGeneral, Session("rs"))


    ' Session("Con").Close()

    'Dim contador As Integer = 0


    ' If dtPersonasEx.Rows.Count > 0 Then
    '         dag_Plaza_Ex.Visible = True
    '         dag_Plaza_Ex.DataSource = dtPersonasEx
    '        dag_Plaza_Ex.DataBind()
    'Else
    '    dag_Plaza_Ex.Visible = False
    'End If

    '  System.IO.File.Delete(fileName)

    'Else
    '  ' NOTIFY THE USER THET A FILE WAS NOT UPLOADED
    '  lbl_plaza_lay.Text = "Error: Seleccione un archivo."
    ' dag_Plaza_Ex.Visible = False
    ' dag_Plaza_NoEx.Visible = False
    ''AsyncFileUpload2.Enabled = True
    ' 'btn_carga_layPlz.Enabled = True
    ' End If

    'Else
    '      lbl_plaza_lay.Text = "No se ha agregado una institución"
    'End If


    ' End Sub

    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)

        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "none")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #FFF")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion
    End Sub

    'Protected Sub btn_guarda_plz_Click(sender As Object, e As EventArgs)

    'If Session("IdInsti") > 0 Then
    '      Session("cmd") = New ADODB.Command()
    '      Session("Con").Open()
    '     Session("cmd").ActiveConnection = Session("Con")
    '     Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '     Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti").ToString)
    '     Session("cmd").Parameters.Append(Session("parm"))
    '     Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombrePls.Text)
    '     Session("cmd").Parameters.Append(Session("parm"))
    '     Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 20, txt_clavePls.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("NIVEL", Session("adVarChar"), Session("adParamInput"), 10, ddl_nivel.SelectedItem.Value)
    '     Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("TURNO", Session("adVarChar"), Session("adParamInput"), 10, ddl_turno.SelectedItem.Value)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 1)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
    '   Session("cmd").Parameters.Append(Session("parm"))
    '   Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
    '   Session("cmd").Parameters.Append(Session("parm"))
    '  Session("cmd").CommandText = "INS_PLAZA_INST"
    '  Session("rs") = Session("cmd").Execute()

    'If Session("rs").Fields("ID").value.ToString > 0 Then
    '      limpiaFormaPlz()
    '       lbl_plaza.Text = "Guardado correctamente"
    'Else
    '      lbl_plaza.Text = "Error: Ya existe una plaza con ese nombre y/o clave."
    'End If

    '   Session("Con").Close()

    '     llena_Plazas()
    'Else
    '    lbl_plaza.Text = "No se ha agregado una institución"
    '    End If


    '  End Sub

    ' Private Sub limpiaFormaPlz()
    ' ckb_plaza.Checked = False
    ' txt_nombrePls.Text = ""
    ' txt_clavePls.Text = ""

    ' ddl_nivel.Items.Clear()
    'Dim elijaNivel As New ListItem("ELIJA", "-1")
    '  ddl_nivel.Items.Add(elijaNivel)

    'Dim elijaNivel1 As New ListItem("1", "1")
    '  ddl_nivel.Items.Add(elijaNivel1)

    ' Dim elijaNivel2 As New ListItem("2", "2")
    '  ddl_nivel.Items.Add(elijaNivel2)

    'Dim elijaNivel3 As New ListItem("3", "3")
    '  ddl_nivel.Items.Add(elijaNivel3)

    'Dim elijaNivel4 As New ListItem("4", "4")
    '  ddl_nivel.Items.Add(elijaNivel4)

    ' elijaNivel5 As New ListItem("5", "5")
    ' ddl_nivel.Items.Add(elijaNivel5)

    'Dim elijaNivel6 As New ListItem("6", "6")
    ' ddl_nivel.Items.Add(elijaNivel6)




    ' ddl_turno.Items.Clear()
    'Dim elijaTurno As New ListItem("ELIJA", "-1")
    '  ddl_turno.Items.Add(elijaTurno)

    'Dim elijaTurno1 As New ListItem("DIURNO", "1")
    '  ddl_turno.Items.Add(elijaTurno1)

    'Dim elijaTurno2 As New ListItem("NOCTURNO", "2")
    '   ddl_turno.Items.Add(elijaTurno2)

    'Dim elijaTurno3 As New ListItem("MIXTO", "3")
    '   ddl_turno.Items.Add(elijaTurno3)


    ' End Sub


    'Private Sub llena_Plazas()
    ' Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    'Dim dtper As New Data.DataTable()
    '    Session("Con").Open()
    '   Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 10, Session("IdInsti").ToString)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "SEL_PLAZASINSTI"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dtper, Session("rs"))
    '    Session("Con").Close()
    'If dtper.Rows.Count > 0 Then
    '       dag_plazas.Visible = True
    '        dag_plazas.DataSource = dtper
    '       dag_plazas.DataBind()
    'Else
    '       dag_plazas.Visible = False
    'End If

    ' End Sub


End Class