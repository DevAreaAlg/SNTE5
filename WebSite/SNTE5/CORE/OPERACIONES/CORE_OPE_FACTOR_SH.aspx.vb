Imports System.Data
Public Class CORE_OPE_FACTOR_SH
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Cargar Factor de Seguro Hipotecario", "Factor de Seguro Hipotecario")

        If Not IsPostBack Then
            CargaAnios()
            FactoresSH()
        End If

    End Sub

    Private Sub CargaAnios()

        ddl_anios.Items.Clear()
        Dim elija As New ListItem("ELIJA", "")
        ddl_anios.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FACTORES_SEGURO_HIPOTECARIOS_ANIOS"
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

    Private Sub FactoresSH()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtFactoresSH As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FACTORES_SEGURO_HIPOTECARIOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtFactoresSH, Session("rs"))
        Session("Con").Close()

        dag_bitacora_factores.DataSource = dtFactoresSH
        dag_bitacora_factores.DataBind()

        dtFactoresSH.Clear()

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar.Click

        If txt_factor.Text <> txt_confirmar.Text Then
            lbl_estatus.Text = "Error: Los valores del factor no coinciden."
            Exit Sub
        End If

        If ValidaFactor(txt_factor.Text) Then
            pnl_modal_confirmar.Visible = True
            modal_confirmar.Show()
        Else
            lbl_estatus.Text = "Error: Factor incorrecto."
        End If

    End Sub

    Protected Sub btn_confirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_confirmar.Click

        GuardaFactor()

    End Sub

    Private Sub GuardaFactor()

        Dim Resultado As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_anios.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FACTOR", Session("adVarChar"), Session("adParamInput"), 50, txt_factor.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_FACTORES_SEGURO_HIPOTECARIOS"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESULTADO").value.ToString
        Session("Con").Close()

        If Resultado = "OK" Then
            lbl_estatus.Text = "Se ha guardado con éxito el nuevo factor de seguro."
            LimpiarDatos()
            FactoresSH()
        ElseIf Resultado = "ANIOEXISTE" Then
            lbl_estatus.Text = "Error: Ya se registró el factor de el seguro hipotecario para dicho año."
        ElseIf Resultado = "" Then
            lbl_estatus.Text = "Error: No puede registrar un factor de seguro hipotecario para un año menor a la fecha del sistema."
        End If

    End Sub

    Private Function ValidaFactor(ByVal factor As String) As Boolean
        Return Regex.IsMatch(factor, ("^[0-9]{1}\.{1}[0-9]{8}$"))
    End Function

    Private Sub LimpiarDatos()

        txt_factor.Text = ""
        txt_confirmar.Text = ""
        ddl_anios.SelectedValue = ""

    End Sub

End Class