Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CORE_CNF_USUARIOS_CREAR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Edición de Usuarios", "Edición Usuarios")

        If Not Me.IsPostBack Then
            If Session("ID") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                llena_sucursales("cmb_suc")
                LlenaUnNeg()
                LlenaPoliticas("cmb_pol")
                LlenaPuestos("cmb_puesto")
                CargaInstitucion()
                llenaTipousuario()

                Session("SELCOM") = "SELECT MSTUSUARIO_ID AS ID, MSTUSUARIO_NOMBRES AS NOMBRES,  MSTUSUARIO_PATERNO AS PATERNO, MSTUSUARIO_MATERNO AS MATERNO, MSTUSUARIO_USUARIO AS USUARIO, MSTUSUARIO_ESTATUS AS ESTATUS, MSTUSUARIO_ID_UNNEG AS ID_UNNEG,  MSTUSUARIO_ID_PUESTO AS ID_PUESTO,MSTUSUARIO_ID_POLITICA AS ID_POLITICA, MSTUSUARIO_ID_SUCURSAL AS ID_SUCURSAL, ISNULL(MSTUSUARIO_EMAIL,'') AS EMAIL, ISNULL(MSTUSUARIO_LADA,'') AS LADA, ISNULL(MSTUSUARIO_TELEFONO,'') AS TELEFONO, ISNULL(MSTUSUARIO_LADA_PERSONAL,'') AS LADA_PERSONAL,  ISNULL(MSTUSUARIO_TELEFONO_PERSONAL,'') AS TELEFONO_PERSONAL, ISNULL(MSTUSUARIO_EXT,'') AS EXT,	ISNULL(MSTUSUARIO_ID_TIPO_USUARIO,0) AS ID_TIPO_USUARIO, ISNULL(MSTUSUARIO_CREADOX,0) AS CREADOX,ISNULL(MSTUSUARIO_FECHA_CREADO,'') AS FECHA_CREADO,ISNULL(MSTUSUARIO_MODIFICADOX,0) AS MODIFICADOX,ISNULL(MSTUSUARIO_FECHA_MODIFICADO,'') AS FECHA_MODIFICADO FROM MSTUSUARIO WHERE MSTUSUARIO_ID <> 0  ORDER BY MSTUSUARIO_ID"
                If Session("ID") = -1 Then
                    txt_id.Text = "Nuevo Usuario"
                ElseIf Session("ID") > 0 Then
                    txt_id.Text = Session("ID")
                    llena_usuario()
                    llena_roles()
                    llena_modulos()

                End If
            End If
        End If
    End Sub

    Private Sub limpia()
        txt_email.Text = ""
        txt_lada.Text = ""
        txt_materno.Text = ""
        txt_nombre.Text = ""
        txt_paterno.Text = ""
        txt_tel.Text = ""
        txt_usuario.Text = ""
        cmb_area.SelectedIndex = -1
        cmb_politica.SelectedIndex = -1
        cmb_sucursal.SelectedIndex = -1
        cmb_tipo_usuario.SelectedIndex = -1
    End Sub

    Protected Sub btn_guardar1_Click(sender As Object, e As EventArgs)
        panel_roles.Visible = True
        panel_modulos.Visible = True

        If txt_email.Text <> "" Then
            If ValidaCorreo(txt_email.Text) = False Then
                lbl_guardado.Text = "Error: El correo tiene formato invalido."
                Exit Sub
            End If
        End If


        guardar_usuario()
        llena_roles()
        llena_modulos()
    End Sub

    Private Sub guardar_usuario()

        Dim estatus As Integer

        If chk_estatus.Checked = False Then
            estatus = 0
        Else
            estatus = 1
        End If

        If Session("ID") = -1 Then
            'lbl_guardado.Text = "SE GENERA CONTRASEÑA"
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRES", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 200, estatus)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 200, txt_usuario.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("UNNEG", Session("adVarChar"), Session("adParamInput"), 10, cmb_area.SelectedIndex)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 200, txt_email.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 10, cmb_puesto.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDINST", Session("adVarChar"), Session("adParamInput"), 10, cmb_institucion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("POLITICA", Session("adVarChar"), Session("adParamInput"), 10, cmb_politica.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 10, cmb_sucursal.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 200, txt_lada.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TEL", Session("adVarChar"), Session("adParamInput"), 200, txt_tel.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EXT", Session("adVarChar"), Session("adParamInput"), 200, txt_ext.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 200, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOUSER", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo_usuario.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_USUARIO"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                Session("ID") = Session("rs").fields("ID").value.ToString
                ViewState("RES") = Session("rs").fields("RES").value.ToString
            End If

            Session("Con").Close()

            If ViewState("RES") = 1 Or ViewState("RES") = 2 Then

                'llena_usuario()

                Dim cadena As String
                Dim RndNum As New Random()

                cadena = Session("MascoreG").randkeyMinMay(7) + RndNum.Next(0, 9).ToString
                actualizacontraseña(cadena)
                txt_id.Text = Session("ID").ToString

            ElseIf ViewState("RES") = 3 Then
                lbl_guardado.Text = "Error: Ya existe el usuario"
            ElseIf ViewState("RES") = 0 Then
                lbl_guardado.Text = "Alerta: No puede activar el usuario. Aún no ha agregado roles y/o permisos"
            End If
        ElseIf Session("ID") > 0 Then
            ' lbl_guardado.Text = "SOLO SE ACTUALIZA INFOA"
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, Session("ID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRES", Session("adVarChar"), Session("adParamInput"), 100, txt_nombre.Text.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_paterno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 200, txt_materno.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 200, estatus)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USUARIO", Session("adVarChar"), Session("adParamInput"), 200, txt_usuario.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("UNNEG", Session("adVarChar"), Session("adParamInput"), 10, cmb_area.SelectedIndex)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 200, txt_email.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PUESTO", Session("adVarChar"), Session("adParamInput"), 10, cmb_puesto.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDINST", Session("adVarChar"), Session("adParamInput"), 10, cmb_institucion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("POLITICA", Session("adVarChar"), Session("adParamInput"), 10, cmb_politica.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SUCURSAL", Session("adVarChar"), Session("adParamInput"), 10, cmb_sucursal.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("LADA", Session("adVarChar"), Session("adParamInput"), 200, txt_lada.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TEL", Session("adVarChar"), Session("adParamInput"), 200, txt_tel.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("EXT", Session("adVarChar"), Session("adParamInput"), 200, txt_ext.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 200, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOUSER", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo_usuario.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_USUARIO"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").eof Then
                Session("ID") = Session("rs").fields("ID").value.ToString
                ViewState("RES") = Session("rs").fields("RES").value.ToString
            End If

            Session("Con").Close()
            txt_id.Text = Session("ID").ToString
            If ViewState("RES") = 1 Or ViewState("RES") = 2 Then

                lbl_guardado.Text = "Guardado correctamente"

            ElseIf ViewState("RES") = 3 Then
                lbl_guardado.Text = "Error: Ya existe el usuario"
            ElseIf ViewState("RES") = 0 Then
                lbl_guardado.Text = "Alerta: No puede activar el usuario. Aún no ha agregado roles y/o permisos"
            End If

        End If

        llena_usuario()

    End Sub


    Private Sub actualizacontraseña(ByVal cadena As String)

        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim RESP As String = String.Empty
        Dim PREGUNTA As String = String.Empty
        Dim USER As String = String.Empty
        Dim apMat As String = ""

        If txt_materno.Text = "" Then
            apMat = ""
        Else
            apMat = txt_materno.Text
        End If

        Dim nombre As String = txt_nombre.Text + " " + txt_paterno.Text + " " + apMat

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ANTERIORPWD", Session("adVarChar"), Session("adParamInput"), 15, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PWD", Session("adVarChar"), Session("adParamInput"), 15, cadena)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("ID"))
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
            lbl_guardado.Text = ""
            Dim sbhtml As StringBuilder = New StringBuilder
            subject = "Contraseña del Sistema"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE SECCIÓN 5</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) usuario:  " + nombre + "</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br/>")
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
                lbl_guardado.Text = "Error: Contraseña inválida, genere de nuevo una contraseña"
            Else
                lbl_guardado.Text = "Usuario guardado correctamente e informado de su contraseña generada"
            End If

        Else


        End If

        Session("Con").Close()

    End Sub


    ' Llena CMB con cátalogo de tipo de usuario '
    Private Sub llenaTipousuario()
        cmb_tipo_usuario.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_tipo_usuario.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_TIPOUSU"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("TIPO").Value.ToString)
            cmb_tipo_usuario.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub




    Private Sub LlenaUnNeg()
        cmb_area.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_area.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_UNNEG"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATUNNEG_NOMBRE").Value.ToString, Session("rs").Fields("CATUNNEG_ID").Value.ToString)
            cmb_area.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaPoliticas(ByVal i As String)
        cmb_politica.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_politica.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_POLITICAS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_politica.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llena_sucursales(ByVal i As String)
        cmb_sucursal.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_sucursal.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_sucursal.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub LlenaPuestos(ByVal i As String)
        cmb_puesto.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_puesto.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PUESTOS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_puesto.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub


    Public Function TraduceUnneg(ByVal UnNeg As String) As String
        Return Session("MascoreG").UnnegGenerales(UnNeg)
    End Function

    Public Function TraducePolitica(ByVal Polit As String) As String
        Return Session("MascoreG").PoliticaGenerales(Polit)
    End Function

    Public Function TraduceSucursal(ByVal Sucur As String) As String
        Return Session("MascoreG").SucursalGenerales(Sucur)
    End Function

    Public Function TraducePuesto(ByVal Puesto As String) As String
        Return Session("MascoreG").PuestoGenerales(Puesto)
    End Function

    Private Sub llena_usuario()
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 50, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_USUARIO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_id.Text = Session("rs").Fields("ID").value.ToString
            txt_nombre.Text = Session("rs").Fields("NOMBRE").value.ToString
            txt_paterno.Text = Session("rs").Fields("PATERNO").value.ToString
            txt_materno.Text = Session("rs").Fields("MATERNO").value.ToString
            txt_usuario.Text = Session("rs").Fields("USUARIO").value.ToString
            txt_lada.Text = Session("rs").Fields("LADA").value.ToString
            txt_tel.Text = Session("rs").Fields("TEL").value.ToString
            txt_ext.Text = Session("rs").Fields("EXT").value.ToString
            txt_email.Text = Session("rs").Fields("EMAIL").value.ToString
            If Session("rs").Fields("SUCURSAL").value.ToString = "1" Then
                cmb_tipo_usuario.SelectedValue = Session("rs").Fields("TIPO").value.ToString
                cmb_politica.SelectedValue = Session("rs").Fields("POLITICA").value.ToString
                cmb_sucursal.SelectedValue = Session("rs").Fields("SUCURSAL").value.ToString
                cmb_area.SelectedValue = Session("rs").Fields("UNNEG").value.ToString
                cmb_puesto.SelectedValue = Session("rs").Fields("PUESTO").value.ToString
                cmb_institucion.SelectedValue = Session("rs").Fields("INSTI").value.ToString
                If Session("rs").Fields("ESTATUS").value.ToString = "1" Then
                    chk_estatus.Checked = True
                ElseIf Session("rs").Fields("ESTATUS").value.ToString = "0" Then
                    chk_estatus.Checked = False
                End If
            Else
                cmb_institucion.SelectedValue = Session("rs").Fields("INSTI").value.ToString
                cmb_politica.SelectedValue = Session("rs").Fields("POLITICA").value.ToString
                cmb_sucursal.SelectedValue = Session("rs").Fields("SUCURSAL").value.ToString

                cmb_area.SelectedValue = Session("rs").Fields("UNNEG").value.ToString

                cmb_puesto.SelectedValue = Session("rs").Fields("PUESTO").value.ToString

                cmb_tipo_usuario.SelectedValue = Session("rs").Fields("TIPO").value.ToString

                If Session("rs").Fields("ESTATUS").value.ToString = "1" Then
                    chk_estatus.Checked = True
                ElseIf Session("rs").Fields("ESTATUS").value.ToString = "0" Then
                    chk_estatus.Checked = False
                End If
            End If

        End If
        Session("Con").Close()
    End Sub


    Private Sub CargaInstitucion()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim institucion As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INSTITUCIONES_USUARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(institucion, Session("rs"))
        Session("Con").Close()

        If institucion.Rows.Count = 1 Then
            cmb_institucion.DataSource = institucion
            For i As Integer = 0 To institucion.Rows.Count() - 1
                Dim item As New ListItem(institucion.Rows(i).Item(0).ToString + ".-" + institucion.Rows(i).Item(2), institucion.Rows(i).Item(0))
                cmb_institucion.Items.Add(item)

            Next
        Else
            cmb_institucion.Items.Clear()
            Dim elija As New ListItem("ELIJA", "-1")
            cmb_institucion.Items.Add(elija)

            For i As Integer = 0 To institucion.Rows.Count() - 1
                Dim item As New ListItem(institucion.Rows(i).Item(0).ToString + ".-" + institucion.Rows(i).Item(2), institucion.Rows(i).Item(0))
                cmb_institucion.Items.Add(item)
            Next
        End If

    End Sub

    Protected Sub llena_roles()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtRolesAsignados As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 50, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ROLES_USUARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtRolesAsignados, Session("rs"))
        Session("Con").Close()
        If dtRolesAsignados.Rows.Count > 0 Then
            dag_rol_usu.Visible = True
            dag_rol_usu.DataSource = dtRolesAsignados
            dag_rol_usu.DataBind()
        Else
            dag_rol_usu.Visible = False
        End If
    End Sub

    Private Sub llena_modulos()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ModulosGeneral As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 50, Session("ID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MODULOS_USUARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ModulosGeneral, Session("rs"))
        Session("Con").Close()

        If ModulosGeneral.Rows.Count > 0 Then
            dag_mod_si.Visible = True
            dag_mod_si.DataSource = ModulosGeneral
            dag_mod_si.DataBind()
        Else
            dag_mod_si.Visible = False
        End If

    End Sub

    Protected Sub btn_guardar_r_Click(sender As Object, e As EventArgs)
        'Data table que se llena con el contenido del datagrid
        Dim dtRoles As New Data.DataTable()
        dtRoles.Columns.Add("ID", GetType(Integer))
        dtRoles.Columns.Add("NOMBRE", GetType(String))
        dtRoles.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_rol_usu.Rows.Count() - 1
            dtRoles.Rows.Add(CInt(dag_rol_usu.Rows(i).Cells(0).Text), dag_rol_usu.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_rol_usu.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los roles a un usuario
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_ROL_USU_ASIGNAR", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("ID", SqlDbType.Int)
                Session("parm").Value = Session("ID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("ROLES", SqlDbType.Structured)
                Session("parm").Value = dtRoles
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()
                label1.Text = "Guardado correctamente"
            End Using
        Catch ex As Exception
            'lbl_RolNumero.Text = "Error"

        Finally

            llena_roles()
            llena_modulos()
            UpdatePanelDatos.Update()
            llena_usuario()

        End Try
    End Sub

    Protected Sub btn_guardar_m_Click(sender As Object, e As EventArgs)

        'Data table que se llena con los contenidos de los datagrids
        Dim dtModulos As New Data.DataTable()
        dtModulos.Columns.Add("IDG", GetType(Integer))
        dtModulos.Columns.Add("DESCRIPCION", GetType(String))
        dtModulos.Columns.Add("ASIGNADO", GetType(Integer))

        For i As Integer = 0 To dag_mod_si.Rows.Count() - 1
            dtModulos.Rows.Add(CInt(dag_mod_si.Rows(i).Cells(0).Text), dag_mod_si.Rows(i).Cells(1).Text, Convert.ToInt32(DirectCast(dag_mod_si.Rows(i).FindControl("chk_asignado"), CheckBox).Checked))
        Next

        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                'Stored procedure quie asigan los modulos a un rol.
                connection.Open()
                ' Configure the SqlCommand and SqlParameter.
                Dim insertCommand As New SqlCommand("INS_MOD_USU_ASIGNAR", connection)
                insertCommand.CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = New SqlParameter("IDUSUARIO", SqlDbType.Int)
                Session("parm").Value = Session("ID")
                insertCommand.Parameters.Add(Session("parm"))

                'Parametro que representa una tabla en SQL
                Session("parm") = New SqlParameter("MODULOS", SqlDbType.Structured)
                Session("parm").Value = dtModulos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("IDUSER", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                insertCommand.ExecuteNonQuery()
                connection.Close()
                label2.Text = "Guardado correctamente"
            End Using
        Catch ex As Exception
            '<span class="text_input_nice_label" style="margin-left:20px">Rol Número</span>
            lbl_RolNumero.Text = "Error"

        Finally

            llena_roles()
            llena_modulos()
            UpdatePanelDatos.Update()
            llena_usuario()

        End Try

    End Sub

    Private Function ValidaCorreo(ByVal correo As String) As Boolean
        Return Regex.IsMatch(correo, ("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$"))
    End Function


#Region "Sub para abrir y cerrar folders"
    'folder close or open
    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "none")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#113964 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #C0CDD5")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion
    End Sub
#End Region

End Class