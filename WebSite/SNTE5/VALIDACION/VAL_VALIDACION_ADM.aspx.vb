Imports System.Data.SqlClient

Public Class VAL_VALIDACION_ADM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Expedientes por Validar", "Expedientes por Validar")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            LlenarColaValidacion() 'Llena la cola de validacion para que los validadores eligan un expediente
            AdminValidar()
        End If

    End Sub

    Private Sub LlenarColaValidacion()
        'Se llena la cola de validacion con los expedientes que se encuantran en "proceso"

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtColaValidacion As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_COLA_VALIDACION"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtColaValidacion, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_cola1.DataSource = dtColaValidacion
        DAG_cola1.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub AdminValidar()
        'Se llena la tabla de expedientes de un validador con los expedientes que el tomo para su validacion
        'Se actualizara constantemente en caso de expedientes con docuemntos que se volvieron a digitalizar

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtColaValidacion As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCS_VALIDADOR"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtColaValidacion, Session("rs"))
        'se vacian los expedientes al formulario
        DAG_Cola.DataSource = dtColaValidacion
        DAG_Cola.DataBind()

        Session("Con").Close()

    End Sub

    Private Function CandadoValidacion(ByVal folio As String, ByVal Usuario As String) As Boolean
        Dim respuesta As Boolean
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Usuario)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCS_VALIDADOR_BLOQUEADOS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("BLOQUEADO").Value.ToString = "0" Then
            respuesta = True
        Else
            respuesta = False
        End If

        Session("Con").Close()

        Return respuesta

    End Function

    Private Sub DAG_COLA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_Cola.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor
        Session("TIPO") = e.Item.Cells(9).Text
        Dim Estatus As String = e.Item.Cells(3).Text
        Session("CVEEXPE") = e.Item.Cells(1).Text

        If (e.CommandName = "VALIDAR") Then

            If Session("TIPO") = "CRED" Or Session("TIPO") = "CAP" Then
                Session("PERSONAID") = e.Item.Cells(2).Text
                Session("FOLIO") = e.Item.Cells(0).Text
                If CandadoValidacion(Session("FOLIO"), Session("USERID").ToString) = True Then
                    lbl_status.Text = ""
                    Response.Redirect("/VALIDACION/VAL_VALIDACION_EXP.aspx")
                Else
                    lbl_status.Text = "Error: El expediente está en uso por otro usuario"
                End If

            Else

                Session("IDCARTA") = e.Item.Cells(6).Text
                Session("FOLIO") = e.Item.Cells(0).Text
                Session("PERSONAID") = e.Item.Cells(1).Text

                If CandadoValidacion(Session("FOLIO"), Session("USERID").ToString) = True Then
                    Response.Redirect("ValidacionExterna.aspx")
                Else
                    lbl_status.Text = "Error: El expediente está en uso por otro usuario"
                End If

            End If

        End If


    End Sub

    Protected Sub DAG_COLA1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_cola1.ItemDataBound

        e.Item.Cells(9).Visible = False
        e.Item.Cells(10).Visible = False 'TIPO DE VALIDACION CREDITO,DISP,CCRED

    End Sub

    Protected Sub DAG_COLA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_Cola.ItemDataBound

        e.Item.Cells(8).Visible = False
        e.Item.Cells(9).Visible = False 'TIPO DE VALIDACION CREDITO,DISP,CCRED

    End Sub

    Private Sub DAG_COLA1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_cola1.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "VALIDAR") Then

            Session("FOLIO") = e.Item.Cells(0).Text
            Session("CVEEXPE") = e.Item.Cells(1).Text
            Session("PERSONAID") = e.Item.Cells(2).Text


            If CandadoValidacion(Session("FOLIO"), Session("USERID").ToString) = True Then

                'Actualizacion de los datos de un expediente en la cola de validacion cuando es acaparado por un validador
                Session("Con").Open()
                Session("cmd") = New ADODB.Command()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_COLA_VALIDACION_VALIDAR"
                Session("rs") = Session("cmd").Execute()
                Session("Con").Close()

                Session("TIPO") = e.Item.Cells(10).Text

                Select Case Session("TIPO")
                    Case "CREDITO"
                        Response.Redirect("/VALIDACION/VAL_VALIDACION_EXP.aspx")
                    Case "CRED"
                        Response.Redirect("/VALIDACION/VAL_VALIDACION_EXP.aspx")
                    Case "CAP"
                        Response.Redirect("/VALIDACION/VAL_VALIDACION_EXP.aspx")
                    Case "DISP"
                        Session("IDDISP") = e.Item.Cells(7).Text
                        Response.Redirect("ValidacionExterna.aspx")
                    Case "CCRED"
                        Session("IDCARTA") = e.Item.Cells(7).Text
                        Response.Redirect("ValidacionExterna.aspx")
                End Select

                DAG_Cola.DataBind()
                LlenarColaValidacion()
            Else
                lbl_status.Text = "Error: El expediente está en uso por otro usuario"
            End If

        End If

    End Sub


    Protected Sub btn_actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_actualizar.Click
        lbl_status.Text = ""
        Response.Redirect("/VALIDACION/VAL_VALIDACION_ADM.aspx")

    End Sub

    Protected Sub btn_validar_Click()
        Dim custDA As New OleDb.OleDbDataAdapter()


        Dim dtFolios As New DataTable()
        dtFolios.Columns.Add("FOLIO", GetType(Integer))
        dtFolios.Columns.Add("CONFIRMA", GetType(Decimal))

        For i As Integer = 0 To DAG_cola1.Items.Count() - 1
            If Convert.ToInt32(DirectCast(DAG_cola1.Items(i).FindControl("chk_seleccionado"), CheckBox).Checked) = 1 Then
                dtFolios.Rows.Add(Convert.ToInt32(DirectCast(DAG_cola1.Items(i).FindControl("FOLIO"), Label).Text),
                                 1)
            End If
        Next

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()

                Dim insertCommand As New SqlCommand("UPD_VALIDA_MULTIPLE", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("FOLIOS", SqlDbType.Structured)
                Session("parm").Value = dtFolios
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("USERID", SqlDbType.Int)
                Session("parm").Value = Session("USERID").ToString
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar, 20)
                Session("parm").Value = Session("Sesion").ToString
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)

            End Using

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            LlenarColaValidacion()
        End Try
    End Sub

    Private Sub ckb_todos_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_todos.CheckedChanged
        If ckb_todos.Checked Then
            For i As Integer = 0 To DAG_cola1.Items.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_cola1.Items(i).FindControl("chk_seleccionado"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_cola1.Items.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_cola1.Items(i).FindControl("chk_seleccionado"), CheckBox)
                chkRow.Checked = 0
            Next
        End If
    End Sub

End Class