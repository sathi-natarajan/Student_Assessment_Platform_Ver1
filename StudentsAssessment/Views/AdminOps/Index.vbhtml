@Code
    ViewData("Title") = "Index"
End Code

<h2>ADMINISTRATIVE OPERATIONS SECTION</h2>

<link href="~/Content/Superfish/css/superfish.css" rel="stylesheet" media="screen" />
<link href="~/Content/Superfish/css/superfish-vertical.css" rel="stylesheet" media="screen" />
<script src="~/Content/Superfish/js/jquery.js"></script>
<script src="~/Content/Superfish/js/superfish.js"></script>
<script src="~/Content/Superfish/js/hoverIntent.js"></script>

<div class="row">
    <div class="col-sm-3"></div>
    <div class="col-sm-3">
        <!--Admin profile here later -- co-->
    </div>
    <div class="col-sm-3"></div>
    <div class="col-sm-3" style="margin-left:60%;margin-top:50px;position:absolute;">
        @<strong>ADMIN TASKS</strong>@<br />@<br />
        @<ul id="TeacherTasks" Class="sf-menu sf-vertical">
            <li Class="current">
                <a href="#">>BULK ADD ACCOUNTS</a>
                <ul>
                    <li>
                        <a href="@Url.Action("BulkAddStudentAccts", "AdminOps")">STUDENT</a>
                    </li>
                    <li>
                        <a href="@Url.Action("BulkAddTeacherAccts", "AdminOps")">TEACHER</a>
                    </li>
                </ul>
            </li>
            <li>
                <a href="@Url.Action("Logout", "Login")">LOG OUT</a>
            </li>
        </ul>
    </div>
    </div>
