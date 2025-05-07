Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient


Public Class CRED_EXP_MESA_CTRL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Asignar Miembros", "Edición de Comités")
        If Not Me.IsPostBack Then
            'LLENO LOS COMBOS
            LlenaComites()
        End If

    End Sub

    'Muestra los comites
    Private Sub LlenaComites()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_comite.Items.Add(elija)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COMITES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("COMITE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_comite.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub cmb_comite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_comite.SelectedIndexChanged
        If cmb_comite.SelectedItem.Value.ToString <> "0" Then
            llena_modulos()
            VerificActivacion()
        Else
            Chk_ActivaDesactivar.Checked = False
            pnl_chkActivarDes.Visible = False
            pnl_dtgCom.Visible = False
        End If
    End Sub

    Private Sub llena_modulos()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ModulosGeneral As New Data.DataTable()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCOMIT", Session("adVarChar"), Session("adParamInput"), 20, cmb_comite.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMITES_MIEMBROS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ModulosGeneral, Session("rs"))
        Session("Con").Close()

        If ModulosGeneral.Rows.Count > 0 Then
            dag_comite.Visible = True
            dag_comite.DataSource = ModulosGeneral
            dag_comite.DataBind()
        Else
            dag_comite.Visible = False
        End If

    End Sub

    Private Sub VerificActivacion()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCOMIT", Session("adVarChar"), Session("adParamInput"), 20, cmb_comite.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMITES_MIEMBROS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("ACTIVO").Value.ToString = "2" Then
            Chk_ActivaDesactivar.Checked = True
            pnl_chkActivarDes.Visible = False
            lbl_StatusActivo.Text = ""
        ElseIf Session("rs").Fields("ACTIVO").Value.ToString = "1" Then
            Chk_ActivaDesactivar.Checked = True
            pnl_chkActivarDes.Visible = True
            'lbl_StatusActivo.Text = ""

        Else
            Chk_ActivaDesactivar.Checked = False
            pnl_chkActivarDes.Visible = True
            'lbl_StatusActivo.Text = "El comité no se encuentra activo."
        End If
        pnl_dtgCom.Visible = True
        Session("Con").Close()
    End Sub

    Protected Sub Chk_ActivaDesactivar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk_ActivaDesactivar.CheckedChanged
        ActivarDesactivarComite()
        VerificActivacion()
    End Sub

    Private Sub ActivarDesactivarComite()
        lbl_StatusActivo.Text = ""

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDCOMIT", Session("adVarChar"), Session("adParamInput"), 20, cmb_comite.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If Chk_ActivaDesactivar.Checked = True Then
            Session("parm") = Session("cmd").CreateParameter("ACTIVAR", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("ACTIVAR", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_ACTIVAR_COMITE"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            ViewState("RES") = Session("rs").Fields("RES").value.ToString
            If ViewState("RES") = 0 Then
                lbl_StatusActivo.Text = "Error: Aún no se han asignado usuarios al comité"
            Else
                If Chk_ActivaDesactivar.Checked = True Then
                    lbl_StatusActivo.Text = "Se ha activado el comité: " + cmb_comite.SelectedItem.Text
                Else
                    lbl_StatusActivo.Text = "Se ha desactivado el comité: " + cmb_comite.SelectedItem.Text
                End If
            End If
        End If
        Session("Con").Close()

    End Sub

    Protected Sub btn_asignar_Click(sender As Object, e As EventArgs)

        'Data table que se llena con los contenidos de los datagrids
        Dim dtModulos As New Data.DataTable()
        dtModulos.Columns.Add("IDG", GetType(Integer))
        dtModulos.Columns.Add("DESCRIPCION", GetType(String))
        dtModulos.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_comite.Rows.Count() - 1
            dtModulos.Rows.Add(CInt(dag_comite.Rows(i).Cells(0).Text), dag_comite.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_comite.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_COMITE_MIEMBRO", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("ID", SqlDbType.Int)
                Session("parm").Value = cmb_comite.SelectedItem.Value.ToString
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("MIEMBROS", SqlDbType.Structured)
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
                lbl_StatusActivo.Text = "Guardado correctamente"
            End Using
        Catch ex As Exception
            lbl_StatusActivo.Text = "Error"

        Finally
            llena_modulos()
            VerificActivacion()
        End Try
    End Sub


End Class