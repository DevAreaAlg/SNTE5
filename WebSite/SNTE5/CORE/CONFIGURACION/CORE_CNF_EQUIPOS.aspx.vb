Imports System.Windows.Forms

Public Class CORE_CNF_EQUIPOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Búsqueda de Equipos", "Equipos")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            'establecer sucursales disponibles de entre los equipos existentes
            llena_sucursales("cmb_suc")
            'llenar tabla equipos
            llena_eq_comp1()
        End If
        Session.Remove("EQ_MODCRE")

    End Sub



    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click

        lbl_estatus.Text = ""

        llena_sucursales("cmb_suc")

        cmb_status_eq_filter.Items.Clear()
        Dim elijaEst As New ListItem("ELIJA", "-1")
        cmb_status_eq_filter.Items.Add(elijaEst)
        Dim elijaEst1 As New ListItem("INACTIVO", "0")
        cmb_status_eq_filter.Items.Add(elijaEst1)
        Dim elijaEst2 As New ListItem("ACTIVO", "1")
        cmb_status_eq_filter.Items.Add(elijaEst2)
        llena_eq_comp1()



    End Sub
    Private Sub llena_eq_comp1()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_sucursal_busqueda.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_status_eq_filter.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EQ_COMP"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()
        If dteq.Rows.Count > 0 Then
            dag_eq.Visible = True
            dag_eq.DataSource = dteq
            dag_eq.DataBind()

        Else
            dag_eq.Visible = False
        End If
    End Sub
    Private Sub llena_eq_comp()

        If cmb_sucursal_busqueda.SelectedValue = "-1" And cmb_status_eq_filter.SelectedValue = "-1" Then
            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"

        Else
            lbl_estatus.Text = ""
            Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
            Dim dteq As New Data.DataTable()
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_sucursal_busqueda.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_status_eq_filter.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EQ_COMP"
            Session("rs") = Session("cmd").Execute()

            custDA.Fill(dteq, Session("rs"))
            Session("Con").Close()
            If dteq.Rows.Count > 0 Then
                dag_eq.Visible = True
                dag_eq.DataSource = dteq
                dag_eq.DataBind()

            Else
                dag_eq.Visible = False
                lbl_estatus.Text = "No hay resultados para la búsqueda"
            End If
        End If



    End Sub

    Private Sub llena_sucursales(ByVal i As String)
        cmb_sucursal_busqueda.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_sucursal_busqueda.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()


        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_sucursal_busqueda.Items.Add(item)

            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub btn_buscarEquipos_Click(sender As Object, e As EventArgs)
        llena_eq_comp()
    End Sub

    Protected Sub redirect_crear_eq(sender As Object, e As EventArgs)
        Dim accion As Integer = -1
        Dim equipo As Integer = -1
        Session("EQ_MODCRE") = New Integer() {accion, equipo}
        Response.Redirect("CORE_CNF_EQUIPOS_CREAR.aspx")
    End Sub

    Protected Sub dag_eq_ItemCommand(source As Object, e As DataGridCommandEventArgs)
        If e.CommandName.Equals("EDITAR") Then
            Dim accion As Integer = 1
            Dim equipo As Integer = e.Item.Cells(0).Text
            Session("EQ_MODCRE") = New Integer() {accion, equipo}
            Response.Redirect("CORE_CNF_EQUIPOS_CREAR.aspx")
        End If
    End Sub

End Class