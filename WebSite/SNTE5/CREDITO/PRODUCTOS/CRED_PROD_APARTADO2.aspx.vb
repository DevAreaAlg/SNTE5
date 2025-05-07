Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_PROD_APARTADO2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            TryCast(Me.Master, MasterMascore).CargaASPX("Requisitos Generales", "REQUISITOS GENERALES")

            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                'LLENO LOS RESPECTIVOS LABELS
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                Session("flag") = "0"
                Llenareqadicionales()
                Llenadocumentos()
                muestradocumentos()
            End If
        End If
    End Sub
#Region "REQUISITOS"

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

            txt_min_referencias.Text = Session("rs").Fields("MSTREQADI_MIN_REFERENCIAS").Value.ToString
            txt_max_referencias.Text = Session("rs").Fields("MSTREQADI_MAX_REFERENCIAS").Value.ToString
            txt_min_avales.Text = Session("rs").Fields("MSTREQADI_MIN_AVALES").Value.ToString
            txt_max_avales.Text = Session("rs").Fields("MSTREQADI_MAX_AVALES").Value.ToString
            txt_min_code.Text = Session("rs").Fields("MSTREQADI_MIN_CODEUDORES").Value.ToString
            txt_max_code.Text = Session("rs").Fields("MSTREQADI_MAX_CODEUDORES").Value.ToString


        End If
        Session("flag") = Session("rs").Fields("RESPUESTA").Value.ToString
        Session("Con").Close()


    End Sub

    Protected Sub btn_guardar_datos_Click(sender As Object, e As EventArgs)

        If ((CInt(txt_min_referencias.Text) > CInt(txt_max_referencias.Text)) Or (CInt(txt_min_avales.Text) > CInt(txt_max_avales.Text)) Or (CInt(txt_min_code.Text) > CInt(txt_max_code.Text))) Then
            lbl_guardado.Text = "Error: No puede agregar una cantidad mayor en el campo mínimo"
        Else
            If Session("flag").ToString = "1" Then
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_min_referencias.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_max_referencias.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINAVAL", Session("adVarChar"), Session("adParamInput"), 10, txt_min_avales.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXAVAL", Session("adVarChar"), Session("adParamInput"), 10, txt_max_avales.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINCODEUDOR", Session("adVarChar"), Session("adParamInput"), 10, txt_min_code.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXCODEUDOR", Session("adVarChar"), Session("adParamInput"), 10, txt_max_code.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "UPD_CNFPCR_REF_AV_COD"
                Session("cmd").Execute()
                Session("Con").Close()
                lbl_guardado.Text = "Guardado correctamente"
            Else 'insertarán nuevos datos.
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_min_referencias.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXREFERENCIA", Session("adVarChar"), Session("adParamInput"), 10, txt_max_referencias.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINAVAL", Session("adVarChar"), Session("adParamInput"), 10, txt_min_avales.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXAVAL", Session("adVarChar"), Session("adParamInput"), 10, txt_max_avales.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MINCODEUDOR", Session("adVarChar"), Session("adParamInput"), 10, txt_min_code.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MAXCODEUDOR", Session("adVarChar"), Session("adParamInput"), 10, txt_max_code.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "INS_CNFPCR_REF_AV_COD"
                Session("cmd").Execute()
                Session("Con").Close()
                lbl_guardado.Text = "Guardado correctamente"

            End If
        End If
        Llenareqadicionales()
    End Sub

#End Region

#Region "DOCUMENTACION"

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
        Session("parm") = Session("cmd").CreateParameter("FASE", Session("adVarChar"), Session("adParamInput"), 10, Mid(Fase, 6))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFPCR_DOCUMENTOS_AGREGADOS"
        Session("rs") = Session("cmd").Execute()
        lbl_guardado_doctos.Text = "Eliminado correctamente"
        Session("Con").Close()
        muestradocumentos()
    End Sub


    Private Sub DAG_documentos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_documentos.ItemCommand

        If (e.CommandName = "ELIMINAR") Then
            Elimina_documentos(e.Item.Cells(0).Text, e.Item.Cells(3).Text)

        End If

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
        Session("cmd").CommandText = "SEL_CNFPCR_DOCUMENTOS_ACTIVOS"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DESCRIPCION").Value.ToString, Session("rs").Fields("CATTIPDOC_ID_TIPO_DOC").Value.ToString)
            cmb_tipodoc.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'Limpia los controles al momento de guardar un documento
    Private Sub limpiaformadocs()
        cmb_tipodoc.SelectedIndex = 0
        cmb_cantidad.SelectedIndex = 0
        cmb_fase.SelectedIndex = 0
        lst_Documentos.Items.Clear()

    End Sub

    'Guarda la configuración correspondiente a la documentación
    Protected Sub btn_guardar_doctos_Click(sender As Object, e As EventArgs)

        lbl_guardado_doctos.Text = ""
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
        Session("cmd").CommandText = "INS_CNFPCR_REQUISITOS"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("VALIDACION").value.ToString = "ERROR" Then
            lbl_guardado_doctos.Text = "Error: Ya está asignado este documento en esa fase"
        Else
            lbl_guardado_doctos.Text = "Guardado correctamente"
        End If


        Session("Con").Close()


        muestradocumentos()
        limpiaformadocs()


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

    Protected Sub cmb_tipodoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_tipodoc.SelectedIndexChanged

        LlenaDocumentosAux()

    End Sub
#End Region

End Class