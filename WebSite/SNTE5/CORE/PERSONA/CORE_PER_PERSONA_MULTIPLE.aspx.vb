Imports System.Data
Imports System.Data.SqlClient
Public Class CORE_PER_PERSONA_MULTIPLE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Carga Múltiple", "Carga Múltiple Personas")

        If Not Me.IsPostBack Then
            CreaTablas()
        End If
    End Sub

    Private Sub CreaTablas()

        Dim TablaPagos As New DataTable

        TablaPagos.Columns.Add("NUMERO", GetType(Integer))
        TablaPagos.Columns.Add("NUM_EXP", GetType(String))
        TablaPagos.Columns.Add("ID_CLIENTE", GetType(Integer))
        TablaPagos.Columns.Add("NOMBRE", GetType(String))
        TablaPagos.Columns.Add("FOLIO", GetType(Integer))
        TablaPagos.Columns.Add("ABONO", GetType(Decimal))
        TablaPagos.Columns.Add("ERROR", GetType(String))
        TablaPagos.Columns.Add("AUXERROR", GetType(Integer))

        Session("TablaPagos") = TablaPagos

    End Sub

    Protected Sub btn_CargarArch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CargarArch.Click

        AsyncFileUpload1.Enabled = False
        btn_CargarArch.Enabled = False
        lbl_Status_Carga.Text = ""
        CargarArchCSV()
    End Sub

    Private Sub CargarArchCSV()

        If AsyncFileUpload1.PostedFile.FileName = "" Then
            lbl_Status_Carga.Text = "Error: Selecciona un archivo."
            Exit Sub
        End If

        'EVALUAR QUE SEA CSV
        If Right(System.IO.Path.GetFileName(AsyncFileUpload1.PostedFile.FileName), 3).ToUpper <> "CSV" Then
            lbl_Status_Carga.Text = "Error: Debe subir el ""layout"" en formato .csv ."
            Exit Sub
        End If

        Dim Resultado As String = "OK"
        Dim dtArchivo As New DataTable
        Dim Variable As String = ""
        Dim contadorFila As Integer = 0
        dtArchivo.Columns.Add("LINEA", GetType(String))
        Dim linea As String = ""

        Using fs As System.IO.Stream = AsyncFileUpload1.PostedFile.InputStream
            Using oRead As New System.IO.StreamReader(fs, System.Text.Encoding.Default)
                Do While Not oRead.EndOfStream
                    linea = oRead.ReadLine()
                    If contadorFila > 0 Then
                        dtArchivo.Rows.Add(linea)
                        'lbl_Status_Carga.Text = lbl_Status_Carga.Text + "" + linea
                        'Dim arrVar As String() = Split(linea, ",")
                    End If

                    contadorFila = contadorFila + 1


                Loop
            End Using



            If Resultado = "OK" Then
                fs.Position = 0

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim PersonaGeneral As New Data.DataTable()
                PersonaGeneral.Columns.Add("EXITO", GetType(Integer))
                PersonaGeneral.Columns.Add("FILA", GetType(Integer))
                PersonaGeneral.Columns.Add("IDPERSONA", GetType(Integer))
                PersonaGeneral.Columns.Add("NOMBRES", GetType(String))
                PersonaGeneral.Columns.Add("APELLIDOS", GetType(String))
                PersonaGeneral.Columns.Add("DETALLE", GetType(String))
                PersonaGeneral.Columns.Add("AUX_ERROR", GetType(Integer))

                Dim dtPersonasEx As New Data.DataTable()
                dtPersonasEx.Columns.Add("FILA", GetType(Integer))
                dtPersonasEx.Columns.Add("ID_PERSONA", GetType(Integer))
                dtPersonasEx.Columns.Add("NOMBRES", GetType(String))
                dtPersonasEx.Columns.Add("APELLIDOS", GetType(String))

                Dim dtPersonasNoEx As New Data.DataTable()
                dtPersonasNoEx.Columns.Add("FILA", GetType(Integer))
                dtPersonasNoEx.Columns.Add("NOMBRES", GetType(String))
                dtPersonasNoEx.Columns.Add("APELLIDOS", GetType(String))
                dtPersonasNoEx.Columns.Add("DETALLE", GetType(String))
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
                Dim dtPagos As New Data.DataTable()




                Dim strConnString As String 'INSERTO BINARIO EN BD
                strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
                Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("INS_PERSONA_FISICA_CARGA_MULTIPLE", sqlConnection)
                'Stored Procedure 
                sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure
                'Parametros para la inserción del Stored Procedure
                sqlCmdObj.Parameters.AddWithValue("@DTARCHIVO", dtArchivo)
                sqlCmdObj.Parameters.AddWithValue("@IDSUC", Session("SUCID"))
                sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID"))
                sqlCmdObj.Parameters.AddWithValue("@IDSESION", Session("SESION"))



                sqlConnection.Open()

                Dim myReader As SqlDataReader = sqlCmdObj.ExecuteReader(CommandBehavior.CloseConnection)


                PersonaGeneral.Load(myReader)

                myReader.Close()

                sqlConnection.Close()



                Dim contador As Integer = 0

                For Each row As Data.DataRow In PersonaGeneral.Rows()
                    If PersonaGeneral.Rows(contador).Item("EXITO").ToString = 1 Then

                        dtPersonasEx.Rows.Add(PersonaGeneral.Rows(contador).Item("FILA"), PersonaGeneral.Rows(contador).Item("IDPERSONA"), PersonaGeneral.Rows(contador).Item("NOMBRES"), PersonaGeneral.Rows(contador).Item("APELLIDOS"))
                    Else

                        dtPersonasNoEx.Rows.Add(PersonaGeneral.Rows(contador).Item("FILA"), PersonaGeneral.Rows(contador).Item("NOMBRES"), PersonaGeneral.Rows(contador).Item("APELLIDOS"), PersonaGeneral.Rows(contador).Item("DETALLE"))
                    End If
                    contador += 1
                Next

                If dtPersonasEx.Rows.Count > 0 Then
                    dag_Persona_Ex.Visible = True
                    dag_Persona_Ex.DataSource = dtPersonasEx
                    dag_Persona_Ex.DataBind()
                Else
                    dag_Persona_Ex.Visible = False
                End If

                If dtPersonasNoEx.Rows.Count > 0 Then
                    Dag_Persona_NoEx.Visible = True
                    Dag_Persona_NoEx.DataSource = dtPersonasNoEx
                    Dag_Persona_NoEx.DataBind()
                Else
                    Dag_Persona_NoEx.Visible = False
                End If
                ' DELETE THE FILE
                'System.IO.File.Delete(fileName)

                If Resultado = "OK" Then

                Else
                    lbl_Status_Carga.Text = Resultado
                End If
            Else
                lbl_Status_Carga.Text = Resultado
            End If

        End Using

        '' that the FileUpload control contains a file.
        'If (AsyncFileUpload1.HasFile) Then
        '    ' Get the name of the file to upload.
        '    ' Dim fileName As String = Server.HtmlEncode(AsyncFileUpload1.FileName)
        '    Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload1.FileName.ToString)
        '    ' Get the extension of the uploaded file.
        '    Dim extension As String = System.IO.Path.GetExtension(fileName)

        '    ' Allow only files with .txt or .cvs extensions
        '    ' to be uploaded.
        '    If (extension = ".csv") Then

        '        ' Call the SaveAs method to save                
        '        AsyncFileUpload1.SaveAs(fileName)

        '        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        '        Dim PersonaGeneral As New Data.DataTable()
        '        PersonaGeneral.Columns.Add("EXITO", GetType(Integer))
        '        PersonaGeneral.Columns.Add("FILA", GetType(Integer))
        '        PersonaGeneral.Columns.Add("IDPERSONA", GetType(Integer))
        '        PersonaGeneral.Columns.Add("NOMBRES", GetType(String))
        '        PersonaGeneral.Columns.Add("APELLIDOS", GetType(String))
        '        PersonaGeneral.Columns.Add("DETALLE", GetType(String))
        '        PersonaGeneral.Columns.Add("AUX_ERROR", GetType(Integer))

        '        Dim dtPersonasEx As New Data.DataTable()
        '        dtPersonasEx.Columns.Add("FILA", GetType(Integer))
        '        dtPersonasEx.Columns.Add("ID_PERSONA", GetType(Integer))
        '        dtPersonasEx.Columns.Add("NOMBRES", GetType(String))
        '        dtPersonasEx.Columns.Add("APELLIDOS", GetType(String))

        '        Dim dtPersonasNoEx As New Data.DataTable()
        '        dtPersonasNoEx.Columns.Add("FILA", GetType(Integer))
        '        dtPersonasNoEx.Columns.Add("NOMBRES", GetType(String))
        '        dtPersonasNoEx.Columns.Add("APELLIDOS", GetType(String))
        '        dtPersonasNoEx.Columns.Add("DETALLE", GetType(String))

        '        Session("cmd") = New ADODB.Command()
        '        Session("Con").Open()
        '        Session("cmd").ActiveConnection = Session("Con")
        '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        '        Session("parm") = Session("cmd").CreateParameter("FILENAME", Session("adVarChar"), Session("adParamInput"), 1000, fileName)
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("EXTENSION", Session("adVarChar"), Session("adParamInput"), 15, extension)
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("cmd").CommandText = "INS_PERSONA_FISICA_CARGA_MULTIPLE"
        '        Session("rs") = Session("cmd").Execute()

        '        custDA.Fill(PersonaGeneral, Session("rs"))


        '        Session("Con").Close()

        '        Dim contador As Integer = 0

        '        For Each row As Data.DataRow In PersonaGeneral.Rows()
        '            If PersonaGeneral.Rows(contador).Item("EXITO") = 1 Then

        '                dtPersonasEx.Rows.Add(PersonaGeneral.Rows(contador).Item("FILA"), PersonaGeneral.Rows(contador).Item("IDPERSONA"), PersonaGeneral.Rows(contador).Item("NOMBRES"), PersonaGeneral.Rows(contador).Item("APELLIDOS"))
        '            Else

        '                dtPersonasNoEx.Rows.Add(PersonaGeneral.Rows(contador).Item("FILA"), PersonaGeneral.Rows(contador).Item("NOMBRES"), PersonaGeneral.Rows(contador).Item("APELLIDOS"), PersonaGeneral.Rows(contador).Item("DETALLE"))
        '            End If
        '            contador += 1
        '        Next

        '        If dtPersonasEx.Rows.Count > 0 Then
        '            dag_Persona_Ex.Visible = True
        '            dag_Persona_Ex.DataSource = dtPersonasEx
        '            dag_Persona_Ex.DataBind()
        '        Else
        '            dag_Persona_Ex.Visible = False
        '        End If

        '        If dtPersonasNoEx.Rows.Count > 0 Then
        '            Dag_Persona_NoEx.Visible = True
        '            Dag_Persona_NoEx.DataSource = dtPersonasNoEx
        '            Dag_Persona_NoEx.DataBind()
        '        Else
        '            Dag_Persona_NoEx.Visible = False
        '        End If
        '        ' DELETE THE FILE
        '        System.IO.File.Delete(fileName)
        '    Else
        '        ' NOTIFY THE USER WHY THEIR FILE WAS NOT UPLOADED
        '        lbl_Status_Carga.Text = "Error: Sólo puede subir archivos con la siguientes extensiones:(*.csv)"

        '        dag_Persona_Ex.Visible = False
        '        Dag_Persona_NoEx.Visible = False
        '        AsyncFileUpload1.Enabled = True
        '        btn_CargarArch.Enabled = True
        '    End If
        'Else
        '    ' NOTIFY THE USER THET A FILE WAS NOT UPLOADED
        '    lbl_Status_Carga.Text = "Error: Seleccione un archivo."
        '    dag_Persona_Ex.Visible = False
        '    Dag_Persona_NoEx.Visible = False
        '    AsyncFileUpload1.Enabled = True
        '    btn_CargarArch.Enabled = True
        'End If

    End Sub


    Private Function ValidaENTEROS(ByVal valor As String) As Boolean

        Return Regex.IsMatch(valor, ("^\d+$"))

    End Function

    Private Function ValidaDECIMAL(ByVal valor As String) As Boolean

        Return Regex.IsMatch(valor, ("^\d+(\.\d{1,7})?$"))

    End Function

    Private Function validaNOMBRE(ByVal valor As String) As Boolean
        Return Regex.IsMatch(valor, ("^[a-zñáéíóúA-ZÁÉÍÓÚ ]*$"))

    End Function

    Private Function ValidaFecha(ByVal fecha As String) As Boolean

        Return Regex.IsMatch(fecha, ("^\d{4}-((0[1-9])|(1[012]))-((0[1-9]|[12]\d)|3[01])$"))

    End Function

End Class