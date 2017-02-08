Imports System.Web.Mvc

Namespace Controllers
    Public Class CreateClass1Controller
        Inherits Controller

        ' GET: CreateClass1
        Function Index() As ActionResult
            Dim objCreateClassData As New CreateClassData
            objCreateClassData.LoadClassesList()
            Return View(objCreateClassData)
        End Function
        <HttpPost()>
        Function Index(ByVal objCreateClassData As CreateClassData) As ActionResult
            Dim objCreateClassData1 As New CreateClassData
            objCreateClassData1.LoadClassesList() 'Should fill each time like this
            If ModelState.IsValid Then
                If IsNumeric(objCreateClassData.NumCreditHrs) = True Then
                    Dim strSelClass As String = objCreateClassData.SelectedClass
                    Session("LoggedInTeacherID") = 1
                    Dim strerror As String = ""
                    If objCreateClassData.CreateClass(strerror) Then
                        ViewBag.StatusMessage = "Successfully added class to this teacher's list of classes"
                    Else
                        ViewBag.StatusMessage = strerror
                    End If
                Else
                    ViewBag.StatusMessage = "Number of credit hours must be numeric"
                End If

            Else

            End If
            Return View(objCreateClassData1)
        End Function

        ' <HttpPost()>
        Function RefreshClassInfo(ByVal SelClass As Integer) As ActionResult
            'Dim iSelClass As Integer = Integer.Parse(objCreateClassData.SelectedClass)
            Dim objCreateClassData1 As New CreateClassData
            objCreateClassData1.LoadClassesList() 'Should fill each time like this
            objCreateClassData1.LoadClassInfo(SelClass)
            'ViewBag.ClassStart = objCreateClassData1.dtStart
            'ViewBag.ClassEnd = objCreateClassData1.dtEnd
            'ViewBag.ClassDuration = objCreateClassData1.ClassDuration
            'ViewBag.CreditHours = objCreateClassData1.NumCreditHrs
            Return View("Index", objCreateClassData1)
            ' Return View("Index")
        End Function
    End Class
End Namespace