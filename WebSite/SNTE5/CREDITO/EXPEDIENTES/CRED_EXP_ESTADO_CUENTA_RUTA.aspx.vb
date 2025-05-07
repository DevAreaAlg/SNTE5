Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports WnvWordToPdf
Imports System.IO
Imports Ionic.Zip

Public Class CRED_EXP_ESTADO_CUENTA_RUTA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Estado de Cuenta", "Estado de Cuenta")

        If Not Me.IsPostBack Then
            LlenarPeriodos()
            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona
            btn_descargar_EdoCuentaAportacion.Enabled = False
            btn_descargar_EdoCuentaPrestamo.Enabled = False
            cmb_sistema.Enabled = False
        End If

        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            validaNumPersona()
        End If

    End Sub

    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        validaNumPersona()
        'btn_descargar_EdoCuentaPrestamo.Enabled = "True"
    End Sub

    Private Sub validaNumPersona()
        lbl_estatus.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_estatus.Text = "Error: RFC incorrecto."
                lbl_info_disp.Text = "Agremiado no encontrado o RFC incorrecto."
                'lbl_NombrePersonaBusqueda.Text = "Agremiado no encontrado o RFC incorrecto."
                div_NombrePersonaBusqueda.Visible = False
                btn_descargar_EdoCuentaAportacion.Enabled = False
                btn_descargar_EdoCuentaPrestamo.Enabled = False
                cmb_sistema.Items.Add("SIN SISTEMA")
                cmb_sistema.Enabled = False
                'div_NombrePersonaBusqueda.Visible = False
            Else
                'Metodo que llena el droplist con los tipos de productos

                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                btn_descargar_EdoCuentaAportacion.Enabled = True
                btn_descargar_EdoCuentaPrestamo.Enabled = True
                div_NombrePersonaBusqueda.Visible = True

            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
        End If
    End Sub

    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            'lbl_alerta.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

        cmb_sistema.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SISTEMAS_X_RFC"
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("SISTEMA").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_sistema.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub cmb_resultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_sistema.SelectedIndexChanged
        If cmb_sistema.SelectedItem.Value = 2 Then
            btn_descargar_EdoCuentaPrestamo.Enabled = "False"
        Else
            btn_descargar_EdoCuentaPrestamo.Enabled = "True"
        End If

        obtieneIdSis()
    End Sub

    Private Sub obtieneIdSis()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SISTEMA", Session("adVarChar"), Session("adParamInput"), 20, cmb_sistema.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC_SISTEMA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            txt_IdCliente.Text = CStr(idp)
            Session("PERSONAID") = txt_IdCliente.Text
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Private Sub BuscarIDCliente()

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
            Session("SALARIO") = Session("rs").fields("SALARIO").value.ToString
            Session("NETO") = Session("rs").fields("NETO").value.ToString
            Session("CAPACIDAD") = Session("rs").fields("CAPACIDAD").value.ToString
            Session("OTROSCRED") = Session("rs").fields("OTROSCRED").value.ToString
            Session("PORPLAZORENO") = Session("rs").fields("PLAZORENO").value.ToString
            Session("NUMCTRL") = Session("rs").Fields("NUMCONT").value.ToString
            Session("NUMINST") = Session("rs").Fields("INSTI").value.ToString
            Session("ANTIGUEDAD") = Session("rs").Fields("ANTIGUEDAD").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_estatus.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""

        ElseIf Existe = -1 Then
            Session("idperbusca") = ""
            lbl_estatus.Text = "Error: No existe el número de control"
            lbl_NombrePersonaBusqueda.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
        Else
            lbl_estatus.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            btn_descargar_EdoCuentaAportacion.Enabled = True
            btn_descargar_EdoCuentaPrestamo.Enabled = True
            cmb_sistema.Enabled = True
            lbl_info_disp.Text = ""
        End If


    End Sub

    ''ESTADO DE CUENTA - APORTACIONES
    Protected Sub btn_descargar_EdoCuentaAportacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_descargar_EdoCuentaAportacion.Click
        Session("RFC_PERSONA") = tbx_rfc.Text
        VER_ESTADOCTA(Session("PERSONAID"), Session("RFC_PERSONA"))
    End Sub

    Private Sub VER_ESTADOCTA(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String)

        EstadoCuentaAportaciones(ID_PERSONA, RFC_PERSONA, "", False)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ESTADO_CUENTA_APORTACIONES(RFC:" + Session("NUMTRAB") + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)

            .End()
            .Flush()
        End With
    End Sub

    Private Sub EstadoCuentaAportaciones(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String, ByVal SISTEMA As String, ByVal EnvioMasivo As Boolean)

        Dim periodo As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty
        Dim clave_CT As String = String.Empty
        Dim municipio As String = String.Empty
        Dim rfc As String = String.Empty
        Dim nombre As String = String.Empty

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            periodo = Session("rs").Fields("PERIODO").Value.ToString()
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
        'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + RFC_PERSONA + "_" + SISTEMA + "_APORTACIONES.pdf", FileMode.Create))
        End If

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
        Y = 695 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer


        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

        Y = Y - 16
        X = 450

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)
        Y = Y - 16
        X = 450

        'Delegación
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
        Y = Y - 15
        X = 450

        'Municipio
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

        Y = Y - 16
        X = 450

        'Región
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

        Y = Y - 16
        X = 450




        Y = 648
        X = 80

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
        Dim ajustePeriodoAnterior As Decimal = 0.00
        Dim saldoAhorro As Decimal = 0.00
        Dim saldoAFavor As Decimal = 0.00
        Dim totalPrestamoComplemenatario As Decimal = 0.00
        Dim InteresesInversiones As Decimal = 0.00
        Dim InteresesPrestamos As Decimal = 0.00



        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ESTADO_CTA"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            prestamosAhorro = Session("rs").Fields("PRESTAMO_AHORRO").Value.ToString()
            interesesAhorro = Session("rs").Fields("INTERESES_AHORRO").Value.ToString()
            totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
            totalDescuento = Session("rs").Fields("TOTAL_DESCUENTOS").Value.ToString()
            pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
            totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
            restoPrestamo = Session("rs").Fields("RESTO_PRESTAMO").Value.ToString()
            saldoAFavor = Session("rs").Fields("SALDO_A_FAVOR").Value.ToString()

            aportacionTrab = Session("rs").Fields("APORTACION_TRAB").Value.ToString()
            aportacionEntidad = Session("rs").Fields("APORTACION_ENTIDAD").Value.ToString()
            InteresesInversiones = Session("rs").Fields("INTERESES_INVERSIONES").Value.ToString()
            InteresesPrestamos = Session("rs").Fields("INTERESES_PRESTAMO").Value.ToString()
            SubTotalAhorro = Session("rs").Fields("SUBTOTAL_AHORRO").Value.ToString()
            prestComplementarios = Session("rs").Fields("PRESTAMOS_COMPLEMENTARIOS").Value.ToString()
            interesesComplementaros = Session("rs").Fields("INTERESES_COMPLEMENTARIOS").Value.ToString()
            totalPrestamoComplemenatario = Session("rs").Fields("TOTAL_PRESTAMO_COMPLEMENTARIOS").Value.ToString()
            ajustePrestamo = Session("rs").Fields("AJUSTE_PRESTAMO").Value.ToString()
            ajustePension = Session("rs").Fields("AJUSTE_PENSION").Value.ToString()
            ajustePeriodoAnterior = Session("rs").Fields("AJUSTE_SALDO_PRESTAMO_ANTERIOR").Value.ToString()
            saldoAhorro = Session("rs").Fields("SALDO_AHORRO").Value.ToString()

        End If
        Session("Con").Close()

        X = 160  'X empieza de izquierda a derecha
        Y = 595 'Y empieza de abajo hacia arriba

        'Prestamo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestamosAhorro), X, Y, 0)


        X = 160
        Y = 575
        'Intereses Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesAhorro), X, Y, 0)

        X = 160
        Y = 556
        'Total Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        X = 160
        Y = 538
        'Total Descuentos 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalDescuento), X, Y, 0)


        X = 160
        Y = 520
        'Pagos Anticipados 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


        X = 160
        Y = 499
        'Total Pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)


        X = 160
        Y = 480
        'Resto Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(restoPrestamo), X, Y, 0)


        X = 160
        Y = 461
        'Saldo a Favor 
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAFavor), X, Y, 0)


        Y = 597
        X = 450
        'Aportaciones Trabajador
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionTrab), X, Y, 0)


        Y = Y - 20
        X = 450
        'Aportaciones Entidad
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionEntidad), X, Y, 0)


        Y = Y - 18
        X = 450
        'Intereses por inversiones
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesInversiones), X, Y, 0)

        Y = Y - 20
        X = 450
        'Intereses por préstamos
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesPrestamos), X, Y, 0)


        Y = Y - 19
        X = 450
        'SubTotal Ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(SubTotalAhorro), X, Y, 0)


        Y = Y - 19
        X = 450
        'Préstamo Complementarios
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestComplementarios), X, Y, 0)


        Y = Y - 19
        X = 450
        'Intereses
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesComplementaros), X, Y, 0)


        Y = Y - 19
        X = 450
        'Total Préstamo Complementario
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamoComplemenatario), X, Y, 0)

        Y = Y - 17
        X = 450
        'Ajuste Préstamo de ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePrestamo), X, Y, 0)


        Y = Y - 20
        X = 450
        'Ajuste Pension 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePension), X, Y, 0)


        Y = Y - 20
        X = 450
        'Ajuste Saldo Préstamos periodo anterior
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePeriodoAnterior), X, Y, 0)


        Y = Y - 26
        X = 450
        'Saldo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAhorro), X, Y, 0)


        Y = Y - 52
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
        Y = 335
        X = 30

        Session("Con").Open()

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
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


                    Y = 746
                    X = 30

                    'XT = X
                    'YT = Y + 35
                Else
                    Y = Y - 10
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
        End If

        Session("Con").Close()
        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuenta.pdf")

        cb = writer.DirectContent
        EstadoCuenta = writer.GetImportedPage(Reader, 2)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        cb.SetFontAndSize(bf, 9)


        Y = 675
        X = 75

        cb.EndText()
        document.Close()

    End Sub

    Private Sub EstadoCuentaAportaciones_2(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String, ByVal SISTEMA As String, ByVal EnvioMasivo As Boolean)

        Dim periodo As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty
        Dim clave_CT As String = String.Empty
        Dim municipio As String = String.Empty
        Dim rfc As String = String.Empty
        Dim nombre As String = String.Empty

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            periodo = Session("rs").Fields("PERIODO").Value.ToString()
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
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaunicoINDV.pdf")
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
        'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + RFC_PERSONA + "_" + SISTEMA + "_APORTACIONES.pdf", FileMode.Create))
        End If

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
        Y = 703 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer


        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

        Y = Y - 16
        X = 450

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)
        Y = Y - 16
        X = 450

        'Delegación
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
        Y = Y - 16
        X = 450

        'Municipio
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

        Y = Y - 17
        X = 450

        'Región
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

        Y = Y - 17
        X = 450




        Y = 648
        X = 80

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
        Dim ajustePeriodoAnterior As Decimal = 0.00
        Dim saldoAhorro As Decimal = 0.00
        Dim saldoAFavor As Decimal = 0.00
        Dim totalPrestamoComplemenatario As Decimal = 0.00
        Dim InteresesInversiones As Decimal = 0.00
        Dim InteresesPrestamos As Decimal = 0.00
        Dim RetroactivoTrabajador As Decimal = 0.00
        Dim RetroactivoEntidad As Decimal = 0.00
        Dim TotalRetroactivo As Decimal = 0.00

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ESTADO_CTA_UNICOS"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            prestamosAhorro = Session("rs").Fields("PRESTAMO_AHORRO").Value.ToString()
            interesesAhorro = Session("rs").Fields("INTERESES_AHORRO").Value.ToString()
            totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
            totalDescuento = Session("rs").Fields("TOTAL_DESCUENTOS").Value.ToString()
            pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
            totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
            restoPrestamo = Session("rs").Fields("RESTO_PRESTAMO").Value.ToString()
            saldoAFavor = Session("rs").Fields("SALDO_A_FAVOR").Value.ToString()

            aportacionTrab = Session("rs").Fields("APORTACION_TRAB").Value.ToString()
            aportacionEntidad = Session("rs").Fields("APORTACION_ENTIDAD").Value.ToString()
            InteresesInversiones = Session("rs").Fields("INTERESES_INVERSIONES").Value.ToString()
            InteresesPrestamos = Session("rs").Fields("INTERESES_PRESTAMO").Value.ToString()
            SubTotalAhorro = Session("rs").Fields("SUBTOTAL_AHORRO").Value.ToString()
            prestComplementarios = Session("rs").Fields("PRESTAMOS_COMPLEMENTARIOS").Value.ToString()
            interesesComplementaros = Session("rs").Fields("INTERESES_COMPLEMENTARIOS").Value.ToString()
            totalPrestamoComplemenatario = Session("rs").Fields("TOTAL_PRESTAMO_COMPLEMENTARIOS").Value.ToString()
            ajustePrestamo = Session("rs").Fields("AJUSTE_PRESTAMO").Value.ToString()
            ajustePension = Session("rs").Fields("AJUSTE_PENSION").Value.ToString()
            ajustePeriodoAnterior = Session("rs").Fields("AJUSTE_SALDO_PRESTAMO_ANTERIOR").Value.ToString()
            saldoAhorro = Session("rs").Fields("SALDO_AHORRO").Value.ToString()

            RetroactivoTrabajador = Session("rs").Fields("RETROACTIVO_TRABAJADOR").Value.ToString()
            RetroactivoEntidad = Session("rs").Fields("RETROACTIVO_ENTIDAD").Value.ToString()
            TotalRetroactivo = Session("rs").Fields("TOTAL_RETROACTIVO").Value.ToString()
        End If
        Session("Con").Close()

        X = 160  'X empieza de izquierda a derecha
        Y = 595 'Y empieza de abajo hacia arriba

        'Prestamo Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestamosAhorro), X, Y, 0)


        X = 160
        Y = 575
        'Intereses Ahorro 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesAhorro), X, Y, 0)

        X = 160
        Y = 556
        'Total Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        X = 160
        Y = 538
        'Total Descuentos 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalDescuento), X, Y, 0)


        X = 160
        Y = 520
        'Pagos Anticipados 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


        X = 160
        Y = 499
        'Total Pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)

        X = 160
        Y = 480
        'Resto Préstamo 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(restoPrestamo), X, Y, 0)


        X = 160
        Y = 461
        'Saldo a Favor 
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAFavor), X, Y, 0)


        Y = 603
        X = 450
        'Aportaciones Trabajador
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionTrab), X, Y, 0)


        Y = Y - 20
        X = 450
        'Aportaciones Entidad
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionEntidad), X, Y, 0)


        Y = Y - 18
        X = 450
        'Intereses por inversiones
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesInversiones), X, Y, 0)

        Y = Y - 20
        X = 450
        'Intereses por préstamos
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesPrestamos), X, Y, 0)


        Y = Y - 19
        X = 450
        'SubTotal Ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(SubTotalAhorro), X, Y, 0)


        Y = Y - 19
        X = 450
        'Préstamo Complementarios
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestComplementarios), X, Y, 0)


        Y = Y - 19
        X = 450
        'Intereses
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesComplementaros), X, Y, 0)


        Y = Y - 19
        X = 450
        'Total Préstamo Complementario
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamoComplemenatario), X, Y, 0)

        Y = Y - 17
        X = 450
        'Ajuste Préstamo de ahorro
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePrestamo), X, Y, 0)


        Y = Y - 20
        X = 450
        'Ajuste Pension 
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePension), X, Y, 0)


        Y = Y - 20
        X = 450
        'Ajuste Saldo Préstamos periodo anterior
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAhorro), X, Y, 0)


        Y = Y - 19
        X = 450
        'Saldo Ahorro 
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(RetroactivoTrabajador), X, Y, 0)


        Y = Y - 19
        X = 450
        'Saldo Ahorro 
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(RetroactivoEntidad), X, Y, 0)

        Y = Y - 20
        X = 450
        'Saldo Ahorro 
        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(TotalRetroactivo), X, Y, 0)


        Y = Y - 52
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
        Y = 320
        X = 30

        Session("Con").Open()

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
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
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaunicoINDV.pdf")

                    cb = writer.DirectContent
                    EstadoCuenta = writer.GetImportedPage(Reader, 2)
                    cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    cb.SetFontAndSize(bf, 9)


                    Y = 746
                    X = 30

                    'XT = X
                    'YT = Y + 35
                Else
                    Y = Y - 10
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
        End If

        Session("Con").Close()
        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaunicoINDV.pdf")

        cb = writer.DirectContent
        EstadoCuenta = writer.GetImportedPage(Reader, 2)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        cb.SetFontAndSize(bf, 9)


        Y = 675
        X = 75

        cb.EndText()
        document.Close()

    End Sub

    ''ESTADO DE CUENTA - PRESTAMOS 

    Protected Sub btn_descargar_EdoCuentaPrestamo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_descargar_EdoCuentaPrestamo.Click
        Session("RFC_PERSONA") = tbx_rfc.Text
        VER_ESTADOCTA_PRESTAMOS(Session("PERSONAID"), Session("RFC_PERSONA"))
    End Sub

    Private Sub VER_ESTADOCTA_PRESTAMOS(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String)

        EstadoCuentaPrestamos(ID_PERSONA, RFC_PERSONA, "", False)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ESTADO_CUENTA_PRESTAMOS(RFC:" + Session("NUMTRAB") + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With
    End Sub

    Private Sub EstadoCuentaPrestamos(ByVal ID_PERSONA As String, ByVal RFC_PERSONA As String, ByVal SISTEMAS As String, ByVal EnvioMasivo As Boolean)

        Dim periodo As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty
        Dim clave_CT As String = String.Empty
        Dim municipio As String = String.Empty
        Dim rfc As String = String.Empty
        Dim nombre As String = String.Empty


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            periodo = Session("rs").Fields("PERIODO").Value.ToString()
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
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")
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

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + RFC_PERSONA + "_" + SISTEMAS + "_PRESTAMOS.pdf", FileMode.Create))
        End If


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

        X = 435  'X empieza de izquierda a derecha
        Y = 695 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

        Y = Y - 15
        X = 435

        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)

        Y = Y - 15
        X = 435

        'Delegación
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
        Y = Y - 15
        X = 435

        'Municipio
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

        Y = Y - 15
        X = 435

        'Región
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

        Y = Y - 15
        X = 435




        Y = 648
        X = 80

        'Nombre
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, nombre.ToString, X, Y, 0)

        Y = Y - 15
        X = 65


        Dim producto As String = String.Empty
        Dim fechaPrestamo As String = String.Empty
        Dim plazo As String = String.Empty
        Dim autorizado As Decimal = 0.00
        Dim interes As Decimal = 0.00
        Dim totalPrestamo As Decimal = 0.00
        Dim pagosAnticipados As Decimal = 0.00
        Dim totalPagado As Decimal = 0.00
        Dim resto As Decimal = 0.00
        Dim bonificacion As Decimal = 0.00


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADOCTA_PRESTAMOS"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then


            producto = Session("rs").Fields("PRODUCTO").Value.ToString()
            fechaPrestamo = Session("rs").Fields("FECHA_PRESTAMO").Value.ToString()
            plazo = Session("rs").Fields("PLAZO").Value.ToString()
            autorizado = Session("rs").Fields("AUTORIZADO").Value.ToString()
            interes = Session("rs").Fields("INTERES").Value.ToString()
            totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
            pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
            totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
            resto = Session("rs").Fields("RESTO").Value.ToString()
            bonificacion = Session("rs").Fields("BONIFICACION").Value.ToString()

        End If
        Session("Con").Close()


        X = 435  'X empieza de izquierda a derecha
        Y = 601 'Y empieza de abajo hacia arriba


        'Producto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, producto, X, Y, 0)


        Y = 587
        X = 435
        'Fecha Préstamo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, fechaPrestamo, X, Y, 0)


        Y = Y - 15
        X = 435
        'Plazo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, plazo, X, Y, 0)


        Y = Y - 15
        X = 435
        'Autorizado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(autorizado), X, Y, 0)

        Y = Y - 14
        X = 435
        'Intereses
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interes), X, Y, 0)


        Y = Y - 17
        X = 435
        'Total Préstamo
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        Y = Y - 391
        X = 462
        'Total Préstamo 2
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


        Y = Y - 20
        X = 462
        'Pagos Anticipados
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


        Y = Y - 18
        X = 462
        'Total pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(bonificacion), X, Y, 0)


        Y = Y - 19
        X = 462
        'Total pagado
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)


        Y = Y - 18
        X = 462
        'Resto
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(resto), X, Y, 0)


        Y = 480
        X = 40

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)


        Session("Con").Open()

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA_PRESTAMOS"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
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
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")

                    cb = writer.DirectContent
                    EstadoCuenta = writer.GetImportedPage(Reader, 1)
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

                X = 90
                'QUINCENA
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("QUINCENA").Value.ToString, X, Y, 0)

                'AÑO
                X = X + 120
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("ANIO").Value.ToString, X, Y, 0)

                'DESCUENTO 
                X = X + 138
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("DESCUENTO").Value.ToString), X, Y, 0)

                'SALDO
                X = X + 175
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SALDO").Value.ToString), X, Y, 0)

                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()

        'GLOSARIO TERMINOS
        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")

        cb = writer.DirectContent
        EstadoCuenta = writer.GetImportedPage(Reader, 2)
        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        'Se cambia el tamaño y el tipo de letra para agregar la nota al PDF
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = 65
        Y = 675


        cb.EndText()
        document.Close()


    End Sub

    Protected Sub btn_generar_estado_prest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_generar_estado_prest.Click
        Try
            CrearCarpetas("PRESTAMOS")

            Dim dtLog As DataTable = New DataTable()
            dtLog.Columns.Add("RFC", Type.GetType("System.String"))
            dtLog.Columns.Add("REGION", Type.GetType("System.String"))
            dtLog.Columns.Add("DELEGACION", Type.GetType("System.String"))
            dtLog.Columns.Add("TIPO_EDO_CTA", Type.GetType("System.String"))
            dtLog.Columns.Add("SISTEMA", Type.GetType("System.String"))

            'Generación de edos de cta de Prestamos.
            Dim da As New DataAccess()
            Dim ds As DataSet = da.RegresaDataSet("SEL_ESTADOS_CTA_PRESTAMOS_MASIVOS")

            If ds.Tables(0).Rows().Count() > 0 Then

                Dim drLogEncPrest As DataRow = dtLog.NewRow()
                drLogEncPrest("RFC") = "RFC"
                drLogEncPrest("REGION") = "REGION"
                drLogEncPrest("DELEGACION") = "DELEGACION"
                drLogEncPrest("TIPO_EDO_CTA") = "TIPO ESTADO CUENTA"
                drLogEncPrest("SISTEMA") = "SISTEMA"
                dtLog.Rows.Add(drLogEncPrest)

                For iDatos As Integer = 0 To ds.Tables(0).Rows().Count() - 1
                    Dim RFC As String = ds.Tables(0).Rows(iDatos).Item("RFC").ToString()
                    Dim REGION As String = ds.Tables(0).Rows(iDatos).Item("REGION").ToString()
                    Dim DELEGACION As String = ds.Tables(0).Rows(iDatos).Item("DELEGACION").ToString()
                    Dim SISTEMA As String = ds.Tables(0).Rows(iDatos).Item("SISTEMA").ToString()
                    Dim ID_PERSONA As String = ds.Tables(0).Rows(iDatos).Item("ID_PERSONA").ToString()

                    'Invocar método para la generación del edo de cta de prestamos
                    EstadoCuentaPrestamos(ID_PERSONA, RFC, SISTEMA, True)

                    Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + cmb_periodo.SelectedValue.ToString + "\PRESTAMOS\" + REGION + "\" + DELEGACION + "\"

                    'CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\Prestamos.pdf", PathDestino, True, RFC, "PRESTAMOS")
                    CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + RFC + "_" + SISTEMA + "_PRESTAMOS.pdf", PathDestino, True, RFC, SISTEMA, "PRESTAMOS")

                    Dim drLog As DataRow = dtLog.NewRow()
                    drLog("RFC") = RFC
                    drLog("REGION") = REGION
                    drLog("DELEGACION") = DELEGACION
                    drLog("TIPO_EDO_CTA") = "PRESTAMOS"
                    drLog("SISTEMA") = SISTEMA
                    dtLog.Rows.Add(drLog)
                Next
            End If


            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@LOG_ESTADO_CTA", dtLog)
            Hsh.Add("@PERIODO", cmb_periodo.SelectedItem.ToString())
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))

            da.GuardarActualizar("INS_LOG_ESTADOCUENTA", Hsh)


            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtLog.Rows

                For i = 0 To dtLog.Columns.Count - 1

                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")

                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + cmb_periodo.SelectedItem.ToString + " - " + "ESTADO CUENTA PRESTAMOS" + ".csv")
            context.Response.End()


        Catch ex As Exception
            'lbl_estatus.Text = "Se han generado correctamente los Estados de Cuenta - Préstamos"
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub


    Protected Sub btn_generar_estado_aport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_generar_estado_aport.Click
        Try
            CrearCarpetas("APORTACIONES")

            Dim dtLog As DataTable = New DataTable()
            dtLog.Columns.Add("RFC", Type.GetType("System.String"))
            dtLog.Columns.Add("REGION", Type.GetType("System.String"))
            dtLog.Columns.Add("DELEGACION", Type.GetType("System.String"))
            dtLog.Columns.Add("TIPO_EDO_CTA", Type.GetType("System.String"))
            dtLog.Columns.Add("SISTEMA", Type.GetType("System.String"))


            'Generación de edos de cta de Aportaciones.
            Dim da As New DataAccess()
            Dim dsAportaciones As DataSet = da.RegresaDataSet("SEL_ESTADOS_CTA_APORTACIONES_MASIVOS")

            If dsAportaciones.Tables(0).Rows().Count() > 0 Then

                Dim drLogEncAport As DataRow = dtLog.NewRow()
                drLogEncAport("RFC") = "RFC"
                drLogEncAport("REGION") = "REGION"
                drLogEncAport("DELEGACION") = "DELEGACION"
                drLogEncAport("TIPO_EDO_CTA") = "TIPO ESTADO CUENTA"
                drLogEncAport("SISTEMA") = "SISTEMA"

                dtLog.Rows.Add(drLogEncAport)

                For iDatosAport As Integer = 0 To dsAportaciones.Tables(0).Rows().Count() - 1
                    Dim RFC As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("RFC").ToString()
                    Dim REGION As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("REGION").ToString()
                    Dim DELEGACION As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("DELEGACION").ToString()
                    Dim SISTEMA As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("SISTEMA").ToString()
                    Dim IDPERSONA As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("ID_PERSONA").ToString()

                    'Inviocar método para la generación del edo de cta de aportaciones
                    'Plantilla vieja
                    'EstadoCuentaAportaciones(IDPERSONA, RFC, SISTEMA, True)

                    EstadoCuentaAportaciones_2(IDPERSONA, RFC, SISTEMA, True)

                    Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + cmb_periodo.SelectedValue.ToString + "\APORTACIONES\" + REGION + "\" + DELEGACION + "\"

                    'CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\Prestamos.pdf", PathDestino, True, RFC, "PRESTAMOS")
                    CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + RFC + "_" + SISTEMA + "_APORTACIONES.pdf", PathDestino, True, RFC, SISTEMA, "APORTACIONES")

                    Dim drLog As DataRow = dtLog.NewRow()
                    drLog("RFC") = RFC
                    drLog("REGION") = REGION
                    drLog("DELEGACION") = DELEGACION
                    drLog("TIPO_EDO_CTA") = "APORTACIONES"
                    drLog("SISTEMA") = SISTEMA
                    dtLog.Rows.Add(drLog)
                Next

            End If

            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@LOG_ESTADO_CTA", dtLog)
            Hsh.Add("@PERIODO", cmb_periodo.SelectedItem.ToString())
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))

            da.GuardarActualizar("INS_LOG_ESTADOCUENTA", Hsh)


            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtLog.Rows

                For i = 0 To dtLog.Columns.Count - 1

                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")

                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + cmb_periodo.SelectedItem.ToString + " - " + "ESTADO CUENTA APORTACIONES" + ".csv")
            context.Response.End()



        Catch ex As Exception
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

    Private Sub LlenarPeriodos()
        cmb_periodo.Items.Clear()
        'Dim elija As New ListItem("ELIJA", "-1")
        'cmb_periodo.Items.Add(elija)


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERIODO_ESTADO_CUENTA"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").Value.ToString, Session("rs").Fields("VALOR").Value.ToString)
            cmb_periodo.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub CrearCarpetas(TIPO As String)
        Dim urlestadosdecuenta As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta")

        If Not System.IO.Directory.Exists(urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + TIPO) Then
            My.Computer.FileSystem.CreateDirectory(urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + TIPO)
        Else
            My.Computer.FileSystem.DeleteDirectory(urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + TIPO, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If

        Dim da As New DataAccess()
        Dim dsRegiones As DataSet = da.RegresaDataSet("SEL_REGIONES_PARA_CARPETAS")

        If dsRegiones.Tables(0).Rows().Count() > 0 Then
            For iDatosAport As Integer = 0 To dsRegiones.Tables(0).Rows().Count() - 1
                Dim REGION As String = dsRegiones.Tables(0).Rows(iDatosAport).Item("ID_REGION").ToString()
                Dim DELEGACION As String = dsRegiones.Tables(0).Rows(iDatosAport).Item("DELEGACION").ToString()

                My.Computer.FileSystem.CreateDirectory(urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + TIPO + "\" + REGION + "\" + DELEGACION)
            Next
        End If
        '-------------------
        'Session("cmd") = New ADODB.Command()
        'Session("Con").Open()
        'Session("cmd").ActiveConnection = Session("Con")
        'Session("cmd").CommandType = CommandType.StoredProcedure
        'Session("cmd").CommandText = "SEL_REGIONES_PARA_CARPETAS"
        'Session("rs") = Session("cmd").Execute()

        'Do While Not Session("rs").EOF
        '    Dim item As New ListItem(Session("rs").Fields("ID_REGION").Value.ToString)
        '    My.Computer.FileSystem.CreateDirectory(urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + item.Value + "\" + Session("rs").Fields("DELEGACION").Value.ToString)
        '    Session("rs").movenext()
        'Loop
        'Session("Con").Close()
        '--------------------
    End Sub

    Public Sub CopiarArchivo(ByVal origPath As String, ByVal destPath As String, ByVal overwrite As Boolean, ByVal RFC As String, ByVal SISTEMA As String, ByVal Tipo As String)
        Try
            If Not System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(destPath)) Then
                CreaCarpeta(System.IO.Path.GetDirectoryName(destPath))
            End If

            If System.IO.Path.GetExtension(destPath) = "" Then
                If Tipo = "PRESTAMOS" Then
                    'destPath = System.IO.Path.Combine(destPath, "Prestamos.pdf")
                    destPath = System.IO.Path.Combine(destPath, RFC + "_" + SISTEMA + "_PRESTAMOS.pdf")
                ElseIf Tipo = "APORTACIONES" Then
                    destPath = System.IO.Path.Combine(destPath, RFC + "_" + SISTEMA + "_APORTACIONES.pdf")
                End If
            End If

            If Not System.IO.File.Exists(destPath) Then
                System.IO.File.Copy(origPath, destPath, True)
                InsertaRuta(RFC, destPath, Tipo, SISTEMA)
                System.IO.File.Delete(origPath)
            Else

                If overwrite = True Then
                    EliminaArchivo(destPath)
                    System.IO.File.Copy(origPath, destPath, True)
                    InsertaRuta(RFC, destPath, Tipo, SISTEMA)
                    System.IO.File.Delete(origPath)
                End If
            End If




        Catch __unusedException1__ As Exception
            Throw
        End Try
    End Sub

    Public Sub InsertaRuta(ByVal RFC As String, ByVal fullPath As String, ByVal tipo As String, ByVal sitema As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_RUTA_TABLA_FINAL"
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, RFC)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RUTA", Session("adVarChar"), Session("adParamInput"), 200, fullPath)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 20, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SISTEMA", Session("adVarChar"), Session("adParamInput"), 200, sitema)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CICLO", Session("adVarChar"), Session("adParamInput"), 20, "2020-2021")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub
    Public Shared Sub EliminaArchivo(ByVal fullPath As String)
        If System.IO.File.Exists(fullPath) Then
            Dim info As System.IO.FileInfo = New System.IO.FileInfo(fullPath)
            info.Attributes = System.IO.FileAttributes.Normal
            System.IO.File.Delete(fullPath)
        End If
    End Sub

    Public Shared Sub EliminaCarpeta(ByVal fullPath As String)
        If System.IO.Directory.Exists(fullPath) Then
            Dim info As System.IO.FileInfo = New System.IO.FileInfo(fullPath)
            info.Attributes = System.IO.FileAttributes.Normal
            System.IO.File.Delete(fullPath)
        End If
    End Sub

    Public Shared Sub CreaCarpeta(ByVal fullPath As String)
        If Not System.IO.Directory.Exists(fullPath) Then
            System.IO.Directory.CreateDirectory(fullPath)
        End If
    End Sub

    Protected Sub btn_ComprimirEdosCuentaNoEnviados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ComprimirEdosCuentaNoEnviados.Click
        Dim urlestadosdecuenta As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\"
        Dim da As New DataAccess()
        Dim dscomprimir As DataSet = da.RegresaDataSet("SEL_BUSCAR_RUTAS_PARA_COMPRIMIR")

        If dscomprimir.Tables(0).Rows().Count() > 0 Then
            For iDatosAport As Integer = 0 To dscomprimir.Tables(0).Rows().Count() - 1
                'Dim RFC As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("ANTIGU_RFC").ToString()
                Dim REGION As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("ID_REGION").ToString()
                Dim DELEGACION As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("DELEGACION").ToString()
                Dim HASHDELEGACION As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("HASHDELEGACION").ToString()
                '        '        'Dim SISTEMA As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("SISTEMA").ToString()
                Dim TIPO As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("TIPO").ToString()
                '        '        'Dim IDPERSONA As String = dscomprimir.Tables(0).Rows(iDatosAport).Item("ID_PERSONA").ToString()
                Dim rutaAntigua As String = urlestadosdecuenta + dscomprimir.Tables(0).Rows(iDatosAport).Item("RUTA_ANTIGUA").ToString()
                Dim rutaNueva As String = urlestadosdecuenta + dscomprimir.Tables(0).Rows(iDatosAport).Item("RUTA_NUEVA").ToString()
                'Dim carpetaAntigua As String = urlestadosdecuenta + dscomprimir.Tables(0).Rows(iDatosAport).Item("ANTIGU_CARPETA").ToString()
                Dim carpetaNueva As String = urlestadosdecuenta + dscomprimir.Tables(0).Rows(iDatosAport).Item("RUTA_CARPETA_NUEVA").ToString()

                Dim nombreArchivo As String = HASHDELEGACION + ".zip"
                If System.IO.Directory.Exists(rutaAntigua) Then
                    If Not System.IO.Directory.Exists(carpetaNueva) Then
                        System.IO.Directory.CreateDirectory(carpetaNueva)
                    End If

                    If Not System.IO.File.Exists(rutaNueva) Then
                        'System.IO.File.Copy(rutaAntigua, rutaNueva, True)
                        Using zip As ZipFile = New ZipFile()
                            zip.AddDirectory(rutaAntigua, nombreArchivo)
                            zip.Save(rutaNueva)
                        End Using
                    End If

                End If
            Next
        End If

    End Sub

    Public Function ByteArrayToString(ByVal ba() As Byte) As String
        Dim hex As New StringBuilder(ba.Length * 2)
        For Each b As Byte In ba
            hex.AppendFormat("{0:x2}", b)
        Next
        Return hex.ToString()
    End Function

    Private Sub btn_generar_estados_pres_pensiones_Click(sender As Object, e As EventArgs) Handles btn_generar_estados_pres_pensiones.Click
        Try
            CrearCarpetas("PRESTAMOS")

            Dim dtLog As DataTable = New DataTable()
            dtLog.Columns.Add("RFC", Type.GetType("System.String"))
            dtLog.Columns.Add("REGION", Type.GetType("System.String"))
            dtLog.Columns.Add("DELEGACION", Type.GetType("System.String"))
            dtLog.Columns.Add("TIPO_EDO_CTA", Type.GetType("System.String"))
            dtLog.Columns.Add("SISTEMA", Type.GetType("System.String"))

            'Generación de edos de cta de Prestamos.
            Dim da As New DataAccess()
            Dim ds As DataSet = da.RegresaDataSet("SEL_ESTADOS_CTA_PRESTAMOS_PENSIONES")

            If ds.Tables(0).Rows().Count() > 0 Then

                Dim drLogEncPrest As DataRow = dtLog.NewRow()
                drLogEncPrest("RFC") = "RFC"
                drLogEncPrest("REGION") = "REGION"
                drLogEncPrest("DELEGACION") = "DELEGACION"
                drLogEncPrest("TIPO_EDO_CTA") = "TIPO ESTADO CUENTA"
                drLogEncPrest("SISTEMA") = "SISTEMA"
                dtLog.Rows.Add(drLogEncPrest)

                For iDatos As Integer = 0 To ds.Tables(0).Rows().Count() - 1
                    Dim RFC As String = ds.Tables(0).Rows(iDatos).Item("RFC").ToString()
                    Dim REGION As String = ds.Tables(0).Rows(iDatos).Item("REGION").ToString()
                    Dim DELEGACION As String = ds.Tables(0).Rows(iDatos).Item("DELEGACION").ToString()
                    Dim SISTEMA As String = ds.Tables(0).Rows(iDatos).Item("SISTEMA").ToString()
                    Dim ID_PERSONA As String = ds.Tables(0).Rows(iDatos).Item("ID_PERSONA").ToString()

                    'Invocar método para la generación del edo de cta de prestamos
                    EstadoCuentaPrestamos(ID_PERSONA, RFC, SISTEMA, True)

                    Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + cmb_periodo.SelectedValue.ToString + "\PRESTAMOS\" + REGION + "\" + DELEGACION + "\"

                    'CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\Prestamos.pdf", PathDestino, True, RFC, "PRESTAMOS")
                    CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + RFC + "_" + SISTEMA + "_PRESTAMOS.pdf", PathDestino, True, RFC, SISTEMA, "PRESTAMOS")

                    Dim drLog As DataRow = dtLog.NewRow()
                    drLog("RFC") = RFC
                    drLog("REGION") = REGION
                    drLog("DELEGACION") = DELEGACION
                    drLog("TIPO_EDO_CTA") = "PRESTAMOS"
                    drLog("SISTEMA") = SISTEMA
                    dtLog.Rows.Add(drLog)
                Next
            End If


            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@LOG_ESTADO_CTA", dtLog)
            Hsh.Add("@PERIODO", cmb_periodo.SelectedItem.ToString())
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))

            da.GuardarActualizar("INS_LOG_ESTADOCUENTA", Hsh)


            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtLog.Rows

                For i = 0 To dtLog.Columns.Count - 1

                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")

                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + cmb_periodo.SelectedItem.ToString + " - " + "ESTADO CUENTA PRESTAMOS" + ".csv")
            context.Response.End()


        Catch ex As Exception
            'lbl_estatus.Text = "Se han generado correctamente los Estados de Cuenta - Préstamos"
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

    Private Sub btn_generar_estados_aport_pensiones_Click(sender As Object, e As EventArgs) Handles btn_generar_estados_aport_pensiones.Click
        Try
            CrearCarpetas("APORTACIONES")

            Dim dtLog As DataTable = New DataTable()
            dtLog.Columns.Add("RFC", Type.GetType("System.String"))
            dtLog.Columns.Add("REGION", Type.GetType("System.String"))
            dtLog.Columns.Add("DELEGACION", Type.GetType("System.String"))
            dtLog.Columns.Add("TIPO_EDO_CTA", Type.GetType("System.String"))
            dtLog.Columns.Add("SISTEMA", Type.GetType("System.String"))


            'Generación de edos de cta de Aportaciones.
            Dim da As New DataAccess()
            Dim dsAportaciones As DataSet = da.RegresaDataSet("SEL_ESTADOS_CTA_APORTACIONES_PENSIONES")

            If dsAportaciones.Tables(0).Rows().Count() > 0 Then

                Dim drLogEncAport As DataRow = dtLog.NewRow()
                drLogEncAport("RFC") = "RFC"
                drLogEncAport("REGION") = "REGION"
                drLogEncAport("DELEGACION") = "DELEGACION"
                drLogEncAport("TIPO_EDO_CTA") = "TIPO ESTADO CUENTA"
                drLogEncAport("SISTEMA") = "SISTEMA"

                dtLog.Rows.Add(drLogEncAport)

                For iDatosAport As Integer = 0 To dsAportaciones.Tables(0).Rows().Count() - 1
                    Dim RFC As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("RFC").ToString()
                    Dim REGION As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("REGION").ToString()
                    Dim DELEGACION As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("DELEGACION").ToString()
                    Dim SISTEMA As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("SISTEMA").ToString()
                    Dim IDPERSONA As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("ID_PERSONA").ToString()

                    'Inviocar método para la generación del edo de cta de aportaciones
                    EstadoCuentaAportaciones(IDPERSONA, RFC, SISTEMA, True)

                    Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + cmb_periodo.SelectedValue.ToString + "\APORTACIONES\" + REGION + "\" + DELEGACION + "\"

                    'CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\Prestamos.pdf", PathDestino, True, RFC, "PRESTAMOS")
                    CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + RFC + "_" + SISTEMA + "_APORTACIONES.pdf", PathDestino, True, RFC, SISTEMA, "APORTACIONES")

                    Dim drLog As DataRow = dtLog.NewRow()
                    drLog("RFC") = RFC
                    drLog("REGION") = REGION
                    drLog("DELEGACION") = DELEGACION
                    drLog("TIPO_EDO_CTA") = "APORTACIONES"
                    drLog("SISTEMA") = SISTEMA
                    dtLog.Rows.Add(drLog)
                Next

            End If

            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@LOG_ESTADO_CTA", dtLog)
            Hsh.Add("@PERIODO", cmb_periodo.SelectedItem.ToString())
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))

            da.GuardarActualizar("INS_LOG_ESTADOCUENTA", Hsh)


            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtLog.Rows

                For i = 0 To dtLog.Columns.Count - 1

                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")

                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + cmb_periodo.SelectedItem.ToString + " - " + "ESTADO CUENTA APORTACIONES" + ".csv")
            context.Response.End()



        Catch ex As Exception
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

    Private Sub btn_genera_estados_cuenta_unico_Click(sender As Object, e As EventArgs) Handles btn_genera_estados_cuenta_unico.Click
        Try
            CrearCarpetas("APORTACIONES")

            'Generación de edos de cta de Aportaciones.
            Dim da As New DataAccess()
            Dim dsAportaciones As DataSet = da.RegresaDataSet("SEL_ESTADOS_CTA_APORTACIONES_MASIVOS_UNICO")
            Dim DetalleAport As DataTable = dsAportaciones.Tables(0)
            Dim DetalleCCT As DataTable = dsAportaciones.Tables(2)
            If dsAportaciones.Tables(0).Rows().Count() > 0 Then

                For iDatosAport As Integer = 0 To dsAportaciones.Tables(1).Rows().Count() - 1

                    Dim REGION_BANDERA As Integer = dsAportaciones.Tables(1).Rows(iDatosAport).Item("REGION").ToString()
                    Dim DELEGACION_BANDERA As String = dsAportaciones.Tables(1).Rows(iDatosAport).Item("DELEGACION").ToString()

                    Dim strWhere As String = "REGION=" + CStr(REGION_BANDERA) + " AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'"
                    Dim TABLAAUX As DataRow() = DetalleAport.[Select](strWhere)
                    Dim numero As Integer = DetalleAport.[Select](strWhere).Count()
                    ''Se toman valores por cada registros
                    Dim RFC As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("RFC").ToString()
                    Dim REGION As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("REGION").ToString()
                    Dim DELEGACION As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("DELEGACION").ToString()
                    Dim SISTEMA As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("SISTEMA").ToString()
                    Dim IDPERSONA As String = dsAportaciones.Tables(0).Rows(iDatosAport).Item("ID_PERSONA").ToString()

                    If DELEGACION_BANDERA <> "D-III-2" Then
                        'Inviocar método para la generación del edo de cta de aportaciones
                        EstadoCuentaAportaciones_Unico(True, TABLAAUX, CStr(REGION_BANDERA), DELEGACION_BANDERA, numero)

                        Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + cmb_periodo.SelectedValue.ToString + "\APORTACIONES\" + CStr(REGION_BANDERA) + "\" + DELEGACION_BANDERA + "\"

                        'CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\Prestamos.pdf", PathDestino, True, RFC, "PRESTAMOS")
                        CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + CStr(REGION_BANDERA) + "_" + DELEGACION_BANDERA + "_APORTACIONES.pdf", PathDestino, True, CStr(REGION_BANDERA), DELEGACION_BANDERA, "APORTACIONES")
                    Else
                        For idatos As Integer = 0 To DetalleCCT.Rows.Count() - 1
                            Dim BANDERA_CCT As String = DetalleCCT.Rows(idatos).Item("CCT").ToString()

                            Dim strWhere2 As String = "REGION=" + CStr(REGION_BANDERA) + " AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'" + " AND " + "CCT= '" + BANDERA_CCT + "'"
                            Dim TABLAAUX2 As DataRow() = DetalleAport.[Select](strWhere2)
                            Dim numero2 As Integer = DetalleAport.[Select](strWhere2).Count()
                            If numero2 > 0 Then
                                Dim urlestadosdecuenta As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta")
                                Dim ruta As String = urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + "APORTACIONES" + "\" + CStr(REGION_BANDERA) + "\" + DELEGACION_BANDERA + "\" + BANDERA_CCT
                                If Not System.IO.Directory.Exists(ruta) Then
                                    My.Computer.FileSystem.CreateDirectory(ruta)
                                End If

                                EstadoCuentaAportaciones_Unico(True, TABLAAUX2, CStr(REGION_BANDERA), DELEGACION_BANDERA + "_" + BANDERA_CCT, numero2)
                                CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + CStr(REGION_BANDERA) + "_" + DELEGACION_BANDERA + "_" + BANDERA_CCT + "_APORTACIONES.pdf", ruta, True, CStr(REGION_BANDERA), DELEGACION_BANDERA + "_" + BANDERA_CCT, "APORTACIONES")

                            End If
                        Next
                    End If
                Next

            End If
        Catch ex As Exception
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

    Private Sub EstadoCuentaAportaciones_Unico(ByVal EnvioMasivo As Boolean, ByVal tabla As DataRow(), ByVal reg As String, ByVal del As String, ByVal numeroreg As Integer)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaunico.pdf")
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
        'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + reg + "_" + del + "_APORTACIONES.pdf", FileMode.Create))
        End If

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim EstadoCuenta As iTextSharp.text.pdf.PdfImportedPage

        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single

        Dim contador As Integer = 0

        For Each Detalle As DataRow In tabla

            contador += 1
            Dim ID_PERSONA As Integer = Detalle("ID_PERSONA").ToString
            Dim RFC_PERSONA As String = Detalle("RFC").ToString
            Dim SISTEMA As String = Detalle("SISTEMA").ToString

            Dim periodo As String = String.Empty
            Dim region As String = String.Empty
            Dim delegacion As String = String.Empty
            Dim clave_CT As String = String.Empty
            Dim municipio As String = String.Empty
            Dim rfc As String = String.Empty
            Dim nombre As String = String.Empty

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                periodo = Session("rs").Fields("PERIODO").Value.ToString()
                region = Session("rs").Fields("REGION").Value.ToString()
                delegacion = Session("rs").Fields("DELEGACION").Value.ToString()
                clave_CT = Session("rs").Fields("CLAVE_CT").Value.ToString()
                municipio = Session("rs").Fields("MUNICIPIO").Value.ToString()
                rfc = Session("rs").Fields("RFC").Value.ToString()
                nombre = Session("rs").Fields("NOMBRE").Value.ToString()


            End If
            Session("Con").Close()


            EstadoCuenta = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)

            'ready to draw text
            cb.BeginText()

            Dim distanciaHorizontal As Integer = 240
            Dim distanciaVertical As Integer = 15

            X = 450  'X empieza de izquierda a derecha
            Y = 703 'Y empieza de abajo hacia arriba


            Dim XOrdena As Integer
            Dim YOrdena As Integer

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

            Y = Y - 16
            X = 450

            'RFC
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)
            Y = Y - 16
            X = 450

            'Delegación
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
            Y = Y - 16
            X = 450

            'Municipio
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

            Y = Y - 17
            X = 450

            'Región
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

            Y = Y - 17
            X = 450




            Y = 648
            X = 80

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
            Dim ajustePeriodoAnterior As Decimal = 0.00
            Dim saldoAhorro As Decimal = 0.00
            Dim saldoAFavor As Decimal = 0.00
            Dim totalPrestamoComplemenatario As Decimal = 0.00
            Dim InteresesInversiones As Decimal = 0.00
            Dim InteresesPrestamos As Decimal = 0.00

            Dim RetroactivoTrabajador As Decimal = 0.00
            Dim RetroactivoEntidad As Decimal = 0.00
            Dim TotalRetroactivo As Decimal = 0.00



            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ESTADO_CTA_UNICOS"
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then

                prestamosAhorro = Session("rs").Fields("PRESTAMO_AHORRO").Value.ToString()
                interesesAhorro = Session("rs").Fields("INTERESES_AHORRO").Value.ToString()
                totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
                totalDescuento = Session("rs").Fields("TOTAL_DESCUENTOS").Value.ToString()
                pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
                totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
                restoPrestamo = Session("rs").Fields("RESTO_PRESTAMO").Value.ToString()
                saldoAFavor = Session("rs").Fields("SALDO_A_FAVOR").Value.ToString()

                aportacionTrab = Session("rs").Fields("APORTACION_TRAB").Value.ToString()
                aportacionEntidad = Session("rs").Fields("APORTACION_ENTIDAD").Value.ToString()
                InteresesInversiones = Session("rs").Fields("INTERESES_INVERSIONES").Value.ToString()
                InteresesPrestamos = Session("rs").Fields("INTERESES_PRESTAMO").Value.ToString()
                SubTotalAhorro = Session("rs").Fields("SUBTOTAL_AHORRO").Value.ToString()
                prestComplementarios = Session("rs").Fields("PRESTAMOS_COMPLEMENTARIOS").Value.ToString()
                interesesComplementaros = Session("rs").Fields("INTERESES_COMPLEMENTARIOS").Value.ToString()
                totalPrestamoComplemenatario = Session("rs").Fields("TOTAL_PRESTAMO_COMPLEMENTARIOS").Value.ToString()
                ajustePrestamo = Session("rs").Fields("AJUSTE_PRESTAMO").Value.ToString()
                ajustePension = Session("rs").Fields("AJUSTE_PENSION").Value.ToString()
                ajustePeriodoAnterior = Session("rs").Fields("AJUSTE_SALDO_PRESTAMO_ANTERIOR").Value.ToString()
                saldoAhorro = Session("rs").Fields("SALDO_AHORRO").Value.ToString()

                RetroactivoTrabajador = Session("rs").Fields("RETROACTIVO_TRABAJADOR").Value.ToString()
                RetroactivoEntidad = Session("rs").Fields("RETROACTIVO_ENTIDAD").Value.ToString()
                TotalRetroactivo = Session("rs").Fields("TOTAL_RETROACTIVO").Value.ToString()

            End If
            Session("Con").Close()

            X = 160  'X empieza de izquierda a derecha
            Y = 595 'Y empieza de abajo hacia arriba

            'Prestamo Ahorro 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestamosAhorro), X, Y, 0)


            X = 160
            Y = 575
            'Intereses Ahorro 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesAhorro), X, Y, 0)

            X = 160
            Y = 556
            'Total Préstamo 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


            X = 160
            Y = 538
            'Total Descuentos 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalDescuento), X, Y, 0)


            X = 160
            Y = 520
            'Pagos Anticipados 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


            X = 160
            Y = 499
            'Total Pagado
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)

            X = 160
            Y = 480
            'Resto Préstamo 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(restoPrestamo), X, Y, 0)


            X = 160
            Y = 461
            'Saldo a Favor 
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAFavor), X, Y, 0)


            Y = 603
            X = 450
            'Aportaciones Trabajador
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionTrab), X, Y, 0)


            Y = Y - 20
            X = 450
            'Aportaciones Entidad
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(aportacionEntidad), X, Y, 0)


            Y = Y - 18
            X = 450
            'Intereses por inversiones
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesInversiones), X, Y, 0)

            Y = Y - 20
            X = 450
            'Intereses por préstamos
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(InteresesPrestamos), X, Y, 0)


            Y = Y - 19
            X = 450
            'SubTotal Ahorro
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(SubTotalAhorro), X, Y, 0)


            Y = Y - 19
            X = 450
            'Préstamo Complementarios
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(prestComplementarios), X, Y, 0)


            Y = Y - 19
            X = 450
            'Intereses
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interesesComplementaros), X, Y, 0)


            Y = Y - 19
            X = 450
            'Total Préstamo Complementario
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamoComplemenatario), X, Y, 0)

            Y = Y - 17
            X = 450
            'Ajuste Préstamo de ahorro
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePrestamo), X, Y, 0)


            Y = Y - 20
            X = 450
            'Ajuste Pension 
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(ajustePension), X, Y, 0)


            Y = Y - 20
            X = 450
            'Ajuste Saldo Préstamos periodo anterior
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(saldoAhorro), X, Y, 0)


            Y = Y - 19
            X = 450
            'Saldo Ahorro 
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(RetroactivoTrabajador), X, Y, 0)


            Y = Y - 19
            X = 450
            'Saldo Ahorro 
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(RetroactivoEntidad), X, Y, 0)

            Y = Y - 20
            X = 450
            'Saldo Ahorro 
            ' cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(TotalRetroactivo), X, Y, 0)


            Y = Y - 52
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
            Y = 320
            X = 30

            Session("Con").Open()

            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA"
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
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
                        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaunico.pdf")

                        cb = writer.DirectContent
                        EstadoCuenta = writer.GetImportedPage(Reader, 2)
                        cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                        cb.BeginText()
                        cb.SetFontAndSize(bf, 9)


                        Y = 746
                        X = 30

                        'XT = X
                        'YT = Y + 35
                    Else
                        Y = Y - 10
                        X = 60
                    End If
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 7.5)

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
            End If

            Session("Con").Close()
            cb.EndText()

            If contador <> numeroreg Then

                document.NewPage()
                Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaunico.pdf")

                cb = writer.DirectContent
                EstadoCuenta = writer.GetImportedPage(Reader, 1)
                cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                cb.BeginText()
                cb.SetFontAndSize(bf, 9)


                Y = 675
                X = 75

                cb.EndText()
            End If

        Next

        document.Close()
    End Sub

    Private Sub btn_genera_estados_cuenta_unico_prest_Click(sender As Object, e As EventArgs) Handles btn_genera_estados_cuenta_unico_prest.Click
        Try
            CrearCarpetas("PRESTAMOS")

            'Generación de edos de cta de Prestamos.
            Dim da As New DataAccess()
            Dim ds As DataSet = da.RegresaDataSet("SEL_ESTADOS_CTA_PRESTAMOS_MASIVOS_UNICO")
            Dim DetallePrest As DataTable = ds.Tables(0)
            Dim DetalleCCT As DataTable = ds.Tables(2)

            For iDatosAport As Integer = 0 To ds.Tables(1).Rows().Count() - 1

                Dim REGION_BANDERA As Integer = ds.Tables(1).Rows(iDatosAport).Item("REGION").ToString()
                Dim DELEGACION_BANDERA As String = ds.Tables(1).Rows(iDatosAport).Item("DELEGACION").ToString()

                Dim strWhere As String = "REGION=" + CStr(REGION_BANDERA) + " AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'"
                Dim TABLAAUX As DataRow() = DetallePrest.[Select](strWhere)
                Dim numero As Integer = DetallePrest.[Select](strWhere).Count()
                ''Se toman valores por cada registros
                Dim RFC As String = ds.Tables(0).Rows(iDatosAport).Item("RFC").ToString()
                Dim REGION As String = ds.Tables(0).Rows(iDatosAport).Item("REGION").ToString()
                Dim DELEGACION As String = ds.Tables(0).Rows(iDatosAport).Item("DELEGACION").ToString()
                Dim SISTEMA As String = ds.Tables(0).Rows(iDatosAport).Item("SISTEMA").ToString()
                Dim IDPERSONA As String = ds.Tables(0).Rows(iDatosAport).Item("ID_PERSONA").ToString()

                If DELEGACION_BANDERA <> "D-III-2" Then
                    'Inviocar método para la generación del edo de cta de aportaciones
                    EstadoCuentaPrestamos_Unico(True, TABLAAUX, CStr(REGION_BANDERA), DELEGACION_BANDERA, numero)

                    Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + cmb_periodo.SelectedValue.ToString + "\PRESTAMOS\" + CStr(REGION_BANDERA) + "\" + DELEGACION_BANDERA + "\"

                    'CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\Prestamos.pdf", PathDestino, True, RFC, "PRESTAMOS")
                    CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + CStr(REGION_BANDERA) + "_" + DELEGACION_BANDERA + "_PRESTAMOS.pdf", PathDestino, True, CStr(REGION_BANDERA), DELEGACION_BANDERA, "PRESTAMOS")

                Else
                    For idatos As Integer = 0 To DetalleCCT.Rows.Count() - 1
                        Dim BANDERA_CCT As String = DetalleCCT.Rows(idatos).Item("CCT").ToString()

                        Dim strWhere2 As String = "REGION=" + CStr(REGION_BANDERA) + " AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'" + " AND " + "CCT= '" + BANDERA_CCT + "'"
                        Dim TABLAAUX2 As DataRow() = DetallePrest.[Select](strWhere2)
                        Dim numero2 As Integer = DetallePrest.[Select](strWhere2).Count()
                        If numero2 > 0 Then
                            Dim urlestadosdecuenta As String = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta")
                            Dim ruta As String = urlestadosdecuenta + "\" + cmb_periodo.SelectedValue.ToString + "\" + "PRESTAMOS" + "\" + CStr(REGION_BANDERA) + "\" + DELEGACION_BANDERA + "\" + BANDERA_CCT
                            If Not System.IO.Directory.Exists(ruta) Then
                                My.Computer.FileSystem.CreateDirectory(ruta)
                            End If

                            EstadoCuentaPrestamos_Unico(True, TABLAAUX2, CStr(REGION_BANDERA), DELEGACION_BANDERA + "_" + BANDERA_CCT, numero2)
                            CopiarArchivo(Session("APPATH").ToString + "DocPlantillas\EstadoCuenta\" + CStr(REGION_BANDERA) + "_" + DELEGACION_BANDERA + "_" + BANDERA_CCT + "_PRESTAMOS.pdf", ruta, True, CStr(REGION_BANDERA), DELEGACION_BANDERA + "_" + BANDERA_CCT, "PRESTAMOS")

                        End If
                    Next

                End If

            Next


        Catch ex As Exception
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

    Private Sub EstadoCuentaPrestamos_Unico(ByVal EnvioMasivo As Boolean, ByVal tabla As DataRow(), ByVal reg As String, ByVal del As String, ByVal numeroreg As Integer)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")
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
        'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        If EnvioMasivo = False Then
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))
        Else
            'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\Prestamos.pdf", FileMode.Create))
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\" + reg + "_" + del + "_PRESTAMOS.pdf", FileMode.Create))

        End If

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim EstadoCuenta As iTextSharp.text.pdf.PdfImportedPage

        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single

        Dim contador As Integer = 0

        For Each Detalle As DataRow In tabla

            contador += 1
            Dim ID_PERSONA As Integer = Detalle("ID_PERSONA").ToString
            Dim RFC_PERSONA As String = Detalle("RFC").ToString
            Dim SISTEMA As String = Detalle("SISTEMA").ToString
            Dim periodo As String = String.Empty
            Dim region As String = String.Empty
            Dim delegacion As String = String.Empty
            Dim clave_CT As String = String.Empty
            Dim municipio As String = String.Empty
            Dim rfc As String = String.Empty
            Dim nombre As String = String.Empty


            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CTA_ENCABEZADO"
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                periodo = Session("rs").Fields("PERIODO").Value.ToString()
                region = Session("rs").Fields("REGION").Value.ToString()
                delegacion = Session("rs").Fields("DELEGACION").Value.ToString()
                clave_CT = Session("rs").Fields("CLAVE_CT").Value.ToString()
                municipio = Session("rs").Fields("MUNICIPIO").Value.ToString()
                rfc = Session("rs").Fields("RFC").Value.ToString()
                nombre = Session("rs").Fields("NOMBRE").Value.ToString()


            End If
            Session("Con").Close()


            EstadoCuenta = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)

            'ready to draw text
            cb.BeginText()

            Dim distanciaHorizontal As Integer = 240
            Dim distanciaVertical As Integer = 15

            X = 435  'X empieza de izquierda a derecha
            Y = 695 'Y empieza de abajo hacia arriba

            Dim XOrdena As Integer
            Dim YOrdena As Integer

            'RFC
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, periodo, X, Y, 0)

            Y = Y - 15
            X = 435

            'RFC
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, rfc, X, Y, 0)

            Y = Y - 15
            X = 435

            'Delegación
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, delegacion, X, Y, 0)
            Y = Y - 15
            X = 435

            'Municipio
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, municipio, X, Y, 0)

            Y = Y - 15
            X = 435

            'Región
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, region, X, Y, 0)

            Y = Y - 15
            X = 435




            Y = 648
            X = 80

            'Nombre
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, nombre.ToString, X, Y, 0)

            Y = Y - 15
            X = 65


            Dim producto As String = String.Empty
            Dim fechaPrestamo As String = String.Empty
            Dim plazo As String = String.Empty
            Dim autorizado As Decimal = 0.00
            Dim interes As Decimal = 0.00
            Dim totalPrestamo As Decimal = 0.00
            Dim pagosAnticipados As Decimal = 0.00
            Dim totalPagado As Decimal = 0.00
            Dim resto As Decimal = 0.00
            Dim bonificacion As Decimal = 0.00


            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_ESTADOCTA_PRESTAMOS"
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then


                producto = Session("rs").Fields("PRODUCTO").Value.ToString()
                fechaPrestamo = Session("rs").Fields("FECHA_PRESTAMO").Value.ToString()
                plazo = Session("rs").Fields("PLAZO").Value.ToString()
                autorizado = Session("rs").Fields("AUTORIZADO").Value.ToString()
                interes = Session("rs").Fields("INTERES").Value.ToString()
                totalPrestamo = Session("rs").Fields("TOTAL_PRESTAMO").Value.ToString()
                pagosAnticipados = Session("rs").Fields("PAGOS_ANTICIPADOS").Value.ToString()
                totalPagado = Session("rs").Fields("TOTAL_PAGADO").Value.ToString()
                resto = Session("rs").Fields("RESTO").Value.ToString()
                bonificacion = Session("rs").Fields("BONIFICACION").Value.ToString()


            End If
            Session("Con").Close()



            X = 435  'X empieza de izquierda a derecha
            Y = 601 'Y empieza de abajo hacia arriba


            'Producto
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, producto, X, Y, 0)


            Y = 587
            X = 435
            'Fecha Préstamo
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, fechaPrestamo, X, Y, 0)


            Y = Y - 15
            X = 435
            'Plazo
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, plazo, X, Y, 0)


            Y = Y - 15
            X = 435
            'Autorizado
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(autorizado), X, Y, 0)

            Y = Y - 14
            X = 435
            'Intereses
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(interes), X, Y, 0)


            Y = Y - 17
            X = 435
            'Total Préstamo
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


            Y = Y - 391
            X = 462
            'Total Préstamo 2
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPrestamo), X, Y, 0)


            Y = Y - 20
            X = 462
            'Pagos Anticipados
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(pagosAnticipados), X, Y, 0)


            Y = Y - 18
            X = 462
            'Total pagado
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(bonificacion), X, Y, 0)


            Y = Y - 19
            X = 462
            'Total pagado
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(totalPagado), X, Y, 0)


            Y = Y - 18
            X = 462
            'Resto
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(resto), X, Y, 0)


            Y = 480
            X = 40

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 10)


            Session("Con").Open()

            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CNFEXP_ESTADO_CUENTA_PRESTAMOS"
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 20, ID_PERSONA)
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
                        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")

                        cb = writer.DirectContent
                        EstadoCuenta = writer.GetImportedPage(Reader, 1)
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

                    X = 90
                    'QUINCENA
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("QUINCENA").Value.ToString, X, Y, 0)

                    'AÑO
                    X = X + 120
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("ANIO").Value.ToString, X, Y, 0)

                    'DESCUENTO 
                    X = X + 138
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, FormatCurrency(Session("rs").Fields("DESCUENTO").Value.ToString), X, Y, 0)

                    'SALDO
                    X = X + 175
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT, FormatCurrency(Session("rs").Fields("SALDO").Value.ToString), X, Y, 0)

                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()

            'GLOSARIO TERMINOS
            cb.EndText()

            If contador <> numeroreg Then

                document.NewPage()
                Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EstadoCuenta\estadocuentaprestamos.pdf")

                cb = writer.DirectContent
                EstadoCuenta = writer.GetImportedPage(Reader, 1)
                cb.AddTemplate(EstadoCuenta, 1, 0, 0, 1, 0, 0)
                cb.BeginText()
                cb.SetFontAndSize(bf, 9)


                Y = 675
                X = 75

                cb.EndText()
            End If

        Next

        document.Close()
    End Sub

    Private Sub btn_reportes_excel_Click(sender As Object, e As EventArgs) Handles btn_reportes_excel.Click
        Try
            'Generación de reportes
            Dim da As New DataAccess()
            Dim ds As DataSet = da.RegresaDataSet("SEL_REPORTE_EXCEL_PAGO_AHORRO")
            Dim DetallePrest As DataTable = ds.Tables(0)
            Dim DetalleCCT As DataTable = ds.Tables(2)

            For iDatosAport As Integer = 0 To ds.Tables(1).Rows().Count() - 1

                Dim REGION_BANDERA As String = ds.Tables(1).Rows(iDatosAport).Item("REGION").ToString()
                Dim DELEGACION_BANDERA As String = ds.Tables(1).Rows(iDatosAport).Item("DELEGACION").ToString()

                Dim strWhere As String = "REGION='" + CStr(REGION_BANDERA) + "' AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'"
                Dim TABLAAUX As DataRow() = DetallePrest.[Select](strWhere)

                If DELEGACION_BANDERA <> "D-III-2" Then

                    Dim contador = 0

                    Dim Tabla_Final As DataTable = New DataTable()

                    Tabla_Final.Columns.Add("#")
                    Tabla_Final.Columns.Add("CCT")
                    Tabla_Final.Columns.Add("RFC")
                    Tabla_Final.Columns.Add("NOMBRE")
                    Tabla_Final.Columns.Add("TELEFONO")
                    Tabla_Final.Columns.Add("BANCO")
                    Tabla_Final.Columns.Add("CLABE")
                    Tabla_Final.Columns.Add("TOTAL")
                    Tabla_Final.Columns.Add("FIRMA")

                    For Each Detalle As DataRow In TABLAAUX

                        contador += 1

                        Tabla_Final.Rows.Add(contador,
                                             Detalle("CCT").ToString,
                                             Detalle("RFC").ToString,
                                             Detalle("NOMBRE").ToString,
                                             Detalle("TELEFONO").ToString,
                                             Detalle("BANCO").ToString,
                                             Detalle("CLABE").ToString,
                                             Detalle("SALDO").ToString,
                                             Detalle("FIRMA").ToString)

                    Next

                    For i As Integer = 1 To 10
                        contador += 1

                        Tabla_Final.Rows.Add(contador,
                                             "",
                                             "",
                                            "" & vbCrLf & "",
                                             "",
                                             "",
                                             "",
                                             "",
                                             "")
                    Next

                    'Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlrepexcel") + "\" + cmb_periodo.SelectedValue.ToString + "\REPORTES\" + CStr(REGION_BANDERA) + "\" + DELEGACION_BANDERA + "\"
                    Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlrepexcel") + "\" + REGION_BANDERA + "_" + DELEGACION_BANDERA

                    Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Reportes", "Word", "Reporte devoluciones_V2", "Reporte devoluciones_V2")

                    MyPDF.remplazarEtiqueta("REGION", REGION_BANDERA)
                    MyPDF.remplazarEtiqueta("DELEGACION", DELEGACION_BANDERA)

                    MyPDF.makeTable(0, 9, Tabla_Final)
                    MyPDF.Savepdf(PathDestino)


                Else
                    Dim BANDERA_CCT As String = String.Empty
                    For iDatos As Integer = 0 To DetalleCCT.Rows.Count() - 1
                        BANDERA_CCT = DetalleCCT.Rows(iDatos).Item("CCT").ToString()
                        Dim strWhere2 As String = "REGION='" + CStr(REGION_BANDERA) + "' AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'" + " AND " + "CCT= '" + BANDERA_CCT + "'"
                        Dim TABLAAUX2 As DataRow() = DetallePrest.[Select](strWhere2)

                        Dim numero2 As Integer = DetallePrest.[Select](strWhere2).Count()

                        If numero2 > 0 Then

                            Dim contador = 0

                            Dim Tabla_Final As DataTable = New DataTable()

                            Tabla_Final.Columns.Add("#")
                            Tabla_Final.Columns.Add("CCT")
                            Tabla_Final.Columns.Add("RFC")
                            Tabla_Final.Columns.Add("NOMBRE")
                            Tabla_Final.Columns.Add("TELEFONO")
                            Tabla_Final.Columns.Add("BANCO")
                            Tabla_Final.Columns.Add("CLABE")
                            Tabla_Final.Columns.Add("TOTAL")
                            Tabla_Final.Columns.Add("FIRMA")

                            For Each Detalle As DataRow In TABLAAUX2

                                contador += 1

                                Tabla_Final.Rows.Add(contador,
                                                     Detalle("CCT").ToString,
                                                     Detalle("RFC").ToString,
                                                     Detalle("NOMBRE").ToString,
                                                     Detalle("TELEFONO").ToString,
                                                     Detalle("BANCO").ToString,
                                                     Detalle("CLABE").ToString,
                                                     Detalle("SALDO").ToString,
                                                     Detalle("FIRMA").ToString)

                            Next

                            For i As Integer = 1 To 10
                                contador += 1

                                Tabla_Final.Rows.Add(contador,
                                                     "",
                                                     "",
                                                    "" & vbCrLf & "",
                                                     "",
                                                     "",
                                                     "",
                                                     "",
                                                     "")
                            Next

                            Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlrepexcel") + "\" + REGION_BANDERA + "_" + DELEGACION_BANDERA + "_" + BANDERA_CCT

                            Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Reportes", "Word", "Reporte devoluciones_V2", "Reporte devoluciones_V2")

                            MyPDF.remplazarEtiqueta("REGION", REGION_BANDERA)
                            MyPDF.remplazarEtiqueta("DELEGACION", DELEGACION_BANDERA + "_" + BANDERA_CCT)

                            MyPDF.makeTable(0, 9, Tabla_Final)
                            MyPDF.Savepdf(PathDestino)


                        End If


                    Next

                End If


            Next


        Catch ex As Exception
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

    Private Sub btn_reportes_cheque_Click(sender As Object, e As EventArgs) Handles btn_rep_cheque.Click
        Try
            'Generación de reportes
            Dim da As New DataAccess()
            Dim ds As DataSet = da.RegresaDataSet("SEL_REPORTE_EXCEL_PAGO_AHORRO_CHQ")
            Dim DetallePrest As DataTable = ds.Tables(0)


            For iDatosAport As Integer = 0 To ds.Tables(1).Rows().Count() - 1

                Dim REGION_BANDERA As String = ds.Tables(1).Rows(iDatosAport).Item("REGION").ToString()
                Dim DELEGACION_BANDERA As String = ds.Tables(1).Rows(iDatosAport).Item("DELEGACION").ToString()

                Dim strWhere As String = "REGION='" + CStr(REGION_BANDERA) + "' AND " + "DELEGACION= '" + DELEGACION_BANDERA + "'"
                Dim TABLAAUX As DataRow() = DetallePrest.[Select](strWhere)

                Dim contador = 0

                Dim Tabla_Final As DataTable = New DataTable()

                Tabla_Final.Columns.Add("#")
                Tabla_Final.Columns.Add("CCT")
                Tabla_Final.Columns.Add("RFC")
                Tabla_Final.Columns.Add("NOMBRE")
                Tabla_Final.Columns.Add("SISTEMA")
                Tabla_Final.Columns.Add("NUM_CHEQUE")
                Tabla_Final.Columns.Add("TOTAL")
                Tabla_Final.Columns.Add("TIPO")
                Tabla_Final.Columns.Add("FIRMA")

                For Each Detalle As DataRow In TABLAAUX

                    contador += 1

                    Tabla_Final.Rows.Add(contador,
                                         Detalle("CCT").ToString,
                                         Detalle("RFC").ToString,
                                         Detalle("NOMBRE").ToString,
                                         Detalle("SISTEMA").ToString,
                                         Detalle("NUM_CHEQUE").ToString,
                                         Detalle("TOTAL").ToString,
                                         Detalle("TIPO").ToString,
                                         Detalle("FIRMA").ToString)

                Next



                'Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlrepexcel") + "\" + cmb_periodo.SelectedValue.ToString + "\REPORTES\" + CStr(REGION_BANDERA) + "\" + DELEGACION_BANDERA + "\"
                Dim PathDestino As String = ConfigurationManager.AppSettings.[Get]("urlrepexcelchq") + "\" + REGION_BANDERA + "_" + DELEGACION_BANDERA

                Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Reportes", "Word", "Cheque", "Cheque")

                MyPDF.remplazarEtiqueta("REGION", REGION_BANDERA)
                MyPDF.remplazarEtiqueta("DELEGACION", DELEGACION_BANDERA)

                MyPDF.makeTable(0, 9, Tabla_Final)
                MyPDF.Savepdf(PathDestino)
            Next


        Catch ex As Exception
            lbl_estatus.Text = ex.ToString()
        End Try
    End Sub

End Class