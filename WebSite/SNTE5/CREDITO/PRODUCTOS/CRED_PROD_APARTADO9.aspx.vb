Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO9
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Pagarés , Contratos y Solicitudes", "Configuración de Pagarés , Contratos y Solicitudes")

        If Not Page.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                LlenaPagare()
                LlenaContratos()
                llenasolicitudes()
            End If
        End If
    End Sub

    Protected Sub limpiar_sessionV(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnk As LinkButton = sender
        Dim href As String = lnk.Attributes("Redirect").ToString()
        Session.Remove("EQ_MODCRE")
        Response.Redirect(href)

    End Sub

#Region "Asignacion Pagares"

    'Muestra las garantias asignadas y disponibles
    Private Sub LlenaPagare()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim GarantiasGeneral As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_APARTADO9_PAGARES_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(GarantiasGeneral, Session("rs"))
        Session("Con").Close()

        If GarantiasGeneral.Rows.Count > 0 Then
            dag_PagAsigandos.Visible = True
            dag_PagAsigandos.DataSource = GarantiasGeneral
            dag_PagAsigandos.DataBind()
        Else
            dag_PagAsigandos.Visible = False
        End If

    End Sub

    Protected Sub btn_asignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Data table que se llena con los contenidos de los datagrids
        Dim dtPagares As New Data.DataTable()
        dtPagares.Columns.Add("IDPAGARE", GetType(Integer))
        dtPagares.Columns.Add("NOMBRE", GetType(String))
        dtPagares.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_PagAsigandos.Rows.Count() - 1
            dtPagares.Rows.Add(CInt(dag_PagAsigandos.Rows(i).Cells(0).Text), dag_PagAsigandos.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_PagAsigandos.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked))
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_APARTADO9_PAGARES_ASIGNADOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("PAGARE", SqlDbType.Structured)
                Session("parm").Value = dtPagares
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                '  Execute the command.
                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()
                    Session("AUXILIAR") = myReader.GetInt32(0).ToString
                End While
                myReader.Close()

                lbl_status.Text = "Guardado correctamente"

            End Using

        Catch ex As Exception
            lbl_status.Text = "Error al asignar un pagaré."
        Finally
            Session("AUXILIAR") = Nothing
            LlenaPagare()
        End Try

    End Sub

#End Region


#Region "Contratos"
    Private Sub LlenaContratos()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim GarantiasGeneral As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_APARTADO9_CONTRATOS_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(GarantiasGeneral, Session("rs"))
        Session("Con").Close()

        If GarantiasGeneral.Rows.Count > 0 Then
            dag_contratos.Visible = True
            dag_contratos.DataSource = GarantiasGeneral
            dag_contratos.DataBind()
        Else
            dag_contratos.Visible = False
        End If
    End Sub
    Protected Sub btn_asignar_Contrato_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Data table que se llena con los contenidos de los datagrids
        Dim dtContratos As New Data.DataTable()
        dtContratos.Columns.Add("IDCONTRATO", GetType(Integer))
        dtContratos.Columns.Add("NOMBRE", GetType(String))
        dtContratos.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_contratos.Rows.Count() - 1
            dtContratos.Rows.Add(CInt(dag_contratos.Rows(i).Cells(0).Text), dag_contratos.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_contratos.Rows(i).FindControl("chk_contrAsig"), CheckBox).Checked))
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_APARTADO9_CONTRATOS_ASIGNADOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("CONTRATO", SqlDbType.Structured)
                Session("parm").Value = dtContratos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                '  Execute the command.
                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()
                    Session("AUXILIAR") = myReader.GetInt32(0).ToString
                End While
                myReader.Close()

                If Session("AUXILIAR") = 1 Then
                    lbl_contratosEstatus.Visible = True
                    lbl_contratosEstatus.Text = "Guardado correctamente"
                End If

            End Using

        Catch ex As Exception
            lbl_contratosEstatus.Text = "Error al asignar un contrato."
        Finally
            Session("AUXILIAR") = Nothing
            LlenaContratos()
        End Try

    End Sub


#End Region


#Region "Busqueda o modificación de solicitudes relacionadas"
    Protected Sub btn_solicitudes_Click(sender As Object, e As EventArgs) Handles btn_solicitudes.Click

        'Data table que se llena con los contenidos de los datagrids
        Dim dtSOLIC As New Data.DataTable()
        dtSOLIC.Columns.Add("IDSOLIC", GetType(Integer))
        dtSOLIC.Columns.Add("NOMBRE", GetType(String))
        dtSOLIC.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_solic.Rows.Count() - 1
            dtSOLIC.Rows.Add(CInt(dag_solic.Rows(i).Cells(0).Text), dag_solic.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_solic.Rows(i).FindControl("chk_SAsignado"), CheckBox).Checked))
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_APARTADO9_SOLICITUDES_ASIGNADOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("SOLICITUDES", SqlDbType.Structured)
                Session("parm").Value = dtSOLIC
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                '  Execute the command.
                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                While myReader.Read()
                    Session("AUXILIAR") = myReader.GetInt32(0).ToString
                End While
                myReader.Close()

                If Session("AUXILIAR") = 1 Then
                    lbl_solicitudes.Visible = True
                    lbl_solicitudes.Text = "Guardado correctamente"
                End If

            End Using

        Catch ex As Exception
            lbl_solicitudes.Visible = True
            lbl_solicitudes.Text = "Error al asignar una solicitud."
        Finally
            Session("AUXILIAR") = Nothing
            '()
        End Try
    End Sub


    'Muestra las garantias asignadas y disponibles
    Private Sub llenasolicitudes()


        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtsolic As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_APARTADO9_SOLICITUDES_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtsolic, Session("rs"))
        Session("Con").Close()

        If dtsolic.Rows.Count > 0 Then
            dag_solic.Visible = True
            dag_solic.DataSource = dtsolic
            dag_solic.DataBind()
        Else
            dag_solic.Visible = False
        End If

    End Sub

#End Region

End Class