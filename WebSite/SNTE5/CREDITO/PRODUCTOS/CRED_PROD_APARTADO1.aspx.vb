Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Plazos, montos y fondeo", "PLAZOS/ MONTOS/ FONDEO")

        If Not Me.IsPostBack Then
            If Session("PRODID") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                'LLENO LOS COMBOS
                Session("flagmonto") = "0"
                Llenarplazos()
                LlenarMONTOS()
                llena_fuentes()
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
            End If
        End If
    End Sub

    Protected Sub btn_guardar_montoplazos_Click(sender As Object, e As EventArgs)

        If (CInt(txt_min.Text) > CInt(txt_max.Text)) Then
            lbl_status.Text = "Error: No puede agregar una límite mayor en en el límite menor."
            Exit Sub
        Else

            Dim plazomin As Integer = Convert.ToInt32(txt_min.Text) * 15
            Dim plazomax As Integer = Convert.ToInt32(txt_max.Text) * 15

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MINIMO", Session("adVarChar"), Session("adParamInput"), 10, plazomin.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MAXIMO", Session("adVarChar"), Session("adParamInput"), 10, plazomax.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, "DIAS")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFPCR_PLAZO"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()
            'lbl_agregar.Text = "Se han actualizado correctamente los datos"

        End If


        If (CInt(txt_infmonto.Text) > CInt(txt_supmonto.Text)) Then
            lbl_status.Text = "Error: No puede agregar una límite mayor en en el límite menor"

        Else
            'Si los campos están llenos indica que es una actualización
            If Session("flagmonto").ToString = "1" Then

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINIMO", Session("adVarChar"), Session("adParamInput"), 10, txt_infmonto.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXIMO", Session("adVarChar"), Session("adParamInput"), 10, txt_supmonto.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_CNFPCR_MONTOS"
                Session("rs") = Session("cmd").Execute()
                Session("Con").Close()
                lbl_status.Text = "Guardado correctamente"

            Else 'indica que los campos están vacíos
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("MINIMO", Session("adVarChar"), Session("adParamInput"), 10, txt_infmonto.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXIMO", Session("adVarChar"), Session("adParamInput"), 10, txt_supmonto.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                'Valores para actualizar el id del plazo en la tabla mstproductos
                Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_CNFPCR_MONTOS"
                Session("rs") = Session("cmd").Execute()
                Session("Con").Close()
                lbl_status.Text = "Guardado correctamente"

            End If
        End If
    End Sub


    '------------PLAZOS ---------------
    'Muestra en los txtbox los registros si es que existen dentro de la base de datos.
    Private Sub Llenarplazos()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PLAZO_PRODUCTO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof() Then
            txt_max.Enabled = True
            Dim plazomin As Integer = Convert.ToInt32(Session("rs").Fields("MINIMO").Value.ToString) / 15
            Dim plazomax As Integer = Convert.ToInt32(Session("rs").Fields("MAXIMO").Value.ToString) / 15
            txt_min.Text = plazomin.ToString
            txt_max.Text = plazomax.ToString
        End If
        Session("Con").Close()

    End Sub

    '------------MONTOS ---------------
    'Muestra en los txtbox los registros si es que existen dentro de la base de datos.
    Private Sub LlenarMONTOS()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_MONTO_PRODUCTO"

        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("MONTO").Value.ToString = "1" Then

            txt_infmonto.Text = Session("rs").Fields("MSTMONTOS_LIMITE_INF").Value.ToString
            txt_supmonto.Text = Session("rs").Fields("MSTMONTOS_LIMITE_SUP").Value.ToString
            Session("flagmonto") = Session("rs").Fields("MONTO").Value.ToString

        End If
        Session("Con").Close()

    End Sub

    Protected Sub llena_fuentes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ModulosGeneral As New Data.DataTable()

        Dim dtFuentesAsignados As New Data.DataTable()
        dtFuentesAsignados.Columns.Add("ID", GetType(Integer))
        dtFuentesAsignados.Columns.Add("NOMBRE", GetType(String))
        dtFuentesAsignados.Columns.Add("ASIGNADO", GetType(Integer))

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 50, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_FONDO_PROD"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ModulosGeneral, Session("rs"))
        Session("Con").Close()

        If ModulosGeneral.Rows.Count > 0 Then
            dag_mod_si.Visible = True
            dag_mod_si.DataSource = ModulosGeneral
            dag_mod_si.DataBind()
        Else
            dag_mod_si.Visible = False
        End If

    End Sub

    Protected Sub btn_guardar_r_Click(sender As Object, e As EventArgs)
        'Data table que se llena con el contenido del datagrid
        Dim dtFuentes As New Data.DataTable()
        dtFuentes.Columns.Add("ID", GetType(Integer))
        dtFuentes.Columns.Add("NOMBRE", GetType(String))
        dtFuentes.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_mod_si.Rows.Count() - 1
            dtFuentes.Rows.Add(CInt(dag_mod_si.Rows(i).Cells(0).Text), dag_mod_si.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_mod_si.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los roles a un usuario
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_FOND_PROD_ASIGNAR", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("ID", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("FONDEO", SqlDbType.Structured)
                Session("parm").Value = dtFuentes
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

            lbl_asigFuentesEstatus.Text = "Guardado correctamente"

        Catch ex As Exception
            lbl_asigFuentesEstatus.Text = "Error al asignar fuentes."
        Finally
            llena_fuentes()
        End Try
    End Sub


End Class