Imports Microsoft.Office.Interop.Word

Public Class DIGI_MOSTRAR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("FOLIO") = 0 Then
            VerDocumentoPersona()
        End If

        VerDocumentoDigitalizado()
    End Sub

    Private Sub VerDocumentoDigitalizado()
        Dim RutaDigitaliza As String = ConfigurationManager.AppSettings.[Get]("urldigi")
        Dim Nombre As String = ""
        Dim Ext As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("DOCUMENTO_DIGITALIZADO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_NOMBRE_DOCUMENTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Nombre = Session("rs").Fields("NOMBRE").Value
            Ext = Session("rs").Fields("EXTENSION").Value
        End If
        Session("Con").close()

        If Nombre <> "" And Ext <> "" Then
            With Response
                Try
                    Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urldigi")
                    Dim cPath As String = urldigi + "\"
                    Dim archivo As String = Nombre
                    Dim ResultDocName As String = archivo + ".pdf"

                    Dim Filename As String = archivo + ".pdf"
                    Dim FilePath As String = cPath
                    Dim fs As System.IO.FileStream
                    fs = System.IO.File.Open(FilePath + archivo, System.IO.FileMode.Open)
                    Dim bytBytes(fs.Length) As Byte
                    fs.Read(bytBytes, 0, fs.Length)
                    fs.Close()

                    Response.Buffer = True
                    Response.Clear()
                    Response.ClearContent()
                    Response.ClearHeaders()
                    Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", ResultDocName))
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.BinaryWrite(bytBytes)
                    Response.End()
                Catch ex As Exception
                    estatus.Text = ex.ToString
                End Try
            End With

        Else
            estatus.Text = "Error: No existe un registro guardado en base de datos para el archivo"
        End If

    End Sub

    Private Sub VerDocumentoPersona()

        Dim strConnString As String 'Ruta de la BD
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_DOC_PERSONA", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@DOCUMENTO", Session("DOCUMENTO_DIGITALIZADO"))
        If Session("DETALLEVENGODE") = Nothing Or Session("DETALLEVENGODE") = "" Then
            sqlCmdObj.Parameters.AddWithValue("@IDPERSONA", Session("PERSONAID"))
        Else
            sqlCmdObj.Parameters.AddWithValue("@IDPERSONA", Session("PERSONAIDAUX"))
        End If

        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        Dim iByteLength As Long
        iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)
        Dim byFileData(iByteLength) As Byte
        sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        sdrRecordset.Close()
        sqlConnection.Close()

        'Obtiene los bytes (imagen) y las despliega con Adobe Reader por el formato PDF 
        Dim ms As New System.IO.MemoryStream()
        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition", "attachment; filename=Auxiliar.pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            .OutputStream.Write(byFileData, 0, byFileData.Length)
            .End()
        End With

    End Sub

End Class