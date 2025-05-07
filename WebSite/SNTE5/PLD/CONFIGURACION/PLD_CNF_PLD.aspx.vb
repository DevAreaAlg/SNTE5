Imports System.Data
Imports System.Data.SqlClient
Public Class PLD_CNF_PLD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuraciones de PLD", "CONFIGURACIÓN DE MOTOR PLD")

        If Not Me.IsPostBack Then
            LlenaConfiguracionPLD()
            LlenaDatosTabuladores()
            LlenaVariablesFraccionados()
            LlenaDatosFraccionados()
        End If

    End Sub

    Private Sub LlenaConfiguracionPLD()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFLAVADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_PorcMinMov.Text = Session("rs").fields("PORCENTAJE_MIN_MOV").value.ToString
            txt_NumDesvEst.Text = Session("rs").fields("NUM_DESVIACION_ESTANDAR").value.ToString
            txt_LimInfMoral.Text = Session("rs").fields("LIMITE_INFERIOR_MORAL").value.ToString
            txt_LimInfFisica.Text = Session("rs").fields("LIMITE_INFERIOR").value.ToString
            txt_LimOpeRelMoral.Text = Session("rs").fields("LIM_OPEREL_PESOS_MORAL").value.ToString
            txt_LimOpeRelFisica.Text = Session("rs").fields("LIM_OPEREL_PESOS_FISICA").value.ToString
            txt_LimOpeRelDolar.Text = Session("rs").fields("LIM_OPEREL_DOLAR").value.ToString
            txt_LimOpeRel.Text = Session("rs").fields("LIM_OPE_REL").value.ToString
            txt_LimMin.Text = Session("rs").fields("LIM_MIN").value.ToString
            txt_LimMax.Text = Session("rs").fields("LIM_MAX").value.ToString
            txt_DiasOpeRel.Text = Session("rs").fields("DIASX_OPE_REL").value.ToString
            txt_LimOpeRelMensPesos.Text = Session("rs").fields("LIM_OPEREL_MENSUAL_PESOS").value.ToString
            txt_LimOpeRelMensDolar.Text = Session("rs").fields("LIM_OPEREL_MENSUAL_DOLARES").value.ToString
            cmb_AutoOpe.Items.FindByValue(Session("rs").fields("AUTOR_OPE").value.ToString).Selected = True
            txt_NumOpeMAx.Text = Session("rs").fields("NUM_OPE_MAX").value.ToString
            txt_NumOpeInstMon.Text = Session("rs").fields("NUM_OPE_INSTMON").value.ToString
            txt_RevAltRiesgo.Text = Session("rs").fields("REV_ALTO_RIESGO").value.ToString
            txt_RevBajRiesgo.Text = Session("rs").fields("REV_BAJO_RIESGO").value.ToString

        End If
        Session("Con").Close()

    End Sub

    Private Sub LlenaDatosTabuladores()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtTabulador As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFLAVADO_TABULADOR"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtTabulador, Session("rs"))
        Session("Con").Close()

        If dtTabulador.Rows.Count > 0 Then
            dag_Tabulador.Visible = True
            dag_Tabulador.DataSource = dtTabulador
            dag_Tabulador.DataBind()
        Else
            dag_Tabulador.Visible = False
        End If

    End Sub

    Protected Sub btn_GuardarConf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardarConf.Click

        If CInt(txt_LimMax.Text) <= CInt(txt_LimMin.Text) Then
            lbl_InfoConf.Text = "Error: El máximo de meses para cálculo de perfil transaccional debe ser mayor al mínimo de meses."
        Else
            If CInt(txt_LimMin.Text) <= 2 Then
                lbl_InfoConf.Text = "Error: El mínimo de meses para cálculo de perfil transaccional debe ser mayor o igual a 3 meses."
            Else
                If CInt(txt_PorcMinMov.Text) > 100 Or CInt(txt_PorcMinMov.Text) < 10 Then
                    lbl_InfoConf.Text = "Error: El porcentaje mínimo de movimientos debe ser menor/igual a 100% o mayor/igual a 10%."
                Else
                    If CInt(txt_DiasOpeRel.Text) > 30 Then
                        lbl_InfoConf.Text = "Error: El rango no puede ser mayor a 30 dias."
                    Else
                        If CInt(txt_NumDesvEst.Text) = 0 Then
                            lbl_InfoConf.Text = "Error: El número de desviación estándar debe ser mayor a 0."
                        Else
                            GuardaConfiguracionPLD()
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub GuardaConfiguracionPLD()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PORCENTAJE_MIN_MOV", Session("adVarChar"), Session("adParamInput"), 10, txt_PorcMinMov.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUM_DESVIACION_ESTANDAR", Session("adVarChar"), Session("adParamInput"), 10, txt_NumDesvEst.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIMITE_INFERIOR_MORAL", Session("adVarChar"), Session("adParamInput"), 50, txt_LimInfMoral.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIMITE_INFERIOR", Session("adVarChar"), Session("adParamInput"), 50, txt_LimInfFisica.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_OPEREL_PESOS_MORAL", Session("adVarChar"), Session("adParamInput"), 50, txt_LimOpeRelMoral.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_OPEREL_PESOS_FISICA", Session("adVarChar"), Session("adParamInput"), 50, txt_LimOpeRelFisica.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_OPEREL_DOLAR", Session("adVarChar"), Session("adParamInput"), 50, txt_LimOpeRelDolar.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_OPE_REL", Session("adVarChar"), Session("adParamInput"), 50, txt_LimOpeRel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_MIN", Session("adVarChar"), Session("adParamInput"), 50, txt_LimMin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_MAX", Session("adVarChar"), Session("adParamInput"), 50, txt_LimMax.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASX_OPE_REL", Session("adVarChar"), Session("adParamInput"), 10, txt_DiasOpeRel.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_OPEREL_MENSUAL_PESOS", Session("adVarChar"), Session("adParamInput"), 50, txt_LimOpeRelMensPesos.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIM_OPEREL_MENSUAL_DOLARES", Session("adVarChar"), Session("adParamInput"), 50, txt_LimOpeRelMensDolar.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("AUTOR_OPE", Session("adVarChar"), Session("adParamInput"), 10, cmb_AutoOpe.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUM_OPE_MAX", Session("adVarChar"), Session("adParamInput"), 10, txt_NumOpeMAx.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUM_OPE_INSTMON", Session("adVarChar"), Session("adParamInput"), 10, txt_NumOpeInstMon.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REV_ALTO_RIESGO", Session("adVarChar"), Session("adParamInput"), 10, txt_RevAltRiesgo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REV_BAJO_RIESGO", Session("adVarChar"), Session("adParamInput"), 10, txt_RevBajRiesgo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFLAVADO"
        Session("cmd").Execute()
        Session("Con").Close()

        lbl_InfoConf.Text = "Guardado correctamente"

    End Sub

    Private Sub dag_Tabulador_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Tabulador.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            EliminaConfTabulador(e.Item.Cells(0).Text, e.Item.Cells(1).Text, e.Item.Cells(2).Text, e.Item.Cells(3).Text)
            LlenaDatosTabuladores()
        End If

    End Sub

    Private Sub EliminaConfTabulador(ByVal MontoMin As Decimal, ByVal MontoMax As Decimal, ByVal TipoPersona As String, ByVal Mult As Decimal)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("MONTOMIN", Session("adVarChar"), Session("adParamInput"), 50, MontoMin)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOMAX", Session("adVarChar"), Session("adParamInput"), 50, MontoMax)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 20, TipoPersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MULTIPLICADOR", Session("adVarChar"), Session("adParamInput"), 50, Mult)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFLAVADO_TABULADOR"
        Session("cmd").Execute()
        Session("Con").Close()

        lbl_info.Text = "Se ha eliminado correctamente el registro."

    End Sub

    Protected Sub btn_GuardarTabulador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardarTabulador.Click

        If CDec(txt_MontoMax.Text) <= CDec(txt_MontoMin.Text) Then
            lbl_info.Text = "Error: El monto máximo debe ser mayor al monto mínimo."
        Else
            GuardaTabuladorPLD()
        End If

        txt_MontoMin.Text = ""
        txt_MontoMax.Text = ""
        txt_Multiplicador.Text = ""

    End Sub

    Private Sub GuardaTabuladorPLD()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("MONTOMIN", Session("adVarChar"), Session("adParamInput"), 50, txt_MontoMin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOMAX", Session("adVarChar"), Session("adParamInput"), 50, txt_MontoMax.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOPER", Session("adVarChar"), Session("adParamInput"), 20, cmb_TipoPersona.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MULTIPLICADOR", Session("adVarChar"), Session("adParamInput"), 10, txt_Multiplicador.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFLAVADO_TABULADOR"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").fields("ESTATUS").value.ToString = 1 Then
                lbl_info.Text = "Guardado correctamente"
            Else
                lbl_info.Text = "Error: Los límites de la configiración, no deben existir dentro de los límites de otro registro para este tipo de persona."
            End If
        End If
        Session("Con").Close()

        LlenaDatosTabuladores()

    End Sub

#Region "Fraccionados"

    Private Sub LlenaDatosFraccionados()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtFraccionados As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFLAVADO_FRACCIONADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtFraccionados, Session("rs"))
        Session("Con").Close()

        If dtFraccionados.Rows.Count > 0 Then
            dag_Fraccionados.Visible = True
            dag_Fraccionados.DataSource = dtFraccionados
            dag_Fraccionados.DataBind()
        Else
            dag_Fraccionados.Visible = False
        End If

    End Sub

    Private Sub LlenaVariablesFraccionados()

        CrearTablasFraccionados()
        LlenaSucursalesIni()
        LlenaProductosIni()

    End Sub

    Private Sub CrearTablasFraccionados()

        Session("Sucursales_NoAsignados") = Nothing
        Session("Sucursales_Asignados") = Nothing
        Session("Productos_NoAsignados") = Nothing
        Session("Productos_Asignados") = Nothing

        Dim Sucursales_NoAsignados As New Data.DataTable
        Dim Sucursales_Asignados As New Data.DataTable
        Dim Productos_NoAsignados As New Data.DataTable
        Dim Productos_Asignados As New Data.DataTable

        Sucursales_NoAsignados.Columns.Add("IDSUCURSAL", GetType(Integer)) '0
        Sucursales_NoAsignados.Columns.Add("SUCURSAL", GetType(String)) '1

        Sucursales_Asignados.Columns.Add("IDSUCURSAL", GetType(Integer)) '0
        Sucursales_Asignados.Columns.Add("SUCURSAL", GetType(String)) '1

        Productos_NoAsignados.Columns.Add("IDPRODUCTO", GetType(Integer)) '0
        Productos_NoAsignados.Columns.Add("PRODUCTO", GetType(String)) '1

        Productos_Asignados.Columns.Add("IDPRODUCTO", GetType(Integer)) '0
        Productos_Asignados.Columns.Add("PRODUCTO", GetType(String)) '1

        Session("Sucursales_NoAsignados") = Sucursales_NoAsignados
        Session("Sucursales_Asignados") = Sucursales_Asignados
        Session("Productos_NoAsignados") = Productos_NoAsignados
        Session("Productos_Asignados") = Productos_Asignados

    End Sub

    Private Sub LlenaSucursalesIni()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim SucursalesGeneral As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUC_ASIGNADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(SucursalesGeneral, Session("rs"))
        Session("Con").Close()

        If SucursalesGeneral.Rows.Count > 0 Then
            dag_suc_si.Visible = True
            dag_suc_si.DataSource = SucursalesGeneral
            dag_suc_si.DataBind()
        Else
            dag_suc_si.Visible = False
        End If

    End Sub

    Private Sub LlenaProductosIni()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ProductosGeneral As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PROD_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ProductosGeneral, Session("rs"))
        Session("Con").Close()

        If ProductosGeneral.Rows.Count > 0 Then
            dag_prod_si.Visible = True
            dag_prod_si.DataSource = ProductosGeneral
            dag_prod_si.DataBind()
        Else
            dag_prod_si.Visible = False
        End If

    End Sub

    Protected Sub btn_FraccNumOperaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_FraccNumOperaciones.Click

        'Data table que se llena con los contenidos de los datagrids  SUCURSAL
        Dim dtSuc As New Data.DataTable()
        dtSuc.Columns.Add("IDG", GetType(Integer))
        dtSuc.Columns.Add("DESCRIPCION", GetType(String))
        dtSuc.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_suc_si.Rows.Count() - 1
            dtSuc.Rows.Add(CInt(dag_suc_si.Rows(i).Cells(0).Text), dag_suc_si.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_suc_si.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        'Data table que se llena con los contenidos de los datagrids  PRODUCTOS
        Dim dtProd As New Data.DataTable()
        dtProd.Columns.Add("IDG", GetType(Integer))
        dtProd.Columns.Add("DESCRIPCION", GetType(String))
        dtProd.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_prod_si.Rows.Count() - 1
            dtProd.Rows.Add(CInt(dag_prod_si.Rows(i).Cells(0).Text), dag_prod_si.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_prod_si.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFLAVADO_FRACCIONADOS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("SUCURSALES", SqlDbType.Structured)
                Session("parm").Value = dtSuc
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("PRODUCTOS", SqlDbType.Structured)
                Session("parm").Value = dtProd
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("NUMOPE", SqlDbType.Int)
                Session("parm").Value = txt_FraccNumOperaciones.Text
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
            lbl_InfoFraccionados.Text = "Error"

        Finally

            LlenaSucursalesIni()
            LlenaProductosIni()
            lbl_InfoFraccionados.Text = ""
            LlenaDatosFraccionados()
            txt_FraccNumOperaciones.Text = ""
            'CrearTablasFraccionados()

        End Try

    End Sub

    Protected Sub dag_Fraccionados_pageIndexChanged(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs) Handles dag_Fraccionados.PageIndexChanged

        dag_Fraccionados.CurrentPageIndex = e.NewPageIndex
        LlenaDatosFraccionados()

    End Sub

    Protected Sub dag_Fraccionados_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Fraccionados.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_InfoFraccionados.Text = "Registro eliminado correctamente."
            EliminarConfFraccionados(e.Item.Cells(0).Text, e.Item.Cells(2).Text, e.Item.Cells(4).Text)
        End If

        LlenaDatosFraccionados()

    End Sub

    Private Sub EliminarConfFraccionados(ByVal IDSucursal As Integer, ByVal IDProducto As Integer, ByVal NumOperaciones As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUCURSAL", Session("adVarChar"), Session("adParamInput"), 10, IDSucursal)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, IDProducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMOPE", Session("adVarChar"), Session("adParamInput"), 10, NumOperaciones)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFLAVADO_FRACCIONADOS"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region

End Class