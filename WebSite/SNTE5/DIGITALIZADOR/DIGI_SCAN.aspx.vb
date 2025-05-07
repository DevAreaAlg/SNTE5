Imports System.IO
Public Class DIGI_SCAN
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Se encargara de _Guardar a la BD el archivo en bytes y demas informacion a la tabla de documentos de expediente
        Dim rnd As New Random()
        Dim value As Integer = CInt(rnd.Next(101, 999))

        Dim files As HttpFileCollection = HttpContext.Current.Request.Files
        Dim uploadfile As HttpPostedFile = files("RemoteFile")

        Dim iFileLength = uploadfile.ContentLength

        Dim inputBuffer(iFileLength) As Byte
        Dim inputStream As System.IO.Stream

        inputStream = uploadfile.InputStream
        inputStream.Read(inputBuffer, 0, iFileLength)

        'Varios Query String con informacion necesaria lara las inserciones a la BD llamadas desde un Java Script
        Dim Lime As String = Request.QueryString("Lime")
        Dim Lime2 As String = Request.QueryString("Lime2")
        Dim Lime3 As String = Request.QueryString("Lime3")
        Dim Lime4 As String = Request.QueryString("Lime4")
        Dim Lime5 As String = Request.QueryString("Lime5")
        Dim Lime6 As String = Request.QueryString("Lime6")
        Dim Lime7 As String = Request.QueryString("Lime7")

        Dim Fecha As String = CStr(Format(CDate(Today), "yyyyMMdd"))
        Dim Hora As String = CStr(Format(CDate(TimeOfDay), "HHmmss"))
        Dim nombrearchivo As String = Fecha + Hora + Left(Lime, 1) + value.ToString

        Session("NOMBRE_ARCHIVO") = nombrearchivo

        Dim fs As Stream = uploadfile.InputStream
        Dim br As New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(fs.Length)
        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urldigi")

        uploadfile.SaveAs(urldigi + "\" + nombrearchivo)

        Dim strConnString As String 'Ruta de la BD
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("INS_MST_DOCEXP", sqlConnection)

        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        'Parametros para la incesrion del Stored Procedure
        sqlCmdObj.Parameters.AddWithValue("@IDDOCUMENTO", Lime2)
        sqlCmdObj.Parameters.AddWithValue("@FOLIO", Lime)
        sqlCmdObj.Parameters.AddWithValue("@IDUSER", Lime3)
        sqlCmdObj.Parameters.AddWithValue("@SESION", Lime4)
        sqlCmdObj.Parameters.AddWithValue("@ESTATUS_EXP", Lime5)
        sqlCmdObj.Parameters.AddWithValue("@CLAVE_DOCUMENTO", Lime6)
        sqlCmdObj.Parameters.AddWithValue("@FECHA_DOC", Lime7)
        sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOC", nombrearchivo)
        sqlCmdObj.Parameters.AddWithValue("@EXTENSION", "pdf")
        sqlConnection.Open()
        sqlCmdObj.ExecuteNonQuery()
        sqlConnection.Close()

    End Sub


End Class