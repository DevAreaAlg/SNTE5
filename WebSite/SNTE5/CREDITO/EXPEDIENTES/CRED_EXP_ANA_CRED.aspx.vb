Public Class CRED_EXP_ANA_CRED
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Expedientes por Validar", "Expedientes por Validar")
        If Not Me.IsPostBack Then

            AdminAnalisisExpedientes()
            Session("VENGODEDIGGLOBAL") = Nothing
        End If

    End Sub


    Protected Sub btn_actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_actualizar.Click

        Response.Redirect("CRED_EXP_ANA_CRED.aspx")

    End Sub

    Private Sub AdminAnalisisExpedientes()
        'Se llena la tabla de expedientes que se generaron en la sucursal y aun no tiene decision de aprobado o rechazado
        'Se mostraran los expedientes que estan en validacio, faltan digitalizar documentos o contiene documentos rechazados

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtColaAnalisis As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXP_ANALISIS"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtColaAnalisis, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Analisis.DataSource = dtColaAnalisis
        DAG_Analisis.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub DAG_Analisis_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_Analisis.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        Dim Estatus As String = e.Item.Cells(4).Text
        Dim EstatusDigit As String = e.Item.Cells(7).Text
        Session("ESTATUS_EXPEDIENTE") = e.Item.Cells(3).Text

        If (e.CommandName = "ANALISIS1") Then
            If Estatus = "ANALISIS FASE 1" Then
                If EstatusDigit = "NO" Then
                    Session("FOLIO") = e.Item.Cells(0).Text
                    Session("PRODUCTO") = e.Item.Cells(1).Text
                    Session("PROSPECTO") = e.Item.Cells(3).Text
                    Response.Redirect("CRED_EXP_ANA_FASE1.aspx")
                Else
                    lbl_AlertaNoAcceso.Text = "Error: Este expediente actualmente está siendo utilizado"
                End If
            Else
                lbl_AlertaNoAcceso.Text = "Error: No puede modificar este expediente durante el estatus actual"
            End If
        End If

        DAG_Analisis.DataBind()
        AdminAnalisisExpedientes()

    End Sub

End Class