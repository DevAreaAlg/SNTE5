Public Class CAP_PROD_APARTADO1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Parámetros Generales", "PARÁMETROS GENERALES")

        If Not Me.IsPostBack Then
            'LLENO LOS RESPECTIVOS LABELS
            lbl_Producto.Text = Session("PROD_NOMBRE").ToString

            'LLENO LOS COMBOS
            Llenareqadicionales()
            muestradocumentos()
            Llenadocumentos()
            llenacaptacion()
            LlenaParametrosGenerales()

            If rad_no.Checked Then 'SI EL PRODUCTO ES MANCOMUNADO CAPTURA EL NUMERO DE PERSONAS MANCOMUNADAS
                RequiredFieldValidator_perAut.Enabled = False
                RequiredFieldValidator_numMan.Enabled = False
                'txt_perAut.Text = ""
                'txt_perAut.Enabled = False
                'txt_numMan.Enabled = False
                'txt_numMan.Text = ""
            ElseIf rad_fun_si.Checked Then

                RequiredFieldValidator_perAut.Enabled = True
                RequiredFieldValidator_numMan.Enabled = True
                ''txt_perAut.Text = ""
                'txt_perAut.Enabled = True
                'txt_numMan.Enabled = True
                ''txt_numMan.Text = ""

            End If

        End If

    End Sub

    '--------------------REQUISITOS ADICIONALES------------------

    'Muestra los  requisitos adicionales (MIN y MAX Referencias y Beneficiarios)
    Private Sub Llenareqadicionales()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_REF_AV_COD_PRODUCTO"

        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("RESPUESTA").Value.ToString = "1" Then

            txt_referencia.Text = Session("rs").Fields("MSTREQADI_MIN_REFERENCIAS").Value.ToString
            txt_maxreferencia.Text = Session("rs").Fields("MSTREQADI_MAX_REFERENCIAS").Value.ToString
            txt_beneficiario.Text = Session("rs").Fields("MSTREQADI_MIN_BENEFICIARIOS").Value.ToString
            txt_maxbeneficiario.Text = Session("rs").Fields("MSTREQADI_MAX_BENEFICIARIOS").Value.ToString


        End If
        Session("flag") = Session("rs").Fields("RESPUESTA").Value.ToString

        Session("Con").Close()


    End Sub

    'Guarda los datos de la configuración de referencias, avales y codeudores
    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click

        lbl_estatusgneral.Text = ""


        If ((CInt(txt_referencia.Text) > CInt(txt_maxreferencia.Text)) Or (CInt(txt_beneficiario.Text) > CInt(txt_maxbeneficiario.Text))) Then
            lbl_estatus.Text = "Error: No puede agregar una cantidad mayor en el campo minimo"
        Else
            If Session("flag").ToString = "1" Then

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_referencia.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_maxreferencia.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_beneficiario.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_maxbeneficiario.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_CNFPRODCAP_REF_BEN"
                Session("cmd").Execute()
                Session("Con").Close()
                lbl_estatus.Text = "Guardado correctamente"
            Else 'insertarán nuevos datos.

                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_referencia.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_maxreferencia.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_beneficiario.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXBENEFICIARIO", Session("adVarChar"), Session("adParamInput"), 10, txt_maxbeneficiario.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_CNFPRODCAP_REF_BEN"
                Session("cmd").Execute()
                Session("Con").Close()
                lbl_estatus.Text = "Guardado correctamente"

            End If
        End If
    End Sub

    '-------------------DOCUMENTACION-----------------------

    'DBGRID que muestra los documentos agregados
    Private Sub muestradocumentos()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdocumentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_DOCUMENTOS_AGREGADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtdocumentos, Session("rs"))
        DAG_documentos.DataSource = dtdocumentos
        DAG_documentos.DataBind()
        Session("Con").Close()

    End Sub

    Private Sub DAG_documentos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_documentos.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            Elimina_documentos(e.Item.Cells(0).Text, e.Item.Cells(3).Text)

            muestradocumentos()
        End If

    End Sub

    'Botón ELiminar del DBGRID (Que permite eliminar la documentación seleccionada completamente de la tabla CATREQUISITOS
    Private Sub Elimina_documentos(ByVal idtipodoc As String, ByVal Fase As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("TIPODOC", Session("adVarChar"), Session("adParamInput"), 10, idtipodoc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 15, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        If Fase = "FASE 1" Then
            Session("parm") = Session("cmd").CreateParameter("FASE", Session("adVarChar"), Session("adParamInput"), 10, 1)
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("FASE", Session("adVarChar"), Session("adParamInput"), 10, 2)
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("cmd").CommandText = "DEL_CNFPRODCAP_DOCUMENTOS_AGREGADOS"
        Session("rs") = Session("cmd").Execute()
        lbl_verifica2.Text = "Eliminado correctamente"
        Session("Con").Close()
    End Sub

    'Llena los documentos que existen en la BD
    Private Sub Llenadocumentos()

        cmb_tipodoc.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_tipodoc.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPCR_DOCUMENTOS_ACTIVOS_CAPTACION"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CATTIPDOC_ID_TIPO_DOC").Value.ToString)
            cmb_tipodoc.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'Guarda la configuración correspondiente a la documentación
    Protected Sub btn_guardardoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardardoc.Click

        lbl_estatusgneral.Text = ""
        lbl_estatus.Text = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDTIPO", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipodoc.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CANTIDAD", Session("adVarChar"), Session("adParamInput"), 10, cmb_cantidad.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FASE", Session("adVarChar"), Session("adParamInput"), 10, cmb_fase.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFPRODCAP_REQUISITOS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("VALIDACION").value.ToString = "AGREGAR" Then
            lbl_verifica2.Text = "Guardado correctamente"
        Else
            lbl_verifica2.Text = "Error: Ya está asignado este documento en esa fase"
        End If


        Session("Con").Close()

        muestradocumentos()
        limpiaforma3()

    End Sub

    'Limpia los controles al momento de guardar un documento
    Private Sub limpiaforma3()

        cmb_tipodoc.SelectedIndex = 0
        cmb_cantidad.SelectedIndex = 0
        cmb_fase.SelectedIndex = 0

    End Sub

    '-----------------------GENERALES-----------------------

    'Muestra los diferentes tipos de captacion
    Private Sub llenacaptacion()

        cmb_captacion.Items.Clear()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_captacion.Items.Add(elija)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFPRODCAP_TIPO_CAPTACION"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_captacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()


    End Sub

    Protected Sub cmb_captacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_captacion.SelectedIndexChanged

        If cmb_captacion.SelectedItem.Value = "1" Then
            pnl_VistaSaldoMin.Visible = True
            rad_VistaSaldoMinReqSI.Checked = False
            rad_VistaSaldoMinReqNO.Checked = True
            txt_VistaSaldoMin.Enabled = False
            txt_VistaSaldoMin.Text = "0.0"
        Else
            pnl_VistaSaldoMin.Visible = False
            rad_VistaSaldoMinReqSI.Checked = False
            rad_VistaSaldoMinReqNO.Checked = True
            txt_VistaSaldoMin.Enabled = False
            txt_VistaSaldoMin.Text = "0.0"
        End If

    End Sub

    Protected Sub rad_VistaSaldoMinReqNO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_VistaSaldoMinReqNO.CheckedChanged
        txt_VistaSaldoMin.Enabled = False
        txt_VistaSaldoMin.Text = "0.0"
    End Sub

    Protected Sub rad_VistaSaldoMinReqSI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_VistaSaldoMinReqSI.CheckedChanged
        txt_VistaSaldoMin.Enabled = True
        txt_VistaSaldoMin.Text = ""
    End Sub

    'Muestra si el producto es mancomunado y la periodicidad de pago de interes
    Private Sub LlenaParametrosGenerales()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPRODCAP_MANCOMUNADO_PERIODICIDAD"
        Session("rs") = Session("cmd").Execute()

        Dim val As String
        val = Session("rs").Fields("CAPTACION").value.ToString

        ' txt_periodicidad.Text = Session("rs").Fields("PERIODICIDAD").Value.ToString
        txt_saldo.Text = Session("rs").Fields("SALDO").Value.ToString
        cmb_captacion.SelectedValue = val

        'Mancomunado
        Select Case Session("rs").Fields("MANCOMUNADO").Value.ToString
            Case "-1"
                rad_no.Checked = True
                txt_perAut.Enabled = False
                txt_numMan.Enabled = False
            Case "0"
                rad_no.Checked = True
                txt_perAut.Enabled = False
                txt_numMan.Enabled = False
            Case "1"
                rad_si.Checked = True
                txt_perAut.Enabled = True
                txt_numMan.Enabled = True

            Case Else
                lbl_estatusgneral.Text = "no se guarda"


        End Select

        'Investigacion PPE
        Select Case Session("rs").Fields("REQPPE").Value.ToString
            Case "-1"
                rad_ppe_no.Checked = True
                rad_ppe_si.Checked = False
            Case "0"
                rad_ppe_no.Checked = True
            Case Else
                rad_ppe_si.Checked = True

        End Select

        'REQUERIMIENTO RELACION CON FUNCIONARIOS
        Select Case Session("rs").Fields("REQFUN").Value
            Case "-1"
                rad_fun_no.Checked = True
                rad_fun_si.Checked = False
            Case "0"
                rad_fun_no.Checked = True
            Case Else
                rad_fun_si.Checked = True

        End Select

        Select Case Session("rs").Fields("NUMMANCOMUNADAS").Value
            Case "-1"
                txt_numMan.Text = ""
            Case "0"
                txt_numMan.Text = Session("rs").Fields("NUMMANCOMUNADAS").Value
            Case "1"
                txt_numMan.Text = Session("rs").Fields("NUMMANCOMUNADAS").Value
            Case Else
                txt_numMan.Text = Session("rs").Fields("NUMMANCOMUNADAS").Value
        End Select

        Select Case Session("rs").Fields("NUMFIRMAS").Value
            Case "-1"
                txt_perAut.Text = ""
            Case "0"
                txt_perAut.Text = Session("rs").Fields("NUMFIRMAS").Value
            Case "1"
                txt_perAut.Text = Session("rs").Fields("NUMFIRMAS").Value
            Case Else
                txt_perAut.Text = Session("rs").Fields("NUMFIRMAS").Value
        End Select

        If val = "1" Then
            pnl_VistaSaldoMin.Visible = True
            If Session("rs").Fields("VISTA_REQ_SALDO_MIN").Value = "1" Then
                rad_VistaSaldoMinReqSI.Checked = True
                rad_VistaSaldoMinReqNO.Checked = False
                txt_VistaSaldoMin.Enabled = True
                txt_VistaSaldoMin.Text = Session("rs").Fields("VISTA_SALDO_MIN").Value
            Else
                rad_VistaSaldoMinReqSI.Checked = False
                rad_VistaSaldoMinReqNO.Checked = True
                txt_VistaSaldoMin.Enabled = False
                txt_VistaSaldoMin.Text = Session("rs").Fields("VISTA_SALDO_MIN").Value
            End If
        Else
            pnl_VistaSaldoMin.Visible = False
            rad_VistaSaldoMinReqSI.Checked = False
            rad_VistaSaldoMinReqNO.Checked = True
            txt_VistaSaldoMin.Enabled = False
            txt_VistaSaldoMin.Text = "0.0"
        End If

        Session("Con").Close()


    End Sub

    Private Function validaradios(ByVal rad_positivo As RadioButton, ByVal rad_negativo As RadioButton) As Boolean

        Dim resp As Boolean
        resp = True

        If rad_positivo.Checked = False And rad_negativo.Checked = False Then
            resp = False
        End If

        Return resp
    End Function

    'Btn guardar mancomunado y periodicidad
    Protected Sub btn_guardargenerales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardargenerales.Click


        If Validamonto(txt_saldo.Text) = True Then
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            If rad_si.Checked Then
                Session("parm") = Session("cmd").CreateParameter("MANCOMUNADO", Session("adVarChar"), Session("adParamInput"), 10, 1)
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("MANCOMUNADO", Session("adVarChar"), Session("adParamInput"), 10, 0)
                Session("cmd").Parameters.Append(Session("parm"))
            End If



            Session("parm") = Session("cmd").CreateParameter("CAPTACION", Session("adVarChar"), Session("adParamInput"), 10, cmb_captacion.SelectedItem.Value.ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 10, txt_saldo.Text)
            Session("cmd").Parameters.Append(Session("parm"))

            If rad_ppe_si.Checked Then
                Session("parm") = Session("cmd").CreateParameter("REQPPE", Session("adVarChar"), Session("adParamInput"), 10, 1)
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("REQPPE", Session("adVarChar"), Session("adParamInput"), 10, 0)
                Session("cmd").Parameters.Append(Session("parm"))
            End If
            If rad_fun_si.Checked Then 'SI REQUIERE CAPTURA RELACION CON FUNCIONARIOS
                Session("parm") = Session("cmd").CreateParameter("REQFUN", Session("adVarChar"), Session("adParamInput"), 10, 1)
                Session("cmd").Parameters.Append(Session("parm"))
            Else 'NO REQUIERE  RELACION CON FUNCIONARIOS
                Session("parm") = Session("cmd").CreateParameter("REQFUN", Session("adVarChar"), Session("adParamInput"), 10, 0)
                Session("cmd").Parameters.Append(Session("parm"))
            End If

            If rad_si.Checked Then 'SI EL PRODUCTO ES MANCOMUNADO CAPTURA EL NUMERO DE PERSONAS MANCOMUNADAS
                Session("parm") = Session("cmd").CreateParameter("NUMMAN", Session("adVarChar"), Session("adParamInput"), 15, txt_numMan.Text)
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("NUMMAN", Session("adVarChar"), Session("adParamInput"), 15, 0)
                Session("cmd").Parameters.Append(Session("parm"))

            End If

            If rad_si.Checked Then 'SI EL PRODUCTO ES MANCOMUNADO CAPTURA EL NUMERO DE FIRMAS
                Session("parm") = Session("cmd").CreateParameter("NUMFIRMA", Session("adVarChar"), Session("adParamInput"), 15, txt_perAut.Text)
                Session("cmd").Parameters.Append(Session("parm"))
            Else
                Session("parm") = Session("cmd").CreateParameter("NUMFIRMA", Session("adVarChar"), Session("adParamInput"), 15, 0)
                Session("cmd").Parameters.Append(Session("parm"))

            End If


            If cmb_captacion.SelectedItem.Value = "1" Then
                If rad_VistaSaldoMinReqSI.Checked = True Then
                    Session("parm") = Session("cmd").CreateParameter("VISTA_REQ_SALDO_MIN", Session("adVarChar"), Session("adParamInput"), 10, 1)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("VISTA_SALDO_MIN", Session("adVarChar"), Session("adParamInput"), 50, txt_VistaSaldoMin.Text)
                    Session("cmd").Parameters.Append(Session("parm"))
                Else
                    Session("parm") = Session("cmd").CreateParameter("VISTA_REQ_SALDO_MIN", Session("adVarChar"), Session("adParamInput"), 10, 0)
                    Session("cmd").Parameters.Append(Session("parm"))
                    Session("parm") = Session("cmd").CreateParameter("VISTA_SALDO_MIN", Session("adVarChar"), Session("adParamInput"), 50, 0.0)
                    Session("cmd").Parameters.Append(Session("parm"))
                End If
            Else
                Session("parm") = Session("cmd").CreateParameter("VISTA_REQ_SALDO_MIN", Session("adVarChar"), Session("adParamInput"), 10, 0)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("VISTA_SALDO_MIN", Session("adVarChar"), Session("adParamInput"), 50, 0.0)
                Session("cmd").Parameters.Append(Session("parm"))
            End If

            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_CNFPRODCAP_MANCOMUNADO_PERIODICIDAD"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()

            'If Session("flagGeneral").ToString = "1" Then 'se actualiza los 2 registros
            '    lbl_estatusgneral.Text = "Se han actualizado correctamente los datos"
            'Else ' es nuevo el registro
            lbl_estatusgneral.Text = "Guardado correctamente"
            'End If
        Else
            lbl_estatusgneral.Text = "Error: El saldo mínimo es incorrecto"

        End If





    End Sub

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    'Validación del Radio NO
    Protected Sub rad_nosoc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_no.CheckedChanged

        RequiredFieldValidator_perAut.Enabled = False
        RequiredFieldValidator_numMan.Enabled = False
        txt_perAut.Text = ""
        txt_perAut.Enabled = False
        txt_numMan.Enabled = False
        txt_numMan.Text = ""

    End Sub

    'Validacion del Radio SI
    Protected Sub rad_sisoc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_si.CheckedChanged

        RequiredFieldValidator_perAut.Enabled = True
        RequiredFieldValidator_numMan.Enabled = True
        txt_perAut.Text = ""
        txt_perAut.Enabled = True
        txt_numMan.Enabled = True
        txt_numMan.Text = ""

    End Sub

    Protected Sub cmb_tipodoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipodoc.SelectedIndexChanged

        LlenaDocumentosAux()

    End Sub

    Private Sub LlenaDocumentosAux()

        lst_Documentos.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDTIPODOC", Session("adVarChar"), Session("adParamInput"), 10, cmb_tipodoc.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DOCUMENTOS_X_TIPO_DOCUMENTO_PRODUCTO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DOCUMENTO").VALUE.ToString, Session("rs").Fields("IDDOC").Value.ToString)
            lst_Documentos.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

End Class