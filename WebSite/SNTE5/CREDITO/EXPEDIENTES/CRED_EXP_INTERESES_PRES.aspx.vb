Public Class CRED_EXP_INTERESES_PRES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Intereses por Préstamos", "Intereses por Préstamos")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            CargaCiclos()
        End If

    End Sub

#Region "Carga Inicial"

    Private Sub CargaInteresesPrestamo(ByVal anio As Integer)

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtInteresesPrestamo As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CICLO", Session("adVarChar"), Session("adParamInput"), 10, anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INTERESES_X_DISPERSAR"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtInteresesPrestamo, Session("rs"))
        Session("Con").Close()

        If DtInteresesPrestamo.Rows.Count > 0 Then
            dgd_intereses_prestamo.DataSource = DtInteresesPrestamo
            dgd_intereses_prestamo.DataBind()
        Else
            dgd_intereses_prestamo.DataSource = Nothing
            dgd_intereses_prestamo.DataBind()
        End If

    End Sub

#End Region

#Region "Eventos"

    Protected Sub dgd_intereses_prestamo_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles dgd_intereses_prestamo.ItemCommand

        lbl_estatus_intereses.Text = ""

        If (e.CommandName = "DISPERSAR") Then
            If e.Item.Cells(7).Text <> 2 Then 'Id de Estatus de CONFIRMADOS que impide que se vuelvan a DISPERSAR nuevamente.
                Dim Resultado As String = ValidaAportacionesPrestamos(e.Item.Cells(1).Text, e.Item.Cells(0).Text)
                If Resultado = "OK" Then
                    Dim ani As Integer = cmb_ciclo.SelectedItem.Value.ToString
                    Session("cmd") = New ADODB.Command()
                    Session("Con").Open()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 20, ani)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").CommandText = "SEL_VALIDA_CICLO"
                    Session("rs") = Session("cmd").Execute()

                    Dim Existe As Integer = Session("rs").fields("BANDERA").value.ToString

                    Session("Con").Close()
                    If Existe = 1 Then
                        DispersaRendimientos(e.Item.Cells(0).Text, e.Item.Cells(1).Text, e.Item.Cells(2).Text)
                        lbl_estatus_intereses.Text = "Éxito: Los intereses de préstamo han sido DISPERADOS correctamente."
                        CargaInteresesPrestamo(ani)
                    Else
                        lbl_estatus_intereses.Text = "Error: No se pueden dispersar rendimientos de ciclos vencidos "
                    End If
                Else
                        lbl_estatus_intereses.Text = "Error: Los intereses de préstamo no se pueden DISPERSAR, no cuenta con layouts " + Resultado
                End If
            Else
                lbl_estatus_intereses.Text = "Error: Los intereses ya han sido CONFIRMADOS."
            End If
        End If

        If (e.CommandName = "CONFIRMAR") Then
            If e.Item.Cells(7).Text = 3 Then 'Id de Estatus de DISPERSADOS que pueden ser CONFIRMADOS.
                lbl_anio.Text = e.Item.Cells(0).Text
                lbl_quincena.Text = e.Item.Cells(1).Text
                lbl_id_prod.Text = e.Item.Cells(2).Text
                lbl_intereses.Text = "Intereses a confirmar " + e.Item.Cells(4).Text
                lbl_dispersion.Text = "Intereses para dispersión de " + e.Item.Cells(5).Text
                lbl_fondo.Text = "Intereses para el fondo de contingencia de " + e.Item.Cells(6).Text
                lbl_periodo.Text = "Periodo: Quincena " + e.Item.Cells(1).Text + " del " + e.Item.Cells(0).Text
                pnl_modal_confirmar.Visible = True
                modal_confirmar.Show()
            ElseIf e.Item.Cells(7).Text = 1 Then
                lbl_estatus_intereses.Text = "Error: Los intereses aún no han sido DISPERSADOS."
            Else
                lbl_estatus_intereses.Text = "Error: Los intereses ya han sido CONFIRMADOS."
            End If
        End If

        If (e.CommandName = "REPORTE") Then
            If e.Item.Cells(7).Text <> 1 Then 'Id de Estatus de CONFIRMADOS que impide que se vuelvan a DISPERSAR nuevamente.
                GeneraReporte(e.Item.Cells(0).Text, e.Item.Cells(1).Text, e.Item.Cells(2).Text)
            Else
                lbl_estatus_intereses.Text = "Error: Los intereses aún no han sido DISPERSADOS."
            End If
        End If

    End Sub

    Protected Sub btn_confirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_confirmar.Click

        Dim Resultado As String = ValidaDispersionInteresesPrestamo(lbl_anio.Text, lbl_quincena.Text, lbl_id_prod.Text)
        If Resultado = "OK" Then
            Dim ani As Integer = cmb_ciclo.SelectedItem.Value.ToString

            ConfirmaRendimientos(lbl_anio.Text, lbl_quincena.Text, lbl_id_prod.Text)
            lbl_estatus_intereses.Text = "Éxito: Los intereses de préstamo han sido CONFIRMADOS correctamente."
            CargaInteresesPrestamo(ani)
        Else
            lbl_estatus_intereses.Text = "Error: No se ha DISPERSADO los intereses por préstamo."
        End If

    End Sub

#End Region

#Region "Rendimientos"

    Private Function ValidaAportacionesPrestamos(ByVal QnaPrestamos As Integer, ByVal AnioPrestamos As Integer) As String

        Dim Resultado As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("QNA_PRESTAMOS", Session("adVarChar"), Session("adParamInput"), 10, QnaPrestamos)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANIO_PRESTAMOS", Session("adVarChar"), Session("adParamInput"), 10, AnioPrestamos)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_APORTACION_X_PRESTAMOS"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESULTADO").value.ToString
        Session("Con").Close()

        Return Resultado

    End Function

    Private Function ValidaDispersionInteresesPrestamo(ByVal AnioPrestamos As Integer, ByVal QnaPrestamos As Integer, ByVal IdProdPrestamos As Integer) As String

        Dim Resultado As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, AnioPrestamos)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("QNA_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, QnaPrestamos)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_PROD_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, IdProdPrestamos)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_DISPERSION_X_PRESTAMO"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESPUESTA").value.ToString
        Session("Con").Close()

        Return Resultado

    End Function

    Private Sub DispersaRendimientos(ByVal AnioIntereses As Integer, ByVal QnaIntereses As Integer, ByVal IdProducto As Integer)

        Session("Con").ConnectionTimeout = 5000
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandTimeout = 1000000
        Session("parm") = Session("cmd").CreateParameter("ANIO_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, AnioIntereses)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("QNA_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, QnaIntereses)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_PROD_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, IdProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_RENDIMIENTOS_PRESTAMO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_estatus_intereses.Text = "Éxito: Se DISPERSARON correctamente los intereses."

    End Sub

    Private Sub ConfirmaRendimientos(ByVal AnioIntereses As Integer, ByVal QnaIntereses As Integer, ByVal IdProducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, AnioIntereses)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("QNA_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, QnaIntereses)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_PROD_INT_PRESTAMO", Session("adVarChar"), Session("adParamInput"), 10, IdProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CONFIRMA_RENDIMIENTOS_PRESTAMO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_estatus_intereses.Text = "Éxito: Se CONFIRMARON correctamente los intereses."

    End Sub

#End Region

    Private Sub GeneraReporte(Anio As Integer, Qna As Integer, IdProd As Integer)

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim dteExcelmovimientos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, Anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("QNA", Session("adVarChar"), Session("adParamInput"), 10, Qna)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, IdProd)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_RENDIMIENTOS_INT_PREST"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteExcelmovimientos, Session("rs"))
        ViewState("dtmovimientos") = dteExcelmovimientos
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dteExcelmovimientos.Columns.Count - 1
            context.Response.Write(dteExcelmovimientos.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As DataRow In dteExcelmovimientos.Rows
            For i = 0 To dteExcelmovimientos.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim NombreExcel As String
        Dim FechaSis As Date = Session("FechaSis")

        NombreExcel = "Reporte Rendimientos " + " " + FechaSis.ToString("dd-MM-yyyy")

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreExcel.ToString + ".csv")
        context.Response.End()

    End Sub

    Private Sub CargaCiclos()


        cmb_ciclo.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CICLOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TEXT").Value.ToString, Session("rs").Fields("VALUE").Value.ToString)
            cmb_ciclo.Items.Add(item)
            Session("rs").movenext()
        Loop



        Session("Con").Close()
    End Sub

    Private Sub cmb_ciclo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ciclo.SelectedIndexChanged
        Dim ani As Integer = cmb_ciclo.SelectedItem.Value.ToString
        CargaInteresesPrestamo(ani)
    End Sub
End Class