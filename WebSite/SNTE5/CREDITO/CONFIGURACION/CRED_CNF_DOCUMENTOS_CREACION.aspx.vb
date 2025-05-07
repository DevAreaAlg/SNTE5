Public Class CRED_CNF_DOCUMENTOS_CREACION
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Edición de Contratos y Pagarés", "Contratos y Pagarés")

        Try
            If Session("ID_DOC") Then
                Response.Redirect("CRED_CNF_DOCUMENTOS.aspx")
            End If
        Catch ex As Exception
        End Try
        If Not Me.IsPostBack Then
            If Session("ID_DOC")(0).Equals("-1") Then
                txt_id.Text = "Nuevo Documento"
                div_estatus.Visible = False
            Else
                txt_id.Text = Session("ID_DOC")(0)
                consultaDatos(Session("ID_DOC")(0), Session("ID_DOC")(1))

            End If
        End If
        ' ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptUpPnlPostB", " $('.panel_folder_toogle').click(function(event) {var falder_content=$(this).parent().siblings('.panel-body').children('.panel-body_content');if($(this).hasClass('up')){$(this).removeClass('up');$(this).addClass('down');falder_content.show('6666',null);}else if($(this).hasClass('down')){$(this).removeClass('down');falder_content.hide('333',null);$(this).addClass('up');}});", True)
    End Sub
#Region "Consulta Datos"
    Private Sub consultaDatos(ByVal id As String, ByVal tipo As String)
        Session("Con").Open()

        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID", Session("adVarChar"), Session("adParamInput"), 10, id)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPO", Session("adVarChar"), Session("adParamInput"), 300, tipo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CONTR_PAG"
        Session("rs") = Session("cmd").Execute()

        txt_nombre.Text = TryCast(Session("rs"), ADODB.Recordset).Fields("NOMBRE").Value.ToString
        If Session("rs").Fields("ESTATUS").Value.ToString.Equals("2") Then
            lbl_inEdi.Visible = True
            div_estatus.Visible = False
        Else
            div_estatus.Visible = True
            lbl_inEdi.Visible = False
            checkbox_activo.Checked = CBool(Session("rs").Fields("ESTATUS").Value)
        End If
        cmb_tipoDoc.Enabled = False
        cmb_tipoDoc.SelectedValue = Session("ID_DOC")(1)
        Select Case Session("ID_DOC")(1)
            Case "PAGARE"
                div_clave.Visible = False
                txt_TIPO_COBRO.Text = Session("rs").Fields("TIPO_COBRO").Value.ToString
            Case "CONTRATO"
                div_TIPO_COBRO.Visible = False
                txt_clave.Text = Session("rs").Fields("CLAVE").Value.ToString
        End Select
        lbl_modificadox.Text = Session("rs").Fields("MODIFICADOX").Value.ToString
        lbl_fecha_mod.Text = Session("rs").Fields("FECHA_MOD").Value.ToString


        Session("Con").Close()
    End Sub
#End Region

End Class