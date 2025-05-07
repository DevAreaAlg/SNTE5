Public Class CORE_CNF_SISTEMA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Sistema", "Configuración Sistema")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            LLENA_SSL()
            LLENA_ENVIO()
            MostrarDatos()
        End If
    End Sub

    Protected Sub btn_guardar1_Click(sender As Object, e As EventArgs)
        guardar()
    End Sub

    Private Sub LLENA_SSL()
        cmb_ssl.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_ssl.Items.Add(elija)

        Dim item As New ListItem("SI", 1)
        cmb_ssl.Items.Add(item)
        Dim item2 As New ListItem("NO", 0)
        cmb_ssl.Items.Add(item2)
    End Sub
    Private Sub LLENA_ENVIO()
        cmb_envio.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_envio.Items.Add(elija)

        Dim item As New ListItem("SI", 1)
        cmb_envio.Items.Add(item)
        Dim item2 As New ListItem("NO", 0)
        cmb_envio.Items.Add(item2)
    End Sub

    Private Sub guardar()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("SERVIDOR_CORREO", Session("adVarChar"), Session("adParamInput"), 50, txt_correo_servidor.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USUARIO_CORREO", Session("adVarChar"), Session("adParamInput"), 50, txt_usuario_correo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CONTRASEÑA_CORREO", Session("adVarChar"), Session("adParamInput"), 20, txt_psw_correo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PUERTO", Session("adVarChar"), Session("adParamInput"), 10, txt_puerto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CUENTA_CORREO", Session("adVarChar"), Session("adParamInput"), 100, txt_from_correo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SSL", Session("adVarChar"), Session("adParamInput"), 10, cmb_ssl.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RUTA", Session("adVarChar"), Session("adParamInput"), 300, txt_ruta_app.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TAM_DIG", Session("adVarChar"), Session("adParamInput"), 10, txt_tam_dig.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LONG_FOLIO", Session("adVarChar"), Session("adParamInput"), 10, 5)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LONG_CLIENTE", Session("adVarChar"), Session("adParamInput"), 10, 5)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ENVIO", Session("adVarChar"), Session("adParamInput"), 10, cmb_envio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFSISTEMA"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_guardado.Text = "Guardado correctamente"

        MostrarDatos()
    End Sub

    Private Sub MostrarDatos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFSISTEMA"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            txt_correo_servidor.Text = Session("rs").Fields("SERVIDOR").value.ToString
            txt_from_correo.Text = Session("rs").Fields("CORREO_FROM").value.ToString
            txt_puerto.Text = Session("rs").Fields("PUERTO").value.ToString
            txt_psw_correo.Text = Session("rs").Fields("PSW").value.ToString
            txt_usuario_correo.Text = Session("rs").Fields("USUARIO_CORREO").value.ToString
            txt_ruta_app.Text = Session("rs").Fields("RUTA").value.ToString
            txt_tam_dig.Text = Session("rs").Fields("TAM_DIG").value.ToString
            cmb_ssl.Items.FindByValue(Session("rs").Fields("SSL_").Value.ToString).Selected = True
            cmb_envio.Items.FindByValue(Session("rs").Fields("ENVIO").Value.ToString).Selected = True
        End If

        Session("Con").Close()
    End Sub

End Class