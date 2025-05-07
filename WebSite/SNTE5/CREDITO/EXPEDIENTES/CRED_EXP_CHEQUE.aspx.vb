Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_EXP_CHEQUE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Reposición e Impresión de Cheques", "Reposición e Impresión de Cheques")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona
        End If

        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            validaNumPersona()
        End If

    End Sub

    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        validaNumPersona()
        ObtienePrestamos()
    End Sub

    Private Sub validaNumPersona()
        lbl_estatus.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_estatus.Text = "Error: RFC incorrecto."
                div_NombrePersonaBusqueda.Visible = False
            Else
                'Metodo que llena el droplist con los tipos de productos

                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True

            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
        End If
    End Sub


    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            'lbl_alerta.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
        End If

        Session("Con").Close()

    End Sub

    Private Sub BuscarIDCliente()

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
            Session("SALARIO") = Session("rs").fields("SALARIO").value.ToString
            Session("NETO") = Session("rs").fields("NETO").value.ToString
            Session("CAPACIDAD") = Session("rs").fields("CAPACIDAD").value.ToString
            Session("OTROSCRED") = Session("rs").fields("OTROSCRED").value.ToString
            Session("PORPLAZORENO") = Session("rs").fields("PLAZORENO").value.ToString
            Session("NUMCTRL") = Session("rs").Fields("NUMCONT").value.ToString
            Session("NUMINST") = Session("rs").Fields("INSTI").value.ToString
            Session("ANTIGUEDAD") = Session("rs").Fields("ANTIGUEDAD").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_estatus.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""

        ElseIf Existe = -1 Then
            Session("idperbusca") = ""
            lbl_estatus.Text = "Error: No existe el número de control"
            lbl_NombrePersonaBusqueda.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
        Else
            lbl_estatus.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
        End If

    End Sub

    Private Sub ObtienePrestamos()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REEMPLAZO_NUM_CHEQUE"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

        If DAG_Analisis.Visible = True Then
            btn_reposicion.Visible = True
            btn_imprimir.Visible = True
            btn_eliminar.Visible = True
            txt_num.Visible = True
            lbl_num.Visible = True
        Else
            btn_reposicion.Visible = False
            btn_imprimir.Visible = False
            txt_num.Visible = False
            lbl_num.Visible = False
            btn_eliminar.Visible = False
        End If

    End Sub

    Protected Sub btn_eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_eliminar.Click
        'lbl_guardar.Text = ""

        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Eliminar"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            DeshaceNumCheque()
        Else
        lbl_estatus.Text = "Error: No ha elegido préstamos para deshacer N° de cheque"
        End If

    End Sub
    Protected Sub btn_reposicion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_reposicion.Click
        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Reposicion"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 And txt_num.Text <> "" Then
            ReposicionCheque()
            txt_num.Text = ""
        Else
            lbl_estatus.Text = "Error: Capture N° de cheque."
        End If

    End Sub

    Protected Sub btn_imprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_imprimir.Click

        Dim bandera As Integer = 0
        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Imprimir"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            Session("RFC_PERSONA") = tbx_rfc.ToString
            VER_CHEQUE(Session("RFC_PERSONA"))
        Else
            lbl_estatus.Text = "Error: No ha elegido préstamos para imprimir cheque"
        End If



    End Sub

    Private Sub DeshaceNumCheque()

        Dim custDA As New OleDb.OleDbDataAdapter()
        'Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim dtNumCheque As New DataTable()

        dtNumCheque.Columns.Add("FOLIO", GetType(Integer))
        dtNumCheque.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtNumCheque.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Eliminar"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("INS_DES_NUM_CHEQUE", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("CONFIRMA", SqlDbType.Structured)
                Session("parm").Value = dtNumCheque
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lbl_estatus.Text = "N° de cheque cancelado correctamente"
            End Using
        Catch ex As Exception
            lbl_estatus.Text = ex.Message()
        Finally
            ObtienePrestamos()
        End Try

    End Sub

    Private Sub ReposicionCheque()
        Dim custDA As New OleDb.OleDbDataAdapter()
        'Dim dtMovimientosDescuentos As New Data.DataTable()
        Dim dtNumCheque As New DataTable()

        dtNumCheque.Columns.Add("FOLIO", GetType(Integer))
        dtNumCheque.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtNumCheque.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Reposicion"), CheckBox).Checked))
        Next

        If dtNumCheque.Rows.Count() > 2 Then
            lbl_estatus.Text = "Error: Solo puede seleccionar un préstamo"
        Else
            Try
                Dim dsDatos As String = ""
                Dim Hsh As Hashtable = New Hashtable()

                Hsh.Add("@CONFIRMA", dtNumCheque)
                Hsh.Add("@VALOR", txt_num.Text)
                Hsh.Add("@IDUSER", Session("USERID"))
                Hsh.Add("@SESION", Session("Sesion"))
                Dim da As New DataAccess()
                dsDatos = da.RegresaUnaCadena("INS_NUM_CHEQUE_PRESTAMO_REP", Hsh)

                If dsDatos = "OK" Then
                    lbl_estatus.Text = "N° de cheque guardado correctamente"

                End If
            Catch ex As Exception
                lbl_estatus.Text = ex.Message()
            Finally
                ObtienePrestamos()
            End Try
        End If



    End Sub

    Private Sub VER_CHEQUE(ByVal RFC_PERSONA As String)

        Cheque(RFC_PERSONA)

        With Response
            .BufferOutput = True
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/octet-stream"
            .AddHeader("Content-disposition",
                       "attachment; filename= CHEQUE(RFC:" + Session("NUMTRAB") + ").pdf")
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetNoStore()
            Response.Cache.SetMaxAge(System.TimeSpan.Zero)

            Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

            .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
            .End()
            .Flush()
        End With
    End Sub


    Private Sub Cheque(ByVal RFC_PERSONA As String)

        Dim fecha As String = ""
        Dim nombre As String = ""
        Dim monto As String = 0.00
        Dim montoLetra As String = ""
        Dim nombre2 As String = ""
        Dim rfc As String = ""
        Dim fechaPago As String = ""
        Dim liquido As Decimal = 0.00
        Dim CT As String = ""

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtNumCheque As New DataTable()

        dtNumCheque.Columns.Add("FOLIO", GetType(Integer))
        dtNumCheque.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
            dtNumCheque.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(i).FindControl("chk_Imprimir"), CheckBox).Checked))
        Next

        'Comienza seccion de escritura del pdf 
        'Declara memory stream para salida

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\Cheque\cheque.pdf")
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
            .AddAuthor("SNTE -  SNTE")
            .AddCreationDate()
            .AddCreator("SNTE - Cheque")
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

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los roles a un usuario
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("SEL_CNFEXP_CHEQUE", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("CONFIRMA", SqlDbType.Structured)
                Session("parm").Value = dtNumCheque
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))


                'Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "\DocPlantillas\" + lcUrl)
                Try

                    Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                    While myReader.Read()


                        nombre = myReader.GetString(0)
                        CT = myReader.GetString(1)
                        rfc = myReader.GetString(2)
                        monto = myReader.GetString(3)
                        montoLetra = myReader.GetString(4)
                        fecha = myReader.GetString(5)
                        'nombre = myReader.GetString(6)


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
        Dim distanciaHorizontal As Integer = 240
        Dim distanciaVertical As Integer = 15

        X = 450  'X empieza de izquierda a derecha
        Y = 736 'Y empieza de abajo hacia arriba

        Dim XOrdena As Integer
        Dim YOrdena As Integer

        'FECHA
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "" + fecha, X, Y, 0)
        Y = Y - 45
        X = 60

        'NOMBRE
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "" + nombre, X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                                                                         
                                                                                               " + monto.ToString, X, Y, 0)
        Y = Y - 25
        X = 65

        'MONTO LETRA
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "" + montoLetra, X, Y, 0)
        Y = Y - 145
        X = 60

                        'NOMBRE
                        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "        " + nombre, X, Y, 0)
                        Y = Y - 35
        X = 65


        'RFC
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                   " + rfc, X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                                                                                        " + fecha, X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                                                                                                                                    " + "$" + monto.ToString, X, Y, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                                                                                                                                                                            " + CT, X, Y, 0)
        Y = Y - 60
        X = 65

        'FECHA
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "                                                           " + montoLetra, X, Y, 0)
        Y = Y - 15
        X = 65


        cb.EndText()
        document.Close()
                    End While
                    myReader.Close()
                    document.Close()

                    'worddoc.SaveAs(NewDocName)
                    lbl_estatus.Text = ""
                Catch ex As Exception
                    lbl_estatus.Text = "Error al crear el documento"
                End Try

            End Using

            'End Using
        Catch ex As Exception
            lbl_estatus.Text = "Error de conexión"

        Finally

        End Try

    End Sub

    'Session("Con").Open()
    'Session("cmd") = New ADODB.Command()
    'Session("cmd").ActiveConnection = Session("Con")
    'Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    'Session("cmd").CommandText = "SEL_CNFEXP_CHEQUE"
    'Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
    'Session("cmd").Parameters.Append(Session("parm"))
    'Session("rs") = Session("cmd").Execute()
    'If Not Session("rs").eof Then

    '    fecha = Session("rs").Fields("FECHA").Value.ToString()
    '    nombre = Session("rs").Fields("NOMBRE").Value.ToString()
    '    monto = Session("rs").Fields("MONTO").Value.ToString()
    '    montoLetra = Session("rs").Fields("MONTO_LETRA").Value.ToString()
    '    nombre2 = Session("rs").Fields("NOMBRE").Value.ToString()
    '    rfc = Session("rs").Fields("RFC").Value.ToString()
    '    fechaPago = Session("rs").Fields("FECHA").Value.ToString()
    '    liquido = Session("rs").Fields("MONTO").Value.ToString()
    '    CT = Session("rs").Fields("CLAVE_CT").Value.ToString()


    'End If
    'Session("Con").Close()

    ''Comienza seccion de escritura del pdf 
    ''Declara memory stream para salida

    'Session("ms") = New System.IO.MemoryStream()
    ''Crea un reader para la solicitud

    'Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
    ''Ruta donde está el PDF
    'Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\Cheque\cheque.pdf")
    ''Traigo el total de paginas
    'Dim n As Integer = 0
    'n = Reader.NumberOfPages

    ''Traigo el tamaño de la primera pagina
    'Dim psize As iTextSharp.text.Rectangle
    'psize = Reader.GetPageSize(1)

    'Dim width, height As Single
    'width = psize.Width
    'height = psize.Height

    'Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)


    ''CREACION DE UN WRITER QUE LEA EL DOCUMENTO
    'Dim XT, YT, XAux As Single
    'Dim writer As iTextSharp.text.pdf.PdfWriter
    'writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

    ''Se abre el documento
    'document.Open()
    'Dim cb As iTextSharp.text.pdf.PdfContentByte
    'cb = writer.DirectContent

    '' METO LA SOLICITUD ORIGINAL
    'Dim Cheque As iTextSharp.text.pdf.PdfImportedPage

    'Cheque = writer.GetImportedPage(Reader, 1)
    'cb.AddTemplate(Cheque, 1, 0, 0, 1, 0, 0)



End Class