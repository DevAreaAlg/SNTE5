Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Ionic.Zip
Public Class COB_DOC_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Asignación de eventos", "Asignación de eventos")
        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If
            LlenaEventos()

        End If
    End Sub


    Private Sub LlenaEventos()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim PlantillasGeneral As New Data.DataTable()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_EVENTOSDOC_X_CREDITOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(PlantillasGeneral, Session("rs"))
        Session("Con").Close()

        If PlantillasGeneral.Rows.Count > 0 Then
            dag_AsigEventos.Visible = True
            dag_AsigEventos.DataSource = PlantillasGeneral
            dag_AsigEventos.DataBind()
        Else
            dag_AsigEventos.Visible = True
        End If

    End Sub


    Protected Sub btn_gegBit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_gegBit.Click

        Dim contador As Integer

        For i As Integer = 0 To dag_AsigEventos.Rows.Count() - 1
            Dim bandera As Integer

            bandera = Convert.ToInt32(DirectCast(dag_AsigEventos.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked)
            If bandera = 1 Then
                contador = contador + 1
            End If
        Next

        If contador > 0 Then
            GeneraBitacoraExcel()
            lbl_status.Text = ""
        Else
            lbl_status.Text = "Debe seleccionar un evento para generar la bitacora"
        End If

    End Sub


    Protected Sub GeneraBitacoraExcel()

        ' Declaro la App
        Dim xlApp As New Microsoft.Office.Interop.Excel.Application
        xlApp.DisplayAlerts = False
        ' Declaro el Workbook
        Dim xlWorkbook As Microsoft.Office.Interop.Excel.Workbook
        ' Abro el archivo deseado
        xlWorkbook = xlApp.Workbooks.Open(Session("APPATH").ToString + "\Word\COBRANZA\BITACORA_COBRANZA.xlsx")
        ' Declaro el WorkSheet
        Dim xlWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorksheet = xlWorkbook.Sheets("Bitacora")

        Dim Fila As Integer = 1
        Dim Texto As String = ""
        Dim IND As Integer, IND2 As Integer
        Dim ID_DES As String = ""
        Dim DOMICI_DES As String = ""
        Dim NUM_INT As String = ""
        Dim EXTERIOR As String = ""
        xlWorksheet.Cells(Fila, 1) = "NOMBRE DE LA EMPRESA"

        Fila = 11

        Dim contador As Integer

        For i As Integer = 0 To dag_AsigEventos.Rows.Count() - 1
            Dim bandera As Integer

            bandera = Convert.ToInt32(DirectCast(dag_AsigEventos.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked)
            If bandera = 1 Then
                contador = contador + 1
                Texto = dag_AsigEventos.Rows(i).Cells(1).Text
                IND = Texto.IndexOf("(")
                IND2 = Texto.IndexOf("FOLIO")
                xlWorksheet.Cells(Fila, 5) = Mid(Texto, IND, (IND2 - (IND + 5)))
                xlWorksheet.Cells(Fila, 6) = Texto

                Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
                Dim PlantillasGeneral As New Data.DataTable()

                ID_DES = dag_AsigEventos.Rows(i).Cells(7).Text

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, ID_DES)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("TIPODIR", Session("adVarChar"), Session("adParamInput"), 10, 1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_DIRECCION"
                Session("rs") = Session("cmd").Execute()
                NUM_INT = Session("rs").fields("NUMINT").value.ToString

                If NUM_INT = "" Then
                    EXTERIOR = ""
                Else
                    EXTERIOR = " Int. " + NUM_INT
                End If
                DOMICI_DES = Session("rs").fields("CALLE").value.ToString + " Ext. " + Session("rs").fields("NUMEXT").value.ToString + EXTERIOR + ", " + Session("rs").fields("ASENTAMIENTO").value.ToString + ", " + Session("rs").fields("MUNICIPIO").value.ToString + ", " + Session("rs").fields("ESTADO").value.ToString
                Session("Con").Close()

                xlWorksheet.Cells(Fila, 7) = DOMICI_DES

                Fila = Fila + 1
            End If
        Next



        Fila = Fila + 1

        xlWorksheet.Cells(Fila, 1) = "Incidencias durante el trayecto:"

        Fila = Fila + 4
        xlWorksheet.Cells(Fila, 1) = "Nombre y firma de notificador:   __________________________________________________"
        Fila = Fila + 1
        xlWorksheet.Cells(Fila, 1) = "Fecha de registro de información:   __________________________________________________"
        Fila = Fila + 1
        xlWorksheet.Cells(Fila, 1) = "Nombre y firma de gerente de sucursal que verifica:   __________________________________________________"

        Dim Filename, FilePath As String
        Filename = "BITACORA_COBRANZA-" + Now.Day.ToString + "-" + Now.Month.ToString + "-" + Now.Year.ToString + ".xls"
        FilePath = Session("APPATH").ToString + "\Word\COBRANZA\"
        xlWorkbook.SaveAs(FilePath + Filename, FileFormat:=56)
        xlWorkbook.Close()

        Dim fs As System.IO.FileStream
        fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
        Dim bytBytes(fs.Length) As Byte
        fs.Read(bytBytes, 0, fs.Length)
        fs.Close()
        'Borra el archivo creado en memoria
        DelHDFile(FilePath + Filename)
        Response.Buffer = True
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()
        Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", Filename))
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.BinaryWrite(bytBytes)
        Response.End()

    End Sub





    Protected Sub btn_genPre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_genPre.Click

        Dim contador As Integer

        For i As Integer = 0 To dag_AsigEventos.Rows.Count() - 1
            Dim bandera As Integer

            bandera = Convert.ToInt32(DirectCast(dag_AsigEventos.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked)
            If bandera = 1 Then
                contador = contador + 1
            End If
        Next

        If contador > 0 Then
            GeneraEventos()
        Else
            lbl_status.Text = "Debe seleccionar un evento para prellenar"
        End If

    End Sub




    Private Sub GeneraEventos()
        'Data table que se llena con los contenidos de los datagrids

        Dim Path As String = Session("APPATH").ToString + "\Word\COBRANZA\DOCEVENTOS"


        Path = Path + "\EXPEDIENTE_" + Session("USERID").ToString
        Session("NUEVARUTA") = Path
        If Not Directory.Exists(Session("NUEVARUTA").ToString) Then
            Directory.CreateDirectory(Session("NUEVARUTA").ToString)
        Else
            For Each fichero As String In Directory.GetFiles(Session("NUEVARUTA").ToString, "*.*")
                System.IO.File.Delete(fichero)
            Next
            'Directory.Delete(Session("NUEVARUTA").ToString)
        End If

        lbl_status.Text = Session("NUEVARUTA").ToString


        Dim dtEventos As New Data.DataTable()
        dtEventos.Columns.Add("IDEVENTO", GetType(Integer))
        dtEventos.Columns.Add("FOLIO", GetType(Integer))
        dtEventos.Columns.Add("IDPLANTILLA", GetType(Integer))
        dtEventos.Columns.Add("IDPERSONA", GetType(Integer))

        Dim IDEVENTO As Integer
        Dim NUMFOLIO As Integer
        Dim IDPLANTILLA As Integer
        Dim IDPERSONA As Integer



        For i As Integer = 0 To dag_AsigEventos.Rows.Count() - 1

            Dim bandera As Integer

            bandera = Convert.ToInt32(DirectCast(dag_AsigEventos.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked)

            If bandera = 1 Then


                IDEVENTO = CInt(dag_AsigEventos.Rows(i).Cells(0).Text)
                NUMFOLIO = CInt(dag_AsigEventos.Rows(i).Cells(4).Text)
                IDPLANTILLA = CInt(dag_AsigEventos.Rows(i).Cells(2).Text)
                IDPERSONA = CInt(dag_AsigEventos.Rows(i).Cells(7).Text)

                dtEventos.Rows.Add(IDEVENTO, NUMFOLIO, IDPLANTILLA, IDPERSONA)
                lbl_status.Text = " IDEVENTO: " + dag_AsigEventos.Rows(i).Cells(0).Text + " NUMFOLIO: " + dag_AsigEventos.Rows(i).Cells(4).Text + " IDPLANTILLA: " + dag_AsigEventos.Rows(i).Cells(2).Text + " IDPERSONA: " + dag_AsigEventos.Rows(i).Cells(7).Text + "NOMBREARCHIVO: " + dag_AsigEventos.Rows(i).Cells(8).Text
                GenerarFormato(dag_AsigEventos.Rows(i).Cells(8).Text, IDPERSONA, dag_AsigEventos.Rows(i).Cells(5).Text, dag_AsigEventos.Rows(i).Cells(10).Text, NUMFOLIO, IDPLANTILLA, dag_AsigEventos.Rows(i).Cells(11).Text, dag_AsigEventos.Rows(i).Cells(4).Text)
                '"NOMBRE ARCHIVO"cNomArchivo As String, ByVal cIDCliente As String, ByVal cCliente As String, ByVal PagATRA As String, ByVal FOLIO As String, ByVal IDPLANTILLA As Integer, ByVal cNomDest As String, ByVal cFolio As String
            End If

        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)


                connection.Open()

                Dim insertCommand As New SqlCommand("INS_COB_GENERA_EVENTOSDOC", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                ' Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("EVENTOS", SqlDbType.Structured)
                Session("parm").Value = dtEventos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()

                lbl_status.Text = "Guardado correctamente"

            End Using

        Catch ex As Exception
            lbl_status.Text = "Error al generar"
        Finally

        End Try

        lbl_status.Text = "ultimaruta " + Session("NUEVARUTA").ToString

        Comprimir(Session("NUEVARUTA").ToString)

        Dim ruta_Save = Session("NUEVARUTA").ToString + "\EXPEDIENTE_" + Session("USERID") + ".zip"

        lbl_status.Text = ruta_Save

        Dim file As System.IO.FileInfo = New System.IO.FileInfo(ruta_Save) '-- if the file exists on the server

        If file.Exists Then 'set appropriate headers
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End() 'if file does not exist

        Else
            lbl_status.Text = "No existen documentos digitalizados"
        End If

    End Sub

    Private Sub Comprimir(ByVal Ruta As String)

        Dim itempaths As String() = New String() {Ruta}
        Using zip As ZipFile = New ZipFile()
            For i = 0 To itempaths.Length - 1
                zip.AddItem(itempaths(i), "")
            Next
            zip.Save(Ruta + "\EXPEDIENTE_" + Session("USERID").ToString + ".zip")
        End Using
    End Sub

    Private Sub GenerarFormato(ByVal cNomArchivo As String, ByVal cIDCliente As String, ByVal cCliente As String, ByVal PagATRA As String, ByVal FOLIO As String, ByVal IDPLANTILLA As Integer, ByVal cNomDest As String, ByVal cFolio As String)
        Dim lcUrl As String = cNomArchivo + ".docx"
        Dim NewDocName As String = cNomArchivo + FOLIO + cIDCliente + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        Dim cPath As String = Session("APPATH") + "\Word\COBRANZA\"
        Dim cPath1 As String = Session("NUEVARUTA").ToString + "\"
        Dim cPathNewDoc As String = cPath1 + NewDocName + ".docx"
        'lbl_status_docs.Text = " ruta: " + cPath1
        'lbl_status.Text = "rutaarchivo " + cPathNewDoc

        'lbl_status.Text = lbl_status.Text + " " + cNomArchivo + " " + cIDCliente + " " + cCliente + " " + PagATRA + " " + FOLIO
        'lbl_status_docs.Text = " lcUrl: " + lcUrl + " NewDocName: " + NewDocName + " cPath: " + cPath + " cPathNewDoc: " + cPathNewDoc
        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPath + lcUrl)
            Try
                worddoc.SaveAs(cPathNewDoc)
                Session("Con").Open()
                If IDPLANTILLA = 1 Then
                    ObtieneInfoPL1(NewDocName, cPath1, cPathNewDoc, cIDCliente, cCliente, PagATRA)
                ElseIf IDPLANTILLA = 2 Then
                    ObtieneInfoPL2(NewDocName, cPath1, cPathNewDoc, cIDCliente, cCliente, PagATRA, cNomDest, cFolio)
                Else
                    ObtieneInfoPL3(NewDocName, cPath1, cPathNewDoc, cIDCliente, cCliente, PagATRA)
                End If

            Catch ex As Exception
                lbl_status.Text = ex.ToString
            Finally
                Session("Con").Close()
            End Try
        End Using
    End Sub


    Private Sub ObtieneInfoPL1(ByVal NewDocName1 As String, ByVal cPath1 As String, ByVal cPathNewDoc1 As String, ByVal idCliente1 As String, ByVal nomCliente1 As String, ByVal pagAtra1 As String)

        Dim cCallenum, cAsentamiento, cMunicipio, cEstado, cCP, cDireccion As String
        Dim cFecha As String
        'lbl_status.Text = " NewDocName1: " + NewDocName1 + " cPath1: " + cPath1 + " cPathNewDoc1: " + cPathNewDoc1 + " idCliente1: " + idCliente1 + " nomCliente1: " + nomCliente1 + " pagAtra1: " + pagAtra1
        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPathNewDoc1)

            'Try
            cCallenum = ""
            cAsentamiento = ""
            cMunicipio = ""
            cEstado = ""
            cCP = ""
            cDireccion = ""
            cFecha = Session("FechaSis")

            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_SUCURSAL"
            Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                cCallenum = Session("rs").Fields("CALLENUM").Value.ToString
                cAsentamiento = Session("rs").Fields("ASENTAMIENTO").Value.ToString
                cMunicipio = Session("rs").Fields("MUNICIPIO").Value.ToString
                cEstado = Session("rs").Fields("ESTADO").Value.ToString
                cDireccion = cCallenum + ", " + cAsentamiento + ", " + cMunicipio + ", " + cEstado + ", "

                'lbl_status.Text = " cDireccion: " + cDireccion + " cDia: " + cDia + " cMes: " + cMes + " cAno: " + cAno + " idCliente1: " + idCliente1 + " nomCliente1: " + nomCliente1 + " pagAtra1: " + pagAtra1
                worddoc.ReplaceText("[DIRECCION_SUCURSAL]", cDireccion, False, RegexOptions.None)
                worddoc.ReplaceText("[FECHA]", cFecha, False, RegexOptions.None)
                worddoc.ReplaceText("[ID_CLIENTE]", idCliente1, False, RegexOptions.None)
                worddoc.ReplaceText("[NOMBRE_CLIENTE]", nomCliente1, False, RegexOptions.None)
                worddoc.ReplaceText("[NUM_PAGOS_ATRASADOS]", pagAtra1, False, RegexOptions.None)

                worddoc.Save()

            End If

            ConviertePDF(NewDocName1, cPath1)
            'Catch ex As Exception
            '    lbl_status.Text = "NO"
            'End Try
            ''lbl_status.Text = "SI"

        End Using
    End Sub


    Private Sub ObtieneInfoPL2(ByVal NewDocName1 As String, ByVal cPath1 As String, ByVal cPathNewDoc1 As String, ByVal idCliente1 As String, ByVal nomCliente1 As String, ByVal pagAtra1 As String, ByVal cNomDest1 As String, ByVal cFolio As String)

        Dim cCallenum, cAsentamiento, cMunicipio, cEstado, cCP, cDireccion As String
        Dim cFecha As String
        lbl_status.Text = " NewDocName1: " + NewDocName1 + " cPath1: " + cPath1 + " cPathNewDoc1: " + cPathNewDoc1 + " idCliente1: " + idCliente1 + " nomCliente1: " + nomCliente1 + " pagAtra1: " + pagAtra1
        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPathNewDoc1)
            Try
                cCallenum = ""
                cAsentamiento = ""
                cMunicipio = ""
                cEstado = ""
                cCP = ""
                cDireccion = ""
                cFecha = Session("FechaSis")

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_SUCURSAL"
                Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then

                    cCallenum = Session("rs").Fields("CALLENUM").Value.ToString
                    cAsentamiento = Session("rs").Fields("ASENTAMIENTO").Value.ToString
                    cMunicipio = Session("rs").Fields("MUNICIPIO").Value.ToString
                    cEstado = Session("rs").Fields("ESTADO").Value.ToString
                    cDireccion = cCallenum + ", " + cAsentamiento + ", " + cMunicipio + ", " + cEstado + ", "

                    'lbl_status.Text = " cDireccion: " + cDireccion + " cDia: " + cDia + " cMes: " + cMes + " cAno: " + cAno + " idCliente1: " + idCliente1 + " nomCliente1: " + nomCliente1 + " pagAtra1: " + pagAtra1

                End If

                worddoc.ReplaceText("[DIRECCION_SUCURSAL]", cDireccion, False, RegexOptions.None)
                worddoc.ReplaceText("[FECHA]", cFecha, False, RegexOptions.None)
                worddoc.ReplaceText("[NUM_FOLIO]", cFolio, False, RegexOptions.None)
                worddoc.ReplaceText("[NOMBRE_AVAL]", cNomDest1, False, RegexOptions.None)
                worddoc.ReplaceText("[ID_CLIENTE]", idCliente1, False, RegexOptions.None)
                worddoc.ReplaceText("[NOMBRE_CLIENTE]", nomCliente1, False, RegexOptions.None)
                worddoc.ReplaceText("[NUM_PAGOS_ATRASADOS]", pagAtra1, False, RegexOptions.None)

                worddoc.Save()


                ConviertePDF(NewDocName1, cPath1)
            Catch ex As Exception

            End Try
        End Using
    End Sub


    Private Sub ObtieneInfoPL3(ByVal NewDocName1 As String, ByVal cPath1 As String, ByVal cPathNewDoc1 As String, ByVal idCliente1 As String, ByVal nomCliente1 As String, ByVal pagAtra1 As String)

        Dim cCallenum, cAsentamiento, cMunicipio, cEstado, cCP, cDireccion As String
        Dim cDia, cMes, cAno As String
        ' lbl_status.Text = " NewDocName1: " + NewDocName1 + " cPath1: " + cPath1 + " cPathNewDoc1: " + cPathNewDoc1 + " idCliente1: " + idCliente1 + " nomCliente1: " + nomCliente1 + " pagAtra1: " + pagAtra1
        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPathNewDoc1)
            Try
                cCallenum = ""
                cAsentamiento = ""
                cMunicipio = ""
                cEstado = ""
                cCP = ""
                cDireccion = ""
                cDia = ""
                cMes = ""
                cAno = ""
                cDia = Now.Day.ToString
                cMes = Now.Month.ToString
                cAno = Now.Year.ToString
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_SUCURSAL"
                Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then

                    cCallenum = Session("rs").Fields("CALLENUM").Value.ToString
                    cAsentamiento = Session("rs").Fields("ASENTAMIENTO").Value.ToString
                    cMunicipio = Session("rs").Fields("MUNICIPIO").Value.ToString
                    cEstado = Session("rs").Fields("ESTADO").Value.ToString
                    cDireccion = cCallenum + ", " + cAsentamiento + ", " + cMunicipio + ", " + cEstado + ", "

                    'lbl_status.Text = " cDireccion: " + cDireccion + " cDia: " + cDia + " cMes: " + cMes + " cAno: " + cAno + " idCliente1: " + idCliente1 + " nomCliente1: " + nomCliente1 + " pagAtra1: " + pagAtra1

                End If

                worddoc.ReplaceText("[DIRECCION_SUCURSAL]", cDireccion, False, RegexOptions.None)
                worddoc.ReplaceText("[FECHA_DIA]", cDia, False, RegexOptions.None)
                worddoc.ReplaceText("[FECHA_MES]", cMes, False, RegexOptions.None)
                worddoc.ReplaceText("[FECHA_ANIO]", cAno, False, RegexOptions.None)
                worddoc.ReplaceText("[ID_CLIENTE]", idCliente1, False, RegexOptions.None)
                worddoc.ReplaceText("[NOMBRE_CLIENTE]", nomCliente1, False, RegexOptions.None)
                worddoc.ReplaceText("[NUM_PAGOS_ATRASADOS]", pagAtra1, False, RegexOptions.None)

                worddoc.Save()


                ConviertePDF(NewDocName1, cPath1)
            Catch ex As Exception

            End Try
        End Using
    End Sub


    Private Sub ConviertePDF(ByVal NewDocName As String, ByVal cPath As String)

        Dim result As String = ""
        Dim objNewWord As New Microsoft.Office.Interop.Word.Application()
        Dim ResultDocName As String = NewDocName + ".pdf"

        'Try
        Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(cPath + NewDocName + ".docx")
        objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", cPath), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
        objNewDoc.Save()
        objNewDoc.Close()
        System.IO.File.Delete(cPath + NewDocName + ".docx")
        'Elimina el Documento WORD ya Prellenado
        'System.IO.File.Delete(cPath + NewDocName + ".docx")

        ' Se genera el PDF
        'Dim Filename As String = NewDocName + ".pdf"
        'Dim FilePath As String = cPath
        'Dim fs As System.IO.FileStream
        'fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
        'Dim bytBytes(fs.Length) As Byte
        'fs.Read(bytBytes, 0, fs.Length)
        'fs.Close()

        ''Borra el archivo creado en memoria
        'DelHDFile(FilePath + Filename)
        'Response.Buffer = True
        'Response.Clear()
        'Response.ClearContent()
        'Response.ClearHeaders()
        'Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", ResultDocName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        'Response.BinaryWrite(bytBytes)
        'Response.End()
        'lbl_status_docs.Text = "Todo ok"
        'Catch ex As Exception
        '    result = (ex.Message)
        '    lbl_status_docs.Text = result
        'Finally
        '    lbl_status_docs.Text = "afuerza"
        objNewWord.Quit()
        'End Try

        objNewWord = Nothing
    End Sub




    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub

End Class