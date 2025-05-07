Public Class COB_BUSQUEDA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            LlenaPendientes()
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Abogados", "ABOGADOS")
    End Sub


    '--------------------------------PENDIENTES-------------------

    Private Sub Elimina_Prospecto(ByVal despachoid As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_COB_DESP_BAJA_PF"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub
    Private Sub dag_pendientes_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dag_pendientes.ItemCommand
        Session("DESPACHOID") = CInt(e.Item.Cells(0).Text)
        Session("IDDESPACHO_RESUMEN") = Session("DESPACHOID")
        If (e.CommandName = "ELIMINAR") Then
            Elimina_Prospecto(Session("DESPACHOID"))
            Session("DESPACHOID") = Nothing
            LlenaPendientes()
        End If

        If (e.CommandName = "EDITAR") Then
            Session("DESPACHOID") = e.Item.Cells(0).Text
            Response.Redirect("COB_DESP_PERSONAF.aspx")
        End If
        If (e.CommandName = "RESUMEN") Then
            Session("DESPACHOID") = e.Item.Cells(0).Text
            Response.Redirect("COB_DESP_RESUMEN.aspx")
        End If
    End Sub

    Private Sub btn_nuevo_Click(sender As Object, e As EventArgs) Handles btn_nuevo.Click
        Session("DESPACHOID") = "-1"
        Response.Redirect("COB_DESP_PERSONAF.aspx")
    End Sub

    Private Sub btn_busca_Click(sender As Object, e As EventArgs) Handles btn_busca.Click
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtper As New Data.DataTable()
        If txt_id.Text = "" And txt_nombre.Text = "" And txt_nombre.Text = "" Then
            dag_pendientes.Visible = False
            lbl_estatus.Text = "Error: debe ingresar al menos un parámetro de búsqueda"
        Else
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 10, txt_id.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FILTRO_ABOGADO"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dtper, Session("rs"))
            Session("Con").Close()
            If dtper.Rows.Count > 0 Then
                dag_pendientes.Visible = True
                dag_pendientes.DataSource = dtper
                dag_pendientes.DataBind()
                lbl_estatus.Text = ""
            Else
                dag_pendientes.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
        End If
    End Sub
    Private Sub LlenaPendientes()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPendientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()

        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 1, "F")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_PENDIENTES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPendientes, Session("rs"))
        Session("Con").Close()
        If dtPendientes.Rows.Count > 0 Then
            dag_pendientes.Visible = True
            dag_pendientes.DataSource = dtPendientes
            dag_pendientes.DataBind()
        Else
            dag_pendientes.Visible = False
        End If
    End Sub
    Private Sub btn_elimina_filtro_Click(sender As Object, e As EventArgs) Handles btn_elimina_filtro.Click
        lbl_estatus.Text = ""
        txt_id.Text = ""
        txt_nombre.Text = ""
        LlenaPendientes()
    End Sub

End Class