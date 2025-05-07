Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Public Class CRED_EXP_POLIZAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TryCast(Master, MasterMascore).CargaASPX("Pólizas Contables", "Pólizas Contables")

        If Not IsPostBack Then

            LlenaInstituciones()

        End If

    End Sub

    Private Sub LlenaInstituciones()

        ddl_entidades.Items.Clear()
        Dim elija As New ListItem("ELIJA", "-1")
        ddl_entidades.Items.Add(elija)

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("cmd").CommandText = "SEL_ENTIDADES"
        Session("rs") = Session("cmd").Execute()

        Do While Not Session("rs").EOF
            Dim item As New ListItem(Session("rs").Fields("ID").Value.ToString + " .- " + Session("rs").Fields("NOMBRE").Value.ToString, Session("rs").Fields("ID").Value.ToString)
            ddl_entidades.Items.Add(item)
            Session("rs").movenext()
        Loop

        Session("Con").Close()

    End Sub

    Protected Sub btn_poliza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_poliza.Click

        GenerarExcel()

    End Sub

    Private Sub GenerarExcel()

        'Declaro la App
        Dim xlApp As New Microsoft.Office.Interop.Excel.Application
        xlApp.DisplayAlerts = False
        'Declaro el Workbook
        Dim xlWorkbook As Microsoft.Office.Interop.Excel.Workbook
        'Abro el archivo deseado
        xlWorkbook = xlApp.Workbooks.Open(Session("APPATH").ToString + "\Excel\Poliza.xlsx")
        'Declaro el WorkSheet
        Dim xlWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorksheet = xlWorkbook.Sheets("Contable")

        'Encabezado del documento de Excel'
        xlWorksheet.Cells(2, 2) = txt_fecha_mov.Text.ToString
        xlWorksheet.Cells(3, 2) = ddl_tipo_poliza.SelectedItem.Value.ToString
        xlWorksheet.Cells(4, 2) = txt_no_cheque.Text.ToString
        Dim array_entidad() As String = Split(ddl_entidades.SelectedItem.Text, " .- ")
        xlWorksheet.Cells(5, 2) = "REGISTRO EMISION DE CREDITO ENTIDAD " + array_entidad(1).ToString

        Session("Con").Open()
        Session("cmd") = New ADODB.Command()
        Session("cmd").ActiveConnection = Session("Con")
        Session("cmd").CommandType = CommandType.StoredProcedure
        Session("parm") = Session("cmd").CreateParameter("FECHA_MOV", Session("adVarChar"), Session("adParamInput"), 10, txt_fecha_mov.Text.ToString)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("ID_ENTIDAD", Session("adVarChar"), Session("adParamInput"), 10, ddl_entidades.SelectedItem.Value)
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("USERID", Session("adVarChar"), Session("adParamInput"), 10, Session("USERID"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("parm") = Session("cmd").CreateParameter("SESION", Session("adVarChar"), Session("adParamInput"), 15, Session("Sesion"))
        Session("cmd").Parameters.Append(Session("parm"))
        Session("cmd").CommandText = "REP_POLIZA_CONTABLE"
        Session("rs") = Session("cmd").Execute()

        Dim COORDX As Integer
        COORDX = 8

        If Not Session("rs").eof Then
            Do While Not Session("rs").EOF

                xlWorksheet.Cells(COORDX, 1) = Session("rs").Fields("CUENTA").Value.ToString
                xlWorksheet.Cells(COORDX, 2) = Session("rs").Fields("CARGO").Value.ToString
                xlWorksheet.Cells(COORDX, 3) = Session("rs").Fields("ABONO").Value.ToString
                xlWorksheet.Cells(COORDX, 4) = Session("rs").Fields("DESCRIPCION").Value.ToString

                COORDX += 1
                Session("rs").movenext()

            Loop
        End If

        Session("Con").Close()

        Dim Filename, FilePath As String
        Filename = "Poliza_Contable.xls"
        FilePath = Session("APPATH").ToString + "Excel\"
        xlWorkbook.SaveAs(FilePath + Filename, FileFormat:=56)
        xlWorkbook.Close()

        Dim fs As IO.FileStream

        fs = IO.File.Open(FilePath + Filename, IO.FileMode.Open)
        Dim bytBytes(fs.Length) As Byte
        fs.Read(bytBytes, 0, fs.Length)
        fs.Close()
        'Borra el archivo creado en memoria
        DelHDFile(FilePath + Filename)
        Response.Buffer = True
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()
        Response.AddHeader("Content-disposition", String.Format("attachment;filename={0}", Filename))
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.BinaryWrite(bytBytes)
        Response.End()

    End Sub

    Private Sub DelHDFile(ByVal File As String)

        Dim fi As New IO.FileInfo(File)
        If (fi.Attributes And IO.FileAttributes.ReadOnly) <> 0 Then
            fi.Attributes = fi.Attributes Xor IO.FileAttributes.ReadOnly
        End If

        IO.File.Delete(File)

    End Sub

End Class