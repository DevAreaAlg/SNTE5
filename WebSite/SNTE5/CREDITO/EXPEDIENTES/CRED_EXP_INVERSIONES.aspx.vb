Public Class CRED_EXP_INVERSIONES
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Inversiones", "Inversiones")

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

    Private Sub CargaInversiones(ByVal anio As Integer)

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtInversiones As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INVERSIONES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtInversiones, Session("rs"))
        Session("Con").Close()

        If DtInversiones.Rows.Count > 0 Then
            dgd_inversiones.DataSource = DtInversiones
            dgd_inversiones.DataBind()
        Else
            dgd_inversiones.DataSource = Nothing
            dgd_inversiones.DataBind()
        End If

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar.Click
        If ValidaMonto(tbx_monto_inversion.Text) = True Then
            If ValidaTasa(tbx_tasa_inversion.Text) = True Then
                If Convert.ToDateTime(tbx_fecha_fin_inv.Text) >= Convert.ToDateTime(tbx_fecha_ini_inv.Text) Then
                    Dim anio2 As Integer = cmb_ciclo.SelectedItem.Value.ToString

                    Session("cmd") = New ADODB.Command()
                    Session("Con").Open()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 20, anio2)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").CommandText = "SEL_VALIDA_CICLO"
                    Session("rs") = Session("cmd").Execute()

                    Dim Existe As Integer = Session("rs").fields("BANDERA").value.ToString

                    Session("Con").Close()
                    If Existe = 1 Then
                        GuardarInversion(anio2)
                        CargaInversiones(anio2)
                        InteresesDevengados(anio2)
                        LimpiarCampos()
                    Else
                        lbl_estatus_inversion.Text = "Error: No se pueden guardar nuevas inversiones en ciclos vencidos "
                    End If
                Else
                    lbl_estatus_inversion.Text = "Error: La fecha de término de inversión no puede ser menor a la fecha de inicio de la inversión."
                End If
            Else
                lbl_estatus_inversion.Text = "Error: La tasa de inversión tiene formato incorrecto."
            End If
        Else
            lbl_estatus_inversion.Text = "Error: El monto de inversión tiene formato incorrecto."
        End If
    End Sub

    Private Sub GuardarInversion(ByVal anio As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_inversion.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO_INVERSION", Session("adVarChar"), Session("adParamInput"), 20, tbx_monto_inversion.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA_INVERSION", Session("adVarChar"), Session("adParamInput"), 20, tbx_tasa_inversion.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_INICIO_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, tbx_fecha_ini_inv.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_TERMINO_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, tbx_fecha_fin_inv.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CICLIO", Session("adVarChar"), Session("adParamInput"), 20, anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_INVERSION"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_estatus_inversion.Text = "Éxito: Se guardaron correctamente los datos de la inversión."

    End Sub

    Private Sub LimpiarCampos()

        tbx_monto_inversion.Text = ""
        tbx_tasa_inversion.Text = ""
        tbx_fecha_ini_inv.Text = ""
        tbx_fecha_fin_inv.Text = ""
        tbx_id_inversion.Text = "0"
        dgd_inversion_detalle.DataSource = Nothing
        dgd_inversion_detalle.DataBind()

    End Sub

    Private Function ValidaMonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]{1,17}(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Function ValidaTasa(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]{1,3}(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Sub InteresesDevengados(ByVal anio As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 10, anio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INTERESES_DEVENGADOS"
        Session("rs") = Session("cmd").Execute()
        tbx_devengados.Text = Session("rs").fields("DEVENGADOS").value.ToString
        tbx_devengados_75.Text = Session("rs").fields("DEVENGADOS_75").value.ToString
        tbx_devengados_25.Text = Session("rs").fields("DEVENGADOS_25").value.ToString
        Session("Con").Close()

    End Sub

    Protected Sub dgd_inversiones_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles dgd_inversiones.ItemCommand

        lbl_estatus_inversion.Text = ""

        If (e.CommandName = "EDITAR") Then
            If e.Item.Cells(1).Text <> 2 Then 'Id de Estatus de Inversión CONFIRMADA y no puede editarse.
                BuscarInversion(e.Item.Cells(0).Text)
            Else
                lbl_estatus_inversion.Text = "Error: La inversión esta CONFIRMADA y no se puede editar."
            End If
        End If

        If (e.CommandName = "DISPERSAR") Then
            If e.Item.Cells(1).Text <> 2 Then 'Id de Estatus de Inversión CONFIRMADA y no se puede DISPERSAR nuevamente.
                Dim Resultado As String = ValidaAportacionesInversion(e.Item.Cells(0).Text)
                If Resultado = "OK" Then
                    Dim anio3 As Integer = cmb_ciclo.SelectedItem.Value.ToString
                    AplicaDispersion(e.Item.Cells(0).Text, anio3)

                    CargaInversiones(anio3)
                    lbl_estatus_inversion.Text = "Éxito: La Inversión se ha DISPERSADO correctamente."
                Else
                    lbl_estatus_inversion.Text = "Error: La inversión no se puede DISPERSAR, no cuenta con layouts " + Resultado
                End If
            Else
                lbl_estatus_inversion.Text = "Error: La inversión ya está CONFIRMADA y no puede DISPERSARSE nuevamente."
            End If
        End If

        If (e.CommandName = "REPORTE") Then
            If e.Item.Cells(1).Text <> 1 Then 'Id de Estatus de Inversión ACTIVA que no se ha DISPERSADO.
                DescargaReporteRendimiento(e.Item.Cells(0).Text)
            Else
                lbl_estatus_inversion.Text = "Error: La inversión aún no se ha DISPERSADO."
            End If
        End If

        If (e.CommandName = "CONFIRMAR") Then
            If e.Item.Cells(1).Text = 3 Then 'Id de Estatus de Inversión DISPERSADA que puede ser CONFIRMADA.
                lbl_id_estatus.Text = e.Item.Cells(1).Text
                lbl_id_inversion.Text = e.Item.Cells(0).Text
                lbl_inversion.Text = "Inversión de " + e.Item.Cells(2).Text
                lbl_intereses.Text = "Intereses devengados de " + e.Item.Cells(8).Text
                lbl_dispersion.Text = "Intereses para dispersión de " + e.Item.Cells(9).Text
                lbl_fondo.Text = "Intereses para el fondo de contingencia de " + e.Item.Cells(10).Text
                lbl_pregunta.Text = "¿Desea confirmar la inversión ?"
                pnl_modal_confirmar.Visible = True
                modal_confirmar.Show()
            ElseIf e.Item.Cells(1).Text = 1 Then 'Id de Estatus de Inversión ACTIVA que no se puede CONFIRMAR debido a que no se ha DISPERSADO previamente.
                lbl_estatus_inversion.Text = "Error: La inversión aún no se puede CONFIRMAR, debido a que aún no se ha DISPERSADO."
            Else
                lbl_estatus_inversion.Text = "Error: La inversión ya está CONFIRMADA."
            End If
        End If

        If (e.CommandName = "DETALLE") Then
            CargaInversionDetalle(e.Item.Cells(0).Text)
        End If

    End Sub

    Protected Sub btn_confirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_confirmar.Click

        Dim Resultado As String = ValidaAportacionesInversion(lbl_id_inversion.Text)
        If Resultado = "OK" Then
            Dim ResultadoDispersion As String = ValidaDispersionesInversion(lbl_id_inversion.Text)
            If ResultadoDispersion = "OK" Then
                LiquidarInversion(lbl_id_inversion.Text)
                lbl_estatus_inversion.Text = "Éxito: La Inversión se ha LIQUIDADO."
                Dim anio4 As Integer = cmb_ciclo.SelectedItem.Value.ToString

                CargaInversiones(anio4)
            Else
                lbl_estatus_inversion.Text = "Error: No se ha DISPERSADO la inversión."
            End If
        Else
            lbl_estatus_inversion.Text = "Error: La inversión no se puede DISPERSAR, no cuenta con layouts " + Resultado
        End If

    End Sub

    Private Sub DescargaReporteRendimiento(ByVal IdInversion As Integer)

        'Try

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtMovimientosDescuentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("@ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REPORTE_DISPERSION_INVERSION"
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
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "REPORTE_RENDIMIENTO_INVERSION" + ".csv")
        context.Response.End()

        'Catch ex As Exception
        '    lbl_status.Text = ex.Message()
        'Finally
        '    LlenarExpXPagar()
        'End Try

    End Sub

    Private Function ValidaAportacionesInversion(ByVal IdInversion As Integer) As String

        Dim Resultado As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_APORTACION_X_INVERSION"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESULTADO").value.ToString
        Session("Con").Close()

        Return Resultado

    End Function

    Private Function ValidaDispersionesInversion(ByVal IdInversion As Integer) As String

        Dim Resultado As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_DISPERSION_X_INVERSION"
        Session("rs") = Session("cmd").Execute()
        Resultado = Session("rs").fields("RESPUESTA").value.ToString
        Session("Con").Close()

        Return Resultado

    End Function

    Private Sub BuscarInversion(ByVal IdInversion As Integer)

        lbl_estatus_inversion.Text = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INVERSIONES_BY_ID"
        Session("rs") = Session("cmd").Execute()
        tbx_monto_inversion.Text = Session("rs").fields("MONTO_INVERSION").value.ToString
        tbx_tasa_inversion.Text = Session("rs").fields("TASA_INVERSION").value.ToString
        tbx_fecha_ini_inv.Text = Session("rs").fields("FECHA_INICIO").value.ToString
        tbx_fecha_fin_inv.Text = Session("rs").fields("FECHA_TERMINO").value.ToString
        tbx_id_inversion.Text = Session("rs").fields("ID_INVERSION").value.ToString
        Session("Con").Close()

    End Sub

    Private Sub AplicaDispersion(ByVal IdInversion As Integer, ByVal anios As Integer)

        lbl_estatus_inversion.Text = ""

        Session("Con").ConnectionTimeout = 5000
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandTimeout = 1000000
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ANIO", Session("adVarChar"), Session("adParamInput"), 20, anios)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_APLICA_DISPERSION_INVERSION"
        Session("rs") = Session("cmd").Execute()

        Session("Con").Close()

    End Sub



    Private Sub LiquidarInversion(ByVal IdInversion As Integer)

        lbl_estatus_inversion.Text = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_INVERSION"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub CargaInversionDetalle(ByVal IdInversion As Integer)

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtInversiones As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INVERSION", Session("adVarChar"), Session("adParamInput"), 10, IdInversion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INVERSION_DETALLE"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtInversiones, Session("rs"))
        Session("Con").Close()

        If DtInversiones.Rows.Count > 0 Then
            dgd_inversion_detalle.DataSource = DtInversiones
            dgd_inversion_detalle.DataBind()
        Else
            dgd_inversion_detalle.DataSource = Nothing
            dgd_inversion_detalle.DataBind()
        End If

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
        Dim anios As Integer = cmb_ciclo.SelectedItem.Value.ToString
        lbl_estatus_inversion.Text = ""
        CargaInversiones(anios)
        InteresesDevengados(anios)
    End Sub
End Class