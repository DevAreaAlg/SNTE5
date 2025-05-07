Imports System.IO
Imports Ionic.Zip
Imports System.Data
Imports System.Data.SqlClient
Public Class CRED_EXP_CONSULTAEXP_DIGITAL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Consulta de Expediente Digital", "Consulta de Expediente Digital")

        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Session("idperbusca") = Nothing
            Empresa()

            If Session("VENGODE") = "ConsultaExp.aspx" Or Session("VENGODE") = "UNI_RED_SOCIAL.aspx" Or Session("VENGODE") = "CRED_EXP_EXP_DIGITAL.aspx" Or Session("VENGO") = "DIGI_GLOBAL" And Not Session("PERSONAID") Is Nothing Then

                txt_IdCliente.Text = Session("PERSONAID").ToString
                tbx_rfc.Text = Session("NUMTRAB").ToString
                Session("CLIENTE") = Session("PROSPECTO").ToString
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
                div_NombrePersonaBusqueda.Visible = True
                MuestraExpedientes()
                'MuestraDocumentosDig()
                pnl_expedientes.Visible = True

                folderA(div_selCliente, "down")
                folderA(pnl_expedientes, "down")



                'DetalleExpediente()
                ' lnk_persona.Attributes.Add("OnClick", "ResumenPersona()")

                Session("idperbusca") = Nothing
                Session("VENGODE") = Nothing
            End If
        End If
        'validaPersona()
        txt_IdCliente.Attributes.Add("onkeydown", "ClickBotonBusqueda('" + txt_IdCliente.ClientID + "','" + lnk_Continuar.ClientID + "')")
        lnk_BusquedaPersona.Attributes.Add("OnClick", "busquedapersonafisica()")

        If Session("idperbusca") <> Nothing Then
            tbx_rfc.Text = Session("idperbusca").ToString
            Session("CLIENTE") = Session("PROSPECTO").ToString
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            Session("idperbusca") = Nothing
            validaPersona()
        End If

        lbl_statusc.Text = ""
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "ScriptPaneles", "$('.panel_folder_toogle').click(function(event) { var folder_content=$(this).parent().siblings('.panel-body').children('.panel-body_content');if($(this).hasClass('up')){$(this).removeClass('up');$(this).addClass('down');folder_content.show('6666',null);$(this).parent().css({'background': '#696462 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px' });}else if($(this).hasClass('down')){$(this).removeClass('down');folder_content.hide('333',null);$(this).addClass('up');$(this).parent().css({ 'background': '#696462 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px'  });}});", True)
    End Sub

    Private Sub BuscarIDCliente()
        'Busca el ID de Cliente que el usuario ingreso y verifica si existe o no
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, txt_IdCliente.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_NOMBRE_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        If Not Session("rs").eof Then
            Session("CLIENTE") = Session("rs").fields("PROSPECTO").value.ToString
            Session("TIPOPER") = Session("rs").fields("TIPOPER").value.ToString
        End If
        Session("Con").Close()

        If Existe = 0 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: Persona con datos incompletos"
            txt_IdCliente.Text = ""
            lbl_NombrePersonaBusqueda.Text = ""
            folderA(div_selCliente, "down")
            pnl_expedientes.Visible = False
            folderA(pnl_expedientes, "up")
            'pnl_cnfexp.Visible = False
            'folderA(pnl_cnfexp, "up")

        ElseIf Existe = -1 Then
            Session("idperbusca") = ""
            lbl_statusc.Text = "Error: No existe el número de trabajador"
            lbl_NombrePersonaBusqueda.Text = ""
            folderA(div_selCliente, "down")
            pnl_expedientes.Visible = False
            folderA(pnl_expedientes, "up")
            'pnl_cnfexp.Visible = False
            'folderA(pnl_cnfexp, "up")
            Session("PERSONAID") = txt_IdCliente.Text
        Else
            lbl_statusc.Text = ""
            Session("PERSONAID") = txt_IdCliente.Text
            lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString
            div_NombrePersonaBusqueda.Visible = True
            MuestraExpedientes()
            pnl_expedientes.Visible = True
            folderA(div_selCliente, "up")
            folderA(pnl_expedientes, "down")
            'folderA(pnl_cnfexp, "up")
        End If
    End Sub

    Private Sub MuestraExpedientes()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSUARIO", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CONEXP_DIGITALIZA_DOCUMENTOS"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtexpedientes, Session("rs"))
        dag_Expendientes.DataSource = dtexpedientes
        dag_Expendientes.DataBind()


    End Sub

    Private Sub MuestraReest()

        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dtexpedientes As New Data.DataTable()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_REESTRUCTURAS_PLANPAGO"
        Session("rs") = Session("cmd").Execute()

        custDA.Fill(dtexpedientes, Session("rs"))
        'dag_reest.DataSource = dtexpedientes
        'dag_reest.DataBind()

        Session("Con").Close()

    End Sub

    Function tipo_persona() As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 10, Session("PERSONAID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_TIPO_PERSONA"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("TIPO").value.ToString
        Session("Con").Close()
        Return tipo
    End Function

    Function tipo_exp(ByVal folio As Integer) As String
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, folio)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIPO_EXP"
        Session("rs") = Session("cmd").Execute()
        Dim tipo As String = Session("rs").Fields("CLAVE").value.ToString
        Session("Con").Close()
        Return tipo
    End Function

    Private Sub DAG_Expedientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dag_Expendientes.ItemCommand
        If (e.CommandName = "CONSULTAR") Then
            pnl_editar.Visible = True

            Session("FOLIO") = e.Item.Cells(0).Text
            Session("CVEEXP") = e.Item.Cells(1).Text
            Session("PRODUCTO") = e.Item.Cells(2).Text
            Session("PROSPECTO") = Session("CLIENTE")

            MuestraReest()
            MuestraDocumentosDig()

        End If
        '    pnl_cnfexp.Visible = True
        folderA(div_selCliente, "up")
        folderA(pnl_expedientes, "up")
        'folderA(pnl_cnfexp, "down")

    End Sub


    Protected Sub lnk_Continuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Continuar.Click
        validaPersona()
    End Sub

    Private Sub validaPersona()
        lbl_statusc.Text = ""
        obtieneId()
        ' si no ha ingresado un id el usuario tomara el id de una busqueda
        If txt_IdCliente.Text = "" Then
            If Session("idperbusca") = Nothing Then
                lbl_statusc.Text = "Error: número de trabajador incorrecto"

            Else
                Session("CLIENTE") = Session("PROSPECTO")
                Session("PROSPECTO") = Nothing
                lbl_NombrePersonaBusqueda.Text = Session("CLIENTE").ToString

                div_NombrePersonaBusqueda.Visible = True
            End If
        Else
            Session("idperbusca") = Nothing
            'si el usuario ingreso un id de cliente o lo busco,  se verifica que existe
            BuscarIDCliente()

        End If
    End Sub

    Private Sub obtieneId()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("RFCPERSONA", Session("adVarChar"), Session("adParamInput"), 20, tbx_rfc.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_ID_PERSONA_X_RFC"
        Session("rs") = Session("cmd").Execute()

        Dim Existe As Integer = Session("rs").fields("EXISTE").value.ToString
        Dim idp As Integer = Session("rs").fields("IDPERSONA").value.ToString

        If Existe = -1 Then
            Session("idperbusca") = ""
            txt_IdCliente.Text = ""


        Else
            lbl_statusc.Text = ""
            txt_IdCliente.Text = CStr(idp)
            Session("NUMTRAB") = tbx_rfc.Text

        End If

        Session("Con").Close()
    End Sub

    'Obtengo el nombre de la empresa
    Private Sub Empresa()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_EMPRESA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("EMPRESA") = Session("rs").fields("RAZON").value.ToString
        End If
        Session("Con").Close()

    End Sub

    Private Sub Limpiavariables()
        Session("idperbusca") = Nothing 'variable de sesion de el modulo de busqueda de persona
        Session("PROSPECTO") = Nothing
        Session("PERSONAID") = Nothing
        Session("TIPOPER") = Nothing
        Session("TASA") = Nothing
        Session("INDICE") = Nothing
        Session("MONTO") = Nothing
        Session("PLAZO") = Nothing
        Session("UPLAZO") = Nothing
        Session("PERIODO") = Nothing
        Session("UPERIODO") = Nothing
        Session("FECHA") = Nothing
        Session("OPCION") = Nothing
        Session("CLASIFICACION") = Nothing
        Session("TIPOPLANPAGO") = Nothing
        Session("CAT") = Nothing
        Session("OPCION") = Nothing
        Session("C_COBRO") = Nothing
        Session("C_TIEMPO") = Nothing
        Session("C_TOTAL") = Nothing
        Session("C_IVA") = Nothing
    End Sub



    Private Sub verificafacultad()

        Dim facultad As Integer

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FOLIO", Session("adVarChar"), Session("adParamInput"), 10, Session("FOLIO"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_VERIFICA_FACULTAD_BLOQUEO_EXPEDIENTE"
        Session("rs") = Session("cmd").Execute()

        facultad = Session("rs").fields("FACULTAD").VALUE.ToString

        'If facultad = "1" Then
        '    lnk_bloqueo.Enabled = True
        'Else
        '    lnk_bloqueo.enabled = False
        'End If

        Session("Con").close()

    End Sub

    Private Sub limpiamodal()
        Session("OPCION") = Nothing
    End Sub

    Sub folderA(ByRef pnl As HtmlGenericControl, ByVal accion As String)

        Dim head As HtmlGenericControl = pnl.FindControl("head_" + pnl.ID)
        Dim toogle As HtmlGenericControl = pnl.FindControl("toogle_" + pnl.ID)
        Dim content As HtmlGenericControl = pnl.FindControl("content_" + pnl.ID)


        content.Attributes("class") = content.Attributes("class").Replace("init_show", "")

        If accion.Equals("down") Then
            head.Attributes.CssStyle.Add("background", "#696462 !important")
            head.Attributes.CssStyle.Add("color", "#fff")
            head.Attributes.CssStyle.Add("border", "solid 1px transparent")
            head.Attributes.CssStyle.Add("border-radius", " 4px 4px 0px 0px")
            content.Attributes.CssStyle.Add("display", "block")
        End If
        If accion.Equals("up") Then
            head.Attributes.CssStyle.Add("background", "#696462 !important")
            head.Attributes.CssStyle.Add("color", "inherit")
            head.Attributes.CssStyle.Add("border", "solid 1px #c0cdd5")
            head.Attributes.CssStyle.Add("border-radius", "4px")
            content.Attributes.CssStyle.Add("display", "none")
        End If

        toogle.Attributes("class") = toogle.Attributes("class").Replace("down", "")
        toogle.Attributes("class") = toogle.Attributes("class").Replace("up", "")
        toogle.Attributes("class") = toogle.Attributes("class") & " " & accion

    End Sub
    ''
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
        Session("cmd").CommandText = "SEL_CONEXP_DOCUMENTOS_DIG"
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



    Protected Sub DAG_DocDigit_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DAG_DocDigit.ItemCommand

        If (e.CommandName = "VER") Then

            Session("DOCUMENTO_DIGITALIZADO") = e.Item.Cells(0).Text
            Response.Redirect("../../DIGITALIZADOR/DIGI_MOSTRAR.aspx")

        End If

        If (e.CommandName = "EDITAR") Then
            Session("VENGODE") = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx"
            Session("FOLIO") = Session("FOLIO")
            Session("DOCUMENTO_DIGITALIZADO_GRAL") = e.Item.Cells(1).Text
            Session("NOMBRE_ARCHIVO") = e.Item.Cells(2).Text
            Session("CLAVE") = e.Item.Cells(0).Text
            Session("OTRO") = "NO"
            Session("FECHA") = CStr(Format(CDate(Today), "dd/MM/yyyy"))
            Session("ESTATUS_EXPEDIENTE") = "COMITE"
            Response.Redirect("../../DIGITALIZADOR/DIGI_GLOBAL.aspx")

        End If

    End Sub


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


    Private Sub Comprimir(ByVal Ruta As String)

        Dim itempaths As String() = New String() {Ruta}
        Using zip As ZipFile = New ZipFile()
            For i = 0 To itempaths.Length - 1
                zip.AddItem(itempaths(i), "")
            Next
            zip.Save(Ruta + "\DOCUMENTOS_FOLIO_" + Session("FOLIO") + ".zip")
        End Using
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

    Protected Sub lnk_digitalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_digitalizar.Click
        Session("VENGODE") = "CRED_EXP_CONSULTAEXP_DIGITAL.aspx"
        Session("DOCUMENTO_DIGITALIZADO_GRAL") = 9
        Session("CLAVE") = 0
        Session("FOLIO") = Session("FOLIO")
        Session("ESTATUS_EXPEDIENTE") = "COMITE"
        Session("DOCUMENTOID") = 0
        Session("NOMBRE_ARCHIVO") = ""
        Session("OTRO") = "SI"
        Session("FECHA") = CStr(Format(CDate(Today), "dd/MM/yyyy"))
        Response.Redirect("~/DIGITALIZADOR/DIGI_GLOBAL.aspx")
    End Sub

End Class