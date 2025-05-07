Public Class CORE_REP_DENOMINACIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Select Case Session("SERIE_REPORTES")

                Case "CI"
                    lbl_TipoRep.Text = "CORTE INICIAL"
                    LlenaCajasCorte()
                Case "TE"
                    lbl_TipoRep.Text = "TRANSFERENCIA DE EFECTIVO"
                    LlenaCajasFlujoReporte()
                Case Else
                    lbl_TipoRep.Text = "HOLA MUNDO!"
            End Select

        End If

    End Sub

    Private Sub LlenaCajasCorte()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtCajaCorte As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_CAJAS_SUC_CORTE"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtCajaCorte, Session("rs"))
        'se vacian los expedientes al formulario
        dag_Reportes.DataSource = dtCajaCorte
        dag_Reportes.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub LlenaCajasFlujoReporte()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtCajaFlujo As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CAJAS_TRANS_EFEC"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtCajaFlujo, Session("rs"))
        'se vacian los expedientes al formulario
        dag_Reportes.DataSource = dtCajaFlujo
        dag_Reportes.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub dag_Reportes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Reportes.ItemCommand
        If (e.CommandName = "REPORTE") Then
            Select Case Session("SERIE_REPORTES")

                Case "CI"
                    ImprimeReporteCorteInicial(e.Item.Cells(0).Text, e.Item.Cells(2).Text)
                Case "TE"
                    ImprimeReporteTransEfectivo(e.Item.Cells(0).Text, e.Item.Cells(2).Text)

            End Select
        End If

        dag_Reportes.DataBind()

    End Sub

    Private Sub ImprimeReporteCorteInicial(ByVal idcaja As Integer, ByVal seriefolio As String)

        LlenaReporteCorteInicial(idcaja, seriefolio)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= CorteInicial.pdf")
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

    Private Sub LlenaReporteCorteInicial(ByVal idcaja As Integer, ByVal seriefolio As String)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\CorteInicial.pdf")

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
            .AddAuthor("Desarrollo")
            .AddCreationDate()
            .AddCreator("Corte Inicial")
            .AddSubject("Corte Inicial")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Corte Inicial")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Corte Inicial")
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 14)

        Dim X, Y As Single
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = 80
        Y = 665

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Folio: " + seriefolio, X, Y, 0)
        Y = Y - 30
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Valor", X, Y, 0)
        X = X + 90
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tipo de Denominación", X, Y, 0)
        X = X + 160
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cantidad", X, Y, 0)
        X = X + 130
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Subtotal", X, Y, 0)
        Y = Y - 7
        X = X - 390
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "_____________________________________________________________________________________________", X, Y, 0)

        Y = Y - 7

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CORTE_FLUJO_DENOM_INICIAL"
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, idcaja)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Dim fecha As String
            fecha = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHASIS").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "Fecha: " + fecha, 300, 665, 0)

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)

            Dim Total As Decimal
            Do While Not Session("rs").EOF

                Y = Y - 13
                X = 110

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("VALOR").Value.ToString), X, Y, 0)
                X = X + 105
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("DENOMINACION").Value.ToString, X, Y, 0)
                X = X + 130
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("CANTIDAD").Value.ToString, X, Y, 0)
                X = X + 155
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SUBTOTAL").Value.ToString), X, Y, 0)

                Total = Session("rs").Fields("TOTAL").Value.ToString

                Session("rs").movenext()
            Loop

            Y = Y - 30
            X = 500

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "Total:    " + FormatCurrency(Total), X, Y, 0)

        End If

        Session("Con").Close()

        cb.EndText()
        document.Close()

    End Sub

    Private Sub ImprimeReporteTransEfectivo(ByVal idcaja As Integer, ByVal seriefolio As String)

        LlenaReporteTransEfec(idcaja, seriefolio)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= TransferenciaEfectivo.pdf")
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

    Private Sub LlenaReporteTransEfec(ByVal idcaja As Integer, ByVal seriefolio As String)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TrasnsEfectivo.pdf")

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
            .AddAuthor("Desarrollo")
            .AddCreationDate()
            .AddCreator("Transferencia Efectivo")
            .AddSubject("Transferencia Efectivo")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Transferencia Efectivo")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Transferencia Efectivo")
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 14)

        Dim X, Y As Single
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = 80
        Y = 665

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Folio: " + seriefolio, X, Y, 0)
        Y = Y - 30
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Valor", X, Y, 0)
        X = X + 90
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tipo de Denominación", X, Y, 0)
        X = X + 160
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cantidad", X, Y, 0)
        X = X + 130
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Subtotal", X, Y, 0)
        Y = Y - 7
        X = X - 390
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "_____________________________________________________________________________________________", X, Y, 0)

        Y = Y - 7

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FLUJO_DENOM_TRANS_EFEC"
        Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, idcaja)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE_FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 20, seriefolio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Dim fecha As String
            fecha = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHASIS").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "Fecha: " + fecha, 300, 665, 0)

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)

            Dim Total As Decimal
            Do While Not Session("rs").EOF

                Y = Y - 13
                X = 110

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("VALOR").Value.ToString), X, Y, 0)
                X = X + 105
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("DENOMINACION").Value.ToString, X, Y, 0)
                X = X + 130
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("CANTIDAD").Value.ToString, X, Y, 0)
                X = X + 155
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SUBTOTAL").Value.ToString), X, Y, 0)

                Total = Session("rs").Fields("TOTAL").Value.ToString

                Session("rs").movenext()
            Loop

            Y = Y - 30
            X = 500

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "Total:    " + FormatCurrency(Total), X, Y, 0)

        End If

        Session("Con").Close()

        cb.EndText()
        document.Close()

    End Sub

    Protected Sub lnk_Cerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Cerrar.Click

        Session("SERIE_REPORTES") = Nothing
        Response.Write("<script language='javascript'> {window.returnValue="""";window.close();}</script>")

    End Sub

End Class