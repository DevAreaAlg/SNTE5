Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Web
Imports System.Web.HttpContext

Public Class TheGlobalClass

    'FUNCION QUE GENERA CLAVE ALEATORIA PARA IDENTIFICAR LA SESION DEL USUARIO
    Public Function RandKey(ByVal longitud As Integer) As String

        'Funcion que Genera una llave aleatoria de session
        Dim strRandomString As String
        strRandomString = ""
        Dim RndNum As New Random()
        Dim intCounter As Integer
        For intCounter = 1 To longitud
            Select Case RndNum.Next(0, 35)
                Case 0 : strRandomString &= "A"
                Case 1 : strRandomString &= "B"
                Case 2 : strRandomString &= "C"
                Case 3 : strRandomString &= "D"
                Case 4 : strRandomString &= "E"
                Case 5 : strRandomString &= "F"
                Case 6 : strRandomString &= "G"
                Case 7 : strRandomString &= "H"
                Case 8 : strRandomString &= "I"
                Case 9 : strRandomString &= "J"
                Case 10 : strRandomString &= "K"
                Case 11 : strRandomString &= "L"
                Case 12 : strRandomString &= "M"
                Case 13 : strRandomString &= "N"
                Case 14 : strRandomString &= "O"
                Case 15 : strRandomString &= "P"
                Case 16 : strRandomString &= "Q"
                Case 17 : strRandomString &= "R"
                Case 18 : strRandomString &= "S"
                Case 19 : strRandomString &= "T"
                Case 20 : strRandomString &= "U"
                Case 21 : strRandomString &= "V"
                Case 22 : strRandomString &= "W"
                Case 23 : strRandomString &= "X"
                Case 24 : strRandomString &= "Y"
                Case 25 : strRandomString &= "Z"
                Case 26 : strRandomString &= "0"
                Case 27 : strRandomString &= "1"
                Case 28 : strRandomString &= "2"
                Case 29 : strRandomString &= "3"
                Case 30 : strRandomString &= "4"
                Case 31 : strRandomString &= "5"
                Case 32 : strRandomString &= "6"
                Case 33 : strRandomString &= "7"
                Case 34 : strRandomString &= "8"
                Case 35 : strRandomString &= "9"
            End Select
        Next intCounter
        Return strRandomString

    End Function

    'FUNCION QUE GENERA CLAVE ALEATORIA PARA IDENTIFICAR LA SESION DEL USUARIO MAYUSCULAS Y MINUSCULAS
    Public Function RandKeyMinMay(ByVal longitud As Integer) As String

        'Funcion que Genera una llave aleatoria de session

        Dim strRandomString As String
        strRandomString = ""
        Dim RndNum As New Random()
        Dim intCounter As Integer
        For intCounter = 1 To longitud
            Select Case RndNum.Next(0, 61)
                Case 0 : strRandomString &= "A"
                Case 1 : strRandomString &= "B"
                Case 2 : strRandomString &= "C"
                Case 3 : strRandomString &= "D"
                Case 4 : strRandomString &= "E"
                Case 5 : strRandomString &= "F"
                Case 6 : strRandomString &= "G"
                Case 7 : strRandomString &= "H"
                Case 8 : strRandomString &= "I"
                Case 9 : strRandomString &= "J"
                Case 10 : strRandomString &= "K"
                Case 11 : strRandomString &= "L"
                Case 12 : strRandomString &= "M"
                Case 13 : strRandomString &= "N"
                Case 14 : strRandomString &= "O"
                Case 15 : strRandomString &= "P"
                Case 16 : strRandomString &= "Q"
                Case 17 : strRandomString &= "R"
                Case 18 : strRandomString &= "S"
                Case 19 : strRandomString &= "T"
                Case 20 : strRandomString &= "U"
                Case 21 : strRandomString &= "V"
                Case 22 : strRandomString &= "W"
                Case 23 : strRandomString &= "X"
                Case 24 : strRandomString &= "Y"
                Case 25 : strRandomString &= "Z"
                Case 26 : strRandomString &= "0"
                Case 27 : strRandomString &= "1"
                Case 28 : strRandomString &= "2"
                Case 29 : strRandomString &= "3"
                Case 30 : strRandomString &= "4"
                Case 31 : strRandomString &= "5"
                Case 32 : strRandomString &= "6"
                Case 33 : strRandomString &= "7"
                Case 34 : strRandomString &= "8"
                Case 35 : strRandomString &= "9"
                Case 36 : strRandomString &= "a"
                Case 37 : strRandomString &= "b"
                Case 38 : strRandomString &= "c"
                Case 39 : strRandomString &= "d"
                Case 40 : strRandomString &= "e"
                Case 41 : strRandomString &= "f"
                Case 42 : strRandomString &= "g"
                Case 43 : strRandomString &= "h"
                Case 44 : strRandomString &= "i"
                Case 45 : strRandomString &= "j"
                Case 46 : strRandomString &= "k"
                Case 47 : strRandomString &= "l"
                Case 48 : strRandomString &= "m"
                Case 49 : strRandomString &= "n"
                Case 50 : strRandomString &= "o"
                Case 51 : strRandomString &= "p"
                Case 52 : strRandomString &= "q"
                Case 53 : strRandomString &= "r"
                Case 54 : strRandomString &= "s"
                Case 55 : strRandomString &= "t"
                Case 56 : strRandomString &= "u"
                Case 57 : strRandomString &= "v"
                Case 58 : strRandomString &= "w"
                Case 59 : strRandomString &= "x"
                Case 60 : strRandomString &= "y"
                Case 61 : strRandomString &= "z"
            End Select
        Next intCounter
        Return strRandomString

    End Function

    Public Function RandKeyMay(ByVal longitud As Integer) As String

        'Funcion que Genera una llave aleatoria de session

        Dim strRandomString As String
        strRandomString = ""
        Dim RndNum As New Random()
        Dim intCounter As Integer
        For intCounter = 1 To longitud
            Select Case RndNum.Next(0, 25)
                Case 0 : strRandomString &= "A"
                Case 1 : strRandomString &= "B"
                Case 2 : strRandomString &= "C"
                Case 3 : strRandomString &= "D"
                Case 4 : strRandomString &= "E"
                Case 5 : strRandomString &= "F"
                Case 6 : strRandomString &= "G"
                Case 7 : strRandomString &= "H"
                Case 8 : strRandomString &= "I"
                Case 9 : strRandomString &= "J"
                Case 10 : strRandomString &= "K"
                Case 11 : strRandomString &= "L"
                Case 12 : strRandomString &= "M"
                Case 13 : strRandomString &= "N"
                Case 14 : strRandomString &= "O"
                Case 15 : strRandomString &= "P"
                Case 16 : strRandomString &= "Q"
                Case 17 : strRandomString &= "R"
                Case 18 : strRandomString &= "S"
                Case 19 : strRandomString &= "T"
                Case 20 : strRandomString &= "U"
                Case 21 : strRandomString &= "V"
                Case 22 : strRandomString &= "W"
                Case 23 : strRandomString &= "X"
                Case 24 : strRandomString &= "Y"
                Case 25 : strRandomString &= "Z"

            End Select
        Next intCounter
        Return strRandomString

    End Function

    Public Shared Function GetIP4Address() As String

        Dim IP4Address As String = String.Empty

        For Each IPA As System.Net.IPAddress In System.Net.Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress)

            If IPA.AddressFamily.ToString() = "InterNetwork" Then

                IP4Address = IPA.ToString()

                Exit For

            End If

        Next

        If IP4Address <> String.Empty Then

            Return IP4Address

        End If

        For Each IPA As System.Net.IPAddress In System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())

            If IPA.AddressFamily.ToString() = "InterNetwork" Then

                IP4Address = IPA.ToString()

                Exit For

            End If

        Next

        Return IP4Address

    End Function

    'Función que te permite agregar el signo de pesos
    Public Function FormatNumberCurr(ByVal val As String) As String
        If val = "0" Then
            Return "0.00"
        End If
        Return FormatNumber(val)
    End Function

    'FUNCION QUE REGISTRA EL TERMINO DE SESION POR EL USUARIO AL DAR CLICK EN LA LIGA SALIR
    Public Sub Salir_sistema(ByVal iduser As String, ByVal MAC As String, ByVal idsesion As String)

        'SE INSERTA EN LA BD EL CORRESPONDIENTE LOG DE FIN DE SESION POR USUARIO
        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("TIPO_FIN", 200, 1, 20, "FNSESCOR")
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ID_USER", 200, 1, 9, iduser)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("MAC", 200, 1, 50, MAC)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ID_SESION", 200, 1, 20, idsesion)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "INS_FIN_SESION"
        cmd.Execute()

        Con.Close()
    End Sub

    'FUNCION QUE OBTIENE EL NOMBRE DEL JEFE DE SUCURSAL DE ACUERDO A SU ID
    Public Function Jefenombre(ByVal idjefesucursal As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDJEFESUC", 200, 1, 15, idjefesucursal)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_JEFE_SUCURSAL_GENERALES"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("NOMBRES").Value.ToString

        Con.Close()

        Return nombre
    End Function

    'FUNCION QUE OBTIENE LOS DATOS GENERALES DE UNA UNIDAD DE NEGOCIO ESPECIFICA
    Public Function UnnegGenerales(ByVal iduneg As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDUNNEG", 200, 1, 15, iduneg)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_UNNEG_GENERALES"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("CATUNNEG_NOMBRE").Value.ToString

        Con.Close()

        Return nombre
    End Function



    'FUNCION QUE OBTIENE LOS DATOS GENERALES DE UNA UNIDAD DE NEGOCIO ESPECIFICA
    Public Function TipoUsuario(ByVal idtipo As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDTIPO", 200, 1, 15, idtipo)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_TIPO_USUARIO"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("CATTIPOUSER_NOMBRE").Value.ToString

        Con.Close()

        Return nombre
    End Function

    'FUNCION QUE OBTIENE LOS DATOS GENERALES DE UNA SUCURSAL ESPECIFICA
    Public Function SucursalGenerales(ByVal idsuc As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDSUC", 200, 1, 15, idsuc)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_SUCURSAL_GENERALES"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("CATSUCURSAL_NOMBRE").Value.ToString

        Con.Close()

        Return nombre
    End Function

    'FUNCION QUE OBTIENE LOS DATOS GENERALES DE UN PUESTO ESPECIFICO
    Public Function PuestoGenerales(ByVal idpues As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDPUES", 200, 1, 15, idpues)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_PUESTO_GENERALES"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("CATPUESTOS_DESCRIPCION").Value.ToString

        Con.Close()

        Return nombre
    End Function

    'FUNCION QUE OBTIENE LOS DATOS GENERALES DE UNA POLITICA ESPECIFICA
    Public Function PoliticaGenerales(ByVal idpol As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDPOLIT", 200, 1, 15, idpol)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_POLITICA_GENERALES"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("MSTPOLITICAS_DESCRIPCION").Value.ToString

        Con.Close()

        Return nombre

    End Function

    'FUNCION QUE REVISA SI EL USUARIO TIENE ACCESO AUTORIZADO A UN MODULO ESPECIFICO
    Public Function RevisaPermisos(ByVal id_usr As String, ByVal id_ses As String, ByVal aspx As String) As String

        Dim autorizado As String

        'SE VERIFICA SI EL  USUARIO TIENE PERMISO DE ACCESO Y SE INSERTA EN BITACORA DE SESION EL RESULTADO
        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("USERID", 200, 1, 15, id_usr)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ID_SESION", 200, 1, 15, id_ses)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ASPX", 200, 1, 50, aspx)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "INS_ACCESO_MODULO"

        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        autorizado = rs.Fields("AUTORIZADO").Value.ToString

        Con.Close()

        Return autorizado
    End Function

    'FUNCION QUE OBTIENE LOS DATOS GENERALES DE UNA SUCURSAL ESPECIFICA (FRANCISCO MORENO)
    Public Function PaisGenerales(ByVal idpais As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDPAIS", 200, 1, 15, idpais)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_PAIS_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("CATPAIS_PAIS").Value.ToString

        Con.Close()

        Return nombre
    End Function

    'FUNCION QUE DEVUELVE LA RELACION LABORAL A PARTIR DE LA ABREVIATURA (FRANCISCO MORENO)
    Public Function RelacionLaboral(ByVal abRelacionLaboral As String) As String

        Dim LaRelacionLaboral As String
        LaRelacionLaboral = ""

        Select Case abRelacionLaboral
            Case "SOC"
                LaRelacionLaboral = "SOCIO"
            Case "EMP"
                LaRelacionLaboral = "EMPLEADO"
            Case "DUE"
                LaRelacionLaboral = "DUEÑO(A)"
            Case "AMB"
                LaRelacionLaboral = "SOCIO(A) Y EMPLEADO(A)"
        End Select

        Return LaRelacionLaboral

    End Function

    'FUNCION QUE DEVUELVE LA PERIODICIDAD DE LOS INGRESOS A PARTIR DE LA ABREVIATURA (FRANCISCO MORENO)
    Public Function PeriodicidadIngresos(ByVal abPeriodicidadIngresos As String) As String

        Dim LaPeriodicidadIngresos As String
        LaPeriodicidadIngresos = ""

        Select Case abPeriodicidadIngresos
            Case "DIA"
                LaPeriodicidadIngresos = "DIARIO"
            Case "SEM"
                LaPeriodicidadIngresos = "SEMANAL"
            Case "QUI"
                LaPeriodicidadIngresos = "QUINCENAL"
            Case "MEN"
                LaPeriodicidadIngresos = "MENSUAL"
            Case "BIM"
                LaPeriodicidadIngresos = "BIMESTRAL"
            Case "TRI"
                LaPeriodicidadIngresos = "TRIMESTRAL"
            Case "SEM"
                LaPeriodicidadIngresos = "SEMESTRAL"
            Case "ANU"
                LaPeriodicidadIngresos = "ANUAL"
        End Select

        Return LaPeriodicidadIngresos

    End Function

    'FUNCION QUE DEVUELVE EL TIPO DE VIALIDAD A PARTIR DE LA ABREVIATURA (FRANCISCO MORENO)
    Public Function TipoViviendaget(ByVal abTipoVivienda As String) As String

        Dim ElTipoVivienda As String
        ElTipoVivienda = ""

        Select Case abTipoVivienda
            Case "0"
                ElTipoVivienda = "ELIJA"
            Case "PRO"
                ElTipoVivienda = "PROPIA"
            Case "FAM"
                ElTipoVivienda = "FAMILIAR"
            Case "REN"
                ElTipoVivienda = "RENTADA"
            Case "PRE"
                ElTipoVivienda = "PRESTADA"
            Case "HIP"
                ElTipoVivienda = "HIPOTECADA"
            Case "PAG"
                ElTipoVivienda = "PAGANDOLA"
        End Select

        Return ElTipoVivienda

    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DE LA ACTIVIDAD A PARTIR DEL ID (FRANCISCO MORENO)
    Public Function ActividadEconomica(ByVal idact As String) As String
        Dim nombre As String
        nombre = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDACTIVIDAD", 200, 1, 15, idact)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_UNA_ACTIVIDAD_ECONOMICA"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        nombre = rs.Fields("CATACTECO_ACTIVIDAD").Value.ToString

        Con.Close()

        Return nombre

    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DE LA ACTIVIDAD A PARTIR DEL ID (FRANCISCO MORENO)
    Public Function InstitucionBancaria(ByVal idinst As String) As String
        Dim institucion As String
        institucion = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDINSTITUCION", 200, 1, 15, idinst)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_UNA_INSTITUCION_BANCARIA"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            institucion = rs.Fields("CATINSTFINAN_INSTITUCION").Value.ToString
        Else
            institucion = ""
        End If
        Con.Close()

        Return institucion

    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DEL ESTADO A PARTIR DEL ID
    Public Function Estadoget(ByVal idestado As String) As String
        Dim estado As String
        estado = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDPAIS", 200, 1, 15, idestado)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_ESTADOS_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            estado = rs.Fields("CATCP_ESTADO").Value.ToString
        Else
            estado = ""
        End If

        Con.Close()

        Return estado
    End Function

    Public Function Estadoget2(ByVal IDESTADO As String, ByVal CP As String) As String
        Dim estado As String
        estado = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm, PARM2
        parm = cmd.CreateParameter("IDESTADO", 200, 1, 15, IDESTADO)
        cmd.Parameters.Append(parm)
        PARM2 = cmd.CreateParameter("CP", 200, 1, 15, CP)
        cmd.Parameters.Append(PARM2)
        cmd.CommandText = "SEL_ESTADO_X_IDESTADO"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        estado = rs.Fields("CATCP_ESTADO").Value.ToString

        Con.Close()

        Return estado
    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DEL MUNICIPIO A PARTIR DEL ID
    Public Function Municipioget(ByVal idmunicipio As String, ByVal idestado As String) As String
        Dim municipio As String
        municipio = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDMUNICIPIO", 200, 1, 4, idmunicipio)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("IDESTADO", 200, 1, 2, idestado)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_MUNICIPIO_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            municipio = rs.Fields("CATCP_MUNICIPIO").Value.ToString
        Else
            municipio = ""
        End If

        Con.Close()

        Return municipio
    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DEL MUNICIPIO EN SIM
    Public Function Municipioget2(ByVal idmunicipio As String, ByVal IDESTADO As String, ByVal cp As String) As String
        Dim municipio As String
        municipio = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm, parm2, parm3
        parm = cmd.CreateParameter("IDMUNICIPIO", 200, 1, 15, idmunicipio)
        cmd.Parameters.Append(parm)
        parm2 = cmd.CreateParameter("IDESTADO", 200, 1, 15, IDESTADO)
        cmd.Parameters.Append(parm2)
        parm3 = cmd.CreateParameter("CP", 200, 1, 15, cp)
        cmd.Parameters.Append(parm3)

        cmd.CommandText = "SEL_MUNICIPIO_X_IDMUNICIPIO"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        municipio = rs.Fields("CATCP_MUNICIPIO").Value.ToString

        Con.Close()

        Return municipio
    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DEL ASENTAMIENTO A PARTIR DEL ID DEL ASENTAMIENTO Y DE LA PERSONA
    Public Function Asentamientoget(ByVal idasentamiento As String, ByVal idmunicipio As String, ByVal idestado As String) As String
        Dim asentamiento As String
        asentamiento = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDASENTAMIENTO", 200, 1, 5, idasentamiento)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("IDMUNICIPIO", 200, 1, 4, idmunicipio)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("IDESTADO", 200, 1, 2, idestado)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_ASENTAMIENTO_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            asentamiento = rs.Fields("CATCP_ASENTAMIENTO").Value.ToString
        Else
            asentamiento = ""
        End If

        Con.Close()

        Return asentamiento
    End Function

    Public Function Asentamientoget2(ByVal idasentamiento As String, ByVal idmunicipio As String, ByVal IDESTADO As String, ByVal cp As String) As String
        Dim asentamiento As String
        asentamiento = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm, parm2, parm3, parm4
        parm = cmd.CreateParameter("IDASENTAMIENTO", 200, 1, 15, idasentamiento)
        cmd.Parameters.Append(parm)
        parm2 = cmd.CreateParameter("IDMUNICIPIO", 200, 1, 15, idmunicipio)
        cmd.Parameters.Append(parm2)
        parm3 = cmd.CreateParameter("IDESTADO", 200, 1, 15, IDESTADO)
        cmd.Parameters.Append(parm3)
        parm4 = cmd.CreateParameter("CP", 200, 1, 15, cp)
        cmd.Parameters.Append(parm4)

        cmd.CommandText = "SEL_ASENTAMIENTO_X_IDASENTAMIENTO"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        asentamiento = rs.Fields("CATCP_ASENTAMIENTO").Value.ToString

        Con.Close()

        Return asentamiento
    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DE LA VIALIDAD A PARTIR DEL ID
    Public Function Vialidadget(ByVal idvialidad As String) As String
        Dim vialidad As String
        vialidad = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDVIALIDAD", 200, 1, 15, idvialidad)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_VIALIDAD_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            vialidad = rs.Fields("CATVIALIDAD_DESCRIPCION").Value.ToString
        Else
            vialidad = ""
        End If

        Con.Close()

        Return vialidad
    End Function

    'FUNCION QUE OBTIENE LA DESCRIPCION DEL TIPO DE DIRECCION A PARTIR DEL ID
    Public Function Tipodirget(ByVal idtipodir As String) As String
        Dim tipodireccion As String
        tipodireccion = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDTIPODIRECCION", 200, 1, 15, idtipodir)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_TIPODIRECCION_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            tipodireccion = rs.Fields("CATTIPODIR_DESCRIPCION").Value.ToString
        Else
            tipodireccion = ""
        End If

        Con.Close()

        Return tipodireccion
    End Function

    'FUNCION QUE OBTIENE EL NOMBRE DE UNA PERSONA MORAL O FISICA A PARTIR DEL ID Y EL TIPO DE PERSONA
    Public Function NombrePersonaget(ByVal idpersona As String, ByVal tipopersona As String) As String
        Dim nombrePersona As String
        nombrePersona = ""

        If idpersona <> "" Then
            'SE CONSULTA EL NOMBRE DE LA PERSONA
            Dim Con = CreateObject("ADODB.Connection")
            Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Con.ConnectionTimeout = 240
            Dim cmd = New ADODB.Command()
            Con.Open()
            cmd.ActiveConnection = Con
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            Dim parm = cmd.CreateParameter("PERSONAID", 200, 1, 15, idpersona)
            cmd.Parameters.Append(parm)
            cmd.CommandText = "SEL_PERSONA_FISICA_PERSONALES"
            Dim rs = CreateObject("ADODB.Recordset")
            rs = cmd.Execute()
            If Not rs.EOF Then
                If tipopersona = "1" Then
                    nombrePersona = rs.Fields("NOMBRE1").Value.ToString + " " + rs.Fields("NOMBRE2").Value.ToString + " " + rs.Fields("PATERNO").Value.ToString + " " + rs.Fields("MATERNO").Value.ToString
                Else
                    nombrePersona = rs.Fields("NOMBRE1").Value.ToString
                End If
            Else
                nombrePersona = ""
            End If
        End If

        Return nombrePersona

    End Function


    Public Function Paisget(ByVal idpais As String) As String
        Dim pais As String
        pais = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDPAIS", 200, 1, 15, idpais)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_PAISES_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        pais = rs.Fields("CATPAIS_PAIS").Value.ToString

        Con.Close()

        Return pais
    End Function

    Public Function MunicipioActaConstitutivaget(ByVal idmunicipio As String, ByVal idpersona As String) As String
        Dim municipio As String
        municipio = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDMUNICIPIO", 200, 1, 15, idmunicipio)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("IDPERSONA", 200, 1, 15, idpersona)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_MUNICIPIO_X_ID_ACTA_CONS"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        municipio = rs.Fields("CATCP_MUNICIPIO").Value.ToString

        Con.Close()

        Return municipio
    End Function

    Public Function CtaContableCaja(ByVal idcta As String) As String
        Dim cuenta As String
        cuenta = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDCTA", 200, 1, 15, idcta)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_DESCRIPCION_CUENTA_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            cuenta = rs.Fields("CUENTA").Value.ToString
        Else
            cuenta = ""
        End If

        Con.Close()

        Return cuenta
    End Function

    Public Function TipoSocget(ByVal idtiposoc As String) As String
        Dim sociedad As String
        sociedad = ""

        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("IDTIPOSOC", 200, 1, 15, idtiposoc)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_TIPOSOCIEDAD_X_ID"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        sociedad = rs.Fields("CATTIPOSOC_DESCRIPCION").Value.ToString

        Con.Close()

        Return sociedad
    End Function

    Public Function lpad_long_ceros(ByVal i As String, ByVal tipo As String) As String
        Dim cad As String
        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("TIPO", 200, 1, 1, tipo)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("CADENA", 200, 1, 10, i)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_FORMATO_CLIE_FOLIO"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        cad = rs.Fields("CAD_RES").Value

        Con.Close()
        Return cad
    End Function

    Function impresion_ticket_CRED(ByVal serie_folio As String, ByVal folio_impreso As String, ByVal id_user As Integer, ByVal razon_social As String) As String

        Dim ctas As Boolean = False
        Dim displayString As String, contrato As String, nombre_producto As String, saldo_total As String, saldo_dis As String, fecha As String, sub_mov As String
        Dim lenn As Integer, minus As Integer, lft As Integer
        Dim id_persona As Integer
        Dim razon_social_empresa, array_razon_social(), linea_actual, caracteres As String

        razon_social_empresa = razon_social

        displayString = vbCrLf & vbCrLf & vbCrLf

        If Len(razon_social_empresa) > 27 Then
            'Se utilizas dos líneas
            array_razon_social = Split(razon_social_empresa)
            linea_actual = ""
            For i As Integer = 0 To array_razon_social.Length - 1
                If array_razon_social(i) <> "" Then
                    If Len(linea_actual + " " + array_razon_social(i)) < 27 Then
                        linea_actual += " " + array_razon_social(i)
                    Else
                        caracteres = ""
                        caracteres = caracteres.PadLeft(Int((40 - Len(linea_actual)) / 2))
                        displayString += caracteres + linea_actual + caracteres + vbCrLf
                        linea_actual = array_razon_social(i)
                    End If
                End If
            Next

            caracteres = ""
            caracteres = caracteres.PadLeft(Int((40 - Len(linea_actual)) / 2))
            displayString += caracteres + linea_actual + caracteres + vbCrLf + vbCrLf

        Else

            caracteres = ""
            caracteres = caracteres.PadLeft(Int(40 - Len(razon_social_empresa) / 2))
            displayString += caracteres + razon_social_empresa + caracteres + vbCrLf + vbCrLf

        End If


        Dim RefNombreCred As String = ""


        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("SERIE", 200, 1, 3, serie_folio)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("FOLIO", 200, 1, 8, folio_impreso)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_VENTANILLA_DATOS_FICHA_GRAL"
        Dim rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        If Not rs.EOF Then
            id_persona = rs.Fields("IDPERSONA").Value
            RefNombreCred = rs.Fields("NOMBRE_OP").Value
            If id_persona <> -1 Then
                displayString += rs.Fields("DIR1").Value.ToString & vbCrLf
                displayString += rs.Fields("DIR2").Value.ToString & vbCrLf
                displayString += rs.Fields("FECHA_HORA").Value.ToString & vbCrLf & vbCrLf
                displayString += rs.Fields("NOMBRE").Value & vbCrLf
                displayString += "FOLIO: " + serie_folio + folio_impreso & vbCrLf
                displayString += "NUM CLIENTE: " + rs.Fields("IDPERSONA").Value.ToString & vbCrLf

                contrato = rs.Fields("FOLIO").Value.ToString
                nombre_producto = rs.Fields("NOMBRE_PROD").Value
                saldo_total = rs.Fields("SALDO_TOTAL").Value.ToString
                saldo_dis = rs.Fields("SALDO_DIS").Value.ToString
                fecha = Left(rs.Fields("FECHA_HORA").Value.ToString, 10)
                If rs.Fields("CTAS_CAP").Value.ToString = "1" Then
                    ctas = True
                End If
            Else
                displayString += "FOLIO:        " + serie_folio + folio_impreso & vbCrLf
                lenn = rs.Fields("CONCEPTO").Value.Length
                If lenn > 26 Then
                    displayString += "CONCEPTO:     " + Mid(rs.Fields("CONCEPTO").Value, 1, 26) & vbCrLf
                    displayString += "              " + Mid(rs.Fields("CONCEPTO").Value, 27, lenn) & vbCrLf
                Else
                    displayString += "CONCEPTO:     " + rs.Fields("CONCEPTO").Value & vbCrLf
                End If
                displayString += "CUENTA CARGO: " + rs.Fields("CTA_CARGO").Value & vbCrLf
                lenn = rs.Fields("DESC_CARGO").Value.Length
                displayString += IIf(lenn > 26, "              " + Mid(rs.Fields("DESC_CARGO").Value, 1, 26) & vbCrLf + "              " _
                + Mid(rs.Fields("DESC_CARGO").Value, 27, lenn) & vbCrLf, "              " _
                + rs.Fields("DESC_CARGO").Value & vbCrLf)
                displayString += "CUENTA ABONO: " + rs.Fields("CTA_ABONO").Value & vbCrLf
                lenn = rs.Fields("DESC_ABONO").Value.Length
                displayString += IIf(lenn > 26, "              " + Mid(rs.Fields("DESC_ABONO").Value, 1, 26) & vbCrLf + "              " _
                + Mid(rs.Fields("DESC_ABONO").Value, 27, lenn) & vbCrLf, "              " _
                + rs.Fields("DESC_ABONO").Value & vbCrLf)
                'displayString += "              " + rs.Fields("DESC_ABONO").Value & vbCrLf
                displayString += "MONTO:        " + rs.Fields("MONTO").ValuE.ToString & vbCrLf
                displayString += "FECHA:        " + Left(rs.Fields("FECHA").Value.ToString, 10) & vbCrLf
            End If

        End If
        Con.Close()

        If id_persona <> -1 Then
            displayString += vbCrLf & vbCrLf
            displayString += "Detalle de movimientos       Movimientos" & vbCrLf
            displayString += "----------------------------------------" & vbCrLf

            Con = CreateObject("ADODB.Connection")
            Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Con.ConnectionTimeout = 240
            cmd = New ADODB.Command()
            Con.Open()
            cmd.ActiveConnection = Con
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            parm = cmd.CreateParameter("SERIE", 200, 1, 3, serie_folio)
            cmd.Parameters.Append(parm)
            parm = cmd.CreateParameter("FOLIO", 200, 1, 8, folio_impreso)
            cmd.Parameters.Append(parm)
            cmd.CommandText = "SEL_VENTANILLA_DATOS_FICHA"
            rs = CreateObject("ADODB.Recordset")
            rs = cmd.Execute()

            If Not rs.EOF Then
                Do While Not rs.EOF
                    lenn = rs.Fields("DESCRIPCION_MOV").Value.ToString.Length
                    If lenn <= 30 Then
                        displayString += rs.Fields("DESCRIPCION_MOV").Value.ToString
                        While (lenn < 30)
                            displayString += " "
                            lenn += 1
                        End While
                        displayString += "$" + FormatNumberCurr(rs.Fields("MONTO").Value.ToString) & vbCrLf
                    Else
                        sub_mov = Mid(rs.Fields("DESCRIPCION_MOV").Value, 27, lenn)
                        displayString += Mid(rs.Fields("DESCRIPCION_MOV").Value, 1, 27) & vbCrLf
                        lenn = sub_mov.Length
                        displayString += sub_mov
                        While (lenn < 30)
                            displayString += " "
                            lenn += 1
                        End While
                        displayString += "$" + FormatNumberCurr(rs.Fields("MONTO").Value.ToString) & vbCrLf
                    End If
                    rs.movenext()
                Loop
            End If
            Con.Close()

            displayString += "----------------------------------------" & vbCrLf & vbCrLf
            displayString += nombre_producto + " (NO. CTA.: " + contrato + ") " & vbCrLf

            'displayString += "SALDO DISPONIBLE: $" + FormatNumberCurr(saldo_dis) & vbCrLf
            'displayString += "SALDO TOTAL: $" + FormatNumberCurr(saldo_total) & vbCrLf

        End If


        If ctas = True Then

            Con = CreateObject("ADODB.Connection")
            Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
            Con.ConnectionTimeout = 240
            cmd = New ADODB.Command()
            Con.Open()
            cmd.ActiveConnection = Con
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            parm = cmd.CreateParameter("SERIE", 200, 1, 3, serie_folio)
            cmd.Parameters.Append(parm)
            parm = cmd.CreateParameter("FOLIO", 200, 1, 8, folio_impreso)
            cmd.Parameters.Append(parm)
            cmd.CommandText = "SEL_VENTANILLA_DATOS_FICHA_OTRAS_CTAS"
            rs = CreateObject("ADODB.Recordset")
            rs = cmd.Execute()

            'If Not Session("rs").EOF Then
            If Not rs.EOF Then
                Do While Not rs.EOF
                    displayString += "----------------------------------------" & vbCrLf
                    displayString += rs.Fields("PRODUCTO").Value + " (NO. CTA.: " + rs.Fields("FOLIO").Value.ToString + ")" & vbCrLf
                    ' displayString += "SALDO DISPONIBLE: $" + FormatNumberCurr(rs.Fields("SALDO_DIS").Value.ToString) & vbCrLf
                    'displayString += "SALDO TOTAL: $" + FormatNumberCurr(rs.Fields("SALDO_TOTAL").Value.ToString) & vbCrLf
                    rs.movenext()
                Loop
            End If
            Con.Close()
        End If

        displayString += vbCrLf & vbCrLf & vbCrLf
        displayString += "            ---------------             "
        displayString += "            Firma del Cajero            " & vbCrLf

        Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        parm = cmd.CreateParameter("ID_USER", 200, 1, 10, id_user)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "SEL_USUARIO"
        rs = CreateObject("ADODB.Recordset")
        rs = cmd.Execute()

        lenn = rs.Fields("USUARIO").Value.Length
        minus = 41 - lenn
        lft = minus Mod 2
        minus = CInt(minus / 2)
        lenn = 0
        If lft > 0 Then
            While (lenn <= minus)
                displayString += " "
                lenn += 1
            End While
            'displayString += lft
            'displayString += rs.Fields("USUARIO").Value
            lenn = 0
            While (lenn <= minus)
                displayString += " "
                lenn += 1
            End While
        Else
            While (lenn <= minus)
                displayString += " "
                lenn += 1
            End While
            displayString += rs.Fields("USUARIO").Value
            lenn = 0
            While (lenn <= minus)
                displayString += " "
                lenn += 1
            End While
        End If
        Con.Close()
        If id_persona <> -1 Then
            displayString += vbCrLf & vbCrLf & vbCrLf
            displayString += "            ---------------             " & vbCrLf
            displayString += "           Firma del Cliente            " & vbCrLf
            displayString += RefNombreCred & vbCrLf
        End If

        displayString += "* No Válido sin Sello y Firma del Cajero" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
        displayString += "                   -                    " & vbCrLf

        Return displayString

    End Function


End Class

Public Class Persona 'FMN 24/08/217

    Public Function Calcula_edad(ByVal fecha_nacimiento As Date, ByVal fecha_hoy As Date) As Integer
        'FMN  23/08/2017

        Dim edad As Integer

        edad = -1

        If Month(fecha_nacimiento) < Month(fecha_hoy) Then
            edad = Year(fecha_hoy) - Year(fecha_nacimiento)
        Else
            If Month(fecha_nacimiento) = Month(fecha_hoy) Then
                If Day(fecha_nacimiento) <= Day(fecha_hoy) Then
                    edad = Year(fecha_hoy) - Year(fecha_nacimiento)
                Else
                    edad = Year(fecha_hoy) - Year(fecha_nacimiento) - 1
                End If
            Else
                edad = Year(fecha_hoy) - Year(fecha_nacimiento) - 1
            End If
        End If

        Return edad

    End Function
    Public Function compara_RFC_default(ByVal RFCActual As String, ByVal RFCDefault As String) As Boolean
        'FMN  23/08/2017

        Dim derecha, izquierda, derechaDefault, izquierdaDefault As String

        If Len(RFCActual) = 13 Then
            derecha = Right(RFCActual, 3)
            izquierda = Left(RFCActual, 4)

            derechaDefault = Right(RFCDefault, 3)
            izquierdaDefault = Left(RFCDefault, 4)

            If derecha = derechaDefault And izquierda = izquierdaDefault Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function concatenar_RFC_default(ByVal fecha As String, ByVal RFCDefault As String) As String
        'FMN  23/08/2017

        Dim rfc_menor, fecha_temporal As String

        fecha_temporal = fecha.Replace("/", "")

        rfc_menor = Left(RFCDefault, 4) + Right(fecha_temporal, 2) + fecha_temporal.Substring(2, 2) + Left(fecha_temporal, 2) + Right(RFCDefault, 3)

        Return rfc_menor

    End Function

    Public Function Revisa_RFC(ByVal RFCActual As String, ByVal RFCDefault As String, ByVal fechaNacimiento As String, ByVal fechaActual As String) As String

        Dim RFCDevolver As String

        RFCDevolver = ""

        If Not String.IsNullOrEmpty(fechaNacimiento) Then
            If Calcula_edad(fechaNacimiento, fechaActual) >= 18 Then
                If Not compara_RFC_default(Trim(RFCActual), RFCDefault) Then
                    RFCDevolver = RFCActual
                End If
            End If
        Else
            If Not compara_RFC_default(RFCActual, RFCDefault) Then
                RFCDevolver = RFCActual
            End If
        End If

        Return RFCDevolver

    End Function
End Class 'Fin de Clase Persona

' Create our own utility for exceptions
Public NotInheritable Class ExceptionUtility

    ' All methods are static, so this can be private
    Private Sub New()
        MyBase.New()
    End Sub

    ' Log an Exception
    Public Shared Sub LogException(ByVal exc As Exception, ByVal source As String, ByVal MAC As String, ByVal id_user As String, ByVal id_sesion As String)

        Dim Err As String = ""

        'Guardo el objeto de Error en una variable string para insertar en la BD
        If exc.InnerException IsNot Nothing Then
            Err = "Inner Exception Type: " + exc.InnerException.GetType.ToString + vbCrLf +
            "Inner Exception: " + exc.InnerException.Message + vbCrLf +
            "Inner Source: " + exc.InnerException.Source + vbCrLf
            If exc.InnerException.StackTrace IsNot Nothing Then
                Err = Err + "Inner Stack Trace: " + exc.InnerException.StackTrace + vbCrLf
            End If
        End If
        Err = Err + "Exception Type: " + exc.GetType.ToString + vbCrLf +
        "Exception: " + exc.Message + vbCrLf +
        "Source: " + source + vbCrLf
        If exc.StackTrace IsNot Nothing Then
            Err = Err + "Stack Trace: " + exc.StackTrace + vbCrLf
        End If


        'SE INSERTA EN LA BD EL CORRESPONDIENTE LOG DE FIN DE SESION POR ERROR
        Dim con = CreateObject("ADODB.Connection")
        con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        con.Open()
        cmd.ActiveConnection = con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Dim parm = cmd.CreateParameter("ID_EVENTO", 200, 1, 10, "ERGENNET") 'IHG 2017-10-03 Se cambio el id por la clave del evento 
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ID_USER", 200, 1, 15, id_user)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ERROR", 200, 1, 3000, Err)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("ID_SESION", 200, 1, 15, id_sesion)
        cmd.Parameters.Append(parm)
        parm = cmd.CreateParameter("MAC", 200, 1, 50, MAC)
        cmd.Parameters.Append(parm)
        cmd.CommandText = "INS_LOGERROR"
        cmd.Execute()

        con.Close()


    End Sub

End Class


