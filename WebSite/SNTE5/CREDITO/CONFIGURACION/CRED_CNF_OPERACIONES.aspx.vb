Public Class CRED_CNF_OPERACIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Operaciones", "Configuración de Operaciones")

        If Not Me.IsPostBack Then
            LlenaCnfOperaciones()

        End If

    End Sub


    Private Sub LlenaCnfOperaciones()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFOPERACIONES"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_MINPAGSUC.Text = Session("rs").Fields("MINPAGSUC").value.ToString
            txt_DIASEXP.Text = Session("rs").Fields("DIASEXP").value.ToString
            cmb_AlertaPLD.SelectedValue = Session("rs").Fields("PERPLD").value.ToString
            cmb_inhabil.SelectedValue = Session("rs").Fields("DIASINHA").value.ToString
            txt_DIASASUESUC.Text = Session("rs").Fields("DIASASUE").value.ToString
            txt_CERTI.Text = Session("rs").Fields("CERTIFICADO").value.ToString
            txt_KEY.Text = Session("rs").Fields("CLAVE").value.ToString
            txt_RUTATIM.Text = Session("rs").Fields("RUTATIM").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Protected Sub btn_btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click

        ModificaCNFCAP()

    End Sub

    'METODO GUARDA LOS DATOS DE CONFIGURACION DE CAPTACION 
    Private Sub ModificaCNFCAP()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MINPAGSUC", Session("adVarChar"), Session("adParamInput"), 20, CInt(txt_MINPAGSUC.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASEXP", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_DIASEXP.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERPLD", Session("adVarChar"), Session("adParamInput"), 50, cmb_AlertaPLD.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASINHA", Session("adVarChar"), Session("adParamInput"), 10, cmb_inhabil.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASASUE", Session("adVarChar"), Session("adParamInput"), 10, CInt(txt_DIASASUESUC.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CERTIFICADO", Session("adVarChar"), Session("adParamInput"), 500, txt_CERTI.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLAVE", Session("adVarChar"), Session("adParamInput"), 100, txt_KEY.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RUTATIM", Session("adVarChar"), Session("adParamInput"), 2000, txt_RUTATIM.Text)
        Session("cmd").Parameters.Append(Session("parm"))


        Session("cmd").CommandText = "UPD_CNFOPERACIONES"
        Session("rs") = Session("cmd").Execute()

        Dim ACTUALIZA As String
        ACTUALIZA = Session("rs").Fields("ACTUALIZA").Value.ToString

        If ACTUALIZA = "SI" Then
            Session("Con").Close()
            lbl_Alerta.Text = "Guardado correctamente"
        Else

            lbl_Alerta.Text = ACTUALIZA
            Session("Con").Close()
        End If
    End Sub

End Class