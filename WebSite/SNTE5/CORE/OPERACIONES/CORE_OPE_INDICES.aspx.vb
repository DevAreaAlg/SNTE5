Imports System.Data
Imports System.Data.SqlClient
Public Class CORE_OPE_INDICES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración de Índices", "Índices Financieros")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Llenaindices()
            Facultad()
            FechasIndices()

            CargaAnios()
            CargaIPCs()

            Llenadiario()
            Llenahistorico(0)
            Session("FLAG") = 0
            lnk_layout.Attributes.Add("onclick", "window.open('/DocPlantillas/Manuales/" + "FORMATO_LAYOUT_INDICES.pdf" + "');")
        End If
    End Sub

    'Llena los indices activos del catindifinan
    Private Sub Llenaindices()

        cmb_indice.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_indice.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDINDICES", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_INDICES" 'Reutilizando el SP de Ruben
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("CATINDIFINAN_NOMBRE").Value.ToString, Session("rs").Fields("CATINDIFINAN_ID_INDICE").Value.ToString)
            cmb_indice.Items.Add(item)
            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

    Private Sub Facultad()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FACULTAD_INDICE_FINANCIERO"
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("rs") = Session("cmd").Execute()
        Session("FACULTAD") = Session("rs").Fields("FACULTAD").value.ToString
        Session("Con").Close()
    End Sub


    Private Sub FechasIndices()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FECHAS_INDICE_FINANCIERO"
        Session("rs") = Session("cmd").Execute()
        lbl_fecha_udi.Text = Session("rs").Fields("FUDI").value.ToString
        lbl_fecha_tiee.Text = Session("rs").Fields("FTIEE").value.ToString
        lbl_fecha_cetes.Text = Session("rs").Fields("FCETES").value.ToString

        Session("Con").Close()


    End Sub

    Protected Sub cmb_indice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_indice.SelectedIndexChanged
        LimpiaForma() 'Limpio los txt cuando se elije cambiar de indice dentro del combo

    End Sub
    'Limpio los lbls cuando cambia el tipo de indice en el combo
    Private Sub LimpiaForma()
        txt_repetir.Text = ""
        txt_valor.Text = ""
        btn_guardar.Enabled = True
        lbl_status.Text = ""
    End Sub

    Private Function Validaindice(ByVal valor As String) As Boolean

        Return Regex.IsMatch(valor, ("^[0-9]{1,2}(\.[0-9]{6})?$"))

    End Function

    'Botón Guardar(Guarda el indice financiero en la tabla MSTINDIFINAN)
    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        lbl_status.Text = ""
        If txt_valor.Text = txt_repetir.Text Then 'Primera validación... Que concuerden los valores 
            If Validaindice(txt_valor.Text) = True Then

                asignaindice()
                Llenadiario()
                Llenahistorico(0)
                limpiacontroles()
            Else
                lbl_status.Text = "Error: El valor debe de tener 6 decimales"
                Exit Sub
            End If

        Else
            lbl_status.Text = "Error: No coinciden los valores del índice"
        End If
    End Sub


    Private Sub limpiacontroles()
        txt_repetir.Text = ""
        txt_valor.Text = ""
        cmb_indice.SelectedIndex = "0"

    End Sub

    Private Sub asignaindice()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INDICE", Session("adVarChar"), Session("adParamInput"), 10, cmb_indice.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 10, txt_valor.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHASIS", Session("adVarChar"), Session("adParamInput"), 20, Session("FechaSis").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FACULTADC", Session("adVarChar"), Session("adParamInput"), 15, Session("FACULTAD").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "INS_INDICE_FINANCIERO"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("RESPUESTA").value.ToString = "EXISTE" Then  'Segunda Validación.... Que no exista el valor con esa fecha de sistema en la BD
            lbl_status.Text = "Error: El valor para esta fecha de sistema ya ha sido capturado"
            btn_guardar.Enabled = False
        Else
            ' no tiene la facultad y solamente guarda el valor correctamente y deshabilito el botón
            lbl_status.Text = "Guardado correctamente"
            btn_guardar.Enabled = False
            'Si tiene la facultad el mensaje de estatus cambia.. y habilito el botón
            If Session("FACULTAD").ToString = "1" Then
                lbl_status.Text = "Guardado correctamente"
                btn_guardar.Enabled = True
            End If

        End If

        Session("Con").Close()


    End Sub


    Protected Sub btn_cargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cargar.Click

        If AsyncFileUpload1.PostedFile.FileName = "" Then
            lbl_status_indices.Text = "Error: Selecciona un archivo."
            Exit Sub
        End If
        ''lbl_status_indices.Text = AsyncFileUpload1.FileName.ToString
        'EVALUAR QUE SEA CSV
        If Right(System.IO.Path.GetFileName(AsyncFileUpload1.PostedFile.FileName), 3).ToUpper <> "CSV" Then
            lbl_status_indices.Text = "Error: Debe subir el ""layout"" en formato .csv ."
            Exit Sub
        End If

        Dim Resultado As String = "OK"

        Dim dtArchivo As New DataTable
        Dim Variable As String = ""
        Dim contadorFila As Integer = 0
        dtArchivo.Columns.Add("LINEA", GetType(String))
        Dim linea As String = ""
        Using fs As System.IO.Stream = AsyncFileUpload1.PostedFile.InputStream
            Using oRead As New System.IO.StreamReader(fs, System.Text.Encoding.Default)
                Do While Not oRead.EndOfStream
                    linea = oRead.ReadLine()
                    dtArchivo.Rows.Add(linea)

                    Dim arrVar As String() = Split(linea, ",")
                    contadorFila = contadorFila + 1
                    If Resultado = "OK" Then

                        'Variable = Variable + " *** " + oRead.ReadLine()
                        If arrVar(0) = "" Or arrVar(0) = Nothing Or ValidaIdINI(arrVar(0)) = False Then
                            Resultado = "Error: Id de índice incorrecto, en la fila: " + CStr(contadorFila)
                        ElseIf arrVar(1) = "" Or arrVar(1) = Nothing Or ValidaValorIndi(arrVar(1)) = False Then
                            Resultado = "Error: Valor de índice incorrecto, en la fila: " + CStr(contadorFila)
                        ElseIf arrVar(2) = "" Or arrVar(2) = Nothing Or ValidaFecha(arrVar(2)) = False Then
                            Resultado = "Error: Valor de fecha incorrecto, en la fila: " + CStr(contadorFila)
                        End If
                    End If


                Loop

            End Using
            lbl_status_indices.Text = Resultado
            If Resultado = "OK" Then
                fs.Position = 0


                Dim strConnString As String 'INSERTO BINARIO EN BD
                strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString

                Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)

                Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("INS_INDICES_FINAN", sqlConnection)
                'Stored Procedure 
                sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

                'Parametros para la inserción del Stored Procedure
                sqlCmdObj.Parameters.AddWithValue("@DTARCHIVO", dtArchivo)
                sqlCmdObj.Parameters.AddWithValue("@IDUSER", Session("USERID"))
                'sqlCmdObj.Parameters.AddWithValue("@FECHA", DirectCast(Master.FindControl("lbl_fecha"), Label).Text)
                sqlCmdObj.Parameters.AddWithValue("@SESION", Session("SESION"))
                ' btn_curves.Text = fs.Length.ToString()

                sqlConnection.Open()

                Dim myReader As SqlDataReader = sqlCmdObj.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()
                    Resultado = myReader.GetString(0)
                End While
                myReader.Close()

                sqlConnection.Close()


                If Resultado = "OK" Then
                    lbl_status_indices.Text = "El archivo: " + AsyncFileUpload1.FileName.ToString + " se procesó correctamente."
                Else
                    lbl_status_indices.Text = Resultado
                End If
            Else
                lbl_status_indices.Text = Resultado
            End If

            Llenahistorico(0)
            FechasIndices()

        End Using

        '' that the FileUpload control contains a file.
        'If (AsyncFileUpload1.HasFile) Then


        '    ' Get the name of the file to upload.
        '    ' Dim fileName As String = Server.HtmlEncode(AsyncFileUpload1.FileName)
        '    'Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload1.FileName.ToString)
        '    Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload1.FileName.ToString)
        '    ' Get the extension of the uploaded file.

        '    lbl_status_indices.Text = AsyncFileUpload1.FileName.ToString

        '    Dim extension As String = System.IO.Path.GetExtension(fileName)
        '    ' Allow only files with .txt or .cvs extensions
        '    ' to be uploaded.
        '    If (extension = ".txt") Or (extension = ".csv") Then

        '        ' Call the SaveAs method to save                
        '        AsyncFileUpload1.SaveAs(fileName)

        '        Session("cmd") = New ADODB.Command()
        '        Session("Con").Open()
        '        Session("cmd").ActiveConnection = Session("Con")
        '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        '        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("FILENAME", Session("adVarChar"), Session("adParamInput"), 1000, fileName)
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("EXTENSION", Session("adVarChar"), Session("adParamInput"), 15, extension)
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        '        Session("cmd").Parameters.Append(Session("parm"))
        '        Session("cmd").CommandText = "INS_INDICE_FINANCIERO_ARCHIVO"
        '        Session("rs") = Session("cmd").Execute()

        '        Select Case Session("rs").fields("ERROR").value.ToString

        '            Case "OK"
        '                lbl_status_indices.Text = "Guardado correctamente"
        '            Case "ERROR"
        '                lbl_status_indices.Text = "Error: Layout incorrecto"
        '            Case "FALLA"
        '                lbl_status_indices.Text = "Error: Layout incorrecto, verifique los datos"
        '        End Select


        '        Session("Con").Close()

        '        'delete the fiel
        '        FechasIndices()
        '        Llenahistorico(0)
        '        'System.IO.File.Delete(fileName)


        '    Else
        '        ' Notify the user why their file was not uploaded.
        '        lbl_status_indices.Text = "Error: Sólo puede subir archivos con la siguientes extensiones:(*.txt/*.csv )"
        '    End If

        'Else
        '    ' Notify the user that a file was not uploaded.
        '    lbl_status_indices.Text = "Error:Seleccione un archivo."
        'End If
    End Sub

    Private Function ValidaIdINI(ByVal fecha As String) As Boolean

        Return Regex.IsMatch(fecha, ("^\d+$"))

    End Function


    Private Function ValidaValorIndi(ByVal fecha As String) As Boolean

        Return Regex.IsMatch(fecha, ("^\d+(\.\d{1,7})?$"))

    End Function

    Private Function ValidaFecha(ByVal fecha As String) As Boolean

        Return Regex.IsMatch(fecha, ("^\d{4}-((0[1-9])|(1[012]))-((0[1-9]|[12]\d)|3[01])$"))

    End Function



    Private Sub Llenadiario()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtindice As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DIARIO_VALOR_INDICES"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtindice, Session("rs"))
        'se vacian los expedientes al formulario
        dag_indice.DataSource = dtindice
        dag_indice.DataBind()

        Session("Con").Close()

    End Sub


    Private Sub Llenahistorico(ByVal Filtro As Integer)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtindiceValor As New Data.DataTable()


        If Filtro = 1 Then
            dag_historial.CurrentPageIndex = 0
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHAFILTRO", Session("adVarChar"), Session("adParamInput"), 20, txt_FechaFiltro.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_HISTORIAL_VALOR_INDICES"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtindiceValor, Session("rs"))
        'se vacian los expedientes al formulario
        dag_historial.DataSource = dtindiceValor
        dag_historial.DataBind()
        Session("Con").Close()

    End Sub
    Protected Sub lnk_CancelaFechaFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_CancelaFechaFiltro.Click
        txt_FechaFiltro.Text = ""
        Llenadiario()
        Llenahistorico(1)
    End Sub

    Protected Sub lnk_FechaFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_FechaFiltro.Click
        Llenadiario()
        Llenahistorico(1)
    End Sub


    Protected Sub dag_historial_pageIndexChanged(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs) Handles dag_historial.PageIndexChanged

        dag_historial.CurrentPageIndex = e.NewPageIndex
        Llenahistorico(0)

    End Sub
    Protected Sub btn_EXCEL_Click() Handles btn_EXCEL.Click

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtINDICES As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHAFILTRO", Session("adVarChar"), Session("adParamInput"), 20, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_HISTORIAL_VALOR_INDICES"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtINDICES, Session("rs"))

        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtINDICES.Columns.Count - 1
            context.Response.Write(dtINDICES.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtINDICES.Rows

            For i = 0 To dtINDICES.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "INDICES FINANCIEROS" + ".csv")
        context.Response.End()

    End Sub

#Region "IPC"

    Private Sub CargaAnios()

        ddl_anios.Items.Clear()
        Dim elija As New ListItem("ELIJA", "")
        ddl_anios.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_IPC_ANIOS"
        Session("rs") = Session("cmd").Execute()

        Dim contador As Integer = 1

        Do While Not Session("rs").EOF

            If contador = 1 Then

                Dim anioSis As Integer = Year(Convert.ToDateTime(Session("FechaSis").ToString))
                Dim proxAnioSis As Integer = anioSis + 1

                If Session("rs").Fields("ANIO").Value.ToString <> proxAnioSis Then

                    Dim lst_proxAnioSis As New ListItem(proxAnioSis.ToString, proxAnioSis.ToString)
                    ddl_anios.Items.Add(lst_proxAnioSis)

                End If

            End If

            contador += 1

            Dim item As New ListItem(Session("rs").Fields("ANIO").Value.ToString, Session("rs").Fields("ANIO").Value.ToString)
            ddl_anios.Items.Add(item)
            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

    Private Sub CargaIPCs()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtIndicesIPC As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_IPC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtIndicesIPC, Session("rs"))
        Session("Con").Close()

        dag_bitacora_ipc.DataSource = dtIndicesIPC
        dag_bitacora_ipc.DataBind()

        dtIndicesIPC.Clear()

    End Sub

    Protected Sub btn_guardar_ipc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar_ipc.Click

        If txt_ipc.Text <> txt_confirmar.Text Then
            lbl_estatus.Text = "Error: Los valores de IPC no coinciden."
            Exit Sub
        End If

        If ValidaIPC(txt_ipc.Text) Then
            pnl_modal_confirmar.Visible = True
            modal_confirmar.Show()
        Else
            lbl_estatus.Text = "Error: Factor incorrecto."
        End If

    End Sub

    Protected Sub btn_confirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_confirmar.Click

        GuardarIPC()

    End Sub

    Private Sub GuardarIPC()

        Dim Resultado As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_anios.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 50, txt_ipc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFPCR_IPC"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESULTADO").value.ToString
        Session("Con").Close()

        If Resultado = "OK" Then
            lbl_estatus.Text = "Se ha guardado con éxito el nuevo valor de IPC."
            CargaIPCs()
            Llenadiario()
            Llenahistorico(0)
        ElseIf Resultado = "ANIOEXISTE" Then
            lbl_estatus.Text = "Error: Ya se registró el valor de IPC para dicho año."
        ElseIf Resultado = "" Then
            lbl_estatus.Text = "Error: No puede registrar un valor de IPC para un año menor a la fecha del sistema."
        End If

    End Sub

    Private Function ValidaIPC(ByVal factor As String) As Boolean
        Return Regex.IsMatch(factor, ("^[0-9]{1,2}(\.[0-9]{1,6})?$"))
    End Function

#End Region

End Class