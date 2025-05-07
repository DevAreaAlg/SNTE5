Public Class CRED_TES_RECEPCION_EFECTIVO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Recepción Efectivo", "Recepción de Efectivo")
        If Not Me.IsPostBack Then
            Session("ENTRADASALIDA") = "ENTRADA"
            'lbl_titulo.Text = Session("ID_OP").ToString
        End If
        llena_envios()
    End Sub

    Private Sub llena_envios()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtenvios As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_RECIBE", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACIONES"
        Session("rs") = Session("cmd").Execute()
        'If Not Session("rs").EOF Then
        custDA.Fill(dtenvios, Session("rs"))
        grd_operaciones.DataSource = dtenvios
        grd_operaciones.DataBind()
        'End If
        Session("Con").Close()
    End Sub

    Private Sub Llena_cheques(ByVal idop As Integer)
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcheques As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, idop)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES_OPERACION"
        Session("rs") = Session("cmd").Execute()
        'If Not Session("rs").EOF Then
        custDA.Fill(dtcheques, Session("rs"))
        grd_info_recepcion.DataSource = dtcheques
        grd_info_recepcion.DataBind()
        'End If
        Session("Con").Close()
    End Sub

    Private Sub grd_operaciones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grd_operaciones.ItemCommand
        'Thread.Sleep(2000)
        'e.Item es la fila sobre la cual dio click el cursor
        Session("ID_OP") = CInt(e.Item.Cells(0).Text)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_OP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_DETALLE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_origen_txt.Text = Session("rs").Fields("ORIGEN").Value.ToString
            lbl_destino_txt.Text = Session("rs").Fields("DESTINO").Value.ToString
            lbl_envia_txt.Text = Session("rs").Fields("ENVIA").Value.ToString
            lbl_transporta_txt.Text = Session("rs").Fields("TRANSPORTA").Value.ToString
            lbl_recibe_txt.Text = Session("rs").Fields("RECIBE").Value.ToString
            lbl_fecha_txt.Text = Session("rs").Fields("FECHA").Value.ToString
            lbl_monto_txt.Text = "$" + Session("rs").Fields("MONTO").Value.ToString
        End If
        Session("Con").Close()
        'If Session("TIPO_OP") <> "CHEQUES" Then
        '    lbl_monto.Visible = True
        '    lbl_monto_txt.Visible = True
        '    grd_info_recepcion.Visible = False
        'Else
        '    lbl_monto_txt.Visible = False
        '    lbl_monto.Visible = False
        '    grd_info_recepcion.Visible = True
        Llena_cheques(Session("ID_OP"))
        'End If
    End Sub

    Private Sub ObtieneNuevaSerie()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CORTE_OBTIENE_SERIE"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            'Session("SERIE") = Session("rs").Fields("SERIE").Value.ToString
            Session("FOLIO_IMP") = Session("rs").Fields("FOLIO_IMP").Value.ToString
        End If
        Session("Con").Close()
    End Sub

    Private Sub aplica_recepcion()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_OP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE", Session("adVarChar"), Session("adParamInput"), 3, Session("SERIE"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP", Session("adVarChar"), Session("adParamInput"), 8, Session("FOLIO_IMP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_OPERACION"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub limpia_datos()
        llena_envios()
        lbl_origen_txt.Text = ""
        lbl_destino_txt.Text = ""
        lbl_envia_txt.Text = ""
        lbl_transporta_txt.Text = ""
        lbl_recibe_txt.Text = ""
        lbl_fecha_txt.Text = ""
        lbl_monto_txt.Text = ""
        grd_info_recepcion.Visible = False
    End Sub

    Function valida_digit() As Boolean
        Dim res As Boolean
        res = False
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_OP", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_OP"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_OPERACION_DOC_NO_DIGIT"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("EXISTEN").Value.ToString = "NO" Then
            res = True
        End If
        Session("Con").Close()
        Return res
    End Function

    Protected Sub btn_digit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_digit.Click
        If lbl_origen_txt.Text = "" Then
            lbl_status.Text = "Error: No ha seleccionado ningun envio pendiente!!!"
            Exit Sub
        Else
            lbl_status.Text = ""
            Session("VENGODE") = "~/CREDITO/TESORERIA/CRED_TES_RECEPCION_EFECTIVO.aspx"
            Response.Redirect("~/DIGITALIZADOR/DIGI_GLOBAL.aspx")
        End If
    End Sub

    Protected Sub btn_recibir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_recibir.Click
        If lbl_origen_txt.Text = "" Then
            lbl_status.Text = "Error: No ha seleccionado ningun envio pendiente!!!"
            Exit Sub
        Else
            lbl_status.Text = ""
            If valida_digit() Then
                Dim monto As Decimal = CDec(Right(lbl_monto_txt.Text, lbl_monto_txt.Text.Length - 1))
                Session("SERIE") = "ED"
                ObtieneNuevaSerie()
                aplica_recepcion()
                If monto <> 0.0 Then
                    CajaX_MAC()
                    Session("MONTO_EFECTIVO") = monto

                    ClientScript.RegisterStartupScript(GetType(String), "TiraEfectivo", "window.open(""../../CREDITO/VENTANILLA/CRED_VEN_TIRAEFECTIVO.aspx"", ""RP"", ""width=530,height=510,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=1,left=1"");", True)
                End If
                limpia_datos()
            Else
                lbl_status.Text = "Error: No ha digitalizado la constancia de envio!!!"
                Exit Sub
            End If
        End If
    End Sub

    Private Sub CajaX_MAC()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CAJAS_X_MAC"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Session("IDCAJA_USR") = Session("rs").Fields("IDCAJA").Value.ToString
        End If

        Session("Con").Close()

    End Sub

End Class