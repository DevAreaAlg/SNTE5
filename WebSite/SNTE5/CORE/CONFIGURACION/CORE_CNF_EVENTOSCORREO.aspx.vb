Imports System.Data
Imports System.Data.SqlClient
Public Class CORE_CNF_EVENTOSCORREO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Eventos de Correo", "Eventos de Correo")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            CargaEventos()
        End If

    End Sub

    Private Sub CargaEventos()

        ddl_eventos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        ddl_eventos.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EVENTOS_ENVCORREO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCEVENTO").Value.ToString, Session("rs").Fields("IDEVENTO").Value.ToString)
            ddl_eventos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub ddl_eventos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_eventos.SelectedIndexChanged
        If ddl_eventos.SelectedItem.Value.ToString <> "0" Then
            pnl_usuarios.Visible = True
            CargaUsuarios(ddl_eventos.SelectedItem.Value.ToString)
            lbl_estatus_activo.Text = ""
        Else
            pnl_usuarios.Visible = False
        End If
    End Sub

    Private Sub CargaUsuarios(ByVal CveEvento As String)

        Dim custDA As New Data.OleDb.OleDbDataAdapter()
        Dim Usuarios As New Data.DataTable()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDEVENTO", Session("adVarChar"), Session("adParamInput"), 20, CveEvento)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EVENTOS_CORREO_USUARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(Usuarios, Session("rs"))
        Session("Con").Close()

        If Usuarios.Rows.Count > 0 Then
            dag_usuarios.Visible = True
            dag_usuarios.DataSource = Usuarios
            dag_usuarios.DataBind()
        Else
            dag_usuarios.Visible = False
        End If

    End Sub

    Protected Sub btn_asignar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_asignar.Click

        'Data table que se llena con los contenidos de los datagrids
        Dim dtUsers As New DataTable()
        dtUsers.Columns.Add("ID", GetType(Integer))
        dtUsers.Columns.Add("USER", GetType(String))
        dtUsers.Columns.Add("ESTATUS", GetType(Integer))

        For i As Integer = 0 To dag_usuarios.Rows.Count() - 1
            dtUsers.Rows.Add(CInt(dag_usuarios.Rows(i).Cells(0).Text), dag_usuarios.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_usuarios.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_EVENTO_CORREO_USUARIO", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDEVENTO", SqlDbType.Int)
                Session("parm").Value = ddl_eventos.SelectedItem.Value.ToString
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("USERS", SqlDbType.Structured)
                Session("parm").Value = dtUsers
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()
                lbl_estatus_activo.Text = "Usuarios guardados correctamente"
            End Using
        Catch ex As Exception
            lbl_estatus_activo.Text = "Error en la aplicación"
        Finally
            CargaUsuarios(ddl_eventos.SelectedItem.Value)
        End Try

    End Sub


End Class