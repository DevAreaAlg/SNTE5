Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_COTIZADOR_PLAN
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim nombreProducto As String
        Dim monto As String
        Dim tipoTasa As String
        Dim tasa As Decimal
        Dim idIndice As Integer
        Dim idproducto As Integer
        Dim tipoPlan As String
        Dim tipoTasaMora As String
        Dim tasaMora As Decimal
        Dim idIndiceMora As Integer
        Dim idSuc As Integer
        Dim tipoPeriodo As String
        Dim periodo As Integer
        Dim cadena As String
        Dim fechaPago As String
        Dim plazo As Integer
        Dim unidadPlazo As String
        Dim rango As Integer
        Dim montoDisposicion As Decimal
        Dim id_cotizacion As Integer

        id_cotizacion = Session("ID")
        tipoPlan = Session("TIPOPLAN")
        idproducto = Session("IDPROD")
        monto = Session("MONTO")
        tipoTasa = Session("TIPOTASA")
        tasa = Session("TASA")
        idIndice = Session("IDINDICE")
        tipoTasaMora = Session("TIPOTASAMORA")
        tasaMora = Session("TASAMORA")
        idIndiceMora = Session("IDINDICEMORA")
        idSuc = Session("SUCURSAL")
        tipoPeriodo = Session("TIPOPERIODO")
        periodo = Session("PERIODO")
        cadena = Session("CADENA")
        fechaPago = Session("FECHAPAGO")
        unidadPlazo = Session("UNIDADPLAZO")
        plazo = Session("PLAZO")
        nombreProducto = Session("NOMBRE_PRODUCTO")
        rango = Session("RANGO")
        montoDisposicion = Session("MONTODISPOSICION")


        Select Case Session("CLASIFICACION")

            Case "ARFIN"
                VERPLAN_ARRENDAMIENTO(idproducto, tipoPlan, nombreProducto, monto, tasa, tasaMora, idSuc, tipoPeriodo, periodo, fechaPago, unidadPlazo, plazo)
            Case "CTAC"
                VERPLAN_GENERAL(idproducto, tipoPlan, monto, tipoTasa, tasa, idIndice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion)
            Case "SREV"
                VERPLAN_GENERAL(idproducto, tipoPlan, monto, tipoTasa, tasa, idIndice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion)
            Case "SIM"
                If tipoPlan = "ES" Then
                    VERPLAN_GENERAL_ESPECIAL(idproducto, tipoPlan, monto, tipoTasa, tasa, idIndice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion, id_cotizacion)
                Else
                    VERPLAN_GENERAL(idproducto, tipoPlan, monto, tipoTasa, tasa, idIndice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion)
                End If

            Case "PESP"
                VERPLAN_GENERAL_ESPECIAL(idproducto, tipoPlan, monto, tipoTasa, tasa, idIndice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion, id_cotizacion)
            Case Else

        End Select

    End Sub

    Private Sub VERPLAN_GENERAL(ByVal idproducto As Integer, ByVal tipoPlan As String, ByVal monto As Decimal, ByVal tipoTasa As String, ByVal tasa As Decimal, ByVal idindice As Integer, ByVal tipoTasaMora As String, ByVal tasaMora As Decimal, ByVal idIndiceMora As Integer, ByVal idSuc As Integer, ByVal tipoPeriodo As String, ByVal periodo As Integer, ByVal cadena As String, ByVal fechaPago As String, ByVal unidadPlazo As String, ByVal plazo As Integer, ByVal rango As Integer, ByVal montoDisposicion As Decimal)

        General(idproducto, tipoPlan, monto, tipoTasa, tasa, idindice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= PLANPAGOS.pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With


    End Sub

    Private Sub VERPLAN_GENERAL_ESPECIAL(ByVal idproducto As Integer, ByVal tipoPlan As String, ByVal monto As Decimal, ByVal tipoTasa As String, ByVal tasa As Decimal, ByVal idindice As Integer, ByVal tipoTasaMora As String, ByVal tasaMora As Decimal, ByVal idIndiceMora As Integer, ByVal idSuc As Integer, ByVal tipoPeriodo As String, ByVal periodo As Integer, ByVal cadena As String, ByVal fechaPago As String, ByVal unidadPlazo As String, ByVal plazo As Integer, ByVal rango As Integer, ByVal montoDisposicion As Decimal, ByVal id_cotizacion As Integer)

        General_Especial(idproducto, tipoPlan, monto, tipoTasa, tasa, idindice, tipoTasaMora, tasaMora, idIndiceMora, idSuc, tipoPeriodo, periodo, cadena, fechaPago, unidadPlazo, plazo, rango, montoDisposicion, id_cotizacion)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= PLANPAGOS.pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With


    End Sub


    Private Sub General(ByVal IDPROD As Integer, ByVal tipoPlan As String, ByVal monto As Decimal, ByVal tipoTasa As String, ByVal tasa As Decimal, ByVal idindice As Integer, ByVal tipoTasaMora As String, ByVal tasaMora As Decimal, ByVal idIndiceMora As Integer, ByVal idSuc As Integer, ByVal tipoPeriodo As String, ByVal periodo As Integer, ByVal cadena As String, ByVal fechaPago As String, ByVal unidad As String, ByVal plazo As Integer, ByVal rango As Integer, ByVal montoDisposicion As Decimal)
        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solicitud y especifíca la ruta dónde esta el PDF

        'Ruta donde está el PDF
        Dim Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

        'Traigo el total de paginas
        Dim n As Integer = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        'CREACION DE UN DOCUMENTO

        Dim document As New iTextSharp.text.Document(psize, 100, 100, 200, 100)

        With document
            .AddAuthor("Desarrollo ")
            .AddCreationDate()
            .AddCreator("Plan de Pagos")
            .AddSubject("Plan de Pagos")
            .AddTitle("Plan de Pagos")
            .AddKeywords("Plan de Pagos")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Plan_PFSI As iTextSharp.text.pdf.PdfImportedPage = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '9 es el tamaño de la letra
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 65  'X empieza de izquierda a derecha
        Y = 680 'Y empieza de abajo hacia arriba


        Dim producto As String
        Dim tipoPlanPagos As String
        Dim plazoPago As String
        Dim fecha_Pago As String
        Dim cat As Decimal
        Dim tasaPlan As String
        Dim montoCredito As String
        Dim tasa_mora As String
        Dim periodicidad As String
        Dim capitalTotal As String
        Dim interes_iva As String
        Dim clasificacion As String


        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COTIZADOR_PDF_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 11, IDPROD)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_PLAN", Session("adVarChar"), Session("adParamInput"), 10, tipoPlan)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 30, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 30, tipoTasa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, tasa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, idindice)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASAMORA", Session("adVarChar"), Session("adParamInput"), 20, tipoTasaMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMORA", Session("adVarChar"), Session("adParamInput"), 20, tasaMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICEMORA", Session("adVarChar"), Session("adParamInput"), 11, idIndiceMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERIODO", Session("adVarChar"), Session("adParamInput"), 15, tipoPeriodo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 20, periodo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_PAGO", Session("adVarChar"), Session("adParamInput"), 10, fechaPago)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 20, unidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, plazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RANGO", Session("adVarChar"), Session("adParamInput"), 10, rango)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            producto = Session("rs").Fields("PRODUCTO").Value.ToString
            tipoPlanPagos = Session("rs").Fields("TIPO_PLAN").Value.ToString
            plazoPago = Session("rs").Fields("PLAZO").Value.ToString
            fecha_Pago = Session("rs").Fields("FECHA_PAGO").Value.ToString
            tasaPlan = Session("rs").Fields("TASA").Value.ToString
            tasa_mora = Session("rs").Fields("TASA_MORA").Value.ToString
            periodicidad = Session("rs").Fields("PERIODICIDAD").Value.ToString
            rango = Session("rs").Fields("RANGO").Value.ToString
            clasificacion = Session("rs").Fields("CLASIFICACION").Value.ToString
            montoCredito = Session("rs").Fields("MONTO").Value.ToString

        End If
        Session("Con").Close()

        Dim plan As DataTable = New DataTable()
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("SEL_COTIZADOR_PDF_GENERAL", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure
            insertCommand.Parameters.Add("IDPROD", SqlDbType.Int).Value = IDPROD
            insertCommand.Parameters.Add("TIPO_PLAN", SqlDbType.VarChar).Value = tipoPlan
            If clasificacion = "SREV" Or clasificacion = "CTAC" Then
                insertCommand.Parameters.Add("MONTO", SqlDbType.Decimal).Value = montoDisposicion
            Else
                insertCommand.Parameters.Add("MONTO", SqlDbType.Decimal).Value = monto
            End If
            insertCommand.Parameters.Add("TIPOTASA", SqlDbType.VarChar).Value = tipoTasa
            insertCommand.Parameters.Add("TASA", SqlDbType.Decimal).Value = tasa
            insertCommand.Parameters.Add("IDINDICE", SqlDbType.Int).Value = idindice
            insertCommand.Parameters.Add("IDSUC", SqlDbType.Int).Value = idSuc
            insertCommand.Parameters.Add("TIPOPERIODO", SqlDbType.VarChar).Value = tipoPeriodo
            insertCommand.Parameters.Add("PERIODO", SqlDbType.Int).Value = periodo
            insertCommand.Parameters.Add("CADENA", SqlDbType.VarChar).Value = cadena
            insertCommand.Parameters.Add("FECHAPAGO", SqlDbType.VarChar).Value = fechaPago
            insertCommand.Parameters.Add("UNIDAD", SqlDbType.VarChar).Value = unidad
            insertCommand.Parameters.Add("PLAZO", SqlDbType.Int).Value = plazo
            insertCommand.Parameters.Add("RANGO", SqlDbType.Int).Value = rango
            insertCommand.Parameters.Add("ID_COTIZACION", SqlDbType.Int).Value = 0
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            plan.Load(myReader)

            cat = plan.Rows(0).Item("CAT").ToString()
            capitalTotal = plan.Rows(0).Item("CAPITAL_TOTAL").ToString()
            interes_iva = plan.Rows(0).Item("INTERES_IVA").ToString()
            plazoPago = plan.Rows(0).Item("PLAZO_FINAL").ToString()
        End Using



        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Producto: " + producto, X, Y, 0)
        Y = Y - distanciaVertical
        'Tipo de plan de pagos
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, tipoPlanPagos, X, Y, 0)


        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = X + distanciaHorizontal
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo: " + plazoPago + " " + unidad, X, Y, 0)

        Y = Y - distanciaVertical
        X = X - distanciaHorizontal
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha de pago del préstamo: " + fecha_Pago, X, Y, 0)

        X = X + distanciaHorizontal

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa: ", X, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, tasaPlan, X + 30, Y, 0)
        X = X + (distanciaHorizontal / 2) + 15

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CAT: " + CStr(cat) + " %", X, Y, 0)

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)


        X = X - distanciaHorizontal - (distanciaHorizontal / 2) - 15
        Y = Y - distanciaVertical
        If clasificacion = "SREV" Or clasificacion = "CTAC" Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Línea del Préstamo: " + montoCredito, X, Y, 0)
            Y = Y - distanciaVertical
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto de Disposición: " + FormatCurrency(montoDisposicion), X, Y, 0)
            Y = Y + distanciaVertical
        Else
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto: " + montoCredito, X, Y, 0)
        End If

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = X + distanciaHorizontal
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa Moratoria: ", X, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, tasa_mora, X + 70, Y, 0)

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        If clasificacion = "SREV" Or clasificacion = "CTAC" Then
            Y = Y - distanciaVertical
        End If
        Y = Y - distanciaVertical
        X = X - distanciaHorizontal
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Periodicidad: " + periodicidad, X, Y, 0)

        If clasificacion = "SREV" Or clasificacion = "CTAC" Then
            Y = Y + distanciaVertical
        End If
        X = X + distanciaHorizontal
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total a Pagar:  $ " + capitalTotal, X, Y, 0)
        Y = Y - distanciaVertical
        If clasificacion = "CTAC" Then
            Y = Y - distanciaVertical
            X = X - distanciaHorizontal
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Rango de Capital: " + CStr(rango), X, Y, 0)
            X = X + distanciaHorizontal
        End If

        If clasificacion = "CTAC" Then
            Y = Y + distanciaVertical
        End If
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total de Interés + IVA a Pagar: $ " + interes_iva, X, Y, 0)


        Y = Y - 10

        'Declaro de nuevo los títulos para el encabezado de la tabla
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)
        X = 65
        Y = Y - 80
        'Declaras XT y YT para conservar los valores iniciales de X y Y para utilizarlos posteriormente

        XT = X
        YT = Y + 35


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


        For Each rs As DataRow In plan.Rows
            If Y < 90 Then
                Y = 645
                X = 65
                cb.EndText()

                document.NewPage()
                Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

                cb = writer.DirectContent
                Plan_PFSI = writer.GetImportedPage(Reader, 1)
                cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)
                cb.BeginText()
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 9)

                XT = X
                YT = Y + 35

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

                cb.SetFontAndSize(bf, 8)
            Else
                Y = Y - 15
                X = 65
            End If

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)
            X = 70
            'NO. PAGO
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, rs("NUM_PAGO").ToString, X, Y, 0)

            'FECHA DE PAGO
            X = X + 60
            Dim fecalt As String
            fecalt = rs("FECHAPAGO").ToString.Substring(0, 2) + "/" + rs("FECHAPAGO").ToString.Substring(3, 2) + "/" + rs("FECHAPAGO").ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)

            'FECHA RECOMENDADA
            X = X + 75
            Dim fecaltrec As String
            fecaltrec = rs("FECHAREC").ToString.Substring(0, 2) + "/" + rs("FECHAREC").ToString.Substring(3, 2) + "/" + rs("FECHAREC").ToString.Substring(6, 4)

            If fecaltrec <> fecalt Then
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecaltrec, X, Y, 0)
            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)
            End If

            fecalt = Nothing
            fecaltrec = Nothing


            'CAPITAL
            X = X + 95
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + rs("CAPITAL").ToString, X, Y, 0)

            'INTERES
            X = X + 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + rs("INTERES").ToString, X, Y, 0)

            'IVA
            X = X + 65
            Dim IVAEXENTO As String
            IVAEXENTO = rs("IVA").ToString

            If IVAEXENTO = 0 Then
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)
            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + rs("IVA").ToString, X, Y, 0)

            End If
            'TOTAL
            X = X + 65
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + rs("TOTAL").ToString, X, Y, 0)

            X = X + 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "$" + rs("SALDO").ToString, X, Y, 0)
        Next

        Y = Y - 15
        X = 65

        'NOTAS
        If Y < 120 Then
            cb.EndText()

            document.NewPage()
            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

            cb = writer.DirectContent
            Plan_PFSI = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* CAT (Costo Anual Total sin IVA). ", X, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Para fines informativos y de comparación exclusivamente. ", X + 130, Y, 0)
        Y = Y - 15
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El presente documento es de carácter informativo en el cual se deslinda de toda obligación y compromiso por parte de la Entidad Financiera.", X, Y, 0)
        Y = Y - 15

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El importe de IVA está determinado conforme a la tasa vigente establecida en la Ley del Impuesto al Valor Agregado, por lo que", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, " esta puede ser modificada conforme lo establezca la citada Ley.", X, Y, 0)
        Y = Y - 15

        If clasificacion = "CTAC" Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* Préstamo Cuenta Corriente, por lo tanto los pagos cambiarán de acuerdo a las disposiciones generadas.", X, Y, 0)
            Y = Y - 15
        End If

        If clasificacion = "SREV" Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* Préstamo Simple Revolvente, por lo tanto los pagos cambiarán de acuerdo a las disposiciones generadas.", X, Y, 0)
        End If


        cb.EndText()
        document.Close()
    End Sub

    Private Sub VERPLAN_ARRENDAMIENTO(ByVal idproducto As Integer, ByVal tipoPlan As String, ByVal nombreProducto As String, ByVal monto As Decimal, ByVal tasa As Decimal, ByVal tasaMora As Decimal, ByVal idSuc As Integer, ByVal tipoPeriodo As String, ByVal periodo As Integer, ByVal fechaPago As String, ByVal unidadPlazo As String, plazo As Integer)

        GeneralArrendamiento(idproducto, nombreProducto, tipoPlan, monto, tasa, tasaMora, idSuc, tipoPeriodo, periodo, fechaPago, unidadPlazo, plazo)
        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= PLANPAGOS.pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With


    End Sub


    Private Sub General_Especial(ByVal IDPROD As Integer, ByVal tipoPlan As String, ByVal monto As Decimal, ByVal tipoTasa As String, ByVal tasa As Decimal, ByVal idindice As Integer, ByVal tipoTasaMora As String, ByVal tasaMora As Decimal, ByVal idIndiceMora As Integer, ByVal idSuc As Integer, ByVal tipoPeriodo As String, ByVal periodo As Integer, ByVal cadena As String, ByVal fechaPago As String, ByVal unidad As String, ByVal plazo As Integer, ByVal rango As Integer, ByVal montoDisposicion As Decimal, ByVal id_cotizacion As Integer)
        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solicitud y especifíca la ruta dónde esta el PDF

        'Ruta donde está el PDF
        Dim Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

        'Traigo el total de paginas
        Dim n As Integer = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        'CREACION DE UN DOCUMENTO

        Dim document As New iTextSharp.text.Document(psize, 100, 100, 200, 100)

        With document
            .AddAuthor("Desarrollo ")
            .AddCreationDate()
            .AddCreator("Plan de Pagos")
            .AddSubject("Plan de Pagos")
            .AddTitle("Plan de Pagos")
            .AddKeywords("Plan de Pagos")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Plan_PFSI As iTextSharp.text.pdf.PdfImportedPage = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '9 es el tamaño de la letra
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 65  'X empieza de izquierda a derecha
        Y = 680 'Y empieza de abajo hacia arriba


        Dim producto As String
        Dim tipoPlanPagos As String
        Dim plazoPago As String
        Dim fecha_Pago As String
        Dim cat As Decimal
        Dim tasaPlan As String
        Dim montoCredito As String
        Dim tasa_mora As String
        Dim periodicidad As String
        Dim capitalTotal As String
        Dim interes_iva As String
        Dim clasificacion As String
        Dim indice As String

        Dim IVAEXENTO As String
        Dim c_pcjte As String = ""
        Dim ctotal As String = ""
        Dim c_iva As String = ""
        Dim c_comision As String = ""
        Dim c_cobro_fra As String = ""
        Dim c_tiempo_fra As String = ""
        Dim c_iva_fra As String = ""
        Dim c_total_fra As String = ""
        Dim opcionComFra As Integer



        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COTIZADOR_PDF_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 11, IDPROD)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_PLAN", Session("adVarChar"), Session("adParamInput"), 10, tipoPlan)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 30, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 30, tipoTasa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, tasa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 20, idindice)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOTASAMORA", Session("adVarChar"), Session("adParamInput"), 20, tipoTasaMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMORA", Session("adVarChar"), Session("adParamInput"), 20, tasaMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICEMORA", Session("adVarChar"), Session("adParamInput"), 11, idIndiceMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPERIODO", Session("adVarChar"), Session("adParamInput"), 15, tipoPeriodo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 20, periodo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_PAGO", Session("adVarChar"), Session("adParamInput"), 10, fechaPago)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 20, unidad)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 10, plazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RANGO", Session("adVarChar"), Session("adParamInput"), 10, rango)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            producto = Session("rs").Fields("PRODUCTO").Value.ToString
            tipoPlanPagos = Session("rs").Fields("TIPO_PLAN").Value.ToString
            plazoPago = Session("rs").Fields("PLAZO").Value.ToString
            fecha_Pago = Session("rs").Fields("FECHA_PAGO").Value.ToString
            clasificacion = Session("rs").Fields("CLASIFICACION").Value.ToString
            montoCredito = Session("rs").Fields("MONTO").Value.ToString

        End If
        Session("Con").Close()

        Dim plan As DataTable = New DataTable()
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            connection.Open()
            ' Configure the SqlCommand and SqlParameter.
            Dim insertCommand As New SqlCommand("SEL_COTIZADOR_PDF_GENERAL", connection)
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure
            insertCommand.Parameters.Add("IDPROD", SqlDbType.Int).Value = IDPROD
            insertCommand.Parameters.Add("TIPO_PLAN", SqlDbType.VarChar).Value = tipoPlan
            insertCommand.Parameters.Add("MONTO", SqlDbType.Decimal).Value = monto
            insertCommand.Parameters.Add("TIPOTASA", SqlDbType.VarChar).Value = tipoTasa
            insertCommand.Parameters.Add("TASA", SqlDbType.Decimal).Value = tasa
            insertCommand.Parameters.Add("IDINDICE", SqlDbType.Int).Value = idindice
            insertCommand.Parameters.Add("IDSUC", SqlDbType.Int).Value = idSuc
            insertCommand.Parameters.Add("TIPOPERIODO", SqlDbType.VarChar).Value = tipoPeriodo
            insertCommand.Parameters.Add("PERIODO", SqlDbType.Int).Value = periodo
            insertCommand.Parameters.Add("CADENA", SqlDbType.VarChar).Value = cadena
            insertCommand.Parameters.Add("FECHAPAGO", SqlDbType.VarChar).Value = fechaPago
            insertCommand.Parameters.Add("UNIDAD", SqlDbType.VarChar).Value = unidad
            insertCommand.Parameters.Add("PLAZO", SqlDbType.Int).Value = plazo
            insertCommand.Parameters.Add("RANGO", SqlDbType.Int).Value = rango
            insertCommand.Parameters.Add("ID_COTIZACION", SqlDbType.Int).Value = id_cotizacion
            '  Execute the command.
            Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
            plan.Load(myReader)

            cat = plan.Rows(0).Item("CAT").ToString()
            capitalTotal = plan.Rows(0).Item("CAPITAL_TOTAL").ToString()
            interes_iva = plan.Rows(0).Item("INTERES_IVA").ToString()
            plazoPago = plan.Rows(0).Item("PLAZO_FINAL").ToString()

            c_pcjte = plan.Rows(0).Item("C_PCTJE").ToString()
            ctotal = plan.Rows(0).Item("C_TOTAL").ToString()
            c_iva = plan.Rows(0).Item("C_IVA").ToString()
            c_comision = plan.Rows(0).Item("C_COMISION").ToString()
            c_cobro_fra = plan.Rows(0).Item("C_COBRO_FRA").ToString()
            c_tiempo_fra = plan.Rows(0).Item("C_TIEMPO_FRA").ToString()
            c_iva_fra = plan.Rows(0).Item("C_IVA_FRA").ToString()
            c_total_fra = plan.Rows(0).Item("C_TOTAL_FRA").ToString()
            'opcionComFra = plan.Rows(0).Item("OPCION_FRA").ToString()
        End Using



        'producto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Producto: " + producto, X, Y, 0)
        Y = Y - 15

        'Titulo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plan de Pagos Saldos Insolutos (ESPECIAL)", X, Y, 0)

        Y = Y - 15
        X = 65

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha de pago de préstamo: " + fechaPago, X, Y, 0)
        Y = Y - 15
        X = 65
        'Subtitulo de Monto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto:", X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, montoCredito, X + 40, Y, 0)

        X = 320
        Y = 650
        'Muestro el plazo con su respectiva unidad
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo: " + plazoPago, X, Y, 0)

        X = 320
        Y = Y - 15
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Periodicidad: ESPECIAL ", X, Y, 0)
        Y = Y - 15

        X = 320
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 11)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CAT: " + CStr(cat) + " %", X, Y, 0)
        Y = Y - 15

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        'COMISION DE APERTURA
        X = 320
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Comisión Apertura (" + c_pcjte + "): " + c_comision, X, Y, 0)
        Y = Y - 15
        X = 320
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "IVA de Comisión: " + c_iva, X, Y, 0)
        Y = Y - 15
        X = 320
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total Comisión: " + ctotal, X, Y, 0)
        Y = Y - 15

        X = 65
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total a Pagar: " + capitalTotal, X, Y, 0)
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
        Session("cmd").CommandText = "SEL_COTIZADOR_TASAS_POR_ID"
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID").ToString)
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

        End If
        Session("Con").Close()
        Y = Y - 10

        'Declaro de nuevo los títulos para el encabezado de la tabla
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)
        X = 70
        Y = Y - 100
        'Declaras XT y YT para conservar los valores iniciales de X y Y para utilizarlos posteriormente
        '  Dim XT, YT, XAux As Single
        XT = X
        YT = Y + 25

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



        For Each rs As DataRow In plan.Rows
            'COMIENZA LA SEGUNDA HOJA DEL PDF
            If Y < 90 Then
                Y = 645
                X = 65
                cb.EndText()

                document.NewPage()
                Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

                cb = writer.DirectContent
                Plan_PFSI = writer.GetImportedPage(Reader, 1)
                cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)
                cb.BeginText()
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 9)

                XT = X
                YT = Y + 35

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

                cb.SetFontAndSize(bf, 8)
            Else
                Y = Y - 15
                X = 65
            End If


            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)
            X = 70

            'NO. PAGO
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, rs("NUM_PAGO").ToString, X, Y, 0)

            'FECHA DE PAGO
            X = X + 50
            Dim fecalt As String
            fecalt = rs("FECHA").ToString.Substring(0, 2) + "/" + rs("FECHA").ToString.Substring(3, 2) + "/" + rs("FECHA").ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)
            'FECHA RECOMENDADA
            X = X + 60
            Dim fecaltrec As String
            fecaltrec = rs("FECHAREC").ToString.Substring(0, 2) + "/" + rs("FECHAREC").ToString.Substring(3, 2) + "/" + rs("FECHAREC").ToString.Substring(6, 4)

            If fecaltrec <> fecalt Then
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecaltrec, X, Y, 0)
            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "-- -- -- -- --", X, Y, 0)
            End If

            fecalt = Nothing
            fecaltrec = Nothing

            'CAPITAL
            X = X + 80
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(rs("CAPITAL").ToString), X, Y, 0)

            'INTERES
            X = X + 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(rs("INTERES").ToString), X, Y, 0)

            'IVA
            X = X + 5

            'COMISIONES
            X = X + 45
            Dim COMISIONES As String
            COMISIONES = rs("COMISION").ToString
            If COMISIONES = 0 Then
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)

            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(rs("COMISION").ToString), X, Y, 0)

            End If

            'IVA COMISIONES
            X = X + 55

            'Dim IVAEXENTO As String
            Dim IVAComision As String
            IVAEXENTO = rs("IVA").ToString
            IVAComision = rs("IVACOMISION").ToString
            If IVAEXENTO = "0.00" Then
                If IVAComision = "0.00" Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(CDec(rs("IVACOMISION").ToString) + CDec(rs("IVA").ToString)), X, Y, 0)
                End If
            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(CDec(rs("IVACOMISION").ToString) + CDec(rs("IVA").ToString)), X, Y, 0)
            End If
            'TOTAL
            X = X + 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(rs("TOTAL").ToString), X, Y, 0)

            X = X + 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(rs("SALDO").ToString), X, Y, 0)

        Next

        Y = Y - 15
        X = 65

        'NOTAS
        If Y < 120 Then
            cb.EndText()

            document.NewPage()
            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

            cb = writer.DirectContent
            Plan_PFSI = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* CAT (Costo Anual Total sin IVA). ", X, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Para fines informativos y de comparación exclusivamente. ", X + 130, Y, 0)
        Y = Y - 15
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El presente documento es de carácter informativo en el cual se deslinda de toda obligación y compromiso por parte de la Entidad Financiera.", X, Y, 0)
        Y = Y - 15

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El importe de IVA está determinado conforme a la tasa vigente establecida en la Ley del Impuesto al Valor Agregado, por lo que", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, " esta puede ser modificada conforme lo establezca la citada Ley.", X, Y, 0)
        Y = Y - 15

        cb.EndText()
        document.Close()
    End Sub


    Private Sub GeneralArrendamiento(ByVal idproducto As Integer, ByVal nombreProducto As String, ByVal tipoPlan As String, ByVal monto As Decimal, ByVal tasa As Decimal, ByVal tasaMora As Decimal, ByVal idSuc As Integer, ByVal tipoPeriodo As String, ByVal periodo As Integer, ByVal fechaPago As String, ByVal unidadPlazo As String, plazo As Integer)
        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solicitud y especifíca la ruta dónde esta el PDF
        Dim Reader As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

        'Ruta donde está el PDF
        'Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\plandepagos.pdf")

        'Traigo el total de paginas
        Dim n As Integer = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        'CREACION DE UN DOCUMENTO

        Dim document As New iTextSharp.text.Document(psize, 100, 100, 200, 100)

        With document
            .AddAuthor("Desarrollo")
            .AddCreationDate()
            .AddCreator("Plan de Pagos")
            .AddSubject("Plan de Pagos")
            .AddTitle("Plan de Pagos")
            .AddKeywords("Plan de Pagos")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO

        Dim writer As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Plan_PFSI As iTextSharp.text.pdf.PdfImportedPage = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '9 es el tamaño de la letra
        cb.SetFontAndSize(bf, 9)
        Dim X, Y As Single
        X = 65  'X empieza de izquierda a derecha
        Y = 680 'Y empieza de abajo hacia arriba
        'Titulo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Producto: " + nombreProducto, X, Y, 0)
        Y -= 15
        X = 65
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plan de Pagos Arrendamiento Financiero", X, Y, 0)
        X = 305
        Y -= 15
        X = 65
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha de inicio del arrendamiento: " + fechaPago, X, Y, 0)
        X = 305
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Tasa: ", X, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CStr(tasa) + "% Anual  CAT: 0.00 %", X + 30, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)
        Y -= 15
        X = 65
        'Subtitulo de Monto       
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto: " + FormatCurrency(monto), X, Y, 0)
        X = 305
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Comisión Apertura (0 % ): $ 0.00", X, Y, 0)
        Y -= 15
        X = 65 'Muestro el plazo con su respectiva unidad

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo: " + CStr(plazo) + " " + unidadPlazo, X, Y, 0)

        X = 305
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "IVA de Comisión: $ 0.00", X, Y, 0)
        Y -= 15
        X = 65
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Periodicidad: Mensual", X, Y, 0)
        X = 305
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Total Comisión: $0.00", X, Y, 0)
        Y -= 15

        Dim yMontoSuma As Integer = Y

        'Declaro de nuevo los títulos para el encabezado de la tabla
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)
        X = 65
        Y -= 55
        'Declaras XT y YT para conservar los valores iniciales de X y Y para utilizarlos posteriormente
        Dim XT, YT, XAux As Single
        XT = X
        YT = Y + 35

        'FECHA DE PAGO
        XAux = XT
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha", XT, YT, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XT, YT - 10, 0)

        'FECHA DE PAGO RECOMENDADA
        XT += 70
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha de Pago", XT, YT, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Recomendada", XT, YT - 10, 0)

        'MENSUALIDAD TOTAL
        XT += 80
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Mensualidad", XT, YT, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XT, YT - 10, 0)

        'INTERES
        XT += 75
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)

        'CAPITAL
        XT += 67
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

        'IVA CAPITAL
        XT += 65
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA Capital", XT, YT, 0)

        'IVA INTERES
        XT += 70
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA Interés", XT, YT, 0)

        'NUMERO DE PAGO
        XT += 63
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COTIZADOR_ARFIN"
        Session("parm") = Session("cmd").CreateParameter("ID_PROD", Session("adVarChar"), Session("adParamInput"), 11, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 30, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 30, unidadPlazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 30, plazo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SUC", Session("adVarChar"), Session("adParamInput"), 11, idSuc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA_ORD", Session("adVarChar"), Session("adParamInput"), 20, tasa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA_MOR", Session("adVarChar"), Session("adParamInput"), 20, tasaMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIA", Session("adVarChar"), Session("adParamInput"), 100, periodo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_PER", Session("adVarChar"), Session("adParamInput"), 20, tipoPeriodo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_INICIO", Session("adVarChar"), Session("adParamInput"), 10, fechaPago)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF
                'COMIENZA LA SEGUNDA HOJA DEL PDF
                If Y < 90 Then
                    Y = 645
                    X = 65
                    cb.EndText()

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")

                    cb = writer.DirectContent
                    Plan_PFSI = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 9)

                    XT = X
                    YT = Y + 35
                    XAux = XT

                    'FECHA DE PAGO
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Fecha", XT, YT, 0)
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "de Pago", XAux, YT - 10, 0)

                    'MENSUALIDAD TOTAL
                    XT += 65

                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Mensualidad", XT, YT, 0)
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Total", XAux + 75, YT - 10, 0)

                    'INTERES
                    XT += 85
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Interés", XT, YT, 0)

                    'CAPITAL
                    XT += 80
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Capital", XT, YT, 0)

                    'IVA RENTA
                    XT += 80
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA Capital", XT, YT, 0)

                    'IVA INTERES
                    XT += 70
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "IVA Interés", XT, YT, 0)

                    '# NUMERO DE PAGO
                    XT += 75
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "# Pago", XT, YT, 0)

                    cb.SetFontAndSize(bf, 8)
                Else
                    Y -= 15
                    X = 65
                End If

                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 8)


                'FECHA PAGO
                Dim fecalt As String
                fecalt = Session("rs").Fields("FECHA_PAGO").Value.ToString
                fecalt = fecalt.Substring(0, 2) + "/" + fecalt.Substring(3, 2) + "/" + fecalt.Substring(6, 4)
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)

                'FECHA RECOMENDADA DE PAGO
                X += 70
                Dim fecaltrec As String
                fecaltrec = Session("rs").Fields("FECHA_REC").Value.ToString
                fecaltrec = fecaltrec.Substring(0, 2) + "/" + fecaltrec.Substring(3, 2) + "/" + fecaltrec.Substring(6, 4)

                If fecaltrec <> fecalt Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecaltrec, X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, fecalt, X, Y, 0)
                End If

                'MENSUALIDAD TOTAL
                X += 100
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("TOTAL").Value.ToString), X, Y, 0)

                'INTERÉS
                X += 75
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("INTERES").Value.ToString), X, Y, 0)

                fecalt = Nothing
                fecaltrec = Nothing

                'CAPITAL
                X += 69
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("CAPITAL").Value.ToString), X, Y, 0)

                'IVA RENTA
                X += 63

                Dim IVAEXENTO As Decimal = Session("rs").Fields("IVA").Value

                If IVAEXENTO = 0.0 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, "------", X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(IVAEXENTO.ToString), X, Y, 0)
                End If

                'IVA INTERES
                X += 68
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("IVA_INTERES").Value.ToString), X, Y, 0)

                'TOTAL
                X += 48
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, Session("rs").Fields("NUM_PAGO").Value.ToString, X, Y, 0)

                Session("rs").movenext()
            Loop
        End If

        X = 65
        Y -= 10
        'Nota del PDF
        'NOTAS
        If Y < 120 Then
            cb.EndText()
            document.NewPage()

            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\PlanPagos\plandepagos.pdf")
            cb = writer.DirectContent
            Plan_PFSI = writer.GetImportedPage(Reader, 1)

            cb.AddTemplate(Plan_PFSI, 1, 0, 0, 1, 0, 0)
            cb.BeginText()

            'Se cambia el tamaño y el tipo de letra para agregar la nota al PDF
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            X = 65
            Y = 650
        Else
            X = 65
            Y -= 15
        End If

        'Nota del PDF
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "NOTAS:", X, Y, 0)
        Y = Y - 15
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* CAT (Costo Anual Total sin IVA). ", X, Y, 0)
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Para fines informativos y de comparación exclusivamente. ", X + 130, Y, 0)
        Y = Y - 15
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "* El importe de IVA está determinado conforme a la tasa vigente establecida en la Ley del Impuesto al Valor Agregado, por lo que", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, " esta puede ser modificada conforme lo establezca la citada Ley.", X, Y, 0)

        Session("Con").Close()
        cb.EndText()
        document.Close()
    End Sub

End Class