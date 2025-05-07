Public Class CRED_TES_REGISTRO_ENVIO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        llena_envios()
    End Sub

    Private Sub llena_envios()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtenvios As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_ENVIA", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACIONES_CONS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtenvios, Session("rs"))
        Session("Con").Close()
        If dtenvios.Rows.Count > 0 Then
            grd_operaciones.DataSource = dtenvios
            grd_operaciones.DataBind()
        Else
            lbl_status.Text = "No existen operaciones pendientes"
        End If

    End Sub

    Private Sub llena_registro_envio(ByVal idop As Integer)
        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EnvioChequeEfectivo.pdf")

        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        'CREACION DE UN DOCUMENTO

        Dim document As New iTextSharp.text.Document(psize, 60, 60, 108, 108)

        With document
            .AddAuthor("Desarrollo - ")
            .AddCreationDate()
            .AddCreator(" - Transferencia Efectivo")
            .AddSubject("Envio Cheques/Efectivo")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Envio Cheques/Efectivo")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Envio Cheques/Efectivo")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO

        Dim writer As iTextSharp.text.pdf.PdfWriter
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        ' step 3: we open the document
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Solicitud As iTextSharp.text.pdf.PdfImportedPage

        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

        cb.BeginText()

        Dim bf As iTextSharp.text.pdf.BaseFont
        'bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        'cb.SetFontAndSize(bf, 14)

        Dim X, Y As Single
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = 80
        Y = 665

        Dim env_che As Char
        env_che = "n"
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, idop)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_DETALLE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Origen: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ORIGEN").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Destino: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DESTINO").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Envia: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ENVIA").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Transporta: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TRANSPORTA").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Recibe: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("RECIBE").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHA").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(Session("rs").Fields("MONTO").Value.ToString), X, Y, 0)
            X = 80
            Y -= 15
        End If
        Session("Con").Close()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, idop)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES_OPERACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Y -= 15
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 11)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CHEQUES", X, Y, 0)
            Y -= 30
            cb.SetFontAndSize(bf, 9)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Folio", X, Y, 0)
            'X += 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Banco", X, Y, 0)
            X += 120
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Número de Cuenta", X, Y, 0)
            X += 85
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Número de Cheque", X, Y, 0)
            X += 120
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto", X, Y, 0)
            X += 70
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Estatus", X, Y, 0)
            'X += 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha Recibido", X, Y, 0)
            Y -= 7
            X = 80
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "_____________________________________________________________________________________________", X, Y, 0)
            Y -= 7
            Do While Not Session("rs").EOF
                Y -= 13
                X = 80
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("BANCO").Value.ToString, X, Y, 0)
                X += 120
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUMCUENTA").Value.ToString, X, Y, 0)
                X += 85
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ID_CHEQUE").Value.ToString, X, Y, 0)
                X += 120
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(Session("rs").Fields("MONTO").Value.ToString), X, Y, 0)
                X += 70
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHA_RECIBIDO").Value.ToString, X, Y, 0)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
        cb.EndText()
        document.Close()
    End Sub

    Private Sub imprime_registro_envio(ByVal idop As Integer)

        llena_registro_envio(idop)
        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= RegistroEnvio.pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With

    End Sub

    Private Sub grd_operaciones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grd_operaciones.ItemCommand
        imprime_registro_envio(CInt(e.Item.Cells(0).Text))
    End Sub

End Class