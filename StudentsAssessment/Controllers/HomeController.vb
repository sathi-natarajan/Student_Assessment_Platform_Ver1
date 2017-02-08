Imports System.Data.SqlClient
Imports System.IO

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    Function TakeTest() As ActionResult
        Return View()
    End Function

    Function TeacherLogin() As ActionResult
        'They will need to log in only if they have logged out 
        'If Session("LoggedInTeacherID") IsNot Nothing Then
        TempData("LoginPageFor") = "TEACHERS" 'When using RedirecttoAction, use this not Viewdata.  Then later, assign to viewdata(see Login.Index)
        'Return RedirectToAction("Index", "Login", New { FileUploadMsg = "File uploaded successfully" })
        Return RedirectToAction("Index", "Login")
        'Else
        '    'Dim objLoginData As LoginData
        '    'TO DO - Get their login credentials from db first.
        '    Return RedirectToAction("Index", "Login")
        'End If
    End Function

    Function StudentLogin() As ActionResult
        'If Session("LoggedInStudentID") IsNot Nothing Then
        TempData("LoginPageFor") = "STUDENTS"
        Return RedirectToAction("Index", "Login")
        'Else
        '    Return RedirectToAction("Index", "Login")
        'End If
    End Function

    Function AdminLogin() As ActionResult
        'They will need to log in only if they have logged out 
        'If Session("LoggedInTeacherID") IsNot Nothing Then
        TempData("LoginPageFor") = "ADMINISTRATORS" 'When using RedirecttoAction, use this not Viewdata.  Then later, assign to viewdata(see Login.Index)
        'Return RedirectToAction("Index", "Login", New { FileUploadMsg = "File uploaded successfully" })
        Return RedirectToAction("Index", "Login")
        'Else
        '    'Dim objLoginData As LoginData
        '    'TO DO - Get their login credentials from db first.
        '    Return RedirectToAction("Index", "Login")
        'End If
    End Function

    Function CreateAccounts() As ActionResult
        Dim model As CreateAccountData = New CreateAccountData()
        model.LoadSchools()
        Return View(model)
        'Return View()
    End Function

    '<HttpPost()>
    'Public Function Process(ByVal objSubjects As School) As ActionResult
    '    Dim str1 As String = objSubjects.Selectedsubject
    '    Return View()
    'End Function

    <HttpPost()>
    Function DoCreateAccount(ByVal objActType As CreateAccountData) As ActionResult
        If ModelState.IsValid Then
            Dim iSchoolID As Integer = 0
            'check also the type of acount selection
            If objActType.TypeofAccount Is Nothing OrElse
                    Not (objActType.TypeofAccount.Equals("student") Or objActType.TypeofAccount.Equals("teacher")) Then
                ViewBag.StatusMessage = "Type of account MUST be selected"
            ElseIf String.IsNullOrEmpty(objActType.SelectedSchool) = False AndAlso objActType.ISSchoolValid(iSchoolID) = False Then
                ViewBag.StatusMessage = "Invalid School"
            Else
                Dim strFirstname As String = objActType.Firstname
                Dim strLastname As String = objActType.Lastname
                Dim strActType As String = objActType.TypeofAccount
                Dim strSelectedSchool As String = objActType.SelectedSchool
                Dim strError As String = ""
                Select Case strActType.ToLower
                    Case "student"
                        If objActType.CreateStudentAccount(strError) Then
                            ViewBag.StatusMessage = String.Format("Successfully created acount for TEACHER {0}", objActType.Firstname + " " + objActType.Lastname)
                        Else
                            ViewBag.StatusMessage = String.Format("Account creation failed for STUDENT {0}.  Reason: {1}", objActType.Firstname + " " + objActType.Lastname + vbCrLf, vbCrLf + strError)
                        End If
                    Case "teacher"
                        If objActType.CreateTeacherAccount(strError) Then
                            ViewBag.StatusMessage = String.Format("Successfully created acount for TEACHER {0}", objActType.Firstname + " " + objActType.Lastname)
                        Else
                            ViewBag.StatusMessage = String.Format("Account creation failed for TEACHER {0}.  Reason: {1}", objActType.Firstname + " " + objActType.Lastname + vbCrLf, vbCrLf + strError)
                        End If
                End Select

            End If
        End If
        Return View("CreateAccounts") 'if this is different view, than validation does not work.
    End Function

    Function BulkCreateAccounts() As ActionResult
        'Here, check if administrator logged in.  if not, get them logged in as admin and come back here.
        If Session("LoggedinAdminID") Is Nothing OrElse IsNumeric(Session("LoggedinAdminID")) = False Then
            'If not logged in as admin
            TempData("NeedsAdminAccess") = 1
            TempData("Feature") = "CREATE MULTIPLE ACCOUNTS"
            TempData("TargetAction") = "Home/BulkCreateAccounts"
            TempData("LoginPageFor") = "ADMINISTRATORS"
            Return RedirectToAction("Index", "Login")
        Else
            Return View()
        End If
    End Function

    <HttpPost()>
    Function DoBulkCreateAccounts(ByVal objBulkAcctType As BulkAccounts) As ActionResult
        If Session("LoggedinAdminID") IsNot Nothing Then
            'If ModelState.IsValid Then
            'Dim strFirstname As String = objActType.Firstname
            '    Dim strLastname As String = objActType.Lastname
            If objBulkAcctType.TypeofAccount IsNot Nothing Then
                Dim strActType As String = objBulkAcctType.TypeofAccount
                'Dim strSchool As String = objActType.School
                'If strActType.ToLower = "teacher" Then
                '    ViewBag.AccountType = 1
                'ElseIf strActType.ToLower = "student" Then
                '    ViewBag.AccountType = 2
                'Else
                '    ViewBag.AccountType = 0
                'End If
                ViewBag.AccountType = strActType 'loses on repeated page reload
                Session("AccountType") = strActType
            Else
                ViewBag.AccountType = ""
                ViewBag.StatusMessage = "An account type MUST be selected"
            End If

            'End If
            Return View("BulkCreateAccounts") 'if this is different view, than validation does not work.
        Else
            TempData("NeedsAdminAccess") = 1
            Return RedirectToAction("Index", "Login")
        End If

    End Function
    '<HttpPost()>
    'Function PreviewBulkAccounts(ByVal objBulkAcctType As BulkAccounts) As ActionResult
    '    Return View("BulkCreateAccounts")
    'End Function

    '<HttpPost()>
    'Function DoActuallyCreateBulkAccts(ByVal objBulkAcctType As BulkAccounts) As ActionResult
    '    Dim strActType As String = objBulkAcctType.TypeofAccount
    '    Session("LoggedInAdminID") = Nothing
    '    Session("NeedsAdminAccess") = Nothing
    '    Return View("Index")
    'End Function

#Region "Upload from Excel files"
    Public Function ImportExcel() As ActionResult
        Return View()
    End Function

    <ActionName("ImportExcel")>
    <HttpPost()>
    Public Function ImportExcel1() As ActionResult


        If Request.Files("FileUpload1").ContentLength > 0 Then
            Dim extension As String = System.IO.Path.GetExtension(Request.Files("FileUpload1").FileName).ToLower()
            Dim query As String = Nothing
            Dim connString As String = ""


            Dim strErrors As String = ""

            Dim validFileTypes As String() = {".xls", ".xlsx", ".csv"}

            Dim path1 As String = String.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files("FileUpload1").FileName)
            If Not Directory.Exists(path1) Then
                Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"))
            End If
            If validFileTypes.Contains(extension) Then
                If System.IO.File.Exists(path1) Then
                    System.IO.File.Delete(path1)
                End If
                Request.Files("FileUpload1").SaveAs(path1)
                If extension = ".csv" Then
                    Dim dt As DataTable = Utility.ConvertCSVtoDataTable(path1)
                    ViewBag.Data = dt
                    'Connection String to Excel Workbook  
                ElseIf extension.Trim() = ".xls" Then
                    connString = (Convert.ToString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") & path1) + ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
                    Dim dt As DataTable = Utility.ConvertXSLXtoDataTable(path1, connString)
                    CreateAccountsFromDT(dt, strErrors)
                    If String.IsNullOrEmpty(strErrors) = True Then
                        ViewBag.StatusMessage = "All supplied accounts have been successfully created"
                    Else
                        ViewBag.StatusMessage = "The following accounts could not be created (for given reasons): " + strErrors
                    End If
                    ViewBag.Data = dt

                ElseIf extension.Trim() = ".xlsx" Then
                    connString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & path1) + ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
                    Dim dt As DataTable = Utility.ConvertXSLXtoDataTable(path1, connString)
                    ViewBag.Data = dt

                End If
            Else

                ViewBag.[Error] = "Please Upload Files in .xls, .xlsx or .csv format"

            End If
        End If

        Return View("BulkCreateAccounts")
    End Function

    Private Sub CreateAccountsFromDT(dt As DataTable, ByRef strErrors As String)
        Dim strError As String = ""
        Dim objAccountsData As CreateAccounts1
        '@column.ColumnName.ToUpper()
        For Each Row As DataRow In dt.Rows
            objAccountsData = New CreateAccounts1
            objAccountsData.Firstname = Row("Firstname")
            objAccountsData.Lastname = Row("Lastname")
            objAccountsData.Username = Row("Username")
            If Session("AccountType") = "teacher" Then
                If objAccountsData.CreateTeacherAccount(strError) Then
                Else
                    strErrors += String.Format("Account for : {0}, Reason: {1}", objAccountsData.Firstname + " " + objAccountsData.Lastname, strError)
                End If
            ElseIf Session("AccountType") = "student" Then
                If objAccountsData.CreateStudentAccount(strError) Then
                Else
                    strErrors += strError
                End If
            End If
            '@Row(column).ToString()
        Next
    End Sub
#End Region

    <HttpPost()>
    Public Function Process(ByVal objSubjects As CreateAccountData) As ActionResult
        Dim str1 As String = objSubjects.SelectedSchool
        Return View("CreateAccounts")
    End Function
End Class
