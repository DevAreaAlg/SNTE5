Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient

Public Class CRED_CNF_PAGARES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Pagarés", "Configuración Pagarés")

        If Not Me.IsPostBack Then

            ' Split string oara obtener el resultado y el nombre del manual
            Dim resultado
            resultado = Split(Session("MascoreG").RevisaPermisos(Session("IDMOD").ToString, Session("USERID").ToString, Session("MAC").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString()), "A"), "%")

            lbl_subtitulo.Text = "Configuración de Pagarés"

            PermisosCrearPagare()

            If Session("IDMOD").ToString = "14" Then
                TabContainer1.ActiveTabIndex = 0
                TabPanel1.Enabled = False
                TabPanel0.Enabled = True
                LlenarProductos()
            Else
                lbl_subtitulo.Text = Session("APARTADO").ToString
                lbl_Producto.Text = "PRODUCTO: " + Session("PROD_NOMBRE").ToString
                TabContainer1.ActiveTabIndex = 1
                TabPanel1.Enabled = True
                TabPanel0.Enabled = False
                btn_Terminar.Enabled = False
                LlenaPagares()
            End If

            'Un focus sobre el text box para iniciar en el.
            lbl_status.Visible = False
            txt_PagareNombre.Focus()
            LlenaPlantillaPagareCreado()

        End If
        txt_PagareNombre.Focus()
    End Sub

    Protected Sub PermisosCrearPagare()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FACULTAD_CREAR_PAGARE"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("PERMISO").Value.ToString = 0 Then
            TabPanel2.Enabled = False
        End If

        Session("Con").Close()

    End Sub

    Private Sub LlenarProductos()

        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmb_Productos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_Productos.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_PRODUCTOS_CONF"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("MSTPRODUCTOS_NOMBRE").Value.ToString + " (" + Session("rs").Fields("DESTINO").Value.ToString + ")", Session("rs").Fields("MSTPRODUCTOS_ID_PROD").Value.ToString)
            cmb_Productos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub LlenaPagares()
        'Muestra las comisiones asignadas y disponibles

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ModulosGeneral As New Data.DataTable()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_PAGARES_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ModulosGeneral, Session("rs"))
        Session("Con").Close()

        If ModulosGeneral.Rows.Count > 0 Then
            dag_mod_si.Visible = True
            dag_mod_si.DataSource = ModulosGeneral
            dag_mod_si.DataBind()
        Else
            dag_mod_si.Visible = False
        End If

    End Sub

    Protected Sub btn_asignar_Click(sender As Object, e As EventArgs)

        'Data table que se llena con los contenidos de los datagrids
        Dim dtModulos As New Data.DataTable()
        dtModulos.Columns.Add("IDG", GetType(Integer))
        dtModulos.Columns.Add("DESCRIPCION", GetType(String))
        dtModulos.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_mod_si.Rows.Count() - 1
            dtModulos.Rows.Add(CInt(dag_mod_si.Rows(i).Cells(0).Text), dag_mod_si.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_mod_si.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_MOD_ROL_ASIGNAR", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDROL", SqlDbType.Int)
                Session("parm").Value = Session("IDROL")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("MODULOS", SqlDbType.Structured)
                Session("parm").Value = dtModulos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()

            End Using
        Catch ex As Exception
            '<span class="text_input_nice_label" style="margin-left:20px">Rol Número</span>
            lbl_RolNumero.Text = "Error"

        Finally
            LlenaPagares()
        End Try
    End Sub

    Protected Sub btn_GeneraPlantillaPagare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GeneraPlantillaPagare.Click

        'Boton para generar una plantilla de pagare una vez que este haya sido completado por el usuario.
        If txt_PagareNombre.Text = "" Then
            lbl_AlertaGenerarPlantilla.Text = "Error: Debe ingresar un nombre o descripción"
        ElseIf txt_PlantillaPagare.Text = "" Then
            'No dejara crear una plantilla si no tiene algo escrito el textbox
            lbl_AlertaGenerarPlantilla.Text = "Error: Documento en Blanco"
        ElseIf txt_PlantillaPagare.Text.Length > 5000 Then
            lbl_AlertaGenerarPlantilla.Text = "Error: Solo 5000 caracteres o menos en la descripción"
        Else
            GuardarPlantillaPagare()
            LlenaPlantillaPagareCreado()
            btn_GeneraPlantillaPagare.Enabled = False
            btn_GeneraPlantillaPagare.Enabled = False
            cmb_TipoCobro.SelectedIndex = "0"
            cmb_TipoCobro.Enabled = False
            If Session("IDMOD").ToString = "14" And Session("PRODUCTOID") <> Nothing Then
                LlenaPagares()
            ElseIf Session("IDMOD").ToString <> "14" Then
                LlenaPagares()
            End If
        End If

    End Sub

    Protected Sub GuardarPlantillaPagare()

        'Guarda la plantilla de un pagare nuevo creado por el usuario.
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_PagareNombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CONTENIDO", Session("adVarChar"), Session("adParamInput"), 50000, txt_PlantillaPagare.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_COBRO", Session("adVarChar"), Session("adParamInput"), 15, cmb_TipoCobro.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PLANTILLA_PAGARE"
        Session("rs") = Session("cmd").Execute()
        Session("PAGAREID") = Session("rs").Fields("IDPAGARE").Value.ToString

        Session("Con").Close()

        lbl_AlertaGenerarPlantilla.Text = "Plantilla de Pagaré Generada."
        txt_PagareNombre.Enabled = False
        txt_PlantillaPagare.Enabled = False
        btn_VerPagare.Enabled = True

    End Sub

    Protected Sub btn_Terminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Terminar.Click
        'Boton para regresar a Menu Principal
        Session("PRODID") = Nothing
        Session("PAGAREID") = Nothing
        Response.Redirect("MenuPrincipal.aspx")

    End Sub

    Protected Sub lnk_AgrMunicipio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrMunicipio.Click
        'Agrega tag de campo de [MUNICIPIO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[MUNICIPIO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrEstado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrEstado.Click
        'Agrega tag de campo de [ESTADO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[ESTADO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrDiaSuscribe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrDiaSuscribe.Click
        'Agrega tag de campo de [DIA] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[DIA]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrMesSuscribe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrMesSuscribe.Click
        'Agrega tag de campo de [MES] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[MES]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrAnioSuscribe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrAnioSuscribe.Click
        'Agrega tag de campo de [ANIO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[ANIO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_FechaPagoPagare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_FechaPagoPagare.Click
        'Agrega tag de campo de [FECHA_PAGO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[FECHA_PAGO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrMonto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrMonto.Click
        'Agrega tag de campo de [MONTO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[MONTO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_NumAbonos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_NumAbonos.Click
        'Agrega tag de campo de [NUM_PAGOS] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[NUM_PAGOS]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrPeriodicidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrPeriodicidad.Click
        'Agrega tag de campo de [PERIODICIDAD] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[PERIODICIDAD]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrTipoPeriodicidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrTipoPeriodicidad.Click
        'Agrega tag de campo de [TIPO_PERIODICIDAD] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[TIPO_PERIODICIDAD]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrTasaInteresNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrTasaInteresNormal.Click
        'Agrega tag de campo de [TASA_NORMAL] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[TASA_NORMAL]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrTasaInteresMora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrTasaInteresMora.Click
        'Agrega tag de campo de [TASA_MORA] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[TASA_MORA]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrVencimientoAnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrVencimientoAnt.Click
        'Agrega tag de campo de [VENCIMIENTO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[VENCIMIENTO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_TasaCastigo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_TasaCastigo.Click

        'Agrega tag de campo de [TASA_CASTIGO] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[TASA_CASTIGO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_AgrMontoLetra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrMontoLetra.Click

        'Agrega tag de campo de [MONTO_LETRA] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[MONTO_LETRA]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub lnk_TipoCobro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_TipoCobro.Click

        'Agrega tag de campo de [MONTO_LETRA] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[TIPO_COBRO]"
        txt_PlantillaPagare.Focus()

    End Sub

    Protected Sub btn_VerPagare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_VerPagare.Click

        VerPagare()
        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= Pagare.pdf")
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

    Protected Sub VerPagare()
        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\Archivos\Pagare.pdf")

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

        Dim document As New iTextSharp.text.Document(psize, 50, 50, 90, 100)

        With document
            .AddAuthor("DESARROLLO - MAS FINANCIERA")
            .AddCreationDate()
            .AddCreator("MAS FINANCIERA - PAGARÉ")
            .AddSubject("PAGARÉ - " + Session("PAGAREID"))
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("PAGARÉ - " + Session("PAGAREID"))
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("PAGARÉ - " + Session("PAGAREID"))
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
        'ready to draw text
        ' step 4: we add a paragraph to the document

        cb.BeginText()

        Dim bf As iTextSharp.text.pdf.BaseFont
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 10)

        Dim X, Y As Single
        X = 300
        Y = 750

        'VOY POR LA INFORMACIÓN DEL PROSPECTO

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPAGARE", Session("adVarChar"), Session("adParamInput"), 10, Session("PAGAREID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MOSTRAR_PLANTILLA_PAGARE"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "PAGARÉ", X, Y, 0)

            Dim Parrafo1 As String
            Parrafo1 = Session("rs").Fields("CONTENIDO").Value.ToString

            Dim paragraph1 As New iTextSharp.text.Paragraph(Parrafo1, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL))

            paragraph1.SetAlignment("JUSTIFY")

            document.Add(paragraph1)

            ' Y = document.GetBottom(5)
            Y = 170

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 8)

            Y = Y + 30
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "SUSCRIPTOR (ES)", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________    ____________________________________________", X, Y - 40, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________    ____________________________________________", X, Y - 100, 0)
            Y = Y - 30
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "  FIRMA:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "  FIRMA:", X, Y - 60, 0)
            X = 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "NOMBRE DE SUSCRIPTOR:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "NOMBRE DE SUSCRIPTOR:", X, Y - 60, 0)
            Y = Y - 25
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "DOMICILIO:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "DOMICILIO:", X, Y - 60, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "__________________________________________________________________________________________", X, Y - 10, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "__________________________________________________________________________________________", X, Y - 70, 0)

            X = 300
            Y = Y - 70
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "AVAL (ES)", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________    ____________________________________________", X, Y - 40, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "____________________________________________    ____________________________________________", X, Y - 100, 0)
            Y = Y - 30
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "  FIRMA:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "  FIRMA:", X, Y - 60, 0)
            X = 100
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "NOMBRE DE AVAL:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "NOMBRE DE AVAL:", X, Y - 60, 0)
            Y = Y - 25
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "DOMICILIO:", X, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "DOMICILIO:", X, Y - 60, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "__________________________________________________________________________________________", X, Y - 10, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "__________________________________________________________________________________________", X, Y - 70, 0)

        End If

        Session("Con").Close()
        cb.EndText()
        document.Close()

    End Sub

    Protected Sub btn_NuevoPagare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_NuevoPagare.Click

        LlenaPlantillaPagareCreado()
        Session("PAGAREID") = Nothing
        Response.Redirect("CnfPagares.aspx")

    End Sub

    Protected Sub btn_GuardarEdicion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardarEdicion.Click

        'Boton para guardar un pagare que ha sido editado por un usuario
        If txt_PagareNombre.Text = "" Then
            lbl_AlertaGenerarPlantilla.Text = "Error: Debe ingresar un nombre o descripción"
        ElseIf txt_PlantillaPagare.Text = "" Then
            'No dejara crear una plantilla si no tiene algo escrito el textbox
            lbl_AlertaGenerarPlantilla.Text = "Error: Documento en Blanco"
        ElseIf txt_PlantillaPagare.Text.Length > 5000 Then
            lbl_AlertaGenerarPlantilla.Text = "Error: Solo 5000 caracteres o menos en la descripción"
        Else
            ActualizarPagare()
            lbl_AlertaGenerarPlantilla.Text = "Pagaré Actualizado"
            btn_VerPagare.Enabled = True
            LlenaPlantillaPagareCreado()
        End If

    End Sub

    Protected Sub LlenaPlantillaPagareCreado()

        'Se llena una tabla con los pagares creados hasta la fecha

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtListaPlantillaPagare As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PLANTILLAS_PAGARE"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtListaPlantillaPagare, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_PlantPag.DataSource = dtListaPlantillaPagare
        DAG_PlantPag.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub DAG_PlantPag_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_PlantPag.ItemCommand

        'Funcion de boton editar
        Dim idpagare As Integer
        If (e.CommandName = "EDITAR") Then

            Session("PAGAREID") = e.Item.Cells(0).Text
            idpagare = e.Item.Cells(0).Text

            cmb_TipoCobro.Enabled = True
            EditarPlantillaPagare(idpagare)
            btn_GeneraPlantillaPagare.Enabled = False
            btn_VerPagare.Enabled = True
            btn_GuardarEdicion.Enabled = True
            lbl_AlertaGenerarPlantilla.Text = ""

        End If

        'DAG_PlantPag.DataBind()

    End Sub

    Protected Sub EditarPlantillaPagare(ByVal idpagare As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPAGARE", Session("adVarChar"), Session("adParamInput"), 15, idpagare)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONTENIDO_PAGARE"

        Session("rs") = Session("cmd").Execute()
        txt_PagareNombre.Enabled = True
        txt_PlantillaPagare.Enabled = True
        txt_PagareNombre.Text = Session("rs").Fields("NOMBRE").Value.ToString
        txt_PlantillaPagare.Text = Session("rs").Fields("CONTENIDO").Value.ToString
        cmb_TipoCobro.SelectedValue = Session("rs").Fields("TIPO_COBRO").Value.ToString

        Session("Con").Close()

    End Sub

    Protected Sub ActualizarPagare()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPAGARE", Session("adVarChar"), Session("adParamInput"), 10, Session("PAGAREID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_PagareNombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CONTENIDO", Session("adVarChar"), Session("adParamInput"), 50000, txt_PlantillaPagare.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_COBRO", Session("adVarChar"), Session("adParamInput"), 15, cmb_TipoCobro.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONTENIDO_PAGARE"

        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Protected Sub btn_Aceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Aceptar.Click

        Session("PRODID") = cmb_Productos.SelectedItem.Value
        Session("PAGAREID") = Nothing
        LlenaPagares()
        TabPanel1.Enabled = True
        TabContainer1.ActiveTabIndex = 1

    End Sub

    'Protected Sub btn_VerPagare0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_VerPagare0.Click

    '    If Lst_asignados.SelectedItem Is Nothing And Lst_disponibles.SelectedItem Is Nothing Then
    '        lbl_status.Text = "Error: Seleccione un Pagaré"
    '        lbl_status.Visible = True
    '    ElseIf Lst_asignados.SelectedItem Is Nothing And Not (Lst_disponibles.SelectedItem Is Nothing) Then
    '        Session("PAGAREID") = Lst_disponibles.SelectedItem.Value
    '        VerPagare()
    '        With Response
    '            .BufferOutput = True
    '            .ClearContent()
    '            .ClearHeaders()
    '            .ContentType = "application/octet-stream"
    '            .AddHeader("Content-disposition",
    '                       "attachment; filename= Pagare.pdf")
    '            Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '            Response.Cache.SetNoServerCaching()
    '            Response.Cache.SetNoStore()
    '            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

    '            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

    '            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
    '            .End()
    '            .Flush()
    '        End With
    '    ElseIf Not (Lst_asignados.SelectedItem Is Nothing) And Lst_disponibles.SelectedItem Is Nothing Then
    '        Session("PAGAREID") = Lst_asignados.SelectedItem.Value
    '        VerPagare()
    '        With Response
    '            .BufferOutput = True
    '            .ClearContent()
    '            .ClearHeaders()
    '            .ContentType = "application/octet-stream"
    '            .AddHeader("Content-disposition",
    '                       "attachment; filename= Pagare.pdf")
    '            Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '            Response.Cache.SetNoServerCaching()
    '            Response.Cache.SetNoStore()
    '            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

    '            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

    '            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
    '            .End()
    '            .Flush()
    '        End With
    '    End If

    'End Sub

    Protected Sub cmb_Productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Productos.SelectedIndexChanged

        TabPanel1.Enabled = False
        lbl_status.Text = ""

    End Sub

    Protected Sub lnk_AgrMontodisp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_AgrMontodisp.Click
        'Agrega tag de campo de [MONTODISP] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[MONTODISP]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_disp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_disp.Click
        'Agrega tag de campo de [NUMDISP] para el pagare
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[NUMDISP]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_fechacorte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_fechacorte.Click
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[FECHACORTE]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_fecha_vigencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_fecha_vigencia.Click
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[FECHAVIGENCIA]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_fecha_contrato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_fecha_contrato.Click
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[FECHACONTRATO]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_contrato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_contrato.Click
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[NUMCONTRATO]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_acreditado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_acreditado.Click
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[ACREDITADO]"
        txt_PlantillaPagare.Focus()
    End Sub

    Protected Sub lnk_monto_Carta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_monto_Carta.Click
        txt_PlantillaPagare.Text = txt_PlantillaPagare.Text + "[MONTO_CARTA]"
        txt_PlantillaPagare.Focus()
    End Sub

End Class