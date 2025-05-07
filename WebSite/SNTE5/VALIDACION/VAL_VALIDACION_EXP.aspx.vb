Public Class VAL_VALIDACION_EXP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then


            If Session("FOLIO") = 0 Then
                lbl_Folio.Text = ""
                lbl_Prospecto.Text = "VALIDACION DE DOCUMENTOS DE PERSONA:  " + Session("PERSONAID")
                lbl_Producto.Text = ""
                DocsXValidarXPersona()
                LlenarEstatusDocs()
                LlenarDocsPerValidados()
                VerificarDocsPerCompletos()
            Else
                'Mostar los datos generales de un expediente: Folio, nombre de cliente y producto
                DatosExpediente()
                lbl_Folio.Text = "Datos del Expediente:  " + CStr(Session("CVEEXPE"))
                lbl_Prospecto.Text = Session("PROSPECTO")
                lbl_Producto.Text = Session("PRODUCTO")
                Session("ANTERIOR_VER") = "VALIDADOR"

                If tipo_persona() = "F" Then
                    lnk_ResumenPersona.Attributes.Add("OnClick", "ResumenPersona()")
                Else
                    lnk_ResumenPersona.Attributes.Add("OnClick", "ResumenPersonaM()")
                End If
                DocsXValidarXExpediente()
                LlenarEstatusDocs()
                LlenarDocsValidados()
                VerificarAprobacionCompleta()
                LlenaActores()
                Session("PERSONAID_F") = cmb_personas.SelectedItem.Value
            End If
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Validador", "Validación de Documentos")
    End Sub

    Function tipo_persona() As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_TIPO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("TIPO").value.ToString
        Session("Con").Close()
        Return tipo
    End Function

    Private Sub DatosExpediente()
        'Obtiene los datos generales de un expediente: folio, nombre de cliente y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"

        Session("rs") = Session("cmd").Execute()

        Session("PRODUCTO") = Session("rs").fields("PRODUCTO").value.ToString
        Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString
        Session("CLAVEFOLIO") = Session("rs").fields("CLAVE").value.ToString
        Session("IDINSTITUCION") = Session("rs").fields("INSTITUCION").value.ToString
        Session("NUMCONTROL") = Session("rs").fields("NUMCONTROL").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub DocsXValidarXExpediente()
        'Se llena el ListBox the Documentos X validar de un expediente 

        lst_DocumentosXValidar.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DOCUMENTOS_X_VALIDAR"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem((Session("rs").Fields("CATTIPDOC_DESCRIPCION").Value.ToString + " - " + Session("rs").Fields("DOC").Value.ToString), Session("rs").Fields("IDDOC").Value.ToString)
            lst_DocumentosXValidar.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub lst_DocumentosXValidar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lst_DocumentosXValidar.SelectedIndexChanged

        If lst_DocumentosXValidar.SelectedItem Is Nothing Then
        Else
            Session("DOCUMENTO_DIGITALIZADO") = lst_DocumentosXValidar.SelectedItem.Value 'Variable de sesion para tener el docuemnto elegido
        End If

    End Sub

    Protected Sub btn_ver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ver.Click

        ' boton para ver el docuemnto y pueda ser revisado por el validador
        If lst_DocumentosXValidar.SelectedItem Is Nothing Then
            lbl_AlertaVer.Visible = True
            lbl_AlertaVer.Text = "Error: Seleccione un documento"
        Else
            lbl_AlertaVer.Visible = False
            cmb_Estatus.Enabled = True
            Response.Redirect("/DIGITALIZADOR/DIGI_MOSTRAR.aspx")

        End If

    End Sub

    Private Sub LlenarEstatusDocs()
        'Se llena la lista de estatus a elegir para los documentos

        Dim ELIJA As New ListItem("ELIJA", "0")
        cmb_Estatus.Items.Clear()
        cmb_Estatus.Items.Add(ELIJA)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("cmd").CommandText = "SEL_ESTATUS_VALIDA"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            'Llenado de los datos obtenidos
            Dim item As New ListItem(Session("rs").Fields("CATVALIDA_DESCRIPCION").Value.ToString, Session("rs").Fields("CATVALIDA_ID_VALIDA").Value.ToString)

            cmb_Estatus.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_Estatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Estatus.SelectedIndexChanged

        'Revisa si algun estatus fue seleccionado
        If cmb_Estatus.SelectedItem.Value <> "0" Then
            btn_Guardar.Enabled = True ' Habilita Boton Guardar
        End If
        lbl_AlertaVer.Visible = False

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click
        'Boton para guardar el estatus elegido de un documento
        If lst_DocumentosXValidar.SelectedItem Is Nothing Then
            lbl_AlertaVer.Text = "Error: Seleccione un documento"
            lbl_AlertaVer.Visible = True
            cmb_Estatus.SelectedValue = "0"
            btn_Guardar.Enabled = False

        Else
            lbl_AlertaVer.Visible = False
            If Session("FOLIO") = 0 Then
                CalificarDocumentoPer()
                DocsXValidarXPersona()
                LlenarDocsPerValidados()
                VerificarDocsPerCompletos()
            Else

                CalificarDocumento()
                DocsXValidarXExpediente()
                LlenarDocsValidados()
                VerificarAprobacionCompleta()
            End If
            cmb_Estatus.SelectedValue = "0"
            btn_Guardar.Enabled = False

        End If

    End Sub

    Private Sub CalificarDocumento()
        'Agrega la calificacion del documento actual

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTO_DIGITALIZADO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, cmb_Estatus.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_ESTATUS_DOCUMENTO"

        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Sub LlenarDocsValidados()
        'Se llena la cola de validacion con los expedientes que se encuantran en "proceso"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtColaValidacion As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCUMENTOS_VALIDADOS"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtColaValidacion, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_cola.DataSource = dtColaValidacion
        DAG_cola.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub DAG_COLA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_cola.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "ELIMINAR") Then
            Session("DOC_VALIDADO") = e.Item.Cells(0).Text
            'Actualizacion de los datos de un expediente en la cola de validacion cuando es acaparado por un validador
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOC_VALIDADO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_REGRESAR_ESTATUS_DOCUMENTO"
            Session("rs") = Session("cmd").Execute()

            If Session("rs").Fields("BORRO").Value.ToString = "NO" Then
                lbl_AlertaVer.Text = "Error: Este documento ya no puede ser modificado"
                lbl_AlertaVer.Visible = True
            End If

            Session("Con").Close()


        End If

        DAG_cola.DataBind()
        DocsXValidarXExpediente()
        LlenarDocsValidados()
        VerificarAprobacionCompleta()

    End Sub

    Private Sub VerificarAprobacionCompleta()
        'Verifica si se han validado todos los documentos 

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_VERIFICAR_APROBACION_COMPLETA"

        Session("rs") = Session("cmd").Execute()

        Dim AUX As Integer = Session("rs").Fields("CONT").Value

        If AUX = 0 Then
            btn_DocumentosAprovados.Enabled = True 'Si esta completo el expediente habilita el boton de insertar a la cola de validacion
            btn_Enviar.Enabled = False
        Else
            If AUX < 0 Then
                btn_Enviar.Enabled = True
                btn_DocumentosAprovados.Enabled = False 'No esta completo el expediente no se habilita el boton
            Else
                btn_Enviar.Enabled = False
                btn_DocumentosAprovados.Enabled = False
            End If
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_DocumentosAprovados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_DocumentosAprovados.Click
        If Session("FOLIO") = 0 Then
            AprobarDocsPer()
        Else
            AprobarExpediente()
            AvisoCambioEstatus()
            ApruebaMesaControlCorreo()
        End If
        Response.Redirect("/VALIDACION/VAL_VALIDACION_ADM.aspx")
    End Sub

    Private Sub AprobarExpediente()
        'Se aprueba el expediente completo con documentos aprobados 

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_EXPEDIENTE_DOCUMENTOS_VALIDADOS"

        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub btn_Enviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Enviar.Click
        If Session("FOLIO") = 0 Then
            DocumentosPerRechazados()
        End If
        'Boton envia expediente con docuemntos rechazados
        DocumentosRechazados()
        AvisoCambioEstatus()
        RechazaMesaControlCorreo()

        Response.Redirect("/VALIDACION/VAL_VALIDACION_ADM.aspx")
        'End If

    End Sub

    Private Sub DocumentosRechazados()
        'Se agrega estatus de expediente a expediente con doicuemntos rechazados 

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_EXPEDIENTE_DOCUMENTOS_RECHAZADOS"

        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub lnk_busquedagtia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busquedagtia.Click
        ClientScript.RegisterStartupScript(GetType(String), "Garantias", "window.open(""/CREDITO/EXPEDIENTE/Busqueda_Gtias.aspx"", ""RP"", ""width=600,height=500,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
    End Sub

    Protected Sub lnk_notas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_notas.Click

        ModalPopupExtender1.Show()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_CNFEXP_VALIDADOR_NOTAS]"
        Session("rs") = Session("cmd").Execute()

        Dim nota As String = ""
        Do While Not Session("rs").EOF

            nota = nota + vbCrLf + Session("rs").Fields("TIPO").Value.ToString + vbCrLf + Session("rs").Fields("NOTAS").Value.ToString + vbCrLf


            Session("rs").movenext()


        Loop
        lbl_notasexp.Text = nota

        Session("Con").Close()

    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        ModalPopupExtender1.Hide()
        lbl_notasexp.Text = ""
    End Sub

    Private Sub AvisoCambioEstatus()

        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim user As String = String.Empty
        Dim contenido As String = String.Empty
        'Insertar a la Cola de validacion para la fase 2
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AVISOCORREO_ESTATUS_USUARIO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                user = Session("rs").Fields("USUARIO").Value.ToString
                contenido = Session("rs").Fields("CONTENIDO").Value.ToString
                subject = "CAMBIO DE ESTATUS DE EXPEDIENTE (" + CStr(Session("FOLIO")) + ")"
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td>Estimado(a) :   " + user + +contenido + "</td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br />")
                sbhtml.Append("<br></br>")
                sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br></br>")
                clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            Loop
        End If

        Session("Con").Close()
    End Sub


    Private Sub LlenaActores()
        'Se llena la lista de actores
        cmb_personas.Items.Clear()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_ACTORES_EXTRA_PRELLENADO"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            'Llenado de los datos obtenidos
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString + "(" + Session("rs").Fields("TIPO").Value.ToString + ")", Session("rs").Fields("IDPERSONA").Value.ToString)

            cmb_personas.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub cmb_personas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_personas.SelectedIndexChanged
        Session("PERSONAID_F") = cmb_personas.SelectedItem.Value
    End Sub

    ''DATOS PARA DOCUMENTOS DE PERSONAS

    Private Sub DocsXValidarXPersona()
        'Se llena el ListBox the Documentos X validar de un expediente 

        lst_DocumentosXValidar.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DOC_PERSONA_X_VALIDAR"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem((Session("rs").Fields("CATTIPDOC_DESCRIPCION").Value.ToString + " - " + Session("rs").Fields("DOC").Value.ToString), Session("rs").Fields("IDDOC").Value.ToString)
            lst_DocumentosXValidar.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CalificarDocumentoPer()
        'Agrega la calificacion del documento actual

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTO_DIGITALIZADO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, cmb_Estatus.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_ESTATUS_DOCUMENTO_PER"

        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Sub LlenarDocsPerValidados()
        'Se llena la cola de validacion con los expedientes que se encuantran en "proceso"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtColaValidacion As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCS_PER_VALIDADOS"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtColaValidacion, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_cola.DataSource = dtColaValidacion
        DAG_cola.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub VerificarDocsPerCompletos()
        'Verifica si se han validado todos los documentos 

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICAR_DOCS_PER_COMPLETOS"
        Session("rs") = Session("cmd").Execute()
        Dim AUX As Integer = Session("rs").Fields("CONT").Value
        If AUX = 0 Then
            btn_DocumentosAprovados.Enabled = True 'Si esta completo el expediente habilita el boton de insertar a la cola de validacion
        Else
            If AUX < 0 Then
                btn_Enviar.Enabled = True
                btn_DocumentosAprovados.Enabled = False 'No esta completo el expediente no se habilita el boton
            Else
                btn_Enviar.Enabled = False
                btn_DocumentosAprovados.Enabled = False
            End If
        End If
        Session("Con").Close()
    End Sub

    Private Sub AprobarDocsPer()
        'Se aprueba el expediente completo con documentos aprobados 

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_DOCS_PER_VALIDADOS"

        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Sub DocumentosPerRechazados()
        'Se agrega estatus de expediente a expediente con doicuemntos rechazados 

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_DOCS_PER_RECHAZADOS"

        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub

#Region "Envio de Correos"

    Private Sub ApruebaMesaControlCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "APROBMESACON")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Expediente " + Session("CLAVEFOLIO").ToString + " ha sido aprobado por Mesa de Control"

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Se informa que el expediente digital correspondiente al folio " + Session("CLAVEFOLIO").ToString + "</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>del agremiado " + Session("PROSPECTO").ToString + " ha sido aprobado.</td></tr>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

    Private Sub RechazaMesaControlCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "RECHAMESACON")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Expediente " + Session("CLAVEFOLIO").ToString + " ha sido rechazado por Mesa de Control"

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Se informa que el expediente digital correspondiente al folio " + Session("CLAVEFOLIO").ToString + "</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>del agremiado " + Session("PROSPECTO").ToString + " ha sido rechazado.</td></tr>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

#End Region

End Class