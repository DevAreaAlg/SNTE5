Public Class CAP_EXP_APARTADO3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Pep/Funcionarios/Soicitud", "PEP/FUNCIONARIOS/PRELLENADO SOLICITUD-CONTRATO")

        If Not Me.IsPostBack Then
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("FOLIO"))
            Session("VENGODE") = "CAP_EXP_APARTADO3.ASPX"
            Documentos()
            ObtenerProducto()
            verifica_ppe_relfun()
            folderA(pnl_pre_sol, "up")

            If pnl_ppe.Visible Then
                folderA(pnl_ppe, "up")

                rad_cliente.Checked = True
                activa_cliente_ppe("Cliente")

                llena_parentesco(cmb_politica_parentesco.ID.ToString)
                Llenacategorias()
                llena_organos()
                llena_puestos(cmb_politca_organo.SelectedItem.Value)
                activa_puesto(cmb_politca_organo.SelectedItem.Value)
                carga_datos_ppe(Session("FOLIO"))
                llena_personas_politicas(Session("FOLIO"))

            End If

            If pnl_relfun.Visible And panel_relfun.Enabled Then
                folderA(pnl_relfun, "up")
                llena_miembros_consejo()
                llena_parentesco(cmb_consejo_parentesco.ID.ToString)
                llena_miembros_consejo_asignados()
            End If

        End If


        'ScriptManager1.RegisterPostBackControl(btn_DescargarPrellenadoCred)
    End Sub

    Private Sub verifica_ppe_relfun()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VERIFICA_PPE_RELFUN"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        pnl_ppe.Visible = IIf(Session("rs").Fields("PPE").Value = 1, True, False)
        pnl_relfun.Visible = IIf(Session("rs").Fields("RELFUN").Value = 1, True, False)
        'panel_relfun.Enabled = Not (pnl_ppe.Visible)
        Session("Con").Close()
    End Sub



    Private Sub ObtenerProducto()

        'Mostar los datos generales de un expediente: folio, nombre de afiliado y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"

        Session("rs") = Session("cmd").Execute()

        Session("PRODUCTO") = Session("rs").fields("PRODUCTO").value.ToString
        Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString
        Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString

        Session("Con").Close()

    End Sub

    Private Sub LlenarSolicitudAperturaCuenta()

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim MFCSG As String
        MFCSG = "SolCaptacion1Fisica" + ".pdf"


        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\Solicitudes\" + MFCSG) 'Session("AppPath").ToString()

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

        Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        With document
            .AddAuthor("Desarrollo - Infoquest")
            .AddCreationDate()
            .AddCreator("Infoquest - Solicitud de Captación")
            .AddSubject("Solicitud - " + Session("FOLIO"))
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Solicitud - " + Session("FOLIO"))
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Solicitud - " + Session("FOLIO"))
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

        ' step 4: we add content

        'bgnd.SetAbsolutePosition(0, 0)

        'document.Add(bgnd)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        'cb.SetFontAndSize(bf, 9)
        Dim X, Y As Single
        X = 0
        Y = 0
        'VOY POR LA INFORMACIÓN DEL PROSPECTO

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "  SEL_SUCURSAL"
        Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'SUCURSAL
            Y = 686
            X = 280
            cb.SetFontAndSize(bf, 13)
            Dim suc As String
            suc = Session("rs").Fields("NOMBRE").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, suc, X, Y, 0)

        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_GENERALES"
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Y = 645
            X = 485 '37
            cb.SetFontAndSize(bf, 8)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FIEL").Value.ToString, X, Y, 0)
        End If


        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'No. Cliente
            Y = 672
            X = 280
            Session("PERSONAID") = Session("rs").Fields("IDCLIENTE").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("IDCLIENTE").Value.ToString, X, Y, 0)

            'FECHA DE ALTA
            Y = 672
            X = 460
            Dim fecalt As String
            fecalt = Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, fecalt, X, Y, 0)
            fecalt = Nothing
            'bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            'Nombre Completo del Prospecto
            Y = 643
            X = 115 '37
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PROSPECTO").Value.ToString, X, Y, 0)


            'PAIS
            Y = 509
            X = 115
            Dim Pais As String
            Dim Estado As String
            Dim paisestado As String()

            paisestado = Session("rs").Fields("PAIS").Value.ToString.Split("-")


            Pais = paisestado.First.ToString
            Estado = paisestado.Last.ToString



            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Estado.ToString, X, Y, 0)

            X = 485
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Pais.ToString, X, Y, 0)


            Y = 584
            X = 485
            Dim Nac As String
            Dim Genti As String
            Dim Nacimento As String()

            Nacimento = Session("rs").Fields("PAIS").Value.ToString.Split("-")


            Nac = Nacimento.First.ToString
            Genti = Nacimento.Last.ToString

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Nac, X, Y, 0)

            'FECHA DE NAC
            Y = 494
            X = 115
            Dim fecnac As String
            fecnac = Session("rs").Fields("FECHANAC").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(6, 4)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, fecnac, X, Y, 0)
            fecnac = Nothing

            'SEXO
            Y = 524
            X = 300

            Dim sex As String
            sex = Session("rs").Fields("SEXO").Value.ToString
            Select Case Session("rs").Fields("SEXO").Value.ToString
                Case "H"
                    sex = "MASCULINO"
                Case "M"
                    sex = "FEMENINO"
            End Select
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, sex, X, Y, 0)

            'CURP
            Y = 494
            X = 300
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("CURP").Value.ToString, X, Y, 0)

            'RFC
            Y = 494
            X = 485
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("RFC").Value.ToString, X, Y, 0)

            'EDAD
            Y = 523
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("EDADES").Value.ToString, X, Y, 0)

            ''NOTAS
            'Y = 166
            'X = 75
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NOTAS").Value.ToString, X, Y, 0)

        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_DOMICILIO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'CALLE
            Y = 629
            X = 115

            Dim Domicilio As String
            Domicilio = Session("rs").Fields("CALLE").Value.ToString + "  " + Session("rs").Fields("NUMEXT").Value.ToString + "  " + Session("rs").Fields("NUMINT").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Domicilio, X, Y, 0)

            'ASENTAMIENTO
            Y = 615
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ASENTAMIENTO").Value.ToString, X, Y, 0)

            'MUNICIPIO
            Y = 599
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MUNICIPIO").Value.ToString, X, Y, 0)

            'ESTADO
            Y = 584
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ESTADO").Value.ToString, X, Y, 0)

            'REFRENCIAS_DOMICILIO
            Y = 568
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("REFRENCIAS_DOMICILIO").Value.ToString, X, Y, 0)




            'CODIGO POSTAL
            Y = 600
            X = 485
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("CP").Value.ToString, X, Y, 0)

            'TIPO DE VIVIENDA
            Y = 464
            X = 485
            Dim tipovivienda As String
            tipovivienda = Session("rs").Fields("TIPO_VIVIENDA").Value.ToString
            Select Case Session("rs").Fields("TIPO_VIVIENDA").Value.ToString
                Case "PRO"
                    tipovivienda = "PROPIA"
                Case "FAM"
                    tipovivienda = "FAMILIAR"
                Case "REN"
                    tipovivienda = "RENTADA"
                Case "PRE"
                    tipovivienda = "PRESTADA"
                Case "HIP"
                    tipovivienda = "HIPOTECADA"
                Case "PAG"
                    tipovivienda = "PAGANDOLA"
            End Select
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, tipovivienda, X, Y, 0)
            tipovivienda = Nothing

        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CONTACTO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then



            'EMAIL
            Y = 554
            X = 300
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("CORREO").Value.ToString, X, Y, 0)


            'TELEFONO
            Y = 554
            X = 115

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TELEFONO_PARTICULAR").Value.ToString, X, Y, 0)

            'TELEFONO OFI
            Y = 380
            X = 300
            Dim TEL_OFI As String
            TEL_OFI = Session("rs").Fields("TELEFONO_TRABAJO").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, TEL_OFI, X, Y, 0)




        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CIVILES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'CONYUGE
            Y = 479
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("CONYUGE").Value.ToString, X, Y, 0)

            'DEPENDIENTES
            Y = 464
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DEPENDIENTES").Value.ToString, X, Y, 0)

            'ESTADO CIVIL
            Y = 525
            X = 485

            Dim edocivil As String
            edocivil = Session("rs").Fields("ESTADOCIVIL").Value.ToString
            Select Case Session("rs").Fields("ESTADOCIVIL").Value.ToString
                Case "SOL"
                    edocivil = "SOLTERO(A)"
                Case "CAS"
                    edocivil = "CASADO(A)"
                Case "UNL"
                    edocivil = "UNION LIBRE"
                Case "DIV"
                    edocivil = "DIVORCIADO(A)"
                Case "VIU"
                    edocivil = "VIUDO(A)"
            End Select
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, edocivil, X, Y, 0)
            edocivil = Nothing


            'REGIMEN
            Y = 480
            X = 485

            Dim regimen As String
            regimen = Session("rs").Fields("REGIMEN_CONYUGAL").Value.ToString
            Select Case Session("rs").Fields("REGIMEN_CONYUGAL").Value.ToString
                Case "SEP"
                    regimen = "BIENES SEPARADOS"
                Case "MAN"
                    regimen = "BIENES MANCOMUNADOS"
                Case "OTR"
                    regimen = "OTRO"
                Case Else
                    regimen = ""

            End Select
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, regimen, X, Y, 0)
            edocivil = Nothing



        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_LABORALES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'PUESTO
            Y = 364
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PUESTO").Value.ToString, X, Y, 0)

            'GIRO DEL NEGOCIO
            Y = 409
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "", X, Y, 0)

            'EMPRESA
            Y = 424
            X = 115
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("EMPRESA").Value.ToString, X, Y, 0)

            'DOMICILIO EMPRESA
            Y = 393
            X = 115
            Dim DomicilioEmpresa As String
            DomicilioEmpresa = Session("rs").Fields("CALLE_EMPRESA").Value.ToString + "  " + Session("rs").Fields("NUMEXT_EMPRESA").Value.ToString + "  " + Session("rs").Fields("NUMINT_EMPRESA").Value.ToString + "  " + Session("rs").Fields("ASENTAMIENTO_EMPRESA").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, DomicilioEmpresa, X, Y, 0)

            'ANTIGUEDAD
            Y = 409
            X = 485
            Dim ant As String
            ant = Session("rs").Fields("ANTIGUEDAD").Value.ToString
            If ant = "1" Then
                ant = ant + " AÑO"
            Else
                ant = ant + " AÑOS"
            End If
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, ant, X, Y, 0)
            ant = Nothing

            'ESTADO
            Y = 378
            X = 115
            Dim ESTADO_EMPRESA As String
            ESTADO_EMPRESA = Session("rs").Fields("ESTADO_EMPRESA").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, ESTADO_EMPRESA, X, Y, 0)



            'ESTADO
            'Y = 379
            'X = 300
            'Dim TEL_EMPRESA As String
            'TEL_EMPRESA = Session("rs").Fields("NUMEXT_EMPRESA").Value.ToString + Session("rs").Fields("NUMINT_EMPRESA").Value.ToString
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, TEL_EMPRESA, X, Y, 0)


            'CP
            Y = 379
            X = 485
            Dim CP As String
            CP = Session("rs").Fields("CP").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CP, X, Y, 0)





        End If



        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INGRESO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'SUELDO
            Y = 363
            X = 485
            Dim SUELDO As String
            SUELDO = Session("rs").Fields("INGRESO").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, SUELDO, X, Y, 0)

        End If





        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_BENEFICIARIOS_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Y = 323
            X = 115

            Do While Not Session("rs").EOF

                'NOMBRE DE BENEFICIARIO
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NOMBREBENEF").Value.ToString, X, Y, 0)

                'PORCENTAJE DE BENEFICIARIO

                X = 485

                Dim Fechanac As String
                Fechanac = Left(Session("rs").Fields("FECHANAC").Value.ToString, 10)
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Fechanac, X, Y, 0)
                Fechanac = Nothing




                'RELACION DE BENEFICIARIO
                X = 115
                Y = Y - 15
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIRECCION").Value.ToString, X, Y, 0)


                X = 485
                ' Y = Y - 15
                Dim CP As String
                CP = Session("rs").Fields("CP").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CP, X, Y, 0)
                CP = Nothing


                'ESTADO
                X = 115
                Y = Y - 15
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ESTADO").Value.ToString, X, Y, 0)


                X = 485
                ' Y = Y - 15
                Dim porcentaje As String
                porcentaje = Session("rs").Fields("PORCENTAJEBENEF").Value.ToString + "%"
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, porcentaje, X, Y, 0)
                porcentaje = Nothing

                X = 300
                Dim TEL As String
                TEL = Session("rs").Fields("TELEFONOBENEF").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, TEL, X, Y, 0)
                TEL = Nothing



                Session("rs").movenext()
                Y = Y - 22
                X = 115
            Loop
        End If
        Session("Con").Close()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_TARJETA_PRELLENADO"


        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERSONAFLAG", Session("adVarChar"), Session("adParamInput"), 20, 0)
        Session("cmd").Parameters.Append(Session("parm"))


        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Y = 55
            X = 115

            'If (Y - 80) < 20 Then
            '    cb.EndText()
            '    X = 115
            '    Y = 700

            '    document.NewPage()
            '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Solicitudes\Sol_Captacion1FisicaBasic.pdf")

            '    cb = writer.DirectContent

            '    Solicitud = writer.GetImportedPage(Reader, 1)
            '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
            '    cb.BeginText()

            '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            '    cb.SetFontAndSize(bf, 8)

            '    'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "MANCOMUNADOS ", X, Y, 0)
            '    Y = Y - 12

            '    X = 115
            'Else

            '    'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "MANCOMUNADOS ", X, Y, 0)
            '    Y = Y - 12

            'End If



            Do While Not Session("rs").eof

                'If (Y - 80) < 10 Then
                '    cb.EndText()
                '    X = 115
                '    Y = 680

                '    document.NewPage()
                '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Solicitudes\Sol_Captacion1FisicaBasic.pdf")

                '    cb = writer.DirectContent

                '    Solicitud = writer.GetImportedPage(Reader, 1)
                '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
                '    cb.BeginText()

                '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                '    cb.SetFontAndSize(bf, 8)

                '    ' cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

                '    X = 115
                '    ' Y = Y - 15
                'End If
                'ready to draw text

                'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                '9 es el tamaño de la letra
                cb.SetFontAndSize(bf, 8)
                'X = 33  'X empieza de izquierda a derecha
                'Y = 180 'Y empieza de abajo hacia arriba



                'Y = Y + 18
                Dim nom_persona As String
                nom_persona = Session("rs").Fields("CLIENTE").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("CLIENTE").Value.ToString, X, Y, 0)
                X = X + 340


                Dim id_persona As String
                id_persona = Session("rs").Fields("IDCLIENTE").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("IDCLIENTE").Value.ToString, X, Y, 0)
                Y = Y - 15
                X = 115
                Session("rs").movenext()
            Loop
        End If
        'Session("Con").Close()




        cb.EndText()




        document.NewPage()

        MFCSG = "SolCaptacion2Fisica" + ".pdf"
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\Solicitudes\" + MFCSG)

        cb = writer.DirectContent
        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        cb.BeginText()
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)

        cb.EndText()
        document.Close()


        Session("Con").Close()

    End Sub





    'Private Sub PrellenadoContrato()

    '    'Comienza seccion de escritura del pdf 
    '    'Declara memory stream para salida
    '    Session("ms") = New System.IO.MemoryStream()

    '    'Crea un reader para la solictud

    '    Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")

    '    'Traigo el total de paginas
    '    Dim n As Integer = 0
    '    n = Reader.NumberOfPages

    '    'Traigo el tamaño de la primera pagina
    '    Dim psize As iTextSharp.text.Rectangle
    '    psize = Reader.GetPageSize(1)

    '    Dim width, height As Single
    '    width = psize.Width
    '    height = psize.Height

    '    'CREACION DE UN DOCUMENTO

    '    Dim document As New iTextSharp.text.Document(psize, 60, 60, 108, 108)

    '    With document
    '        .AddAuthor("Infoquest")
    '        .AddCreationDate()
    '        .AddCreator("Infoquest- Contrato")
    '        .AddSubject("Contrato - " + Session("FOLIO"))
    '        'Use the filename as the title... You can give it any title of course.        
    '        .AddTitle("Contrato - " + Session("FOLIO"))
    '        'Add keywords, whatever keywords you want to attach to it       
    '        .AddKeywords("Contrato - " + Session("FOLIO"))
    '        .Open()
    '    End With

    '    'CREACION DE UN WRITER QUE LEA EL DOCUMENTO

    '    Dim writer As iTextSharp.text.pdf.PdfWriter
    '    writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

    '    ' step 3: we open the document
    '    document.Open()
    '    Dim cb As iTextSharp.text.pdf.PdfContentByte
    '    cb = writer.DirectContent

    '    ' METO LA SOLICITUD ORIGINAL
    '    Dim Solicitud As iTextSharp.text.pdf.PdfImportedPage

    '    Solicitud = writer.GetImportedPage(Reader, 1)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    cb.BeginText()

    '    Dim bf As iTextSharp.text.pdf.BaseFont
    '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
    '    cb.SetFontAndSize(bf, 9)

    '    Dim X, Y As Single
    '    X = 485
    '    Y = 720

    '    Session("Con").Open()
    '    Session("rs") = CreateObject("ADODB.Recordset")
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
    '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("rs") = Session("cmd").Execute()
    '    If Not Session("rs").eof Then

    '        Session("PERSONAID") = Session("rs").Fields("IDCLIENTE").Value.ToString

    '        'NO. CLIENTE
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("IDCLIENTE").Value.ToString, X, Y, 0)

    '        Y = Y - 18

    '        'FOLIO DE EXPEDIENTE
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("FOLIO").ToString, X, Y, 0)

    '    End If

    '    Dim DomicilioCliente As String

    '    Session("rs") = CreateObject("ADODB.Recordset")
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_DATOS_DOMICILIO_PRELLENADO"
    '    Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("rs") = Session("cmd").Execute()
    '    If Not Session("rs").eof Then

    '        'CALLE, NUMERO EXTERIOR, NUMERO INTERIOR
    '        Y = 267
    '        X = 430
    '        DomicilioCliente = Session("rs").Fields("CALLE").Value.ToString + "  " + Session("rs").Fields("NUMEXT").Value.ToString + "  " + Session("rs").Fields("NUMINT").Value.ToString + ", " + Session("rs").Fields("ASENTAMIENTO").Value.ToString + " " + Session("rs").Fields("MUNICIPIO").Value.ToString + ", " + Session("rs").Fields("ESTADO").Value.ToString + " " + Session("rs").Fields("CP").Value.ToString
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, DomicilioCliente, X - 225, Y, 0)

    '    End If



    '    Session("Con").Close()

    '    cb.EndText()

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 2)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 3)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 4)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 5)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 6)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 7)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 8)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
    '    cb.BeginText()
    '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
    '    cb.SetFontAndSize(bf, 9)


    '    cb.EndText()






    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 9)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 10)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    cb.BeginText()
    '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
    '    cb.SetFontAndSize(bf, 9)

    '    If Session("TIPO_CAPTACION") <> "INVERSION" Then
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "N/A", 43, 235, 0)
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "N/A", 300, 128, 0)

    '    End If


    '    cb.EndText()

    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 11)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)



    '    document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\ContratoCaptacion.pdf")
    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 12)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    cb.BeginText()
    '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
    '    cb.SetFontAndSize(bf, 8)

    '    Session("Con").Open()
    '    Session("rs") = CreateObject("ADODB.Recordset")
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_DATOS_SUCURSAL_PRELLENADO"
    '    Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("rs") = Session("cmd").Execute()
    '    If Not Session("rs").eof Then
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MUNICIPIO").Value.ToString, 225, 678, 0)
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2), 410, 678, 0)
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES").Value.ToString, 450, 678, 0)
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHASIS").Value.ToString.Substring(8, 2), 543, 678, 0)

    '    End If

    '    Session("Con").Close()
    '    cb.EndText()


    '    document.NewPage()       'document.NewPage()
    '    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\CaratulaCaptacion.pdf")

    '    cb = writer.DirectContent
    '    Solicitud = writer.GetImportedPage(Reader, 1)
    '    cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)

    '    cb.BeginText()
    '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
    '    cb.SetFontAndSize(bf, 9)



    '    Session("Con").Open()
    '    Session("rs") = CreateObject("ADODB.Recordset")
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_DATOS_CAPTACION_PRELLENADO"
    '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("rs") = Session("cmd").Execute()
    '    If Not Session("rs").eof Then


    '        Session("PRODUCTO") = Session("rs").Fields("PRODUCTO").Value.ToString
    '        Session("TIPO_CAPTACION") = Session("rs").Fields("TIPO").Value.ToString
    '        Session("TASA") = Session("rs").Fields("TASA_NORMAL").Value.ToString
    '        Session("GAT") = Session("rs").Fields("GAT_VISTA").Value.ToString
    '        Session("TIPO_TASA") = Session("rs").Fields("TIPO_TASA").Value.ToString
    '        Session("INDICE_VISTA") = Session("rs").Fields("INDICE_VISTA").Value.ToString


    '        'PRODUCTO
    '        Y = 653
    '        X = 204
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("PRODUCTO"), X, Y, 0)

    '        'TIPO OPERACION
    '        Y = Y - 15
    '        X = 150
    '        'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("TIPO_CAPTACION"), X, Y, 0)

    '        cb.SetFontAndSize(bf, 12)

    '        'TASA NORMAL
    '        Y = Y - 85
    '        X = 120

    '        If Session("TIPO_TASA") = "FIJA" Then
    '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("TASA"), X, Y, 0)
    '        Else
    '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "INDICE: " + Session("INDICE_VISTA"), X, Y, 0)
    '            Y = Y - 15
    '            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, " PUNTOS: " + Session("TASA_NORMAL"), X, Y, 0)
    '        End If


    '        'GAD
    '        X = X + 110
    '        Y = Y + 11
    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("GAT"), X, Y, 0)


    '    End If

    '    Session("Con").Close()


    '    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
    '    cb.SetFontAndSize(bf, 8)

    '    Session("Con").Open()
    '    Session("rs") = CreateObject("ADODB.Recordset")
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_DATOS_CREDITO_ACTORES_EXTRA_PRELLENADO"
    '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("rs") = Session("cmd").Execute()
    '    Dim avales As Integer = 0
    '    Dim lado As String = "I"
    '    If Not Session("rs").eof Then

    '        Session("Nombre") = Session("rs").Fields("NOMBRE").Value.ToString



    '        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("Nombre").ToString, 200, 269, 0)


    '    End If
    '    Session("Con").Close()

    '    cb.EndText()
    '    document.Close()

    'End Sub

    'Private Sub DelHDFile(ByVal File As String)

    '    Dim fi As New System.IO.FileInfo(File)
    '    If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
    '        fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
    '    End If

    '    System.IO.File.Delete(File)

    'End Sub

    'Protected Sub btn_DescargarPrellenadoCred_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_DescargarPrellenadoCred.Click

    '    'If Session("TIPOPER") = "F" Then
    '    LlenarSolicitudAperturaCuenta()
    '    'Else
    '    '    LlenarSolicitudCreditoMoral()
    '    'End If

    '    ActualizarSemaforo()

    '    With Response
    '        .BufferOutput = True
    '        .ClearContent()
    '        .ClearHeaders()
    '        .ContentType = "application/octet-stream"
    '        .AddHeader("Content-disposition",
    '                   "attachment; filename= SOLICITUD CAPTACION.pdf")
    '        Response.Cache.SetNoServerCaching()
    '        Response.Cache.SetNoStore()
    '        Response.Cache.SetMaxAge(System.TimeSpan.Zero)

    '        Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

    '        .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
    '        .End()
    '        .Flush()
    '    End With

    'End Sub

    Public Function RandKey(ByVal longitud As Integer) As String

        Dim strRandomString As String
        strRandomString = ""
        Dim RndNum As New Random()
        Dim intCounter As Integer
        For intCounter = 1 To longitud
            Select Case RndNum.Next(0, 35)
                Case 0 : strRandomString &= "A"
                Case 1 : strRandomString &= "B"
                Case 2 : strRandomString &= "C"
                Case 3 : strRandomString &= "D"
                Case 4 : strRandomString &= "E"
                Case 5 : strRandomString &= "F"
                Case 6 : strRandomString &= "G"
                Case 7 : strRandomString &= "H"
                Case 8 : strRandomString &= "I"
                Case 9 : strRandomString &= "J"
                Case 10 : strRandomString &= "K"
                Case 11 : strRandomString &= "L"
                Case 12 : strRandomString &= "M"
                Case 13 : strRandomString &= "N"
                Case 14 : strRandomString &= "O"
                Case 15 : strRandomString &= "P"
                Case 16 : strRandomString &= "Q"
                Case 17 : strRandomString &= "R"
                Case 18 : strRandomString &= "S"
                Case 19 : strRandomString &= "T"
                Case 20 : strRandomString &= "U"
                Case 21 : strRandomString &= "V"
                Case 22 : strRandomString &= "W"
                Case 23 : strRandomString &= "X"
                Case 24 : strRandomString &= "Y"
                Case 25 : strRandomString &= "Z"
                Case 26 : strRandomString &= "0"
                Case 27 : strRandomString &= "1"
                Case 28 : strRandomString &= "2"
                Case 29 : strRandomString &= "3"
                Case 30 : strRandomString &= "4"
                Case 31 : strRandomString &= "5"
                Case 32 : strRandomString &= "6"
                Case 33 : strRandomString &= "7"
                Case 34 : strRandomString &= "8"
                Case 35 : strRandomString &= "9"
            End Select
        Next intCounter
        Return strRandomString

    End Function

    Private Sub ActualizarSemaforo()
        'Mostar los datos generales de un expediente: folio, nombre de afiliado y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFEXPCAP_SEMAFORO_APARTADO3"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub



    Private Sub llena_parentesco(ByVal id As String)
        If id = "cmb_consejo_parentesco" Then
            cmb_consejo_parentesco.Items.Clear()
            cmb_consejo_parentesco.Items.Add(New ListItem("ELIJA", "-1"))
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_TIPO_RELACION_ALTA_ED_PERSONA"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_consejo_parentesco.Items.Add(New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString))
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
        If id = "cmb_politica_parentesco" Then
            cmb_politica_parentesco.Items.Clear()
            cmb_politica_parentesco.Items.Add(New ListItem("ELIJA", "-1"))
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_TIPO_RELACION_ALTA_ED_PERSONA"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_politica_parentesco.Items.Add(New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString))
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If
    End Sub

    Private Sub Llenacategorias()
        cmb_categoria.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_categoria.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_CATEGORIA_PPE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATEGORIA").Value.ToString, Session("rs").Fields("CLAVE").Value.ToString)
            cmb_categoria.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llena_organos()
        cmb_politca_organo.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_politca_organo.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_ORGANOS_PPE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ORGANO").Value, Session("rs").Fields("ORGANO").Value)
            cmb_politca_organo.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llena_miembros_consejo()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_miembros_consejo.Items.Clear()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_CONSEJO"
        Session("rs") = Session("cmd").Execute()
        cmb_miembros_consejo.Items.Add(elija)
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_miembros_consejo.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub activa_cliente_ppe(ByVal mode As String)
        If mode = "Cliente" Then
            txt_politica_nombre1.Enabled = False
            txt_politica_paterno.Enabled = False
            txt_politica_nombre2.Enabled = False
            txt_politica_materno.Enabled = False
            cmb_politica_parentesco.Enabled = False
            req_politica_nombre1.Enabled = False
            req_politica_paterno.Enabled = False
            req_politica_parentesco.Enabled = False
            RequiredFieldValidatorUFecSer.Enabled = False
            txtUFecSer.Enabled = False
            cmb_categoria.AutoPostBack = True
        Else
            txt_politica_nombre1.Enabled = True
            txt_politica_paterno.Enabled = True
            txt_politica_nombre2.Enabled = True
            txt_politica_materno.Enabled = True
            cmb_politica_parentesco.Enabled = True
            req_politica_nombre1.Enabled = True
            req_politica_paterno.Enabled = True
            req_politica_parentesco.Enabled = True
            cmb_categoria.AutoPostBack = True
        End If
    End Sub

    Private Sub clear_datos()
        llena_parentesco(cmb_politica_parentesco.ID.ToString)
        Llenacategorias()
        llena_organos()
        cmb_nivel.ClearSelection()
        cmb_nivel.Items.FindByValue("-1").Selected = True
        txt_descripcion.Text = ""
        txtUFecSer.Text = ""
        cmb_politca_puesto.Items.Clear()
        txt_politica_nombre1.Text = ""
        txt_politica_nombre2.Text = ""
        txt_politica_paterno.Text = ""
        txt_politica_materno.Text = ""
        txt_descripcion.Text = ""
    End Sub

    Protected Sub rad_cliente_CheckedChanged(sender As Object, e As EventArgs) Handles rad_cliente.CheckedChanged
        clear_datos()
        If rad_cliente.Checked Then
            activa_cliente_ppe("Cliente")
            carga_datos_ppe(Session("FOLIO"))
        Else
            activa_puesto(cmb_categoria.SelectedItem.Value)
            activa_cliente_ppe("Familiar")
        End If
    End Sub

    Protected Sub rad_familiar_CheckedChanged(sender As Object, e As EventArgs) Handles rad_familiar.CheckedChanged
        clear_datos()
        If rad_familiar.Checked Then
            activa_puesto(cmb_categoria.SelectedItem.Value)
            activa_cliente_ppe(rad_familiar.Text)
        Else
            activa_cliente_ppe(rad_cliente.Text)
            carga_datos_ppe(Session("FOLIO"))
        End If
    End Sub

    Private Sub llena_personas_politicas(ByVal folio As Integer)

        LlenaFamiliaPPE(Session("PERSONAID"))

    End Sub

    Private Sub carga_datos_ppe(ByVal folio As Integer)
        Dim id_puesto As Integer = 0
        Dim desc_puesto As String = ""
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PPE_CLIENTE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            txt_descripcion.Text = Session("rs").Fields("DESCRIPCION").Value
            cmb_categoria.ClearSelection()
            cmb_categoria.SelectedValue = (Session("rs").Fields("CVE").Value.ToString)
            cmb_politca_organo.ClearSelection()
            cmb_politca_organo.SelectedValue = (Session("rs").Fields("ORGANO").Value.ToString)
            cmb_politca_puesto.ClearSelection()
            Dim idedo As String = Session("rs").Fields("IDPUESTO").Value
            Dim item_puesto As New ListItem(Session("rs").Fields("PUESTO").Value.ToString, idedo)
            cmb_politca_puesto.Items.Add(item_puesto)

            cmb_nivel.ClearSelection()
            cmb_nivel.Items.FindByValue(Session("rs").Fields("NIVEL").Value).Selected = True

        End If
        Session("Con").Close()

        activa_puesto(cmb_categoria.SelectedItem.Value)
    End Sub

    Protected Sub dag_politica_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_politica.ItemCommand



        If (e.CommandName = "ELIMINAR") Then
            'IHG - 2017-08-16 Nueva definicion PPE
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERPOL", Session("adVarChar"), Session("adParamInput"), 10, CInt(e.Item.Cells(0).Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_CNFEXP_PPE"
            Session("cmd").Execute()
            Session("Con").Close()
            LlenaFamiliaPPE(Session("PERSONAID"))


        End If
    End Sub


    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click

        Dim cFigura As String
        Dim nIdFigura As Integer
        nIdFigura = 0
        cFigura = ""
        If rad_cliente.Checked Then
            cFigura = "CLIENTE"
        Else
            cFigura = "FAMILIAR CLIENTE"
        End If

        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FIGURA", Session("adVarChar"), Session("adParamInput"), 100, cFigura)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CNFEXP_FIGURA_PPE"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then
                nIdFigura = Session("rs").Fields("IDFIGURA").Value
            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try


        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_FIGURA", Session("adVarChar"), Session("adParamInput"), 10, nIdFigura)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CVECATEGORIA", Session("adVarChar"), Session("adParamInput"), 20, cmb_categoria.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_PUESTO", Session("adVarChar"), Session("adParamInput"), 10, IIf(cmb_politca_puesto.Enabled, cmb_politca_puesto.SelectedValue, -1))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NIVEL", Session("adVarChar"), Session("adParamInput"), 10, cmb_nivel.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ULTIMA_FECHA_SERVICIO", Session("adVarChar"), Session("adParamInput"), 10, txtUFecSer.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 300, txt_descripcion.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_PARENTESCO", Session("adVarChar"), Session("adParamInput"), 10, IIf(cmb_politica_parentesco.Enabled, cmb_politica_parentesco.SelectedValue, -1))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_nombre1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_nombre2.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFEXP_PPEXP"
            Session("rs") = Session("cmd").Execute()

            If Session("rs").Fields("EXISTE").Value = 0 Then
                lbl_estatus_politica.Text = "Guardado correctamente"
            Else
                lbl_estatus_politica.Text = "Ya existe un figura capturada "
            End If

        Catch ex As Exception
            lbl_estatus_politica.Text = "Error en alta"
        Finally

            Session("Con").Close()
        End Try

        If rad_cliente.Checked Then
            carga_datos_ppe(Session("FOLIO"))
        End If

        clear_datos()
        LlenaFamiliaPPE(Session("PERSONAID"))


    End Sub


    Private Sub LlenaFamiliaPPE(ByVal nIdPersona As Integer) 'IHG 2017-08-16 Nueva definicion PPE
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_politica As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, nIdPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LISTA", Session("adVarChar"), Session("adParamInput"), 1, "L")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_PPE_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_politica, Session("rs"))
        Session("Con").Close()
        If dt_politica.Rows.Count > 0 Then
            dag_politica.Visible = True
            dag_politica.DataSource = dt_politica
            dag_politica.DataBind()
        Else
            dag_politica.Visible = False
        End If
    End Sub

    Private Sub activa_puesto(ByVal mode As String)
        lbl_estatus_politica.Text = ""
        If mode <> "POLEXP" Then
            cmb_politca_puesto.Enabled = False
            cmb_politca_organo.Enabled = False
            cmb_nivel.Enabled = False
            req_politica_puesto.Enabled = False
            req_politica_organo.Enabled = False
            RequiredFieldValidator_nivel.Enabled = False
            llena_organos()
            llena_puestos(-1)
            cmb_nivel.ClearSelection()
            cmb_nivel.Items.FindByValue("-1").Selected = True
            'txt_descripcion.Text = ""
            txtUFecSer.Enabled = False
            RequiredFieldValidatorUFecSer.Enabled = False
            txt_descripcion.Enabled = False
            RequiredFieldValidator_descripcion.Enabled = False
        Else
            cmb_politca_puesto.Enabled = True
            cmb_politca_organo.Enabled = True
            cmb_nivel.Enabled = True
            req_politica_puesto.Enabled = True
            req_politica_organo.Enabled = True
            RequiredFieldValidator_nivel.Enabled = True
            'txt_descripcion.Text = ""
            txtUFecSer.Enabled = True
            RequiredFieldValidatorUFecSer.Enabled = True
            txt_descripcion.Enabled = True
            RequiredFieldValidator_descripcion.Enabled = True
        End If
    End Sub

    Protected Sub cmb_categoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_categoria.SelectedIndexChanged
        activa_puesto(cmb_categoria.SelectedItem.Value)
    End Sub

    Private Sub llena_puestos(ByVal param As String)
        cmb_politca_puesto.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_politca_puesto.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ORGANO", Session("adVarChar"), Session("adParamInput"), 300, param)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PUESTOS_PPE"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PUESTO").Value, Session("rs").Fields("IDPUESTO").Value.ToString)

            cmb_politca_puesto.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub cmb_politca_organo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_politca_organo.SelectedIndexChanged
        lbl_estatus_politica.Text = ""
        llena_puestos(cmb_politca_organo.SelectedItem.Value)
    End Sub

    Protected Sub cmb_miembros_consejo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_miembros_consejo.SelectedIndexChanged
        If cmb_miembros_consejo.SelectedItem.Value <> "-1" Then
            lbl_consejo_parentesco.Visible = True
            cmb_consejo_parentesco.Visible = True
            req_consejo_parentesco.Enabled = True
            llena_parentesco(cmb_consejo_parentesco.ID)
            lnk_agregar_consejo.Visible = True
        Else
            lbl_consejo_parentesco.Visible = False
            cmb_consejo_parentesco.Visible = False
            req_consejo_parentesco.Enabled = False
            lnk_agregar_consejo.Visible = False
        End If
    End Sub

    Private Sub llena_miembros_consejo_asignados()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_miembros_consejo As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERSONA_FISICA_RELACIONES_CONSEJO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_miembros_consejo, Session("rs"))
        Session("Con").Close()
        If dt_miembros_consejo.Rows.Count > 0 Then
            dag_miembros_consejo.Visible = True
            dag_miembros_consejo.DataSource = dt_miembros_consejo
            dag_miembros_consejo.DataBind()
        Else
            dag_miembros_consejo.Visible = False
        End If
    End Sub

    Private Sub guardar_miembro_consejo()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMIEMBRO_CONSEJO", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_miembros_consejo.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPARENTESCO", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_consejo_parentesco.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PERSONA_FISICA_RELACION_CONSEJO"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("RES").Value = "1" Then
            Session("Con").Close()
            lbl_estatus_relfun.Text = ""
            llena_miembros_consejo_asignados()
        Else
            lbl_estatus_relfun.Text = "Error: Ya existe una relación creada con los datos anteriores, verifique"
            Session("Con").Close()
        End If
    End Sub

    Protected Sub lnk_agregar_consejo_Click(sender As Object, e As EventArgs) Handles lnk_agregar_consejo.Click
        guardar_miembro_consejo()
        llena_miembros_consejo_asignados()
        llena_miembros_consejo()
        cmb_consejo_parentesco.Visible = False
        req_consejo_parentesco.Enabled = False
        lnk_agregar_consejo.Visible = False
        lbl_consejo_parentesco.Visible = False
        pnl_pre_sol.Visible = True
        folderA(pnl_relfun, "up")
    End Sub

    Protected Sub dag_miembros_consejo_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_miembros_consejo.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDMIEMBRO_CONSEJO", Session("adVarChar"), Session("adParamInput"), 10, CInt(e.Item.Cells(0).Text))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_BAJA_RELACION_CONSEJO"
            Session("cmd").Execute()
            Session("Con").Close()
            llena_miembros_consejo_asignados()
        End If
    End Sub

    'Protected Sub btn_skip_Click(sender As Object, e As EventArgs) Handles btn_skip.Click
    '    activa_cliente_ppe("Cliente")
    '    Dim perpolid As Integer
    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_nombre1.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("NOMBRE2", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_nombre2.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_paterno.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 300, txt_politica_materno.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 11, -1)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDPARENTESCO", Session("adVarChar"), Session("adParamInput"), 11, CInt(cmb_politica_parentesco.SelectedItem.Value))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "INS_PERSONA_FISICA_RELACION_POLITICA"
    '    Session("rs") = Session("cmd").Execute()
    '    perpolid = Session("rs").Fields("IDPERPOL").Value
    '    Session("Con").Close()
    '    txt_politica_nombre1.Text = ""
    '    txt_politica_nombre2.Text = ""
    '    txt_politica_paterno.Text = ""
    '    txt_politica_materno.Text = ""

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("IDPERPOL", Session("adVarChar"), Session("adParamInput"), 11, perpolid)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("CVE", Session("adVarChar"), Session("adParamInput"), 50, "SINREG")
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 300, txt_descripcion.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("NIVEL", Session("adVarChar"), Session("adParamInput"), 11, CInt(cmb_nivel.SelectedItem.Value))
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 15, 0)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "INS_CNFEXP_PPE"
    '    Session("cmd").Execute()
    '    Session("Con").Close()

    '    lbl_estatus_politica.Text = "Guardado correctamente"
    '    clear_datos()
    '    llena_personas_politicas(Session("FOLIO"))
    '    llena_parentesco(cmb_politica_parentesco.ID)
    '    Llenacategorias()
    '    llena_organos()
    '    carga_datos_ppe(Session("FOLIO"))

    '    If rad_cliente.Checked Then
    '        carga_datos_ppe(Session("FOLIO"))
    '    End If
    '    If pnl_relfun.Visible = True Then
    '        folderA(pnl_ppe, "up")
    '        panel_relfun.Enabled = True
    '        folderA(pnl_relfun, "down")
    '        llena_miembros_consejo()
    '    Else
    '        pnl_relfun.Visible = False
    '        pnl_pre_sol.Visible = True
    '        folderA(pnl_ppe, "up")
    '        folderA(pnl_pre_sol, "down")
    '    End If

    'End Sub

    'Protected Sub btn_skip_relfun_Click(sender As Object, e As EventArgs) Handles btn_skip_relfun.Click
    '    pnl_pre_sol.Visible = True
    '    folderA(pnl_relfun, "up")
    'End Sub

    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "none")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #C0CDD5")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub


    Protected Sub btn_docs_Click(sender As Object, e As EventArgs) Handles btn_docs.Click
        Dim cReca As String
        cReca = ""
        ActualizarSemaforo()
        Select Case cmb_documento.SelectedItem.Value
            Case "SOLICITUD"
                cReca = "RECA: XXXXXX"
                If Session("TIPOPER") = "F" Then
                    GeneraSolicitudAC_PF("SolicitudAperturaPF", cReca)
                Else

                    GenerarSolicitudAC_PM("SolicitudAperturaPM", cReca)
                End If

            Case "CONTRATO"
                cReca = "RECA: 13940-003-028321/01-05994-1117"
                GeneraContrato("Contrato", cReca)

            Case Else
        End Select

    End Sub

    Private Sub Documentos()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_documento.Items.Clear()
        cmb_documento.Items.Add(elija)
        Dim item As New ListItem("SOLICITUD", "SOLICITUD")
        cmb_documento.Items.Add(item)
        Dim itemContrato As New ListItem("CONTRATO", "CONTRATO")
        cmb_documento.Items.Add(itemContrato)
    End Sub


#Region "Solicitud PF"
    Private Sub GeneraSolicitudAC_PF(ByVal cNomArchivo As String, cReca As String)
        Dim lcUrl As String = cNomArchivo + ".docx"
        Dim NewDocName As String = cNomArchivo + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        Dim cPath As String = Session("APPATH") + "\Word\"
        Dim cPathNewDoc As String = cPath + NewDocName + ".docx"

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPath + lcUrl)
            Try
                worddoc.SaveAs(cPathNewDoc)
                Session("Con").Open()
                ObtieneInfoSol(NewDocName, cPath, cPathNewDoc, cReca)
            Catch ex As Exception
            Finally
                Session("Con").Close()
            End Try
        End Using

    End Sub

    Private Sub ObtieneInfoSol(ByVal NewDocName As String, ByVal cPath As String, ByVal cPathNewDoc As String, cReca As String)

        Dim cSuc, cFiel, cFechaAlta, cNombreCte, cPais, cEstadoNac, cDomicilioEmp, cFechaNac, cSexo, cEscolaridad, cNombrePRE As String
        Dim cCURP, cRFC, cCalle, cColonia, cMunicipio, cEstado, cRef, cCP, cTipoViv, cCorreo, cTel, cTelOfi, cJefe As String
        Dim cNomConyuge, cEstadoCivil, cRegimen, cPuesto, cAntiguedadEmp, cEstadoEmp, cCPEmp, cEmpresa, cMunEmp, cGiroEmp As String
        Dim cNombrePR, cSPR, cNPR, cSPRE, cNPRE, cNacionalidad, cTPais() As String
        Dim nEdad, nDependientes, nIngreso, nAntDom, nReg, nCte As Integer

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPathNewDoc)
            Try
                cSuc = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_SUCURSAL"
                Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cSuc = Session("rs").Fields("NOMBRE").Value.ToString
                End If
                worddoc.ReplaceText("{*SUCURSAL*}", cSuc, False, RegexOptions.None)
                worddoc.ReplaceText("{*NUMERO CLIENTE*}", Session("PERSONAID"), False, RegexOptions.None)
                worddoc.ReplaceText("{*FOLIO*}", Session("FOLIO"), False, RegexOptions.None)
                worddoc.ReplaceText("{*RECA*}", cReca, False, RegexOptions.None)

                cFiel = ""
                cEscolaridad = ""
                cGiroEmp = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_PERSONA_FISICA_GENERALES"
                Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cFiel = Session("rs").Fields("MSTGENERALESFIEL").Value
                    Select Case Session("rs").Fields("GRADOACADEMICO").Value
                        Case "PRI"
                            cEscolaridad = "PRIMARIA"
                        Case "SEC"
                            cEscolaridad = "SECUNDARIA"
                        Case "BAC"
                            cEscolaridad = "BACHILLERATO"
                        Case "TEC"
                            cEscolaridad = "TECNICA/COMERCIAL"
                        Case "LIC"
                            cEscolaridad = "LICENCIATURA"
                        Case "ESP"
                            cEscolaridad = "ESPECIALIDAD"
                        Case "MAE"
                            cEscolaridad = "MAESTRIA"
                        Case "DOC"
                            cEscolaridad = "DOCTORADO"
                        Case Else
                            cEscolaridad = "NINGUNO"
                    End Select
                    cGiroEmp = Session("rs").Fields("ACTIVIDAD").Value
                End If
                worddoc.ReplaceText("{*FIEL*}", cFiel, False, RegexOptions.None)
                worddoc.ReplaceText("{*ESCOLARIDAD*}", cEscolaridad, False, RegexOptions.None)
                worddoc.ReplaceText("{*GIRO EMPRESA*}", cGiroEmp, False, RegexOptions.None)
                worddoc.Save()

                cFechaAlta = ""
                cNombreCte = ""
                cPais = ""
                cEstadoNac = ""
                cFechaNac = ""
                cSexo = ""
                cCURP = ""
                cRFC = ""
                nEdad = 0
                cNacionalidad = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    If Session("rs").Fields("FECHA_ALTA").Value.ToString <> "1900-01-01" Then
                        cFechaAlta = Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(6, 4)
                    End If
                    cNombreCte = Session("rs").Fields("PROSPECTO").Value
                    cPais = Session("rs").Fields("PAIS").Value
                    cTPais = cPais.Split("-")
                    cPais = cTPais.First
                    cEstadoNac = cTPais.Last

                    If Session("rs").Fields("FECHANAC").Value.ToString <> "1900-01-01" Then
                        cFechaNac = Session("rs").Fields("FECHANAC").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(6, 4)
                    End If
                    cSexo = Session("rs").Fields("SEXO").Value
                    If cSexo = "H" Then
                        cSexo = "MASCULINO"
                    Else
                        cSexo = "FEMENINO"
                    End If
                    cCURP = Session("rs").Fields("CURP").Value
                    nEdad = 0
                    If Not IsDBNull(Session("rs").Fields("EDADES").Value) Then
                        nEdad = Session("rs").Fields("EDADES").Value
                    End If
                    cNacionalidad = Session("rs").Fields("NACIONALIDAD").Value
                    Dim personaCargar As New Persona
                    cRFC = personaCargar.Revisa_RFC(Session("rs").Fields("RFC").Value, Session("RFCMENORDEFAULT"), Session("rs").Fields("FECHANAC").Value.ToString, Session("FechaSis"))
                End If

                worddoc.ReplaceText("{*FECHA*}", cFechaAlta, False, RegexOptions.None)
                worddoc.ReplaceText("{*NOMBRE CLIENTE*}", cNombreCte, False, RegexOptions.None)
                worddoc.ReplaceText("{*LUGAR NACIMIENTO*}", cEstadoNac, False, RegexOptions.None)
                worddoc.ReplaceText("{*FECHA NACIMIENTO*}", cFechaNac, False, RegexOptions.None)
                worddoc.ReplaceText("{*PAIS NAC*}", cPais, False, RegexOptions.None)
                worddoc.ReplaceText("{*NACIONALIDAD*}", cNacionalidad, False, RegexOptions.None)
                worddoc.ReplaceText("{*SEXO*}", cSexo, False, RegexOptions.None)
                worddoc.ReplaceText("{*CURP*}", cCURP, False, RegexOptions.None)
                worddoc.ReplaceText("{*RFC*}", cRFC, False, RegexOptions.None)
                worddoc.ReplaceText("{*EDAD*}", nEdad.ToString, False, RegexOptions.None)
                worddoc.Save()

                cCalle = ""
                cColonia = ""
                cMunicipio = ""
                cEstado = ""
                cRef = ""
                cCP = ""
                cTipoViv = ""
                nAntDom = 0
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_DOMICILIO_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cCalle = Session("rs").Fields("CALLE").Value + "  " + Session("rs").Fields("NUMEXT").Value.ToString + "  " + Session("rs").Fields("NUMINT").Value.ToString
                    cColonia = Session("rs").Fields("ASENTAMIENTO").Value
                    cMunicipio = Session("rs").Fields("MUNICIPIO").Value
                    cEstado = Session("rs").Fields("ESTADO").Value
                    cRef = Session("rs").Fields("REFRENCIAS_DOMICILIO").Value
                    cCP = Session("rs").Fields("CP").Value.ToString
                    nAntDom = Session("rs").Fields("ANTIGUEDAD_DOMICILIO").Value
                    Select Case Session("rs").Fields("TIPO_VIVIENDA").Value.ToString
                        Case "PRO"
                            cTipoViv = "PROPIA"
                        Case "FAM"
                            cTipoViv = "FAMILIAR"
                        Case "REN"
                            cTipoViv = "RENTADA"
                        Case "PRE"
                            cTipoViv = "PRESTADA"
                        Case "HIP"
                            cTipoViv = "HIPOTECADA"
                        Case "PAG"
                            cTipoViv = "PAGANDOLA"
                    End Select
                End If
                worddoc.ReplaceText("{*CALLE*}", cCalle, False, RegexOptions.None)
                worddoc.ReplaceText("{*COLONIA*}", cColonia, False, RegexOptions.None)
                worddoc.ReplaceText("{*MUNICIPIO*}", cMunicipio, False, RegexOptions.None)
                worddoc.ReplaceText("{*ESTADO*}", cEstado, False, RegexOptions.None)
                worddoc.ReplaceText("{*CP*}", cCP, False, RegexOptions.None)
                worddoc.ReplaceText("{*TIPO VIVIENDA*}", cTipoViv, False, RegexOptions.None)
                worddoc.ReplaceText("{*REFERENCIAS*}", cRef, False, RegexOptions.None)
                worddoc.ReplaceText("{*ANTIG DOM*}", nAntDom.ToString, False, RegexOptions.None)
                worddoc.Save()

                cCorreo = ""
                cTel = ""
                cTelOfi = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_CONTACTO_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cCorreo = Session("rs").Fields("CORREO").Value
                    cTel = Session("rs").Fields("TELEFONO_MOVIL").Value.ToString + " " + Session("rs").Fields("TELEFONO_PARTICULAR").Value.ToString
                    cTelOfi = Session("rs").Fields("TELEFONO_TRABAJO").Value.ToString
                End If
                worddoc.ReplaceText("{*CORREO*}", cCorreo, False, RegexOptions.None)
                worddoc.ReplaceText("{*TELEFONO*}", cTel, False, RegexOptions.None)
                worddoc.ReplaceText("{*TEL EMPRESA*}", cTelOfi, False, RegexOptions.None)

                cNomConyuge = ""
                cEstadoCivil = ""
                cRegimen = ""
                nDependientes = 0
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_CIVILES_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cNomConyuge = Session("rs").Fields("CONYUGE").Value.ToString
                    nDependientes = Session("rs").Fields("DEPENDIENTES").Value
                    Select Case Session("rs").Fields("ESTADOCIVIL").Value
                        Case "SOL"
                            cEstadoCivil = "SOLTERO(A)"
                        Case "CAS"
                            cEstadoCivil = "CASADO(A)"
                        Case "UNL"
                            cEstadoCivil = "UNION LIBRE"
                        Case "DIV"
                            cEstadoCivil = "DIVORCIADO(A)"
                        Case "VIU"
                            cEstadoCivil = "VIUDO(A)"
                    End Select
                    Select Case Session("rs").Fields("REGIMEN_CONYUGAL").Value
                        Case "SEP"
                            cRegimen = "BIENES SEPARADOS"
                        Case "MAN"
                            cRegimen = "BIENES MANCOMUNADOS"
                        Case "OTR"
                            cRegimen = "OTRO"
                        Case Else
                            cRegimen = ""

                    End Select
                End If

                worddoc.ReplaceText("{*NOMBRE CONYUGE*}", cNomConyuge, False, RegexOptions.None)
                worddoc.ReplaceText("{*ESTADO CIVIL*}", cEstadoCivil, False, RegexOptions.None)
                worddoc.ReplaceText("{*REGIMEN*}", cRegimen, False, RegexOptions.None)
                worddoc.ReplaceText("{*NO DEPENDIENTES*}", nDependientes.ToString, False, RegexOptions.None)
                worddoc.Save()

                cPuesto = ""
                cEmpresa = ""
                cDomicilioEmp = ""
                cAntiguedadEmp = ""
                cEstadoEmp = ""
                cCPEmp = ""
                cMunEmp = ""
                cJefe = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_LABORALES_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cPuesto = Session("rs").Fields("PUESTO").Value
                    cEmpresa = Session("rs").Fields("EMPRESA").Value
                    cDomicilioEmp = Session("rs").Fields("CALLE_EMPRESA").Value + "  " + Session("rs").Fields("NUMEXT_EMPRESA").Value.ToString + "  " + Session("rs").Fields("NUMINT_EMPRESA").Value.ToString + "  " + Session("rs").Fields("ASENTAMIENTO_EMPRESA").Value
                    cAntiguedadEmp = Session("rs").Fields("ANTIGUEDAD").Value.ToString + " AÑOS"
                    If Session("rs").Fields("ANTIGUEDAD").Value <= 1 Then
                        cAntiguedadEmp = Session("rs").Fields("ANTIGUEDAD").Value.ToString + " AÑO"
                    End If
                    cEstadoEmp = Session("rs").Fields("ESTADO_EMPRESA").Value
                    cCPEmp = Session("rs").Fields("CP").Value
                    cMunEmp = Session("rs").Fields("MUNICIPIO_EMPRESA").Value
                    cJefe = Session("rs").Fields("NOMBRE_JEFE").Value
                End If
                worddoc.ReplaceText("{*PUESTO*}", cPuesto, False, RegexOptions.None)
                worddoc.ReplaceText("{*EMPRESA*}", cEmpresa, False, RegexOptions.None)
                worddoc.ReplaceText("{*DOM EMPRESA*}", cDomicilioEmp, False, RegexOptions.None)
                worddoc.ReplaceText("{*ANTIG EMPRESA*}", cAntiguedadEmp, False, RegexOptions.None)
                worddoc.ReplaceText("{*ESTADO EMPRESA*}", cEstadoEmp, False, RegexOptions.None)
                worddoc.ReplaceText("{*CP EMPRESA*}", cCPEmp, False, RegexOptions.None)
                worddoc.ReplaceText("{*MUNEMPRESA*}", cMunEmp, False, RegexOptions.None)
                worddoc.ReplaceText("{*JEFE*}", cJefe, False, RegexOptions.None)
                worddoc.Save()

                nIngreso = 0
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_INGRESO_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    nIngreso = Session("rs").Fields("INGRESO").Value.ToString
                End If
                worddoc.ReplaceText("{*INGRESO*}", nIngreso.ToString, False, RegexOptions.None)

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_BENEFICIARIOS_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                nReg = 0
                If Not Session("rs").eof Then
                    Do While Not Session("rs").EOF
                        nReg = nReg + 1
                        worddoc.ReplaceText("{*TNB" + LTrim(Str(nReg)) + "*}", "Nombre:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TPOB" + LTrim(Str(nReg)) + "*}", "Porcentaje:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TPAB" + LTrim(Str(nReg)) + "*}", "Parentesco:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFNB" + LTrim(Str(nReg)) + "*}", "Fecha de Nacimiento:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TTB" + LTrim(Str(nReg)) + "*}", "Teléfono(s):", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TDB" + LTrim(Str(nReg)) + "*}", "Domicilio:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TCPB" + LTrim(Str(nReg)) + "*}", "C.P.:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*BENEFICIARIO" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("NOMBREBENEF").Value, False, RegexOptions.None)
                        worddoc.ReplaceText("{*PRCNTJ" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("PORCENTAJEBENEF").Value, False, RegexOptions.None)
                        worddoc.ReplaceText("{*PARNTSC" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("RELACIONBENEF").Value, False, RegexOptions.None)
                        cFechaNac = ""
                        If Session("rs").Fields("FECHANAC").Value.ToString <> "1900-01-01" Then
                            cFechaNac = Left(Session("rs").Fields("FECHANAC").Value.ToString, 10)
                        End If
                        worddoc.ReplaceText("{*FNACB" + LTrim(Str(nReg)) + "*}", cFechaNac, False, RegexOptions.None)
                        worddoc.ReplaceText("{*TB" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("TELEFONOBENEF").Value.ToString, False, RegexOptions.None)
                        worddoc.ReplaceText("{*DOMB" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("DIRECCION").Value + " " + Session("rs").Fields("ESTADO").Value + " ", False, RegexOptions.None)
                        worddoc.ReplaceText("{*CP" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("CP").Value, False, RegexOptions.None)
                        Session("rs").movenext()
                    Loop
                    worddoc.Save()
                End If
                If nReg < 5 Then
                    For i As Integer = nReg To 5
                        worddoc.ReplaceText("{*BENEFICIARIO" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*PRCNTJ" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*PARNTSC" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*FNACB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*DOMB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*CP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TNB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TPOB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TPAB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFNB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TTB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TDB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TCPB" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)

                    Next
                End If

                Dim cSC, cSF, cNC, cNF, cAC, cAF, cCargoC, cCargoF, cNivelC, cNivelF, cParentesco, cNombrePPE As String

                cSC = ""
                cSF = ""
                cNC = "X"
                cNF = "X"
                cAC = ""
                cAF = ""
                cCargoC = ""
                cCargoF = ""
                cNivelC = ""
                cNivelF = ""
                cParentesco = ""
                cNombrePPE = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_PPE_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("LISTA", Session("adVarChar"), Session("adParamInput"), 1, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    Do While Not Session("rs").eof
                        If Session("rs").Fields("CVECATEGORIA").Value = "POLEXP" Then
                            If Not Session("rs").Fields("FIGURA").Value.Contains("FAMILIAR") Then
                                cSC = "X"
                                cNC = " "
                                cAC = Session("rs").Fields("FECHA_FIN_SERVICIO").Value.ToString.Substring(6, 4)
                                cCargoC = Session("rs").Fields("PUESTO").Value
                                cNivelC = Session("rs").Fields("IDNIVEL").Value
                            ElseIf Session("rs").Fields("FIGURA").Value.Contains("FAMILIAR") Then
                                cSF = "X"
                                cNF = " "
                                cAF = Session("rs").Fields("FECHA_FIN_SERVICIO").Value.ToString.Substring(6, 4)
                                cNombrePPE = Session("rs").Fields("NOMBRE1").Value + " " + Session("rs").Fields("NOMBRE2").Value + " " + Session("rs").Fields("PATERNO").Value + " " + Session("rs").Fields("MATERNO").Value
                                cCargoF = Session("rs").Fields("PUESTO").Value
                                cNivelF = Session("rs").Fields("IDNIVEL").Value.ToString
                                cParentesco = Session("rs").Fields("PARENTESCO").Value
                            End If
                        End If
                        Session("rs").movenext()
                    Loop
                End If
                worddoc.ReplaceText("{*SCP*}", cSC, False, RegexOptions.None)
                worddoc.ReplaceText("{*NCP*}", cNC, False, RegexOptions.None)
                worddoc.ReplaceText("{*AÑO*}", cAC, False, RegexOptions.None)
                worddoc.ReplaceText("{*CARGO_PPE*}", cCargoC, False, RegexOptions.None)
                worddoc.ReplaceText("{*NIVEL_PPE*}", cNivelC, False, RegexOptions.None)
                worddoc.ReplaceText("{*SFCP*}", cSF, False, RegexOptions.None)
                worddoc.ReplaceText("{*NFCP*}", cNF, False, RegexOptions.None)
                worddoc.ReplaceText("{*AÑO_F*}", cAF, False, RegexOptions.None)
                worddoc.ReplaceText("{*NOMBRE_PPE*}", cNombrePPE, False, RegexOptions.None)
                worddoc.ReplaceText("{*PARENTESCO_PPE*}", cParentesco, False, RegexOptions.None)
                worddoc.ReplaceText("{*CARGO_PPE_F*}", cCargoF, False, RegexOptions.None)
                worddoc.ReplaceText("{*NIVEL_PPE_F*}", cNivelF, False, RegexOptions.None)
                worddoc.Save()

                cNombrePR = ""
                cSPR = "X"
                cNPR = ""
                Session("cmd") = New ADODB.Command() 'Propietario Real
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CNFEXP_PROPIETARIO"
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cNombrePR = Session("rs").Fields("NOMBRE").Value + " Relación:" + Session("rs").Fields("RELACION").Value
                    cSPR = ""
                    cNPR = "X"
                End If
                worddoc.ReplaceText("{*NOMBRE PRO*}", cNombrePR, False, RegexOptions.None)
                worddoc.ReplaceText("{*SPRO*}", cSPR, False, RegexOptions.None)
                worddoc.ReplaceText("{*NPRO*}", cNPR, False, RegexOptions.None)

                cSPRE = ""
                cNPRE = "X"
                cNombrePRE = ""
                Session("cmd") = New ADODB.Command() 'Proveedor Recursos
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CNFEXP_PROVEEDORREC"
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cSPRE = "X"
                    cNPRE = ""
                    cNombrePRE = Session("rs").Fields("NOMBRE").Value + " Relación:" + Session("rs").Fields("RELACION").Value
                End If
                worddoc.ReplaceText("{*SPRE*}", cSPRE, False, RegexOptions.None)
                worddoc.ReplaceText("{*NPRE*}", cNPRE, False, RegexOptions.None)
                worddoc.ReplaceText("{*NOMBRE PRE*}", cNombrePRE, False, RegexOptions.None)
                worddoc.Save()

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_TARJETA_FIRMAS_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                nReg = 0
                If Not Session("rs").eof Then
                    Do While Not Session("rs").EOF
                        nReg = nReg + 1
                        worddoc.ReplaceText("{*TNOMBREAUT" + LTrim(Str(nReg)) + "*}", "Nombre:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TNOCTEAUT" + LTrim(Str(nReg)) + "*}", "No. Cliente", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFNAUT" + LTrim(Str(nReg)) + "*}", "Fecha Nacimiento:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMA" + LTrim(Str(nReg)) + "*}", "Firma:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOMBREAUT" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("NOMBRE").Value, False, RegexOptions.None)
                        cFechaNac = ""
                        If Left(Session("rs").Fields("FECHANAC").Value.ToString, 10) <> "01/01/1900" Then
                            cFechaNac = Session("rs").Fields("FECHANAC").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(6, 4)
                        End If
                        worddoc.ReplaceText("{*FNAUT" + LTrim(Str(nReg)) + "*}", cFechaNac, False, RegexOptions.None)
                        nCte = 0
                        If Not IsDBNull(Session("rs").Fields("IDPERSONA").Value) Then
                            nCte = Session("rs").Fields("IDPERSONA").Value
                        End If
                        worddoc.ReplaceText("{*NOCTEAUT" + LTrim(Str(nReg)) + "*}", nCte.ToString, False, RegexOptions.None)
                        Session("rs").movenext()
                    Loop
                End If
                If nReg < 2 Then
                    For i As Integer = nReg To 2
                        worddoc.ReplaceText("{*TNOMBREAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TNOCTEAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFNAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMA" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOMBREAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOCTEAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*FNAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                    Next
                    worddoc.Save()
                End If

                nReg = 0
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_CNFEXP_PERSONA_CAPTACION_VISTA"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").EOF Then
                    Do While Not Session("rs").EOF
                        nReg = nReg + 1
                        worddoc.ReplaceText("{*COTITULAR" + Str(nReg).Trim + "*}", Session("rs").Fields("NOMBRE").Value + vbCr, False, RegexOptions.None)
                        worddoc.ReplaceText("{*TRAYACOT" + Str(nReg).Trim + "*}", "______________________________________________" + vbCr, False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMACOT" + Str(nReg).Trim + "*}", "NOMBRE Y FIRMA COTITULAR" + vbCrLf, False, RegexOptions.None)
                        Session("rs").movenext()
                    Loop
                End If
                If nReg < 3 Then
                    For i As Integer = nReg To 3
                        worddoc.ReplaceText("{*COTITULAR" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TRAYACOT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMACOT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                    Next
                End If

                worddoc.Save()
                ConviertePDF(NewDocName, cPath)
            Catch ex As Exception
            End Try
        End Using
    End Sub

#End Region


#Region "Solicitud PM"
    Private Sub GenerarSolicitudAC_PM(ByVal cNomArchivo As String, ByVal cReca As String)
        Dim lcUrl As String = cNomArchivo + ".docx"
        Dim NewDocName As String = cNomArchivo + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        Dim cPath As String = Session("APPATH") + "\Word\"
        Dim cPathNewDoc As String = cPath + NewDocName + ".docx"

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPath + lcUrl)
            Try
                worddoc.SaveAs(cPathNewDoc)
                Session("Con").Open()
                ObtieneInfoSolPM(NewDocName, cPath, cPathNewDoc, cReca)
            Catch ex As Exception
            Finally
                Session("Con").Close()
            End Try
        End Using

    End Sub

    Private Sub ObtieneInfoSolPM(ByVal NewDocName As String, ByVal cPath As String, ByVal cPathNewDoc As String, cReca As String)
        Dim cSuc, cFiel, cFechaAlta, cRazon, cComercial, cTpoSoc, cRfc, cGiro, cObjSoc, cPais, cFecCons As String
        Dim cNacionalidad, cNotas, cTel, cCorreo, cCalle, cCol, cEst, cMun, cCP, cCalleS, cColS, cEstS, cMunS, cCPS As String
        Dim cNombrePR, cSPR, cNPR, cSPRE, cNPRE, cFechaNac, cNombrePre, cNombreRepLeg As String
        Dim nReg As Integer
        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPathNewDoc)
            Try
                cSuc = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_SUCURSAL"
                Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cSuc = Session("rs").Fields("SUCURSAL").Value.ToString
                End If
                worddoc.ReplaceText("{*SUCURSAL*}", cSuc, False, RegexOptions.None)
                worddoc.ReplaceText("{*FOLIO*}", Session("FOLIO").ToString, False, RegexOptions.None)
                worddoc.ReplaceText("{*RECA*}", cReca, False, RegexOptions.None)

                cFechaAlta = ""
                cRazon = ""
                cComercial = ""
                cRfc = ""
                cTpoSoc = ""
                cObjSoc = ""
                cGiro = ""
                cPais = ""
                cFecCons = ""
                cNacionalidad = ""
                cNotas = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    Session("PERSONAID") = Session("rs").Fields("IDCLIENTE").Value

                    If Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(6, 4) <> "1900" Then
                        cFechaAlta = Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHA_ALTA").Value.ToString.Substring(6, 4)
                    End If
                    cRazon = Session("rs").Fields("PROSPECTO").Value
                    cComercial = Session("rs").Fields("NOMBRE_COMERCIAL").Value
                    cRfc = Session("rs").Fields("RFC").Value
                    cTpoSoc = Session("rs").Fields("TIPOSOC").Value
                    cObjSoc = Session("rs").Fields("OBJ_SOCIAL").Value
                    cGiro = Session("rs").Fields("GIRO_MERC").Value
                    cPais = Session("rs").Fields("PAIS").Value
                    If Session("rs").Fields("FECHANAC").Value.ToString.Substring(6, 4) <> "1900" Then
                        cFecCons = Session("rs").Fields("FECHANAC").Value.ToString.Substring(0, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(3, 2) + "/" + Session("rs").Fields("FECHANAC").Value.ToString.Substring(6, 4)
                    End If
                    cNacionalidad = Session("rs").Fields("NACIONALIDAD").Value
                    cNotas = Session("rs").Fields("NOTAS").Value
                End If
                worddoc.ReplaceText("{*NUMERO CLIENTE*}", Session("PERSONAID").ToString, False, RegexOptions.None)
                worddoc.ReplaceText("{*FECHA ALTA*}", cFechaAlta, False, RegexOptions.None)
                worddoc.ReplaceText("{*RAZON*}", cRazon, False, RegexOptions.None)
                worddoc.ReplaceText("{*COMERCIAL*}", cComercial, False, RegexOptions.None)
                worddoc.ReplaceText("{*RFC*}", cRfc, False, RegexOptions.None)
                worddoc.ReplaceText("{*TIPO SOCIEDAD*}", cTpoSoc, False, RegexOptions.None)
                worddoc.ReplaceText("{*OBJETO SOCIAL*}", cObjSoc, False, RegexOptions.None)
                worddoc.ReplaceText("{*GIRO*}", cGiro, False, RegexOptions.None)
                worddoc.ReplaceText("{*FECHA CONS*}", cFecCons, False, RegexOptions.None)
                worddoc.ReplaceText("{*NACIONALIDAD*}", cNacionalidad, False, RegexOptions.None)
                worddoc.ReplaceText("{*LUGAR CONS*}", cPais, False, RegexOptions.None)
                worddoc.ReplaceText("{*NOTAS*}", cNotas, False, RegexOptions.None)
                worddoc.Save()

                cFiel = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "  SEL_PERSONA_MORAL_INGRESOS"
                Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cFiel = Session("rs").Fields("FIEL").Value
                End If
                worddoc.ReplaceText("{*FIEL*}", cFiel, False, RegexOptions.None)


                cTel = ""
                cCorreo = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_CONTACTO_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cTel = Session("rs").Fields("TELEFONO_MOVIL").Value.ToString + " " + Session("rs").Fields("TELEFONO_TRABAJO").Value
                    cCorreo = Session("rs").Fields("CORREO").Value
                End If
                worddoc.ReplaceText("{*TEL CONTACTO*}", cTel, False, RegexOptions.None)
                worddoc.ReplaceText("{*CORREO CONTACTO*}", cCorreo, False, RegexOptions.None)

                cCalle = ""
                cCol = ""
                cEst = ""
                cMun = ""
                cCP = ""
                cCalleS = ""
                cColS = ""
                cEstS = ""
                cMunS = ""
                cCPS = ""

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_DOMICILIO_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    'DIRECCION FISICA
                    cCalle = Session("rs").Fields("CALLE").Value + "  " + Session("rs").Fields("NUMEXT").Value.ToString + "  " + Session("rs").Fields("NUMINT").Value.ToString
                    cCol = Session("rs").Fields("ASENTAMIENTO").Value
                    cEst = Session("rs").Fields("ESTADO").Value
                    cMun = Session("rs").Fields("MUNICIPIO").Value
                    cCP = Session("rs").Fields("CP").Value
                    'DIRECCION SOCIAL 
                    cCalleS = Session("rs").Fields("CALLE_SOCIAL").Value + "  " + Session("rs").Fields("NUMEXT_SOCIAL").Value.ToString + "  " + Session("rs").Fields("NUMINT_SOCIAL").Value.ToString
                    cColS = Session("rs").Fields("ASENTAMIENTO_SOCIAL").Value
                    cEstS = Session("rs").Fields("ESTADO_SOCIAL").Value
                    cMunS = Session("rs").Fields("MUNICIPIO_SOCIAL").Value
                    cCPS = Session("rs").Fields("CP_SOCIAL").Value
                End If
                worddoc.ReplaceText("{*CALLE F*}", cCalle, False, RegexOptions.None)
                worddoc.ReplaceText("{*CALLE S*}", cCalleS, False, RegexOptions.None)
                worddoc.ReplaceText("{*COLONIA S*}", cCol, False, RegexOptions.None)
                worddoc.ReplaceText("{*COLONIA F*}", cColS, False, RegexOptions.None)
                worddoc.ReplaceText("{*MUNICIPIO F*}", cMun, False, RegexOptions.None)
                worddoc.ReplaceText("{*MUNICIPIO S*}", cMunS, False, RegexOptions.None)
                worddoc.ReplaceText("{*ESTADO F*}", cEst, False, RegexOptions.None)
                worddoc.ReplaceText("{*ESTADO S*}", cEstS, False, RegexOptions.None)
                worddoc.ReplaceText("{*CP F*}", cCP, False, RegexOptions.None)
                worddoc.ReplaceText("{*CP S*}", cCPS, False, RegexOptions.None)
                worddoc.Save()

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_REP_LEGAL"
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                nReg = 0
                If Not Session("rs").eof Then
                    Do While Not Session("rs").EOF
                        nReg = nReg + 1
                        If nReg <= 3 Then
                            worddoc.ReplaceText("{*TNOMBREREP" + LTrim(Str(nReg)) + "*}", "Nombre:", False, RegexOptions.None)
                            worddoc.ReplaceText("{*TCURPREP" + LTrim(Str(nReg)) + "*}", "CURP:", False, RegexOptions.None)
                            worddoc.ReplaceText("{*TRFCREP" + LTrim(Str(nReg)) + "*}", "RFC:", False, RegexOptions.None)
                            worddoc.ReplaceText("{*TCARGOREP" + LTrim(Str(nReg)) + "*}", "Cargo:", False, RegexOptions.None)
                            worddoc.ReplaceText("{*NOMBREREP" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("NOMBRE1").Value + " " + Session("rs").Fields("NOMBRE2").Value + " " + Session("rs").Fields("PATERNO").Value + " " + Session("rs").Fields("MATERNO").Value, False, RegexOptions.None)
                            worddoc.ReplaceText("{*CURPREP" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("CURP").Value, False, RegexOptions.None)
                            worddoc.ReplaceText("{*RFCREP" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("RFC").Value, False, RegexOptions.None)
                            worddoc.ReplaceText("{*CARGOREP" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("CARGO").Value, False, RegexOptions.None)
                        End If
                        Session("rs").movenext()
                    Loop
                    worddoc.Save()
                End If
                If nReg < 3 Then
                    For i As Integer = nReg To 3
                        worddoc.ReplaceText("{*TNOMBREREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOMBREREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TCURPREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*CURPREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TRFCREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*RFCREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*CARGOREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TCARGOREP" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                    Next
                End If


                cNombrePR = ""
                cSPR = "X"
                cNPR = ""
                Session("cmd") = New ADODB.Command() 'Propietario Real
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CNFEXP_PROPIETARIO"
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cNombrePR = Session("rs").Fields("NOMBRE").Value + ", Relación: " + Session("rs").Fields("RELACION").Value
                    cSPR = ""
                    cNPR = "X"
                End If
                worddoc.ReplaceText("{*NOMBRE PRO*}", cNombrePR, False, RegexOptions.None)
                worddoc.ReplaceText("{*SPRO*}", cSPR, False, RegexOptions.None)
                worddoc.ReplaceText("{*NPRO*}", cNPR, False, RegexOptions.None)

                cSPRE = ""
                cNPRE = "X"
                cNombrePre = ""
                Session("cmd") = New ADODB.Command() 'Proveedor Recursos
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_CNFEXP_PROVEEDORREC"
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").eof Then
                    cSPRE = "X"
                    cNPRE = ""
                    cNombrePre = Session("rs").Fields("NOMBRE").Value + ", Relación: " + Session("rs").Fields("RELACION").Value
                End If
                worddoc.ReplaceText("{*SPRE*}", cSPRE, False, RegexOptions.None)
                worddoc.ReplaceText("{*NPRE*}", cNPRE, False, RegexOptions.None)
                worddoc.ReplaceText("{*NOMBRE PRE*}", cNombrePre, False, RegexOptions.None)
                worddoc.Save()

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_TARJETA_FIRMAS_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                nReg = 0
                If Not Session("rs").eof Then
                    Do While Not Session("rs").EOF
                        nReg = nReg + 1
                        worddoc.ReplaceText("{*TNOMBREAUT" + LTrim(Str(nReg)) + "*}", "Nombre:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TNOCTEAUT" + LTrim(Str(nReg)) + "*}", "Nº Cliente:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFNAUT" + LTrim(Str(nReg)) + "*}", "Fecha Nacimiento:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMA" + LTrim(Str(nReg)) + "*}", "Firma:", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOMBREAUT" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("NOMBRE").Value, False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOCTEAUT" + LTrim(Str(nReg)) + "*}", Session("rs").Fields("IDPERSONA").Value.ToString, False, RegexOptions.None)
                        cFechaNac = ""
                        If Session("rs").Fields("FECHANAC").Value.ToString <> "1900-01-01" Then
                            cFechaNac = Session("rs").Fields("FECHANAC").Value.ToString
                        End If
                        worddoc.ReplaceText("{*FNAUT" + LTrim(Str(nReg)) + "*}", cFechaNac, False, RegexOptions.None)
                        Session("rs").movenext()
                    Loop
                    worddoc.Save()
                End If
                If nReg < 2 Then
                    For i As Integer = nReg To 2
                        worddoc.ReplaceText("{*TNOMBREAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TNOCTEAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFNAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMA" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOMBREAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*NOCTEAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*FNAUT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                    Next
                End If

                cNombreRepLeg = ""
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_REPLEG_DOC_LEGAL"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("PERSONAID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").EOF Then
                    cNombreRepLeg = Session("rs").Fields("NOMBRE").Value
                End If
                worddoc.ReplaceText("{*NOMBRE_REP*}", cNombreRepLeg, False, RegexOptions.None)

                nReg = 0
                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_CNFEXP_PERSONA_CAPTACION_VISTA"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                If Not Session("rs").EOF Then
                    Do While Not Session("rs").EOF
                        nReg = nReg + 1
                        worddoc.ReplaceText("{*COTITULAR" + Str(nReg).Trim + "*}", Session("rs").Fields("NOMBRE").Value + vbCr, False, RegexOptions.None)
                        worddoc.ReplaceText("{*TRAYACOT" + Str(nReg).Trim + "*}", "______________________________________________" + vbCr, False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMACOT" + Str(nReg).Trim + "*}", "NOMBRE Y FIRMA COTITULAR" + vbCrLf, False, RegexOptions.None)
                        Session("rs").movenext()
                    Loop
                End If
                If nReg < 3 Then
                    For i As Integer = nReg To 3
                        worddoc.ReplaceText("{*COTITULAR" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TRAYACOT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                        worddoc.ReplaceText("{*TFIRMACOT" + LTrim(Str(i)) + "*}", "", False, RegexOptions.None)
                    Next
                End If

                worddoc.Save()
                ConviertePDF(NewDocName, cPath)
            Catch ex As Exception
            End Try
        End Using
    End Sub


#End Region


#Region "Contratos"
    Private Sub GeneraContrato(ByVal cNomArchivo As String, ByVal cReca As String)

        Dim lcUrl As String = cNomArchivo + ".docx"
        Dim NewDocName As String = cNomArchivo + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        Dim cPath As String = Session("APPATH") + "\Word\"
        Dim cPathNewDoc As String = cPath + NewDocName + ".docx"

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPath + lcUrl)
            Try
                worddoc.SaveAs(cPathNewDoc)
                Session("Con").Open()
                ObtieneInfo(NewDocName, cPath, cPathNewDoc, cReca)
            Catch ex As Exception
            Finally
                Session("Con").Close()
            End Try
        End Using
    End Sub

    Private Sub ObtieneInfo(ByVal NewDocName As String, ByVal cPath As String, ByVal cPathNewDoc As String, ByVal cReca As String)
        Dim cDomicilioCliente
        Dim result As String = ""
        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPathNewDoc)
            Try
                worddoc.ReplaceText("{*RECA*}", cReca, False, RegexOptions.None)
                cDomicilioCliente = ""
                Session("PERSONAID") = 0
                Session("TASA") = ""
                Session("GAT_NOMI") = ""
                Session("GAT_REAL") = ""
                Session("TIPO_TASA") = ""
                Session("INDICE_VISTA") = ""
                Session("NOMBRE") = ""
                ' Session("PRODUCTO") = ""

                Session("rs") = CreateObject("ADODB.Recordset")
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("rs") = Session("cmd").Execute()
                worddoc.ReplaceText("{*FOLIO*}", Session("FOLIO"), False, RegexOptions.None)
                If Not Session("rs").eof Then
                    Session("PERSONAID") = Session("rs").Fields("IDCLIENTE").Value
                    worddoc.ReplaceText("{*NUMERO CLIENTE*}", Session("rs").Fields("IDCLIENTE").Value.ToString, False, RegexOptions.None)
                End If

                worddoc.Save()
                ConviertePDF(NewDocName, cPath)
                lbl_status_docs.Text = "ok"
            Catch ex As Exception
                result = (ex.Message)
                lbl_status_docs.Text = result
            End Try
        End Using
    End Sub

    Private Sub ConviertePDF(ByVal NewDocName As String, ByVal cPath As String)
        Dim result As String = ""
        Dim objNewWord As New Microsoft.Office.Interop.Word.Application()
        Dim ResultDocName As String = NewDocName + ".pdf"

        Try
            Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(cPath + NewDocName + ".docx")
            objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", cPath), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
            objNewDoc.Close()
            'Elimina el Documento WORD ya Prellenado
            System.IO.File.Delete(cPath + NewDocName + ".docx")

            ' Se genera el PDF
            Dim Filename As String = NewDocName + ".pdf"
            Dim FilePath As String = cPath
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
            Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", ResultDocName))
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(bytBytes)
            Response.End()
            lbl_status_docs.Text = "Todo ok"
        Catch ex As Exception
            result = (ex.Message)
            lbl_status_docs.Text = result
        Finally
            lbl_status_docs.Text = "afuerza"
            objNewWord.Quit()
        End Try

        objNewWord = Nothing
    End Sub
    Private Sub OpenWordDocumentBefore(ByVal Contrato As String)

        Dim result As String = ""
        Dim objWord As Microsoft.Office.Interop.Word.Application
        Dim objDoc As Microsoft.Office.Interop.Word.Document

        Dim NewDocName As String = Contrato + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString

        Dim objNewWord As New Microsoft.Office.Interop.Word.Application()
        Dim missing As Object = System.Type.Missing
        Try

            objWord = CreateObject("Word.Application")
            objWord.Visible = True
            objDoc = objWord.Documents.Open(Session("APPATH") + "\Word\" + Contrato + ".docx")

            objDoc.SaveAs(Session("APPATH") + "\Word\" + NewDocName + ".docx")

            objDoc.Close()
            objWord.Quit()

        Catch ex As Exception

        End Try

        Try



            Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(Session("APPATH") + "\Word\" + NewDocName + ".docx")

            objNewWord.Visible = False
            'objNewDoc.Bookmarks("ConNoCliente").Range.Text = "holamundo"

            Session("GAT_NOMI") = Nothing
            Session("TASA") = Nothing
            Session("GAT_REAL") = Nothing
            Session("CONT_GAT_NOMI") = Nothing
            Session("CONT_GAT_REAL") = Nothing
            Session("TIPO_TASA") = Nothing
            Session("INDICE_VISTA") = Nothing
            Session("FECHA_ALTA") = Nothing
            Session("NO_CONTRATO") = Nothing
            Session("NOMBRECLIENTE") = Nothing
            Session("DIRECCION_CTE") = Nothing
            Session("FECHA_CREACION") = Nothing


            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_CAPTACION_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then


                Session("PRODUCTO") = Session("rs").Fields("PRODUCTO").Value.ToString
                Session("TIPO_CAPTACION") = Session("rs").Fields("TIPO").Value.ToString
                Session("TASA") = Session("rs").Fields("TASA_NORMAL").Value.ToString
                Session("GAT_NOMI") = Session("rs").Fields("GAT_VISTA").Value.ToString
                Session("GAT_REAL") = Session("rs").Fields("GAT_VISTA_REAL").Value.ToString
                Session("CONT_GAT_NOMI") = Session("rs").Fields("GAT_VISTA").Value.ToString
                Session("CONT_GAT_REAL") = Session("rs").Fields("GAT_VISTA_REAL").Value.ToString
                Session("TIPO_TASA") = Session("rs").Fields("TIPO_TASA").Value.ToString
                Session("INDICE_VISTA") = Session("rs").Fields("INDICE_VISTA").Value.ToString
                Session("FECHA_CREACION") = Session("rs").Fields("FECHA_CREACION").Value.ToString

                '[CONT_NO_FOLIO]


                'If Session("TIPO_TASA") = "FIJA" Then
                '    objDoc.Bookmarks("TASA_ANUAL").Range.Text = Session("TASA").ToString()
                '    'Else
                '    '     "INDICE: " + Session("INDICE_VISTA")
                '    '    " PUNTOS: " + Session("TASA_NORMAL")
                'End If




            End If

            Session("Con").Close()



            Dim alta As String = ""

            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 200, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                Session("NO_CONTRATO") = Session("FOLIO").ToString()
                Session("NOMBRECLIENTE") = Session("PROSPECTO").ToString()

                alta = Session("rs").Fields("FECHA_ALTA").Value.ToString()


            End If
            Session("Con").Close()



            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_CREDITO_ACTORES_EXTRA_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                '
                If Session("rs").Fields("TIPO").Value.ToString = "CLIENTE" Then

                    Session("DIRECCION_CTE") = Session("rs").Fields("DOMICILIO").Value.ToString()
                End If


            End If
            Session("Con").Close()


            Session("DIA") = Nothing
            Session("MES") = Nothing
            Session("AÑO") = Nothing
            Session("NOMBRE_SUCURSAL") = Nothing
            Session("ESTADO") = Nothing
            Session("MUNICIPIO") = Nothing

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

                Session("DIA") = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2)
                Session("MES") = Session("rs").Fields("MES").Value.ToString()
                Session("AÑO") = Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
                Session("NOMBRE_SUCURSAL") = Session("rs").Fields("NOMBRE").Value.ToString()
                Session("MUNICIPIO") = Session("rs").Fields("MUNICIPIO").Value.ToString()
                Session("ESTADO") = Session("rs").Fields("ESTADO").Value.ToString()

            End If

            Session("Con").Close()


            Session("NOM_PRODUCTO") = Nothing

            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_TIPO_EXP"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then

                Session("NOM_PRODUCTO") = Session("rs").Fields("NOM_PROD").Value.ToString()


            End If

            Session("Con").Close()


            Dim FechaAlta = Split(alta.ToString, " ")


            ' Datos Caratula 
            objNewDoc.Bookmarks("Nombre_PROD").Range.Text = "Depósito a la Vista" ''Session("NOM_PRODUCTO").ToString()
            objNewDoc.Bookmarks("TASA_ANUAL").Range.Text = Session("TASA").ToString()
            objNewDoc.Bookmarks("GAT_NOMI").Range.Text = Session("GAT_NOMI").ToString()
            objNewDoc.Bookmarks("GAT").Range.Text = Session("GAT_REAL").ToString()

            objNewDoc.Bookmarks("NOMBRE_CLIENTE1").Range.Text = Session("NOMBRECLIENTE").ToString()
            objNewDoc.Bookmarks("NOMBRE_CLIENTE2").Range.Text = Session("NOMBRECLIENTE").ToString()
            objNewDoc.Bookmarks("NOMBRE_SUCURSAL").Range.Text = Session("NOMBRE_SUCURSAL").ToString()
            objNewDoc.Bookmarks("NUMERO_CLIENTE").Range.Text = Session("PERSONAID").ToString()
            objNewDoc.Bookmarks("NO_CONTRATO").Range.Text = Session("FOLIO").ToString()
            objNewDoc.Bookmarks("FECHA_ALTA").Range.Text = Session("FECHA_CREACION").ToString()

            'Datos Contrato
            objNewDoc.Bookmarks("Cont_NumCont").Range.Text = Session("FOLIO").ToString()
            objNewDoc.Bookmarks("Cont_NumCliente").Range.Text = Session("PERSONAID").ToString()
            objNewDoc.Bookmarks("Cont_Nombre_Cliente").Range.Text = Session("NOMBRECLIENTE").ToString()
            objNewDoc.Bookmarks("Cont_Direccion_Cliente").Range.Text = Session("DIRECCION_CTE").ToString()
            objNewDoc.Bookmarks("CONT_GAT_NOM").Range.Text = Session("GAT_NOMI").ToString()
            objNewDoc.Bookmarks("CONT_GAT_REAL").Range.Text = Session("GAT_REAL").ToString()
            objNewDoc.Bookmarks("Cont_Nombre_Cliente2").Range.Text = Session("NOMBRECLIENTE").ToString()
            objNewDoc.Bookmarks("Cont_Nombre_EJEC").Range.Text = " " '"NOMBRE Y FIRMA DEL EJECUTIVO"
            objNewDoc.Bookmarks("Cont_Nombre_Cliente3").Range.Text = Session("NOMBRECLIENTE").ToString()

            'Datos Inversion En Contrato de Captacion
            objNewDoc.Bookmarks("Cont_Tasa").Range.Text = "N/A"
            objNewDoc.Bookmarks("CONT_GAT_NOMI2").Range.Text = "N/A"
            objNewDoc.Bookmarks("CONT_GAT_REAL2").Range.Text = "N/A"

            'Datos Fecha y Lugar En Contrato
            objNewDoc.Bookmarks("dia").Range.Text = Session("DIA").ToString()
            objNewDoc.Bookmarks("MES").Range.Text = Session("MES").ToString()
            objNewDoc.Bookmarks("AÑO").Range.Text = Session("AÑO").ToString()
            objNewDoc.Bookmarks("Cont_Tasa2").Range.Text = Session("TASA").ToString()
            objNewDoc.Bookmarks("CIUDAD").Range.Text = " " + Session("MUNICIPIO").ToString() + ", " + Session("ESTADO").ToString()



            objNewDoc.Save()

            objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", Session("APPATH") + "\Word\"), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
            objNewDoc.Close()
            'Elimina el Documento WORD ya Prellenado
            System.IO.File.Delete(Session("APPATH") + "\Word\" + NewDocName + ".docx")

            Dim Filename As String = NewDocName + ".pdf"
            Dim FilePath As String = Session("APPATH") + "\Word\"

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

        Catch ex As Exception
            result = (ex.Message)
        Finally
            objNewWord.Quit()
        End Try


        objDoc = Nothing
        objWord = Nothing
        objNewWord = Nothing

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub
#End Region

End Class