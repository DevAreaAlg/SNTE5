Imports Microsoft.VisualBasic

Public Class Reportes_Param_Ctrl
    Inherits System.Web.UI.WebControls.Panel
    Public Property Cntrl As System.Web.UI.Control
    Public Property Cntrl_type As String
    Public Property Clave As String
    Public Property Dependientes As New List(Of Reportes_Param_Ctrl)
    Public Event CambioValor As EventHandler
    Public Property TieneDep As Boolean

    Public Sub New(ByVal control As String, ByVal query As String, ByVal opcional As Boolean, ByVal nombre As String, ByVal label As String, ByVal regex As String, ByVal regex_label As String, ByVal valGroup As String, ByVal validate As Boolean, ByVal tDeps As Boolean)

        TieneDep = tDeps
        Cntrl_type = control
        'lbl_estatus.Text = nombre

        Select Case control
            Case "DROPDOWN"
                Dim pnllabels As New Panel
                Dim paramCntrl As New DropDownList
                Dim paramLabel As New Label
                Me.CssClass = "vertical"
                pnllabels.CssClass = "div_labels"
                'llenado dropdown
                If Not String.IsNullOrEmpty(query) Then
                    paramCntrl.DataSource = execParamQuery(query)
                    paramCntrl.DataTextField = "TEXT"
                    paramCntrl.DataValueField = "VALUE"
                    paramCntrl.DataBind()
                End If
                'caracteristicas dropdown
                paramCntrl.Items.Add(New ListItem("ELIJA", -1))
                paramCntrl.SelectedValue = -1
                paramCntrl.AutoPostBack = tDeps
                paramCntrl.ID = nombre
                paramCntrl.CssClass = "btn btn-primary2"
                AddHandler paramCntrl.SelectedIndexChanged, AddressOf detectaCambioValor
                Cntrl = paramCntrl
                paramLabel.Text = label
                Clave = nombre
                pnllabels.Controls.Add(paramLabel)
                If Not opcional Then
                    Dim requireFieldVal As New RequiredFieldValidator
                    requireFieldVal.ControlToValidate = paramCntrl.ID
                    requireFieldVal.InitialValue = -1
                    requireFieldVal.Display = 2
                    requireFieldVal.CssClass = "alertaValidator bold"
                    requireFieldVal.ErrorMessage = "Falta Dato"
                    requireFieldVal.ValidationGroup = valGroup
                    requireFieldVal.Enabled = validate
                    requireFieldVal.Visible = validate
                    pnllabels.Controls.Add(requireFieldVal)
                End If
                If Not regex.Equals("") Then
                    Dim regexVal As New RegularExpressionValidator
                    regexVal.ControlToValidate = paramCntrl.ID
                    regexVal.ValidationExpression = regex
                    regexVal.Display = 2
                    regexVal.ValidationGroup = valGroup
                    regexVal.Enabled = validate
                    regexVal.Visible = validate
                    regexVal.CssClass = "alertaValidator bold"
                    regexVal.ErrorMessage = regex_label
                    pnllabels.Controls.Add(regexVal)
                End If
                Me.Controls.Add(pnllabels)
                Me.Controls.Add(paramCntrl)
            Case "TEXTBOX"

                Dim pnllabels As New Panel
                Dim paramCntrl As New TextBox
                Dim paramLabel As New Label
                Me.CssClass = "text_input_nice_div module_subsec_elements"
                pnllabels.CssClass = "text_input_nice_labels"
                paramCntrl.AutoPostBack = tDeps
                paramCntrl.CssClass = "btn btn-primary2"
                paramCntrl.CssClass = "text_input_nice_input"
                paramCntrl.ID = nombre
                AddHandler paramCntrl.TextChanged, AddressOf detectaCambioValor
                Cntrl = paramCntrl
                paramLabel.Text = label
                paramLabel.CssClass = "text_input_nice_label"
                Clave = nombre
                pnllabels.Controls.Add(paramLabel)
                If Not opcional Then
                    Dim requireFieldVal As New RequiredFieldValidator
                    requireFieldVal.ControlToValidate = paramCntrl.ID
                    requireFieldVal.Display = 2
                    requireFieldVal.ValidationGroup = valGroup
                    requireFieldVal.Enabled = validate
                    requireFieldVal.Visible = validate
                    requireFieldVal.CssClass = "alertaValidator bold"
                    requireFieldVal.ErrorMessage = "Falta Dato"
                    pnllabels.Controls.Add(requireFieldVal)
                End If
                If Not regex.Equals("") Then
                    Dim regexVal As New RegularExpressionValidator
                    regexVal.ControlToValidate = paramCntrl.ID
                    regexVal.ValidationExpression = regex
                    regexVal.Display = 2
                    regexVal.Enabled = validate
                    regexVal.Visible = validate
                    regexVal.ValidationGroup = valGroup
                    regexVal.CssClass = "alertaValidator bold"
                    regexVal.ErrorMessage = regex_label
                    pnllabels.Controls.Add(regexVal)
                End If
                Me.Controls.Add(paramCntrl)
                Me.Controls.Add(pnllabels)

            Case "DATE"
                ' Dim filtro As New AjaxControlToolkit.FilteredTextBoxExtender
                Dim pnllabels As New Panel
                Dim paramCntrl As New TextBox
                Dim paramLabel As New Label


                Me.CssClass = "text_input_nice_div module_subsec_elements"
                pnllabels.CssClass = "text_input_nice_labels"
                paramCntrl.AutoPostBack = tDeps
                paramCntrl.CssClass = "btn btn-primary2"
                paramCntrl.CssClass = "text_input_nice_input"
                paramCntrl.ID = nombre
                AddHandler paramCntrl.TextChanged, AddressOf detectaCambioValor
                Cntrl = paramCntrl
                paramLabel.Text = label
                paramLabel.CssClass = "text_input_nice_label"
                Clave = nombre



                pnllabels.Controls.Add(paramLabel)
                If Not opcional Then
                    Dim requireFieldVal As New RequiredFieldValidator
                    requireFieldVal.ControlToValidate = paramCntrl.ID
                    requireFieldVal.Display = 2
                    requireFieldVal.ValidationGroup = valGroup
                    requireFieldVal.Enabled = validate
                    requireFieldVal.Visible = validate
                    requireFieldVal.CssClass = "alertaValidator bold"
                    requireFieldVal.ErrorMessage = "Falta Dato"
                    pnllabels.Controls.Add(requireFieldVal)
                End If
                If Not regex.Equals("") Then
                    Dim regexVal As New RegularExpressionValidator
                    regexVal.ControlToValidate = paramCntrl.ID
                    regexVal.ValidationExpression = ""
                    regexVal.Display = 2
                    regexVal.Enabled = validate
                    regexVal.Visible = validate
                    regexVal.ValidationGroup = valGroup
                    regexVal.CssClass = "alertaValidator bold"
                    regexVal.ErrorMessage = regex_label
                    pnllabels.Controls.Add(regexVal)
                End If



                Me.Controls.Add(paramCntrl)
                ' Me.Controls.Add(filtro)
                Me.Controls.Add(pnllabels)

                'filtro.TargetControlID = paramCntrl.ID
                'filtro.FilterType = AjaxControlToolkit.FilterTypes.Custom
                'filtro.ValidChars = "/0123456789"
                'pnllabels.Controls.Add(filtro)

                'Dim ajaxCalen As New AjaxControlToolkit.CalendarExtender

                'ajaxCalen.ID = "CalendarExtender" + nombre
                ''ajaxCalen.ClientIDMode = "server"
                'ajaxCalen.Format = "dd/MM/yyyy"
                'ajaxCalen.TargetControlID = nombre
                ''ajaxCalen.ViewStateMode = ViewStateMode.Enabled
                ''ajaxCalen.d



                'Dim ajaxCalen1 As New AjaxControlToolkit.MaskedEditExtender
                ''ajaxCalen.
                'ajaxCalen1.ID = "MaskedEditExtender" + nombre
                '' ajaxCalen1.ClientIDMode = "server"
                'ajaxCalen1.Mask = "99/99/9999"
                'ajaxCalen1.MaskType = "Date"
                'ajaxCalen1.TargetControlID = nombre
            Case Else
                Throw New System.Exception("El sistema no reconoce el control de tipo:" & control)
        End Select


        generarDependientes(True)


    End Sub

#Region "generador de dependientes basado en el valor de su padre"

    Public Sub generarDependientes(ByVal enable As Boolean)
        If TieneDep Then
            'borramos los  parametros dependientes previamente almacenados
            Dependientes.Clear()
            'DECLARAMOS VARIABLES PARA LA OBTENCIÓN DE LOS PARAMETROS DEPENDIENTES
            Dim Con = CreateObject("ADODB.Connection")
            Dim cmd = New ADODB.Command()
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim dt_paramDep As New Data.DataTable
            'establezemos las características para la conexión exitosa con el servidor
            Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Con.ConnectionTimeout = 240
            'se abre la conexión
            Con.Open()
            Try
                cmd.ActiveConnection = Con
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                Dim param = cmd.CreateParameter("CLAVE_PARAM", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 150, Clave)
                cmd.Parameters.Append(param)
                cmd.CommandText = "SEL_REPORTES_PARAM_DEP"
                Dim rs As ADODB.Recordset = cmd.Execute()
                custDA.Fill(dt_paramDep, rs)
                For Each row As Data.DataRow In dt_paramDep.Rows

                    Dim cregex As String
                    If IsDBNull(row("REGEX")) Then
                        cregex = ""
                    Else
                        cregex = row("REGEX")
                    End If

                    Dim regex_lbl As String
                    If IsDBNull(row("REGEX_LABEL")) Then
                        regex_lbl = ""
                    Else
                        regex_lbl = row("REGEX_LABEL")
                    End If
                    Dim cquery As String
                    If IsDBNull(row("QUERY")) Then
                        cquery = ""
                    Else
                        cquery = TryCast(row("QUERY"), String).Replace(Clave, obtenerValor())
                    End If
                    Dim cntrlParam As New Reportes_Param_Ctrl(row("CNTRL"), cquery, CBool(row("OPCIONAL")), row("NOMBRE"), row("LABEL"), cregex, regex_lbl, "generar_reporte_param", CBool(row("DEPENDIENTES")), enable)
                    cntrlParam.CssClass = cntrlParam.CssClass & " module_subsec_elements"
                    cntrlParam.Enabled = enable
                    Dependientes.Add(cntrlParam)

                Next
            Catch ex As Exception
                Throw New System.Exception("Error al recuperar los datos de los parametros dependientes del parametro con clave: " & Clave & " ---- " & ex.ToString)
            Finally
                Con.Close()
            End Try
        End If
    End Sub

#End Region

#Region "sub que pinta los dependientes en el panel que contendra el control"

    Public Sub pintaDep(ByRef panelP As Panel)
        For Each paramctrl As Reportes_Param_Ctrl In Dependientes
            panelP.Controls.Add(paramctrl)
            paramctrl.pintaDep(panelP)
        Next
    End Sub

#End Region

#Region "funcion que obtiene el valor del control en cuestion"

    Public Function obtenerValor() As String
        Dim lval As String = ""
        Select Case Cntrl_type
            Case "DROPDOWN"
                lval = TryCast(Cntrl, DropDownList).SelectedItem.Value
            Case "TEXTBOX"
                lval = TryCast(Cntrl, TextBox).Text
        End Select
        Return lval
    End Function

#End Region

#Region "evento de cambio de valor del control"

    Protected Sub detectaCambioValor(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent CambioValor(sender, e)
    End Sub

#End Region

#Region "PARAM QUERY EXEC"

    Private Function execParamQuery(ByVal query As String) As Data.DataTable
        'declaramos el Data.DataTable a regresar
        Dim finalData As New Data.DataTable
        'declaramos las variabes necesarias para la conexión con la base de datos y la captura de los datos generados.
        Dim Con = CreateObject("ADODB.Connection")
        Dim cmd = New ADODB.Command()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        'establezemos las características para la conexión exitosa con el servidor
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        'se abre la conexión
        Con.Open()
        Try

            cmd.ActiveConnection = Con
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            Dim param = cmd.CreateParameter("QUERY", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 8000, query)
            cmd.Parameters.Append(param)
            cmd.CommandText = "SEL_REPORTES_EXEC_PARAM"
            Dim rs As ADODB.Recordset = cmd.Execute()
            custDA.Fill(finalData, rs)

        Catch ex As Exception
            Throw New System.Exception("Error al ejecutar el query de llenado del parametro con clave: " & Clave & " ---- " & ex.ToString)
        Finally

            Con.Close()
        End Try
        Return finalData
    End Function

#End Region

End Class
