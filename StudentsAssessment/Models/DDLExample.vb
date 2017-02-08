Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Public Class DDLExample

    Public Sub New()
        SchoolsList = New List(Of SelectListItem)
        'SubjectList.Add(New SelectListItem With {.Text = "School 1", .Value = "1"})
        'SubjectList.Add(New SelectListItem With {.Text = "School 2", .Value = "2"})
        'SubjectList.Add(New SelectListItem With {.Text = "School 3", .Value = "3"})
    End Sub

    <Required(ErrorMessage:="Firstname is required")>
    <StringLength(25)>
    <Display(Name:="First Name:")>
    Public Property Username As String

    <DisplayName("SELECT A SCHOOL:")>
    <Required(ErrorMessage:="A school must be selected")>
    Public Property SchoolsList As List(Of SelectListItem)
    Public Property SelectedSchool As String 'This MUST BE a property (not just a string) for a value to come out
End Class
