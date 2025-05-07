Imports System.Data
Imports System.Data.SqlClient

Public Class DataAccess
    Public CadenaCnx As String = ConfigurationManager.ConnectionStrings("ConnectionStringDA").ConnectionString
    Public Cnx As SqlConnection
    Private Cmnd As SqlCommand
    Private SqlAdapter As SqlDataAdapter
    Private DtSt As DataSet
    Private DtTbl As DataTable

    Public Function RegresaDataSet(ByVal StoredProcedure As String) As DataSet
        DtSt = New DataSet()
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(StoredProcedure, Cnx)
        Cmnd.CommandType = CommandType.StoredProcedure
        Cmnd.CommandTimeout = 0
        SqlAdapter = New SqlDataAdapter(Cmnd)
        SqlAdapter.Fill(DtSt)
        Cnx.Dispose()
        Cmnd.Dispose()
        SqlAdapter.Dispose()
        Return DtSt
    End Function

    Public Function RegresaDataSet(ByVal StoredProcedure As String, ByVal HashT As Hashtable) As DataSet
        DtSt = New DataSet()
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(StoredProcedure, Cnx)
        Cmnd.CommandType = CommandType.StoredProcedure
        Cmnd.CommandTimeout = 0

        For Each Fila As DictionaryEntry In HashT
            Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), Fila.Value)
        Next

        SqlAdapter = New SqlDataAdapter(Cmnd)
        SqlAdapter.Fill(DtSt)
        Cnx.Dispose()
        Cmnd.Parameters.Clear()
        Cmnd.Dispose()
        SqlAdapter.Dispose()
        Return DtSt
    End Function

    Public Sub GuardarActualizar(ByVal StoredProcedure As String, ByVal HashT As Hashtable)
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(StoredProcedure, Cnx)
        Cmnd.CommandType = CommandType.StoredProcedure
        Cmnd.CommandTimeout = 0

        For Each Fila As DictionaryEntry In HashT
            Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), Fila.Value)
        Next

        Cnx.Open()
        Cmnd.ExecuteScalar()
        Cnx.Close()
        Cnx.Dispose()
        Cmnd.Parameters.Clear()
        Cmnd.Dispose()
    End Sub

    Public Function RegresaDataTable(ByVal StoredProcedure As String, ByVal HashT As Hashtable) As DataTable
        DtTbl = New DataTable()
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(StoredProcedure, Cnx)
        Cmnd.CommandType = CommandType.StoredProcedure
        Cmnd.CommandTimeout = 0

        For Each Fila As DictionaryEntry In HashT
            Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), Fila.Value)
        Next

        SqlAdapter = New SqlDataAdapter(Cmnd)
        SqlAdapter.Fill(DtTbl)
        Cnx.Dispose()
        Cmnd.Parameters.Clear()
        Cmnd.Dispose()
        SqlAdapter.Dispose()
        Return DtTbl
    End Function

    Public Function RegresaDataTable(ByVal StoredProcedure As String) As DataTable
        DtTbl = New DataTable()
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(StoredProcedure, Cnx)
        Cmnd.CommandType = CommandType.StoredProcedure
        Cmnd.CommandTimeout = 0

        SqlAdapter = New SqlDataAdapter(Cmnd)
        SqlAdapter.Fill(DtTbl)
        Cnx.Dispose()
        Cmnd.Parameters.Clear()
        Cmnd.Dispose()
        SqlAdapter.Dispose()
        Return DtTbl
    End Function

    Public Function RegresaUnaCadena(ByVal ConsultaSql As String) As String
        Dim CadenaResultado As String = String.Empty
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(ConsultaSql, Cnx)
        Dim SqlReader As SqlDataReader
        Cmnd.CommandType = CommandType.Text
        Cmnd.CommandTimeout = 0
        Cnx.Open()
        SqlReader = Cmnd.ExecuteReader()

        While SqlReader.Read()
            CadenaResultado = SqlReader(0).ToString()
        End While

        SqlReader.Close()
        Cnx.Close()
        Cnx.Dispose()
        Cmnd.Dispose()
        Return CadenaResultado
    End Function

    Public Function RegresaUnaCadena(ByVal StoredProcedure As String, ByVal HashT As Hashtable) As String
        Dim CadenaResultado As String = String.Empty
        Cnx = New SqlConnection(CadenaCnx)
        Cmnd = New SqlCommand(StoredProcedure, Cnx)
        Dim SqlReader As SqlDataReader
        Cmnd.CommandType = CommandType.StoredProcedure
        Cmnd.CommandTimeout = 0

        For Each Fila As DictionaryEntry In HashT
            Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), Fila.Value)
        Next

        Cnx.Open()
        SqlReader = Cmnd.ExecuteReader()

        While SqlReader.Read()
            CadenaResultado = SqlReader(0).ToString()
        End While

        SqlReader.Close()
        Cnx.Close()
        Cnx.Dispose()
        Cmnd.Dispose()
        Return CadenaResultado
    End Function

End Class
