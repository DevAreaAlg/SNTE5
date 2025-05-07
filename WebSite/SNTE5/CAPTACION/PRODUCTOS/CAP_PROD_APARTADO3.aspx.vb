Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Public Class CAP_PROD_APARTADO3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración de Comisiones", "ADMINISTRACIÓN DE COMISIONES")

        If Not Page.IsPostBack Then
            lbl_Producto.Text = Session("PROD_NOMBRE").ToString
            LlenaComisiones()
            LlenaComisionesAtributos()

        End If

        semaforo()
    End Sub

    '-----------ASIGNACION Y DESHABILITACION DE COMISIONES-------------
    Private Sub semaforo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFPRODCAP_SEMAFORO_APARTADO3"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    '----------MUESTRA COMISIONES ASIGNADAS Y DISPONIBLES----------------
    Private Sub LlenaComisiones()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ComisionesGeneral As New Data.DataTable()

        Dim dtComisionesAsignadas As New Data.DataTable()
        dtComisionesAsignadas.Columns.Add("IDC", GetType(Integer))
        dtComisionesAsignadas.Columns.Add("NOMBRE", GetType(String))
        dtComisionesAsignadas.Columns.Add("ASIGNADO", GetType(Integer))

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

        For Each row As Data.DataRow In ComisionesGeneral.Rows()
            dtComisionesAsignadas.ImportRow(row)
        Next

        If dtComisionesAsignadas.Rows.Count > 0 Then
            dag_ComiAsigandas.Visible = True
            dag_ComiAsigandas.DataSource = dtComisionesAsignadas
            dag_ComiAsigandas.DataBind()
        Else
            dag_ComiAsigandas.Visible = False
        End If

    End Sub

    '-------------AGREGA COMISIONEs ASIGNADAS----------------
    Protected Sub btn_asignar_Click(ByVal sender As Object, e As EventArgs)

        'Data table que se llena con los contenidos de los datagrids
        Dim dtComisiones As New Data.DataTable()
        dtComisiones.Columns.Add("IDC", GetType(Integer))
        dtComisiones.Columns.Add("NOMBRE", GetType(String))
        dtComisiones.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_ComiAsigandas.Rows.Count() - 1
            dtComisiones.Rows.Add(CInt(dag_ComiAsigandas.Rows(i).Cells(0).Text), dag_ComiAsigandas.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_ComiAsigandas.Rows(i).FindControl("chk_ComiAsignada"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure que asigna las comisiones al producto de captación
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_COMISIONES_ASIGNADAS_CAPTACION", connection)
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

            End Using

            lbl_status.Text = "Guardado correctamente"

        Catch ex As Exception
            lbl_status.Text = "Error al asignar una comisión."
        Finally
            LlenaComisiones()
        End Try

        LlenaComisionesAtributos()
    End Sub

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
                Case "SALDMIN_VISTA"
                    Session("CLAVECOM") = e.Item.Cells(1).Text
                    Administracion_SaldoMinVista()
                Case Else
            End Select
        End If
    End Sub

    Protected Sub lnk_RegresarTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_RegresarTotal.Click

        Select Case Session("CLAVECOM")
            Case "SALDMIN_VISTA"
                Limpia_SaldoMinVista()
            Case Else
        End Select

        dag_Comisiones.Visible = True
        lnk_RegresarTotal.Visible = False
        lbl_status_atributos.Text = ""
        LlenaComisionesAtributos()

    End Sub

    '------------------------------------  COMISION POR INCUMPLIMIENTO DE SALDO PROMEDIO MINIMO  ------------------------------------

    Private Sub Administracion_SaldoMinVista()

        pnl_SaldoMinVista.Visible = True
        dag_Comisiones.Visible = False
        lnk_RegresarTotal.Visible = True
        Atributos_SaldoMinVista()

    End Sub

    Private Sub Atributos_SaldoMinVista()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMCAPSALDMIN_X_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("TIPO").Value = "PORC" Then
            lbl_TipoMin.Text = "%"
            rad_SaldoMinVista_Porcentaje.Checked = True
            rad_SaldoMinVista_Monto.Checked = False
            txt_Valor.Text = Session("rs").Fields("VALOR").Value
            btn_SaldoMinVista.Enabled = True
        Else
            lbl_TipoMin.Text = "$"
            rad_SaldoMinVista_Porcentaje.Checked = False
            rad_SaldoMinVista_Monto.Checked = True
            txt_Valor.Text = Session("rs").Fields("VALOR").Value
            btn_SaldoMinVista.Enabled = True
        End If


        Session("Con").Close()

    End Sub

    Private Sub Limpia_SaldoMinVista()

        txt_Valor.Text = ""
        lbl_TipoMin.Text = ""
        rad_SaldoMinVista_Monto.Checked = False
        rad_SaldoMinVista_Porcentaje.Checked = False
        btn_SaldoMinVista.Enabled = False
        pnl_SaldoMinVista.Visible = False
        lnk_RegresarTotal.Visible = True

    End Sub

    Protected Sub rad_SaldoMinVista_Porcentaje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_SaldoMinVista_Porcentaje.CheckedChanged
        lbl_TipoMin.Text = "%"
        btn_SaldoMinVista.Enabled = True
    End Sub

    Protected Sub rad_SaldoMinVista_Monto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_SaldoMinVista_Monto.CheckedChanged
        lbl_TipoMin.Text = "$"
        btn_SaldoMinVista.Enabled = True
    End Sub

    Protected Sub btn_SaldoMinVista_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SaldoMinVista.Click

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If rad_SaldoMinVista_Monto.Checked = True Then
            Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 20, "MONTO")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 20, "PORC")
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 50, txt_Valor.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_COMCAPSALDMIN_ATRIBUTOS"
        Session("cmd").Execute()

        lbl_status_atributos.Text = "Atributos asignados a la comisión"
        txt_Valor.Text = ""
        lbl_TipoMin.Text = ""
        rad_SaldoMinVista_Monto.Checked = False
        rad_SaldoMinVista_Porcentaje.Checked = False
        btn_SaldoMinVista.Enabled = False

        Session("Con").Close()

        Atributos_SaldoMinVista()

    End Sub

End Class