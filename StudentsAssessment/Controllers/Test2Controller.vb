Imports System.Web.Mvc

Namespace Controllers
    Public Class Test2Controller
        Inherits Controller

        ' GET: Test2
        Function Index(ByVal objLoginData As LoginData) As ActionResult
            Dim strFirstname = objLoginData.Firstname
            Return View(objLoginData)
        End Function
    End Class
End Namespace