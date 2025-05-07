Imports Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Data
Imports System.Data.SqlClient

Public Class Act_InfoTrab
    Inherits System.Web.UI.Page

    Public estatus As String = String.Empty
    Dim tableTrabajadores As New DataTable

    Public Sub ActualizaInfo(Persona As Integer)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Persona)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "INS_PERSONA_PLAZA_CARGA_MULTIPLE"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        'GetTokenAPI()
        'GetInfoTrabajadorAPI(Persona)
        'ProcInfoTrabajadoresAPI()
    End Sub



    'Protected Sub GetTokenAPI()

    '    Try

    '        Dim requestToken As System.Net.WebRequest = System.Net.WebRequest.Create("https://siidrh.ugto.mx/identity/auth/connect/token")
    '        Dim dataToken As String = "grant_type=client_credentials&client_id=CE001&client_secret=6HEBV-3RNRZ-C4PFF-CGX8X-OCJ6R&scope=APIPrestamos"
    '        requestToken.Method = "POST"
    '        requestToken.ContentType = "application/x-www-form-urlencoded"
    '        requestToken.ContentLength = dataToken.Length
    '        requestToken.Credentials = System.Net.CredentialCache.DefaultCredentials

    '        Dim encodingToken As New UTF8Encoding()
    '        Using dsAuxToken = requestToken.GetRequestStream()
    '            dsAuxToken.Write(encodingToken.GetBytes(dataToken), 0, dataToken.Length)
    '        End Using

    '        Dim responseToken As System.Net.WebResponse = requestToken.GetResponse()
    '        Dim dsToken As System.IO.Stream = responseToken.GetResponseStream()
    '        Dim readerToken As New System.IO.StreamReader(dsToken)
    '        Dim responseServerToken As String = readerToken.ReadToEnd()
    '        readerToken.Close()
    '        responseToken.Close()

    '        Try
    '            Dim joToken As JObject = JObject.Parse(responseServerToken)
    '            ViewState("TOKEN") = joToken.SelectToken("access_token").ToString
    '            ViewState("TOKENTYPE") = joToken.SelectToken("token_type").ToString
    '        Catch ex As Exception
    '            ViewState("TOKEN") = Nothing
    '            ViewState("TOKENTYPE") = Nothing
    '            ViewState("MENSAJE") = "ER: No se encontro un token de accesso."
    '        End Try

    '    Catch hex As System.Net.WebException
    '        ViewState("MENSAJE") = "WEBER: " + hex.Message()
    '    Catch ex As Exception
    '        ViewState("MENSAJE") = "ER: " + ex.Message()
    '    Finally
    '        estatus = ViewState("MENSAJE")
    '    End Try

    'End Sub

    'Public Sub GetInfoTrabajadorAPI(idpersona As Integer)

    '    Try

    '        Dim clientCatalogs As New RestClient("https://siidrh.ugto.mx/ws/APIPrestamos/v1.0/catalogo/trabajadores/" + (idpersona.ToString).PadLeft(5, "0"))
    '        Dim requestCatalogo As New RestRequest(Method.POST)
    '        requestCatalogo.AddHeader("Authorization", ViewState("TOKENTYPE") + " " + ViewState("TOKEN"))
    '        Dim responseCatalogo As New RestResponse
    '        responseCatalogo = clientCatalogs.Execute(requestCatalogo)
    '        Dim jsonTrabajadores As String = responseCatalogo.Content

    '        Try

    '            Dim tableTrabajadoresSQL As New DataTable
    '            tableTrabajadoresSQL.Columns.Add("DATAINFO", GetType(String))

    '            Dim joCatalog As JObject = JObject.Parse(jsonTrabajadores)
    '            Dim dataTrabajadores As List(Of JToken) = joCatalog.Children().ToList

    '            tableTrabajadores.Columns.Add("No. Empleado", GetType(String))
    '            tableTrabajadores.Columns.Add("Primer Nombre", GetType(String))
    '            tableTrabajadores.Columns.Add("Segundo Nombre", GetType(String))
    '            tableTrabajadores.Columns.Add("Apellido Paterno", GetType(String))
    '            tableTrabajadores.Columns.Add("Apellido Materno", GetType(String))
    '            tableTrabajadores.Columns.Add("CURP", GetType(String))
    '            tableTrabajadores.Columns.Add("RFC", GetType(String))
    '            tableTrabajadores.Columns.Add("Sexo", GetType(String))
    '            tableTrabajadores.Columns.Add("Fecha de Nacimiento", GetType(String))
    '            tableTrabajadores.Columns.Add("Nacionalidad", GetType(String))
    '            tableTrabajadores.Columns.Add("Asentamiento", GetType(String))
    '            tableTrabajadores.Columns.Add("Municipio", GetType(String))
    '            tableTrabajadores.Columns.Add("Estado", GetType(String))
    '            tableTrabajadores.Columns.Add("Calle", GetType(String))
    '            tableTrabajadores.Columns.Add("Número Exterior", GetType(String))
    '            tableTrabajadores.Columns.Add("Número Interior", GetType(String))
    '            tableTrabajadores.Columns.Add("Código Postal", GetType(String))
    '            tableTrabajadores.Columns.Add("Plaza", GetType(String))
    '            tableTrabajadores.Columns.Add("Nivel", GetType(String))
    '            tableTrabajadores.Columns.Add("Sueldo", GetType(String))
    '            tableTrabajadores.Columns.Add("Periodicidad", GetType(String))
    '            tableTrabajadores.Columns.Add("Años antigüedad", GetType(String))
    '            tableTrabajadores.Columns.Add("Meses antigüedad", GetType(String))
    '            tableTrabajadores.Columns.Add("Asentamiento laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Municipio laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Estado laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Calle laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Exterior laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Interior laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("C.P. laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Correo electrónico", GetType(String))
    '            tableTrabajadores.Columns.Add("Teléfono laboral", GetType(String))
    '            tableTrabajadores.Columns.Add("Nombramiento", GetType(String))
    '            tableTrabajadores.Columns.Add("Tipo Trabajador", GetType(String))
    '            tableTrabajadores.Columns.Add("Porcentaje de liquidez", GetType(String))

    '            For Each itemTrbajadores As JProperty In dataTrabajadores
    '                itemTrbajadores.CreateReader()

    '                Select Case itemTrbajadores.Name
    '                    Case "data"
    '                        For Each msg As JObject In itemTrbajadores.Values

    '                            tableTrabajadores.Rows.Add(
    '                                        msg("Numero de empleado de PCET"),
    '                                        msg("Primer nombre"),
    '                                        msg("Segundo nombre"),
    '                                        msg("Apellido paterno"),
    '                                        msg("Apellido materno"),
    '                                        msg("CURP"),
    '                                        msg("RFC"),
    '                                        msg("Sexo").ToString.Replace("1", "H").Replace("2", "M"),
    '                                        Left(msg("Fecha de nacimiento"), 10),
    '                                        msg("Nacionalidad"),
    '                                        msg("Asentamiento"),
    '                                        msg("Municipio"),
    '                                        msg("Estado"),
    '                                        msg("Calle"),
    '                                        msg("Número exterior"),
    '                                        msg("Número interior"),
    '                                        msg("Código Postal"),
    '                                        msg("Plaza"),
    '                                        msg("Nivel"),
    '                                        msg("SUELDO"),
    '                                        msg("Periodicidad"),
    '                                        msg("Antigüedad año"),
    '                                        msg("Antigüedad mes"),
    '                                        msg("Asentamiento Laboral"),
    '                                        msg("Municipio Laboral"),
    '                                        msg("Estado Laboral"),
    '                                        msg("Calle Laboral"),
    '                                        msg(" Número exterior Laboral"),
    '                                        msg("Número interior Laboral"),
    '                                        msg("Código Postal Laboral"),
    '                                        (msg("Correo electrónico").ToString).Split(",").FirstOrDefault,
    '                                        (msg("Teléfono Laboral").ToString).Split(",").FirstOrDefault,
    '                                        msg("Tipo de Nombramiento"),
    '                                        msg("Tipo de Trabajador"),
    '                                        msg("Porcentaje"))

    '                            tableTrabajadoresSQL.Rows.Add(
    '                                        msg("Numero de empleado de PCET").ToString + "," +
    '                                        msg("Primer nombre").ToString + "," +
    '                                        msg("Segundo nombre").ToString + "," +
    '                                        msg("Apellido paterno").ToString + "," +
    '                                        msg("Apellido materno").ToString + "," +
    '                                        msg("CURP").ToString + "," +
    '                                        msg("RFC").ToString + "," +
    '                                        (msg("Sexo").ToString.Replace("1", "H").Replace("2", "M")).ToString + "," +
    '                                        (Left(msg("Fecha de nacimiento"), 10)).ToString + "," +
    '                                        msg("Nacionalidad").ToString + "," +
    '                                        msg("Asentamiento").ToString + "," +
    '                                        msg("Municipio").ToString + "," +
    '                                        msg("Estado").ToString + "," +
    '                                        msg("Calle").ToString + "," +
    '                                        msg("Número exterior").ToString + "," +
    '                                        msg("Número interior").ToString + "," +
    '                                        msg("Código Postal").ToString + "," +
    '                                        msg("Plaza").ToString + "," +
    '                                        msg("Nivel").ToString + "," +
    '                                        msg("SUELDO").ToString + "," +
    '                                        msg("Periodicidad").ToString + "," +
    '                                        msg("Antigüedad año").ToString + "," +
    '                                        msg("Antigüedad mes").ToString + "," +
    '                                        msg("Asentamiento Laboral").ToString + "," +
    '                                        msg("Municipio Laboral").ToString + "," +
    '                                        msg("Estado Laboral").ToString + "," +
    '                                        msg("Calle Laboral").ToString + "," +
    '                                        msg(" Número exterior Laboral").ToString + "," +
    '                                        msg("Número interior Laboral").ToString + "," +
    '                                        msg("Código Postal Laboral").ToString + "," +
    '                                        ((msg("Correo electrónico").ToString).Split(",").FirstOrDefault).ToString + "," +
    '                                        ((msg("Teléfono Laboral").ToString).Split(",").FirstOrDefault).ToString + "," +
    '                                        msg("Tipo de Nombramiento").ToString + "," +
    '                                        msg("Tipo de Trabajador").ToString + "," +
    '                                        msg("Porcentaje").ToString)

    '                        Next
    '                End Select

    '            Next

    '            If tableTrabajadores.Rows.Count > 0 Then
    '                ViewState("TABLETRABAJADORES") = tableTrabajadoresSQL
    '            Else
    '                ViewState("TABLETRABAJADORES") = Nothing
    '            End If

    '        Catch ex As Exception
    '            ViewState("MENSAJE") = "ER: No se encontró un token de accesso."
    '        End Try

    '    Catch ex As Exception
    '        ViewState("MENSAJE") = "ER: " + ex.Message()
    '    Finally

    '        estatus = ViewState("MENSAJE")

    '    End Try

    'End Sub

    'Protected Sub ProcInfoTrabajadoresAPI()
    '    Try

    '        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
    '            connection.Open()

    '            Dim PersonaGeneral As New Data.DataTable()
    '            PersonaGeneral.Columns.Add("EXITO", GetType(Integer))
    '            PersonaGeneral.Columns.Add("FILA", GetType(Integer))
    '            PersonaGeneral.Columns.Add("IDPERSONA", GetType(Integer))
    '            PersonaGeneral.Columns.Add("NOMBRES", GetType(String))
    '            PersonaGeneral.Columns.Add("APELLIDOS", GetType(String))
    '            PersonaGeneral.Columns.Add("DETALLE", GetType(String))
    '            PersonaGeneral.Columns.Add("AUX_ERROR", GetType(Integer))


    '            ' Configure the SqlCommand and SqlParameter.
    '            Dim insertCommand As New SqlCommand("INS_PERSONAS_CARGA_API", connection)
    '            insertCommand.CommandType = System.Data.CommandType.StoredProcedure
    '            insertCommand.CommandTimeout = 360
    '            'Parametro que representa una tabla en SQL
    '            Session("parm") = New SqlParameter("DATOSAPI", SqlDbType.Structured)
    '            Session("parm").Value = ViewState("TABLETRABAJADORES")
    '            insertCommand.Parameters.Add(Session("parm"))

    '            Session("parm") = New SqlParameter("IDUSER", SqlDbType.VarChar)
    '            Session("parm").Value = Session("USERID")
    '            insertCommand.Parameters.Add(Session("parm"))

    '            Session("parm") = New SqlParameter("IDSESION", SqlDbType.VarChar)
    '            Session("parm").Value = Session("SESION")
    '            insertCommand.Parameters.Add(Session("parm"))
    '            '  Execute the command.

    '            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
    '            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()

    '            PersonaGeneral.Load(myReader)
    '            myReader.Close()

    '            Dim contador As Integer = 0

    '            ViewState("MENSAJE") = "Información actualizada correctamente"

    '            connection.Close()
    '        End Using


    '    Catch ex As Exception
    '        ViewState("MENSAJE") = "ER: " + ex.Message()
    '    Finally

    '        estatus = ViewState("MENSAJE")
    '    End Try
    'End Sub


End Class
