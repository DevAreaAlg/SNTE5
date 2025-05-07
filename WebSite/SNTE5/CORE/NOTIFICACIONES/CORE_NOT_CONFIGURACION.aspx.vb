Imports System.Data.SqlClient
Public Class CORE_NOT_CONFIGURACION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Notificaciones", "CONFIGURACIÓN NOTIFICACIONES")

            'establezco el valor de la a label que contiene el número de notificación 
            If Session("ID_CATNOT") Then
                txt_id_not.Text = Session("ID_CATNOT")
                obtenerDatos()
                obtenerRoles()
            Else
                Response.Redirect("CORE_NOT_NOTIFICACIONES_ADM.aspx")
            End If
        End If
    End Sub
#Region "Obtener datos notificación"
    Private Sub obtenerDatos()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_CATNOT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CATNOTIFICACIONES_DATOS"
        Session("rs") = Session("cmd").Execute()
        txt_clave_notif.Text = Session("rs").Fields("CLAVE").Value.ToString
        ckb_activo.Checked = CBool(Session("rs").Fields("ESTATUS").Value)
        txt_descrip_notif.Text = Session("rs").Fields("DESCRIPCION").Value.ToString
        txt_nombre_notif.Text = Session("rs").Fields("NOMBRE").Value.ToString
        txt_text_notif.Text = Session("rs").Fields("TEXTO").Value.ToString
        txt_tiempo_notif.Text = Session("rs").Fields("TIEMPO").Value.ToString

        Session("Con").Close()





    End Sub
#End Region
#Region "Obtener roles relacionadaso con la notificación"
    Private Sub obtenerRoles()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_CATNOT", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_CATNOT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CATNOTIFICACIONES_ROLES"
        Session("rs") = Session("cmd").Execute()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtra As New Data.DataTable()
        custDA.Fill(dtra, Session("rs"))
        If dtra.Rows(0).Item("REL") Like 1 Then
            pnl_roles.Visible = "true"
            dag_rol_rel.DataSource = dtra
            dag_rol_rel.DataBind()
        Else

        End If
        lbl_roles_rel.Text = dtra.Rows(0).Item("REL_TXT")
        Session("Con").Close()



    End Sub
#End Region
#Region "btn guardar roles asignados click"
    Protected Sub btn_guardarRolesAsig_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If lbl_rolAsigEstatus.Visible And Not lbl_rolAsigEstatus.CssClass.Contains(" hide_show") Then
                lbl_rolAsigEstatus.CssClass = lbl_rolAsigEstatus.CssClass & " hide_show"
            End If
            Dim dt_tosend As New Data.DataTable
            dt_tosend.Columns.Add("IDCONCEPTO", Type.GetType("System.Int32"))
            dt_tosend.Columns.Add("VALOR", Type.GetType("System.Int32"))
            For Each row As GridViewRow In dag_rol_rel.Rows
                Dim row2add As Data.DataRow = dt_tosend.NewRow
                row2add("IDCONCEPTO") = row.Cells(0).Text
                row2add("VALOR") = Convert.ToInt32(TryCast(row.Cells(3).Controls(1), CheckBox).Checked)
                dt_tosend.Rows.Add(row2add)
            Next
            Dim newdt_roles As New Data.DataTable

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)
                connection.Open()
                Try
                    Dim insertCommand As New SqlCommand("UPD_CATNOTIFICACIONES_ROLES", connection)
                    insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                    Session("parm") = New SqlParameter("ID_CATNOT", Data.SqlDbType.Int)
                    Session("parm").Value = Session("ID_CATNOT")
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("ROLES", Data.SqlDbType.Structured)
                    Session("parm").Value = dt_tosend
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("ID_USER", Data.SqlDbType.Int)
                    Session("parm").Value = Session("USERID")
                    insertCommand.Parameters.Add(Session("parm"))

                    Session("parm") = New SqlParameter("SESION", Data.SqlDbType.VarChar)
                    Session("parm").Value = Session("Sesion")
                    insertCommand.Parameters.Add(Session("parm"))

                    Dim sqlread As SqlDataReader = insertCommand.ExecuteReader()

                    newdt_roles.Load(sqlread)
                    dag_rol_rel.DataSource = newdt_roles
                    dag_rol_rel.DataBind()
                    If newdt_roles.Rows.Count > 0 Then
                        If newdt_roles.Rows(0)("AVISO") = 0 Then
                            lbl_rolAsigEstatus.Text = "Error al asignar roles a la notificación."
                        Else
                            lbl_rolAsigEstatus.Text = "Guardado correctamente"
                        End If
                    Else
                        lbl_rolAsigEstatus.Text = "Error al asignar roles a la notificación."
                    End If



                Catch ex As Exception
                    lbl_rolAsigEstatus.Text = ex.ToString
                Finally
                    connection.Close()
                End Try



            End Using
        Catch ex As Exception
            lbl_rolAsigEstatus.Text = ex.ToString
        End Try

        lbl_rolAsigEstatus.Visible = True

    End Sub
#End Region
#Region "guarder datos"
    Sub guardarDatos(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar.Click

        If lbl_men.Visible Then
            Dim pcl = lbl_men.CssClass
            If InStr(pcl, "hide_show") > 0 Then
                pcl = pcl.Remove(InStr(pcl, " hide_show") - 1)
            End If
            lbl_men.CssClass = pcl & " hide_show"
        Else
            lbl_men.Visible = True
        End If
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_CATNOT", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_CATNOT"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, Convert.ToInt32(ckb_activo.Checked))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIEMPO", Session("adVarChar"), Session("adParamInput"), 10, txt_tiempo_notif.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CATNOTIFICACIONES_DATOS"
        Session("rs") = Session("cmd").Execute()
        lbl_men.Text = Session("rs").Fields("RES").Value
        Session("Con").Close()





    End Sub
#End Region

End Class