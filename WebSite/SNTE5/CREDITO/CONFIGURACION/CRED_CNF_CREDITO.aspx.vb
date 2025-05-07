Public Class CRED_CNF_CREDITO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Préstamo", "Configuración Préstamo")

        If Not Me.IsPostBack Then
            LlenaCnfCredito()

        End If

    End Sub


    Private Sub LlenaCnfCredito()

        Dim ACTIVO As String


        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFCREDITO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_LIMINFBURO.Text = Session("rs").Fields("LIMITEINF").value.ToString
            txt_LIMSUPBURO.Text = Session("rs").Fields("LIMITESUP").value.ToString
            txt_SCOREMINBURO.Text = Session("rs").Fields("SCOREMIN").value.ToString
            ACTIVO = Session("rs").Fields("ACTEPRC").value.ToString
            If ACTIVO = "1" Then
                checkbox_activo.Checked = True
            End If

            If ACTIVO = "0" Or ACTIVO <> "1" Then
                checkbox_activo.Checked = False
            End If
            txt_TASAMAX.Text = Session("rs").Fields("TASAMAXCAT").value.ToString
            txt_RANPROMPAG.Text = Session("rs").Fields("PROMPAGO").value.ToString
            txt_COGAIN.Text = Session("rs").Fields("COVERINV").value.ToString

        End If
        Session("Con").Close()



    End Sub







    Protected Sub btn_btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click

        ModificaCNFCAP()

    End Sub

    'METODO GUARDA LOS DATOS DE CONFIGURACION DE CAPTACION 
    Private Sub ModificaCNFCAP()

        Dim ACTIVO As String
        If checkbox_activo.Checked = True Then
            ACTIVO = "1"
        End If

        If checkbox_activo.Checked = False Then
            ACTIVO = "0"
        End If


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIMITEINF", Session("adVarChar"), Session("adParamInput"), 20, txt_LIMINFBURO.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LIMITESUP", Session("adVarChar"), Session("adParamInput"), 20, txt_LIMSUPBURO.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SCOREMIN", Session("adVarChar"), Session("adParamInput"), 20, txt_SCOREMINBURO.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ACTEPRC", Session("adVarChar"), Session("adParamInput"), 20, ACTIVO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMAXCAT", Session("adVarChar"), Session("adParamInput"), 20, Convert.ToDecimal(txt_TASAMAX.Text))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PROMPAGO", Session("adVarChar"), Session("adParamInput"), 20, txt_RANPROMPAG.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("COVERINV", Session("adVarChar"), Session("adParamInput"), 20, txt_COGAIN.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFCREDITO"
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