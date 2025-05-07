Public Class CORE_NOT_NOTIFICACIONES_ADM
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Notificaciones", "Notificaciones")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            llenaRoles()
            'cargo todas las notificaciones
            cargarNotif1()
        End If
        If Session("ID_CATNOT") Then
            Session.Remove("ID_CATNOT")
        End If

    End Sub
    Private Sub llenaRoles()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ROLES"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dteq, Session("rs"))

        For Each row As Data.DataRow In dteq.Rows
            If row("ESTATUS") = "ACTIVO" Then

                Dim item As New ListItem(row("NOMBRE"), row("ROLID"))
                ddl_rol.Items.Add(item)

            End If


        Next

        Session("Con").Close()
    End Sub

#Region "Carga de Notificaciones"

    Private Sub cargarNotif1()



        Dim rols = ddl_rol.SelectedItem.Text
        If rols Like "ELIJA" Then
            rols = ""
        End If
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ROL", Session("adVarChar"), Session("adParamInput"), 100, rols)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, ddl_estatus.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CATNOTIFICACIONES"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_catnot.Visible = True
            dag_catnot.DataSource = dteq
            dag_catnot.DataBind()

        Else
            dag_catnot.Visible = False
            lbl_estatus.Text = "No hay resultados para la búsqueda"
        End If








    End Sub


    Private Sub cargarNotif()

        If txt_nombre.Text = "" And ddl_estatus.SelectedValue = "-1" And ddl_estatus.SelectedValue = "-1" Then
            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"
        Else
            Dim rols = ddl_rol.SelectedItem.Text
            If rols Like "ELIJA" Then
                rols = ""
            End If
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim dteq As New Data.DataTable()

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ROL", Session("adVarChar"), Session("adParamInput"), 100, rols)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, ddl_estatus.SelectedValue)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_CATNOTIFICACIONES"
            Session("rs") = Session("cmd").Execute()

            custDA.Fill(dteq, Session("rs"))
            Session("Con").Close()

            If dteq.Rows.Count > 0 Then
                dag_catnot.Visible = True
                dag_catnot.DataSource = dteq
                dag_catnot.DataBind()

            Else
                dag_catnot.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
        End If










    End Sub

    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click
        txt_nombre.Text = ""

        llenaRoles()
        ddl_estatus.Items.Clear()
        Dim elijaEst As New ListItem("ELIJA", "-1")
        ddl_estatus.Items.Add(elijaEst)
        Dim elijaEst1 As New ListItem("INACTIVO", "0")
        ddl_estatus.Items.Add(elijaEst1)
        Dim elijaEst2 As New ListItem("ACTIVO", "1")
        ddl_estatus.Items.Add(elijaEst2)
        lbl_estatus.Text = ""
        cargarNotif1()


    End Sub





#End Region
#Region "evento click buscar"
    Sub btn_bucar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_bucar.Click
        cargarNotif()
    End Sub
#End Region
#Region "evento click editar"
    Protected Sub dag_catnot_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_catnot.ItemCommand
        If (e.CommandName = "EDITAR") Then
            Session("ID_CATNOT") = e.Item.Cells(0).Text
            Response.Redirect("CORE_NOT_CONFIGURACION.aspx")
        End If
    End Sub
#End Region

End Class