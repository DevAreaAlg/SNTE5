Imports WnvWordToPdf
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class PDFCreator
    Private url As String
    Private NewDocName As String
    Private baseDocName As String
    Private originPath As String
    Private destiniPath As String

    Private worddoc As Novacode.DocX

    Sub New()
        Me.url = ""
        Me.NewDocName = ""
        Me.baseDocName = ""
        Me.originPath = ""
        Me.destiniPath = ""
    End Sub

    Sub New(url As String, originPath As String, destiniPath As String, baseDocName As String, NewDocName As String)
        Me.url = url
        Me.NewDocName = NewDocName
        Me.baseDocName = baseDocName
        Me.originPath = originPath
        Me.destiniPath = destiniPath

        Me.worddoc = Novacode.DocX.Load(url + "\" + originPath + "\" + baseDocName + ".DOCX")




    End Sub
    Public Sub remplazarEtiqueta(name As String, value As String)
        worddoc.ReplaceText("[" + name + "]", value, False, RegexOptions.None)
    End Sub

    Public Sub makeTable(no_table As Integer, FontSize As Integer, tabla As DataTable)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        't.Alignment = Novacode.Alignment.center
        For i As Integer = 0 To tabla.Rows.Count - 1
            t.InsertRow()
            For j As Integer = 0 To tabla.Columns.Count - 1
                t.Rows(i + 1).Cells(j).Paragraphs.First().Append(tabla(i)(j)).FontSize(FontSize).Alignment = Novacode.Alignment.center

            Next
        Next
    End Sub

    Public Sub InsertRow(no_table As Integer, i As Integer)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        t.InsertRow()
    End Sub

    Public Sub InsertRowValue(no_table As Integer, FontSize As Integer, i As Integer, j As Integer, text As String)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        t.Rows(i).Cells(j).Paragraphs.First().Append(text).FontSize(FontSize).Alignment = Novacode.Alignment.center
    End Sub
    Public Sub InsertRowBoldValue(no_table As Integer, FontSize As Integer, i As Integer, j As Integer, text As String)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        t.Rows(i).Cells(j).Paragraphs.First().Append(text).FontSize(FontSize).Bold().Alignment = Novacode.Alignment.center
        t.Rows(i).Cells(j).SetBorder(Novacode.TableCellBorderType.Top, New Novacode.Border)
        t.Rows(i).Cells(j).SetBorder(Novacode.TableCellBorderType.Bottom, New Novacode.Border)
    End Sub

    'Public Sub RowBold(no_table As Integer, i As Integer)
    '    Dim t As Novacode.Table = worddoc.Tables(no_table)
    '    t.Rows(i).
    'End Sub

    Public Sub sustituir()

    End Sub
    Public Sub AlignColumnsLeft(no_table As Integer, column As Integer)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        For i As Integer = 1 To t.RowCount - 1
            t.Rows(i).Cells(column).Paragraphs.First().Alignment = Novacode.Alignment.left
        Next
    End Sub
    Public Sub AlignColumnsRight(no_table As Integer, column As Integer)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        For i As Integer = 1 To t.RowCount - 1
            t.Rows(i).Cells(column).Paragraphs.First().Alignment = Novacode.Alignment.right
        Next
    End Sub
    Public Sub AlignColumnsCenter(no_table As Integer, column As Integer)
        Dim t As Novacode.Table = worddoc.Tables(no_table)
        For i As Integer = 1 To t.RowCount - 1
            t.Rows(i).Cells(column).Paragraphs.First().Alignment = Novacode.Alignment.center
        Next
    End Sub
    Public Sub agregarimagen2(Session)
        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(url + "\" + destiniPath + "\" + NewDocName + ".pdf")
        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        With document
            .AddAuthor("SALTILLO -  SALTILLO")
            .AddCreationDate()
            .AddCreator("SALTILLO - Cheque")
            .AddSubject("Cheque")
            .AddTitle("Cheque")
            .AddKeywords("Cheque")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()

        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Cheque As iTextSharp.text.pdf.PdfImportedPage

        Cheque = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Cheque, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim X2, Y2 As Single
        Dim distanciaHorizontal As Integer = 240.0R
        Dim distanciaVertical As Integer = 15

        X = 450  'X empieza de izquierda a derecha
        ''y estaba en 50
        Y = 735 'Y empieza de abajo hacia arriba

        Y2 = 595
        X2 = 60


        Dim XOrdena As Integer
        Dim YOrdena As Integer

        'DIA, MES, AÑO
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "@@@@@@@@@@@", X, Y, 0)

        cb.EndText()
        document.NewPage()

        document.Close()
    End Sub
    Public Sub agregarimagen(Session)
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(url + "\" + destiniPath + "\" + NewDocName + ".pdf")
        Dim osize = Reader.GetPageSizeWithRotation(1)
        Dim docu As New iTextSharp.text.Document(osize)
        Dim pdfw As iTextSharp.text.pdf.PdfWriter

        pdfw = iTextSharp.text.pdf.PdfWriter.GetInstance(docu, New FileStream(url + "\" + destiniPath + "\" + NewDocName + ".pdf", FileMode.Open, FileAccess.Write, FileShare.None))

        '------------

        '---------------
        docu.Open()

        Dim firma1 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(url + "\" + "img\firma_delegado_black.png")
        firma1.ScalePercent(16.7)
        firma1.SetAbsolutePosition(140, 500) 'posición en la que se inserta. 140 (de izquierda a derecha). 500 (de abajo hacia arriba)
        docu.Add(firma1)
        'Forzamos vaciamiento del buffer.
        'pdfw.Flush()
        'Cerramos el documento.
        docu.Close()
        'pdfw.Close()
        'Session("ms") = New System.IO.MemoryStream()
        ''Crea un reader para la solicitud

        'Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing

        'Dim DOCU As Document()

        'Dim w As PdfWriter = New PdfWriter()


        ''Comienza seccion de escritura del pdf 
        ''Declara memory stream para salida

        'Session("ms") = New System.IO.MemoryStream()
        ''Crea un reader para la solicitud

        'Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        ''Ruta donde está el PDF
        'Reader = New iTextSharp.text.pdf.PdfReader(url + "\" + destiniPath + "\" + NewDocName + ".pdf")
        ''Traigo el total de paginas
        ''Dim n As Integer = 0
        ''n = Reader.NumberOfPages

        ''Traigo el tamaño de la primera pagina
        'Dim psize As iTextSharp.text.Rectangle
        'psize = Reader.GetPageSize(1)

        'Dim width, height As Single
        'width = psize.Width
        'height = psize.Height

        'Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        'With document
        '    .AddAuthor("SALTILLO -  SALTILLO")
        '    .AddCreationDate()
        '    .AddCreator("SALTILLO - Cheque")
        '    .AddSubject("Cheque")
        '    .AddTitle("Cheque")
        '    .AddKeywords("Cheque")
        '    .Open()
        'End With

        ''CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        '''Dim XT, YT, XAux As Single
        '''Dim writer As iTextSharp.text.pdf.PdfWriter
        '''writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        ''Se abre el documento
        'document.Open()

        '''Dim cb As iTextSharp.text.pdf.PdfContentByte
        '''cb = writer.DirectContent

        '''' METO LA SOLICITUD ORIGINAL
        '''Dim Cheque As iTextSharp.text.pdf.PdfImportedPage

        '''Cheque = writer.GetImportedPage(Reader, 1)
        '''cb.AddTemplate(Cheque, 1, 0, 0, 1, 0, 0)

        ''ready to draw text
        'Dim firma1 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(url + "\" + "img\firma_delegado_black.png")
        ''cb.AddImage(firma1)
        'document.Add(firma1)
        ''cb.BeginText()

        ''cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "        " + DIA, X, Y, 0)

        ''cb.EndText()

        'document.Close()
    End Sub
    Public Sub save(Response, Session)
        worddoc.SaveAs(url + "\" + destiniPath + "\" + NewDocName + ".DOCX")
        worddoc.SaveAs(url + "\" + destiniPath + "\" + NewDocName + "2.DOCX")

        Dim result As String = ""

        Try
            Dim winKey As String = ConfigurationManager.AppSettings.[Get]("WINKEY")
            Dim wordToPdfConverter As New WordToPdfConverter()
            wordToPdfConverter.LicenseKey = winKey
            wordToPdfConverter.ConvertWordFileToFile(url + "\" + destiniPath + "\" + baseDocName + ".DOCX", url + "\" + destiniPath + "\" + NewDocName + ".pdf")

            'Elimina el Documento WORD ya Prellenado
            System.IO.File.Delete(url + "\" + destiniPath + "\" + NewDocName + ".DOCX")

            agregarimagen2(Session)

            ' Se genera el PDF
            Dim Filename As String = NewDocName + ".pdf"
            Dim FilePath As String = url + "\" + destiniPath + "\"
            Dim fs As System.IO.FileStream
            fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
            Dim bytBytes(fs.Length) As Byte
            fs.Read(bytBytes, 0, fs.Length)
            fs.Close()

            'se va a actualzar?
            'ActualizaExtatusContratoImpreso()

            'Borra el archivo creado en memoria
            DelHDFile(FilePath + Filename)

            Response.Buffer = True
            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", NewDocName + ".pdf"))
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(bytBytes)
            Response.End()

        Catch ex As Exception
            result = (ex.Message)
        Finally
            'objNewWord.Quit()
        End Try
    End Sub
    Public Sub saveAs(url As String)
        worddoc.SaveAs(url + ".DOCX")
    End Sub

    Public Sub Savepdf(url As String)
        worddoc.SaveAs(url + ".DOCX")

        Dim result As String = ""

        Try
            Dim winKey As String = ConfigurationManager.AppSettings.[Get]("WINKEY")
            Dim wordToPdfConverter As New WordToPdfConverter()
            wordToPdfConverter.LicenseKey = winKey
            wordToPdfConverter.ConvertWordFileToFile(url + ".DOCX", url + ".pdf")

            'Elimina el Documento WORD ya Prellenado
            System.IO.File.Delete(url + ".DOCX")



        Catch ex As Exception
            result = (ex.Message)
        Finally
            'objNewWord.Quit()
        End Try
    End Sub

    Public Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub
End Class
