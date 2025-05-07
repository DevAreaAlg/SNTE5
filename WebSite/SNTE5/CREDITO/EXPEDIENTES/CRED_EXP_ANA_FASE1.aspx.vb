Public Class CRED_EXP_ANA_FASE1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Análisis", "Análisis de Expediente")

        If Not Me.IsPostBack Then
            lbl_Prospecto.Text = Session("PROSPECTO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente:  " + Session("FOLIO")

            If Session("VENGODE") <> "CNFEXP_PPE.aspx" Then
                EstatusEnAnalisis()
            End If

            If Session("VENGODEDIGGLOBAL") = "DigitalizadorGlobal.aspx" Then
                btn_guardar.Enabled = False
                lbl_estatus.Text = "Guardado correctamente"
            End If

            DatosExpediente()
            'Datos Generales de Expediente: Folio, Nombre de Cliente y Producto

            NombreRepLeg()
            LlenaComites()
            'REVISO DIRECTO A ANALISIS COMITE
            DirectoComite()
            'AGREGO FACULTAD DE AUTORIZACION DE CREDITO
            FacultadAutoriza()

        End If

    End Sub

    Private Sub FacultadAutoriza()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_FACULTAD_AUTORIZA_EXP"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Dim res As String = Session("rs").fields("RESULTADO").value.ToString

        Session("Con").Close()

        If res = "SI" Then

            Dim idcom As String = ObtieneIDcomite()
            Dim item As New ListItem("APROBACION AUTOMATICA", idcom)
            cmb_comites.Items.Add(item)
            cmb_comites.Enabled = True
            txt_razoncom.Enabled = True

        End If

    End Sub

    Private Sub DirectoComite()
        Dim comite As String = ""
        Dim razon As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EXP_DIRECTO_COMITE"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            comite = Session("rs").fields("COMITE").value.ToString
            razon = Session("rs").fields("RAZON").value.ToString
        End If
        Session("Con").Close()



        If comite <> "0" Then 'ya fue enviado a analisis directo a comite
            cmb_comites.Items.FindByValue(comite).Selected = True
            txt_razoncom.Text = razon
            cmb_comites.Enabled = False
            txt_razoncom.Enabled = False
        Else 'reviso si el analista tiene facultad para enviar directamente a comite el exp
            FacultadComite()
        End If

    End Sub

    Private Sub FacultadComite()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_FACULTAD_EXP_COMITE"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("RESULTADO").value.ToString = "SI" Then
            cmb_comites.Enabled = True
            txt_razoncom.Enabled = True
        Else
            cmb_comites.Enabled = False
            txt_razoncom.Enabled = False
        End If

        Session("Con").Close()

    End Sub

    Private Sub LlenaComites()

        Dim elija As New ListItem("ELIJA", "0")

        cmb_comites.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COMITES_ACTIVOS_PRODUCTO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("COMITE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_comites.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub LIMPIAVARIABLES()
        Session("VENGODE") = Nothing
        Session("IDSESCOMIT") = Nothing
        Session("VENGODEDIGGLOBAL") = Nothing

    End Sub

    Private Sub EstatusEnAnalisis()
        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_ESTATUS_EN_DIGITALIZACION"

        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Sub DatosExpediente()
        'Mostar los datos generales de un expediente: folio, nombre de cliente y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"

        Session("rs") = Session("cmd").Execute()

        Session("PRODUCTO") = Session("rs").fields("PRODUCTO").value.ToString
        Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub LlenaScoreBuro()

        cmb_score.Items.Clear()

        Dim liminf As Integer
        Dim limsup As Integer
        Dim elija As New ListItem("ELIJA", "0")
        cmb_score.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_LIMITES_BURO"
        Session("rs") = Session("cmd").Execute()


        liminf = CInt(Session("rs").Fields("LIMINF").Value)
        limsup = CInt(Session("rs").Fields("LIMSUP").Value)
        Session("Con").Close()

        For i As Integer = liminf To limsup
            Dim item As New ListItem(i.ToString, i.ToString)
            cmb_score.Items.Add(item)
        Next

    End Sub

    Private Sub LlenaScoreCirculo()
        cmb_score.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_score.Items.Add(elija)

        Dim item1 As New ListItem("BUENO", "BUENO")
        cmb_score.Items.Add(item1)

        Dim item2 As New ListItem("MALO", "MALO")
        cmb_score.Items.Add(item2)

        Dim item3 As New ListItem("REGULAR", "REGULAR")
        cmb_score.Items.Add(item3)

    End Sub

    Protected Sub cmb_buro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_buro.SelectedIndexChanged

        Select Case cmb_buro.SelectedItem.Value
            Case "BURO"
                LlenaScoreBuro()
            Case "CIRCULO"
                LlenaScoreCirculo()
            Case "NODISP"
                cmb_score.Items.Clear()
                Dim item As New ListItem("NO DISPONIBLE", "NODISP")
                cmb_score.Items.Add(item)
        End Select


    End Sub

    Private Sub ActualizaScore()

        'actualizo estatus expediente
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "UPD_EXPEDIENTE_SCORE_BURO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("BURO", Session("adVarChar"), Session("adParamInput"), 15, cmb_buro.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SCORE", Session("adVarChar"), Session("adParamInput"), 15, cmb_score.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub


    Private Sub NombreRepLeg()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CONFGLOBAL_MASCORE_ENTIDAD"
        Session("rs") = Session("cmd").Execute()
        Session("REPLEG_ENTIDAD") = Session("rs").Fields("REPLEG").value.ToString
        Session("Con").Close()
    End Sub

    Protected Sub Genera_Acta_Firmada(ByVal Folio As String, ByVal Idses As String)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaDirCredito.pdf")

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

        Dim document As New iTextSharp.text.Document(psize, 35, 40, 380, 180)

        With document
            .AddAuthor("INFOQUEST")
            .AddCreationDate()
            .AddCreator("INFOQUEST- Dictamen Director")
            .AddSubject("Dictamen Director - " + Folio)
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Dictamen Director - " + Folio)
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Dictamen Director - " + Folio)
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
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_SUCURSAL_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            X = 325
            Y = 675
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)

        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Y = Y - 18
            Dim fechasis As String
            fechasis = Session("rs").Fields("FECHA_SISTEMA").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHA_SISTEMA").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHA_SISTEMA").Value.ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, fechasis, X, Y, 0)
            fechasis = Nothing

            Session("PERSONAID") = Session("rs").Fields("IDCLIENTE").Value.ToString

            X = X + 160
            'FOLIO DE EXPEDIENTE
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Folio, X, Y, 0)

            cb.SetFontAndSize(bf, 8)

            Y = Y - 30
            X = X - 355
            'NO. CLIENTE
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("IDCLIENTE").Value.ToString, X, Y, 0)

            'NOMBRE DEL PROSPECTO
            Y = Y - 17
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PROSPECTO").Value.ToString, X, Y, 0)

        End If

        Dim Gasto As String

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'MONTO txt_razoncom
            Y = Y - 17
            Gasto = FormatCurrency(Session("rs").Fields("MONTO").Value.ToString)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Gasto, X, Y, 0)

            'FINALIDAD
            Y = Y - 34
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FINALIDAD").Value.ToString, X, Y, 0)

            'TASA NORMAL
            Y = Y - 95
            X = X - 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TASA_NORMAL").Value.ToString, X, Y, 0)

            'PERIODICIDAD
            X = X + 260
            If Session("rs").Fields("PERIODICIDAD").Value.ToString = "" Or Session("rs").Fields("PERIODICIDAD").Value.ToString Is Nothing Then
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PERIODICIDAD_UNIDAD").Value.ToString, X, Y, 0)
            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CADA " + Session("rs").Fields("PERIODICIDAD").Value.ToString + " " + Session("rs").Fields("PERIODICIDAD_UNIDAD").Value.ToString, X, Y, 0)
            End If

            'PLAZO
            Y = Y - 17
            X = X - 260
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PLAZO").Value.ToString + " " + Session("rs").Fields("TIPO_PLAZO").Value.ToString, X, Y, 0)

            'PRODUCTO
            X = X + 300
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PRODUCTO").Value.ToString, X, Y, 0)

        End If

        Y = 520
        X = 130
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'MONTO txt_razoncom
            'Y = Y - 17
            Gasto = FormatCurrency(Session("rs").Fields("MONTO").Value.ToString)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Gasto, X, Y, 0)
        End If


        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_GARANTIAS_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Y = 575
            X = 145
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("GARANTIA").Value.ToString, X, Y, 0)

        End If

        'Session("rs") = CreateObject("ADODB.Recordset")
        'Session("cmd") = New ADODB.Command()
        'Session("cmd").ActiveConnection = Session("Con")
        'Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        'Session("cmd").CommandText = "SEL_DATOS_SESION_COMITE_PRELLENADO"
        'Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, Idses)
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("rs") = Session("cmd").Execute()
        'If Not Session("rs").eof Then

        '    Y = Y - 38
        '    X = 165

        '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '    cb.SetFontAndSize(bf, 9)

        '    Dim resultado As String
        '    resultado = Session("rs").Fields("RESULTADO").Value.ToString
        '    If resultado = "RECHAZOREC" Then
        '        resultado = "RECHAZADO CON RECOMENDACIONES"
        '    End If
        '    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, resultado, X, Y, 0)

        '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '    cb.SetFontAndSize(bf, 8)

        '    Y = Y - 18
        '    X = X - 40

        '    If Session("rs").Fields("MONTO_AUTOR").Value.ToString = 0.0 Then
        '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Gasto, X, Y, 0)
        '    Else
        '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(Session("rs").Fields("MONTO_AUTOR").Value.ToString), X, Y, 0)
        '    End If

        '    X = X - 20

        '    Dim ObLargo As Decimal
        '    Dim aux As Integer = 0
        '    ObLargo = Len(Session("rs").Fields("OBSERVACIONES").Value.ToString) / 85

        '    Y = Y - 17
        '    While (ObLargo > 0)
        '        If aux = 0 Then
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Left(Session("rs").Fields("OBSERVACIONES").Value.ToString, 85), X, Y, 0)
        '            aux = aux + 100
        '        Else
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Mid(Session("rs").Fields("OBSERVACIONES").Value.ToString, (aux + 1), 85), X, Y, 0)
        '            aux = aux + 110
        '        End If

        '        Y = Y - 10
        '        ObLargo = ObLargo - 1
        '    End While

        '    Dim Parrafo1 As String
        '    Parrafo1 = Session("rs").Fields("RECOMENDACIONES").Value.ToString

        '    Dim paragraph1 As New iTextSharp.text.Paragraph(Parrafo1, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL))

        '    paragraph1.SetAlignment("JUSTIFY")

        '    document.Add(paragraph1)

        'End If




        Dim firma As Integer
        firma = 0

        Y = 160

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INFO_JEFE_SUCURSAL_FOLIO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF



                If (Y - 50) < 30 Then
                    cb.EndText()
                    Y = 725

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ActaDirCreditoBlanco.pdf")

                    cb = writer.DirectContent
                    Solicitud = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                End If

                If firma = 1 Then
                    X = 430
                End If
                If firma = 0 Or firma = 2 Then
                    X = 130
                End If


                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, """Jefe de Sucursal""", X, Y, 0)

                Y = Y - 40
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "_______________________________________", X, Y, 0)

                Y = Y - 12
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)
                Y = Y - 10

                Y = Y + 10

                firma = firma + 1

                If firma = 2 Then
                    Y = Y - 80
                    firma = 0
                End If

                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()
        cb.EndText()
        document.Close()


    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click

        ActualizaScore()

        If cmb_comites.SelectedItem.Value <> "0" Then 'si tiene la facultad

            If cmb_comites.SelectedItem.Value <> "4" Then 'fue enviado sin analisis automatico a comite o director de credito
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "UPD_EXPEDIENTE_ANALIZADO_COMITE"
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDCOMITE", Session("adVarChar"), Session("adParamInput"), 10, cmb_comites.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").Execute()
                Session("Con").Close()

                'obtengo correos de miembros de comite (IDENTIFICADOR = 1)
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_EXPEDIENTE_CORREOS_COMITE"
                Session("parm") = Session("cmd").CreateParameter("IDCOM", Session("adVarChar"), Session("adParamInput"), 10, cmb_comites.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()


                'ENVIO EL CORREO A CADA MIEMBRO
                Do While Not Session("rs").EOF
                    ' EnviaMail(Session("rs").Fields("EMAIL").Value.ToString, Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("PUESTO").Value.ToString, cmb_comites.SelectedItem.Value)
                    Session("rs").movenext()
                Loop

                Session("Con").Close()

                lbl_estatus.Text = "El expediente fue enviado a " + cmb_comites.SelectedItem.Text + " para su análisis"
            Else 'EL ANALISTA TIENE LA FACULTAD PARA AUTORIZAR EL CREDITO SIN ANALISIS AUTOMATICO
                'OBTENGO EL MONTO SOLICITADO DEL CREDITO Y LOS LIMITES PARA ENVIAR A COMITE Y DIRECTOR DE CREDITO
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_ANAEXP_MONTO_CRED_LIMITES"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()

                Dim resul As String
                resul = Session("rs").Fields("RESULTADO").Value.ToString

                Session("Con").Close()

                If resul = "AUTORIZADO" Then 'el monto si esta dentro de los limites por lo que se autoriza el credito

                    Session("Con").Open()
                    Session("cmd") = New ADODB.Command()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("cmd").CommandText = "UPD_EXPEDIENTE_ANALIZADO_FASE_1"
                    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").Execute()
                    Session("Con").Close()

                    'se avisa a los miembros del comite 
                    Session("Con").Open()
                    Session("cmd") = New ADODB.Command()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("cmd").CommandText = "SEL_EXPEDIENTE_CORREOS_COMITE"
                    Session("parm") = Session("cmd").CreateParameter("IDCOM", Session("adVarChar"), Session("adParamInput"), 10, cmb_comites.SelectedItem.Value)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("rs") = Session("cmd").Execute()


                    'ENVIO EL CORREO A CADA MIEMBRO
                    Do While Not Session("rs").EOF
                        ' EnviaMailAutorizaAuto(Session("rs").Fields("EMAIL").Value.ToString, Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("PUESTO").Value.ToString, cmb_comites.SelectedItem.Value)
                        Session("rs").movenext()
                    Loop

                    Session("Con").Close()

                    Session("Resultado") = resul

                    Dim idComite As String = ObtieneIDcomite()
                    Dim idSesComit As String = ObtieneIDsesion(idComite)
                    ActualizaSesion(idSesComit)
                    Session("IDSESCOMIT") = idSesComit

                    lnk_generar_acta.Enabled = True
                    lnk_digitalizar.Enabled = True
                    pnl_acta.Visible = True

                    lbl_estatus.Text = "El resultado del análisis del expediente fue: APROBADO"


                Else 'EL MONTO SOLICITADO NO ESTA DENTRO DE LOS LIMITES POR LO QUE SE ENVIA AL ANALISIS AUTOMATICO
                    GoTo ANAMOTOR
                End If

            End If

        Else 'flujo normal
ANAMOTOR:   Dim resultado As String
            'invoco motor de analisis automatico
            resultado = MotorAnalisis()

            'envio correo de aviso a miembros comite
            If resultado = "COMITE" Or resultado = "DIRECTOR" Then

                Dim idcomite As Integer
                Select Case resultado
                    Case "COMITE"
                        idcomite = 1
                    Case "DIRECTOR"
                        idcomite = 2
                End Select

                ''obtengo correos de miembros de comite 
                'Session("Con").Open()
                'Session("cmd") = New ADODB.Command()
                'Session("cmd").ActiveConnection = Session("Con")
                'Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                'Session("cmd").CommandText = "SEL_EXPEDIENTE_CORREOS_COMITE"
                'Session("parm") = Session("cmd").CreateParameter("IDCOM", Session("adVarChar"), Session("adParamInput"), 10, idcomite)
                'Session("cmd").Parameters.Append(Session("parm"))
                'Session("rs") = Session("cmd").Execute()


                ''ENVIO EL CORREO A CADA MIEMBRO
                'Do While Not Session("rs").EOF
                '    EnviaMail(Session("rs").Fields("EMAIL").Value.ToString, Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("PUESTO").Value.ToString, idcomite)
                '    Session("rs").movenext()
                'Loop

                'Session("Con").Close()

            End If

            'en caso de aprobacion o rechazo del expediente se le avisa al jefe de sucursal correspondiente
            If resultado = "RECHAZADO" Or resultado = "APROBADO" Then

                Session("Resultado") = resultado

                Dim idComite As String = ObtieneIDcomite()
                Dim idSesComit As String = ObtieneIDsesion(idComite)
                ActualizaSesion(idSesComit)

                lnk_generar_acta.Enabled = True
                lnk_digitalizar.Visible = True
                pnl_acta.Visible = True



            End If

            lbl_estatus.Text = "El resultado del análisis del expediente fue: " + resultado

        End If

        btn_guardar.Enabled = False

    End Sub


    Private Function ObtieneIDcomite() As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_IDCOMITE_AUTOMATICO"
        Session("rs") = Session("cmd").Execute()

        Dim IDCOMITE As String = Session("rs").Fields("IDCOMITE").value.ToString()
        Session("Con").Close()

        Return IDCOMITE

    End Function



    Private Function ObtieneIDsesion(ByVal idComite As String) As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCOMITE", Session("adVarChar"), Session("adParamInput"), 10, idComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_ANAEXP_CREA_SESION"
        Session("rs") = Session("cmd").Execute()

        Dim Idsescomit As String = Session("rs").Fields("IDSESCOMIT").value.ToString()

        Session("Con").Close()

        Return Idsescomit

    End Function



    Private Sub ActualizaSesion(ByVal idSesComit As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, idSesComit)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DICTAMEN", Session("adVarChar"), Session("adParamInput"), 10, Session("Resultado").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_ANAEXP_DICTAMEN_AUTOMATICO"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()
    End Sub


    Protected Sub lnk_digitalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_digitalizar.Click
        Session("VENGODE") = "CRED_EXP_ANA_FASE1.ASPX"
        Session("IDSESCOMIT") = Session("IDSESCOMIT")
        Response.Redirect("~/DIGITALIZADOR/DIGI_GLOBAL.aspx")
    End Sub


    Protected Sub lnk_generar_acta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar_acta.Click

        Dim dest As String
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim folio As String = String.Empty
        Dim producto As String = String.Empty
        Dim cliente As String = String.Empty
        Dim resultado As String = String.Empty
        dest = CorreoJefeSucursal()
        Genera_Acta_Firmada(Session("FOLIO").ToString, Session("IDSESCOMIT"))

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ACTAFIRMADA" + Session("Resultado") + ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With
        folio = Session("FOLIO").ToString
        producto = Session("PRODUCTO").ToString
        cliente = Session("PROSPECTO").ToString
        resultado = Session("Resultado").ToString
        subject = "Resultado Analisis Expediente Número " + folio
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a) Jefe de Sucursal: </td></tr>")
        sbhtml.Append("<tr><td>A continuación se muestra el resultado del análisis del expediente con número:  " + "<b>" + folio + "</b>" + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br />")
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
        sbhtml.Append("<tr><td width='75%'>Número de expediente:</td><td>" + "<b>" + folio + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td width='30%'>Producto: </td>" + "<b>" + producto + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td width='50%'>Cliente: </td>" + "<b>" + cliente + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td width='50%'>Resultado:  </td>" + "<b>" + resultado + "</b>" + "</td></tr>")
        sbhtml.Append("<br></br>")
        sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br></br>")
        clase_Correo.Envio_email(sbhtml.ToString, subject, dest, cc)
        lnk_digitalizar.Enabled = True

    End Sub
    Private Function MotorAnalisis() As String
        Dim res As String = "Información Incompleta"

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "UPD_EXPEDIENTE_ANALISIS_AUTOMATICO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("rs") = Session("cmd").Execute()


        If Not Session("rs").eof Then
            res = Session("rs").Fields("RESULTADO").Value.ToString
        End If
        Session("Con").Close()
        Return res
    End Function

    Private Sub EnviaMailJefeSucursal(ByVal destinatario As String, ByVal resultado As String)


        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim folio As String = String.Empty
        Dim producto As String = String.Empty
        Dim cliente As String = String.Empty

        folio = Session("FOLIO").ToString
        producto = Session("PRODUCTO").ToString
        cliente = Session("PROSPECTO").ToString
        resultado = Session("Resultado")
        subject = "Resultado Análisis Expediente Número " + folio
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a) Jefe de Sucursal: </td></tr>")
        sbhtml.Append("<tr><td>A continuación se muestra el resultado del análisis del expediente con número:  " + "<b>" + folio + "</b>" + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br />")
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
        sbhtml.Append("<tr><td width='75%'>Número de expediente:</td><td>" + "<b>" + folio + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td width='30%'>Producto: </td>" + "<b>" + producto + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td width='50%'>Cliente: </td>" + "<b>" + cliente + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td width='50%'>Resultado: </td>" + "<b>" + resultado + "</b>" + "</td></tr>")
        sbhtml.Append("<br></br>")
        sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br></br>")

        clase_Correo.Envio_email(sbhtml.ToString, subject, destinatario, cc)
    End Sub

    Private Function CorreoJefeSucursal() As String
        Dim destinatario As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CORREO_JEFE_SUCURSAL_FOLIO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("rs") = Session("cmd").Execute()


        If Not Session("rs").eof Then
            destinatario = Session("rs").Fields("EMAIL").Value.ToString
        End If
        Session("Con").Close()

        Return destinatario
    End Function

    'Private Sub AvisoCambioEstatus()

    '    Dim correo As String

    '    'Insertar a la Cola de validacion para la fase 2
    '    Session("cmd") = New ADODB.Command()
    '    Session("Con").Open()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "SEL_AVISOCORREO_ESTATUS_USUARIO"
    '    Session("rs") = Session("cmd").Execute()
    '    Do While Not Session("rs").EOF

    '        correo = "Estimado(a) " + Session("rs").Fields("USUARIO").Value.ToString + vbCrLf + vbCrLf + Session("rs").Fields("CONTENIDO").Value.ToString + vbCrLf
    '        correo = correo + vbCrLf + "Atentamente" + vbCrLf + vbCrLf + "MAS.Core" + vbCrLf + Session("EMPRESA").ToString

    '        Const ConfigNamespace As String = "http://schemas.microsoft.com/cdo/configuration/"
    '        Dim oMsg As New CDO.Message()
    '        Dim iConfig As New CDO.Configuration()
    '        Dim Flds As ADODB.Fields = iConfig.Fields
    '        With iConfig.Fields
    '            .Item(ConfigNamespace & "smtpserver").Value = Session("MAIL_SERVER")
    '            .Item(ConfigNamespace & "smtpserverport").Value = Session("MAIL_SERVER_PORT")
    '            .Item(ConfigNamespace & "sendusing").Value = CDO.CdoSendUsing.cdoSendUsingPort
    '            .Item(ConfigNamespace & "sendusername").Value = Session("MAIL_SERVER_USER")
    '            .Item(ConfigNamespace & "sendpassword").Value = Session("MAIL_SERVER_PWD")
    '            .Item(ConfigNamespace & "smtpauthenticate").Value = CDO.CdoProtocolsAuthentication.cdoBasic
    '            .Update()
    '        End With

    '        With oMsg
    '            .Configuration = iConfig
    '            .From = Session("MAIL_SERVER_FROM")
    '            .To = Session("rs").Fields("EMAIL").Value.ToString
    '            .Subject = "CAMBIO DE ESTATUS DE EXPEDIENTE (" + CStr(Session("FOLIO")) + ")"
    '            .TextBody = correo
    '            .Send()
    '        End With
    '        oMsg = Nothing
    '        iConfig = Nothing

    '        Session("rs").movenext()
    '    Loop

    '    Session("Con").Close()
    'End Sub

End Class