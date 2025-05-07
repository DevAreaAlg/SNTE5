Public Class CRED_EXP_DICTAMEN
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_COMITE_DETALLE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            If Session("rs").Fields("COM_DIR").Value <> 1 Then
                lbl_com_num_ses.Text = Session("rs").Fields("NUM_SES").Value.ToString
                lbl_com_monto.Text = Session("rs").Fields("MONTO").Value.ToString
                lbl_com_res.Text = Session("rs").Fields("RES").Value
                lbl_com_fecha.Text = Left(Session("rs").Fields("FECHA").Value, 10)
                lbl_com_obs.Text = Session("rs").Fields("OBS").Value
                lbl_com_rec.Text = Session("rs").Fields("REC").Value
            Else
                lbl_dir_num_ses.Text = Session("rs").Fields("NUM_SES").Value.ToString
                lbl_dir_monto.Text = Session("rs").Fields("MONTO").Value.ToString
                lbl_dir_res.Text = Session("rs").Fields("RES").Value
                lbl_dir_fecha.Text = Left(Session("rs").Fields("FECHA").Value, 10)
                lbl_dir_obs.Text = Session("rs").Fields("OBS").Value
                lbl_dir_rec.Text = Session("rs").Fields("REC").Value
            End If
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub


End Class