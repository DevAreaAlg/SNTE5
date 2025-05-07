Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_PRESUPUESTOS
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Master, MasterMascore).CargaASPX("Presupuestos", "Presupuestos")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            CargaPeriodos()
            CargaSaldoActual()
        End If
    End Sub

    Private Sub CargaPeriodos()

        cmb_Quincenas.Items.Clear()
        Dim elija As New ListItem("ELIJA", "")
        cmb_Quincenas.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FECHAS_PRESUPUESTO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("FECHADESC").Value.ToString, Session("rs").Fields("FECHA").Value.ToString)
            cmb_Quincenas.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub


    Private Sub CargaSaldoActual()

        Dim SaldoActual As Decimal

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SALDO_ACTUAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            SaldoActual = Session("rs").Fields("SALDO_ACTUAL").value
            txt_saldoAct.Text = SaldoActual.ToString("C")

        End If
        Session("Con").Close()

    End Sub


    Protected Sub cmb_Quincenas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Quincenas.SelectedIndexChanged

        lbl_estatusPres.Text = ""
        txt_monto.Text = ""

        If cmb_Quincenas.SelectedItem.Value = "ELIJA" Then

        Else

            Session("FECHAQNA") = cmb_Quincenas.SelectedItem.Value
            PresupuestosAplicados()
        End If

    End Sub

    Protected Sub btn_guardar_pres_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar_pres.Click
        lbl_estatusPres.Text = ""
        CargaPresupuesto()
        CargaSaldoActual()
        PresupuestosAplicados()
        txt_monto.Text = ""
        txt_comisiones.Text = ""
    End Sub

    Private Sub CargaPresupuesto()
        Dim Presupuesto As Decimal = 0.00
        Dim Comisiones As Decimal = 0.00

        If txt_monto.Text = "" And txt_comisiones.Text = "" Then
            lbl_estatusPres.Text = "Error: Debe agregar al menos un Monto Presupuesto o un Monto Comisiones "
        Else
            If txt_monto.Text <> "" Then
                Presupuesto = txt_monto.Text
            End If
            If txt_comisiones.Text <> "" Then
                Comisiones = txt_comisiones.Text
            End If
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("PERIODO", Session("adVarChar"), Session("adParamInput"), 10, cmb_Quincenas.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 10, Presupuesto)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO_COMISIONES", Session("adVarChar"), Session("adParamInput"), 10, Comisiones)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_PRESUPUESTOS"
            Session("rs") = Session("cmd").Execute()

            'lbl_estatusPres.Text = "Se guardó correctamente"

            Session("Con").Close()
        End If



    End Sub

    Private Sub PresupuestosAplicados()

        Try
            'Se limpia el grid para que se recargue.
            dag_AuxPres.DataBind()

            Dim dt As DataTable = New DataTable()
            Dim Hsh As Hashtable = New Hashtable()
            Hsh.Add("@FECHA", Session("FECHAQNA"))
            Dim da As New DataAccess()
            dt = da.RegresaDataTable("SEL_PRESUPUESTOS", Hsh)
            dag_AuxPres.DataSource = dt
            dag_AuxPres.DataBind()

            If dag_AuxPres.Items.Count > 0 Then
                dag_AuxPres.Visible = True
            Else

                dag_AuxPres.Visible = False
            End If

        Catch ex As Exception
            lbl_estatusPres.Text = ex.Message.ToString()
        End Try




    End Sub


End Class