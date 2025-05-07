Public Class COB_DESP_RESUMEN_DESP
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        TryCast(Me.Master, MasterMascore).CargaASPX("Resumen de despacho", "RESUMEN DESPACHO")
        If Not Me.IsPostBack Then
            resumenDir(Session("DESPACHOID_M"))
            resumenPers(Session("DESPACHOID_M"))
            resumenContacto(Session("DESPACHOID_M"))
            resumenUsuarios(Session("DESPACHOID_M"))
        End If
        btn_imprimir.Attributes.Add("OnClick", "imprimir()")
    End Sub
    '---------------------------------RESUMEN------------------------------------------
    Private Sub resumenPers(ByVal despachoid As Integer)


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_GENERALES_PM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then


            If Session("rs").fields("RES").value.ToString = "1" Then
                lbl_id_abogado.Text = despachoid.ToString
                lbl_nombre_res.Text = Session("rs").Fields("NOMBRECOMERCIAL").Value + " " + Session("rs").Fields("RAZONSOCIAL").Value
                lbl_tit_res.Text = Session("rs").Fields("NOMBRE1").Value + " " + Session("rs").Fields("NOMBRE2").Value + " " + Session("rs").Fields("PATERNO").Value + " " + Session("rs").Fields("MATERNO").Value
                lbl_rfc_res.Text = IIf(Session("rs").Fields("RFC").Value.ToString = "", "-", Session("rs").Fields("RFC").Value.ToString)

                Dim notas As String
                notas = Session("rs").Fields("NOTAS").Value
                ' lbl_nota_res.Rows = CInt(notas.Length / 45) + 1
                lbl_nota_res.Text = notas

            End If


        End If
        Session("Con").Close()
    End Sub

    Private Sub resumenDir(ByVal despachoid As String)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 15, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_DIRECCIONES_PM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            lbl_callenum_res.Text = Session("rs").Fields("CALLE").Value.ToString + " " + "Número: " + "  " + Session("rs").Fields("NUMEXT").Value.ToString +
              IIf(Session("rs").Fields("NUMINT").Value.ToString = "", "-", "  Interior: " + Session("rs").Fields("NUMINT").Value.ToString)
            lbl_asentamiento_res.Text = Session("rs").Fields("ASENTAMIENTO").Value.ToString

            lbl_municipio_res.Text = Session("rs").Fields("MUNICIPIO").Value.ToString
            lbl_estado_res.Text = Session("rs").Fields("ESTADO").Value.ToString
        End If

        Session("Con").Close()

    End Sub

    Private Sub resumenContacto(ByVal despachoid As String)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 15, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_CONTACTOS_PM"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            lbl_c1_tel.Text = "(" + Session("rs").Fields("C1_LADA").Value.ToString + ") " + Session("rs").Fields("C1_TEL").Value.ToString
            lbl_c1_tel_ofi.Text = IIf(Session("rs").Fields("C1_LADA_OFI").Value.ToString = "", "-", "(" + Session("rs").Fields("C1_LADA_OFI").Value.ToString + ") " + Session("rs").Fields("C1_TEL_OFI").Value.ToString) +
                IIf(Session("rs").Fields("C1_EXT_OFI").Value.ToString = "", "", "  EXT: " + Session("rs").Fields("C1_EXT_OFI").Value.ToString)

            lbl_c2_tel.Text = IIf(Session("rs").Fields("C2_LADA").Value.ToString = "", "-", "(" + Session("rs").Fields("C2_LADA").Value.ToString + ") " + Session("rs").Fields("C2_TEL").Value.ToString)
            lbl_c2_tel_ofi.Text = IIf(Session("rs").Fields("C2_LADA_OFI").Value.ToString = "", "-", "(" + Session("rs").Fields("C2_LADA_OFI").Value.ToString + ") " + Session("rs").Fields("C2_TEL_OFI").Value.ToString) +
                IIf(Session("rs").Fields("C2_EXT_OFI").Value.ToString = "", "", "  EXT: " + Session("rs").Fields("C2_EXT_OFI").Value.ToString)

            lbl_c1_email.Text = Session("rs").Fields("C1_EMAIL").Value.ToString
            lbl_c2_email.Text = IIf(Session("rs").Fields("C2_EMAIL").Value.ToString = "", "-", Session("rs").Fields("C2_EMAIL").Value.ToString)


            lbl_c1_nombre.Text = Session("rs").Fields("C1_NOMBRE1").Value + " " + Session("rs").Fields("C1_NOMBRE2").Value + " " + Session("rs").Fields("C1_PATERNO").Value + " " + Session("rs").Fields("C1_MATERNO").Value
            lbl_c2_nombre.Text = IIf(Session("rs").Fields("C2_NOMBRE1").Value.ToString = "", "-", Session("rs").Fields("C2_NOMBRE1").Value + " " + Session("rs").Fields("C2_NOMBRE2").Value + " " + Session("rs").Fields("C2_PATERNO").Value + " " + Session("rs").Fields("C2_MATERNO").Value)
        End If
        Session("Con").Close()
    End Sub

    Private Sub resumenUsuarios(ByVal despachoid As String)

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dt As New Data.DataTable()


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDDESPACHO", Session("adVarChar"), Session("adParamInput"), 10, despachoid)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COB_DESP_USUARIOS_ASIGNADOS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dt, Session("rs"))
        Session("Con").Close()
        If dt.Rows.Count > 0 Then
            dag_users.Visible = True
            dag_users.DataSource = dt
            dag_users.DataBind()
        Else
            dag_users.Visible = False
        End If
    End Sub

End Class