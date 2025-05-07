Imports System.Math

Public Class CORE_CNF_EQUIPOS_CREAR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TryCast(Me.Master, MasterMascore).CargaASPX("Edición de Equipos", "Edición Equipos")

        If Not Page.IsPostBack Then
            If Session("EQ_MODCRE") Is Nothing Then
                Response.Redirect("/LOGIN.aspx")
            Else
                'establecer opciones oficinas
                llena_sucursales()

                'ESTABLECER LOS DATOS de modificación o creación de equipos
                ObtieneDatosEquipo()
            End If
        End If
    End Sub

    Private Sub llena_sucursales()

        cmb_sucursal_busqueda.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        cmb_sucursal_busqueda.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_SUCURSALES"
        Session("rs") = Session("cmd").Execute()
        Do While Not Session("rs").EOF

            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value, Session("rs").Fields("IDSUC").Value.ToString)
            cmb_sucursal_busqueda.Items.Add(item)

            Session("rs").movenext()
        Loop
        Session("Con").Close()

    End Sub

    Private Sub ObtieneDatosEquipo()



        If String.IsNullOrEmpty(Session("EQ_MODCRE").ToString) Then
            Response.Redirect("CORE_CNF_EQUIPOS.aspx")
        ElseIf Session("EQ_MODCRE")(0) < 0 Then
            txt_numEquipo.Text = "Nuevo Equipo"
        Else
            txt_numEquipo.Text = Session("EQ_MODCRE")(1)

            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("EQ_MODCRE")(1))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EQ_COMP_DATOS"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then
                cmb_sucursal_busqueda.SelectedValue = Session("rs").Fields("IDSUC").Value.ToString
                input_txt_nombreEquipo.Text = Session("rs").Fields("NOMBRE").Value.ToString
                input_txt_direccionMac.Text = Session("rs").Fields("MAC").Value.ToString
                checkbox_activo.Checked = CBool(Session("rs").Fields("ESTATUS").Value)

                lbl_macRepetida.Visible = False

            End If
            Session("Con").Close()
        End If

    End Sub

    Protected Sub lnk_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_guardar.Click


        Dim oficina_id As String = CInt(cmb_sucursal_busqueda.SelectedItem.Value)
        Dim equipo_nom As String = input_txt_nombreEquipo.Text
        Dim equipo_mac As String = input_txt_direccionMac.Text
        Dim movil As Integer = 0
        Dim activo As Integer = Abs(CInt(checkbox_activo.Checked))


        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
        Session("cmd").CommandText = "INS_EQ_COMP"
        Session("parm") = Session("cmd").CreateParameter("ACCION", Session("adVarChar"), Session("adParamInput"), 15, Session("EQ_MODCRE")(0))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_EQUIPO", Session("adVarChar"), Session("adParamInput"), 15, Session("EQ_MODCRE")(1))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDSUC", Session("adVarChar"), Session("adParamInput"), 15, oficina_id)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MOVIL", Session("adVarChar"), Session("adParamInput"), 15, movil)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("MAC", Session("adVarChar"), Session("adParamInput"), 17, equipo_mac)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("NOMBRE", Session("adVarChar"), Session("adParamInput"), 200, equipo_nom)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ESTATUS", Session("adVarChar"), Session("adParamInput"), 15, activo)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("IDUSER", Session("adVarChar"), Session("adParamInput"), 15, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 200, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("rs") = Session("cmd").Execute()
        Session("EQ_MODCRE")(1) = Session("rs").Fields("IDEQCOM").Value
        Session("EQ_MODCRE")(0) = Session("rs").Fields("RES").Value
        Session("Con").Close()

        If Session("EQ_MODCRE")(0) = 0 Then
            ObtieneDatosEquipo()
            lbl_statuseq.Text = "Ya existe el registro"

        ElseIf Session("EQ_MODCRE")(0) = 2 Then
            lbl_statuseq.Text = "Equipo actualizado correctamente"


            Session("Con").Open()
            Session("cmd") = New ADODB.Command()
            Session("cmd").ActiveConnection = Session("Con")
            Session("cmd").CommandType = System.Data.CommandType.StoredProcedure
            Session("parm") = Session("cmd").CreateParameter("ID_EQ", Session("adVarChar"), Session("adParamInput"), 10, Session("EQ_MODCRE")(1))
            Session("cmd").Parameters.Append(Session("parm"))
            Session("cmd").CommandText = "SEL_EQ_COMP_DATOS"
            Session("rs") = Session("cmd").Execute()
            If Not Session("rs").EOF Then

                cmb_sucursal_busqueda.SelectedValue = Session("rs").Fields("IDSUC").Value.ToString
                input_txt_nombreEquipo.Text = Session("rs").Fields("NOMBRE").Value.ToString
                input_txt_direccionMac.Text = Session("rs").Fields("MAC").Value.ToString
                checkbox_activo.Checked = CBool(Session("rs").Fields("ESTATUS").Value)
                'checkbox_movil.Checked = CBool(Session("rs").Fields("MOVIL").Value)

                lbl_macRepetida.Visible = False

            End If
            Session("Con").Close()

        Else
            lbl_statuseq.Text = "Guardado correctamente"
            txt_numEquipo.Text = Session("EQ_MODCRE")(1)
        End If
        ObtieneDatosEquipo()
    End Sub

    Protected Sub limpiar_sessionV(sender As Object, e As EventArgs)
        Dim lnk As LinkButton = sender
        Dim href As String = lnk.Attributes("Redirect").ToString()
        Session.Remove("EQ_MODCRE")
        Response.Redirect(href)
    End Sub


End Class