Imports System.Web.Mvc

Namespace Controllers
    <HandleError()>
    Public Class TeachersController
        Inherits Controller

        ' GET: TeacherOps
        <HandleError()>
        Function Index() As ActionResult
            Return View()
        End Function

        <HandleError()>
        Function AddClass() As ActionResult
            If Session("LoggedinteacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinteacherID")) = True Then
                Dim objCreateClassData As New CreateClassData
                objCreateClassData.LoadClassesList()
                Return View(objCreateClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If

        End Function

        <HttpPost()>
        Function AddClass(ByVal objCreateClassData As CreateClassData) As ActionResult
            If Session("LoggedinteacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinteacherID")) = True Then
                Dim objCreateClassData1 As New CreateClassData
                objCreateClassData1.LoadClassesList() 'Should fill each time like this
                If ModelState.IsValid Then
                    If IsNumeric(objCreateClassData.NumCreditHrs) = True Then
                        Dim strSelClass As String = objCreateClassData.SelectedClass
                        ' Session("LoggedInTeacherID") = 1
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
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function RemoveClass() As ActionResult
            If Session("LoggedinteacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinteacherID")) = True Then
                Dim objCreateClassData As New CreateClassData
                objCreateClassData.LoadTaughtClassesList()
                Return View(objCreateClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        <HttpPost()>
        Function RemoveClass(ByVal objCreateClassData As CreateClassData) As ActionResult
            If Session("LoggedinteacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinteacherID")) = True Then
                Dim objCreateClassData1 As New CreateClassData
                objCreateClassData1.LoadTaughtClassesList() 'Should fill each time like this
                If ModelState.IsValid Then
                    If IsNumeric(objCreateClassData.NumCreditHrs) = True Then
                        Dim strSelClass As String = objCreateClassData.SelectedClass
                        ' Session("LoggedInTeacherID") = 1
                        Dim strerror As String = ""
                        If objCreateClassData.RemoveClass(strerror) Then
                            ViewBag.StatusMessage = "Successfully removed this class from this teacher's list of classes"
                        Else
                            ViewBag.StatusMessage = strerror
                        End If
                    Else
                        ViewBag.StatusMessage = "Number of credit hours must be numeric"
                    End If

                Else

                End If
                Return View(objCreateClassData1)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function ViewClasses() As ActionResult
            If Session("LoggedinteacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinteacherID")) = True Then
                Dim objDT As DataTable
                Dim objCreateClassData As New CreateClassData
                objDT = objCreateClassData.GetTaughtClassesInfoDT()
                If objDT.Rows.Count = 0 Then
                    ViewBag.Error = "The teacher's list of classes is currently empty"
                Else
                    ViewBag.Data = objDT
                End If
                Return View(objCreateClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If

        End Function
        Public Function GetClassData(ByVal iClassID As Integer) As ActionResult
            Dim objCreateClassData As New CreateClassData
            objCreateClassData.LoadClassInfo(iClassID)
            'Return Json(String.Format("Returning from FirstAJAX action {0}", param), JsonRequestBehavior.AllowGet)
            Return Json(New With {.NumCreditHrs = objCreateClassData.NumCreditHrs,
                        .ClassDuration = objCreateClassData.ClassDuration,
                        .dtStart = objCreateClassData.dtStart.ToShortDateString,
                        .dtEnd = objCreateClassData.dtEnd.ToShortDateString}, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace