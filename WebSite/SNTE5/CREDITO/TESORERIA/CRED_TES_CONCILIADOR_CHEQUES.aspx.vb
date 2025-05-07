Public Class CRED_TES_CONCILIADOR_CHEQUES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Conciliación Cheques", "Conciliación de Cheques")
        If Not Me.IsPostBack Then
            LLena_sucursales()
            Llena_cheques()
        End If
        'btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")
        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")
        If Not Session("idperbusca") Is Nothing Then
            txt_filtro_persona.Text = Session("idperbusca").ToString
        End If
    End Sub

    Private Sub LLena_sucursales()
        cmb_filtro_sucursal.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_filtro_sucursal.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_filtro_sucursal.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()

        'txt_filtro_persona.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_idPersona.ClientID + "','" + btn_Modificar.ClientID + "')")
    End Sub

    Private Sub Llena_cheques()
        Dim cob As Integer, sbc As Integer, cet As Integer, ces As Integer, cco As Integer, can As Integer
        Dim cliente As String

        If chk_filtro_tipo_cob.Checked = True Then
            cob = 1
        Else
            cob = 0
        End If

        If chk_filtro_tipo_sbc.Checked = True Then
            sbc = 1
        Else
            sbc = 0
        End If

        If txt_filtro_persona.Text = "" Then
            cliente = "0"
        Else
            cliente = txt_filtro_persona.Text
        End If

        If chk_filtro_estatus_cet.Checked = True Then
            cet = 1
        Else
            cet = 0
        End If

        If chk_filtro_estatus_ces.Checked = True Then
            ces = 1
        Else
            ces = 0
        End If

        If chk_filtro_estatus_cco.Checked = True Then
            cco = 1
        Else
            cco = 0
        End If
        can = 0
        'If chk_filtro_estatus_cco.Checked = True Then
        '    can = 1
        'Else
        '    can = 0
        'End If

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcheques As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, CInt(cliente))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_filtro_sucursal.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 15, txt_filtro_fecha_inicio.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 15, txt_filtro_fecha_fin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SBC", Session("adVarChar"), Session("adParamInput"), 1, sbc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("COB", Session("adVarChar"), Session("adParamInput"), 1, cob)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CET", Session("adVarChar"), Session("adParamInput"), 1, cet)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CES", Session("adVarChar"), Session("adParamInput"), 1, ces)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CEM", Session("adVarChar"), Session("adParamInput"), 1, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CCO", Session("adVarChar"), Session("adParamInput"), 1, cco)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CAN", Session("adVarChar"), Session("adParamInput"), 1, can)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CHEQUES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcheques, Session("rs"))
        grd_cheques.DataSource = dtcheques
        grd_cheques.DataBind()
        Session("Con").Close()
        txt_filtro_fecha_fin.Text = ""
        txt_filtro_fecha_inicio.Text = ""
        'txt_filtro_persona.Text = ""
    End Sub

    Private Function VerificaFecha(ByVal fecha As String) As Boolean

        Dim correcto As Boolean = True

        If DateDiff(DateInterval.Year, CDate(fecha), Now()) > 150 Or (DateAdd(DateInterval.Day, -1, Now()) <= CDate(fecha)) Then
            correcto = False
        End If

        Return correcto

    End Function

    Private Sub grd_cheques_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grd_cheques.ItemCommand
        'Thread.Sleep(2000)
        'e.Item es la fila sobre la cual dio click el cursor
        'lbl_titulo.Text = "NO ENTRO"

        If e.Item.Cells(7).Text <> "COBRADO" Then
            lbl_status.Text = "Error: No se puede conciliar un cheque que aun no esta cobrado."
        Else

            'lbl_status.Text = "idcheque " + Session("ID_CHEQUE") + " iduser " + Session("USERID") + " ideq " + Session("ID_EQ")

            Session("ID_CHEQUE") = e.Item.Cells(4).Text
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID_CHEQUE", Session("adVarChar"), Session("adParamInput"), 100, Session("ID_CHEQUE"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 11, Session("ID_EQ"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_CONCILIACION"
            Session("cmd").Execute()
            Session("Con").Close()
            Llena_cheques()
        End If

    End Sub

    Private Sub Limpiavariables()
        Session("idperbusca") = Nothing
        Session("TIPOPER") = Nothing
        Session("PROSPECTO") = Nothing
    End Sub

    Protected Sub lnk_filtro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_filtro.Click
        If txt_filtro_fecha_inicio.Text <> "" And txt_filtro_fecha_fin.Text <> "" Then
            If VerificaFecha(txt_filtro_fecha_inicio.Text) And VerificaFecha(txt_filtro_fecha_fin.Text) Then
                Llena_cheques()
            Else
                lbl_status.Text = "Error: Indique las fechas del periodo a filtrar, con fechas correctas y validas."
                Exit Sub
            End If
        Else
            txt_filtro_fecha_fin.Text = ""
            txt_filtro_fecha_inicio.Text = ""
            Llena_cheques()
        End If
    End Sub

    Protected Sub lnk_eliminar_filtro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_eliminar_filtro.Click
        lbl_status.Text = ""
        txt_filtro_persona.Text = ""
        LLena_sucursales()
        chk_filtro_tipo_cob.Checked = False
        chk_filtro_tipo_sbc.Checked = False
        chk_filtro_tipo_cob.Checked = False
        chk_filtro_estatus_cco.Checked = False
        chk_filtro_estatus_ces.Checked = False
        chk_filtro_estatus_cet.Checked = False
        txt_filtro_fecha_inicio.Text = ""
        txt_filtro_fecha_fin.Text = ""
        Llena_cheques()
    End Sub


End Class