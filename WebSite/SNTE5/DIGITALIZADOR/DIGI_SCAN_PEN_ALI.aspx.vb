Imports System.IO
Public Class DIGI_SCAN_PEN_ALI
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim FechaSistema As Date = Convert.ToDateTime(Session("FechaSis").ToString)

        Dim rnd As New Random()
        Dim value As Integer = rnd.Next(101, 999)

        Dim files As HttpFileCollection = HttpContext.Current.Request.Files
        Dim uploadfile As HttpPostedFile = files("RemoteFile")

        Dim iFileLength = uploadfile.ContentLength

        Dim inputBuffer(iFileLength) As Byte
        Dim inputStream As Stream

        inputStream = uploadfile.InputStream
        inputStream.Read(inputBuffer, 0, iFileLength)

        'Varios Query String con informacion necesaria lara las inserciones a la BD llamadas desde un Java Script
        Dim Lime As String = Request.QueryString("Lime")
        Dim Lime2 As String = Request.QueryString("Lime2")
        Dim Lime3 As String = Request.QueryString("Lime3")
        Dim Lime4 As String = Request.QueryString("Lime4")

        Dim Fecha As String = Format(Convert.ToDateTime(Lime4), "yyyyMMdd")
        Dim nombrearchivo As String = "PENALI" + Fecha + Lime + value.ToString

        Session("NOMBRE_ARCHIVO") = nombrearchivo

        Dim fs As Stream = uploadfile.InputStream
        Dim br As New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(fs.Length)
        Dim SaveFile As String = Server.MapPath("/DOCUMENTOS_DIGITALIZADOS/" + nombrearchivo.ToString)

        uploadfile.SaveAs(SaveFile)

        Dim strConnString As String 'Ruta de la BD
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New SqlClient.SqlCommand("UPD_PEN_ALI_DOC", sqlConnection)

        sqlCmdObj.CommandType = CommandType.StoredProcedure

        'Parametros para la incesrion del Stored Procedure
        sqlCmdObj.Parameters.AddWithValue("@IDPENSION", Lime)
        sqlCmdObj.Parameters.AddWithValue("@IDUSER", Lime2)
        sqlCmdObj.Parameters.AddWithValue("@SESION", Lime3)
        sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOC", nombrearchivo)
        sqlCmdObj.Parameters.AddWithValue("@EXTENSION", "pdf")
        sqlConnection.Open()
        sqlCmdObj.ExecuteNonQuery()
        sqlConnection.Close()

    End Sub

End Class