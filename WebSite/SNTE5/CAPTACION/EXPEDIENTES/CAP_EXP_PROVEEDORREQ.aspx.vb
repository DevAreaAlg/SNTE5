Public Class CAP_EXP_PROVEEDORREQ
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TryCast(Me.Master, MasterMascore).CargaASPX("Proveedores", "Asignación de Proveedores de Recursos")

        If Not Me.IsPostBack Then

            'LLENO COMBOS CORRESPONDIENTES
            LlenaProveedor()
            Llenatiporelacion()

            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")

        End If

        'Regresa el nombre de la persona que fue creada en Alta de Persona Fisica
        Dim auxiliar As Integer

        If Session("VENGODE") = ("CNFEXP_PROVEEDORREC.aspx") Then

            auxiliar = Session("PERSONAID")
            Session("PERSONAID") = Session("PROVEEDORAUX")
            Session("PROVEEDORAUX") = auxiliar
            Session("VENGODE") = Nothing

        End If

        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If



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

    'Llena las referencias que tiene agregadas esa persona.
    Private Sub LlenaProveedor()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtproveedor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PROVEEDORREC"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtproveedor, Session("rs"))
        dag_Proveedor.DataSource = dtproveedor
        dag_Proveedor.DataBind()


        Session("Con").Close()

    End Sub

    Private Sub dag_Proveedor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Proveedor.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            Elimina_Proveedor(e.Item.Cells(0).Text)
            LlenaProveedor()
        End If

    End Sub

    Protected Sub dag_Proveedor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dag_Proveedor.ItemDataBound
        e.Item.Cells(0).Visible = False
    End Sub

    'Elimina completamente el propietario
    Private Sub Elimina_Proveedor(ByVal idpersona As String)

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
        Session("cmd").CommandText = "DEL_CNFEXP_PROVEEDORREC"
        Session("cmd").Execute()

        Session("Con").Close()
        Session("idperbusca") = Nothing

    End Sub

#Region "Busqueda"

    Protected Sub lnk_busqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busqueda.Click
        lbl_status.Text = ""
        lbl_alerta.Text = ""
        lbl_alertacodeudor.Text = ""
        lbl_alertadependiente.Text = ""
        lbl_alertaconsejo.Text = ""
    End Sub

    Private Sub LimpiaForma1()
        cmb_tipo_relacion.SelectedIndex = 0
        Session("idperbusca") = Nothing
        txt_IdCliente.Text = ""
    End Sub

    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        lbl_status.Text = ""  'Limpio el lbl status

        If txt_IdCliente.Text = Session("PERSONAID") Then
            lbl_status.Text = "Error: No puede asignarse a sí mismo"
            LimpiaForma1()
        Else
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipo_relacion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPROVEEDOR", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFEXP_PROVEEDORREC"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then

                Select Case Session("rs").Fields("RESPUESTA").value.ToString

                    Case "NOEXISTEPERSONA"

                        lbl_status.Text = "Error: No existe el número de cliente " + txt_IdCliente.Text

                    Case "PERSONAINCOMPLETA"

                        lbl_status.Text = "Error: Persona con datos incompletos"

                    Case "YAEXISTEPERSONA"

                        lbl_status.Text = "Error: Esta persona ya fue asignada como propietario real"

                    Case "PERSONACOMPLETA"

                        lbl_status.Text = "Guardado correctamente"

                    Case Else


                End Select

            End If

            Session("Con").Close()
            LimpiaForma1()
            LlenaProveedor()
        End If



    End Sub
#End Region

End Class