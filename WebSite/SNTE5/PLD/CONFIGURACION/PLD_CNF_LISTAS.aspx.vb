Public Class PLD_CNF_LISTAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            'PEPLlenaInstituciones()

        End If

        txt_PLD5IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_PLD5IdCliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_PLD5IdCliente.Text = Session("idperbusca")
            lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
            Session("idperbusca") = Nothing
        End If



    End Sub
    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Lista de personas políticamente expuestas", "LISTA DE PERSONAS POLÍTICAMENTE EXPUESTAS")
        If Not Me.IsPostBack Then
            PEPLlenaInstituciones()
        End If

        'txt_PLD5IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_PLD5IdCliente.ClientID + "','" + lnk_BusquedaPersona.ClientID + "')")
        'btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")
    End Sub
    Protected Sub lnk_EliminarFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_EliminarFiltro.Click

        cmb_ListaPEPInst.SelectedValue = "-1"
        txt_ListaPEPNomb.Text = ""
        txt_ListaPEPPat.Text = ""
        txt_ListaPEPMat.Text = ""
        txt_ListaPEPCargo.Text = ""
        txt_ListaPEPUnAdmin.Text = ""
        txt_PLD5IdCliente.Text = ""
        lbl_NombrePersonaBusqueda.Text = ""
        dag_ListaPEP.Visible = False
    End Sub

    Private Sub PEPLlenaInstituciones()

        cmb_ListaPEPInst.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_ListaPEPInst.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PLD_LISTA_PEP_INSTIUCION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("INST").Value, Session("rs").Fields("INST").Value.ToString)
                cmb_ListaPEPInst.Items.Add(item)
                Session("rs").movenext()
            Loop

        End If

        Session("Con").Close()

    End Sub


    Protected Sub btn_PEPExcel_Click() Handles btn_PEPExcel.Click
        ConsultaListaPEPExcel()

    End Sub

    'Borra el archivo temporal
    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub

    Private Sub ConsultaListaPEPExcel()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPLD5 As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 500, txt_ListaPEPNomb.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 500, txt_ListaPEPPat.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 500, txt_ListaPEPMat.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_ListaPEPInst.SelectedItem.Value = "-1" Then
            Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 1000, "")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 1000, cmb_ListaPEPInst.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("CARGO", Session("adVarChar"), Session("adParamInput"), 1000, txt_ListaPEPCargo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD_ADMIN", Session("adVarChar"), Session("adParamInput"), 1000, txt_ListaPEPUnAdmin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLD_LISTA_PEP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPLD5, Session("rs"))
        Session("Con").Close()

        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()
        context.Response.ContentEncoding = System.Text.Encoding.Default
        Dim i As Integer

        For i = 0 To dtPLD5.Columns.Count - 1

            context.Response.Write(dtPLD5.Columns(i).Caption + ",")
        Next
        context.Response.Write(Environment.NewLine)
        For Each Renglon As Data.DataRow In dtPLD5.Rows

            For i = 0 To dtPLD5.Columns.Count - 1
                context.Response.Write(Renglon.Item(i).ToString.Replace(",", String.Empty).Replace("&nbsp;", " ") + ",")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "LISTASPEP" + ".csv")
        context.Response.End()

    End Sub

    Private Sub ConsultaListaPEP()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPLD5 As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 500, txt_ListaPEPNomb.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 500, txt_ListaPEPPat.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 500, txt_ListaPEPMat.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If cmb_ListaPEPInst.SelectedItem.Value = "-1" Then
            Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 1000, "")
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 1000, cmb_ListaPEPInst.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("CARGO", Session("adVarChar"), Session("adParamInput"), 1000, txt_ListaPEPCargo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD_ADMIN", Session("adVarChar"), Session("adParamInput"), 1000, txt_ListaPEPUnAdmin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLD_LISTA_PEP"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPLD5, Session("rs"))
        Session("Con").Close()

        If dtPLD5.Rows.Count > 0 Then
            If dtPLD5.Rows.Count > 100 Then
                ConsultaListaPEPExcel()
            Else
                dag_ListaPEP.Visible = True
                dag_ListaPEP.DataSource = dtPLD5
                dag_ListaPEP.DataBind()
            End If
        Else
            dag_ListaPEP.Visible = False
            lbl_status.Text = "No existen registros"
        End If

    End Sub

    Protected Sub lnk_PEPConsular_Click(sender As Object, e As System.EventArgs) Handles lnk_PEPConsular.Click

        ConsultaListaPEP()

    End Sub

    Private Sub btn_buscapersona_Click(sender As Object, e As ImageClickEventArgs) Handles btn_buscapersona.Click

    End Sub
    Private Sub Llenapersona()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_PLD5IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'lbl_NombrePersonaBusqueda.Text = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = "" + Session("rs").Fields("PROSPECTO").Value.ToString
            Session("Con").Close()
            Exit Sub
        End If
        If Session("rs").Fields("PROSPECTO").Value.ToString = "-1" Then
            ' lbl_.Text = "El número de cliente introducido no existe"
            lbl_NombrePersonaBusqueda.Visible = False
            Session("Con").Close()
            Exit Sub
        End If
        lbl_NombrePersonaBusqueda.Visible = True
        lbl_NombrePersonaBusqueda.Text = "" + Session("rs").Fields("PROSPECTO").Value.ToString



        Session("Con").Close()
    End Sub

    Private Sub lnk_seleccionar_Click(sender As Object, e As EventArgs) Handles lnk_seleccionar.Click
        Session("PERSONAID") = CInt(txt_PLD5IdCliente.Text)
        Llenapersona()
    End Sub

End Class