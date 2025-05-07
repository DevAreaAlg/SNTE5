Public Class CRED_PROD_APARTADO6
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Tasas de interés e IVA", "Tasas de Interés/IVA")

        If Not Me.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                'LLENO LOS RESPECTIVOS LABELS
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString

                Llenaiva()
                Llenaindices()
                LlenaDatos()
            End If
        End If
    End Sub

    Private Sub Llenaiva()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_IVA"
        Session("rs") = Session("cmd").Execute()

        'If Session("rs").Fields("IVA").Value.ToString <> "-100.00" Then

        '    If Session("rs").Fields("IVA").Value.ToString = "-1.00" Then 'fue seleccionado iva de sucursal
        '        cmb_fuenteiva.Items.FindByValue("SUC").Selected = True
        '        txt_iva.Text = ""
        '        txt_iva.Enabled = False
        '    Else 'fue seleccionado iva especial
        '        cmb_fuenteiva.Items.FindByValue("PROD").Selected = True
        '        txt_iva.Text = Session("rs").Fields("IVA").Value.ToString
        '        txt_iva.Enabled = True
        '    End If
        'Else
        '    Dim elija As New ListItem("ELIJA", "0")
        '    cmb_fuenteiva.Items.Add(elija)
        '    cmb_fuenteiva.Items.FindByValue("0").Selected = True
        '    RequiredFieldValidator_fuenteiva.Enabled = True

        'End If

        If Session("rs").fields("MANEJO_IVA").value.ToString = "2" Then
            rad_Interes_Gravado.Checked = True
        ElseIf Session("rs").fields("MANEJO_IVA").value.ToString = "1" Then
            rad_IVA_Exento.Checked = True
        Else
            rad_IVA_Normal.Checked = True
        End If

        Session("Con").Close()

    End Sub

    Private Sub Llenaindices()
        cmb_indice_normal_ind.Items.Clear()
        cmb_indice_mora_ind.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        Dim elija2 As New ListItem("ELIJA", "0")
        cmb_indice_normal_ind.Items.Add(elija)
        cmb_indice_mora_ind.Items.Add(elija2)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDINDICES", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_INDICES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATINDIFINAN_NOMBRE").Value.ToString, Session("rs").Fields("CATINDIFINAN_ID_INDICE").Value.ToString)
            Dim item2 As New ListItem(Session("rs").Fields("CATINDIFINAN_NOMBRE").Value.ToString, Session("rs").Fields("CATINDIFINAN_ID_INDICE").Value.ToString)
            cmb_indice_normal_ind.Items.Add(item)
            cmb_indice_mora_ind.Items.Add(item2)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaDatos()
        'TASA NORMAL FIJA
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAS", Session("adVarChar"), Session("adParamInput"), 10, "FIJ")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_DATOS_TASAS"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("BE").Value.ToString = "1" Then
            txt_tasa_normal_fija_min.Text = Session("rs").Fields("MSTTASAS_TASA_MIN").Value.ToString
            txt_tasa_normal_fija_max.Text = Session("rs").Fields("MSTTASAS_TASA_MAX").Value.ToString

            If Session("rs").Fields("MSTTASAS_ESTATUS").Value.ToString = "1" Then
                chk_estatus_normal_fija.Checked = True
            End If
            If Session("rs").Fields("MSTTASAS_VARIABLE").Value.ToString = "1" Then
                chk_variable_normal_fija.Checked = True
            End If
        End If
        Session("Con").Close()
        'Do While Not Session("rs").EOF
        '    Dim item As New ListItem(Session("rs").Fields("CATSUCURSAL_NOMBRE").Value.ToString, Session("rs").Fields("CATSUCURSAL_ID_SUCURSAL").Value.ToString)
        '    cmb_sucursal.Items.Add(item)

        '    Session("rs").movenext()
        'Loop

        'TASA NORMAL INDIZADA
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "NOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAS", Session("adVarChar"), Session("adParamInput"), 10, "IND")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_DATOS_TASAS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("BE").Value.ToString = "1" Then

            txt_tasa_normal_ind_min.Text = Session("rs").Fields("MSTTASAS_TASA_MIN").Value.ToString
            txt_tasa_normal_ind_max.Text = Session("rs").Fields("MSTTASAS_TASA_MAX").Value.ToString

            If Session("rs").Fields("MSTTASAS_ESTATUS").Value.ToString = "1" Then
                chk_estatus_normal_ind.Checked = True
            End If
            If Session("rs").Fields("MSTTASAS_VARIABLE").Value.ToString = "1" Then
                chk_variable_normal_ind.Checked = True
            End If
            cmb_indice_normal_ind.Items.FindByValue(Session("rs").Fields("INDICE").Value.ToString).Selected = True
        End If
        Session("Con").Close()
        'TASA MORATORIA FIJA
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "MOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAS", Session("adVarChar"), Session("adParamInput"), 10, "FIJ")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_DATOS_TASAS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("BE").Value.ToString = "1" Then

            txt_tasa_mora_fija_min.Text = Session("rs").Fields("MSTTASAS_TASA_MIN").Value.ToString
            txt_tasa_mora_fija_max.Text = Session("rs").Fields("MSTTASAS_TASA_MAX").Value.ToString

            If Session("rs").Fields("MSTTASAS_ESTATUS").Value.ToString = "1" Then
                chk_estatus_mora_fija.Checked = True
            End If
            If Session("rs").Fields("MSTTASAS_VARIABLE").Value.ToString = "1" Then
                chk_variable_mora_fija.Checked = True
            End If
        End If
        Session("Con").Close()

        'TASA MORATORIA INDIZADA
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, "MOR")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAS", Session("adVarChar"), Session("adParamInput"), 10, "IND")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_DATOS_TASAS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("BE").Value.ToString = "1" Then

            txt_tasa_mora_ind_min.Text = Session("rs").Fields("MSTTASAS_TASA_MIN").Value.ToString
            txt_tasa_mora_ind_max.Text = Session("rs").Fields("MSTTASAS_TASA_MAX").Value.ToString

            If Session("rs").Fields("MSTTASAS_ESTATUS").Value.ToString = "1" Then
                chk_estatus_mora_ind.Checked = True
            End If
            If Session("rs").Fields("MSTTASAS_VARIABLE").Value.ToString = "1" Then
                chk_variable_mora_ind.Checked = True
            End If
            cmb_indice_mora_ind.Items.FindByValue(Session("rs").Fields("INDICE").Value.ToString).Selected = True
        End If

        Session("Con").Close()
    End Sub

    Private Sub GuardaTasas(ByVal tasamin As Decimal, ByVal tasamax As Decimal, ByVal tipo As String, ByVal clas As String, ByVal var As Integer, ByVal idindice As Integer, ByVal estatus As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMIN", Session("adVarChar"), Session("adParamInput"), 10, tasamin)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMAX", Session("adVarChar"), Session("adParamInput"), 10, tasamax)
        Session("cmd").Parameters.Append(Session("parm"))
        'Session("parm") = Session("cmd").CreateParameter("TANTOS", Session("adVarChar"), Session("adParamInput"), 10, tantos)
        'Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAS", Session("adVarChar"), Session("adParamInput"), 10, clas)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VARIABLE", Session("adVarChar"), Session("adParamInput"), 10, var)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 10, idindice)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFPCR_DATOS_TASAS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

    Protected Sub btn_guardarnormfija_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarnormfija.Click
        Dim varnorfij As Integer = 0
        Dim stsnorfij As Integer = 0
        If chk_variable_normal_fija.Checked = True Then
            varnorfij = 1
        End If
        If chk_estatus_normal_fija.Checked = True Then
            stsnorfij = 1
        End If
        If txt_tasa_normal_fija_min.Text <> "" And txt_tasa_normal_fija_max.Text <> "" Then
            If CDec(txt_tasa_normal_fija_min.Text) <= CDec(txt_tasa_normal_fija_max.Text) Then
                If CDec(txt_tasa_normal_fija_min.Text) <= 999 Or CDec(txt_tasa_normal_fija_max.Text) <= 999 Then
                    GuardaTasas(txt_tasa_normal_fija_min.Text, txt_tasa_normal_fija_max.Text, "NOR", "FIJ", varnorfij, 0, stsnorfij)
                    lbl_statusnorfij.Text = "Guardado correctamente"
                    Llenaindices()
                    LlenaDatos()
                Else
                    lbl_statusnorfij.Text = "Error: Las tasas no pueden ser mayor a 100"
                End If
            Else
                lbl_statusnorfij.Text = "Error: La tasa mínima es mayor que la tasa máxima"
            End If
        Else
            lbl_statusnorfij.Text = "Error: Datos incompletos"
        End If
    End Sub

    Protected Sub btn_guardarnormind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarnormind.Click

        Dim varnorind As Integer = 0

        Dim stsnorind As Integer = 0

        If chk_variable_normal_ind.Checked = True Then
            varnorind = 1
        End If

        If chk_estatus_normal_ind.Checked = True Then
            stsnorind = 1
        End If

        If txt_tasa_normal_ind_min.Text <> "" And txt_tasa_normal_ind_max.Text <> "" And cmb_indice_normal_ind.SelectedItem.Value <> "0" Then
            If CDec(txt_tasa_normal_ind_min.Text) <= CDec(txt_tasa_normal_ind_max.Text) Then
                If CDec(txt_tasa_normal_ind_min.Text) <= 999 Or CDec(txt_tasa_normal_ind_max.Text) <= 999 Then
                    GuardaTasas(txt_tasa_normal_ind_min.Text, txt_tasa_normal_ind_max.Text, "NOR", "IND", varnorind, cmb_indice_normal_ind.SelectedItem.Value, stsnorind)
                    lbl_statusnorind.Text = "Guardado correctamente"
                    Llenaindices()
                    LlenaDatos()
                Else
                    lbl_statusnorind.Text = "Error: Las tasas no pueden ser mayor a 100"
                End If

            Else
                lbl_statusnorind.Text = "Error: La tasa mínima es mayor que la tasa máxima"
            End If
        Else
            lbl_statusnorind.Text = "Error: Datos incompletos"
        End If

    End Sub

    Protected Sub btn_guardarmorafija_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarmorafija.Click
        Dim varnorfij As Integer = 0
        Dim stsnorfij As Integer = 0
        If chk_variable_mora_fija.Checked = True Then
            varnorfij = 1
        End If
        If chk_estatus_mora_fija.Checked = True Then
            stsnorfij = 1
        End If
        If txt_tasa_mora_fija_min.Text <> "" And txt_tasa_mora_fija_max.Text <> "" Then
            If CDec(txt_tasa_mora_fija_min.Text) <= CDec(txt_tasa_mora_fija_max.Text) Then
                If CDec(txt_tasa_mora_fija_min.Text) <= 999 Or CDec(txt_tasa_mora_fija_max.Text) <= 999 Then
                    GuardaTasas(txt_tasa_mora_fija_min.Text, txt_tasa_mora_fija_max.Text, "MOR", "FIJ", varnorfij, 0, stsnorfij)
                    lbl_statusmorfij.Text = "Guardado correctamente"
                    Llenaindices()
                    LlenaDatos()
                Else
                    lbl_statusmorfij.Text = "Error: Las tasas no pueden ser mayor a 100"
                End If
            Else
                lbl_statusmorfij.Text = "Error: La tasa mínima es mayor que la tasa máxima"
            End If
        Else
            lbl_statusmorfij.Text = "Error: Datos incompletos"
        End If

    End Sub

    Protected Sub btn_guardarmoraind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarmoraind.Click
        Dim varnorind As Integer = 0

        Dim stsnorind As Integer = 0

        If chk_variable_mora_ind.Checked = True Then
            varnorind = 1
        End If

        If chk_estatus_mora_ind.Checked = True Then
            stsnorind = 1
        End If

        If txt_tasa_mora_ind_min.Text <> "" And txt_tasa_mora_ind_max.Text <> "" And cmb_indice_mora_ind.SelectedItem.Value <> "0" Then
            If CDec(txt_tasa_mora_ind_min.Text) <= CDec(txt_tasa_mora_ind_max.Text) Then
                If CDec(txt_tasa_mora_ind_min.Text) <= 999 Or CDec(txt_tasa_mora_ind_max.Text) <= 999 Then
                    GuardaTasas(txt_tasa_mora_ind_min.Text, txt_tasa_mora_ind_max.Text, "MOR", "IND", varnorind, cmb_indice_mora_ind.SelectedItem.Value, stsnorind)
                    lbl_statusmorind.Text = "Guardado correctamente"
                    Llenaindices()
                    LlenaDatos()
                Else
                    lbl_statusmorind.Text = "Error: Las tasas no pueden ser mayor a 100"
                End If
            Else
                lbl_statusmorind.Text = "Error: La tasa mínima es mayor que la tasa máxima"
            End If
        Else
            lbl_statusmorind.Text = "Error: Datos incompletos"
        End If
    End Sub

    Protected Sub btn_guardaiva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardaiva.Click

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("parm") = Session("cmd").CreateParameter("IVA", Session("adVarChar"), Session("adParamInput"), 10, 16)
        Session("cmd").Parameters.Append(Session("parm"))

        If rad_Interes_Gravado.Checked = True Then
            Session("parm") = Session("cmd").CreateParameter("MANEJO_IVA", Session("adVarChar"), Session("adParamInput"), 10, 2)
            Session("cmd").Parameters.Append(Session("parm"))
        ElseIf rad_IVA_Exento.Checked = True Then
            Session("parm") = Session("cmd").CreateParameter("MANEJO_IVA", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("MANEJO_IVA", Session("adVarChar"), Session("adParamInput"), 10, 0)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFPCR_IVA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_statusiva.Text = "Guardado correctamente"

    End Sub

End Class