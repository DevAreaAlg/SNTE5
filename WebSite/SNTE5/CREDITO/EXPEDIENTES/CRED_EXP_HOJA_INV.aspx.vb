Public Class CRED_EXP_HOJA_INV
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If    ' Split string oara obtener el resultado y el nombre del manual

            '   img.Attributes.Add("onclick", "window.open('Manuales/" + resultado(1) + "');")
            llenadatos()
            LlenaServicios()
        End If

    End Sub

    Private Sub llenadatos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_HOJA"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_sucursalt.Text = Session("rs").Fields("SUCURSAL").Value.ToString
            lbl_clientet.Text = Session("PERSONA")
            lbl_fechat.Text = Session("FechaSis")
            lbl_nombret.Text = Session("rs").Fields("NOMBRE").Value.ToString
            lbl_calleynumt.Text = (Session("rs").Fields("CALLE").Value.ToString + " " + Session("rs").Fields("NUMERO").Value.ToString)
            lbl_municipiot.Text = Session("rs").Fields("MUNICIPIO").Value.ToString
            lbl_estador.Text = Session("rs").Fields("ESTADO").Value.ToString
            lbl_cpt.Text = Session("rs").Fields("CP").Value.ToString
            lbl_edocivilt.Text = Session("rs").Fields("ESTADOCIVIL").Value.ToString
            lbl_nombreconyuget.Text = Session("rs").Fields("NOMBRECONYUGE").Value.ToString
            If Session("rs").Fields("TIPOVIVIENDA").Value.ToString = "FAM" Then
                lbl_tipoviviendat.Text = "FAMILIAR"
            End If
            If Session("rs").Fields("TIPOVIVIENDA").Value.ToString = "REN" Then
                lbl_tipoviviendat.Text = "RENTADA"
            End If
            If Session("rs").Fields("TIPOVIVIENDA").Value.ToString = "PRO" Then
                lbl_tipoviviendat.Text = "PROPIA"
            End If
            If Session("rs").Fields("TIPOVIVIENDA").Value.ToString = "HIP" Then
                lbl_tipoviviendat.Text = "HIPOTECADA"
            End If
            lbl_empresat.Text = Session("rs").Fields("NOMBREEMPRESA").Value.ToString
            lbl_puestot.Text = Session("rs").Fields("PUESTO").Value.ToString
        End If

        Session("Con").Close()
    End Sub

    Private Sub LlenaServicios()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_GASTOS_SE"
        Session("rs") = Session("cmd").Execute()


        Dim i As Integer
        Dim gastos(100) As Label
        Dim lines(100) As Label
        Dim cr As Literal
        Dim s As Literal
        Dim s2(100) As Literal
        'Dim s3(100) As Literal
        s = New Literal
        'lines.Text = "________"
        s.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        's2.Text = "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        's3.text = "&nbsp;&nbsp;"
        i = 0
        pnl_servicios.Controls.Add(s)
        Do While Not Session("rs").EOF
            gastos(i) = New Label
            lines(i) = New Label
            'cr(i) = New Literal   
            gastos(i).CssClass = "texto"
            gastos(i).Font.Size = FontSize.Medium

            gastos(i).Text = Session("rs").Fields("CATCONCEPGTOS_GASTO").Value.ToString
            lines(i).Text = "______"

            'lines(i).CssClass = "texto"
            'cr(i).Text = "<br />"
            gastos(i).Width = 120
            pnl_servicios.Controls.Add(gastos(i))
            pnl_servicios.Controls.Add(lines(i))
            'pnl_servicios.Controls.Add(cr(i))

            's2(i).Text = "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            's3(i).Text = "<br /><br />&nbsp;&nbsp;"
            i = i + 1
            s2(i) = New Literal
            If (i Mod 4) = 0 Then
                s2(i).Text = "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                pnl_servicios.Controls.Add(s2(i))
            Else
                s2(i).Text = "&nbsp;&nbsp;&nbsp;"
                pnl_servicios.Controls.Add(s2(i))
            End If
            'pnl_servicios.Controls.Add(total(i))
            'pnl_servicios.Controls.Add(totalgastos(i))
            Session("rs").movenext()

        Loop

        Session("Con").Close()

        'Session("montos") = montos


    End Sub

End Class