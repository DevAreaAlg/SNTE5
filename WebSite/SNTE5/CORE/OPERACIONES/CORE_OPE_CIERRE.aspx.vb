Imports System.Data
Imports System.IO
Imports System.Linq
Public Class CORE_OPE_CIERRE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Cierre de Operaciones", "Cierre de Operaciones")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            revisaliberacion()

            'llena_checks()
        End If
    End Sub

    Private Sub revisaliberacion()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_leftovers As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_VERIFICA_CRED_LIBERADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_leftovers, Session("rs"))
        Session("Con").Close()

        dt_leftovers.Clear()

    End Sub

    Protected Sub btn_cambiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cambiar.Click

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_DESCUENTOS_CONFIRMADOS"
        Session("rs") = Session("cmd").Execute()

        Dim RESPUESTA As String
        RESPUESTA = Session("rs").Fields("RESPUESTA").Value.ToString

        Session("Con").Close()

        ' If RESPUESTA = "1" Or RESPUESTA = "0" Then
        Session("cmd") = New ADODB.Command()
        Session("Con").ConnectionTimeout = 2400

        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandTimeout = 1000000
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CIERRE_DIARIO_UNIVERSIDAD"
        Session("rs") = Session("cmd").Execute()

        Dim RES As String
        RES = Session("rs").Fields("RES").Value.ToString

        If RES = "1" Then
            Session("FechaSis") = Left(Session("rs").Fields("MSTSISFECHA_FECHA").Value.ToString, 10)
            lbl_status.Text = "Cierre de operaciones ejecutado exitosamente"

            Dim lbl_fecha_sistema As Label
            lbl_fecha_sistema = CType(Master.FindControl("lbl_fecha"), Label)

            If Not lbl_fecha_sistema Is Nothing Then
                lbl_fecha_sistema.Text = "Fecha Sistema: " + Session("FechaSis")

            End If

            btn_cambiar.Enabled = False
        End If
        If RES = "0" Then
            lbl_status.Text = Session("rs").Fields("ALERTA").Value
        End If

        Session("Con").Close()

        If RES = "1" Then
            GenerarOrdenDescuento()
        End If
        'Else
        'lbl_status.Text = "Error: Existen quincenas de descuentos por revisar"
        ' End If

    End Sub


    Protected Sub btn_descuentos_conciliar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_descuentos_conciliar.Click

        DescargaCSVDescuentosConciliar()

    End Sub

    Private Sub DescargaCSVDescuentosConciliar()

        Try

            Dim custDA As New OleDb.OleDbDataAdapter()
            Dim dtDescuentos As New DataTable()
            Dim NombreCSV As String = "Descuentos por conciliar"

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DESCUENTOS_POR_CONCILIAR"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtDescuentos, Session("rs"))
            Session("Con").Close()

            Dim context As HttpContext = HttpContext.Current
            context.Response.Clear()
            context.Response.ContentEncoding = Encoding.Default
            Dim i As Integer

            For Each Renglon As DataRow In dtDescuentos.Rows
                For i = 0 To dtDescuentos.Columns.Count - 1
                    context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
                Next
                context.Response.Write(Environment.NewLine)
            Next

            context.Response.ContentType = "text/csv"
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreCSV + ".csv")
            context.Response.End()

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally

        End Try

    End Sub

#Region "Cierre Comentado"

    'Protected Sub dag_if_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_if.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim imagen As Image = CType(e.Item.FindControl("SEMAFORO"), Image)
    '        Dim terminado As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "SEMAFORO").ToString())
    '        imagen.ImageUrl = IIf(terminado = 1, "~\img\SemaforoVERDE.png", "~\img\SemaforoROJO.png")
    '    End If
    'End Sub

    'Protected Sub dag_div_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_div.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim imagen As Image = CType(e.Item.FindControl("SEMAFORO"), Image)
    '        Dim terminado As Int32 = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "SEMAFORO").ToString())
    '        imagen.ImageUrl = IIf(terminado = 1, "~\img\SemaforoVERDE.png", "~\img\SemaforoROJO.png")
    '    End If
    'End Sub

    'Private Sub llena_checks()
    '    Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    '    Dim dt_leftovers As New Data.DataTable()
    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_COND_CIERRE_DIA_CORTE_CAJAS"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dt_leftovers, Session("rs"))
    '    dag_cc.DataSource = dt_leftovers
    '    dag_cc.DataBind()

    '    If dt_leftovers.Select("SEMAFORO=0").Length > 0 Then
    '        btn_cambiar.Enabled = False
    '    End If

    '    dt_leftovers.Clear()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_COND_CIERRE_DIA_INDICES_FINANCIEROS"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dt_leftovers, Session("rs"))
    '    dag_if.DataSource = dt_leftovers
    '    dag_if.DataBind()

    '    If dt_leftovers.Select("SEMAFORO=0").Length > 0 Then
    '        btn_cambiar.Enabled = False
    '    End If

    '    dt_leftovers.Clear()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_COND_CIERRE_DIA_DIVISAS"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dt_leftovers, Session("rs"))
    '    dag_div.DataSource = dt_leftovers
    '    dag_div.DataBind()
    '    If dt_leftovers.Select("SEMAFORO=0").Length > 0 Then
    '        btn_cambiar.Enabled = False
    '    End If
    '    Session("Con").Close()

    'End Sub

    'Private Sub AvisoPLDCorreo()

    '    Dim subject As String = String.Empty 'variable para el asunto del correo
    '    Dim cc As String = String.Empty 'correo de copia
    '    Dim clase_Correo As New Correo 'variable para la clase de correo
    '    Dim sbhtml As New StringBuilder
    '    Dim correo As String
    '    Dim pendientes As String
    '    Dim cliente As String = String.Empty
    '    Dim exp As String = String.Empty
    '    Dim folio As String = String.Empty
    '    Dim descripcion As String = String.Empty
    '    Dim monto As String = String.Empty
    '    Dim nota As String = String.Empty
    '    Dim fecha As String = String.Empty
    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_OPERACION_ALERTA_PLD_AVISO_PENDIENTES"
    '    Session("rs") = Session("cmd").Execute()

    '    pendientes = Session("rs").Fields("PENDIENTES").Value.ToString
    '    subject = "Alerta: Operaciones inusuales, relevantes y preocupantes pendientes"

    '    If pendientes = "SI" Then

    '        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
    '        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #87CEEB; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
    '        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
    '        sbhtml.Append("<tr><td>Estimado(a) Oficial :  " + Session("rs").Fields("NOMBRE").Value.ToString + "</td></tr>")
    '        sbhtml.Append("<tr><td>Se informa que las siguientes alertas de operaciones inusuales, relevantes y preocupantes siguen en lista de espera para su revisión.</td></tr>")
    '        sbhtml.Append("<br></br>")
    '        sbhtml.Append("</table>")

    '        Do While Not Session("rs").EOF
    '            cliente = Session("rs").Fields("PERSONA").Value.ToString
    '            exp = Session("rs").Fields("FOLIO").Value.ToString
    '            descripcion = Session("rs").Fields("DESCRIPCION").Value.ToString
    '            monto = Session("rs").Fields("MONTO").Value.ToString
    '            nota = Session("rs").Fields("NOTA").Value.ToString
    '            fecha = Session("rs").Fields("FECHA").Value.ToString
    '            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
    '            sbhtml.Append("<tr><td width='30%'>Cliente: </td>" + "<b>" + cliente + "</b>" + "</td></tr>")
    '            sbhtml.Append("<tr><td width='50%'>Expediente: </td>" + "<b>" + folio + "</b>" + "</td></tr>")
    '            sbhtml.Append("<tr><td width='30%'>Operación: </td>" + "<b>" + descripcion + "</b>" + "</td></tr>")
    '            sbhtml.Append("<tr><td width='50%'>Monto: </td>" + "<b>" + monto + "</b>" + "</td></tr>")
    '            sbhtml.Append("<tr><td width='50%'>Descripciòn: </td>" + "<b>" + nota + "</b>" + "</td></tr>")
    '            sbhtml.Append("<tr><td width='50%'>Fecha de Sistema: </td>" + "<b>" + fecha + "</b>" + "</td></tr>")
    '            sbhtml.Append("<br></br>")
    '            Session("rs").movenext()
    '        Loop

    '        sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA").ToString + "</td></tr>")
    '        sbhtml.Append("<br></br>")
    '        sbhtml.Append("</table>")
    '        sbhtml.Append("<br></br>")

    '    End If
    '    Session("Con").Close()
    '    If pendientes = "SI" Then

    '        Session("Con").Open()
    '        Session("cmd") = New ADODB.Command()
    '        Session("cmd").ActiveConnection = Session("Con")
    '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "ALERTAPLD")
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
    '        Session("rs") = Session("cmd").Execute()


    '        Do While Not Session("rs").EOF
    '            clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

    '            Session("rs").movenext()
    '        Loop

    '        Session("Con").Close()

    '    End If

    'End Sub

    'Private Sub AvisoEntregaReportes()

    '    Dim correo As String

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_AVISOS_REPORTES"
    '    Session("rs") = Session("cmd").Execute()
    '    Do While Not Session("rs").EOF

    '        correo = "Estimado(a) " + Session("rs").Fields("USUARIO").Value.ToString + vbCrLf + vbCrLf + "REPORTE: " + Session("rs").Fields("REPORTE").Value.ToString + vbCrLf + "FECHA LIMITE: " + Session("rs").Fields("FECHA_LIM").Value.ToString + vbCrLf + Session("rs").Fields("AVISO").Value.ToString + vbCrLf
    '        correo = correo + vbCrLf + "Atentamente" + vbCrLf + vbCrLf + "MAS.Core" + vbCrLf + Session("EMPRESA").ToString

    '        Const ConfigNamespace As String = "http://schemas.microsoft.com/cdo/configuration/"
    '        Dim oMsg As New CDO.Message()
    '        Dim iConfig As New CDO.Configuration()
    '        Dim Flds As ADODB.Fields = iConfig.Fields
    '        With iConfig.Fields
    '            .Item(ConfigNamespace & "smtpserver").Value = Session("MAIL_SERVER")
    '            .Item(ConfigNamespace & "smtpserverport").Value = Session("MAIL_SERVER_PORT")
    '            .Item(ConfigNamespace & "sendusing").Value = CDO.CdoSendUsing.cdoSendUsingPort
    '            If Session("MAIL_SERVER_SSL") = 1 Then
    '                .Item(ConfigNamespace & "smtpusessl").Value = True
    '            End If
    '            .Item(ConfigNamespace & "sendusername").Value = Session("MAIL_SERVER_USER")
    '            .Item(ConfigNamespace & "sendpassword").Value = Session("MAIL_SERVER_PWD")
    '            .Item(ConfigNamespace & "smtpauthenticate").Value = CDO.CdoProtocolsAuthentication.cdoBasic
    '            .Update()
    '        End With

    '        With oMsg
    '            .Configuration = iConfig
    '            .From = Session("MAIL_SERVER_FROM")
    '            .To = Session("rs").Fields("CORREO").Value.ToString
    '            .Subject = "Aviso de entrega de reporte: " + Session("rs").Fields("REPORTE").Value.ToString
    '            .TextBody = correo
    '            .Send()
    '        End With
    '        oMsg = Nothing
    '        iConfig = Nothing

    '        Session("rs").movenext()
    '    Loop

    '    Session("Con").Close()

    'End Sub

#End Region

#Region "Descuentos"

    'Protected Sub descarga_descuentos()

    '    Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
    '    Dim dtDESCUENTOS As New Data.DataTable()

    '    Dim RutaPoliza As String = ConfigurationManager.AppSettings.[Get]("urlDescuentos")
    '    Dim Filename As String = "ArchivoPagosXAplicarPrestamos (" + Replace(Session("FechaSis").ToString, "/", "") + ") " + CStr(Session("USERID")) + ".csv"

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
    '    Session("cmd").Parameters.Append(Session("parm"))
    '    Session("cmd").CommandText = "SEL_DESCUENTOS_DESGLOSE"
    '    Session("rs") = Session("cmd").Execute()
    '    custDA.Fill(dtDESCUENTOS, Session("rs"))

    '    Session("Con").Close()

    '    Dim i As Integer
    '    Using swOutputFile As New StreamWriter(File.Open(RutaPoliza + "\" + Filename, FileMode.Append))

    '        For i = 0 To dtDESCUENTOS.Columns.Count - 1
    '            swOutputFile.Write(dtDESCUENTOS.Columns(i).Caption + ",")
    '        Next

    '        swOutputFile.Write(Environment.NewLine)

    '        For Each Renglon As Data.DataRow In dtDESCUENTOS.Rows

    '            For i = 0 To dtDESCUENTOS.Columns.Count - 1
    '                swOutputFile.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
    '            Next

    '            swOutputFile.Write(Environment.NewLine)
    '        Next
    '        swOutputFile.Close()
    '    End Using

    'End Sub

    'Protected Sub btn_GetArchivoDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GetArchivoDescuentos.Click

    '    Dim xlApp = Nothing
    '    xlApp = New Microsoft.Office.Interop.Excel.Application
    '    xlApp.DisplayAlerts = False

    '    Dim xlWorkbook As Microsoft.Office.Interop.Excel.Workbook = xlApp.Workbooks.Open(Session("APPATH").ToString + "ExcelPlantillas\ArchivoPagosXAplicarPrestamos.xlsx")
    '    Dim xlWorksheet As Microsoft.Office.Interop.Excel.Worksheet = xlWorkbook.Sheets("DESCUENTOS")

    '    Dim Fila As Integer = 1
    '    Dim ColumnaInicial As Integer = 1
    '    Dim Columna As Integer = ColumnaInicial
    '    Dim Filename As String
    '    Dim RutaPoliza As String
    '    RutaPoliza = ConfigurationManager.AppSettings.[Get]("urlpolizas")
    '    Filename = "ArchivoPagosXAplicarPrestamos (" + Replace(Session("FechaSis").ToString, "/", "") + ") " + CStr(Session("USERID")) + ".xlsx"

    '    Try
    '        xlWorksheet.Cells(Fila, Columna) = "Num_empleado"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "ID_deduccion"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "Digito"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "NumePagos"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "TotalPagos"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "Importe"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "FechaEnvio"
    '        Columna = Columna + 1
    '        xlWorksheet.Cells(Fila, Columna) = "FechaDscto"

    '        Fila = Fila + 1
    '        Columna = ColumnaInicial

    '        Session("Con").Open()
    '        Session("cmd") = New ADODB.Command()
    '        Session("cmd").ActiveConnection = Session("Con")
    '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
    '        Session("cmd").Parameters.Append(Session("parm"))
    '        Session("cmd").CommandText = "SEL_DESCUENTOS_DESGLOSE"
    '        Session("rs") = Session("cmd").Execute()
    '        If Not Session("rs").eof Then
    '            Do While Not Session("rs").EOF

    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("Num_empleado").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("ID_deduccion").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("Digito").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("NumePagos").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("TotalPagos").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("Importe").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("FechaEnvio").Value.ToString
    '                Columna = Columna + 1
    '                xlWorksheet.Cells(Fila, Columna) = Session("rs").Fields("FechaDscto").Value.ToString

    '                Fila = Fila + 1
    '                Columna = ColumnaInicial
    '                Session("rs").movenext()

    '            Loop
    '        End If

    '        Session("Con").Close()

    '        xlWorkbook.SaveAs(RutaPoliza + "\" + Filename)
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook)
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet)
    '        xlWorkbook.Close()
    '        xlWorksheet = Nothing
    '        xlWorkbook = Nothing
    '        xlApp.DisplayAlerts = False
    '        xlApp.Quit()

    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)

    '        xlApp = Nothing
    '    Catch ex As Exception
    '        lbl_status.Text = ex.Message()
    '    Finally

    '        GC.Collect()
    '        GC.WaitForPendingFinalizers()

    '    End Try


    'End Sub

#End Region


#Region "Orden de Descuento"

    Private Sub GenerarOrdenDescuento()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_GENERA_ORDEN_DESCUENTO"
        Session("rs") = Session("cmd").Execute()
        Dim Envia As Integer = Convert.ToInt32(Session("rs").Fields("RESPUESTA").Value.ToString)
        Session("Con").Close()

        If Envia = 1 Then
            GeneraOrdenDescuentoCorreo()
        End If

    End Sub

    Private Sub GeneraOrdenDescuentoCorreo()

        Dim cc As String = String.Empty 'Correo al cual se le puede enviar copia
        Dim correo As New Correo 'Variable para la clase de correo
        Dim sbhtml As New StringBuilder

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "GENORDDESC")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()

        Dim Periodo As Date = Convert.ToDateTime(Session("FechaSis"))
        Dim subject As String = "Nueva Orden de Descuento Periodo " + Periodo.ToString("dd/MM/yyyy")

        Do While Not Session("rs").EOF

            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a)</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Se informa que ya puede generar la siguiente Orden de Descuentos.</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Favor de pasar al modulo correspondiente para generar su archivo DBF u Oficio de Descuentos.</td></tr>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente: " + Session("EMPRESA").ToString + "</td></tr>")
            sbhtml.Append("</table>")

            'Envio de Correo
            correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

            sbhtml.Clear()

            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

#End Region

End Class