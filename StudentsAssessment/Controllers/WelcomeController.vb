Imports System.Web.Mvc

Namespace Controllers
    Public Class WelcomeController
        Inherits Controller

        ' GET: Welcome
        Function Index() As ActionResult
            If Session("LoggedInTeacherID") IsNot Nothing OrElse Session("LoggedInStudentID") IsNot Nothing OrElse
                 Session("LoggedInAdminID") IsNot Nothing Then
                'Dim objLoginData As New LoginData
                'objLoginData.IDNo = Integer.Parse(Session("LoggedInTeacherID"))
                'objLoginData.LoadTeacherInfo()
                If Session("LoginData") IsNot Nothing AndAlso TypeOf Session("LoginData") Is LoginData Then
                    Dim objLoginData As LoginData = CType(Session("LoginData"), LoginData)
                    Return View(objLoginData)
                Else
                    Return RedirectToAction("Index", "Home")
                End If
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function
    End Class
End Namespace