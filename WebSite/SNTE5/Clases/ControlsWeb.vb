Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class ControlsWeb
    Inherits DataAccess

    Public Token As String, TokenType As String, Mensaje As String

    ''' <summary>
    ''' Ejecuta un procedimiento almacenado y regresa un DropDownList lleno con el resultado de la consulta.
    ''' </summary>
    ''' <param name="DropD">Nombre del control DropDownList que se llenará.</param>
    ''' <param name="StoredProcedure">Nombre del procedimiento almacenado.</param>
    ''' <param name="HashT">Parámetros que se enviarán al procedimiento almacenado.</param>
    ''' <param name="Indice">Índice de la tabla del DataSet con el que se llena el DropDownList.</param>
    ''' <param name="ItemText">DataTextField del DropDownList.</param>
    ''' <param name="ItemValue">DataValueField del DropDownList.</param>
    ''' <param name="DefaultText">Texto que se agregará en el índice cero del control.</param>
    ''' <returns></returns>
    Public Function LlenaDropDownList(ByVal DropD As DropDownList, ByVal StoredProcedure As String, ByVal HashT As Hashtable, ByVal Indice As Integer, ByVal ItemText As String, ByVal ItemValue As String, ByVal DefaultText As String) As DropDownList
        DropD.DataSource = RegresaDataSet(StoredProcedure, HashT).Tables(Indice)
        DropD.DataTextField = ItemText
        DropD.DataValueField = ItemValue
        DropD.DataBind()
        DropD.Items.Insert(0, DefaultText)
        Return DropD
    End Function

    ''' <summary>
    ''' Ejecuta un procedimiento almacenado y regresa un DropDownList lleno con el resultado de la consulta.
    ''' </summary>
    ''' <param name="DropD">Nombre del control DropDownList que se llenará.</param>
    ''' <param name="StoredProcedure">Nombre del procedimiento almacenado.</param>
    ''' <param name="Indice">Índice de la tabla del DataSet con el que se llena el DropDownList.</param>
    ''' <param name="ItemText">DataTextField del DropDownList.</param>
    ''' <param name="ItemValue">DataValueField del DropDownList.</param>
    ''' <param name="DefaultText">Texto que se agregará en el índice cero del control.</param>
    ''' <returns></returns>
    Public Function LlenaDropDownList(ByVal DropD As DropDownList, ByVal StoredProcedure As String, ByVal Indice As Integer, ByVal ItemText As String, ByVal ItemValue As String, ByVal DefaultText As String) As DropDownList
        DropD.DataSource = RegresaDataSet(StoredProcedure).Tables(Indice)
        DropD.DataTextField = ItemText
        DropD.DataValueField = ItemValue
        DropD.DataBind()
        DropD.Items.Insert(0, DefaultText)
        Return DropD
    End Function

    ''' <summary>
    ''' Ejecuta un procedimiento almacenado y regresa un GridView lleno con el resultado de la consulta.
    ''' </summary>
    ''' <param name="GridV">Nombre del GridView que se llenará.</param>
    ''' <param name="StoredProcedure">Nombre del procedimiento almacenado.</param>
    ''' <param name="HashT">Parámetros que se enviarán al procedimiento almacenado.</param>
    ''' <param name="Indice">Índice de la tabla del DataSet con el que se llena el GridView.</param>
    ''' <returns></returns>
    Public Function LlenaGridView(ByVal GridV As GridView, ByVal StoredProcedure As String, ByVal HashT As Hashtable, ByVal Indice As Int32) As GridView
        GridV.DataSource = RegresaDataSet(StoredProcedure, HashT).Tables(Indice)
        GridV.DataBind()
        Return GridV
    End Function

    ''' <summary>
    ''' Ejecuta un procedimiento almacenado y regresa un DataGrid lleno con el resultado de la consulta.
    ''' </summary>
    ''' <param name="GridV">Nombre del DataGrid que se llenará.</param>
    ''' <param name="StoredProcedure">Nombre del procedimiento almacenado.</param>
    ''' <param name="HashT">Parámetros que se enviarán al procedimiento almacenado.</param>
    ''' <param name="Indice">Índice de la tabla del DataSet con el que se llena el DataGrid.</param>
    ''' <returns></returns>
    Public Function LlenaDataGrid(ByVal GridV As DataGrid, ByVal StoredProcedure As String, ByVal HashT As Hashtable, ByVal Indice As Int32) As DataGrid
        GridV.DataSource = RegresaDataSet(StoredProcedure, HashT).Tables(Indice)
        GridV.DataBind()
        Return GridV
    End Function

    Protected Sub GetTokenAPI()
        Try

            Dim requestToken As System.Net.WebRequest = System.Net.WebRequest.Create(ConfigurationManager.AppSettings.[Get]("API_Login").ToString())
            Dim dataToken As String = "UserName=" + ConfigurationManager.AppSettings.[Get]("UserName").ToString() +
                "&UserSecret=" + ConfigurationManager.AppSettings.[Get]("UserSecret").ToString()
            requestToken.Method = "POST"
            requestToken.ContentType = "application/x-www-form-urlencoded"
            requestToken.ContentLength = dataToken.Length
            requestToken.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim encodingToken As New UTF8Encoding()
            Using dsAuxToken = requestToken.GetRequestStream()
                dsAuxToken.Write(encodingToken.GetBytes(dataToken), 0, dataToken.Length)
            End Using

            Dim responseToken As System.Net.WebResponse = requestToken.GetResponse()
            Dim dsToken As System.IO.Stream = responseToken.GetResponseStream()
            Dim readerToken As New System.IO.StreamReader(dsToken)
            Dim responseServerToken As String = readerToken.ReadToEnd()
            readerToken.Close()
            responseToken.Close()

            Try
                Dim joToken As JObject = JObject.Parse(responseServerToken)
                Token = joToken.SelectToken("token").ToString
            Catch ex As Exception
                Token = Nothing
                TokenType = Nothing
                Mensaje = "ER: No se encontro un token de accesso."
            End Try

        Catch hex As System.Net.WebException
            Mensaje = "WEBER: " + hex.Message()
        Catch ex As Exception
            Mensaje = "ER: " + ex.Message()
        End Try
    End Sub

    Public Function PostCreaCtaApp(ByVal Cta As CrearCuentaAppModel) As String
        Dim body As String = String.Empty

        Try
            GetTokenAPI()

            Dim objCta As JObject = New JObject()
            objCta.Add("ID_PROCEDENCIA", Cta.ID_PROCEDENCIA)
            objCta.Add("ID_PERSONA", Cta.ID_PERSONA)
            objCta.Add("USER_NAME", Cta.USER_NAME)
            objCta.Add("CORREO", Cta.CORREO)
            objCta.Add("TELEFONO", Cta.TELEFONO)
            objCta.Add("CONTRASENIA", Cta.CONTRASENIA)
            objCta.Add("USER_TRANS", Cta.USER_TRANS)

            Dim data As Byte() = UTF8Encoding.UTF8.GetBytes(objCta.ToString())

            Dim request As HttpWebRequest
            request = TryCast(WebRequest.Create(ConfigurationManager.AppSettings.[Get]("API_CrearCuenta").ToString()), HttpWebRequest)
            request.Timeout = 10 * 1000
            request.Method = "POST"
            request.ContentLength = data.Length
            request.ContentType = "application/json; charset=utf-8"
            request.Headers.Add("Token", Token)


            Using postStream As System.IO.Stream = request.GetRequestStream()
                postStream.Write(data, 0, data.Length)
            End Using

            Dim response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())

            body = reader.ReadToEnd()
        Catch ex As Exception
            body = ex.Message.ToString()
        End Try

        Return body
    End Function

    Public Function PostContraseña(ByVal Usuario As String) As String
        Dim body As String = String.Empty

        Try
            GetTokenAPI()

            Dim request As HttpWebRequest
            request = TryCast(WebRequest.Create(ConfigurationManager.AppSettings.[Get]("API_OlvideContrasenia").ToString() + "?Usuario=" + Usuario), HttpWebRequest)
            request.Timeout = 10 * 1000
            request.Method = "POST"
            request.ContentType = "application/json; charset=utf-8"
            request.Headers.Add("Token", Token)

            Dim postStream As System.IO.Stream = request.GetRequestStream()
            Dim response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())

            body = reader.ReadToEnd()
        Catch ex As Exception
            body = ex.Message.ToString()
        End Try

        Return body
    End Function
End Class
