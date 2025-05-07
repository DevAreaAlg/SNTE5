Imports System.Data
Imports System.Data.SqlClient
Public Class PEN_CNF_CALENDARIOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            CargaAnios()
            CargaQuincenas(-1)
            CargaActual()
            lnk_layout.Attributes.Add("onclick", "window.open('/DocPlantillas/Manuales/Layout_Calendario_Institucional.csv');")
        End If

        TryCast(Master, MasterMascore).CargaASPX("Calendario Institucional", "Calendario Institucional")

    End Sub

    Protected Sub lnk_calendario_actual_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnk_calendario_actual.Click

        lbl_estatus.Text = ""

        ExcelCalendario()

    End Sub

    Private Sub CargaActual()
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_QUINCENA_BASE_TEXT"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_actual.Text = Session("rs").Fields("VALOR").value.ToString
        End If
        Session("Con").Close()
    End Sub

    Private Sub ExcelCalendario()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtConsulta As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CALENDARIO_INSTITUCIONAL"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Calendario Institucional " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    Protected Sub btn_carga_calendario_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_carga_calendario.Click

        CargarArchCSV()

    End Sub

    Private Sub CargarArchCSV()

        lbl_estatus.Text = ""

        If fud_carga_calendario.PostedFile.FileName = "" Then
            lbl_estatus.Text = "Error: Selecciona un archivo."
            Exit Sub
        End If

        If Right(IO.Path.GetFileName(fud_carga_calendario.PostedFile.FileName), 3).ToUpper <> "CSV" Then
            lbl_estatus.Text = "Error: Debe subir el ""layout"" en formato .csv."
            Exit Sub
        End If

        Dim dtCalendario As New DataTable
        dtCalendario.Columns.Add("QUINCENA", GetType(String))
        dtCalendario.Columns.Add("FECHA_INICIAL", GetType(String))
        dtCalendario.Columns.Add("FECHA_FINAL", GetType(String))
        dtCalendario.Columns.Add("FECHA_INICIAL_INTERESES", GetType(String))
        dtCalendario.Columns.Add("FECHA_FINAL_INTERESES", GetType(String))

        Dim contador As Integer = 0

        Using fs As IO.Stream = fud_carga_calendario.PostedFile.InputStream

            Using oRead As New IO.StreamReader(fs, Encoding.Default)

                Do While Not oRead.EndOfStream

                    Dim linea As String = oRead.ReadLine()

                    If contador >= 1 Then

                        Dim arrVar As String() = Split(linea, ",")
                        dtCalendario.Rows.Add(arrVar(0), arrVar(1), arrVar(2), arrVar(3), arrVar(4))

                    End If

                    contador += 1

                Loop

            End Using

        End Using

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("UPD_FECHAS_CALENDARIO", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("ARCHIVO_CALENDARIO", SqlDbType.Structured)
                Session("parm").Value = dtCalendario
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim sqlRead As SqlDataReader = insertCommand.ExecuteReader()
                Dim dtEstatus As New DataTable()
                dtEstatus.Load(sqlRead)

                connection.Close()

                dag_estatus_fechas.DataSource = dtEstatus
                'dag_estatus_fechas.Visible = True
                dag_estatus_fechas.DataBind()

            End Using

        Catch ex As Exception

            lbl_estatus.Text = "ERROR: Error de base de datos."

        Finally

        End Try

    End Sub

    Protected Sub btn_guardar_qna_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar_qna.Click

        UpdateQnasBase()
        CargaActual()

    End Sub


    Private Sub UpdateQnasBase()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_anio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("QNA", Session("adVarChar"), Session("adParamInput"), 10, ddl_quincena.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFOPERACIONES_QNA_BASE"
        Session("rs") = Session("cmd").Execute()

        lbl_estatusQna.Text = "Se guardó correctamente la quincena base"


        Session("Con").Close()

    End Sub




    Private Sub CargaAnios()

        ddl_anio.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        ddl_anio.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ANIO_BASE"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TEXT").Value.ToString, Session("rs").Fields("VALUE").Value.ToString)
            ddl_anio.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub


    Private Sub CargaQuincenas(anio As Integer)

        ddl_quincena.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        ddl_quincena.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, anio)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_QUINCENA_BASE"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TEXT").Value.ToString, Session("rs").Fields("VALUE").Value.ToString)
            ddl_quincena.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub ddl_anio_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_anio.SelectedIndexChanged
        CargaQuincenas(ddl_anio.SelectedItem.Value.ToString)
    End Sub

    Protected Sub ddl_quincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_quincena.SelectedIndexChanged
    End Sub

End Class