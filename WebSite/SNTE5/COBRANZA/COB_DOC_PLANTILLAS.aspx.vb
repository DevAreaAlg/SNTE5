Imports System.Math
Imports System.Data.DataRow
Imports System.Data
Imports System.Data.SqlClient
Public Class COB_DOC_PLANTILLAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración plantillas", "Administración plantillas documentos de cobranza")
        If Not Me.IsPostBack Then

            LlenaEventos()
            Session("AUXILIAR1") = "-1"
        End If

    End Sub


    Protected Sub cmb_Eventos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Eventos.SelectedIndexChanged



        If cmb_Eventos.SelectedItem.Value = "0" Then
            dag_AsigPlantillas.Visible = False
            lbl_status.Text = ""
        ElseIf cmb_Eventos.SelectedItem.Value = Session("AUXILIAR1") Then

            LlenaPlantillasXEventos()
        Else
            LlenaPlantillasXEventos()
            lbl_status.Text = ""
        End If


    End Sub


    Private Sub LlenaPlantillasXEventos()



        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim PlantillasGeneral As New Data.DataTable()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDEVENTO", Session("adVarChar"), Session("adParamInput"), 10, cmb_Eventos.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_PLANTILLAS_X_EVENTO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(PlantillasGeneral, Session("rs"))
        Session("Con").Close()

        If PlantillasGeneral.Rows.Count > 0 Then
            dag_AsigPlantillas.Visible = True
            dag_AsigPlantillas.DataSource = PlantillasGeneral
            dag_AsigPlantillas.DataBind()
        Else
            dag_AsigPlantillas.Visible = False
        End If
        Session("AUXILIAR1") = cmb_Eventos.SelectedItem.Value

    End Sub


    Private Sub LlenaEventos()

        cmb_Eventos.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_Eventos.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COB_EVENTOS_PLANTILLAS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("EVENTO").Value.ToString, Session("rs").Fields("IDEVENTO").Value.ToString)
            cmb_Eventos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub


    Protected Sub btn_asignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_asignar.Click
        'Data table que se llena con los contenidos de los datagrids
        Dim dtPlantillas As New Data.DataTable()
        dtPlantillas.Columns.Add("IDPLANTILLA", GetType(Integer))
        dtPlantillas.Columns.Add("NOMBRE", GetType(String))
        dtPlantillas.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_AsigPlantillas.Rows.Count() - 1
            dtPlantillas.Rows.Add(CInt(dag_AsigPlantillas.Rows(i).Cells(0).Text), dag_AsigPlantillas.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_AsigPlantillas.Rows(i).FindControl("chk_PagAsignado"), CheckBox).Checked))
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_COB_ASIGNA_PLANTILLAS", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDEVENTO", SqlDbType.Int)
                Session("parm").Value = cmb_Eventos.SelectedItem.Value
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("PLANTILLA", SqlDbType.Structured)
                Session("parm").Value = dtPlantillas
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
                    lbl_status.Text = Session("AUXILIAR").ToString

                End While
                myReader.Close()

            End Using

        Catch ex As Exception

        Finally
            Session("AUXILIAR") = Nothing
            lbl_status.Text = "Guardado correctamente"
            LlenaPlantillasXEventos()
        End Try
    End Sub

End Class