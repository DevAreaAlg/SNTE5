Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_EXP_PAGACRED_CHEQUE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Pago de Préstamos por cheque", "Pago de Préstamos por cheque")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            LlenarPréstamos()
            ObtienePrestamos()
            CargaSaldoActual()

        End If
    End Sub

    Private Sub LlenarPréstamos()
        ddl_pres.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_pres.Items.Add(elija)


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PRESTAMOS_PAGADOS_CHEQUE"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("FOLIO").Value.ToString)
            ddl_pres.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub


    Protected Sub btn_imp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_imp.Click
        'pruebaImprimir(CInt(txt_num.Text))
        'PRUEBA()
        PRUEBA_POLIZA()
        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                        "attachment; filename= CHEQUE.pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With
    End Sub
    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar.Click
        '


        '
        'lbl_guardar.Text = "FOLIO: " + ddl_pres.SelectedValue.ToString() + " CHEQUE:" + txt_num.Text

        Dim Hsh As Hashtable = New Hashtable()

        Hsh.Add("@FOLIO", ddl_pres.SelectedValue.ToString())
        Hsh.Add("@VALOR", txt_num.Text)
        Hsh.Add("@IDUSER", Session("USERID"))
        Hsh.Add("@SESION", Session("Sesion"))
        Dim da As New DataAccess()
        Dim GUARDADO As String = da.RegresaUnaCadena("INS_NUM_CHEQUE_PRESTAMO", Hsh)

        If GUARDADO = "OK" Then
            lbl_guardar.Text = "Guardado correctamente."
            ObtienePrestamos()
            txt_num.Text = ""
            LlenarPréstamos()
        Else
            lbl_guardar.Text = "Error no se pudo guardar número de cheque."

        End If
    End Sub
    Protected Sub PRUEBA()
        Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "CHEQUEP7", "CHEQUEP7")
        Dim etiquetas() As String = {"NOMBRE_AGREMIADO", "RFC", "NUMERO", "CANTIDAD", "CANTIDAD_LETRA", "FECHA", "CENTRO_TRAB"}
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 20, txt_num.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, "0")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRUEBA_DATOS_CHEQUE"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()

        For i As Integer = 0 To dteq.Rows.Count - 1
            For j As Integer = 0 To dteq.Columns.Count - 1
                MyPDF.remplazarEtiqueta(etiquetas(j), dteq(i)(j))
            Next
        Next

        MyPDF.save(Response, Session)
    End Sub
    Protected Sub PRUEBA_POLIZA()
        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\Solicitudes\basecheque.pdf")
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
            .AddAuthor("SALTILLO -  SALTILLO")
            .AddCreationDate()
            .AddCreator("SALTILLO - Cheque")
            .AddSubject("Cheque")
            .AddTitle("Cheque")
            .AddKeywords("Cheque")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()

        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Cheque As iTextSharp.text.pdf.PdfImportedPage

        Cheque = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Cheque, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim X2, Y2 As Single
        Dim distanciaHorizontal As Integer = 240.0R
        Dim distanciaVertical As Integer = 15

        X = 450  'X empieza de izquierda a derecha
        ''y estaba en 50
        Y = 735 'Y empieza de abajo hacia arriba

        Y2 = 595
        X2 = 60


        Dim XOrdena As Integer
        Dim YOrdena As Integer

        'DIA, MES, AÑO
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "@@@@@@@@@@@", X, Y, 0)

        cb.EndText()
        document.NewPage()

        document.Close()
        'Return
        'Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "POLIZA_CHEQUE", "POLIZA_CHEQUE")
        'Dim etiquetas() As String = {"NOMBRE_AGREMIADO", "RFC", "NUMERO", "CANTIDAD", "CANTIDAD_LETRA", "FECHA", "CENTRO_TRAB"}
        'Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        'Dim dteq As New Data.DataTable()

        'Session("Con") = CreateObject("ADODB.Connection")
        'Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        'Session("Con").Open()
        'Session("cmd") = New ADODB.Command()
        'Session("cmd").ActiveConnection = Session("Con")
        'Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        'Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 20, txt_num.Text)
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, "0")
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, "")
        'Session("cmd").Parameters.Append(Session("parm"))
        'Session("cmd").CommandText = "SEL_PRUEBA_DATOS_CHEQUE"
        'Session("rs") = Session("cmd").Execute()
        'custDA.Fill(dteq, Session("rs"))
        'Session("Con").Close()

        'For i As Integer = 0 To dteq.Rows.Count - 1
        '    For j As Integer = 0 To dteq.Columns.Count - 1
        '        MyPDF.remplazarEtiqueta(etiquetas(j), dteq(i)(j))
        '    Next
        'Next

        'MyPDF.save(Response, Session)
    End Sub
    'Protected Sub GENERAR_CHEQUE()
    '    Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "CHEQUEP7", "CHEQUEP7")
    '    Dim etiquetas() As String = {"NOMBRE_AGREMIADO", "RFC", "NUMERO", "CANTIDAD", "CANTIDAD_LETRA", "FECHA", "CENTRO_TRAB"}
    '    Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    '    Dim dteq As New Data.DataTable()

    '    Session("Con") = CreateObject("ADODB.Connection")
    '    Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 20, txb_FechaCheque.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("NUMCHEQUE", Session("adVarChar"), Session("adParamInput"), 20, Fila.Cells(8).Text.ToString())
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, "0")
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, "")
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "SEL_DATOS_CHEQUE_AHORRO"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dteq, Session("rs"))
    '    Session("Con").Close()

    '    For i As Integer = 0 To dteq.Rows.Count - 1
    '        For j As Integer = 0 To dteq.Columns.Count - 1
    '            MyPDF.remplazarEtiqueta(etiquetas(j), dteq(i)(j))
    '        Next
    '    Next

    '    MyPDF.save(Response)
    'End Sub
    'Protected Sub GENERAR_POLIZA_CHEQUE()
    '    Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "POLIZA_CHEQUE", "POLIZA_CHEQUE")
    '    Dim etiquetas() As String = {"NOMBRE_AGREMIADO", "RFC", "NUMERO", "CANTIDAD", "CANTIDAD_LETRA", "FECHA", "CENTRO_TRAB"}
    '    Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    '    Dim dteq As New Data.DataTable()

    '    Session("Con") = CreateObject("ADODB.Connection")
    '    Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 20, txb_FechaCheque.Text)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("NUMCHEQUE", Session("adVarChar"), Session("adParamInput"), 20, Fila.Cells(8).Text.ToString())
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, "0")
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, "")
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "SEL_DATOS_CHEQUE_AHORRO"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dteq, Session("rs"))
    '    Session("Con").Close()

    '    For i As Integer = 0 To dteq.Rows.Count - 1
    '        For j As Integer = 0 To dteq.Columns.Count - 1
    '            MyPDF.remplazarEtiqueta(etiquetas(j), dteq(i)(j))
    '        Next
    '    Next

    '    MyPDF.save(Response)
    'End Sub
    Protected Sub pruebaImprimir(tipo As Integer)
        Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "CHEQUEP71", "CHEQUEP71")
        'MyPDF.remplazarEtiqueta("NOMBRE_AGREMIADO", "PRUEBA UNO JOHNSON FREDDIE")
        'MyPDF.prueba(Response)
        'Return
        Dim etiquetas() As String = {"NOMBRE_AGREMIADO", "RFC", "NUMERO", "CANTIDAD", "CANTIDAD_LETRA", "FECHA", "CENTRO_TRAB"}

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 20, txt_num.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, "0")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PRUEBA_DATOS_CHEQUE"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()

        'MyPDF.NUEVAPAGINA(dteq, etiquetas)
        'MyPDF.remplazarEtiqueta("NOMBRE_AGREMIADO", "PRUEBA DOS MERCURY BRIAN")
        'MyPDF.NUEVAPAGINA()
        'MyPDF.NUEVAPAGINA()
        'MyPDF.save(Response)
        'MyPDF.remplazarEtiqueta("NOMBRE_AGREMIADO", "")
        'MyPDF.remplazarEtiqueta("CANTIDAD", "")
        'MyPDF.remplazarEtiqueta("CANTIDAD_LETRA", "")
        'MyPDF.remplazarEtiqueta("FECHA", "")
        'MyPDF.remplazarEtiqueta("RFC", "")
        'MyPDF.remplazarEtiqueta("NUMERO", "")
        'MyPDF.remplazarEtiqueta("CENTRO_TRAB", "")

        '
        Dim MyPDF2 As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "POLIZA_CHEQUEZ", "POLIZA_CHEQUEZ")
        MyPDF2.remplazarEtiqueta("NOMBRE_AGREMIADO", "PRUEBA UNO JOHNSON FREDDIE")
        MyPDF2.save(Response, Session)
        '
    End Sub
    Private Sub ObtienePrestamos()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXPEDIENTES_PORPAGAR_CHEQUE"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()
        If DAG_Analisis.Rows.Count > 0 Then
            btn_pagar.Visible = True
            'btn_prov.Visible = True
            'btn_pagos.Visible = True
            lbl_registros_tol.Text = "Total de préstamos: " + DAG_Analisis.Rows.Count.ToString
        Else
            btn_pagar.Visible = False
            'btn_prov.Visible = False
            'btn_pagos.Visible = False
            lbl_registros_tol.Text = "Total de préstamos: 0"
        End If


    End Sub


    Protected Sub Suma(sender As Object, e As EventArgs)

        Dim data As String = ""
        Dim monto As Decimal = 0.00
        Dim registros As Integer = 0
        Dim presuFin As Decimal = CDec(lbl_presactual.Text)
        Dim dataFin As String = ""

        For Each row As GridViewRow In DAG_Analisis.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chk_Aplicado"), CheckBox)
                If chkRow.Checked Then
                    If (DirectCast(row.FindControl("CLAVE_PRODUCTO"), Label).Text).ToString <> "PD" Then
                        registros += 1
                        monto += (CDec(row.Cells(4).Text))
                        data = monto.ToString("C")
                        presuFin -= (CDec(row.Cells(4).Text))
                        dataFin = presuFin.ToString("C")
                        dataFin = presuFin.ToString("C")
                    End If
                End If
            End If
        Next

        If monto >= 0.00 Then
            btn_pagar.Visible = True
            'btn_pagos.Visible = True
            'btn_prov.Visible = True
        Else
            btn_pagar.Visible = False
            'btn_pagos.Visible = False
            'btn_prov.Visible = False
        End If

        lbl_registros_sel.Text = "Registros Seleccionados: " + registros.ToString
        lbl_acumulado.Text = data
        lbl_presfinal.Text = dataFin

    End Sub

    Private Sub CargaSaldoActual()

        Dim SaldoActual As Decimal

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SALDO_ACTUAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            SaldoActual = Session("rs").Fields("SALDO_ACTUAL").value
            lbl_presactual.Text = SaldoActual.ToString("C")

        End If
        Session("Con").Close()

    End Sub

    Protected Sub btn_GetArchivoDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagar.Click

        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            'PagoCreditosCorreo()
            PagaCreditos()
        Else
            lbl_status.Text = "Error: No ha elegido préstamos para pagar"
        End If

    End Sub

    Private Sub PagaCreditos()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim dtDescuentos As New DataTable()

        dtDescuentos.Columns.Add("FOLIO", GetType(Integer))
        dtDescuentos.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtDescuentos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("INS_PRESTAMOS_CONFIRMADOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("CONFIRMA", SqlDbType.Structured)
                Session("parm").Value = dtDescuentos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lbl_status.Text = "Préstamos entregados correctamente"
            End Using
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            ObtienePrestamos()
            lbl_acumulado.Text = ""
            lbl_registros_sel.Text = "Registros Seleccionados: 0"

        End Try

    End Sub


    Private Sub PagoCreditosCorreo()

        Dim dtDescuentos As New DataTable()
        dtDescuentos.Columns.Add("FOLIO", GetType(String))
        dtDescuentos.Columns.Add("APLICADO", GetType(String))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                dtDescuentos.Rows.Add(DAG_Analisis.Rows(i).Cells(0).Text, "SI")
            Else
                dtDescuentos.Rows.Add(DAG_Analisis.Rows(i).Cells(0).Text, "NO")
            End If
        Next

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "REPCREDPAG")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim subject As String = "Pagos de préstamos autorizados"

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("</br>")
            'sbhtml.Append("<tr><td>Estimado(a) : " + Session("rs").Fields("NOMBRE").Value.ToString + "</td></tr>")
            sbhtml.Append("<tr><td>Se le informa que se han realizado el pago de crédito(s). </td></tr>")
            sbhtml.Append("</br>")
            sbhtml.Append("<tr><td>Favor de descargar órdenes de descuento y verificar .</td></tr>")
            sbhtml.Append("</br></br></br>")
            sbhtml.Append("</table>")

            'Table start.
            sbhtml.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>")
            'Adding HeaderRow.
            sbhtml.Append("<tr>")
            For Each column As DataColumn In dtDescuentos.Columns
                sbhtml.Append(("<th style='background-color: #113964;border: 1px solid #ccc'>" _
                            + (column.ColumnName + "</th>")))
            Next
            sbhtml.Append("</tr>")
            'Adding DataRow.
            For Each row As DataRow In dtDescuentos.Rows
                sbhtml.Append("<tr>")
                For Each column As DataColumn In dtDescuentos.Columns
                    sbhtml.Append(("<td style='width:200px;border: 1px solid #ccc'>" _
                                + (row(column.ColumnName).ToString + "</td>")))
                Next
                sbhtml.Append("</tr>")
            Next
            'Table end.
            sbhtml.Append("</table>")
            'ltTable.Text = sbhtml.ToString

            sbhtml.Append("</br>")
            sbhtml.Append("<table>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("</br></br></br>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub



End Class