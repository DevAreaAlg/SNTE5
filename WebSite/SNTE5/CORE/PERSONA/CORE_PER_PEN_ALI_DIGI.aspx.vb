Imports System.IO
Public Class CORE_PER_PEN_ALI_DIGI
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            InformacionAgremiado()
            btn_escanear_doc.Attributes.Add("onClick", "btnScan_onclick(); return false;")
            btn_guardar_doc.Attributes.Add("onClick", "btnUpload_onclick();")
            btn_borrar_doc.Attributes.Add("onClick", "btnBorrar_onclick();")
            lbl_tam_max.Text = "Nota: El tamaño del documento no puede exceder los " + Session("TAMDIG") + " KB"

        End If

    End Sub

#Region "Eventos"

    Protected Sub lnk_regresar_lista_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_regresar_lista.Click

        Response.Redirect("CORE_PER_PEN_ALI.aspx")

    End Sub

    Protected Sub lnk_regresar_agremiado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_regresar_agremiado.Click

        Response.Redirect("CORE_PER_PEN_ALI_AGREMIADO.aspx")

    End Sub

    Protected Sub btn_guardar_doc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar_doc.Click

        lbl_estatus.Text = "Éxito: Digitalización concluida."
        InformacionAgremiado()

    End Sub

    Protected Sub btn_ver_doc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_ver_doc.Click

        VerDocumento()

    End Sub

    Protected Sub btn_eliminar_doc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_eliminar_doc.Click

        EliminarDocumento()
        InformacionAgremiado()

    End Sub

    Protected Sub btn_subir_doc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_subir_doc.Click

        If fud_subir_doc.HasFile = False Then
            lbl_estatus.Text = "Error: Debe seleccionar un archivo."
            Exit Sub
        End If

        If Path.GetExtension(fud_subir_doc.FileName.ToUpper()) = ".PDF" Then
            SubirDocumento()
        Else
            lbl_estatus.Text = "Error: Debe seleccionar un archivo tipo .PDF."
            Exit Sub
        End If

    End Sub

#End Region

    Private Sub InformacionAgremiado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPENSION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPENSION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PEN_ALI_DOC"
        Session("rs") = Session("cmd").Execute()
        tbx_id_pension.Text = Session("IDPENSION").ToString
        tbx_rfc_agremiado.Text = Session("rs").fields("RFC").value.ToString
        tbx_nombre_agremiado.Text = Session("rs").fields("DEMANDADO").value.ToString
        tbx_region.Text = Session("rs").fields("REGION").value.ToString
        tbx_delegacion.Text = Session("rs").fields("DELEGACION").value.ToString
        tbx_ct.Text = Session("rs").fields("CT").value.ToString
        tbx_beneficiario.Text = Session("rs").fields("BENEFICIARIO").value.ToString
        tbx_estatus.Text = Session("rs").fields("ESTATUS").value.ToString
        tbx_nombre_doc.Text = Session("rs").fields("DOCUMENTO").value.ToString
        Session("Con").Close()

        If tbx_nombre_doc.Text = "" Then
            btn_eliminar_doc.Enabled = False
            btn_ver_doc.Enabled = False
            btn_subir_doc.Enabled = True
            btn_escanear_doc.Enabled = True
            btn_guardar_doc.Enabled = True
            btn_borrar_doc.Enabled = True
        Else
            btn_eliminar_doc.Enabled = True
            btn_ver_doc.Enabled = True
            btn_subir_doc.Enabled = False
            btn_escanear_doc.Enabled = False
            btn_guardar_doc.Enabled = False
            btn_borrar_doc.Enabled = False
        End If

    End Sub

    Private Sub VerDocumento()

        Dim Ruta As String = Server.MapPath("/DOCUMENTOS_DIGITALIZADOS/" + tbx_nombre_doc.Text.ToString)

        If File.Exists(Ruta) Then

            lbl_estatus.Text = ""

            Try

                Dim fs As FileStream
                fs = File.Open(Ruta, FileMode.Open)
                Dim bytBytes(fs.Length) As Byte
                fs.Read(bytBytes, 0, fs.Length)
                fs.Close()

                Response.Buffer = True
                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", tbx_nombre_doc.Text.ToString + ".pdf"))
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(bytBytes)
                Response.End()

            Catch ex As Exception
                lbl_estatus.Text = ex.ToString
            End Try
        Else
            lbl_estatus.Text = "Error: El archivo ha sido movido o eliminado."
        End If

    End Sub

    Private Sub EliminarDocumento()

        Dim strConnString As String 'Ruta de la BD
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New SqlClient.SqlCommand("UPD_PEN_ALI_DOC", sqlConnection)
        sqlCmdObj.CommandType = CommandType.StoredProcedure
        sqlCmdObj.Parameters.AddWithValue("@IDPENSION", tbx_id_pension.Text.ToString)
        sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID").ToString)
        sqlCmdObj.Parameters.AddWithValue("@SESION", Session("Sesion").ToString)
        sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOC", "")
        sqlCmdObj.Parameters.AddWithValue("@EXTENSION", "")
        sqlConnection.Open()
        sqlCmdObj.ExecuteNonQuery()
        sqlConnection.Close()

        Dim Ruta As String = Server.MapPath("/DOCUMENTOS_DIGITALIZADOS/" + tbx_nombre_doc.Text.ToString)

        lbl_estatus.Text = "Éxito: El documento ha sido eliminado."

        DelHDFile(Ruta)

    End Sub

    Private Sub DelHDFile(ByVal FilePath As String)

        If File.Exists(FilePath) Then
            Dim fi As New FileInfo(FilePath)
            If (fi.Attributes And FileAttributes.ReadOnly) <> 0 Then
                fi.Attributes = fi.Attributes Xor FileAttributes.ReadOnly
            End If
        Else
            lbl_estatus.Text = "Error: El archivo ha sido movido o eliminado."
        End If

        File.Delete(FilePath)

    End Sub

    Private Sub SubirDocumento()

        Try

            Dim rnd As New Random()
            Dim value As Integer = rnd.Next(101, 999)
            Dim Fecha As String = Format(Convert.ToDateTime(MyBase.Session("FechaSis").ToString), "yyyyMMdd")
            Dim nombrearchivo As String = "PENALI" + Fecha + tbx_id_pension.Text.ToString + value.ToString

            Dim filePath As String = fud_subir_doc.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim ext As String = Path.GetExtension(filename)
            Dim contenttype As String = String.Empty

            Select Case ext
                Case ".pdf"
                    contenttype = "application/pdf"
                    Exit Select
            End Select

            If contenttype = String.Empty Then

                lbl_estatus.Text = "Error: El tipo de archivo no es reconocido (solo se aceptan *.PDF)."

            Else

                Dim size As Integer = fud_subir_doc.PostedFile.ContentLength
                Dim Tamdig As Integer = Session("TAMDIG")

                If size >= (Tamdig * 1024) Then '20170

                    lbl_estatus.Text = "Error: El archivo excede el máximo permitido."
                    Exit Sub

                End If

                If size <= (Tamdig * 1024) Then

                    Dim fs As Stream = fud_subir_doc.PostedFile.InputStream
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(fs.Length)
                    Dim SaveFile As String = Server.MapPath("/DOCUMENTOS_DIGITALIZADOS/" + nombrearchivo.ToString)

                    fud_subir_doc.SaveAs(SaveFile)

                    Dim strConnString As String 'Ruta de la BD
                    strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                    Dim sqlConnection As New SqlClient.SqlConnection(strConnString)

                    Dim sqlCmdObj As New SqlClient.SqlCommand("UPD_PEN_ALI_DOC", sqlConnection)
                    'Stored Procedure 
                    sqlCmdObj.CommandType = CommandType.StoredProcedure

                    'Parametros para la incesrion del Stored Procedure
                    sqlCmdObj.Parameters.AddWithValue("@IDPENSION", tbx_id_pension.Text.ToString)
                    sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@SESION", Session("Sesion").ToString)
                    sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOC", nombrearchivo.ToString)
                    sqlCmdObj.Parameters.AddWithValue("@EXTENSION", "pdf")

                    sqlConnection.Open()
                    sqlCmdObj.ExecuteNonQuery()
                    sqlConnection.Close()

                    lbl_estatus.Text = "Éxito: Digitalización concluida."

                End If

            End If

        Catch ex As Exception
            lbl_estatus.Text = ex.ToString
        Finally
            InformacionAgremiado()
        End Try

    End Sub

End Class