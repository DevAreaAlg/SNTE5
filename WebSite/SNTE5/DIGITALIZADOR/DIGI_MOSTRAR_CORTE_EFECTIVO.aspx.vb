Public Class DIGI_MOSTRAR_CORTE_EFECTIVO
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Archivo que me permite abrir el documento dentro de la base de datos
        VerDocumentoDigitalizado()
    End Sub

    Private Sub VerDocumentoDigitalizado()

        Dim extension As String
        Dim strConnString As String

        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_CORTE_CAJA", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@IDCAJA", Session("IDCAJA"))
        sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOCUMENTO", Session("NOMBRE_DOCUMENTO"))
        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        extension = sdrRecordset.GetString(1)

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