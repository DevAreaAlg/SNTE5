Public Class CRED_CNF_PAGARES_ADM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Envío de Pagarés", "Envío de Pagarés")
        If Not Me.IsPostBack Then

            ObtieneAbrvEmpresa()
            LlenaSucursales()
            LlenaLotesPendientes()
            LlenaLotesEntregados()

        End If
    End Sub


    Private Sub ObtieneAbrvEmpresa()

        Session("Con").Open()

        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ABRV_EMPRESA"
        Session("rs") = Session("cmd").Execute()

        Dim abrv As String
        abrv = Session("rs").Fields("ABRV").Value.ToString()

        lbl_CSGFolioIni.Text = abrv + "-"
        lbl_CSGFolioFin.Text = abrv + "-"

        Session("Con").Close()

    End Sub

    'Muestra las Sucursales Activas
    Private Sub LlenaSucursales()

        cmb_SucDest.Items.Clear()
        cmb_sucursal.Items.Clear()
        Dim elija As New ListItem("ELIJA", "0")
        Dim elija2 As New ListItem("ELIJA", "0")
        cmb_SucDest.Items.Add(elija)
        cmb_sucursal.Items.Add(elija2)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_SucDest.Items.Add(item)
            cmb_sucursal.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'Llena los lotes de pagare que aun no han sido confirmados a entrega por parte d ela sucursal
    Protected Sub LlenaLotesPendientes()

        'Se llena una tabla con los lotes pagares pendientes por ser recibidos

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtLotesPagare As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_LOTES_PAGARE_PEND"

        Session("rs") = Session("cmd").Execute()

        'se agregan los lotes a una tabla en memoria
        custDA.Fill(dtLotesPagare, Session("rs"))
        'se vacian los lotes al formulario
        dag_LotesPend.DataSource = dtLotesPagare
        dag_LotesPend.DataBind()

        Session("Con").Close()

    End Sub

    'Llena los lotes de pagare que aun no han sido confirmados a entrega por parte d ela sucursal
    Protected Sub LlenaLotesEntregados()

        'Se llena una tabla con los lotes pagares pendientes por ser recibidos

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtLotesPagareEnt As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_LOTES_PAGARE_RECIBIDOS"

        Session("rs") = Session("cmd").Execute()

        'se agregan los lotes a una tabla en memoria
        custDA.Fill(dtLotesPagareEnt, Session("rs"))
        'se vacian los lotes al formulario
        dag_LotesRecibidos.DataSource = dtLotesPagareEnt
        dag_LotesRecibidos.DataBind()

        Session("Con").Close()

    End Sub

    Protected Sub btn_EnviaLote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_EnviaLote.Click

        EnvioLotes()

    End Sub

    'Solicitud de 
    Private Sub EnvioLotes()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIOINI", Session("adVarChar"), Session("adParamInput"), 20, txt_FolioIni.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIOFIN", Session("adVarChar"), Session("adParamInput"), 20, txt_FolioFin.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUCDEST", Session("adVarChar"), Session("adParamInput"), 10, cmb_SucDest.SelectedValue.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_ENVIO_LOTES"
        Session("rs") = Session("cmd").Execute()

        Dim ENVIO As String
        Dim abreviatura As String
        ENVIO = Session("rs").Fields("ENVIO").Value.ToString
        abreviatura = Session("rs").Fields("ABREVIATURA").Value.ToString

        If ENVIO = "SI" Then
            Session("Con").Close()
            ObtieneCorreos(abreviatura + "-" + txt_FolioIni.Text, abreviatura + "-" + txt_FolioFin.Text, cmb_SucDest.SelectedItem.Value, cmb_SucDest.SelectedItem.Text)
            lbl_Alerta.Text = "Lote de Pagarés Enviado, se ha enviado un email de envio a la sucursal correspondiente."
            Limpia()
        Else

            lbl_Alerta.Text = ENVIO
            Session("Con").Close()
        End If

        LlenaLotesPendientes()
        LlenaLotesEntregados()

    End Sub

    Private Sub Limpia()

        txt_FolioFin.Text = ""
        txt_FolioIni.Text = ""
        cmb_SucDest.SelectedValue = "0"

    End Sub

    'Llena los lotes de pagare que aun no han sido confirmados a entrega por parte d ela sucursal
    Protected Sub ObtieneCorreos(ByVal FolioIni As String, ByVal FolioFin As String, ByVal Suc As Integer, ByVal Sucursal As String)

        'obtengo correos de miembros de comite (IDENTIFICADOR = 1)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_LOTES_PAGARE_CORREO"
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Suc)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'ENVIO EL CORREO A CADA MIEMBRO
        Do While Not Session("rs").EOF
            EnviaMail(Session("rs").Fields("EMAIL").Value.ToString, Session("rs").Fields("NOMBRE").Value.ToString, FolioIni, FolioFin, Sucursal)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub EnviaMail(ByVal destinatario As String, ByVal nombre As String, ByVal folioini As String, ByVal foliofin As String, ByVal Sucursal As String)
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder

        subject = "Envio de lote de pagaré a sucursal"
        sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
        sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
        sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        sbhtml.Append("<tr><td>Estimado(a) : " + nombre + "</td></tr>")
        sbhtml.Append("<tr><td>Se le avisa que se ha enviado un lote de pagarés con folio inicial: " + "<b>" + folioini + "</b>" + "  y folio final: " + "<b>" + foliofin + "</b>" + " a la sucursal " + "<b>" + Sucursal + "</b>" + "</td></tr>")
        sbhtml.Append("<tr><td>Una vez que se le entregue el lote de pagarés favor de confirmar la entrega en su respectivo módulo en el apartado de configuración.</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br />")
        sbhtml.Append("<br></br>")
        sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
        sbhtml.Append("</table>")
        sbhtml.Append("<br></br>")

        clase_Correo.Envio_email(sbhtml.ToString, subject, destinatario, cc)

    End Sub
    Protected Sub btn_cancelar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar1.Click
        lbl_AlertaFiltro.Text = ""
        txt_fechaA.Text = ""
        txt_fechaB.Text = ""
        LlenaSucursales()
        LlenaLotesPendientes()
        LlenaLotesEntregados()
    End Sub

    Protected Sub btn_aceptar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aceptar1.Click
        lbl_AlertaFiltro.Text = ""
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtLotesPagareEnt As New Data.DataTable()

        If (txt_fechaA.Text <> "" And txt_fechaB.Text = "") Or (txt_fechaA.Text = "" And txt_fechaB.Text <> "") Then

            lbl_AlertaFiltro.Text = "Error: Se debe llenar ambas fechas para obtener un rango de fechas"

        Else

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_FILTRO_LOTES_PAGARE"
            Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, cmb_sucursal.SelectedIndex)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAA", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaA.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FECHAB", Session("adVarChar"), Session("adParamInput"), 10, txt_fechaB.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            Session("rs") = Session("cmd").Execute()

            'se agregan los lotes a una tabla en memoria
            custDA.Fill(dtLotesPagareEnt, Session("rs"))
            'se vacian los lotes al formulario
            dag_LotesRecibidos.DataSource = dtLotesPagareEnt
            dag_LotesRecibidos.DataBind()

            Session("Con").Close()

        End If

        LlenaLotesPendientes()

    End Sub


End Class