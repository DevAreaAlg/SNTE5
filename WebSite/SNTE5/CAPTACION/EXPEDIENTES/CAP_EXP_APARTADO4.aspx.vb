Imports System.IO
Public Class CAP_EXP_APARTADO4
    Inherits System.Web.UI.Page

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Digitalización de Documentos", "DIGITALIZACIÓN DE DOCUMENTOS")
        If Not Me.IsPostBack Then
            'Mostramos el numero de Folio del Expediente
            EstatusEnDigitalizacion()

            Dim menuPanel As Panel
            menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

            If Not menuPanel Is Nothing Then
                menuPanel.Visible = False
            End If

            'Datos Generales de Expediente: Folio, Nombre de Afiliado y Producto

            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("FOLIO"))
            lbl_estatus.Text = Session("ESTATUS_EXPEDIENTE").ToString
            'Inicializa los atributos de las acciones de digitalizacion
            btn_ElegirDocumento.Attributes.Add("onClick", "btnScan_onclick();return false;")
            btn_Guardar.Attributes.Add("onClick", "btnUpload_onclick();")
            btn_Borrar.Attributes.Add("onClick", "btnBorrar_onclick();")

            If Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Or Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS RECHAZADOS" Then
                MostrarDocumentosNoDigitalizados()
            Else
                MostrarDocumentosNoDigitalizadosFase2()
            End If

            'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
            MostrarDocumentosDigitalizados() 'Mostrara los documentos que ya han sido Digitalizados
            VerificarDocsCompletos() 'Verifica si ya han sido digitalizados tods los documentos
            Session("VENGODE") = "CAP_EXP_APARTADO4.ASPX"
            lbl_TamMax.Text = "El tamaño del documento no puede exceder los " + Session("TAMDIG") + " KB"
        End If

    End Sub

    'Protected Sub lnk_Regresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Regresar.Click

    '    Dim menuPanel As Panel
    '    menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

    '    If Not menuPanel Is Nothing Then
    '        menuPanel.Enabled = True
    '    End If

    '    EstatusEnDigitalizacion()
    '    Session("VENGODE") = "CNFEXP_APARTADO9.aspx"
    '    Response.Redirect("ADMEXPEDIENTE.aspx")

    'End Sub

    Private Sub EstatusEnDigitalizacion()

        Try
            'Bandera para mostrar si el exepdiente esta en uso
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_ESTATUS_EN_DIGITALIZACION"
            Session("cmd").Execute()

        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try

    End Sub

    Private Sub MostrarDocumentosNoDigitalizados()
        'Se llena el ListBox the Documentos requeridos o faltantes para completar el expediente 

        lst_DocNoDigi.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXP", Session("adVarChar"), Session("adParamInput"), 30, Session("ESTATUS_EXPEDIENTE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_TIPO_DOCUMENTOS_X_PRODUCTO"

        Session("rs") = Session("cmd").Execute()
        '(Session("rs").Fields("CLAVE").Value.ToString + " - " + 
        If Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Or Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS RECHAZADOS" Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCDOC").Value.ToString, Session("rs").Fields("TIPODOC").Value.ToString)
                lst_DocNoDigi.Items.Add(item)
                Session("rs").movenext()
            Loop
        Else
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCDOC").Value.ToString, Session("rs").Fields("TIPODOC").Value.ToString)
                lst_DocNoDigi.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

    End Sub

    Private Sub MostrarDocumentosNoDigitalizadosFase2()
        'Se llena el ListBox the Documentos requeridos o faltantes para completar el expediente 
        lst_DocNoDigi.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXP", Session("adVarChar"), Session("adParamInput"), 30, Session("ESTATUS_EXPEDIENTE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_TIPO_DOCUMENTOS_X_PRODUCTO_FASE2"

        Session("rs") = Session("cmd").Execute()
        '(Session("rs").Fields("CLAVE").Value.ToString + " - " + 
        If Session("ESTATUS_EXPEDIENTE") = "DIGITALIZACION FASE 2" Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCDOC").Value.ToString, Session("rs").Fields("TIPODOC").Value.ToString)
                lst_DocNoDigi.Items.Add(item)
                Session("rs").movenext()
            Loop
        Else
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCDOC").Value.ToString, Session("rs").Fields("TIPODOC").Value.ToString)
                lst_DocNoDigi.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub



    Private Sub MostrarDocumentosDigitalizados()

        ' Muestra los Documentos que ya han sido Digitalizados
        lst_DocDigi.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DOCUMENTOS_DIGITALIZADOS"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem((Session("rs").Fields("CATTIPDOC_DESCRIPCION").Value.ToString + " - " + Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value.ToString), Session("rs").Fields("MSTDOCEXP_CLAVE_DOCUMENTO").Value.ToString)
            lst_DocDigi.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub btn_Digitalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Digitalizar.Click

        Dim Posicion As Integer

        'Primero habilitara el dropdown para elegir un documento especifico para digitalizarlo (IFE, Pasaporte, Recibo de Luz, etc..)
        If lst_DocNoDigi.SelectedItem Is Nothing Then
            lbl_AlertaDigitaliza.Text = "Error: Seleccione un tipo de documento."
            lbl_AlertaDigitaliza.Visible = True
            lbl_AlertaNoBorrar.Text = ""
            lbl_AlertaVerBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
            lbl_FechaExp.Visible = False
        Else
            Posicion = lst_DocNoDigi.SelectedItem.Value().IndexOf(";")
            Session("CLAVE_DOCUMENTO") = Mid(lst_DocNoDigi.SelectedItem.Value(), Posicion + 2, 100)
            lbl_AlertaDigitaliza.Visible = False
            lst_DocumentosEspecificos.Visible = True
            txt_fechadoc.Text = ""
            LlenarDocumentos()
            lbl_FechaExp.Visible = False
        End If

    End Sub

    Private Sub LlenarDocumentos()

        'Llena la lista de Docuemntos (IFE, Recibo de Luz, etc...)
        lst_DocumentosEspecificos.Items.Clear()
        Dim elija As New ListItem("DOCUMENTO A ESCANEAR", "0")
        lst_DocumentosEspecificos.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPODOCUMENTO", Session("adVarChar"), Session("adParamInput"), 22, lst_DocNoDigi.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXP", Session("adVarChar"), Session("adParamInput"), 30, Session("ESTATUS_EXPEDIENTE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 30, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DOCUMENTOS_X_TIPO_DOCUMENTO"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value.ToString, Session("rs").Fields("CATDOCTOS_ID_DOCUMENTO").Value.ToString)
            lst_DocumentosEspecificos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub lst_DocumentosEspecificos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lst_DocumentosEspecificos.SelectedIndexChanged

        'Revisa si algun Documento fue seleccionado
        If lst_DocumentosEspecificos.SelectedItem.Value <> "0" Then
            btn_ElegirDocumento.Visible = True
            ' Variable de sesion para guardar el id del documento seleccionado
            Session("DOCUMENTOID") = lst_DocumentosEspecificos.SelectedItem.Value
            btn_Guardar.Enabled = True ' Habilita Boton Guardar
            btn_Borrar.Enabled = True ' Habilita Boton Borrar
            btn_Subir.Enabled = True
            FileUpload1.Enabled = True
            lbl_AlertaDigitaliza.Text = ""
            lbl_AlertaNoBorrar.Text = ""
            lbl_AlertaVerBorrar.Text = ""
            lbl_UploadEstatus.Visible = False

            lbl_AlertaDigitaliza.Visible = True

            DocumentoFechaExpiracion()

        End If

    End Sub

    Private Sub DocumentoFechaExpiracion()

        'Metodo para eliminar el Documento Seleccionado
        'No permite eliminar un documento que ya halla sido aprobado por validadores
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, lst_DocumentosEspecificos.SelectedValue.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCUMENTO_EXPIRA"
        Session("rs") = Session("cmd").Execute()

        Session("EXPIRA") = Session("rs").fields("EXPIRA").value.ToString

        If Session("EXPIRA").ToString = "SI EXPIRA" Then
            txt_fechadoc.Enabled = True
            req_fechadoc.Enabled = True
        Else
            txt_fechadoc.Enabled = False
            req_fechadoc.Enabled = False
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_ElegirDocumento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ElegirDocumento.Click

        btn_Borrar.Attributes.Add("onClick", "btnBorrar_onclick();")
        btn_Guardar.Attributes.Add("onClick", "btnUpload_onclick();")

    End Sub

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click

        If Session("EXPIRA").ToString = "SI EXPIRA" And txt_fechadoc.Text = "" Then
            lbl_FechaExp.Text = "Falta Dato!"
            lbl_FechaExp.Visible = True
            AsignaFechaDoc()
        Else
            AsignaFechaDoc()
            lbl_FechaExp.Visible = False
            If Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Then
                MostrarDocumentosNoDigitalizados()
            Else
                MostrarDocumentosNoDigitalizadosFase2()
            End If 'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
            MostrarDocumentosDigitalizados() 'Mostrara los documentos ya Dititalizados
            VerificarDocsCompletos() 'Verifica si ya estan digitalizados todos los requerimientos
            lst_DocumentosEspecificos.Visible = False
            btn_ElegirDocumento.Visible = False
            btn_Guardar.Enabled = False
            btn_Borrar.Enabled = False
            btn_Subir.Enabled = False
            FileUpload1.Enabled = False
            txt_fechadoc.Text = ""
        End If

    End Sub

    Private Sub AsignaFechaDoc()

        ' Funcion (truco) utilizada para agragra la fecha de documento en caso de la digitalizacion por TWAIN
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_DOC", Session("adVarChar"), Session("adParamInput"), 10, txt_fechadoc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_FECHA_EXPIRA_DOC"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub btn_Borrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Borrar.Click

        If Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Then
            MostrarDocumentosNoDigitalizados()
        Else
            MostrarDocumentosNoDigitalizadosFase2()
        End If 'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
        MostrarDocumentosDigitalizados() 'Mostrara los documentos ya Dititalizados
        VerificarDocsCompletos() 'Verifica si ya estan digitalizados todos los requerimientos
        lst_DocumentosEspecificos.Visible = False
        btn_ElegirDocumento.Visible = False
        btn_Guardar.Enabled = False
        btn_Borrar.Enabled = False
        btn_Subir.Enabled = False
        FileUpload1.Enabled = False

    End Sub

    Protected Sub btn_Ver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Ver.Click

        'Boton para ver un documento ya digitalizado y poder revisar un docuemnto con mas detalle
        If lst_DocDigi.SelectedItem Is Nothing Then
            lbl_AlertaVerBorrar.Text = "Error: Seleccione un documento."
            lbl_AlertaVerBorrar.Visible = True
            lbl_AlertaDigitaliza.Text = ""
            lbl_AlertaNoBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
        Else
            Session("DOCUMENTO_DIGITALIZADO") = lst_DocDigi.SelectedItem.Value() 'Variable de sesion para tener el docuemnto elegido
            lbl_AlertaVerBorrar.Text = ""

            Response.Redirect("~/DIGITALIZADOR/DIGI_MOSTRAR.aspx")

        End If

    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        'Boton para eliminar un Documento de la lista de Documentos Digitalizados
        If lst_DocDigi.SelectedItem Is Nothing Then
            lbl_AlertaVerBorrar.Text = "Error: Seleccione un documento."
            lbl_AlertaVerBorrar.Visible = True
            lbl_AlertaDigitaliza.Text = ""
            lbl_AlertaNoBorrar.Text = ""
            lbl_UploadEstatus.Visible = False
        Else
            Session("DOCUMENTO_DIGITALIZADO") = lst_DocDigi.SelectedItem.Value() 'Variable de sesion para tener el docuemnto elegido
            lbl_AlertaVerBorrar.Visible = False
            EliminarDocumentoDigitalizado() 'Metodo para eliminar el documento seleccionado
            If Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Or Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS RECHAZADOS" Then
                MostrarDocumentosNoDigitalizados()
            Else
                MostrarDocumentosNoDigitalizadosFase2()
            End If 'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
            MostrarDocumentosDigitalizados() 'Mostrara los documentos Dititalizados
            VerificarDocsCompletos() 'Verifica si los requisitos estan completos
        End If
    End Sub

    Private Sub EliminarDocumentoDigitalizado()

        'Metodo para eliminar el Documento Seleccionado
        'No permite eliminar un documento que ya halla sido aprobado por validadores
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTO_DIGITALIZADO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXPEDIENTE", Session("adVarChar"), Session("adParamInput"), 30, Session("ESTATUS_EXPEDIENTE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_DOCUMENTOS_DIGITALIZADOS"
        Session("rs") = Session("cmd").Execute()

        Dim AUX As Integer = Session("rs").Fields("ALERTA").Value
        If AUX = 0 Then
            lbl_AlertaNoBorrar.Text = "Error: El Documento ya esta Aceptado por validador, no puede ser borrado"
        End If

        Session("Con").Close()

    End Sub

    Private Sub VerificarDocsCompletos()

        'Metodo para saber si todos los documentos de un expediente estan completos
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EXPEDIENTE_COMPLETO_INCOMPLETO"
        Session("rs") = Session("cmd").Execute()
        Dim AUX As Integer = Session("rs").Fields("CONT").Value
        If AUX = 0 Then
            btn_Insertar_ColaValidacion.Enabled = True 'Si esta completo el expediente habilita el boton de insertar a la cola de validacion
        Else
            btn_Insertar_ColaValidacion.Enabled = False 'No esta completo el expediente no se habilita el boton
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_Insertar_ColaValidacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Insertar_ColaValidacion.Click

        'Boton para insertar en la cola de validacion
        'EstatusEnDigitalizacion()
        InsertarColaValidacion()
        AvisoCambioEstatus()
        'lbl_TerminoDigitFolio.Text = "Expediente listo para validacion con FOLIO: " + Session("FOLIO")
        'ModalPopupExtender.Show()
        Dim menuPanel As Panel
        menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

        If Not menuPanel Is Nothing Then
            menuPanel.Enabled = True
        End If

        Response.Redirect("~/CAPTACION/EXPEDIENTES/CAP_EXP_GENERAL.ASPX")



    End Sub

    Private Sub InsertarColaValidacion()

        'Insertar a la Cola de validacion para la fase 2
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_MSTVALIDACION"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()
    End Sub

    Protected Sub btn_subir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Subir.Click

        Try


            ' Read the file and convert it to Byte Array
            Dim filePath As String = FileUpload1.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim ext As String = Path.GetExtension(filename)
            Dim contenttype As String = String.Empty

            'Set the contenttype based on File Extension
            Select Case ext
                Case ".pdf"
                    contenttype = "application/pdf"
                    Exit Select
            End Select


            If contenttype = String.Empty Then

                lbl_UploadEstatus.Text = "El tipo de archivo no es reconocido (solo se aceptan *.PDF)"
                lbl_UploadEstatus.Visible = True

            Else

                Dim size As Integer = FileUpload1.PostedFile.ContentLength

                Dim Tamdig As Integer = Session("TAMDIG")

                'consulta Session("DOCUMENTOID").ToString para saber el tamaño Max

                'Stored Procedure 
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDDOCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTOID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_TAMANO_MAX_DIGITAL_X_DOCUMENTO"
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").EOF Then

                    If Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value = "EXPEDIENTE" Then
                        'Nuevo Valor
                        Tamdig = Session("rs").Fields("MAX_TAMANO_DIGIT").Value
                    Else
                        Tamdig = Session("TAMDIG")
                    End If
                    Session("Con").Close()
                    'lbl_titulo.Text = FileUpload1.PostedFile.ContentLength.ToString + " " + Session("DOCUMENTOID") + " " + (20170 * 1024).ToString
                End If



                If size >= (Tamdig * 1024) Then '20170
                    lbl_UploadEstatus.Text = "El archivo excede el maximo permitido para este documento."
                    lbl_UploadEstatus.Visible = True
                    lst_DocumentosEspecificos.Visible = False
                    btn_ElegirDocumento.Visible = False
                    btn_Guardar.Enabled = False
                    btn_Borrar.Enabled = False
                    btn_Subir.Enabled = False
                    FileUpload1.Enabled = False
                    Exit Sub

                End If


                If size <= (Tamdig * 1024) Then

                    Dim fs As Stream = FileUpload1.PostedFile.InputStream
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(fs.Length)

                    'lbl_titulo.Text = "" + CStr(size)

                    Dim strConnString As String 'Ruta de la BD
                    strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                    Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

                    Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("INS_MST_DOCEXP", sqlConnection)
                    'Stored Procedure 
                    sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

                    'Parametros para la incesrion del Stored Procedure
                    sqlCmdObj.Parameters.AddWithValue("@IDDOCUMENTO", Session("DOCUMENTOID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@FOLIO", Session("FOLIO").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@ARCHIVO", bytes)
                    sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@SESION", Session("Sesion").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@ESTATUS_EXP", Session("ESTATUS_EXPEDIENTE").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@CLAVE_DOCUMENTO", Session("CLAVE_DOCUMENTO").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@FECHA_DOC", txt_fechadoc.Text)

                    sqlConnection.Open()
                    sqlCmdObj.ExecuteNonQuery()
                    sqlConnection.Close()

                    lbl_UploadEstatus.Text = "El archivo ha sido guardado correctamente"
                    lbl_UploadEstatus.Visible = True



                End If

            End If



        Catch ex As Exception
        Finally
            If Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS POR DIGITALIZAR" Or Session("ESTATUS_EXPEDIENTE") = "DOCUMENTOS RECHAZADOS" Then
                MostrarDocumentosNoDigitalizados()
            Else
                MostrarDocumentosNoDigitalizadosFase2()
            End If 'Mostrara los requisitos (documentos) dentro de la lista de Doc aun No Dititalizados
            MostrarDocumentosDigitalizados() 'Mostrara los documentos ya Dititalizados
            VerificarDocsCompletos() 'Verifica si ya estan digitalizados todos los requerimientos
            lst_DocumentosEspecificos.Visible = False
            btn_ElegirDocumento.Visible = False
            btn_Guardar.Enabled = False
            btn_Borrar.Enabled = False
            btn_Subir.Enabled = False
            FileUpload1.Enabled = False
        End Try

    End Sub

    Private Sub AvisoCambioEstatus()

        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim usuario As String = String.Empty
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
        Do While Not Session("rs").EOF

            usuario = Session("rs").Fields("USUARIO").Value.ToString
            contenido = Session("rs").Fields("CONTENIDO").Value.ToString
            subject = "CAMBIO DE ESTATUS DE EXPEDIENTE (" + CStr(Session("FOLIO")) + ")"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a):  " + usuario + +contenido + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
        Loop

        Session("Con").Close()
    End Sub

End Class