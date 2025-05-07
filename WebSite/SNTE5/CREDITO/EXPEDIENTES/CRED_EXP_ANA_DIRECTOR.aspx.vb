Public Class CRED_EXP_ANA_DIRECTOR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Análisis de Expediente", "Análisis de Expediente")
        If Not Me.IsPostBack Then

            'Datos Generales de Expediente: Folio, Nombre de Cliente y Producto
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))
            lbl_Prospecto.Text = Session("PROSPECTO") + " (" + Session("PERSONAID").ToString + ")"
            lbl_Producto.Text = Session("PRODUCTO")
            DetalleExpediente()
            Empresa()
        End If

        'agrego metodo javascript a datos persona
        If tipo_persona() = "F" Then
            lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
        Else
            lnk_persona.Attributes.Add("OnClick", "ResumenPersonaM()")
        End If

        If Session("VENGODEDIGGLOBAL") = "DigitalizadorGlobal.aspx" Then
            LimpiaVariables()
            Response.Redirect("CRED_EXP_DIRECTOR.aspx")
        End If
    End Sub

    Private Sub DetalleExpediente()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()


        'lbl_ProductoDetalleB.Text = Session("rs").Fields("PRODUCTO").value.ToString()
        lbl_MontoB.Text = Session("rs").Fields("MONTO").value.ToString()
        lbl_PlazoB.Text = Session("rs").Fields("PLAZO").value.ToString()
        lbl_PeriodicidadB.Text = Session("rs").Fields("PERIODICIDAD").value.ToString()
        lbl_TasaNormalB.Text = Session("rs").Fields("TASA_NORMAL").value.ToString()
        lbl_TasaMoraB.Text = Session("rs").Fields("TASA_MORA").value.ToString()
        lbl_fechaliberaB.Text = Session("rs").Fields("AUX_FECHA_LIBERA").value.ToString()
        lbl_emergB.Text = Session("rs").Fields("EMERGENCIA").value.ToString()
        lbl_clasB.Text = Session("rs").Fields("CLASIFICACION").value.ToString()

        Session("CLASIFICACION") = Session("rs").Fields("CLASIFICACION").value.ToString()

        Session("TIPOPLANPAGO") = Session("rs").Fields("TIPOPLAN").value.ToString()
        Select Case Session("TIPOPLANPAGO")
            Case "SI"
                lbl_tipoplanB.Text = "SALDOS INSOLUTOS"
                lnk_PlanPagos.Visible = True

            Case "PFSI"
                lbl_tipoplanB.Text = "PAGOS FIJOS SI"
                lnk_PlanPagos.Visible = True

            Case "ES"
                lbl_tipoplanB.Text = "PLAN ESPECIAL"
                lnk_PlanPagos.Visible = True

            Case Else
                lbl_tipoplanB.Text = ""
                lnk_PlanPagos.Visible = False

        End Select

        'lbl_notasB.Text = Session("rs").Fields("NOTAS").value.ToString()
        'lbl_nivel_pep.Text = Session("rs").Fields("NIVEL").value.ToString()
        'lbl_desc_pep.Text = Session("rs").Fields("DESCRIPCION").value.ToString()
        'lbl_res_pep.Text = Session("rs").Fields("CATEGORIA").value.ToString()
        Session("Con").Close()

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

        ' If comite = "2" Then 'ya fue enviado a analisis directo a comite
        'lbl_razonB.Text = razon

        'End If
    End Sub

    Private Sub Empresa()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_EMPRESA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("EMPRESA") = Session("rs").fields("RAZON").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Function tipo_persona() As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_TIPO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("TIPO").value.ToString
        Session("Con").Close()
        Return tipo
    End Function

    Private Sub LimpiaVariables()
        Session("MONTO") = Nothing
        Session("PLAZO") = Nothing
        Session("UNIDAD") = Nothing
        Session("DIAS") = Nothing
        Session("MESES") = Nothing
        Session("TASAMIN") = Nothing
        Session("TASAMAX") = Nothing
        Session("NUMPAGOS") = Nothing
        Session("GENERAPAGO") = Nothing
        Session("TIPOTASA") = Nothing
        Session("TIPOPLANPAGO") = Nothing
        Session("TASA") = Nothing
        Session("PERIODICIDAD") = Nothing
        Session("UNIDADPERIODO") = Nothing
        Session("IDSESCOMIT") = Nothing
        Session("FOLIO") = Nothing
        Session("PRODUCTO") = Nothing
        Session("CLASIFICACION") = Nothing
        Session("OPCION") = Nothing
        Session("C_COBRO") = Nothing
        Session("C_TIEMPO") = Nothing
        Session("C_TOTAL") = Nothing
        Session("C_IVA") = Nothing
    End Sub

    Protected Sub lnk_garantias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_garantias.Click
        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Response.Redirect("CRED_EXP_GARANTIA.aspx")
    End Sub

    Protected Sub lnk_historial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_historial.Click
        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Response.Redirect("CRED_EXP_HISTORIAL.aspx")
    End Sub

    Protected Sub lnk_docsexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_docsexp.Click

        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Response.Redirect("CRED_EXP_EXP_DIGITAL.aspx")
    End Sub

    Protected Sub lnk_redsocial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_redsocial.Click
        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Response.Redirect("~/UNIVERSAL/UNI_RED_SOCIAL.aspx")
    End Sub

    Protected Sub lnk_gastos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_gastos.Click
        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Response.Redirect("CRED_EXP_INVESTIGACIONES.aspx")
    End Sub

    'Obtiene que tipo de plan es
    Private Sub tipoplan()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_PLANPAGO"
        Session("rs") = Session("cmd").Execute()

        'SI EXISTE UN PLAN DE PAGOS SE HABILITA EL LINK PARA VER EL PLAN DE PAGOS
        If Session("rs").Fields("RESPUESTA").value.ToString = "EXISTE" Then

            Session("TIPOPLANPAGO") = Session("rs").Fields("TIPOPLAN").value.ToString
            Session("PERIODICIDAD") = Session("rs").Fields("PERIODICIDAD").value.ToString
            Session("UNIDADPERIODO") = Session("rs").Fields("UNIDADPERIODICIDAD").value.ToString
            Session("TASA") = Session("rs").Fields("TASA").value.ToString
            Session("IDINDICE") = Session("rs").Fields("IDINDICE").value.ToString
            Session("TIPOTASA") = Session("rs").Fields("TIPOTASA").value.ToString



        End If
        Session("Con").Close()
    End Sub


    '------------------------------------PLANES DE PAGOS-----------------------------------
    Protected Sub lnk_PlanPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_PlanPagos.Click
        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Session("CLIENTE") = Session("PROSPECTO")
        Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
    End Sub


    Protected Sub btn_resultado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_resultado.Click
        If cmb_resultado.SelectedItem.Value = "RECHAZADO" Then
            Dim ruta As String
            Dim archivo As String
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_DOCUMENTACION_RECHAZADA"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                ruta = ConfigurationManager.AppSettings.[Get]("urldigi")
                archivo = Session("rs").fields("ARCHIVO").value.ToString
                DelHDFile(ruta + "/" + archivo)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 15, Session("IDSESCOMIT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DICTAMEN", Session("adVarChar"), Session("adParamInput"), 10, cmb_resultado.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("OBSERVACION", Session("adVarChar"), Session("adParamInput"), 1000, txt_observacion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        'If txt_MontoAutor.Text = "" Then
        '    txt_MontoAutor.Text = 0.0
        'End If
        Session("parm") = Session("cmd").CreateParameter("MONTO_AUTOR", Session("adVarChar"), Session("adParamInput"), 15, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RECOMENDACIONES", Session("adVarChar"), Session("adParamInput"), 1000, "")
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_ANAEXP_DICTAMEN_COMITE"
        Session("cmd").Execute()

        Session("Con").Close()

        btn_resultado.Enabled = False
        cmb_resultado.Enabled = False

        Dim res As String
        res = CorreoJefeSucursal()
        EnviaMailJefeSucursal(res, cmb_resultado.SelectedItem.Text)

        AvisoCambioEstatus()

        lbl_resdictamen.Text = "Guardado correctamente"

        lnk_dictamen.Enabled = True
        lnk_digitalizar.Enabled = True

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        'Dim fi As New System.IO.FileInfo(File)
        'If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
        '    fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        'End If

        'System.IO.File.Delete(File)

    End Sub

    Protected Sub cmb_resultado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_resultado.SelectedIndexChanged

        If cmb_resultado.SelectedValue = "RECHAZOREC" Then
            'txt_MontoAutor.Enabled = True
            'txt_Recomendaciones.Enabled = True
            'RequiredFieldValidatortxt_MontoAutor.Enabled = True
            'RequiredFieldValidatortxt_Recomendaciones.Enabled = True
        Else
            'txt_MontoAutor.Text = ""
            'txt_Recomendaciones.Text = ""
            'txt_MontoAutor.Enabled = False
            'txt_Recomendaciones.Enabled = False
            'RequiredFieldValidatortxt_MontoAutor.Enabled = False
            'RequiredFieldValidatortxt_Recomendaciones.Enabled = False
        End If

    End Sub


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

    Protected Sub lnk_restructura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_restructura.Click
        'Response.Redirect("DetalleRestructura.aspx")
        ClientScript.RegisterStartupScript(GetType(String), "", "window.open('CRED_EXP_REESTRUCTURA.aspx', 'DR', 'width=700,height=350,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1');", True)
    End Sub

    Protected Sub lnk_dictamen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_dictamen.Click
        PrellenadoActaDictamen()
        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= Dictamen_Director.pdf")
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


    Private Sub PrellenadoActaDictamen()

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
            .AddSubject("Dictamen Director - " + Session("FOLIO"))
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Dictamen Director - " + Session("FOLIO"))
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Dictamen Director - " + Session("FOLIO"))
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
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
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
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("FOLIO").ToString, X, Y, 0)

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
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            'MONTO
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

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_GARANTIAS_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Y = 575
            X = 145
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("GARANTIA").Value.ToString, X, Y, 0)

        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_SESION_COMITE_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDSESCOMIT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Y = Y - 38
            X = 165

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)

            Dim resultado As String
            resultado = Session("rs").Fields("RESULTADO").Value.ToString
            If resultado = "RECHAZOREC" Then
                resultado = "RECHAZADO CON RECOMENDACIONES"
            End If
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, resultado, X, Y, 0)

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            Y = Y - 18
            X = X - 40

            If Session("rs").Fields("MONTO_AUTOR").Value.ToString = 0.0 Then
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Gasto, X, Y, 0)
            Else
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(Session("rs").Fields("MONTO_AUTOR").Value.ToString), X, Y, 0)
            End If

            X = X - 20

            Dim ObLargo As Decimal
            Dim aux As Integer = 0
            ObLargo = Len(Session("rs").Fields("OBSERVACIONES").Value.ToString) / 85

            Y = Y - 17
            While (ObLargo > 0)
                If aux = 0 Then
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Left(Session("rs").Fields("OBSERVACIONES").Value.ToString, 85), X, Y, 0)
                    aux = aux + 100
                Else
                    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Mid(Session("rs").Fields("OBSERVACIONES").Value.ToString, (aux + 1), 85), X, Y, 0)
                    aux = aux + 110
                End If

                Y = Y - 10
                ObLargo = ObLargo - 1
            End While

            Dim Parrafo1 As String
            Parrafo1 = Session("rs").Fields("RECOMENDACIONES").Value.ToString

            Dim paragraph1 As New iTextSharp.text.Paragraph(Parrafo1, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL))

            paragraph1.SetAlignment("JUSTIFY")

            document.Add(paragraph1)

        End If


        Dim firma As Integer
        firma = 0

        Y = 240

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_MIEMBROS_COMITE_CREDITO"
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDSESCOMIT"))
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

                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "_______________________________________", X, Y, 0)
                ''VOTO
                'X = X + 95
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("VOTO").Value.ToString, X, Y, 0)
                Y = Y - 10
                'X = X - 50
                'NOMBRE
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)
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

    Protected Sub lnk_digitalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_digitalizar.Click
        Session("VENGODE") = "CRED_EXP_ANA_DIRECTOR.ASPX"
        Response.Redirect("~/DIGITALIZADOR/DIGI_GLOBAL.aspx")
    End Sub

    Private Sub AvisoCambioEstatus()

        Dim correo As String
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim usuario As String = String.Empty
        Dim contenido As String = String.Empty
        'Insertar a la Cola de validacion para la fase 2
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_AVISOCORREO_ESTATUS_USUARIO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            usuario = Session("rs").Fields("USUARIO").Value.ToString
            contenido = Session("rs").Fields("CONTENIDO").Value.ToString
            subject = "CAMBIO DE ESTATUS DE EXPEDIENTE (" + CStr(Session("FOLIO")) + ")"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a):  " + usuario + +contenido + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub lnk_notas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_notas.Click

        ModalPopupExtender1.Show()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_CNFEXP_VALIDADOR_NOTAS]"
        Session("rs") = Session("cmd").Execute()

        Dim nota As String = ""
        Do While Not Session("rs").EOF

            nota = nota + vbCrLf + Session("rs").Fields("TIPO").Value.ToString + vbCrLf + Session("rs").Fields("NOTAS").Value.ToString + vbCrLf

            Session("rs").movenext()

        Loop
        lbl_notasexp.Text = nota

        Session("Con").Close()

    End Sub

End Class