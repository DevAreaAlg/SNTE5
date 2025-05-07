Public Class CAP_PROD_APARTADO2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Tasas", "TASAS")

        If Not Me.IsPostBack Then
            lbl_Producto.Text = Session("PROD_NOMBRE").ToString
            Llenaindices()
            LlenaDatos()
        End If
    End Sub

    'llena los indices financieros
    Private Sub Llenaindices()
        cmb_indice_normal_ind.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_indice_normal_ind.Items.Add(elija)


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_INDICES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATINDIFINAN_NOMBRE").Value.ToString, Session("rs").Fields("CATINDIFINAN_ID_INDICE").Value.ToString)

            cmb_indice_normal_ind.Items.Add(item)


            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    'Muestra los datos que fueron registrados para ese producto
    Private Sub LlenaDatos()

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

        End If
        Session("Con").Close()

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

            cmb_indice_normal_ind.Items.FindByValue(Session("rs").Fields("INDICE").Value.ToString).Selected = True
        End If
        Session("Con").Close()


    End Sub


    'Btn Guarda tasa normal
    Protected Sub btn_guardarnormfija_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarnormfija.Click
        Dim varnorfij As Integer = 0
        Dim stsnorfij As Integer = 0
        lbl_statusnorind.Text = ""
        If chk_estatus_normal_fija.Checked = True Then
            stsnorfij = 1
        End If

        If txt_tasa_normal_fija_min.Text <> "" And txt_tasa_normal_fija_max.Text <> "" Then
            If CDec(txt_tasa_normal_fija_min.Text) <= CDec(txt_tasa_normal_fija_max.Text) Then
                If CDec(txt_tasa_normal_fija_min.Text) <= 999 Or CDec(txt_tasa_normal_fija_max.Text) <= 999 Then
                    GuardaTasas(txt_tasa_normal_fija_min.Text, txt_tasa_normal_fija_max.Text, "NOR", "FIJ", 0, stsnorfij)
                    lbl_statusnorfij.Text = "Guardado correctamente"
                    Llenaindices()
                    LlenaDatos()
                Else
                    lbl_statusnorfij.Text = "Las tasas no pueden ser mayor de 100"
                End If

            Else
                lbl_statusnorfij.Text = "La tasa minima es mayor que la tasa máxima"
            End If
        Else
            lbl_statusnorfij.Text = "Datos incompletos"
        End If


    End Sub

    'Btn guarda la tasa indizada
    Protected Sub btn_guardarnormind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardarnormind.Click


        Dim varnorind As Integer = 0
        Dim stsnorind As Integer = 0
        lbl_statusnorfij.Text = ""
        If chk_estatus_normal_ind.Checked = True Then
            stsnorind = 1
        End If

        If txt_tasa_normal_ind_min.Text <> "" And txt_tasa_normal_ind_max.Text <> "" And cmb_indice_normal_ind.SelectedItem.Value <> "0" Then
            If CDec(txt_tasa_normal_ind_min.Text) <= CDec(txt_tasa_normal_ind_max.Text) Then
                If CDec(txt_tasa_normal_ind_min.Text) <= 999 Or CDec(txt_tasa_normal_ind_max.Text) <= 999 Then
                    GuardaTasas(txt_tasa_normal_ind_min.Text, txt_tasa_normal_ind_max.Text, "NOR", "IND", cmb_indice_normal_ind.SelectedItem.Value, stsnorind)
                    lbl_statusnorind.Text = "Guardado correctamente"
                    Llenaindices()
                    LlenaDatos()
                Else
                    lbl_statusnorind.Text = "Las tasas no pueden ser mayor de 100"
                End If

            Else
                lbl_statusnorind.Text = "La tasa minima es mayor que la tasa máxima"
            End If
        Else
            lbl_statusnorind.Text = "Datos incompletos"
        End If

    End Sub

    'Guarda Tasas
    Private Sub GuardaTasas(ByVal tasamin As Decimal, ByVal tasamax As Decimal, ByVal tipo As String, ByVal clas As String, ByVal idindice As Integer, ByVal estatus As Integer)

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
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 10, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAS", Session("adVarChar"), Session("adParamInput"), 10, clas)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINDICE", Session("adVarChar"), Session("adParamInput"), 10, idindice)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 10, estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFPRODCAP_TASAS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub

End Class