Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_VEN_AMORTIZACION_MULTIPLE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Amortizador Múltiple", "Amortizador Múltiple")
        If Not Me.IsPostBack Then

            'LlenaInstituciones()
            Llenobancos()
            CreaTablas()

        End If
    End Sub

    Private Sub Llenobancos()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_banco.Items.Clear()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
        Session("rs") = Session("cmd").Execute()
        cmb_banco.Items.Add(elija)
        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                cmb_banco.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
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

    Private Sub DelHDFile(ByVal File As String)
        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If
        System.IO.File.Delete(File)
    End Sub

    Protected Sub btn_CargarArch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CargarArch.Click

        btn_CargarArch.Enabled = False
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
                    dtArchivo.Rows.Add(linea)

                    Dim arrVar As String() = Split(linea, ",")
                    contadorFila = contadorFila + 1
                    If Resultado = "OK" Then

                        'Variable = Variable + " *** " + oRead.ReadLine()
                        If arrVar(0) = "" Or arrVar(0) = Nothing Or ValidaENTEROS(arrVar(0)) = False Then
                            Resultado = "Error: Id de persona incorrecto, en la fila: " + CStr(contadorFila)
                        ElseIf arrVar(1) = "" Or arrVar(1) = Nothing Or validaNOMBRE(arrVar(1)) = False Then
                            Resultado = "Error: Nombre incorrecto, en la fila: " + CStr(contadorFila)
                        ElseIf arrVar(2) = "" Or arrVar(2) = Nothing Or ValidaENTEROS(arrVar(2)) = False Then
                            Resultado = "Error: Folio incorrecto, en la fila: " + CStr(contadorFila)
                        ElseIf arrVar(3) = "" Or arrVar(3) = Nothing Or ValidaDECIMAL(arrVar(3)) = False Then
                            Resultado = "Error: Págo incorrecto, en la fila: " + CStr(contadorFila)
                        End If
                    End If

                Loop
            End Using



            If Resultado = "OK" Then
                fs.Position = 0


                Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
                Dim dtPagos As New Data.DataTable()




                Dim strConnString As String 'INSERTO BINARIO EN BD
                strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
                Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
                Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_PAGOS_X_INST_ARCHIVO", sqlConnection)
                'Stored Procedure 
                sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure
                'Parametros para la inserción del Stored Procedure
                sqlCmdObj.Parameters.AddWithValue("@DTARCHIVO", dtArchivo)
                sqlCmdObj.Parameters.AddWithValue("@IDSUC", Session("SUCID"))
                sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID"))
                sqlCmdObj.Parameters.AddWithValue("@IDSESION", Session("SESION"))



                sqlConnection.Open()

                Dim myReader As SqlDataReader = sqlCmdObj.ExecuteReader(CommandBehavior.CloseConnection)


                dtPagos.Load(myReader)

                myReader.Close()

                sqlConnection.Close()


                For i = 0 To dtPagos.Rows.Count - 1
                    If dtPagos.Rows(i).Item("AUXERROR") = "0" Then
                        btn_Aplicar.Enabled = True
                        btn_Cancelar.Enabled = True
                    ElseIf dtPagos.Rows(i).Item("AUXERROR") = "1" Then
                        btn_Aplicar.Enabled = False
                        btn_Cancelar.Enabled = True
                        Exit For
                    ElseIf dtPagos.Rows(i).Item("AUXERROR") = "2" Then
                        lbl_Status_Carga.Text = Session("rs").fields("ERROR").value.ToString
                        dag_Pagos.Visible = False
                        cmb_banco.Enabled = False
                        pnl_Pagos.Visible = False
                        'cmb_InstitucionB.Enabled = True
                        'cmb_PlanInstB.Enabled = True
                        AsyncFileUpload1.Enabled = True
                        btn_CargarArch.Enabled = True
                        Exit For
                    End If
                Next



                If Resultado = "OK" Then
                    lbl_Status_Carga.Text = ""
                    pnl_Pagos.Visible = True
                    dag_Pagos.Visible = True
                    cmb_banco.Enabled = True
                    dag_Pagos.DataSource = dtPagos
                    dag_Pagos.DataBind()
                    Session("TablaPagos") = dtPagos

                    Llenobancos()

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
        '        Dim dtPagos As New Data.DataTable()

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
        '        Session("cmd").CommandText = "SEL_PAGOS_X_INST_ARCHIVO"
        '        Session("rs") = Session("cmd").Execute()

        '        pnl_Pagos.Visible = True
        '        dag_Pagos.Visible = True
        '        cmb_banco.Enabled = True
        '        custDA.Fill(dtPagos, Session("rs"))
        '        dag_Pagos.DataSource = dtPagos
        '        dag_Pagos.DataBind()
        '        Session("TablaPagos") = dtPagos
        '        Session("Con").Close()
        '        Llenobancos()

        '        ' DELETE THE FILE
        '        System.IO.File.Delete(fileName)
        '    Else
        '        ' NOTIFY THE USER WHY THEIR FILE WAS NOT UPLOADED
        '        lbl_Status_Carga.Text = "Error: Sólo puede subir archivos con la siguientes extensiones:(*.csv)"
        '        dag_Pagos.Visible = False
        '        'cmb_InstitucionB.Enabled = True
        '        'cmb_PlanInstB.Enabled = True
        '        AsyncFileUpload1.Enabled = True
        '        btn_CargarArch.Enabled = True
        '    End If
        'Else
        '    ' NOTIFY THE USER THET A FILE WAS NOT UPLOADED
        '    lbl_Status_Carga.Text = "Error: Seleccione un archivo."
        '    dag_Pagos.Visible = False
        '    'cmb_InstitucionB.Enabled = True
        '    'cmb_PlanInstB.Enabled = True
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

    Protected Sub dag_Pagos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_Pagos.ItemDataBound

        'If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

        '    Dim ArchError As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "AUXERROR").ToString())

        '    If ArchError <> "0" Then
        '        If ArchError = "2" Then
        '            lbl_Status_Carga.Text = Session("rs").fields("ERROR").value.ToString
        '            dag_Pagos.Visible = False
        '            cmb_banco.Enabled = False
        '            pnl_Pagos.Visible = False
        '            'cmb_InstitucionB.Enabled = True
        '            'cmb_PlanInstB.Enabled = True
        '            AsyncFileUpload1.Enabled = True
        '            btn_CargarArch.Enabled = True
        '        Else
        '            btn_Aplicar.Enabled = False
        '            btn_Cancelar.Enabled = True
        '        End If
        '    Else
        '        btn_Aplicar.Enabled = True
        '        btn_Cancelar.Enabled = True
        '    End If
        'End If

    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click

        dag_Pagos.DataSource = Nothing
        dag_Pagos.DataBind()
        pnl_Pagos.Visible = False
        cmb_banco.Enabled = False
        btn_Aplicar.Enabled = False
        btn_Cancelar.Enabled = False
        'cmb_InstitucionB.Enabled = True
        'cmb_PlanInstB.Enabled = True
        AsyncFileUpload1.Enabled = True
        btn_CargarArch.Enabled = True
        Session("TablaPagos") = Nothing

    End Sub

    Protected Sub btn_aplicar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Aplicar.Click

        AplicaAmortizaciones()

        dag_Pagos.DataSource = Nothing
        dag_Pagos.DataBind()
        pnl_Pagos.Visible = False
        cmb_banco.Enabled = False
        btn_Aplicar.Enabled = False
        btn_Cancelar.Enabled = False

        'LlenaInstituciones()
        'cmb_InstitucionB.Enabled = True
        'cmb_PlanInstB.Items.Clear()
        'cmb_PlanInstB.Enabled = False
        AsyncFileUpload1.Enabled = False
        btn_CargarArch.Enabled = False

        lbl_Status_Carga.Text = "Pagos realizados con exito!"

    End Sub

    Private Sub AplicaAmortizaciones()

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("INS_CONTA_AMORTIZACION_X_PLAN_INST", connection)
            insertCommand.CommandTimeout = 600
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure

            'Session("parm") = New SqlParameter("IDINSTITUCION", SqlDbType.Int)
            'Session("parm").Value = cmb_InstitucionB.SelectedItem.Value
            'insertCommand.Parameters.Add(Session("parm"))

            'Session("parm") = New SqlParameter("CLAVEPLAN", SqlDbType.VarChar)
            'Session("parm").Value = cmb_PlanInstB.SelectedItem.Value
            'insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("PAGOS", SqlDbType.Structured)
            Session("parm").Value = Session("TablaPagos")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("CTABANCO", SqlDbType.Int)
            Session("parm").Value = cmb_banco.SelectedItem.Value
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDSUC", SqlDbType.Int)
            Session("parm").Value = Session("SUCID")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
            Session("parm").Value = Session("USERID")
            insertCommand.Parameters.Add(Session("parm"))

            Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
            Session("parm").Value = Session("Sesion")
            insertCommand.Parameters.Add(Session("parm"))

            insertCommand.ExecuteNonQuery()
            connection.Close()

        End Using

        'EnviaMail()

    End Sub

    Private Sub EnviaMail()

        Const ConfigNamespace As String =
                   "http://schemas.microsoft.com/cdo/configuration/"
        Dim oMsg As New CDO.Message()
        Dim iConfig As New CDO.Configuration()
        Dim Flds As ADODB.Fields = iConfig.Fields
        With iConfig.Fields
            .Item(ConfigNamespace & "smtpserver").Value = Session("MAIL_SERVER")
            .Item(ConfigNamespace & "smtpserverport").Value = Session("MAIL_SERVER_PORT")
            .Item(ConfigNamespace & "sendusing").Value =
                CDO.CdoSendUsing.cdoSendUsingPort
            .Item(ConfigNamespace & "sendusername").Value = Session("MAIL_SERVER_USER")
            .Item(ConfigNamespace & "sendpassword").Value = Session("MAIL_SERVER_PWD")
            .Item(ConfigNamespace & "smtpauthenticate").Value =
                CDO.CdoProtocolsAuthentication.cdoBasic
            .Update()
        End With

        With oMsg
            .Configuration = iConfig
            .From = Session("MAIL_SERVER_FROM")
            .To = "ffagoaga@creditured.com"
            .CC = "carlos@infoquest.com.mx"
            .Subject = "Amortización de Préstamos"
            .TextBody = "Buen Día a Todos" + vbCrLf + vbCrLf +
                "Se les informa que se han aplicado las amortizaciones correspondientes los préstamos: " + vbCrLf +
                "Atentamente" + vbCrLf + vbCrLf + "MAS.Core" + vbCrLf +
                "Red de Ayuda Solidaria"
            .Send()
        End With
        oMsg = Nothing
        iConfig = Nothing

    End Sub

End Class