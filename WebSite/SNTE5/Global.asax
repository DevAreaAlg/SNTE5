<%@ Application Language="VB" %>
<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup    
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        'en caso de error en la aplicacion lo proceso en la pagina de error     
        'Server.Transfer("ErrorPage.aspx")
        'mientras este en desarrollo y deshabilitado error page remuevo aqui variables sesion
        'Session.RemoveAll()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started

        Session("adCmdStoredProc") = 4
        Session("adInteger") = 3
        Session("adParamInput") = 1
        Session("adVarChar") = 200
        Session("adChar") = 129
        Session("adDate") = 7
        Session("adBinary") = 128

        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CONFGLOBAL_MASCORE"


        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            'Obtiene la ruta global de archivos
            'Session("APPATH") = Session("rs").Fields("RUTA").Value.ToString
            Session("APPATH") = Server.MapPath("~")
            Session("MAIL_SERVER") = Session("rs").Fields("CSERVER").Value.ToString
            Session("MAIL_SERVER_PORT") = Session("rs").Fields("CPUERTO").Value.ToString
            Session("MAIL_SERVER_USER") = Session("rs").Fields("CUSER").Value.ToString
            Session("MAIL_SERVER_PWD") = Session("rs").Fields("CPWD").Value.ToString
            Session("MAIL_SERVER_FROM") = Session("rs").Fields("CFROM").Value.ToString
            Session("MAIL_SERVER_SSL") = Session("rs").Fields("CSSL").Value.ToString
            Session("CVE_ENTIDAD") = Session("rs").fields("CVEENTIDAD").value.ToString
            Session("TAMDIG") = Session("rs").fields("TAMDIG").value.ToString
            Session("MAIL_SERVER_ENVIO") = Session("rs").Fields("ENVIO").Value.ToString

            'Session("TIPO_ENTIDAD") = Session("rs").fields("TIPO").value.ToString
            'Session("NIVEL_ENTIDAD") = Session("rs").fields("NIVEL").value.ToString
        End If

        Session("Con").Close()

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DATOS_EMPRESA_PRELLENADO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            Session("EMPRESA") = Session("rs").fields("RAZON").value
        End If
        Session("Con").Close()

        '' Cambia el local a 2058 para evitar conflictos de región
        'Session.LCID = 2058
        ''Despliega el nombre de la aplicacion
        'Session("TituloGlobal") = "MAS.Core"
        ''instancia de la clase global para acceder a subs y funciones generales
        Session("MascoreG") = New SNTE5.TheGlobalClass
        ''Despliega la BD donde esta conectado el sistema
        'Session("DataBase") = "PENSIONESN"
        ''Despliega el ususario de BD que se esta utilizando para la conexion a la base de datos
        'Session("DataBaseUsr") = "sa"
        ''Despliega la contrasena del ususario de BD para la conexion a la base de datos
        'Session("DataBasePwd") = "Infoq02"
        ''obtiene la IP del cliente conectado
        Session("IPuser") = Session("MascoreG").GetIP4Address()
        ''indica el modulo origen despues de un post
        'Session("IDMOD") = 0
        'Session("Logo") = "sysimages/logo.png"

    End Sub



    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.


        Session("MascoreG").Salir_sistema(Session("USERID").ToString, Session("MAC").ToString, Session("Sesion").ToString)

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("ID_EQ"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("LOGGED_IN", Session("adVarChar"), Session("adParamInput"), 10, 0)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "UPD_LOGGED_IN"
        Session("cmd").Execute()
        Session("Con").Close()
        Session.RemoveAll()


    End Sub

</script>