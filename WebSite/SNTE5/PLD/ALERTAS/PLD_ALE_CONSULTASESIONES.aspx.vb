Public Class PLD_ALE_CONSULTASESIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Consulta Sesiones Alertas", "ACTAS DE SESIONES DE ALERTAS PLD")
        If Not Me.IsPostBack Then
            LlenaSesiones()
        End If

        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            Session("idperbusca") = Nothing
        End If
    End Sub

    Protected Sub btn_creaF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_creaF.Click
        LlenaSesiones()
    End Sub

    Private Sub LlenaSesiones()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtOpePend As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If txt_IdCliente.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, "-1")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("FECHAINI", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaini.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAFIN", Session("adVarChar"), Session("adParamInput"), 10, txt_fechafin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SESIONES_PLD_TERMINADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtOpePend, Session("rs"))

        Session("Con").Close()

        If dtOpePend.Rows.Count > 0 Then
            dag_Sesiones.Visible = True
            dag_Sesiones.DataSource = dtOpePend
            dag_Sesiones.DataBind()
        Else
            dag_Sesiones.Visible = True
        End If

    End Sub

    Protected Sub btn_eliminaF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminaF.Click
        txt_IdCliente.Text = ""
        txt_fechaini.Text = ""
        txt_fechafin.Text = ""
        ' lbl_NombrePersonaBusquedTexto.Text = ""
        LlenaSesiones()
    End Sub

    Protected Sub dag_Sesiones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Sesiones.ItemCommand

        'lbl_Info.Text = ""

        If (e.CommandName = "ACTA") Then
            If e.Item.Cells(1).Text = "OC" Then
                GenerarActaOC(e.Item.Cells(0).Text)
            End If
            If e.Item.Cells(1).Text = "CCC" Then
                GenerarActaCCC(e.Item.Cells(0).Text)
            End If
        End If

        If (e.CommandName = "VERDIGIT") Then
            Session("SESION_COMITE") = e.Item.Cells(0).Text
            Response.Redirect("MostrarDigitActaCCC.aspx")
        End If

        If (e.CommandName = "DIGIT") Then
            If RevisaFacultadesPLD() = 1 Then
                lbl_statusc.Text = ""
                Session("VENGODE") = "PLD_ALE_CONSULTASESIONES.aspx"
                Session("SESION_COMITE") = e.Item.Cells(0).Text
                Response.Redirect("/DIGITALIZADOR/DIGI_GLOBAL.aspx")
            Else
                lbl_statusc.Text = "Error: No tiene los permisos necesarios para volver a digitalizar esta acta."
            End If
        End If

    End Sub
    Private Sub GenerarActaOC(ByVal IDSesionComite As Integer)

        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\RepOpePLD.pdf")

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
            .AddAuthor("Reporte Operaciones PLD - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Reporte Operaciones PLD")
            .AddSubject("Reporte Operaciones PLD")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Reporte Operaciones PLD")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Reporte Operaciones PLD")
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        Dim X, Y As Single
        X = 425
        Y = 675

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_ACTA_OC"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 410
            Y = Y - 43
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "10:00", X, Y, 0)
            X = X + 120
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHA_SESION").Value.ToString, X, Y, 0)
            X = X + 250
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "FINMEX", X, Y, 0)
            X = X - 305
            Y = Y - 47
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)

            X = X + 185
            Y = Y - 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X - 462
            Y = Y - 13
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_REL").Value.ToString, X, Y, 0)
            X = X + 20
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_REL").Value.ToString, X, Y, 0)

            X = X + 345
            Y = Y - 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 85
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X - 458
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_INU").Value.ToString, X, Y, 0)
            X = X + 20
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_INU").Value.ToString, X, Y, 0)

            X = X - 65
            Y = Y - 82
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_PRE").Value.ToString, X, Y, 0)
            X = X + 20
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_PRE").Value.ToString, X, Y, 0)

        End If

        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TEMPORAL.pdf")

        cb = writer.DirectContent
        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        X = 300
        Y = 750
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "ANEXO 1", X, Y, 0)
        Y = Y - 10
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

        X = 50
        Y = Y - 30

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ALERTAPLD_X_SESION_ACTA"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF
                If (Y - 80) < 20 Then
                    cb.EndText()
                    X = 300
                    Y = 750

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TEMPORAL.pdf")

                    cb = writer.DirectContent
                    Solicitud = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

                    X = 50
                    Y = Y - 30
                End If

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Operación: " + Session("rs").Fields("OPERACION").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Len(Session("rs").Fields("NOTA").Value.ToString) > 100 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Left(Session("rs").Fields("NOTA").Value.ToString, 100), X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                     " + Mid(Session("rs").Fields("NOTA").Value.ToString, 101), X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Session("rs").Fields("NOTA").Value.ToString, X, Y, 0)
                End If

                Y = Y - 13
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Session("rs").Fields("FOLIO").Value.ToString = "-1" Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: ", X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: " + Session("rs").Fields("FOLIO").Value.ToString, X, Y, 0)
                End If

                X = X + 250
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha: " + Left(Session("rs").Fields("FECHA").Value.ToString, 10), X, Y, 0)

                X = X - 250
                Y = Y - 13
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Dictamen: " + Session("rs").Fields("ESTATUS").Value.ToString, X, Y, 0)

                Dim ObLargo As Decimal
                Dim aux As Integer = 0
                ObLargo = Len(Session("rs").Fields("OBSERVACIONES").Value.ToString) / 100

                Y = Y - 13
                While (ObLargo > 0)
                    If aux = 0 Then
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Left(Session("rs").Fields("OBSERVACIONES").Value.ToString, 100), X, Y, 0)
                        aux = aux + 100
                    Else
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Mid(Session("rs").Fields("OBSERVACIONES").Value.ToString, (aux + 1), 110), X, Y, 0)
                        aux = aux + 110
                    End If

                    Y = Y - 10
                    ObLargo = ObLargo - 1
                End While

                Y = Y - 20

                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

        cb.EndText()

        document.Close()

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ConsultaReporteAlertasPLD(" + CStr(IDSesionComite) + ").pdf")
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


    Private Sub GenerarActaCCC0(ByVal IDSesionComite As Integer)

        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaSesionCCC1.pdf")

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
            .AddAuthor("Desarrollo - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Reporte Denominación")
            .AddSubject("Reporte Denominación")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Reporte Denominación")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Reporte Denominación")
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        Dim X, Y As Single
        X = 425
        Y = 685

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_ACTA_CCC"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 460
            Y = Y - 53
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "10:00", X, Y, 0)
            X = X + 95
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 95
            Y = Y - 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 130
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_REL").Value.ToString, X, Y, 0)
            X = X - 200
            Y = Y - 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TRIMESTRE").Value.ToString, X, Y, 0)
            X = X + 250
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_REL").Value.ToString, X, Y, 0)
            X = X - 170
            Y = Y - 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 85
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_INU").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 103
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 215
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_INU").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_PRE").Value.ToString, X, Y, 0)

            cb.EndText()

            document.NewPage()
            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaSesionCCC2.pdf")

            cb = writer.DirectContent
            Solicitud = writer.GetImportedPage(Reader, 1)
            cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
            cb.BeginText()
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            X = 80
            Y = 650

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 210
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_PRE").Value.ToString, X, Y, 0)
            X = X + 265
            Y = Y - 35
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 520
            Y = Y - 10
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 80
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)

        End If

        Y = Y - 50
        Dim lado As Integer = 0

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ALERTAPLD_MIEMBROS_COMITE_ACTA"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF

                If lado = 0 Then
                    X = 165
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________________", X, Y, 0)
                    Y = Y - 12
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("USUARIO").Value.ToString, X, Y, 0)
                    Y = Y + 12
                    lado = 1
                Else
                    X = 445
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________________", X, Y, 0)
                    Y = Y - 12
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("USUARIO").Value.ToString, X, Y, 0)
                    Y = Y + 12
                    lado = 0
                End If

                If Y < 30 And lado = 0 Then
                    cb.EndText()
                    X = 300
                    Y = 750

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TEMPORAL.pdf")

                    cb = writer.DirectContent
                    Solicitud = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "MIEMBROS DE SESIÓN", X, Y, 0)

                    Y = Y - 50
                End If

                Y = Y - 50

                Session("rs").movenext()
            Loop
        End If

        cb.EndText()

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

        cb = writer.DirectContent
        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        X = 300
        Y = 750

        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

        X = 50
        Y = Y - 30

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ALERTAPLD_X_SESION_ACTA"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF
                If (Y - 80) < 20 Then
                    cb.EndText()
                    X = 300
                    Y = 750

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TEMPORAL.pdf")

                    cb = writer.DirectContent
                    Solicitud = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()
                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

                    X = 50
                    Y = Y - 30
                End If

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Operación: " + Session("rs").Fields("OPERACION").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Len(Session("rs").Fields("NOTA").Value.ToString) > 100 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Left(Session("rs").Fields("NOTA").Value.ToString, 100), X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                     " + Mid(Session("rs").Fields("NOTA").Value.ToString, 101), X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Session("rs").Fields("NOTA").Value.ToString, X, Y, 0)
                End If

                Y = Y - 13
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Session("rs").Fields("FOLIO").Value.ToString = "-1" Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: ", X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: " + Session("rs").Fields("FOLIO").Value.ToString, X, Y, 0)
                End If

                X = X + 250
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha: " + Left(Session("rs").Fields("FECHA").Value.ToString, 10), X, Y, 0)

                X = X - 250
                Y = Y - 13
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Dictamen: " + Session("rs").Fields("ESTATUS").Value.ToString, X, Y, 0)

                Y = Y - 13
                If Len(Session("rs").Fields("OBSERVACIONES").Value.ToString) > 100 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Left(Session("rs").Fields("OBSERVACIONES").Value.ToString, 100), X, Y, 0)
                    Y = Y - 10
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                       " + Mid(Session("rs").Fields("OBSERVACIONES").Value.ToString, 101), X, Y, 0)
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Session("rs").Fields("OBSERVACIONES").Value.ToString, X, Y, 0)
                End If

                Y = Y - 20

                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

        cb.EndText()

        document.Close()

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ConsultaSesionCCC(" + CStr(IDSesionComite) + ").pdf")
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

    Private Sub GenerarActaCCC(ByVal IDSesionComite As Integer)

        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaSesionCCC1.pdf")

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
            .AddAuthor("Desarrollo - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Reporte Denominación")
            .AddSubject("Reporte Denominación")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Reporte Denominación")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Reporte Denominación")
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
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 8)

        Dim X, Y As Single
        X = 425
        Y = 685

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_ACTA_CCC"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 460
            Y = Y - 53
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "10:00", X, Y, 0)
            X = X + 95
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 95
            Y = Y - 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 130
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_REL").Value.ToString, X, Y, 0)
            X = X - 200
            Y = Y - 105
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TRIMESTRE").Value.ToString, X, Y, 0)
            X = X + 250
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_REL").Value.ToString, X, Y, 0)
            X = X - 170
            Y = Y - 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 85
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_INU").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 103
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 215
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_INU").Value.ToString, X, Y, 0)
            X = X - 235
            Y = Y - 60
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_EVENTOS").Value.ToString, X, Y, 0)
            X = X + 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_OPE_PRE").Value.ToString, X, Y, 0)

            cb.EndText()




            'document.NewPage()
            'Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\ActaSesionCCC2.pdf")

            'cb = writer.DirectContent
            'Solicitud = writer.GetImportedPage(Reader, 1)
            'cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
            'cb.BeginText()
            'bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            'cb.SetFontAndSize(bf, 8)

            'X = 80

            'Y = 650

            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_EVENTOS").Value.ToString, X, Y, 0)
            'X = X + 210
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUM_REP_OPE_PRE").Value.ToString, X, Y, 0)
            'X = X + 265
            'Y = Y - 35
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            'X = X - 520
            'Y = Y - 10
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            'X = X + 80
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)

        End If


        'Y = Y - 50
        'Dim lado As Integer = 0

        'Session("rs") = CreateObject("ADODB.Recordset")
        'Session("cmd") = New ADODB.Command()
        'Session("cmd").ActiveConnection = Session("Con")
        'Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        'Session("cmd").CommandText = "SEL_ALERTAPLD_MIEMBROS_COMITE_ACTA"
        'Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("rs") = Session("cmd").Execute()
        'If Not Session("rs").eof Then

        '    Do While Not Session("rs").EOF

        '        If lado = 0 Then
        '            X = 165
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________________", X, Y, 0)
        '            Y = Y - 12
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("USUARIO").Value.ToString, X, Y, 0)
        '            Y = Y + 12
        '            lado = 1
        '        Else
        '            X = 445
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________________", X, Y, 0)
        '            Y = Y - 12
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("USUARIO").Value.ToString, X, Y, 0)
        '            Y = Y + 12
        '            lado = 0
        '        End If

        '        If Y < 30 And lado = 0 Then
        '            cb.EndText()
        '            X = 300
        '            Y = 750

        '            document.NewPage()
        '            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

        '            cb = writer.DirectContent
        '            Solicitud = writer.GetImportedPage(Reader, 1)
        '            cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        '            cb.BeginText()
        '            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '            cb.SetFontAndSize(bf, 8)

        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "MIEMBROS DE SESIÓN", X, Y, 0)

        '            Y = Y - 50
        '        End If

        '        Y = Y - 50

        '        Session("rs").movenext()
        '    Loop
        'End If

        'cb.EndText()

        'document.NewPage()
        'Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

        'cb = writer.DirectContent
        'Solicitud = writer.GetImportedPage(Reader, 1)
        'cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        'cb.BeginText()
        'bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        'cb.SetFontAndSize(bf, 8)

        'X = 300
        'Y = 750

        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

        'X = 50
        'Y = Y - 30

        'Session("rs") = CreateObject("ADODB.Recordset")
        'Session("cmd") = New ADODB.Command()
        'Session("cmd").ActiveConnection = Session("Con")
        'Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        'Session("cmd").CommandText = "SEL_ALERTAPLD_X_SESION_ACTA"
        'Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, IDSesionComite)
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("rs") = Session("cmd").Execute()
        'If Not Session("rs").eof Then

        '    Do While Not Session("rs").EOF
        '        If (Y - 80) < 20 Then
        '            cb.EndText()
        '            X = 300
        '            Y = 750

        '            document.NewPage()
        '            Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\TEMPORAL.pdf")

        '            cb = writer.DirectContent
        '            Solicitud = writer.GetImportedPage(Reader, 1)
        '            cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        '            cb.BeginText()
        '            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        '            cb.SetFontAndSize(bf, 8)

        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

        '            X = 50
        '            Y = Y - 30
        '        End If

        '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Operación: " + Session("rs").Fields("OPERACION").Value.ToString, X, Y, 0)

        '        Y = Y - 13
        '        If Len(Session("rs").Fields("NOTA").Value.ToString) > 100 Then
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Left(Session("rs").Fields("NOTA").Value.ToString, 100), X, Y, 0)
        '            Y = Y - 10
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                     " + Mid(Session("rs").Fields("NOTA").Value.ToString, 101), X, Y, 0)
        '        Else
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Descripción: " + Session("rs").Fields("NOTA").Value.ToString, X, Y, 0)
        '        End If

        '        Y = Y - 13
        '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)

        '        Y = Y - 13
        '        If Session("rs").Fields("FOLIO").Value.ToString = "-1" Then
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: ", X, Y, 0)
        '        Else
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Expediente: " + Session("rs").Fields("FOLIO").Value.ToString, X, Y, 0)
        '        End If

        '        X = X + 250
        '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha: " + Left(Session("rs").Fields("FECHA").Value.ToString, 10), X, Y, 0)

        '        X = X - 250
        '        Y = Y - 13
        '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Dictamen: " + Session("rs").Fields("ESTATUS").Value.ToString, X, Y, 0)

        '        Y = Y - 13
        '        If Len(Session("rs").Fields("OBSERVACIONES").Value.ToString) > 100 Then
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Left(Session("rs").Fields("OBSERVACIONES").Value.ToString, 100), X, Y, 0)
        '            Y = Y - 10
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                       " + Mid(Session("rs").Fields("OBSERVACIONES").Value.ToString, 101), X, Y, 0)
        '        Else
        '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Observaciones: " + Session("rs").Fields("OBSERVACIONES").Value.ToString, X, Y, 0)
        '        End If

        '        Y = Y - 20

        '        Session("rs").movenext()
        '    Loop
        'End If

        'Session("Con").Close()

        ' cb.EndText()

        document.Close()

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= ConsultaSesionCCC(" + CStr(IDSesionComite) + ").pdf")
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


    Private Function RevisaFacultadesPLD() As Integer

        Dim PersmisoDigitalizacion As Integer = 0

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLD_FACULTADES"
        Session("rs") = Session("cmd").Execute()

        PersmisoDigitalizacion = Session("rs").Fields("DIGIT_ACTAS").Value.ToString()

        Session("Con").Close()

        Return PersmisoDigitalizacion

    End Function

End Class