Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports StudentsAssessment.CreateAccounts1

Public Class CreateAccounts1
    '<Required(ErrorMessage:="Account type is required")>
    <Display(Name:="ACCOUNT TYPE")>
    Public Property TypeofAccount As String

    <Required(ErrorMessage:="Firstname is required")>
    <StringLength(25)>
    <Display(Name:="First Name")>
    Public Property Firstname() As String
    <Required(ErrorMessage:="Lastname is required")>
    <Display(Name:="Last Name")>
    <StringLength(25)>
    Public Property Lastname As String

    <Required(ErrorMessage:="Username is required")>
    <StringLength(25)>
    Public Property Username As String

    '<Required(ErrorMessage:="Password is required")>
    '<StringLength(25)>
    'Public Property Password As String

    <Required(ErrorMessage:="School name is required")>
    <Display(Name:="SCHOOL")>
    Public Property School As String

    <Required(ErrorMessage:="School name is required")>
    <Display(Name:="SCHOOL:")>
    Public Property SchoolsList As List(Of SelectListItem)

    Public Property SelectedSchool As String 'This MUST BE a property (not just a string) for a value to come out

    Public Sub New()
        SchoolsList = New List(Of SelectListItem)
    End Sub
    Public Function CreateTeacherAccount(ByRef strError As String) As Boolean
        Dim bCreated As Boolean
        Try
            Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
            Using objConn As New SqlConnection(connString)
                '1.Check if this acount already exists
                Dim strSQL = <![CDATA[
                    SELECT COUNT(TeacherID) FROM Teachers WHERE Username=@Username AND Password=@Password
                    ]]>.Value()
                Using objCommand1 As New SqlCommand
                    With objCommand1
                        .Connection = objConn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@Username", Username)
                        .Parameters.AddWithValue("@Password", "teacher")
                    End With
                    'Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    If objCommand1.ExecuteScalar() > 0 Then
                        bCreated = False
                        strError = String.Format("Username '{0}' already exists.", Username)
                    Else
                        Using objCommand2 As New SqlCommand
                            strSQL = <![CDATA[
                   INSERT INTO Teachers(Firstname, Lastname, Username, Password,Created,Updated) VALUES(@Firstname, @Lastname, @Username, @Password,@Created,@Updated)
                    ]]>.Value()
                            With objCommand2
                                .Connection = objConn
                                ' .Connection.Open()
                                .CommandText = strSQL
                                .Parameters.AddWithValue("@Firstname", Firstname)
                                .Parameters.AddWithValue("@Lastname", Lastname)
                                .Parameters.AddWithValue("@Username", Username)
                                .Parameters.AddWithValue("@Password", "teacher")
                                .Parameters.AddWithValue("@Updated", DateTime.Now.ToShortDateString)
                                .Parameters.AddWithValue("@Created", DateTime.Now.ToShortDateString)
                            End With
                            If objCommand2.ExecuteNonQuery Then
                                bCreated = True
                                'Get the new ID
                                Dim iIDNo As Integer
                                Using objCommand3 As SqlCommand = New SqlCommand
                                    With objCommand3
                                        .Connection = objConn
                                        '.Connection.Open() 'already open at this point
                                        .CommandText = "SELECT @@Identity"
                                    End With
                                    iIDNo = objCommand3.ExecuteScalar()
                                End Using
                            Else
                                bCreated = False
                            End If
                        End Using
                    End If
                End Using
            End Using
            ' End Using
        Catch ex As Exception
            bCreated = False
        End Try
        Return bCreated
    End Function

    Public Function CreateStudentAccount(ByRef strError As String) As Boolean
        Dim bCreated As Boolean
        Try
            Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
            Using objConn As New SqlConnection(connString)
                '1.Check if this acount already exists
                Dim strSQL = <![CDATA[
                    SELECT COUNT(studentID) FROM Students WHERE Username=@Username AND Password=@Password
                    ]]>.Value()
                Using objCommand1 As New SqlCommand
                    With objCommand1
                        .Connection = objConn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@Username", Username)
                        .Parameters.AddWithValue("@Password", "student")
                    End With
                    'Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    If objCommand1.ExecuteScalar() > 0 Then
                        bCreated = False
                        strError = String.Format("Username '{0}' already exists.", Username)
                    Else
                        Using objCommand2 As New SqlCommand
                            strSQL = <![CDATA[
                   INSERT INTO Students(Firstname, Lastname, Username, Password,Created,Updated) VALUES(@Firstname, @Lastname, @Username, @Password,@Created,@Updated)
                    ]]>.Value()
                            With objCommand2
                                .Connection = objConn
                                ' .Connection.Open()
                                .CommandText = strSQL
                                .Parameters.AddWithValue("@Firstname", Firstname)
                                .Parameters.AddWithValue("@Lastname", Lastname)
                                .Parameters.AddWithValue("@Username", Username)
                                .Parameters.AddWithValue("@Password", "student")
                                .Parameters.AddWithValue("@Updated", DateTime.Now.ToShortDateString)
                                .Parameters.AddWithValue("@Created", DateTime.Now.ToShortDateString)
                            End With
                            If objCommand2.ExecuteNonQuery Then
                                bCreated = True
                                'Get the new ID
                                Dim iIDNo As Integer
                                Using objCommand3 As SqlCommand = New SqlCommand
                                    With objCommand3
                                        .Connection = objConn
                                        '.Connection.Open() 'already open at this point
                                        .CommandText = "SELECT @@Identity"
                                    End With
                                    iIDNo = objCommand3.ExecuteScalar()
                                End Using
                            Else
                                bCreated = False
                            End If
                        End Using
                    End If
                End Using
            End Using
            ' End Using
        Catch ex As Exception
            bCreated = False
        End Try
        Return bCreated
    End Function
End Class
