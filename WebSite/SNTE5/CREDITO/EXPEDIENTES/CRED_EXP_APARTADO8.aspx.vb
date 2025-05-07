Imports System.Windows.Forms.Application

Public Class CRED_EXP_APARTADO8
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Pagaré y contrato", "Pagaré/Contrato")

        If Not Me.IsPostBack Then
            Session("Cont_text") = Nothing
            Session("VENGODE") = "CRED_EXP_APARTADO8.aspx"
            lbl_Prospecto.Text = Session("CLIENTE")
            lbl_Producto.Text = Session("PRODUCTO")
            lbl_Folio.Text = "Datos de Expediente: " + Session("CVEEXPE").ToString

            Clasificacion()
            NombreRepLeg()
            EstatusPagareFolio()
            LlenaFoliosPagare()
            NombreSucursal()
            INFORMACION_GENERAL()
            Tipo_contrato()

        End If
    End Sub

    Private Sub Clasificacion()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 20, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_CLASIF_CRED"
        Session("rs") = Session("cmd").Execute()
        Session("CLASIFICACION") = Session("rs").Fields("CLAVE").Value.ToString
        Session("TIPO_LINEA") = Session("rs").Fields("TIPO_LINEA").Value.ToString
        Session("FOLIO_LINEA") = Session("rs").Fields("FOLIO_LINEA").Value.ToString

        Session("Con").Close()
        btn_Contrato.Enabled = True

    End Sub

    Private Sub NombreSucursal()
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_SUCURSAL"
        Session("rs") = Session("cmd").Execute()
        Session("NOMBRESUC") = Session("rs").Fields("NOMBRE").value.ToString
        Session("Con").Close()
    End Sub

    Private Sub INFORMACION_GENERAL()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_INFO_X_FOLIO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("PERSONAID") = Session("rs").Fields("PERSONAID").value.ToString
            Session("TIPOPER") = Session("rs").Fields("TIPOPER").value.ToString
        End If

        Session("Con").Close()
    End Sub

#Region "Pagare"

    'Verifica si ya se prelleno el pagare de este folio , en caso de si se deshabilita el poder generarlo de nuevo.
    Private Sub EstatusPagareFolio()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ESTATUS_PAGARE"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("ESTATUS_PAGARE") = Session("rs").Fields("ESTATUS").Value.ToString

        Session("Con").Close()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FACULTAD_PAGARES"
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Session("FACULTAD") = Session("rs").Fields("FACULTAD").value.ToString

        Session("Con").Close()
        btn_Pagare.Enabled = True
        If Session("ESTATUS_PAGARE") = "NOGENERADO" Then
            btn_Pagare.Enabled = True
        Else
            btn_Pagare.Enabled = False
        End If

        If Session("FACULTAD") = "1" Then
            btn_Pagare.Enabled = True
        End If

        ' SI EL EXPEDIENTE ES DE CUENTA CORRIENTE O CARTA DE CREDITO EL BOTON APARECE DESHABILITADO AUNQUE TENGA LA FACULTAD DE GENERARLO
        If Session("ESTATUS_PAGARE") = "NOGENERAR" Then
            btn_Pagare.Enabled = False
        End If

    End Sub

    'Nombre Repesentante Legal
    Private Sub NombreRepLeg()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CONFGLOBAL_MASCORE_ENTIDAD"
        Session("rs") = Session("cmd").Execute()
        Session("REPLEG_ENTIDAD") = Session("rs").Fields("REPLEG").value.ToString
        Session("Con").Close()
    End Sub

    Private Sub LlenaFoliosPagare()

        Dim elija As New ListItem("ELIJA", "0")
        cmb_FolioPagare.Items.Clear()
        cmb_FolioPagare.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_FOLIOS_PAGARE"
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 10, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            Do While Not Session("rs").EOF
                Dim idCaja As String
                idCaja = Session("rs").Fields("FOLIO_PAGARE").Value.ToString
                Dim item As New ListItem(idCaja, idCaja)
                cmb_FolioPagare.Items.Add(item)
                Session("rs").movenext()
            Loop
        End If

        Session("Con").Close()

    End Sub

    Protected Sub btn_Pagare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Pagare.Click
        VerificarPagare()
    End Sub

    Private Sub VerificarPagare()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_VERIFICA_PAGARE"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_PAGARE", Session("adVarChar"), Session("adParamInput"), 11, cmb_FolioPagare.SelectedItem.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Session("PRELLENA") = Session("rs").Fields("PRELLENAR").Value.ToString
        Session("Con").Close()

        If Session("PRELLENA") = "PRELLENAR" Then

            ActualizaExtatusPagareImpreso()
            PrellenadoPagare()
            LlenaFoliosPagare()

        Else
            lbl_EstatusPagare.Text = Session("PRELLENA")
        End If
    End Sub

    Private Sub ActualizaExtatusPagareImpreso()

        Dim PAGARE As String
        Dim subject As String = String.Empty 'variable para el asunto del correo
        Dim cc As String = String.Empty 'correo de copia
        Dim clase_Correo As New Correo 'variable para la clase de correo
        Dim sbhtml As New StringBuilder
        Dim nombre As String = String.Empty
        Dim suc As String = String.Empty

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_CNFEXP_PAGARE"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FOLIO_PAGARE", Session("adVarChar"), Session("adParamInput"), 20, cmb_FolioPagare.SelectedItem.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        PAGARE = Session("rs").Fields("MASPAGARE").Value.ToString

        Session("Con").Close()

        If PAGARE = "ALERTA" Then

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("CLAVEEVENTO", Session("adVarChar"), Session("adParamInput"), 20, "LIMPAGARE")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EMAIL_EVENTOS"
            Session("rs") = Session("cmd").Execute()

            Do While Not Session("rs").EOF

                nombre = Session("rs").Fields("NOMBRE").Value.ToString
                suc = Session("NOMBRESUC")
                subject = "Alerta: Envío de más pagaré"
                sbhtml.Append("<table style='FONT-SIZE: 10pt; WIDTH: 850px; FONT-FAMILY: Tahoma;' cellpadding='1' cellspacing='1' border='0'>")
                sbhtml.Append("<tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: white; BACKGROUND-COLOR: #113964; TEXT-ALIGN: center' colspan='2'>SNTE</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
                sbhtml.Append("<tr><td>Estimado(a):  " + nombre + "</td></tr>")
                sbhtml.Append("<tr><td>Se informa que la sucursal :  " + suc + " ha alcanzado el mínimo de pagaré que debe tener, favor de enviar un nuevo lote de pagares a la sucursal correspondiente. </td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br />")
                sbhtml.Append("<br></br>")
                sbhtml.Append("<tr><td width='250'><b>Atentamente. " + Session("EMPRESA") + "</td></tr>")
                sbhtml.Append("</table>")
                sbhtml.Append("<br></br>")
                clase_Correo.Envio_email(sbhtml.ToString, subject, Session("rs").Fields("EMAIL").Value.ToString, cc)

                Session("rs").movenext()
            Loop

            Session("Con").Close()

        End If

    End Sub

    Private Sub PrellenadoPagare()

        Dim NewDocName As String = cmb_FolioPagare.SelectedItem.Text + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
        Dim ResultDocName As String = "Pagare-" & cmb_FolioPagare.SelectedItem.Text & ".pdf"

        Session("Con").Open()
        Try
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_DATOS_PAGARE_PRELLENADO"
            Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()


            Dim url As String = Session("rs").Fields("URL").Value.ToString
            Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH").ToString + "\DocPlantillas\Pagares\" + url)
                For Each col As ADODB.Field In Session("rs").Fields
                    If Not col.Name Like "URL" Then
                        worddoc.ReplaceText("{*" & col.Name & "*}", col.Value.ToString, False, RegexOptions.None)
                        worddoc.SaveAs(Server.MapPath("~") + "Word\" + NewDocName + ".docx")
                    End If
                Next
            End Using

        Catch ex As Exception
        Finally
            Session("Con").Close()
        End Try

        Dim result As String = ""
        Dim objNewWord As New Microsoft.Office.Interop.Word.Application()

        Try

            Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(Server.MapPath("~") + "Word\" + NewDocName + ".docx")
            objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", Server.MapPath("~") + "Word\"), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False,
                                                       Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
            objNewDoc.Close()
            'Elimina el Documento WORD ya Prellenado
            System.IO.File.Delete(Server.MapPath("~") + "Word\" + NewDocName + ".docx")

            ' Se genera el PDF
            Dim Filename As String = NewDocName + ".pdf"
            Dim FilePath As String = Server.MapPath("~") + "Word\"
            Dim fs As System.IO.FileStream
            fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
            Dim bytBytes(fs.Length) As Byte
            fs.Read(bytBytes, 0, fs.Length)
            fs.Close()

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
            lbl_EstatusPagare.Text = ex.ToString
        Finally
            objNewWord.Quit()
        End Try
        'Session("Con").Close()
        objNewWord = Nothing
    End Sub

#End Region

#Region "Contratos"

    Private Sub Tipo_contrato()

        'Lleno el Drop Down List con los tipos de productos existentes
        cmb_FormatoContrato.Items.Clear()
        Dim ELIJA As New ListItem("ELIJA", "-1")
        cmb_FormatoContrato.Items.Add(ELIJA)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_TIPO_CONTRATO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            'Llenado de los datos obtenidos
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("URL").Value.ToString)
            cmb_FormatoContrato.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub btn_Contrato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Contrato.Click

        Dim url As String = cmb_FormatoContrato.SelectedValue.ToString

        If Not url Like (-1).ToString Then

            If url = "CONVENIO" Then
                Dim NewDocName As String = cmb_FormatoContrato.SelectedItem.Text + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
                Dim ResultDocName As String = "CONTRATO " + cmb_FormatoContrato.SelectedItem.Text + "(FOLIO - " + CStr(Session("CVEEXPE")) + ").pdf"

                Using worddoc As Novacode.DocX = Novacode.DocX.Load(Session("APPATH") + "DocPlantillas\Contratos\" + url + ".docx")
                    ReemplazarEtiquetasContrato(worddoc)
                    worddoc.SaveAs(Server.MapPath("~") + "Word\" + NewDocName + ".docx")
                End Using

                Dim result As String = ""
                Dim objNewWord As New Microsoft.Office.Interop.Word.Application()

                Try

                    Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(Server.MapPath("~") + "Word\" + NewDocName + ".docx")
                    objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", Server.MapPath("~") + "Word\"), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False,
                                                           Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
                    objNewDoc.Close()
                    'Elimina el Documento WORD ya Prellenado
                    System.IO.File.Delete(Server.MapPath("~") + "Word\" + NewDocName + ".docx")

                    ' Se genera el PDF
                    Dim Filename As String = NewDocName + ".pdf"
                    Dim FilePath As String = Server.MapPath("~") + "Word\"
                    Dim fs As System.IO.FileStream
                    fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
                    Dim bytBytes(fs.Length) As Byte
                    fs.Read(bytBytes, 0, fs.Length)
                    fs.Close()

                    ActualizaExtatusContratoImpreso()

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
                    objNewWord.Quit()
                End Try

                objNewWord = Nothing
            Else
                Dim NewDocName As String = cmb_FormatoContrato.SelectedItem.Text + Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
                Dim ResultDocName As String = "CONTRATO " + cmb_FormatoContrato.SelectedItem.Text + "(FOLIO - " + CStr(Session("CVEEXPE")) + ").pdf"

                Using worddoc As Novacode.DocX = Novacode.DocX.Load(Server.MapPath("~") + "DocPlantillas\Contratos\" + url + ".docx")
                    ReemplazarEtiquetasContrato(worddoc)
                    worddoc.SaveAs(Server.MapPath("~") + "Word\" + NewDocName + ".docx")
                End Using

                Dim result As String = ""
                Dim objNewWord As New Microsoft.Office.Interop.Word.Application()

                Try

                    Dim objNewDoc As Microsoft.Office.Interop.Word.Document = objNewWord.Documents.Open(Server.MapPath("~") + "Word\" + NewDocName + ".docx")
                    objNewWord.ActiveDocument.ExportAsFixedFormat(String.Format("{0}" + NewDocName + ".pdf", Server.MapPath("~") + "Word\"), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False,
                                                           Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument)
                    objNewDoc.Close()
                    'Elimina el Documento WORD ya Prellenado
                    System.IO.File.Delete(Server.MapPath("~") + "Word\" + NewDocName + ".docx")

                    ' Se genera el PDF
                    Dim Filename As String = NewDocName + ".pdf"
                    Dim FilePath As String = Server.MapPath("~") + "Word\"
                    Dim fs As System.IO.FileStream
                    fs = System.IO.File.Open(FilePath + Filename, System.IO.FileMode.Open)
                    Dim bytBytes(fs.Length) As Byte
                    fs.Read(bytBytes, 0, fs.Length)
                    fs.Close()

                    ActualizaExtatusContratoImpreso()

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
                    objNewWord.Quit()
                End Try

                objNewWord = Nothing
            End If

        End If
    End Sub

    Private Sub ReemplazarEtiquetasContrato(ByRef doc As Novacode.DocX)
        Dim NUMCLI As String = ""
        NUMCLI = Session("PERSONAID").ToString
        NUMCLI = NUMCLI.PadLeft(5, "0")

        'doc.ReplaceText("[NO_EMPLEADO]", NUMCLI, False, RegexOptions.None)
        'doc.ReplaceText("{*FOLIO*}", Session("FOLIO").ToString, False, RegexOptions.None)
        'doc.ReplaceText("[NOM_EMPLEADO]", Session("CLIENTE").ToString, False, RegexOptions.None)
        'doc.ReplaceText("[NOM_PROD]", Session("PRODUCTO").ToString, False, RegexOptions.None)
        doc.ReplaceText("[ID_FOLIO]", Session("FOLIO").ToString, False, RegexOptions.None)

        Dim Producto As String = ""
        Dim Monto As String = ""
        Dim finalidad As String = ""
        Dim Monto_letra As String = ""
        Dim Monto_num_letra As String = ""
        Dim plazo As String = ""
        Dim Tipo_plazo As String = ""
        Dim Tasa_normal As String = ""
        Dim periodicidad2 As String = ""
        Dim periodicidad_unidad As String = ""
        Dim Cat As String = ""
        Dim Num_pagos As String = ""

        Dim abono As String = ""
        Dim mon_letra As String = ""
        Dim Comision_apertura As String = ""
        Dim Comision_apertura_letra As String = ""
        Dim Tasa_mora As String = ""
        Dim Tasa_normal_letra As String = ""
        Dim Tasa_mora_letra As String = ""

        Dim DiaLib As String = ""
        Dim MesLib As String = ""
        Dim AnoLib As String = ""

        Dim DirLib As String = ""

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_EXTRA_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Monto = Session("rs").Fields("MONTO").Value.ToString
            mon_letra = Session("rs").Fields("MONTO_LETRA").Value.ToString
            Monto_num_letra = Monto + " (" + mon_letra + ")"

            doc.ReplaceText("[MONTO_NUM_LETRA]", Monto_num_letra, False, RegexOptions.None)

        End If
        Session("Con").Close()

        Session("TOTAL") = Nothing

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CNFEXP_PDF_ENCABEZADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("TOTAL") = Session("rs").Fields("CAPITAL_TOTAL").Value.ToString
        End If
        Session("Con").Close()


        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_EXTRA_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            periodicidad2 = Session("rs").Fields("PERIODICIDAD").Value.ToString

        End If
        Session("Con").Close()

        Session("TIPO_CREDITO") = Nothing

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_NOMBRE_PRODUCTO_X_FOLIO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("NOM_PROD") = Session("rs").Fields("NOMBRE_PRODUCTO").Value.ToString()
            doc.ReplaceText("[NOM_PROD]", Session("NOM_PROD").ToString, False, RegexOptions.None)
        End If
        Session("Con").Close()

        Dim Destino As String
        Dim fecha_corte As String

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Producto = Session("rs").Fields("PRODUCTO").Value.ToString()
            Monto = Session("rs").Fields("MONTO").Value.ToString()
            Monto_letra = Session("rs").Fields("MONTO_LETRA").Value.ToString()
            plazo = Session("rs").Fields("PLAZO").Value.ToString()
            Tipo_plazo = Session("rs").Fields("TIPO_PLAZO").Value.ToString()
            finalidad = Session("rs").Fields("FINALIDAD").Value.ToString()
            Tasa_normal = Session("rs").Fields("TASA_NORMAL").Value.ToString()
            periodicidad_unidad = Session("rs").Fields("PERIODICIDAD_UNIDAD").Value.ToString()
            Cat = Session("rs").Fields("CAT").Value.ToString()
            Num_pagos = Session("rs").Fields("NUM_PAGOS").Value.ToString()
            Destino = Session("rs").Fields("DESTINO").Value.ToString()
            fecha_corte = Session("rs").Fields("FECHA_LIM").Value.ToString()
            DiaLib = Session("rs").Fields("DIALIB").Value.ToString()
            MesLib = Session("rs").Fields("MESLIB").Value.ToString()
            AnoLib = Session("rs").Fields("ANIOLIB").Value.ToString()

            DirLib = DiaLib + " de " + MesLib + " del " + AnoLib

            doc.ReplaceText("[FECHA_PAGO]", DirLib, False, RegexOptions.None)
            doc.ReplaceText("[CAT]", Cat, False, RegexOptions.None)
            doc.ReplaceText("[FINALIDAD]", finalidad, False, RegexOptions.None)
            doc.ReplaceText("[TASA]", Tasa_normal, False, RegexOptions.None)
        End If

        Session("Con").Close()

        Dim idPersona As String = ""
        Dim edoCivil As String = ""
        Dim Nombre_Cliente As String = ""
        Dim Domicilio_Cliente As String = ""
        Dim id_identificacion As String = ""
        Dim OCUPACION As String = ""

        Dim id_aval As String = ""
        Dim nombre_aval As String = ""
        Dim dir_aval As String = ""
        Dim ine_aval As String = ""
        Dim ocup_aval As String = ""

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CREDITO_ACTORES_EXTRA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            Do While Not Session("rs").eof

                If Session("rs").Fields("TIPO").Value.ToString() = "CLIENTE" Then

                    idPersona = Session("rs").Fields("NUMTRAB").Value.ToString()
                    Nombre_Cliente = Session("rs").Fields("NOMBRE").Value.ToString()
                    edoCivil = Session("rs").Fields("EDO_CIVIL").Value.ToString()
                    Domicilio_Cliente = Session("rs").Fields("DOMICILIO").Value.ToString()
                    id_identificacion = Session("rs").Fields("NUM_ID").Value.ToString()
                    OCUPACION = Session("rs").Fields("OCUPACION").Value.ToString()

                    doc.ReplaceText("[NOM_EMPLEADO]", Nombre_Cliente, False, RegexOptions.None)
                    doc.ReplaceText("[EDO_CIVIL]", edoCivil, False, ReaderOptions.None)
                    doc.ReplaceText("[DIR_EMPLEADO]", Domicilio_Cliente, False, ReaderOptions.None)
                    doc.ReplaceText("[OCUPACION]", OCUPACION, False, ReaderOptions.None)
                    doc.ReplaceText("[NO_EMPLEADO]", idPersona, False, ReaderOptions.None)
                    'doc.ReplaceText("[ID_OFICIAL]", id_identificacion, False, ReaderOptions.None)

                ElseIf Session("rs").Fields("TIPO").Value.ToString() = "AVAL" Then

                    id_aval = Session("rs").Fields("NUMTRAB").Value.ToString()
                    nombre_aval = Session("rs").Fields("NOMBRE").Value.ToString()
                    dir_aval = Session("rs").Fields("DOMICILIO").Value.ToString()
                    ine_aval = Session("rs").Fields("NUM_ID").Value.ToString()
                    ocup_aval = Session("rs").Fields("OCUPACION").Value.ToString()

                    doc.ReplaceText("[NOMBRE_AVAL]", nombre_aval, False, RegexOptions.None)
                    doc.ReplaceText("[NO_AVAL]", id_aval, False, ReaderOptions.None)
                    doc.ReplaceText("[DIR_AVAL]", dir_aval, False, ReaderOptions.None)
                    doc.ReplaceText("[OCUPACION_AVAL]", ocup_aval, False, ReaderOptions.None)
                    'doc.ReplaceText("[ID_OFI_AVAL]", "", False, ReaderOptions.None)

                End If

                Session("rs").movenext()
            Loop

        End If


        Dim FechaSis As String = ""
        Dim DiaSis As String = ""
        Dim MesSis As String = ""
        Dim AnoSis As String = ""

        Session("Con").Close()

        Session("DIA") = Nothing
        Session("MES") = Nothing
        Session("AÑO") = Nothing
        Session("NOMBRE_SUCURSAL") = Nothing
        Session("ESTADO") = Nothing
        Session("MUNICIPIO") = Nothing

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_SUCURSAL_PRELLENADO"
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 11, Session("SUCID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then

            DiaSis = Session("rs").Fields("FECHASIS").Value.ToString.Substring(0, 2)
            MesSis = Session("rs").Fields("MES").Value.ToString()
            AnoSis = Session("rs").Fields("FECHASIS").Value.ToString.Substring(6, 4)
            Session("NOMBRE_SUCURSAL") = Session("rs").Fields("NOMBRE").Value.ToString()
            Session("MUNICIPIO") = Session("rs").Fields("MUNICIPIO").Value.ToString()
            Session("ESTADO") = Session("rs").Fields("ESTADO").Value.ToString()
            FechaSis = DiaSis + " días del mes de " + MesSis + " del " + AnoSis
            doc.ReplaceText("[FECHA_SISTEMA]", FechaSis, False, RegexOptions.None)
        End If
        Session("Con").Close()

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)

    End Sub

    Private Sub ActualizaExtatusContratoImpreso()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "UPD_CNFEXP_CONTRATO"
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 11, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 11, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").Execute()

        Session("Con").Close()

    End Sub

#End Region

End Class