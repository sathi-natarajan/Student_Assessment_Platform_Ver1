Imports System.Data.OleDb
Imports System.IO

Public Class Utility
    Public Shared Function ConvertCSVtoDataTable(strFilePath As String) As DataTable
        Dim dt As New DataTable()
        Using sr As New StreamReader(strFilePath)
            Dim headers As String() = sr.ReadLine().Split(","c)
            For Each header As String In headers
                dt.Columns.Add(header)
            Next

            While Not sr.EndOfStream
                Dim rows As String() = sr.ReadLine().Split(","c)
                If rows.Length > 1 Then
                    Dim dr As DataRow = dt.NewRow()
                    For i As Integer = 0 To headers.Length - 1
                        dr(i) = rows(i).Trim()
                    Next
                    dt.Rows.Add(dr)
                End If

            End While
        End Using


        Return dt
    End Function


    Public Shared Function ConvertXSLXtoDataTable(strFilePath As String, connString As String) As DataTable
        Dim oledbConn As New OleDbConnection(connString)
        Dim dt As New DataTable()
        Try

            oledbConn.Open()
            Dim cmd As New OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn)
            Dim oleda As New OleDbDataAdapter()
            oleda.SelectCommand = cmd
            Dim ds As New DataSet()
            oleda.Fill(ds)


            dt = ds.Tables(0)
        Catch
        Finally

            oledbConn.Close()
        End Try

        Return dt

    End Function
End Class
