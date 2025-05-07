Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CORE_CNF_ROLES_CREAR
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        TryCast(Me.Master, MasterMascore).CargaASPX("Edición de Roles", "Edición Roles")

        If Not Me.IsPostBack Then
            If Session("IDROL") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                Session("ID_CATEG_SEL") = -1
            End If
        End If
        Dim categ_control As New Categ_Ctrl()
        AddHandler categ_control.SelectedCat, AddressOf selectedCateg
        out_catg_ctrl.Controls.Add(categ_control)
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType, "ScriptsN", " $('.nodeCross').click(function(){$(this).toggleClass('active');$(this).parent().next('.subDiv').toggleClass('active');});", True)
    End Sub

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Not Me.IsPostBack Then

            If Session("IDROL") = -1 Then
                panel_roles.Visible = False
                btn_asignar.Visible = False
            ElseIf Session("IDROL") > 0 Then
                panel_roles.Visible = True
                btn_asignar.Visible = True

                llena_rol()
                llena_modulos()

            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            If Session("IDROL") = -1 Then
                txt_id.Text = "Nuevo rol"
                lbl_guardado.Visible = "False"
            ElseIf Session("IDROL") > 0 Then
                txt_id.Text = Session("IDROL")
                pnl_reportes.Attributes.CssStyle.Add("display", "block")
                reportes(Session("ID_CATEG_SEL"))
            End If

        End If
    End Sub

    Private Sub guardar_rol()

        Dim estatus As Integer
        If chk_estatus.Checked = False Then
            estatus = 0
        Else
            estatus = 1
        End If
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDROL", Session("adVarChar"), Session("adParamInput"), 10, Session("IDROL"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DESCRIPCION", Session("adVarChar"), Session("adParamInput"), 200, txt_descripcion.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 200, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 200, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_ROLES"
        Session("rs") = Session("cmd").Execute()
        Session("IDROL") = Session("rs").fields("IDROL").value.ToString
        ViewState("RES") = Session("rs").fields("RES").value.ToString
        txt_id.Text = Session("IDROL")
        panel_roles.Visible = True
        btn_asignar.Visible = True
        Session("Con").Close()
        llena_rol()
    End Sub

    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs)

        guardar_rol()
        llena_modulos()
        lbl_guardado.Visible = "True"
        If ViewState("RES") = 1 Or ViewState("RES") = 2 Then
            lbl_guardado.Text = "Guardado correctamente"
            pnl_reportes.Attributes.CssStyle.Add("display", "block")
            reportes(Session("ID_CATEG_SEL"))
        ElseIf ViewState("RES") = 3 Then
            lbl_guardado.Text = "Error: No puede activar este rol hasta asignarle permisos"
        End If

    End Sub

    Private Sub llena_rol()
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDROL", Session("adVarChar"), Session("adParamInput"), 50, Session("IDROL"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ROL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_nombre.Text = Session("rs").Fields("NOMBRE").value.ToString
            txt_descripcion.Text = Session("rs").Fields("DESCRIPCION").value.ToString
            If Session("rs").Fields("ESTATUS").value.ToString = "1" Then
                chk_estatus.Checked = True
            ElseIf Session("rs").Fields("ESTATUS").value.ToString = "0" Then
                chk_estatus.Checked = False
            End If
        End If
        Session("Con").Close()
    End Sub

    Private Sub llena_modulos()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ModulosGeneral As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDROL", Session("adVarChar"), Session("adParamInput"), 50, Session("IDROL"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MODULO_ROL"
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

    Protected Sub btn_asignar_Click(sender As Object, e As EventArgs)

        asigna_modulos()

    End Sub

    Protected Sub asigna_modulos()
        'Data table que se llena con los contenidos de los datagrids
        Dim dtModulos As New Data.DataTable()
        dtModulos.Columns.Add("IDG", GetType(Integer))
        dtModulos.Columns.Add("DESCRIPCION", GetType(String))
        dtModulos.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_mod_si.Rows.Count() - 1
            dtModulos.Rows.Add(CInt(dag_mod_si.Rows(i).Cells(0).Text), dag_mod_si.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_mod_si.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_MOD_ROL_ASIGNAR", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDROL", SqlDbType.Int)
                Session("parm").Value = Session("IDROL")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("MODULOS", SqlDbType.Structured)
                Session("parm").Value = dtModulos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))
                insertCommand.ExecuteNonQuery()
                connection.Close()
                lbl_permisos.Text = "Guardado correctamente"
            End Using

        Catch ex As Exception
            lbl_permisos.Text = "Error"

        Finally

            llena_modulos()
            UpdatePanelDatos.Update()
            llena_rol()

        End Try

    End Sub

#Region "REPORTES"
#Region "Obtención y actualización de reportes asignados asignados o no asignados a un rol para el rol."
    Private Sub reportes(ByVal id_cat As Integer)
        Try
            If lbl_repEstatus.Visible And Not lbl_repEstatus.CssClass.Contains(" hide_show") Then
                lbl_repEstatus.CssClass = lbl_repEstatus.CssClass & " hide_show"
            End If
            Dim dt_tosend As New Data.DataTable
            dt_tosend.Columns.Add("IDCONCEPTO", Type.GetType("System.Int32"))
            dt_tosend.Columns.Add("VALOR", Type.GetType("System.Int32"))
            For Each row As GridViewRow In grdVw_reportesAsig.Rows
                Dim row2add As Data.DataRow = dt_tosend.NewRow
                row2add("IDCONCEPTO") = row.Cells(0).Text
                row2add("VALOR") = Convert.ToInt32(TryCast(row.Cells(4).Controls(1), CheckBox).Checked)
                dt_tosend.Rows.Add(row2add)
            Next
            Dim rep As New Data.DataTable
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                connection.Open()
                Try
                    Dim insertCommand As New SqlCommand("UPD_REPORTES_ASIGNADOS", connection)
                    insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                    Session("parm") = New SqlParameter("ID_ROL", Data.SqlDbType.Int)
                    Session("parm").Value = Session("IDROL")
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("REPORTES", Data.SqlDbType.Structured)
                    Session("parm").Value = dt_tosend
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("ID_USER", Data.SqlDbType.Int)
                    Session("parm").Value = Session("USERID")
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("ID_CATEG", Data.SqlDbType.VarChar)
                    Session("parm").Value = id_cat
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("SESION", Data.SqlDbType.VarChar)
                    Session("parm").Value = Session("Sesion")
                    insertCommand.Parameters.Add(Session("parm"))

                    Dim sqlread As SqlDataReader = insertCommand.ExecuteReader()

                    rep.Load(sqlread)
                    grdVw_reportesAsig.DataSource = rep
                    grdVw_reportesAsig.DataBind()
                    If rep.Rows.Count > 0 Then
                        If rep.Rows(0)("AVISO") = 0 Then
                            lbl_repEstatus.Text = rep.Rows(0)("AVISO")
                        Else
                            lbl_repEstatus.Text = "Guardado correctamente"
                        End If
                    Else
                        lbl_repEstatus.Text = "Error al asignar reportes al rol."
                    End If



                Catch ex As Exception
                    lbl_repEstatus.Text = ex.ToString
                Finally
                    connection.Close()
                End Try



            End Using
        Catch ex As Exception
            lbl_repEstatus.Text = ex.ToString
        End Try






    End Sub
#End Region
#Region "Evento link para mostrar conrtrol para busuqeda de reportes por categoría."
    'Protected Sub lnk_BuscarRepXCateg_Click(sender As Object, e As EventArgs)
    '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "SrcripMuestraCateg", "$('#" & out_catg_ctrl.ClientID & "').show('666',null);", True)
    'End Sub
#End Region
#Region "Evento cuando se selecciona una categoría en el buscador de reportes por categoría."
    Protected Sub selectedCateg(ByVal sender As LinkButton, e As EventArgs)
        Session("ID_CATEG_SEL") = sender.Attributes("valor")
        lbl_repEstatus.Visible = False
    End Sub
#End Region
#Region "Evento link para mostrr todoslos reportes disponibles."
    'Protected Sub lnk_BuscarRepGen_Click(sender As Object, e As EventArgs)
    '    Session("ID_CATEG_SEL") = -1
    '    reportes(Session("ID_CATEG_SEL"))
    '    lbl_repEstatus.Visible = False
    '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "SrcripMuestraCateg", "if($('#" & out_catg_ctrl.ClientID & "').css('display') != 'none')$('#" & out_catg_ctrl.ClientID & "').hide('666',null);", True)
    'End Sub
#End Region
#Region "click guerdar reportes asignados"
    Protected Sub btn_guardarRep_Click(sender As Object, e As EventArgs)
        reportes(-1)
        lbl_repEstatus.Visible = True

    End Sub
#End Region
#End Region

End Class