Public Class CRED_CNF_DESBLOQUEO_EXP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Desbloqueo de Expedientes", "Desbloqueo de Expedientes")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            'LLENO LOS COMBOS
            LlenaFoliosBloqueados()
            LlenaFoliosBloqueadosValidador()
            LlenaUsuarios()
        End If
    End Sub

    Private Sub LlenaFoliosBloqueados()

        cmb_DesExpedienteDig.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_DesExpedienteDig.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FOLIOS_BLOQUEADOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF

            Dim folio As String = Session("rs").Fields("FOLIO").Value.ToString
            Dim clave As String = Session("rs").Fields("CLAVE").Value.ToString
            Dim item As New ListItem(("FOLIO: " + clave), folio)
            cmb_DesExpedienteDig.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub btn_DesExpedienteDig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_DesExpedienteDig.Click
        lbl_status.Text = ""
        lbl_AlertaDesExpedienteDig.Text = ""

        'Verifico que haya usuario seleccionado
        If cmb_DesExpedienteDig.SelectedItem.Value = "0" Then
            lbl_AlertaDesExpedienteDig.Text = "Error: Seleccione un expediente a desbloquear"
            Exit Sub
        End If
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, cmb_DesExpedienteDig.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_ESTATUS_EN_DIGITALIZACION"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_AlertaDesExpedienteDig.Text = "Expediente desbloqueado exitosamente"

        LlenaFoliosBloqueados()

    End Sub


    Private Sub LlenaFoliosBloqueadosValidador()

        cmb_folio.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        cmb_folio.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FOLIOS_BLOQUEADOS_VALIDADOR"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim folio As String = Session("rs").Fields("FOLIO").Value.ToString
            Dim clave As String = Session("rs").Fields("CLAVE").Value.ToString
            Dim Usuario As String = Session("rs").Fields("USUARIO").Value.ToString
            Dim item As New ListItem(("FOLIO: " + clave + " USUARIO VALIDADOR: " + Usuario), folio)
            cmb_folio.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub LlenaUsuarios()


        cmb_usuario.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_usuario.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_USUARIOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_usuario.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub btn_modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_modificar.Click

        lbl_status.Text = ""
        lbl_AlertaDesExpedienteDig.Text = ""
        Dim Respuesta As String = ""

        'Verifico que haya usuario seleccionado
        If cmb_folio.SelectedItem.Value = "0" Then
            lbl_status.Text = "Error: Seleccione un expediente"
            Exit Sub
        End If

        If cmb_usuario.SelectedItem.Value = "0" Then
            lbl_status.Text = "Error: Seleccione un usuario"
            Exit Sub
        End If


        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, cmb_folio.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 15, cmb_usuario.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_FOLIOS_BLOQUEADOS_VALIDADOR"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Respuesta = Session("rs").Fields("RESPUESTA").Value.ToString
        End If
        Session("Con").Close()


        If Respuesta = "ERROR" Then
            lbl_status.Text = "Error: Seleccione otro usuario distinto al asignado"
        Else
            lbl_status.Text = "Expediente actualizado exitosamente"
            LlenaFoliosBloqueadosValidador()
            LlenaUsuarios()
        End If


    End Sub

End Class