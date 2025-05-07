Imports System.Data.SqlClient
Imports System.Data
Imports SNTE5.Reportes_Param_Ctrl
Imports SNTE5.Categ_Ctrl
Imports System.IO

Public Class CORE_REP_GENERADOR
    'Inherits Reportes_Param_Ctrl
    Inherits System.Web.UI.Page


#Region "Page_Init"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'se verifica si es postback
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            TryCast(Me.Master, MasterMascore).CargaASPX("Generador de Reportes", "GENERADOR DE REPORTES")
            Session("sRepRows") = New List(Of Data.DataRow)()
            Session("REPORTE_PARAM_CTRLS") = New List(Of Reportes_Param_Ctrl)
            Session("userAvRep") = New Data.DataTable

            'se consultan los reportes disponibles para el usuario
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Session("Con").Open()
            Try
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ID_ROL", Session("adVarChar"), Session("adParamInput"), 20, -1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ID_REPORTE", Session("adVarChar"), Session("adParamInput"), 20, -1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ID_CATEG", Session("adVarChar"), Session("adParamInput"), 20, -1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, 1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_REPORTES_DATOS"
                Session("rs") = Session("cmd").Execute()
                custDA.Fill(Session("userAvRep"), Session("rs"))
            Finally
                Session("Con").Close()
            End Try
        End If

        'se crea el abol de categorías
        Dim arbol As New Categ_Ctrl()
        arbol.Attributes.CssStyle.Add("flex", "1")

        AddHandler arbol.SelectedCat, AddressOf CatSelected
        pnl_outArbolCat.Controls.Add(arbol)

        If Session("REPORTE_PARAM_CTRLS").Count > 0 Then
            For Each pnl As Reportes_Param_Ctrl In Session("REPORTE_PARAM_CTRLS")
                pnl_repParam.Controls.Add(pnl)
                pnl.pintaDep(pnl_repParam)
            Next
        End If

        buildReps(Session("sRepRows"))

        Dim sm As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        sm.RegisterPostBackControl(Me.btn_generarExel)
        'btn_generarExel.Attributes.Add("onclick", "return call()")
    End Sub

#End Region

#Region "Page-Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'se registran scripts para que funcionen dentro de los up_pnls
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType, "ScriptsN", " $('.nodeCross').click(function(){$(this).toggleClass('active');$(this).parent().next('.subDiv').toggleClass('active');}); $('.panel_folder_toogle').click(function(event) {var falder_content=$(this).parent().siblings('.panel-body').children('.panel-body_content');if($(this).hasClass('up')){$(this).removeClass('up');$(this).addClass('down');falder_content.show('6666',null);}else if($(this).hasClass('down')){$(this).removeClass('down');falder_content.hide('333',null);$(this).addClass('up');}});", True)

        If Not Me.IsPostBack Then

            pnl_outArbolCat.Attributes.CssStyle.Add("flex", "1")
            pnl_outArbolCat.Attributes.CssStyle.Add("padding", "10px")
            pnl_outArbolCat.Attributes.CssStyle.Add("min-width", "250px")

            Session("REPORTEID") = New Integer
        End If


        TryCast(Me.Master, MasterMascore).CargaASPX("Generador de Reportes", "Generador de Reportes")
    End Sub

#End Region

    'CatSelected - evento que se dispara cuando se seleciona una categoria dentro del arbol de categorias
    Protected Sub CatSelected(ByVal sender As LinkButton, ByVal e As System.EventArgs)

        Session("sRepRows") = New List(Of Data.DataRow)()
        pnl_reportes.Controls.Clear()
        lbl_categoria.CssClass = ""
        lbl_categoria.Text = sender.Text

        Dim selectedRows() As Data.DataRow = Session("userAvRep").Select("CATEG=" & sender.Attributes("valor").ToString)
        If selectedRows.Count > 0 Then
            For Each row As Data.DataRow In selectedRows
                Session("sRepRows").Add(row)
            Next

        End If

        buildReps(Session("sRepRows"))

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptsPBa", "document.forms[0].submit();", True)
    End Sub


#Region "build dinamic reps"

    Private Sub buildReps(ByVal rows As List(Of Data.DataRow))
        If rows.Count > 0 Then
            For Each row As Data.DataRow In rows


                Dim rowRep As New HtmlGenericControl
                rowRep.TagName = "div"
                rowRep.Attributes.CssStyle.Add("display", "flex")
                rowRep.Attributes.CssStyle.Add("justify-content", "space-between")
                rowRep.Attributes.CssStyle.Add("margin-bottom", "10px")
                rowRep.Attributes.CssStyle.Add("margin-left", "20px")
                rowRep.Attributes.CssStyle.Add("align-items", "center")
                Dim rowRepLbl As New LinkButton

                rowRepLbl.Attributes("valor") = row("ID")
                rowRepLbl.Attributes.CssStyle.Add("display", "flex")
                rowRepLbl.Attributes.CssStyle.Add("font-size", "14px")
                rowRepLbl.Attributes.CssStyle.Add("font-family", "Lato")
                rowRepLbl.Attributes.CssStyle.Add("font-weight", "300")
                rowRepLbl.Attributes.CssStyle.Add("line-height", " 1.1")
                rowRepLbl.Attributes.CssStyle.Add("cursor", " pointer")
                rowRepLbl.ClientIDMode = ClientIDMode.AutoID

                Dim lbl As New Label
                lbl.Text = "&#9679; " & row("NOMBRE")

                AddHandler rowRepLbl.Click, AddressOf preparar_Click

                pnl_datosBRep.Visible = False

                rowRepLbl.Controls.Add(lbl)

                rowRep.Controls.Add(rowRepLbl)

                pnl_reportes.Controls.Add(rowRep)
                pnl_datosBRep.Visible = False

            Next
        ElseIf Me.IsPostBack Then
            Dim lblnoREP As New Label
            lblnoREP.CssClass = "alerta"
            lblnoREP.Text = "No hay reportes disponibles en esta categoría."
            pnl_reportes.Controls.Add(lblnoREP)
            pnl_datosBRep.Visible = False
        End If
    End Sub

#End Region

#Region "Método que genera los parámetros del reporte en cuestión."
    Private Sub generaParametros()

        For Each param2Del As Web.UI.Control In pnl_repParam.Controls
            param2Del.Dispose()
        Next
        pnl_repParam.Controls.Clear()
        Session("REPORTE_PARAM_CTRLS") = New List(Of Reportes_Param_Ctrl)


        Dim repRow As Data.DataRow = Session("userAvRep").Select("ID=" & Session("REPORTEID").ToString)(0)
        lbl_repName.InnerText = repRow("NOMBRE")
        div_repDes.InnerText = repRow("DESCRIPCION")
        If repRow("GENERA") = 1 Then
            btn_GenerarRep.Visible = True
        Else
            btn_GenerarRep.Visible = False
        End If

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim datTab As New Data.DataTable
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_REPORTE", Session("adVarChar"), Session("adParamInput"), 20, Session("REPORTEID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REPORTES_PARAM"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(datTab, Session("rs"))
        Session("Con").Close()
        If datTab.Rows.Count > 0 Then
            Try
                For Each row As Data.DataRow In datTab.Rows

                    Dim regex As String
                    If IsDBNull(row("REGEX")) Then
                        regex = ""
                    Else
                        regex = row("REGEX")
                    End If

                    Dim regex_lbl As String
                    If IsDBNull(row("REGEX_LABEL")) Then
                        regex_lbl = ""
                    Else
                        regex_lbl = row("REGEX_LABEL")
                    End If
                    Dim query As String
                    If IsDBNull(row("QUERY")) Then
                        query = ""
                    Else
                        query = row("QUERY")
                    End If

                    Dim cntrlParam As New Reportes_Param_Ctrl(row("CNTRL"), query, CBool(row("OPCIONAL")), row("NOMBRE"), row("LABEL"), regex, regex_lbl, "generar_reporte_param", CBool(row("DEPENDIENTES")), True)
                    cntrlParam.CssClass = cntrlParam.CssClass & " module_subsec_elements"
                    If CBool(row("DEPENDIENTES")) Then
                        AddHandler cntrlParam.CambioValor, AddressOf actualizaParam
                    End If
                    Session("REPORTE_PARAM_CTRLS").Add(cntrlParam)
                Next
            Catch ex As Exception
                lbl_estatus.Text = lbl_categoria.Text & "------" & ex.ToString
                lbl_estatus.Visible = True
            End Try

        End If

        pnl_datosBRep.Visible = True

        If Session("REPORTE_PARAM_CTRLS").Count > 0 Then
            For Each pnl As Panel In Session("REPORTE_PARAM_CTRLS")
                pnl_repParam.Controls.Add(pnl)
            Next
        Else
            Dim lblnoparam As New Label
            lblnoparam.ForeColor = System.Drawing.Color.Red
            lblnoparam.Text = "Este reporte no requiere datos para su generación."
            pnl_repParam.Controls.Add(lblnoparam)
        End If
    End Sub

#End Region

#Region "Actualiza los parametros dependientes de parametros"
    Private Sub actualizaParam(ByVal sender As Object, e As System.EventArgs)
        lbl_categoria.Text = TryCast(sender, DropDownList).SelectedItem.Text
        pnl_repParam.Controls.Clear()
        For Each param As Reportes_Param_Ctrl In Session("REPORTE_PARAM_CTRLS")
            param.generarDependientes(True)
            pnl_repParam.Controls.Add(param)
            param.pintaDep(pnl_repParam)
        Next

    End Sub
#End Region

#Region "Evento que se dispara cuando se da click a preparar un reporte"
    Protected Sub preparar_Click(ByVal sender As LinkButton, ByVal e As System.EventArgs)
        lbl_estatus.Visible = False
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptHideReporteGrdView", "$('#div_outGridViewRep').hide('3333',null);", True)
        lbl_noRep.Visible = False
        Session("REPORTEID") = sender.Attributes("valor")
        generaParametros()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptsPB", "document.forms[0].submit();", True)
    End Sub
#End Region

#Region "fun que genera el reporte"
    Protected Function GenerarRep() As Data.DataTable
        Dim dtparams As New Data.DataTable()
        Dim dtDataRep As New Data.DataTable()
        dtparams.Columns.Add("CONCEPTO", GetType(String))
        dtparams.Columns.Add("VALOR", GetType(String))
        lbl_estatus.Visible = False

        For Each Param As Reportes_Param_Ctrl In Session("REPORTE_PARAM_CTRLS")
            dtparams.Rows.Add(Param.Clave, Param.obtenerValor())
        Next

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

            'Stored procedure quie asigan los modulos a un rol.
            connection.Open()
            Try
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("SEL_REPORTES", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure
                insertCommand.CommandTimeout = 36000
                Session("parm") = New SqlParameter("ID_REPORTE", SqlDbType.Int)
                Session("parm").Value = Session("REPORTEID")
                insertCommand.Parameters.Add(Session("parm"))
                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("PARMS_REPORTE", SqlDbType.Structured)
                Session("parm").Value = dtparams
                insertCommand.Parameters.Add(Session("parm"))
                Dim encodingToken As New UTF8Encoding()
                Dim sqlread As SqlDataReader = insertCommand.ExecuteReader()
                dtDataRep.Load(sqlread)
            Catch ex As Exception
                lbl_categoria.Text = ex.ToString
                dtDataRep = dtparams
            End Try
            connection.Close()
        End Using

        Return dtDataRep


    End Function
#End Region

#Region "generar exel"
    Sub generarExel(sender As Object, e As EventArgs)


        lbl_estatus.Visible = True
            lbl_estatus.Text = ""

            Dim dtparams As New Data.DataTable()
            dtparams.Columns.Add("CONCEPTO", GetType(String))
            dtparams.Columns.Add("VALOR", GetType(String))

            For Each Param As Reportes_Param_Ctrl In Session("REPORTE_PARAM_CTRLS")
                dtparams.Rows.Add(Param.Clave, Param.obtenerValor())
            Next

            ' Validación antes de ejecutar los procedimientos

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                connection.Open()
                Try
                    ' Configure the SqlCommand and SqlParameter.
                    Dim insertCommand As New SqlCommand("SEL_REPORTES_VALIDACION", connection)
                    insertCommand.CommandType = System.Data.CommandType.StoredProcedure
                    insertCommand.CommandTimeout = 360
                    'Parametro que representa una tabla en SQL
                    Session("parm") = New SqlParameter("ID_REPORTE", SqlDbType.Int)
                    Session("parm").Value = Session("REPORTEID")
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("PARMS_REPORTE", SqlDbType.Structured)
                    Session("parm").Value = dtparams
                    insertCommand.Parameters.Add(Session("parm"))

                    Dim encodingToken As New UTF8Encoding()
                    Dim sqlread As SqlDataReader = insertCommand.ExecuteReader()

                    While sqlread.Read()
                        ViewState("RES") = sqlread.GetString(0)
                    End While
                    sqlread.Close()

                Catch ex As Exception
                    lbl_categoria.Text = ex.ToString

                End Try
                connection.Close()
            End Using

            If ViewState("RES") = "OK" Then


                Dim dtDataRep As Data.DataTable = GenerarRep()
                If dtDataRep.Rows.Count > 0 Then

                    Try

                        Dim context As HttpContext = HttpContext.Current
                        context.Response.Clear()
                        context.Response.ContentEncoding = System.Text.Encoding.Default

                        context.Response.Clear()
                        context.Response.Buffer = True



                        context.Response.AddHeader("content-disposition", "attachment;filename=" & lbl_repName.InnerText & "_" & Now.Day.ToString & "_" & Now.Month.ToString & "_" & Now.Year.ToString & "_" & Now.Hour.ToString & "_" & Now.Minute.ToString & "_" & Now.Second.ToString & "_" & ".csv")



                        context.Response.Charset = ""
                        context.Response.ContentType = "application/text"
                        Dim sb As New StringBuilder()

                        For Each column As Data.DataColumn In dtDataRep.Columns
                            sb.Append(column.ColumnName & ","c)
                        Next
                        sb.Append(vbCr & vbLf)
                        For Each row As Data.DataRow In dtDataRep.Rows
                            For Each itm As Data.DataColumn In dtDataRep.Columns
                                sb.Append(row.Item(itm).ToString().Replace(",", ";") & ","c)
                            Next
                            sb.Append(vbCr & vbLf)
                        Next

                        context.Response.Output.Write(sb.ToString())

                    Catch ex As Exception
                        lbl_estatus.Text = "Un error ha ocurrido. Detalles: " + ex.ToString
                    Finally

                        Response.Flush()
                        Context.Response.SuppressContent = True
                        Context.Response.Redirect("CORE_REP_GENERADOR.aspx", False)
                        Context.ApplicationInstance.CompleteRequest()

                    End Try



                Else
                    lbl_estatus.Text = "El reporte no cuenta con registros"
                End If

            Else
                lbl_estatus.Text = ViewState("RES").ToString
            End If


    End Sub
#End Region

    Private Sub excel()
        'Declaro la App
        Dim xlApp As New Microsoft.Office.Interop.Excel.Application
        xlApp.DisplayAlerts = False
        'Declaro el Workbook
        Dim xlWorkbook As Microsoft.Office.Interop.Excel.Workbook
        'Abro el archivo deseado
        xlWorkbook = xlApp.Workbooks.Open(Session("APPATH").ToString + "\Excel\base.xlsx")
        'Declaro el WorkSheet
        Dim xlWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorksheet = xlWorkbook.Sheets("Hoja1")

        '----------------
        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtConsulta As New DataTable()
        '----------------

        Dim nombreReporte As String = "Reporte Delegacion"

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 25, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_MEMBRESIA_DELEGACIONES"
        Session("rs") = Session("cmd").Execute()

        'Encabezado
        xlWorksheet.Cells(1, 5) = "REPORTE DE DELEGACIÓN"

        'Columnas informacion
        xlWorksheet.Cells(3, 1) = "REGION"
        xlWorksheet.Cells(3, 2) = "DELEGACION"
        xlWorksheet.Cells(3, 3) = "CCT"
        xlWorksheet.Cells(3, 4) = "RFC"
        xlWorksheet.Cells(3, 5) = "NOMBRE"
        xlWorksheet.Cells(3, 6) = "CORREO"
        xlWorksheet.Cells(3, 7) = "TELEFONO"
        xlWorksheet.Cells(3, 8) = "BANCO"
        xlWorksheet.Cells(3, 9) = "CLABE"

        'Formato para poner en negritas el texto por rango de celdas
        xlWorksheet.Range("A1:I3").Font.Bold = True

        Dim COORDX As Integer
        COORDX = 4

        If Not Session("rs").eof Then
            'Recursivo para los datos del SP
            Do While Not Session("rs").EOF

                xlWorksheet.Cells(COORDX, 1) = Session("rs").Fields("REGION").Value.ToString()
                xlWorksheet.Cells(COORDX, 2) = Session("rs").Fields("DELEGACION").Value.ToString()
                xlWorksheet.Cells(COORDX, 3) = Session("rs").Fields("CCT").Value.ToString()
                xlWorksheet.Cells(COORDX, 4) = Session("rs").Fields("RFC").Value.ToString()
                xlWorksheet.Cells(COORDX, 5) = Session("rs").Fields("NOMBRE").Value.ToString()
                xlWorksheet.Cells(COORDX, 6) = Session("rs").Fields("CORREO").Value.ToString()
                xlWorksheet.Cells(COORDX, 7) = Session("rs").Fields("TELEFONO").Value.ToString()
                xlWorksheet.Cells(COORDX, 8) = Session("rs").Fields("BANCO").Value.ToString()
                xlWorksheet.Cells(COORDX, 9) = "'" + Session("rs").Fields("CLABE").Value.ToString()

                COORDX += 1
                Session("rs").movenext()

            Loop
        End If

        COORDX += 1

        Session("Con").Close()
        Dim Filename, FilePath As String
        'Filename = "Poliza_Contable.xls"
        Filename = nombreReporte + ".xls"
        FilePath = Session("APPATH").ToString + "Excel\"
        xlWorkbook.SaveAs(FilePath + Filename, FileFormat:=56)
        xlWorkbook.Close()

        Dim fs As IO.FileStream

        fs = IO.File.Open(FilePath + Filename, IO.FileMode.Open)
        Dim bytBytes(fs.Length) As Byte
        fs.Read(bytBytes, 0, fs.Length)
        fs.Close()
        'Borra el archivo creado en memoria
        DelHDFile(FilePath + Filename)
        Response.Buffer = True
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()
        Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", Filename))
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.BinaryWrite(bytBytes)
        Response.End()
    End Sub

    Private Sub DelHDFile(ByVal File1 As String)
        If File.Exists(File1) Then
            Dim fi As New System.IO.FileInfo(File1)
            If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
                fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
            End If
        Else
            lbl_estatus.Text = "Alerta: El archivo ha sido movido o eliminado"
        End If
        System.IO.File.Delete(File1)
    End Sub

#Region "generar reporte en gridview"
    Protected Sub generar_GridView(sender As Object, e As EventArgs)
        Dim dtDataRep As Data.DataTable = GenerarRep()
        If dtDataRep.Rows.Count > 0 Then
            grv_Reporte.DataSource = dtDataRep
            grv_Reporte.DataBind()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptReporteGrdView", "$('#div_outGridViewRep').show('6666',null);", True)
            lbl_estatus.Text = "Éxito al generar el reporte"
        Else
            lbl_estatus.Text = "El reporte no cuenta con registros."
        End If
        lbl_estatus.Visible = True
    End Sub
#End Region


End Class