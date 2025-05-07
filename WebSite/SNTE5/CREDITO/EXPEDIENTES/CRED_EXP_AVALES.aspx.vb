Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_AVALES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Avales", "ASIGNACIÓN DE AVALES")

        If Not Me.IsPostBack Then
            If Session("FOLIO") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If

            LlenaInstituciones()
            'LLENO COMBOS CORRESPONDIENTES
            Llenaaval()
            Llenatiporelacion()
            Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona
            Session("VENGODE") = ("CRED_EXP_AVALES.aspx")
            Session("PERSONAID") = Session("PERSONAID")
            'Datos Generales de Expediente: Folio, Nombre de Cliente y producto
            lbl_Folio.Text = "Datos del Expediente: " + Session("CVEEXPE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")
            IniciaContador()
            expbloqueado()
        End If

        'Regresa el nombre de la persona que fue creada en Alta de Persona Fisica
        Dim auxiliar As Integer


        auxiliar = Session("PERSONAID2")
        Session("PERSONAID2") = Session("AVALAUX")
        Session("AVALAUX") = auxiliar
        'Datos()


        'Declaro las funciones necesarias que se mandan llamar por medio de Java Script
        lnk_busqueda.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            txt_IdCliente1.Text = Session("idperbusca").ToString
            ddl_Instituciones.SelectedValue = Session("INSTITUCION").ToString
            Session("idperbusca") = Nothing
        End If

    End Sub

    '----------------------------VERIFICA SI EL EXPEDIENTE ESTÁ BLOQUEADO----------------------------------------
    Private Sub expbloqueado()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        'Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("BLOQUEADO") = Session("rs").fields("BLOQUEADO").value.ToString()

            If Session("BLOQUEADO") = "1" Then 'Si está bloqueado el expediente se deshabilitan los botones
                ' btn_guardar.Enabled = False
                btn_agregar.Enabled = False

            End If

        End If
        Session("Con").Close()


    End Sub


    'Llena los avales que tiene agregadas esa persona.
    Private Sub Llenaaval()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtaval As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_AVAL"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtaval, Session("rs"))
        DAG_aval.DataSource = dtaval
        DAG_aval.DataBind()
        Session("Con").Close()

    End Sub

    'Elmina completamente el AVAL de la BD
    Private Sub DAG_aval_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_aval.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            lbl_status.Text = ""
            lbl_alerta.Text = ""
            lbl_alertacodeudor.Text = ""
            lbl_alertadependiente.Text = ""
            lbl_alertaconsejo.Text = ""

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, e.Item.Cells(0).Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            'Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_CNFEXP_AVAL"
            Session("cmd").Execute()
            Session("Con").Close()

            Session("CONTADOR") = Session("CONTADOR") - 1

            Session("DIFERENCIA") = CInt(Session("MINAVAL")) - CInt(Session("CONTADOR"))
            If Session("DIFERENCIA") < 0 Then
                Session("DIFERENCIA") = 0
            End If
            'lbl_maxavales.Text = "Faltan: " + " " + Session("DIFERENCIA").ToString + " aval(es)"

            Llenaaval()
        End If

    End Sub

    Protected Sub DAG_aval_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DAG_aval.ItemDataBound
        If Session("BLOQUEADO") = "1" Then
            e.Item.Cells(5).Visible = False ' Se pone invisible la columna eliminar aval
        End If
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

    'Limpia los lbls al buscar un aval
    Private Sub LimpiaForma2()
        cmb_relacion.SelectedIndex = 0
        Session("idperbusca") = Nothing
        txt_IdCliente1.Text = ""
        txt_IdCliente.Text = ""
    End Sub

    'Limpia los lbls al crear un nuevo aval
    Private Sub LimpiaForma1()
        'cmb_tiporel.SelectedIndex = 0
        Session("AVALAUX") = Nothing
    End Sub

    'Botón que guarda un nuevo aval
    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click

        lbl_status.Text = ""
        obtieneId()
        If Session("CONTADOR") < Session("MAXAVAL") And Session("MAXAVAL") >= Session("MINAVAL") Then


            ' lbl_status.Text = "PERSONAID2: " + Session("PERSONAID").ToString + " RELACION: " + CStr(cmb_relacion.SelectedItem.Value) + " IDVAL: " + txt_IdCliente.Text + " USERID: " + Session("USERID").ToString + " SESION: " + Session("Sesion").ToString + " FOLIO: " + Session("FOLIO").ToString


            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandTimeout = 600
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDRELACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_relacion.SelectedItem.Value)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDAVAL", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "INS_CNFEXP_AVAL"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then

                Select Case Session("rs").Fields("EXISTE").value.ToString

                    Case "ELMISMO"
                        lbl_status.Text = "Error: No puede agregarse a sí mismo como aval"

                    Case "YAEXISTE"
                        lbl_status.Text = "Error: Esta persona ya fue asignada como aval"

                    Case "MORA"
                        lbl_status.Text = "Error: Esta persona tiene préstamos atrasados"

                    Case "NOEXISTE"

                        Select Case Session("rs").Fields("RESPUESTA").value.ToString

                            Case "NOAGREGADO"
                                lbl_status.Text = "Error: La persona no existe en la base de datos"

                            Case "PERSONAINCOMPLETA"
                                lbl_status.Text = "Error: Persona con datos incompletos"

                            Case "NOPAGA"
                                lbl_status.Text = "Error: El aval no cubre la capacidad de pago"

                            Case "TIENEOTROROL"
                                lbl_status.Text = "Error: Esta persona ya está asignada como codeudor o referencia"

                            Case Else
                                lbl_status.Text = ""
                        End Select

                    Case Else

                        lbl_status.Text = ""

                End Select


                'INSERTO EL AVAL Y MODIFICO EL CONTADOR (SÓLO SE MODIFICA SI SE INSERTA UN AVAL)
                If Session("rs").Fields("FLAG").value.ToString = "CONTAR" Then
                    Contador()
                End If

                'ALERTA DE AVAL EN OTROS CREDITOS
                If Session("rs").Fields("ALERTA").value.ToString = "" Then
                    lbl_alerta.Visible = False
                Else
                    lbl_alerta.Visible = True
                    lbl_alerta.Text = Session("rs").Fields("ALERTA").value.ToString
                End If

                'ALERTA DE CODEUDORES EN OTROS CREDITOS
                If Session("rs").Fields("ALERTACODEUDOR").value.ToString = "" Then
                    lbl_alertacodeudor.Visible = False
                Else
                    lbl_alertacodeudor.Visible = True
                    lbl_alertacodeudor.Text = Session("rs").Fields("ALERTACODEUDOR").value.ToString
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
            Llenaaval() ' Se muestra el aval en el DataGRid

        Else
            lbl_status.Text = "Error: Ya cumple con el máximo de avales establecidos"
        End If

    End Sub



    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDDEPEN", Session("adVarChar"), Session("adParamInput"), 10, ddl_Instituciones.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString
        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""


        Else

            txt_IdCliente.Text = CStr(idp)


        End If

        Session("Con").Close()
    End Sub

    Private Sub LlenaInstituciones()

        ddl_Instituciones.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        ddl_Instituciones.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Instituciones.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub Contador()

        Session("CONTADOR") = Session("CONTADOR") + 1
        Session("DIFERENCIA") = CInt(Session("MINAVAL")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If

        'lbl_maxavales.Text = "Faltan: " + " " + Session("DIFERENCIA").ToString + " aval(es)"
    End Sub

    Protected Sub lnk_busqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_busqueda.Click
        lbl_status.Text = ""
        lbl_alerta.Text = ""
        lbl_alertacodeudor.Text = ""
        lbl_alertadependiente.Text = ""
        lbl_alertaconsejo.Text = ""
    End Sub

    'Trae de la BD los valores minimos y máximos de Avales y el número de avales que ya tiene ese folio
    Protected Sub IniciaContador()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        'Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MIN_MAX_REQ_ADICIONALES"
        Session("rs") = Session("cmd").Execute()

        Session("MINAVAL") = Session("rs").Fields("MINAVAL").value.ToString
        Session("MAXAVAL") = Session("rs").Fields("MAXAVAL").value.ToString
        Session("CONTADOR") = Session("rs").Fields("CONTADORAVAL").value.ToString

        Session("DIFERENCIA") = CInt(Session("MINAVAL")) - CInt(Session("CONTADOR"))
        If Session("DIFERENCIA") < 0 Then
            Session("DIFERENCIA") = 0
        End If
        Session("Con").Close()

        ' lbl_maxavales.Text = "Faltan: " + " " + Session("DIFERENCIA").ToString + " aval(es)"
    End Sub


    Private Function Capacidad_pago_Aval(ByVal folio As String, ByVal idpersona_Aval As String) As Boolean
        Dim RESULTADO As Boolean

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, CInt(folio))
        'Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA_AVAL", Session("adVarChar"), Session("adParamInput"), 15, CInt(idpersona_Aval))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CAPACIDAD_PAGO_AVAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            If Session("rs").Fields("RESPUESTA").value.ToString = "AGREGAR" Then
                RESULTADO = True
            Else
                RESULTADO = False
            End If
        End If
        Session("Con").Close()
        Return RESULTADO
    End Function
End Class