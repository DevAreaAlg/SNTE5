Public Class CRED_PROD_APARTADO4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Parámetros Contables", "Parámetros Contables")

        If Not Me.IsPostBack Then
            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
                LlenaCuentasContables()
            End If
        End If
    End Sub

    Private Sub LlenaCuentasContables()

        cmb_CUENTAS.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_CTA_CAP_VIGENTE"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_CUENTAS.Items.Add(item)
            If Session("rs").Fields("ASIGNADO").Value.ToString = 1 Then
                cmb_CUENTAS.Items.FindByValue(Session("rs").Fields("ID").Value.ToString).Selected = True
            End If

            Session("rs").movenext()
        Loop

        Session("Con").Close()

        'esta comentado debido a que no existe todavia el modulo de configuracion de expedientes y la tabla mstexpediente
        ' VerificaCambioDestino(Session("PRODID"))

    End Sub

    Private Sub VerificaCambioDestino(ByVal idproducto As Integer)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, idproducto)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_VERIFICA_CAMBIO_DESTINO"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").fields("ESTATUS").value.ToString = "1" Then
            cmb_CUENTAS.Enabled = True
        Else
            cmb_CUENTAS.Enabled = False
        End If
        Session("Con").Close()

    End Sub

    Private Sub AsignaCuentaContable()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PRODID", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CUENTA", Session("adVarChar"), Session("adParamInput"), 10, cmb_CUENTAS.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFPCR_CTA_CAP_VIGENTE"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

    End Sub


    Protected Sub btn_guardacobro_Click(sender As Object, e As System.EventArgs)
        If cmb_CUENTAS.SelectedItem.Value = "0" Then
            lbl_ctacont.Text = "Error: Seleccione cuenta de capital vigente válida"
            Exit Sub
        End If

        AsignaCuentaContable()

        LlenaCuentasContables()
        lbl_ctacont.Text = "Guardado correctamente"
        Exit Sub
    End Sub

End Class