Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class UNI_BUSQUEDA_PERSONA
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        If Not Me.IsPostBack Then

            If Request.QueryString("tipop") = "f" Or Request.QueryString("tipop") = "1" Or Request.QueryString("tipop") = "2" Then
                lst_encontrados.Items.Clear()
                btn_buscarfisica.Visible = True
                pnl_busquedafisica.Visible = True
                pnl_resultados.Visible = True
                btn_buscarfisica.Visible = True
                Session("TIPOPER") = "F"
                lbl_tipoPersona.Visible = False
                rad_pm.Visible = False
                rad_pf.Visible = False
                'CargaInstituciones()
            End If


            If Request.QueryString("tipop") = "m" Then
                lst_encontrados.Items.Clear()
                pnl_busquedamoral.Visible = True
                btn_buscarmoral.Visible = True
                pnl_resultados.Visible = True
                Session("TIPOPER") = "M"
                lbl_tipoPersona.Visible = False
                rad_pm.Visible = False
                rad_pf.Visible = False
            End If

            Session("origen") = "OUTSIDE"
            If Request.QueryString("origen") = "INSIDE" Then
                Session("origen") = Request.QueryString("origen")
            End If

            Session("BUSQUEDA") = (Request.QueryString("tipop"))

            ' LlenaInstituciones()


        End If

        If Request.QueryString("tipob") = "ori" Then
            Session("tipob") = "ori"
        Else
            Session("tipob") = "des"
        End If

        lbl_status.Text = ""
        btn_aceptarbusqueda.Enabled = False
    End Sub

    Protected Sub btn_buscarfisica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscarfisica.Click
        Dim esteitem As String
        Dim elnombre As String
        lst_encontrados.Items.Clear()
        elnombre = ""
        esteitem = ""
        ' obtieneId()

        ' If txt_numclt.Text <> "" Then
        'nNumCte = CInt(txt_numclt.Text)
        ' End If

        If txt_nombre1buscar1.Text <> "" Or txt_rfc.Text <> "" Then
            Try

                'lbl_status.Text = " NOMBRE: " + txt_nombre1buscar1.Text + " PATERNO: " + txt_paternobuscar1.Text + " MATERNO: " + txt_maternobuscar1.Text + " NUMCLIENTE: " + CStr(nNumCte) + " ORIGEN: " + Session("origen")
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("NOMBRE1", Session("adVarChar"), Session("adParamInput"), 300, txt_nombre1buscar1.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("PATERNO", Session("adVarChar"), Session("adParamInput"), 100, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("MATERNO", Session("adVarChar"), Session("adParamInput"), 100, "")
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 15, txt_rfc.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("INSTITUCION", Session("adVarChar"), Session("adParamInput"), 10, -1)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_BUSQUEDA_FISICA"
                Session("rs") = Session("cmd").Execute()
                Do While Not Session("rs").EOF
                    elnombre = Session("rs").Fields("NUMTRAB").Value.ToString + " , " + Session("rs").Fields("NOMBRE1").Value.ToString +
                        " " + Session("rs").Fields("NOMBRE2").Value.ToString + " " + Session("rs").Fields("PATERNO").Value.ToString +
                        " " + Session("rs").Fields("MATERNO").Value.ToString
                    esteitem = elnombre + ", " + Left(Session("rs").Fields("FECHANAC").Value.ToString, 10) + ", " + Session("rs").Fields("INSTI").Value.ToString
                    Dim item As New ListItem(esteitem, Session("rs").Fields("RFC").Value.ToString)
                    lst_encontrados.Items.Add(item)
                    Session("rs").movenext()
                Loop
            Catch ex As Exception
            Finally
                Session("COn").Close()
            End Try
            '----------------------------------------------------------
            If lst_encontrados.Items.Count = 0 Then
                lbl_status.Text = "No existen coincidencias"
            Else
                btn_aceptarbusqueda.Enabled = True
            End If
        Else
            lbl_status.Text = "Debe ingresar por lo menos un nombre para realizar la búsqueda, por favor, verifique."
            '----------------------------------------------------------
        End If
    End Sub
    'Private Sub CargaInstituciones()
    '    ddl_institucionBusq.Items.Clear()

    '    Dim elija As New ListItem("ELIJA", "-1")
    '    ddl_institucionBusq.Items.Add(elija)
    '    Try
    '        Session("cmd") = New ADODB.Command()
    '        Session("Con").Open()
    '        Session("cmd").ActiveConnection = Session("Con")
    '        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
    '        Session("cmd").CommandText = "SEL_INSTITUCIONES"
    '        Session("rs") = Session("cmd").Execute()

    '        Do While Not Session("rs").EOF
    '            Dim Seleccion As New ListItem(Session("rs").Fields("ID").Value.ToString + ".- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)

    '            ddl_institucionBusq.Items.Add(Seleccion)
    '            Session("rs").movenext()
    '        Loop
    '    Catch ex As Exception
    '    Finally
    '        Session("Con").Close()
    '    End Try

    'End Sub
    Protected Sub btn_aceptarbusqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aceptarbusqueda.Click
        'Dim eltexto As String
        'Dim elscriptt As String
        If lst_encontrados.SelectedItem Is Nothing Then
            lbl_status.Text = "No seleccionó una persona."
            btn_aceptarbusqueda.Enabled = True
        Else
            'eltexto = lst_encontrados.SelectedItem.Text
            'eltexto = Left(eltexto, InStr(10, eltexto, ",", CompareMethod.Binary) - 1)
            'eltexto = Right(eltexto, (Len(eltexto) - InStr(1, eltexto, ",", CompareMethod.Binary)) - 1)

            'hdn_conyuge.Value = eltexto.ToString
            'Session("idperbusca") = lst_encontrados.SelectedItem.Value.ToString
            'Session("PROSPECTO") = eltexto

            'elscriptt = "<script language='javascript'> {" +
            '    "window.returnValue=""" + eltexto + """;" +
            '    "window.close();}</script>"
            'Response.Write(elscriptt)
            Dim eltexto As String
            eltexto = lst_encontrados.SelectedItem.Text
            eltexto = Right(eltexto, (Len(eltexto) - InStr(1, eltexto, ",", CompareMethod.Binary)) - 1)
            eltexto = Left(eltexto, InStr(10, eltexto, ",", CompareMethod.Binary) - 1)
            hdn_conyuge.Value = eltexto.ToString
            'Se incorpora una nueva varible de sesión para identificar si la búsqueda se hace para un Usuario o un cliente
            If Session("BUSQUEDA") = "2" Then '2= Usuario
                Session("idperbusca_Usuario") = lst_encontrados.SelectedItem.Value.ToString
                Session("PROSPECTO_Usuario") = eltexto
            Else
                Session("IDPERSONAAFI") = lst_encontrados.SelectedItem.Value.ToString
                Session("idperbusca") = lst_encontrados.SelectedItem.Value.ToString
                Session("PROSPECTO") = eltexto
            End If


            Dim var As String = Request.ServerVariables("HTTP_USER_AGENT")
            Dim elscriptt As String = ""
            If InStr(var, "Chrome") Then

                elscriptt = "<script language='javascript'> {" +
                        "window.returnValue=""" + eltexto + """;" +
                         "window.opener.location.reload();" +
                        "window.close();}</script>"
            Else
                elscriptt = "<script language='javascript'> {" +
                        "window.returnValue=""" + eltexto + """;" +
                        "window.close();}</script>"
            End If


            Response.Write(elscriptt)
            End If
    End Sub

    Protected Sub btn_cancelarbusqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelarbusqueda.Click

        Session("idperbusca_Usuario") = Nothing
        Session("PROSPECTO_Usuario") = Nothing
        Session("idperbusca") = Nothing
        Session("PROSPECTO") = Nothing
        Session("INSTITUCION") = Nothing
        Session("BUSQUEDA") = Nothing
        Session("origen") = Nothing
        Session("TIPOPER") = Nothing
        Response.Write("<script language='javascript'> { window.returnValue=""""; window.close();}</script>")

    End Sub

    Protected Sub rad_pf_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_pf.CheckedChanged
        lst_encontrados.Items.Clear()
        btn_buscarmoral.Visible = False
        pnl_busquedamoral.Visible = False
        btn_buscarfisica.Visible = True
        pnl_busquedafisica.Visible = True
        pnl_resultados.Visible = True
        Session("TIPOPER") = "F"
    End Sub

    Protected Sub rad_pm_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_pm.CheckedChanged
        lst_encontrados.Items.Clear()
        pnl_busquedafisica.Visible = False
        btn_buscarfisica.Visible = False
        pnl_busquedamoral.Visible = True
        btn_buscarmoral.Visible = True
        pnl_resultados.Visible = True
        Session("TIPOPER") = "M"
    End Sub


    Private Function ValidaRFC(ByVal rfc As String) As Boolean
        Return Regex.IsMatch(rfc, ("^[a-zA-Z]{2,3}(\d{6})((\D|\d){3})?$"))
    End Function

    Protected Sub btn_buscarmoral_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscarmoral.Click
        Dim cRs As String
        Dim cRfc As String
        Dim nNumCte As Integer
        Dim esteitem As String
        Dim elnombre As String

        lst_encontrados.Items.Clear()
        cRs = txt_razonsocial.Text
        cRfc = txt_rfcm.Text
        nNumCte = 0
        If txt_numclientem.Text <> "" Then
            nNumCte = CInt(txt_numclientem.Text)
        End If

        If (cRs <> "" Or cRfc <> "" Or nNumCte <> 0) Then
            If (cRfc <> "" And ValidaRFC(cRfc) = False) Then
                lbl_status.Text = "Error en RFC"
                Exit Sub
            End If

            Try
                Session("cmd") = New ADODB.Command()
                Session("Con").Open()
                Session("cmd").ActiveConnection = Session("Con")
                Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
                Session("parm") = Session("cmd").CreateParameter("RAZONSOCIAL", Session("adVarChar"), Session("adParamInput"), 300, cRs)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("NOMBRECOM", Session("adVarChar"), Session("adParamInput"), 300, txt_nombrecomercial.Text)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 15, cRfc)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("parm") = Session("cmd").CreateParameter("IDPERSONA", Session("adVarChar"), Session("adParamInput"), 15, nNumCte)
                Session("cmd").Parameters.Append(Session("parm"))
                Session("cmd").CommandText = "SEL_BUSQUEDA_PERSONA_MORAL"
                Session("rs") = Session("cmd").Execute()
                Do While Not Session("rs").EOF
                    elnombre = Session("rs").Fields("IDPERSONA").Value.ToString + " , " + Session("rs").Fields("NOMBRE1").Value.ToString
                    esteitem = elnombre + ", " + Left(Session("rs").Fields("RFC").Value.ToString, 12)
                    Dim item As New ListItem(esteitem, Session("rs").Fields("IDPERSONA").Value.ToString)
                    lst_encontrados.Items.Add(item)
                    Session("rs").movenext()
                Loop
            Catch ex As Exception
            Finally
                Session("Con").Close()
            End Try
            '----------------------------------------------------------
            If lst_encontrados.Items.Count = 0 Then
                lbl_status.Text = "No existen coincidencias"
            Else
                btn_aceptarbusqueda.Enabled = True
            End If

        Else
            lbl_status.Text = "Ingrese Razon Social o RFC"
        End If
    End Sub

End Class