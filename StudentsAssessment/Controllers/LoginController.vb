Imports System.Web.Mvc

Namespace Controllers

    <OutputCache(Duration:=0)>
    Public Class LoginController
        Inherits Controller

        ' GET: Login
        Function Index() As ActionResult
            If TempData("LoginPageFor") IsNot Nothing Then
                ViewBag.LoginPageFor = TempData("LoginPageFor").ToString()
                ViewBag.LoginPageHeader = String.Format("Log-in page for {0}", TempData("LoginPageFor").ToString())
                If TempData("TargetAction") IsNot Nothing Then
                    Session("TargetAction") = TempData("TargetAction").ToString()
                End If

                Session("for") = ViewBag.LoginPageFor
                If TempData("NeedsAdminAccess") IsNot Nothing Then
                    Session("NeedsAdminAccess") = TempData("NeedsAdminAccess")
                    ViewBag.LoginPageHeader = String.Format("The '{0}' feature needs admin access to this system.  Please login as admin below", TempData("Feature").ToString)
                End If
                Return View()
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        <HttpPost()>
        Function Index(ByVal objLoginData As LoginData) As ActionResult
            If ModelState.IsValid Then
                'TO DO - ViewData is getting lost when it comes back here from Index action above.
                'If String.IsNullOrEmpty(ViewBag.LoginPageFor) = False Then
                'Select Case ViewBag.LoginPageFor.ToString.ToLower
                If Session("For") IsNot Nothing Then
                    Select Case Session("For").ToString.ToLower
                        Case "teachers"
                            If objLoginData.checkTeacherLogin(objLoginData.Username, objLoginData.Password) Then
                                objLoginData.LoadTeacherInfo()
                                Session("LoggedinTeacherID") = Integer.Parse(objLoginData.IDNo)
                                Session("WhereIWas") = "Login"
                                Session("Greetings") = String.Format("Hello {0}, Welcome to Student Assessment Platform!", objLoginData.Firstname)
                                'Return View("Welcome", objLoginData) 'Let in
                                Session("LoginData") = objLoginData
                                'Return RedirectToAction("Index", "Welcome", objLoginData)
                                Return RedirectToAction("Index", "Welcome")
                            Else
                                ViewBag.StatusMessage = "Invalid Username or Password"
                                Return View()
                            End If
                        Case "students"
                            If objLoginData.checkStudentLogin(objLoginData.Username, objLoginData.Password) Then
                                objLoginData.LoadStudentInfo()
                                Session("LoggedinStudentID") = Integer.Parse(objLoginData.IDNo)
                                Session("WhereIWas") = "Login"
                                Session("Greetings") = String.Format("Hello {0}, Welcome to Student Assessment Platform!", objLoginData.Firstname)
                                'Return View("Welcome", objLoginData) 'Let in
                                Session("LoginData") = objLoginData
                                'Return RedirectToAction("Index", "Welcome", objLoginData)
                                Return RedirectToAction("Index", "Welcome")
                            Else
                                ViewBag.StatusMessage = "Invalid Username or Password"
                                Return View()
                            End If
                        Case "administrators"
                            'TO DO = THIS SECTION SHOWING ADMIN SECTION IN THE SAME PAGE.  nOT LIKE STUDENTS AND TEACHERS LOGIN.
                            If objLoginData.checkAdminLogin(objLoginData.Username, objLoginData.Password) Then
                                objLoginData.LoadAdminInfo()
                                Session("LoggedinAdminID") = Integer.Parse(objLoginData.IDNo)
                                If Session("NeedsAdminAccess") = 1 Then
                                    'Session("NeedsAdminAccess") = Nothing ' you do that when you finish the process/when the process fails
                                    Return RedirectToAction(Session("TargetAction").ToString.Split("/")(1), Session("TargetAction").ToString.Split("/")(0))
                                Else
                                    'objLoginData.LoadAdminInfo()
                                    ' Session("LoggedinAdminID") = Integer.Parse(objLoginData.IDNo)
                                    Session("WhereIWas") = "Login"
                                    Session("Greetings") = String.Format("Hello {0}, Welcome to Student Assessment Platform!", objLoginData.Firstname)
                                    ' Return View("Welcome", objLoginData) 'Let in
                                    ' Return RedirectToAction("Index", "Welcome", objLoginData)
                                    Return RedirectToAction("Index", "Welcome", objLoginData)
                                End If
                            Else
                                ViewBag.StatusMessage = "Invalid Username or Password"
                                Return View()
                            End If
                        Case Else
                            ViewBag.StatusMessage = "Invalid login type"
                            Return View()
                    End Select
                Else
                    ViewBag.StatusMessage = "Internal problem.  Please contact admin"
                    Return View()
                End If
            Else
                Return View()
            End If
        End Function

        Function Logout() As ActionResult
            Session.Clear()
            Session.Abandon()
            Return RedirectToAction("Index", "Home")
        End Function
    End Class


End Namespace