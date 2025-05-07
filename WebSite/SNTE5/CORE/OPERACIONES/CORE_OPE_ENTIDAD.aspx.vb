Public Class CORE_OPE_ENTIDAD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Configuración de Entidad", "Configuración de Entidad")

        If Not Me.IsPostBack Then
            LlenaCnfEntidad()

        End If

    End Sub


    Private Sub LlenaCnfEntidad()



        Dim ESTADO As String
        Dim MUNICIPIO As String
        Dim ASENTAMIENTO As String


        Session("Con") = CreateObject("ADODB.Connection")
        Session("Con").ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Session("Con").ConnectionTimeout = 240
        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "SEL_CNFENTIDAD"
        Session("rs") = Session("cmd").Execute()
        If Not Session("rs").eof Then
            txt_CVENTIDAD.Text = Session("rs").Fields("CVEENTIDAD").value.ToString
            txt_ABREEMP.Text = Session("rs").Fields("ABRENT").value.ToString
            txt_RFCENTIDAD.Text = Session("rs").Fields("RFC").value.ToString
            txt_RAZONENTIDAD.Text = Session("rs").Fields("RAZON").value.ToString
            txt_cp.Text = Session("rs").Fields("CP").value.ToString
            ESTADO = Session("rs").Fields("IDESTADO").value.ToString
            MUNICIPIO = Session("rs").Fields("IDMUNICIPIO").value.ToString
            ASENTAMIENTO = Session("rs").Fields("ASENTAMIENTO").value.ToString
            txt_CALLE.Text = Session("rs").Fields("CALLE").value.ToString
            txt_NUMEXT.Text = Session("rs").Fields("NUMEXT").value.ToString
            txt_NUMINT.Text = Session("rs").Fields("NUMINT").value.ToString
            txt_FECHINIOPE.Text = Session("rs").Fields("FECHINIOPE").value.ToString
            txt_REPLEGAL.Text = Session("rs").Fields("REPLEG").value.ToString
        End If
        Session("Con").Close()

        busquedaCP(txt_cp.Text)
        ddl_Estado.SelectedItem.Value = ESTADO
        ddl_municipio.SelectedItem.Value = MUNICIPIO
        ddl_asentamiento.SelectedItem.Value = ASENTAMIENTO


    End Sub


    Protected Sub btn_buscadat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscadat.Click
        busquedaCP(txt_cp.Text)

    End Sub


    Private Sub busquedaCP(ByVal CP As String)
        ddl_Estado.Items.Clear()
        ddl_municipio.Items.Clear()
        ddl_asentamiento.Items.Clear()
        If txt_cp.Text = "" Then
            Exit Sub
        End If

        Session("Con").Open()
        Session("rs") = CreateObject("ADODB.Recordset")
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, CP)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DATOS_x_CP"

        Session("rs") = Session("cmd").Execute()



        Dim idedo As String = ""
        Dim idmuni As String = ""

        If Not Session("rs").EOF Then 'SE ENCONTRARON DATOS PARA EL CP


            idedo = Session("rs").Fields("CATCP_ID_ESTADO").Value.ToString
            idmuni = Session("rs").Fields("CATCP_ID_MUNICIPIO").Value.ToString

            Dim item_edo As New ListItem(Session("rs").Fields("CATCP_ESTADO").Value.ToString, idedo)
            ddl_Estado.Items.Add(item_edo)

            Dim item_mun As New ListItem(Session("rs").Fields("CATCP_MUNICIPIO").Value.ToString, idmuni)
            ddl_municipio.Items.Add(item_mun)

            Do While Not Session("rs").EOF

                Dim item As New ListItem(Session("rs").Fields("CATCP_ASENTAMIENTO").Value.ToString + " (" + Session("rs").Fields("CATCP_TIPO_ASENTAMIENTO").Value.ToString + ")", Session("rs").Fields("CATCP_ID_ASENTAMIENTO").Value.ToString)

                ddl_asentamiento.Items.Add(item)

                Session("rs").movenext()
            Loop

        End If
        Session("Con").Close()
    End Sub

    Protected Sub btn_btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click

        ModificaCNFCAP()


    End Sub

    'METODO GUARDA LOS DATOS DE CONFIGURACION DE CAPTACION 
    Private Sub ModificaCNFCAP()

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CVENTIDAD", Session("adVarChar"), Session("adParamInput"), 20, txt_CVENTIDAD.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ABRENTIDAD", Session("adVarChar"), Session("adParamInput"), 10, txt_ABREEMP.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RFC", Session("adVarChar"), Session("adParamInput"), 12, txt_RFCENTIDAD.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("RAZON", Session("adVarChar"), Session("adParamInput"), 500, txt_RAZONENTIDAD.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CP", Session("adVarChar"), Session("adParamInput"), 10, txt_cp.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDESTADO", Session("adVarChar"), Session("adParamInput"), 10, ddl_Estado.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDMUNICIPIO", Session("adVarChar"), Session("adParamInput"), 10, ddl_municipio.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDASENTAMIENTO", Session("adVarChar"), Session("adParamInput"), 10, ddl_asentamiento.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("CALLE", Session("adVarChar"), Session("adParamInput"), 1000, txt_CALLE.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMEXT", Session("adVarChar"), Session("adParamInput"), 50, txt_NUMEXT.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NUMINT", Session("adVarChar"), Session("adParamInput"), 50, txt_NUMINT.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("FECHINIOPE", Session("adVarChar"), Session("adParamInput"), 10, txt_FECHINIOPE.Text)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("REPLEGAL", Session("adVarChar"), Session("adParamInput"), 200, txt_REPLEGAL.Text)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "UPD_CNFENTIDAD"
        Session("rs") = Session("cmd").Execute()

        Dim ACTUALIZA As String
        ACTUALIZA = Session("rs").Fields("ACTUALIZA").Value.ToString

        If ACTUALIZA = "SI" Then


            lbl_Alerta.Text = "Guardado correctamente"



        Else

            lbl_Alerta.Text = ACTUALIZA

        End If

        Session("Con").Close()

        LlenaCnfEntidad()



    End Sub

End Class