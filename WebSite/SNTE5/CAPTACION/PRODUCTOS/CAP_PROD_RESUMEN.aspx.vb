Public Class CAP_PROD_RESUMEN
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Resumen del Producto de Captación", "RESUMEN DEL PRODUCTO DE CAPTACIÓN")
        If Not Me.IsPostBack Then

        End If
        btn_imprimir.Attributes.Add("OnClick", "imprimir()")
        'Generales
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPRODRES"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RESUMEN_CAPTACION_DATOS_GENERALES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            ' lbl_periodicidadpagointeres.Text = Session("rs").fields("PERIODICIDAD").value.ToString + " Día(s)"
            lbl_saldominimo.Text = Session("rs").fields("SALDO").value.ToString
            lbl_mancomunadoproducto.Text = Session("rs").fields("MANCOMUNADO").value.ToString
            lbl_ppe_resp.Text = Session("rs").fields("PPE").value.ToString
            lbl_reqfun.Text = Session("rs").fields("REQFUN").value.ToString
            lbl_tipocap.Text = Session("rs").fields("TIPOCAP").value.ToString
            lbl_refresmin.Text = Session("rs").fields("MINREF").value.ToString
            lbl_refresmax.Text = Session("rs").fields("MAXREF").value.ToString
            lbl_benresmin.Text = Session("rs").fields("MINBEN").value.ToString
            lbl_benresmax.Text = Session("rs").fields("MAXBEN").value.ToString
            lbl_cuentares.Text = Session("rs").fields("DESCRIPCION").value.ToString
        End If



        Session("Con").Close()



        'Generales
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPRODRES"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RESUMEN_CAPTACION_DATOS_PRINCIPALES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_idprodres.Text = Session("IDPRODRES").ToString
            lbl_nombreres.Text = Session("rs").fields("NOMBRE").value.ToString
            ' lbl_descripcionres.Attributes.Add("style", "overflow :hidden")
            'lbl_descripcionres.Attributes.CssStyle.Add("TEXT-ALIGN", "right")
            lbl_descripcionres.Text = Session("rs").fields("DESCRIPCION_PROD").value.ToString
            If Session("rs").fields("TIPOP").value.ToString = "F" Then
                lbl_tipopersonares.Text = "FISICA"
            Else
                lbl_tipopersonares.Text = "MORAL"
            End If
            If Session("rs").fields("TIPOP").value.ToString = "A" Then
                lbl_tipopersonares.Text = "AMBOS"
            End If

            lbl_tipoprodres.Text = Session("rs").fields("TIPOPROD").value.ToString

        End If
        Session("Con").Close()
    End Sub


End Class