Public Class CRED_EXP_APARTADO6
    Inherits System.Web.UI.Page

    Dim egresos As New List(Of TextBox)()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Informe de Investigación", "Informe de Investigación")

        If Not Me.IsPostBack Then

            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("CVEEXPE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")
            Session("PERSONA") = Session("PERSONAID")
            Session("VENGODE") = "CRED_EXP_APARTADO6.aspx"

            LlenaPersonas()
            expbloqueado()
        End If
        btn_generahoja.Attributes.Add("OnClick", "generahoja()")
        LlenaGastos()
    End Sub

    Function tipo_persona() As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_TIPO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("TIPO").value.ToString
        Session("Con").Close()
        Return tipo
    End Function


    Private Sub LlenaDatos()
        Dim latitud As String, longitud As String, tp As String, id_act As Integer
        Dim CoorStr
        id_act = -2
        tp = tipo_persona()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_INV_DATOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            txt_datos.Text = Session("rs").Fields("DATOS").Value.ToString
            If tp = "F" Then
                id_act = Session("rs").Fields("ACTIVIDAD").Value

            End If


            txt_nom_info.Text = Session("rs").Fields("NOMBRE").Value.ToString
            txt_notas.Text = Session("rs").Fields("NOTAS").Value.ToString

            CoorStr = Split(Replace(Replace(Session("rs").Fields("COORDENADAS").Value.ToString, "(", ""), ")", ""), ",")
            latitud = CoorStr(0)
            longitud = CoorStr(1)
            txt_latitud.Text = latitud
            txt_longitud.Text = longitud
            latitud = "center=" + txt_latitud.Text + "," + txt_longitud.Text + "&zoom=" + hdn_zoom.Value + "&size=680x215&markers=color:red|" + Session("rs").Fields("COORDENADAS").Value.ToString
            'hdn_coordenadas.Value = Session("rs").Fields("COORDENADAS").Value.ToString
            'hdn_zoom.Value = Session("rs").Fields("ZOOM").Value.ToString
            If Session("rs").Fields("AVISO").Value.ToString = 1 Then
                '' ModalPopupExtender1.Show()
            End If
            Session("datosinv") = 1
        Else

            Session("datosinv") = 0
        End If
        Session("Con").Close()


        If tp = "F" Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONA"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_INV_DATOS_CON"
            Session("rs") = Session("cmd").Execute()

            Session("Con").Close()
        End If

    End Sub
    Private Sub consultacoordenadas()
        Dim tipop As String
        tipop = tipo_persona()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPERSONA"))
        Session("cmd").Parameters.Append(Session("parm"))
        If tipop = "F" Then
            Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Else
            Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 4)
        End If
        'Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 4)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COORDENADAS_IDPERS_IDTIPO"
        Session("rs") = Session("cmd").Execute()
        hdn_coordenadas.Value = Session("rs").Fields("COORDENADAS").Value.ToString
        hdn_zoom.Value = Session("rs").Fields("ZOOM").Value.ToString
        Session("Con").Close()
    End Sub

    Protected Sub btn_persona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_persona.Click
        'Session("PERSONA") = cmb_persona.SelectedItem.Value
        txt_datos.Text = ""
        txt_nom_info.Text = ""
        txt_notas.Text = ""
        txt_latitud.Text = ""
        txt_longitud.Text = ""
        LlenaDatos()
        ''Servicios.Recarga(Session("PERSONA"))
        'ModalPopupExtender1.Show()
        pnl_investigacion.Enabled = True
        ''TabContainer1.ActiveTab = TabPanel2
    End Sub

    Protected Sub cmb_persona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_persona.SelectedIndexChanged
        pnl_investigacion.Enabled = False
        Session("PERSONA") = cmb_persona.SelectedItem.Value
        'consultacoordenadas()
        Session("MAPA") = "center=" + hdn_coordenadas.Value + "&zoom=" + hdn_zoom.Value + "&size=680x215&markers=color:red|" + hdn_coordenadas.Value
        'Servicios.Recarga(Session("PERSONA"))
        'Label1.Text = Session("MAPA")
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'Dim latitud As String
        'Dim longitud As String
        Dim zoom As String
        'Dim CoorStr
        'CoorStr = Split(Replace(Replace(hdn_coordenadas.Value.ToString, "(", ""), ")", ""), ",")
        'latitud = CoorStr(0)
        'longitud = CoorStr(1)
        zoom = hdn_zoom.Value.ToString

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("DATOS", Session("adVarChar"), Session("adParamInput"), 3000, txt_datos.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("parm") = Session("cmd").CreateParameter("ACTCON", Session("adVarChar"), Session("adParamInput"), 11, -1)

        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBREINFO", Session("adVarChar"), Session("adParamInput"), 700, txt_nom_info.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 3000, txt_notas.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LATITUD", Session("adVarChar"), Session("adParamInput"), 30, txt_latitud.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LONGITUD", Session("adVarChar"), Session("adParamInput"), 30, txt_longitud.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ZOOM", Session("adVarChar"), Session("adParamInput"), 4, zoom)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_INV_DATOS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        LlenaDatos()
    End Sub

#Region "Datos de egresos de la persona"
    'Genero los gastos dinámicamente
    Private Sub LlenaGastos()
        'limpia el panel
        pnl_web_ctrl.Controls.Clear()

        Dim sumaGastos As Double

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_EGRESOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            'declaro variables
            Dim nombreGasto As String
            Dim idGasto As String
            Dim montoGasto As String
            'verfico si ya expiró el monto
            ' If Session("rs").Fields("AVISO").value.ToString = 1 Then
            montoGasto = Session("rs").Fields("MONTO").Value.ToString
            'Else
            '    montoGasto = "0.0"
            'End If
            'asigno varibles
            nombreGasto = Session("rs").Fields("CATCONCEPGTOS_GASTO").Value.ToString
            idGasto = Session("rs").Fields("IDGASTO").Value.ToString

            'creo el control dinamicamente
            Dim controlGasto As HtmlGenericControl
            controlGasto = newEgresosField(idGasto, nombreGasto, montoGasto)
            'agrego el control
            pnl_web_ctrl.Controls.Add(controlGasto)
            Session("rs").movenext()

            'sumo el monto actual a la suma de los gastos mensules
            sumaGastos += montoGasto
        Loop
        lbl_gastosMensuales.Text = "$" + Session("MascoreG").FormatNumberCurr(sumaGastos)
        Session("Con").Close()



    End Sub
#End Region

#Region "Fun dinamic egresos input"
    'función que regresa un input con las caracteristicas necesarias para representar un gasto
    Function newEgresosField(ByVal id As String, ByVal title As String, ByVal monto As String) As HtmlGenericControl
        ' se declaran variables
        Dim outer_div As New HtmlGenericControl
        Dim content_div As New HtmlGenericControl
        Dim input As New TextBox
        Dim labels_div As New HtmlGenericControl
        Dim title_label As New Label
        Dim requiredFieldValidator As New RequiredFieldValidator
        Dim filtro As New AjaxControlToolkit.FilteredTextBoxExtender
        Dim filtroE As New RegularExpressionValidator
        'se establezen las caracterizticas del control
        outer_div.TagName = "div"
        outer_div.Attributes("class") = "module_subsec_elements"

        content_div.TagName = "div"
        content_div.Attributes("class") = "module_subsec_elements_content text_input_nice_div "

        input.CssClass = "text_input_nice_input"
        input.ID = "txt_g" & id
        input.Text = monto

        labels_div.TagName = "div"
        labels_div.Attributes("class") = "text_input_nice_labels"

        title_label.ID = "lbl_g" & id
        title_label.CssClass = "text_input_nice_label"
        title_label.Text = title

        requiredFieldValidator.ControlToValidate = input.ID
        requiredFieldValidator.CssClass = "alertaValidator bold"
        requiredFieldValidator.Text = "Falta Dato"
        requiredFieldValidator.Display = ValidatorDisplay.Dynamic

        filtroE.ControlToValidate = input.ID
        filtroE.CssClass = "alertaValidator bold"
        filtroE.Text = "Formato Erroneo"
        filtroE.Display = ValidatorDisplay.Dynamic
        filtroE.ValidationExpression = "[0-9]{0,17}(\.[0-9][0-9]?)?"


        filtro.TargetControlID = input.ID
        filtro.FilterType = AjaxControlToolkit.FilterTypes.Custom
        filtro.ValidChars = ".0123456789"


        'se agregan las características al control
        labels_div.Controls.Add(title_label)
        labels_div.Controls.Add(requiredFieldValidator)
        labels_div.Controls.Add(filtroE)

        content_div.Controls.Add(input)
        content_div.Controls.Add(filtro)
        content_div.Controls.Add(labels_div)

        outer_div.Controls.Add(content_div)
        'guarda el textbox del control en una lista de los text box
        egresos.Add(input)
        'regresa el control
        Return outer_div
    End Function



#End Region

    Private Sub LlenaPersonas()
        'Procedimiento que obtiene el catálogo de actividades económicas y las despliega en el combo correspondiente
        cmb_persona.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PERSONAS_INV"
        Session("rs") = Session("cmd").Execute()

        'Dim elija As New ListItem("ELIJA", "")
        'cmb_persona.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem((Session("rs").Fields("NOMBRE").Value.ToString + " - " + Session("rs").Fields("DESCRIPCION").Value.ToString), Session("rs").Fields("ID").Value.ToString)
            cmb_persona.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub expbloqueado()

        Dim bloqueado As String = ""
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            bloqueado = Session("rs").fields("BLOQUEADO").value.ToString()

            If bloqueado = "1" Then 'Si está bloqueado el expediente se deshabilitan los botones
                btn_persona.Enabled = False
                cmb_persona.Enabled = False
                pnl_investigacion.Enabled = False
            End If

        End If
        Session("Con").Close()

    End Sub

End Class