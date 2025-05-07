Imports System.Data.SqlClient

Public Class UNI_DataAccess

    Private Property NombreCampo As String
    Private Property Dato As Object
    Private ParametersList As New List(Of UNI_DataAccess)
    Dim cnx As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringg").ToString)

    Public Sub AddParameters(ByVal NombreCampoSQL As String, ByVal DatoSQL As Object)
        Dim WebApi As New UNI_DataAccess
        WebApi.NombreCampo = NombreCampoSQL
        WebApi.Dato = DatoSQL
        ParametersList.Add(WebApi)
    End Sub

    Public Sub Execute(ByVal NombreSP As String)

        Dim cmd As New SqlCommand(NombreSP, cnx)
        cmd.CommandType = CommandType.StoredProcedure
        For Each List In From a As UNI_DataAccess In ParametersList Select a.NombreCampo, a.Dato
            cmd.Parameters.Add(New SqlParameter(List.NombreCampo, List.Dato))
        Next
        cnx.Open()
        cmd.ExecuteNonQuery()
        cnx.Close()
        ParametersList.Clear()
    End Sub

    Public Function ExecuteReader(ByVal NombreSP As String) As DataSet
        Dim cmd As New SqlDataAdapter(NombreSP, cnx)
        cmd.SelectCommand.CommandType = CommandType.StoredProcedure
        For Each List In From a As UNI_DataAccess In ParametersList Select a.NombreCampo, a.Dato
            cmd.SelectCommand.Parameters.Add(New SqlParameter(List.NombreCampo, List.Dato))
        Next
        Dim ds As New DataSet
        cmd.Fill(ds)
        ParametersList.Clear()
        Return ds
    End Function
End Class
