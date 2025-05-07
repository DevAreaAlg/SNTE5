Public Class CAP_EXP_APARTADO1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Parámetros Generales", "PARÁMETROS GENERALES")

        If Not Me.IsPostBack Then
            Session("VENGODE") = "CAP_EXP_APARTADO1.ASPX"
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")

            Llenapersona()
            IniciaContador()
            Llenatasas()
            TASAS()
            LlenaNota()
            DatosTasa()

            Llenareferencias()



            If cmb_tipotasa.SelectedItem.Value = "FIJ" Then
                RequiredFieldValidator_puntos.Visible = False
            ElseIf cmb_tipotasa.SelectedItem.Value = "IND" Then
                RequiredFieldValidator_tasa.Visible = False
            End If


        End If

        'Regresa el id de la persona mancomunada, o firma de la busqueda de persona
        lnk_busqMan.Attributes.Add("OnClick", "busquedapersonafisica(1)")

        If Session("idperbusca") <> Nothing Then
            lbl_nombre1.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If



        lnk_busq.Attributes.Add("OnClick", "busquedapersonafisica(2)")
        If Session("idperbusca_Usuario") <> Nothing Then
            lbl_nombrebusqueda.Text = Session("idperbusca_Usuario").ToString
            Session("idperbusca_Usuario") = Nothing
        End If



    End Sub




    'Llena las personas  que tiene agregadas esa persona.
    Private Sub Llenapersona()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtpersona As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PERSONA_CAPTACION_VISTA"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtpersona, Session("rs"))
        DAG_persona.DataSource = dtpersona
        DAG_persona.DataBind()

        Session("Con").Close()
    End Sub

    Private Sub DAG_persona_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_persona.ItemCommand

        If (e.CommandName = "ELIMINAR") Then

            lbl_nombre1.Text = ""
            lbl_statusmancomunado.Text = ""
            Elimina_persona(e.Item.Cells(0).Text)
            Llenapersona()


        End If

    End Sub

    'Elimina completamente la persona de la BD
    Private Sub Elimina_persona(ByVal personaid As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_MANCOMUNADA_CAPTACION_VISTA"
        Session("cmd").Execute()

        Session("Con").Close()

        IniciaContador()


        Session("idperbusca") = Nothing

    End Sub

    '**********************TASAS********************

    'Muestra los datos de la tasa
    Private Sub DatosTasa()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_TASA_CAPTACION_VISTA"
        Session("rs") = Session("cmd").Execute()
        'Envío los datos de la tasa guardada

        If Not Session("rs").EOF Then

            If Session("rs").fields("INDICE").value.ToString = Nothing Then
                cmb_tipotasa.SelectedValue = "FIJ"
                lbl_tasa.Text = "*Tasa: (Desde " + Session("TASAMIN") + " hasta " + Session("TASAMAX") + ") "
                txt_tasa.Text = Session("rs").fields("TASA").value.ToString
                txt_puntos.Enabled = False
            Else
                cmb_tipotasa.SelectedValue = "IND"
                txt_tasa.Enabled = False
                lbl_indice.Text = Session("rs").fields("INDICE").value.ToString
                lbl_puntos.Text = " *Puntos: (Desde " + Session("TASAMININD") + " hasta " + Session("TASAMAXIND") + ") "
                txt_puntos.Text = Session("rs").fields("TASA").value.ToString

            End If

        End If

        Session("Con").Close()
    End Sub

    'Muestra los minimos y maximos configurados en el producto
    Private Sub TASAS()

        'Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MINMAX_TASA_CAPTACION_VISTA"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            'MIN Y MAXIMOS DE TASA NORMAL Y TASA MORATORIO
            Session("TASAMIN") = Session("rs").fields("TASAMIN").value.ToString
            Session("TASAMAX") = Session("rs").fields("TASAMAX").value.ToString
            Session("TASAMININD") = Session("rs").fields("TASAMININD").value.ToString
            Session("TASAMAXIND") = Session("rs").fields("TASAMAXIND").value.ToString
            Session("INDICE") = Session("rs").fields("INDICE").value.ToString

            '---------------INDICES NORMALES INDIZADOS--------------
            If Session("rs").fields("INDICE").value.ToString = "1" Then  'INDICE ES 1 ES UDI
                lbl_indice.Text = "UDI"
                lbl_indice.Text = "UDI"
            End If
            If Session("rs").fields("INDICE").value.ToString = "2" Then  'INDICE ES 2 ES TIIE
                lbl_indice.Text = "TIIE 28"
                lbl_indice.Text = "TIIE 28"
            End If

            If Session("rs").fields("INDICE").value.ToString = "3" Then  'INDICE ES 3 ES CETES
                lbl_indice.Text = "CETES 28"
                lbl_indice.Text = "CETES 28"
            End If

        End If
        Session("Con").Close()

    End Sub

    ' De acuerdo al tipo de tasa seleccionado se muestran los diferentes lbls
    Protected Sub cmb_tipotasa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipotasa.SelectedIndexChanged
        lbl_statusmancomunado.Text = ""
        Select Case cmb_tipotasa.SelectedItem.Value.ToString
            Case "FIJ"
                TASAS() 'TRae en variables de sesion los valores minimos y maximos de la tasa
                lbl_tasa.Visible = True
                lbl_tasa.Text = "*Tasa:(Desde " + Session("TASAMIN") + " hasta " + Session("TASAMAX") + ")"
                txt_tasa.Enabled = True
                lbl_puntos.Text = "Puntos %"
                txt_puntos.Enabled = False
                lbl_indice.Visible = False
                txt_puntos.Text = ""
                RequiredFieldValidator_puntos.Visible = False
                RequiredFieldValidator_tasa.Enabled = True
                RequiredFieldValidator_tasa.Visible = True
            Case "IND"
                TASAS() 'TRae en variables de sesion los valores minimos y maximos de la tasa
                lbl_puntos.Visible = True
                lbl_puntos.Text = "*Puntos (Desde " + Session("TASAMININD") + " hasta " + Session("TASAMAXIND") + ")"
                lbl_tasa.Visible = True
                lbl_tasa.Text = "Tasa:"
                lbl_indice.Visible = True
                txt_puntos.Enabled = True
                txt_tasa.Text = ""
                txt_tasa.Enabled = False
                RequiredFieldValidator_tasa.Visible = False
                RequiredFieldValidator_puntos.Enabled = True
                RequiredFieldValidator_puntos.Visible = True

            Case "0"
                lbl_tasa.Text = ""
                lbl_puntos.Text = ""
                lbl_indice.Text = ""
        End Select
    End Sub

    'Llena las tasas 
    Private Sub Llenatasas()

        cmb_tipotasa.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TASAS_CAPTACION_VISTA"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "0")

        cmb_tipotasa.Items.Add(elija)

        Do While Not Session("rs").eof

            If Session("rs").Fields("CLASIFICACION").Value.ToString = "FIJ" Then
                Dim item As New ListItem("FIJA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                cmb_tipotasa.Items.Add(item)

            Else
                Dim item1 As New ListItem("INDIZADA", Session("rs").Fields("CLASIFICACION").Value.ToString)
                cmb_tipotasa.Items.Add(item1)

            End If

            Session("rs").movenext()
        Loop


        Session("Con").Close()
    End Sub

    'Btn Guarda Tasas
    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click

        Dim contNotas As Integer

        contNotas = CInt(txt_notas.Text.Length)

        If contNotas <= 2000 Then
            If cmb_tipotasa.SelectedItem.Value = "FIJ" Then


                If (CDec(txt_tasa.Text) <= CDec(Session("TASAMAX").ToString) And CDec(txt_tasa.Text) >= CDec(Session("TASAMIN").ToString)) Then
                    Session("Con").Open()
                    Session("cmd") = New ADODB.Command()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipotasa.SelectedItem.Value)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, txt_tasa.Text)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 10, 0)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 10, 0)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, txt_notas.Text)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").CommandText = "INS_CNFEXP_TASA_CAPTACION_VISTA"
                    Session("cmd").Execute()
                    Session("Con").Close()

                    lbl_status.Text = "Guardado correctamente"
                    LlenaNota()
                    'limpiatasa()
                Else

                    lbl_status.Text = "Error: Tasa fuera del rango establecido"

                End If

            ElseIf cmb_tipotasa.SelectedItem.Value = "IND" Then

                If (CDec(txt_puntos.Text) <= CDec(Session("TASAMAXIND").ToString) And CDec(txt_puntos.Text) >= CDec(Session("TASAMININD").ToString)) Then
                    Session("Con").Open()
                    Session("cmd") = New ADODB.Command()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("TIPOTASA", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipotasa.SelectedItem.Value)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 10, 0)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("PUNTOS", Session("adVarChar"), Session("adParamInput"), 10, txt_puntos.Text)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 10, Session("INDICE").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, txt_notas.Text)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").CommandText = "INS_CNFEXP_TASA_CAPTACION_VISTA"
                    Session("cmd").Execute()
                    Session("Con").Close()
                    LlenaNota()
                    lbl_status.Text = "Guardado correctamente"
                    'limpiatasa()

                Else
                    lbl_status.Text = "Error: Tasa fuera del rango establecido"

                End If

            End If
        Else
            lbl_status.Text = "Error: Excede el número de caracteres permitidos en notas"
        End If


    End Sub

    'Limpia los controles de tasas
    Private Sub limpiatasa()
        cmb_tipotasa.SelectedIndex = 0
        lbl_tasa.Text = ""
        txt_puntos.Text = ""
        txt_tasa.Text = ""
        lbl_indice.Text = ""

    End Sub

    '******************MANCOMUNADO****************

    'Muestra el nombre de la persona cuando se agrega de Alta de Persona fisica
    Private Sub Datos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "dbo.SEL_CNFEXP_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        'Envío el nombre del nueva persona
        lbl_nombre1.Text = Session("rs").fields("NOMBRE").value.ToString

        Session("Con").Close()
    End Sub

    Private Sub DatosAprobada()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("AUX").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "dbo.SEL_CNFEXP_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()
    End Sub



    Private Sub LlenaNota()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_NOTAS_CAPTACION"
        Session("rs") = Session("cmd").Execute()

        txt_notas.Text = Session("rs").Fields("NOTAS").value.ToString
        Session("Con").Close()
    End Sub

    Private Sub Llenareferencias()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtreferencias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FIRMAS_AUTORIZADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtreferencias, Session("rs"))
        DAG_PerAcep.DataSource = dtreferencias
        DAG_PerAcep.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub DAG_referencias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_persona.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            Elimina_persona(e.Item.Cells(0).Text)
            Llenareferencias()

        End If

    End Sub

    Private Sub Elimina_PersonaAprobada(ByVal IdPersona As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, IdPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_FIRMAS"
        Session("cmd").Execute()
        Session("CONTADOR") = Session("CONTADOR") - 1
        Session("Con").Close()
        IniciaContador()
        Session("idperbusca") = Nothing
    End Sub

    Protected Sub DAG_PerAcep_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_PerAcep.ItemDataBound

        e.Item.Cells(0).Visible = False
        If Session("BLOQUEADO") = "1" Then
            e.Item.Cells(3).Visible = False ' Se pone invisible la columna

        End If
    End Sub

    Private Sub PersonaAceptada_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_PerAcep.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            Elimina_PersonaAprobada(e.Item.Cells(0).Text)

            lbl_bsq_persona.Text = ""
            Llenareferencias()
            lbl_nombrebusqueda.Text = ""

        End If

    End Sub

    'Trae de la BD los valores minimos y máximos de referencias que tiene esa persona
    Protected Sub IniciaContador()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_NUM_FIRMAS"
        Session("rs") = Session("cmd").Execute()
        Session("MAXREFERENCIA") = Session("rs").Fields("MSTPRODUCTOS_NUM_FIRMAS").value.ToString
        Session("MAXREFERENCIAMANCOMUNADAS") = Session("rs").Fields("MSTPRODUCTOS_NUM_PERSONAS_MANCOMUNADAS").value.ToString
        Session("Con").Close()
        If Session("MAXREFERENCIA") = 0 Then
            Button1.Visible = False
        End If

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FIRMAS_REGISTRADAS"
        Session("rs") = Session("cmd").Execute()

        Session("CONTADOR") = Session("rs").Fields("FIRMAS_REGISTRADAS").value.ToString

        Session("Con").Close()


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_NUM_MANCOMUNADO"
        Session("rs") = Session("cmd").Execute()

        Session("CONTADORMANCOMUNADAS") = Session("rs").Fields("PERSONAS_MANCOMUNADAS").value.ToString

        Session("Con").Close()

        Session("DIFERENCIA2") = CInt(Session("MAXREFERENCIA")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA2") < 0 Then
            Session("DIFERENCIA2") = 0
        End If

        lbl_firmasCount.Text = "Faltan: " + Session("DIFERENCIA2").ToString + " firma(s)"

        Session("DIFERENCIA") = CInt(Session("MAXREFERENCIAMANCOMUNADAS")) - CInt(Session("CONTADORMANCOMUNADAS"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If

        lbl_statusmancomunadocont.Text = "Faltan: " + Session("DIFERENCIA").ToString + " mancomunado(s)"

    End Sub

    Private Sub Contador()

        Session("CONTADORMANCOMUNADAS") = Session("CONTADORMANCOMUNADAS") + 1
        Session("DIFERENCIA") = CInt(Session("MAXREFERENCIAMANCOMUNADAS")) - CInt(Session("CONTADORMANCOMUNADAS"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If

        lbl_statusmancomunadocont.Text = "Faltan: " + Session("DIFERENCIA").ToString + " mancomunda(s)"

    End Sub

    Private Sub Contador2()

        Session("CONTADOR") = Session("CONTADOR") + 1
        Session("DIFERENCIA2") = CInt(Session("MAXREFERENCIA")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA2") < 0 Then
            Session("DIFERENCIA2") = 0
        End If

        lbl_firmasCount.Text = "Faltan: " + Session("DIFERENCIA2").ToString + " firmas(s)"

    End Sub

    Protected Sub PrellenadoTarjeta()
        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\TarjetaFirmasAutorizadas.pdf")

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
            .AddAuthor("Desarrollo - MASCORE")
            .AddCreationDate()
            .AddCreator("MASCORE - Tarjeta Firmas Autorizadas")
            .AddSubject("Firmas Autorizadas")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Firmas Autorizadas")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Firmas Autorizadas")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO

        Dim writer As iTextSharp.text.pdf.PdfWriter
        Dim bf As iTextSharp.text.pdf.BaseFont
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Taj_FA As iTextSharp.text.pdf.PdfImportedPage

        Taj_FA = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Taj_FA, 1, 0, 0, 1, 0, 0)

        Dim X, Y As Single
        Dim id_persona As String
        id_persona = 0
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_GENERALES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'ready to draw text
            cb.BeginText()
            'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            '9 es el tamaño de la letra
            cb.SetFontAndSize(bf, 8)

            X = 30  'X empieza de izquierda a derecha
            Y = 675 'Y empieza de abajo hacia arriba
            'Titulo
            '    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plan de Pagos Saldos Insolutos", X, Y, 0)
            X = X
            '  cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha de pago de préstamo: " + "HOLA", X, Y, 0)
            Y = Y - 18
            X = 125

            'Subtitulo de Monto

            id_persona = Session("rs").Fields("IDCLIENTE").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("IDCLIENTE").Value.ToString, X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
            Y = Y - 15

            'Dim nom_persona As String
            'nom_persona = Session("rs").Fields("CLIENTE").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("PROSPECTO").Value, X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
        End If
        Session("Con").Close()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_DOMICILIO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Y = Y - 15
            Dim DomicilioCliente As String
            DomicilioCliente = Session("rs").Fields("CALLE").Value + "  " + Session("rs").Fields("NUMEXT").Value.ToString + "  " + Session("rs").Fields("NUMINT").Value.ToString
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, DomicilioCliente, X, Y, 0)

            Y = Y - 15

            Dim Dir_Persona As String
            Dir_Persona = Session("rs").Fields("ASENTAMIENTO").Value + " " + Session("rs").Fields("MUNICIPIO").Value + "  " + Session("rs").Fields("ESTADO").Value + "  " + Session("rs").Fields("CP").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Dir_Persona, X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)

            'Y = Y - 15

            'Muestro el plazo con su respectiva unidad

            'If 1 = "FIJ" Then 'selecciono tasa fija
            '    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo:           " + Session("UNIDAD") + "    Tasa: " + ("hola 3" + "% Anual"), X, Y, 0)
            'Else 'selecciono tasa indizada
            '    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plazo:           " + Session("UNIDAD") + "    Tasa: " + ("hola 4" + " " + txt_puntos.Text + "% Anual"), X, Y, 0)
            'End If

        End If

        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CONTACTO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, id_persona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Y = Y - 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TELEFONO_MOVIL").Value.ToString + " " + Session("rs").Fields("TELEFONO_PARTICULAR").Value.ToString, X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
        End If
        Session("Con").Close()

        Dim nCoTitular As Integer
        Dim cDomCoTitular As String
        nCoTitular = 0
        cDomCoTitular = ""
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_PERSONA_CAPTACION_VISTA"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'ready to draw text
            'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            '9 es el tamaño de la letra
            cb.SetFontAndSize(bf, 8)

            X = 30  'X empieza de izquierda a derecha
            Y = 675 'Y empieza de abajo hacia arriba
            'Titulo
            '    cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Plan de Pagos Saldos Insolutos", X, Y, 0)
            X = X
            '  cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha de pago de préstamo: " + "HOLA", X, Y, 0)
            Y = Y - 110
            X = 125

            'Subtitulo de Monto
            'Dim id_persona As String
            'id_persona = Session("rs").Fields("IDCLIENTE").Value.ToString
            nCoTitular = Session("rs").Fields("IDMANCOMUNADO").Value
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("IDMANCOMUNADO").Value.ToString, X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
            Y = Y - 15

            'Dim nom_persona As String
            'nom_persona = Session("rs").Fields("CLIENTE").Value.ToString
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)


        End If
        Session("Con").Close()

        If nCoTitular > 0 Then 'Direccion /Telefono CoTitular
            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_DOMICILIO_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, nCoTitular)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                Y = Y - 15
                cDomCoTitular = Session("rs").Fields("CALLE").Value.ToString + "  " + Session("rs").Fields("NUMEXT").Value.ToString + "  " + Session("rs").Fields("NUMINT").Value.ToString
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, cDomCoTitular, X, Y, 0)

                Y = Y - 15

                Dim Dir_Persona As String
                Dir_Persona = Session("rs").Fields("ASENTAMIENTO").Value.ToString + "  " + Session("rs").Fields("ESTADO").Value.ToString + "  " + Session("rs").Fields("CP").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Dir_Persona, X, Y, 0)
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
            End If

            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_CONTACTO_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 11, nCoTitular)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                Y = Y - 15
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TELEFONO_MOVIL").Value.ToString + " " + Session("rs").Fields("TELEFONO_PARTICULAR").Value.ToString, X, Y, 0)
            End If

            Session("Con").Close()
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_TARJETA_FIRMAS_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            X = 50  'X empieza de izquierda a derecha
            Y = 355 'Y empieza de abajo hacia arriba
            '  Dim bf As iTextSharp.text.pdf.BaseFont

            Do While Not Session("rs").eof

                If (Y - 80) < 20 Then
                    cb.EndText()
                    X = 300
                    Y = 680

                    document.NewPage()
                    Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocsPlantillas\TarjetaFirmasAutorizadas02.pdf")

                    cb = writer.DirectContent

                    Taj_FA = writer.GetImportedPage(Reader, 1)
                    cb.AddTemplate(Taj_FA, 1, 0, 0, 1, 0, 0)
                    cb.BeginText()

                    bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                    cb.SetFontAndSize(bf, 8)

                    ' cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "OPERACIONES REVISADAS", X, Y, 0)

                    X = 50
                    Y = Y - 30
                End If



                'ready to draw text


                'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                '9 es el tamaño de la letra
                cb.SetFontAndSize(bf, 8)



                'Subtitulo de Monto
                Dim nom_persona As String
                nom_persona = Session("rs").Fields("NOMBRE").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Nombre: " + Session("rs").Fields("NOMBRE").Value.ToString, X, Y, 0)
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)

                Y = Y - 15
                Dim DomicilioCliente As String
                DomicilioCliente = Session("rs").Fields("CALLE").Value.ToString
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Domicilio: " + DomicilioCliente, X, Y, 0)

                Y = Y - 15

                Dim Dir_Persona As String
                Dir_Persona = Session("rs").Fields("COLONIA").Value.ToString + "  " + Session("rs").Fields("ESTADO").Value.ToString + "  " + Session("rs").Fields("CP").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Colonia: " + Dir_Persona, X, Y, 0)
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)

                Y = Y - 15


                Dim tel_Persona As String
                tel_Persona = Session("rs").Fields("TELEFONO").Value.ToString
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Telefono: " + tel_Persona, X, Y, 0)
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)

                Y = Y - 15



                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Firma:__________________________________________________________________________________ ", X, Y, 0)
                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)

                Y = Y - 45

                Session("rs").movenext()
            Loop




            If (Y - 80) < 20 Then
                cb.EndText()
                X = 300
                Y = 680

                document.NewPage()
                Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocsPlantillas\TarjetaFirmasAutorizadas02.pdf")

                cb = writer.DirectContent

                Taj_FA = writer.GetImportedPage(Reader, 1)
                cb.AddTemplate(Taj_FA, 1, 0, 0, 1, 0, 0)
                cb.BeginText()

                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(bf, 8)



                X = 50
                Y = Y - 30
            End If

            Y = Y - 45
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Documentos Presentados:____________________________________________________________________ ", X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
            X = X + 250
            Y = Y - 45
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, "Jefe de Sucursal:____________________________________ ", X, Y, 0)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "$ " + Session("MASCOREG").FormatNumberCurr("hola 2"), X + 40, Y, 0)
        End If




        Session("Con").Close()
        cb.EndText()
        document.Close()

    End Sub



    Protected Sub btn_guardar_nuevapersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_mancomunada.Click
        Agrega_persona()
    End Sub


    Private Sub Agrega_persona()

        lbl_statusmancomunado.Text = ""

        If lbl_nombre1.Text = Session("PERSONAID") Then

            lbl_statusmancomunado.Text = "Error: No puede asignarse a si mismo"
            'Session("idperbusca") = Nothing
        Else

            If Session("CONTADORMANCOMUNADAS") < Session("MAXREFERENCIAMANCOMUNADAS") Then

                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPERSONAMANCOMUNADA", Session("adVarChar"), Session("adParamInput"), 10, lbl_nombre1.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_CNFEXP_MANCOMUNADO_CAPTACION_VISTA"
                Session("rs") = Session("cmd").Execute()


                If Session("rs").Fields("RESPUESTA").value.ToString = "NOEXISTEPERSONA" Then

                    lbl_statusmancomunado.Text = "Error: No existe el número de cliente " + lbl_nombre1.Text

                ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "PERSONAINCOMPLETA" Then

                    lbl_statusmancomunado.Text = "Error: Persona con datos incompletos"

                ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "YAMANCOMUNADA" Then
                    lbl_statusmancomunado.Text = "La persona está agregada como persona mancomunada"


                ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "PERSONACOMPLETA" Then

                    lbl_statusmancomunado.Text = "Guardado correctamente"
                    Contador()
                    'Session("idperbusca") = Nothing
                End If

                Session("Con").Close()
                LimpiaPersona()
                Llenapersona()

                lbl_nombre1.Text = ""
                lbl_nombrebusqueda.Text = ""

            Else

                lbl_statusmancomunado.Text = "Error: Límite para personas mancomunadas"
                lbl_nombre1.Text = ""
                lbl_nombrebusqueda.Text = ""
                LimpiaPersona()
            End If
        End If





    End Sub

    Private Sub LimpiaPersona()

        lbl_nombre1.Text = ""
        lbl_nombrebusqueda.Text = ""
        Session("AUX") = Nothing
        Session("idperbusca") = Nothing

    End Sub

    Protected Sub Btn_GuardarBusq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_GuardarBusq.Click


        lbl_bsq_persona.Text = ""
        If lbl_nombrebusqueda.Text = Session("PERSONAID") Then
            lbl_bsq_persona.Text = "Error: No puede agregarse a sí mismo"
            LimpiaPersona()

            Session("idperbusca_Usuario") = Nothing
        Else

            If Session("CONTADOR") < Session("MAXREFERENCIA") Then

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDCLIENTE", Session("adVarChar"), Session("adParamInput"), 100, lbl_nombrebusqueda.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                'AGREGO LOS NUEVOS PARAMETROS CREADOS

                Session("cmd").CommandText = "INS_CNFEXP_TARJETAFIRMAPERSONA"
                Session("rs") = Session("cmd").Execute()


                If Session("rs").Fields("RESPUESTA").value.ToString = "NOEXISTEPERSONA" Then

                    lbl_bsq_persona.Text = "Error: No existe el número de afiliado " + lbl_nombrebusqueda.Text

                ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "PERSONAINCOMPLETA" Then

                    lbl_bsq_persona.Text = "Error: Persona con datos incompletos"

                ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "PERSONAYAGAREGADA" Then

                    lbl_bsq_persona.Text = "Error: Esta persona ya fue asiganada como persona autorizada"

                ElseIf Session("rs").Fields("RESPUESTA").value.ToString = "PERSONACOMPLETA" Then

                    lbl_bsq_persona.Text = "Guardado correctamente"

                    Contador2()
                End If

                Session("Con").Close()

                Llenareferencias()
                LimpiaPersona()
                lbl_nombre1.Text = ""
                lbl_nombrebusqueda.Text = ""
                LimpiaPersona()
            Else
                lbl_bsq_persona.Text = "Error: Ya cumple con el máximo de personas autorizadas"
                lbl_nombre1.Text = ""
                lbl_nombrebusqueda.Text = ""
            End If


        End If






    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lbl_bsq_persona.Text = ""




        If Session("DIFERENCIA2") > 0 Then
            lbl_bsq_persona.Text = ""
            lbl_validaPrellenar.Text = "Error: No cumple con el máximo de firmas autorizadas"
        Else

            PrellenadoTarjeta()
            With Response
                .BufferOutput = True
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/octet-stream"
                .AddHeader("Content-disposition",
                       "attachment; filename=TarjetaPrellenada.pdf")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.SetNoServerCaching()
                Response.Cache.SetNoStore()
                Response.Cache.SetMaxAge(System.TimeSpan.Zero)

                Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

                .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
                .End()
                .Flush()
            End With
        End If

    End Sub

End Class