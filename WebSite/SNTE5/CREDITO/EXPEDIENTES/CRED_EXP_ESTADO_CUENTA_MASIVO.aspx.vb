Public Class CRED_EXP_ESTADO_CUENTA_MASIVO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Estado de Cuenta", "Estado de Cuenta")

    End Sub

    Protected Sub btn_descargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_descargar.Click
        'Session("RFC_PERSONA") = tbx_rfc.ToString
        'VER_ESTADOCTA(Session("RFC_PERSONA"))
        Dim lista_RFC As New List(Of String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_LISTA_DE_RFC"
        Session("rs") = Session("cmd").Execute()


        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF
                lista_RFC.Add(Session("rs").fields("RFC_PERSONA").value.ToString)
                'Session("RFC_PERSONA") = Session("rs").fields("RFC_PERSONA").value.ToString
                'VER_ESTADOCTA(Session("RFC_PERSONA"))
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
        For i = 0 To lista_RFC.Count - 1
            VER_ESTADOCTA(lista_RFC(i).ToString)
        Next
    End Sub

    Private Sub VER_ESTADOCTA(ByVal RFC_PERSONA As String)

        EstadoCuenta(RFC_PERSONA)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ESTADO_CUENTA(RFC:" + RFC_PERSONA + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With
    End Sub

    Private Sub EstadoCuenta(ByVal RFC_PERSONA As String)

        Dim region As String = ""
        Dim delegacion As String = ""
        Dim clave_CT As String = ""
        Dim municipio As String = ""
        Dim rfc As String = ""
        Dim nombre As String = ""


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("RFC_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, RFC_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            region = Session("rs").Fields("REGION").Value.ToString()
            delegacion = Session("rs").Fields("DELEGACION").Value.ToString()
            clave_CT = Session("rs").Fields("CLAVE_CT").Value.ToString()
            municipio = Session("rs").Fields("MUNICIPIO").Value.ToString()
            rfc = Session("rs").Fields("RFC").Value.ToString()
            nombre = Session("rs").Fields("NOMBRE").Value.ToString()


        End If
        Session("Con").Close()

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuenta.pdf")
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
            .AddAuthor("SNTE -  SNTE")
            .AddCreationDate()
            .AddCreator("SNTE - Estado de Cuenta")
            .AddSubject("Estado de Cuentas")
            .AddTitle("Estado de Cuenta")
            .AddKeywords("Estado de Cuenta")
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
        Dim EstadoCuenta As iTextSharp.text.pdf.PdfImportedPage

        EstadoCuenta = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 450  'X empieza de izquierda a derecha
        Y = 745 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)

        Y = Y - 15
        X = 450

        'Delegación
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
        Y = Y - 15
        X = 450

        'Municipio
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

        Y = Y - 15
        X = 450

        'Clave CT
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, clave_CT, X, Y, 0)

        Y = Y - 15
        X = 450


        'Región
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

        Y = Y - 15
        X = 65

        'Nombre
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, nombre.ToString, X, Y, 0)

        Y = Y - 15
        X = 65




        Dim prestamosAhorro As Decimal = 0.00
        Dim aportacionTrab As Decimal = 0.00
        Dim interesesAhorro As Decimal = 0.00
        Dim aportacionEntidad As Decimal = 0.00
        Dim totalPrestamo As Decimal = 0.00
        Dim SubTotalAhorro As Decimal = 0.00
        Dim totalDescuento As Decimal = 0.00
        Dim prestComplementarios As Decimal = 0.00
        Dim pagosAnticipados As Decimal = 0.00
        Dim interesesComplementaros As Decimal = 0.00
        Dim totalPagado As Decimal = 0.00
        Dim total As Decimal = 0.00
        Dim restoPrestamo As Decimal = 0.00
        Dim ajustePrestamo As Decimal = 0.00
        Dim ajustePension As Decimal = 0.00
        Dim saldoAhorro As Decimal = 0.00
        Dim saldoCuentaEje As Decimal = 0.00
        Dim totalPrestamoComplemenatario As Decimal = 0.00

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ESTADO_CTA"
        Session("parm") = Session("cmd").CreateParameter("RFC_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            prestamosAhorro = Session("rs").Fields("PRESTAMO_AHORRO").Value.ToString()
            aportacionTrab = Session("rs").Fields("APORTACION_TRAB").Value.ToString()
            interesesAhorro = Session("rs").Fields("INTERESES_AHORRO").Value.ToString()
            aportacionEntidad = Session("rs").Fields("APORTACION_ENTIDAD").Value.ToString()
            totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
            SubTotalAhorro = Session("rs").Fields("SUBTOTAL_AHORRO").Value.ToString()

            totalDescuento = Session("rs").Fields("TOTAL_DESCUENTOS").Value.ToString()
            prestComplementarios = Session("rs").Fields("PRESTAMOS_COMPLEMENTARIOS").Value.ToString()
            pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
            interesesComplementaros = Session("rs").Fields("INTERESES_COMPLEMENTARIOS").Value.ToString()
            totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
            total = Session("rs").Fields("TOTAL_PRESTAMO_COMPLEMENTARIOS").Value.ToString()

            restoPrestamo = Session("rs").Fields("RESTO_PRESTAMO").Value.ToString()
            saldoCuentaEje = Session("rs").Fields("SALDO_CTA_EJE").Value.ToString()

            ajustePrestamo = Session("rs").Fields("AJUSTE_PRESTAMO").Value.ToString()
            ajustePension = Session("rs").Fields("AJUSTE_PENSION").Value.ToString()
            saldoAhorro = Session("rs").Fields("SALDO_AHORRO").Value.ToString()

            totalPrestamoComplemenatario = Session("rs").Fields("TOTAL_PRESTAMO_COMPLEMENTARIOS").Value.ToString()

        End If
        Session("Con").Close()



        X = 170  'X empieza de izquierda a derecha
        Y = 612 'Y empieza de abajo hacia arriba

        'Prestamo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestamosAhorro), X, Y, 0)


        X = 170
        Y = 592
        'Intereses Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesAhorro), X, Y, 0)

        X = 170
        Y = 573
        'Total Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        X = 170
        Y = 553
        'Total Descuentos 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalDescuento), X, Y, 0)


        X = 170
        Y = 535
        'Pagos Anticipados 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


        X = 170
        Y = 517
        'Total Pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)


        X = 170
        Y = 497
        'Resto Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(restoPrestamo), X, Y, 0)


        X = 170
        Y = 477
        'Saldo a Favor 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoCuentaEje), X, Y, 0)


        Y = 617
        X = 470
        'Aportaciones Trabajador
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionTrab), X, Y, 0)


        Y = Y - 20
        X = 470
        'Aportaciones Entidad
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionEntidad), X, Y, 0)


        Y = Y - 17
        X = 470
        'Intereses por inversiones
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(0.00), X, Y, 0)

        Y = Y - 20
        X = 470
        'Intereses por préstamos
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(0.00), X, Y, 0)


        Y = Y - 17
        X = 470
        'SubTotal Ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(SubTotalAhorro), X, Y, 0)


        Y = Y - 18
        X = 470
        'Préstamo Complementarios
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestComplementarios), X, Y, 0)


        Y = Y - 19
        X = 470
        'Intereses
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesComplementaros), X, Y, 0)


        Y = Y - 20
        X = 470
        'Total Préstamo Complementario
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamoComplemenatario), X, Y, 0)

        Y = Y - 20
        X = 470
        'Ajuste Préstamo de ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePrestamo), X, Y, 0)


        Y = Y - 20
        X = 470
        'Ajuste Pension 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePension), X, Y, 0)


        'Y = Y - 15
        'X = 30
        ''Ajuste Préstamo de Contingencia (DU)
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePrestamo), X, Y, 0)


        Y = Y - 20
        X = 470
        'Ajuste Saldo Préstamos periodo anterior
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(0.00), X, Y, 0)


        Y = Y - 38
        X = 470
        'Saldo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAhorro), X, Y, 0)


        Y = Y - 58
        X = 30


        'ESTADO DE CUENTA

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)

        'XT = X
        'YT = Y - 6

        'QUINCENA
        'XT = XT + 50

        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Quincena", XT, YT, 0)

        ''AÑO
        'XT = XT + 130
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Año", XT, YT, 0)


        ''APORTACION TRABAJADOR 
        'XT = XT + 125
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "AP Trabajador", XT, YT, 0)


        ''APORTACION ENTIDAD
        'XT = XT + 110
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "AP Entidad", XT, YT, 0)

        ''TOTAL
        'XT = XT + 100
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)
        Y = Y - 35
        X = 30

        Session("Con").Open()

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA"
        Session("parm") = Session("cmd").CreateParameter("RFC_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        X = 70

        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF
                If Y < 90 Then
                    Y = 645
                    X = 65
                    cb.EndText()

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuenta.pdf")

                    cb = writer.DirectContent
                    EstadoCuenta = writer.GetImportedPage(Reader, 2)
                    cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    cb.SetFontAndSize(bf, 9)


                    Y = 710
                    X = 30

                    'XT = X
                    'YT = Y + 35
                Else
                    Y = Y - 14
                    X = 60
                End If
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 8)

                X = 65
                'QUINCENA
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("QUINCENA").Value.ToString, X, Y, 0)

                'AÑO
                X = X + 100
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("ANIO").Value.ToString, X, Y, 0)

                'APORTACION TRABAJADOR 
                X = X + 130
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("AP_TRABAJADOR").Value.ToString), X, Y, 0)

                'APORTACION ENTIDAD
                X = X + 170
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("AP_ENTIDAD").Value.ToString), X, Y, 0)

                'TOTAL
                X = X + 96
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("TOTAL").Value.ToString), X, Y, 0)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If

        cb.EndText()
        document.Close()

    End Sub
End Class