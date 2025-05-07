Public Class COB_PROM_PAGO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Promesa Pago", "PROMESA PAGO")
        If Not Me.IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If

            EstatusPromesa()
            lbl_user_estatus.Text = Session("NombreUsr")


        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
        End If
    End Sub
#Region "Estatus Promesa"
    Private Sub EstatusPromesa()

        cmb_estatus_prom.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_estatus_prom.Items.Add(elija)
        Dim item As New ListItem("CUMPLIDA", "CUMPLIDA")
        cmb_estatus_prom.Items.Add(item)
        Dim item2 As New ListItem("NO CUMPLIDA", "NO CUMPLIDA")
        cmb_estatus_prom.Items.Add(item2)


    End Sub
#End Region

#Region "Seleccionar Cliente y Folio"
    Private Sub lnk_seleccionar_Click(sender As Object, e As EventArgs) Handles lnk_seleccionar.Click

        Session("PERSONAID") = CInt(txt_cliente.Text)
        LlenaFoliosDespacho()



    End Sub
    Private Sub LlenaFoliosDespacho()




        cmb_folio.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_folio.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_cliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_INFO_FOLIOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            If Session("rs").Fields("COND").value.ToString = "0" Then

                cmb_folio.Enabled = False
                lbl_infoc.Text = "El cliente no cuenta con expedientes activos o liquidados"
                Session("Con").Close()

                Exit Sub
            End If

            If Session("rs").Fields("NOMBRE").value.ToString = "-1" Then
                lbl_infoc.Text = "El número de cliente introducido no existe"
                cmb_folio.Enabled = False
                Session("Con").Close()
                Exit Sub
            End If

            ' lbl_clienteB.Text = "" + Session("rs").Fields("NOMBRE").Value.value.ToString
            cmb_folio.Enabled = True
            lbl_infoc.Text = ""

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("FOLIO").value.ToString, Session("rs").Fields("FOLIO").value.ToString)
                cmb_folio.Items.Add(item)
                Session("rs").movenext()
            Loop
        Else
            cmb_folio.Enabled = False
            lbl_info.Text = "El cliente no cuenta con expedientes activos o liquidados"


        End If

        Session("Con").Close()
    End Sub

    Private Sub cmb_folio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_folio.SelectedIndexChanged
        pnl_amortizaciones.Visible = False

        If cmb_folio.SelectedIndex = "0" Then
            pnl_promesas.Visible = False
        Else
            'pnl_promesas.Visible = True
            Dim Folio = cmb_folio.SelectedItem.Value
            lbl_info.Text = ""
            lbl_infoc.Text = ""
            lbl_status.Text = ""

            If Folio <> 0 Then
                Session("FOLIO") = Folio
                Promesas()
            Else
                Session("FOLIO") = Nothing

            End If

        End If

    End Sub
    Private Sub Promesas()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtpromesa As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_PROM_PAGO"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtpromesa, Session("rs"))
        If dtpromesa.Rows.Count > 0 Then
            pnl_promesas.Visible = True
            dag_promesa.Visible = True
            dag_promesa.DataSource = dtpromesa
            dag_promesa.DataBind()
        Else
            pnl_promesas.Visible = False
            dag_promesa.Visible = False
            lbl_infoc.Text = "No existen promesas de pago generados para este expediente"
        End If

        Session("Con").Close()


    End Sub

    Private Sub dag_promesa_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dag_promesa.ItemCommand
        lbl_info.Text = ""
        lbl_status.Text = ""

        Session("IDPROMESA") = e.Item.Cells(0).Text
        Dim ID As Integer
        ID = e.Item.Cells(6).Text

        If (e.CommandName = "EDITAR") Then
            pnl_amortizaciones.Visible = True
            LlenaAmortizaciones()
            pnl_promesas.Visible = False
            lbl_prom.Text = e.Item.Cells(2).Text


        End If

        If (e.CommandName = "ELIMINAR") Then
            'Existe una relación
            If ID > 0 Then
                Eliminar(Session("FOLIO"), Session("IDPROMESA"), ID, Session("USERID"), Session("SESION"))
                lbl_status.Text = "Relación eliminada correctamente"
                Promesas()
            Else
                lbl_status.Text = "Error: No se tiene una relación de amortización con promesa."
            End If


        End If


    End Sub
    Private Sub LlenaAmortizaciones()
        Dim dt As New Data.DataTable
        CrearTablas()
        LlenaEventosIni(Session("FOLIO"), dt)

    End Sub
    Private Sub CrearTablas()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Session("Amortizaciones") = Nothing

        Session("Amortizaciones") = Nothing

        Dim Amortizaciones As New Data.DataTable
        Amortizaciones.Columns.Add("ID", GetType(Integer)) '0
        Amortizaciones.Columns.Add("CADENA", GetType(String)) '1
        Amortizaciones.Columns.Add("ASIGNADO", GetType(Integer))
        Session("Amortizaciones") = Amortizaciones


    End Sub
    Private Sub LlenaEventosIni(ByVal Folio As Integer, ByRef dt As Data.DataTable)
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()



        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_PROM_PAGO_AMORTIZACIONES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt, Session("rs"))
        Do While Not Session("rs").EOF
            Session("Amortizaciones").Rows.Add(Session("rs").Fields("IDTRANS").Value, Session("rs").Fields("CADENA").Value.ToString, Session("rs").Fields("ASIGNADO").Value)
            Session("rs").movenext()
        Loop
        ' 'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dt, Session("rs"))
        If dt.Rows.Count > 0 Then
            dag_Ini.Visible = True
            dag_Ini.DataSource = dt
            dag_Ini.DataBind()
        Else
            dag_Ini.Visible = False
            lbl_info.Text = "No hay amortizaciones disponibles"
        End If
        Session("Con").Close()


        LlenaEventos()

    End Sub
    Private Sub LlenaEventos()

        'lst_amortizaciones.Items.Clear()
        'Dim dt As New Data.DataTable()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim rows() As System.Data.DataRow
        rows = Session("Amortizaciones").Select()

        For Each row As System.Data.DataRow In rows
            Dim item As New ListItem(CStr(row(1)), row(0))
            '  lst_amortizaciones.Items.Add(item)

        Next

    End Sub
    Private Sub Eliminar(ByVal Folio As String, ByVal idpromesa As String, ByVal idtrans As String, ByVal userid As String, ByVal sesion As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROMESA", Session("adVarChar"), Session("adParamInput"), 10, idpromesa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTRANS", Session("adVarChar"), Session("adParamInput"), 10, idtrans)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, userid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 50, sesion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_COB_PROM_PAGO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Private Sub lnk_guardar_cancelar_Click(sender As Object, e As EventArgs) Handles lnk_guardar_cancelar.Click
        pnl_amortizaciones.Visible = False
        txt_notas_aviso.Text = ""
        cmb_estatus_prom.SelectedIndex = "0"
        pnl_promesas.Visible = True
    End Sub

    Private Sub lnk_guardar_aviso_Click(sender As Object, e As EventArgs) Handles lnk_guardar_aviso.Click
        Try
            Dim dt As New Data.DataTable
            Dim i As Integer
            Dim ID As Integer
            Dim fila As Integer
            Session("Amortizaciones") = Nothing

            Dim Amortizaciones As New Data.DataTable
            Amortizaciones.Columns.Add("ID", GetType(Integer)) '0
            Amortizaciones.Columns.Add("CADENA", GetType(String)) '1
            Amortizaciones.Columns.Add("ASIGNADO", GetType(Integer)) '1
            Session("Amortizaciones") = Amortizaciones
            'LlenaEventosIni(Session("FOLIO"), dt)
            Select Case cmb_estatus_prom.Text
                Case "CUMPLIDA"
                    ''Ciclo que permite recorrer la lista disponibles
                    For i = 0 To dag_Ini.Rows.Count - 1
                        If DirectCast(dag_Ini.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked = True Then

                            ID = CInt(dag_Ini.Rows(i).Cells(0).Text)

                            For Each dRow As GridViewRow In dag_Ini.Rows
                                If DirectCast(dRow.FindControl("chk_PagAsignado"), CheckBox).Checked = True Then

                                    fila = dRow.Cells(0).Text
                                    Guardar(Session("FOLIO"), Session("IDPROMESA"), ID, cmb_estatus_prom.SelectedItem.Value, txt_notas_aviso.Text, Session("USERID"), Session("SESION"))
                                    pnl_amortizaciones.Visible = False
                                    txt_notas_aviso.Text = ""
                                    ' cmb_estatus_prom.SelectedIndex = "0"
                                    pnl_promesas.Visible = True
                                    Promesas()
                                    LlenaEventos()
                                End If

                            Next dRow


                        Else
                            lbl_info.Text = "Error: Seleccione una amortización disponible."
                        End If
                        'cmb_estatus_prom.SelectedIndex = "0"
                    Next
                    cmb_estatus_prom.SelectedIndex = "0"
                Case "NO CUMPLIDA"

                    ' For i = 0 To dag_Ini.Rows.Count - 1
                    If dag_Ini.Rows.Count = 0 And dag_Ini.Visible = False Then

                        Guardar(Session("FOLIO"), Session("IDPROMESA"), ID, cmb_estatus_prom.SelectedItem.Value, txt_notas_aviso.Text, Session("USERID"), Session("SESION"))
                        pnl_amortizaciones.Visible = False
                        txt_notas_aviso.Text = ""
                        ' cmb_estatus_prom.SelectedIndex = "0"
                        pnl_promesas.Visible = True
                        Promesas()
                        'Next
                        LlenaEventos()



                    Else
                        If dag_Ini.Rows.Count < 0 And dag_Ini.Visible = True Then

                            Guardar(Session("FOLIO"), Session("IDPROMESA"), ID, cmb_estatus_prom.SelectedItem.Value, txt_notas_aviso.Text, Session("USERID"), Session("SESION"))
                            pnl_amortizaciones.Visible = False
                            txt_notas_aviso.Text = ""

                            pnl_promesas.Visible = True
                            Promesas()
                            'Next
                            LlenaEventos()


                        End If
                    End If
                    If dag_Ini.Rows.Count > 0 And dag_Ini.Visible = True Then
                        If Not DirectCast(dag_Ini.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked = True Then
                            For i = 0 To dag_Ini.Rows.Count - 1
                                Guardar(Session("FOLIO"), Session("IDPROMESA"), ID, cmb_estatus_prom.SelectedItem.Value, txt_notas_aviso.Text, Session("USERID"), Session("SESION"))
                                pnl_amortizaciones.Visible = False
                                txt_notas_aviso.Text = ""
                                ' cmb_estatus_prom.SelectedIndex = "0"
                                pnl_promesas.Visible = True
                                Promesas()
                                'Next
                                LlenaEventos()

                            Next
                        Else
                            If DirectCast(dag_Ini.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked = True Then

                                lbl_info.Text = "Error: No puede elegir amortizaciones en una promesa de pago no cumplida."

                            End If

                        End If



                    End If
                    cmb_estatus_prom.SelectedIndex = "0"

            End Select

        Catch ex As Exception
            lbl_info.Text = ex.ToString()
        End Try

    End Sub

    Private Sub Guardar(ByVal Folio As String, ByVal idpromesa As String, ByVal idtrans As String, ByVal estatus As String, ByVal notas As String, ByVal userid As String, ByVal sesion As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROMESA", Session("adVarChar"), Session("adParamInput"), 10, idpromesa)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTRANS", Session("adVarChar"), Session("adParamInput"), 10, idtrans)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 100, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 3000, notas)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, userid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 50, sesion)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COB_PROM_PAGO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()


    End Sub

    Private Sub lnk_guardar_aviso_Load(sender As Object, e As EventArgs) Handles lnk_guardar_aviso.Load

    End Sub

    Private Sub cmb_estatus_prom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_estatus_prom.SelectedIndexChanged
        If dag_Ini.Visible = False And cmb_estatus_prom.Text = "CUMPLIDA" Then
            lbl_info.Text = "No puede elegir estatus cumplida"
            lnk_guardar_aviso.Visible = False
            lnk_guardar_cancelar.Visible = False
        Else
            lnk_guardar_aviso.Visible = True
            lnk_guardar_cancelar.Visible = True
            lbl_info.Text = ""

        End If
    End Sub
#End Region

End Class