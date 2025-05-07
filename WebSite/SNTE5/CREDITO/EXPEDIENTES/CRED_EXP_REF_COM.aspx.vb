Public Class CRED_EXP_REF_COM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Referencias Comerciales", "Referencias Comerciales")

        If Not Me.IsPostBack Then

            lbl_Folio.Text = "Datos del Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")

        End If

        LlenaRefBancarias()
        llenaProveedores()
        LlenaClientes()
    End Sub

#Region "Referencias Bancarias"

    Private Sub LlenaRefBancarias() ' SEL_CNFEXP_ACTIVOS_CXCNODOC
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtEfectivo As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_REF_BANCARIAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtEfectivo, Session("rs"))
        DAG_efectivo.DataSource = dtEfectivo
        DAG_efectivo.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub Guarda_RefBancarias()
        Dim total As Decimal = CDec(txt_saldo_efe.Text)
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("BANCO", Session("adVarChar"), Session("adParamInput"), 100, txt_institucion_Efe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMCTA", Session("adVarChar"), Session("adParamInput"), 18, txt_nocta_efe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMEJEC", Session("adVarChar"), Session("adParamInput"), 150, txt_NomEjec.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 19, txt_telefono.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 15, txt_saldo_efe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_REF_BANCARIAS"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcEfectivo.Text = "Guardado correctamente"
        LlenaRefBancarias()
        txt_institucion_Efe.Text = ""
        txt_nocta_efe.Text = ""
        txt_saldo_efe.Text = ""
        txt_NomEjec.Text = ""
        txt_telefono.Text = ""
    End Sub

    Protected Sub btn_guarda_AcEfectivo_Click(sender As Object, e As EventArgs) Handles btn_guarda_AcEfectivo.Click
        Guarda_RefBancarias()
    End Sub

    Private Sub Elimina_refBanc(ByVal cveinst As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_REF_BANCARIAS"
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcEfectivo.Text = ""
    End Sub

    Protected Sub DAG_RefBanc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_efectivo.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            Elimina_refBanc(e.Item.Cells(1).Text)
            LlenaRefBancarias()
        End If
    End Sub

#End Region

#Region "Proveedores"

    Private Sub llenaProveedores()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtPasCXPNODOC As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PPAL_PROV"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtPasCXPNODOC, Session("rs"))
        DAG_CXPnoDOC.DataSource = dtPasCXPNODOC
        DAG_CXPnoDOC.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub Guarda_Proveedores()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROV", Session("adVarChar"), Session("adParamInput"), 200, txt_institucion_Prov.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIMITECRED", Session("adVarChar"), Session("adParamInput"), 18, txt_nocta_Prov.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMEJEC", Session("adVarChar"), Session("adParamInput"), 200, txt_NomEjec_Prov.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 19, txt_telefono_Prov.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SALDO", Session("adVarChar"), Session("adParamInput"), 15, txt_saldo_efe_Prov.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PPAL_PROV"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcCXPnoDOC.Text = "Guardado correctamente"
        llenaProveedores()
        txt_institucion_Prov.Text = ""
        txt_nocta_Prov.Text = ""
        txt_NomEjec_Prov.Text = ""
        txt_telefono_Prov.Text = ""
        txt_saldo_efe_Prov.Text = ""
    End Sub

    Protected Sub btn_guarda_Prov_Click(sender As Object, e As EventArgs) Handles btn_guarda_CXPnoDOC.Click
        Guarda_Proveedores()
    End Sub

    Private Sub Elimina_Proveedores(ByVal cveinst As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PPAL_PROV"
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_status_AcCXPnoDOC.Text = ""
    End Sub

    Protected Sub DAG_Proveedores_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_CXPnoDOC.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            Elimina_Proveedores(e.Item.Cells(1).Text)
            llenaProveedores()
        End If
    End Sub

#End Region

#Region "Clientes"
    Private Sub LlenaClientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtbienesmu As New Data.DataTable()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PPAL_CLIENTES"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtbienesmu, Session("rs"))
        DAG_bienesmu.DataSource = dtbienesmu
        DAG_bienesmu.DataBind()
        Session("Con").Close()

    End Sub

    Protected Sub Guarda_Clientes()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLIENTE", Session("adVarChar"), Session("adParamInput"), 100, txt_institucion_Cte.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VTAS_ANUALES", Session("adVarChar"), Session("adParamInput"), 200, txt_nocta_Cte.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMCONTACTO", Session("adVarChar"), Session("adParamInput"), 19, txt_NomEjec_Cte.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 19, txt_telefono_Cte.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 19, txt_saldo_efe_Cte.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_PPAL_CLIENTES"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusbienmu.Text = "Guardado correctamente"
        LlenaClientes()
        txt_institucion_Cte.Text = ""
        txt_nocta_Cte.Text = ""
        txt_saldo_efe_Cte.Text = ""
        txt_telefono_Cte.Text = ""
        txt_NomEjec_Cte.Text = ""
    End Sub

    Protected Sub btn_guarda_Ctes_Click(sender As Object, e As EventArgs) Handles btn_guardabienmu.Click
        Guarda_Clientes()
    End Sub

    Private Sub Elimina_Cliente(ByVal cveinst As Integer)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVE_INST", Session("adVarChar"), Session("adParamInput"), 10, cveinst)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "DEL_CNFEXP_PPAL_CLIENTES"
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusbienmu.Text = ""
    End Sub

    Protected Sub DAG_Clientes_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DAG_bienesmu.ItemCommand
        If (e.CommandName = "ELIMINAR") Then
            Elimina_Cliente(e.Item.Cells(1).Text)
            LlenaClientes()
        End If
    End Sub

#End Region

End Class