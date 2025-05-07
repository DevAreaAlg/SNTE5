Imports System.IO
Imports Ionic.Zip
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_EXP_DIGITAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Documentación", "Consulta de Expedientes Digitales")

        If Not Me.IsPostBack Then
            Dim menuPanel As Panel
            menuPanel = CType(Master.FindControl("modulos_menu"), Panel)

            If Not menuPanel Is Nothing Then
                menuPanel.Visible = False
            End If

            DatosExpediente()
            MuestraDocumentosDig()
            'MuestraDocumentosDigDisp()
            llena_documentos_garantias()
            LlenaDictamenes()
            'MuestraDocumentosCarta()
            'MuestraDocumentosTransBanc()
            Session("VENGODE") = "ConsultaExp.aspx"
        End If
    End Sub

    'Obtiene los datos generales de un Expediente (Nombre de prospecto, producto y Fecha de creacion del expediente)
    Private Sub DatosExpediente()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_DATOS_EXPEDIENTE"

        Session("rs") = Session("cmd").Execute()

        If Not Session("rs").EOF Then
            lbl_Prospecto.Text = Session("rs").Fields("PROSPECTO").Value.ToString
            lbl_Producto.Text = Session("rs").Fields("PRODUCTO").Value.ToString
            lbl_Folio.Text = "Datos del Expediente: " + Session("rs").Fields("CVEEXP").Value.ToString
            'lbl_FechaCreado.Text = "FECHA CREADO: " + Session("rs").Fields("FECHA").Value.ToString
            'lbl_FaseDigit.Text = "FASE EN PROSPECCIÓN: " + Session("rs").Fields("FASE").Value.ToString
        Else
            lbl_status.Text = "Error: no hay datos disponibles"
        End If

        Session("Con").Close()

    End Sub

    'DBGRID Muestra documentos digitalizados
    Private Sub MuestraDocumentosDig()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdocumentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_DOCUMENTOS_DIGITALIZADOS"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtdocumentos, Session("rs"))
        DAG_DocDigit.DataSource = dtdocumentos
        DAG_DocDigit.DataBind()

        Session("Con").Close()

    End Sub

    Private Sub MuestraDocumentosDigDisp()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdocumentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONDISP_DOCUMENTOS_DIGITALIZADOS"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtdocumentos, Session("rs"))
        'se vacian los expedientes al formulario
        If dtdocumentos.Rows.Count > 0 Then
            pnl_disposicion.Visible = True
            dag_disposiciones.DataSource = dtdocumentos
            dag_disposiciones.DataBind()
        Else
            pnl_disposicion.Visible = False
        End If
        Session("Con").Close()

    End Sub

    Private Sub llena_documentos_garantias()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdocumentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_DOCUMENTOS_DIGITALIZADOS_GARANTIAS"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtdocumentos, Session("rs"))
        Session("Con").Close()
        If dtdocumentos.Rows.Count > 0 Then
            pnl_doc_gar.Visible = True
            dag_doc_gar.DataSource = dtdocumentos
            dag_doc_gar.DataBind()
        Else
            pnl_doc_gar.Visible = False
        End If
    End Sub

    Private Sub LlenaDictamenes()
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtsesiones As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFEXP_ACTA_COMITE"
        Session("rs") = Session("cmd").Execute()
        'se agregan los expedientes a una tabla en memoria
        custDA.Fill(dtsesiones, Session("rs"))
        Session("Con").Close()
        'se vacian los expedientes al formulario
        If dtsesiones.Rows.Count > 0 Then
            pnl_dictamenes.Visible = True
            dag_dictamenes.DataSource = dtsesiones
            dag_dictamenes.DataBind()
        Else
            pnl_dictamenes.Visible = False
        End If
    End Sub

    Private Sub MuestraDocumentosCarta()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdocumentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_DOCUMENTOS_DIGITALIZADOS_CCRED"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtdocumentos, Session("rs"))
        'se vacian los expedientes al formulario
        If dtdocumentos.Rows.Count > 0 Then
            pnl_carta.Visible = True
            DAG_CARTA.DataSource = dtdocumentos
            DAG_CARTA.DataBind()
        Else
            pnl_carta.Visible = False
        End If

        Session("Con").Close()

    End Sub

    Private Sub MuestraDocumentosTransBanc()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtdocumentos As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_MOSTRAR_IMAGEN_TRANS_BANC_DIG"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dtdocumentos, Session("rs"))
        'se vacian los expedientes al formulario
        If dtdocumentos.Rows.Count > 0 Then
            pnl_transbanc.Visible = True
            dag_transbanc.DataSource = dtdocumentos
            dag_transbanc.DataBind()
        Else
            pnl_transbanc.Visible = False
        End If

        Session("Con").Close()

    End Sub

    Private Sub dag_carta_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_CARTA.ItemCommand
        If (e.CommandName = "VER") Then
            Session("DOCUMENTO_DIGITALIZADO") = e.Item.Cells(0).Text
            Session("IDCARTA") = e.Item.Cells(3).Text
            Response.Redirect("MostrarDigitCcred.aspx")
        End If
    End Sub

    Private Sub dag_doc_gar_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_doc_gar.ItemCommand
        If (e.CommandName = "VER") Then
            Session("CVEGARANTIA") = (e.Item.Cells(1).Text)
            Session("TIPOGARANTIA") = (e.Item.Cells(0).Text)
            Session("NOMBRE_DOCUMENTO") = (e.Item.Cells(3).Text)
            Response.Redirect("MostrarDigitGarantias.aspx")
        End If
    End Sub

    Protected Sub DAG_DocDigit_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_DocDigit.ItemCommand

        If (e.CommandName = "VER") Then

            Session("DOCUMENTO_DIGITALIZADO") = e.Item.Cells(0).Text
            Response.Redirect("../../DIGITALIZADOR/DIGI_MOSTRAR.aspx")

        End If

    End Sub

    Protected Sub dag_transbanc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_transbanc.ItemCommand

        If (e.CommandName = "VER") Then
            Session("IDTRANS") = e.Item.Cells(0).Text
            Response.Redirect("MostrarDigitTransBanc.aspx")

        End If

    End Sub

    Protected Sub DAG_DISPOSICIONES_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_disposiciones.ItemCommand

        If (e.CommandName = "VER") Then

            Session("DOCUMENTO_DIGITALIZADO") = e.Item.Cells(0).Text
            Session("IDDISP") = e.Item.Cells(3).Text
            Response.Redirect("MostrarDigitPagare.aspx")

        End If

    End Sub

    Private Sub dag_dictamenes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_dictamenes.ItemCommand
        If (e.CommandName = "VER") Then
            Session("IDSESCOMIT") = (e.Item.Cells(0).Text)
            Session("NOMBRE_DOCUMENTO") = (e.Item.Cells(1).Text)
            Response.Redirect("MostrarDigitComite.aspx")
        End If
    End Sub

    'Protected Sub btn_ver_todo_Click(sender As Object, e As EventArgs) Handles btn_ver_todo.Click

    'Dim FOLIO As String = Session("FOLIO")
    'm Path As String = Session("APPATH").ToString + "DOCUMENTOS_DIGITALIZADOS"


    '   Path = Path + "\EXPEDIENTE_" + Session("FOLIO")

    'If Not Directory.Exists(Path) Then
    '   Directory.CreateDirectory(Path)
    'End If


    ' Session("Con").Open()
    ' Session("cmd") = New ADODB.Command()
    ' Session("cmd").ActiveConnection = Session("Con")
    ' Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    ' Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, FOLIO)
    ' Session("cmd").Parameters.Append(Session("parm"))
    'Session("cmd").CommandText = "SEL_COB_MOSTRAR_DOCUMENTOS"
    ' Session("rs") = Session("cmd").Execute()
    'Do While Not Session("rs").EOF
    'lbl_status.Text = "PATH: " + Path + " FOLIO: " + FOLIO + " CLAVE DOC: " + Session("rs").Fields("CVEDOC").Value.ToString + " ID_DOCU: " + Session("rs").Fields("ID_DOC").Value.ToString + " NOMBRE_DOC: " + Session("rs").Fields("NOMBRE").Value.ToString + " CATEGORIA: " + Session("rs").Fields("CATEGORIA").Value.ToString
    ' VerDocumentoDigitalizado(Path, FOLIO, Session("rs").Fields("CVEDOC").Value.ToString, Session("rs").Fields("ID_DOC").Value.ToString, Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("CATEGORIA").Value.ToString)
    '  Session("rs").movenext()
    'Loop
    'Session("Con").Close()

    ' Comprimir(Path)

    'Dim ruta_Save = Path + "\DOCUMENTOS_FOLIO_" + Session("FOLIO") + ".zip"
    'Dim file As System.IO.FileInfo = New System.IO.FileInfo(ruta_Save) '-- if the file exists on the server
    'If file.Exists Then 'set appropriate headers
    '   Response.Clear()
    '  Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
    ' Response.AddHeader("Content-Length", file.Length.ToString())
    ' Response.ContentType = "application/octet-stream"
    ' Response.WriteFile(file.FullName)
    'Response.End() 'if file does not exist
    'Else
    'lbl_status.Text = "No existen documentos digitalizados"
    'End If
    ' End Sub

    Private Sub VerDocumentoDigitalizado(ByVal Path As String, ByVal FOLIO As String, ByVal CVEDOC As String, ByVal IDDOCUMENTO As String, NOMBRE As String, CATEGORIA As String)

        If File.Exists(Path + "\" + NOMBRE + ".pdf") Then
            DelHDFile(Path + "\" + NOMBRE + ".pdf")
        End If
        If File.Exists(Path + "\DOCUMENTOS_FOLIO_" + Session("FOLIO") + ".zip") Then
            DelHDFile(Path + "\DOCUMENTOS_FOLIO_" + Session("FOLIO") + ".zip")
        End If

        If CATEGORIA = "EXP" Then
            GuardaArchivos_Digitalizados(FOLIO, CVEDOC, NOMBRE, Path)
        End If
        If CATEGORIA = "CCRED" Then
            GuardaArchivos_CartaCredito(FOLIO, CVEDOC, IDDOCUMENTO, NOMBRE, Path)
        End If
        If CATEGORIA = "REV" Then
            GuardaArchivos_LineasRevolventes(FOLIO, CVEDOC, IDDOCUMENTO, NOMBRE, Path)
        End If
        If CATEGORIA = "GTIA" Then
            GuardaArchivos_Garantias(FOLIO, CVEDOC, "FOTO", NOMBRE, Path)
        End If
        If CATEGORIA = "COM" Then
            GuardaArchivos_Comite(FOLIO, CVEDOC, NOMBRE, Path)
        End If
        If CATEGORIA = "TRANS" Then
            GuardaArchivos_TransBanc(FOLIO, CVEDOC, NOMBRE, Path)
        End If

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New System.IO.FileInfo(File)
        If (fi.Attributes And System.IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor System.IO.FileAttributes.ReadOnly
        End If

        System.IO.File.Delete(File)
    End Sub

    Private Sub GuardaArchivos_Digitalizados(ByVal Folio As Integer, ByVal CVEDOC As String, ByVal nombreDocumento As String, ByVal RutaArchivo As String)
        Dim server = ConfigurationManager.AppSettings.[Get]("server")
        Dim database = ConfigurationManager.AppSettings.[Get]("db")
        Dim user = ConfigurationManager.AppSettings.[Get]("user")
        Dim password = ConfigurationManager.AppSettings.[Get]("pass")

        Dim strConnString As String
        'strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + ";Initial Catalog=" + database + ";User ID=" + user + ";Pwd=" + password + ";"
        strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + "; Server = " + server + " ;Initial Catalog= " + database + " ;User ID= " + user + " ;Pwd= " + password + ";"

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure
        sqlCmdObj.Parameters.AddWithValue("@DOCUMENTO", CVEDOC)
        sqlCmdObj.Parameters.AddWithValue("@FOLIO", Folio)
        sqlConnection.Open()
        Dim myReader As SqlDataReader = sqlCmdObj.ExecuteReader(CommandBehavior.CloseConnection)
        While myReader.Read()
            Dim urldigi As String = ConfigurationManager.AppSettings.[Get]("urldigi")
            'Dim archivo As String = RutaDigitaliza + "\" + Nombre + "." + Ext
            Dim cPath As String = urldigi + "\"
            Dim FilePath As String = cPath

            Dim archivo2 As String = myReader.GetString(0)

            Dim fs As System.IO.FileStream
            fs = System.IO.File.Open(FilePath + archivo2, System.IO.FileMode.Open)
            Dim bytBytes(fs.Length) As Byte
            fs.Read(bytBytes, 0, fs.Length)
            fs.Close()
            File.WriteAllBytes(RutaArchivo + "\" + nombreDocumento + "(" + CVEDOC + ")" + ".pdf", bytBytes)
        End While
        myReader.Close()
        sqlConnection.Close()


        'Dim strConnString As String 'INSERTO BINARIO EN BD
        'strConnString = ConfigurationManager.ConnectionStrings("ConnectionStringDotNet").ToString
        'Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        'Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN", sqlConnection)
        ''Stored Procedure 
        'sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure
        ''Parametros para la inserción del Stored Procedure

        'sqlCmdObj.Parameters.AddWithValue("@DOCUMENTO", CVEDOC)
        'sqlCmdObj.Parameters.AddWithValue("@FOLIO", Folio)


        'sqlConnection.Open()

        'Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        'sdrRecordset.Read()

        'Dim iByteLength As Long

        'iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)

        'Dim byFileData(iByteLength) As Byte

        'sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        'sdrRecordset.Close()
        'sqlConnection.Close()

        ''Permite guardar en system los archivos
        'File.WriteAllBytes(RutaArchivo + "\" + nombreDocumento + "(" + CVEDOC + ")" + ".pdf", byFileData)

    End Sub

    Private Sub GuardaArchivos_Garantias(ByVal Folio As Integer, ByVal CVEDOC As String, ByVal nombreDocumento As String, ByVal tipoGarantia As String, ByVal RutaArchivo As String)
        'Dim extension As String
        Dim strConnString As String

        strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + ";Initial Catalog=" + Session("DataBase") + ";User ID=" + Session("DataBaseUsr") + ";Pwd=" + Session("DataBasePwd") + ";"

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_GARANTIA", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure
        sqlCmdObj.Parameters.AddWithValue("@CVE_GARANTIA", CVEDOC)
        sqlCmdObj.Parameters.AddWithValue("@TIPO_GARANTIA", tipoGarantia)
        sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOCUMENTO", nombreDocumento)
        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        'extension = sdrRecordset.GetString(1)

        Dim iByteLength As Long
        iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)

        Dim byFileData(iByteLength) As Byte
        sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        sdrRecordset.Close()
        sqlConnection.Close()

        'Permite guardar en system los archivos
        File.WriteAllBytes(RutaArchivo + "\" + "FOTO" + "(" + CVEDOC + ")" + ".pdf", byFileData)


    End Sub

    Private Sub GuardaArchivos_CartaCredito(ByVal Folio As Integer, ByVal CVEDOC As String, ByVal idDocumento As String, ByVal nombreDocumento As String, ByVal RutaArchivo As String)
        Dim strConnString As String

        strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + ";Initial Catalog=" + Session("DataBase") + ";User ID=" + Session("DataBaseUsr") + ";Pwd=" + Session("DataBasePwd") + ";"

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_CCRED", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@FOLIO", Folio)
        sqlCmdObj.Parameters.AddWithValue("@IDCARTA", idDocumento)
        sqlCmdObj.Parameters.AddWithValue("@DOCUMENTO", CVEDOC)
        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        Dim iByteLength As Long
        iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)
        Dim byFileData(iByteLength) As Byte
        sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        sdrRecordset.Close()
        sqlConnection.Close()

        'Permite guardar en system los archivos
        File.WriteAllBytes(RutaArchivo + "\" + nombreDocumento + "(" + CVEDOC + ") CARTA NUM " + idDocumento + ".pdf", byFileData)

    End Sub

    Private Sub GuardaArchivos_LineasRevolventes(ByVal Folio As Integer, ByVal CVEDOC As String, ByVal idDocumento As String, ByVal nombreDocumento As String, ByVal RutaArchivo As String)
        Dim strConnString As String

        strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + ";Initial Catalog=" + Session("DataBase") + ";User ID=" + Session("DataBaseUsr") + ";Pwd=" + Session("DataBasePwd") + ";"

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_PAGARE_DISP", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@FOLIO", Folio)
        sqlCmdObj.Parameters.AddWithValue("@IDDISP", idDocumento)
        sqlCmdObj.Parameters.AddWithValue("@DOCUMENTO", CVEDOC)
        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        Dim iByteLength As Long
        iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)
        Dim byFileData(iByteLength) As Byte
        sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        sdrRecordset.Close()
        sqlConnection.Close()

        'Permite guardar en system los archivos
        File.WriteAllBytes(RutaArchivo + "\" + nombreDocumento + "(" + CVEDOC + ") DISPOSICION NUM " + idDocumento + ".pdf", byFileData)

    End Sub

    Private Sub GuardaArchivos_Comite(ByVal Folio As Integer, ByVal CVEDOC As String, ByVal nombreDocumento As String, ByVal RutaArchivo As String)
        'Dim extension As String
        Dim strConnString As String

        strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + ";Initial Catalog=" + Session("DataBase") + ";User ID=" + Session("DataBaseUsr") + ";Pwd=" + Session("DataBasePwd") + ";"

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_SESCOMITE", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@IDSESION", CVEDOC)
        sqlCmdObj.Parameters.AddWithValue("@NOMBRE_DOCUMENTO", nombreDocumento)
        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        'extension = sdrRecordset.GetString(1)

        Dim iByteLength As Long
        iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)

        Dim byFileData(iByteLength) As Byte
        sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        sdrRecordset.Close()
        sqlConnection.Close()
        File.WriteAllBytes(RutaArchivo + "\" + nombreDocumento + "(" + CVEDOC + ")" + ".pdf", byFileData)
    End Sub

    Private Sub GuardaArchivos_TransBanc(ByVal Folio As Integer, ByVal CVEDOC As String, ByVal nombreDocumento As String, ByVal RutaArchivo As String)
        'Dim extension As String
        Dim strConnString As String

        strConnString = "Data Source=" + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + ";Initial Catalog=" + Session("DataBase") + ";User ID=" + Session("DataBaseUsr") + ";Pwd=" + Session("DataBasePwd") + ";"

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(strConnString)
        Dim sqlCmdObj As New System.Data.SqlClient.SqlCommand("SEL_MOSTRAR_IMAGEN_TRANS_BANC", sqlConnection)
        sqlCmdObj.CommandType = System.Data.CommandType.StoredProcedure

        sqlCmdObj.Parameters.AddWithValue("@FOLIO", Folio)
        sqlCmdObj.Parameters.AddWithValue("@IDTRANS", CVEDOC)
        sqlConnection.Open()

        Dim sdrRecordset As System.Data.SqlClient.SqlDataReader = sqlCmdObj.ExecuteReader()
        sdrRecordset.Read()

        'extension = sdrRecordset.GetString(1)

        Dim iByteLength As Long
        iByteLength = sdrRecordset.GetBytes(0, 0, Nothing, 0, 0)

        Dim byFileData(iByteLength) As Byte
        sdrRecordset.GetBytes(0, 0, byFileData, 0, iByteLength - 1)

        sdrRecordset.Close()
        sqlConnection.Close()
        File.WriteAllBytes(RutaArchivo + "\" + nombreDocumento + "(" + CVEDOC + ")" + ".pdf", byFileData)
    End Sub

    Private Sub Comprimir(ByVal Ruta As String)

        Dim itempaths As String() = New String() {Ruta}
        Using zip As ZipFile = New ZipFile()
            For i = 0 To itempaths.Length - 1
                zip.AddItem(itempaths(i), "")
            Next
            zip.Save(Ruta + "\DOCUMENTOS_FOLIO_" + Session("FOLIO") + ".zip")
        End Using
    End Sub

End Class