Public Class CAP_CNF_CAPTACION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Captación", "Configuración Captación")

        If Not Me.IsPostBack Then
            LlenaCnfCaptacion()
        End If

    End Sub

    Private Sub LlenaCnfCaptacion()

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFCAPTACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_ISRVISTA.Text = Session("rs").Fields("ISRVISTA").value.ToString
            txt_DIASEXP.Text = Session("rs").Fields("DIASEXPIRA").value.ToString
            txt_TASAINFL.Text = Session("rs").Fields("TAZAINFLACION").value.ToString
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
        Session("parm") = Session("cmd").CreateParameter("ISRVISTA", Session("adVarChar"), Session("adParamInput"), 20, txt_ISRVISTA.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASEXPIRA", Session("adVarChar"), Session("adParamInput"), 20, txt_DIASEXP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAINFLACION", Session("adVarChar"), Session("adParamInput"), 10, txt_TASAINFL.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFCAPTACION"
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