Imports System.IO
Imports System.Net
Imports Ionic.Zip

Public Class CRED_EXP_ESTADOS_CTA_REGION
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LlenarPeriodos()
            llenarregiones()
        End If
        TryCast(Me.Master, MasterMascore).CargaASPX("Estados de Cuenta por Región", "Estados de Cuenta por Región")


    End Sub

    Protected Sub ddl_region_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_region.SelectedIndexChanged
        lbl_estatus.Text = ""
        llenardelegaciones(ddl_region.SelectedItem.Value.ToString)
    End Sub

    Private Sub llenarregiones()
        Dim ELIJA As New ListItem("ELIJA", "-1")
        ddl_region.Items.Add(ELIJA)

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_REGIONES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_region.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub

    Private Sub llenardelegaciones(region As Integer)
        ddl_delegacion.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_delegacion.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = Data.CommandType.StoredProcedure

        Session("parm") = Session("cmd").CreateParameter("REGION", Session("adVarChar"), Session("adParamInput"), 10, region)
        Session("cmd").Parameters.Append(Session("parm"))

        Session("cmd").CommandText = "SEL_DELEGACIONES_POR_REGION"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("DELEGACION").Value.ToString, Session("rs").Fields("ID_DELEGACION").Value.ToString)
            ddl_delegacion.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()
    End Sub

    Private Sub Comprimir(ByVal Ruta As String)
        Dim nombreArchivo As String = ""
        If ddl_delegacion.SelectedValue.ToString = -1 Then
            nombreArchivo = "CARPETA_REGION_" + ddl_region.SelectedItem.ToString
        Else
            nombreArchivo = "CARPETA_REGION_" + ddl_region.SelectedItem.ToString + "_DELEGACION_" + ddl_delegacion.SelectedItem.ToString
        End If
        Response.Clear()
        Dim itempaths As String() = New String() {Ruta}
        Using zip As ZipFile = New ZipFile()
            'For i = 0 To itempaths.Length - 1
            '    zip.AddItem(itempaths(i), "")
            'Next
            'zip.Save(Ruta + "\DOCUMENTOS_FOLIO.zip")
            zip.AddDirectory(Ruta, nombreArchivo)
            'zip.AddFile(Ruta + "\DOCUMENTOS_FOLIO.zip", "Documentos")
            zip.Save(Response.OutputStream)
        End Using
        Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".zip")
        Response.ContentType = "application/octet-stream"
        Response.End()

        'Dim itempaths As String() = New String() {Ruta}
        'Dim extractPath As String = ".\extract"
        'Dim zipPath As String = ".\DOCUMENTOS_FOLIO.zip"
        'Using zip As ZipFile = New ZipFile()
        '    For i = 0 To itempaths.Length - 1
        '        zip.AddItem(itempaths(i), "")
        '    Next
        '    zip.Save(Ruta + "\DOCUMENTOS_FOLIO.zip")

        '    Response.AddHeader("Content-Disposition", "attachment; filename=MisDocumentos.zip")
        '    Response.ContentType = "application/octet-stream"
        '    Response.End()
        'End Using

        'Response.Clear()
        'Dim ElZip As ZipFile = New ZipFile()
        'Using ElZip
        '    ElZip.AddDirectory(Ruta)
        '    ElZip.AddFile(Ruta + "\DOCUMENTOS_FOLIO.zip", "Documentos")
        '    ElZip.Save(Response.OutputStream)
        'End Using
        'Response.AddHeader("Content-Disposition", "attachment; filename=MisDocumentos.zip")
        'Response.ContentType = "application/octet-stream"
        'Response.End()
    End Sub

    Protected Sub btn_descargar_EdoCuentaPrestamo_Click(sender As Object, e As EventArgs) Handles btn_descargar_EdoCuentaPrestamo.Click


        Dim PathOrigen As String
        If ddl_delegacion.SelectedValue.ToString = -1 Then
            PathOrigen = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + ddl_periodo.SelectedValue.ToString + "\" + ddl_region.SelectedValue.ToString
        Else
            PathOrigen = ConfigurationManager.AppSettings.[Get]("urlestadosdecuenta") + "\" + ddl_periodo.SelectedValue.ToString + "\" + ddl_region.SelectedValue.ToString + "\" + ddl_delegacion.SelectedItem.ToString
        End If
        If Not System.IO.Directory.Exists(PathOrigen) Then
            lbl_estatus.Text = "Error: Hace falta generar estados de cuenta."

        Else
            Comprimir(PathOrigen)
        End If
        'My.Computer.Network.DownloadFile(PathOrigen + "DOCUMENTOS_FOLIO.zip", "donwload.zip")
    End Sub

    Private Sub LlenarPeriodos()
        'ddl_periodo.Items.Clear()

        Session("cmd") = New ADODB.Command()
        Session("Con").Open()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_PERIODO_ESTADO_CUENTA"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("PERIODO").Value.ToString, Session("rs").Fields("VALOR").Value.ToString)
            ddl_periodo.Items.Add(item)
            Session("rs").movenext()
        Loop
        Session("Con").Close()
    End Sub
End Class