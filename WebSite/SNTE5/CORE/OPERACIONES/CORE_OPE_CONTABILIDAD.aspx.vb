Imports System.Data.SqlClient
Imports System.IO
Public Class CORE_OPE_CONTABILIDAD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            LlenaTipo()
            LlenaPeriodo()

        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Administración de Contabilidad", "Contabilidad")

    End Sub

    Private Sub LlenaTipo()

        Try
            Dim cw = New ControlsWeb()
            cw.LlenaDropDownList(cmb_Tipo, "SEL_POLIZAS_CONTABLES", 0, "NOMBRE", "ID", "ELIJA")

        Catch ex As Exception
            lbl_status.Text = ex.Message.ToString()
        End Try

    End Sub

    Private Sub LlenaPeriodo()

        Try
            Dim cw = New ControlsWeb()
            cw.LlenaDropDownList(cmb_Qna, "SEL_FECHAS_POLIZA", 0, "FECHA", "FECHADESC", "ELIJA")

        Catch ex As Exception
            lbl_status.Text = ex.Message.ToString()
        End Try

    End Sub

    Protected Sub lnk_generar_conta_Click(sender As Object, e As EventArgs) Handles btn_Generar.Click
        Try
            Dim dsDatos As String = ""
            Dim Hsh As Hashtable = New Hashtable()

            Hsh.Add("@TIPO", cmb_Tipo.SelectedItem.Value())
            Hsh.Add("@PERIODO", cmb_Qna.SelectedItem.Value())
            Hsh.Add("@IDUSER", Session("USERID"))
            Hsh.Add("@SESION", Session("Sesion"))

            Dim da = New DataAccess()
            dsDatos = da.RegresaUnaCadena("INS_GENERA_CONTABILIDAD", Hsh)

            If dsDatos = "OK" Then
                lbl_status.Text = "Contabilidad generada correctamente"
            Else
                lbl_status.Text = "Ha ocurrido un error al generar la contabilidad"
            End If
        Catch ex As Exception
            lbl_status.Text = ex.Message()
        End Try
    End Sub

    Protected Sub btn_EXCEL_Click() Handles btn_EXCEL.Click

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtDivisas As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Tipo.SelectedItem.Value())
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 20, cmb_Qna.SelectedItem.Value())
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_POLIZA_CONTABLE_EXCEL"
        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtDivisas, Session("rs"))

        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtDivisas.Columns.Count - 1
            context.Response.Write(dtDivisas.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtDivisas.Rows

            For i = 0 To dtDivisas.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Contabilidad" + ".csv")
        context.Response.End()

    End Sub

    Protected Sub btn_poliza_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_txt.Click

        lbl_status.Text = ""
        'If txt_secuencia.Text <> "" Then
        DescargaTXTLayout()
        'Else
        '    lbl_status.Text = "Error: Capture número de secuencia."
        'End If

    End Sub

    Private Sub DescargaTXTLayout()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtRegistros As New DataTable()
        Dim NombreTXT As String = "poliza_"

        Dim dtBancos As New DataTable()
        dtBancos.Columns.Add("FOLIO", GetType(Integer))
        dtBancos.Columns.Add("ID_PERSONA", GetType(Integer))
        dtBancos.Columns.Add("CLAVE_EXPEDIENTE", GetType(String))
        dtBancos.Columns.Add("MONTO", GetType(Decimal))

        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()

                Dim insertCommand As New SqlCommand("SEL_POLIZA_CONTABLE_TXT", connection)
                insertCommand.CommandTimeout = 6000
                insertCommand.CommandType = CommandType.StoredProcedure


                Session("parm") = New SqlParameter("TIPO", SqlDbType.Int)
                Session("parm").Value = cmb_Tipo.SelectedItem.Value()
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("PERIODO", SqlDbType.VarChar, 20)
                Session("parm").Value = cmb_Qna.SelectedItem.Value()
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("FOLIO", SqlDbType.VarChar, 10)
                Session("parm").Value = txt_num.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("CODIGO", SqlDbType.VarChar, 100)
                Session("parm").Value = txt_concepto.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("CONCEPTO", SqlDbType.VarChar, 100)
                Session("parm").Value = txt_nombre.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("REFERENCIA", SqlDbType.VarChar, 51)
                Session("parm").Value = txt_referencia.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("USERID", SqlDbType.Int)
                Session("parm").Value = Session("USERID").ToString
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar, 20)
                Session("parm").Value = Session("Sesion").ToString
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dtRegistros.Load(myReader)

                Dim context As HttpContext = HttpContext.Current
                context.Response.Clear()
                context.Response.ContentEncoding = Encoding.Default

                For Each Renglon As DataRow In dtRegistros.Rows
                    context.Response.Write(Renglon.Item(0).ToString)
                    context.Response.Write(Environment.NewLine)
                Next

                context.Response.ContentType = "text/csv"
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreTXT + Session("FechaSis").ToString + ".txt")
                context.Response.End()

                myReader.Close()

            End Using
            lbl_status.Text = ""

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            'lbl_status.Text = "Se ha generado la póliza contable."
        End Try

    End Sub

End Class