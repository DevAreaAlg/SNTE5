Public Class CRED_PROD_APARTADO5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            TryCast(Me.Master, MasterMascore).CargaASPX("Parámetros de intereses, cartera y comité", "PARÁMETROS DE INTERESES/CARTERA/COMITÉ")

            If Session("PROD_NOMBRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                'LLENO LOS RESPECTIVOS LABELS
                Session("flag") = "0"
                datosgenerales()
                lbl_Producto.Text = Session("PROD_NOMBRE").ToString
            End If
        End If
    End Sub

    'Muestra los datos 
    Private Sub datosgenerales()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_DIAS_VENCIDA_PRODUCTO"

        Session("rs") = Session("cmd").Execute()

        If Session("rs").Fields("RESPUESTA").Value.ToString = "1" Then

            txt_diasven.Text = Session("rs").Fields("VENCIDA").Value.ToString
            txt_pagos.Text = Session("rs").Fields("PAGO").Value.ToString
            txt_monto.Text = Session("rs").Fields("COMITE").Value.ToString
            txt_normal.Text = Session("rs").Fields("NORMAL").Value.ToString
            txt_mora.Text = Session("rs").Fields("MORATORIO").Value.ToString
            cmb_periodo.Text = Session("rs").Fields("PERIODOS").Value.ToString
            txt_garantia.Text = Session("rs").Fields("SEGURO").value.ToString
            txt_ingreso.Text = Session("rs").fields("RAZON").value.ToString
            txt_director.Text = Session("rs").fields("DIRECTOR").value.ToString
            Session("flag") = Session("rs").Fields("RESPUESTA").Value.ToString


            If Session("rs").Fields("COBROMORA").Value.ToString = Nothing Then
                rad_abono.Checked = True

            Else
                If Session("rs").Fields("COBROMORA").Value.ToString = "ABONOV" Then
                    rad_abono.Checked = True
                Else
                    rad_capital.Checked = True

                End If
            End If



        End If
        Session("Con").Close()

    End Sub


    'Validacion de montos del apartado 1 y 5
    Private Sub Validacionmontos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("IDPROD", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFPCR_VALIDACION_MONTOS"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").eof Then
            Session("MIN") = Session("rs").Fields("MINIMO").Value.ToString
            Session("MAX") = Session("rs").Fields("MAXIMO").Value.ToString
            Session("RESPUESTA") = Session("rs").Fields("RESPUESTA").Value.ToString
        End If


        Session("Con").Close()


    End Sub


    'Botón Guardar 
    Protected Sub btn_guardar_Click(sender As Object, e As System.EventArgs)

        'VALIDACION QUE TENGA CAPTURADO LOS RANGOS DE MONTOS DEL APARTADO 1
        Validacionmontos()

        If Session("RESPUESTA") = "NOEXISTE" Then

            lbl_status.Text = "Error: Guarde primero los valores del apartado 1"
            limpiadatos()
            Exit Sub
        End If

        'si se guardaron los limites de monto en apartado 1
        If Session("flag").ToString = "1" And Session("RESPUESTA") = "EXISTE" Then
            'el monto minimo de direccion de credito y de comite son 0 (se desactivan las dos capas)
            If CInt(txt_director.Text) = 0 And CInt(txt_monto.Text) = 0 Then
                'la razon ingreso-abono es mayor a 0
                If CInt(txt_ingreso.Text) > 0 Then
                    GuardaDatos()
                    lbl_status.Text = "Guardado correctamente"
                Else
                    lbl_status.Text = "Error: Razón Ingreso-Abono debe ser mayor a 0"
                End If
            Else
                'el monto minimo de direccion de credito y de comite se van a habilitar (mayores a 0)
                If CInt(txt_director.Text) <> 0 And CInt(txt_monto.Text) <> 0 Then
                    'ambos montos minimos deben estar dentro de los limites inf y sup del apartado 1
                    If Session("MIN") <= CInt(txt_director.Text) And CInt(txt_director.Text) < Session("MAX") And Session("MIN") <= CInt(txt_monto.Text) And CInt(txt_monto.Text) < Session("MAX") Then
                        'el monto minimo del director debe ser menor al del comite
                        If CInt(txt_monto.Text) > CInt(txt_director.Text) Then
                            'la razon ingreso-abono debe ser mayor a 0
                            If CInt(txt_ingreso.Text) > 0 Then
                                GuardaDatos()
                                lbl_status.Text = "Guardado correctamente"
                            Else
                                lbl_status.Text = "Error: Razón ingreso-abono debe ser mayor a 0"
                            End If

                        Else
                            lbl_status.Text = "Error: El monto de evaluación por director de préstamo debe ser menor al monto de evaluación de comité"
                        End If
                    Else
                        lbl_status.Text = "Error: Los montos deben estar dentro de los límites establecidos en el apartado 1"
                    End If

                Else
                    'uno de los dos montos minimos esta desactivado (es igual a 0) pero el otro no
                    If (CInt(txt_director.Text) = 0 And (Session("MIN") <= CInt(txt_monto.Text) And CInt(txt_monto.Text) < Session("MAX"))) Or (CInt(txt_monto.Text) = 0 And Session("MIN") <= CInt(txt_director.Text) And CInt(txt_director.Text) < Session("MAX")) Then
                        'la razon ingreso-abono debe ser mayor a 0
                        If CInt(txt_ingreso.Text) > 0 Then
                            GuardaDatos()
                            lbl_status.Text = "Guardado correctamente"
                        Else
                            lbl_status.Text = "Error: Razón Ingreso-Abono debe ser mayor a 0"
                        End If
                    Else
                        lbl_status.Text = "Error: Los montos deben estar dentro de los límites establecidos en el apartado 1"
                    End If
                End If

            End If

        End If
    End Sub

    Private Sub GuardaDatos()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPRODUCTO", Session("adVarChar"), Session("adParamInput"), 10, Session("PRODID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASVENCIDAS", Session("adVarChar"), Session("adParamInput"), 10, txt_diasven.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        If rad_abono.Checked = True Then 'seleccionaron abono 
            Session("parm") = Session("cmd").CreateParameter("COBRO", Session("adVarChar"), Session("adParamInput"), 10, "ABONOV")
            Session("cmd").Parameters.Append(Session("parm"))
        Else 'seleccionaron capital
            Session("parm") = Session("cmd").CreateParameter("COBRO", Session("adVarChar"), Session("adParamInput"), 10, "CAPITAL")
            Session("cmd").Parameters.Append(Session("parm"))
        End If

        Session("parm") = Session("cmd").CreateParameter("PAGOS", Session("adVarChar"), Session("adParamInput"), 10, txt_pagos.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTO", Session("adVarChar"), Session("adParamInput"), 10, txt_monto.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASNORMAL", Session("adVarChar"), Session("adParamInput"), 10, txt_normal.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("DIASMORATORIO", Session("adVarChar"), Session("adParamInput"), 10, txt_mora.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERIODOGRAINT", Session("adVarChar"), Session("adParamInput"), 10, cmb_periodo.SelectedItem.Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTOSEGURO", Session("adVarChar"), Session("adParamInput"), 10, txt_garantia.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZONINGRESO", Session("adVarChar"), Session("adParamInput"), 10, txt_ingreso.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MONTODIRECTOR", Session("adVarChar"), Session("adParamInput"), 20, txt_director.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFPCR_INTERESES_CARTERA_COMITE"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub

    Private Sub limpiadatos()
        txt_diasven.Text = ""
        txt_director.Text = ""
        txt_garantia.Text = ""
        txt_ingreso.Text = ""
        txt_monto.Text = ""
        txt_mora.Text = ""
        txt_normal.Text = ""
        txt_pagos.Text = ""
        txt_director.Text = ""
        cmb_periodo.SelectedIndex = "0"
    End Sub


End Class