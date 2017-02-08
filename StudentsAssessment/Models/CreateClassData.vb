Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient


Public Class CreateClassData
    Public Sub New()
        ClassesList = New List(Of SelectListItem)
        TaughtClassesList = New List(Of SelectListItem)
        'SubjectList.Add(New SelectListItem With {.Text = "School 1", .Value = "1"})
        'SubjectList.Add(New SelectListItem With {.Text = "School 2", .Value = "2"})
        'SubjectList.Add(New SelectListItem With {.Text = "School 3", .Value = "3"})
        ' LoadClassInfo(2)
    End Sub

    <DisplayName("SELECT A CLASS TO ADD:")>
    <Required(ErrorMessage:="A class must be selected")>
    Public Property ClassesList As List(Of SelectListItem)

    <DisplayName("SELECT A CLASS TO REMOVE:")>
    <Required(ErrorMessage:="A class must be selected")>
    Public Property TaughtClassesList As List(Of SelectListItem)

    Public Property SelectedClass As String 'This MUST BE a property (not just a string) for a value to come out

    <Required(ErrorMessage:="No. of credits is required")>
    <Display(Name:="Number of credit hours:")>
    <DisplayFormat(DataFormatString:="{0:n2}", ApplyFormatInEditMode:=True)>
    Public Property NumCreditHrs() As Decimal

    <Required(ErrorMessage:="Start Date is required")>
    <Display(Name:="Class Start date:")>
    <DataType(DataType.Date)>
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:MM/dd/yyyy}")>
    Public Property dtStart As DateTime

    <Required(ErrorMessage:="Class end date is required")>
    <Display(Name:="Class End date:")>
    <DataType(DataType.Date)>
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:MM/dd/yyyy}")>
    Public Property dtEnd As DateTime

    <Required(ErrorMessage:="Class Duration is required")>
    <Display(Name:="Class Duration:")>
    Public Property ClassDuration() As Integer

    Public Function CreateClass(ByRef strError As String) As Boolean
        Dim bCreated As Boolean
        Try
            Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
            Using objConn As New SqlConnection(connString)
                '1.Check if this acount already exists
                Dim strSQL = <![CDATA[
                    SELECT TOP 1 EntryID FROM TeachersClasses WHERE TeacherID=@TeacherID AND ClassID=@ClassID
                    ]]>.Value()
                Using objCommand1 As New SqlCommand
                    With objCommand1
                        .Connection = objConn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(HttpContext.Current.Session("LoggedInTeacherID")))
                        .Parameters.AddWithValue("@ClassID", Integer.Parse(SelectedClass))
                    End With
                    'Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    If objCommand1.ExecuteScalar() > 0 Then
                        bCreated = False
                        strError = "This teacher seems to already be teaching this class"
                    Else
                        Using objCommand2 As New SqlCommand
                            strSQL = <![CDATA[
                   INSERT INTO TeachersClasses(TeacherID,ClassID) VALUES(@TeacherID,@ClassID)
                    ]]>.Value()
                            With objCommand2
                                .Connection = objConn
                                ' .Connection.Open()
                                .CommandText = strSQL
                                .Parameters.AddWithValue("@TeacherID", Integer.Parse(HttpContext.Current.Session("LoggedInTeacherID")))
                                .Parameters.AddWithValue("@ClassID", Integer.Parse(SelectedClass))
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

    Public Function GetTaughtClassesInfoDT() As DataTable
        Dim objDS As New DataSet
        Try
            Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
            Using objConn As New SqlConnection(connString)
                Dim strSQL = <![CDATA[
                                SELECT TeachersClasses.ClassID,Classname FROM TeachersClasses 
                                INNER JOIN Classes ON TeachersClasses.classID=Classes.ClassID 
                                WHERE TeacherID=@TeacherID 
                    ]]>.Value()
                Using objCommand1 As New SqlCommand
                    With objCommand1
                        .Connection = objConn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(HttpContext.Current.Session("LoggedinTeacherID")))
                    End With
                    Using objDA As New SqlDataAdapter(objCommand1)
                        objDA.Fill(objDS)
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
        Return objDS.Tables(0)
    End Function

    Public Function RemoveClass(ByRef strError As String) As Boolean
        Dim bDeleted As Boolean
        Try
            Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
            Using objConn As New SqlConnection(connString)
                '1.Check if this acount already exists
                Dim strSQL = <![CDATA[
                    SELECT TOP 1 EntryID FROM TeachersClasses WHERE TeacherID=@TeacherID AND ClassID=@ClassID
                    ]]>.Value()
                Using objCommand1 As New SqlCommand
                    With objCommand1
                        .Connection = objConn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(HttpContext.Current.Session("LoggedInTeacherID")))
                        .Parameters.AddWithValue("@ClassID", Integer.Parse(SelectedClass))
                    End With
                    'Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    If objCommand1.ExecuteScalar() = 0 Then
                        bDeleted = False
                        strError = "This teacher does not teach this class"
                    Else
                        Using objCommand2 As New SqlCommand
                            strSQL = <![CDATA[
                            DELETE FROM TeachersClasses WHERE TeacherID=@TeacherID AND ClassID=@ClassID
                    ]]>.Value()
                            With objCommand2
                                .Connection = objConn
                                ' .Connection.Open()
                                .CommandText = strSQL
                                .Parameters.AddWithValue("@TeacherID", Integer.Parse(HttpContext.Current.Session("LoggedInTeacherID")))
                                .Parameters.AddWithValue("@ClassID", Integer.Parse(SelectedClass))
                            End With
                            If objCommand2.ExecuteNonQuery Then
                                bDeleted = True
                                'Get the new ID
                                'Dim iIDNo As Integer
                                'Using objCommand3 As SqlCommand = New SqlCommand
                                '    With objCommand3
                                '        .Connection = objConn
                                '        '.Connection.Open() 'already open at this point
                                '        .CommandText = "SELECT @@Identity"
                                '    End With
                                '    iIDNo = objCommand3.ExecuteScalar()
                                'End Using
                            Else
                                bDeleted = False
                            End If
                        End Using
                    End If
                End Using
            End Using
            ' End Using
        Catch ex As Exception
            bDeleted = False
        End Try
        Return bDeleted
    End Function

    Public Sub LoadClassesList()
        Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        ' Read the connection string from the web.config file
        Using objConn As New SqlConnection(connString)
            Dim strSQL = <![CDATA[
                                SELECT ClassID,Classname FROM Classes
                    ]]>.Value()
            Using objCommand1 As New SqlCommand
                With objCommand1
                    .Connection = objConn
                    .Connection.Open()
                    .CommandText = strSQL
                End With
                Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    While objReader.Read()
                        ClassesList.Add(New SelectListItem With {.Text = objReader("Classname").ToString, .Value = objReader("ClassID").ToString})
                        'SubjectList.Add(New SelectListItem With {.Text = "Chemistry", .Value = "3"})
                        'SubjectList.Add(New SelectListItem With {.Text = "Biology", .Value = "2"})
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadTaughtClassesList()
        Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        ' Read the connection string from the web.config file
        Using objConn As New SqlConnection(connString)
            Dim strSQL = <![CDATA[
                                SELECT TeachersClasses.ClassID,Classname FROM TeachersClasses 
                                INNER JOIN Classes ON TeachersClasses.classID=Classes.ClassID 
                                WHERE TeacherID=@TeacherID 
                    ]]>.Value()
            Using objCommand1 As New SqlCommand
                With objCommand1
                    .Connection = objConn
                    .Connection.Open()
                    .CommandText = strSQL
                    .Parameters.AddWithValue("@TeacherID", Integer.Parse(HttpContext.Current.Session("LoggedinTeacherID")))
                End With
                Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    While objReader.Read()
                        TaughtClassesList.Add(New SelectListItem With {.Text = objReader("Classname").ToString, .Value = objReader("ClassID").ToString})
                        'SubjectList.Add(New SelectListItem With {.Text = "Chemistry", .Value = "3"})
                        'SubjectList.Add(New SelectListItem With {.Text = "Biology", .Value = "2"})
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadClassInfo(ByVal iclassID As Integer)
        Dim connString As String = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        ' Read the connection string from the web.config file
        Using objConn As New SqlConnection(connString)
            Dim strSQL = <![CDATA[
                    SELECT ClassID,Classname,ClassStart, ClassEnd, ClassDuration, CreditHours FROM Classes
                    WHERE ClassID=@ClassID
                    ]]>.Value()
            Using objCommand1 As New SqlCommand
                With objCommand1
                    .Connection = objConn
                    .Connection.Open()
                    .CommandText = strSQL
                    .Parameters.AddWithValue("@ClassID", iclassID)
                End With
                Using objReader As SqlDataReader = objCommand1.ExecuteReader()
                    If objReader.Read() Then
                        SelectedClass = objReader("ClassID").ToString
                        If IsDBNull(objReader("ClassStart")) Then
                            dtStart = DateTime.MinValue
                        Else
                            dtStart = DateTime.Parse(objReader("ClassStart"))
                        End If

                        If IsDBNull(objReader("ClassStart")) Then
                            dtEnd = DateTime.MaxValue
                        Else
                            dtEnd = DateTime.Parse(objReader("ClassEnd"))
                        End If

                        If IsDBNull(objReader("CreditHours")) Then
                            NumCreditHrs = 0.0
                        Else
                            NumCreditHrs = Decimal.Parse(objReader("CreditHours"))
                        End If

                        If IsDBNull(objReader("ClassDuration")) Then
                            ClassDuration = 0
                        Else
                            ClassDuration = Integer.Parse(objReader("ClassDuration"))
                        End If

                    End If
                End Using
            End Using
        End Using
    End Sub




End Class
