Public Class CRED_EXP_PLAN_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim opcion As String = ""
        If Session("VENGODE") = "ConsultaExp.aspx" Or Session("VENGODE") = "CRED_EXP_EXPEDIENTE.ASPX" Or Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX" Or Session("VENGODE") = "SREV_PLANPAGOS.aspx" Or Session("VENGODE") = "SREV_PLANPAGOS_ESPECIAL.aspx" Or Session("VENGODE") = "COB_INFO_GENERAL.aspx" Then
            'PARA QUE CONSULTE LA ULTIMA DISPOSICION
            Session("IDDISP") = "-1"
        End If

        '' SE AGREGA NUEVA VARIABLE, PARA IMPLEMENTAR EL RECALCULO DE LOS PLANES DE PAGOS.
        'If Not Session("VENGODE") = "RECALCULO_PLAN_PAGOS.aspx" Then
        '    opcion = "GENERAL"
        'Else
        '    opcion = Session("OPCION")
        'End If

        VERPLAN(Session("FOLIO"), Session("SUCID"))

    End Sub

    Private Sub VERPLAN(ByVal FOLIO As String, ByVal idsuc As String)

        PlanPagos(FOLIO, idsuc)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= PLANPAGOS(FOLIO:" + Session("CVEEXPE") + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With


    End Sub

    Private Sub PlanPagos(ByVal folio As String, ByVal idsuc As String)

        Dim folioCadena As String = ""
        Dim rfc As String = ""
        Dim titulo As String = ""
        Dim producto As String = ""
        Dim monto As String = ""
        Dim plazo As String = ""
        Dim fechaPago As String = ""
        Dim unidadPeriodicidad As String = ""
        Dim periodicidad As String = ""
        Dim tipotasa As String = ""
        Dim tasa As String = ""
        Dim indice As String = ""
        Dim tasa_mora As String = ""
        Dim indice_mora As String = ""
        Dim c_pcjte As String = ""
        Dim ctotal As String = ""
        Dim c_iva As String = ""
        Dim c_comision As String = ""
        Dim c_cobro_fra As String = ""
        Dim c_tiempo_fra As String = ""
        Dim c_iva_fra As String = ""
        Dim c_total_fra As String = ""
        Dim capital_total As String = ""
        Dim interes_iva As String = ""
        Dim cat As String = ""
        Dim tipoPlan As String = ""
        Dim clasificacion As String = ""
        Dim idDisposicion As String = ""
        Dim fechaDisposicion As String = ""
        Dim montoDisposicion As String = ""
        Dim c_pctje_dis As String = ""
        Dim c_comision_dis As String = ""
        Dim c_iva_dis As String = ""
        Dim monto_ant_dis As String = ""
        Dim saldo_insoluto_dis As String = ""
        Dim interes_dis As String = ""
        Dim opcionComFra As Integer = 0
        Dim clave_comision As String = ""
        Dim no_qui As Integer = 0

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_PDF_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            opcionComFra = Session("rs").Fields("OPCION_COM_FRA").Value.ToString()
            idDisposicion = Session("rs").Fields("IDDISPOSICION").Value.ToString()
            montoDisposicion = Session("rs").Fields("MONTODISPOSICION").Value.ToString()
            c_pctje_dis = Session("rs").Fields("C_PCTJE_DIS").Value.ToString()
            c_comision_dis = Session("rs").Fields("C_COMISION_DIS").Value.ToString()
            c_iva_dis = Session("rs").Fields("C_IVA_DIS").Value.ToString()
            monto_ant_dis = Session("rs").Fields("MONTO_ANT_DIS").Value.ToString()
            saldo_insoluto_dis = Session("rs").Fields("SALDO_INSOLUTO_DIS").Value.ToString()
            interes_dis = Session("rs").Fields("INTERES_DIS").Value.ToString()
            fechaDisposicion = Session("rs").Fields("FECHADISPOSICION").Value.ToString()
            tipoPlan = Session("rs").Fields("TIPOPLANPAGO").Value.ToString()
            clasificacion = Session("rs").Fields("CVE_CLASIFICACION").Value.ToString()
            folioCadena = Session("rs").Fields("CVEEXP").Value.ToString()
            rfc = Session("rs").Fields("RFC").Value.ToString()
            producto = Session("rs").Fields("PRODUCTO").Value.ToString()
            monto = Session("rs").Fields("MONTO").Value.ToString()
            plazo = Session("rs").Fields("PLAZO").Value.ToString()
            no_qui = Session("rs").Fields("NO_QUI").Value.ToString()
            fechaPago = Session("rs").Fields("FECHA_LIBERACION").Value.ToString()
            unidadPeriodicidad = Session("rs").Fields("UNIDADPERIODICIDAD").Value.ToString()
            periodicidad = Session("rs").Fields("PERIODICIDAD").Value.ToString()
            tipotasa = Session("rs").Fields("TIPO_TASA").Value.ToString()
            tasa = Session("rs").Fields("TASA").Value.ToString()
            indice = Session("rs").Fields("INDICE").Value.ToString()
            tasa_mora = Session("rs").Fields("TASA_MORA").Value.ToString()
            indice_mora = Session("rs").Fields("INDICE_MORA").Value.ToString()
            c_pcjte = Session("rs").Fields("C_PCTJE").Value.ToString()
            ctotal = Session("rs").Fields("C_TOTAL").Value.ToString()
            c_iva = Session("rs").Fields("C_IVA").Value.ToString()
            c_comision = Session("rs").Fields("C_COMISION").Value.ToString()
            c_cobro_fra = Session("rs").Fields("C_COBRO_FRA").Value.ToString()
            c_tiempo_fra = Session("rs").Fields("C_TIEMPO_FRA").Value.ToString()
            c_iva_fra = Session("rs").Fields("C_IVA_FRA").Value.ToString()
            c_total_fra = Session("rs").Fields("C_TOTAL_FRA").Value.ToString()
            capital_total = Session("rs").Fields("CAPITAL_TOTAL").Value.ToString()
            interes_iva = Session("rs").Fields("INTERES_IVA").Value.ToString()
            cat = Session("rs").Fields("CAT").Value.ToString()
            clave_comision = Session("rs").Fields("CLAVE").Value.ToString()
        End If

        Session("Con").Close()



        If tipoPlan = "PFSI" Then
            titulo = "Plan de Pagos Fijos Saldos Insolutos"
        ElseIf tipoPlan = "SI" Then
            titulo = "Plan de Pagos Saldos Insolutos"
        End If


        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")
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
            .AddAuthor("SNTE - SNTE")
            .AddCreationDate()
            .AddCreator("SNTE - Plan de Pagos")
            .AddSubject("Plan de Pagos")
            .AddTitle("Plan de Pagos")
            .AddKeywords("Plan de Pagos")
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
        Dim PlanPagos As iTextSharp.text.pdf.PdfImportedPage

        PlanPagos = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(PlanPagos, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 65  'X empieza de izquierda a derecha
        Y = 670 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer

        ' ENCABEZADOS DE LOS PRODUCTOS CUENTA CORRIENTE Y SIMPLE REVOLVENTE

        'ENCABEZADO DEL PRODUCTO SIMPLE
        If clasificacion = "SIM" Or clasificacion = "PINS" Then

            'Producto
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Producto: " + producto, X, Y, 0)

            X = 305

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "RFC: " + rfc + "    Contrato: " + folioCadena, X, Y, 0)
            Y = Y - 15
            X = 65

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha de pago de préstamo: " + fechaPago, X, Y, 0)
            Y = Y - 15
            X = 65

            'Subtitulo de Monto
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, monto, X + 40, Y, 0)

            Y = Y - 15
            X = 65 'Muestro el plazo con su respectiva unidad
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo: " + no_qui.ToString + " QUINCENAS", X, Y, 0)

            Y = Y - 15
            X = 65

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Nombre: " + Session("CLIENTE").ToString, X, Y, 0)

            X = 305
            Y = 655

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 10)

            If tipotasa = "FIJ" Then 'selecciono tasa fija
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa: ", X, Y, 0)
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, tasa, X + 30, Y, 0)
            Else 'selecciono tasa indizada
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa: ", X, Y, 0)

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, indice + " " + tasa, X + 30, Y, 0)

            End If
            Y = Y - 15
            X = 305 'Muestro el plazo con su respectiva unidad

            ' X = 305
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total Interés: " + interes_iva, X, Y, 0)

            Y = Y - 15
            X = 305

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total a Pagar: " + capital_total, X, Y, 0)

            'Declaro de nuevo los títulos para el encabezado de la tabla
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)
            X = 70
            Y = Y - 65
            'Declaras XT y YT para conservar los valores iniciales de X y Y para utilizarlos posteriormente

            XT = X
            YT = Y + 25
        End If


        'ENCABEZADO DEL PRODUCTO ESPECIAL
        If clasificacion = "PESP" Then

            'producto
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Producto: " + producto, X, Y, 0)
            Y = Y - 15

            'Titulo
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plan de Pagos Saldos Insolutos (ESPECIAL)", X, Y, 0)
            X = 320
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "RFC: " + rfc + "    Contrato: " + folioCadena, X, Y, 0)
            Y = Y - 15
            X = 65

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)

            'Subtitulo de Monto
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, monto, X + 40, Y, 0)

            X = 320
            Y = 650
            'Muestro el plazo con su respectiva unidad
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo: " + plazo, X, Y, 0)

            X = 320
            Y = Y - 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Periodicidad: ESPECIAL ", X, Y, 0)
            Y = Y - 15

            X = 320
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 11)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CAT: " + cat, X, Y, 0)
            Y = Y - 15

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)

            'COMISION DE APERTURA
            X = 320


            X = 65
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total a Pagar: " + capital_total, X, Y, 0)
            Y = Y - 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total Interés (IVA incluido): " + interes_iva, X, Y, 0)


            'COMISION DE FRANQUICIA
            If opcionComFra = 1 Then
                Y = Y + 15
                X = 320
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Comisión Franquicia: " + c_cobro_fra, X, Y, 0)
                Y = Y - 15
                X = 320
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "IVA de Comisión: " + c_iva_fra, X, Y, 0)
                Y = Y - 15
                X = 320
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total Comisión: " + c_total_fra, X, Y, 0)
                Y = Y - 15
                X = 320
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cobro de Comisión: " + c_tiempo_fra, X, Y, 0)
                Y = Y - 15
            End If

            X = 65
            Y = 615

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 10)

            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_TASAS_POR_FOLIO"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                Do While Not Session("rs").EOF
                    If Session("rs").Fields("TIPOTASA").Value.ToString = "NOR" Then
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa Ordinaria: ", X, Y, 0)
                        Y = Y - 10
                        If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then

                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TASA").Value.ToString + "% Anual " + "del: " + Left(Session("rs").Fields("FINI").Value.ToString, 10) + " al: " + Left(Session("rs").Fields("FFIN").Value.ToString, 10), X, Y, 0)
                        Else 'TASA NORMAL INDIZADA

                            Select Case Session("rs").Fields("INDICE").Value.ToString
                                Case "1"
                                    indice = "UDI"
                                Case "2"
                                    indice = "TIIE 28"
                                Case "3"
                                    indice = "CETES 28"

                            End Select
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, indice + " (" + Session("rs").Fields("PUNTOS").Value.ToString + "% Anual) " + "del: " + Left(Session("rs").Fields("FINI").Value.ToString, 10) + " al: " + Left(Session("rs").Fields("FFIN").Value.ToString, 10), X, Y, 0)
                        End If
                    Else
                        Y = Y - 10
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa Moratoria: ", X, Y, 0)
                        Y = Y - 10

                        If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TASA").Value.ToString + "% Mensual " + "del: " + Left(Session("rs").Fields("FINI").Value.ToString, 10) + " al: " + Left(Session("rs").Fields("FFIN").Value.ToString, 10), X, Y, 0)
                        Else 'TASA MORA INDIZADA

                            Select Case Session("rs").Fields("INDICE").Value.ToString
                                Case "1"
                                    indice = "UDI"
                                Case "2"
                                    indice = "TIIE 28"
                                Case "3"
                                    indice = "CETES 28"
                            End Select

                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, indice + " (" + Session("rs").Fields("PUNTOS").Value.ToString + "% Mensual) " + "del: " + Left(Session("rs").Fields("FINI").Value.ToString, 10) + " al: " + Left(Session("rs").Fields("FFIN").Value.ToString, 10), X, Y, 0)
                        End If
                    End If
                    Y = Y - 10

                    Session("rs").movenext()
                Loop

                Session("Con").Close()
                Y = Y - 10

                'Declaro de nuevo los títulos para el encabezado de la tabla
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 9)
                X = 75
                Y = Y - 100
                'Declaras XT y YT para conservar los valores iniciales de X y Y para utilizarlos posteriormente

                XT = X
                YT = Y + 25

            End If

        End If

        'ENCABEZADO DEL PDF 

        Dim IVAEXENTO As String
        Dim fecalt As String
        Dim fecaltrec As String


        If clasificacion = "PESP" Then

            'NO. PAGO
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

            'FECHA DE PAGO
            XT = XT + 50
            XAux = XT
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha", XT, YT, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XAux, YT - 10, 0)
            'FECHA RECOMENDADA
            XT = XT + 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha Límite", XT, YT, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XT, YT - 10, 0)

            'CAPITAL
            XT = XT + 65
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

            'INTERES
            XT = XT + 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)

            'IVA
            XT = XT + 5

            'COMISIONES
            XT = XT + 45
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Comisiones", XT, YT, 0)

            'IVA
            XT = XT + 55
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA", XT, YT, 0)
            'TOTAL
            XT = XT + 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)

            'SALDO
            XT = XT + 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo", XT, YT, 0)

        ElseIf clasificacion = "PINS" Then
            XT = XT + 10

            'NO. PAGO
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Quincena", XT, YT, 0)

            'SALDO CAPITAL
            XT = XT + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo Capital", XT, YT, 0)


            'CAPITAL
            XT = XT + 80
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

            'SALDO INTERES
            XT = XT + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo Interés", XT, YT, 0)

            'INTERES
            XT = XT + 80
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)


            'TOTAL
            XT = XT + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)

            'SALDO
            XT = XT + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo", XT, YT, 0)


        Else

            'NO. PAGO
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

            'FECHA DE PAGO
            XT = XT + 60
            XAux = XT
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha de Pago", XT, YT, 0)
            ' cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "", XAux, YT - 10, 0)

            'FECHA RECOMENDADA
            XT = XT + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha Límite", XT, YT, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XAux + 75, YT - 10, 0)

            'CAPITAL
            XT = XT + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

            'INTERES
            XT = XT + 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)

            'IVA
            XT = XT + 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA", XT, YT, 0)
            'TOTAL
            XT = XT + 65
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)

            'SALDO
            XT = XT + 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo", XT, YT, 0)

        End If

        If clasificacion = "PINS" Then
            Session("Con").Open()

            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_PLAN_PAGOS_PINS"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
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
                        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

                        cb = writer.DirectContent
                        PlanPagos = writer.GetImportedPage(Reader, 1)
                        cb.AddTemplate(PlanPagos, 1, 0, 0, 1, 0, 0)
                        cb.BeginText()
                        cb.SetFontAndSize(bf, 9)

                        XT = X
                        YT = Y + 35
                    Else
                        Y = Y - 15
                        X = 65
                    End If
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    X = 85
                    'NO. PAGO
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("FECHA").Value.ToString, X, Y, 0)

                    'SALDO CAPITAL
                    X = X + 105
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SALDO_CAP").Value.ToString), X, Y, 0)

                    'CAPITAL
                    X = X + 80
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("CAPITAL").Value.ToString), X, Y, 0)

                    'SALDO INTERES
                    X = X + 75
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SALDO_INT").Value.ToString), X, Y, 0)

                    'INTERES
                    X = X + 75
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("INTERES").Value.ToString), X, Y, 0)

                    'TOTAL
                    X = X + 80
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("TOTAL").Value.ToString), X, Y, 0)

                    'SALDO
                    X = X + 70
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SALDO").Value.ToString), X, Y, 0)

                    Session("rs").movenext()
                Loop

            End If

        Else

            'DEPENDIENDO DE LA CLASIFICACION SE MANDA LLAMAR EL PROCEDIMIENTO 
            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_PDF_GENERAL"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, idsuc)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                Do While Not Session("rs").EOF
                    If Y < 90 Then
                        Y = 645
                        X = 65
                        cb.EndText()

                        document.NewPage()
                        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

                        cb = writer.DirectContent
                        PlanPagos = writer.GetImportedPage(Reader, 1)
                        cb.AddTemplate(PlanPagos, 1, 0, 0, 1, 0, 0)
                        cb.BeginText()
                        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                        cb.SetFontAndSize(bf, 9)

                        XT = X
                        YT = Y + 35

                        If clasificacion = "PESP" Then

                            'NO. PAGO
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

                            'FECHA DE PAGO
                            XT = XT + 50
                            XAux = XT
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha", XT, YT, 0)
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XAux, YT - 10, 0)
                            'FECHA RECOMENDADA
                            XT = XT + 60
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha Límite", XT, YT, 0)
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XT, YT - 10, 0)

                            'CAPITAL
                            XT = XT + 65
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

                            'INTERES
                            XT = XT + 60
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)

                            'IVA
                            XT = XT + 5

                            'COMISIONES
                            XT = XT + 45
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Comisiones", XT, YT, 0)

                            'IVA
                            XT = XT + 55
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA", XT, YT, 0)


                            'TOTAL
                            XT = XT + 50
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)

                            'SALDO
                            XT = XT + 60
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo", XT, YT, 0)

                        Else
                            'NO. PAGO
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

                            'FECHA DE PAGO
                            XT = XT + 60
                            XAux = XT
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha de Pago", XT, YT, 0)
                            'FECHA RECOMENDADA
                            XT = XT + 75
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha Límite", XT, YT, 0)
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XAux + 80, YT - 10, 0)

                            'CAPITAL
                            XT = XT + 75
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

                            'INTERES
                            XT = XT + 70
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)
                            'IVA
                            XT = XT + 70
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA", XT, YT, 0)

                            'TOTAL
                            XT = XT + 65
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT, 0)

                            XT = XT + 70
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Saldo", XT, YT, 0)

                        End If

                        cb.SetFontAndSize(bf, 8)
                    Else
                        Y = Y - 15
                        X = 65
                    End If

                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    If clasificacion = "PESP" Then
                        X = 70
                        'NO. PAGO
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("NUM_PAGO").Value.ToString, X, Y, 0)

                        'FECHA DE PAGO
                        X = X + 50

                        fecalt = Session("rs").Fields("FECHA").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHA").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHA").Value.ToString.Substring(6, 4)
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)

                        'FECHA RECOMENDADA
                        X = X + 60
                        fecaltrec = Session("rs").Fields("FECHAREC").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHAREC").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHAREC").Value.ToString.Substring(6, 4)

                        If fecaltrec <> fecalt Then
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecaltrec, X, Y, 0)
                        Else
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "-- -- -- -- --", X, Y, 0)
                        End If

                        fecalt = Nothing
                        fecaltrec = Nothing

                        'CAPITAL
                        X = X + 80
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((Session("rs").Fields("CAPITAL").Value.ToString)), X, Y, 0)

                        'INTERES
                        X = X + 60
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((Session("rs").Fields("INTERES").Value.ToString)), X, Y, 0)

                        'IVA
                        X = X + 5

                        'COMISIONES
                        X = X + 45
                        Dim COMISIONES As String
                        COMISIONES = Session("rs").Fields("COMISION").Value.ToString
                        If COMISIONES = 0 Then
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)

                        Else
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((Session("rs").Fields("COMISION").Value.ToString)), X, Y, 0)

                        End If

                        'IVA COMISIONES
                        X = X + 55

                        Dim IVAComision As String
                        IVAEXENTO = Session("rs").Fields("IVA").Value.ToString
                        IVAComision = Session("rs").Fields("IVACOMISION").Value.ToString
                        If IVAEXENTO = "0.00" Then
                            If IVAComision = "0.00" Then
                                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)
                            Else
                                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((CDec(Session("rs").Fields("IVACOMISION").Value.ToString) + CDec(Session("rs").Fields("IVA").Value.ToString))), X, Y, 0)
                            End If
                        Else
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((CDec(Session("rs").Fields("IVACOMISION").Value.ToString) + CDec(Session("rs").Fields("IVA").Value.ToString))), X, Y, 0)
                        End If
                        'TOTAL
                        X = X + 50
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((Session("rs").Fields("TOTAL").Value.ToString)), X, Y, 0)

                        X = X + 60
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency((Session("rs").Fields("SALDO").Value.ToString)), X, Y, 0)

                    Else
                        X = 70
                        'NO. PAGO
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("NUM_PAGO").Value.ToString, X, Y, 0)

                        'FECHA DE PAGO
                        X = X + 60

                        fecalt = Session("rs").Fields("FECHA").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHA").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHA").Value.ToString.Substring(6, 4)
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)

                        'FECHA RECOMENDADA
                        X = X + 75
                        fecaltrec = Session("rs").Fields("FECHAREC").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHAREC").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHAREC").Value.ToString.Substring(6, 4)

                        If fecaltrec <> fecalt Then
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecaltrec, X, Y, 0)
                        Else
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)
                        End If

                        fecalt = Nothing
                        fecaltrec = Nothing

                        'CAPITAL
                        X = X + 95
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("CAPITAL").Value.ToString, X, Y, 0)

                        'INTERES
                        X = X + 70
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("INTERES").Value.ToString, X, Y, 0)

                        'IVA
                        X = X + 65
                        IVAEXENTO = Session("rs").Fields("IVA").Value.ToString

                        If IVAEXENTO = 0 Then
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)
                        Else
                            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + IVAEXENTO, X, Y, 0)

                        End If
                        'TOTAL
                        X = X + 65
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("TOTAL").Value.ToString, X, Y, 0)

                        X = X + 70
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + Session("rs").Fields("SALDO").Value.ToString, X, Y, 0)

                    End If

                    Session("rs").movenext()

                Loop

            End If
        End If

        Y = Y - 15
        X = 65

        'NOTAS
        If Y < 120 Then
            cb.EndText()

            document.NewPage()
            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

            cb = writer.DirectContent
            PlanPagos = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(PlanPagos, 1, 0, 0, 1, 0, 0)
            cb.BeginText()
            'Se cambia el tamaño y el tipo de letra para agregar la nota al PDF
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            X = 65
            Y = 650

        Else
            X = 65
            Y = Y - 15
        End If

        'Nota del PDF
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "NOTAS:", X, Y, 0)
        Y = Y - 15

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El presente documento es de carácter informativo en el cual se deslinda de toda obligación y compromiso por parte de la Institución,", X, Y, 0)
        Y = Y - 15

        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El importe de IVA está determinado conforme a la tasa vigente establecida en la Ley del Impuesto al Valor Agregado, por lo que", X, Y, 0)
        ' Y = Y - 10

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, " esta puede ser modificada conforme lo establezca la citada Ley.", X, Y, 0)
        Y = Y - 15

        If periodicidad = 0 Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El monto a pagar de interés puede variar en cada período debido a la diferencia de días que los componen.", X, Y, 0)
            Y = Y - 15
        End If
        If indice <> "" Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* Al tener tasas indizadas, los pagos varían conforme a la base que se haya elegido como indicador financiero.", X, Y, 0)
            Y = Y - 15
        End If
        If clasificacion = "CTAC" Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* Préstamo Cuenta Corriente, por lo tanto los pagos cambiarán de acuerdo a las disposiciones generadas.", X, Y, 0)
            Y = Y - 15
        End If

        If clasificacion = "SREV" Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* Préstamo Simple Revolvente, por lo tanto los pagos cambiarán de acuerdo a las disposiciones generadas.", X, Y, 0)
        End If

        Session("Con").Close()

        cb.EndText()
        document.Close()

    End Sub


End Class