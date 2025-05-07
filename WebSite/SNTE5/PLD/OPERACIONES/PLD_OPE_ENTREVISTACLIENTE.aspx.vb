Public Class PLD_OPE_ENTREVISTACLIENTE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Entrevista Cliente", "ENTREVISTA CLIENTE")

        If Not Me.IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("Login.aspx")
            End If

        End If
        LlenaEntPendientes()
    End Sub

    '-------------------- ENTREVISTAS PENDIENTES POR CAPTURAR --------------------
    Private Sub LlenaEntPendientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtEntPend As New Data.DataTable()
        dag_EntPend.DataSource = Nothing
        dag_EntPend.DataBind()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ENTREVISTAS_PENDIENTES_X_SUCURSAL"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtEntPend, Session("rs"))
        Session("Con").Close()

        If dtEntPend.Rows.Count > 0 Then
            dag_EntPend.Visible = True
            dag_EntPend.DataSource = dtEntPend
            dag_EntPend.DataBind()
        Else
            dag_EntPend.Visible = True
        End If

    End Sub

    '-------------------- PARA IMPRIMIR FORMATO ENTREVISTA --------------------
    Protected Sub lnk_FormatoEntrevista_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_FormatoEntrevista.Click
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud
        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EntrevistaPLD1.pdf")

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
            .AddAuthor("Entrevista Perfil - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Entrevista Perfil")
            .AddSubject("Entrevista Perfil")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Entrevista Perfil")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Entrevista Perfil")
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

        document.NewPage()
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EntrevistaPLD2.pdf")

        cb = writer.DirectContent
        Solicitud = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Solicitud, 1, 0, 0, 1, 0, 0)
        document.Close()

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= EntrevistaPLD.pdf")
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

    Private Sub dag_EntPend_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_EntPend.ItemCommand

        Session("IDALERTA") = e.Item.Cells(0).Text
        Session("PERSONAID") = e.Item.Cells(1).Text
        Session("FOLIO") = e.Item.Cells(3).Text
        Session("NOMBRE_PERSONA") = e.Item.Cells(2).Text

        If (e.CommandName = "DIGITALIZAR") Then
            CreaRegistroEntrevista()

            Session("VENGODE") = "PLD_OPE_ENTREVISTACLIENTE.aspx"
            Response.Redirect("../../DIGITALIZADOR/DIGI_GLOBAL.aspx")
        End If

        If (e.CommandName = "CAPTURAR") Then

            Session("idperbusca") = Nothing
            Session("IdPersonaModificar") = Nothing
            Session("VENGODE") = "PLDEntrevistaCliente.aspx"

            If RevisaDigitEntrevista() = 1 Then

                lbl_Status.Text = ""
                pnl_captura.Visible = True
                'TabContainer1.ActiveTab = TabPanel2

                If tipo_persona() = "F" Then
                    lnk_ResumenPersona.Attributes.Add("OnClick", "ResumenPersona()")
                Else
                    lnk_ResumenPersona.Attributes.Add("OnClick", "ResumenPersonaM()")
                End If

                txt_TerCP.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_TerCP.ClientID + "','" + btn_Terbuscadat.ClientID + "')")
                lnk_TerBusquedaCP.Attributes.Add("OnClick", "busquedaCP()")

                txt_MoralCP.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_MoralCP.ClientID + "','" + btn_Moralbuscadat.ClientID + "')")
                lnk_MoralBusquedaCP.Attributes.Add("OnClick", "busquedaCP()")

                LlenaAlertas()
                Llenatiporelacion()
                LlenaPaises()
                rad_SiEnt.Checked = True
                pnl_NoRealizoEnt.Visible = False
                pnl_SiRealizoEnt.Visible = True
                lbl_IDAlerta1.Text = "" + Session("IDALERTA")
                lbl_Cliente1.Text = "" + Session("NOMBRE_PERSONA")
                lbl_Folio1.Text = "" + Session("FOLIO")

            Else
                pnl_captura.Visible = False
                lbl_Status.Text = "Error: Debe digitalizar la entrevista firmada antes de capturar."
                Session("VENGODE") = Nothing
            End If

        End If

    End Sub

    '-------------------- TIPO DE PERSONA PARA RESUEMEN DE PERSONA --------------------
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

    '-------------------- OPERACIONES ACTIVADAS POR ALERTA --------------------
    Private Sub LlenaAlertas()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtAlertasPLD As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_ALERTA_PLD"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAlertasPLD, Session("rs"))
        'se vacian los expedientes al formulario
        dag_Operacion.DataSource = dtAlertasPLD
        dag_Operacion.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub rad_SiEnt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_SiEnt.CheckedChanged
        '-------------------- REALIZO LA ENTREVISTA --------------------
        pnl_NoRealizoEnt.Visible = False
        pnl_SiRealizoEnt.Visible = True
        txt_TerCP.Text = ""

    End Sub

    Protected Sub rad_NoEnt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_NoEnt.CheckedChanged

        '-------------------- NO REALIZO LA ENTREVISTA --------------------
        pnl_NoRealizoEnt.Visible = True
        pnl_SiRealizoEnt.Visible = False

    End Sub

    '-------------------- SE GUARDA LA ENTREVISTA NO CAPTURADA --------------------
    Protected Sub btn_GuardaNoEnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardaNoEnt.Click

        If txt_RazonNoEnt.Text.Length > 5000 Then
            lbl_StatusNoEnt.Text = "Error: La razon por no entrevistar debe contener un maximo de 5000 caracteres."
        ElseIf txt_NoEntObservaciones.Text.Length > 200 Then
            lbl_StatusNoEnt.Text = "Error: Las observaciones deben contener un maximo de 1000 caracteres."
        Else
            InsertaCapturaNoEnt()
            LimpiaDatos()
            lbl_Status.Text = "Guardado correctamente."
            pnl_captura.Visible = False
            'TabContainer1.ActiveTab = TabPanel1
        End If
        LlenaEntPendientes()
    End Sub

    '-------------------- ENTREVISTA NO CAPTURADA SE GURDAN LOS DATOS MINIMOS --------------------
    Private Sub InsertaCapturaNoEnt()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REALIZOENT", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 5000, txt_RazonNoEnt.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("OBSERVACIONES", Session("adVarChar"), Session("adParamInput"), 1000, txt_NoEntObservaciones.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CAPTURA_NO_ENTREVISTA_PLD"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub btn_GuardaSiEnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardaSiEnt.Click

        If txt_OrigenDeposito.Text.Length > 500 Then
            lbl_StatusSiEnt.Text = "Error: El origen de depósito debe contener un maximo de 500 caracteres."
            Exit Sub
        End If
        If cmb_PuestoPublico.SelectedItem.Value = "1" And txt_AntPuestoPublico.Text = "" Then
            lbl_StatusSiEnt.Text = "Error: Debe ingresar antigüedad en puesto público."
            Exit Sub
        End If
        If txt_TiempoEnMex.Text <> "" Then
            If txt_MotivoEnMex.Text = "" Then
                lbl_StatusSiEnt.Text = "Error: Debe registrar motivo por el cual reside en México."
                Exit Sub
            Else
                If txt_MotivoEnMex.Text.Length > 300 Then
                    lbl_StatusSiEnt.Text = "Error: Motivo por el cual reside en México debe contener maximo 300 caracteres."
                    Exit Sub
                End If
            End If
        End If
        If cmb_MoralRelacion.SelectedItem.Value = "OTRO" Then
            If txt_MoralRelOtro.Text = "" Then
                lbl_StatusSiEnt.Text = "Error: No especifico la relación con la persona moral."
                Exit Sub
            End If
        End If

        If txt_SiEntObservaciones.Text.Length > 200 Then
            lbl_StatusSiEnt.Text = "Error: Las observaciones deben contener un maximo de 1000 caracteres."
            Exit Sub
        End If

        InsertaCapturaSiEnt()
        LimpiaDatos()
        LlenaEntPendientes()
        lbl_Status.Text = "Se guardo la captura correctamente."
        pnl_captura.Visible = False
        'TabContainer1.ActiveTab = TabPanel1

    End Sub

    '-------------------- ENTREVISTA CAPTURADA SE GUARDAN LOS DATOS CAPTURADOS --------------------
    Private Sub InsertaCapturaSiEnt()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REALIZOENT", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ORIGEN_DEPOSITO", Session("adVarChar"), Session("adParamInput"), 500, txt_OrigenDeposito.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PER_DEPOSITO", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_PeriodicidadDeposito.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO_PER_DEPOSITO", Session("adVarChar"), Session("adParamInput"), 10, cmb_PeriodicidadDeposito.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))

        ' --------------- PUESTO PUBLICO ---------------
        Session("parm") = Session("cmd").CreateParameter("PUESTO_POL", Session("adVarChar"), Session("adParamInput"), 10, cmb_PuestoPublico.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_PuestoPublico.SelectedItem.Value = 1 Then
            Session("parm") = Session("cmd").CreateParameter("ANT_PUESTO_POL", Session("adVarChar"), Session("adParamInput"), 10, txt_AntPuestoPublico.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("ANT_PUESTO_POL", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        ' --------------- EXTRANJERO ---------------
        If txt_TiempoEnMex.Text <> "" Then
            Session("parm") = Session("cmd").CreateParameter("EXT_TIEMPO_MEX", Session("adVarChar"), Session("adParamInput"), 10, txt_TiempoEnMex.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("EXT_TIEMPO_MEX", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("EXT_MOTIVO_MEX", Session("adVarChar"), Session("adParamInput"), 300, txt_MotivoEnMex.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If txt_TiempoMasMexico.Text <> "" Then
            Session("parm") = Session("cmd").CreateParameter("EXT_TIEMPO_MAS_MEX", Session("adVarChar"), Session("adParamInput"), 10, txt_TiempoMasMexico.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("EXT_TIEMPO_MAS_MEX", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("EXT_CAT_PASAPORTE", Session("adVarChar"), Session("adParamInput"), 50, txt_CategoriaPasaporte.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        ' --------------- MORAL ---------------
        Session("parm") = Session("cmd").CreateParameter("MORAL_NOMBRE", Session("adVarChar"), Session("adParamInput"), 300, txt_MoralNombres.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MORAL_PATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_MoralPaterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MORAL_MATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_MoralMaterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_MoralRelacion.SelectedItem.Value <> "-1" Then
            Session("parm") = Session("cmd").CreateParameter("MORAL_RELACION", Session("adVarChar"), Session("adParamInput"), 100, cmb_MoralRelacion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("MORAL_RELACION", Session("adVarChar"), Session("adParamInput"), 100, "-1")
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        If cmb_MoralRelacion.SelectedItem.Value = "OTRO" Then
            Session("parm") = Session("cmd").CreateParameter("MORAL_RELACION_OTRO", Session("adVarChar"), Session("adParamInput"), 100, txt_MoralRelOtro.Text)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("MORAL_RELACION_OTRO", Session("adVarChar"), Session("adParamInput"), 100, "")
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("MORAL_CALLENUM", Session("adVarChar"), Session("adParamInput"), 500, txt_MoralCalle.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If txt_MoralCP.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("MORAL_CP", Session("adVarChar"), Session("adParamInput"), 5, txt_MoralCP.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MORAL_ID_ASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 20, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MORAL_ID_MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 20, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MORAL_ID_ESTADO", Session("adVarChar"), Session("adParamInput"), 20, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("MORAL_CP", Session("adVarChar"), Session("adParamInput"), 5, txt_MoralCP.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MORAL_ID_ASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Morallocalidad.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MORAL_ID_MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Moralmunicipio.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MORAL_ID_ESTADO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Moralestado.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("MORAL_ID_NACIONALIDAD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Moralpais.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MORAL_TELEFONO", Session("adVarChar"), Session("adParamInput"), 50, txt_MoralTel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MORAL_CELULAR", Session("adVarChar"), Session("adParamInput"), 50, txt_MoralCel.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        ' --------------- TERCERO ---------------
        Session("parm") = Session("cmd").CreateParameter("TER_NOMBRE", Session("adVarChar"), Session("adParamInput"), 300, txt_TerNombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TER_PATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_TerPaterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TER_MATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_TerPaterno.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TER_RELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_TerRelacion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TER_CALLENUM", Session("adVarChar"), Session("adParamInput"), 500, txt_TerCalle.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If txt_TerCP.Text = "" Then
            Session("parm") = Session("cmd").CreateParameter("TER_CP", Session("adVarChar"), Session("adParamInput"), 5, txt_TerCP.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TER_ID_ASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 20, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TER_ID_MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 20, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TER_ID_ESTADO", Session("adVarChar"), Session("adParamInput"), 20, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("TER_CP", Session("adVarChar"), Session("adParamInput"), 5, txt_TerCP.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TER_ID_ASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Tercolonia.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TER_ID_MUNICIPIO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Termunicipio.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TER_ID_ESTADO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Terestado.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("TER_ID_NACIONALIDAD", Session("adVarChar"), Session("adParamInput"), 10, cmb_Terpais.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))

        ' --------------- OTROS ---------------
        Session("parm") = Session("cmd").CreateParameter("OBSERVACIONES", Session("adVarChar"), Session("adParamInput"), 1000, txt_SiEntObservaciones.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CAPTURA_SI_ENTREVISTA_PLD"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    '-------------------- EN CASO DE SI EJERCER PUESTO PUBLICO --------------------
    Protected Sub cmb_PuestoPublico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_PuestoPublico.SelectedIndexChanged

        If cmb_PuestoPublico.SelectedItem.Value = "0" Then
            lbl_AntPuestoPublico.Visible = False
            lbl_AntPuestoPublico0.Visible = False
            txt_AntPuestoPublico.Visible = False
        Else
            lbl_AntPuestoPublico.Visible = True
            lbl_AntPuestoPublico0.Visible = True
            txt_AntPuestoPublico.Visible = True
        End If

    End Sub

    '-------------------- TIPO DE RELACION TERCERA PERSONA --------------------
    Private Sub Llenatiporelacion()

        cmb_TerRelacion.Items.Clear()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_TerRelacion.Items.Add(elija)
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_TerRelacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    '-------------------- DOMICILIOS --------------------
    Protected Sub btn_Terbuscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Terbuscadat.Click
        cmb_Terestado.Items.Clear()
        cmb_Termunicipio.Items.Clear()
        cmb_Tercolonia.Items.Clear()

        If (txt_TerCP.Text) = "" Then
            Exit Sub
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_TerCP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_x_CP"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP
            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString)
            cmb_Terestado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
            cmb_Termunicipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                cmb_Tercolonia.Items.Add(item)

                Session("rs").movenext()
            Loop
        Else 'NO SE ENCONTRO EL CP
            'lbl_cpnoenc.Visible = True
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_Moralbuscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Moralbuscadat.Click
        cmb_Moralestado.Items.Clear()
        cmb_Moralmunicipio.Items.Clear()
        cmb_Morallocalidad.Items.Clear()

        If (txt_MoralCP.Text) = "" Then
            Exit Sub
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_MoralCP.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_x_CP"

        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP
            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString)
            cmb_Moralestado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString)
            cmb_Moralmunicipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                cmb_Morallocalidad.Items.Add(item)

                Session("rs").movenext()
            Loop
        Else 'NO SE ENCONTRO EL CP
            'lbl_cpnoenc.Visible = True
        End If
        Session("Con").Close()
    End Sub

    ' -------------------- NACIONALIDAD --------------------
    Private Sub LlenaPaises()
        'obtengo el catalogo de paises de la bd y lo inserto en los dos combos de lugares de nacimiento

        cmb_Terpais.Items.Clear()
        cmb_Moralpais.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PAISES"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_Terpais.Items.Add(elija)
        cmb_Moralpais.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATPAIS_PAIS").Value.ToString, Session("rs").Fields("CATPAIS_ID_PAIS").Value.ToString)
            cmb_Terpais.Items.Add(item)
            cmb_Moralpais.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    ' -------------------- RELACION CON LA PEROSNA MORAL --------------------
    Protected Sub cmb_MoralRelacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_MoralRelacion.SelectedIndexChanged

        If cmb_MoralRelacion.SelectedItem.Value = "OTRO" Then
            lbl_MoralRelOtro.Visible = True
            txt_MoralRelOtro.Visible = True
        Else
            lbl_MoralRelOtro.Visible = False
            txt_MoralRelOtro.Visible = False
        End If

    End Sub

    Private Sub LimpiaDatos()

        txt_OrigenDeposito.Text = ""
        txt_PeriodicidadDeposito.Text = ""
        cmb_PeriodicidadDeposito.SelectedValue = "-1"
        cmb_PuestoPublico.SelectedValue = "0"
        txt_AntPuestoPublico.Text = ""
        txt_TiempoEnMex.Text = ""
        txt_MotivoEnMex.Text = ""
        txt_TiempoMasMexico.Text = ""
        txt_CategoriaPasaporte.Text = ""

        txt_MoralNombres.Text = ""
        txt_MoralPaterno.Text = ""
        txt_MoralMaterno.Text = ""
        cmb_MoralRelacion.SelectedValue = "-1"
        txt_MoralCP.Text = ""
        cmb_Moralestado.Items.Clear()
        cmb_Morallocalidad.Items.Clear()
        cmb_Moralmunicipio.Items.Clear()
        txt_MoralCalle.Text = ""
        cmb_Moralpais.Items.Clear()
        txt_MoralTel.Text = ""
        txt_MoralCel.Text = ""
        txt_MoralRelOtro.Text = ""

        txt_TerNombre.Text = ""
        txt_TerPaterno.Text = ""
        txt_TerMaterno.Text = ""
        cmb_TerRelacion.Items.Clear()
        txt_TerCP.Text = ""
        cmb_Terestado.Items.Clear()
        cmb_Tercolonia.Items.Clear()
        cmb_Termunicipio.Items.Clear()
        txt_TerCalle.Text = ""
        cmb_Terpais.Items.Clear()

        txt_SiEntObservaciones.Text = ""
        txt_NoEntObservaciones.Text = ""

        txt_RazonNoEnt.Text = ""

        lbl_StatusNoEnt.Text = ""
        lbl_StatusSiEnt.Text = ""

    End Sub

    Private Function RevisaDigitEntrevista() As Integer

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REVISA_ENTREVISTA_PLD_DIGIT"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").Fields("DIGITALIZADO").Value.ToString = "SI" Then
                Session("Con").Close()
                Return 1
            End If
        End If
        Session("Con").Close()
        Return 0

    End Function

    Private Sub CreaRegistroEntrevista()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_ENTREVISTA_PLD"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Private Function RevisaTipoPersona() As Integer
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REVISA_TIPO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").Fields("TIPO").Value.ToString = "AFILIADO" Then
                Session("Con").Close()
                Return 1
            ElseIf Session("rs").Fields("TIPO").Value.ToString = "EXTERNO" Then
                Session("Con").Close()
                Return 2
            Else
                Session("Con").Close()
                Return 0
            End If
        End If
        Session("Con").Close()
        Return 0
    End Function

    'Protected Sub lnk_editar_Click(sender As Object, e As EventArgs) Handles lnk_editar.Click

    '    If RevisaTipoPersona() = 1 Then
    '        Session("PERSONAID") = Session("PERSONAID")
    '        Response.Redirect("../../CORE/PERSONA/CORE_PER_PERSONA.aspx")
    '    ElseIf RevisaTipoPersona() = 2 Then
    '        Session("PERSONAID") = Session("PERSONAID")
    '        Response.Redirect("../../CORE/PERSONA/CORE_PER_PERSONAEXT.aspx")
    '    Else
    '        lbl_Status.Text = "Error: Existe un problema con el cliente, por favor verifique en la edición de personas"
    '    End If
    'End Sub

End Class