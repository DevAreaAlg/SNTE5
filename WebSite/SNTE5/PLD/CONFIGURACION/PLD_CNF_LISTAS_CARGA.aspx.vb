Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Public Class PLD_CNF_LISTAS_CARGA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
        End If
    End Sub
    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Carga de Lista PEP", "CARGA DE LISTA PEP")
        If Not Me.IsPostBack Then

        End If

    End Sub


    Protected Sub cmb_TipoLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_TipoLista.SelectedIndexChanged

        Select Case cmb_TipoLista.SelectedItem.Value
            Case "PEP"
                lbl_Permit.Text = "Nota: Sólo se permiten archivos con la extensión *.txt y las columnas deben ser divididas por una coma (,)."
            Case "BLO"
                lbl_Permit.Text = "Nota: Sólo se permiten archivos con la extensión *.xml y con su debido formato."
        End Select

        lbl_Status_Carga.Text = ""

    End Sub

    Protected Sub btn_CargarArch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CargarArch.Click
        lbl_Permit.Text = ""
        lbl_Status_Carga.Text = ""
        Select Case cmb_TipoLista.SelectedItem.Value
            Case "PEP"
                CargarArchPEP()
            Case "BLO"
                CargarArchBloqueados()
        End Select

    End Sub

    Private Sub CargarArchPEP()
        lbl_Status_Carga.Text = ""
        ' that the FileUpload control contains a file.
        If (AsyncFileUpload1.HasFile) Then
            ' Get the name of the file to upload.
            ' Dim fileName As String = Server.HtmlEncode(AsyncFileUpload1.FileName)
            Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload1.FileName.ToString)
            ' Get the extension of the uploaded file.
            Dim extension As String = System.IO.Path.GetExtension(fileName)

            ' Allow only files with .txt or .cvs extensions
            ' to be uploaded.
            If (extension = ".txt") Then

                ' Call the SaveAs method to save                
                AsyncFileUpload1.SaveAs(fileName)

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FILENAME", Session("adVarChar"), Session("adParamInput"), 1000, fileName)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("EXTENSION", Session("adVarChar"), Session("adParamInput"), 15, extension)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_PLD_CARGA_LISTAPEP"
                Session("rs") = Session("cmd").Execute()

                If Session("rs").Fields("NUEVO").Value.ToString = "-1" Then
                    lbl_Status_Carga.Text = "Error: El archivo no contiene el formato correcto."

                Else
                    If Session("rs").Fields("EXISTE").Value = "1" Then
                        lbl_Status_Carga.Text = "Error: La persona ya fue capturado en el sistema"
                    Else

                        lbl_Status_Carga.Text = "Guardado correctamente"
                    End If
                End If

                Session("Con").Close()

                ' DELETE THE FILE
                System.IO.File.Delete(fileName)
            Else
                ' NOTIFY THE USER WHY THEIR FILE WAS NOT UPLOADED
                lbl_Status_Carga.Text = "Error: Sólo puede subir archivos con la siguiente extensión:(*.txt)"
            End If
        Else
            ' NOTIFY THE USER THET A FILE WAS NOT UPLOADED
            lbl_Status_Carga.Text = "Error: Seleccione un archivo."
        End If

    End Sub

    Private Sub CargarArchBloqueados()

        Dim Bloqueados As New Data.DataTable
        Dim auxiliar As String = ""

        Bloqueados.Columns.Add("TIPOPER", GetType(String)) '0
        Bloqueados.Columns.Add("PATERNO", GetType(String)) '1
        Bloqueados.Columns.Add("MATERNO", GetType(String)) '2
        Bloqueados.Columns.Add("NOMBRES", GetType(String)) '3
        Bloqueados.Columns.Add("RFC", GetType(String)) '4

        ' that the FileUpload control contains a file.
        If (AsyncFileUpload1.HasFile) Then
            ' Get the name of the file to upload.
            ' Dim fileName As String = Server.HtmlEncode(AsyncFileUpload1.FileName)
            Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload1.FileName.ToString)
            ' Get the extension of the uploaded file.
            Dim extension As String = System.IO.Path.GetExtension(fileName)

            ' Allow only files with .XML extensions to be uploaded.
            If (extension = ".xml") Then
                Try
                    AsyncFileUpload1.SaveAs(fileName)

                    Dim reader As XmlTextReader = New XmlTextReader(fileName)
                    Dim AuxPersona As Integer = 0
                    Dim AuxTipoPer As Integer = 0
                    Dim AuxPaterno As Integer = 0
                    Dim AuxMaterno As Integer = 0
                    Dim AuxNombres As Integer = 0
                    Dim AuxRFC As Integer = 0

                    Dim TipoPer As String = ""
                    Dim Paterno As String = ""
                    Dim Materno As String = ""
                    Dim Nombres As String = ""
                    Dim RFC As String = ""

                    Do While (reader.Read())
                        Select Case reader.NodeType
                            Case XmlNodeType.Element 'Display beginning of element.
                                If reader.Name = "PersonasSolicitud" Then
                                    AuxPersona = 1
                                End If
                                If AuxPersona = 1 Then
                                    If reader.Name = "Persona" Then
                                        AuxTipoPer = 1
                                    End If
                                    If reader.Name = "Paterno" Then
                                        AuxPaterno = 1
                                    End If
                                    If reader.Name = "Materno" Then
                                        AuxMaterno = 1
                                    End If
                                    If reader.Name = "Nombre" Then
                                        AuxNombres = 1
                                    End If
                                    If reader.Name = "Rfc" Then
                                        AuxRFC = 1
                                    End If
                                End If
                            Case XmlNodeType.Text 'Display the text in each element.
                                If AuxPersona = 1 Then
                                    If AuxTipoPer = 1 Then
                                        TipoPer = reader.Value
                                    End If
                                    If AuxPaterno = 1 Then
                                        Paterno = reader.Value
                                    End If
                                    If AuxMaterno = 1 Then
                                        Materno = reader.Value
                                    End If
                                    If AuxNombres = 1 Then
                                        Nombres = reader.Value
                                    End If
                                    If AuxRFC = 1 Then
                                        RFC = reader.Value
                                    End If
                                End If
                            Case XmlNodeType.EndElement 'Display end of element.
                                If reader.Name = "PersonasSolicitud" Then
                                    AuxPersona = 0
                                    Bloqueados.Rows.Add(CStr(TipoPer), CStr(Paterno), CStr(Materno), CStr(Nombres), CStr(RFC))
                                End If
                                If reader.Name = "Persona" Then
                                    AuxTipoPer = 0
                                End If
                                If reader.Name = "Paterno" Then
                                    AuxPaterno = 0
                                End If
                                If reader.Name = "Materno" Then
                                    AuxMaterno = 0
                                End If
                                If reader.Name = "Nombre" Then
                                    AuxNombres = 0
                                End If
                                If reader.Name = "Rfc" Then
                                    AuxRFC = 0
                                End If
                        End Select
                    Loop

                    reader.Close()
                    System.IO.File.Delete(fileName)

                    lbl_Status_Carga.Text = auxiliar

                    Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                        connection.Open()
                        ' Configure the SqlCommand and SqlParameter.
                        Dim insertCommand As New SqlCommand("INS_PLD_CARGA_LISTABLOQ", connection)
                        insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                        Session("parm") = New SqlParameter("BLOQUEADOS", SqlDbType.Structured)
                        Session("parm").Value = Bloqueados
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


                    lbl_Status_Carga.Text = "Guardado correctamente"

                Catch errorVariable As Exception
                    'Error trapping
                    lbl_Status_Carga.Text = "Error al leer el archivo."
                    lbl_Status_Carga.Text = errorVariable.ToString
                End Try

            Else
                ' NOTIFY THE USER WHY THEIR FILE WAS NOT UPLOADED
                lbl_Status_Carga.Text = "Error: Sólo puede subir archivos con la siguiente extensión:(*.xml)"
            End If
        Else
            ' NOTIFY THE USER THET A FILE WAS NOT UPLOADED
            lbl_Status_Carga.Text = "Error: Seleccione un archivo."
        End If

    End Sub

    Protected Sub btn_ConsultaLista_Click(sender As Object, e As System.EventArgs) Handles btn_ConsultaLista.Click

        Select Case cmb_TipoLista.SelectedItem.Value
            Case "PEP"
                ConsultaListaPEPExcel()
            Case "BLO"
                ConsultaListaBloqueadosExcel()
        End Select

    End Sub

    Private Sub ConsultaListaPEPExcel()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPLD5 As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 500, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 500, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 500, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 1000, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CARGO", Session("adVarChar"), Session("adParamInput"), 1000, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD_ADMIN", Session("adVarChar"), Session("adParamInput"), 1000, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLD_LISTA_PEP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPLD5, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtPLD5.Columns.Count - 1

            context.Response.Write(dtPLD5.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtPLD5.Rows

            For i = 0 To dtPLD5.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "LISTAPEP" + ".csv")
        context.Response.End()

    End Sub

    Private Sub ConsultaListaBloqueadosExcel()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPLD5 As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 500, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 500, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 500, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 1000, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 1000, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLD_LISTA_BLOQ"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPLD5, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtPLD5.Columns.Count - 1

            context.Response.Write(dtPLD5.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtPLD5.Rows

            For i = 0 To dtPLD5.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "LISTABLOQUEADOS" + ".csv")
        context.Response.End()

    End Sub

    Private Sub DelHDFile(ByVal File As String)
        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If
        System.IO.File.Delete(File)
    End Sub
#Region "folder"
    'folder close or open
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub

#End Region

End Class