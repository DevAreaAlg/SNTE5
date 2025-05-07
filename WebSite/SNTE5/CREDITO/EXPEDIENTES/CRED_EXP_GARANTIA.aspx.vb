Public Class CRED_EXP_GARANTIA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Detalle de Garantías", "Garantías")
        If Not Me.IsPostBack Then
            Session("CLAVEGTIA") = Session("CVEGARANTIA")
            Session("TIPOGTIA") = Session("TIPOGARANTIA")
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("PROSPECTO")

            muestragarantias()

        End If

    End Sub


    Private Sub LlenaEstados()
        cmb_Estado.Items.Clear()
        cmb_estado_notaria.Items.Clear()
        cmb_estado_registro.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        Dim elija1 As New ListItem("ELIJA", "-1")
        Dim elija2 As New ListItem("ELIJA", "-1")

        cmb_Estado.Items.Add(elija)
        cmb_estado_notaria.Items.Add(elija1)
        cmb_estado_registro.Items.Add(elija2)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EDO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString)
            cmb_Estado.Items.Add(item)
            cmb_estado_notaria.Items.Add(item)
            cmb_estado_registro.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub cmb_edo_civil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_edo_civil.SelectedIndexChanged
        If cmb_edo_civil.SelectedValue = "SOL" Or cmb_edo_civil.SelectedValue = "DIV" Or cmb_edo_civil.SelectedValue = "VIU" Then
            txt_conyuge.Text = ""
            txt_conyuge.Enabled = False
            RequiredFieldValidator_conyuge.Enabled = False
        Else
            txt_conyuge.Enabled = True
            RequiredFieldValidator_conyuge.Enabled = True
        End If
    End Sub


    Protected Sub cmb_Estado_registro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_estado_registro.SelectedIndexChanged

        Llenamunicipio_registro(cmb_estado_registro.SelectedItem.Value.ToString)
    End Sub

    'En base al estado seleccionado se muestra el municipio
    Protected Sub cmb_Estado_notaria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_estado_notaria.SelectedIndexChanged

        Llenamunicipionotaria(cmb_estado_notaria.SelectedItem.Value.ToString)
    End Sub

    Private Sub Llenatiporelacion()

        cmb_relacion.Items.Clear()


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_relacion.Items.Add(elija)


        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_relacion.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub


    'Llena los municipios
    Private Sub Llenamunicipio_registro(ByVal estado As Integer)


        cmb_municipio_registro.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 10, estado)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MUNICIPIOS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Dim item_mun As New ListItem(Session("rs").Fields("MUNICIPIO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_municipio_registro.Items.Add(item_mun)

            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub


    Private Sub Llenamunicipionotaria(ByVal estado As String)

        cmb_municipio_notaria.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 10, estado)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MUNICIPIOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item_mun As New ListItem(Session("rs").Fields("MUNICIPIO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_municipio_notaria.Items.Add(item_mun)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub


    Private Sub LlenaASENTAMIENTOS(ByVal municipio As String)

        cmb_municipio_notaria.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDMUN", Session("adVarChar"), Session("adParamInput"), 10, municipio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ASENTAMIENTOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item_mun As New ListItem(Session("rs").Fields("ASENTAMIENTO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_asentamiento.Items.Add(item_mun)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub



    Private Sub Llenamunicipiinmueble(ByVal estado As String)

        cmb_municipio_notaria.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 10, estado)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MUNICIPIOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item_mun As New ListItem(Session("rs").Fields("MUNICIPIO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_municipio.Items.Add(item_mun)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub


    'Muestra las garantias en el DB GRID
    Private Sub muestragarantias()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtgarantias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_GARANTIAS_ASIGNADAS"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtgarantias, Session("rs"))
        DAG_garantias.DataSource = dtgarantias
        DAG_garantias.DataBind()
        Session("Con").Close()
    End Sub




    Private Sub DocumentosDigitalizados()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVEGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTIPO", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_GARANTIAS_DOCS_DIGIT"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").eof


            If Session("rs").Fields("DOCUMENTO").value.ToString = "FOTO" Then
                lnk_foto.Enabled = True

            End If
            Session("rs").movenext()
        Loop
        Session("Con").close()

    End Sub

    Private Sub DAG_garantias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_garantias.ItemCommand

        'Se habilita el acordion de detalle
        If (e.CommandName = "DETALLE") Then
            'Accordion1.SelectedIndex = 1
            'AccordionPane2.Enabled = True
            pnl_detalle.Visible = True
            '--OBTIENE LA CVE Y EL TIPO DE GARANTIA QUE HAY EN EL DB GRID
            Session("CVEGARANTIA") = (e.Item.Cells(1).Text)
            Session("TIPOGARANTIA") = (e.Item.Cells(0).Text)
            Session("IDGARANTIA") = (e.Item.Cells(2).Text)
            CARGA_ESTADOSMUN()
            Llenamunicipio_registro(CInt(Session("IDESTADO_REG")))
            Llenamunicipiinmueble(CInt(Session("IDESTADO_INMUEBLE")))
            LlenaASENTAMIENTOS(CInt(Session("IDMUN_INMUEBLE")))
            Llenamunicipionotaria(CInt(Session("IDESTADO_NOTARIA")))
            LlenaEstados()
            Llenatiporelacion()
            Detallegarantia()

            If cmb_edo_civil.SelectedValue = "SOL" Or cmb_edo_civil.SelectedValue = "DIV" Or cmb_edo_civil.SelectedValue = "VIU" Then

                txt_conyuge.Enabled = False
                RequiredFieldValidator_conyuge.Enabled = False
            Else
                txt_conyuge.Enabled = True
                RequiredFieldValidator_conyuge.Enabled = True
            End If

        End If
    End Sub


    Protected Sub txt_cp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp.TextChanged
        cmb_Estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
    End Sub
    Private Sub cp_hip(ByVal cp As String)

        cmb_Estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()

        'CP de garantia prendaria
        If cp = "" Then
            Exit Sub
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, cp)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP

            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString)
            cmb_Estado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
            cmb_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)
                cmb_asentamiento.Items.Add(item)
                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()
    End Sub

    'Cp hipotecaria
    Protected Sub btn_cp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cp.Click
        cp_hip(txt_cp.Text)
        cmb_asentamiento.Enabled = True
        cmb_Estado.Enabled = True
        cmb_municipio.Enabled = True
    End Sub


    Private Sub CARGA_ESTADOSMUN()

        Dim est_inmueble As Integer
        Dim est_registro As Integer
        Dim est_notaria As Integer
        Dim CP As Integer

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVEGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTIPO", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_GARANTIA"
        Session("rs") = Session("cmd").Execute()
        est_inmueble = Session("rs").Fields("ESTADO_INMUEBLE").Value.ToString
        est_registro = Session("rs").Fields("ESTADO_REGISTRO").Value.ToString
        est_notaria = Session("rs").Fields("ESTADO_NOTARIA").Value.ToString
        CP = Session("rs").Fields("CP").value.ToString
        Session("IDMUN_INMUEBLE") = CP
        Session("IDESTADO_REG") = est_registro
        Session("IDESTADO_NOTARIA") = est_notaria
        Session("IDESTADO_INMUEBLE") = est_inmueble
        Session("Con").Close()
    End Sub
    Private Sub Detallegarantia()

        If Session("TIPOGARANTIA").ToString = "2" Or Session("TIPOGARANTIA").ToString = "3" Then
            DocumentosDigitalizados()
        End If


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVEGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTIPO", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_GARANTIA"
        Session("rs") = Session("cmd").Execute()

        Select Case Session("TIPOGARANTIA").ToString

            Case "1" 'Sin garantia


            Case "2" 'Hipotecaria

                Dim clave_catastral = Session("rs").Fields("CLAVE_CATASTRAL").Value.ToString
                Dim Folio_real = Session("rs").Fields("FOLIO_REAL").Value.ToString
                Dim CP = Session("rs").Fields("CP").Value.ToString
                Dim Calle_numero = Session("rs").Fields("DIRECCION").Value.ToString
                Dim Inmueble = Session("rs").Fields("INMUEBLE").Value.ToString
                Dim Valor_garantia = Session("rs").Fields("VALOR").Value.ToString
                Dim Referencia = Session("rs").Fields("REFERENCIA").Value.ToString
                Dim Nombre_propietario = Session("rs").Fields("NOMBRE_PROPIETARIO").Value.ToString
                Dim RFC = Session("rs").Fields("RFC").Value.ToString
                Dim FNAC = Session("rs").Fields("FNAC").Value.ToString
                Dim Tel = Session("rs").Fields("TELEFONO").Value.ToString
                Dim EDO_CIIVL = Session("rs").Fields("EDO_CIVIL").Value.ToString
                Dim conyuge = Session("rs").Fields("CONYUGE").Value.ToString
                Dim Descripcion = Session("rs").Fields("Descripcion").Value.ToString
                Dim RPPC = Session("rs").Fields("RPPC").Value.ToString
                Dim FECHA_ESCRITURA = Session("rs").Fields("FECHA_ESCRITURA").Value.ToString
                Dim ESTADO_REGISTRO = Session("rs").Fields("ESTADO_REGISTRO").Value.ToString
                Dim M2_Terreno = Session("rs").Fields("M2_Terreno").Value.ToString
                Dim M2_CONSTRUIDO = Session("rs").Fields("M2_CONSTRUIDO").Value.ToString
                Dim Antiguedad = Session("rs").Fields("ANTIGUEDAD").Value.ToString
                Dim Gravamen = Session("rs").Fields("GRAVAMEN").Value.ToString
                Dim Inst_Gravamen = Session("rs").Fields("INST_GRAVAMEN").Value.ToString
                Dim AFORO = Session("rs").Fields("AFORO").Value.ToString
                Dim AVALUO = Session("rs").Fields("AVALUO").Value.ToString
                Dim FECHA_AVALUO = Session("rs").Fields("FECHA_AVALUO").Value.ToString
                Dim VALOR_AVALUO = Session("rs").Fields("VALOR_AVALUO").Value.ToString
                Dim ID_ESTADO_NOTARIA = Session("rs").Fields("ESTADO_NOTARIA").Value.ToString
                Dim ESTADO_INMUEBLE = Session("rs").Fields("ESTADO_INMUEBLE").Value.ToString
                Dim ID_RELACION = Session("rs").Fields("ID_RELACION").Value.ToString
                Dim NUM_NOTARIO = Session("rs").Fields("NUM_NOTARIO").Value.ToString
                Dim NOMBRE_NOTARIO = Session("rs").Fields("NOMBRE_NOTARIO").Value.ToString
                Dim MUN_INMUEBLE = Session("rs").Fields("MUN_INMUEBLE").Value.ToString
                Dim MUN_REGISTRO = Session("rs").Fields("MUN_REGISTRO").Value.ToString
                Dim MUN_NOTARIA = Session("rs").Fields("MUN_NOTARIA").Value.ToString
                Dim ASENTAMIENTO = Session("rs").Fields("ASENTAMIENTO").Value.ToString

                If Session("rs").Fields("SEGURO").ToString = "SI" Then
                Else
                    If Not Session("rs").eof Then

                        cmb_Estado.SelectedValue = ESTADO_INMUEBLE

                        txt_clave.Text = clave_catastral.ToString
                        txt_rppc.Text = Folio_real.ToString
                        txt_cp.Text = CP.ToString
                        txt_calle_inm.Text = Calle_numero.ToString
                        txt_inm.Text = Inmueble.ToString
                        txt_monto_hip.Text = Valor_garantia.ToString
                        txt_referencias_inm.Text = Referencia.ToString
                        txt_propietario.Text = Nombre_propietario.ToString
                        txt_rfc.Text = RFC.ToString
                        If Session("rs").FIELDS("SEXO").Value.ToString = "H" Then
                            cmb_sexo.SelectedValue = "H"
                        ElseIf Session("rs").FIELDS("SEXO").Value.ToString = "M" Then
                            cmb_sexo.SelectedValue = "H"
                        Else
                            cmb_sexo.SelectedValue = "-1"
                        End If
                        txt_fechanac.Text = FNAC.ToString
                        txt_telefono.Text = Tel.ToString

                        If Session("rs").FIELDS("EDO_CIVIL").Value.ToString = "SOL" Then
                            cmb_edo_civil.SelectedValue = "SOL"
                        ElseIf Session("rs").FIELDS("EDO_CIVIL").Value.ToString = "CAS" Then
                            cmb_edo_civil.SelectedValue = "CAS"
                        ElseIf Session("rs").FIELDS("EDO_CIVIL").Value.ToString = "UNL" Then
                            cmb_edo_civil.SelectedValue = "UNL"
                        ElseIf Session("rs").FIELDS("EDO_CIVIL").Value.ToString = "VIU" Then
                            cmb_edo_civil.SelectedValue = "VIU"
                        ElseIf Session("rs").FIELDS("EDO_CIVIL").Value.ToString = "DIV" Then
                            cmb_edo_civil.SelectedValue = "DIV"
                        Else
                            cmb_edo_civil.SelectedValue = "-1"
                        End If
                        txt_conyuge.Text = conyuge.ToString
                        txt_descripcion_gtia.Text = Descripcion.ToString
                        txt_folioes.Text = RPPC.ToString
                        txt_fechaes.Text = FECHA_ESCRITURA.ToString
                        cmb_estado_registro.SelectedValue = ESTADO_REGISTRO.ToString

                        txt_m2terreno.Text = M2_Terreno.ToString
                        txt_m2construido.Text = M2_CONSTRUIDO.ToString
                        txt_antiguedad.Text = Antiguedad.ToString
                        txt_gravamen.Text = Gravamen.ToString
                        txt_institucion_Gravamen.Text = Inst_Gravamen.ToString
                        txt_aforo.Text = AFORO.ToString
                        txt_avaluo.Text = AVALUO.ToString
                        txt_fecha_avaluo.Text = FECHA_AVALUO.ToString
                        txt_valor_avaluo.Text = VALOR_AVALUO.ToString
                        cmb_estado_notaria.SelectedValue = ID_ESTADO_NOTARIA.ToString
                        cmb_relacion.SelectedValue = ID_RELACION.ToString
                        txt_num_notario.Text = NUM_NOTARIO.ToString
                        txt_nombre_notario.Text = NOMBRE_NOTARIO.ToString
                        cmb_municipio.SelectedValue = MUN_INMUEBLE.ToString
                        cmb_municipio_registro.SelectedValue = MUN_REGISTRO.ToString
                        cmb_municipio_notaria.SelectedValue = MUN_NOTARIA.ToString
                        cmb_asentamiento.SelectedValue = ASENTAMIENTO.ToString


                    End If
                End If

        End Select

        Session("Con").Close()
    End Sub

    Private Sub Actualiza_garantias()

        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 300, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CLAVE_CAT", Session("adVarChar"), Session("adParamInput"), 25, txt_clave.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO_REAL", Session("adVarChar"), Session("adParamInput"), 25, txt_rppc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 5, txt_cp.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTADO_INM", Session("adVarChar"), Session("adParamInput"), 10, cmb_Estado.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MUNICIPIO_INM", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ASENTAMIENTO_INM", Session("adVarChar"), Session("adParamInput"), 10, cmb_asentamiento.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CALLE_NUM", Session("adVarChar"), Session("adParamInput"), 100, txt_calle_inm.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INMUEBLE", Session("adVarChar"), Session("adParamInput"), 200, txt_inm.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VALOR_GARANTIA", Session("adVarChar"), Session("adParamInput"), 50, txt_monto_hip.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 1000, txt_referencias_inm.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 300, txt_propietario.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO_REL", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 10, cmb_sexo.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHA_NAC_PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_fechanac.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC_PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TEL_PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_telefono.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EST_CIVIL_PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_edo_civil.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CONYUYGE_PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 150, txt_conyuge.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DESCRIPCION_GARANTIA", Session("adVarChar"), Session("adParamInput"), 1000, txt_descripcion_gtia.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NUM_ESCRITURA", Session("adVarChar"), Session("adParamInput"), 50, txt_folioes.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHA_ESCRITURA", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaes.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTADO_REGISTRO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado_registro.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MUNICIPIO_REGISTRO", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio_registro.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NUM_NOTARIO", Session("adVarChar"), Session("adParamInput"), 50, txt_num_notario.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE_NOTARIO", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre_notario.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTADO_NOTARIA", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado_notaria.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MUNICIPIO_NOTARIA", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio_notaria.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("M2_TERRENO", Session("adVarChar"), Session("adParamInput"), 50, txt_m2terreno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("M2_CONSTRUIDO", Session("adVarChar"), Session("adParamInput"), 50, txt_m2construido.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ANTIGUEDAD", Session("adVarChar"), Session("adParamInput"), 50, txt_antiguedad.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("GRAVAMEN", Session("adVarChar"), Session("adParamInput"), 250, txt_gravamen.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("INST_GRAVAMEN", Session("adVarChar"), Session("adParamInput"), 100, txt_institucion_Gravamen.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("AFORO", Session("adVarChar"), Session("adParamInput"), 50, txt_aforo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("AVALUO", Session("adVarChar"), Session("adParamInput"), 100, txt_avaluo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHA_AVALUO", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_avaluo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VALOR_AVALUO", Session("adVarChar"), Session("adParamInput"), 50, txt_valor_avaluo.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_DETALLES_GARANTIA"
            Session("rs") = Session("cmd").Execute()
        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally
            Session("Con").close()
        End Try
    End Sub

    Protected Sub btn_guardar_hip_Click(sender As Object, e As EventArgs)
        Try
            Actualiza_garantias()
            Detallegarantia()
            lbl_status.Text = "Información guardada correctamente"

        Catch ex As Exception
            lbl_status.Text = ex.ToString
        End Try
    End Sub

    Protected Sub DAG_garantias_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_garantias.ItemDataBound

        e.Item.Cells(0).Visible = False ' Se pone invisible la columna tipo de garantia
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False
    End Sub

    Protected Sub lnk_foto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_foto.Click
        Session("NOMBRE_DOCUMENTO") = "FOTO"
        Response.Redirect("../../DIGITALIZADOR/DIGI_MOSTRAR_GARANTIAS.aspx")
    End Sub


    Protected Sub btn_editar_foto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_editar_foto.Click
        Session("VENGODE") = "CRED_EXP_GARANTIA"
        Response.Redirect("../../DIGITALIZADOR/DIGI_GLOBAL.aspx")
    End Sub


End Class