Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient

Public Class CRED_VEN_DESCUENTOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Pólizas Contables", "Pólizas contables")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            CargaPeriodos()
        End If

    End Sub

    Private Sub CargaPeriodos()

        Try
            Dim cw As New ControlsWeb()
            cw.LlenaDropDownList(cmb_Quincenas, "SEL_FECHAS_CICLO_ACTIVO", 0, "FECHA", "FECHADESC", "ELIJA")

        Catch ex As Exception
            lbl_status.Text = ex.Message.ToString()
        End Try

    End Sub

    Protected Sub cmb_Quincenas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Quincenas.SelectedIndexChanged

        If cmb_Quincenas.SelectedItem.Value = "ELIJA" Then
            btn_GetPolizaDescuentos.Visible = False
        Else
            btn_GetPolizaDescuentos.Visible = True
        End If


    End Sub
    Protected Sub btn_btn_GetPolizaDescuentos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GetPolizaDescuentos.Click

        DescargaPolizaMovimientos()

    End Sub
    Private Sub DescargaPolizaMovimientos()

        Dim Encabezado As String
        Dim Fecha, FechaAux, NumPoliza As String
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dttxt As New Data.DataTable()
        Dim i As Integer
        Dim context As HttpContext = HttpContext.Current

        FechaAux = cmb_Quincenas.SelectedItem.ToString()
        Fecha = Right(cmb_Quincenas.SelectedItem.ToString(), 4)
        FechaAux = Left(cmb_Quincenas.SelectedItem.ToString(), 5)
        Fecha = Fecha + Right(FechaAux, 2)
        FechaAux = Left(FechaAux, 2)
        Fecha = Fecha + FechaAux

        NumPoliza = "000000" + tbx_numPoliza.Text
        Encabezado = "P " + Fecha + " 1 " + NumPoliza + " 1 000 MOVIMIENTOS QUINCENALES                                                                              1 2 "

        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default

        context.Response.Write(Encabezado)
        context.Response.Write(Environment.NewLine)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NUMPOLIZA", Session("adVarChar"), Session("adParamInput"), 10, tbx_numPoliza.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHAREPORTE", Session("adVarChar"), Session("adParamInput"), 10, cmb_Quincenas.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REPORTE_QUINCENAL_CONTPAQ"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dttxt, Session("rs"))
        Session("Con").Close()

        For Each Renglon As Data.DataRow In dttxt.Rows

            For i = 0 To dttxt.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString)

            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteQuincenalContabilidadContpaq" + Fecha + ".txt")
        context.Response.End()

    End Sub

End Class