Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class NOTIFICACIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        TryCast(TryCast(Me.Master, MasterMascore).FindControl("lbl_tituloASPX"), Label).Text = "Notificaciones"

        Cargar_not()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim menuPanel As Panel
        menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

        If Not menuPanel Is Nothing Then
            menuPanel.Visible = True
            LlenaDatos()
        End If
    End Sub
#Region "cargar_not"
    'METODO PARA CARGAR LAS NOTIFICACIONES ACTIVAS DEL USUARIO
    Private Sub Cargar_not()
        'declaramos variables
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtnotif As New Data.DataTable()
        'EXECUTAMOS EL PROCEDIMENTO QUE NOS TRAERA LOS TEXTOS DE LAS NOTIFICACIONES ACTIVAS PARA EL USUARIO 
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_NOTIFICACIONES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtnotif, Session("rs"))
        If dtnotif.Rows.Count = 0 Then
            no_notif.Visible = "True"
            cont_notif.Visible = "False"
        Else
            For Each row As Data.DataRow In dtnotif.Rows
                Dim texto As String
                texto = (row.Item("TEXTO").ToString())
                Dim id As Long = (row.Item("ID"))
                Dim colorEx As String = ""
                Select Case row.Item("EXP_STATUS")
                    Case 2
                        colorEx = "#fed189"
                    Case 1
                        colorEx = "#113964"
                    Case 0
                        colorEx = "#9e9e9e"
                End Select
                'declaro los controles que contendran la notificación
                Dim notif As New Panel
                Dim notif_in As New HtmlGenericControl
                Dim notif_status As New HtmlGenericControl
                Dim notif_text As New HtmlGenericControl
                Dim notif_out_btn As New HtmlGenericControl
                Dim notif_in_btn As New Button
                Dim notif_date As New Label
                'establezco las características de los controles
                notif.CssClass = "panel notif_panel"
                notif.ID = "not" & id.ToString

                notif_in.TagName = "div"
                notif_in.Attributes("class") = "module_subsec align_items_flex_center no_m"
                notif_in.Attributes("style") = "justify-content:space-between;font-size:16px; padding:7.5px;"

                notif_status.TagName = "div"
                notif_status.Attributes("class") = "module_subsec_elements module_subsec_free-elements"
                notif_status.Attributes("style") = "border-radius:50px; width:16px;height:16px;  opacity: 0.7; background-color:" & colorEx & ";"

                notif_text.TagName = "div"
                notif_text.Attributes("class") = "text_input_nice_label title_tag"
                notif_text.Attributes("style") = "flex-grow:1;display:flex;flex-wrap:wrap;"
                notif_text.InnerText = texto

                notif_out_btn.TagName = "div"
                notif_out_btn.Attributes("class") = "module_subsec_elements module_subsec_free-elements"

                notif_in_btn.ID = "not_btn" & id.ToString
                notif_in_btn.CssClass = "btn btn-primary btn_hn"
                notif_in_btn.Text = "Resolver"

                notif_date.Text = row.Item("FECHA")
                notif_date.CssClass = "text_input_nice_label title_tag"
                'notif_date.Attributes.CssStyle.Add("color", "#113964")

                AddHandler notif_in_btn.Click, AddressOf Not_marcar
                'uno los controles
                notif_out_btn.Controls.Add(notif_in_btn)
                notif_in.Controls.Add(notif_status)
                notif_in.Controls.Add(notif_text)
                notif_in.Controls.Add(notif_date)
                notif_in.Controls.Add(notif_out_btn)
                notif.Controls.Add(notif_in)
                'agrego la notificacion al panel que contiene las notificaciones
                cont_notif.Controls.Add(notif)
            Next
        End If
        Session("Con").Close()


    End Sub
    'evento que maneja cuando se le da click a los botones generados dinamicamente
    Protected Sub Not_marcar(ByVal sender As Button, ByVal e As System.EventArgs)
        'se declaran e inicializan variables
        Dim dtnot As New Data.DataTable
        dtnot.Columns.Add("IDCONCEPTO", GetType(String))
        dtnot.Columns.Add("VALOR", GetType(Decimal))
        Dim id As Integer
        id = sender.ID.Substring(7, sender.ID.Length - 7)
        Dim notificacion As Panel
        notificacion = cont_notif.FindControl("not" & id)
        dtnot.Rows.Add(id, 2)
        Dim pclass As String = notificacion.CssClass
        'se executa una animación y se elimina el control que contiene la notificación
        cont_notif.Controls.Remove(notificacion)
        'EXECUTAMOS EL PROCEDIMENTO QUE NOS cambiara el estado de la notificación en cuastion a "resuelto"
        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("UPD_NOTIFS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("NOTIFS", SqlDbType.Structured)
                Session("parm").Value = dtnot
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()
            End Using

        Catch ex As Exception
            notificacion.CssClass = pclass
        End Try

        If cont_notif.Controls.Count = 0 Then
            no_notif.Visible = "True"
            cont_notif.Visible = "False"
        End If
    End Sub


#End Region




    Private Sub LlenaDatos()
        'OBTENGO DATOS SIMPLES DEL USUARIO PARA DESPLEGARLOS EN LAS PLECAS
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_USUARIO"
        Session("rs") = Session("cmd").Execute()


        'CREO VARIABLES DE SESION DE NOMBRE DE USUARIO, FECHA ULTIMA SESION Y FECHA DE SISTEMA
        Session("NombreUsr") = Session("rs").Fields("NOMBRE").Value.ToString()
        Session("FechaSis") = Left(Session("rs").Fields("FECHASIS").Value.ToString, 10)
        Session("FechaUltSes") = Session("rs").Fields("ULT_SESION").Value.ToString()
        Session("Con").Close()



    End Sub


    Protected Sub btn_test_Click(sender As Object, e As EventArgs) Handles btn_test.Click


        ' Create a spreadsheet document by supplying the filepath.
        ' By default, AutoSave = true, Editable = true, and Type = xlsx.
        'Dim spreadsheetDocument As SpreadsheetDocument = spreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook)
        Dim spreadsheetDocument As SpreadsheetDocument = SpreadsheetDocument.Create("E:\hola\hola.xlsx", SpreadsheetDocumentType.Workbook)
        ' Add a WorkbookPart to the document.
        Dim workbookpart As WorkbookPart = spreadsheetDocument.AddWorkbookPart
        workbookpart.Workbook = New Workbook

        ' Add a WorksheetPart to the WorkbookPart.
        Dim worksheetPart As WorksheetPart = workbookpart.AddNewPart(Of WorksheetPart)()
        worksheetPart.Worksheet = New Worksheet(New SheetData())

        ' Add Sheets to the Workbook.
        Dim sheets As Sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(Of Sheets)(New Sheets())

        ' Append a new worksheet and associate it with the workbook.
        Dim sheet As Sheet = New Sheet
        sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart)
        sheet.SheetId = 1
        sheet.Name = "mySheet"

        sheets.Append(sheet)

        workbookpart.Workbook.Save()

        ' Close the document.
        spreadsheetDocument.Close()

        Response.Clear()
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("content-disposition", "attachment;filename=name_your_file.xlsx")
        Response.WriteFile("E:\hola\hola.xlsx")
        Response.End()


    End Sub

    Public Sub CreateSpreadsheetWorkbook(ByVal filepath As String)



    End Sub



End Class