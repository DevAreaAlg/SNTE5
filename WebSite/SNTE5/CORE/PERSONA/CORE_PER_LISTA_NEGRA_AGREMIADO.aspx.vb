Public Class CORE_PER_LISTA_NEGRA_AGREMIADO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("ID_BL_AGREMIADO") = 0 Then
            TryCast(Master, MasterMascore).CargaASPX("Agregar Agremiado a Lista Negra", "Agregar Agremiado a Lista Negra")
            lbl_titulo_bl.Text = "Agregar Agremiado a Lista Negra"
        Else
            TryCast(Master, MasterMascore).CargaASPX("Editar Agremiado de Lista Negra", "Editar Agremiado de Lista Negra")
            lbl_titulo_bl.Text = "Editar Agremiado de Lista Negra"
        End If

        If Not IsPostBack Then
            Permisos()
            tbx_id_persona.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + tbx_id_persona.ClientID + "','" + btn_buscar.ClientID + "')")
            btn_buscar_agremiado.Attributes.Add("OnClick", "busquedapersonafisica(1)")

        End If

        If Session("idperbusca") <> "" Then
            tbx_rfc.Text = Session("idperbusca")
            Session("idperbusca") = ""
        End If

    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_buscar.Click
        BuscarAgremiado()
    End Sub

    Private Sub Permisos()

        PermisoAgregarEditarListaNegra()

        If Session("ID_BL_AGREMIADO") = 0 Then

            stn_buscar_agremiado.Visible = True
            tbx_id_registro.Text = 0
            btn_desbloquear.Enabled = False

        Else

            stn_buscar_agremiado.Visible = False
            tbx_id_registro.Text = Session("ID_BL_AGREMIADO").ToString

            If Session("ID_ESTATUS_BL") = 0 Then
                btn_desbloquear.Enabled = False
                btn_guardar.Enabled = False
            Else
                PermisoDesbloquearListaNegra()
            End If

            InformacionAgremiado()

        End If

    End Sub

    Private Sub PermisoAgregarEditarListaNegra()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FACULTAD", Session("adVarChar"), Session("adParamInput"), 50, "ADDBLACKLIST")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIENE_FACULTAD"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("IDTIENEFACULTAD").value.ToString = 0 Then
            btn_guardar.Enabled = False
        Else
            btn_guardar.Enabled = True
        End If

        Session("Con").Close()

    End Sub

    Private Sub PermisoDesbloquearListaNegra()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FACULTAD", Session("adVarChar"), Session("adParamInput"), 50, "DESBLACKLIST")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIENE_FACULTAD"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("IDTIENEFACULTAD").value.ToString = 0 Then
            btn_desbloquear.Enabled = False
        Else
            btn_desbloquear.Enabled = True
        End If

        Session("Con").Close()

    End Sub

    Private Sub BuscarAgremiado()

        lbl_estatus_rfc.Text = ""
        lbl_estatus_agremiado.Text = ""
        tbx_id_registro.Text = 0
        btn_desbloquear.Enabled = False
        IdAgremiado()

        If tbx_id_persona.Text = "" Then
            lbl_estatus_rfc.Text = "Error: Validar el RFC debido a que el agremiado no existe en la base de datos o es incorrecto."
        Else
            InformacionAgremiado()
        End If

    End Sub

    Private Sub IdAgremiado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("EXISTE").value.ToString = -1 Then
            tbx_id_persona.Text = ""
        Else
            tbx_id_persona.Text = Session("rs").fields("IDPERSONA").value.ToString
        End If

        Session("Con").Close()

    End Sub

    Private Sub InformacionAgremiado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_persona.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_BLACKLIST", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_BLACK_LIST_AGREMIADO"
        Session("rs") = Session("cmd").Execute()
        tbx_rfc_agremiado.Text = Session("rs").fields("RFC").value.ToString
        tbx_nombre_agremiado.Text = Session("rs").fields("AGREMIADO").value.ToString
        tbx_notas.Text = Session("rs").fields("NOTAS").value.ToString
        tbx_region.Text = Session("rs").fields("REGION").value.ToString
        tbx_delegacion.Text = Session("rs").fields("DELEGACION").value.ToString
        tbx_ct.Text = Session("rs").fields("CT").value.ToString
        Session("Con").Close()

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_guardar.Click
        GuardarCambios(0) 'Id de Desbloqueo 0 debido a que solo se guardan cambios.
    End Sub

    Private Sub GuardarCambios(ByVal IdDesbloqueo As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_BLACKLIST", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_registro.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, tbx_id_persona.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 15, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, tbx_notas.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_DESBLOQUEO", Session("adVarChar"), Session("adParamInput"), 5, IdDesbloqueo.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_BLACK_LIST"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("RESULTADO").value.ToString = "OK" Then
            lbl_estatus_agremiado.Text = "Éxito: Se ha guardado correctamente los cambios realizados."
            tbx_id_registro.Text = Session("rs").fields("IDBLACKLIST").value.ToString
            tbx_notas.Text = Session("rs").fields("NOTAS").value.ToString
        ElseIf Session("rs").fields("RESULTADO").value.ToString = "EXISTSBL" Then
            lbl_estatus_agremiado.Text = "Error: El agremiado ya ha sido registrado previamente en la lista negra."
            tbx_id_registro.Text = Session("rs").fields("IDBLACKLIST").value.ToString
        ElseIf Session("rs").fields("RESULTADO").value.ToString = "DESBAGREBL" Then
            lbl_estatus_agremiado.Text = "Éxito: Se ha desbloqueado al agremiado."
            tbx_notas.Text = Session("rs").fields("NOTAS").value.ToString
            btn_guardar.Enabled = False
            btn_desbloquear.Enabled = False
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_desbloquear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_desbloquear.Click
        GuardarCambios(1) 'Id de Desbloqueo 1 para no solo guardar cambios, también Desbloquear al Agremiado.
    End Sub

End Class