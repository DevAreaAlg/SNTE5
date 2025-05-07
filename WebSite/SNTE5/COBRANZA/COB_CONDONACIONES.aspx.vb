Public Class COB_CONDONACIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Condonaciones", "Condonación de Interés / Capital / Comisiones")
        If Not Me.IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("Index.aspx")
            End If

            lbl_monto.Text = "Monto a condonar"
            lbl_confmonto.Text = "Confirmar monto"
        End If

        txt_cliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_cliente.ClientID + "','" + lnk_seleccionar.ClientID + "')")
        btn_buscapersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> "" Then
            txt_cliente.Text = Session("idperbusca")
            Session("idperbusca") = Nothing
        End If
    End Sub

    Protected Sub btn_seleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_seleccionar.Click
        lbl_Info.Text = ""
        Session("PERSONAID") = CInt(txt_cliente.Text)
        Llenadatos()
        cmb_folio.Enabled = True
        cmb_TipoCondonacion.Enabled = False
        txt_IntMoratorio.Enabled = False
        txt_IntMoratorioConf.Enabled = False
        txt_IntNormal.Enabled = False
        txt_IntNormalConf.Enabled = False
        txt_Capital.Enabled = False
        txt_CapitalConf.Enabled = False
        btn_Condonar.Enabled = False

        lbl_IntNormalDebe.Text = ""
        lbl_IntMoratorioDebe.Text = ""
        lbl_CapitalDebe.Text = ""
        lbl_IntN.Text = ""
        lbl_IntM.Text = ""
        lbl_Cap.Text = ""
    End Sub

    Private Sub Llenadatos()
        cmb_folio.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_cliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FOLIOS_GENERAL_CLIENTE"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_folio.Items.Add(elija)
        If Not Session("rs").EOF Then
            If Session("rs").Fields("NOMBRE").Value.ToString = "-1" Then
                lbl_Info.Text = "Error: No existe el número de cliente"
                lbl_Cliente.Text = ""
                lbl_Cartera.Text = ""
                Session("Con").Close()
                Exit Sub
            End If

            If Session("rs").Fields("COND").Value.ToString = "0" Then
                lbl_Cartera.Text = ""
                lbl_Info.Text = "Error: El cliente no tiene expedientes activos"
                lbl_Cliente.Text = "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString
                Session("Con").Close()
                Exit Sub
            End If

            lbl_Cliente.Text = "Cliente: " + Session("rs").Fields("NOMBRE").Value.ToString

            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("FOLIO").Value.ToString, Session("rs").Fields("FOLIO").Value.ToString)
                cmb_folio.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If
        Session("Con").Close()
    End Sub

    Private Sub RevisaFacultades()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONDONACIONES_FACULTADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("INTERES_CRED").Value.ToString = "1" Then
            txt_IntMoratorio.Enabled = True
            txt_IntMoratorioConf.Enabled = True
            txt_IntNormal.Enabled = True
            txt_IntNormalConf.Enabled = True

            lbl_IntNormalDebe.Visible = True
            lbl_IntMoratorioDebe.Visible = True
        Else
            txt_IntMoratorio.Enabled = False
            txt_IntMoratorioConf.Enabled = False
            txt_IntNormal.Enabled = False
            txt_IntNormalConf.Enabled = False

            txt_IntMoratorio.Text = "0.00"
            txt_IntMoratorioConf.Text = "0.00"
            txt_IntNormal.Text = "0.00"
            txt_IntNormalConf.Text = "0.00"

            lbl_IntNormalDebe.Visible = False
            lbl_IntMoratorioDebe.Visible = False
        End If

        If Session("rs").Fields("CAPITAL_CRED").Value.ToString = "1" Then
            txt_Capital.Enabled = True
            txt_CapitalConf.Enabled = True

            lbl_CapitalDebe.Visible = True
        Else
            txt_Capital.Enabled = False
            txt_CapitalConf.Enabled = False

            txt_Capital.Text = "0.00"
            txt_CapitalConf.Text = "0.00"

            lbl_CapitalDebe.Visible = False
        End If

        If Session("rs").Fields("INTERES_CRED").Value.ToString = "0" And Session("rs").Fields("CAPITAL_CRED").Value.ToString = "0" Then
            cmb_TipoCondonacion.Enabled = False
            btn_Condonar.Enabled = False
        Else
            cmb_TipoCondonacion.Enabled = True
            btn_Condonar.Enabled = True
        End If

        lbl_IntN.Text = ""
        lbl_IntM.Text = ""
        lbl_Cap.Text = ""

        Session("Con").Close()

    End Sub

    Protected Sub cmb_folio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_folio.SelectedIndexChanged
        lbl_estatus.Text = ""
        txt_Notas.Text = ""
        If cmb_folio.SelectedItem.Value <> 0 Then
            Session("FOLIO") = cmb_folio.SelectedItem.Value
            DebeIntereses()
            LlenaComsiones()
            RevisaFacultades()
        Else
            Session("FOLIO") = Nothing
            cmb_TipoCondonacion.Enabled = False
            txt_IntMoratorio.Enabled = False
            txt_IntMoratorioConf.Enabled = False
            txt_IntNormal.Enabled = False
            txt_IntNormalConf.Enabled = False
            txt_Capital.Enabled = False
            txt_CapitalConf.Enabled = False
            btn_Condonar.Enabled = False
            lbl_Cartera.Text = ""
            lbl_IntNormalDebe.Text = ""
            lbl_IntMoratorioDebe.Text = ""
            lbl_IntN.Text = ""
            lbl_IntM.Text = ""
            lbl_CapitalDebe.Text = ""
            lbl_Cap.Text = ""
        End If
    End Sub

    'Muestra el interes normal y moratorio que debe una perosna y su respectivo expediente
    Private Sub DebeIntereses()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONTA_INTERESES_DEBE_FOLIO"

        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("TIPO_PROD").Value.ToString = "CRED" Then
            pnl_interes.Visible = True
            lbl_Cartera.Text = Session("rs").Fields("CARTERA").Value.ToString
            lbl_IntNormalDebe.Text = "$" + Session("MascoreG").FormatNumberCurr(Session("rs").Fields("INTNORMAL").Value.ToString)
            lbl_IntMoratorioDebe.Text = "$" + Session("MascoreG").FormatNumberCurr(Session("rs").Fields("INTMORA").Value.ToString)
            lbl_CapitalDebe.Text = "$" + Session("MascoreG").FormatNumberCurr(Session("rs").Fields("CAPITAL").Value.ToString)
        Else
            pnl_interes.Visible = False
            pnl_comisiones.Visible = True
            lbl_Cartera.Text = ""

        End If

        Session("Con").Close()

    End Sub

    Protected Sub cmb_TipoCondonacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_TipoCondonacion.SelectedIndexChanged
        lbl_estatus.Text = ""
        txt_Notas.Text = ""
        lbl_IntN.Text = ""
        lbl_IntM.Text = ""
        lbl_Cap.Text = ""
        If cmb_TipoCondonacion.SelectedValue.ToString = "M" Then
            lbl_monto.Text = "Monto a Condonar"
            lbl_confmonto.Text = "Confirmar Monto"
        Else
            lbl_monto.Text = "Porcentaje a Condonar"
            lbl_confmonto.Text = "Confirmar Porcentaje"
        End If

    End Sub

    'Aplica las Condonaciones
    Protected Sub btn_Condonar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Condonar.Click

        Dim IntNorm As Decimal
        Dim IntMora As Decimal
        Dim Capital As Decimal

        If txt_IntNormal.Text = "" Then
            txt_IntNormal.Text = "0.00"
            txt_IntNormalConf.Text = "0.00"
        End If
        If txt_IntMoratorio.Text = "" Then
            txt_IntMoratorio.Text = "0.00"
            txt_IntMoratorioConf.Text = "0.00"
        End If
        If txt_Capital.Text = "" Then
            txt_Capital.Text = "0.00"
            txt_CapitalConf.Text = "0.00"
        End If

        If Valida() = 3 And (txt_IntNormal.Text <> "0.00" Or txt_IntMoratorio.Text <> "0.00" Or txt_Capital.Text <> "0.00") Then
            IntNorm = CDec(txt_IntNormal.Text)
            IntMora = CDec(txt_IntMoratorio.Text)
            Capital = CDec(txt_Capital.Text)

            If cmb_TipoCondonacion.SelectedValue.ToString = "M" Then
                If IntNorm <= CDec(lbl_IntNormalDebe.Text) And IntMora <= CDec(lbl_IntMoratorioDebe.Text) And Capital <= CDec(lbl_CapitalDebe.Text) Then
                    If IntNorm > CDec(lbl_IntNormalDebe.Text) Then
                        IntNorm = CDec(lbl_IntNormalDebe.Text)
                    End If
                    If IntMora > CDec(lbl_IntMoratorioDebe.Text) Then
                        IntMora = CDec(lbl_IntMoratorioDebe.Text)
                    End If
                    If Capital > CDec(lbl_CapitalDebe.Text) Then
                        Capital = CDec(lbl_CapitalDebe.Text)
                    End If

                    lbl_IntN.Text = IntNorm
                    lbl_IntM.Text = IntMora
                    lbl_Cap.Text = Capital
                    CondonarInteres(IntNorm, IntMora, Capital)
                    txt_Notas.Text = ""
                    lbl_estatus.Text = "Guardado correctamente"
                Else
                    txt_Notas.Text = ""
                    lbl_IntN.Text = ""
                    lbl_IntM.Text = ""
                    lbl_Cap.Text = ""
                    lbl_estatus.Text = "Error: Alguno de los montos es mayor al total posible a condonar. Verifique"
                End If
            Else
                If CDec(txt_IntNormal.Text) <= 100.0 And CDec(txt_IntMoratorio.Text) <= 100.0 And CDec(txt_Capital.Text) <= 100.0 Then
                    IntNorm = (CDec(lbl_IntNormalDebe.Text) * (IntNorm / 100))
                    IntMora = (CDec(lbl_IntMoratorioDebe.Text) * (IntMora / 100))
                    Capital = (CDec(lbl_CapitalDebe.Text) * (Capital / 100))

                    If IntNorm > CDec(lbl_IntNormalDebe.Text) Then
                        IntNorm = CDec(lbl_IntNormalDebe.Text)
                    End If
                    If IntMora > CDec(lbl_IntMoratorioDebe.Text) Then
                        IntMora = CDec(lbl_IntMoratorioDebe.Text)
                    End If
                    If Capital > CDec(lbl_CapitalDebe.Text) Then
                        Capital = CDec(lbl_CapitalDebe.Text)
                    End If

                    lbl_IntN.Text = IntNorm
                    lbl_IntM.Text = IntMora
                    lbl_Cap.Text = Capital
                    CondonarInteres(IntNorm, IntMora, Capital)
                    txt_Notas.Text = ""
                    lbl_estatus.Text = "Guardado correctamente"
                Else
                    txt_Notas.Text = ""
                    lbl_estatus.Text = "Error: No puede condonar un monto mayor a un 100%."
                End If
            End If

            DebeIntereses()
            txt_IntNormal.Text = ""
            txt_IntMoratorio.Text = ""
            txt_Capital.Text = ""
            txt_IntNormalConf.Text = ""
            txt_IntMoratorioConf.Text = ""
            txt_CapitalConf.Text = ""

        Else
            If (txt_IntNormal.Text = "0.00" And txt_IntMoratorio.Text = "0.00" And txt_Capital.Text = "0.00") Then
                lbl_estatus.Text = ""
                txt_IntNormalConf.Text = ""
                txt_IntMoratorioConf.Text = ""
                txt_IntNormal.Text = ""
                txt_IntMoratorio.Text = ""
                txt_Capital.Text = ""
                txt_CapitalConf.Text = ""
            Else
                lbl_estatus.Text = "Error: No coincide monto de confirmación de un monto"
                lbl_IntN.Text = ""
                lbl_IntM.Text = ""
                lbl_Cap.Text = ""
                txt_IntNormalConf.Text = ""
                txt_IntMoratorioConf.Text = ""
                txt_CapitalConf.Text = ""
                If txt_IntNormal.Text = "0.00" Then
                    txt_IntNormal.Text = ""
                End If
                If txt_IntMoratorio.Text = "0.00" Then
                    txt_IntMoratorio.Text = ""
                End If
                If txt_Capital.Text = "0.00" Then
                    txt_Capital.Text = ""
                End If
            End If
        End If

    End Sub

    Private Sub CondonarInteres(ByVal IntNorm As Decimal, ByVal IntMora As Decimal, ByVal Capital As Decimal)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSR", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INTNORMAL", Session("adVarChar"), Session("adParamInput"), 50, IntNorm)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INTMORA", Session("adVarChar"), Session("adParamInput"), 50, IntMora)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CAPITAL", Session("adVarChar"), Session("adParamInput"), 50, Capital)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 1000, txt_Notas.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CONTA_CONDONA_INTERES"
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

    Public Function Valida() As Integer

        Dim condona As Integer = 0

        If txt_IntNormalConf.Text = "" Then
            condona = 0
        Else
            If CDec(txt_IntNormal.Text) = CDec(txt_IntNormalConf.Text) Then
                condona = condona + 1
            Else
                condona = 0
            End If
        End If

        If txt_IntMoratorioConf.Text = "" Then
            condona = 0
        Else
            If CDec(txt_IntMoratorio.Text) = CDec(txt_IntMoratorioConf.Text) Then
                condona = condona + 1
            Else
                condona = 0
            End If
        End If

        If txt_CapitalConf.Text = "" Then
            condona = 0
        Else
            If CDec(txt_Capital.Text) = CDec(txt_CapitalConf.Text) Then
                condona = condona + 1
            Else
                condona = 0
            End If
        End If

        Return condona

    End Function

#Region "CondonaComisiones"

    Private Sub LlenaComsiones()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtComisiones As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONDONA_COMISIONES"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtComisiones, Session("rs"))

        If dtComisiones.Rows.Count > 0 Then
            lbl_NotasCondCom.Text = ""
            dag_CondComisiones.DataSource = dtComisiones
            dag_CondComisiones.DataBind()
            dag_CondComisiones.Visible = True
        Else
            lbl_NotasCondCom.Text = "No existen registros de comisiones."
            dag_CondComisiones.Visible = False
        End If

        Session("Con").Close()

    End Sub

    Private Sub dag_CondComisiones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_CondComisiones.ItemCommand

        If (e.CommandName = "CONDONAR") Then

            Session("IDCOMISION") = e.Item.Cells(0).Text
            Session("FECHA") = e.Item.Cells(7).Text
            Session("SERIE") = e.Item.Cells(9).Text
            Session("FOLIO_IMP") = e.Item.Cells(10).Text

            ModalPopupExtender_CondComision.Show()

        End If

    End Sub

    Protected Sub btn_ConfirmarCondCom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ConfirmarCondCom.Click

        CondonarComision(Session("FOLIO"), Session("IDCOMISION"), Session("FECHA"), Session("SERIE"), Session("FOLIO_IMP"))

        Session("IDCOMISION") = Nothing
        Session("FECHA") = Nothing
        Session("SERIE") = Nothing
        Session("FOLIO_IMP") = Nothing
        txt_NotasCondCom.Text = ""
        lbl_InfoCondComision.Text = "Guardado correctamente"
        LlenaComsiones()
        ModalPopupExtender_CondComision.Hide()

    End Sub

    Protected Sub btn_CancelarCondCom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CancelarCondCom.Click

        Session("IDCOMISION") = Nothing
        Session("FECHA") = Nothing
        Session("SERIE") = Nothing
        Session("FOLIO_IMP") = Nothing
        txt_NotasCondCom.Text = ""
        lbl_InfoCondComision.Text = ""
        LlenaComsiones()
        ModalPopupExtender_CondComision.Hide()

    End Sub

    Private Sub CondonarComision(ByVal Folio As Integer, ByVal IDComision As Integer, ByVal Fecha As String, ByVal Serie As String, ByVal FolioImp As String)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDCOMISION", Session("adVarChar"), Session("adParamInput"), 10, IDComision)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SERIE_COB", Session("adVarChar"), Session("adParamInput"), 50, Serie)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_IMP_COB", Session("adVarChar"), Session("adParamInput"), 50, FolioImp)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHASIS_COB", Session("adVarChar"), Session("adParamInput"), 50, Fecha)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 1000, txt_NotasCondCom.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 1000, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 1000, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CONTA_CONDONA_COMISIONES"
        Session("cmd").Execute()
        Session("Con").Close()

    End Sub

#End Region


End Class