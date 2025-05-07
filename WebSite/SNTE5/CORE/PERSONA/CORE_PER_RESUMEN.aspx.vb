Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Data
Imports System.Data.SqlClient

Public Class CORE_PER_RESUMEN
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Not Me.IsPostBack Then

        End If
        If Session("PERSONAID_F") Is Nothing Then
            'GetInfoCuentasBancariasAPI()
            RealizaResumen(Session("PERSONAID").ToString)
        Else
            'GetInfoCuentasBancariasAPI()
            RealizaResumen(Session("PERSONAID_F").ToString)
        End If

        btn_imprimir.Attributes.Add("OnClick", "imprimir()")
    End Sub

    Private Sub carga_personales(ByVal personaid As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_PERSONALES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Dim NUMEMP As String = ""
            'NUMEMP = personaid.ToString
            'NUMEMP = NUMEMP.PadLeft(5, "0")
            lbl_claveres.Text = Session("rs").Fields("NUMTRAB").Value
            lbl_nombreres.Text = Session("rs").Fields("NOMBRE1").Value + " " + Session("rs").Fields("NOMBRE2").Value + " " + Session("rs").Fields("PATERNO").Value + " " + Session("rs").Fields("MATERNO").Value
            lbl_curpres.Text = Session("rs").Fields("CURP").Value
            lbl_rfcres.Text = Session("rs").Fields("RFC").Value
            lbl_sexores.Text = IIf(Session("rs").Fields("SEXO").Value = "", "OTRO", (IIf(Session("rs").Fields("SEXO").Value = "H", "HOMBRE", "MUJER")))
            lbl_fechanacres.Text = Left(Session("rs").Fields("FECHANAC").Value.ToString, 10)
            Dim notas As String
            notas = Session("rs").Fields("NOTAS").Value
        End If
        Session("Con").Close()
    End Sub

#Region "Cuentas Bancarias"



    Protected Sub GetTokenAPI()

        Try

            Dim requestToken As System.Net.WebRequest = System.Net.WebRequest.Create("https://siidrh.ugto.mx/identity/auth/connect/token")
            Dim dataToken As String = "grant_type=client_credentials&client_id=CE001&client_secret=6HEBV-3RNRZ-C4PFF-CGX8X-OCJ6R&scope=APIPrestamos"
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
                ViewState("TOKEN") = joToken.SelectToken("access_token").ToString
                ViewState("TOKENTYPE") = joToken.SelectToken("token_type").ToString
            Catch ex As Exception
                ViewState("TOKEN") = Nothing
                ViewState("TOKENTYPE") = Nothing
                ViewState("MENSAJE") = "ER: No se encontro un token de accesso."
            End Try

        Catch hex As System.Net.WebException
            ViewState("MENSAJE") = "WEBER: " + hex.Message()
        Catch ex As Exception
            ViewState("MENSAJE") = "ER: " + ex.Message()
        Finally
            'lbl_cuentas.Text = ViewState("MENSAJE")
        End Try

    End Sub

    Protected Sub GetInfoCuentasBancariasAPI()

        'cmb_banco.Items.Clear()
        'cmb_banco.Items.Add(New ListItem("ELIJA", "-1"))

        GetTokenAPI()

        Try

            Dim clientCatalogs As New RestClient("https://siidrh.ugto.mx/ws/APIPrestamos/v1.0/catalogo/cuenta/bancaria/" + Session("PERSONAID").ToString)
            Dim requestCatalogo As New RestRequest(Method.POST)
            requestCatalogo.AddHeader("Authorization", ViewState("TOKENTYPE") + " " + ViewState("TOKEN"))
            Dim responseCatalogo As New RestResponse
            responseCatalogo = clientCatalogs.Execute(requestCatalogo)
            Dim jsonCuentas As String = responseCatalogo.Content

            Try

                Dim tableCuentasBancarias As New DataTable
                tableCuentasBancarias.Columns.Add("IDBANCO", GetType(String))
                tableCuentasBancarias.Columns.Add("BANCO", GetType(String))
                tableCuentasBancarias.Columns.Add("CUENTA", GetType(String))

                Dim foundRowCuenta() As DataRow
                Dim foundRowBanco() As DataRow

                Dim joCatalog As JObject = JObject.Parse(jsonCuentas)
                Dim dataCuentas As List(Of JToken) = joCatalog.Children().ToList
                'dag_Cuentas.DataSource = dataCuentas

                For Each itemCuentas As JProperty In dataCuentas
                    itemCuentas.CreateReader()
                    Select Case itemCuentas.Name
                        Case "data"
                            For Each msg As JObject In itemCuentas.Values

                                foundRowCuenta = tableCuentasBancarias.Select("CUENTA='" + msg("Cuenta").ToString + "'")
                                foundRowBanco = tableCuentasBancarias.Select("IDBANCO='" + msg("ID_banco").ToString + "'")

                                If foundRowCuenta.Any = False And foundRowBanco.Any = False Then

                                    tableCuentasBancarias.Rows.Add(msg("ID_banco"), msg("Banco"), msg("Cuenta"))

                                    'cmb_banco.Items.Add(New ListItem(" Cuenta: " + msg("Cuenta").ToString + " - " + "Banco: " + msg("Banco").ToString, msg("Cuenta").ToString))

                                End If

                                foundRowCuenta = Nothing
                                foundRowBanco = Nothing

                            Next
                    End Select
                Next

                If tableCuentasBancarias.Rows.Count > 0 Then
                    dag_Cuentas.DataSource = tableCuentasBancarias
                    dag_Cuentas.DataBind()
                Else

                End If

                'ViewState("MENSAJE") = CStr(tableCuentasBancarias.Rows.Count)
                'ViewState("CUENTASBANCARIAS") = tableCuentasBancarias

            Catch ex As Exception
                'ViewState("MENSAJE") = "ER: No se encontro un token de accesso."
                'lbl_cuentas.Text = ViewState("MENSAJE")
            End Try

            'ViewState("MENSAJE") = ""
            'lbl_cuentas.Text = "llega aqui"
        Catch ex As Exception
            ' ViewState("MENSAJE") = "ER: " + ex.Message()
            'lbl_cuentas.Text = ViewState("MENSAJE")
        Finally
            'lbl_cuentas.Text = ViewState("MENSAJE")
        End Try

    End Sub

#End Region

    Private Sub RealizaResumen(ByVal personaid As String)
        carga_personales(CInt(personaid))
        'Se consulta el domicilio de la persona
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPODIRID", Session("adVarChar"), Session("adParamInput"), 3, "1")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DIRECCION"
        Session("rs") = Session("cmd").Execute()

        'Se asignan los datos de domicilio a las etiquetas
        If Not Session("rs").EOF Then
            lbl_calleynores.Text = Session("rs").Fields("CALLE").Value.ToString +
            " No. " + Session("rs").Fields("NUMEXT").Value.ToString +
            IIf(Session("rs").Fields("NUMEXT").Value.ToString = "", "", " Interior " + Session("rs").Fields("NUMINT").Value.ToString)
            lbl_localidadres.Text = Session("rs").Fields("ASENTAMIENTO").Value.ToString
            lbl_municipiores.Text = Session("rs").Fields("MUNICIPIO").Value.ToString
            lbl_estadores.Text = Session("rs").Fields("ESTADO").Value.ToString
            lbl_antiempres.Text = Session("rs").Fields("ANTIGUEDAD").Value.ToString
            lbl_sueldores.Text = Session("rs").Fields("SUELDO").Value.ToString
            lbl_periodosueldores.Text = Session("rs").Fields("PERIODICIDAD").Value.ToString
        End If
        Session("Con").Close()



        ' Correo electrónico
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORREOE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_corres.Text = Session("rs").Fields("CORREO").value.ToString()
        End If
        Session("Con").Close()


        ' Teléfono 
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TELEFONO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_telCel.Text = Session("rs").Fields("TELEFONO").value.ToString()
        End If
        Session("Con").Close()

    End Sub


End Class