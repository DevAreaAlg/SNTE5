Public Class CRED_EXP_CODEUDORES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Codeudores", "Asignación de Codeudores")

        If Not Me.IsPostBack Then

            'LLENO COMBOS CORRESPONDIENTES
            Llenacodeudor()
            Llenatiporelacion()
            Session("idperbusca") = Nothing

            lbl_Folio.Text = "Datos de Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")
            IniciaContador()
            expbloqueado()
        End If

        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")
        If Session("idperbusca") <> Nothing Then
            txt_IdCliente.Text = Session("idperbusca").ToString
            Session("idperbusca") = Nothing
        End If

        Dim auxiliar As Integer
        'Regresa el nombre de la persona que fue creada en Alta de Persona Fisica
        If Session("VENGODE") = ("CRED_EXP_CODEUDORES.aspx") Then

            auxiliar = Session("PERSONAID")
            Session("PERSONAID") = Session("CODEUDORAUX")
            Session("CODEUDORAUX") = auxiliar
            Datos()
            Session("VENGODE") = Nothing
        End If

    End Sub

    '----------------------------VERIFICA SI EL EXPEDIENTE ESTÁ BLOQUEADO----------------------------------------
    Private Sub expbloqueado()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("BLOQUEADO") = Session("rs").fields("BLOQUEADO").value.ToString()

            If Session("BLOQUEADO") = "1" Then 'Si está bloqueado el expediente se deshabilitan los botones
                'btn_guardar.Enabled = False
                btn_agregar.Enabled = False
            End If

        End If
        Session("Con").Close()
    End Sub

    Private Sub Datos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        'Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("CODEUDORAUX").ToString)
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'Envío el nombre del nuevo codeudor
            'lbl_datoscodeudorn.Text = Session("rs").fields("NOMBRE").value.ToString
        End If
        Session("Con").Close()
    End Sub

    'Llena los codeudor que tiene agregadas esa persona.
    Private Sub Llenacodeudor()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtcodeudor As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CODEUDOR"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtcodeudor, Session("rs"))
        DAG_codeudor.DataSource = dtcodeudor
        DAG_codeudor.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub DAG_codeudor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_codeudor.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            lbl_alerta.Text = ""
            lbl_alertaaval.Text = ""
            lbl_alertadependiente.Text = ""
            lbl_alertaconsejo.Text = ""
            Elimina_codeudor(e.Item.Cells(0).Text)
            Llenacodeudor()
        End If

    End Sub

    Protected Sub DAG_codeudor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_codeudor.ItemDataBound
        If Session("BLOQUEADO") = "1" Then
            e.Item.Cells(5).Visible = False ' Se pone invisible la columna eliminar aval
        End If

    End Sub
    'Elimina completamente la referencia de la BD
    Private Sub Elimina_codeudor(ByVal personaid As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        'Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, personaid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_CODEUDOR"
        Session("cmd").Execute()
        Session("Con").Close()
        Session("CONTADOR") = Session("CONTADOR") - 1
        Session("DIFERENCIA") = CInt(Session("MINCODEUDOR")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        lbl_codeudores.Text = "Faltan: " + Session("DIFERENCIA").ToString + " codeudor(es)"

        Llenacodeudor()
        IniciaContador()
    End Sub

    'Procedimiento que obtiene el catálogo de tipo de relación  y las despliega en el combo correspondiente
    Private Sub Llenatiporelacion()
        cmb_relacion.Items.Clear()
        'cmb_tiporel.Items.Clear()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_RELACION"
        Session("rs") = Session("cmd").Execute()

        Dim elija As New ListItem("ELIJA", "")
        cmb_relacion.Items.Add(elija)
        'cmb_tiporel.Items.Add(elija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CATRELACIONES_RELACION").Value.ToString, Session("rs").Fields("CATRELACIONES_ID_RELACION").Value.ToString)
            cmb_relacion.Items.Add(item)
            'cmb_tiporel.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub LimpiaForma2()
        cmb_relacion.SelectedIndex = 0
        txt_IdCliente.Text = ""
        Session("idperbusca") = Nothing

    End Sub
    Private Sub LimpiaForma1()
        'cmb_tiporel.SelectedIndex = 0
        Session("CODEUDORAUX") = Nothing
    End Sub

    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        lbl_status.Text = ""

        If Session("CONTADOR") < Session("MAXCODEUDOR") And Session("MAXCODEUDOR") >= Session("MINCODEUDOR") Then

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDCODEUDOR", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFEXP_CODEUDOR"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then

                Select Case Session("rs").Fields("EXISTE").value.ToString

                    Case "ELMISMO"
                        lbl_status.Text = "Error: No puede agregarse a sí mismo como codeudor"
                    Case "YAEXISTE"
                        lbl_status.Text = "Error: Esta persona ya fue asignada como codeudor"
                    Case "NOEXISTE"

                        Select Case Session("rs").Fields("RESPUESTA").value.ToString
                            Case "NOAGREGADO"
                                lbl_status.Text = "Error: La persona no existe en la base de datos"

                            Case "PERSONAINCOMPLETA"
                                lbl_status.Text = "Error: Persona con datos incompletos"

                            Case "TIENEOTROROL"
                                lbl_status.Text = "Error: Esta persona ya está asignada como aval o referencia"
                            Case Else
                                lbl_status.Text = ""
                        End Select

                    Case Else

                        lbl_status.Text = ""

                End Select


                'INSERTO EL CODEUDOR Y MODIFICO EL CONTADOR (SÓLO SE MODIFICA SI SE INSERTA UN CODEUDOR)
                If Session("rs").Fields("FLAG").value.ToString = "CONTAR" Then
                    Contador()
                End If

                'MENSAJE DE ALERTA QUE YA EXISTE ESE CODEUDOR EN LA BASE DE DATOS
                If Session("rs").Fields("ALERTA").value.ToString = "" Then
                    lbl_alerta.Visible = False
                Else
                    lbl_alerta.Visible = True
                    lbl_alerta.Text = Session("rs").Fields("ALERTA").value.ToString
                End If

                'ALERTA DE AVALES EN OTROS CREDITOS
                If Session("rs").Fields("ALERTAAVAL").value.ToString = "" Then
                    lbl_alertaaval.Visible = False
                Else
                    lbl_alertaaval.Visible = True
                    lbl_alertaaval.Text = Session("rs").Fields("ALERTAAVAL").value.ToString
                End If

                'ALERTA DE DEPENDIENTES EN OTROS CREDITOS
                If Session("rs").Fields("ALERTADEPENDIENTE").value.ToString = "" Then
                    lbl_alertadependiente.Visible = False
                Else
                    lbl_alertadependiente.Visible = True
                    lbl_alertadependiente.Text = Session("rs").Fields("ALERTADEPENDIENTE").value.ToString
                End If

                'ALERTA DE MIEMBRO DE CONSEJO
                If Session("rs").Fields("ALERTACONSEJO").value.ToString = "" Then
                    lbl_alertaconsejo.Visible = False
                Else
                    lbl_alertaconsejo.Visible = True
                    lbl_alertaconsejo.Text = Session("rs").Fields("ALERTACONSEJO").value.ToString
                End If


            End If

            Session("Con").Close()
            LimpiaForma2() 'Se limpian los lbls 
            Llenacodeudor()

        Else
            lbl_status.Text = "Error: Ya cumple con el máximo de codeudores establecidos"
        End If


    End Sub

    Protected Sub lnk_busqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busqueda.Click
        lbl_status.Text = ""
        lbl_alerta.Text = ""
        lbl_alertaaval.Text = ""
        lbl_alertadependiente.Text = ""
        lbl_alertaconsejo.Text = ""
    End Sub
    Private Sub Contador()

        Session("CONTADOR") = Session("CONTADOR") + 1
        Session("DIFERENCIA") = CInt(Session("MINCODEUDOR")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If

        lbl_codeudores.Text = "Faltan: " + Session("DIFERENCIA").ToString + " codeudor(es)"
    End Sub


    'Trae de la BD los valores minimos y máximos de Avales y el número de avales que ya tiene ese folio
    Protected Sub IniciaContador()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MIN_MAX_REQ_ADICIONALES"
        Session("rs") = Session("cmd").Execute()

        Session("MINCODEUDOR") = Session("rs").Fields("MINCOD").value.ToString
        Session("MAXCODEUDOR") = Session("rs").Fields("MAXCOD").value.ToString
        Session("CONTADOR") = Session("rs").Fields("CONTADORCODEUDOR").value.ToString

        Session("DIFERENCIA") = CInt(Session("MINCODEUDOR")) - Math.Abs((CInt(Session("CONTADOR"))))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        Session("Con").Close()

        lbl_codeudores.Text = "Faltan: " + Session("DIFERENCIA").ToString + " codeudor(es)"
    End Sub


End Class