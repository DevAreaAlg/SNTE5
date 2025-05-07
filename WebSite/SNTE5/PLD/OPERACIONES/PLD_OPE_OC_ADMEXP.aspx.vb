Public Class PLD_OPE_OC_ADMEXP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Oficial de Cumplimiento", "Operaciones PLD")
        If Not Me.IsPostBack Then


            'Revisa que haya iniciando sesión
            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If

            'lbl_subtitulo.Text = "Operaciones PLD pendientes"

            LlenaAlertasPLDSinSesion()
            LlenaSesionesPendientes()

            lnk_EntrevistaPLD.Attributes.Add("OnClick", "det_EntrevistaPLD()")
            lnk_PersonaPolitica.Attributes.Add("OnClick", "his_PPE()")



        End If

        VerificaCCCActivo()

        If Session("PNL") = 2 Then
            'lbl_subtitulo.Text = "Información de alerta"
            pnl_OpePend.Visible = False
            pnl_DatosOpe.Visible = True

            lbl_Folio.Text = "Datos del expediente: " + Session("FOLIO")
            lbl_Prospecto.Text = Session("PROSPECTO") + " (" + Session("PERSONAID").ToString + ")"


            DetalleOperacion()

            If Session("OPERACION") <> "OPERACION PREOCUPANTE" Then

                DetalleExpediente()
                Empresa()
                If tipo_persona() = "F" Then
                    lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
                Else
                    lnk_persona.Attributes.Add("OnClick", "ResumenPersonaM()")
                End If

                lnk_restructura.Attributes.Add("OnClick", "det_restructura()")

            Else

            End If

            Session("PNL") = Nothing
        End If

    End Sub

    Protected Sub btn_actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_actualizar.Click

        Response.Redirect("PLD_OPE_OC_ADMEXP.aspx")

    End Sub


    Private Sub LlenaAlertasPLDSinSesion()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtOpePend As New Data.DataTable()
        dag_OpePend.DataSource = Nothing
        dag_OpePend.DataBind()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_OPERACIONES_PLD_SINSESION_OC"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtOpePend, Session("rs"))

        Session("Con").Close()

        If dtOpePend.Rows.Count > 0 Then
            dag_OpePend.Visible = True
            dag_OpePend.DataSource = dtOpePend
            dag_OpePend.DataBind()
        Else
            dag_OpePend.Visible = True
        End If

    End Sub

    Private Sub LlenaSesionesPendientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtSesiones As New Data.DataTable()
        dag_Sesiones.DataSource = Nothing
        dag_Sesiones.DataBind()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SESIONES_PLD_ABIERTAS_OC"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtSesiones, Session("rs"))

        Session("Con").Close()

        If dtSesiones.Rows.Count > 0 Then
            dag_Sesiones.Visible = True
            dag_Sesiones.DataSource = dtSesiones
            dag_Sesiones.DataBind()
        Else
            'lbl_SesionesTitulo.Visible = 
            dag_Sesiones.Visible = True
        End If

    End Sub

    Private Sub VerificaCCCActivo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VERIFICA_CCC_NO_ACTIVO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").fields("ACTIVO").value.ToString = "1" Then
                lbl_CCCActivo.Text = ""
                Session("CCCACTIVO") = "1"
            Else
                lbl_CCCActivo.Text = "ALERTA: El comité de comunicación y control se encuentra desactivado. Al cerrar una sesión de alertas el dictamen otorgado por el oficial de cumplimiento será el final."
                Session("CCCACTIVO") = "0"
            End If
            Session("Con").Close()
        End If
    End Sub

    Private Sub LimpiaOpePend()
        Session("IDALEOPE") = Nothing
        Session("IDALERTA") = Nothing
        Session("PERSONAID") = Nothing
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("OPERACION") = Nothing
        Session("SUCURSAL") = Nothing
        Session("FECHAALERTA") = Nothing
        Session("IDOPERACION") = Nothing
        Session("TIPOPRODUCTO") = Nothing
        Session("SESION_COMITE") = Nothing
    End Sub

    Private Sub Limpia()
        Session("IDALEOPE") = Nothing
        Session("IDALERTA") = Nothing
        Session("PERSONAID") = Nothing
        Session("FOLIO") = Nothing
        Session("PROSPECTO") = Nothing
        Session("OPERACION") = Nothing
        Session("SUCURSAL") = Nothing
        Session("FECHAALERTA") = Nothing
        Session("IDOPERACION") = Nothing
        Session("TIPOPRODUCTO") = Nothing
        Session("SESION_COMITE") = Nothing

        txt_observacion.Text = ""
        cmb_Justificado.SelectedValue = "-1"
    End Sub

    Protected Sub dag_OpePend_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_OpePend.ItemCommand

        Session("IDALERTA") = e.Item.Cells(0).Text
        Session("OPERACION") = e.Item.Cells(1).Text
        Session("SUCURSAL") = e.Item.Cells(2).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PROSPECTO") = e.Item.Cells(4).Text
        Session("FOLIO") = e.Item.Cells(5).Text
        Session("FECHAALERTA") = e.Item.Cells(6).Text
        Session("IDOPERACION") = e.Item.Cells(7).Text
        Session("TIPOPRODUCTO") = e.Item.Cells(8).Text
        Session("SESION_COMITE") = e.Item.Cells(9).Text
        Session("IDALEOPE") = e.Item.Cells(10).Text

        If (e.CommandName = "ABRIR") Then
            'lbl_subtitulo.Text = "Asignar alerta a sesión"
            lbl_Status.Text = ""
            pnl_OpePend.Visible = False
            pnl_DatosOpe.Visible = False
            pnl_AbrirAlertas.Visible = False
            pnl_AsignaSesion.Visible = True
            LlenaSesionesAlertasPLD()
        End If

    End Sub

    Private Sub LlenaSesionesAlertasPLD()

        cmb_SesionComite.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_SesionComite.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SESIONES_ALERTAS_PLD"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("SESION").Value.ToString, Session("rs").Fields("IDSESION").Value.ToString)
            cmb_SesionComite.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_SesionComite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_SesionComite.SelectedIndexChanged

        If cmb_SesionComite.SelectedItem.Value = "-1" Then
            dag_AlertaXComite.Visible = False
            btn_AsignarComite.Visible = False
        ElseIf cmb_SesionComite.SelectedItem.Value = "0" Then
            dag_AlertaXComite.Visible = False
            btn_AsignarComite.Visible = True
        Else
            dag_AlertaXComite.Visible = True
            btn_AsignarComite.Visible = True
            LlenaAlertaXComite()
        End If

    End Sub

    Private Sub LlenaAlertaXComite()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAlertas As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ALERTAS_PLD_X_SESION"
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, cmb_SesionComite.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtAlertas, Session("rs"))

        Session("Con").Close()

        If dtAlertas.Rows.Count > 0 Then
            dag_AlertaXComite.Visible = True
            dag_AlertaXComite.DataSource = dtAlertas
            dag_AlertaXComite.DataBind()
        Else
            dag_AlertaXComite.Visible = True
        End If

    End Sub

    Protected Sub btn_AsignarComite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AsignarComite.Click
        AsignaSesion()
        pnl_AsignaSesion.Visible = False
        pnl_DatosOpe.Visible = True
        Dictaminar()
    End Sub

    Private Sub AsignaSesion()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_ASIGNA_SESION_ALERTA_PLD"
        Session("parm") = Session("cmd").CreateParameter("IDSESION_CCC", Session("adVarChar"), Session("adParamInput"), 10, cmb_SesionComite.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Session("SESION_COMITE") = Session("rs").Fields("IDSESCOMIT").Value.ToString

        Session("Con").Close()

    End Sub

    Protected Sub dag_Sesiones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Sesiones.ItemCommand

        Session("SESION_COMITE") = e.Item.Cells(0).Text

        If (e.CommandName = "ABRIR") Then

            LlenaAlertasPLDSesion()
        End If
        If (e.CommandName = "ACTA") Then
            If e.Item.Cells(1).Text <> "0" Then
                lbl_Status.Text = "Error: Aun no se han generado los dictámenes de todas la alertas de esta sesión"
            Else
                lbl_Status.Text = ""
                GenerarActaOC()
            End If
        End If
        If (e.CommandName = "DIGITALIZAR") Then

            If Session("CCCACTIVO") = "0" Then
                If e.Item.Cells(1).Text <> "0" Then
                    lbl_Status.Text = "Error: Aun no se han generado los dictámenes de todas la alertas de esta sesión"
                Else
                    lbl_Status.Text = ""
                    Session("VENGODE") = "AdmExpedientesOC.aspx"
                    Response.Redirect("DigitalizadorGlobal.aspx")
                End If
            Else
                lbl_Status.Text = "No puede digitalizar el reporte ya que se debe generar el acta para sesión de comité de comunicación y control"

            End If


        End If
        If (e.CommandName = "CERRAR") Then
            VerificaAlertasSesiones()
        End If
    End Sub

    Private Sub LlenaAlertasPLDSesion()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAlertaXComite As New Data.DataTable()
        dag_AbrirAlertas.DataSource = Nothing
        dag_AbrirAlertas.DataBind()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACIONES_PLD_PENDIENTES_X_SESION_OC"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtAlertaXComite, Session("rs"))

        Session("Con").Close()

        If dtAlertaXComite.Rows.Count > 0 Then
            'lbl_subtitulo.Text = "Seleccionar alerta por dictaminar"
            lbl_Status.Text = ""
            pnl_OpePend.Visible = False
            pnl_DatosOpe.Visible = False
            pnl_AsignaSesion.Visible = False
            pnl_AbrirAlertas.Visible = True
            dag_AbrirAlertas.Visible = True
            dag_AbrirAlertas.DataSource = dtAlertaXComite
            dag_AbrirAlertas.DataBind()
        Else
            dag_AbrirAlertas.Visible = False
            lbl_Status.Text = "La sesión no tiene alertas pendientes por dictaminar"
        End If

    End Sub

    Protected Sub dag_AbrirAlertas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_AbrirAlertas.ItemCommand

        Session("IDALERTA") = e.Item.Cells(0).Text
        Session("OPERACION") = e.Item.Cells(1).Text
        Session("SUCURSAL") = e.Item.Cells(2).Text
        Session("PERSONAID") = e.Item.Cells(3).Text
        Session("PROSPECTO") = e.Item.Cells(4).Text
        Session("FOLIO") = e.Item.Cells(5).Text
        Session("FECHAALERTA") = e.Item.Cells(6).Text
        Session("IDOPERACION") = e.Item.Cells(7).Text
        Session("TIPOPRODUCTO") = e.Item.Cells(8).Text
        Session("SESION_COMITE") = e.Item.Cells(9).Text
        Session("IDALEOPE") = e.Item.Cells(10).Text

        If (e.CommandName = "ABRIR") Then
            pnl_AbrirAlertas.Visible = False
            pnl_DatosOpe.Visible = True
            Dictaminar()
        End If

    End Sub

    Private Sub VerificaAlertasSesiones()

        Dim Estatus As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_REVISA_ALERTASPLD_X_SESION_PEND_OC"
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Estatus = Session("rs").fields("ESTATUS").value.ToString
            Session("Con").Close()
        End If

        If Estatus = "PENDIENTES" Then
            lbl_Status.Text = "Error: No puede cerrar la sesion mientras existan alertas pendientes asignadas a la misma."
        ElseIf Estatus = "NODIGIT" Then
            lbl_Status.Text = "Error: Debe prellenar y digitalizar el reporte de alertas ya que no existe un comité de comunicación y control"
        Else
            ModalPopupExtender1.Show()
        End If

    End Sub

    Protected Sub btn_CerrarSesion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CerrarSesion.Click
        CerrarSesionOC()
        NotificaComite()
        DictamenCCCNoActivo()
        ModalPopupExtender1.Hide()
        LlenaAlertasPLDSinSesion()
        LlenaSesionesPendientes()
        lbl_Status.Text = "Sesión cerrada y lista para comité de comunicación y control"
    End Sub

    Protected Sub btn_OtraAlerta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_OtraAlerta.Click
        ModalPopupExtender1.Hide()
    End Sub

    Private Sub CerrarSesionOC()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSESION_COMITE", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CERRAR_SESION_ALERTAPLD_OC"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Sub NotificaComite()
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim destino As String = String.Empty
        Dim puesto As String = String.Empty
        'obtengo correos de miembros de comite (IDENTIFICADOR = 1)
        'ENVIO EL CORREO A CADA MIEMBRO

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_MIEMBROS_COMITE"
        Session("parm") = Session("cmd").CreateParameter("CLAVECOMITE", Session("adVarChar"), Session("adParamInput"), 10, "CCC")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            subject = "Nueva Sesión " + "Comité de comunicación y control"
            destino = "Comité de comunicación y control"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) :  " + Session("rs").Fields("NOMBRE").Value.ToString + "</td></tr>")
            sbhtml.Append("<tr><td>Como miembro de :  " + destino + " (" + puesto + ")</td></tr>")
            sbhtml.Append("<tr><td>Se le invita a revisar las alertas reportadas en una nueva sesión con clave :   " + Session("SESION_COMITE") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("</table>")
            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)
            Session("rs").movenext()
        Loop

        Session("Con").Close()



    End Sub




    Private Sub DictamenCCCNoActivo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "UPD_DICTAMEN_OC_CCC_NO_ACTIVO"
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").fields("ACTIVO").value.ToString = "1" Then
                lbl_Status.Text = ""
            Else
                lbl_Status.Text = "EL comité de comunicación y control no esta activado, los dictámenes otorgados por el oficial de cumplimiento fueron el resultado final de las alertas."
            End If
            Session("Con").Close()
        End If
    End Sub

    Private Sub GenerarActaOC()

        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\RepOpePLD.pdf")

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
        X = 445
        Y = 675

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_ACTA_OC"
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DIA_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 40
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("MES_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X + 75
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ANIO_FECHA_SISTEMA").Value.ToString, X, Y, 0)
            X = X - 430
            Y = Y - 43
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "10:00", X, Y, 0)
            X = X + 120
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHA_SESION").Value.ToString, X, Y, 0)
            X = X + 250
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CAJA DE LA SIERRA GORDA", X, Y, 0)
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
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

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
        Session("parm") = Session("cmd").CreateParameter("IDSESCOMIT", Session("adVarChar"), Session("adParamInput"), 10, Session("SESION_COMITE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").EOF
                If (Y - 80) < 20 Then
                    cb.EndText()
                    X = 300
                    Y = 750

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TEMPORAL.pdf")

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
                       "attachment; filename= ReporteAlertasPLD(" + Session("SESION_COMITE") + ").pdf")
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

    ' INFORMACION, DETALLE Y DICTAMINACION DE DATOS DE UNA ALERTA

    Private Sub Dictaminar()

        'lbl_subtitulo.Text = "Información de Alerta"
        pnl_OpePend.Visible = False
        pnl_DatosOpe.Visible = True
        pnl_AsignaSesion.Visible = False

        lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
        lbl_Prospecto.Text = Session("PROSPECTO") + " (" + Session("PERSONAID").ToString + ")"


        DetalleOperacion()

        If Session("OPERACION") <> "OPERACION PREOCUPANTE" Then

            DetalleExpediente()
            Empresa()
            If tipo_persona() = "F" Then
                lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")
            Else
                lnk_persona.Attributes.Add("OnClick", "ResumenPersonaM()")
            End If

            lnk_restructura.Attributes.Add("OnClick", "det_restructura()")


        Else

        End If



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

    Private Sub DetalleOperacion()

        lbl_Status.Text = ""
        lbl_IDAlertaM.Text = Session("IDALERTA")
        lbl_TipoOpeM.Text = Session("OPERACION")
        lbl_SucursalM.Text = Session("SUCURSAL")
        lbl_FechaAlertaM.Text = Session("FECHAALERTA")

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DETALLE_OPERACION_PLD"
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        lbl_FolioImpM.Text = Session("rs").Fields("FOLIO_IMP").value.ToString
        lbl_MontoOpePLDM.Text = FormatCurrency(Session("rs").Fields("MONTO").value.ToString)
        lbl_NotaOpePLDM.Text = Session("rs").Fields("NOTA").value.ToString

        If Session("OPERACION") = "OPERACION PREOCUPANTE" Then
            lbl_RealizoEntM.Text = "-"
            lnk_EntrevistaPLD.Enabled = False
            lnk_HistorialAlertas.Enabled = False
            lnk_PersonaPolitica.Enabled = False
            lbl_TipoPerfilM.Text = "-"
            lbl_PerfilPersonaM.Text = "-"
        Else
            lnk_HistorialAlertas.Enabled = True
            lnk_PersonaPolitica.Enabled = True

            If Session("rs").Fields("REALIZO_ENT").value.ToString = "-1" Then
                lbl_RealizoEntM.Text = "ENTREVISTA AUN NO CAPTURADA"
                lnk_EntrevistaPLD.Enabled = False
            ElseIf Session("rs").Fields("REALIZO_ENT").value.ToString = "1" Then
                lbl_RealizoEntM.Text = "ENTREVISTA REALIZADA CON EXITO"
                lnk_EntrevistaPLD.Enabled = True
            Else
                lbl_RealizoEntM.Text = "ENTREVISTA NO REALIZADA"
                lnk_EntrevistaPLD.Enabled = True
            End If
            lbl_TipoPerfilM.Text = Session("rs").Fields("TIPO_PERFIL").value.ToString
            lbl_PerfilPersonaM.Text = FormatCurrency(Session("rs").Fields("PERFIL").value.ToString)
        End If

        Session("Con").Close()

    End Sub

    Private Sub DetalleExpediente()

        If Session("TIPOPRODUCTO") = "1" Then

            pnl_DatosCred.Visible = True
            pnl_DatosCaptacion.Visible = False

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_ANAEXP_DETALLE_EXPEDIENTE"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            lbl_ProductoDetalleB.Text = Session("rs").Fields("PRODUCTO").value.ToString
            lbl_MontoB.Text = Session("rs").Fields("MONTO").value.ToString
            lbl_PlazoB.Text = Session("rs").Fields("PLAZO").value.ToString
            lbl_PeriodicidadB.Text = Session("rs").Fields("PERIODICIDAD").value.ToString
            lbl_TasaNormalB.Text = Session("rs").Fields("TASA_NORMAL").value.ToString
            lbl_TasaMoraB.Text = Session("rs").Fields("TASA_MORA").value.ToString
            lbl_fechaliberaB.Text = Session("rs").Fields("AUX_FECHA_LIBERA").value.ToString
            'If Session("rs").Fields("MINISTRACIONES").value.ToString() = "1" Then
            'lbl_ministraB.Text = "SI"
            'Else
            'lbl_ministraB.Text = "NO"
            'End If

            Session("TIPOPLANPAGO") = Session("rs").Fields("TIPOPLAN").value
            Select Case Session("TIPOPLANPAGO")
                Case "SI"
                    lbl_tipoplanB.Text = "SALDOS INSOLUTOS"
                Case "PFSI"
                    lbl_tipoplanB.Text = "PAGOS FIJOS SI"
                Case "ES"
                    lbl_tipoplanB.Text = "PLAN ESPECIAL"
            End Select

            Session("Con").Close()

            lnk_garantias.Visible = True
            lnk_gastos.Visible = True
            lnk_restructura.Visible = True

        ElseIf Session("TIPOPRODUCTO") = "2" Then

            pnl_DatosCred.Visible = False
            pnl_DatosCaptacion.Visible = True

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DETALLE_EXPEDIENTE_CAPTACION"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            lbl_ProductoCap1.Text = Session("rs").Fields("PRODUCTO").value.ToString
            lbl_TasaCap1.Text = Session("rs").Fields("TASA_CAP").value.ToString
            lbl_SaldoCap1.Text = FormatCurrency(Session("rs").Fields("SALDO").value.ToString)
            lbl_UltFechaDepCap1.Text = Session("rs").Fields("ULT_FECHA_DEP").value.ToString
            lbl_UltFechaRetCap1.Text = Session("rs").Fields("ULT_FECHA_RET").value.ToString

            Session("Con").Close()

            lnk_garantias.Visible = False
            lnk_gastos.Visible = False
            lnk_restructura.Visible = False

        Else

            lnk_garantias.Visible = False
            lnk_gastos.Visible = False
            lnk_restructura.Visible = False

        End If


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
            Session("EMPRESA") = Session("rs").fields("RAZON").value
        End If
        Session("Con").Close()

    End Sub

    Protected Sub lnk_HistorialAlertas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_HistorialAlertas.Click

        Session("VENGODE") = "AdmExpedientesOC.aspx"
        Session("PNL") = 2
        Response.Redirect("../ALERTAS/PLD_ALE_HISTORIAL.aspx")

    End Sub

    Protected Sub lnk_garantias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_garantias.Click
        Session("VENGODE") = "AdmExpedientesOC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_GARANTIA.aspx")
    End Sub

    Protected Sub lnk_historial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_historial.Click
        Session("VENGODE") = "AdmExpedientesOC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_HISTORIAL.aspx")
    End Sub

    Protected Sub lnk_docsexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_docsexp.Click
        Session("VENGODE") = "AdmExpedientesOC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_EXP_DIGITAL.aspx")
    End Sub

    Protected Sub lnk_redsocial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_redsocial.Click
        Session("VENGODE") = "AdmExpedientesOC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../UNIVERSAL/UNI_RED_SOCIAL.aspx")
    End Sub

    Protected Sub lnk_gastos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_gastos.Click
        Session("VENGODE") = "AdmExpedientesOC.aspx"
        Session("PNL") = 2
        Response.Redirect("../../CREDITO/EXPEDIENTES/CRED_EXP_INVESTIGACIONES.aspx")
    End Sub


    Protected Sub lnk_PlanPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_PlanPagos.Click
        Response.Redirect("PLAN_PAGOS_GENERAL.aspx")
    End Sub

    Protected Sub btn_Resultado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Resultado.Click

        If txt_observacion.Text.Length > 5000 Then
            lbl_statusresultado.Text = "Error: Las observaciones debe contener un maximo de 5000 caracteres."
            Exit Sub
        End If
        lbl_statusresultado.Text = ""
        TerminarRevision()
        pnl_DatosOpe.Visible = False
        pnl_OpePend.Visible = True
        Limpia()
        LlenaAlertasPLDSinSesion()
        LlenaSesionesPendientes()

    End Sub

    Private Sub TerminarRevision()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDOPERACION", Session("adVarChar"), Session("adParamInput"), 10, Session("IDOPERACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RESULTADO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Justificado.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("OBSERVACIONES", Session("adVarChar"), Session("adParamInput"), 5000, txt_observacion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDALEOPE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALEOPE").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_DICTAMEN_OFICIAL_CUMPLIMIENTO"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

End Class