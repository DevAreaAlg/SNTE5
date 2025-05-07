Public Class CRED_TES_ENVIO_EFECTIVO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Envío Efectivo", "Envío de Efectivo")
        If Not Me.IsPostBack Then

            Session("ENTRADASALIDA") = "SALIDA"
            llena_usuarios()
            origen()
            llena_cheques()
        End If
        lnk_constancias.Attributes.Add("OnClick", "cons()")
        If Not Session("VAL_BIO") Is Nothing Then
            If Session("VAL_BIO") Then
                Session("FP") = Nothing
                Session("VAL_BIO") = Nothing
                aplica_envio(Session("MONTO"), Session("ID_ORIGEN"), Session("ID_DESTINO"), cmb_transporta.SelectedItem.Value, cmb_recibe.SelectedItem.Value)
                btn_registro.Enabled = True
                If rad_sucursal_ori.Checked And Session("MONTO") > 0.0 Then
                    CajaX_MAC()
                    Session("MONTO_EFECTIVO") = Session("MONTO")
                    ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""TiraEfectivo.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
                    llena_cheques()
                    origen()
                    destino()
                End If
            Else
                lbl_status.Text = "Validacion de huella incorrecta"
                Exit Sub
            End If
        End If
    End Sub

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Sub llena_cheques()
        Lst_en_sucursal.Items.Clear()
        Lst_empaquetados.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 15, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 15, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SBC", Session("adVarChar"), Session("adParamInput"), 1, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("COB", Session("adVarChar"), Session("adParamInput"), 1, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CET", Session("adVarChar"), Session("adParamInput"), 1, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CES", Session("adVarChar"), Session("adParamInput"), 1, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CEM", Session("adVarChar"), Session("adParamInput"), 1, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CCO", Session("adVarChar"), Session("adParamInput"), 1, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CAN", Session("adVarChar"), Session("adParamInput"), 1, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("BANCO").Value.ToString + " - " + Session("rs").Fields("NUMCUENTA").Value.ToString _
                                     + " - " + Session("rs").Fields("ID_CHEQUE").Value.ToString + " - " + Session("rs").Fields("MONTO").Value.ToString _
                                     + " - " + Left(Session("rs").Fields("FECHA_RECIBIDO").Value.ToString, 10), Session("rs").Fields("ID_CHEQUE").Value.ToString)
            If Session("rs").Fields("ESTATUS").Value.ToString <> "EMPAQUETADO" Then
                Lst_en_sucursal.Items.Add(item)
            Else
                Lst_empaquetados.Items.Add(item)
            End If
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llena_usuarios()
        cmb_envia.Items.Clear()
        cmb_recibe.Items.Clear()
        cmb_transporta.Items.Clear()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()
        Dim elija As New ListItem("ELIJA", "-1")
        Dim elija2 As New ListItem("ELIJA", "-1")
        Dim elija3 As New ListItem("ELIJA", "-1")
        cmb_transporta.Items.Add(elija3)
        cmb_envia.Items.Add(elija)
        cmb_recibe.Items.Add(elija2)


        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            Dim item2 As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            Dim item3 As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_transporta.Items.Add(item3)
            cmb_envia.Items.Add(item)
            cmb_recibe.Items.Add(item2)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
        cmb_envia.Items.FindByValue(Session("USERID")).Selected = True
        cmb_envia.Enabled = False
        If rad_banco_ori.Checked Then
            'cmb_recibe.Items.FindByValue(Session("USERID")).Selected = True
            cmb_recibe.Items.FindByValue("-1").Selected = True
            cmb_recibe.Enabled = True
        Else
            cmb_recibe.Items.FindByValue("-1").Selected = True
            cmb_recibe.Enabled = True
        End If
    End Sub


    Private Sub llena_sucursales(ByVal ctrl As String)
        If ctrl <> "des" Then
            cmb_ori.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_ENVIOS_ORIGEN_DESTINO"
            Session("rs") = Session("cmd").Execute()

            Dim elija As New ListItem("ELIJA", "-1")
            cmb_ori.Items.Add(elija)
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                cmb_ori.Items.Add(item)
                Session("rs").movenext()
            Loop
            Session("Con").Close()
        Else
            Dim con_des As Integer
            If rad_banco_ori.Checked Then
                con_des = Session("SUCID")
            Else
                con_des = 0
            End If
            cmb_des.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, con_des)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_ENVIOS_ORIGEN_DESTINO"
            Session("rs") = Session("cmd").Execute()

            Dim elija2 As New ListItem("ELIJA", "-1")
            cmb_des.Items.Add(elija2)
            Do While Not Session("rs").EOF
                Dim item2 As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                cmb_des.Items.Add(item2)
                Session("rs").movenext()
            Loop
            Session("Con").Close()

        End If
    End Sub

    Private Sub llena_bancos(ByVal ctrl As String)
        If ctrl <> "des" Then
            Dim elija As New ListItem("ELIJA", "-1")
            cmb_ori.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
            Session("rs") = Session("cmd").Execute()
            cmb_ori.Items.Add(elija)
            If Not Session("rs").EOF Then
                Do While Not Session("rs").EOF
                    Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                    cmb_ori.Items.Add(item)
                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
        Else
            Dim elija2 As New ListItem("ELIJA", "-1")
            cmb_des.Items.Clear()
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_VENTANILLA_CTAS_BANCOS"
            Session("rs") = Session("cmd").Execute()
            cmb_des.Items.Add(elija2)
            If Not Session("rs").EOF Then
                Do While Not Session("rs").EOF
                    Dim item2 As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CTA").Value.ToString)
                    cmb_des.Items.Add(item2)
                    Session("rs").movenext()
                Loop
            End If
            Session("Con").Close()
        End If
    End Sub

    Private Sub origen()

        lbl_suc_ori.Visible = True
        cmb_ori.Visible = True
        If rad_sucursal_ori.Checked Then
            rad_banco.Enabled = True
            llena_sucursales("ori")
        Else
            rad_sucursal.Checked = True
            rad_banco.Checked = False
            rad_banco.Enabled = False
            llena_bancos("ori")
        End If
        destino()
        llena_usuarios()

    End Sub

    Protected Sub rad_sucursal_ori_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_sucursal_ori.CheckedChanged
        origen()
    End Sub

    Protected Sub rad_banco_ori_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_banco_ori.CheckedChanged
        origen()
    End Sub

    Private Sub destino()
        lbl_suc_des.Visible = True
        cmb_des.Visible = True
        lbl_envia.Visible = True
        lbl_recibe.Visible = True
        lbl_transporta.Visible = True
        cmb_envia.Visible = True
        cmb_recibe.Visible = True
        cmb_transporta.Visible = True
        btn_enviar.Visible = True
        btn_registro.Visible = True
        If rad_sucursal.Checked Then
            llena_sucursales("des")
        Else
            llena_bancos("des")
        End If
        llena_usuarios()

    End Sub

    Protected Sub rad_sucursal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_sucursal.CheckedChanged
        destino()
    End Sub

    Protected Sub rad_banco_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_banco.CheckedChanged
        destino()
    End Sub

    Private Sub cambia_estatus_cheques(ByVal idcheque As String, ByVal estatus As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCHEQUE", Session("adVarChar"), Session("adParamInput"), 100, idcheque)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CHEQUES_ESTATUS"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_add.Click
        Dim i As Integer

        If Lst_en_sucursal.SelectedItem Is Nothing Then
            lbl_status.Text = "Error: Seleccione uno o varios cheques en sucursal."
        Else
            For i = 0 To Lst_en_sucursal.Items.Count - 1
                If Lst_en_sucursal.Items(i).Selected = True Then
                    cambia_estatus_cheques(Lst_en_sucursal.Items(i).Value, "EMPAQUETADO")
                End If
            Next
            llena_cheques()
            lbl_status.Text = "Cheque empaquetado"
        End If
    End Sub

    Protected Sub btn_rem_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_rem.Click
        Dim i As Integer

        If Lst_empaquetados.SelectedItem Is Nothing Then
            lbl_status.Text = "Error: Seleccione uno o varios cheques empaquetados."
        Else
            For i = 0 To Lst_empaquetados.Items.Count - 1
                If Lst_empaquetados.Items(i).Selected = True Then
                    cambia_estatus_cheques(Lst_empaquetados.Items(i).Value, "EN SUCURSAL")
                End If
            Next
            llena_cheques()
            lbl_status.Text = "Cheque en sucursal"
        End If
    End Sub

    Private Sub aplica_envio(ByVal monto As Decimal, ByVal id_origen As Integer, ByVal id_destino As Integer, ByVal id_transporta As Integer, ByVal id_recibe As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 10, monto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_ORIGEN", Session("adVarChar"), Session("adParamInput"), 10, id_origen)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_DESTINO", Session("adVarChar"), Session("adParamInput"), 10, id_destino)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_TRANSPORTA", Session("adVarChar"), Session("adParamInput"), 10, id_transporta)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_RECIBE", Session("adVarChar"), Session("adParamInput"), 10, id_recibe)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_OPERACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Session("ID_OP") = CInt(Session("rs").Fields("ID_OP").Value.ToString)
        End If
        Session("Con").Close()
        lbl_status.Text = "Envío realizado exitosamente!!!"
        txt_monto.Text = ""
    End Sub

    Private Sub ObtieneNuevaSerie()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_OBTIENE_SERIE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            'Session("SERIE") = Session("rs").Fields("SERIE").Value.ToString
            Session("FOLIO_IMP") = Session("rs").Fields("FOLIO_IMP").Value.ToString
        End If
        Session("Con").Close()
    End Sub

    Private Sub llena_registro_envio(ByVal idop As Integer)

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida
        Session("ms") = New System.IO.MemoryStream()

        'Crea un reader para la solictud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\EnvioChequeEfectivo.pdf")

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
            .AddCreator("MASCORE - Transferencia Efectivo")
            .AddSubject("Envio Cheques/Efectivo")
            'Use the filename as the title... You can give it any title of course.        
            .AddTitle("Envio Cheques/Efectivo")
            'Add keywords, whatever keywords you want to attach to it       
            .AddKeywords("Envio Cheques/Efectivo")
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

        Dim X, Y As Single
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        X = 80
        Y = 665

        Dim env_che As Char
        env_che = "n"
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, idop)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_DETALLE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Origen: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ORIGEN").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Destino: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("DESTINO").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Envia: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ENVIA").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Transporta: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("TRANSPORTA").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Recibe: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("RECIBE").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHA").Value.ToString, X, Y, 0)
            X = 80
            Y -= 15

            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto: ", X, Y, 0)
            X += 50
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(Session("rs").Fields("MONTO").Value.ToString), X, Y, 0)
            X = 80
            Y -= 15
        End If
        Session("Con").Close()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, idop)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES_OPERACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Y -= 15
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 11)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "CHEQUES", X, Y, 0)
            Y -= 30
            cb.SetFontAndSize(bf, 9)
            'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Folio", X, Y, 0)
            'X += 90
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Banco", X, Y, 0)
            X += 120
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Número de Cuenta", X, Y, 0)
            X += 85
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Número de Cheque", X, Y, 0)
            X += 120
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Monto", X, Y, 0)
            X += 70
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Fecha Recibido", X, Y, 0)
            Y -= 7
            X = 80
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            cb.SetFontAndSize(bf, 9)
            cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "_____________________________________________________________________________________________", X, Y, 0)
            Y -= 7
            Do While Not Session("rs").EOF
                Y -= 13
                X = 80
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("BANCO").Value.ToString, X, Y, 0)
                X += 120
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("NUMCUENTA").Value.ToString, X, Y, 0)
                X += 85
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("ID_CHEQUE").Value.ToString, X, Y, 0)
                X += 120
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FormatCurrency(Session("rs").Fields("MONTO").Value.ToString), X, Y, 0)
                X += 70
                cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, Session("rs").Fields("FECHA_RECIBIDO").Value.ToString, X, Y, 0)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
        cb.EndText()
        document.Close()
    End Sub

    Private Sub imprime_registro_envio(ByVal idop As Integer)
        llena_registro_envio(idop)
        origen()
        destino()
        'ClientScript.RegisterStartupScript(GetType(String), "db", "<script language='javascript'> {document.getElementById('btn_registro').disabled=true; __doPostBack('', '');}</script>", True)
        With Response

            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= RegistroEnvio.pdf")
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

    Protected Sub btn_enviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_enviar.Click
        lbl_status.Text = ""
        Session("ID_ORIGEN") = cmb_ori.SelectedItem.Value
        Session("ID_DESTINO") = cmb_des.SelectedItem.Value
        Session("CHEQUES_EMP") = Lst_empaquetados.Items.Count
        If cmb_ori.SelectedItem.Value = "-1" Then
            req_ori.Visible = True
            Exit Sub
        Else
            req_ori.Visible = False
        End If
        If cmb_des.SelectedItem.Value = "-1" Then
            req_des.Visible = True
            Exit Sub
        Else
            req_des.Visible = False
        End If
        If cmb_transporta.SelectedItem.Value = "-1" Then
            req_transporta.Visible = True
            Exit Sub
        Else
            req_transporta.Visible = False
        End If
        If cmb_recibe.SelectedItem.Value = "-1" Then
            req_recibe.Visible = True
            Exit Sub
        Else
            req_recibe.Visible = False
        End If
        If cmb_envia.SelectedItem.Value = "-1" Then
            req_envia.Visible = True
            Exit Sub
        Else
            req_envia.Visible = False
        End If
        If rad_banco_ori.Checked Or rad_sucursal_ori.Checked Then
            If Session("CHEQUES_EMP") = 0 And txt_monto.Text = "" Then
                lbl_status.Text = "Error: No existen cheques empaquetados o monto indicado para su envio."
                Exit Sub
            Else
                If Session("CHEQUES_EMP") > 0 And rad_banco_ori.Checked Then
                    lbl_status.Text = "Error: No estan permitidas operaciones con cheques de banco a sucursal."
                    Exit Sub
                Else
                    Session("SERIE") = "ED"
                    ObtieneNuevaSerie()
                    If txt_monto.Text <> "" Then
                        Session("MONTO") = CDec(txt_monto.Text)
                    Else
                        Session("MONTO") = 0.0
                    End If
                    If Validamonto(Session("MONTO").ToString) Then
                        ClientScript.RegisterStartupScript(GetType(String), "Biometrico", "biometric('v');", True)


                        aplica_envio(Session("MONTO"), Session("ID_ORIGEN"), Session("ID_DESTINO"), cmb_transporta.SelectedItem.Value, cmb_recibe.SelectedItem.Value)
                        btn_registro.Enabled = True
                        If rad_sucursal_ori.Checked And Session("MONTO") > 0.0 Then
                            CajaX_MAC()
                            Session("MONTO_EFECTIVO") = Session("MONTO")
                            ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""TiraEfectivo.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
                            llena_cheques()
                            origen()
                            destino()
                        End If
                    Else
                        Exit Sub
                    End If
                End If
            End If
        Else
            lbl_status.Text = "Error: Seleccione el origen del envio."
            Exit Sub
        End If
    End Sub

    Protected Sub btn_registro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_registro.Click
        imprime_registro_envio(Session("ID_OP"))
    End Sub

    Private Sub cancela_op()
        Dim i As Integer
        If Lst_empaquetados.Items.Count = 0 Then
            Exit Sub
        Else
            For i = 0 To Lst_empaquetados.Items.Count - 1
                cambia_estatus_cheques(Lst_empaquetados.Items(i).Value, "EN SUCURSAL")
            Next
        End If
    End Sub

    Private Sub CajaX_MAC()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CAJAS_X_MAC"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Session("IDCAJA_USR") = Session("rs").Fields("IDCAJA").Value.ToString
        End If

        Session("Con").Close()

    End Sub


End Class