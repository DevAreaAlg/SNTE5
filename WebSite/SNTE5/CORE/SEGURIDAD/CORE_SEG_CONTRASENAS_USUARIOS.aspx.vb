Public Class CORE_SEG_CONTRASENAS_USUARIOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Operaciones", "CONFIGURACIÓN OPERACIONES")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            'LLENO LOS COMBOS
            LlenaUsuarios()
            LlenaBloqueados()
            LlenaUsuariosSesionBloqueada()
        End If
    End Sub

    Private Sub LlenaUsuarios()

        cmb_usuarios2.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_usuarios2.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_usuarios2.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub cmb_usuarios2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_usuarios2.SelectedIndexChanged
        lnk_generar.Enabled = True
        btn_Guardar.Enabled = True
    End Sub

    Protected Sub lnk_generar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_generar.Click
        lbl_Results.Text = ""
        Dim cadena As String

        Dim RndNum As New Random()


        cadena = Session("MascoreG").randkeyMinMay(7) + RndNum.Next(0, 9).ToString
        actualizacontraseña(cadena)
    End Sub

    Private Sub actualizacontraseña(ByVal cadena As String)

        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim RESP As String = String.Empty
        Dim PREGUNTA As String = String.Empty
        Dim USER As String = String.Empty

        Dim nombre As String = cmb_usuarios2.SelectedItem.Text

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANTERIORPWD", Session("adVarChar"), Session("adParamInput"), 15, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PWD", Session("adVarChar"), Session("adParamInput"), 15, cadena)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, cmb_usuarios2.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 50, Session("MAC"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERTRANS", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CONTRASEÑA_USR"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").Fields("PREGUNTA").Value.ToString Is Nothing Then
            PREGUNTA = Session("rs").Fields("PREGUNTA").Value.ToString
        End If
        If Not Session("rs").Fields("RESP").Value.ToString Is Nothing Then
            RESP = Session("rs").Fields("RESP").Value.ToString
        End If
        If Not Session("rs").Fields("USUARIO").Value.ToString Is Nothing Then
            USER = Session("rs").Fields("USUARIO").Value.ToString
        End If
        If Session("rs").Fields("VALIDA").Value.ToString = True Then 'CONTRASEÑA ACTUALIZADA EXITOSAMENTE
            Dim sbhtml As StringBuilder = New StringBuilder
            subject = "Contraseña del Sistema"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) usuario:  " + nombre + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td>Información del usuario:</td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Por medio de la presente se le informa que su contraseña generada en el sistema es: <b>" + cadena + "</b></td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Para el usuario: <b>" + USER + "</b></td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Su pregunta secreta es: <b>" + PREGUNTA + "</b></td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Su respuesta secreta generada es: <b>" + RESP + "</b></td></tr>")
            sbhtml.Append("<br/>")
            sbhtml.Append("<tr><td>Favor de ingresar al sistema y cambiar su contraseña, su pregunta y su respuesta secreta.</td></tr>")
            sbhtml.Append("<br/><br/><br/>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")

            If Not (clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)) Then
                lbl_Results.Text = "Error: Contraseña inválida, genere de nuevo una contraseña"
            Else
                lbl_Results.Text = "EL usuario ha sido informado de su contraseña generada"
            End If

        Else


        End If

        Session("Con").Close()
    End Sub



    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click
        lbl_Results.Text = ""
        Dim clase_correo As New Correo
        Dim sbhtml As New StringBuilder
        Dim subject As String = String.Empty
        Dim cc As String = String.Empty
        Dim RESP As String = String.Empty
        Dim PREGUNTA As String = String.Empty
        Dim USER As String = String.Empty
        Dim nombre As String = cmb_usuarios2.SelectedItem.Text
        Dim cadena As String = String.Empty
        'Verifico que haya usuario seleccionado
        If cmb_usuarios2.SelectedItem.Value = "0" Then
            lbl_Results.Text = "Error: Seleccione un usuario"
            Exit Sub
        End If

        'Verifico contraseña
        If VerContraseña() = 0 And txt_pwdn1.Text <> "" And txt_pwdn2.Text <> "" Then 'contraseña correcta
            'se ejecuta sp que verifica que el password nuevo cumpla con las politicas de seguridad y lo actualiza
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ANTERIORPWD", Session("adVarChar"), Session("adParamInput"), 15, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PWD", Session("adVarChar"), Session("adParamInput"), 15, txt_pwdn1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, cmb_usuarios2.SelectedItem.Value.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 50, Session("MAC"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERTRANS", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_CONTRASEÑA_USR"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").Fields("PREGUNTA").Value.ToString Is Nothing Then
                PREGUNTA = Session("rs").Fields("PREGUNTA").Value.ToString
            End If
            If Not Session("rs").Fields("RESP").Value.ToString Is Nothing Then
                RESP = Session("rs").Fields("RESP").Value.ToString
            End If
            If Not Session("rs").Fields("USUARIO").Value.ToString Is Nothing Then
                USER = Session("rs").Fields("USUARIO").Value.ToString
            End If
            subject = "Contraseña del Sistema"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center'  colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) usuario:  " + nombre + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma' cellpadding='0' cellspacing='0' border='0'>")
            sbhtml.Append("<tr><td width='25%'>Información del usuario:</td></td></tr>")
            sbhtml.Append("<tr><td width='75%'>Por medio de la presente se le informa que su contraseña generada en el sistema es:</td><td>" + "<b>" + txt_pwdn1.Text + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Para el usuario:</td>" + "<b>" + USER + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='30%'>Su pregunta secreta es:</td>" + "<b>" + PREGUNTA + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='50%'>Su respuesta secreta generada es:</td>" + "<b>" + RESP + "</b>" + "</td></tr>")
            sbhtml.Append("<tr><td width='250'>Favor de ingresar al sistema y cambiar su contraseña, su pregunta y su respuesta secreta.</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            'MUESTRO PANEL CON PREGUNTA SI EL CORREO ESTA REGISTRADO Y LA CUENTA HABILITADA
            If Session("rs").Fields("VALIDA").Value.ToString = True Then 'CONTRASEÑA ACTUALIZADA EXITOSAMENTE
                lbl_Results.Text = ""
                If Not (clase_correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)) Then
                    lbl_Results.Text = "Contraseña cambiada exitosamente, (Error en el envio de correo)"
                Else

                    lbl_Results.Text = "Guardado correctamente"
                End If

            Else
                lbl_Results.Text = "Error: Contraseña inválida"
                txt_pwdn1.Text = ""
                txt_pwdn2.Text = ""
            End If
            Session("Con").Close()
        Else 'contraseña diferente
            lbl_Results.Text = "Error: Contraseñas no coinciden"
            txt_pwdn1.Text = ""
            txt_pwdn2.Text = ""
        End If

    End Sub

    Private Function VerContraseña() As Integer
        Return String.Compare(txt_pwdn1.Text, txt_pwdn2.Text)
    End Function

    Private Sub LlenaBloqueados()

        cmb_usuariobloq.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")

        cmb_usuariobloq.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_BLOQUEADOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("MSTUSUARIO_ID").Value.ToString)
            cmb_usuariobloq.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub
    Protected Sub btn_desbloquear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_desbloquear.Click
        lbl_statbloq.Text = ""

        'Verifico que haya usuario seleccionado
        If cmb_usuariobloq.SelectedItem.Value = "0" Then
            lbl_statbloq.Text = "Error: Seleccione un usuario a desbloquear"
            Exit Sub
        End If
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, cmb_usuariobloq.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 50, Session("MAC"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERTRANS", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_DESBLOQUEA_USR"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_statbloq.Text = "Guardado correctamente"
        LlenaBloqueados()
        LlenaUsuarios()
    End Sub
    Private Sub LlenaUsuariosSesionBloqueada()

        cmb_usuarioSes.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")

        cmb_usuarioSes.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_SESION_BLOQUEADA"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            If Session("rs").Fields("MSTUSUARIO_ID").Value <> Session("USERID") Then
                Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("MSTUSUARIO_ID").Value.ToString)
                cmb_usuarioSes.Items.Add(item)
            End If
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub BtnDesBloqueSesion_Click(sender As Object, e As EventArgs) Handles BtnDesBloqueSesion.Click
        Lbl_staSesion.Text = ""

        'Verifico que haya usuario seleccionado
        If cmb_usuarioSes.SelectedItem.Value = "0" Then
            Lbl_staSesion.Text = "Error: Seleccione un usuario para desbloquear sesión"
            Exit Sub
        End If

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, cmb_usuarioSes.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 15, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_LOGIN_USUARIO"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        Lbl_staSesion.Text = "Guardado correctamente"
        LlenaUsuariosSesionBloqueada()
    End Sub

End Class