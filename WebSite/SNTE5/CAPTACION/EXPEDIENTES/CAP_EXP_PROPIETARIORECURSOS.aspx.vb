Public Class CAP_EXP_PROPIETARIORECURSOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Propietario real de recursos", "PROPIETARIO REAL DE RECURSOS")

        If Not Me.IsPostBack Then
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("FOLIO"))

            LlenaPropietario()
            Llenatiporelacion()


        End If


        'txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + lnk_Continuar.ClientID + "')")
        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")
        'Declaro las funciones necesarias que se mandan llamar por medio de Java Scrip


        If Session("idperbusca") <> Nothing Then
            txt_numCliente.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If


    End Sub

    Private Sub LlenaPropietario()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtpropietario As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PROPIETARIO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtpropietario, Session("rs"))
        dag_propietario.DataSource = dtpropietario
        dag_propietario.DataBind()


        Session("Con").Close()

    End Sub

    Private Sub dag_propietario_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_propietario.ItemCommand


        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            Elimina_Propietario(e.Item.Cells(0).Text)
            LlenaPropietario()
        End If

    End Sub

    Protected Sub dag_propietario_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_propietario.ItemDataBound
        e.Item.Cells(0).Visible = False
    End Sub

    'Elimina completamente el propietario
    Private Sub Elimina_Propietario(ByVal idpersona As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, idpersona)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PROPIETARIO"
        Session("cmd").Execute()


        Session("Con").Close()
        Session("idperbusca") = Nothing

    End Sub

    'Procedimiento que obtiene el catálogo de tipo de relación  y las despliega en el combo correspondiente
    Private Sub Llenatiporelacion()

        cmb_tipo_relacion.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "")

        cmb_tipo_relacion.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_tipo_relacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        lbl_status.Text = ""  'Limpio el lbl status

        If txt_numCliente.Text = Session("PERSONAID") Then
            lbl_status.Text = "Error: No puede asignarse a sí mismo"
            LimpiaForma()
        Else
            Dim suma As Integer

            If txt_pctje_exi.Text <> "" Then
                suma = CInt(txt_pctje_exi.Text)
            Else
                suma = 0
            End If

            For Each DataGriditem In dag_propietario.Items

                suma = (suma + CInt(DataGriditem.cells(3).Text))

            Next


            If ((suma) <= 100) Then

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo_relacion.SelectedItem.Value)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPROPIETARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_numCliente.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PORCENTAJE", Session("adVarChar"), Session("adParamInput"), 15, txt_pctje_exi.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_CNFEXP_PROPIETARIO"
                Session("rs") = Session("cmd").Execute()

                If Not Session("rs").eof Then

                    Select Case Session("rs").Fields("RESPUESTA").value.ToString

                        Case "NOEXISTEPERSONA"

                            lbl_status.Text = "Error: No existe el número de cliente " + txt_numCliente.Text

                        Case "PERONAINCOMPLETA"

                            lbl_status.Text = "Error: Persona con datos incompletos"

                        Case "PERSONAAGREGADA"

                            lbl_status.Text = "Error: Esta persona ya fue asignada como propietario real"

                        Case "PERSONACOMPLETA"

                            lbl_status.Text = "Guardado correctamente"

                        Case Else


                    End Select

                End If

                Session("Con").Close()
                LimpiaForma()
                LlenaPropietario()
            Else
                If suma > 100 Then
                    lbl_status.Text = "Error: Excede del 100%"
                    LimpiaForma()
                End If

            End If
        End If



    End Sub

    Private Sub LimpiaForma()
        cmb_tipo_relacion.SelectedIndex = 0
        Session("idperbusca") = Nothing
        txt_numCliente.Text = ""
        txt_pctje_exi.Text = ""

    End Sub

End Class