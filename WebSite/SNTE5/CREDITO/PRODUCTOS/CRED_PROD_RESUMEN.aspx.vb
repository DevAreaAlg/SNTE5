Public Class CRED_PROD_RESUMEN
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Resumen del Producto", "RESUMEN DEL PRODUCTO")
        If Not Me.IsPostBack Then
            If Session("IDPRODRES") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If
        End If
        btn_imprimir.Attributes.Add("OnClick", "imprimir()")

        'Generales
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPRODRES"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RESUMEN_CREDITO_GENERAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            lbl_idprodres.Text = Session("rs").fields("ID_PROD").value.ToString
            lbl_nombreres.Text = Session("rs").fields("NOMBRE").value.ToString
            lbl_descripcionres.Text = Session("rs").fields("DESCRIPCION").value.ToString
            lbl_tipopersonares.Text = Session("rs").fields("TIPOP").value.ToString
            lbl_claveProdRes.Text = Session("rs").fields("CLAVE").value.ToString
            lbl_cveDsctoRes.Text = Session("rs").fields("CVE_DSCTO").value.ToString
            lbl_destinores.Text = Session("rs").fields("DESTINO").value.ToString
            lbl_clasifRes.Text = Session("rs").fields("CLAS_PROD").value.ToString
            lbl_tipoprodres.Text = Session("rs").fields("TIPO_PRODUCTO").value.ToString
            lbl_plazosliminfres.Text = Session("rs").fields("MIN_PLAZO").value.ToString
            lbl_plazoslimsupres.Text = Session("rs").fields("MAX_PLAZO").value.ToString
            lbl_plazosunidadres.Text = Session("rs").fields("UNIDAD").value.ToString
            lbl_montoinfres.Text = "$" + Session("MascoreG").FormatNumberCurr(Session("rs").fields("MIN_MONTO").value.ToString)
            lbl_montosupres.Text = "$" + Session("MascoreG").FormatNumberCurr(Session("rs").fields("MAX_MONTO").value.ToString)
            lbl_ivares.Text = Session("rs").fields("IVA").value.ToString + "%"
            lbl_cobroIVARes.Text = Session("rs").fields("COBRO_IVA").value.ToString
            lbl_tipoCobroRes.Text = Session("rs").fields("TIPO_COBRO").value.ToString
            lbl_cuentares.Text = Session("rs").fields("CUENTA").value.ToString
            lbl_saldoopeRes.Text = Session("rs").fields("SALDO_OPERATIVA").value.ToString

        End If
        Session("Con").Close()

        'REQUISITOS GENERALES
        'Configuracion Requerida
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODFUENTE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPRODRES"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPRODDESTINO", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_CLONA_REF_AV_COD"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_refresmin.Text = Session("rs").fields("MINREFERENCIA").value.ToString
            lbl_refresmax.Text = Session("rs").fields("MAXREFERENCIA").value.ToString
            lbl_avalesresmin.Text = Session("rs").fields("MINAVAL").value.ToString
            lbl_avalesresmax.Text = Session("rs").fields("MAXAVAL").value.ToString
            lbl_codresmin.Text = Session("rs").fields("MINCODEUDOR").value.ToString
            lbl_codresmax.Text = Session("rs").fields("MAXCODEUDOR").value.ToString
        End If
        Session("Con").Close()




        'PARAMETROS DE INTERESES MORATORIOS Y CARTERA VENCIDA
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODFUENTE", Session("adVarChar"), Session("adParamInput"), 10, Session("IDPRODRES"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDPRODDESTINO", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_CLONA_PRODUCTO_DIAS_VENCIA"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_diasvencidares.Text = Session("rs").fields("DIASVENCIDA").value.ToString
            lbl_pagossosres.Text = Session("rs").fields("PAGOS").value.ToString
            lbl_montominres.Text = IIf(Session("MascoreG").FormatNumberCurr(Session("rs").fields("MONTOCOM").value.ToString) = "0.00", "INACTIVO", "$" + Session("MascoreG").FormatNumberCurr(Session("rs").fields("MONTOCOM").value.ToString))
            lbl_diasintnorres.Text = Session("rs").fields("DIASNORMAL").value.ToString
            lbl_diasintmorres.Text = Session("rs").fields("DIASMORATORIO").value.ToString
            lbl_pergraciares.Text = IIf(Session("rs").fields("PERIODOGRAINT").value.ToString = "1", "SI", "NO")
            lbl_pergraciares.Text = Session("rs").fields("PERIODOGRAINT").value.ToString
            lbl_cobromorres.Text = Session("rs").fields("COBRO").value.ToString
            lbl_razoningresores.Text = Session("rs").fields("RAZONINGRESO").value.ToString
            lbl_montosegurores.Text = "$" + Session("MascoreG").FormatNumberCurr(Session("rs").fields("MONTOSEGURO").value.ToString)
            lbl_montodirectorres.Text = IIf(Session("MascoreG").FormatNumberCurr(Session("rs").fields("MONTODIRECTOR").value.ToString) = "0.00", "INACTIVO", "$" + Session("MascoreG").FormatNumberCurr(Session("rs").fields("MONTODIRECTOR").value.ToString))


        End If
        Session("Con").Close()






    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

End Class