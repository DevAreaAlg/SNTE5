Imports System.Data
Imports System.Data.SqlClient
Public Class REP_CRED_PROX_PAGOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Próximos Pagos", "Próximos Pagos")

        If Not Me.IsPostBack Then
            'LLENO COMBO CON LOS TIPOS DE PLAN DE PAGO DISPONIBLES PARA EL PRODUCTO
            Llenar_Tipo_prod()
            llena_sucursales()

        End If
    End Sub



    Private Sub Llenar_Tipo_prod()

        cmb_tipo_rep.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_tipo_rep.Items.Add(elija)

        Dim cred As New ListItem("CREDITO", "CRE")
        cmb_tipo_rep.Items.Add(cred)


    End Sub

    Private Sub llena_sucursales()
        cmb_suc.Items.Clear()
        cmb_suc.Items.Add(New ListItem("ELIJA", -1))
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_suc.Items.Add(New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("IDSUC").Value.ToString))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Protected Sub lnk_genera_rep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_genera_rep.Click
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dttxt As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 3, cmb_tipo_rep.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_INI", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_ini.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA_FIN", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_fin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SUC", Session("adVarChar"), Session("adParamInput"), 11, CInt(cmb_suc.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_PROX_PAGOS"

        Session("rs") = Session("cmd").Execute()

        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dttxt, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For Each Renglon As Data.DataRow In dttxt.Rows

            For i = 0 To dttxt.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString)

            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + IIf(cmb_tipo_rep.SelectedItem.Value = "CRE", "Próximas amortizaciones.csv", "Proximos pagos plazo fijo.csv"))
        context.Response.End()


    End Sub

End Class