Public Class PLD_OPE_DETALLE_ENTREVISTA
    Inherits System.Web.UI.Page

    Protected Sub Page_Prerrender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'TryCast(Me.Master, MasterMascore).CargaASPX("Resumen Detalle Entrevista", "RESUMEN DETALLE ENTREVISTA")
        If Not Me.IsPostBack Then

        End If
        btn_imprimir.Attributes.Add("OnClick", "imprimir()")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lbl_IDAlerta1.Text = Session("IDALERTA_AUX")

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        If Session("VENGODE_HIST_ALERT_PLD") = "HistorialAlertasPersona.aspx" Then
            Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA_AUX"))
            Session("cmd").Parameters.Append(Session("parm"))
        Else
            Session("parm") = Session("cmd").CreateParameter("IDALERTA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDALERTA"))
            Session("cmd").Parameters.Append(Session("parm"))
        End If
        Session("cmd").CommandText = "SEL_DETALLE_ENTREVISTAS_PLD"
        Session("rs") = Session("cmd").Execute()

        Dim REALIZO_ENT As String

        lbl_Persona1.Text = Session("rs").Fields("NOMBRE_PERSONA").value.ToString
        lbl_Folio1.Text = Session("rs").Fields("FOLIO").value.ToString
        lbl_FechaAlerta1.Text = Session("rs").Fields("FECHA_ALERTA").value.ToString
        lbl_Usuario1.Text = Session("rs").Fields("USUARIO").value.ToString
        lbl_Sucursal1.Text = Session("rs").Fields("SUCURSAL").value.ToString
        REALIZO_ENT = Session("rs").Fields("REALIZO_ENT").value.ToString
        If REALIZO_ENT = "0" Then
            REALIZO_ENT = "NO"
        Else
            REALIZO_ENT = "SI"
        End If
        lbl_RealizoEnt1.Text = REALIZO_ENT

        If REALIZO_ENT = "NO" Then
            'pnl_NoRealizoEnt.Visible = True
            'pnl_SiRealizoEnt.Visible = False
            lbl_ObservacionesNoEnt1.Visible = False
            'lbl_ObservacionesNoEnt.Visible = True
            lbl_RazonnNoEnt1.Visible = False
            lbl_RazonnNoEnt1.Visible = False
            lbl_RazonnNoEnt1.Text = Session("rs").Fields("RAZON_NO_ENT").value.ToString
            lbl_ObservacionesNoEnt1.Text = Session("rs").Fields("OBSERVACIONES").value.ToString
        Else
            ' pnl_NoRealizoEnt.Visible = False
            ' pnl_SiRealizoEnt.Visible = True
            lbl_ObservacionesNoEnt1.Visible = True
            'lbl_ObservacionesNoEnt.Visible = True
            lbl_RazonnNoEnt1.Visible = True
            lbl_RazonnNoEnt1.Visible = True
            lbl_OrigenDep1.Text = Session("rs").Fields("ORIGEN_DEP").value.ToString
            lbl_PerDeposito1.Text = Session("rs").Fields("PER_DEP").value.ToString
            lbl_PuestoPolitico1.Text = Session("rs").Fields("PUESTO_POL").value.ToString
            If Session("rs").Fields("PUESTO_POL").value.ToString = "NO" Then
                lbl_AntPuestoPolitico1.Text = ""
            Else
                lbl_AntPuestoPolitico1.Text = Session("rs").Fields("ANT_PUESTO_POL").value.ToString
            End If

            If Session("rs").Fields("TIEMPO_MEX").value.ToString = "0" Then
                lbl_TiempoEnMex1.Text = ""
            Else
                lbl_TiempoEnMex1.Text = Session("rs").Fields("TIEMPO_MEX").value.ToString
            End If

            lbl_RazonEnMex1.Text = Session("rs").Fields("RAZON_MEX").value.ToString
            If Session("rs").Fields("TIEMPO_MAS_MEX").value.ToString = "0" Then
                lbl_TiempoMasMex1.Text = ""
            Else
                lbl_TiempoMasMex1.Text = Session("rs").Fields("TIEMPO_MAS_MEX").value.ToString
            End If

            lbl_CatPasaporte1.Text = Session("rs").Fields("CAT_PASAPORTE").value.ToString

            lbl_MoralNombre1.Text = Session("rs").Fields("MORAL_NOMBRE").value.ToString
            lbl_MoralRelacion1.Text = Session("rs").Fields("MORAL_RELACION").value.ToString
            lbl_MoralDireccion1.Text = Session("rs").Fields("MORAL_DIRECCION").value.ToString
            lbl_MoralNacionalidad1.Text = Session("rs").Fields("MORAL_NACIONALIDAD").value.ToString
            lbl_MoralTelefono1.Text = Session("rs").Fields("MORAL_TELEFONO").value.ToString
            lbl_MoralCelular1.Text = Session("rs").Fields("MORAL_CELULAR").value.ToString

            lbl_TerceroNombre1.Text = Session("rs").Fields("TERCERO_NOMBRE").value.ToString
            lbl_TerceroRelacion1.Text = Session("rs").Fields("TERCERO_RELACION").value.ToString
            lbl_TerceroDireccion1.Text = Session("rs").Fields("TERCERO_DIRECCION").value.ToString
            lbl_TerceroNacionalidad1.Text = Session("rs").Fields("TERCERO_NACIONALIDAD").value.ToString

            lbl_ObservacionesSiEnt1.Text = Session("rs").Fields("OBSERVACIONES").value.ToString
        End If

        Session("Con").Close()

    End Sub

    Private Sub lnk_EntrevistaPLDDigit_Click(sender As Object, e As EventArgs) Handles lnk_EntrevistaPLDDigit.Click
        Response.Redirect("../../DIGITALIZADOR/DIGI_MOSTRAR_ENTREVISTA.aspx")
    End Sub

End Class