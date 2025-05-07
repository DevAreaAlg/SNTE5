Public Class CRED_EXP_APARTADO2_PLAN_INSTITUCIONAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Plan de pagos", "Plan de Pagos Institucional")

        If Not Me.IsPostBack Then
            If Session("FOLIO") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If

            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Folio.Text = "Datos del Expediente: " + Session("CVEEXPE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("CLIENTE")
            Session("VENGODE") = "CRED_EXP_APARTADO2_PLAN_INSTITUCIONAL.aspx"

            PlazoQnas()
            TraeMontoPlazo() 'Muestra el monto, el plazo y unidad
            'muestrafechaliberacion() 'Muestra la fecha de liberación
            InsumosPlan() 'Llena los combos de tasas del plan especial
            tipoplan()
            Facultad()
            expbloqueado()
            OBTIENEPLANINSTITUCIONAL()
        End If

    End Sub

    Private Sub LIMPIAVARIABLES()

        Session("MONTO") = Nothing
        Session("FACULTAD") = Nothing
    End Sub

    Private Sub Facultad()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FACULTAD_PLANPAGO_PAGARES"
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("FACULTAD") = Session("rs").Fields("FACULTAD").value.ToString
        Session("Con").Close()
    End Sub

    '----------------------------VERIFICA SI EL EXPEDIENTE ESTÁ BLOQUEADO----------------------------------------
    Private Sub expbloqueado()

        Dim bloqueado As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            bloqueado = Session("rs").fields("BLOQUEADO").value.ToString()

            If bloqueado = "1" And Session("FACULTAD") = "0" Then 'Si está bloqueado el expediente se deshabilitan los botones
                lnk_generar.Enabled = False
            End If

        End If
        Session("Con").Close()
    End Sub

    Private Sub OBTIENEPLANINSTITUCIONAL()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 50, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_PLAN_INSTITUCIONAL"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof() Then
            Session("CLAVE") = Session("rs").fields("CLAVE").value.ToString

        End If

        Session("Con").close()
    End Sub

    Private Sub Monto_plazo()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MONTO_PLAZO_CREDITO"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("MONTO").Value.ToString = "" Then 'no los ha capturado por lo que no lo dejo seguir usando el apartado
            lbl_statusapartado.Text = "Error: el monto y plazo no han sido capturados."
            Session("Con").Close()


            Exit Sub
        Else 'ya capturo el monto y plazo en el apartado 1
            Session("MONTO") = Session("rs").Fields("MONTO").Value.ToString
            lbl_monto.Text = FormatCurrency(Session("MONTO"))
            Dim plazo As Integer = Convert.ToInt32(Session("rs").Fields("PLAZO").Value.ToString) / 15
            lbl_plazo.Text = plazo.ToString
            If Session("rs").Fields("UNIDAD").Value.ToString = "DIAS" Then
                lbl_lplazo.Text = "*Plazo (QUINCENAS):"
            Else
                lbl_lplazo.Text = "*Plazo (" + Session("rs").Fields("UNIDAD").Value.ToString + "):"
            End If
            Session("Con").Close()
        End If
    End Sub



    'Muestra el Monto y el Plazo que trae el producto desde el configurador de Productos
    Private Sub TraeMontoPlazo()

        Monto_plazo()
        'muestrafechaliberacion()


    End Sub

    'Datos de cualquier plan (MIN Y MAX DE LOS INDICES Y TASAS)
    Private Sub InsumosPlan()
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TASA_PERIODO_PESP"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            'MIN Y MAXIMOS DE TASA NORMAL Y TASA MORATORIO
            Session("TASAMINNORFIJ") = Session("rs").fields("TASAMINNORFIJ").value.ToString
            Session("TASAMAXNORFIJ") = Session("rs").fields("TASAMAXNORFIJ").value.ToString
            Session("TASAMINNORIND") = Session("rs").fields("TASAMINNORIND").value.ToString
            Session("TASAMAXNORIND") = Session("rs").fields("TASAMAXNORIND").value.ToString
            Session("INDNORIND") = Session("rs").fields("INDNORIND").value.ToString
            Session("VALORNORIND") = Session("rs").fields("VALORNORIND").value.ToString
            Session("TIPONORIND") = Session("rs").fields("TIPONORIND").value.ToString

            Session("TASAMINMORFIJ") = Session("rs").fields("TASAMINMORFIJ").value.ToString
            Session("TASAMAXMORFIJ") = Session("rs").fields("TASAMAXMORFIJ").value.ToString
            Session("TASAMINMORIND") = Session("rs").fields("TASAMINMORIND").value.ToString
            Session("TASAMAXMORIND") = Session("rs").fields("TASAMAXMORIND").value.ToString
            Session("INDMORIND") = Session("rs").fields("INDMORIND").value.ToString
            Session("VALORMORIND") = Session("rs").fields("VALORMORIND").value.ToString
            Session("TIPOMORIND") = Session("rs").fields("TIPOMORIND").value.ToString

            If Session("TASAMAXNORFIJ") <> 0.00 Then
                lbl_tasanorpfsi.Text = "*Tasa ordinaria: (Desde " + Session("TASAMINNORFIJ") + "% hasta " + Session("TASAMAXNORFIJ") + "%)"
                txt_tasanorpfsi.Text = Session("TASAMAXNORFIJ").ToString
            Else
                lbl_tasanorpfsi.Text = "*Tasa ordinaria: (Desde " + Session("TIPONORIND") + " +" + Session("TASAMINNORIND") + "% hasta " + Session("TIPONORIND") + " +" + Session("TASAMAXNORIND") + "%)"
                txt_tasanorpfsi.Text = Session("TASAMAXNORIND").ToString
            End If

            'If Session("TASAMAXMORFIJ") <> 0.00 Then
            '    lbl_tasamorpfsi.Text = "*Tasa moratoria: (Desde " + Session("TASAMINMORFIJ") + "% hasta " + Session("TASAMAXMORFIJ") + "%)"
            '    txt_tasamorpfsi.Text = Session("TASAMAXMORFIJ").ToString
            'Else
            '    lbl_tasamorpfsi.Text = "*Tasa moratoria: (Desde " + Session("TIPOMORIND") + " +" + Session("TASAMINMORIND") + "% hasta " + Session("TIPOMORIND") + " +" + Session("TASAMAXMORIND") + "%)"
            '    txt_tasamorpfsi.Text = Session("TASAMAXMORIND").ToString
            'End If

        End If
        Session("Con").Close()

    End Sub


    Private Function Validatasa(ByVal tasa As String) As Boolean
        Return Regex.IsMatch(tasa, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function


    '----------------CODIGO DE PLAN DE PAGOS FIJOS---------------------------------


    'Verifica que la tasa y el periodo esté en el límite establecido
    Public Function validacion(ByVal TasaNorPfsi As String, ByVal TasaMorPfsi As String) As Integer

        lbl_errortasanorpfsi.Text = ""
        'lbl_errortasamorpfsi.Text = ""
        Dim COUNT As Integer
        COUNT = 1

        If Session("TASAMAXNORFIJ") <> 0 Then
            If CDec(TasaNorPfsi) < CDec(Session("TASAMINNORFIJ").ToString) Or CDec(TasaNorPfsi) > CDec(Session("TASAMAXNORFIJ").ToString) Then
                lbl_errortasanorpfsi.Text = "Error: La tasa ordinaria no está en los límites establecidos "
                COUNT = 0
            End If
        Else
            If CDec(TasaNorPfsi) < CDec(Session("TASAMINNORIND").ToString) Or CDec(TasaNorPfsi) > CDec(Session("TASAMAXNORIND").ToString) Then
                lbl_errortasanorpfsi.Text = "Error: Los puntos % de la tasa ordinaria no están en los límites establecidos "
                COUNT = 0
            End If
        End If

        'If Session("TASAMAXMORFIJ") <> 0 Then
        '    If CDec(TasaMorPfsi) < CDec(Session("TASAMINMORFIJ").ToString) Or CDec(TasaMorPfsi) > CDec(Session("TASAMAXMORFIJ").ToString) Then
        '        lbl_errortasamorpfsi.Text = "Error: La tasa moratoria no está en los límites establecidos "
        '        COUNT = 0
        '    End If
        'Else
        '    If CDec(TasaMorPfsi) < CDec(Session("TASAMINMORIND").ToString) Or CDec(TasaMorPfsi) > CDec(Session("TASAMAXMORIND").ToString) Then
        '        lbl_errortasamorpfsi.Text = "Error: Los puntos % de la tasa moratoria no están en los límites establecidos"
        '        COUNT = 0
        '    End If
        'End If

        Return COUNT

    End Function

    Private Sub tipoplan()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_PLANPAGO"
        Session("rs") = Session("cmd").Execute()

        'SI EXISTE UN PLAN DE PAGOS SE HABILITA EL LINK PARA VER EL PLAN DE PAGOS
        If Session("rs").Fields("RESPUESTA").value.ToString = "EXISTE" Then
            lnk_verplanpago.Enabled = True

        End If
        Session("Con").Close()
    End Sub

    Protected Sub lnk_verplanpago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_verplanpago.Click
        Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
    End Sub



    Protected Sub lnk_generar_Click(sender As Object, e As EventArgs) Handles lnk_generar.Click


        Dim count As Integer
        lbl_status_pfsi.Text = ""
        Dim tasaNorpfsi As String
        Dim tasaMorpfsi As String

        tasaNorpfsi = txt_tasanorpfsi.Text
        'tasaMorpfsi = txt_tasamorpfsi.Text

        If Session("TASAMAXMORFIJ") <> 0.00 Then
            tasaMorpfsi = Session("TASAMAXMORFIJ").ToString
        Else
            tasaMorpfsi = Session("TASAMAXMORIND").ToString
        End If

        If validacion_fecha_pago() = "ACTUALIZAR" Then

            If Validatasa(tasaNorpfsi) = True Then


                count = validacion(tasaNorpfsi, tasaMorpfsi) 'Primero Verifica que la tasa y la periodicidad este en el margen establecido

                If count = 1 Then 'And validacionfecha() = True Then

                    GuardaPIns()
                    Response.Redirect("CRED_EXP_PLAN_GENERAL.aspx")
                    lnk_verplanpago.Enabled = True

                End If
            Else
                lbl_status_pfsi.Text = "Error: Verifique la tasa"
            End If
        Else
            lbl_status_pfsi.Text = "Error: Fecha anterior a la fecha de sistema o excede la fecha de expiración del expediente"
        End If

    End Sub


    Private Sub GuardaPIns()
        Dim tasaMorpfsi As String

        If Session("TASAMAXMORFIJ") <> 0.00 Then
            tasaMorpfsi = Session("TASAMAXMORFIJ").ToString
        Else
            tasaMorpfsi = Session("TASAMAXMORIND").ToString
        End If
        'lbl_status_pfsi.Text = " FOLIO: " + Session("FOLIO").ToString + " TASANORMA: " + txt_tasanorpfsi.Text + " TASAMORA: " + txt_tasamorpfsi.Text + " PLAN: PFSI USERID: " + Session("USERID").ToString + " SESION: " + Session("SESION").ToString + " SUCID: " + Session("SUCID").ToString
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PLAN_INSTITUCIONAL_QNA"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASA", Session("adVarChar"), Session("adParamInput"), 20, Left(CStr(CDec(txt_tasanorpfsi.Text) + CDec(Session("VALORNORIND"))), InStr(CStr(CDec(txt_tasanorpfsi.Text) + CDec(Session("VALORIND"))), ".") + 2))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TASAMOR", Session("adVarChar"), Session("adParamInput"), 20, CDec(tasaMorpfsi))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOCOBRO", Session("adVarChar"), Session("adParamInput"), 5, "PFSI")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 50, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Function validacion_fecha_pago() As String


        Dim resultado As String = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFEXP_FECHA_LIBERACION"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            resultado = Session("rs").Fields("RESULTADO").value.ToString()
        End If
        Session("Con").Close()

        Return resultado

    End Function

    Private Sub PlazoQnas()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SUCID", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_PLAZO_QUINCENAS"
        Session("rs") = Session("cmd").Execute()
        If Session("rs").Fields("RESULTADO").value.ToString = "SI" Then
            lnk_generar.Enabled = True

        ElseIf Session("rs").Fields("RESULTADO").value.ToString = "NO" Then
            lnk_generar.Enabled = False
            lbl_statusapartado.Text = "Error: debe volver a capturar los datos del Apartado 1."

        End If
        Session("Con").Close()
    End Sub

End Class