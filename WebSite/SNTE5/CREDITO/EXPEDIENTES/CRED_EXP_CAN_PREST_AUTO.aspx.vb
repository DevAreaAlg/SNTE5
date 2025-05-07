Imports System.Data
Imports System.Data.SqlClient

Public Class CRED_EXP_CAN_PREST_AUTO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Cancelación de préstamos", "Cancelación de préstamos")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            'LlenaInstituciones()
            LlenarCancelacion()
        End If
    End Sub

    'Private Sub LlenaInstituciones()
    '    ddl_Instituciones.Items.Clear()
    '    Dim elija As New ListItem("ELIJA", "-1")
    '    ddl_Instituciones.Items.Add(elija)

    '    Session("Con").Open()
    '    Session("cmd") = New ADODB.Command()
    '    Session("cmd").ActiveConnection = Session("Con")
    '    Session("cmd").CommandType = CommandType.StoredProcedure
    '    Session("cmd").CommandText = "SEL_INSTITUCIONES"
    '    Session("rs") = Session("cmd").Execute()

    '    Do While Not Session("rs").EOF
    '        Dim item As New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
    '        ddl_Instituciones.Items.Add(item)
    '        Session("rs").movenext()
    '    Loop

    '    Session("Con").Close()


    'End Sub


    Private Sub DAG_cancelacion_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_cancelacion.SelectedIndexChanged

        Session("FOLIO") = DirectCast(dag_cancelacion.Rows(0).FindControl("FOLIO"), Label).Text

        If (e.CommandName = "ELIMINAR") Then
            CancelaExpediente()

        End If

    End Sub

    Protected Sub btn_GetArchivoDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click

        Dim bandera As Integer = 0
        For i As Integer = 0 To dag_cancelacion.Rows.Count() - 1
            If Convert.ToInt32(DirectCast(dag_cancelacion.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then
                bandera = 1
                Exit For
            End If
        Next

        If bandera = 1 Then
            CancelaExpediente()
        Else
            lbl_cancelar.Text = "Error: No ha elegido préstamos para cancelar."
        End If

    End Sub



    Private Sub LlenarCancelacion()

        Dim dtcancelacion As New Data.DataTable()
        Dim dtinsti As New System.Data.OleDb.OleDbDataAdapter()

        dtcancelacion.Columns.Add("FOLIO", GetType(String))

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CANCELACION_PRESTAMOS"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        dtinsti.Fill(dtcancelacion, Session("rs"))
        'se vacian los expedientes al formulario
        dag_cancelacion.DataSource = dtcancelacion
        dag_cancelacion.DataBind()

        Session("Con").Close()

        If dag_cancelacion.Rows.Count > 0 Then
            btn_cancelar.Visible = True
            lbl_cancelar.Visible = True
        Else
            btn_cancelar.Visible = False
            lbl_cancelar.Visible = False
        End If

    End Sub

    'Protected Sub ddl_Instituciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Instituciones.SelectedIndexChanged
    '    Session("INSTI") = ddl_Instituciones.SelectedValue

    '    lbl_cancelar.Text = ""
    '    LlenarCancelacion()
    'End Sub


    '---------------------------------CANCELAR EXPEDIENTE--------------
    Private Sub CancelaExpediente()

        Dim dtinsti As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcancelacion As New Data.DataTable()
        'Dim dtDescuentos As New DataTable()

        dtcancelacion.Columns.Add("FOLIO", GetType(Integer))
        dtcancelacion.Columns.Add("APLICADO", GetType(Integer))

        For i As Integer = 0 To dag_cancelacion.Rows.Count() - 1

            If Convert.ToInt32(DirectCast(dag_cancelacion.Rows(i).FindControl("chk_Aplicado"), CheckBox).Checked) = 1 Then


                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Convert.ToInt32(DirectCast(dag_cancelacion.Rows(i).FindControl("FOLIO"), Label).Text))
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_CNFEXP_CANCELA_EXPEDIENTE_AUTORIZACION"
                Session("cmd").Execute()


                Session("Con").Close()

            End If
        Next

        lbl_cancelar.Text = "El préstamo fue cancelado"
        LlenarCancelacion()

    End Sub


End Class