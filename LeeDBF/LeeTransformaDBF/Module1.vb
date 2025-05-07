Imports System.Data.OleDb
Imports System.IO

Module Module1

    Sub Main()

        Try
            Dim clArgs() As String = Environment.GetCommandLineArgs()
            Dim Tabla As String = System.IO.Path.GetFileNameWithoutExtension(clArgs(1).ToString())

            Dim SQL As String = "SELECT * FROM " + Tabla
            Dim con = New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + clArgs(1).ToString() + ";Collating Sequence=machine")
            Dim cmd = New OleDbCommand() With {.Connection = con, .CommandType = CommandType.Text}
            cmd.CommandText = SQL
            Dim rdr As OleDbDataReader
            Dim col As DataColumn
            Dim row As DataRow
            Dim dbfTable As New DataTable

            Try
                con.Open()
                rdr = cmd.ExecuteReader

                For i = 0 To rdr.FieldCount - 1
                    col = New DataColumn("COL" + i.ToString, rdr.GetFieldType(i))
                    dbfTable.Columns.Add(col)
                Next

                While rdr.Read
                    row = dbfTable.NewRow
                    For i = 0 To rdr.FieldCount - 1
                        Try
                            row(i) = rdr(i)
                        Catch ex As Exception
                            row(i) = DBNull.Value
                        End Try
                    Next

                    dbfTable.Rows.Add(row)
                End While

                If dbfTable.Rows.Count() = 0 Then
                    Environment.Exit(-1)
                Else
                    Dim ruta As String = clArgs(1).ToString().Replace(".dbf", "")
                    ruta = ruta.Replace(".DBF", "")

                    CreaCSV(ruta, dbfTable)

                    'Cerrar aplicación
                    Environment.Exit(0)
                End If

            Catch ex As Exception
                Environment.Exit(-1)
            Finally
                con.Close()
            End Try
        Catch ex As Exception
            Console.WriteLine(ex.Message.ToString())
        End Try

    End Sub


    Sub CreaCSV(ByVal ruta As String, ByVal dataDBF As DataTable)
        Dim filestream As FileStream = New FileStream(ruta + ".csv", FileMode.Create)
        Dim streamwriter = New StreamWriter(filestream)
        streamwriter.AutoFlush = True
        Console.SetOut(streamwriter)
        Console.SetError(streamwriter)
        Dim rowData As Integer = dataDBF.Rows.Count()
        Dim cadenaColumnas As String

        For Each row As DataRow In dataDBF.Rows
            cadenaColumnas = String.Empty

            For i = 0 To dataDBF.Columns.Count - 1

                If cadenaColumnas = String.Empty Then
                    cadenaColumnas = row("COL" + i.ToString).ToString()
                Else
                    cadenaColumnas = cadenaColumnas + "," + row("COL" + i.ToString).ToString()
                End If

            Next
            Console.WriteLine(cadenaColumnas)
        Next

    End Sub

End Module
