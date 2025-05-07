Public Class PEN_PROC_PERIODOS
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Periodos", "Asignación de Periodos")

        If Not Me.IsPostBack Then
            CargaInstituciones()
            ddl_tipoperiodo.Enabled = False
            ddl_proceso.Enabled = False
            ' btn_crear.Enabled = False
        End If

    End Sub

    Private Sub CargaInstituciones()

        ddl_institucion.Items.Clear()
        ddl_institucion.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PROC_INSTITUCIONES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_institucion.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Protected Sub ddl_institucion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_institucion.SelectedIndexChanged

        If ddl_institucion.SelectedItem.Value = -1 Then
            ddl_proceso.Items.Clear()
            ddl_proceso.Enabled = False
        Else
            ddl_proceso.Enabled = True
            CargaProcesos()
        End If

        ddl_meses.Items.Clear()
        ddl_meses.Items.Add(New ListItem("ELIJA", "-1"))

        dag_Peridos.DataSource = Nothing
        dag_Peridos.DataBind()

    End Sub

    Private Sub CargaProcesos()

        ddl_proceso.Items.Clear()
        ddl_proceso.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_INSTITUCION", Session("adVarChar"), Session("adParamInput"), 11, ddl_institucion.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PROCESO"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_proceso.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("ID").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()


    End Sub

    Protected Sub ddl_proceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_proceso.SelectedIndexChanged

        If ddl_proceso.SelectedItem.Value = -1 Then
            ddl_tipoperiodo.Enabled = False
            dag_Peridos.DataSource = Nothing
            dag_Peridos.DataBind()
        Else
            ddl_tipoperiodo.Enabled = True
            CargaMeses()
            dag_Peridos.DataSource = Nothing
            dag_Peridos.DataBind()
            MostrarPeriodo()
            dag_Peridos.Visible = True
        End If

        'ddl_meses.Items.Clear()
        'ddl_meses.Items.Add(New ListItem("ELIJA", "-1"))
    End Sub


    Private Sub CargaMeses()

        ddl_meses.Items.Clear()
        ddl_meses.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_MESES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            ddl_meses.Items.Add(New ListItem(Session("rs").Fields("MES").Value, Session("rs").Fields("ID_MES").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub ddl_meses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_meses.SelectedIndexChanged

        txt_DiaIni.Enabled = True
        txt_DiaFin.Enabled = True
        txt_HoraIni.Enabled = True
        txt_HoraFin.Enabled = True

    End Sub

    Protected Sub ddl_tipoperiodo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_tipoperiodo.SelectedIndexChanged

        If ddl_tipoperiodo.SelectedItem.Value = "-1" Then
            ddl_tipoperiodo.Enabled = False

        ElseIf ddl_tipoperiodo.SelectedItem.Value = "OTRO" Then

            ddl_meses.Enabled = True

        Else

            ddl_tipoperiodo.Enabled = True
            CargaMeses()

        End If

        txt_DiaIni.Enabled = True
        txt_DiaFin.Enabled = True
        txt_HoraIni.Enabled = True
        txt_HoraFin.Enabled = True

    End Sub

    Protected Sub btn_guarda_periodo_Click(sender As Object, e As EventArgs) Handles btn_guarda_periodo.Click

        Dim mes As Integer
        If ddl_meses.SelectedItem.Value = "-1" Then
            mes = -1
        Else
            mes = ddl_meses.SelectedValue
        End If

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROCESO", Session("adVarChar"), Session("adParamInput"), 10, ddl_proceso.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 5, ddl_tipoperiodo.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMES", Session("adVarChar"), Session("adParamInput"), 10, mes)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIAINI", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_DiaIni.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIAFIN", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_DiaFin.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORAINI", Session("adVarChar"), Session("adParamInput"), 20, txt_HoraIni.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORAFIN", Session("adVarChar"), Session("adParamInput"), 20, txt_HoraFin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSUARIO", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_PROCESOS_PERIODOS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        MostrarPeriodo()
        dag_Peridos.Visible = True
        lbl_statupol.Text = "Guardado Correctamente"
    End Sub

    Protected Sub MostrarPeriodo()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROCESO", Session("adVarChar"), Session("adParamInput"), 50, ddl_proceso.SelectedValue)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PERIODOS_PROCESOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))

        If dteq.Rows.Count > 0 Then
            dag_Peridos.Visible = True
            dag_Peridos.DataSource = dteq
            dag_Peridos.DataBind()
        Else
            dag_Peridos.Visible = False
        End If
        Session("Con").Close()
    End Sub

    Protected Sub dag_Periodos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Peridos.ItemCommand

        Session("IDPROCESO") = e.Item.Cells(0).Text
        Session("IDMES") = e.Item.Cells(2).Text
        Session("DIAINI") = e.Item.Cells(4).Text

        If (e.CommandName = "ELIMINAR") Then
            eliminaProceso()
        End If

    End Sub

    Protected Sub eliminaProceso()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROCESO", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPROCESO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMES", Session("adVarChar"), Session("adParamInput"), 10, Session("IDMES"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIAINI", Session("adVarChar"), Session("adParamInput"), 10, Session("DIAINI"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSUARIO", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_PERIODOS_PROCESOS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        MostrarPeriodo()
    End Sub


End Class