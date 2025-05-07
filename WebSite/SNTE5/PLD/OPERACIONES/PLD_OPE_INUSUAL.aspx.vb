Public Class PLD_OPE_INUSUAL
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Operación Inusual", "Generar Operación Inusual")
        If Not Me.IsPostBack Then

        End If
        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_Continuar.ClientID + "')")
        btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            Session("idperbusca") = Nothing
        End If
    End Sub

    Protected Sub btn_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click

        lbl_statusc.Text = ""

        BuscarIDCliente()

    End Sub


    Private Sub BuscarIDCliente()

        Dim Existe As Integer
        Existe = 0

        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()


        If Not Session("rs").eof Then
            Existe = Session("rs").fields("EXISTE").value.ToString
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If

        Session("Con").Close()

        If Existe = 0 Then

            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Cliente con datos incompletos"
            Session("PERSONAID") = txt_IdCliente.Text
            ExpedientesReportar()

        ElseIf Existe = -1 Then

            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de cliente"
            Session("PERSONAID") = txt_IdCliente.Text
            ExpedientesReportar()

        Else

            txt_IdCliente.Enabled = False
            btn_BusquedaPersona.Enabled = False
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_nompros.Text = "CLIENTE: " + Session("CLIENTE").ToString
            ExpedientesReportar()

        End If

    End Sub

    Private Sub ExpedientesReportar()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLD_EXPEDIENTES_ACTIVOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtexpedientes, Session("rs"))
        Session("Con").Close()

        If dtexpedientes.Rows.Count > 0 Then
            dag_EXPEDIENTES.Visible = True
            dag_EXPEDIENTES.DataSource = dtexpedientes
            dag_EXPEDIENTES.DataBind()
        Else
            dag_EXPEDIENTES.Visible = False
        End If

    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        lbl_statusc.Text = ""
        lbl_nompros.Text = ""
        txt_IdCliente.Enabled = True
        btn_BusquedaPersona.Enabled = True
        dag_EXPEDIENTES.Visible = False
    End Sub

    Protected Sub dag_EXPEDIENTES_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_EXPEDIENTES.ItemCommand

        Dim contNotas As Integer
        contNotas = CInt(txt_Notas.Text.Length)

        If (e.CommandName = "PROVPROP") Then
            Session("FOLIO") = e.Item.Cells(0).Text
            ClientScript.RegisterStartupScript(GetType(String), "Proveedores de Recurso", "window.open(""PLD_OPE_PROVEEDORRECURSO.aspx"", ""RP"", ""width=750px,height=450,resizable=NO,Location=NO,Scrollbars=NO,Status=YES,top=300,left=350"");", True)
        End If
        If (e.CommandName = "REFERENCIAS") Then

        End If
        If (e.CommandName = "REPORTAR") Then

            If txt_Notas.Text = "" Then
                lbl_statusc.Text = "Error: Es necesario capturar notas antes de generar la operación inusual."
            Else

                If contNotas <= 435 Then
                    lbl_statusc.Text = ""
                    GenerarOperacionInusual(e.Item.Cells(0).Text)

                Else
                    lbl_statusc.Text = "Error: Excede el número de caracteres permitidos en notas"
                End If


            End If


        End If


    End Sub

    Private Sub GenerarOperacionInusual(ByVal FOLIO As Integer)

        Dim IDAlerta As Integer
        Dim CLIENTE As String
        Dim clase_correo As New Correo
        Dim cc As String = String.Empty
        Dim sbhtml As New StringBuilder
        Dim subject As String = String.Empty
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, FOLIO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 10, cmb_ProvProp.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 500, txt_Notas.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_OPERACION_ALERTA_MANUAL"
        Session("rs") = Session("cmd").Execute()

        IDAlerta = Session("rs").Fields("IDALERTA").Value.ToString
        CLIENTE = Session("rs").Fields("CLIENTE").Value.ToString

        Session("Con").Close()



        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "ALERTAPLD")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            subject = "Alerta: Operacion inusual manual"
            sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
            sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
            sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            sbhtml.Append("<tr><td>Estimado(a) Oficial: </td></tr>")
            sbhtml.Append("<tr><td> Se le informa que se ha generado una operación inusual manualmente y se encuentra en lista de espera para su revisión.</td></tr>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br />")
            sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
            sbhtml.Append("<br></br>")
            sbhtml.Append("</table>")
            sbhtml.Append("<br></br>")
            If Not (clase_correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)) Then
                lbl_statusc.Text = "Operación generada existosamente. Se ha enviado correo electronico a su oficial de cumplimiento para su dictaminación"
            Else
                lbl_statusc.Text = "Operación generada existosamente. (Error No se a enviado correo electronico)"
            End If
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub


End Class