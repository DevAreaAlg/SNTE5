Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO10
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Asignación del Producto", "Asignación del Producto a Oficinas")

        If Not Me.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                llena_sursales()
            End If
        End If
    End Sub

    Protected Sub llena_sursales()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtSucursalesAsignados As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 50, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SUCURSALES_PRODUCTO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtSucursalesAsignados, Session("rs"))
        Session("Con").Close()
        If dtSucursalesAsignados.Rows.Count > 0 Then
            dag_suc_prod.Visible = True
            dag_suc_prod.DataSource = dtSucursalesAsignados
            dag_suc_prod.DataBind()
        Else
            dag_suc_prod.Visible = False
        End If
    End Sub

    Protected Sub btn_guardar_r_Click(sender As Object, e As EventArgs)
        'Data table que se llena con el contenido del datagrid
        Dim dtSucursales As New Data.DataTable()
        dtSucursales.Columns.Add("ID", GetType(Integer))
        dtSucursales.Columns.Add("NOMBRE", GetType(String))
        dtSucursales.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_suc_prod.Rows.Count() - 1
            dtSucursales.Rows.Add(CInt(dag_suc_prod.Rows(i).Cells(0).Text), dag_suc_prod.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_suc_prod.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los roles a un usuario
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_SUCURSAL_PRODUC_ASIGNAR", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("ID", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("SUCURSALES", SqlDbType.Structured)
                Session("parm").Value = dtSucursales
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()

            End Using
        Catch ex As Exception
            '<span class="text_input_nice_label" style="margin-left:20px">Rol Número</span>

        Finally
            lbl_status.Text = "Guardado correctamente"
            llena_sursales()
        End Try
    End Sub

End Class