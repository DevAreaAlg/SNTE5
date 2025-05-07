Public Class PEN_PROC_LAYOUT
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Carga", "Carga de Archivos")

        If Not Me.IsPostBack Then
            lbl_Status_Carga.Text = ""
            dag_Persona_Ex.DataSource = Nothing

            CargaInstituciones()
            btn_EXCEL.Visible = False
            btn_buscar.Visible = False
        End If

    End Sub

    Private Sub CargaInstituciones()

        ddl_institucion.Items.Clear()
        ddl_institucion.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PROC_INSTITUCIONES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_institucion.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Private Sub CargaProcesos()

        ddl_proceso.Items.Clear()
        ddl_proceso.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INSTITUCION", Session("adVarChar"), Session("adParamInput"), 11, ddl_institucion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROCESO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_proceso.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()


    End Sub


    Protected Sub ddl_institucion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_institucion.SelectedIndexChanged

        If ddl_institucion.SelectedItem.Value = -1 Then
            ddl_proceso.Items.Clear()
            ddl_proceso.Enabled = False
        Else
            ddl_proceso.Enabled = True
            CargaProcesos()
        End If

        ddl_layout.Items.Clear()
        ddl_layout.Items.Add(New ListItem("ELIJA", "-1"))

        ddl_consulta.Items.Clear()
        ddl_consulta.Items.Add(New ListItem("ELIJA", "-1"))

        dag_Persona_Ex.Visible = False
        dag_Persona_Ex.DataSource = Nothing
        dag_Persona_Ex.DataBind()

        dag_lotes.Visible = False
        dag_lotes.DataSource = Nothing
        dag_lotes.DataBind()
        btn_buscar.Visible = False
        btn_EXCEL.Visible = False

    End Sub

    Protected Sub ddl_proceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_proceso.SelectedIndexChanged

        If ddl_proceso.SelectedItem.Value = -1 Then
            ddl_layout.Items.Clear()
            ddl_layout.Enabled = False
            ddl_consulta.Items.Clear()
            ddl_consulta.Enabled = False
        Else
            ddl_layout.Enabled = True
            CargaLayout()
            ddl_consulta.Enabled = True
            CargaLotes()
        End If

        dag_Persona_Ex.Visible = False
        dag_Persona_Ex.DataSource = Nothing
        dag_Persona_Ex.DataBind()

        dag_lotes.Visible = False
        dag_lotes.DataSource = Nothing
        dag_lotes.DataBind()
        btn_buscar.Visible = False
        btn_EXCEL.Visible = False

    End Sub

    Private Sub CargaLayout()

        ddl_layout.Items.Clear()
        ddl_layout.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PROCESO", Session("adVarChar"), Session("adParamInput"), 11, ddl_proceso.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_LAYOUT"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_layout.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub ddl_layout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_layout.SelectedIndexChanged

        dag_Persona_Ex.Visible = False
        dag_Persona_Ex.DataSource = Nothing
        dag_Persona_Ex.DataBind()

    End Sub

    Private Sub valida_archivo()

        If (AsyncFileUpload1.HasFile) Then
            ' Get the name of the file to upload.
            ' Dim fileName As String = Server.HtmlEncode(AsyncFileUpload1.FileName)
            Dim fileName As String = Server.MapPath("/tmp/" + AsyncFileUpload1.FileName.ToString)
            Dim name As String = AsyncFileUpload1.FileName.ToString
            ' Get the extension of the uploaded file.
            Dim extension As String = System.IO.Path.GetExtension(fileName)

            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim PersonaGeneral As New Data.DataTable()
            PersonaGeneral.Columns.Add("IDRESPUESTA", GetType(Integer))
            PersonaGeneral.Columns.Add("RESPUESTA", GetType(String))
            PersonaGeneral.Columns.Add("OBLIGATORIO", GetType(Integer))
            PersonaGeneral.Columns.Add("ORDEN", GetType(String))
            PersonaGeneral.Columns.Add("NOMBRE", GetType(String))
            PersonaGeneral.Columns.Add("EXPRESION_REGULAR", GetType(String))

            Dim dtPersonasEx As New Data.DataTable()
            dtPersonasEx.Columns.Add("TIPO", GetType(String))
            dtPersonasEx.Columns.Add("EVENTO", GetType(String))
            dtPersonasEx.Columns.Add("TRABAJADOR", GetType(String))
            dtPersonasEx.Columns.Add("CUENTA", GetType(String))
            dtPersonasEx.Columns.Add("DESCRIPCION", GetType(String))
            dtPersonasEx.Columns.Add("FECHACREADO", GetType(String))

            'Dim dtPersonasNoEx As New Data.DataTable()
            'dtPersonasNoEx.Columns.Add("IDRESPUESTA", GetType(Integer))
            'dtPersonasNoEx.Columns.Add("RESPUESTA", GetType(String))
            AsyncFileUpload1.SaveAs(fileName)

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDLAYOUT", Session("adVarChar"), Session("adParamInput"), 11, ddl_layout.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE_ARCHIVO", Session("adVarChar"), Session("adParamInput"), 500, name)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EXTENSION", Session("adVarChar"), Session("adParamInput"), 10, extension)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RUTA_ARCHIVO", Session("adVarChar"), Session("adParamInput"), 1000, fileName)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_INSTITUCION", Session("adVarChar"), Session("adParamInput"), 11, ddl_institucion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_PROCESO", Session("adVarChar"), Session("adParamInput"), 11, ddl_proceso.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CTAS_PROCESO_CARGA"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtPersonasEx, Session("rs"))
            Session("Con").Close()



            Dim contador As Integer = 0

            'For Each row As Data.DataRow In PersonaGeneral.Rows()
            '    If PersonaGeneral.Rows(contador).Item("TIPO") = 1 Then
            '        lbl_Status_Carga.Text = ""
            '        lbl_Status_Carga.Text = fileName
            '    Else
            '        lbl_Status_Carga.Text = PersonaGeneral.Rows(contador).Item("RESPUESTA")
            '        lbl_Status_Carga.Text = fileName
            '        Exit Sub
            '    End If
            '    contador += 1
            'Next

            If dtPersonasEx.Rows.Count > 0 Then
                dag_Persona_Ex.Visible = True
                dag_Persona_Ex.DataSource = dtPersonasEx
                dag_Persona_Ex.DataBind()
            Else
                dag_Persona_Ex.Visible = False
            End If

            'If dtPersonasNoEx.Rows.Count > 0 Then
            '    Dag_Persona_NoEx.Visible = True
            '    Dag_Persona_NoEx.DataSource = dtPersonasNoEx
            '    Dag_Persona_NoEx.DataBind()

            'Else
            '    Dag_Persona_NoEx.Visible = False
            'End If
            '' DELETE THE FILE
            System.IO.File.Delete(fileName)


        Else

            ' NOTIFY THE USER THET A FILE WAS NOT UPLOADED
            lbl_Status_Carga.Text = "Error: Seleccione un archivo."

            dag_Persona_Ex.Visible = False
            Dag_Persona_NoEx.Visible = False
            AsyncFileUpload1.Enabled = True
            btn_CargarArch.Enabled = True
        End If

        CargaLotes()

    End Sub

    Protected Sub btn_CargarArch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CargarArch.Click

        valida_archivo()
        dag_lotes.Visible = False
        btn_EXCEL.Visible = False

    End Sub

    Private Sub CargaLotes()

        ddl_consulta.Items.Clear()
        ddl_consulta.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PROCESO", Session("adVarChar"), Session("adParamInput"), 11, ddl_proceso.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINSTI", Session("adVarChar"), Session("adParamInput"), 11, ddl_institucion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_LOTES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_consulta.Items.Add(New ListItem(Session("rs").Fields("LOTE").Value, Session("rs").Fields("IDLOTE").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub ddl_consulta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_consulta.SelectedIndexChanged
        If ddl_consulta.SelectedItem.Value = -1 Then
            btn_buscar.Visible = False
            dag_lotes.DataSource = Nothing
            dag_lotes.DataBind()
            btn_EXCEL.Visible = False
        Else
            btn_buscar.Visible = True
            btn_buscar.Enabled = True
        End If

    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("LOTE", Session("adVarChar"), Session("adParamInput"), 11, ddl_consulta.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_LOG_LOTE"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_lotes.Visible = True
            dag_lotes.DataSource = dteq
            dag_lotes.DataBind()
            btn_EXCEL.Visible = True
        Else
            dag_lotes.Visible = False
        End If
    End Sub

    Protected Sub btn_EXCEL_Click() Handles btn_EXCEL.Click

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtDivisas As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("LOTE", Session("adVarChar"), Session("adParamInput"), 11, ddl_consulta.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROC_LOG_LOTE"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtDivisas, Session("rs"))

        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtDivisas.Columns.Count - 1
            context.Response.Write(dtDivisas.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtDivisas.Rows

            For i = 0 To dtDivisas.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Lote" + ddl_consulta.SelectedItem.Value + ".csv")
        context.Response.End()

    End Sub

End Class