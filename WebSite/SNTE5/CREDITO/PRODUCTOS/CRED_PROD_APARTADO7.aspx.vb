Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO7
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración de Comisiones", "Administración de Comisiones")

        If Not Page.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                LlenaComisiones()
                LlenaComisionesAtributos()
            End If
        End If
    End Sub
#Region "Comisiones"

    'MUESTRA LAS COMISIONES ASIGNADAS Y DISPONIBLES
    Private Sub LlenaComisiones()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ComisionesGeneral As New Data.DataTable()

        Dim dtComisionesAsignadas As New Data.DataTable()
        dtComisionesAsignadas.Columns.Add("IDC", GetType(Integer))
        dtComisionesAsignadas.Columns.Add("NOMBRE", GetType(String))
        dtComisionesAsignadas.Columns.Add("ASIGNADO", GetType(Integer))

        Dim dtComisionesNoAsignadas As New Data.DataTable()
        dtComisionesNoAsignadas.Columns.Add("IDC", GetType(Integer))
        dtComisionesNoAsignadas.Columns.Add("NOMBRE", GetType(String))
        dtComisionesNoAsignadas.Columns.Add("ASIGNADO", GetType(Integer))

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_APARTADO7_COMISIONES_ASIGNADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ComisionesGeneral, Session("rs"))
        Session("Con").Close()


        If ComisionesGeneral.Rows.Count > 0 Then
            dag_ComiAsigandas.Visible = True
            dag_ComiAsigandas.DataSource = ComisionesGeneral
            dag_ComiAsigandas.DataBind()
        Else
            dag_ComiAsigandas.Visible = False
        End If

    End Sub

    'Agrega garantias asignadas
    Protected Sub btn_asignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            'Data table que se llena con los contenidos de los datagrids
            Dim dtComisiones As New Data.DataTable()
            dtComisiones.Columns.Add("IDC", GetType(Integer))
            dtComisiones.Columns.Add("NOMBRE", GetType(String))
            dtComisiones.Columns.Add("ASIGNADO", GetType(Integer))

            For i As Integer = 0 To dag_ComiAsigandas.Rows.Count() - 1
                dtComisiones.Rows.Add(CInt(dag_ComiAsigandas.Rows(i).Cells(0).Text), dag_ComiAsigandas.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_ComiAsigandas.Rows(i).FindControl("chk_ComiAsignada"), CheckBox).Checked))
            Next


            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_APARTADO7_COMISIONES_ASIGNADAS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("COMISIONES", SqlDbType.Structured)
                Session("parm").Value = dtComisiones
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()
                lbl_status.Text = "Guardado correctamente"
            End Using

        Catch ex As Exception
            lbl_status.Text = "Error al asignar una comisión."
        Finally
            LlenaComisiones()
            Limpia_COM_SEG()
            LlenaComisionesAtributos()
        End Try

    End Sub

#End Region

#Region "Atributos"

    Private Sub LlenaComisionesAtributos()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtComisiones As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMISIONES_X_PRODUCTO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtComisiones, Session("rs"))
        Session("Con").Close()
        If dtComisiones.Rows.Count > 0 Then
            dag_Comisiones.Visible = True
            dag_Comisiones.DataSource = dtComisiones
            dag_Comisiones.DataBind()
        Else
            dag_Comisiones.Visible = False
        End If

    End Sub

    Private Sub dag_Comisiones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Comisiones.ItemCommand

        If (e.CommandName = "ATRIBUTOS") Then
            Select Case e.Item.Cells(1).Text
                Case "COM_SEG"
                    Session("CLAVECOM") = e.Item.Cells(1).Text
                    Administracion_COM_SEG()
                Case Else
            End Select
        End If
    End Sub

    Protected Sub lnk_RegresarTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_RegresarTotal.Click

        Select Case Session("CLAVECOM")
            Case "COM_SEG"
                Limpia_COM_SEG()
            Case Else
        End Select

        'TabPanel0.Enabled = True
        dag_Comisiones.Visible = True
        lnk_RegresarTotal.Visible = False
        lbl_status_atributos.Text = ""
        LlenaComisionesAtributos()

    End Sub
#End Region

#Region "Seguro"

    Private Sub Administracion_COM_SEG()

        Try
            pnl_COM_SEG.Visible = True
            btn_COM_SEG.Enabled = True
            dag_Comisiones.Visible = False
            lnk_RegresarTotal.Visible = True
            Atributos_COM_SEG()
        Catch ex As Exception
            lbl_status_atributos.Text = "ERROR: " + ex.Message()
        End Try

    End Sub

    Protected Function GetTipoCobroComSeg() As DataTable

        Dim dtData As New Data.DataTable()
        dtData.Columns.Add("TIPO_COBRO", GetType(String))
        dtData.Columns.Add("ID", GetType(String))

        Try
            dtData.Rows.Add("NINGUNO", "NINGUNO")
            dtData.Rows.Add("PORCENTAJE", "PORCENTAJE")
            dtData.Rows.Add("MONTO", "MONTO")
        Catch ex As Exception
            lbl_status_atributos.Text = "ERROR: " + ex.Message()
        End Try

        Return dtData

    End Function

    Private Sub Atributos_COM_SEG()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtCOM_SEG As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMGASTOSOPE_X_PRODUCTO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtCOM_SEG, Session("rs"))
        Session("Con").Close()
        If dtCOM_SEG.Rows.Count > 0 Then
            dag_com_seg.Visible = True
            dag_com_seg.DataSource = dtCOM_SEG
            dag_com_seg.DataBind()
        Else
            dag_com_seg.Visible = False
        End If

    End Sub

    Private Sub Limpia_COM_SEG()

        btn_COM_SEG.Enabled = False
        pnl_COM_SEG.Visible = False
        lnk_RegresarTotal.Visible = True

    End Sub

    Protected Sub btn_COM_SEG_Click(sender As Object, e As EventArgs)

        Try
            Dim dtData As New Data.DataTable()
            dtData.Columns.Add("TIPO_PERSONAL", GetType(String))
            dtData.Columns.Add("TIPO_COBRO", GetType(String))
            dtData.Columns.Add("MONTO_COBRO", GetType(String))

            Dim ERRORSTR As String = ""

            For i As Integer = 0 To dag_com_seg.Rows.Count() - 1
                If Convert.ToString(DirectCast(dag_com_seg.Rows(i).FindControl("txt_COM_SEG"), TextBox).Text) = "" Then
                    ERRORSTR = "Debe ingresar un valor en Monto de Cobro en todos los atributos."
                End If
            Next

            If ERRORSTR <> "" Then
                lbl_status_atributos.Text = ERRORSTR
            Else
                For i As Integer = 0 To dag_com_seg.Rows.Count() - 1

                    Session("Con").Open()
                    Session("cmd") = New ADODB.Command()
                    Session("cmd").ActiveConnection = Session("Con")
                    Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                    Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("TIPO_PERSONAL", Session("adVarChar"), Session("adParamInput"), 1000, dag_com_seg.Rows(i).Cells(0).Text)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("TIPO_COBRO", Session("adVarChar"), Session("adParamInput"), 10, Convert.ToString(DirectCast(dag_com_seg.Rows(i).FindControl("ddl_COM_SEG"), DropDownList).SelectedItem.Value))
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("MONTO_COBRO", Session("adVarChar"), Session("adParamInput"), 10, Convert.ToString(DirectCast(dag_com_seg.Rows(i).FindControl("txt_COM_SEG"), TextBox).Text))
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("cmd").CommandText = "INS_CNFPRC_COMISION_GASTO_OPE"
                    Session("cmd").Execute()
                    Session("Con").Close()
                Next

                Atributos_COM_SEG()
                lbl_status_atributos.Text = "Se han guardado correctamente los cambios realizados."

            End If

        Catch ex As Exception
            lbl_status_atributos.Text = "ERROR: " + ex.Message()
        End Try

    End Sub


#End Region

End Class