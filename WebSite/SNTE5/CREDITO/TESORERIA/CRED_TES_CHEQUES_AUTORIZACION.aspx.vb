Public Class CRED_TES_CHEQUES_AUTORIZACION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Autorización de Cheques", "Autorización Cheques en Firme")

        If Not Me.IsPostBack Then

            llena_aut()
        End If
    End Sub

    Private Sub llena_aut()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_aut As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_AUTORIZACION_CHEQUES_PENDIENTES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_aut, Session("rs"))
        Session("Con").Close()
        dag_aut_cheques.Visible = True
        dag_aut_cheques.DataSource = dt_aut
        dag_aut_cheques.DataBind()
    End Sub

    Private Sub llena_cheques_x_autorizar(id_aut As Integer)
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_cheques_aut As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_AUT", Session("adVarChar"), Session("adParamInput"), 11, id_aut)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES_X_AUTORIZAR"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_cheques_aut, Session("rs"))
        Session("Con").Close()
        dag_cheques_aut.Visible = True
        dag_cheques_aut.DataSource = dt_cheques_aut
        dag_cheques_aut.DataBind()
    End Sub

    Private Sub dag_aut_cheques_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_aut_cheques.ItemCommand
        llena_cheques_x_autorizar(CInt(e.Item.Cells(0).Text))
        dag_aut_cheques.Visible = False
        lbl_subtitulo.Text = "Ingrese datos del usuario que autoriza la operación."
        pnl_Dictamen.Visible = True
        Session("ID_AUT") = e.Item.Cells(0).Text
        Session("FOLIO") = e.Item.Cells(1).Text
        lbl_NumAuto.Text = "Número de Autorización: " + e.Item.Cells(0).Text
        lbl_Folio.Text = "Expediente: " + e.Item.Cells(1).Text
        lbl_Persona.Text = "Cliente: " + e.Item.Cells(2).Text
        lbl_TipoOpe.Text = "Tipo de Operación: " + e.Item.Cells(3).Text
        lbl_Sucursal.Text = "Sucursal: " + e.Item.Cells(4).Text
        lbl_Usuario2.Text = "Usuario: " + e.Item.Cells(5).Text
        lbl_InfoAutoriza.Text = ""
        btn_AutorPLD_AUTO.Enabled = True
        btn_AutorPLD_CAN.Enabled = True
    End Sub

    Function ver_aut_remota_cheques() As Boolean
        Dim res As Boolean
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_AUT", Session("adVarChar"), Session("adParamInput"), 11, Session("ID_AUT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_AUTORIZACION_CHEQUE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            res = IIf(Session("rs").fields("RES").value = 1, True, False)
            If res Then
                txt_Mtv.Text = Session("rs").fields("RAZON").value.ToString
                cmb_Acc.SelectedValue = Session("rs").fields("ESTATUS").value.ToString
                lbl_InfoAutoriza.Text = "Se ha realizado la autorización de cheques remotamente."

                txt_Usr.Enabled = False
                txt_Pwd.Enabled = False
                txt_Mtv.Enabled = False
                cmb_Acc.Enabled = False
                btn_AutorPLD_AUTO.Enabled = False
                btn_AutorPLD_CAN.Enabled = False
            Else
                txt_Usr.Enabled = True
                txt_Pwd.Enabled = True
                txt_Mtv.Enabled = True
                cmb_Acc.Enabled = True
                btn_AutorPLD_AUTO.Enabled = True
                btn_AutorPLD_CAN.Enabled = True
            End If
        End If
        Session("Con").Close()

        Return res
    End Function

    Function val_user_aut_cheques() As String
        Dim res As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("USER", Session("adVarChar"), Session("adParamInput"), 20, txt_Usr.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PASSWORD", Session("adVarChar"), Session("adParamInput"), 20, txt_Pwd.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_USUARIO_AUTORIZACION_CHEQUE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            res = Session("rs").fields("MENSAJE").value
            If res = "OK" Then
                Session("AUTOR_USR_CHEQUES") = Session("rs").fields("ID_USER").value
            End If
        End If
        Session("Con").Close()
        Return res
    End Function

    Private Sub autoriza_recepcion_cheques(estatus As String, autor_user As Integer, razon As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_AUT", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_AUT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AUTOR_ID_USER", Session("adVarChar"), Session("adParamInput"), 11, autor_user)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 1000, razon)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_AUTORIZACION_CHEQUES"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Protected Sub btn_AutorPLD_AUTO_Click(sender As Object, e As EventArgs) Handles btn_AutorPLD_AUTO.Click
        Dim valida As String
        If ver_aut_remota_cheques() = False Then
            valida = val_user_aut_cheques()
            If Len(txt_Mtv.Text) > 1000 Then
                lbl_InfoAutoriza.Text = "Error: El campo de motivo o razón debe ser menor o igual a 1000 caracteres."
            Else
                If valida = "OK" Then
                    autoriza_recepcion_cheques(cmb_Acc.SelectedItem.Value, Session("AUTOR_USR_CHEQUES"), txt_Mtv.Text)
                    Session("ID_AUT") = Nothing
                    Session("AUTOR_USR_CHEQUES") = Nothing
                    cmb_Acc.ClearSelection()
                    cmb_Acc.Items.FindByValue("-1").Selected = True
                    txt_Usr.Text = ""
                    txt_Pwd.Text = ""
                    txt_Mtv.Text = ""
                    btn_AutorPLD_AUTO.Enabled = False
                    btn_AutorPLD_CAN.Enabled = False
                    lbl_InfoAutoriza.Text = "Dictamen aplicado.<br /><br />"
                Else
                    lbl_InfoAutoriza.Text = valida
                End If
            End If
        End If
    End Sub

    Protected Sub btn_AutorPLD_CAN_Click(sender As Object, e As EventArgs) Handles btn_AutorPLD_CAN.Click
        If ver_aut_remota_cheques() = False Then
            autoriza_recepcion_cheques("CANCELADO", -1, "")
            Session("ID_AUT") = Nothing
            Session("AUTOR_USR_CHEQUES") = Nothing
            pnl_Dictamen.Visible = False
            dag_cheques_aut.Visible = False
            llena_aut()
        End If
    End Sub

End Class