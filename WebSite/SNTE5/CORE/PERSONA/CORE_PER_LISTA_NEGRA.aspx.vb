Public Class CORE_PER_LISTA_NEGRA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Master, MasterMascore).CargaASPX("Lista Negra de Agremiados", "Lista Negra de Agremiados")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            CargaEstatus()
            CargaListaNegra(1) 'Id de Estatus 1 para mostrar los Agremiados Bloqueados
            PermisosAgregarListaNegra()
        End If
    End Sub

    Private Sub PermisosAgregarListaNegra()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FACULTAD", Session("adVarChar"), Session("adParamInput"), 50, "ADDBLACKLIST")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIENE_FACULTAD"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("IDTIENEFACULTAD").value.ToString = 0 Then
            btn_nuevo_bl.Enabled = False
        Else
            btn_nuevo_bl.Enabled = True
        End If

        Session("Con").Close()

    End Sub

    Private Sub CargaEstatus()

        ddl_estatus_agremiado.Items.Clear()

        Dim bloqueado As New ListItem("BLOQUEADOS", "1")
        ddl_estatus_agremiado.Items.Add(bloqueado)

        Dim desbloqueado As New ListItem("DESBLOQUEADOS", "0")
        ddl_estatus_agremiado.Items.Add(desbloqueado)

    End Sub

    Private Sub CargaListaNegra(ByVal IdEstatus As Integer)

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtListaNegra As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, IdEstatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_BLACK_LIST"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtListaNegra, Session("rs"))
        Session("Con").Close()

        If DtListaNegra.Rows.Count > 0 Then
            dgd_lista_negra_agr.DataSource = DtListaNegra
            dgd_lista_negra_agr.DataBind()
        Else
            dgd_lista_negra_agr.DataSource = Nothing
            dgd_lista_negra_agr.DataBind()
        End If

    End Sub

    Protected Sub ddl_estatus_agremiado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_estatus_agremiado.SelectedIndexChanged

        InformacionListaNegra(ddl_estatus_agremiado.SelectedItem.Text)
        CargaListaNegra(ddl_estatus_agremiado.SelectedItem.Value)

    End Sub

    Private Sub InformacionListaNegra(ByVal Tipo As String)

        If Tipo = "BLOQUEADOS" Then

            dgd_lista_negra_agr.Columns(6).Visible = False
            dgd_lista_negra_agr.Columns(7).Visible = False

        Else

            dgd_lista_negra_agr.Columns(6).Visible = True
            dgd_lista_negra_agr.Columns(7).Visible = True

        End If

    End Sub

    Protected Sub btn_nuevo_bl_Click(sender As Object, e As EventArgs) Handles btn_nuevo_bl.Click

        Session("ID_BL_AGREMIADO") = "0"
        Session("ID_ESTATUS_BL") = "0"
        Response.Redirect("CORE_PER_LISTA_NEGRA_AGREMIADO.aspx")

    End Sub

    Protected Sub dgd_lista_negra_agr_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles dgd_lista_negra_agr.ItemCommand
        If (e.CommandName = "EDITAR") Then
            Session("ID_BL_AGREMIADO") = e.Item.Cells(0).Text
            Session("ID_ESTATUS_BL") = e.Item.Cells(9).Text
            Response.Redirect("CORE_PER_LISTA_NEGRA_AGREMIADO.aspx")
        End If
    End Sub

End Class