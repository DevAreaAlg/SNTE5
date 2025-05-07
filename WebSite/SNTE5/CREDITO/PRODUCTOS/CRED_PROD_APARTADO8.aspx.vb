Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO8
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Medios de pago y periodicidad", "Medios de pago y Periodicidad")

        If Not Me.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                'LLENO LOS RESPECTIVOS LABELS
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                'LLENO LOS COMBOS
                'LlenaInstituciones()

                LlenaTipoCobro()
                LlenaPeriodosCap()
                LlenaPeriodosInt()
                Clasificacion()
                Llena_formasPagoCred()
            End If
        End If
    End Sub


#Region "CLASIFICACION"

    Private Sub LlenaTipoCobro()


        cmb_tipocobro.Items.Clear()
        cmb_cta_operativa.Items.Clear()


        Dim elija As New ListItem("ELIJA", "-1")
        cmb_tipocobro.Items.Add(elija)

        Dim elijaOperativa As New ListItem("ELIJA", "-1")
        cmb_cta_operativa.Items.Add(elijaOperativa)

        Dim item As New ListItem
        item = New ListItem("SI", "1")
        cmb_cta_operativa.Items.Add(item)
        item = New ListItem("NO", "0")
        cmb_cta_operativa.Items.Add(item)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_TIPO_COBRO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Select Case Session("rs").Fields("CLASIFICACION").Value.ToString()

                Case "SIM"

                    item = New ListItem("PAGOS FIJOS", "PFSI")
                    cmb_tipocobro.Items.Add(item)
                    item = New ListItem("SALDOS INSOLUTOS", "SI")
                    cmb_tipocobro.Items.Add(item)
                    item = New ListItem("AMBOS", "AMBOS")
                    cmb_tipocobro.Items.Add(item)



                Case "PINS" 'Plan Institucional

                    item = New ListItem("PAGOS FIJOS", "PFSI")
                    cmb_tipocobro.Items.Add(item)
                    panel_periodcap.Visible = False
                    panel_periodint.Visible = False
                Case Else
                    Exit Select

            End Select

            cmb_tipocobro.Items.FindByValue(Session("rs").Fields("TIPOCOBRO").Value.ToString).Selected = True
            cmb_cta_operativa.Items.FindByValue(Session("rs").Fields("CTAOPERATIVA").Value.ToString).Selected = True

        End If

        Session("Con").Close()




    End Sub

    Private Sub Clasificacion()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_CLASIF_CRED_PROD"
        Session("rs") = Session("cmd").Execute()
        Session("CLASIFICACION") = Session("rs").Fields("CLAVE").Value.ToString
        Session("Con").Close()
    End Sub

    Private Sub AsignaTipoCobro()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipocobro.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CTAOPERATIVA", Session("adVarChar"), Session("adParamInput"), 10, cmb_cta_operativa.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASIFICACION", Session("adVarChar"), Session("adParamInput"), 15, Session("CLASIFICACION"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_TAB", Session("adVarChar"), Session("adParamInput"), 15, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFPCR_TIPO_COBRO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        LlenaTipoCobro()
    End Sub

    Private Sub Validatasafija()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_FORMA_COBRO"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("RESPUESTA").Value.ToString = "TASAS" Then
            lbl_statuscobro.Text = "Error!"
            lbl_statuscobro.Visible = True
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_guardacobro_Click(sender As Object, e As System.EventArgs)
        If cmb_tipocobro.SelectedItem.Value = "NA" Then
            lbl_statuscobro.Text = "Error: Seleccione tipo de cobro válido"
            lbl_statuscobro.Visible = True
            Exit Sub
        End If
        If cmb_cta_operativa.SelectedItem.Value = "ELIJA" Then
            lbl_statuscobro.Text = "Error: Seleccione si desea o no procurar saldo cero en cuenta operativa"
            lbl_statuscobro.Visible = True
            Exit Sub
        End If
        AsignaTipoCobro()
        Validatasafija()
        LlenaTipoCobro()
        lbl_statuscobro.Text = "Guardado correctamente"
        lbl_statuscobro.Visible = True
        Exit Sub
    End Sub



#End Region

#Region "PERIODICIDAD CAPITAL"

    Private Sub LlenaPeriodosCap()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()

        'PARA NDIAS
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NDIA")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "CAP")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_ndiascap.Checked = False
            txt_ndiasmincap.Enabled = False
            txt_ndiasmincap.Text = ""
            txt_ndiasmaxcap.Enabled = False
            txt_ndiasmaxcap.Text = ""
        Else
            chk_ndiascap.Checked = True
            txt_ndiasmincap.Enabled = True
            txt_ndiasmincap.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_ndiasmaxcap.Enabled = True
            txt_ndiasmaxcap.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        'PARA NSEM
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NSEM")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "CAP")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_nsemcap.Checked = False
            txt_nsemmincap.Enabled = False
            txt_nsemmincap.Text = ""
            txt_nsemmaxcap.Enabled = False
            txt_nsemmaxcap.Text = ""
        Else
            chk_nsemcap.Checked = True
            txt_nsemmincap.Enabled = True
            txt_nsemmincap.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_nsemmaxcap.Enabled = True
            txt_nsemmaxcap.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        ''PARA NMES
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NMES")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "CAP")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_nmescap.Checked = False
            txt_nmesmincap.Enabled = False
            txt_nmesmincap.Text = ""
            txt_nmesmaxcap.Enabled = False
            txt_nmesmaxcap.Text = ""

        Else
            chk_nmescap.Checked = True
            txt_nmesmincap.Enabled = True
            txt_nmesmincap.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_nmesmaxcap.Enabled = True
            txt_nmesmaxcap.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        ''PARA DIAX
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "DIAX")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "CAP")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_xdiacap.Checked = False
            txt_xdiamincap.Enabled = False
            txt_xdiamincap.Text = ""
            txt_xdiamaxcap.Enabled = False
            txt_xdiamaxcap.Text = ""

        Else
            chk_xdiacap.Checked = True
            txt_xdiamincap.Enabled = True
            txt_xdiamincap.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_xdiamaxcap.Enabled = True
            txt_xdiamaxcap.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        '' ESPE 
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "ESPE")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "CAP")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_espcap.Checked = False
            txt_espmincap.Enabled = False
            txt_espmincap.Text = ""
            txt_espmaxcap.Enabled = False
            txt_espmaxcap.Text = ""

        Else
            chk_espcap.Checked = True
            txt_espmincap.Enabled = True
            txt_espmincap.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_espmaxcap.Enabled = True
            txt_espmaxcap.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_guardapercap_Click(sender As Object, e As System.EventArgs)
        lbl_statpercap.Text = ""

        If chk_ndiascap.Checked Then
            If txt_ndiasmincap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo mínimo está vacío(Sección Cada ""n"" días)"
                Exit Sub
            End If
            If txt_ndiasmaxcap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo máximo está vacío(Sección Cada ""n"" días)"
                Exit Sub
            End If
            If CInt(txt_ndiasmincap.Text) > CInt(txt_ndiasmaxcap.Text) Then
                lbl_statpercap.Text = "Error: Máximo menor a mínimo(Sección Cada ""n"" días)"
                Exit Sub
            End If
            If CInt(txt_ndiasmincap.Text) < 1 Then
                lbl_statpercap.Text = "Error: Mínimo no puede ser menor a 1(Sección Cada ""n"" días)"
                Exit Sub
            End If
        End If

        If chk_nsemcap.Checked Then
            If txt_nsemmincap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo mínimo está vacío(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
            If txt_nsemmaxcap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo máximo está vacío(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
            If CInt(txt_nsemmincap.Text) > CInt(txt_nsemmaxcap.Text) Then
                lbl_statpercap.Text = "Error: Máximo menor a mínimo(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
            If CInt(txt_nsemmincap.Text) < 1 Then
                lbl_statpercap.Text = "Error: Mínimo no puede ser menor a 1(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
        End If

        If chk_nmescap.Checked Then
            If txt_nmesmincap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo mínimo está vacío(Sección Cada ""n"" meses)"
                Exit Sub
            End If
            If txt_nmesmaxcap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo máximo está vacío(Sección Cada ""n"" meses)"
                Exit Sub
            End If
            If CInt(txt_nmesmincap.Text) > CInt(txt_nmesmaxcap.Text) Then
                lbl_statpercap.Text = "Error: Máximo menor a mínimo(Sección Cada ""n"" meses)"
                Exit Sub
            End If
            If CInt(txt_nmesmincap.Text) < 1 Then
                lbl_statpercap.Text = "Error: Mínimo no puede ser menor a 1(Sección Cada ""n"" meses)"
                Exit Sub
            End If
        End If

        If chk_xdiacap.Checked Then
            If txt_xdiamincap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo mínimo está vacío(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If txt_xdiamaxcap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo máximo está vacío(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If CInt(txt_xdiamincap.Text) > CInt(txt_xdiamaxcap.Text) Then
                lbl_statpercap.Text = "Error: Máximo menor a mínimo(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If CInt(txt_xdiamincap.Text) < 1 Then
                lbl_statpercap.Text = "Error: Mínimo no puede ser menor a 1(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If CInt(txt_xdiamaxcap.Text) > 31 Then
                lbl_statpercap.Text = "Error: Máximo no puede ser mayor a 31(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
        End If

        If chk_espcap.Checked Then
            If txt_espmincap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo mínimo está vacío(Sección Plan de pagos especial)"
                Exit Sub
            End If
            If txt_espmaxcap.Text = "" Then
                lbl_statpercap.Text = "Error: Campo máximo está vacío(Sección Plan de pagos especial)"
                Exit Sub
            End If
            If CInt(txt_espmincap.Text) > CInt(txt_espmaxcap.Text) Then
                lbl_statpercap.Text = "Error: Máximo menor a mínimo(Sección Plan de pagos especial)"
                Exit Sub
            End If
            If CInt(txt_espmincap.Text) < 1 Then
                lbl_statpercap.Text = "Error: Mínimo no puede ser menor a 1(Sección Plan de pagos especial)"
                Exit Sub
            End If

        End If

        'validacion para que ponga por lo menos una periodicidad 
        Select Case cmb_tipocobro.SelectedItem.Value()

            Case "SI"
                If chk_ndiascap.Checked = False And chk_nsemcap.Checked = False And chk_nmescap.Checked = False Then
                    lbl_statpercap.Text = "Error: Capture por lo menos una periodicidad diferente a Día X o Plan Especial"
                    Exit Sub
                End If
            Case "AMBOS"

                If chk_ndiascap.Checked = False And chk_nsemcap.Checked = False And chk_nmescap.Checked = False Then
                    lbl_statpercap.Text = "Error: Capture por lo menos una periodicidad diferente a Día X o Plan Especial"
                    Exit Sub
                End If


            Case "ES"
                If chk_espcap.Checked = False And chk_xdiacap.Checked = False Then
                    lbl_statpercap.Text = "Error: Capture por lo menos una periodicidad con Día X o Plan Especial"
                    Exit Sub
                End If
            Case Else

        End Select


        If chk_ndiascap.Checked Then
            AsignaPeriodo("NDIA", "CAP", txt_ndiasmincap.Text, txt_ndiasmaxcap.Text)
        Else
            DesasignaPeriodo("NDIA", "CAP")
        End If

        If chk_nsemcap.Checked Then
            AsignaPeriodo("NSEM", "CAP", txt_nsemmincap.Text, txt_nsemmaxcap.Text)
        Else
            DesasignaPeriodo("NSEM", "CAP")
        End If

        If chk_nmescap.Checked Then
            AsignaPeriodo("NMES", "CAP", txt_nmesmincap.Text, txt_nmesmaxcap.Text)
        Else
            DesasignaPeriodo("NMES", "CAP")
        End If

        If chk_xdiacap.Checked Then
            AsignaPeriodo("DIAX", "CAP", txt_xdiamincap.Text, txt_xdiamaxcap.Text)
        Else
            DesasignaPeriodo("DIAX", "CAP")
        End If

        If chk_espcap.Checked Then
            AsignaPeriodo("ESPE", "CAP", txt_espmincap.Text, txt_espmaxcap.Text)
        Else
            DesasignaPeriodo("ESPE", "CAP")
        End If
        lbl_statpercap.Text = "Guardado correctamente"
        LlenaPeriodosCap()

    End Sub

    Private Sub AsignaPeriodo(ByVal Tipo As String, ByVal Clasf As String, ByVal Min As String, ByVal Max As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, Tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, Clasf)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MIN", Session("adVarChar"), Session("adParamInput"), 10, Min)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MAX", Session("adVarChar"), Session("adParamInput"), 10, Max)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub DesasignaPeriodo(ByVal Tipo As String, ByVal Clasf As String)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, Tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, Clasf)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ELIMINADOX", Session("adVarChar"), Session("adParamInput"), 3, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Protected Sub chk_ndiascap_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_ndiascap.CheckedChanged
        lbl_statpercap.Text = ""

        If Session("CLASIFICACION") = "CTAC" Or Session("CLASIFICACION") = "SREV" Then
            lbl_statpercap.Text = "Error: Para agregar esta periodicidad debe de cambiar la clasificación del producto"
            chk_ndiascap.Checked = False
            Exit Sub
        Else

            If chk_ndiascap.Checked Then
                txt_ndiasmincap.Text = ""
                txt_ndiasmincap.Enabled = True
                txt_ndiasmaxcap.Text = ""
                txt_ndiasmaxcap.Enabled = True

            Else
                txt_ndiasmincap.Text = ""
                txt_ndiasmincap.Enabled = False
                txt_ndiasmaxcap.Text = ""
                txt_ndiasmaxcap.Enabled = False
            End If

        End If

    End Sub

    Protected Sub chk_nsemcap_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_nsemcap.CheckedChanged
        lbl_statpercap.Text = ""

        If Session("CLASIFICACION") = "CTAC" Or Session("CLASIFICACION") = "SREV" Then
            lbl_statpercap.Text = "Error: Para agregar esta periodicidad debe de cambiar la clasificación del producto"
            chk_nsemcap.Checked = False
            Exit Sub
        Else
            If chk_nsemcap.Checked Then
                txt_nsemmincap.Text = ""
                txt_nsemmincap.Enabled = True
                txt_nsemmaxcap.Text = ""
                txt_nsemmaxcap.Enabled = True

            Else
                txt_nsemmincap.Text = ""
                txt_nsemmincap.Enabled = False
                txt_nsemmaxcap.Text = ""
                txt_nsemmaxcap.Enabled = False
            End If
        End If
    End Sub

    Protected Sub chk_nmescap_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_nmescap.CheckedChanged
        lbl_statpercap.Text = ""

        If Session("CLASIFICACION") = "CTAC" Or Session("CLASIFICACION") = "SREV" Then
            lbl_statpercap.Text = "Error: Para agregar esta periodicidad debe de cambiar la clasificación del producto"
            chk_nmescap.Checked = False
            Exit Sub
        Else
            If chk_nmescap.Checked Then
                txt_nmesmincap.Text = ""
                txt_nmesmincap.Enabled = True
                txt_nmesmaxcap.Text = ""
                txt_nmesmaxcap.Enabled = True
            Else
                txt_nmesmincap.Text = ""
                txt_nmesmincap.Enabled = False
                txt_nmesmaxcap.Text = ""
                txt_nmesmaxcap.Enabled = False
            End If
        End If
    End Sub

    Protected Sub chk_xdiacap_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_xdiacap.CheckedChanged

        lbl_statpercap.Text = ""
        If chk_xdiacap.Checked Then
            txt_xdiamincap.Text = ""
            txt_xdiamincap.Enabled = True
            txt_xdiamaxcap.Text = ""
            txt_xdiamaxcap.Enabled = True

        Else
            txt_xdiamincap.Text = ""
            txt_xdiamincap.Enabled = False
            txt_xdiamaxcap.Text = ""
            txt_xdiamaxcap.Enabled = False
        End If
    End Sub

    Protected Sub chk_espcap_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_espcap.CheckedChanged
        If chk_espcap.Checked Then
            txt_espmincap.Text = ""
            txt_espmincap.Enabled = True
            txt_espmaxcap.Text = ""
            txt_espmaxcap.Enabled = True

        Else
            txt_espmincap.Text = ""
            txt_espmincap.Enabled = False
            txt_espmaxcap.Text = ""
            txt_espmaxcap.Enabled = False
        End If
    End Sub


#End Region

#Region "PERIODICIDAD INTERES"

    Private Sub LlenaPeriodosInt()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        'PARA NDIAS
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NDIA")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "INT")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_ndiasint.Checked = False
            txt_ndiasminint.Enabled = False
            txt_ndiasminint.Text = ""
            txt_ndiasmaxint.Enabled = False
            txt_ndiasmaxint.Text = ""

        Else
            chk_ndiasint.Checked = True
            txt_ndiasminint.Enabled = True
            txt_ndiasminint.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_ndiasmaxint.Enabled = True
            txt_ndiasmaxint.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        'PARA NSEM
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NSEM")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "INT")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_nsemint.Checked = False
            txt_nsemminint.Enabled = False
            txt_nsemminint.Text = ""
            txt_nsemmaxint.Enabled = False
            txt_nsemmaxint.Text = ""

        Else
            chk_nsemint.Checked = True
            txt_nsemminint.Enabled = True
            txt_nsemminint.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_nsemmaxint.Enabled = True
            txt_nsemmaxint.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        ''PARA NMES
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NMES")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "INT")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_nmesint.Checked = False
            txt_nmesminint.Enabled = False
            txt_nmesminint.Text = ""
            txt_nmesmaxint.Enabled = False
            txt_nmesmaxint.Text = ""

        Else
            chk_nmesint.Checked = True
            txt_nmesminint.Enabled = True
            txt_nmesminint.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_nmesmaxint.Enabled = True
            txt_nmesmaxint.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        ''PARA DIAX
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "DIAX")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "INT")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_xdiaint.Checked = False
            txt_xdiaminint.Enabled = False
            txt_xdiaminint.Text = ""
            txt_xdiamaxint.Enabled = False
            txt_xdiamaxint.Text = ""

        Else
            chk_xdiaint.Checked = True
            txt_xdiaminint.Enabled = True
            txt_xdiaminint.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_xdiamaxint.Enabled = True
            txt_xdiamaxint.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If
        Session("Con").Close()

        '' ESPE 
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "ESPE")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLASF", Session("adVarChar"), Session("adParamInput"), 3, "INT")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_PERIODICIDADES"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").EOF() Then
            chk_espint.Checked = False
            txt_espminint.Enabled = False
            txt_espminint.Text = ""
            txt_espmaxint.Enabled = False
            txt_espmaxint.Text = ""

        Else
            chk_espint.Checked = True
            txt_espminint.Enabled = True
            txt_espminint.Text = Session("rs").Fields("MSTPERIOD_MINIMO").Value.ToString
            txt_espmaxint.Enabled = True
            txt_espmaxint.Text = Session("rs").Fields("MSTPERIOD_MAXIMO").Value.ToString
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_guardaperint_Click(sender As Object, e As System.EventArgs)
        lbl_statperint.Text = ""

        If chk_ndiasint.Checked Then
            If txt_ndiasminint.Text = "" Then
                lbl_statperint.Text = "Error: Campo mínimo está vacío(Sección Cada ""n"" días)"
                Exit Sub
            End If
            If txt_ndiasmaxint.Text = "" Then
                lbl_statperint.Text = "Error: Campo máximo está vacío(Sección Cada ""n"" días)"
                Exit Sub
            End If
            If CInt(txt_ndiasminint.Text) > CInt(txt_ndiasmaxint.Text) Then
                lbl_statperint.Text = "Error: Máximo menor a mínimo(Sección Cada ""n"" días)"
                Exit Sub
            End If
            If CInt(txt_ndiasminint.Text) < 1 Then
                lbl_statperint.Text = "Error: Mínimo no puede ser menor a 1(Sección Cada ""n"" días)"
                Exit Sub
            End If
        End If

        If chk_nsemint.Checked Then
            If txt_nsemminint.Text = "" Then
                lbl_statperint.Text = "Error: Campo mínimo está vacío(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
            If txt_nsemmaxint.Text = "" Then
                lbl_statperint.Text = "Error: Campo máximo está vacío(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
            If CInt(txt_nsemminint.Text) > CInt(txt_nsemmaxint.Text) Then
                lbl_statperint.Text = "Error: Máximo menor a mínimo(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
            If CInt(txt_nsemminint.Text) < 1 Then
                lbl_statperint.Text = "Error: Mínimo no puede ser menor a 1(Sección Cada ""n"" semanas)"
                Exit Sub
            End If
        End If

        If chk_nmesint.Checked Then
            If txt_nmesminint.Text = "" Then
                lbl_statperint.Text = "Error: Campo mínimo está vacío(Sección Cada ""n"" meses)"
                Exit Sub
            End If
            If txt_nmesmaxint.Text = "" Then
                lbl_statperint.Text = "Error: Campo máximo está vacío(Sección Cada ""n"" meses)"
                Exit Sub
            End If
            If CInt(txt_nmesminint.Text) > CInt(txt_nmesmaxint.Text) Then
                lbl_statperint.Text = "Error: Máximo menor a mínimo(Sección Cada ""n"" meses)"
                Exit Sub
            End If
            If CInt(txt_nmesminint.Text) < 1 Then
                lbl_statperint.Text = "Error: Mínimo no puede ser menor a 1(Sección Cada ""n"" meses)"
                Exit Sub
            End If
        End If

        If chk_xdiaint.Checked Then
            If txt_xdiaminint.Text = "" Then
                lbl_statperint.Text = "Error: Campo mínimo está vacío(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If txt_xdiamaxint.Text = "" Then
                lbl_statperint.Text = "Error: Campo máximo está vacío(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If CInt(txt_xdiaminint.Text) > CInt(txt_xdiamaxint.Text) Then
                lbl_statperint.Text = "Error: Máximo menor a mínimo(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If CInt(txt_xdiaminint.Text) < 1 Then
                lbl_statperint.Text = "Error: Mínimo no puede ser menor a 1(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
            If CInt(txt_xdiamaxint.Text) > 31 Then
                lbl_statperint.Text = "Error: Máximo no puede ser mayor a 31(Sección Los días ""x"" de cada mes)"
                Exit Sub
            End If
        End If

        If chk_espint.Checked Then
            If txt_espminint.Text = "" Then
                lbl_statperint.Text = "Error: Campo mínimo está vacío(Sección Plan de pagos especial)"
                Exit Sub
            End If
            If txt_espmaxint.Text = "" Then
                lbl_statperint.Text = "Error: Campo máximo está vacío(Sección Plan de pagos especial)"
                Exit Sub
            End If
            If CInt(txt_espminint.Text) > CInt(txt_espmaxint.Text) Then
                lbl_statperint.Text = "Error: Máximo menor a mínimo(Sección Plan de pagos especial)"
                Exit Sub
            End If
            If CInt(txt_espminint.Text) < 1 Then
                lbl_statperint.Text = "Error: Mínimo no puede ser menor a 1(Sección Plan de pagos especial)"
                Exit Sub
            End If

        End If

        Select Case cmb_tipocobro.SelectedItem.Value()

            Case "SI"
                If chk_ndiasint.Checked = False And chk_nsemint.Checked = False And chk_nmesint.Checked = False Then
                    lbl_statperint.Text = "Error: Capture por lo menos una periodicidad diferente a Día X o Plan Especial"
                    Exit Sub
                End If
            Case "AMBOS"

                If chk_ndiasint.Checked = False And chk_nsemint.Checked = False And chk_nmesint.Checked = False Then
                    lbl_statperint.Text = "Error: Capture por lo menos una periodicidad diferente a Día X o Plan Especial"
                    Exit Sub
                End If

            Case "ES"
                If chk_espint.Checked = False And chk_xdiaint.Checked = False Then
                    lbl_statperint.Text = "Error: Capture por lo menos una periodicidad con Día X o Plan Especial"
                    Exit Sub
                End If
            Case Else

        End Select

        If chk_ndiasint.Checked Then
            AsignaPeriodo("NDIA", "INT", txt_ndiasminint.Text, txt_ndiasmaxint.Text)
        Else
            DesasignaPeriodo("NDIA", "INT")
        End If

        If chk_nsemint.Checked Then
            AsignaPeriodo("NSEM", "INT", txt_nsemminint.Text, txt_nsemmaxint.Text)
        Else
            DesasignaPeriodo("NSEM", "INT")
        End If

        If chk_nmesint.Checked Then
            AsignaPeriodo("NMES", "INT", txt_nmesminint.Text, txt_nmesmaxint.Text)
        Else
            DesasignaPeriodo("NMES", "INT")
        End If

        If chk_xdiaint.Checked Then
            AsignaPeriodo("DIAX", "INT", txt_xdiaminint.Text, txt_xdiamaxint.Text)
        Else
            DesasignaPeriodo("DIAX", "INT")
        End If

        If chk_espint.Checked Then
            AsignaPeriodo("ESPE", "INT", txt_espminint.Text, txt_espmaxint.Text)
        Else
            DesasignaPeriodo("ESPE", "INT")
        End If
        lbl_statperint.Text = "Guardado correctamente"
        LlenaPeriodosInt()

    End Sub

    Protected Sub chk_ndiasint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_ndiasint.CheckedChanged
        lbl_statperint.Text = ""

        If Session("CLASIFICACION") = "CTAC" Then
            lbl_statperint.Text = "Error: Para agregar esta periodicidad debe de cambiar la clasificación del producto"
            chk_ndiasint.Checked = False

            Exit Sub
        Else

            If chk_ndiasint.Checked Then
                txt_ndiasminint.Text = ""
                txt_ndiasminint.Enabled = True
                txt_ndiasmaxint.Text = ""
                txt_ndiasmaxint.Enabled = True

            Else
                txt_ndiasminint.Text = ""
                txt_ndiasminint.Enabled = False
                txt_ndiasmaxint.Text = ""
                txt_ndiasmaxint.Enabled = False
            End If
        End If
    End Sub

    Protected Sub chk_nsemint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_nsemint.CheckedChanged
        lbl_statperint.Text = ""

        If Session("CLASIFICACION") = "CTAC" Or Session("CLASIFICACION") = "SREV" Then
            lbl_statperint.Text = "Error: Para agregar esta periodicidad debe de cambiar la clasificación del producto"
            chk_nsemint.Checked = False

            Exit Sub
        Else
            If chk_nsemint.Checked Then
                txt_nsemminint.Text = ""
                txt_nsemminint.Enabled = True
                txt_nsemmaxint.Text = ""
                txt_nsemmaxint.Enabled = True

            Else
                txt_nsemminint.Text = ""
                txt_nsemminint.Enabled = False
                txt_nsemmaxint.Text = ""
                txt_nsemmaxint.Enabled = False
            End If
        End If
    End Sub

    Protected Sub chk_nmesint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_nmesint.CheckedChanged
        lbl_statperint.Text = ""

        If Session("CLASIFICACION") = "CTAC" Or Session("CLASIFICACION") = "SREV" Then
            lbl_statperint.Text = "Error: Para agregar esta periodicidad debe de cambiar la clasificación del producto"
            chk_nmesint.Checked = False
            Exit Sub
        Else

            If chk_nmesint.Checked Then
                txt_nmesminint.Text = ""
                txt_nmesminint.Enabled = True
                txt_nmesmaxint.Text = ""
                txt_nmesmaxint.Enabled = True

            Else
                txt_nmesminint.Text = ""
                txt_nmesminint.Enabled = False
                txt_nmesmaxint.Text = ""
                txt_nmesmaxint.Enabled = False
            End If
        End If
    End Sub

    Protected Sub chk_xdiaint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_xdiaint.CheckedChanged

        lbl_statperint.Text = ""

        If chk_xdiaint.Checked Then
            txt_xdiaminint.Text = ""
            txt_xdiaminint.Enabled = True
            txt_xdiamaxint.Text = ""
            txt_xdiamaxint.Enabled = True

        Else
            txt_xdiaminint.Text = ""
            txt_xdiaminint.Enabled = False
            txt_xdiamaxint.Text = ""
            txt_xdiamaxint.Enabled = False
        End If
    End Sub

    Protected Sub chk_espint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_espint.CheckedChanged
        If chk_espint.Checked Then
            txt_espminint.Text = ""
            txt_espminint.Enabled = True
            txt_espmaxint.Text = ""
            txt_espmaxint.Enabled = True

        Else
            txt_espminint.Text = ""
            txt_espminint.Enabled = False
            txt_espmaxint.Text = ""
            txt_espmaxint.Enabled = False
        End If
    End Sub

    Protected Sub btn_copiadecap_Click(sender As Object, e As System.EventArgs)
        lbl_statpercap.Text = ""

        chk_ndiasint.Checked = chk_ndiascap.Checked
        txt_ndiasminint.Text = txt_ndiasmincap.Text
        txt_ndiasmaxint.Text = txt_ndiasmaxcap.Text
        txt_ndiasminint.Enabled = txt_ndiasmincap.Enabled
        txt_ndiasmaxint.Enabled = txt_ndiasmaxcap.Enabled

        chk_nsemint.Checked = chk_nsemcap.Checked
        txt_nsemminint.Text = txt_nsemmincap.Text
        txt_nsemmaxint.Text = txt_nsemmaxcap.Text
        txt_nsemminint.Enabled = txt_nsemmincap.Enabled
        txt_nsemmaxint.Enabled = txt_nsemmaxcap.Enabled

        chk_nmesint.Checked = chk_nmescap.Checked
        txt_nmesminint.Text = txt_nmesmincap.Text
        txt_nmesmaxint.Text = txt_nmesmaxcap.Text
        txt_nmesminint.Enabled = txt_nmesmincap.Enabled
        txt_nmesmaxint.Enabled = txt_nmesmaxcap.Enabled

        chk_xdiaint.Checked = chk_xdiacap.Checked
        txt_xdiaminint.Text = txt_xdiamincap.Text
        txt_xdiamaxint.Text = txt_xdiamaxcap.Text
        txt_xdiaminint.Enabled = txt_xdiamincap.Enabled
        txt_xdiamaxint.Enabled = txt_xdiamaxcap.Enabled

        chk_espint.Checked = chk_espcap.Checked
        txt_espminint.Text = txt_espmincap.Text
        txt_espmaxint.Text = txt_espmaxcap.Text
        txt_espminint.Enabled = txt_espmincap.Enabled
        txt_espmaxint.Enabled = txt_espmaxcap.Enabled
        lbl_statperint.Text = "Valores copiados, no olvide guardar."
    End Sub

#End Region

#Region "FORMA PAGO CREDITO"

    Protected Sub Llena_formasPagoCred()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPagosAsignados As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 50, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "[SEL_CNFPCR_FORMAS_PAGO_CRED]"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPagosAsignados, Session("rs"))
        Session("Con").Close()
        If dtPagosAsignados.Rows.Count > 0 Then
            dag_formapago.Visible = True
            dag_formapago.DataSource = dtPagosAsignados
            dag_formapago.DataBind()
        Else
            dag_formapago.Visible = False
        End If
    End Sub

    Protected Sub btn_guardarpagocred_Click(sender As Object, e As EventArgs)
        'Data table que se llena con el contenido del datagrid
        Dim dtPago As New Data.DataTable()
        dtPago.Columns.Add("ID", GetType(Integer))
        dtPago.Columns.Add("NOMBRE", GetType(String))
        dtPago.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_formapago.Rows.Count() - 1
            dtPago.Rows.Add(CInt(dag_formapago.Rows(i).Cells(0).Text), dag_formapago.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_formapago.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_CNFPCR_ASIGNA_PAGO_CRED", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDPROD", SqlDbType.Int)
                Session("parm").Value = Session("PRODID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("FORMAS", SqlDbType.Structured)
                Session("parm").Value = dtPago
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()
                lbl_pagocred.Text = "Guardado correctamente"
            End Using
        Catch ex As Exception
            '<span class="text_input_nice_label" style="margin-left:20px">Rol Número</span>
            lbl_pagocred.Text = "Error"

        Finally
            Llena_formasPagoCred()
        End Try
    End Sub

#End Region

End Class