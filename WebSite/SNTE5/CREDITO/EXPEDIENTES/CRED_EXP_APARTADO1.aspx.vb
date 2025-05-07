Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_APARTADO1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Datos Financieros", "Datos Financieros")
        If Not Me.IsPostBack Then
            If Session("FOLIO") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If
            'ASIGNO LOS NOMBRES DE LOS LBL
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))
            Session("VENGODE") = "CRED_EXP_APARTADO1.aspx"
            'LLENO LOS DATOS DE LOS COMBOS
            CreaTablas()
            Llenafondeo()
            FinalidadCredito()
            datos()

            Rangosmonto()
            RangosPlazo()
            expbloqueado()
            RevisaReestructura()
            Fondeosasignados()
            RevisaDiasGraciaInt()
            LlenaComisiones()

        End If

    End Sub

#Region "CONFIGURACION GENERAL"

    Private Sub datos()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_APARTADO1"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").Value.ToString

            If Session("RESPUESTA") = "1" Then
                'Muestran los valores en los respectivos txtbox y cmbs.
                If Session("rs").Fields("RENOVACION").Value.ToString > "0" Then
                    txt_monto.Enabled = False
                Else
                    txt_monto.Enabled = True
                End If

                Dim plazo As Integer = Convert.ToInt32(Session("rs").Fields("PLAZO").value.ToString) / 15
                txt_plazo.Text = plazo.ToString
                txt_monto.Text = Session("rs").Fields("MONTO").Value.ToString
                txt_diasgracia.Text = Session("rs").Fields("NORMAL").Value.ToString
                txt_diasgraciamora.Text = Session("rs").Fields("MORATORIO").Value.ToString
                cmb_objetivo.ClearSelection()
                cmb_objetivo.Items.FindByText(Session("rs").Fields("OBJETIVO").Value.ToString).Selected = True
                txt_notas.Text = Session("rs").Fields("NOTAS").Value.ToString
                'txt_gasto_notarial.Text = Session("rs").Fields("GASTONOTARIAL").Value.ToString
            End If
        End If
        Session("Con").Close()
    End Sub

    'Finalidad Credito
    Private Sub FinalidadCredito()

        cmb_objetivo.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_objetivo.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDFOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FINALIDAD"
        Session("rs") = Session("cmd").Execute()

        Dim TipoProd As String = String.Empty
        Dim Contador As Integer = 1

        Do While Not Session("rs").EOF

            If Contador = 1 Then
                TipoProd = Session("rs").Fields("PROD").Value.ToString
                Contador += 1
            End If

            Dim item As New ListItem(Session("rs").Fields("OBJETIVO").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_objetivo.Items.Add(item)
            Session("rs").movenext()

        Loop

        Session("Con").Close()

        If TipoProd = "CH" Then 'ID DE PRODUCTO HIPOTECARIO
            rfv_objetivo.Enabled = True
            ' txt_gasto_notarial.Enabled = True
            'rfv_gasto_notarial.Enabled = True
        End If

    End Sub

    Private Function ValidaGastoNotarial(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]{1,6}(\.[0-9]{1,2})?$"))
    End Function

    'Rango de los montos
    Private Sub Rangosmonto()
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
        Session("cmd").CommandText = "SEL_CNFEXP_RANGOS_MONTOS"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            lbl_rango.Text = "(Desde: " + FormatCurrency(Session("rs").Fields("MONTO_MIN").value.ToString) + " hasta: " + FormatCurrency(Session("rs").Fields("MONTO_MAX").value.ToString) + ")"
            Session("MONTOMIN") = Session("rs").Fields("MONTO_MIN").value.ToString
            Session("MONTOMAX") = Session("rs").Fields("MONTO_MAX").value.ToString
        End If
        Session("Con").Close()
    End Sub

    Private Sub RevisaReestructura()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_MONTOMIN_REEST"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then

            If Session("rs").Fields("ORIGEN").value.ToString <> "0" Then
                Session("MIN_REEST") = Session("rs").Fields("MIN_REEST").value.ToString
                lbl_MinReest.Text = "Monto mínimo de reestructura: " + FormatCurrency(Session("MIN_REEST"))
                lbl_NotaReest.Text = "(Préstamo origen: " + FormatCurrency(Session("rs").Fields("SALDO_ORIGEN").value.ToString) + " - Cuenta eje: " + FormatCurrency(Session("rs").Fields("SALDO_EJE").value.ToString) + " - Descuento pendiente: " + FormatCurrency(Session("rs").Fields("DESCUENTO").value.ToString) + ")"
                lbl_MinReest.Visible = True
                lbl_NotaReest.Visible = True
            Else
                Session("MIN_REEST") = 0.0
                lbl_MinReest.Text = ""
                lbl_NotaReest.Text = ""
                lbl_MinReest.Visible = False
                lbl_NotaReest.Visible = False
            End If

        End If

        Session("Con").Close()

    End Sub

    'Rango de los montos
    Private Sub RangosPlazo()
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
        Session("cmd").CommandText = "SEL_CNFEXP_APARTADO1_RANGOS_PLAZO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then
            Dim plazomin As Integer = Convert.ToInt32(Session("rs").Fields("PLAZO_MIN").value.ToString) / 15
            Dim plazomax As Integer = Convert.ToInt32(Session("rs").Fields("PLAZO_MAX").value.ToString) / 15
            lbl_rango_plazo.Text = "(Desde: " + plazomin.ToString + " hasta: " + plazomax.ToString + " Quincenas)"
            ' txt_plazo.Text = plazomax.ToString
        End If
        Session("Con").Close()
    End Sub

    Private Function Validamonto(ByVal monto As String) As Boolean
        Return Regex.IsMatch(monto, ("^[0-9]+(\.[0-9]{1}[0-9]?)?$"))
    End Function

    Private Function validarenovacion() As Boolean
        Dim plazo As Integer = Convert.ToInt32(txt_plazo.Text) * 15
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 15, plazo.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_RENOVACION_PLAZO"
        Session("rs") = Session("cmd").Execute()
        Dim RESULTADO As Boolean
        If Session("rs").Fields("RESPUESTA").value.ToString = "1" Then
            RESULTADO = True
        Else
            RESULTADO = False
        End If
        Session("Con").Close()
        Return RESULTADO
    End Function

    Private Function validacapacidadpago() As Boolean
        'lbl_status.Text = " IDSUC: " + Session("SUCID").ToString + " FOLIO: " + Session("FOLIO").ToString + " MONTO: " + txt_monto.Text + " PLAZO: " + txt_plazo.Text

        Dim plazo As Integer = Convert.ToInt32(txt_plazo.Text) * 15

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 15, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO_SOLICITAR", Session("adVarChar"), Session("adParamInput"), 20, txt_monto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 15, plazo.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CAPACIDAD_PAGO"
        Session("rs") = Session("cmd").Execute()
        Dim RESULTADO As Boolean
        If Session("rs").Fields("RESPUESTA").value.ToString = "SI" Then
            RESULTADO = True
        Else
            RESULTADO = False
        End If
        Session("Con").Close()
        Return RESULTADO
    End Function

    'Botón Guardar (Guarda Monto,Divisa,Destino,Plazo)
    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs)
        If Validamonto(txt_monto.Text) = True Then
            If CDec(txt_monto.Text) > 0 And CInt(txt_plazo.Text) > 0 Then

                If validaplazo(txt_plazo.Text, "DIAS", CDec(txt_monto.Text)) = True Then
                    If validacapacidadpago() = True Then
                        If CDec(txt_monto.Text) >= CDec(Session("MONTOMIN").ToString) And CDec(txt_monto.Text) <= CDec(Session("MONTOMAX").ToString) Then
                            If CDec(txt_monto.Text) >= CDec(Session("MIN_REEST").ToString) Then

                                If txt_notas.Text.Length > 2000 Then
                                    lbl_status.Text = "Error: Sólo 2000 caracteres o menos en los comentarios adicionales"
                                Else

                                    If validarenovacion() = True Then
                                        ' If txt_gasto_notarial.Text = "" Then
                                        guardadatos()
                                        fuenteFondeo()
                                        ' Else
                                        'If ValidaGastoNotarial(txt_gasto_notarial.Text) Then
                                        'guardadatos()
                                        'fuenteFondeo()
                                        'lbl_gasgasto_notarial.Text = "Gasto notarial: Monto a entregar sería " + (CDec(txt_monto.Text) - CDec(txt_gasto_notarial.Text)).ToString("C")
                                        'Else
                                        '   lbl_status.Text = "Error: Monto del gasto notarial es incorrecto"
                                        'End If
                                        'End If
                                    Else
                                        lbl_status.Text = "Error: El plazo máximo que se puede ampliar la renovación son 12 meses después de la fecha de vencimiento original sin exederse del plazo máximo"
                                    End If

                                End If
                            Else
                                lbl_status.Text = "Error: El monto debe ser mayor o igual al monto mínimo de reestructura"
                            End If
                        Else
                            lbl_status.Text = "Error: Monto fuera de los límites establecidos"
                        End If
                    Else

                        'Exceso de pago
                        pnl_Modal_Confirmar.Visible = True
                        modal_Confirmar.Show()
                        lbl_alerta.Text = "Alerta: El agremiado no cuenta con capacidad de pago para el monto elegido."
                        lbl_mensaje.Text = "Si desea mandan una solicitud a un Administrador para su validación y aprobación " & "<br/>" &
                                           " de click en Aceptar en caso contrario de click en Cancelar para regresar."


                    End If
                Else
                    lbl_status.Text = "Error: Plazo fuera de los límites establecidos en el producto"
                End If
            Else
                lbl_status.Text = "Error: El monto y el plazo no pueden guardarse en 0"
            End If
        Else
                lbl_status.Text = "Error: Monto incorrecto"
            End If

    End Sub

    Protected Sub btn_confirmar_GeneraExp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_confirmar.Click

        CambioEstatus()



    End Sub

    Private Sub CambioEstatus()


        Try
            Dim plazo As Integer = Convert.ToInt32(txt_plazo.Text) * 15


            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPLAZO", Session("adVarChar"), Session("adParamInput"), 10, plazo.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, txt_notas.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("OBJETIVO", Session("adVarChar"), Session("adParamInput"), 200, cmb_objetivo.SelectedItem.Text)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "UPD_ESTATUS_AUTO_EXP"
            Session("rs") = Session("cmd").Execute()

            lbl_status.Text = "Se envió solicitud a un Administrador"

        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally
            Session("Con").Close()
        End Try

    End Sub

    Private Sub guardadatos()

        Try
            Dim plazo As Integer = Convert.ToInt32(txt_plazo.Text) * 15

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 15, txt_monto.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDPLAZO", Session("adVarChar"), Session("adParamInput"), 10, plazo.ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 10, "DIAS")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("OBJETIVO", Session("adVarChar"), Session("adParamInput"), 200, cmb_objetivo.SelectedItem.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, txt_notas.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("GASTONOTARIAL", Session("adVarChar"), Session("adParamInput"), 10, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "UPD_CNFEXP_APARTADO1"
            Session("cmd").Execute()
            lbl_status.Text = "Guardado correctamente"
        Catch ex As Exception
            lbl_status.Text = ex.ToString
        Finally
            Session("Con").Close()
            datos()
        End Try
    End Sub

    Private Function validaplazo(ByVal PLAZO As String, ByVal UNIDAD As String, ByVal MONTO As Decimal) As Boolean

        Dim RESPUESTA As Boolean
        RESPUESTA = False
        Dim plazoValidar As Integer = Convert.ToInt32(PLAZO) * 15

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PLAZO", Session("adVarChar"), Session("adParamInput"), 20, plazoValidar.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("UNIDAD", Session("adVarChar"), Session("adParamInput"), 20, UNIDAD)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 20, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 20, MONTO)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CCRED_PLAZO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            If Session("rs").fields("RESPUESTA").value.ToString = "1" Then
                RESPUESTA = True
            Else
                RESPUESTA = False
            End If

        End If
        Session("Con").Close()

        Return RESPUESTA

    End Function

    Private Sub CreaTablas()

        Dim tabla As New DataTable

        tabla.Columns.Add("MONTO", GetType(Decimal))
        tabla.Columns.Add("PLAZO", GetType(Integer))
        tabla.Columns.Add("NUMPAGO", GetType(Integer))
        tabla.Columns.Add("PAGOMENSUAL", GetType(Decimal))

        Session("tabla") = tabla

    End Sub


#End Region

#Region "FUENTES FONDEO"

    'Muestra las fuentes de fondeo 
    Private Sub Llenafondeo()

        cmb_fondeo.Items.Clear()

        Dim elija As New ListItem("ELIJA", "-1")
        cmb_fondeo.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FONDEO_FOLIO"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)

            cmb_fondeo.Items.Add(item)

            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    'Muestra los Fondeos asignados
    Private Sub Fondeosasignados()

        If Session("FOLIO").ToString = "" Then
            Exit Sub
        End If

        lst_fond.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_CNFEXP_FONDEO_ASIGNADA"
        Session("rs") = Session("cmd").Execute()
        'PROCESAMOS LO QUE REGRESO SP

        Do While Not Session("rs").eof()

            Dim ELEMENTO As New ListItem(Session("rs").Fields("CATFONDEO_NOMBRE").Value.ToString + "(" + Session("rs").Fields("RELEXPFOND_PORCENTAJE").Value.ToString + "%)", Session("rs").Fields("RELEXPFOND_ID_FONDEO").Value.ToString + "#" + Session("rs").Fields("RELEXPFOND_PORCENTAJE").Value.ToString)
            lst_fond.Items.Add(ELEMENTO)
            Session("rs").MOVENEXT()
        Loop
        Session("Con").Close()
        Llenafondeo()
    End Sub

    Protected Sub fuenteFondeo()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FONDEO", Session("adVarChar"), Session("adParamInput"), 10, 1)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PORCENTAJE", Session("adVarChar"), Session("adParamInput"), 10, "100")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ASIGNAR_FONDEO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            If Session("rs").Fields("ALERTA").Value.ToString = "SI" Then
                lbl_statusfondeo.Text = "Guardado correctamente"
            Else
                lbl_statusfondeo.Text = "Error: No puede exceder el 100%."
            End If

            'MANDO A CARGAR LAS FUENTES DE FONDEO ASIGNADAS Y DISPONIBLES
            Session("Con").Close()
        End If
        Fondeosasignados()
        Fondeosdisponibles()
        txt_porcentaje.Text = ""
    End Sub

    'Botón asignar (Asignar una fuente de fondeo)
    Protected Sub btn_asignar_Click(sender As Object, e As System.EventArgs)
        lbl_statusfondeo.Text = ""

        Dim Percent As Integer
        Percent = 0

        For Counter = 0 To lst_fond.Items.Count - 1
            If Not CInt(Split(lst_fond.Items(Counter).Value.ToString(), "#")(0)) = 0 Then
                Percent += CInt(Split(lst_fond.Items(Counter).Value.ToString(), "#")(1))
            End If
        Next

        If CInt(txt_porcentaje.Text) + Percent > 100 Then
            lbl_statusfondeo.Text = "Error: No puede exceder el 100%."
            Exit Sub
        Else
            If cmb_fondeo.Items.Count = 1 Then
                lbl_statusfondeo.Text = "Error: Ya asignó todas las fuentes de fondeo sin completar el 100%"
                Exit Sub
            End If

        End If
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FONDEO", Session("adVarChar"), Session("adParamInput"), 10, cmb_fondeo.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PORCENTAJE", Session("adVarChar"), Session("adParamInput"), 10, txt_porcentaje.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_CNFEXP_ASIGNAR_FONDEO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then

            If Session("rs").Fields("ALERTA").Value.ToString = "SI" Then
                lbl_statusfondeo.Text = "Guardado correctamente"
            Else
                lbl_statusfondeo.Text = "Error: No puede exceder el 100%."
            End If

            'MANDO A CARGAR LAS FUENTES DE FONDEO ASIGNADAS Y DISPONIBLES
            Session("Con").Close()
        End If
        Fondeosasignados()
        Fondeosdisponibles()
        txt_porcentaje.Text = ""
    End Sub

    'Muestra los Fondeos disponibles
    Private Sub Fondeosdisponibles()

        If Session("FOLIO").ToString = "" Then
            Exit Sub
        End If
        cmb_fondeo.Items.Clear()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_FONDEO_DISPONIBLE"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()
        Llenafondeo()
    End Sub

    'Botón remover (Elimina una fuente de fondeo)
    Protected Sub btn_remover_Click(sender As Object, e As System.EventArgs)
        lbl_statusfondeo.Text = ""
        'Si no selecciona una fuente de fondeo al quererla elimnar marca error
        If lst_fond.SelectedItem Is Nothing Then
            lbl_statusfondeo.Text = "Error: Seleccione una fuente de fondeo."
            Exit Sub
        Else

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("FONDEO", Session("adVarChar"), Session("adParamInput"), 10, Split(lst_fond.SelectedItem.Value.ToString(), "#")(0))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "DEL_CNFEXP_ELIMINAR_FONDEO"
            Session("rs") = Session("cmd").Execute()
            Session("Con").Close()
            Fondeosasignados()
            Fondeosdisponibles()
            lbl_statusfondeo.Text = "Se ha eliminado correctamente la fuente de fondeo"
        End If
    End Sub

#End Region

    Private Sub expbloqueado()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_EXP_BLOQUEADO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("BLOQUEADO") = Session("rs").fields("BLOQUEADO").value.ToString()

            If Session("BLOQUEADO") = "1" Then 'Si está bloqueado el expediente se deshabilitan los botones
                btn_guardar.Enabled = False
                btn_asignar.Enabled = False
                btn_remover.Enabled = False
                btn_guardardiasdegracia.Enabled = False
            End If
        End If
        Session("Con").Close()
    End Sub

#Region "DIAS DE GRACIA INTERESES"

    Private Sub RevisaDiasGraciaInt()

        Dim dgnor As String = ""
        Dim dgmor As String = ""

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DIAS_GRACIA_INTERES"

        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            dgnor = Session("rs").fields("DGRACIANOR").value.ToString()
            dgmor = Session("rs").fields("DGRACIAMOR").value.ToString()
        End If
        Session("Con").Close()

        If dgnor = "0" And dgmor = "0" Then
            btn_guardardiasdegracia.Enabled = False
        Else
            btn_guardardiasdegracia.Enabled = True
        End If

        If dgnor <> "0" Then
            Session("DGRACIANOR") = dgnor
            lbl_DiasGracia.Text = lbl_DiasGracia.Text + " (" + Session("DGRACIANOR").ToString + " max):"
            RevisaFacultadDiasCondoacionInt()
        Else
            txt_diasgracia.Text = "0"
            txt_diasgracia.Enabled = "false"
        End If

        If dgmor <> "0" Then
            Session("DGRACIAMOR") = dgmor
            lbl_DiasGraciaMora.Text = lbl_DiasGraciaMora.Text + " (" + Session("DGRACIAMOR").ToString + " max):"
            RevisaFacultadDiasCondoacionIntMora()
        Else
            txt_diasgraciamora.Text = "0"
            txt_diasgraciamora.Enabled = False
        End If
    End Sub

    'Facultad de condonar intereses normales
    Private Sub RevisaFacultadDiasCondoacionInt()
        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_FACULTAD_DIAS_CONDONACION_INT"
        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("PERMISO").value.ToString = "NO" Then
            txt_diasgracia.Enabled = False
            Session("DGRACIANOR") = Nothing
        Else
            If Session("BLOQUEADO") = "0" Then
                txt_diasgracia.Enabled = True
                btn_guardardiasdegracia.Enabled = True
                RequiredFieldValidator_diasgracia.Enabled = True
            End If
        End If
        Session("Con").Close()
    End Sub

    'Facultad de condonar intereses moratorios
    Private Sub RevisaFacultadDiasCondoacionIntMora()
        'Bandera para mostrar si el exepdiente esta en uso
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_FACULTAD_DIAS_CONDONACION_INT_MORA"

        Session("rs") = Session("cmd").Execute()

        If Session("rs").fields("PERMISO").value.ToString = "NO" Then

            txt_diasgraciamora.Enabled = False
            Session("DGRACIAMOR") = Nothing
        Else

            If Session("BLOQUEADO") = "0" Then
                txt_diasgraciamora.Enabled = True
                btn_guardardiasdegracia.Enabled = True
                RequiredFieldValidator_diasgraciamora.Enabled = True
            End If
        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_guardardiasdegracia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardardiasdegracia.Click
        'Permite guardar los valores de intereses
        GuardarDiasGracia()
    End Sub

    Private Sub GuardarDiasGracia()
        'REVISO QUE LOS DIAS DE GRACIA CAPTURADOS NO REBASEN LO CONFIGURADO EN EL PRODUCTO
        If Not Session("DGRACIANOR") Is Nothing Then
            If CInt(Session("DGRACIANOR")) < CInt(txt_diasgracia.Text) Then
                lbl_statusgracia.Text = "Error: Parámetro fuera de límite"
                Exit Sub
            End If
        End If
        If Not Session("DGRACIAMOR") Is Nothing Then
            If CInt(Session("DGRACIAMOR")) < CInt(txt_diasgraciamora.Text) Then
                lbl_statusgracia.Text = "Error: Parámetro fuera de límite"
                Exit Sub
            End If
        End If

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "UPD_CNFEXP_APARTADO1_DIASGRACIA"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INTNOR", Session("adVarChar"), Session("adParamInput"), 10, txt_diasgracia.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("INTMOR", Session("adVarChar"), Session("adParamInput"), 10, txt_diasgraciamora.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 50, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").Execute()
        Session("Con").Close()
        lbl_statusgracia.Text = "Guardado correctamente"

    End Sub

#End Region

    Private Sub LlenaComisiones()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim ModulosGeneral As New Data.DataTable()

        Dim dtFuentesAsignados As New Data.DataTable()
        dtFuentesAsignados.Columns.Add("ID", GetType(Integer))
        dtFuentesAsignados.Columns.Add("NOMBRE", GetType(String))
        dtFuentesAsignados.Columns.Add("ASIGNADO", GetType(Integer))

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 50, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_COMISIONES_ASIGNADAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(ModulosGeneral, Session("rs"))
        Session("Con").Close()

        If ModulosGeneral.Rows.Count > 0 Then
            dag_comisiones.Visible = True
            dag_comisiones.DataSource = ModulosGeneral
            dag_comisiones.DataBind()

        Else
            'dag_comisiones.Visible = False
            panel_comisiones.Visible = False
        End If
    End Sub

    Protected Sub btn_guardar_c_Click()

    End Sub

End Class