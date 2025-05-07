Imports System.IO
Public Class DIGI_GLOBAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Not Me.IsPostBack Then

            'Inicializa los atributos de las acciones de digitalizacion
            btn_ElegirDocumento.Attributes.Add("onClick", "btnScan_onclick();return false;")
            btn_Borrar.Attributes.Add("onClick", "btnBorrar_onclick();")

            If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
                btn_Guardar.Attributes.Add("onClick", "btnUploadGarantia_onclick();")
                lbl_ArchPermitidos.Text = "Nota: Solo se permite subir los siguientes tipos de archivo (.doc, .docx, .jpg, .png, .gif, .pdf)"
            End If

            If Session("VENGODE").ToString = "CRED_EXP_EXPEDIENTE.ASPX" Or Session("VENGODE").ToString = "CRED_EXP_ANA_FASE1.ASPX" Then
                btn_Guardar.Attributes.Add("onClick", "btnUploadComite_onclick();")
                lbl_ArchPermitidos.Text = "Nota: Solo se permite subir los siguientes tipos de archivo (.pdf)"
            End If


            If Session("VENGODE").ToString = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then
                btn_Guardar.Attributes.Add("onClick", "btnUpload_onclick();")
                lbl_ArchPermitidos.Text = "Nota: Solo se permite subir los siguientes tipos de archivo (.pdf)"
                Session("VENGO") = "DIGI_GLOBAL"

                Session("OTRO") = Session("OTRO")

            End If

            MostrarDocumentosNoDig()
            MostrarDocumentosDig()

            lbl_TamMax.Text = "El tamaño del documento no puede exceder los " + Session("TAMDIG") + " KB"

        End If

    End Sub

#Region "Digitalizar"
    Private Sub MostrarDocumentosDig()
        'Se llena el ListBox the Documentos Digitalizados
        If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
            lst_DocDigi.Items.Clear()
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CVE_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_GARANTIAS_DOCS_DIGIT"

            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DOCUMENTO").Value.ToString, Session("rs").Fields("CVE").Value.ToString)
                lst_DocDigi.Items.Add(item)
                Session("rs").movenext()
            Loop

            Session("Con").Close()
        End If


        If Session("VENGODE").ToString = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then
            'lbl_UploadEstatus.Text = Session("FOLIO").ToString + " " + Session("DOCUMENTO_DIGITALIZADO").ToString
            lbl_UploadEstatus.Text = "el nombre del archivo es:" + Session("NOMBRE_ARCHIVO").ToString

            lst_DocDigi.Items.Clear()

            If Session("OTRO") = "NO" Then

                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NOMBRE_DOC", Session("adVarChar"), Session("adParamInput"), 50, Session("NOMBRE_ARCHIVO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CONEXP_EDITAR_DOCUMENTO"
                Session("rs") = Session("cmd").Execute()

                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("DOCUMENTO").Value.ToString, (Session("rs").Fields("CLAVE").Value.ToString + "-" + Session("rs").Fields("NOMBRE").Value.ToString))
                    lst_DocDigi.Items.Add(item)
                    Session("rs").movenext()
                Loop

                Session("Con").Close()
            ElseIf Session("OTRO") = "SI" Then

                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CONEXP_OTROS_DOCUMENTOS"
                Session("rs") = Session("cmd").Execute()

                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("DOCUMENTO").Value.ToString, (Session("rs").Fields("CLAVE").Value.ToString + "-" + Session("rs").Fields("NOMBRE").Value.ToString))
                    lst_DocDigi.Items.Add(item)
                    Session("rs").movenext()
                Loop

                Session("Con").Close()
            End If
        End If


    End Sub

    Private Sub MostrarDocumentosNoDig()
        'Se llena el ListBox the Documentos Aun no Digitalizados
        If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
            cmb_DocNoDigi.Items.Clear()
            Dim elija As New ListItem("ELIJA", "0")
            cmb_DocNoDigi.Items.Add(elija)

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CVE_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_GARANTIAS_DOCS_NO_DIGIT"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DOCUMENTO").Value.ToString, Session("rs").Fields("CVE").Value.ToString)
                cmb_DocNoDigi.Items.Add(item)
                Session("rs").movenext()
            Loop

            Session("Con").Close()
        End If

        If Session("VENGODE").ToString = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then
            cmb_DocNoDigi.Items.Clear()
            Dim elija As New ListItem("ELIJA", "0")
            cmb_DocNoDigi.Items.Add(elija)

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("TIPODOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTO_DIGITALIZADO_GRAL").ToString + ";0")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS_EXP", Session("adVarChar"), Session("adParamInput"), 30, "COMITE")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 30, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DOCUMENTOS_X_TIPO_DOCUMENTO"

            Session("rs") = Session("cmd").Execute()


            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("CATDOCTOS_DOCUMENTO").Value.ToString, Session("rs").Fields("CATDOCTOS_ID_DOCUMENTO").Value.ToString)
                cmb_DocNoDigi.Items.Add(item)
                Session("rs").movenext()
            Loop

            Session("Con").Close()
        End If
    End Sub

    Private Sub btn_Borrar_Click(sender As Object, e As EventArgs) Handles btn_Borrar.Click
        MostrarDocumentosDig() 'Mostrara los documentos ya Dititalizados
        MostrarDocumentosNoDig() 'Mostrara los documentos aun no Dititalizados
        btn_Borrar.Enabled = False
        btn_Guardar.Visible = False
        btn_ElegirDocumento.Enabled = False
        btn_Upload.Enabled = False
        FileUpload1.Enabled = False
        lbl_UploadEstatus.Text = ""
    End Sub

    Private Sub cmb_DocNoDigi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DocNoDigi.SelectedIndexChanged
        lbl_UploadEstatus.Text = ""
        lbl_AlertaNoBorrar.Text = ""
        If cmb_DocNoDigi.SelectedItem.Value = 0 Then
            btn_Borrar.Enabled = False
            btn_Guardar.Enabled = False
            btn_ElegirDocumento.Enabled = False
            btn_Upload.Enabled = False
            FileUpload1.Enabled = False
        Else
            Session("DOCUMENTOID") = cmb_DocNoDigi.SelectedItem.Value
            Session("NOMBRE_DOCUMENTO") = cmb_DocNoDigi.SelectedItem.Text
            btn_Borrar.Enabled = True
            btn_Guardar.Enabled = True
            btn_ElegirDocumento.Enabled = True
            btn_Upload.Enabled = True
            FileUpload1.Enabled = True
        End If
    End Sub

    Private Sub btn_ElegirDocumento_Click(sender As Object, e As EventArgs) Handles btn_ElegirDocumento.Click
        btn_Borrar.Attributes.Add("onClick", "btnBorrar_onclick();")

        If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
            btn_Guardar.Attributes.Add("onClick", "btnUploadGarantia_onclick();")
        End If


        If Session("VENGODE") = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then
            btn_Guardar.Attributes.Add("onClick", "btnUpload_onclick();")
            lbl_ArchPermitidos.Text = "Nota: Solo se permite subir los siguientes tipos de archivo (.pdf)"
        End If

    End Sub

    Private Sub btn_Upload_Click(sender As Object, e As EventArgs) Handles btn_Upload.Click
        lbl_UploadEstatus.Text = ""
        'lbl_titulo.Text = "entra a guardar"
        ' Read the file and convert it to Byte Array
        Dim filePath As String = FileUpload1.PostedFile.FileName
        Dim filename As String = Path.GetFileName(filePath)
        Dim ext As String = Path.GetExtension(filename)
        Dim contenttype As String = String.Empty

        'Set the contenttype based on File Extension
        Select Case ext
            Case ".pdf"
                contenttype = ".pdf"
                Exit Select
        End Select

        If contenttype <> String.Empty Then

            Dim size As Integer = FileUpload1.PostedFile.ContentLength

            If size <= (Session("TAMDIG") * 1024) Then

                'Set the contenttype based on File Extension
                Select Case ext
                    Case ".pdf"
                        contenttype = "application/pdf"
                        Exit Select
                End Select

                If contenttype = String.Empty Then

                    lbl_UploadEstatus.Text = "Error: El tipo de archivo no es reconocido (solo se aceptan *.PDF)"
                    lbl_UploadEstatus.Visible = True

                Else

                    Dim Tamdig As Integer = Session("TAMDIG")

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
                    End If

                    If size >= (Tamdig * 1024) Then '20170
                        lbl_UploadEstatus.Text = "El archivo excede el máximo permitido para este documento."
                        lbl_UploadEstatus.Visible = True
                        btn_ElegirDocumento.Visible = False
                        btn_Guardar.Enabled = False
                        btn_Borrar.Enabled = False
                        btn_Upload.Enabled = False
                        FileUpload1.Enabled = False
                        Exit Sub

                    End If

                    If size <= (Tamdig * 1024) Then

                        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urldigi")
                        Dim cPath As String = urldigi + "\"


                        If Session("OTRO") = "SI" Then

                            Dim rnd As New Random()
                            Dim value As Integer = CInt(rnd.Next(101, 999))

                            Dim Fecha As String = CStr(Format(CDate(Today), "yyyyMMdd"))
                            Dim Hora As String = CStr(Format(CDate(TimeOfDay), "HHmmss"))
                            Dim nombrearchivo As String = Fecha + Hora + Left(Session("FOLIO").ToString, 1) + value.ToString

                            Session("NOMBRE_ARCHIVO") = nombrearchivo


                        Else

                            DelHDFile(cPath + Session("NOMBRE_ARCHIVO").ToString)
                        End If


                        FileUpload1.SaveAs(cPath + Session("NOMBRE_ARCHIVO").ToString)
                        Session("ID_TIPO_DOC") = cmb_DocNoDigi.SelectedItem.Value.ToString()
                        lbl_UploadEstatus.Visible = True
                    End If

                End If

                lbl_UploadEstatus.Text = "Guardado correctamente"
                lbl_UploadEstatus.Visible = True
            Else
                lbl_UploadEstatus.Text = "Error: El tipo de archivo no es reconocido"
                lbl_UploadEstatus.Visible = True
            End If

        End If


        Dim fs As Stream = FileUpload1.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(fs.Length)

                Dim strConnString As String 'Ruta de la BD
                strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
                    Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
                    Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("UPD_CNFEXP_DOC_GARANTIA", sqlConnection)
                    'Stored Procedure 
                    sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

                    'Parametros para la incesrion del Stored Procedure
                    sqlCmdObj.Parameters.AddWithValue("@CVE_GARANTIA", Session("CVEGARANTIA").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@TIPO_GARANTIA", Session("TIPOGARANTIA"))
                    sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOCUMENTO", Session("NOMBRE_DOCUMENTO").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@ARCHIVO", bytes)
                    sqlCmdObj.Parameters.AddWithValue("@USERID", Session("USERID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@SESION", Session("Sesion").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@EXTENSION", contenttype)
                    sqlCmdObj.Parameters.AddWithValue("@FOLIO", Session("FOLIO").ToString)
                    sqlConnection.Open()
                    sqlCmdObj.ExecuteNonQuery()
                    sqlConnection.Close()
                End If


        If Session("VENGODE").ToString = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then

            If Session("OTRO") = "SI" Then


                strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

                Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("INS_MST_DOCEXP", sqlConnection)
                'Stored Procedure 
                sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

                'Parametros para la incesrion del Stored Procedure
                sqlCmdObj.Parameters.AddWithValue("@IDDOCUMENTO", Session("DOCUMENTOID").ToString)
                sqlCmdObj.Parameters.AddWithValue("@FOLIO", Session("FOLIO").ToString)
                sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID").ToString)
                sqlCmdObj.Parameters.AddWithValue("@SESION", Session("Sesion").ToString)
                sqlCmdObj.Parameters.AddWithValue("@ESTATUS_EXP", "COMITE")
                sqlCmdObj.Parameters.AddWithValue("@CLAVE_DOCUMENTO", Session("CLAVE").ToString)
                sqlCmdObj.Parameters.AddWithValue("@FECHA_DOC", "")
                sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOC", Session("NOMBRE_ARCHIVO").ToString)
                sqlCmdObj.Parameters.AddWithValue("@EXTENSION", "pdf")

                sqlConnection.Open()
                sqlCmdObj.ExecuteNonQuery()
                sqlConnection.Close()
            Else

                Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
                Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("UPD_DOCUMENTO_DIG", sqlConnection)
                'Stored Procedure 
                sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

                'Parametros para la incesrion del Stored Procedure
                sqlCmdObj.Parameters.AddWithValue("@CLAVE", Session("CLAVE").ToString)
                sqlCmdObj.Parameters.AddWithValue("@FOLIO", Session("FOLIO").ToString)
                sqlCmdObj.Parameters.AddWithValue("@ID_TIPO_DOC", Session("ID_TIPO_DOC").ToString)
                sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOC", Session("NOMBRE_ARCHIVO").ToString)


                sqlConnection.Open()
                sqlCmdObj.ExecuteNonQuery()
                sqlConnection.Close()
            End If
        End If

        MostrarDocumentosNoDig()
        MostrarDocumentosDig()

        btn_Borrar.Enabled = False
        btn_Guardar.Enabled = False
        btn_ElegirDocumento.Enabled = False
        btn_Upload.Enabled = False
        FileUpload1.Enabled = False
    End Sub

    Private Sub btn_Guardar_Click(sender As Object, e As EventArgs) Handles btn_Guardar.Click
        MostrarDocumentosDig() 'Mostrara los documentos ya Digitalizados
        MostrarDocumentosNoDig() 'Mostrara los documentos aun no Dititalizados
        btn_Borrar.Enabled = False
        btn_Guardar.Enabled = False
        btn_ElegirDocumento.Enabled = False
        btn_Upload.Enabled = False
        FileUpload1.Enabled = False

    End Sub

    Private Sub btn_Ver_Click(sender As Object, e As EventArgs) Handles btn_Ver.Click
        'Boton para ver un documento ya digitalizado y poder revisar un docuemnto con mas detalle
        If lst_DocDigi.SelectedItem Is Nothing Then
            lbl_AlertaVerBorrar.Text = "Error: Seleccione un documento."
            lbl_AlertaVerBorrar.Visible = True
            lbl_AlertaNoBorrar.Text = ""
            lbl_UploadEstatus.Text = ""
        Else
            Dim delimitador As String = "-"
            Session("NOMBRE_DOCUMENTO") = lst_DocDigi.SelectedItem.Text 'Variable de sesion para tener el tipo de documento elegido
            Dim arr As String() = lst_DocDigi.SelectedItem.Value.ToString.Split(delimitador)
            Session("DOCUMENTO_DIGITALIZADO") = arr(0) 'Identificador (clave) del documento
            Session("NOMBRE_ARCHIVO") = arr(1) ' Nombre del documento
            Session("IDSESCOMIT") = Session("IDSESCOMIT")

            lbl_AlertaVerBorrar.Text = ""
            lbl_UploadEstatus.Text = ""

            If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
                Response.Redirect("DIGI_MOSTRAR_GARANTIAS.aspx")
            End If

            If Session("VENGODE").ToString = "CRED_EXP_EXPEDIENTE.ASPX" Or Session("VENGODE").ToString = "CRED_EXP_ANA_FASE1.ASPX" Then
                Response.Redirect("DIGI_MOSTRAR_COMITE.aspx")
            End If

            If Session("VENGODE").ToString = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then
                Response.Redirect("DIGI_MOSTRAR.aspx")
            End If


        End If

    End Sub

    Private Sub btn_Eliminar_Click(sender As Object, e As EventArgs) Handles btn_Eliminar.Click
        'Boton para eliminar un Documento de la lista de Documentos Digitalizados
        If lst_DocDigi.SelectedItem Is Nothing Then
            lbl_AlertaVerBorrar.Text = "Error: Seleccione un documento."
            lbl_AlertaVerBorrar.Visible = True
            lbl_AlertaNoBorrar.Text = ""
        Else
            'Session("DOCUMENTO_DIGITALIZADO") = lst_DocDigi.SelectedItem.Value.ToString
            Dim delimitador As String = "-"
            Dim arr As String() = lst_DocDigi.SelectedItem.Value.ToString.Split(delimitador)
            Session("DOCUMENTO_DIGITALIZADO") = arr(0) 'Identificador (clave) del documento
            Session("NOMBRE_ARCHIVO") = arr(1) ' Nombre del documento
            EliminarDocumentoDigitalizado() 'Metodo para eliminar el documento seleccionado
            MostrarDocumentosDig() 'Mostrara los documentos ya Dititalizados
            MostrarDocumentosNoDig() 'Mostrara los documentos aun no Dititalizados
            btn_Borrar.Enabled = False
            btn_Guardar.Enabled = False
            btn_ElegirDocumento.Enabled = False
            btn_Upload.Enabled = False
            FileUpload1.Enabled = False
            lbl_AlertaVerBorrar.Text = ""
            lbl_UploadEstatus.Text = ""

        End If
    End Sub

    Private Sub EliminarDocumentoDigitalizado()
        'Metodo para eliminar el Documento Seleccionado
        'No permite eliminar un documento que ya halla sido aprobado por validadores
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()

        If Session("VENGODE").ToString = "CNFEXP_APARTADO5.aspx" Then
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CVE_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE_DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 100, lst_DocDigi.SelectedItem.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_DOCUMENTOS_DIGITALIZADOS_GARANTIAS"
            Session("rs") = Session("cmd").Execute()
        End If


        If Session("VENGODE").ToString = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx" Then
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE_DOC", Session("adVarChar"), Session("adParamInput"), 50, Session("NOMBRE_ARCHIVO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_DOCUMENTOS_DIGITALES"
            Session("rs") = Session("cmd").Execute()

        End If
        Session("Con").Close()

        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urldigi")
        Dim cPath As String = urldigi + "\"
        DelHDFile(cPath + Session("NOMBRE_ARCHIVO").ToString)

    End Sub

#End Region

    Private Sub DelHDFile(ByVal File1 As String)
        If File.Exists(File1) Then
            Dim fi As New System.IO.FileInfo(File1)
            If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
                fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
            End If
        Else
            lbl_AlertaNoBorrar.Text = "Alerta: El archivo ha sido movido o eliminado"
        End If
        System.IO.File.Delete(File1)
    End Sub

    Protected Sub lnk_Regresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_regresar.Click

        Session("VENGODE") = "DIGI_GLOBAL"
        Response.Redirect("../CREDITO/EXPEDIENTES/CRED_EXP_CONSULTAEXP_DIGITAL.aspx")

    End Sub


End Class