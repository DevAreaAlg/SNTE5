Imports RestSharp
Imports WnvWordToPdf
Public Class CRED_EXP_APARTADO7
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Prellenado de Documentos", "PRELLENADO DE DOCUMENTOS")

        If Not IsPostBack Then
            If Session("FOLIO") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            End If

            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos del Expediente: " + CStr(Session("CVEEXPE"))
            Session("VENGODE") = "CRED_EXP_APARTADO7.aspx"
            txt_clabe.Enabled = True
            tbx_clabe_conf.Enabled = True
            cmb_banco.Enabled = True
            rfv_ddl_banco.Enabled = True
            rfv_txt_clabe.Enabled = True
            rfv_tbx_clabe_conf.Enabled = True
            CargarMetodosPago()
            ObtenerProducto()
            folderA(pnl_pre_sol, "down")
            bancos()
            CargaInfoBanco()

        End If

    End Sub

    Private Sub CargarMetodosPago()

        ddl_medio_pago.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_medio_pago.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_METODO_PAGO"
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim item As New ListItem(Session("rs").Fields("TEXT").Value, Session("rs").Fields("VALUE").Value.ToString)
                ddl_medio_pago.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Protected Sub ddl_medio_pago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_medio_pago.SelectedIndexChanged

        If ddl_medio_pago.SelectedValue = "CHQ" Then
            txt_clabe.Text = ""
            txt_clabe.Enabled = False
            cmb_banco.SelectedIndex = "-1"
            cmb_banco.Enabled = False
            tbx_clabe_conf.Text = ""
            tbx_clabe_conf.Enabled = False
            rfv_ddl_banco.Enabled = False
            rfv_txt_clabe.Enabled = False
            rfv_tbx_clabe_conf.Enabled = False
        Else
            txt_clabe.Enabled = True
            cmb_banco.Enabled = True
            tbx_clabe_conf.Enabled = True
            rfv_ddl_banco.Enabled = True
            rfv_txt_clabe.Enabled = True
            rfv_tbx_clabe_conf.Enabled = True
        End If

        If ddl_medio_pago.SelectedValue = "-1" Then
            txt_clabe.Text = ""
            txt_clabe.Enabled = False
            cmb_banco.SelectedIndex = "-1"
            cmb_banco.Enabled = False
            tbx_clabe_conf.Text = ""
            tbx_clabe_conf.Enabled = False
        End If

    End Sub

    Protected Sub cmb_banco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_banco.SelectedIndexChanged
        If cmb_banco.SelectedItem.Text = "SCOTIABANK" Then
            lbl_estatus.Text = ""
            txt_clabe.Text = ""
            tbx_clabe_conf.Text = ""
        Else
            lbl_estatus.Text = ""
            txt_clabe.Text = ""
            tbx_clabe_conf.Text = ""
        End If

        If cmb_banco.SelectedValue = -1 Then
            lbl_estatus.Text = ""
            txt_clabe.Text = ""
            txt_clabe.Enabled = False
            ddl_medio_pago.ClearSelection()
            tbx_clabe_conf.Text = ""
            tbx_clabe_conf.Enabled = False
        End If
    End Sub

    Private Sub CargaInfoBanco()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CLABE_BANCO"
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then

            If Session("rs").Fields("TIPO_CUENTA").Value.ToString = 1 Then

                ddl_medio_pago.SelectedValue = "TRS"
                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                'txt_cat.Text = Session("rs").Fields("CATEGORIA").Value.ToString
                'txt_anios.Text = Session("rs").Fields("ANIOS_SERV").Value.ToString
                txt_correo.Text = Session("rs").Fields("CORREO").Value.ToString
                txt_num.Text = Session("rs").Fields("TELEFONO").Value.ToString

            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 2 Then

                ddl_medio_pago.SelectedValue = "TRS"
                cmb_banco.SelectedValue = Session("rs").Fields("INSTITU").Value.ToString
                txt_clabe.Text = Session("rs").Fields("NUM_CUENTA").Value.ToString
                'txt_cat.Text = Session("rs").Fields("CATEGORIA").Value.ToString
                'txt_anios.Text = Session("rs").Fields("ANIOS_SERV").Value.ToString
                txt_correo.Text = Session("rs").Fields("CORREO").Value.ToString
                txt_num.Text = Session("rs").Fields("TELEFONO").Value.ToString

            ElseIf Session("rs").Fields("TIPO_CUENTA").Value.ToString = 3 Then

                ddl_medio_pago.SelectedValue = "CHQ"
                cmb_banco.SelectedIndex = "-1"
                cmb_banco.Enabled = False
                rfv_ddl_banco.Enabled = False
                txt_clabe.Text = ""
                txt_clabe.Enabled = False
                rfv_txt_clabe.Enabled = False
                tbx_clabe_conf.Text = ""
                tbx_clabe_conf.Enabled = False
                rfv_tbx_clabe_conf.Enabled = False
                'txt_cat.Text = Session("rs").Fields("CATEGORIA").Value.ToString
                'txt_anios.Text = Session("rs").Fields("ANIOS_SERV").Value.ToString
                txt_correo.Text = Session("rs").Fields("CORREO").Value.ToString
                txt_num.Text = Session("rs").Fields("TELEFONO").Value.ToString
            End If

        End If

        Session("Con").close()

    End Sub
    Private Sub bancos()

        cmb_banco.Items.Clear()
        cmb_banco.Items.Add(New ListItem("ELIJA", "-1"))
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_INSTITUCIONES_FINANCIERAS"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF
            cmb_banco.Items.Add(New ListItem(Session("rs").Fields("CATINSTFINAN_INSTITUCION").Value, Session("rs").Fields("CATINSTFINAN_ID_INSTITUCION").Value))
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub


#Region "GENERA DOCUMENTOS"

    Protected Sub btn_DescargarPrellenadoCred_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_DescargarPrellenadoCred.Click

        ActualizarSemaforo()
        validaSolicitud()

    End Sub

    Private Sub validaSolicitud()

        Dim destino As String = ""
        Dim estatustrab As String = ""
        Dim claveprod As String = ""
        Dim nombreprod As String = ""
        Dim renoProd As Integer = 0
        'OBTENER EL COUNT TIPO PAGO CAP
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CONF_PRELLENADO_CARTA"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            estatustrab = Session("rs").Fields("ESTATUSTRAB").Value.ToString()
            destino = Session("rs").Fields("DESTINO").Value.ToString()
            claveprod = Session("rs").Fields("CLAVEPROD").Value.ToString()
            nombreprod = Session("rs").Fields("NOMBREPROD").Value.ToString()
            renoProd = Session("rs").Fields("RESTRUCTURA").Value.ToString()
        End If
        Session("Con").Close()

        generadDocs(claveprod, estatustrab, destino, renoProd)

    End Sub

    Private Sub generadDocs(ByVal cveProd As String, ByVal estatusTrab As String, ByVal destino As String, ByVal renoProd As Integer)

        Dim extencion As String = ".DOCX"

        'If renoProd = 0 Then
        '    extencion = ".DOCX"
        'Else
        '    extencion = "_RENO.DOCX"
        'End If

        lbl_llen_solic.Text = cveProd + "_" + estatusTrab + "_" + destino + extencion

        Dim url As String = cveProd + "_" + estatusTrab + "_" + destino + extencion

        If Not url Like (-1).ToString Then

            Dim NewDocName As String = cveProd + "_" + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
            Dim ResultDocName As String

            If renoProd = 0 Then
                ResultDocName = cveProd + "_" + estatusTrab + "_" + destino + "(" + CStr(Session("CVEEXPE")) + ").pdf"
            Else
                ResultDocName = cveProd + "_" + estatusTrab + "_" + destino + "_RENO(" + CStr(Session("CVEEXPE")) + ").pdf"
            End If


            Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "\DocPlantillas\Solicitudes\" + url)
                ReemplazarEtiquetas(worddoc)
                worddoc.SaveAs(Session("APPATH") + "\Word\" + NewDocName + extencion)
            End Using

            Dim result As String = ""
            'Dim objNewWord As New Microsoft.Office.Interop.Word.Application()

            Try
                Dim winKey As String = ConfigurationManager.AppSettings.[Get]("WINKEY")
                Dim wordToPdfConverter As New WordToPdfConverter()
                wordToPdfConverter.LicenseKey = winKey
                wordToPdfConverter.ConvertWordFileToFile(Session("APPATH") + "\Word\" + NewDocName + extencion, Session("APPATH") + "\Word\" + NewDocName + ".pdf")

                'Elimina el Documento WORD ya Prellenado
                System.IO.File.Delete(Session("APPATH") + "\Word\" + NewDocName + extencion)

                ' Se genera el PDF
                Dim Filename As String = NewDocName + ".pdf"
                Dim FilePath As String = Session("APPATH") + "\Word\"
                Dim fs As System.IO.FileStream
                fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
                Dim bytBytes(fs.Length) As Byte
                fs.Read(bytBytes, 0, fs.Length)
                fs.Close()

                'se va a actualzar?
                'ActualizaExtatusContratoImpreso()

                'Borra el archivo creado en memoria
                DelHDFile(FilePath + Filename)
                Response.Buffer = True
                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", ResultDocName))
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(bytBytes)
                Response.End()

            Catch ex As Exception
                result = (ex.Message)
            Finally
                'objNewWord.Quit()
            End Try

            'objNewWord = Nothing

        End If
    End Sub

    Private Sub ReemplazarEtiquetas(ByRef doc As Novacode.DocX)
        Dim anio_factor As Integer = 0
        Dim persona As String = String.Empty
        Dim nomprod As String = String.Empty
        Dim dia As String = String.Empty
        Dim mes As String = String.Empty
        Dim año As String = String.Empty
        Dim cve_exp As String = String.Empty
        Dim montoCred As Decimal = 0.00
        Dim montoCredLetra As String = String.Empty
        Dim montoMaxCred As Decimal = 0.00
        Dim montoMaxCredLetra As String = String.Empty
        Dim direccion As String = String.Empty
        Dim calle As String = String.Empty
        Dim numInt As String = String.Empty
        Dim numExt As String = String.Empty
        Dim poblacion As String = String.Empty
        Dim estadoTrab As String = String.Empty
        Dim muniTrab As String = String.Empty
        Dim tipoProd As String = String.Empty
        Dim tipoCred As String = String.Empty
        Dim adscripcion As String = String.Empty
        Dim seguro As Decimal = 0.00
        Dim monto_transf As Decimal = 0.00
        Dim pagoFijo As Decimal = 0.00
        Dim adeudo As Decimal = 0.00
        Dim pagoFijo_Letra As String = ""
        Dim PagoQuin As Decimal = 0.00
        Dim pagoMen As Decimal = 0.00
        Dim pagoAnual As Decimal = 0.00
        Dim costoAn As Decimal = 0.00
        Dim costoSeg As Decimal = 0.00
        Dim costoQuin As Decimal = 0.00
        Dim porcentaje_seg As String = ""
        Dim fecha_sis As String = ""
        Dim sueldo_quin As String = ""
        Dim saldo_ant As Decimal = 0.00
        Dim cve_exp_rigen As String = ""
        Dim monto_transf_reno As Decimal = 0.00
        Dim nom_suc As String = ""
        Dim curp As String = ""
        Dim rfc As String = ""
        Dim clabeBanco As String = ""
        Dim banco As String = ""
        Dim fnac As String = ""
        Dim edad As String = ""
        Dim sexo As String = ""
        Dim edociv As String = ""
        Dim int_sal_ant As Decimal = 0.00
        Dim telefono As String = String.Empty
        Dim emailTrab As String = String.Empty
        Dim cargoTrab As String = String.Empty
        Dim numCtrl As String = String.Empty
        Dim numPagos As String = String.Empty
        Dim nomInst As String = String.Empty
        Dim iniOpe As String = String.Empty
        Dim intereses As Decimal = 0.0
        Dim cct As String = String.Empty
        Dim region As String = String.Empty
        Dim delegacion As String = String.Empty

        Dim categoria As String = String.Empty
        Dim anios_servicio As String = String.Empty


        Dim periodo As String = String.Empty
        Dim cp As String = String.Empty
        Dim fechaVencimiento As String = ""


        Dim nomAval As String = String.Empty
        Dim direccionAval As String = String.Empty
        Dim calleAval As String = String.Empty
        Dim numIntAval As String = String.Empty
        Dim numExtAval As String = String.Empty
        Dim poblacionAval As String = String.Empty
        Dim estadoAval As String = String.Empty
        Dim muniAval As String = String.Empty


        Dim medio_pago As String = String.Empty
        Dim val1 As String = String.Empty
        Dim medio As String = String.Empty
        Dim val2 As String = String.Empty

        'PARA GARANTES
        Dim nomGarante1 As String = String.Empty
        Dim sexoGarante1 As String = String.Empty
        Dim edadGarante1 As String = String.Empty
        Dim rfcGarante1 As String = String.Empty
        Dim domicilioGarante1 As String = String.Empty
        Dim coloniaGarante1 As String = String.Empty
        Dim estadoGarante1 As String = String.Empty
        Dim telGarante1 As String = String.Empty
        Dim edocivilGarante1 As String = String.Empty
        Dim nomconyugeGarante1 As String = String.Empty
        Dim valorcatGarante1 As String = String.Empty
        Dim avaluocomGarante1 As String = String.Empty


        Dim nomGarante2 As String = String.Empty
        Dim sexoGarante2 As String = String.Empty
        Dim edadGarante2 As String = String.Empty
        Dim rfcGarante2 As String = String.Empty
        Dim domicilioGarante2 As String = String.Empty
        Dim coloniaGarante2 As String = String.Empty
        Dim estadoGarante2 As String = String.Empty
        Dim telGarante2 As String = String.Empty
        Dim edocivilGarante2 As String = String.Empty
        Dim nomconyugeGarante2 As String = String.Empty
        Dim valorcatGarante2 As String = String.Empty
        Dim avaluocomGarante2 As String = String.Empty
        Dim finalidadCred As String = String.Empty

        Dim quincenaPagar As Integer = 0
        Dim anioPagar As Integer = 0
        Dim numcontrol_aval As String = String.Empty
        Dim conyuge As String = String.Empty

        ' SE OBTIENEN LOS DATOS PARA LAS SOLICITUDES DEL PRELLENADO
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_SOLICITUDES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PERSONA", Session("adVarChar"), Session("adParamInput"), 11, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            persona = Session("rs").Fields("NOMBRE").Value.ToString()
            dia = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2)
            mes = Session("rs").Fields("MES").Value.ToString()
            año = Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
            año = año.Replace("/", "")
            cve_exp = Session("rs").Fields("CVE_EXP").Value.ToString()

            calle = Session("rs").Fields("CALLE").Value.ToString()
            numExt = Session("rs").Fields("NUMEXT").Value.ToString()
            numInt = Session("rs").Fields("NUMINT").Value.ToString()

            curp = Session("rs").Fields("CURP").Value.ToString()
            rfc = Session("rs").Fields("RFC").Value.ToString()

            fnac = Session("rs").Fields("FNAC").Value.ToString()
            fnac = fnac.Replace("/", "")
            edad = Session("rs").Fields("EDAD").Value.ToString()
            sexo = Session("rs").Fields("SEXO").Value.ToString()
            edociv = Session("rs").Fields("EDOCIV").Value.ToString()
            conyuge = Session("rs").Fields("CONYUGE").Value.ToString()

            cct = Session("rs").Fields("CCT").Value.ToString()
            region = Session("rs").Fields("REGION").Value.ToString()
            delegacion = Session("rs").Fields("DELEGACION").Value.ToString()

            categoria = Session("rs").Fields("CATEGORIA").Value.ToString()
            anios_servicio = Session("rs").Fields("ANIOS_SERVICIO").Value.ToString()

            periodo = Session("rs").Fields("PERIODO").Value.ToString()
            cp = Session("rs").Fields("CP").Value.ToString()



            direccion = calle

            If numExt <> "" And numExt <> "0" And numInt = "" Then
                direccion += " No." + numExt
            ElseIf numExt <> "" And numInt <> "" And numInt <> "0" Then
                direccion += "  No." + numExt + " " + numInt
            ElseIf numExt = "" And numInt <> "" And numInt <> "0" Then
                direccion += "No." + numInt
            End If

            poblacion = Session("rs").Fields("ASENTAMIENTO").Value.ToString()
            estadoTrab = Session("rs").Fields("ESTADO").Value.ToString()
            muniTrab = Session("rs").Fields("MUNICIPIO").Value.ToString()
            nom_suc = Session("rs").Fields("SUCURSAL").Value.ToString()
            emailTrab = Session("rs").Fields("CORREO").Value.ToString()
            cargoTrab = Session("rs").Fields("OCUPACION").Value.ToString()
            numCtrl = Session("rs").Fields("NUMTRAB").Value.ToString()
            numPagos = Session("rs").Fields("NUMPAGOS").Value.ToString()
            nomInst = Session("rs").Fields("NOMINST").Value.ToString()
            iniOpe = Session("rs").Fields("FECHAINI").Value.ToString()
            numcontrol_aval = Session("rs").Fields("NUMCONTROLAVAL").Value.ToString()

            medio_pago = Session("rs").Fields("MEDIOPAGO").Value.ToString()
            val1 = Session("rs").Fields("VALOR").Value.ToString()
            medio = Session("rs").Fields("MEDIO").Value.ToString()
            val2 = Session("rs").Fields("VALOR2").Value.ToString()
            telefono = Session("rs").Fields("TELEFONO2").Value.ToString()
        End If

        Session("Con").Close()


        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            montoCred = Session("rs").Fields("MONTO").Value.ToString()
            montoCredLetra = Session("rs").Fields("MONTO_LETRA").Value.ToString()
            montoMaxCred = Session("rs").Fields("MONTO_MAX_CRED").Value.ToString()
            montoMaxCredLetra = Session("rs").Fields("MONTO_MAX_CRED_LETRA").Value.ToString()
            tipoProd = Session("rs").Fields("TIPO_PROD").Value.ToString()
            tipoCred = Session("rs").Fields("PRODUCTO").Value.ToString()
            adscripcion = Session("rs").Fields("ADSCRIPCION").Value.ToString()
            pagoFijo = Session("rs").Fields("PAGOFIJO").Value.ToString()
            pagoMen = Session("rs").Fields("PAGOMEN").Value.ToString()
            pagoAnual = Session("rs").Fields("PAGOAN").Value.ToString()
            adeudo = Session("rs").Fields("SUBTOTAL").value.ToString()
            pagoMen = Session("rs").Fields("PAGOMEN").Value.ToString()
            costoSeg = Session("rs").Fields("COSTOSEG").Value.ToString()
            costoAn = Session("rs").Fields("COSTOAN").Value.ToString()
            costoQuin = Session("rs").Fields("COSTOQUIN").value.ToString()
            pagoFijo_Letra = Session("rs").Fields("PAGOFIJO_LETRA").Value.ToString()
            seguro = Session("rs").Fields("SEGURO").Value.ToString()
            monto_transf = Session("rs").Fields("CAP_ENTREGAR").Value.ToString()
            porcentaje_seg = Session("rs").Fields("PORCENTAJE_COMISION").Value.ToString()
            fecha_sis = Session("rs").Fields("FECHA_SISTEMA").Value.ToString()
            sueldo_quin = Session("rs").Fields("SUELDO_QUIN").Value.ToString()
            saldo_ant = Session("rs").Fields("SALDO_ANTERIOR").Value.ToString()
            cve_exp_rigen = Session("rs").Fields("CVE_EXP_ORIGEN_REEST").Value.ToString()
            monto_transf_reno = Session("rs").Fields("MONTO_TRANSF_RENO").Value.ToString()
            clabeBanco = Session("rs").Fields("CLABE_BANCARIA").Value.ToString()
            banco = Session("rs").Fields("BANCO").Value.ToString()
            int_sal_ant = Session("rs").Fields("INT_ORDI").Value.ToString()
            intereses = Session("rs").Fields("INTERESES").Value.ToString()

            nomGarante1 = Session("rs").Fields("NOM_GARANTE1").Value.ToString()
            sexoGarante1 = Session("rs").Fields("SEXO_GH1").Value.ToString()
            edadGarante1 = Session("rs").Fields("EDAD_GH1").Value.ToString()
            rfcGarante1 = Session("rs").Fields("RFC_GH1").Value.ToString()
            domicilioGarante1 = Session("rs").Fields("DOMI_GH1").Value.ToString()
            coloniaGarante1 = Session("rs").Fields("POBLACION_GH1").Value.ToString()
            estadoGarante1 = Session("rs").Fields("ESTADO_GH1").Value.ToString()
            telGarante1 = Session("rs").Fields("TELGH1").Value.ToString()
            edocivilGarante1 = Session("rs").Fields("EDOCIVIL_GH1").Value.ToString()
            nomconyugeGarante1 = Session("rs").Fields("NOMCONYUGE_GH1").Value.ToString()
            valorcatGarante1 = Session("rs").Fields("VALOR_CAT_GH1").Value.ToString()
            avaluocomGarante1 = Session("rs").Fields("AVALUO_COM_GH1").Value.ToString()

            nomGarante2 = Session("rs").Fields("NOM_GARANTE2").Value.ToString()
            sexoGarante2 = Session("rs").Fields("SEXO_GH2").Value.ToString()
            edadGarante2 = Session("rs").Fields("EDAD_GH2").Value.ToString()
            rfcGarante2 = Session("rs").Fields("RFC_GH2").Value.ToString()
            domicilioGarante2 = Session("rs").Fields("DOMI_GH2").Value.ToString()
            coloniaGarante2 = Session("rs").Fields("POBLACION_GH2").Value.ToString()
            estadoGarante2 = Session("rs").Fields("ESTADO_GH2").Value.ToString()
            telGarante2 = Session("rs").Fields("TELGH2").Value.ToString()
            edocivilGarante2 = Session("rs").Fields("EDOCIVIL_GH2").Value.ToString()
            nomconyugeGarante2 = Session("rs").Fields("NOMCONYUGE_GH2").Value.ToString()
            valorcatGarante2 = Session("rs").Fields("VALOR_CAT_GH2").Value.ToString()
            avaluocomGarante2 = Session("rs").Fields("AVALUO_COM_GH2").Value.ToString()
            finalidadCred = Session("rs").Fields("FINALIDAD_CRED").Value.ToString()

            quincenaPagar = Session("rs").Fields("QNA_PAGAR").Value.ToString()
            anioPagar = Session("rs").Fields("ANIO_PAGAR").Value.ToString()
            anio_factor = Session("rs").Fields("ANIO_FACTOR").Value.ToString()

            fechaVencimiento = Session("rs").Fields("FECHA_VENCIMIENTO").Value.ToString()


        End If

        Session("Con").Close()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_AVALES_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            nomAval = Session("rs").Fields("NOMBRE").Value.ToString()


            calleAval = Session("rs").Fields("CALLE").Value.ToString()
            numExtAval = Session("rs").Fields("NUMEXT").Value.ToString()
            numIntAval = Session("rs").Fields("NUMINT").Value.ToString()

            'If numIntAval = "" Then
            direccionAval = calleAval ' + " Num. Ext. #" + numExtAval

            If numExtAval <> "" And numExtAval <> "0" And numIntAval = "" Then
                direccion += " No." + numExtAval
            ElseIf numExtAval <> "" And numIntAval <> "" And numIntAval <> "0" Then
                direccion += "  No." + numExtAval + " " + numIntAval
            ElseIf numExtAval = "" And numExtAval <> "0" And numIntAval <> "" And numIntAval <> "0" Then
                direccion += "No." + numIntAval
            End If

            poblacionAval = Session("rs").Fields("ASENTAMIENTO").Value.ToString()
            estadoAval = Session("rs").Fields("ESTADO").Value.ToString()
            muniAval = Session("rs").Fields("MUNICIPIO").Value.ToString()
        End If

        Session("Con").Close()
        doc.ReplaceText("[NOM_TRABAJADOR]", persona, False, RegexOptions.None)
        doc.ReplaceText("[NOM_PROD]", Session("PRODUCTO").ToString, False, RegexOptions.None)
        doc.ReplaceText("[DIAS]", dia, False, RegexOptions.None)
        doc.ReplaceText("[MES]", mes, False, RegexOptions.None)
        doc.ReplaceText("[ANIO]", año, False, RegexOptions.None)
        doc.ReplaceText("[CVE_EXP]", cve_exp, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_CREDITO]", FormatCurrency(CStr(montoCred)), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_CREDITO_LETRA]", montoCredLetra, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_MAX_CREDITO]", FormatCurrency(CStr(montoMaxCred)), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_MAX_CREDITO_LETRA]", montoMaxCredLetra, False, RegexOptions.None)
        doc.ReplaceText("[ESTADO_TRABAJADOR]", estadoTrab, False, RegexOptions.None)
        doc.ReplaceText("[DOMI_TRABAJADOR]", direccion, False, RegexOptions.None)
        doc.ReplaceText("[POBLACION_TRAB]", poblacion, False, RegexOptions.None)
        doc.ReplaceText("[MUNI_TRABAJADOR]", muniTrab, False, RegexOptions.None)
        doc.ReplaceText("[TIPO_PROD]", tipoProd, False, RegexOptions.None)
        doc.ReplaceText("[ADSCRIPCION]", adscripcion, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_SEG]", FormatCurrency(seguro), False, RegexOptions.None)
        doc.ReplaceText("[MONTO_TRANS]", FormatCurrency(monto_transf), False, RegexOptions.None)
        doc.ReplaceText("[PAGO_FIJO]", FormatCurrency(pagoFijo), False, RegexOptions.None)
        doc.ReplaceText("[PAGO_MEN]", FormatCurrency(pagoMen), False, RegexOptions.None)
        doc.ReplaceText("[COSTO_SEG]", FormatCurrency(costoSeg), False, RegexOptions.None)
        doc.ReplaceText("[TOTAL_MENS]", FormatCurrency(costoAn), False, RegexOptions.None)
        doc.ReplaceText("[TOTAL_QNAL]", FormatCurrency(costoQuin), False, RegexOptions.None)
        doc.ReplaceText("[ADEUDO]", FormatCurrency(adeudo), False, RegexOptions.None)
        doc.ReplaceText("[PAGO_FIJO_LETRA]", pagoFijo_Letra, False, RegexOptions.None)
        doc.ReplaceText("[PORCENTAJE_COMI]", porcentaje_seg, False, RegexOptions.None)
        doc.ReplaceText("[PORCENTAJE_SEG]", seguro, False, RegexOptions.None)
        doc.ReplaceText("[FECHASIS]", fecha_sis, False, RegexOptions.None)
        doc.ReplaceText("[CCT]", cct, False, RegexOptions.None)
        doc.ReplaceText("[REGION]", region, False, RegexOptions.None)
        doc.ReplaceText("[DELEGACION]", delegacion, False, RegexOptions.None)

        doc.ReplaceText("[CATEGORIA]", categoria, False, RegexOptions.None)
        doc.ReplaceText("[ANIOS_SERVICIO]", anios_servicio, False, RegexOptions.None)

        doc.ReplaceText("[PERIODO]", periodo, False, RegexOptions.None)
        doc.ReplaceText("[CP]", cp, False, RegexOptions.None)



        doc.ReplaceText("[CVE_EXP_ORIGEN]", cve_exp_rigen, False, RegexOptions.None)
        doc.ReplaceText("[MONTO_TRANS_RENO]", FormatCurrency(monto_transf_reno), False, RegexOptions.None)
        doc.ReplaceText("[NOM_SUC]", nom_suc, False, RegexOptions.None)
        'doc.ReplaceText("[CBE_BANCARIA]", clabeBanco, False, RegexOptions.None)
        'doc.ReplaceText("[NOM_BANCO]", bncSolicitud, False, RegexOptions.None)

        doc.ReplaceText("[MEDIO_PAGO]", medio_pago, False, RegexOptions.None)
        doc.ReplaceText("[VALOR]", val1, False, RegexOptions.None)
        doc.ReplaceText("[MEDIO]", medio, False, RegexOptions.None)
        doc.ReplaceText("[VALOR2]", val2, False, RegexOptions.None)
        doc.ReplaceText("[INT_SAL_ANT]", FormatCurrency(int_sal_ant), False, RegexOptions.None)
        doc.ReplaceText("[TELTRAB]", telefono, False, RegexOptions.None)
        doc.ReplaceText("[EMAILTRAB]", emailTrab, False, RegexOptions.None)
        doc.ReplaceText("[CARGOTRAB]", cargoTrab, False, RegexOptions.None)
        doc.ReplaceText("[NUMTRAB]", numCtrl, False, RegexOptions.None)
        doc.ReplaceText("[NUMPAGOS]", numPagos, False, RegexOptions.None)
        doc.ReplaceText("[NOMINST]", nomInst, False, RegexOptions.None)
        doc.ReplaceText("[FECHAINI]", iniOpe, False, RegexOptions.None)
        doc.ReplaceText("[CURP]", curp, False, RegexOptions.None)
        doc.ReplaceText("[RFC]", rfc, False, RegexOptions.None)
        doc.ReplaceText("[FNAC]", fnac, False, RegexOptions.None)
        doc.ReplaceText("[SEXO]", sexo, False, RegexOptions.None)
        doc.ReplaceText("[EDOCIVIL]", edociv, False, RegexOptions.None)
        doc.ReplaceText("[EDAD]", edad, False, RegexOptions.None)
        doc.ReplaceText("[INTERES]", FormatCurrency(intereses), False, RegexOptions.None)


        doc.ReplaceText("[NOM_AVAL]", nomAval, False, RegexOptions.None)
        doc.ReplaceText("[DOMI_AVAL]", direccionAval, False, RegexOptions.None)
        doc.ReplaceText("[POBLACION_AVAL]", poblacionAval, False, RegexOptions.None)
        doc.ReplaceText("[MUNI_AVAL]", muniAval, False, RegexOptions.None)
        doc.ReplaceText("[ESTADO_AVAL]", estadoAval, False, RegexOptions.None)
        doc.ReplaceText("[CTRLAVAL]", numcontrol_aval, False, RegexOptions.None)
        doc.ReplaceText("[NOM_CONYUGE]", conyuge, False, RegexOptions.None)
        doc.ReplaceText("[ANIO_FAC]", anio_factor, False, RegexOptions.None)
        doc.ReplaceText("[SAL_QUIN_TRAB]", FormatCurrency(sueldo_quin), False, RegexOptions.None)
        doc.ReplaceText("[QNA_PAGAR]", quincenaPagar, False, RegexOptions.None)

        doc.ReplaceText("[FECHA_VENCIMIENTO]", fechaVencimiento, False, RegexOptions.None)


        'GARANTES HIPOTECARIOS
        If nomGarante1 <> "" Then

            doc.ReplaceText("[MENOS_SAL_ANT]", FormatCurrency(saldo_ant), False, RegexOptions.None)
            doc.ReplaceText("[NOM_GARANTE1]", nomGarante1, False, RegexOptions.None)
            doc.ReplaceText("[SEXO_GH1]", sexoGarante1, False, RegexOptions.None)
            doc.ReplaceText("[EDAD_GH1]", edadGarante1, False, RegexOptions.None)
            doc.ReplaceText("[RFC_GH1]", rfcGarante1, False, RegexOptions.None)
            doc.ReplaceText("[DOMI_GH1]", domicilioGarante1, False, RegexOptions.None)
            doc.ReplaceText("[POBLACION_GH1]", coloniaGarante1, False, RegexOptions.None)
            doc.ReplaceText("[ESTADO_GH1]", estadoGarante1, False, RegexOptions.None)
            doc.ReplaceText("[TELGH1]", telGarante1, False, RegexOptions.None)
            doc.ReplaceText("[EDOCIVIL_GH1]", edocivilGarante1, False, RegexOptions.None)
            doc.ReplaceText("[NOMCONYUGE_GH1]", nomconyugeGarante1, False, RegexOptions.None)
            doc.ReplaceText("[VALOR_CAT_GH1]", valorcatGarante1, False, RegexOptions.None)
            doc.ReplaceText("[AVALUO_COM_GH1]", avaluocomGarante1, False, RegexOptions.None)

            doc.ReplaceText("[NOM_GARANTE2]", nomGarante2, False, RegexOptions.None)
            doc.ReplaceText("[SEXO_GH2]", sexoGarante2, False, RegexOptions.None)
            doc.ReplaceText("[EDAD_GH2]", edadGarante2, False, RegexOptions.None)
            doc.ReplaceText("[RFC_GH2]", rfcGarante2, False, RegexOptions.None)
            doc.ReplaceText("[DOMI_GH2]", domicilioGarante2, False, RegexOptions.None)
            doc.ReplaceText("[POBLACION_GH2]", coloniaGarante2, False, RegexOptions.None)
            doc.ReplaceText("[ESTADO_GH2]", estadoGarante2, False, RegexOptions.None)
            doc.ReplaceText("[TELGH2]", telGarante2, False, RegexOptions.None)
            doc.ReplaceText("[EDOCIVIL_GH2]", edocivilGarante2, False, RegexOptions.None)
            doc.ReplaceText("[NOMCONYUGE_GH2]", nomconyugeGarante2, False, RegexOptions.None)
            doc.ReplaceText("[VALOR_CAT_GH2]", valorcatGarante2, False, RegexOptions.None)
            doc.ReplaceText("[AVALUO_COM_GH2]", avaluocomGarante2, False, RegexOptions.None)
            doc.ReplaceText("[FINALIDAD_CRED]", finalidadCred, False, RegexOptions.None)
            doc.ReplaceText("[PAGO_MEN]", FormatCurrency(pagoMen), False, RegexOptions.None)
            doc.ReplaceText("[PAGO_ANUAL]", FormatCurrency(pagoAnual), False, RegexOptions.None)

            doc.ReplaceText("[ANIO_PAGAR]", anioPagar, False, RegexOptions.None)


        End If
    End Sub

    Public Function ceros(Nro As String, Cantidad As Integer) As String
        Dim numero As String, cuantos As String, i As Integer
        numero = Trim(Nro) 'Trim quita los espacion en blanco
        cuantos = "0"
        For i = 1 To Cantidad
            cuantos = cuantos & "0"
        Next i
        ceros = Mid(cuantos, 1, Cantidad - Len(numero)) & numero
    End Function
#End Region

#Region "Convierte a PDF" 'IHG 2017-07-26

    Private Sub ObtenerProducto()

        'Mostar los datos generales de un expediente: folio, nombre de afiliado y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_X_EXPEDIENTE"

        Session("rs") = Session("cmd").Execute()

        Session("PRODUCTO") = Session("rs").fields("PRODUCTO").value.ToString
        Session("PROSPECTO") = Session("rs").fields("PROSPECTO").value.ToString
        Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString

        Session("Con").Close()

    End Sub

    'Private Sub ConviertePDF(ByVal NewDocName As String, ByVal cPath As String, ByVal NewDocNameA As String)
    '    Dim result As String = ""
    '    ' Dim objNewWord As New Microsoft.Office.Interop.Word.Application()
    '    Dim ResultDocName As String = NewDocName + ".pdf"

    '    Try
    '        Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(cPath + NewDocName + ".docx")
    '        objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", cPath), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
    '        objNewDoc.Close()
    '        'Elimina el Documento WORD ya Prellenado
    '        System.IO.File.Delete(cPath + NewDocName + ".docx")
    '        If NewDocNameA <> "" Then
    '            System.IO.File.Delete(cPath + NewDocNameA + ".docx")
    '        End If

    '        ' Se genera el PDF
    '        Dim Filename As String = NewDocName + ".pdf"
    '        Dim FilePath As String = cPath
    '        Dim fs As System.IO.FileStream
    '        fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
    '        Dim bytBytes(fs.Length) As Byte
    '        fs.Read(bytBytes, 0, fs.Length)
    '        fs.Close()

    '        'Borra el archivo creado en memoria
    '        DelHDFile(FilePath + Filename)
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ClearContent()
    '        Response.ClearHeaders()
    '        Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", ResultDocName))
    '        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '        Response.BinaryWrite(bytBytes)
    '        Response.End()

    '    Catch ex As Exception
    '        lbl_estatus.Text = ex.ToString
    '    Finally
    '        objNewWord.Quit()
    '    End Try

    '    objNewWord = Nothing
    'End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub


    Private Sub ActualizarSemaforo()
        'Mostar los datos generales de un expediente: folio, nombre de afiliado y producto
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_CNFEXP_SEMAFORO_APARTADO7"
        Session("cmd").Execute()
        Session("Con").Close()
    End Sub


    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toggle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)

        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#696462 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "none")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#696462 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #C0CDD5")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion

    End Sub
#End Region

#Region "Validación Bancos"

    Protected Sub btn_guardaBanco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardaBanco.Click

        If Len(txt_num.Text) > 0 Then
            If Len(txt_num.Text) <> 10 Then
                lbl_estatus.Text = "Error: El número de teléfono no puede ser menor a 10 dígitos."
                Exit Sub
            End If
        End If

        If txt_correo.Text <> "" Then
            If ValidaCorreo(txt_correo.Text) = False Then
                lbl_estatus.Text = "Error: El correo tiene formato invalido."
                Exit Sub
            End If
        End If

        Try
            If ddl_medio_pago.SelectedItem.Value = "CHQ" Then
                GuardaBancos()
            Else
                Dim Validacion As Boolean = ValidacionBancosVb()
                If Validacion = True Then
                    GuardaBancos()
                End If
            End If
        Catch ex As Exception
            lbl_estatus.Text = ex.ToString
        End Try
    End Sub

    Private Function ValidacionBancosVb() As Boolean

        If txt_clabe.Text <> tbx_clabe_conf.Text Then
            lbl_estatus.Text = "Error: La CLABE no coincide con el campo de confirmación."
            Return False
        End If

        If Len(txt_clabe.Text) <> 18 Then
            lbl_estatus.Text = "Error: El número de CLABE INTERBANCARIA debe ser de 18 posiciones."
            Return False
            'End If
        End If

        If Len(txt_clabe.Text) = 18 Then
            Dim Validacion As String = ValidaCLABE()
            If Validacion <> "OK" Then
                If Validacion = "FALSE" Then
                    lbl_estatus.Text = "Error: La CLABE no coincide con el Banco seleccionado."
                    Return False
                Else

                    lbl_estatus.Text = "Error: CLABE ya registrada para el agremiado: " + Validacion.ToString
                    Return False
                End If
            End If
        End If

        Return True

    End Function

    Private Function ValidaCLABE() As String

        Dim Respuesta As String = "FALSE"
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_PERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_BANCO", Session("adVarChar"), Session("adParamInput"), 10, cmb_banco.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CLABE_BANCO", Session("adVarChar"), Session("adParamInput"), 20, txt_clabe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VALIDA_CLABE_BANCO"
        Session("rs") = Session("cmd").Execute()
        Respuesta = Session("rs").fields("VALIDACION").value.ToString
        Session("Con").close()
        Return Respuesta

    End Function

    Protected Sub GuardaBancos()

        Dim tipoVal As Integer

        If ddl_medio_pago.SelectedValue = "CHQ" Then  'Si medio de pago es CHQ
            tipoVal = 3
        Else
            'Si medio de pago es Transferencia
            tipoVal = 2
        End If

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("PERSONAID", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDINSTFINAN", Session("adVarChar"), Session("adParamInput"), 10, CInt(cmb_banco.SelectedItem.Value))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("VALOR", Session("adVarChar"), Session("adParamInput"), 50, txt_clabe.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("EMAIL", Session("adVarChar"), Session("adParamInput"), 100, txt_correo.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TELEFONO", Session("adVarChar"), Session("adParamInput"), 50, txt_num.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("TIPOVAL", Session("adVarChar"), Session("adParamInput"), 1, tipoVal)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 25, Session("SESION").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "INS_INSTFINAN_CLABE"
        Session("rs") = Session("cmd").Execute()
        Session("Con").Close()

        lbl_estatus.Text = "Guardado correctamente"
        btn_DescargarPrellenadoCred.Enabled = "True"

    End Sub

#End Region
    Private Function ValidaCorreo(ByVal correo As String) As Boolean
        Return Regex.IsMatch(correo, ("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$"))
    End Function

End Class