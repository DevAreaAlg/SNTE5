Imports System.DirectoryServices.Protocols.LdapConnection
Imports System.DirectoryServices.PropertyCollection
Imports System.Configuration.ConfigurationManager
Imports System.DirectoryServices.AccountManagement
Imports System.Data
Public Class LOGIN
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

    End Sub

    Private Function GeneraFecha() As String
        'Función que genera una cadena con la fecha actual

        Dim fecha As String
        fecha = ""
        Select Case Now.DayOfWeek
            Case DayOfWeek.Monday
                fecha = fecha + "Lunes "
            Case DayOfWeek.Tuesday
                fecha = fecha + "Martes "
            Case DayOfWeek.Wednesday
                fecha = fecha + "Miércoles "
            Case DayOfWeek.Thursday
                fecha = fecha + "Jueves "
            Case DayOfWeek.Friday
                fecha = fecha + "Viernes "
            Case DayOfWeek.Saturday
                fecha = fecha + "Sábado "
            Case DayOfWeek.Sunday
                fecha = fecha + "Domingo "
        End Select

        fecha = fecha + Now.Day.ToString + " de "

        Select Case Now.Month
            Case "1"
                fecha = fecha + "enero de "
            Case "2"
                fecha = fecha + "febrero de "
            Case "3"
                fecha = fecha + "marzo de "
            Case "4"
                fecha = fecha + "abril de "
            Case "5"
                fecha = fecha + "mayo de "
            Case "6"
                fecha = fecha + "junio de "
            Case "7"
                fecha = fecha + "julio de "
            Case "8"
                fecha = fecha + "agosto de "
            Case "9"
                fecha = fecha + "septiembre de "
            Case "10"
                fecha = fecha + "octubre de "
            Case "11"
                fecha = fecha + "noviembre de "
            Case "12"
                fecha = fecha + "diciembre de "
        End Select
        fecha += Now.Year.ToString

        Return fecha
    End Function

    Protected Sub btn_LogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_LogIn.Click

        lbl_ErrorLogIn.Text = ""

        If txt_Usuario.Text = "" Or txt_Password.Text = "" Then
            lbl_ErrorLogIn.Text = "Error: Campo(s) Vacío(s)."
            lbl_ErrorLogIn.Visible = True
            Exit Sub
        End If

        'If rad_LogInAD.Checked = True Then
        '    If ADLogin(txt_Usuario.Text, txt_Password.Text) = False Then
        '        lbl_ErrorLogIn.Text = "Las credenciales proporcionadas son incorrectas."
        '    Else
        '        Login(ViewState("LOGIN_USR"), ViewState("LOGIN_PWD"), ViewState("LOGIN_NOMBRES"), ViewState("LOGIN_APELLIDOS"), ViewState("LOGIN_MAIL"))
        '    End If
        'Else
        LocalLogin(txt_Usuario.Text, txt_Password.Text)
        'End If




    End Sub

    Public Function ADLogin(ByVal USR As String, ByVal PWD As String) As Boolean

        Dim validation As Boolean

        Using dirctoryEntry As New System.DirectoryServices.DirectoryEntry(ConfigurationManager.ConnectionStrings("LDAPConnectionString").ToString, USR, PWD)
            Try

                dirctoryEntry.AuthenticationType = System.DirectoryServices.AuthenticationTypes.Secure
                'dirctoryEntry.RefreshCache()
                Dim nativeObject As New Object
                nativeObject = dirctoryEntry.NativeGuid

                validation = False

                Dim mySearcher As New System.DirectoryServices.DirectorySearcher(dirctoryEntry)
                mySearcher.Filter = "(&(&((objectCategory=user)(sAMAccountName=" + USR + "))(memberOf=CN=SysCred,CN=Users,DC=inopsadmin,DC=net)))"
                mySearcher.PropertiesToLoad.Add("givenName")
                mySearcher.PropertiesToLoad.Add("sn")
                mySearcher.PropertiesToLoad.Add("mail")
                'mySearcher.PropertiesToLoad.Add("memberOf")

                For Each result In mySearcher.FindAll()
                    Dim hola As New System.DirectoryServices.DirectoryEntry
                    hola = result.GetDirectoryEntry()

                    ViewState("LOGIN_USR") = USR
                    ViewState("LOGIN_PWD") = PWD
                    ViewState("LOGIN_NOMBRES") = hola.Properties("givenname").Value.ToString()
                    ViewState("LOGIN_APELLIDOS") = hola.Properties("sn").Value.ToString()
                    ViewState("LOGIN_MAIL") = hola.Properties("mail").Value.ToString()
                    validation = True
                Next

            Catch DSex As System.DirectoryServices.DirectoryServicesCOMException
                validation = False

            Catch ex As Exception
                validation = False
            End Try
        End Using

        Return validation

    End Function

    Public Sub Login(ByVal USR As String, ByVal PWD As String, ByVal NOMBRES As String, ByVal APELLIDOS As String, ByVal CORREO As String)

        Try

            'CONFIGURO CONEXION, CREO IDENTIFICADOR DE SESION Y EJECUTO SP DE VALIDACION
            Session("Con") = CreateObject("ADODB.Connection")
            Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Session("Con").ConnectionTimeout = 240
            Session("LoggedIn") = False
            Session("Sesion") = Now.Day.ToString.PadLeft(2, "0") + Now.Month.ToString.PadLeft(2, "0") + Now.Year.ToString + "-" + Session("MascoreG").randkey(6)
            Session("Con").Open()
            Session("rs") = CreateObject("ADODB.Recordset")
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_LOGIN_AD"
            Session("parm") = Session("cmd").CreateParameter("LOGIN", Session("adVarChar"), Session("adParamInput"), 100, USR)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("PWD", Session("adVarChar"), Session("adParamInput"), 100, PWD)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("NOMBRES", Session("adVarChar"), Session("adParamInput"), 1000, NOMBRES)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("APELLIDOS", Session("adVarChar"), Session("adParamInput"), 1000, APELLIDOS)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("CORREO", Session("adVarChar"), Session("adParamInput"), 100, CORREO)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("rs") = Session("cmd").Execute()

            'PROCESAMOS LO QUE REGRESO SP
            Select Case Session("rs").Fields("EVENTO_ID").Value.ToString
                Case "1" 'INICIO DE SESION EXITOSO
                    Session("LoggedIn") = True
                    'CREAMOS VARIABLE DE SESION CON EL ID DEL USUARIO DEL SISTEMA
                    Session("USERID") = Session("rs").Fields("USUARIO_ID").Value.ToString
                    'CREAMOS VARIABLE DE SESION CON EL nombre DEL USUARIO DEL SISTEMA
                    Session("USERNOM") = Session("rs").Fields("USUARIO_NOMBRES").Value.ToString
                    'CREAMOS VARIABLE DE SESION CON LA SUCURSAL ASIGNADA AL USUARIO DEL SISTEMA
                    Session("SUCID") = Session("rs").Fields("SUC_ID").Value.ToString
                    Session("MAC") = Session("rs").Fields("MAC").Value
                    Session("ID_EQ") = Session("rs").Fields("ID_EQ").Value
                    Session("TIPOUSER") = Session("rs").Fields("TIPOUSER").Value
                    'CREAMOS VARIABLE DE SESION CON EL TIEMPO DE INACTIVIDAD ESTABLECIDO PARA EL USR
                    Session("TIEMPO") = Genera_Tiempo_Inactividad()
                    Session("NIVEL") = "" 'Nivel_Acceso_Usuario()
                    ' Session("FP") = verifica_biometrico(Session("USERID"), "USRLOG")
                    Session("FP") = "0"
                    Session("Con").Close()
                    Session("FP") = Nothing
                    Response.Redirect("NOTIFICACIONES.aspx")
                Case "2" 'INICIO DE SESION INCORRECTO
                    lbl_ErrorLogIn.Text = "Error: Contraseña inválida"
                    lbl_ErrorLogIn.Visible = True
                    Session("Con").Close()
                Case "5" 'HORARIO INVÁLIDO
                    lbl_ErrorLogIn.Text = "Error: Horario de uso inválido"
                    lbl_ErrorLogIn.Visible = True
                    Session("Con").Close()
                Case "7" 'CUENTA DESHABILITADA
                    lbl_ErrorLogIn.Text = "Error: La cuenta está deshabilitada"
                    lbl_ErrorLogIn.Visible = True
                    Session("Con").Close()
                Case "8" 'USUARIO NO REGISTRADO
                    lbl_ErrorLogIn.Text = "Error: Usuario inválido"
                    lbl_ErrorLogIn.Visible = True
                    Session("Con").Close()
                Case "9" 'USUARIO NO REGISTRADO
                    lbl_ErrorLogIn.Text = "Error: Equipo no autorizado para iniciar sesión"
                    lbl_ErrorLogIn.Visible = True
                    Session("Con").Close()
                Case "10" 'USUARIO YA LOGEADO EN EL SISTEMA
                    lbl_ErrorLogIn.Text = "Error: Ya existe una sesión activa para este usuario. Intente en " + Trim(Str(Genera_Tiempo_Bloqueo())) + " min."
                    lbl_ErrorLogIn.Visible = True
                    Session("Con").Close()

            End Select

        Catch ex As Exception

            Session("HOLA") = ex.Message

        End Try

    End Sub

    Public Sub LocalLogin(ByVal USR As String, ByVal PWD As String)

        'CONFIGURO CONEXION, CREO IDENTIFICADOR DE SESION Y EJECUTO SP DE VALIDACION
        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("LoggedIn") = False
        Session("Sesion") = Now.Day.ToString.PadLeft(2, "0") + Now.Month.ToString.PadLeft(2, "0") + Now.Year.ToString + "-" + Session("MascoreG").randkey(6)
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_LOGIN"
        Session("parm") = Session("cmd").CreateParameter("LOGIN", Session("adVarChar"), Session("adParamInput"), 15, USR)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("PWD", Session("adVarChar"), Session("adParamInput"), 15, PWD)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MACS", Session("adVarChar"), Session("adParamInput"), 500, hdn_mcs.Value) ' "74:2F:68:CC:AE:DF"  hdn_mcs.Value
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IP", Session("adVarChar"), Session("adParamInput"), 15, Session("IPuser").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION_ID", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        'PROCESAMOS LO QUE REGRESO SP
        Select Case Session("rs").Fields("EVENTO_ID").Value.ToString
            Case "1" 'INICIO DE SESION EXITOSO
                Session("LoggedIn") = True
                'CREAMOS VARIABLE DE SESION CON EL ID DEL USUARIO DEL SISTEMA
                Session("USERID") = Session("rs").Fields("USUARIO_ID").Value.ToString
                'CREAMOS VARIABLE DE SESION CON EL nombre DEL USUARIO DEL SISTEMA
                Session("USERNOM") = Session("rs").Fields("USUARIO_NOMBRES").Value.ToString
                'CREAMOS VARIABLE DE SESION CON LA SUCURSAL ASIGNADA AL USUARIO DEL SISTEMA
                Session("SUCID") = Session("rs").Fields("SUC_ID").Value.ToString
                Session("MAC") = Session("rs").Fields("MAC").Value
                Session("ID_EQ") = Session("rs").Fields("ID_EQ").Value
                Session("TIPOUSER") = Session("rs").Fields("TIPOUSER").Value
                'CREAMOS VARIABLE DE SESION CON EL TIEMPO DE INACTIVIDAD ESTABLECIDO PARA EL USR
                Session("TIEMPO") = Genera_Tiempo_Inactividad()
                Session("NIVEL") = "" 'Nivel_Acceso_Usuario()
                ' Session("FP") = verifica_biometrico(Session("USERID"), "USRLOG")
                Session("FP") = "0"
                Session("Con").Close()

                Session("FP") = Nothing
                Response.Redirect("NOTIFICACIONES.aspx")
                    'lbl_ErrorLogIn.Text = "Usuario Logeado correctamente"

            Case "2" 'INICIO DE SESION INCORRECTO
                lbl_ErrorLogIn.Text = "Error: Contraseña inválida"
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "3" 'RECHAZO DE INICIO DE SESION POR FECHA DE SISTEMA DESACTUALIZADA
                lbl_ErrorLogIn.Text = "Error: La Fecha del Sistema no ha sido actualizada "
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "4" 'CONTRASEÑA EXPIRO
                ' lbl_ErrorLogIn.Text = "Error: La contraseña ha expirado."
                lbl_ErrorLogIn.Visible = True
                Session("IDMOD") = 1
                Session("SUCID") = Session("rs").Fields("SUC_ID").Value.ToString
                Session("LoggedIn") = False
                Session("USERID") = Session("rs").Fields("USUARIO_ID").Value.ToString
                Session("MAC") = Session("rs").Fields("MAC").Value
                Session("ID_EQ") = Session("rs").Fields("ID_EQ").Value
                Session("TIPOUSER") = Session("rs").Fields("TIPOUSER").Value
                Session("TIEMPO") = Genera_Tiempo_Inactividad()
                Session("NIVEL") = Nivel_Acceso_Usuario()
                Session("Con").Close()

                Response.Redirect("CORE/SEGURIDAD/CORE_SEG_CONTRASENA_CAMBIO.aspx")
            Case "5" 'HORARIO INVÁLIDO
                lbl_ErrorLogIn.Text = "Error: Horario de uso inválido"
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "6" 'EXCEDIO INTENTOS DE VALIDACIÓN
                lbl_ErrorLogIn.Text = "Error: Ha excedido los intentos de validación permitidos"
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "7" 'CUENTA DESHABILITADA
                lbl_ErrorLogIn.Text = "Error: La cuenta está deshabilitada"
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "8" 'USUARIO NO REGISTRADO
                lbl_ErrorLogIn.Text = "Error: Usuario inválido"
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "9" 'USUARIO NO REGISTRADO
                lbl_ErrorLogIn.Text = "Error: Equipo no autorizado para iniciar sesión"
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()
            Case "10" 'USUARIO YA LOGEADO EN EL SISTEMA
                lbl_ErrorLogIn.Text = "Error: Ya existe una sesión activa para este usuario. Intente en " + Trim(Str(Genera_Tiempo_Bloqueo())) + " min."
                lbl_ErrorLogIn.Visible = True
                Session("Con").Close()

        End Select

    End Sub

    Protected Function Genera_Tiempo_Inactividad() As Integer
        'OBTENGO EL TIEMPO DE INACTIVIDAD ESTABLECIDO EN LA POLITICA ASIGNADA AL USUARIO
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIEMPO_INACTIVIDAD"
        Session("rs") = Session("cmd").Execute()

        Return Session("rs").Fields("TIEMPO").Value
    End Function

    Protected Function Nivel_Acceso_Usuario() As Integer
        'OBTENGO EL TIEMPO DE INACTIVIDAD ESTABLECIDO EN LA POLITICA ASIGNADA AL USUARIO
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID").ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_NIVEL_ACCESO_USUARIO"
        Session("rs") = Session("cmd").Execute()

        Return Session("rs").Fields("NIVEL").Value
    End Function

    'Protected Sub lnk_RepPre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_RepPre.Click
    '    Response.Redirect("RegistroOperacionPLD.aspx")
    'End Sub

    'Protected Sub lnk_reg_ax_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_reg_ax.Click
    '    'Response.Redirect("Biometric.aspx?mode=c")
    '    ClientScript.RegisterStartupScript(GetType(String), "Biometrico", "window.open(""BioMetric.aspx?mode=c"", ""BM"", ""center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px"");", True)
    '    'Response.Write("window.open(""BioMetric.aspx?mode=c"", ""BM"", ""center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px"");")
    'End Sub



    Protected Function Genera_Tiempo_Bloqueo() As Integer
        'OBTENGO EL TIEMPO DE BLOQUEO EN LA POLITICA ASIGNADA AL USUARIO IHG 2017-10-04
        Dim nTiempoBloqueo As Integer
        nTiempoBloqueo = 0
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_USER", Session("adVarChar"), Session("adParamInput"), 15, Session("rs").Fields("USUARIO_ID").Value.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_TIEMPO_BLOQUEO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof() Then
            nTiempoBloqueo = Session("rs").Fields("TIEMPO").Value
        End If
        Return nTiempoBloqueo
    End Function


End Class