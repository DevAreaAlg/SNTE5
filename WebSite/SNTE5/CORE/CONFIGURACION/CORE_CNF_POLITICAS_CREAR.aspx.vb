Public Class CORE_CNF_POLITICAS_CREAR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Edición de Políticas", "Edición Políticas")

        If Not Me.IsPostBack Then
            If Session("ID_POLITICA") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                Dim id_poli As String
                id_poli = Session("ID_POLITICA")

                If id_poli = "0" Then
                    txt_id_Politica.Text = "Nueva Política"
                End If
                MostrarPolitica()
            End If
        End If
    End Sub


    Protected Sub click_btn_limpiar()
        txt_nombre_politica.Text = ""
        txt_tiempo_inactividad.Text = ""
        txt_intentos_fallidos.Text = ""
        txt_dias_expiracion.Text = ""
        txt_mem_contrasena.Text = ""
        timepicker1.Text = ""
        timepicker2.Text = ""
        ckb_Activo.Checked = False
        Session("ID_POLITICA") = "0"
        lbl_statupol.Text = ""
    End Sub


    Private Sub MostrarPolitica()
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPOLITICA", Session("adVarChar"), Session("adParamInput"), 50, Session("ID_POLITICA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_POLITICA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_id_Politica.Text = Session("rs").Fields("IDPOLITICA").value.ToString
            txt_nombre_politica.Text = Session("rs").Fields("POLITICA").value.ToString
            txt_tiempo_inactividad.Text = Session("rs").Fields("INACIVIDAD").value.ToString
            txt_intentos_fallidos.Text = Session("rs").Fields("INTENTOS_FALLIDOS").value.ToString
            txt_dias_expiracion.Text = Session("rs").Fields("DIAS_EXPIRACION").value.ToString
            timepicker1.Text = Session("rs").Fields("HORA_INI").value.ToString
            timepicker2.Text = Session("rs").Fields("HORA_FIN").value.ToString
            txt_mem_contrasena.Text = Session("rs").Fields("MEMORIA").value.ToString
            txt_tpo_bloqueo.Text = Session("rs").Fields("TIEMPO_BLOQUEO").value.ToString
            If Session("rs").Fields("ESTATUS").value.ToString = "1" Then
                ckb_Activo.Checked = True
            ElseIf Session("rs").Fields("ESTATUS").value.ToString = "0" Then
                ckb_Activo.Checked = False
            End If

        End If

        Session("Con").Close()

    End Sub


    Protected Sub click_btn_guardar_datos(ByVal sender As Object, ByVal e As EventArgs)

        Dim Estatus As Integer
        If ckb_Activo.Checked = False Then
            Estatus = 0
        Else
            Estatus = 1
        End If


        If (CInt(txt_tpo_bloqueo.Text) > 0) And (CInt(txt_tpo_bloqueo.Text) <= CInt(txt_tiempo_inactividad.Text)) Then
            lbl_statupol.Text = "Error: Los minutos de bloqueo no pueden ser menor o igual a tiempo inactividad"
            Exit Sub
        End If


        If (txt_intentos_fallidos.Text) <= 0 Then
            lbl_statupol.Text = "Error: El número de intentos fallidos no puede ser 0, por favor, verifique."
            Exit Sub
        End If



        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPOLITICA", Session("adVarChar"), Session("adParamInput"), 50, Session("ID_POLITICA"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 50, txt_nombre_politica.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIEMPO_INACTIVIDAD", Session("adVarChar"), Session("adParamInput"), 20, CInt(txt_tiempo_inactividad.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INTENTOS_PERMITIDOS", Session("adVarChar"), Session("adParamInput"), 20, CInt(txt_intentos_fallidos.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIAS_EXPIRA", Session("adVarChar"), Session("adParamInput"), 20, CInt(txt_dias_expiracion.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORARIO_INI", Session("adVarChar"), Session("adParamInput"), 20, timepicker1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("HORARIO_FIN", Session("adVarChar"), Session("adParamInput"), 20, timepicker2.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MEMORIA_CONTRASENA", Session("adVarChar"), Session("adParamInput"), 20, CInt(txt_mem_contrasena.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 20, Estatus)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSUARIO", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MINUTOS_BLOQUEO", Session("adVarChar"), Session("adParamInput"), 20, CInt(txt_tpo_bloqueo.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_UPD_POLITICA"
        Session("rs") = Session("cmd").Execute()
        Session("ID_POLITICA") = Session("rs").fields("ID").value.ToString
        txt_id_Politica.Text = Session("ID_POLITICA").ToString
        Session("Con").Close()
        MostrarPolitica()

        lbl_statupol.Text = "Guardado correctamente"
    End Sub

End Class