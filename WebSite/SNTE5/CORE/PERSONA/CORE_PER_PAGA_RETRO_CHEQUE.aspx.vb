Imports System.Data.SqlClient
Public Class CORE_PER_PAGA_RETRO_CHEQUE
    Inherits System.Web.UI.Page


#Region “LOAD”
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TryCast(Master, MasterMascore).CargaASPX("Pago de retroactivo con cheque", "Pago de retroactivo con cheque")
        If Not Me.IsPostBack Then
            Dim resultado
            resultado = (Session("MascoreG").RevisaPermisos(Session("USERID").ToString, Session("Sesion").ToString, System.IO.Path.GetFileName(Request.Url.ToString())))

            'Si el usuario no tiene permiso para acceder a este modulo se guarda en bitacora y envia a prohibido
            If resultado = "0" Then
                Response.Redirect("/Prohibido.aspx")
            End If

            Llenar_cmbDelegaciones()
            Llenar_cmbRegiones()
            Llenar_cmbEstatusCheques()
        End If
    End Sub
#End Region

#Region “Cargar info combos”

    Private Sub Llenar_cmbDelegaciones()
        'lbl_productos.Text = "TIPO: 1" + " DESTINO: cmb_destino.SelectedItem.Value " + " SUCID: " + Session("SUCID").ToString + " TIPOPER: F " + " PERSONAID: " + Session("PERSONAID")
        'Lleno el combo con los productos respecto al tipo de producto elegido
        ddl_Delegacion.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_Delegacion.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_DELEGACIONES"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Delegacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub llenar_cmbSistema()
        cmb_sistema.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SISTEMAS_X_RFC_AHORRO_CIERRE_CICLO"
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, txt_IdCliente1.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()

        Dim itemElija As New ListItem("ELIJA", "ELIJA")
        cmb_sistema.Items.Add(itemElija)

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("SISTEMA").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmb_sistema.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub Llenar_cmbRegiones()
        'lbl_productos.Text = "TIPO: 1" + " DESTINO: cmb_destino.SelectedItem.Value " + " SUCID: " + Session("SUCID").ToString + " TIPOPER: F " + " PERSONAID: " + Session("PERSONAID")
        'Lleno el combo con los productos respecto al tipo de producto elegido
        ddl_Region.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_Region.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_REGIONES"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_Region.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Private Sub Llenar_cmbEstatusCheques()
        'lbl_productos.Text = "TIPO: 1" + " DESTINO: cmb_destino.SelectedItem.Value " + " SUCID: " + Session("SUCID").ToString + " TIPOPER: F " + " PERSONAID: " + Session("PERSONAID")
        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmbTipoNota.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmbTipoNota.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ESTATUS_CHEQUES"

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmbTipoNota.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    ''cmbChequeNota
    Private Sub Llenar_cmbChequesTrabajador()
        'lbl_productos.Text = "TIPO: 1" + " DESTINO: cmb_destino.SelectedItem.Value " + " SUCID: " + Session("SUCID").ToString + " TIPOPER: F " + " PERSONAID: " + Session("PERSONAID")
        'Lleno el combo con los productos respecto al tipo de producto elegido
        cmbChequeNota.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmbChequeNota.Items.Add(elija)
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_CHEQUES_TRABAJADOR"
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, txt_IdCliente1.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("CHEQUE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            cmbChequeNota.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

#End Region

#Region “Ocultar/mostrar botones”
    Private Sub mostrarBotonesMenejoCheque()

        btnCancelar.Visible = True
        btnPoliza.Visible = True
        btn_asignar.Visible = True
        txt_num.Visible = True
        btn_Entregar.Visible = True
        btn_imprimir.Visible = True
        btn_PagarCheques.Visible = True
        txb_FechaCheque.Visible = True
        chb_Fecha.Visible = True
        lblFechaDeCheque.Visible = True
        lblNumDeChaqueInicial.Visible = True
        txt_notas.Visible = True
        lbl_cmbTipoNota.Visible = True
        lbl_notas.Visible = True
        cmbTipoNota.Visible = True
        btn_GuardarNotas.Visible = True
        txt_notas.Text = ""
        cmbChequeNota.Visible = True
        lblChequeNotas.Visible = True
        btnVerNotasCheque.visible = True

    End Sub
    Private Sub ocultarBotonesMenejoCheque()

        btnCancelar.Visible = False
        btnPoliza.Visible = False
        btn_asignar.Visible = False
        txt_num.Visible = False
        btn_Entregar.Visible = False
        btn_imprimir.Visible = False
        btn_PagarCheques.Visible = False
        txb_FechaCheque.Visible = False
        lblFechaDeCheque.Visible = False
        lblNumDeChaqueInicial.Visible = False
        chb_Fecha.Visible = False
        txt_notas.Visible = False
        lbl_cmbTipoNota.Visible = False
        lbl_notas.Visible = False
        cmbTipoNota.Visible = False
        btn_GuardarNotas.Visible = False
        txt_notas.Text = ""
        cmbChequeNota.Visible = False
        lblChequeNotas.Visible = False
        btnVerNotasCheque.visible = False

    End Sub
    Private Sub mostrarControlesAsignar()
        btn_asignar.Visible = True
        txt_num.Visible = True
    End Sub
    Private Sub ocultarControlesAsignar()
        btn_asignar.Visible = False
        txt_num.Visible = False
    End Sub

    Private Sub ocultarControlesSeleccionaPlaza()
        cmb_sistema.Visible = False
        lbl_sistema.Visible = False
    End Sub

    Private Sub mostrarControlesSeleccionaPlaza()
        cmb_sistema.Visible = True
        lbl_sistema.Visible = True
    End Sub

    Private Sub activarControlesAsignar()
        btn_asignar.Enabled = True
        txt_num.Enabled = True
    End Sub
    Private Sub desactivarControlesAsignar()
        btn_asignar.Enabled = False
        txt_num.Enabled = False
    End Sub

#End Region

#Region “Metodos”

    Private Sub consultarInformacionDeCierreAgremiado(Optional ByVal plaza_agremiado As String = "")

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Dim variables As String = ""
        Dim contador As Integer = 0

        Dim num_control As String = String.Empty
        Dim saldo_ahorro As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim tipo_pago As String = String.Empty ''13
        Dim clave_rastreo As String = String.Empty ''16
        Dim num_paquete As String = String.Empty ''17
        Dim fecha_cveRastreo As String = String.Empty ''18
        Dim Estatus_SPEI As String = String.Empty ''15

        Dim contador2 As Integer = 0

        Try
            cmb_sistema.Items.Clear()

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_INFO_DE_CIERRE_CICLO_AGREMIADO_RETRO"
            Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 20, txt_IdCliente1.Text.ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            If plaza_agremiado <> "" Then
                Session("parm") = Session("cmd").CreateParameter("TIPO_PLAZA", Session("adVarChar"), Session("adParamInput"), 20, plaza_agremiado)
                Session("cmd").Parameters.Append(Session("parm"))
            End If

            Session("rs") = Session("cmd").Execute()

            'se agregan los expedientes a una tabla en memoria
            custDA.Fill(dtAnalisis, Session("rs"))

            'se vacian los expedientes al formulario
            DAG_Analisis.DataSource = dtAnalisis
            DAG_Analisis.DataBind()

        Catch ex As Exception

        Finally
            Session("Con").Close()
        End Try

        Try


            If DAG_Analisis.Rows.Count < 1 Then

                lbl_status.Text = "No se encontró al agremiado: " + CStr(txt_IdCliente1.Text.ToString)
                ocultarBotonesMenejoCheque()
                ocultarControlesAsignar()
                ocultarControlesSeleccionaPlaza()
                Exit Sub

            End If

            If DAG_Analisis.Rows.Count > 1 Then

                lbl_status.Text = "Seleccione plaza"
                ocultarBotonesMenejoCheque()
                ocultarControlesAsignar()
                llenar_cmbSistema()
                mostrarControlesSeleccionaPlaza()
                Exit Sub

            ElseIf DAG_Analisis.Rows.Count = 1 Then

                mostrarBotonesMenejoCheque()
                mostrarControlesAsignar()
                ocultarControlesSeleccionaPlaza()
                Llenar_cmbChequesTrabajador()

            Else

                lbl_status.Text = "No hay registros para procesar"

            End If




            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then

                    tipo_pago = Fila.Cells(12).Text.ToString() ''13
                    clave_rastreo = Fila.Cells(16).Text.ToString() ''16
                    num_paquete = Fila.Cells(17).Text.ToString() ''17
                    fecha_cveRastreo = Fila.Cells(18).Text.ToString() ''18
                    Estatus_SPEI = Fila.Cells(15).Text.ToString() ''15

                    If Not tipo_pago.Equals("CHQ") Then

                        If tipo_pago.Equals("SPEI") Then
                            lbl_status.Text = "El tipo de pago para este trabajador esta registrado como 'SPEI'"
                        ElseIf tipo_pago.Equals("SIN ASIGNAR") Then
                            lbl_status.Text = "No se ha registrado el tipo de pago para este trabajador (Estado actual 'SIN ASIGNAR')"
                        End If

                        ocultarBotonesMenejoCheque()
                        Exit Sub

                    End If


                    If Fila.Cells(8).Text.ToString().Equals("&nbsp;") Or Fila.Cells(8).Text.ToString().Equals("") Or Fila.Cells(8).Text.ToString().Equals("0") Then

                        num_control = Fila.Cells(2).Text.ToString()
                        saldo_ahorro = Fila.Cells(7).Text.ToString()
                        no_cheque = txt_num.Text

                        variables = variables + " FILA-> " + CStr(contador)
                        variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())
                        variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())
                        variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    Else

                        lbl_status.Text = "Este registro ya cuenta con número de cheque asignado"

                    End If

                Else
                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub
                End If
            Next



        Catch ex As Exception

        Finally

            Try

                If no_cheque <> "" Then
                    consultarNotasDeCheque(CInt(no_cheque))
                End If

            Catch ex As Exception

            End Try

        End Try

    End Sub
    Private Sub ObtieneRegistrosTrabajadorRFC()


        lbl_status.Visible = True

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Try
            ''''''''''''''''''''''''''''''''''''''''
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("NUM_CONTROL", Session("adVarChar"), Session("adParamInput"), 20, txt_IdCliente1.Text)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("REGION", Session("adVarChar"), Session("adParamInput"), 100, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("DELEGACION", Session("adVarChar"), Session("adParamInput"), 100, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 100, "")
            Session("cmd").Parameters.Append(Session("parm"))
            Session("parm") = Session("cmd").CreateParameter("ID_USUARIO", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_REGISTROS_AHORRO_PARA_CHEQUE"
            Session("rs") = Session("cmd").Execute()

            'se agregan los expedientes a una tabla en memoria
            custDA.Fill(dtAnalisis, Session("rs"))

            'se vacian los expedientes al formulario
            DAG_Analisis.DataSource = dtAnalisis
            DAG_Analisis.DataBind()

            Session("Con").Close()
            '''''''''''''''''''''''''''''''''''''''''

            If DAG_Analisis.Rows.Count > 1 Then

                mostrarBotonesMenejoCheque()
                mostrarControlesAsignar()


            ElseIf DAG_Analisis.Rows.Count = 1 Then

                lbl_status.Text = "No se encontró al agremiado o no es elegible para pago por cheque"
                ocultarBotonesMenejoCheque()
                ocultarControlesAsignar()

            Else

            End If

        Catch ex As Exception

            lbl_status.Text = ex.Message

        End Try

    End Sub
    Private Sub consultaChequesGenerados()

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Try
            ''''''''''''''''''''''''''''''''''''''''
            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_CHEQUES_AHORRO"
            Session("rs") = Session("cmd").Execute()

            'se agregan los expedientes a una tabla en memoria
            custDA.Fill(dtAnalisis, Session("rs"))



            dag_ChequesGenerados.DataSource = dtAnalisis
            dag_ChequesGenerados.DataBind()


            '''''''''''''''''''''''''''''''''''''''''

        Catch ex As Exception

            lbl_status.Text = ex.Message
        Finally

            Session("Con").Close()

        End Try

    End Sub
    Private Sub AsignarCheques()

        Dim dtValidaDes As New DataTable
        dtValidaDes.Columns.Add("RFC_EMPLEADO", GetType(String))
        dtValidaDes.Columns.Add("SALDO_AHORRO", GetType(String))
        dtValidaDes.Columns.Add("NUM_CHEQUE", GetType(String))
        dtValidaDes.Columns.Add("ESTATUS", GetType(String))
        dtValidaDes.Columns.Add("ID_ESTATUS", GetType(String))

        Dim variables As String = ""
        Dim contador As Integer = 0



        For Each Fila As GridViewRow In DAG_Analisis.Rows
            If Not Fila Is Nothing Then


                If Fila.Cells(8).Text.ToString().Equals("&nbsp;") Or Fila.Cells(8).Text.ToString().Equals("") Then
                    '//Agregar datos a datagrid
                    Dim row As DataRow = dtValidaDes.NewRow

                    variables = variables + " FILA-> " + CStr(contador)

                    row("RFC_EMPLEADO") = Fila.Cells(2).Text.ToString()
                    variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())

                    row("SALDO_AHORRO") = Fila.Cells(7).Text.ToString()
                    variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())

                    row("NUM_CHEQUE") = Fila.Cells(8).Text.ToString()
                    variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    row("ESTATUS") = Fila.Cells(9).Text.ToString()
                    variables = variables + " " + CStr(Fila.Cells(9).Text.ToString())

                    row("ID_ESTATUS") = Fila.Cells(10).Text.ToString()
                    variables = variables + " " + CStr(Fila.Cells(10).Text.ToString())

                    dtValidaDes.Rows.Add(row)
                    contador = contador + 1
                End If

            End If
        Next



    End Sub
    Private Sub AsignarChequeTrabajador()

        Dim variables As String = ""
        Dim contador As Integer = 0

        Dim num_control As String = String.Empty
        Dim saldo_ahorro As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim plaza_agremiado As String = String.Empty

        Try

            If DAG_Analisis.Rows.Count < 1 Then
                lbl_status.Text = "No hay registros para procesar"
                Exit Sub
            End If
            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then


                    If Fila.Cells(8).Text.ToString().Equals("&nbsp;") Or Fila.Cells(8).Text.ToString().Equals("") Or Fila.Cells(8).Text.ToString().Equals("0") Then


                        variables = variables + " FILA-> " + CStr(contador)

                        num_control = Fila.Cells(2).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())

                        saldo_ahorro = Fila.Cells(7).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())

                        plaza_agremiado = Fila.Cells(13).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(13).Text.ToString())

                        no_cheque = txt_num.Text
                        variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    Else

                        lbl_status.Text = "Este registro ya cuenta con número de cheque asignado"
                        Exit Sub

                    End If

                Else

                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub

                End If
            Next

        Catch ex As Exception

        End Try


        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("NUM_CONTROL", Session("adVarChar"), Session("adParamInput"), 50, num_control)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("SALDO_AHORRO", Session("adVarChar"), Session("adParamInput"), 50, saldo_ahorro)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("NO_CHEQUE", Session("adVarChar"), Session("adParamInput"), 50, no_cheque)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("ID_USUARIO", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("PLAZA_AGREMIADO", Session("adVarChar"), Session("adParamInput"), 50, plaza_agremiado)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "INS_NUM_CHEQUE_AHORRO_INDIVIDUAL"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then
                Dim ultimoCheque As Integer = Session("rs").FIELDS("ULTIMO_CHEQUE").value
                Dim existe As Integer = Session("rs").FIELDS("EXISTE").value

                If existe = 0 Then
                    txt_IdCliente1.Text = num_control

                    lbl_status.Text = "Registro asignado correctamente"
                Else
                    lbl_status.Text = "Alerta: El número de cheque ya ha sido asignado a otro registro. (Ultimo cheque utilizado: " + CStr(ultimoCheque) + ")"
                End If
            End If
        Catch ex As Exception
        Finally
            Session("Con").Close()
            consultarInformacionDeCierreAgremiado(plaza_agremiado)
        End Try

    End Sub
    Private Sub EntregarChequeTrabajador()

        Dim num_control As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim estatus_cheque As String = String.Empty
        Dim plaza_agremiado As String = String.Empty

        Dim variables As String = ""
        Dim contador As Integer = 0

        Try

            If DAG_Analisis.Rows.Count < 1 Then
                lbl_status.Text = "No hay registros para procesar"
                Exit Sub
            End If
            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then


                    If Fila.Cells(9).Text.ToString().Equals("EMITIDO") Then

                        variables = variables + " FILA-> " + CStr(contador)

                        num_control = Fila.Cells(2).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())

                        estatus_cheque = Fila.Cells(9).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())

                        plaza_agremiado = Fila.Cells(13).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(13).Text.ToString())

                        no_cheque = CStr(Fila.Cells(8).Text.ToString())
                        variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    Else
                        lbl_status.Text = "Este cheque no puede ser 'ENTREGADO' debido a su estatus, verifique que el cheque este en estatus 'EMITIDO'"
                        Exit Sub

                    End If

                Else
                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            lbl_status.Text = ex.Message
        End Try


        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("NUM_CONTROL", Session("adVarChar"), Session("adParamInput"), 50, num_control)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("NO_CHEQUE", Session("adVarChar"), Session("adParamInput"), 50, no_cheque)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("ID_USUARIO", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("PLAZA_AGREMIADO", Session("adVarChar"), Session("adParamInput"), 50, plaza_agremiado)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "UPD_ESTATUS_ENTREGA_CHEQUE_AHORRO"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then
                Dim existe As Integer = Session("rs").FIELDS("EXISTE").value.ToString
                Dim actualizado As Integer = Session("rs").FIELDS("ACTUALIZADO").value.ToString
                If existe = 1 And actualizado = 1 Then
                    txt_IdCliente1.Text = num_control

                    lbl_status.Text = "Registro actualizado correctamente"
                Else
                    lbl_status.Text = "Alerta: El estatus del cheque no fue actualizado. "
                End If
            End If
        Catch ex As Exception
            lbl_status.Text = ex.Message
        Finally
            Session("Con").Close()
            consultarInformacionDeCierreAgremiado(plaza_agremiado)
        End Try

    End Sub
    Private Sub cancelarChequeTrabajador()

        Dim num_control As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim estatus_cheque As String = String.Empty
        Dim plaza_agremiado As String = String.Empty

        Dim variables As String = ""
        Dim contador As Integer = 0

        Try

            If DAG_Analisis.Rows.Count < 1 Then
                lbl_status.Text = "No hay registros para procesar"
                Exit Sub
            End If
            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then


                    If Fila.Cells(9).Text.ToString().Equals("EMITIDO") Or Fila.Cells(9).Text.ToString().Equals("ENTREGADO") Then

                        variables = variables + " FILA-> " + CStr(contador)

                        num_control = Fila.Cells(2).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())

                        estatus_cheque = Fila.Cells(9).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())

                        plaza_agremiado = Fila.Cells(13).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(13).Text.ToString())

                        no_cheque = CStr(Fila.Cells(8).Text.ToString())
                        variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    ElseIf Fila.Cells(9).Text.ToString().Equals("PAGADO") Then

                        lbl_status.Text = "Este cheque no puede ser 'CANCELADO' debido a su estatus. (PAGADO)'"
                        Exit Sub

                    Else

                        lbl_status.Text = "Este cheque no puede ser 'CANCELADO' debido a su estatus, verifique que el cheque este en estatus 'EMITIDO' o 'ENTREGADO'"
                        Exit Sub

                    End If

                Else
                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            lbl_status.Text = ex.Message
        End Try

        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("NUM_CONTROL", Session("adVarChar"), Session("adParamInput"), 50, num_control)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("NO_CHEQUE", Session("adVarChar"), Session("adParamInput"), 50, no_cheque)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("ID_USUARIO", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("PLAZA_AGREMIADO", Session("adVarChar"), Session("adParamInput"), 50, plaza_agremiado)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "UPD_ESTATUS_CANCELAR_CHEQUE_AHORRO"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then
                Dim existe As Integer = Session("rs").FIELDS("EXISTE").value.ToString
                Dim actualizado As Integer = Session("rs").FIELDS("ACTUALIZADO").value.ToString
                If existe = 1 And actualizado = 1 Then
                    txt_IdCliente1.Text = num_control

                    lbl_status.Text = "Registro actualizado correctamente"
                Else
                    lbl_status.Text = "Alerta: El estatus del cheque no fue actualizado. "
                End If
            End If
        Catch ex As Exception
            lbl_status.Text = ex.Message
        Finally
            Session("Con").Close()
            consultarInformacionDeCierreAgremiado(plaza_agremiado)
        End Try

    End Sub
    Private Sub pagarChequeTrabajador()

        Dim num_control As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim estatus_cheque As String = String.Empty
        Dim plaza_agremiado As String = String.Empty

        Dim variables As String = ""
        Dim contador As Integer = 0

        Try

            If DAG_Analisis.Rows.Count < 1 Then
                lbl_status.Text = "No hay registros para procesar"
                Exit Sub
            End If
            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then


                    If Fila.Cells(9).Text.ToString().Equals("EMITIDO") Or Fila.Cells(9).Text.ToString().Equals("ENTREGADO") Then

                        variables = variables + " FILA-> " + CStr(contador)

                        num_control = Fila.Cells(2).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())

                        estatus_cheque = Fila.Cells(9).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())

                        plaza_agremiado = Fila.Cells(13).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(13).Text.ToString())

                        no_cheque = CStr(Fila.Cells(8).Text.ToString())
                        variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    Else
                        lbl_status.Text = "Este cheque no puede ser 'PAGADO' debido a su estatus, verifique que el cheque este en estatus 'ENTREGADO'"
                        Exit Sub

                    End If

                Else
                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            lbl_status.Text = ex.Message
        End Try

        Try
            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("NUM_CONTROL", Session("adVarChar"), Session("adParamInput"), 50, num_control)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("NO_CHEQUE", Session("adVarChar"), Session("adParamInput"), 50, no_cheque)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("ID_USUARIO", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("PLAZA_AGREMIADO", Session("adVarChar"), Session("adParamInput"), 50, plaza_agremiado)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "UPD_ESTATUS_PAGAR_CHEQUE_AHORRO"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then
                Dim existe As Integer = Session("rs").FIELDS("EXISTE").value.ToString
                Dim actualizado As Integer = Session("rs").FIELDS("ACTUALIZADO").value.ToString
                If existe = 1 And actualizado = 1 Then
                    txt_IdCliente1.Text = num_control

                    lbl_status.Text = "Registro actualizado correctamente"
                Else
                    lbl_status.Text = "Alerta: El estatus del cheque no fue actualizado. "
                End If
            End If
        Catch ex As Exception
            lbl_status.Text = ex.Message
        Finally
            Session("Con").Close()
            consultarInformacionDeCierreAgremiado(plaza_agremiado)
        End Try

    End Sub
    Protected Sub GENERAR_CHEQUE()
        Dim fecha As String = ""
        Dim no_cheque As String = String.Empty

        Dim AGREMIADO As String = String.Empty
        Dim RFC As String = String.Empty
        Dim NUMCHEQUE As String = String.Empty
        Dim CANTIDAD As String = String.Empty
        Dim CANTIDAD_LETRA As String = String.Empty
        Dim FECHA_ As String = String.Empty
        Dim DELEGACION As String = String.Empty
        Dim AGREMIADO2 As String = String.Empty
        Dim CANTIDAD2 As String = String.Empty
        Dim CANTIDAD_LETRA2 As String = String.Empty
        Dim FECHA_2 As String = String.Empty


        If chb_Fecha.Checked Then
            fecha = ""
        Else
            fecha = txb_FechaCheque.Text
        End If

        Try

            If DAG_Analisis.Rows.Count < 1 Then
                lbl_status.Text = "No hay registros para procesar"
                Exit Sub
            End If
            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then


                    If Fila.Cells(8).Text.ToString().Equals("&nbsp;") Or Fila.Cells(8).Text.ToString().Equals("") Or Fila.Cells(8).Text.ToString().Equals("0") Then
                        lbl_status.Text = "Este registro no cuenta con número de cheque"
                        Exit Sub
                    ElseIf Not Fila.Cells(9).Text.ToString().Equals("EMITIDO") Then
                        lbl_status.Text = "El cheque debe estar en estatus 'EMITIDO' para poder ser generado"
                        Exit Sub
                    Else
                        no_cheque = CStr(Fila.Cells(8).Text.ToString())
                    End If

                Else
                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub
                End If
            Next

        Catch ex As Exception

        End Try

        Session("ms") = New System.IO.MemoryStream()
        'Crea un reader para la solicitud

        Dim Reader As iTextSharp.text.pdf.PdfReader = Nothing
        'Ruta donde está el PDF
        Reader = New iTextSharp.text.pdf.PdfReader(Session("APPATH").ToString + "\DocPlantillas\Solicitudes\CHEQUE_FINAL.pdf")
        'Traigo el total de paginas
        Dim n As Integer = 0
        n = Reader.NumberOfPages

        'Traigo el tamaño de la primera pagina
        Dim psize As iTextSharp.text.Rectangle
        psize = Reader.GetPageSize(1)

        Dim width, height As Single
        width = psize.Width
        height = psize.Height

        Dim document As New iTextSharp.text.Document(psize, 0, 0, 0, 0)

        With document
            .AddAuthor("SALTILLO -  SALTILLO")
            .AddCreationDate()
            .AddCreator("SALTILLO - Cheque")
            .AddSubject("Cheque")
            .AddTitle("Cheque")
            .AddKeywords("Cheque")
            .Open()
        End With

        'CREACION DE UN WRITER QUE LEA EL DOCUMENTO
        Dim XT, YT, XAux As Single
        Dim writer As iTextSharp.text.pdf.PdfWriter
        writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, Session("ms"))

        'Se abre el documento
        document.Open()
        '-----------J
        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 10, fecha)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMCHEQUE", Session("adVarChar"), Session("adParamInput"), 10, no_cheque)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, "0")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 10, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CHEQUE_AHORRO_RETRO"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").EOF Then


            AGREMIADO = Session("rs").FIELDS("AGREMIADO").value.ToString
            RFC = Session("rs").FIELDS("RFC").value.ToString
            NUMCHEQUE = Session("rs").FIELDS("NUM_CHEQUE").value.ToString
            CANTIDAD = Session("rs").FIELDS("SALDO_AHORRO").value.ToString
            CANTIDAD_LETRA = Session("rs").FIELDS("SALDO_AHORRO_LETRA").value.ToString
            FECHA_ = Session("rs").FIELDS("FECHA").value.ToString
            DELEGACION = Session("rs").FIELDS("DELEGACION").value.ToString
            AGREMIADO2 = Session("rs").FIELDS("AGREMIADO").value.ToString
            CANTIDAD2 = Session("rs").FIELDS("SALDO_AHORRO").value.ToString
            CANTIDAD_LETRA2 = Session("rs").FIELDS("SALDO_AHORRO_LETRA").value.ToString
            FECHA_2 = Session("rs").FIELDS("FECHA").value.ToString
        End If
        Session("Con").Close()
        '---------J


        Dim cb As iTextSharp.text.pdf.PdfContentByte
        cb = writer.DirectContent

        ' METO LA SOLICITUD ORIGINAL
        Dim Cheque As iTextSharp.text.pdf.PdfImportedPage

        Cheque = writer.GetImportedPage(Reader, 1)
        cb.AddTemplate(Cheque, 1, 0, 0, 1, 0, 0)

        'ready to draw text
        cb.BeginText()
        Dim bf As iTextSharp.text.pdf.BaseFont
        'Solo tiene 3 formatos Helvetica,Time new ,Arial pero la recomendada es la Helvetica
        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 9)

        Dim X, Y As Single
        Dim X2, Y2 As Single
        Dim distanciaHorizontal As Integer = 240.0R
        Dim distanciaVertical As Integer = 15

        X = 450  'X empieza de izquierda a derecha
        ''y estaba en 50
        Y = 735 'Y empieza de abajo hacia arriba

        Y2 = 595
        X2 = 60


        Dim XOrdena As Integer
        Dim YOrdena As Integer


        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FECHA_, 430, 743, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, AGREMIADO, 35, 695, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CANTIDAD.Replace("$", ""), 480, 698, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CANTIDAD_LETRA, 35, 669, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, AGREMIADO2, 105, 520, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, NUMCHEQUE, 428, 520, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, RFC, 130, 484, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, FECHA_2, 267, 484, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CANTIDAD2, 390, 484, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, DELEGACION, 510, 484, 0)
        cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, CANTIDAD_LETRA, 190, 430, 0)

        cb.EndText()

        document.Close()
        '---------

    End Sub
    Protected Sub GENERAR_POLIZA_CHEQUE()
        Dim MyPDF As New PDFCreator(Session("APPATH").ToString, "DocPlantillas\Solicitudes", "Word", "POLIZA_CHEQUER", "POLIZA_CHEQUER")
        Dim etiquetas() As String = {"NOMBRE_AGREMIADO", "RFC", "NUMERO", "CANTIDAD", "CANTIDAD_LETRA", "FECHA", "CENTRO_TRAB"}
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim dteq As New Data.DataTable()

        Dim num_control As String = String.Empty
        Dim no_cheque As String = String.Empty
        Dim estatus_cheque As String = String.Empty

        Dim variables As String = ""
        Dim contador As Integer = 0

        Try

            If DAG_Analisis.Rows.Count < 1 Then
                lbl_status.Text = "No hay registros para procesar"
                Exit Sub
            End If
            For Each Fila As GridViewRow In DAG_Analisis.Rows
                If Not Fila Is Nothing Then


                    If Fila.Cells(8).Text.ToString().Equals("&nbsp;") Or Fila.Cells(8).Text.ToString().Equals("") Then
                        lbl_status.Text = "Este registro no cuenta con número de cheque"
                        Exit Sub
                    Else

                        variables = variables + " FILA-> " + CStr(contador)

                        num_control = Fila.Cells(2).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(2).Text.ToString())

                        estatus_cheque = Fila.Cells(9).Text.ToString()
                        variables = variables + " " + CStr(Fila.Cells(7).Text.ToString())

                        no_cheque = CStr(Fila.Cells(8).Text.ToString())
                        variables = variables + " " + CStr(Fila.Cells(8).Text.ToString())

                    End If

                Else
                    lbl_status.Text = "No hay registros para procesar"
                    Exit Sub
                End If
            Next

        Catch ex As Exception

        End Try


        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA", Session("adVarChar"), Session("adParamInput"), 20, txb_FechaCheque.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMCHEQUE", Session("adVarChar"), Session("adParamInput"), 20, no_cheque)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 20, "0")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 20, "")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_DATOS_CHEQUE_AHORRO_RETRO"
        Session("rs") = Session("cmd").Execute()
        custDA.Fill(dteq, Session("rs"))
        Session("Con").Close()

        For i As Integer = 0 To dteq.Rows.Count - 1
            For j As Integer = 0 To dteq.Columns.Count - 1
                MyPDF.remplazarEtiqueta(etiquetas(j), dteq(i)(j))
            Next
        Next

        MyPDF.save(Response, Session)
    End Sub
    Private Sub insertarNotaDeCheque()

        Dim no_cheque As String = String.Empty
        Dim estatus_nota As String = cmbTipoNota.SelectedValue()
        Dim notas As String = String.Empty

        If txt_notas.Text.Length > 2000 Then
            lbl_status.Text = "Alerta: Las notas no pueden sobrepasar 2000 caracteres."
            Exit Sub
        Else
            notas = txt_notas.Text
        End If




        If cmbChequeNota.SelectedValue.Equals("-1") Then
            lbl_status.Text = "Alerta: Seleccione número de cheque. "
            Exit Sub
        Else
            no_cheque = cmbChequeNota.SelectedValue()
        End If

        Try

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure

            Session("parm") = Session("cmd").CreateParameter("NUM_CHEQUE", Session("adVarChar"), Session("adParamInput"), 50, no_cheque)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("NOTAS", Session("adVarChar"), Session("adParamInput"), 2000, notas)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("ID_USUARIO", Session("adVarChar"), Session("adParamInput"), 50, Session("USERID").ToString)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("parm") = Session("cmd").CreateParameter("CVE_ESTATUSOPE", Session("adVarChar"), Session("adParamInput"), 50, estatus_nota)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("cmd").CommandText = "INS_NOTA_PARA_CHEQUE"
            Session("rs") = Session("cmd").Execute()

            If Not Session("rs").eof Then
                Dim insertado As Integer = Session("rs").FIELDS("INSERTADO").value.ToString

                If insertado = 1 Then
                    lbl_status.Text = "Nota registrada correctamente."
                Else
                    lbl_status.Text = "Alerta: La nota no fue registrada correctamente."
                End If
            End If

        Catch ex As Exception
            lbl_status.Text = ex.Message
        Finally
            Session("Con").Close()
            consultarNotasDeCheque(CInt(no_cheque))
        End Try

    End Sub

    Private Sub consultarNotasDeCheque(num_cheque As Integer)

        Dim custDA As New OleDb.OleDbDataAdapter()
        Dim dtAnalisis As New DataTable()

        Try

            Session("cmd") = New ADODB.Command()
            Session("Con").Open()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("cmd").CommandText = "SEL_NOTAS_DE_CHEQUE"
            Session("parm") = Session("cmd").CreateParameter("NUM_CHEQUE", Session("adVarChar"), Session("adParamInput"), 20, num_cheque)
            Session("cmd").Parameters.Append(Session("parm"))

            Session("rs") = Session("cmd").Execute()

            'se agregan los expedientes a una tabla en memoria
            custDA.Fill(dtAnalisis, Session("rs"))

            'se vacian los expedientes al formulario
            gv_Notas.DataSource = dtAnalisis
            gv_Notas.DataBind()

        Catch ex As Exception

        Finally
            Session("Con").Close()
        End Try

    End Sub

#End Region

#Region “Eventos”

    Protected Sub btn_Continuar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Continuar.Click

        lbl_status.Text = ""
        If txt_IdCliente1.Text = "" Then
            lbl_status.Text = "Ingrese número de control"
        Else
            consultarInformacionDeCierreAgremiado()
        End If

    End Sub

    Protected Sub btn_Asignar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_asignar.Click

        lbl_status.Text = ""
        'AsignarCheques()
        AsignarChequeTrabajador()

    End Sub

    Protected Sub btn_Entregar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Entregar.Click
        lbl_status.Text = ""
        EntregarChequeTrabajador()
    End Sub

    Protected Sub btn_Pagar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_PagarCheques.Click
        lbl_status.Text = ""
        pagarChequeTrabajador()
    End Sub

    Protected Sub btn_Cancelar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        lbl_status.Text = ""
        cancelarChequeTrabajador()
    End Sub

    Protected Sub chb_Fecha_checked(ByVal sender As Object, ByVal e As EventArgs) Handles chb_Fecha.CheckedChanged

        If chb_Fecha.Checked Then
            txb_FechaCheque.Enabled = False
        Else
            txb_FechaCheque.Enabled = True
        End If
    End Sub
    Protected Sub btn_Imprimir_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_imprimir.Click
        lbl_status.Text = ""

        If chb_Fecha.Checked = False And txb_FechaCheque.Text = "" Then
            lbl_status.Text = "Seleccionar alguna de las dos opciones de fecha."
            'lbl_status.Visible = True
            Return
        End If

        GENERAR_CHEQUE()

        Try

            With Response
                .BufferOutput = True
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/octet-stream"
                .AddHeader("Content-disposition",
                            "attachment; filename= CHEQUE.pdf")
                Response.Cache.SetNoServerCaching()
                Response.Cache.SetNoStore()
                Response.Cache.SetMaxAge(System.TimeSpan.Zero)

                Dim pdfAsByteArray As Byte() = Session("ms").ToArray()

                .OutputStream.Write(pdfAsByteArray, 0, pdfAsByteArray.Length)
                .End()
                .Flush()
            End With

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btn_Poliza_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPoliza.Click
        lbl_status.Text = ""

        GENERAR_POLIZA_CHEQUE()

    End Sub

    Private Sub ckb_deshacer_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_deshacer.CheckedChanged
        lbl_status.Text = ""
        If ckb_deshacer.Checked Then
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Deshacer"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Deshacer"), CheckBox)
                chkRow.Checked = 0
            Next
        End If

    End Sub

    Private Sub ckb_ImprimirTodos_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_ImprimirTodos.CheckedChanged
        lbl_status.Text = ""
        If ckb_ImprimirTodos.Checked Then
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Imprimir"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Imprimir"), CheckBox)
                chkRow.Checked = 0
            Next
        End If

    End Sub

    Private Sub ckb_Entregado_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_Entregado.CheckedChanged
        lbl_status.Text = ""
        If ckb_Entregado.Checked Then
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Entregar"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Entregar"), CheckBox)
                chkRow.Checked = 0
            Next
        End If

    End Sub

    Private Sub ckb_Pagar_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_Pagar.CheckedChanged
        lbl_status.Text = ""
        If ckb_Pagar.Checked Then
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Pagar"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Pagar"), CheckBox)
                chkRow.Checked = 0
            Next
        End If

    End Sub

    Private Sub ckb_cancelar_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_cancelar.CheckedChanged
        lbl_status.Text = ""
        If ckb_cancelar.Checked Then
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Cancelar"), CheckBox)
                chkRow.Checked = 1
            Next
        Else
            For i As Integer = 0 To DAG_Analisis.Rows.Count() - 1
                Dim chkRow As CheckBox = TryCast(DAG_Analisis.Rows(i).FindControl("chb_Cancelar"), CheckBox)
                chkRow.Checked = 0
            Next
        End If

    End Sub

    Private Sub cmbSistema_SelectionChanged(sender As Object, e As EventArgs) Handles cmb_sistema.SelectedIndexChanged

        lbl_status.Text = ""

        If cmb_sistema.SelectedValue <> "ELIJA" Then
            consultarInformacionDeCierreAgremiado(cmb_sistema.SelectedValue)
        End If



    End Sub
    ''btn_GuardarNotas   btnListdoDeCheques

    Protected Sub btn_GuardarNotas_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_GuardarNotas.Click

        lbl_status.Text = ""
        If cmbTipoNota.SelectedValue.Equals("-1") Then
            lbl_status.Text = "Seleccione tipo de nota para continuar."
        ElseIf txt_notas.Text = "" Then
            lbl_status.Text = "Ingrese nota para continuar."
        Else
            insertarNotaDeCheque()
        End If

    End Sub
    ''btnVerNotasCheque.visible =True
    Protected Sub btnVerNotasCheque_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerNotasCheque.Click

        lbl_status.Text = ""
        If cmbChequeNota.SelectedValue.Equals("-1") Then
            lbl_status.Text = "Seleccione #cheque para continuar."
        Else
            consultarNotasDeCheque(cmbChequeNota.SelectedValue())
        End If

    End Sub

    Protected Sub btnListdoDeCheques_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnListdoDeCheques.Click

        consultaChequesGenerados()

    End Sub


#End Region

End Class