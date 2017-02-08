Imports System.Web.Mvc

Namespace Controllers
    Public Class AdminOpsController
        Inherits Controller

        ' GET: AdminOps
        Function Index() As ActionResult
            'TO DO - Before below, must check if logged=in as admin.  If not, must make them log-in as admin.
            'That part will be later
            Return View()
        End Function
    End Class
End Namespace