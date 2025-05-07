Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Ionic.Zip
Public Class CRED_EXP_DESCUENTOS_PROC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Pagos y Descuentos", "Pagos y descuentos por aplicar de préstamos a afiliado")

        If Not Me.IsPostBack Then
            LlenaPeriodos()
        End If

    End Sub

    Private Sub LlenaPeriodos()

        cmb_Periodos.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_Periodos.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVE_POLIZA", Session("adVarChar"), Session("adParamInput"), 10, "PAGO PRES")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINSTITUCION", Session("adVarChar"), Session("adParamInput"), 10, "-1")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_POLIZA_SAP_FECHAS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("FECHADESC").Value.ToString, Session("rs").Fields("FECHA").Value.ToString)
            cmb_Periodos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_Periodos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Periodos.SelectedIndexChanged

        If cmb_Periodos.SelectedItem.Value = "-1" Then
            Limpia()
        Else
            ConsultarDescuentos()
        End If

    End Sub

    Private Sub ConsultarDescuentos()

        'Periodos Activos
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim Descuentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_SISTEMA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Periodos.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DESCUENTOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(Descuentos, Session("rs"))
        Session("Con").Close()

        If Descuentos.Rows.Count > 0 Then
            dag_Descuentos.Visible = True
            dag_Descuentos.DataSource = Descuentos
            dag_Descuentos.DataBind()
            btn_GetPolizaDescuentos.Visible = True
            btn_CSVTESORERIA.Visible = True
            btn_CSVINSTITUCION.Visible = True
        Else
            dag_Descuentos.Visible = False
            dag_Descuentos.DataSource = Nothing
            dag_Descuentos.DataBind()
            btn_GetPolizaDescuentos.Visible = False
            lbl_status.Text = "No se encontraron descuentos para la fecha solicitada."
        End If

    End Sub

    Private Sub Limpia()

        dag_Descuentos.Visible = False
        dag_Descuentos.DataSource = Nothing
        dag_Descuentos.DataBind()
        btn_GetPolizaDescuentos.Visible = False
        lbl_status.Text = ""

    End Sub

    Protected Sub dag_Descuentos_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dag_Descuentos.RowCommand

        If e.CommandName = "ACTUALIZAR" Then

            Dim RowToUpdate As Integer = Convert.ToInt32(e.CommandArgument)
            Dim Row As GridViewRow = dag_Descuentos.Rows(RowToUpdate)

            If Row.Cells(8).Text <> "DESCUENTO CANCELADO" And Row.Cells(8).Text <> "DESCUENTO CONFIRMADO" And Row.Cells(8).Text <> "DESCUENTO REGISTRADO" And Row.Cells(8).Text <> "DESCUENTO CONSUMIDO" Then

                Dim Folio As Integer = Row.Cells(2).Text
                Dim IMPORTE_DESCUENTO As Decimal = Row.Cells(5).Text

                EliminarDescuento(Folio, IMPORTE_DESCUENTO)
            Else

                lbl_status.Text = "Error: El descuento no puede ser cancelado and el estatus actual."

            End If

        End If

        ConsultarDescuentos()

    End Sub

    Private Sub EliminarDescuento(ByVal Folio As Integer, ByVal ImporteDescuento As Decimal)

        Dim Resultado As String = ""

        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IMPORTE_DESCUENTO", Session("adVarChar"), Session("adParamInput"), 50, ImporteDescuento)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHA_DESCUENTO", Session("adVarChar"), Session("adParamInput"), 10, ViewState("FECHADESCUENTO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_DESCUENTO_CANCELAR"
            Session("rs") = Session("cmd").Execute()
            Resultado = Session("rs").Fields("RESULTADO").Value.ToString()
            Session("Con").Close()

        Catch ex As Exception
            Resultado = "Error de aplicación."
        End Try

        If Resultado = "OK" Then
            Resultado = "Descuento cancelado correctamente."
        End If

        lbl_status.Text = Resultado

    End Sub

    Protected Sub btn_GetPolizaDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GetPolizaDescuentos.Click

        DescargaPolizaDescuentos()

    End Sub

    Protected Sub btn_CSVTESORERIA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CSVTESORERIA.Click

        DescargaCSVTESORERIA()

    End Sub

    Protected Sub btn_CSVTINSTITUCION_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CSVINSTITUCION.Click

        DescargaCSVINSTITUCION()

    End Sub



    Private Sub GenerarFormato(ByVal numTrab As String, ByVal cveExp As String, ByVal folioExp As String) ', ByVal cIDCliente As String, ByVal cCliente As String, ByVal PagATRA As String, ByVal FOLIO As String, ByVal IDPLANTILLA As Integer)
        Dim lcUrl As String = "RECIBO.docx"
        Dim NewDocName As String = cveExp + "-" + numTrab + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        ' Dim cPath As String = Session("APPATH") + "\Word\COBRANZA\"
        Dim cPath As String = Session("APPATH") + "DocPlantillas\Solicitudes\RECIBOS\"
        Dim cPath1 As String = Session("NUEVARUTA").ToString + "\"
        Dim cPathNewDoc As String = cPath1 + NewDocName + ".docx"
        'lbl_status_docs.Text = " ruta: " + cPath1
        ' lbl_status.Text = "rutaarchivo " + cPathNewDoc

        lbl_status.Text = numTrab + " " + cveExp + " " + folioExp + " " + cPath + " " + lcUrl

        Using worddoc As Novacode.DocX = Novacode.DocX.Load(cPath + lcUrl)
            Try
                worddoc.SaveAs(cPathNewDoc)
                Session("Con").Open()
                'If IDPLANTILLA = 1 Then
                'ObtieneInfoPL1(NewDocName, cPath1, cPathNewDoc, cIDCliente, cCliente, PagATRA)
                'ElseIf IDPLANTILLA = 2 Then
                'ObtieneInfoPL2(NewDocName, cPath1, cPathNewDoc, cIDCliente, cCliente, PagATRA)
                'Else
                'ObtieneInfoPL3(NewDocName, cPath1, cPathNewDoc, cIDCliente, cCliente, PagATRA)
                'End If

            Catch ex As Exception
                lbl_status.Text = ex.ToString
            Finally
                Session("Con").Close()
            End Try
        End Using
    End Sub

    Private Sub Comprimir(ByVal Ruta As String)

        Dim itempaths As String() = New String() {Ruta}
        Using zip As ZipFile = New ZipFile()
            For i = 0 To itempaths.Length - 1
                zip.AddItem(itempaths(i), "")
            Next
            zip.Save(Ruta + "\EXPEDIENTE_" + Session("USERID").ToString + ".zip")
        End Using
    End Sub

    Private Sub DescargaPolizaDescuentos()

        Try
            'lbl_status.Text = "PERIODO: " + cmb_Periodos.SelectedItem.Value + " IDINST: -1 TIPO POLIZA: PAGO PRES" + " IDUSER: " + Session("USERID") + " SESION: " + Session("Sesion")
            ''Periodos Activos
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim dtMovimientosDescuentos As New Data.DataTable()
            Dim NombrePoliza As String = ""

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FECHA_SISTEMA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Periodos.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDINST", Session("adVarChar"), Session("adParamInput"), 10, -1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO_POLIZA", Session("adVarChar"), Session("adParamInput"), 20, "PAGO PRES")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_POLIZA_DESCUENTOS_SAP"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtMovimientosDescuentos, Session("rs"))
            Session("Con").Close()

            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtMovimientosDescuentos.Rows
                For i = 0 To dtMovimientosDescuentos.Columns.Count - 1

                    If i > 1 Then
                        context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
                    Else
                        If iRow = 0 Then
                            NombrePoliza = Renglon.Item(i).ToString
                            iRow = 1
                        End If
                    End If

                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombrePoliza + " (" + Replace(cmb_Periodos.SelectedItem.Value, "/", "") + ")" + ".csv")
            context.Response.End()

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            ConsultarDescuentos()
        End Try

    End Sub

    Private Sub DescargaCSVTESORERIA()

        Try
            'lbl_status.Text = "PERIODO: " + cmb_Periodos.SelectedItem.Value + " IDINST: -1 TIPO POLIZA: PAGO PRES" + " IDUSER: " + Session("USERID") + " SESION: " + Session("Sesion")
            ''Periodos Activos
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim dtMovimientosDescuentos As New Data.DataTable()
            Dim NombrePoliza As String = "CREDITOS_POR_PAGAR"

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FECHA_SISTEMA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Periodos.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CREDITOS_X_PAGAR"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtMovimientosDescuentos, Session("rs"))
            Session("Con").Close()

            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtMovimientosDescuentos.Rows
                For i = 0 To dtMovimientosDescuentos.Columns.Count - 1


                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")


                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombrePoliza + " (" + Replace(cmb_Periodos.SelectedItem.Value, "/", "") + ")" + ".csv")
            context.Response.End()

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            ConsultarDescuentos()
        End Try

    End Sub

    Private Sub DescargaCSVINSTITUCION()

        Try
            'lbl_status.Text = "PERIODO: " + cmb_Periodos.SelectedItem.Value + " IDINST: -1 TIPO POLIZA: PAGO PRES" + " IDUSER: " + Session("USERID") + " SESION: " + Session("Sesion")
            ''Periodos Activos
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim dtMovimientosDescuentos As New Data.DataTable()
            Dim NombrePoliza As String = "CREDITOS_INSTITUCION"

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FECHA_SISTEMA", Session("adVarChar"), Session("adParamInput"), 10, cmb_Periodos.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, 2)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CREDITOS_X_PAGAR"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtMovimientosDescuentos, Session("rs"))
            Session("Con").Close()

            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = System.Text.Encoding.Default
            Dim iRow As Integer = 0
            Dim i As Integer

            For Each Renglon As Data.DataRow In dtMovimientosDescuentos.Rows
                For i = 0 To dtMovimientosDescuentos.Columns.Count - 1


                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")


                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombrePoliza + " (" + Replace(cmb_Periodos.SelectedItem.Value, "/", "") + ")" + ".csv")
            context.Response.End()

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            ConsultarDescuentos()
        End Try

    End Sub


End Class