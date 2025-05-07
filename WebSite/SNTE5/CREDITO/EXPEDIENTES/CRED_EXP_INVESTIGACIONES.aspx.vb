Public Class CRED_EXP_INVESTIGACIONES
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Ingresos-Gastos", "Análisis Ingresos-Gastos")

        If Not Me.IsPostBack Then
            Dim menuPanel As Panel
            menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

            'If Not menuPanel Is Nothing Then
            '    menuPanel.Visible = False
            'End If

            'Datos Generales de Expediente: Folio, Nombre de Cliente y producto

            lbl_Folio.Text = "Detalle de Expediente: " + Session("FOLIO")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Prospecto.Text = Session("PROSPECTO")

        End If

        'LLENO GASTOS CORRESPONDIENTES
        Datos()
        LlenaGastos() 'Se generan dinámicamente los textbox
        DatosGastos() 'Muestra los datos de gastos del estudio socioeconomico
        'Datosinvestigacion() 'Muestra los datos de gastos de la investigacion de campo
        CalculaDiferencia() ' Calcula y suma la diferencia entre la investigacion y el estudio socioeconomico
    End Sub

    'Genero los gastos dinámicamente 
    Private Sub LlenaGastos()

        Session("Con").Open()
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_GASTOS_SE"
        Session("rs") = Session("cmd").Execute()

        Dim i As Integer
        Dim gastoses(100) As Label
        Dim gastosic(100) As Label
        Dim montoses(100) As Label
        Dim montosic(100) As Label
        Dim diferenciagastos(100) As Label
        Dim cr(100) As Literal
        Dim cric(100) As Literal
        Dim crgastos(100) As Literal

        i = 0

        Do While Not Session("rs").EOF

            'Declaro los arreglos del Estudio socioeconomico
            gastoses(i) = New Label
            montoses(i) = New Label
            cr(i) = New Literal
            'Declaro el arreglo de la investigacion de campo
            montosic(i) = New Label
            gastosic(i) = New Label
            cric(i) = New Literal

            diferenciagastos(i) = New Label
            crgastos(i) = New Literal
            '---------ARREGLO DE INVESTIGACION DE CAMPO------------

            gastosic(i).Text = Session("rs").Fields("CATCONCEPGTOS_GASTO").Value.ToString
            gastosic(i).CssClass = "texto"
            cric(i).Text = "<br />"
            gastosic(i).Width = 160
            montosic(i).Width = 50
            montosic(i).CssClass = "textocajas"
            montosic(i).ID = "I" + Session("rs").Fields("CATCONCEPGTOS_ID_GASTO").Value.ToString
            montosic(i).Text = FormatCurrency(0)
            pnl_investigacion.Controls.Add(gastosic(i))
            pnl_investigacion.Controls.Add(montosic(i))
            pnl_investigacion.Controls.Add(cric(i))


            '---------ARREGLO DE ESTUDIO SOCIOECONOMICO------------

            gastoses(i).Text = Session("rs").Fields("CATCONCEPGTOS_GASTO").Value.ToString
            gastoses(i).CssClass = "texto"
            cr(i).Text = "<br />"
            gastoses(i).Width = 150
            montoses(i).Width = 70
            montoses(i).CssClass = "textocajas"
            montoses(i).ID = "E" + Session("rs").Fields("CATCONCEPGTOS_ID_GASTO").Value.ToString
            montoses(i).Text = FormatCurrency(0)
            pnl_socioeconomico.Controls.Add(gastoses(i))
            pnl_socioeconomico.Controls.Add(montoses(i))
            pnl_socioeconomico.Controls.Add(cr(i))

            '---------------DIBUJA LOS LBLS DE DIFERENCIA-----------------------------------
            diferenciagastos(i).CssClass = "textocajas"
            crgastos(i).Text = "<br />"
            diferenciagastos(i).Width = 100
            diferenciagastos(i).Height = 17
            diferenciagastos(i).Text = FormatCurrency(0)
            pnl_diferencia.Controls.Add(diferenciagastos(i))
            pnl_diferencia.Controls.Add(crgastos(i))
            Session("rs").movenext()
            i = i + 1

        Loop


        Session("Con").Close()
        'Se igualan a variables de sesion los arreglos
        Session("montosic") = montosic
        Session("montoses") = montoses
        Session("diferencia") = diferenciagastos

    End Sub

    'Datos del estudio socioeconomico
    Private Sub DatosGastos()
        Session("Con").CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_DATOS_ES"
        Session("rs") = Session("cmd").Execute()


        'Si trae algo la sesión
        If Not Session("rs").eof Then
            lbl_fechaact.Text = "Fecha de última actualización(DD/MM/AAAA): " + Left(Session("rs").Fields("FECHAC").Value.ToString, 10)

            'Variables globales
            Dim montos(100) As Label
            Dim i As Integer
            Dim s As Integer

            If Not Session("montoses") Is Nothing Then
                montos = Session("montoses")
            End If

            i = 0
            s = 0
            Do While Not montos(i) Is Nothing

                Do While Not Session("rs").eof

                    If "E" + Session("rs").Fields("IDGASTO").Value.ToString = montos(i).ID Then
                        montos(i).Text = FormatCurrency(Session("rs").Fields("MONTO").Value.ToString)
                    End If
                    Session("rs").MoveNext()

                Loop

                Session("rs").MoveFirst()
                i = i + 1
            Loop

            Session("montoses") = montos

            'si  vence el estudio socioeconomico muestro el siguiente mensaje
            If Session("rs").Fields("AVISO").value.ToString = "0" Then
                lbl_statuses.Text = "Aviso: Los datos del estudio socioecónomico han expirado"
            End If
        End If

        Session("Con").Close()
    End Sub

    'Proceso que permite mostrar la diferencia entre los 2 arreglos de investigacion y socioeconomico.. y el total de diferencia de los mismos
    Private Sub CalculaDiferencia()

        'Variables globales
        Dim diferencia(100) As Label
        Dim montoses(100) As Label
        Dim montosic(100) As Label
        Dim i As Integer
        Dim s As Integer

        'Se declaran las variables de sesion como arreglos 

        If Not Session("diferencia") Is Nothing Then
            diferencia = Session("diferencia")
        End If

        If Not Session("montoses") Is Nothing Then
            montoses = Session("montoses")
        End If
        If Not Session("montosic") Is Nothing Then
            montosic = Session("montosic")
        End If

        i = 0
        s = 0

        'ciclo que permite recorrer las diferencias que existen entre los montos de investigacion y montos de estudio socioeconomico
        Do While Not montosic(i) Is Nothing

            diferencia(i).Text = FormatCurrency(Math.Abs((montosic(i).Text) - (montoses(i).Text)))
            'Acumulo las diferencias 
            s = s + diferencia(i).Text
            'muestra la suma de las diferencias
            'lbl_diferencia.Text = FormatCurrency(s)
            i = i + 1
        Loop

    End Sub

    Private Sub Datos()
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_INGRESO_PERSONA"
        Session("rs") = Session("cmd").Execute()

        'SI LA PERSONA NO TIENE CODEUDOR SOLO MUESTRA SUS PROPIOS INGRESOS
        If Not Session("rs").EOF Then
            lbl_montoempleo.Text = Session("rs").fields("EMPLEO").value.ToString
            lbl_montoadicional.Text = Session("rs").fields("ADICIONALES").value.ToString
            lbl_montootros.Text = Session("rs").fields("OTRO").value.ToString
            Session("rs").movenext()
            Dim SUMA As Decimal
            Dim TOTAL As Decimal
            SUMA = CDec(lbl_montoempleo.Text) + CDec(lbl_montoadicional.Text) + CDec(lbl_montootros.Text)
            lbl_montosub.Text = FormatCurrency(SUMA)
            TOTAL = SUMA
            lbl_montototal.Text = FormatCurrency(TOTAL)
        End If

        'SI LA PERSONA TIENE CODEUDOR
        If Not Session("rs").EOF Then
            lbl_tit_cliente.Visible = True
            lbl_ingconyuge.Visible = True
            lbl_empleocony.Visible = True
            lbl_mempleocony.Visible = True
            lbl_adicionalcony.Visible = True
            lbl_montoadicionalcony.Visible = True
            lbl_otroscony.Visible = True
            lbl_montootroscony.Visible = True
            lbl_subtotalconyuge.Visible = True
            lbl_subcony.Visible = True

            lbl_mempleocony.Text = Session("rs").fields("EMPLEO").value.ToString
            lbl_montoadicionalcony.Text = Session("rs").fields("ADICIONALES").value.ToString
            lbl_montootroscony.Text = Session("rs").fields("OTRO").value.ToString
            Dim TOTAL As Decimal
            Dim SUBTOTAL As Decimal
            Dim SUMA As Decimal
            '--suma de los ingresos de la persona
            SUMA = CDec(lbl_montoempleo.Text) + CDec(lbl_montoadicional.Text) + CDec(lbl_montootros.Text)
            lbl_montosub.Text = FormatCurrency(SUMA)

            '--suma de ingresos del conyuge
            SUBTOTAL = CDec(lbl_mempleocony.Text) + CDec(lbl_montoadicionalcony.Text) + CDec(lbl_montootroscony.Text)
            lbl_subcony.Text = FormatCurrency(SUBTOTAL)
            '---Total de la suma
            TOTAL = SUMA + SUBTOTAL
            lbl_montototal.Text = FormatCurrency(TOTAL)
        End If
        Session("Con").Close()

    End Sub

End Class