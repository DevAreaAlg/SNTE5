Public Class CRED_VEN_PAGO_ADM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Préstamos a Pagar", "Administrador de Pago de Préstamos")
        If Not Me.IsPostBack Then
            verifica_caja()
        End If
    End Sub

    Private Sub verifica_caja()
        'lblidcaurs.Text = Session("IDCAJA_USR")

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_CAJA"
        Session("rs") = Session("cmd").Execute()

        Session("AUXILIAR") = Session("rs").Fields("RES").Value.ToString
        Session("IDCAJA_USR") = Session("rs").Fields("IDCAJA").Value.ToString

        Session("Con").Close()

        If Session("AUXILIAR") = "0" Then
            lbl_status.Text = "Tu equipo no esta dado de alta para operar como caja, por lo tanto no puedes usar este apartado"
        Else
            lbl_status.Text = ""
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDCAJA", Session("adVarChar"), Session("adParamInput"), 10, Session("IDCAJA_USR"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_VERIFICA_CORTE_INICIAL"
            Session("rs") = Session("cmd").Execute()
            Session("AUXILIAR") = Session("rs").Fields("RES").Value
            Session("Con").Close()
            If Session("AUXILIAR") = "0" Then
                lbl_status.Text = "Esta caja no ha ejecutado corte inicial, por lo tanto no puede operar."
            Else
                lbl_status.Text = ""
                LlenaExpPendientes()
            End If
        End If

        Session("AUXILIAR") = Nothing

    End Sub

    'Llena los lotes de pagare que aun no han sido confirmados a entrega por parte d ela sucursal
    Protected Sub LlenaExpPendientes()

        'Se llena una tabla con los lotes pagares pendientes por ser recibidos

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtExpPend As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EXP_X_PAGAR"

        Session("rs") = Session("cmd").Execute()

        'se agregan los lotes a una tabla en memoria
        custDA.Fill(dtExpPend, Session("rs"))
        'se vacian los lotes al formulario
        dag_CredLib.DataSource = dtExpPend
        dag_CredLib.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub dag_CredLib_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_CredLib.ItemCommand

        'e.Item es la fila sobre la cual dio click el cursor

        If (e.CommandName = "PAGAR") Then
            'lbl_subtitulo.Text = e.Item.ItemIndex
            Session("FOLIO") = e.Item.Cells(0).Text
            Session("PERSONAID") = e.Item.Cells(1).Text
            Session("PROSPECTO") = e.Item.Cells(2).Text
            Session("PRODUCTO") = e.Item.Cells(3).Text
            ValidaPago()

        End If

    End Sub

    Function get_clas_cred() As String
        Dim clasCred As String = ""
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CLASIF_CRED"
        Session("rs") = Session("cmd").Execute()
        clasCred = Session("rs").Fields("CLAVE").Value
        Session("Con").Close()
        Return clasCred
    End Function

    Private Sub ValidaPago()

        Dim res As String

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_EXP_VALIDO_PAGO"
        Session("rs") = Session("cmd").Execute()
        res = Session("rs").Fields("RESULTADO").Value.ToString
        Session("Con").Close()

        Select Case res
            Case "PAGAR"
                'If ValidaCapitalNeto() = 1 Or get_clas_cred() = "ARFIN" Then
                Response.Redirect("CRED_VEN_PAGO_CRED.aspx")
                'Else
                '    lbl_status.Text = "Error: La suma de saldos de los creditos de este afiliado mas el monto solicitado excede los limite de Capital Neto."
                'End If
            Case "EXPIRO"
                lbl_status.Text = "Error: El expediente ha expirado, configure uno nuevo."
            Case "REGENERAR"
                lbl_status.Text = "La fecha de pago no coincide con la de sistema, actualice el plan de pago."
        End Select

    End Sub

    Private Function ValidaCapitalNeto() As Integer

        Dim MontoCredito As Decimal
        Dim Monto_Creditos As Decimal
        Dim MontoCapNeto As Decimal

        MontoCapNeto = Rangosmonto()

        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("rs") = Session("cmd").Execute()

        MontoCredito = Session("rs").fields("MONTO").value.ToString

        Session("Con").Close()

        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MONTOS_ACUMULADOS_CREDITO"
        Session("rs") = Session("cmd").Execute()

        Monto_Creditos = Session("rs").fields("MONTO_CREDITOS").value.ToString

        Session("Con").Close()

        If (Monto_Creditos + MontoCredito) > MontoCapNeto Then
            Return 0
        Else
            Return 1
        End If

    End Function

    'Rango de los montos
    Private Function Rangosmonto() As Decimal

        Dim montocapneto As Decimal

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_RANGOS_MONTOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            montocapneto = Session("rs").Fields("MONTO_CAP_NETO").value.ToString
        End If
        Session("Con").Close()

        Return montocapneto

    End Function


End Class