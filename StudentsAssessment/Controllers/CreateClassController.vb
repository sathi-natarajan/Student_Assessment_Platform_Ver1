Imports System.Web.Mvc

Namespace Models
    Public Class CreateClassController
        Inherits Controller
        'Dim lstSchools As New List(Of String)
        Shared lstClasses As New List(Of String)
        ' GET: SelectList
        Function Index() As ActionResult
            lstClasses.Clear()
            lstClasses.Add("1st grade")
            lstClasses.Add("2nd grade")
            lstClasses.Add("3rd grade")
            lstClasses.Add("4th grade")
            lstClasses.Add("5th grade")
            ViewBag.lstClasses = lstClasses
            Return View()
        End Function

        <HttpPost()>
        Function Index(ByVal School As String) As ActionResult
            ViewBag.lstClasses = lstClasses
            ViewBag.SelectedClass = School
            ViewBag.NumCredits = 4.0
            ViewBag.ClassStart = "03/01/2017"
            ViewBag.ClassEnd = "03/01/2018"
            ViewBag.ClassDurationMin = 120
            Return View()
        End Function
    End Class

End Namespace