Public Class PLD_ALE_PERFIL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("PLD", "CONSULTA PLD")

        If Not Me.IsPostBack Then
            lnk_PersonaPolitica.Attributes.Add("OnClick", "his_PPE()")

            If Session("PERSONAID") <> Nothing Then
                txt_cliente.Text = Session("PERSONAID")
                Llenadatos()
            End If
        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
        End If

    End Sub

    Protected Sub lnk_Regresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Regresar.Click

        Session("idperbusca") = Nothing
        Session("PERSONAID") = Nothing
        Response.Redirect("~/Index.aspx")

    End Sub


    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_seleccionar.Click

        Session("PERSONAID") = CInt(txt_cliente.Text)
        Llenadatos()

    End Sub

    Private Sub Llenadatos()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CLIENTES_COMPLETOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            If Session("rs").Fields("COND").Value.ToString = "0" Then
                lbl_info.Text = "El numero de cliente introducido no existe o no es un cliente completo."
                Session("Con").Close()
                Limpiar()
                Exit Sub
            End If

            lnk_HistorialAlertas.Enabled = True
            lnk_PersonaPolitica.Enabled = True
            lnk_HistorialPerfiles.Enabled = True

            Session("PROSPECTO") = Session("rs").Fields("NOMBRE").Value.ToString
            lbl_clienteB.Text = "" + Session("PROSPECTO")
            lbl_info.Text = ""
        End If

        Session("Con").Close()

        LlenaDatosPerfil()

    End Sub

    Private Sub LlenaDatosPerfil()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 10, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RIESGOS_MATRIZ_DATOS_PERFIL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            lbl_PerfilPersonaM.Text = Session("rs").Fields("PERFIL").Value.ToString
            lbl_TipoPerfilM.Text = Session("rs").Fields("TIPOPERFIL").Value.ToString

        End If

        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDFACTOR", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RIESGOS_MATRIZ_DATOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF
                Select Case Session("rs").Fields("CLAVEFACTOR").Value.ToString
                    Case "RGO ACTECO"
                        lbl_FactorActeco.Text = Session("rs").Fields("FACTOR").Value.ToString
                        lbl_PondActeco.Text = Session("rs").Fields("PONDERACION").Value.ToString + "%"
                        lbl_FactPerActeco.Text = Session("rs").Fields("FACTORPERSONA").Value.ToString
                        lbl_PuntActeco.Text = Session("rs").Fields("PUNTAJE").Value.ToString
                        lbl_PuntPondActeco.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                    Case "RGO PEP"
                        lbl_FactorPEP.Text = Session("rs").Fields("FACTOR").Value.ToString
                        lbl_PondPEP.Text = Session("rs").Fields("PONDERACION").Value.ToString + "%"
                        lbl_FactPerPEP.Text = Session("rs").Fields("FACTORPERSONA").Value.ToString
                        lbl_PuntPEP.Text = Session("rs").Fields("PUNTAJE").Value.ToString
                        lbl_PuntPondPEP.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                    Case "RGO PAIS"
                        lbl_FactorPais.Text = Session("rs").Fields("FACTOR").Value.ToString
                        lbl_PondPais.Text = Session("rs").Fields("PONDERACION").Value.ToString + "%"
                        lbl_FactPerPais.Text = Session("rs").Fields("FACTORPERSONA").Value.ToString
                        lbl_PuntPais.Text = Session("rs").Fields("PUNTAJE").Value.ToString
                        lbl_PuntPondPais.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                    Case "RGO FORMAJ"
                        lbl_FactorFormaJur.Text = Session("rs").Fields("FACTOR").Value.ToString
                        lbl_PondFormaJur.Text = Session("rs").Fields("PONDERACION").Value.ToString + "%"
                        lbl_FactPerFormaJur.Text = Session("rs").Fields("FACTORPERSONA").Value.ToString
                        lbl_PuntFormaJur.Text = Session("rs").Fields("PUNTAJE").Value.ToString
                        lbl_PuntPondFormaJur.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                    Case "RGO SERV"
                        lbl_FactorServicio.Text = Session("rs").Fields("FACTOR").Value.ToString
                        lbl_PondServicio.Text = Session("rs").Fields("PONDERACION").Value.ToString + "%"
                        lbl_FactPerServicio.Text = Session("rs").Fields("FACTORPERSONA").Value.ToString
                        lbl_PuntServicio.Text = Session("rs").Fields("PUNTAJE").Value.ToString
                        lbl_PuntPondServicio.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                    Case "TOTAL"
                        lbl_FactorTotal.Text = Session("rs").Fields("FACTOR").Value.ToString
                        lbl_PondTotal.Text = Session("rs").Fields("PONDERACION").Value.ToString + "%"
                        lbl_FactPerTotal.Text = ""
                        lbl_PuntTotal.Text = ""
                        lbl_PuntPondTotal.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                    Case "GRADRIESGO"
                        lbl_GradoRM.Text = Session("rs").Fields("FACTORPERSONA").Value.ToString + " (" + Session("rs").Fields("PONDERACION").Value.ToString + " - " + Session("rs").Fields("PUNTAJE").Value.ToString + ")"
                        lbl_PuntajeM.Text = Session("rs").Fields("CALIFICACION").Value.ToString
                End Select

                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Private Sub Limpiar()

        lnk_HistorialAlertas.Enabled = False
        lnk_PersonaPolitica.Enabled = False
        lnk_HistorialPerfiles.Enabled = False

        lbl_clienteB.Text = ""

        lbl_PerfilPersonaM.Text = ""
        lbl_TipoPerfilM.Text = ""
        lbl_GradoRM.Text = ""
        lbl_PuntajeM.Text = ""

        lbl_FactorActeco.Text = ""
        lbl_PondActeco.Text = ""
        lbl_FactPerActeco.Text = ""
        lbl_PuntActeco.Text = ""
        lbl_PuntPondActeco.Text = ""

        lbl_FactorPEP.Text = ""
        lbl_PondPEP.Text = ""
        lbl_FactPerPEP.Text = ""
        lbl_PuntPEP.Text = ""
        lbl_PuntPondPEP.Text = ""

        lbl_FactorPais.Text = ""
        lbl_PondPais.Text = ""
        lbl_FactPerPais.Text = ""
        lbl_PuntPais.Text = ""
        lbl_PuntPondPais.Text = ""

        lbl_FactorFormaJur.Text = ""
        lbl_PondFormaJur.Text = ""
        lbl_FactPerFormaJur.Text = ""
        lbl_PuntFormaJur.Text = ""
        lbl_PuntPondFormaJur.Text = ""

        lbl_FactorServicio.Text = ""
        lbl_PondServicio.Text = ""
        lbl_FactPerServicio.Text = ""
        lbl_PuntServicio.Text = ""
        lbl_PuntPondServicio.Text = ""

        lbl_FactorTotal.Text = ""
        lbl_PondTotal.Text = ""
        lbl_FactPerTotal.Text = ""
        lbl_PuntTotal.Text = ""
        lbl_PuntPondTotal.Text = ""

    End Sub

    Protected Sub lnk_HistorialAlertas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_HistorialAlertas.Click

        Session("VENGODE") = "PLD_ALE_PERFIL.aspx"
        Response.Redirect("PLD_ALE_HISTORIAL.aspx")

    End Sub

    Protected Sub lnk_HistorialPerfiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_HistorialPerfiles.Click

        ExcelHistorialMatrizRiesgo()

    End Sub

    Private Sub ExcelHistorialMatrizRiesgo()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtConsulta As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RIESGOS_MATRIZ_HISTORIAL"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtConsulta, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default

        Dim i As Integer
        Dim x As Integer = 0

        For i = 2 To dtConsulta.Columns.Count - 1

            context.Response.Write(dtConsulta.Columns(i).Caption + ",")
        Next

        context.Response.Write(Environment.NewLine)

        For Each Renglon As Data.DataRow In dtConsulta.Rows

            For i = 2 To dtConsulta.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            If Renglon.Item(0).ToString = "GRADRIESGO" Then
                context.Response.Write(Environment.NewLine)
            End If
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Output" + ".csv")
        context.Response.End()

    End Sub

End Class