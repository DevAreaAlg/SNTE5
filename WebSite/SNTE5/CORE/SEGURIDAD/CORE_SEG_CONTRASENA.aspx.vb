Public Class CORE_SEG_CONTRASENA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Cambiar mi contraseña", "Cambiar mi Contraseña")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If
            Llenapreguntas()
        End If

    End Sub

    'Muestra los plazos
    Private Sub Llenapreguntas()
        cmb_pregunta.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_pregunta.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CATALOGO_PREGUNTA_SECRETA"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("PREGUNTA").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_pregunta.Items.Add(item)
            Session("rs").movenext()

        Loop

        Session("Con").Close()

    End Sub

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click
        'Verifico contraseña
        If VerContraseña(txt_pwdn1.Text, txt_pwdn2.Text) = 0 And txt_pwdn1.Text <> "" And txt_pwdn2.Text <> "" And txt_antpwd.Text <> "" Then 'contraseña correcta
            'se ejecuta sp que verifica que el password nuevo cumpla con las politicas de seguridad y lo actualiza
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ACTUALPWD", Session("adVarChar"), Session("adParamInput"), 15, txt_antpwd.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PWD", Session("adVarChar"), Session("adParamInput"), 15, txt_pwdn1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 17, Session("MAC"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERTRANS", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_CONTRASEÑA_USR"
            Session("rs") = Session("cmd").Execute()
            'lbl_Results.Text = Session("rs").Fields("RESPUESTA").Value.ToString + " " + Session("rs").Fields("VALIDA").Value.ToString + txt_antpwd.Text + txt_pwdn1.Text + Session("USERID").ToString
            If Session("rs").Fields("RESPUESTA").Value.ToString = 1 Then
                'MUESTRO PANEL CON PREGUNTA SI EL CORREO ESTA REGISTRADO Y LA CUENTA HABILITADA
                If Session("rs").Fields("VALIDA").Value.ToString = "True" Then 'CONTRASEÑA ACTUALIZADA EXITOSAMENTE

                    lbl_Results.Text = "Guardado correctamente"
                Else
                    lbl_Results.Text = "Error: Contraseña inválida"
                    txt_pwdn1.Text = ""
                    txt_pwdn2.Text = ""
                    txt_antpwd.Text = ""
                End If
            Else
                lbl_Results.Text = "Error: Contraseña actual inválida"
                txt_pwdn1.Text = ""
                txt_pwdn2.Text = ""
                txt_antpwd.Text = ""
            End If


        Else 'contraseña diferente
            lbl_Results.Text = "Error: Falta contraseña o no coinciden"
            txt_pwdn1.Text = ""
            txt_pwdn2.Text = ""
            txt_antpwd.Text = ""


        End If

    End Sub

    Private Function VerContraseña(ByVal PSW As String, ByVal PSW2 As String) As Integer
        Return String.Compare(PSW, PSW2)
    End Function

    Protected Sub lnk_respuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_respuesta.Click
        pnl_pswd.Visible = False
        pnl_secreta.Visible = True
    End Sub

    Protected Sub lnk_regresar_respuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_regresar_respuesta.Click
        pnl_secreta.Visible = False
        pnl_pswd.Visible = True
        txt_antpwd0.Text = ""
        txt_respuesta.Text = ""
        txt_r2.Text = ""
        cmb_pregunta.SelectedIndex = "0"
        lbl_Resultado.Text = ""
        lbl_Results.Text = ""
    End Sub

    Protected Sub BTN_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTN_cancelar.Click
        pnl_secreta.Visible = False
        pnl_pswd.Visible = True
        txt_antpwd0.Text = ""
        txt_respuesta.Text = ""
        txt_r2.Text = ""
        cmb_pregunta.SelectedIndex = "0"
        lbl_Resultado.Text = ""
        lbl_Results.Text = ""
    End Sub

    Protected Sub BTN_guardar_secreta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTN_guardar_secreta.Click
        'Verifico contraseña
        If VerContraseña(txt_respuesta.Text, txt_r2.Text) = 0 And txt_respuesta.Text <> "" And txt_r2.Text <> "" Then 'contraseña correcta

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ACTUALPWD", Session("adVarChar"), Session("adParamInput"), 15, txt_antpwd0.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_PREGUNTA", Session("adVarChar"), Session("adParamInput"), 15, cmb_pregunta.SelectedItem.Value.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("RESP", Session("adVarChar"), Session("adParamInput"), 50, txt_respuesta.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 17, Session("MAC"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERTRANS", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_PREGUNTA_SECRETA"
            Session("rs") = Session("cmd").Execute()


            If Session("rs").Fields("RESPUESTA").Value.ToString = "1" Then
                lbl_Resultado.Text = "Pregunta y respuesta secreta actualizadas"
                cmb_pregunta.SelectedIndex = "0"
                txt_r2.Text = ""
                txt_respuesta.Text = ""
                txt_antpwd0.Text = ""
                Session("Con").Close()
            Else
                lbl_Resultado.Text = "Error: Contraseña actual inválida"
                txt_respuesta.Text = ""
                txt_r2.Text = ""
                txt_antpwd0.Text = ""
                cmb_pregunta.SelectedIndex = "0"

            End If


        Else
            lbl_Resultado.Text = "Error: No coinciden las respuestas secretas"
            txt_r2.Text = ""
            txt_respuesta.Text = ""
            txt_antpwd0.Text = ""
            cmb_pregunta.SelectedIndex = "0"


        End If

    End Sub

    Protected Sub cmb_pregunta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_pregunta.SelectedIndexChanged
        lbl_Resultado.Text = ""
    End Sub

End Class