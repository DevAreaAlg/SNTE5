Public Class CRED_EXP_APARTADO5
    Inherits System.Web.UI.Page

    Dim ACUMULADOR As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Garantías", "Garantías")

        If Not Me.IsPostBack Then
            If Session("FOLIO") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If

            'Datos Generales de Expediente: Folio, Nombre de Cliente y Producto
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))

            llenagarantias()
            Llenatiporelacion()
            LlenaEstados()
            muestragarantias()
            Muestratipoprendaria()
            expbloqueado()

            Session("VENGODE") = "CRED_EXP_APARTADO5.aspx"
        End If
        If Session("DINERO") <> Nothing Then
            CalculaValores() ' se muestra cuanto porcentaje de cobertura global tiene ese folio
            lbl_dinero.Text = "Monto total a cubrir: $" + Session("MascoreG").FormatNumberCurr(Session("DINERO").ToString)

        End If
    End Sub

    Private Sub monto_Cred(ByVal Folio As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_MONTOS"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'Se crean variables de sesion de monto de credito
            lbl_monto_cred.Text = "Monto del préstamo: " + "$" + Session("MascoreG").FormatNumberCurr(Session("rs").Fields("CREDITO").value.ToString)
            Session("MONTOCREDITO") = Session("rs").Fields("CREDITO").value.ToString

        End If
        Session("Con").Close()
    End Sub

    Private Sub llenagarantias()

        monto_Cred(Session("FOLIO").ToString) 'Obtengo las variables de sesion de monto credito y monto seguro

        If CDec(Session("MONTOCREDITO").ToString) = 0.0 Then
            lbl_status_general.Text = "Es necesario capturar el apartado 1"
        Else
            cmb_tipo_garantias.Items.Clear()
            Dim elija As New ListItem("ELIJA", "-1")
            cmb_tipo_garantias.Items.Add(elija)

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_GARANTIAS_ASIGNADAS"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF

                Dim item As New ListItem("TIPO:  " + Session("rs").Fields("TIPO").Value.ToString + "(" + Session("RS").Fields("DESCRIPCION").VALUE.ToString + ")", (Session("rs").Fields("IDG").Value.ToString) + "-" + (Session("rs").Fields("IDT").Value.ToString))
                If Session("rs").Fields("GRANTED").Value.ToString = "1" Then
                    If (Session("rs").Fields("IDG").Value.ToString) <> "1" Then 'Si tiene asignada sin garantia no se muestra en el combo
                        cmb_tipo_garantias.Items.Add(item)
                    End If
                End If

                Session("rs").movenext()

            Loop

            Session("Con").Close()

        End If


    End Sub

    Private Sub montocubrir()

        'monto_Cred(Session("FOLIO").ToString) 'Obtengo las variables de sesion de monto credito y monto seguro

        Dim x = Split(cmb_tipo_garantias.SelectedItem.Value.ToString, "-")
        Session("IDGARANTIA") = x(0)

        If CDec(Session("MONTOCREDITO").ToString) = 0.0 Then
            lbl_status_general.Text = "Es necesario capturar el apartado 1"
        Else

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDGARANTIA"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_GARANTIAS_MONTO"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF

                Session("DINERO") = Session("rs").Fields("DINERO").Value.ToString

                Session("rs").movenext()

            Loop

            Session("Con").Close()
            'lbl_dinero.Text = cmb_tipo_garantias.SelectedItem.Value.ToString
            lbl_dinero.Text = "Monto total a cubrir: $" + Session("MascoreG").FormatNumberCurr(Session("DINERO").ToString)

        End If


    End Sub

    'Se suman los valores que tiene el data grid en la columna de valor 
    Private Sub CalculaValores()

        For Each DataGriditem In dag_garantias.Items
            ACUMULADOR = ACUMULADOR + CDec(DataGriditem.cells(4).Text)
        Next


        If CDec(Session("DINERO").ToString) = 0.0 Then
            lbl_status_general.Text = "(No necesita ningún tipo de garantía para cubrir el préstamo)"
        Else
            'If ACUMULADOR <> 0 Then
            '    Dim Porcentaje As Decimal
            '    Porcentaje = Left((ACUMULADOR * 100) / CDec(Session("DINERO").ToString), 6)
            '    lbl_contador.Text = "Porcentaje de cobertura: " + CStr(Porcentaje) + "%"
            'Else
            '    lbl_contador.Text = ""
            'End If

            'Ya se puede prender el semaforo
            If ACUMULADOR >= CDec(Session("DINERO").ToString) Then
                lbl_status_general.Text = "(Ya cubre el total de garantías para el préstamo)"
            Else
                lbl_status_general.Text = ""
            End If

        End If

    End Sub

    'Se eliminan los valores que tiene el data grid en la columna de valor 
    Private Sub EliminaValores(ByVal valor As String)


        ACUMULADOR = ACUMULADOR - CInt(valor)
        'If ACUMULADOR <> 0 Then
        '    Dim Porcentaje As Decimal
        '    Porcentaje = Left((ACUMULADOR * 100) / CDec(Session("DINERO").ToString), 6)
        '    lbl_contador.Text = "Porcentaje de cobertura: " + CStr(Porcentaje) + "%"

        'Else
        '    lbl_contador.Text = ""
        'End If

        'Ya se puede prender el semaforo
        If ACUMULADOR >= CDec(Session("DINERO").ToString) Then
            lbl_status_general.Text = "(Ya cubre el total de garantías para el préstamo)"
        Else
            lbl_status_general.Text = ""
        End If

    End Sub

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
                cmb_tipo_garantias.Enabled = False
            End If

        End If
        Session("Con").Close()

    End Sub

    Protected Sub cmb_tipo_garantias_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipo_garantias.SelectedIndexChanged

        lbl_status.Text = ""
        lbl_status_pren.Text = ""
        lbl_status_general.Text = ""

        montocubrir()

        Dim x
        'se se guarda el id de tipo de garantia en una variable
        x = Split(cmb_tipo_garantias.SelectedItem.Value.ToString, "-")


        Select Case x(1)

            Case "2" 'hipotecaria
                limpiahipotecaria()
                pnl_gtia_pren.Visible = False
                pnl_gtia_hip.Visible = True

            Case "3" 'prendaria
                Limpiaprendaria()
                pnl_gtia_pren.Visible = True
                pnl_gtia_hip.Visible = False
            Case Else
                pnl_gtia_pren.Visible = False
                pnl_gtia_hip.Visible = False
                limpiahipotecaria()
                Limpiaprendaria()

        End Select

    End Sub

    Private Sub Llenatiporelacion()

        cmb_relacion.Items.Clear()
        cmb_tipo_relacion_pren.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_relacion.Items.Add(elija)
        cmb_tipo_relacion_pren.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_relacion.Items.Add(item)
            cmb_tipo_relacion_pren.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    'Muestra las garantias en el DB GRID
    Private Sub muestragarantias()

        lbl_status_general.Text = ""

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtgarantias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_GARANTIAS_AGREGADAS"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtgarantias, Session("rs"))
        dag_garantias.DataSource = dtgarantias
        dag_garantias.DataBind()
        Session("Con").Close()

    End Sub


    'Llena los estados
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

    'Llena los municipios
    Private Sub Llenamunicipio_registro(ByVal estado As String)

        cmb_municipio_registro.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 10, estado)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MUNICIPIO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
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
        Session("cmd").CommandText = "SEL_MUNICIPIO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
            cmb_municipio_notaria.Items.Add(item_mun)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    'En base al estado seleccionado se muestra el municipio
    Protected Sub cmb_Estado_registro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_estado_registro.SelectedIndexChanged

        Llenamunicipio_registro(cmb_estado_registro.SelectedItem.Value.ToString)
    End Sub

    'En base al estado seleccionado se muestra el municipio
    Protected Sub cmb_Estado_notaria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_estado_notaria.SelectedIndexChanged

        Llenamunicipionotaria(cmb_estado_notaria.SelectedItem.Value.ToString)
    End Sub

    'obtiene el id de garantia
    Private Sub idgarantia()

        Dim x
        'se se guarda el id de tipo de garantia en una variable
        x = Split(cmb_tipo_garantias.SelectedItem.Value.ToString, "-")
        Session("IDGARANTIA") = x(0)


    End Sub

    Private Function validacion_tipo_garantia(ByVal folio As String, ByVal idgarantia As String) As Boolean

        Dim respuesta As Boolean = True

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = " SEL_CNFEXP_GARANTIAS_TIPO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDGARANT", Session("adVarChar"), Session("adParamInput"), 20, idgarantia)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").fields("OPCION").value.ToString = "0" Then
                respuesta = False
            Else
                respuesta = True
            End If
        End If


        Session("Con").Close()

        Return respuesta

    End Function


    Protected Sub cmb_edo_civil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_edo_civil.SelectedIndexChanged
        If cmb_edo_civil.SelectedValue = "CAS" Or cmb_edo_civil.SelectedValue = "UNL" Then
            txt_conyuge.Enabled = True
            txt_conyuge.Text = ""
            RequiredFieldValidator_conyuge.Visible = True

        Else
            txt_conyuge.Enabled = False
            txt_conyuge.Text = ""
            RequiredFieldValidator_conyuge.Visible = False
        End If


    End Sub

    Protected Sub lnk_garantia_hip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_garantia_hip.Click

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_VALIDACION_GARANTIAS_HIP"
        Session("parm") = Session("cmd").CreateParameter("RPPC", Session("adVarChar"), Session("adParamInput"), 100, txt_rppc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 100, txt_clave.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTADO_INMUEBLE", Session("adVarChar"), Session("adParamInput"), 100, cmb_Estado.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MUNICIPIO_INMUEBLE", Session("adVarChar"), Session("adParamInput"), 100, cmb_municipio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("RESPUESTA").VALUE.ToString = "EXISTE" Then

            'If Session("FACULTAD_CAMBINF") = "1" Then


            txt_monto_hip.Enabled = True
            txt_folioes.Enabled = True
            cmb_Estado.Enabled = True
            cmb_municipio.Enabled = True
            txt_fechaes.Enabled = True
            txt_num_notario.Enabled = True
            txt_nombre_notario.Enabled = True
            cmb_municipio_notaria.Enabled = True
            cmb_estado_notaria.Enabled = True
            txt_calle_inm.Enabled = True
            txt_referencias_inm.Enabled = True
            txt_avaluo.Enabled = True


            'Else

            '    txt_monto_hip.Enabled = False
            '    txt_folioes.Enabled = False
            '    cmb_Estado.Enabled = False
            '    cmb_municipio.Enabled = False
            '    txt_fechaes.Enabled = False
            '    txt_avaluo.Enabled = False
            '    txt_num_notario.Enabled = False
            '    txt_nombre_notario.Enabled = False
            '    cmb_municipio_notaria.Enabled = False
            '    cmb_estado_notaria.Enabled = False
            '    txt_calle_inm.Enabled = False
            '    txt_referencias_inm.Enabled = False
            'End If


            txt_monto_hip.Text = Session("rs").fields("VALOR").VALUE.ToString
            txt_folioes.Text = Session("rs").fields("FOLIOESCRITURA").VALUE.ToString
            cmb_estado_registro.Items.RemoveAt(0)
            cmb_estado_registro.Items.FindByValue(Session("rs").Fields("ESTADO_REGISTRO").Value.ToString).Selected = True
            txt_avaluo.Text = Session("rs").fields("FECHAAVALUO").VALUE.ToString
            txt_fechaes.Text = Session("rs").fields("FECHAESCRITURA").VALUE.ToString
            txt_num_notario.Text = Session("rs").fields("NUMNOTARIO").VALUE.ToString
            txt_nombre_notario.Text = Session("rs").fields("NOMBRE").VALUE.ToString
            cmb_estado_notaria.Text = Session("rs").fields("ESTADONOTARIO").VALUE.ToString
            txt_calle_inm.Text = Session("rs").fields("CALLEINM").VALUE.ToString
            txt_referencias_inm.Text = Session("rs").fields("REFERENCIA").VALUE.ToString
            txt_descripcion_gtia.Text = Session("rs").fields("DESCRIPCION").VALUE.ToString

            Session("MONTOORIGINAL") = Session("rs").fields("VALOR").VALUE.ToString
            Session("FOLIOES") = Session("rs").fields("FOLIOESCRITURA").VALUE.ToString
            Session("ESTADO_REGISTRO") = Session("rs").fields("ESTADO_REGISTRO").VALUE.ToString
            Session("FECHAESCRITURA") = Session("rs").fields("FECHAESCRITURA").VALUE.ToString
            Session("FECHAAVALUO") = Session("rs").fields("FECHAAVALUO").VALUE.ToString
            Session("NUMNOTARIO") = Session("rs").fields("NUMNOTARIO").VALUE.ToString
            Session("NOTARIO") = Session("rs").fields("NOMBRE").VALUE.ToString
            Session("ESTADONOTARIA") = Session("rs").fields("ESTADONOTARIO").VALUE.ToString
            Session("CALLEINM") = Session("rs").fields("CALLEINM").VALUE.ToString
            Session("REFERENCIA") = Session("rs").fields("REFERENCIA").VALUE.ToString
            Session("DESCRIPCION") = Session("rs").fields("DESCRIPCION").VALUE.ToString
            Session("MUNICIPIO_NOTARIO") = Session("rs").fields("MUNICIPIONOTARIO").VALUE.ToString
            Session("MUNICIPIO_REGISTRO") = Session("rs").fields("MUNICIPIO_REGISTRO").VALUE.ToString

            Session("Con").Close()

            Llenamunicipio_registro(Session("ESTADO_REGISTRO"))
            Llenamunicipionotaria(Session("ESTADONOTARIA"))
        Else
            Session("Con").Close()
            Exit Sub

        End If


    End Sub



#Region "GRID GARANTIAS"


    Private Sub DAG_garantias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_garantias.ItemCommand

        If (e.CommandName = "ELIMINAR") Then

            If e.Item.Cells(6).Text = "POR VALIDAR" Then
                lbl_status_general.Text = "Error: Documento en proceso de validación"
                Exit Sub
            ElseIf e.Item.Cells(6).Text = "APROBADA" Then
                lbl_status_general.Text = "Error: Documento Aprobado"
                Exit Sub
            Else
                Elimina_garantias(e.Item.Cells(1).Text, e.Item.Cells(4).Text, e.Item.Cells(5).Text)


            End If

        End If

        'Se digitaliza la garantia
        If (e.CommandName = "DIGITALIZAR") Then

            If e.Item.Cells(6).Text = "POR VALIDAR" Then
                lbl_status_general.Text = "Error: Documento en proceso de validación"
                Exit Sub
            ElseIf e.Item.Cells(6).Text = "APROBADA" Then
                lbl_status_general.Text = "Error: Documento Aprobado"
                Exit Sub
            Else
                Session("CVEGARANTIA") = (e.Item.Cells(1).Text)
                Session("TIPOGARANTIA") = (e.Item.Cells(0).Text)
                Session("VENGODE") = "CNFEXP_APARTADO5.aspx"
                Response.Redirect("~/DIGITALIZADOR/DIGI_GLOBAL.aspx")
            End If



        End If

        'Escondo la columna de id tipo
        e.Item.Cells(0).Visible = False

    End Sub


    Protected Sub DAG_garantias_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_garantias.ItemDataBound

        If Session("BLOQUEADO") = "1" Then ' una vez que el expediente este bloqueado ya no puede eliminar ni digitalizar garantias
            e.Item.Cells(7).Visible = False
            e.Item.Cells(8).Visible = False

        End If

    End Sub

    'Botón ELIMINAR del DBGRID(Elimina la garantia completamente de la tabla CATGARANTIAS)
    Private Sub Elimina_garantias(ByVal idgarantia As String, ByVal valor As String, ByVal pctje As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVEGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, idgarantia)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_GARANTIAS_AGREGADAS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_general.Text = "Garantía eliminada éxitosamente"
        EliminaValores(valor)
        muestragarantias()
        cmb_tipo_garantias.SelectedIndex = "0"

    End Sub

    'Validación del semaforo
    Private Sub Semaforo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDGARANT", Session("adVarChar"), Session("adParamInput"), 10, Session("IDGARANTIA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFEXP_SEMAFORO_APARTADO5"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub Iniciar_forma()
        pnl_gtia_pren.Visible = False
        pnl_gtia_hip.Visible = False
        cmb_tipo_garantias.SelectedIndex = "-1"
    End Sub
#End Region
#Region "PRENDARIAS"

    Protected Sub txt_cp_pren_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cp_pren.TextChanged
        cmb_estado_pren.Items.Clear()
        cmb_municipio_pren.Items.Clear()
        cmb_asentamiento_pren.Items.Clear()
    End Sub

    Private Sub cp_pren(ByVal cp As String)

        cmb_estado_pren.Items.Clear()
        cmb_municipio_pren.Items.Clear()
        cmb_asentamiento_pren.Items.Clear()

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
            cmb_estado_pren.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
            cmb_municipio_pren.Items.Add(item_mun)

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)
                cmb_asentamiento_pren.Items.Add(item)
                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()
    End Sub


    '---------Prendaria-----------------
    'CP prendaria
    Protected Sub img_glass_pren_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_glass_pren.Click
        cp_pren(txt_cp_pren.Text)
    End Sub


    'Muestra el tipo de garantias prendarias existentes en el BD
    Private Sub Muestratipoprendaria()

        cmb_tipo_prenda.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_tipo_prenda.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ORIGEN", Session("adVarChar"), Session("adParamInput"), 20, "GARANTIA")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_PRENDARIA"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TIPO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_tipo_prenda.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub btn_guardar_pren_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_pren.Click

        idgarantia()

        If validacion_tipo_garantia(Session("FOLIO"), Session("IDGARANTIA")) = True Or Session("FACULTAD_MIXTA") = "1" Then

            If txt_descripcion_pren.Text.Length > 1000 Or txt_referencias_pren.Text.Length > 1000 Then
                lbl_status_pren.Text = "Error: Sólo 1000 caracteres o menos en la descripción ó referencia "
            Else
                garantiaprendaria()
                Limpiaprendaria()
                muestragarantias() ' se muestran las garantias asignadas
                Iniciar_forma()
                ACUMULADOR = 0
                CalculaValores()
            End If

        Else
            lbl_status_pren.Text = "Error: Recuerde que sólo puede utilizar el mismo tipo de garantía en el expediente"
        End If

    End Sub

    Private Sub garantiaprendaria()

        Dim marca As String
        Dim modelo As String
        Dim año As Integer
        Dim uso As String
        Dim tipo As String
        Dim demanda As String
        Dim factura As Decimal
        Dim aforo As Decimal

        If txt_marca.Text = "" Then
            marca = ""
        Else
            marca = txt_marca.Text
        End If

        If txt_modelo.Text = "" Then
            modelo = ""
        Else
            modelo = txt_modelo.Text
        End If

        If txt_año.Text = "" Then
            año = 0
        Else
            año = CInt(txt_año.Text)
        End If

        If txt_uso.Text = "" Then
            uso = ""
        Else
            uso = txt_uso.Text
        End If

        If txt_tipo.Text = "" Then
            tipo = ""
        Else
            tipo = txt_tipo.Text
        End If

        If txt_demanda.Text = "" Then
            demanda = ""
        Else
            demanda = txt_demanda.Text
        End If

        If txt_valor_factura.Text = "" Then
            factura = 0.0
        Else
            factura = CDec(txt_valor_factura.Text)
        End If

        If txt_aforo_pren.Text = "" Then
            aforo = 0.0
        Else
            aforo = CDec(txt_aforo_pren.Text)
        End If



        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_GARANTIAS_PREN"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 20, txt_monto_pren.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDGARANT", Session("adVarChar"), Session("adParamInput"), 20, Session("IDGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMEROSERIE", Session("adVarChar"), Session("adParamInput"), 100, txt_num_serie.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTIPO", Session("adVarChar"), Session("adParamInput"), 20, cmb_tipo_prenda.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RESPONSABLE", Session("adVarChar"), Session("adParamInput"), 400, txt_responsable.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_cp_pren.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado_pren.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio_pren.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 10, cmb_asentamiento_pren.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLENUM", Session("adVarChar"), Session("adParamInput"), 100, txt_calle_pren.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REFERENCIA", Session("adVarChar"), Session("adParamInput"), 1000, txt_referencias_pren.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 1000, txt_descripcion_pren.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 500, txt_propietario_pren.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo_relacion_pren.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MARCA", Session("adVarChar"), Session("adParamInput"), 100, marca)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MODELO", Session("adVarChar"), Session("adParamInput"), 100, modelo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AÑO", Session("adVarChar"), Session("adParamInput"), 100, año)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USO", Session("adVarChar"), Session("adParamInput"), 100, uso)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 300, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DEMANDA", Session("adVarChar"), Session("adParamInput"), 300, demanda)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR_FACTURA", Session("adVarChar"), Session("adParamInput"), 10, factura)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AFORO", Session("adVarChar"), Session("adParamInput"), 10, aforo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'VARIABLES QUE REGRESO DEL SP
            Session("CVEGARANTIA") = Session("rs").Fields("CVEGARANTIA").value.ToString
            Session("TIPOGARANTIA") = Session("rs").Fields("IDTIPOGARANTIA").value.ToString

            Select Case Session("rs").Fields("ERROR").value.ToString
                Case "OK"
                    lbl_status_pren.Text = "Se ha asignado correctamente la garantía prendaria"
                Case "EXISTE"
                    lbl_status_pren.Text = "NOTA: Ya existe esta garantía prendaría asignada a otro expediente, Se ha asignado correctamente la garantía prendaria"
                Case "ERROR"
                    lbl_status_pren.Text = "Error: Garantía cubierta al 100% en otro(s) expedientes, asigne otra garantía."
                Case "OTRA"
                    lbl_status_pren.Text = "Error: El valor de la garantía no alcanza a cubrir el total del préstamo."
                Case "MISMOFOLIO"
                    lbl_status_pren.Text = "Error: Ya cuenta con esta garantía asignada,Elimínela y vuelva asignarla."
            End Select


        End If

        Session("Con").Close()

    End Sub

    'Limpia los controles de garantia prendaria
    Private Sub Limpiaprendaria()

        txt_monto_pren.Text = ""
        txt_num_serie.Text = ""
        txt_descripcion_pren.Text = ""
        cmb_tipo_prenda.SelectedIndex = "-1"
        txt_responsable.Text = ""
        txt_cp_pren.Text = ""
        cmb_estado_pren.Items.Clear()
        cmb_municipio_pren.Items.Clear()
        cmb_asentamiento_pren.Items.Clear()
        txt_calle_pren.Text = ""
        txt_referencias_pren.Text = ""
        '  cmb_tipo_garantias.SelectedIndex = "-1"
        txt_propietario_pren.Text = ""
        cmb_tipo_relacion_pren.SelectedIndex = "-1"
        txt_marca.Text = ""
        txt_modelo.Text = ""
        txt_año.Text = ""
        txt_uso.Text = ""
        txt_tipo.Text = ""
        txt_demanda.Text = ""
        txt_valor_factura.Text = ""
        txt_aforo_pren.Text = ""

    End Sub
#End Region

#Region "HIPOTECARIAS"

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
    End Sub

    Private Function validafecha(ByVal fechaini As String) As Boolean

        'Dim Año As Integer
        'Dim Mes As Integer
        'Dim Dia As Integer

        Dim res As Boolean = True

        'Año = Right(fechaini, 4)
        'Mes = Mid(fechaini, 4, 2)
        'Dia = Left(fechaini, 2)

        'If (Mes > 12 Or Mes = 0) Or (Dia > 31 Or Dia = 0) Or (Año < 1900 Or Año = 0) Then
        '    res = False
        'End If

        Return res

    End Function

    'Private Function validafechafutura(ByVal fechaini As String) As Boolean

    '    Dim res As Boolean
    '    res = True

    '    Dim Año As Integer
    '    Dim Mes As Integer
    '    Dim Dia As Integer

    '    Dim añoSis As Integer
    '    Dim mesSis As Integer
    '    Dim DiaSis As Integer

    '    Año = Right(fechaini, 4)
    '    Mes = Mid(fechaini, 4, 2)
    '    Dia = Left(fechaini, 2)

    '    añoSis = Right(Session("FechaSis"), 4)
    '    mesSis = Mid(Session("FechaSis"), 4, 2)
    '    DiaSis = Left(Session("FechaSis"), 2)

    '    If Año >= añoSis And Mes >= mesSis And Dia >= DiaSis Then
    '        res = False
    '    End If


    '    Return res
    'End Function

    'Guarda garantia hipotecaria

    Protected Sub btn_guardar_hip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_hip.Click

        lbl_status.Text = ""
        idgarantia()



        If validacion_tipo_garantia(Session("FOLIO"), Session("IDGARANTIA")) = True Or Session("FACULTAD_MIXTA") = "1" Then


            If txt_referencias_inm.Text.Length > 1000 Or txt_descripcion_gtia.Text.Length > 1000 Then
                lbl_status.Text = "Error: Sólo 1000 caracteres o menos en la descripción o referencia "
            Else
                If validafecha(txt_fechaes.Text) = True And validafecha(txt_avaluo.Text) Then

                    ' If validafechafutura(txt_fechaes.Text) = True Then
                    guardaHip()
                    limpiahipotecaria()
                    muestragarantias()
                    Iniciar_forma()
                    ACUMULADOR = 0
                    CalculaValores()
                    'Else
                    '    lbl_status.Text = "Error: Verifique fecha de escrituración"
                    'End If

                Else
                    lbl_status.Text = "Error: Verifique fecha"
                End If
            End If
        Else
            lbl_status.Text = "Error: Recuerde que sólo puede utilizar el mismo tipo de garantía en el expediente"
        End If

    End Sub

    Private Sub guardaHip()

        Dim valor_avaluo As Decimal
        Dim avaluo As String
        Dim institucion_gravamen As String
        Dim gravamen As String
        Dim antiguedad As Integer
        Dim m2terreno As Integer
        Dim m2construido As Integer
        Dim aforo_hip As Decimal
        Dim inmueble As String

        If txt_valor_avaluo.Text = "" Then
            valor_avaluo = 0
        Else
            valor_avaluo = CDec(txt_valor_avaluo.Text)
        End If

        If txt_avaluo.Text = "" Then
            avaluo = ""
        Else
            avaluo = txt_avaluo.Text
        End If

        If txt_institucion_Gravamen.Text = "" Then
            institucion_gravamen = ""
        Else
            institucion_gravamen = txt_institucion_Gravamen.Text
        End If

        If txt_gravamen.Text = "" Then
            gravamen = ""
        Else
            gravamen = txt_gravamen.Text
        End If

        If txt_antiguedad.Text = "" Then
            antiguedad = 0
        Else
            antiguedad = CInt(txt_antiguedad.Text)
        End If

        If txt_m2construido.Text = "" Then
            m2construido = 0
        Else
            m2construido = CInt(txt_m2construido.Text)
        End If

        If txt_m2terreno.Text = "" Then
            m2terreno = 0
        Else
            m2terreno = CInt(txt_m2terreno.Text)
        End If

        If txt_aforo.Text = "" Then
            aforo_hip = 0
        Else
            aforo_hip = CDec(txt_aforo.Text)
        End If

        If txt_inm.Text = "" Then
            inmueble = ""
        Else
            inmueble = txt_inm.Text
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_GARANTIAS_HIP"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 20, txt_monto_hip.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDGARANT", Session("adVarChar"), Session("adParamInput"), 20, Session("IDGARANTIA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RPPC", Session("adVarChar"), Session("adParamInput"), 100, txt_rppc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIOESC", Session("adVarChar"), Session("adParamInput"), 10, txt_folioes.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDESTADOESC", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado_registro.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNIESC", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio_registro.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaes.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMNOTARIO", Session("adVarChar"), Session("adParamInput"), 9, txt_num_notario.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRENOTARIO", Session("adVarChar"), Session("adParamInput"), 400, txt_nombre_notario.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDESTADONOTARIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_estado_notaria.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIONOTARIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio_notaria.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDESTADOIN", Session("adVarChar"), Session("adParamInput"), 10, cmb_Estado.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIOIN", Session("adVarChar"), Session("adParamInput"), 10, cmb_municipio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDLOCALIDADIN", Session("adVarChar"), Session("adParamInput"), 10, cmb_asentamiento.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLENUMIN", Session("adVarChar"), Session("adParamInput"), 100, txt_calle_inm.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REFERENCIAIN", Session("adVarChar"), Session("adParamInput"), 1000, txt_referencias_inm.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 1000, txt_descripcion_gtia.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("SESION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 100, txt_clave.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAAVALUO", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_avaluo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROPIETARIO", Session("adVarChar"), Session("adParamInput"), 500, txt_propietario.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INMUEBLE", Session("adVarChar"), Session("adParamInput"), 500, inmueble)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("M2TERRERNO", Session("adVarChar"), Session("adParamInput"), 10, m2terreno)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("M2CONSTRUCCION", Session("adVarChar"), Session("adParamInput"), 10, m2construido)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANTIGUEDAD", Session("adVarChar"), Session("adParamInput"), 10, antiguedad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("GRAVAMEN", Session("adVarChar"), Session("adParamInput"), 300, gravamen)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INS_GRAVAMEN", Session("adVarChar"), Session("adParamInput"), 300, institucion_gravamen)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AVALUO", Session("adVarChar"), Session("adParamInput"), 300, avaluo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR_AVALUO", Session("adVarChar"), Session("adParamInput"), 10, valor_avaluo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AFORO", Session("adVarChar"), Session("adParamInput"), 10, aforo_hip)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SEXO", Session("adVarChar"), Session("adParamInput"), 5, cmb_sexo.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECH_NAC", Session("adVarChar"), Session("adParamInput"), 10, txt_fechanac.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 13, txt_rfc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 10, txt_telefono.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTADO_CIVIL", Session("adVarChar"), Session("adParamInput"), 5, cmb_edo_civil.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE_CONYUGE", Session("adVarChar"), Session("adParamInput"), 150, txt_conyuge.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'VARIABLES QUE REGRESO DEL SP
            Session("CVEGARANTIA") = Session("rs").Fields("CVEGARANTIA").value.ToString
            Session("TIPOGARANTIA") = Session("rs").Fields("IDTIPOGARANTIA").value.ToString

            Select Case Session("rs").Fields("ERROR").value.ToString
                Case "OK"
                    lbl_status.Text = "Se ha asignado correctamente la garantía hipotecaria"
                Case "EXISTE"
                    lbl_status.Text = "Alerta: Ya existe un Registro Público de la Propiedad y Comercio en otro expediente." + " Se ha asignado correctamente la garantía hipotecaria"
                Case "ERROR"
                    lbl_status.Text = "Error: Garantía cubierta al 100% en otro(s) expedientes, asigne otra garantía."
                Case "OTRA"
                    lbl_status.Text = "Error: El valor de la garantía no alcanza a cubrir el total del préstamo."
                Case "MISMOFOLIO"
                    lbl_status.Text = "Error: Ya cuenta con esta garantía asignada. Elimínela y vuelva asignarla."
            End Select

        End If

        Session("Con").Close()

    End Sub

    'Limpia los controles de hipotecaria
    Private Sub limpiahipotecaria()
        txt_clave.Text = ""
        txt_monto_hip.Text = ""
        txt_rppc.Text = ""
        txt_folioes.Text = ""
        cmb_estado_registro.SelectedIndex = "-1"
        cmb_municipio_registro.Items.Clear()
        txt_avaluo.Text = ""
        txt_fechaes.Text = ""
        txt_num_notario.Text = ""
        txt_nombre_notario.Text = ""
        cmb_estado_notaria.SelectedIndex = "-1"
        cmb_municipio_notaria.Items.Clear()
        txt_cp.Text = ""
        cmb_Estado.Items.Clear()
        cmb_municipio.Items.Clear()
        cmb_asentamiento.Items.Clear()
        txt_calle_inm.Text = ""
        txt_referencias_inm.Text = ""
        txt_descripcion_gtia.Text = ""
        'cmb_tipo_garantias.SelectedIndex = "0"
        txt_fecha_avaluo.Text = ""
        txt_propietario.Text = ""
        cmb_relacion.SelectedIndex = "-1"
        txt_inm.Text = ""
        txt_m2terreno.Text = ""
        txt_m2construido.Text = ""
        txt_antiguedad.Text = ""
        txt_gravamen.Text = ""
        txt_institucion_Gravamen.Text = ""
        txt_avaluo.Text = ""
        txt_valor_avaluo.Text = ""
        txt_aforo.Text = ""
        cmb_sexo.SelectedIndex = "-1"
        txt_fechanac.Text = ""
        txt_rfc.Text = ""
        cmb_edo_civil.SelectedIndex = "-1"
        txt_conyuge.Text = ""
        txt_telefono.Text = ""
    End Sub

#End Region

End Class