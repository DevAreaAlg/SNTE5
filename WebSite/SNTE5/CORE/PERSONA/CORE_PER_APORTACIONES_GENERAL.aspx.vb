Imports System.Data.SqlClient

Public Class CORE_PER_APORTACIONES_GENERAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Devolución de Aportaciones", "APORTACIONES")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            LlenadoAportaciones()
        End If
    End Sub

    Public Sub LlenadoAportaciones()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt_politica As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_APORTACIONES_TOTAL"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt_politica, Session("rs"))



        Session("Con").Close()

        If dt_politica.Rows.Count > 0 Then
            dag_Aport.Visible = True
            dag_Aport.DataSource = dt_politica
            dag_Aport.DataBind()
        Else
            dag_Aport.Visible = False
        End If

    End Sub



    Protected Sub btn_guardar_r_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar_r.Click

        Dim res As String = AplicarDevoluciones()
        If res = "OK" Then
            lbl_status.Text = "Se aplicaron correctamente las aportaciones"
        Else
            lbl_status.Text = "Ah ocurrido un Error"


        End If

    End Sub

    Private Function AplicarDevoluciones() As String

        Dim result As String = "OK"
        Dim dtDevoluciones As New Data.DataTable()
        dtDevoluciones.Columns.Add("RFC", GetType(String))
        dtDevoluciones.Columns.Add("MONTO_PC", GetType(Decimal))
        dtDevoluciones.Columns.Add("TOTAL_REAL", GetType(Decimal))
        dtDevoluciones.Columns.Add("BANDERA", GetType(Integer))

        For i As Integer = 0 To dag_Aport.Rows.Count() - 1
            dtDevoluciones.Rows.Add(
              dag_Aport.Rows(i).Cells(0).Text,
              dag_Aport.Rows(i).Cells(4).Text,
              dag_Aport.Rows(i).Cells(5).Text,
              Convert.ToInt32(DirectCast(dag_Aport.Rows(i).FindControl("chk_NoAplicado"), CheckBox).Checked))
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                Dim insertCommand As New SqlCommand("INS_DEVOLUCION_APORTACIONES", connection)
                insertCommand.CommandTimeout = 1000000
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("TABLA", SqlDbType.Structured)
                Session("parm").Value = dtDevoluciones
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
            result = "ERROR: " + ex.Message()
        Finally
            LlenadoAportaciones()
        End Try

        Return result

    End Function


End Class