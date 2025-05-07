Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración de Garantías", "Administración de Garantías")

        If Not Page.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                LlenaGarantias()
                Llenatipogarantias()
                MuestraGarantias()
            End If
        End If
    End Sub

    Protected Sub limpiar_sessionV(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnk As LinkButton = sender
        Dim href As String = lnk.Attributes("Redirect").ToString()
        Session.Remove("EQ_MODCRE")
        Response.Redirect(href)

    End Sub

#Region "Adminstracion Garantias"

    Private Sub Llenatipogarantias()

        cmb_tipo.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_tipo.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPOS_GARANTIAS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("TIPO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_tipo.Items.Add(item)

            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    'Muestra las garantias existentes
    Private Sub MuestraGarantias()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtgarantias As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_GARANTIAS_AGREGADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtgarantias, Session("rs"))
        dag_Garantias.DataSource = dtgarantias
        dag_Garantias.DataBind()
        Session("Con").Close()

    End Sub

    'Botón que inserta una nueva Garantia
    Protected Sub btn_agregarGarantias_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 50, txt_descripcionGarantia.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 15, cmb_tipo.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PORCENTAJE", Session("adVarChar"), Session("adParamInput"), 10, txt_monto.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            If chk_estatus.Checked = True Then
                Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Else
                Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, 0)
            End If
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFPCR_NUEVAS_GARANTIAS"
            Session("rs") = Session("cmd").Execute()

            If Session("rs").Fields("RESPUESTA").Value.ToString = "ERROR" Then
                lbl_agregar.Text = "Error: Ya existe una garantía con el mismo nombre"
            Else
                lbl_agregar.Text = "Guardado correctamente"
            End If
            Session("Con").Close()
        Catch ex As Exception
            lbl_agregar.Text = "Error al intentar guardar la garantía"
        Finally
            LimpiarDatos()
            LlenaGarantias()
            MuestraGarantias()
        End Try
    End Sub

    Private Sub DAG_garantias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Garantias.ItemCommand

        If (e.CommandName = "HABILITAR") Then
            ActualizaEstatus(e.Item.Cells(0).Text, e.Item.Cells(4).Text)
            MuestraGarantias()
        End If

        If (e.CommandName = "ELIMINAR") Then
            Elimina_Garantias(e.Item.Cells(0).Text)
            MuestraGarantias()
        End If

    End Sub

    'BOtón HABILITAR del DBGRID (Actualiza el estatus de la garantia de la tabla RELPRODGARAN)
    Private Sub ActualizaEstatus(ByVal idgarantia As String, ByVal Estatus As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, idgarantia)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        If Estatus = "INACTIVO" Then
            Session("cmd").CommandText = "UPD_CNFPCR_HABILITA_GARANTIAS"
            Session("rs") = Session("cmd").Execute()
            If Session("rs").Fields("RESULTADO").Value.ToString = "EXITO" Then

                lbl_verifica.Text = "Guardado correctamente"
            Else
                lbl_verifica.Text = "Error: La garantía esta asignada a uno o varios productos"
            End If
            Session("Con").Close()
        Else
            Session("cmd").CommandText = "UPD_CNFPCR_DESHABILITA_GARANTIAS"
            Session("rs") = Session("cmd").Execute()
            If Session("rs").Fields("RESULTADO").Value.ToString = "EXITO" Then

                lbl_verifica.Text = "Guardado correctamente"
            Else
                lbl_verifica.Text = "Error: La garantía esta asignada a uno o varios productos"
            End If

            Session("Con").Close()

        End If

        LlenaGarantias()

    End Sub

    'Botón ELIMINAR del DBGRID(Elimina la garantia completamente de la tabla CATGARANTIAS)
    Private Sub Elimina_Garantias(ByVal idgarantia As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDGARANTIA", Session("adVarChar"), Session("adParamInput"), 10, idgarantia)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFPCR_GARANTIAS_AGREGADAS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            If Session("rs").Fields("RESULTADO").Value.ToString = "EXITO" Then
                lbl_verifica.Text = "Guardado correctamente"
            End If
            If Session("rs").Fields("RESULTADO").Value.ToString = "EXP" Then
                lbl_verifica.Text = "Error: La garantía esta asignada a un expediente"
            End If
            If Session("rs").Fields("RESULTADO").Value.ToString = "ERROR" Then
                lbl_verifica.Text = "Error: La garantía esta asignada a uno o varios productos"
            End If

        End If

        Session("Con").Close()
        LlenaGarantias()

    End Sub

    'Limpia los comandos al momento de agregar una nueva garantia
    Private Sub LimpiarDatos()

        lbl_verifica.Text = ""
        txt_descripcionGarantia.Text = ""
        txt_monto.Text = ""
        chk_estatus.Checked = False
        cmb_tipo.SelectedIndex = 0

    End Sub

#End Region

#Region "Asignacion Garantias"

    'Muestra las garantias asignadas y disponibles
    Private Sub LlenaGarantias()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim GarantiasGeneral As New Data.DataTable()

        Dim dtGarantiasAsignadas As New Data.DataTable()
        dtGarantiasAsignadas.Columns.Add("IDG", GetType(Integer))
        dtGarantiasAsignadas.Columns.Add("DESCRIPCION", GetType(String))
        dtGarantiasAsignadas.Columns.Add("TIPO", GetType(String))
        dtGarantiasAsignadas.Columns.Add("ASIGNADO", GetType(Integer))


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_APARTADO3_GARANTIAS_ASIGNADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(GarantiasGeneral, Session("rs"))
        Session("Con").Close()

        If GarantiasGeneral.Rows.Count > 0 Then
            dag_GtiaAsigandas.Visible = True
            dag_GtiaAsigandas.DataSource = GarantiasGeneral
            dag_GtiaAsigandas.DataBind()
        Else
            dag_GtiaAsigandas.Visible = False
        End If

    End Sub

    'Agrega garantias asignadas
    Protected Sub btn_asignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            'Data table que se llena con los contenidos de los datagrids
            Dim dtGarantias As New Data.DataTable()
            dtGarantias.Columns.Add("IDG", GetType(Integer))
            dtGarantias.Columns.Add("DESCRIPCION", GetType(String))
            dtGarantias.Columns.Add("ASIGNADO", GetType(Integer))

            For i As Integer = 0 To dag_GtiaAsigandas.Rows.Count() - 1
                dtGarantias.Rows.Add(CInt(dag_GtiaAsigandas.Rows(i).Cells(0).Text), dag_GtiaAsigandas.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_GtiaAsigandas.Rows(i).FindControl("chk_GtiaAsignada"), CheckBox).Checked))
            Next

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_APARTADO3_GARANTIAS_ASIGNADAS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("GARANTIAS", SqlDbType.Structured)
                Session("parm").Value = dtGarantias
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                lbl_status.Text = "Guardado correctamente"
                connection.Close()

            End Using

        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally
            LlenaGarantias()
        End Try

    End Sub

#End Region

End Class