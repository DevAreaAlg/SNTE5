Imports System.IO
Imports SocialExplorer.IO.FastDBF
Imports Syroot.Windows.IO
Imports WnvWordToPdf

Public Class CRED_VEN_ORDEN_DESC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Orden de Descuentos", "Orden de Descuentos")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            CargaPeriodos()
        End If

    End Sub

    Protected Sub ddl_quincenas_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_quincenas.SelectedIndexChanged

        Limpia()

    End Sub

    Private Sub CargaPeriodos()

        ddl_quincenas.Items.Clear()
        Dim elija As New ListItem("ELIJA", "")
        ddl_quincenas.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FECHAS_ORDEN_DESCUENTO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("FECHADESC").Value.ToString, Session("rs").Fields("FECHA").Value.ToString)
            ddl_quincenas.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub Limpia()

        lbl_estatus.Text = ""

    End Sub

    Protected Sub btn_descargar_calendario_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_descargar.Click

        GenerarDBF()

    End Sub

    Public Sub GenerarDBF()

        Dim NameFile As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 10, ddl_quincenas.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ORDEN_DESCUENTO_NOMBRE"
        Session("rs") = Session("cmd").Execute()
        NameFile = Session("rs").Fields("NOMBRE_ARCHIVO").Value.ToString
        Session("Con").Close()

        Dim PathFile As String = ConfigurationManager.AppSettings.[Get]("urldigi")
        Dim odbf = New DbfFile(Encoding.GetEncoding(1252))
        odbf.Open(Path.Combine(PathFile, NameFile), FileMode.Create)

        odbf.Header.AddColumn(New DbfColumn("RFC", DbfColumn.DbfColumnType.Character, 13, 0))
        odbf.Header.AddColumn(New DbfColumn("NOMBRE", DbfColumn.DbfColumnType.Character, 40, 0))
        odbf.Header.AddColumn(New DbfColumn("DCTO_X_QNA", DbfColumn.DbfColumnType.Number, 10, 2))

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtDescuentos As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 10, ddl_quincenas.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ORDEN_DESCUENTO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtDescuentos, Session("rs"))
        Session("Con").Close()

        Dim orec = New DbfRecord(odbf.Header)
        'orec.AllowDecimalTruncate = True

        If DtDescuentos.Rows.Count > 0 Then

            For Each RowDescuento As DataRow In DtDescuentos.Rows

                orec.Item(0) = RowDescuento("RFC")
                orec.Item(1) = RowDescuento("NOMBRE")
                orec.Item(2) = RowDescuento("DESCUENTO")
                odbf.Write(orec, True)

            Next

        End If

        'orec.RecordIndex = 0
        'odbf.Write(orec)
        'odbf.WriteHeader()
        odbf.Close()

        With Response

            Try

                Dim fs As FileStream
                fs = File.Open(PathFile + "\" + NameFile, FileMode.Open)
                Dim bytBytes(fs.Length) As Byte
                fs.Read(bytBytes, 0, fs.Length)
                fs.Close()

                Response.Buffer = True
                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", NameFile))
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(bytBytes)
                Response.End()

            Catch ex As Exception
                lbl_estatus.Text = ex.ToString
            End Try

        End With

    End Sub

    Protected Sub lnk_orden_descuento_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnk_oficio_descuentos.Click

        lbl_estatus.Text = ""

        If ddl_quincenas.SelectedItem.Value = "" Then
            lbl_estatus.Text = "Error: Selecciona un período para el oficio."
            Exit Sub
        End If

        GenerarOficioDescuento()

    End Sub

    Private Sub GenerarOficioDescuento()

        Dim PlantillaDocumento As String = "oficiodescuentos.DOCX"
        Dim NombreDocumento As String
        Dim ExtensionDocumento As String = ".DOCX"

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 10, ddl_quincenas.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ORDEN_DESCUENTO_NOMBRE_OFICIO"
        Session("rs") = Session("cmd").Execute()
        NombreDocumento = Session("rs").Fields("NOMBRE_ARCHIVO").Value.ToString
        Session("Con").Close()

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "\DocPlantillas\OrdenDescuentos\" + PlantillaDocumento)
            ReemplazarEtiquetas(worddoc)
            worddoc.SaveAs(Session("APPATH") + "\Word\" + NombreDocumento + ExtensionDocumento)
        End Using

        Dim result As String = ""

        Try

            Dim winKey As String = ConfigurationManager.AppSettings.[Get]("WINKEY")
            Dim wordToPdfConverter As New WordToPdfConverter()
            wordToPdfConverter.LicenseKey = winKey
            wordToPdfConverter.ConvertWordFileToFile(Session("APPATH") + "\Word\" + NombreDocumento + ExtensionDocumento, Session("APPATH") + "\Word\" + NombreDocumento + ".pdf")

            'Elimina el Documento WORD ya Prellenado
            File.Delete(Session("APPATH") + "\Word\" + NombreDocumento + ExtensionDocumento)

            'Se genera el PDF
            Dim Filename As String = NombreDocumento + ".pdf"
            Dim FilePath As String = Session("APPATH") + "\Word\"
            Dim fs As FileStream
            fs = File.Open(FilePath + Filename, FileMode.Open)
            Dim bytBytes(fs.Length) As Byte
            fs.Read(bytBytes, 0, fs.Length)
            fs.Close()

            'Borra el archivo creado en memoria
            DelHDFile(FilePath + Filename)
            Response.Buffer = True
            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", NombreDocumento + ".pdf"))
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(bytBytes)
            Response.End()

        Catch ex As Exception
            result = (ex.Message)
        Finally

        End Try

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New FileInfo(File)
        If (fi.Attributes And FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor FileAttributes.ReadOnly
        End If

        IO.File.Delete(File)

    End Sub

    Private Sub ReemplazarEtiquetas(ByRef doc As Novacode.DocX)

        Dim fecha_oficio As String = String.Empty
        Dim no_prestamos_pa As Integer = 0
        Dim monto_pa As String = String.Empty
        Dim periodo As String = String.Empty

        'SE OBTIENEN LOS DATOS PARA EL OFICIO DE DESCUENTOS
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ORDEN_DESCUENTO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 10, ddl_quincenas.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            fecha_oficio = Session("rs").Fields("FECHA_OFICIO").Value.ToString()
            no_prestamos_pa = Session("rs").Fields("NO_PRESTAMOS_PA").Value.ToString()
            monto_pa = Session("rs").Fields("MONTO_PA").Value.ToString()
            periodo = Session("rs").Fields("PERIODO").Value.ToString()
        End If
        Session("Con").Close()

        doc.ReplaceText("[FECHA_OFICIO]", fecha_oficio, False, RegexOptions.None)
        doc.ReplaceText("[NO_PRESTAMOS_PA]", no_prestamos_pa, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_PA]", monto_pa, False, RegexOptions.None)
        doc.ReplaceText("[PERIODO]", periodo, False, RegexOptions.None)

    End Sub

End Class