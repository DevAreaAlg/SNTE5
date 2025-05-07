Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports WnvWordToPdf

Public Class PEN_PROC_UPLOAD_TOTAL_TRABAJADOR
    Inherits Page

    Dim tableTrabajadores As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Proceso", "Procesa información agremiado")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            CargaTipoMov()
        End If

    End Sub

#Region "Carga de Combos"

    Private Sub CargaTipoMov()

        cmb_Layout.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_Layout.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPO_MOV_LAYOUT"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TIPO_MOV").Value.ToString, Session("rs").Fields("ID_TIPO_MOV").Value.ToString)
            cmb_Layout.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub CargaPeriodos()

        Try
            cmb_Quincenas.Items.Clear()

            Dim elija As New ListItem("ELIJA", "0")
            cmb_Quincenas.Items.Add(elija)
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("LAYOUT", Session("adVarChar"), Session("adParamInput"), 10, cmb_Layout.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FECHAS_CICLO_ACTIVO"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("FECHA").Value.ToString, Session("rs").Fields("FECHADESC").Value.ToString)
                cmb_Quincenas.Items.Add(item)
                Session("rs").movenext()
            Loop

            Session("Con").Close()

            'ControlsWeb.LlenaDropDownList(cmb_Quincenas, "SEL_FECHAS_CICLO_ACTIVO", 0, "FECHA", "FECHADESC", "ELIJA")

        Catch ex As Exception
            lbl_status.Text = ex.Message.ToString()
        End Try

    End Sub


#End Region

    Protected Sub cmb_Layout_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Layout.SelectedIndexChanged

        If cmb_Layout.SelectedItem.Value = 1 Or cmb_Layout.SelectedItem.Value = 2 Or cmb_Layout.SelectedItem.Value = 4 Or cmb_Layout.SelectedItem.Value = 5 Or cmb_Layout.SelectedItem.Value = 6 Then
            Limpia()
            CargaPeriodos()
            cmb_Quincenas.Enabled = True
            FileUpload1.Visible = True
            FileUpload1.Enabled = True
            btn_Subir.Visible = True
            btn_Subir.Enabled = True
        ElseIf cmb_Layout.SelectedItem.Value = 3 Then
            Limpia()
            cmb_Quincenas.Enabled = False
            cmb_Quincenas.Items.Clear()
            FileUpload1.Visible = True
            FileUpload1.Enabled = True
            btn_Subir.Visible = True
            btn_Subir.Enabled = True
        Else
            Limpia()
            cmb_Quincenas.Enabled = False
            cmb_Quincenas.Items.Clear()
            FileUpload1.Visible = False
            FileUpload1.Enabled = False
            btn_Subir.Visible = False
            btn_Subir.Enabled = False
        End If

    End Sub

    Protected Sub cmb_Quincenas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Quincenas.SelectedIndexChanged

        If cmb_Quincenas.SelectedItem.Value = "ELIJA" Then
            Limpia()
        Else
            Session("FECHAQNA") = cmb_Quincenas.SelectedItem.Value
            LotesAplicados()
        End If

    End Sub

    Protected Sub btn_subir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Subir.Click

        lbl_status.Text = String.Empty
        Dim Extension As String
        If FileUpload1.HasFile = False Then

            lbl_status.Text = "Error: Debe seleccionar un archivo."
            Exit Sub

        ElseIf cmb_Layout.SelectedItem.Value = 3 Then

            If ((Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".XLSX") Or
                (Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".XLS")) Then

                Extension = Path.GetExtension(FileUpload1.PostedFile.FileName)
                ProcesaLayoutPrejubilados()
            Else

                lbl_status.Text = "Error: Debe seleccionar un archivo tipo XLS ó XLSX."
                Exit Sub

            End If

        Else

            If cmb_Quincenas.SelectedItem.Value = "0" Then

                lbl_status.Text = "Error: Debe seleccionar un corte de nómina."
                Exit Sub

            Else

                If cmb_Layout.SelectedItem.Value = 1 Then 'Layout de Agremiados

                    Dim nombreArchivo As String = FileUpload1.FileName.Replace("PFQ", "")
                    nombreArchivo = nombreArchivo.Replace(".DBF", "")
                    nombreArchivo = nombreArchivo.Replace(".dbf", "")

                    Dim quincenaCombo As String = cmb_Quincenas.SelectedItem.ToString().Replace("QUINCENA ", "")
                    quincenaCombo = quincenaCombo.Replace(" DEL ", "")
                    quincenaCombo = quincenaCombo.Substring(2, 4) + quincenaCombo.Substring(0, 2)

                    If (Path.GetExtension(FileUpload1.FileName.ToUpper()) <> ".DBF") Then

                        lbl_status.Text = "Error: Debe seleccionar un archivo tipo DBF."
                        Exit Sub

                    ElseIf (nombreArchivo <> quincenaCombo) Then

                        lbl_status.Text = "Error: El nombre del archivo, no coincide con periodo seleccionado."
                        Exit Sub

                    Else
                        Extension = Path.GetExtension(FileUpload1.PostedFile.FileName)
                        CargarArchivoDBF(Extension)
                    End If

                ElseIf cmb_Layout.SelectedItem.Value = 2 Or cmb_Layout.SelectedItem.Value = 6 Then 'Layout de Aportaciones y Descuentos

                    If ((Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".XLSX") Or (Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".XLS")) Then
                        Extension = Path.GetExtension(FileUpload1.PostedFile.FileName)
                        CargarArchivoExcelAportacionesFederales(Extension)
                        ' CargarArchivoXLSX()
                    Else

                        lbl_status.Text = "Error: Debe seleccionar un archivo tipo XLS ó XLSX."
                        Exit Sub

                    End If
                ElseIf cmb_Layout.SelectedItem.Value = 4 Then

                    If ((Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".XLSX") Or (Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".XLS")) Then
                        Extension = Path.GetExtension(FileUpload1.PostedFile.FileName)
                        CargarArchivoExcelDescuentos(Extension)
                        ' CargarArchivoXLSX()
                    Else

                        lbl_status.Text = "Error: Debe seleccionar un archivo tipo XLS ó XLSX."
                        Exit Sub

                    End If
                ElseIf cmb_Layout.SelectedItem.Value = 5 Then

                    If (Path.GetExtension(FileUpload1.FileName.ToUpper()) = ".TXT") Then
                        Extension = Path.GetExtension(FileUpload1.PostedFile.FileName)
                        CargarArchivoTxtAportacionesHomologados(Extension)
                        ' CargarArchivoXLSX()
                    Else

                        lbl_status.Text = "Error: Debe seleccionar un archivo tipo TXT."
                        Exit Sub

                    End If
                End If

            End If

        End If

    End Sub

#Region "Procesamiento de Layout de Prejubilados"

    Private Sub ProcesaLayoutPrejubilados()

        dgd_prejubilados.Visible = False
        dgd_prejubilados.DataSource = Nothing
        dgd_prejubilados.DataBind()

        Dim url As String = ConfigurationManager.AppSettings.[Get]("urlPrejubilados")
        Dim filePath As String = url + "\" + FileUpload1.FileName
        FileUpload1.SaveAs(filePath)

        Dim dtPrejubilados As New DataTable
        dtPrejubilados.Columns.Add("NOMBRE", GetType(String))
        dtPrejubilados.Columns.Add("RFC", GetType(String))
        dtPrejubilados.Columns.Add("FECHAINICIO", GetType(String))
        dtPrejubilados.Columns.Add("FECHATERMINO", GetType(String))
        dtPrejubilados.Columns.Add("ESTATUS", GetType(String))

        Dim m_sConn1 As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties=""Excel 12.0;HDR=YES"""
        Dim hojaExcel As String = "Hoja1"
        'Dim hojaExcel As String = "2020"
        Dim cn As New OleDbConnection(m_sConn1)
        Dim da As New OleDbDataAdapter("select * from [" & hojaExcel & "$]", m_sConn1)
        Dim ds As New DataSet
        da.Fill(ds)
        dtPrejubilados = ds.Tables(0)

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("INS_PREJUBILADOS_LAYOUT", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("PREJUBILADOS_LAYOUT", SqlDbType.Structured)
                Session("parm").Value = dtPrejubilados
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim sqlRead As SqlDataReader = insertCommand.ExecuteReader()
                Dim dtEstatus As New DataTable()
                dtEstatus.Load(sqlRead)

                connection.Close()

                dgd_prejubilados.Visible = True
                dgd_prejubilados.DataSource = dtEstatus
                dgd_prejubilados.DataBind()

            End Using

            DelHDFile(filePath)
            lbl_status_exito.Text = "Procesamiento de Layout exitoso."

        Catch ex As Exception
            lbl_status.Text = ex.Message
            'lbl_status.Text = "ERROR: Error de base de datos."
        Finally

        End Try

    End Sub

#End Region

    Private Sub CargarArchivoDBF(ByVal Extension As String)

        Dim NombreArchivo As String = FileUpload1.FileName
        Dim ResCarga As String = String.Empty
        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urlAgremiados")
        Dim filePath As String = urldigi + "\" + NombreArchivo
        FileUpload1.SaveAs(filePath)

        Dim testString As String = FileUpload1.FileName.ToString
        Dim anioL As Integer = testString.Substring(3, 4)
        Dim quincena As Integer = testString.Substring(7, 2)

        Session("QUINCENA") = quincena
        Session("ANIO") = anioL
        Session("IDINST") = "1"

        Dim dtValidaDes As New DataTable
        dtValidaDes.Columns.Add("RFC", GetType(String))
        dtValidaDes.Columns.Add("CURP", GetType(String))
        dtValidaDes.Columns.Add("CCT", GetType(String))
        dtValidaDes.Columns.Add("NOMBRE", GetType(String))
        dtValidaDes.Columns.Add("SUM_PERCEP", GetType(String))
        dtValidaDes.Columns.Add("SUM_DEDUCC", GetType(String))
        dtValidaDes.Columns.Add("NETO", GetType(String))
        dtValidaDes.Columns.Add("UNI_DIST", GetType(String))
        dtValidaDes.Columns.Add("PLAZA", GetType(String))

        Dim dtValidaColumns As New DataTable

        'Ejecutar aplicación de consola que lee el DBF y lo convierte en un archivo CSV
        Dim pHelp As New System.Diagnostics.ProcessStartInfo
        pHelp.FileName = ConfigurationManager.AppSettings.[Get]("applicationConsole")
        pHelp.Arguments = filePath
        pHelp.UseShellExecute = True
        pHelp.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
        Dim proc As System.Diagnostics.Process = System.Diagnostics.Process.Start(pHelp)
        proc.WaitForExit()

        'Leer archivo CSV
        Dim filePathCSV As String = filePath.Replace(".dbf", ".csv").Replace(".DBF", ".csv")
        Dim linea As String = ""
        Dim contadorFila As Integer = 0

        lbl_status.Text = filePathCSV

        Using oRead As New System.IO.StreamReader(filePathCSV, False)
            Do While Not oRead.EndOfStream
                linea = oRead.ReadLine()
                contadorFila = contadorFila + 1

                If contadorFila > 0 Then

                    Dim arrVar As String() = Split(linea, ",")

                    If arrVar.Count <> 9 Then
                        lbl_status.Text = "Error: el layout no cumple con el número de columnas esperadas"
                        Exit Sub
                    Else
                        dtValidaDes.Rows.Add(arrVar(0), arrVar(1), arrVar(2), arrVar(3), arrVar(4), arrVar(5), arrVar(6), arrVar(7), arrVar(8))
                    End If

                End If
            Loop
        End Using


        Try
            Dim dsDatos As DataSet = New DataSet()
            Dim Hsh As Hashtable = New Hashtable()
            Hsh.Add("@ARCHIVO", dtValidaDes)
            Hsh.Add("@IDINSTITUCION", 1)
            Hsh.Add("@FECHA", cmb_Quincenas.SelectedValue.ToString())
            Hsh.Add("@NUMQUINCENA", quincena)
            Hsh.Add("@ANIO", anioL)
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))

            Dim yaProcesado As String = ""

            'DataAccess.RegresaUnaCadena("INS_VALIDA_ARCHIVO_NOMINA_PROCESADO", Hsh)

            If yaProcesado = "PROCESADO" Then
                ResCarga = "Error: Se intento realizar la carga de un layout de un periodo ya procesado."
            Else
                Dim da As New DataAccess()
                dsDatos = da.RegresaDataSet("INS_VALIDA_ARCHIVO_NOMINA", Hsh)

                If dsDatos.Tables(0).Rows().Count() > 0 Then
                    For i As Integer = 0 To dsDatos.Tables(0).Rows().Count() - 1
                        Session("RES") = dsDatos.Tables(0).Rows(i).Item(0).ToString()
                        Session("LOTE") = dsDatos.Tables(0).Rows(i).Item(1).ToString()
                        Session("NUMQNA") = dsDatos.Tables(0).Rows(i).Item(2).ToString()
                        Session("ANIO") = dsDatos.Tables(0).Rows(i).Item(3).ToString()
                        Session("IDINSTI") = dsDatos.Tables(0).Rows(i).Item(4).ToString()
                        Session("REGISTROS") = dsDatos.Tables(0).Rows(i).Item(5).ToString()
                    Next
                End If

                If Session("RES") = "OK" Then
                    CargaLayoutEnBD(cmb_Layout.SelectedValue, Extension)
                    ResCarga = "Éxito: Se validó correctamente la información del layout."
                ElseIf Session("RES") = "YAEXISTE" Then
                    ResCarga = "Error: Ya existe un layout pendiente por procesar. Si desea cargar otro layout, debe cancelar el layout en estatus Pendiente."
                ElseIf Session("RES") = "FALSE" Then
                    DelHDFile(filePath)
                    DelHDFile(filePathCSV)
                End If

            End If
            ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
            LotesAplicados()
            CargaPeriodos()
            lbl_status.Text = ResCarga
        Catch ex As Exception
            lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message.ToString()
        End Try

    End Sub

    Private Sub CargarArchivoExcelAportacionesFederales(ByVal Extension As String)

        Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ResCarga As String = String.Empty
        Dim auxiliar As String = String.Empty

        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urlAportaciones")
        Dim filePath As String = ""
        Dim filePathXLS As String = ""
        Dim aux As String

        aux = FileName

        filePath = urldigi + "\" + FileUpload1.FileName

        Dim conStr As String = ""

        Select Case Extension
            Case ".xls"
                'Excel 97-03 
                aux = aux.Remove(aux.Length - 4)

                filePathXLS = urldigi + "\" + aux + ".xlsx"

                'lbl_status.Text = filePathXLS
                FileUpload1.SaveAs(filePath)
                Dim Document = New Aspose.Cells.Workbook(filePath)
                Document.Save(filePathXLS, Aspose.Cells.SaveFormat.Xlsx)
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                      .ConnectionString
                DelHDFile(filePath)
                Exit Select
            Case ".xlsx"
                'Excel 07 
                FileUpload1.SaveAs(filePath)
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                      .ConnectionString
                Exit Select
        End Select

        If Extension = ".xls" Then
            conStr = String.Format(conStr, filePathXLS, "Yes")
        Else
            conStr = String.Format(conStr, filePath, "Yes")
        End If


        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)


        Dim SheetName As String

        If Extension = ".xls" Then
            SheetName = dtExcelSchema.Rows(1)("TABLE_NAME").ToString()
        Else
            SheetName = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        End If

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        Dim ds As New DataSet
        oda.Fill(ds)
        connExcel.Close()

        Dim dtValidaDes As New DataTable
        dtValidaDes.Columns.Add("ENTIDAD", GetType(String))
        dtValidaDes.Columns.Add("PROCESO_DE_NOMINA", GetType(String))
        dtValidaDes.Columns.Add("NOMBRE", GetType(String))
        dtValidaDes.Columns.Add("PRIMER_APELLIDO", GetType(String))
        dtValidaDes.Columns.Add("SEGUNDO_APELLIDO", GetType(String))
        dtValidaDes.Columns.Add("CURP", GetType(String))
        dtValidaDes.Columns.Add("RFC", GetType(String))
        dtValidaDes.Columns.Add("CLC", GetType(String))
        dtValidaDes.Columns.Add("CVE_CONCEPTO", GetType(String))
        dtValidaDes.Columns.Add("DESCRIPCION", GetType(String))
        dtValidaDes.Columns.Add("IMPORTE", GetType(String))

        dtValidaDes = ds.Tables(0)

        Dim filterDT As DataTable = dtValidaDes.Clone()

        If dtValidaDes.Rows.Count() > 0 Then
            Dim rows As DataRow() = dtValidaDes.[Select]("CVE_CONCEPTO = '22'")

            For Each row As DataRow In rows
                filterDT.ImportRow(row)
            Next
        End If

        Try

            Dim dsDatos As DataSet = New DataSet()
            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@ARCHIVO", filterDT)
            Hsh.Add("@FECHA", cmb_Quincenas.SelectedValue.ToString())
            Hsh.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))
            Dim da As New DataAccess()
            dsDatos = da.RegresaDataSet("INS_VALIDA_ARCHIVO_APORTACIONES", Hsh)

            If dsDatos.Tables(0).Rows().Count() > 0 Then
                For i As Integer = 0 To dsDatos.Tables(0).Rows().Count() - 1
                    Session("RES") = dsDatos.Tables(0).Rows(i).Item(0).ToString()
                    Session("LOTE") = dsDatos.Tables(0).Rows(i).Item(1).ToString()
                    Session("NUMQNA") = dsDatos.Tables(0).Rows(i).Item(2).ToString()
                    Session("ANIO") = dsDatos.Tables(0).Rows(i).Item(3).ToString()
                    Session("IDINSTI") = dsDatos.Tables(0).Rows(i).Item(4).ToString()
                    Session("REGISTROS") = dsDatos.Tables(0).Rows(i).Item(5).ToString()
                Next
            End If

            If Session("RES") = "OK" Then
                CargaLayoutEnBD(cmb_Layout.SelectedValue, Extension)
                ResCarga = "Éxito: Se validó correctamente la información del layout."
            ElseIf Session("RES") = "YAEXISTE" Then
                ResCarga = "Error: Ya existe un layout pendiente por procesar."
            ElseIf Session("RES") = "FALSE" Then
                If Extension = ".xls" Then
                    DelHDFile(filePathXLS)
                Else
                    DelHDFile(filePath)
                End If

            End If
            ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
            LotesAplicados()
            CargaPeriodos()
            lbl_status.Text = ResCarga
        Catch ex As Exception
            lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message.ToString()
        End Try

    End Sub

    Private Sub CargarArchivoTxtAportacionesHomologados(ByVal Extension As String)

        Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ResCarga As String = String.Empty
        Dim auxiliar As String = String.Empty

        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urlAportacionesH")
        Dim filePath As String = ""


        filePath = urldigi + "\" + FileUpload1.FileName

        Dim conStr As String = ""

        FileUpload1.SaveAs(filePath)

        lbl_status.Text = filePath


        Try

            Dim dsDatos As DataSet = New DataSet()
            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@RUTA", filePath)
            Hsh.Add("@FECHA", cmb_Quincenas.SelectedValue.ToString())
            Hsh.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))
            Dim da As New DataAccess()
            dsDatos = da.RegresaDataSet("INS_VALIDA_ARCHIVO_APORTACIONES_H", Hsh)

            If dsDatos.Tables(0).Rows().Count() > 0 Then
                For i As Integer = 0 To dsDatos.Tables(0).Rows().Count() - 1
                    Session("RES") = dsDatos.Tables(0).Rows(i).Item(0).ToString()
                    Session("LOTE") = dsDatos.Tables(0).Rows(i).Item(1).ToString()
                    Session("NUMQNA") = dsDatos.Tables(0).Rows(i).Item(2).ToString()
                    Session("ANIO") = dsDatos.Tables(0).Rows(i).Item(3).ToString()
                    Session("IDINSTI") = dsDatos.Tables(0).Rows(i).Item(4).ToString()
                    Session("REGISTROS") = dsDatos.Tables(0).Rows(i).Item(5).ToString()
                Next
            End If

            If Session("RES") = "OK" Then
                CargaLayoutEnBD(cmb_Layout.SelectedValue, Extension)
                ResCarga = "Éxito: Se validó correctamente la información del layout."
            ElseIf Session("RES") = "YAEXISTE" Then
                ResCarga = "Error: Ya existe un layout pendiente por procesar."
            ElseIf Session("RES") = "FALSE" Then

                DelHDFile(filePath)

            End If
            ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
            LotesAplicados()
            CargaPeriodos()
            lbl_status.Text = ResCarga
        Catch ex As Exception
            lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message.ToString()

        End Try

    End Sub

    Private Sub CargarArchivoExcelDescuentos(ByVal Extension As String)

        Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ResCarga As String = String.Empty
        Dim auxiliar As String = String.Empty

        Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urlDescuentos")
        Dim filePath As String = ""
        Dim filePathXLS As String = ""
        Dim aux As String

        aux = FileName

        filePath = urldigi + "\" + FileUpload1.FileName

        Dim conStr As String = ""

        Select Case Extension
            Case ".xls"
                'Excel 97-03 
                aux = aux.Remove(aux.Length - 4)

                filePathXLS = urldigi + "\" + aux + ".xlsx"

                'lbl_status.Text = filePathXLS
                FileUpload1.SaveAs(filePath)
                Dim Document = New Aspose.Cells.Workbook(filePath)
                Document.Save(filePathXLS, Aspose.Cells.SaveFormat.Xlsx)
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                      .ConnectionString
                DelHDFile(filePath)
                Exit Select
            Case ".xlsx"
                'Excel 07 
                FileUpload1.SaveAs(filePath)
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                      .ConnectionString
                Exit Select
        End Select

        If Extension = ".xls" Then
            conStr = String.Format(conStr, filePathXLS, "Yes")
        Else
            conStr = String.Format(conStr, filePath, "Yes")
        End If


        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)


        Dim SheetName As String

        If Extension = ".xls" Then
            SheetName = dtExcelSchema.Rows(1)("TABLE_NAME").ToString()
        Else
            SheetName = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        End If

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        Dim ds As New DataSet
        oda.Fill(ds)
        connExcel.Close()

        Dim dtValidaDes As New DataTable
        dtValidaDes.Columns.Add("ENTIDAD", GetType(String))
        dtValidaDes.Columns.Add("PROCESO_DE_NOMINA", GetType(String))
        dtValidaDes.Columns.Add("NOMBRE", GetType(String))
        dtValidaDes.Columns.Add("PRIMER_APELLIDO", GetType(String))
        dtValidaDes.Columns.Add("SEGUNDO_APELLIDO", GetType(String))
        dtValidaDes.Columns.Add("CURP", GetType(String))
        dtValidaDes.Columns.Add("RFC", GetType(String))
        dtValidaDes.Columns.Add("CLC", GetType(String))
        dtValidaDes.Columns.Add("CVE_CONCEPTO", GetType(String))
        dtValidaDes.Columns.Add("DESCRIPCION", GetType(String))
        dtValidaDes.Columns.Add("IMPORTE", GetType(String))

        dtValidaDes = ds.Tables(0)

        Dim filterDT As DataTable = dtValidaDes.Clone()

        If dtValidaDes.Rows.Count() > 0 Then
            Dim rows As DataRow() = dtValidaDes.[Select]("CVE_CONCEPTO = 'PA'")

            For Each row As DataRow In rows
                filterDT.ImportRow(row)
            Next
        End If
        'Dim rowsX As DataRow() = dtValidaDes.[Select]("CVE_CONCEPTO = 'PA'")

        Try

            Dim dsDatos As DataSet = New DataSet()
            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@FECHA", cmb_Quincenas.SelectedValue.ToString())
            Hsh.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))


            Dim da As New DataAccess()
            Dim YAPROCESADO As String = da.RegresaUnaCadena("INS_VALIDA_ARCHIVO_PROCESADO", Hsh)
            'lbl_status.Text = "QUINCENA: " + cmb_Quincenas.SelectedValue.ToString() + " TIPO MOV: " + CStr(cmb_Layout.SelectedItem.Value) + " USERID: " + Session("USERID") + " SESION: " + Session("Sesion") + " PROCESADO: " + YAPROCESADO
            If YAPROCESADO = "PROCESADO" Then
                ResCarga = "Error: Se intentó realizar la carga de un layout de un periodo ya procesado."
            Else
                Dim Hsh2 As Hashtable = New Hashtable()

                Hsh2.Add("@ARCHIVO", filterDT)
                Hsh2.Add("@FECHA", cmb_Quincenas.SelectedValue.ToString())
                Hsh2.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
                Hsh2.Add("@IDUSER", Session("USERID"))
                Hsh2.Add("@SESION", Session("Sesion"))
                Dim dac As New DataAccess()
                dsDatos = dac.RegresaDataSet("INS_VALIDA_ARCHIVO_DESCUENTOS", Hsh2)

                If dsDatos.Tables(0).Rows().Count() > 0 Then
                    For i As Integer = 0 To dsDatos.Tables(0).Rows().Count() - 1
                        Session("RES") = dsDatos.Tables(0).Rows(i).Item(0).ToString()
                        Session("LOTE") = dsDatos.Tables(0).Rows(i).Item(1).ToString()
                        Session("NUMQNA") = dsDatos.Tables(0).Rows(i).Item(2).ToString()
                        Session("ANIO") = dsDatos.Tables(0).Rows(i).Item(3).ToString()
                        Session("IDINSTI") = dsDatos.Tables(0).Rows(i).Item(4).ToString()
                        Session("REGISTROS") = dsDatos.Tables(0).Rows(i).Item(5).ToString()
                    Next
                End If

                If Session("RES") = "OK" Then
                    CargaLayoutEnBD(cmb_Layout.SelectedValue, Extension)
                    ResCarga = "Éxito: Se validó correctamente la información del layout."
                ElseIf Session("RES") = "YAEXISTE" Then
                    ResCarga = "Error: Ya existe un layout pendiente por procesar."
                ElseIf Session("RES") = "FALSE" Then
                    DelHDFile(filePath)
                End If
            End If

            ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
            LotesAplicados()
            CargaPeriodos()
            lbl_status.Text = ResCarga

        Catch ex As Exception
            lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message.ToString()
        End Try
    End Sub


    Private Function selanioqna() As String
        Dim Hsh As Hashtable = New Hashtable()

        Hsh.Add("@FECHA", cmb_Quincenas.SelectedValue.ToString())
        Dim da As New DataAccess()
        Dim ANIOQNA As String = da.RegresaUnaCadena("SEL_QUINCENA_FECHA", Hsh)
        Return ANIOQNA
    End Function

    Private Sub CargaLayoutEnBD(oTipo As Integer, ByVal oExtension As String)

        Try

            Dim filePath As String = FileUpload1.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim urldigi As String = String.Empty
            Dim aux As String = ""
            aux = filename
            If oTipo = 1 Then 'Agremiados
                filename = filename.Replace(".DBF", ".csv")
                filename = filename.Replace(".dbf", ".csv")
                urldigi = ConfigurationManager.AppSettings.[Get]("urlAgremiados")
            ElseIf oTipo = 2 Or oTipo = 6 Then

                urldigi = ConfigurationManager.AppSettings.[Get]("urlAportaciones")

                If Extension = ".xls" Then
                    aux = aux.Remove(aux.Length - 4)
                    filename = aux + ".xlsx"
                Else
                    filename = Path.GetFileName(filePath)
                End If
            ElseIf oTipo = 4 Then

                urldigi = ConfigurationManager.AppSettings.[Get]("urlDescuentos")

                If Extension = ".xls" Then
                    aux = aux.Remove(aux.Length - 4)
                    filename = aux + ".xlsx"
                Else
                    filename = Path.GetFileName(filePath)
                End If

            ElseIf oTipo = 5 Then

                urldigi = ConfigurationManager.AppSettings.[Get]("urlAportacionesH")

                filename = Path.GetFileName(filePath)

            End If

            Dim rutaLay As String = urldigi + "\" + filename

            Dim Hsh As Hashtable = New Hashtable()
            Hsh.Add("@LOTE", Session("LOTE").ToString)
            Hsh.Add("@NUMQNA", Session("NUMQNA").ToString)
            Hsh.Add("@ANIO", Session("ANIO").ToString)
            Hsh.Add("@IDINST", Session("IDINSTI").ToString)
            Hsh.Add("@RUTA", rutaLay)
            Hsh.Add("@REGISTROS", Session("REGISTROS").ToString)
            Hsh.Add("@ESTATUS", 2)
            Hsh.Add("@TIPO", oTipo)
            Hsh.Add("@EXTENSION", oExtension)
            Hsh.Add("@IDUSER", Session("USERID").ToString)
            Hsh.Add("@SESION", Session("Sesion").ToString)
            Dim da As New DataAccess()
            da.GuardarActualizar("INS_MSTAPORTACIONINST", Hsh)

        Catch ex As Exception
            lbl_status.Text = ex.ToString
        End Try
    End Sub

    Private Sub LotesAplicados()

        Try
            'Se limpia el grid para que se recargue.
            dag_PagosXAplicar.DataBind()

            Dim dt As DataTable = New DataTable()
            Dim Hsh As Hashtable = New Hashtable()
            Hsh.Add("@FECHA", Session("FECHAQNA"))
            Hsh.Add("@IDINST", 1)
            Hsh.Add("@TIPO", cmb_Layout.SelectedValue)
            Dim da As New DataAccess()
            dt = da.RegresaDataTable("SEL_LOTES_DEPENDENCIA", Hsh)
            dag_PagosXAplicar.DataSource = dt
            dag_PagosXAplicar.DataBind()


            If dag_PagosXAplicar.Items.Count > 0 Then
                dag_PagosXAplicar.Visible = True
            Else
                FileUpload1.Visible = True
                FileUpload1.Enabled = True
                btn_Subir.Visible = True
                btn_Subir.Enabled = True
                dag_PagosXAplicar.Visible = False
            End If
            lbl_status.Text = ""
        Catch ex As Exception
            lbl_status.Text = ex.Message.ToString()
        End Try

    End Sub

    Private Sub dag_PagosXAplicar_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_PagosXAplicar.ItemCommand


        Dim lote As Integer = CInt(e.Item.Cells(0).Text)
        Dim anio As Integer = CInt(e.Item.Cells(9).Text)
        Dim quincena As Integer = CInt(e.Item.Cells(10).Text)
        Dim correctos As Integer = CInt(e.Item.Cells(11).Text)
        Dim incorrectos As Integer = CInt(e.Item.Cells(12).Text)
        Dim tipoAporta As Integer = CInt(e.Item.Cells(15).Text)

        If (e.CommandName = "aplica") Then
            RegistrosCorrectosIncorrectos(1, lote, anio, quincena, tipoAporta)
        End If

        If (e.CommandName = "noaplica") Then
            RegistrosCorrectosIncorrectos(0, lote, anio, quincena, tipoAporta)
        End If

        If (e.CommandName = "procesar") Then
            If incorrectos > 0 Then
                lbl_status.Text = "Error: No se puede procesar el layot si contiene errores"
            Else

                If tipoAporta = 1 Then
                    ProcesaInfoLayoutNomina(lote, anio, quincena)
                ElseIf tipoAporta = 2 Or tipoAporta = 6 Then
                    ProcesaInfoLayoutAportacionesFederales(lote, anio, quincena)
                ElseIf tipoAporta = 4 Then
                    ProcesaInfoLayoutDescuentos(lote, anio, quincena)
                ElseIf tipoAporta = 5 Then
                    ProcesaInfoLayoutAportacionesHomologados(lote, anio, quincena)
                End If

            End If
        End If

        If (e.CommandName = "cancelar") Then
            If incorrectos > 0 Then
                lbl_status.Text = "Error: No se puede cancelar los procesos con errores"
            Else
                CancelaProcesoLayout(lote, anio, quincena, tipoAporta)
                LotesAplicados()
            End If
        End If

    End Sub

    Protected Sub dag_PagosXAplicar_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dag_PagosXAplicar.ItemDataBound
        If (e.Item.Cells(13).Text.ToString() = "CANCELADO") Then
            e.Item.Cells(5).Enabled = False
            e.Item.Cells(6).Enabled = False

            e.Item.Cells(5).ForeColor = Drawing.Color.Gray
            e.Item.Cells(6).ForeColor = Drawing.Color.Gray
        End If

        If (e.Item.Cells(13).Text.ToString() = "FALLIDO") Then
            e.Item.Cells(5).Enabled = False
            e.Item.Cells(6).Enabled = False

            e.Item.Cells(5).ForeColor = Drawing.Color.Gray
            e.Item.Cells(6).ForeColor = Drawing.Color.Gray
        End If

        If (e.Item.Cells(13).Text.ToString() = "PROCESADO") Then
            e.Item.Cells(5).Enabled = False
            e.Item.Cells(6).Enabled = False

            e.Item.Cells(5).ForeColor = Drawing.Color.Gray
            e.Item.Cells(6).ForeColor = Drawing.Color.Gray
        End If
    End Sub

    Private Sub CancelaProcesoLayout(oLote As Integer, oanio As Integer, oquincena As Integer, otipoMov As Integer)

        Dim HshUrl As Hashtable = New Hashtable()
        HshUrl.Add("@IDINSTI", "1")
        HshUrl.Add("@QUINCENA", oquincena)
        HshUrl.Add("@ANIO", oanio)
        HshUrl.Add("@LOTE", oLote)
        HshUrl.Add("@TIPOMOV", otipoMov)
        Dim da As New DataAccess()
        Session("RUTALAY") = da.RegresaUnaCadena("UPD_CANCELA_PROCESO_LAYOUT_LOTE", HshUrl)
        DelHDFile(Session("RUTALAY"))
        If System.IO.File.Exists(Session("RUTALAY").ToString) = True Then
            System.IO.File.Delete(Session("RUTALAY").ToString)
            lbl_status.Text = "Éxito: Proceso cancelado correctamente"
        End If

        Session("RUTALAY") = String.Empty
    End Sub

    Private Sub ProcesaInfoLayoutNomina(oLote As Integer, oanio As Integer, oquincena As Integer)
        lbl_status.Text = "Procesando..."

        Dim HshUrl As Hashtable = New Hashtable()
        HshUrl.Add("@IDINSTI", "1")
        HshUrl.Add("@QUINCENA", oquincena)
        HshUrl.Add("@ANIO", oanio)
        HshUrl.Add("@LOTE", oLote)
        HshUrl.Add("@TIPO", 1)
        Dim da As New DataAccess()
        Session("RUTALAY") = da.RegresaUnaCadena("SEL_RUTA_PROCESO_LAYOUT_LOTE", HshUrl)


        If Session("RUTALAY") <> "N/A" Then
            Dim dtValidaDes As New DataTable
            dtValidaDes.Columns.Add("RFC", GetType(String))
            dtValidaDes.Columns.Add("CURP", GetType(String))
            dtValidaDes.Columns.Add("CCT", GetType(String))
            dtValidaDes.Columns.Add("NOMBRE", GetType(String))
            dtValidaDes.Columns.Add("SUM_PERCEP", GetType(String))
            dtValidaDes.Columns.Add("SUM_DEDUCC", GetType(String))
            dtValidaDes.Columns.Add("NETO", GetType(String))
            dtValidaDes.Columns.Add("UNI_DIST", GetType(String))
            dtValidaDes.Columns.Add("PLAZA", GetType(String))

            Dim linea As String = ""
            Dim contadorFila As Integer = 0

            Using oRead As New System.IO.StreamReader(Session("RUTALAY").ToString(), False)
                Do While Not oRead.EndOfStream
                    linea = oRead.ReadLine()
                    contadorFila = contadorFila + 1

                    If contadorFila > 0 Then

                        Dim arrVar As String() = Split(linea, ",")

                        If arrVar.Count <> 9 Then
                            lbl_status.Text = "Error: el layout no cumple con el número de columnas esperadas"
                            Exit Sub
                        Else
                            dtValidaDes.Rows.Add(arrVar(0), arrVar(1), arrVar(2), arrVar(3), arrVar(4), arrVar(5), arrVar(6), arrVar(7), arrVar(8))
                        End If

                    End If
                Loop
            End Using

            Try
                Dim RES As String
                Dim Hsh As Hashtable = New Hashtable()
                Hsh.Add("@ARCHIVO", dtValidaDes)
                Hsh.Add("@IDINSTITUCION", 1)
                Hsh.Add("@NUMQUINCENA", oquincena)
                Hsh.Add("@ANIO", oanio)
                Hsh.Add("@LOTE", oLote)
                Hsh.Add("@FECHA", Session("FECHAQNA"))
                Hsh.Add("@IDEQUIPO", Session("ID_EQ"))
                Hsh.Add("@IDUSER", Session("USERID"))
                Hsh.Add("@SESION", Session("Sesion"))
                RES = da.RegresaUnaCadena("INS_PROCESA_INFO_LAYOUT_NOMINA", Hsh)
                Session("RES") = RES

            Catch ex As Exception
                lbl_status.Text = "ERROR: Error de base de datos." + ex.Message().ToString()
            Finally
                If Session("RES") = "OK" Then
                    ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
                    CargaPeriodos()
                    LotesAplicados()

                    Dim nomLayProc As String

                    nomLayProc = CStr(oLote) + "_" + CStr(oanio) + "_" + CStr(oquincena) + "_" + CStr(cmb_Layout.SelectedItem.Value) + "_NOMINA"

                    My.Computer.FileSystem.RenameFile(Session("RUTALAY").ToString, nomLayProc + "_PROCESADO.csv")
                    'DelHDFile(Session("RUTALAY").ToString)
                    lbl_status.Text = "Éxito: Información del layout procesada correctamente"
                    Session("RUTALAY") = String.Empty
                Else
                    lbl_status.Text = "Error: No se puede procesar la información en el estatus actual"
                End If
            End Try
        Else
            lbl_status.Text = "Error: El proceso ha sido cancelado o se está procesando"
        End If

    End Sub



    Private Sub ProcesaInfoLayoutAportacionesFederales(oLote As Integer, oanio As Integer, oquincena As Integer)

        Dim HshUrl As Hashtable = New Hashtable()
        HshUrl.Add("@IDINSTI", 1)
        HshUrl.Add("@QUINCENA", oquincena)
        HshUrl.Add("@ANIO", oanio)
        HshUrl.Add("@LOTE", oLote)
        HshUrl.Add("@TIPO", cmb_Layout.SelectedItem.Value)

        Dim da As New DataAccess()
        Session("RUTALAY") = da.RegresaUnaCadena("SEL_RUTA_PROCESO_LAYOUT_LOTE", HshUrl)

        If Session("RUTALAY") <> "N/A" Then

            Dim Result As String = "OK"
            Dim ResCarga As String = String.Empty
            Dim conStr As String = ""
            Dim extension As String
            conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                      .ConnectionString

            conStr = String.Format(conStr, Session("RUTALAY"), "Yes")

            Dim connExcel As New OleDbConnection(conStr)
            Dim cmdExcel As New OleDbCommand()
            Dim oda As New OleDbDataAdapter()
            Dim dt As New DataTable()

            cmdExcel.Connection = connExcel

            'Get the name of First Sheet 
            connExcel.Open()
            Dim dtExcelSchema As DataTable
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            Dim SheetName As String
            extension = da.RegresaUnaCadena("SEL_EXTENSION_PROCESO_LAYOUT_LOTE", HshUrl)

            If extension = ".xlsx" Then
                SheetName = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
            Else
                SheetName = dtExcelSchema.Rows(1)("TABLE_NAME").ToString()
            End If


            connExcel.Close()

            connExcel.Open()
            cmdExcel.CommandText = "SELECT * From [" & SheetName & "]" '&
            '"Where CVE_CONCEPTO = '22'"
            oda.SelectCommand = cmdExcel
            Dim ds As New DataSet
            oda.Fill(ds)
            connExcel.Close()


            Dim dtValidaDes As New DataTable
            dtValidaDes.Columns.Add("ENTIDAD", GetType(String))
            dtValidaDes.Columns.Add("PROCESO_DE_NOMINA", GetType(String))
            dtValidaDes.Columns.Add("NOMBRE", GetType(String))
            dtValidaDes.Columns.Add("PRIMER_APELLIDO", GetType(String))
            dtValidaDes.Columns.Add("SEGUNDO_APELLIDO", GetType(String))
            dtValidaDes.Columns.Add("CURP", GetType(String))
            dtValidaDes.Columns.Add("RFC", GetType(String))
            dtValidaDes.Columns.Add("CLC", GetType(String))
            dtValidaDes.Columns.Add("CVE_CONCEPTO", GetType(String))
            dtValidaDes.Columns.Add("DESCRIPCION", GetType(String))
            dtValidaDes.Columns.Add("IMPORTE", GetType(String))

            dtValidaDes = ds.Tables(0)

            Dim filterDT As DataTable = dtValidaDes.Clone()

            If dtValidaDes.Rows.Count() > 0 Then
                Dim rows As DataRow() = dtValidaDes.[Select]("CVE_CONCEPTO = '22'")

                For Each row As DataRow In rows
                    filterDT.ImportRow(row)
                Next
            End If

            Try

                Dim RES As String
                Dim Hsh As Hashtable = New Hashtable()

                Hsh.Add("@ARCHIVO", filterDT)
                Hsh.Add("@IDINSTITUCION", 1)
                Hsh.Add("@NUMQUINCENA", oquincena)
                Hsh.Add("@ANIO", oanio)
                Hsh.Add("@LOTE", oLote)
                Hsh.Add("@FECHA", Session("FECHAQNA"))
                Hsh.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
                Hsh.Add("@IDEQUIPO", Session("ID_EQ"))
                Hsh.Add("@IDUSER", Session("USERID"))
                Hsh.Add("@SESION", Session("Sesion"))
                RES = da.RegresaUnaCadena("INS_PROCESA_INFO_LAYOUT_APORTACIONES", Hsh)
                Session("RES") = RES
                If Session("RES") = "OK" Then

                    ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
                    CargaPeriodos()
                    LotesAplicados()


                    lbl_status.Text = "Éxito: Información del layout procesada correctamente"
                    Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urlAportacionesProc")
                    Dim nomLayProc As String

                    nomLayProc = CStr(oLote) + "_" + CStr(oanio) + "_" + CStr(oquincena) + "_" + CStr(cmb_Layout.SelectedItem.Value) + "_APORTACIONES_FEDERALES"

                    My.Computer.FileSystem.RenameFile(Session("RUTALAY").ToString, nomLayProc + "_PROCESADO.xlsx")
                    'My.Computer.FileSystem.MoveFile(Session("RUTALAY").ToString, urldigi + "\test.txt")
                    'DelHDFile(Session("RUTALAY").ToString)
                    Session("RUTALAY") = String.Empty
                Else
                    lbl_status.Text = "Error: No se puede procesar la información en el estatus actual"
                End If
            Catch ex As Exception
                lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message().ToString()
            End Try
        Else
            lbl_status.Text = "Error: El proceso ha sido cancelado o se está procesando"
        End If
    End Sub


    Private Sub ProcesaInfoLayoutAportacionesHomologados(oLote As Integer, oanio As Integer, oquincena As Integer)

        Dim HshUrl As Hashtable = New Hashtable()
        HshUrl.Add("@IDINSTI", 1)
        HshUrl.Add("@QUINCENA", oquincena)
        HshUrl.Add("@ANIO", oanio)
        HshUrl.Add("@LOTE", oLote)
        HshUrl.Add("@TIPO", cmb_Layout.SelectedItem.Value)

        Dim da As New DataAccess()
        Session("RUTALAY") = da.RegresaUnaCadena("SEL_RUTA_PROCESO_LAYOUT_LOTE", HshUrl)

        If Session("RUTALAY") <> "N/A" Then
            Try
                Dim RES As String
                Dim Hsh As Hashtable = New Hashtable()

                Hsh.Add("@RUTA", Session("RUTALAY").ToString)
                Hsh.Add("@IDINSTITUCION", 1)
                Hsh.Add("@NUMQUINCENA", oquincena)
                Hsh.Add("@ANIO", oanio)
                Hsh.Add("@LOTE", oLote)
                Hsh.Add("@FECHA", Session("FECHAQNA"))
                Hsh.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
                Hsh.Add("@IDEQUIPO", Session("ID_EQ"))
                Hsh.Add("@IDUSER", Session("USERID"))
                Hsh.Add("@SESION", Session("Sesion"))
                RES = da.RegresaUnaCadena("INS_PROCESA_INFO_LAYOUT_APORTACIONES_H", Hsh)
                Session("RES") = RES
                If Session("RES") = "OK" Then

                    ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
                    CargaPeriodos()
                    LotesAplicados()

                    Dim nomLayProc As String

                    nomLayProc = CStr(oLote) + "_" + CStr(oanio) + "_" + CStr(oquincena) + "_" + CStr(cmb_Layout.SelectedItem.Value) + "_APORTACIONES_HOMOLOGADOS"

                    My.Computer.FileSystem.RenameFile(Session("RUTALAY").ToString, nomLayProc + "_PROCESADO.txt")

                    'DelHDFile(Session("RUTALAY").ToString)

                    lbl_status.Text = "Éxito: Información del layout procesada correctamente"

                    Session("RUTALAY") = String.Empty
                Else
                    lbl_status.Text = "Error: No se puede procesar la información en el estatus actual"
                End If
            Catch ex As Exception
                lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message().ToString()
            End Try
        Else
            lbl_status.Text = "Error: El proceso ha sido cancelado o se está procesando"
        End If
    End Sub
    Private Sub ProcesaInfoLayoutDescuentos(oLote As Integer, oanio As Integer, oquincena As Integer)

        Dim HshUrl As Hashtable = New Hashtable()
        HshUrl.Add("@IDINSTI", 1)
        HshUrl.Add("@QUINCENA", oquincena)
        HshUrl.Add("@ANIO", oanio)
        HshUrl.Add("@LOTE", oLote)
        HshUrl.Add("@TIPO", cmb_Layout.SelectedItem.Value)

        Dim da As New DataAccess()
        Session("RUTALAY") = da.RegresaUnaCadena("SEL_RUTA_PROCESO_LAYOUT_LOTE", HshUrl)

        If Session("RUTALAY") <> "N/A" Then

            Dim Result As String = "OK"
            Dim ResCarga As String = String.Empty
            Dim conStr As String = ""
            Dim extension As String
            conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                      .ConnectionString

            conStr = String.Format(conStr, Session("RUTALAY"), "Yes")

            Dim connExcel As New OleDbConnection(conStr)
            Dim cmdExcel As New OleDbCommand()
            Dim oda As New OleDbDataAdapter()
            Dim dt As New DataTable()

            cmdExcel.Connection = connExcel

            'Get the name of First Sheet 
            connExcel.Open()
            Dim dtExcelSchema As DataTable
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            Dim SheetName As String
            extension = da.RegresaUnaCadena("SEL_EXTENSION_PROCESO_LAYOUT_LOTE", HshUrl)

            If extension = ".xlsx" Then
                SheetName = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
            Else
                SheetName = dtExcelSchema.Rows(1)("TABLE_NAME").ToString()
            End If


            connExcel.Close()

            connExcel.Open()
            cmdExcel.CommandText = "SELECT * From [" & SheetName & "]" '&
            '"Where CVE_CONCEPTO = '22'"
            oda.SelectCommand = cmdExcel
            Dim ds As New DataSet
            oda.Fill(ds)
            connExcel.Close()


            Dim dtValidaDes As New DataTable
            dtValidaDes.Columns.Add("ENTIDAD", GetType(String))
            dtValidaDes.Columns.Add("PROCESO_DE_NOMINA", GetType(String))
            dtValidaDes.Columns.Add("NOMBRE", GetType(String))
            dtValidaDes.Columns.Add("PRIMER_APELLIDO", GetType(String))
            dtValidaDes.Columns.Add("SEGUNDO_APELLIDO", GetType(String))
            dtValidaDes.Columns.Add("CURP", GetType(String))
            dtValidaDes.Columns.Add("RFC", GetType(String))
            dtValidaDes.Columns.Add("CLC", GetType(String))
            dtValidaDes.Columns.Add("CVE_CONCEPTO", GetType(String))
            dtValidaDes.Columns.Add("DESCRIPCION", GetType(String))
            dtValidaDes.Columns.Add("IMPORTE", GetType(String))

            dtValidaDes = ds.Tables(0)

            Dim filterDT As DataTable = dtValidaDes.Clone()

            If dtValidaDes.Rows.Count() > 0 Then
                Dim rows As DataRow() = dtValidaDes.[Select]("CVE_CONCEPTO = 'PA'")

                For Each row As DataRow In rows
                    filterDT.ImportRow(row)
                Next
            End If

            Try

                Dim RES As String
                Dim Hsh As Hashtable = New Hashtable()

                Hsh.Add("@ARCHIVO", filterDT)
                Hsh.Add("@IDINSTITUCION", 1)
                Hsh.Add("@NUMQUINCENA", oquincena)
                Hsh.Add("@ANIO", oanio)
                Hsh.Add("@LOTE", oLote)
                Hsh.Add("@FECHA", Session("FECHAQNA"))
                Hsh.Add("@TIPOMOV", cmb_Layout.SelectedItem.Value)
                Hsh.Add("@IDEQUIPO", Session("ID_EQ"))
                Hsh.Add("@IDUSER", Session("USERID"))
                Hsh.Add("@SESION", Session("Sesion"))

                RES = da.RegresaUnaCadena("INS_PROCESA_INFO_LAYOUT_DESCUENTOS", Hsh)
                Session("RES") = RES

                If Session("RES") = "OK" Then

                    ProcesaTrabajadoresCorreo(cmb_Layout.SelectedValue)
                    CargaPeriodos()
                    LotesAplicados()

                    Dim nomLayProc As String

                    nomLayProc = CStr(oLote) + "_" + CStr(oanio) + "_" + CStr(oquincena) + "_" + CStr(cmb_Layout.SelectedItem.Value) + "_DESCUENTOS"

                    My.Computer.FileSystem.RenameFile(Session("RUTALAY").ToString, nomLayProc + "_PROCESADO.xlsx")
                    'DelHDFile(Session("RUTALAY").ToString)

                    lbl_status.Text = "Éxito: Información del layout procesada correctamente"

                    Session("RUTALAY") = String.Empty
                Else
                    lbl_status.Text = "Error: No se puede procesar la información en el estatus actual"
                End If
            Catch ex As Exception
                lbl_status_ex.Text = "ERROR: Error de base de datos." + ex.Message().ToString()
            End Try
        Else
            lbl_status.Text = "Error: El proceso ha sido cancelado o se está procesando"
        End If
    End Sub

    Private Sub RegistrosCorrectosIncorrectos(estatus As Integer, olote As Integer, oanio As Integer, oquincena As Integer, otipoMov As Integer)

        Try

            Dim dtMovimientosDescuentos As New Data.DataTable()
            Dim NombreCSV As String

            If (estatus = 1) Then
                NombreCSV = "RegCorrectos"
            Else
                NombreCSV = "RegIncorrectos"
            End If

            Dim HshCI As Hashtable = New Hashtable()
            HshCI.Add("@QNA", oquincena)
            HshCI.Add("@ANIO", oanio)
            HshCI.Add("@IDINST", 1)
            HshCI.Add("@LOTE", olote)
            HshCI.Add("@ESTATUS", estatus)
            HshCI.Add("@TIPOMOV", otipoMov)

            Dim da As New DataAccess()
            dtMovimientosDescuentos = da.RegresaDataTable("SEL_DESCUENTOS_X_LOTE", HshCI)

            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtMovimientosDescuentos.Rows

                For i = 0 To dtMovimientosDescuentos.Columns.Count - 1

                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")

                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreCSV + oanio.ToString() + oquincena.ToString() + ".csv")
            context.Response.End()

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            Limpiar2()
        End Try

    End Sub

    Private Sub Limpia()
        dag_PagosXAplicar.Visible = False
        dag_PagosXAplicar.DataSource = Nothing
        dag_PagosXAplicar.DataBind()
        lbl_status.Text = String.Empty
        FileUpload1.Visible = False
        btn_Subir.Visible = False
        dgd_prejubilados.Visible = False
        dgd_prejubilados.DataSource = Nothing
        dgd_prejubilados.DataBind()
    End Sub

    Private Sub Limpiar2()
        dag_PagosXAplicar.Visible = True
        dag_PagosXAplicar.DataBind()
        lbl_status.Text = String.Empty
        FileUpload1.Visible = True
        FileUpload1.Enabled = True
        btn_Subir.Visible = True
        btn_Subir.Enabled = True
    End Sub

    Private Sub DelHDFile(ByVal File1 As String)
        If File.Exists(File1) Then
            Dim fi As New System.IO.FileInfo(File1)
            If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
                fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
            End If
        Else
            lbl_status.Text = "Alerta: El archivo ha sido movido o eliminado"
        End If
        System.IO.File.Delete(File1)
    End Sub

#Region "Envio de Correos"
    Private Sub ProcesaTrabajadoresCorreo(ByVal tipo As Int32)

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "PROTRAB")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Procesa Trabajadores " + cmb_Quincenas.SelectedItem.Value.ToString

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")

            If (tipo = 1) Then
                sbhtml.Append("<tr><td>Se le informa que se realizó un procesamiento de Nómina de los agremiados.</td></tr>")
            Else
                sbhtml.Append("<tr><td>Se le informa que se realizó un procesamiento de Aportaciones y Descuentos de los agremiados.</td></tr>")
            End If

            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>En Corte de Nómina " + cmb_Quincenas.SelectedItem.Text + "</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Realizado por el Usuario " + Session("USERNOM").ToString + "</td></tr>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

#End Region

End Class