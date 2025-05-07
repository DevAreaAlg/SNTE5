Public Class CORE_OPE_DIVISAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            txt_FechaFiltro.Text = ""
            LlenaDivisas()
            LlenaDivisasValor(0)
            LlenaDivisasValorDiario()

        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración de Divisas", "Divisas")

    End Sub

    Private Sub LlenaDivisas()

        cmb_Divisas.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_Divisas.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DIVISAS_ASIGNA_VALOR"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("IDDIVISA").Value.ToString)
            cmb_Divisas.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub LlenaDivisasValorDiario()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtDivisaValor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DIVISAS_VALOR_DIARIO"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtDivisaValor, Session("rs"))
        'se vacian los expedientes al formulario
        dag_divisas0.DataSource = dtDivisaValor
        dag_divisas0.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub LlenaDivisasValor(ByVal Filtro As Integer)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtDivisaValor As New Data.DataTable()

        If Filtro = 1 Then
            dag_divisas.CurrentPageIndex = 0
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHAFILTRO", Session("adVarChar"), Session("adParamInput"), 20, txt_FechaFiltro.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DIVISAS_VALOR"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtDivisaValor, Session("rs"))
        'se vacian los expedientes al formulario
        dag_divisas.DataSource = dtDivisaValor
        dag_divisas.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub btn_Asignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Asignar.Click

        AsignaValorDivisa()
        LlenaDivisasValor(0)
        LlenaDivisasValorDiario()

    End Sub

    Private Sub AsignaValorDivisa()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDIVISA", Session("adVarChar"), Session("adParamInput"), 20, cmb_Divisas.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 20, txt_ValorMXP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_DIVISAS_VALOR"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Protected Sub lnk_FechaFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_FechaFiltro.Click

        LlenaDivisasValor(1)
        LlenaDivisasValorDiario()

    End Sub

    Protected Sub lnk_CancelaFechaFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_CancelaFechaFiltro.Click

        txt_FechaFiltro.Text = ""
        LlenaDivisasValor(1)
        LlenaDivisasValorDiario()

    End Sub

    Protected Sub dag_divisas_pageIndexChanged(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs) Handles dag_divisas.PageIndexChanged

        dag_divisas.CurrentPageIndex = e.NewPageIndex
        LlenaDivisasValor(0)

    End Sub

    Protected Sub btn_EXCEL_Click() Handles btn_EXCEL.Click

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtDivisas As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHAFILTRO", Session("adVarChar"), Session("adParamInput"), 20, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DIVISAS_VALOR"
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
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Divisas" + ".csv")
        context.Response.End()

    End Sub

End Class