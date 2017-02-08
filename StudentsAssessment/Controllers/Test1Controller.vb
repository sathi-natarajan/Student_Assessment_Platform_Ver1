Imports System.Web.Mvc

Namespace Controllers
    Public Class Test1Controller
        Inherits Controller

        ' GET: Test1
        Function Index() As ActionResult
            Dim objLoginData As New LoginData
            objLoginData.IDNo = 1
            objLoginData.Firstname = "Sathi"
            objLoginData.Lastname = "Natarajan"
            Return RedirectToAction("Index", "Test2", objLoginData)
        End Function
    End Class
End Namespace