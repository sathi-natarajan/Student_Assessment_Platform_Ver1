Imports System.Web.Mvc

Namespace Controllers
    Public Class DDLExampleController
        Inherits Controller

        ' GET: DDLExample
        Function Index() As ActionResult
            Dim model As DDLExample = New DDLExample
            model.SchoolsList.Add(New SelectListItem With {.Text = "School 1", .Value = "1"})
            model.SchoolsList.Add(New SelectListItem With {.Text = "School 2", .Value = "3"})
            model.SchoolsList.Add(New SelectListItem With {.Text = "School 3", .Value = "2"})
            'model.LoadSchools()
            ViewBag.RenderForm = 1
            Return View(model)
            'Return View()
        End Function

        <HttpPost()>
        Public Function Process(ByVal objSubjects As DDLExample) As ActionResult
            Dim str1 As String = objSubjects.SelectedSchool
            'without this workaround, the rerendering of HTMLDropdownListFor on same view page crashes
            'Try removing this and also the ViewBag.RenderForm = 1 line in Index action.
            ViewBag.RenderForm = 0
            Return View("Index", objSubjects)
        End Function
    End Class
End Namespace