Imports System.Data.SqlClient
Imports System.IO

Public Class CRED_EXP_REGE_LAYOUT
    Inherits Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Regeneración de Layout de Banco", "Layout de Banco Individual")

        If Not IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            CargaSaldoActual()
            txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + btn_BusquedaPersona.ClientID + "')")

            btn_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")
        End If

        If Session("idperbusca") <> Nothing Then

            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            Session("INSTITUCION") = Nothing
        End If
    End Sub

    Private Sub MuestraExpedientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_EXPEDIENTES_INDI"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtexpedientes, Session("rs"))
        dag_Expendientes.DataSource = dtexpedientes
        dag_Expendientes.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub DAG_Expedientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Expendientes.ItemCommand

        If (e.CommandName = "CONSULTAR") Then
            Session("FOLIO") = e.Item.Cells(0).Text
            Session("CVEEXP") = e.Item.Cells(1).Text
            Session("PRODUCTO") = e.Item.Cells(2).Text
            Session("PROSPECTO") = Session("CLIENTE")

            If e.Item.Cells(4).Text = "AUTORIZADO" Then

                upnl_bancarios.Visible = True
                panel_datos.Visible = False
                bancos()

                CargaBancos()
                lbl_estatus_bank.Text = ""
                txt_clabe_conf.Text = ""

            ElseIf e.Item.Cells(4).Text = "PAGADO" Then
                bancos()

                LlenarExpXPagar()
                upnl_bancarios.Visible = True
                panel_datos.Visible = True
                CargaBancos()
                lbl_estatus_bank.Text = ""
                txt_clabe_conf.Text = ""


            End If

            ''  folderA(div_selCliente, "up")
            ''folderA(pnl_expedientes, "up")


        End If
    End Sub

    Private Sub BuscarIDCliente()
        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 50, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString

        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If
        Session("Con").Close()

        If Existe = -1 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de control"
            lbl_NombrePersonaBusqueda.Text = ""
            div_NombrePersonaBusqueda.Visible = False
            upnl_bancarios.Visible = False
            panel_datos.Visible = False
            pnl_expedientes.Visible = False

            Session("PERSONAID") = txt_IdCliente.Text
        Else
            lbl_statusc.Text = ""
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("PERSONAID") = txt_IdCliente.Text
        End If

    End Sub


    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        Session("Con").Close()

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""
        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text
            pnl_expedientes.Visible = True
            upnl_bancarios.Visible = False
            panel_datos.Visible = False
        End If

    End Sub


    Private Sub validaTrabajador()
        lbl_statusc.Text = ""
        lbl_status.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: RFC incorrecto"
                lbl_NombrePersonaBusqueda.Text = ""
                div_NombrePersonaBusqueda.Visible = False

                pnl_expedientes.Visible = False


            Else
                Session("CLIENTE") = Session("ID")
                Session("PROSPECTO") = Nothing
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()
        End If
    End Sub

    Protected Sub btn_busqueda_persona(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BusquedaPersona.Click
        txt_clabe.Text = ""
        cmb_banco.ClearSelection()
        'txt_medio_paga.Text = ""
        folderA(panel_bancarios, "up")
    End Sub

    Protected Sub lnk_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Continuar.Click
        limpia_datos()
        validaTrabajador()
        MuestraExpedientes()

    End Sub


    Private Sub limpia_datos()
        folderA(panel_bancarios, "up")
        'txt_medio_paga.Text = ""
        cmb_banco.ClearSelection()
        txt_clabe.Text = ""
        lbl_status.Text = ""
        lbl_statusc.Text = ""
        lbl_estatus_bank.Text = ""

    End Sub
    Private Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            If toogle.Attributes("class").IndexOf("down") >= 0 Then
                content.Attributes.CssStyle.Add("display", "block")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "#fff")
                head.Attributes.CssStyle.Add("border", "solid 1px transparent")
                head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptS" & pnl.ClientID, "$('#" & content.ClientID & "').show('6666',null);", True)
            End If

        ElseIf accion.Equals("up") Then
            If toogle.Attributes("class").IndexOf("up") >= 0 Then
                content.Attributes.CssStyle.Add("display", "none")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "inherit")
                head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
                head.Attributes.CssStyle.Add("border-radius", "4px")
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptH" & pnl.ClientID, "$('#" & content.ClientID & "').hide('6666',null);", True)
            End If
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion


    End Sub
    Private Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String, ByVal efecto As Boolean)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            If toogle.Attributes("class").IndexOf("down") >= 0 Then
                content.Attributes.CssStyle.Add("display", "block")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "#fff")
                head.Attributes.CssStyle.Add("border", "solid 1px transparent")
                head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
                If efecto Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptS" & pnl.ClientID, "$('#" & content.ClientID & "').show('6666',null);", True)
                Else
                    content.Attributes.CssStyle.Add("display", "block")
                End If
            End If

        ElseIf accion.Equals("up") Then
            If toogle.Attributes("class").IndexOf("up") >= 0 Then
                content.Attributes.CssStyle.Add("display", "none")
            Else
                head.Attributes.CssStyle.Add("background", "#696462 !important")
                head.Attributes.CssStyle.Add("color", "inherit")
                head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
                head.Attributes.CssStyle.Add("border-radius", "4px")
                If efecto Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptH" & pnl.ClientID, "$('#" & content.ClientID & "').hide('6666',null);", True)
                Else
                    content.Attributes.CssStyle.Add("display", "none")
                End If
            End If
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion
    End Sub

    Protected Sub btn_guarda_bank_Click(sender As Object, e As EventArgs) Handles btn_guarda_bank.Click

        Try
            Dim Validacion As Boolean = ValidacionBancosVb()
            If Validacion = True Then
                lbl_estatus_bank.Text = "Validaciones con éxito."
                GuardaBancos()
                CargaBancos()

                If panel_datos.Visible = True Then

                    folderA(panel_bancarios, "up")
                    folderA(panel_bancarios, "down")
                    LlenarExpXPagar()

                    txt_clabe_conf.Text = ""
                    txt_secuencia.Text = ""

                Else

                    folderA(panel_bancarios, "down")
                    txt_clabe_conf.Text = ""


                End If
            Else

                If panel_datos.Visible = True Then

                    folderA(panel_bancarios, "up")
                    folderA(panel_bancarios, "down")

                Else

                    folderA(panel_bancarios, "down")


                End If
            End If

        Catch ex As Exception
            lbl_estatus_bank.Text = ex.ToString
        End Try

    End Sub


    Private Function ValidacionBancosVb() As Boolean

        If txt_clabe.Text <> txt_clabe_conf.Text Then
            lbl_estatus_bank.Text = "Error: La CLABE no coincide con el campo de confirmación."
            Return False
        End If

        If cmb_banco.SelectedItem.Text = "SCOTIABANK" Then
            If Len(txt_clabe.Text) <> 11 Then
                If Len(txt_clabe.Text) <> 18 Then
                    lbl_estatus_bank.Text = "Error: El número de cuenta debe ser de 11 o 18 posiciones."
                    Return False
                End If
            End If
        ElseIf cmb_banco.SelectedItem.Text <> "SCOTIABANK" Then
            If Len(txt_clabe.Text) <> 18 Then
                lbl_estatus_bank.Text = "Error: Ingrese correctamente una CLABE de 18 posiciones."
                Return False
            End If
        End If

        If Len(txt_clabe.Text) = 18 Then
            Dim Validacion As String = ValidaCLABE()
            If Validacion <> "OK" Then
                If Validacion = "FALSE" Then
                    lbl_estatus_bank.Text = "Error: La CLABE no coincide con el Banco seleccionado."
                    Return False
                Else

                    lbl_estatus_bank.Text = "Error: CLABE ya registrada para el agremiado: " + Validacion.ToString
                    Return False
                End If
            End If
        End If

        Return True

    End Function


    Private Sub GuardaBancos()

        Dim tipo As Integer
        If cmb_banco.SelectedItem.Text = "SCOTIABANK" Then
            tipo = 1
        ElseIf cmb_banco.SelectedItem.Text <> "SCOTIABANK" Then
            tipo = 2
        End If

        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDINSTFINAN", Session("adVarChar"), Session("adParamInput"), 20, CInt(cmb_banco.SelectedItem.Value))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 20, txt_clabe.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("TIPOVAL", Session("adVarChar"), Session("adParamInput"), 1, tipo)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_INSTFINAN_CLABE_INDI"
            Session("rs") = Session("cmd").Execute()
            Session("Con").close()
        Catch ex As Exception
        Finally
            lbl_estatus_bank.Text = "Información guardada correctamente."
        End Try

    End Sub

    Private Sub CargaBancos()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CLABE_BANCO_INDI"
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            If Session("rs").Fields("TIPO_CUENTA").Value.ToString = 1 Then
                'txt_medio_paga.Text = "*NUMERO DE CUENTA"
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 2 Then
                'txt_medio_paga.Text = "CLABE INTERBANCARIA"
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 3 Then
                'txt_medio_paga.Text = "CHEQUE"
            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 0 Then
                txt_clabe.Text = ""
                cmb_banco.SelectedValue = "-1"
                txt_clabe.Enabled = False
                cmb_banco.Enabled = False
            End If
        End If
        Session("Con").close()

    End Sub


    Private Function ValidaCLABE() As String

        Dim Respuesta As String = "FALSE"
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 300, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_BANCO", Session("adVarChar"), Session("adParamInput"), 10, cmb_banco.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLABE_BANCO", Session("adVarChar"), Session("adParamInput"), 20, txt_clabe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_CLABE_BANCO"
        Session("rs") = Session("cmd").Execute()
        Respuesta = Session("rs").fields("VALIDACION").value.ToString
        Session("Con").close()
        Return Respuesta

    End Function

    Private Sub bancos()
        ' Carga cátalogo de bancos disponibles
        cmb_banco.Items.Clear()
        cmb_banco.Items.Add(New ListItem("ELIJA", "-1"))
        Try
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
            Session("rs") = Session("cmd").Execute()
            Do While Not Session("rs").EOF
                cmb_banco.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
                Session("rs").movenext()
            Loop
        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try
    End Sub

#Region "Carga Inicial"

    Private Sub LlenarExpXPagar()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_EXPEDIENTES_PORPAGAR_INDI"
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INSTI", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, "PA")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtAnalisis, Session("rs"))
        DAG_Analisis.DataSource = dtAnalisis
        DAG_Analisis.DataBind()
        Session("Con").Close()

        If DAG_Analisis.Rows.Count > 0 Then
            btn_layout_bancos.Visible = True
        Else
            btn_layout_bancos.Visible = False
        End If

    End Sub

    Private Sub CargaSaldoActual()

        Dim SaldoActual As Decimal

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SALDO_ACTUAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            SaldoActual = Session("rs").Fields("SALDO_ACTUAL").value
        End If
        Session("Con").Close()

    End Sub

#End Region

#Region "Genera Layout Bancos"

    Protected Sub btn_layout_bancos_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_layout_bancos.Click

        lbl_status.Text = ""
        If txt_secuencia.Text <> "" Then
            DescargaTXTLayout()
        Else
            lbl_status.Text = "Error: Capture número de secuencia."
        End If

    End Sub

    Private Sub DescargaTXTLayout()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtRegistros As New DataTable()
        Dim NombreTXT As String = "pagos_"

        Dim dtBancos As New DataTable()
        dtBancos.Columns.Add("FOLIO", GetType(Integer))
        dtBancos.Columns.Add("ID_PERSONA", GetType(Integer))
        dtBancos.Columns.Add("CLAVE_EXPEDIENTE", GetType(String))
        dtBancos.Columns.Add("MONTO", GetType(Decimal))

        dtBancos.Rows.Add(Convert.ToInt32(DirectCast(DAG_Analisis.Rows(0).FindControl("FOLIO"), Label).Text), Convert.ToInt32(DirectCast(DAG_Analisis.Rows(0).FindControl("NUMCLIENTE"), Label).Text), DAG_Analisis.Rows(0).Cells(0).Text, CDec(DAG_Analisis.Rows(0).Cells(4).Text))


        Try

            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString)

                connection.Open()

                Dim insertCommand As New SqlCommand("SEL_PAGO_A_BANCOS", connection)
                insertCommand.CommandType = CommandType.StoredProcedure

                Session("parm") = New SqlParameter("LAYOUT_PRESTAMOS", SqlDbType.Structured)
                Session("parm").Value = dtBancos
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SECUENCIA", SqlDbType.Int)
                Session("parm").Value = txt_secuencia.Text
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("USERID", SqlDbType.Int)
                Session("parm").Value = Session("USERID")
                insertCommand.Parameters.Add(Session("parm"))

                Session("parm") = New SqlParameter("SESION", SqlDbType.VarChar)
                Session("parm").Value = Session("Sesion")
                insertCommand.Parameters.Add(Session("parm"))

                Dim myReader As SqlDataReader = insertCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dtRegistros.Load(myReader)

                Dim context As HttpContext = HttpContext.Current
                context.Response.Clear()
                context.Response.ContentEncoding = Encoding.Default

                For Each Renglon As DataRow In dtRegistros.Rows
                    context.Response.Write(Renglon.Item(0).ToString)
                    context.Response.Write(Environment.NewLine)
                Next

                context.Response.ContentType = "text/csv"
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreTXT + Session("FechaSis").ToString + ".txt")
                context.Response.End()

                myReader.Close()

            End Using

        Catch ex As Exception
            lbl_status.Text = ex.Message()
        Finally
            lbl_status.Text = "Se ha generado el layout de bancos."
        End Try

    End Sub

#End Region

End Class