Public Class CORE_CNF_OFICINAS_CREAR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Edición de Oficinas", "Edición Oficinas")

        If Not Me.IsPostBack Then
            If Session("ID_SUCURSAL") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                LlenaTipo()
                LlenaJefe()

                If Session("ID_SUCURSAL") > 0 Then
                    MostrarOficina()
                Else
                    txt_id_oficina.Text = "Nueva Oficina"
                End If
            End If
        End If
    End Sub

    Private Sub LlenaTipo()
        ddl_tipo.Items.Clear()
        ddl_tipo.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPOS_OFI_SUC"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_tipo.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID_TIPO").Value.ToString))
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub



    Private Sub LlenaJefe()
        ddl_jefe.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_jefe.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUJEFE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_jefe.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub click_btn_limpiar()
        txt_id_oficina.Text = ""
        txt_nombre_abreviatura.Text = ""
        ddl_jefe.SelectedValue = -1
        txt_nombre.Text = ""
        txt_calle_num.Text = ""
        ddl_Estado.Items.Clear()
        ddl_municipio.Items.Clear()
        ddl_asentamiento.Items.Clear()

        txt_cp.Text = ""
        ddl_tipo.SelectedValue = 0
        txt_lada.Text = ""
        txt_telefono.Text = ""

        ckb_Activo.Checked = False
        Session("ID_SUCURSAL") = "0"
    End Sub

    Protected Sub click_btn_guardar_datos(ByVal sender As Object, ByVal e As EventArgs)

        Dim Estatus As Integer
        Dim inicioope As String
        If ckb_Activo.Checked = False Then
            Estatus = 0
        Else
            Estatus = 1
        End If

        inicioope = ""

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 9, Session("ID_SUCURSAL"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ABREVIATURA", Session("adVarChar"), Session("adParamInput"), 10, txt_nombre_abreviatura.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDJEFESUC", Session("adVarChar"), Session("adParamInput"), 20, ddl_jefe.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
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
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, Estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_OFICINA"
        Session("rs") = Session("cmd").Execute()
        Session("ID_SUCURSAL") = Session("rs").fields("ID").value.ToString
        txt_id_oficina.Text = Session("ID_SUCURSAL")
        Session("Con").Close()
        MostrarOficina()

        lbl_statusofi.Text = "Guardado correctamente"
    End Sub

    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        busquedaCP(txt_cp.Text)

    End Sub

    Private Sub busquedaCP(ByVal CP As String)
        ddl_Estado.Items.Clear()
        ddl_municipio.Items.Clear()
        ddl_asentamiento.Items.Clear()
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

    Private Sub MostrarOficina()
        Dim ESTADO As String
        Dim MUNICIPIO As String
        Dim ASENTAMIENTO As String
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 50, Session("ID_SUCURSAL"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SUCURSAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            txt_id_oficina.Text = Session("ID_SUCURSAL")
            txt_nombre_abreviatura.Text = Session("rs").Fields("ABREVIATURA").value.ToString
            ddl_jefe.SelectedValue = Session("rs").Fields("JEFE").value.ToString
            txt_nombre.Text = Session("rs").Fields("NOMBRE").value.ToString
            txt_calle_num.Text = Session("rs").Fields("CALLENUM").value.ToString
            ESTADO = Session("rs").Fields("IDESTADO").value.ToString
            MUNICIPIO = Session("rs").Fields("IDMUNICIPIO").value.ToString
            ASENTAMIENTO = Session("rs").Fields("IDASENTAMIENTO").value.ToString

            txt_cp.Text = Session("rs").Fields("CP").value.ToString
            ddl_tipo.SelectedValue = Session("rs").Fields("TIPO").value.ToString
            txt_lada.Text = Session("rs").Fields("LADA").value.ToString
            txt_telefono.Text = Session("rs").Fields("TELEFONO").value.ToString


            If Session("rs").Fields("ESTATUS").value.ToString = "1" Then
                ckb_Activo.Checked = True
            ElseIf Session("rs").Fields("ESTATUS").value.ToString = "0" Then
                ckb_Activo.Checked = False
            End If

        End If

        Session("Con").Close()
        busquedaCP(txt_cp.Text)
        ddl_Estado.SelectedValue = ESTADO
        ddl_municipio.SelectedValue = MUNICIPIO
        ddl_asentamiento.SelectedValue = ASENTAMIENTO

    End Sub

End Class