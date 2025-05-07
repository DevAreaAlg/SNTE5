Imports System.IO
Public Class CORE_PER_PEN_ALI
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Pensión Alimenticia", "Demandados Pensión Alimenticia")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Permisos()
            CargarBeneficiarios()
        End If

    End Sub

    Private Sub Permisos()

        PermisoCalcularPensiones()
        PermisoAgregarEditarPension()
        PermisoCancelarPension()

    End Sub

    Private Sub PermisoCalcularPensiones()

        If TieneFacultad(Session("USERID"), "CALCPENALI") = True Then
            btn_calcular_pen.Enabled = True
        Else
            btn_calcular_pen.Enabled = False
        End If

    End Sub

    Private Sub PermisoCancelarPension()

        If TieneFacultad(Session("USERID"), "CANPENALIM") = True Then
            gvw_pensiones.Columns(10).Visible = True

        Else
            gvw_pensiones.Columns(10).Visible = False
        End If

    End Sub

    Private Sub PermisoAgregarEditarPension()

        If TieneFacultad(Session("USERID"), "ADDPENALIM") = True Then
            gvw_pensiones.Columns(9).Visible = True
            btn_nueva_pea.Enabled = True
        Else
            gvw_pensiones.Columns(9).Visible = False
            btn_nueva_pea.Enabled = False
        End If

    End Sub

    Private Function TieneFacultad(ByVal IDUSER As Integer, ByVal FACULTAD As String) As Boolean

        Dim da As New UNI_DataAccess
        da.AddParameters("IDUSER", IDUSER)
        da.AddParameters("FACULTAD", FACULTAD)
        Dim ds As DataSet = da.ExecuteReader("SEL_TIENE_FACULTAD")
        Dim respuesta As Boolean = ds.Tables(0).Rows(0).Item(0)
        Return respuesta

    End Function

    Private Sub CargarBeneficiarios()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim DtPensiones As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_DEMANDADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(DtPensiones, Session("rs"))
        Session("Con").Close()

        If DtPensiones.Rows.Count > 0 Then
            gvw_pensiones.DataSource = DtPensiones
            ViewState("Pensiones") = DtPensiones
            gvw_pensiones.DataBind()
        Else
            gvw_pensiones.DataSource = Nothing
            gvw_pensiones.DataBind()
        End If

    End Sub

    Private Sub gvw_pensiones_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvw_pensiones.PageIndexChanging

        gvw_pensiones.PageIndex = e.NewPageIndex
        gvw_pensiones.DataSource = ViewState("Pensiones")
        gvw_pensiones.DataBind()

    End Sub

    Protected Sub btn_busca_usuario_Click(sender As Object, e As EventArgs) Handles btn_busca_usuario.Click

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dteq As New DataTable()

        lbl_estatus.Text = ""

        If txt_nombre.Text = "" And txt_RFC.Text = "" Then

            lbl_estatus.Text = "Error: Debe ingresar al menos un parámetro de búsqueda"

        Else

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("RFC_USUARIO", Session("adVarChar"), Session("adParamInput"), 50, txt_RFC.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("id", Session("adVarChar"), Session("adParamInput"), 50, 0)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_FILTRO_PERSONA_DEMANDADO"
            Session("rs") = Session("cmd").Execute()
            custDA.Fill(dteq, Session("rs"))
            Session("Con").Close()
            If dteq.Rows.Count > 0 Then
                gvw_pensiones.DataSource = dteq
                ViewState("Pensiones") = dteq
                gvw_pensiones.DataBind()
            Else
                gvw_pensiones.DataSource = Nothing
                gvw_pensiones.DataBind()
                lbl_estatus.Text = "Error: No se encontraron coincidencias."
            End If

        End If

    End Sub

    Protected Sub btn_eliminarB_Click(sender As Object, e As EventArgs) Handles btn_eliminarB.Click

        lbl_estatus.Text = ""
        txt_nombre.Text = ""
        txt_RFC.Text = ""
        CargarBeneficiarios()

    End Sub

    Protected Sub btn_nueva_pea_Click(sender As Object, e As EventArgs) Handles btn_nueva_pea.Click

        Session("ID_PENSION") = "0"
        Session("ID_demandado") = "0"
        Response.Redirect("CORE_PER_PEN_ALI_AGREMIADO.aspx")

    End Sub

    Private Sub gvw_pensiones_RowCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs) Handles gvw_pensiones.RowCommand

        If (e.CommandName = "EDITAR") Then
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Session("ID_PENSION") = Convert.ToInt32(DirectCast(row.FindControl("ID"), Label).Text)
            Session("ID_demandado") = Convert.ToInt32(DirectCast(row.FindControl("ID"), Label).Text)
            Session("RFC_Demandado") = row.Cells(1).Text
            Response.Redirect("CORE_PER_PEN_ALI_AGREMIADO.aspx")
        End If

        If (e.CommandName = "VER") Then
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim NombreDoc As String = Convert.ToString(DirectCast(row.FindControl("NOMBRE_DOC"), Label).Text)
            VerDocumento(NombreDoc)
        End If


        If (e.CommandName = "ELIMINAR") Then
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Session("ID_PENSION") = Convert.ToInt32(DirectCast(row.FindControl("ID"), Label).Text)
            Session("ID_demandado") = Convert.ToInt32(DirectCast(row.FindControl("ID"), Label).Text)
            Session("RFC_Demandado") = row.Cells(1).Text
            pnl_modal_confirmar.Visible = True
            modal_confirmar.Show()

        End If
    End Sub

    Protected Sub btn_confirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_confirmar.Click

        CancelaPension()
        CargarBeneficiarios()

    End Sub

    Private Sub VerDocumento(ByVal NombreDoc As String)

        Dim Ruta As String = Server.MapPath("/DOCUMENTOS_DIGITALIZADOS/" + NombreDoc.ToString)

        If File.Exists(Ruta) Then

            lbl_estatus.Text = ""

            Try

                Dim fs As FileStream
                fs = File.Open(Ruta, FileMode.Open)
                Dim bytBytes(fs.Length) As Byte
                fs.Read(bytBytes, 0, fs.Length)
                fs.Close()

                Response.Buffer = True
                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", NombreDoc.ToString + ".pdf"))
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(bytBytes)
                Response.End()

            Catch ex As Exception
                lbl_estatus.Text = ex.ToString
            End Try
        Else
            lbl_estatus.Text = "Error: No se ha digitalizado, se ha movido o se ha eliminado el expediente."
        End If

    End Sub

    Protected Sub btn_calcular_pen_Click(sender As Object, e As EventArgs) Handles btn_calcular_pen.Click

        Try

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_ALL_RETENCION_PENSION_ALI"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()

            lbl_estatus.Text = "Éxito: Se han calculado las pensiones alimenticias."

        Catch ex As Exception
            lbl_estatus.Text = ex.ToString
        End Try

    End Sub

    Protected Sub btn_descargar_pen_Click(sender As Object, e As EventArgs) Handles btn_descargar_pen.Click

        DescargarReportePensiones()

    End Sub

    Private Sub DescargarReportePensiones()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtConsulta As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "REP_PENSIONES_ALIMENTICIAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = Encoding.Default

        Dim i As Integer
        For i = 0 To dtConsulta.Columns.Count - 1
            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As DataRow In dtConsulta.Rows
            For i = 0 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        Dim fechaSistema As Date = Convert.ToDateTime(Session("FechaSis"))

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Pensiones Alimenticias " + fechaSistema.Day.ToString + "." + fechaSistema.Month.ToString + "." + fechaSistema.Year.ToString + ".csv")
        context.Response.End()

    End Sub

    Private Sub CancelaPension()
        Try

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 50, Session("ID_PENSION"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 50, Session("RFC_Demandado"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 50, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_PENSION_ALI"
            Session("rs") = Session("cmd").Execute()


        Catch ex As Exception
            lbl_estatus.Text = "Error: No se ha podido eliminar la pensión" + ex.ToString
        Finally
            Session("Con").Close()
        End Try
    End Sub
End Class