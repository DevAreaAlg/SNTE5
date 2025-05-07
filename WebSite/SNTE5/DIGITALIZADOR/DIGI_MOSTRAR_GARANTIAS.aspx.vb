Imports System.IO
Public Class DIGI_MOSTRAR_GARANTIAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Archivo que me permite abrir el documento dentro de la base de datos
        VerDocumentoDigitalizado()
    End Sub

    Private Sub VerDocumentoDigitalizado()

        Dim strConnString As String 'Ruta de la BD
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

        Dim RutaDigitaliza As String = ConfigurationManager.AppSettings.[Get]("urldigi")
        Dim Nombre As String
        Dim Ext As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CVE_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("CVEGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_GARANTIA", Session("adVarChar"), Session("adParamInput"), 10, Session("TIPOGARANTIA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE_DOCUMENTO", Session("adVarChar"), Session("adParamInput"), 10, Session("NOMBRE_DOCUMENTO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MOSTRAR_IMAGEN_GARANTIA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Nombre = Session("rs").Fields("NOMBRE").Value
            Ext = Session("rs").Fields("EXTENSION").Value
        End If
        Session("Con").close()
        If File.Exists(RutaDigitaliza + "\" + Nombre) Then

            With Response
                Try
                    Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urldigi")
                    Dim cPath As String = urldigi + "\"
                    Dim archivo As String = Nombre
                    Dim ResultDocName As String = archivo + Ext

                    Dim Filename As String = archivo + Ext
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
                    'estatus.Text = ex.ToString
                End Try
            End With


        End If

    End Sub

End Class