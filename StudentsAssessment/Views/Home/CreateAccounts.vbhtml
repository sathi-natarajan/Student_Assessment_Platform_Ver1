@ModelType StudentsAssessment.CreateAccountData
@Code
    ViewData("Title") = "CreateAccounts"
End Code

 @*initialise Superfish*@ 
<script>

        jQuery(document).ready(function(){
            jQuery('ul.sf-menu').superfish({
            });
            jQuery(".form-control").click(function () {
                alert("Clicked");
            });
        });

    </script>
<div class="row" style="margin-top:100px;">
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="col-sm-9">
        @Using (Html.BeginForm("DoCreateAccount", "Home"))
            @<div Class="form-group">
                @Html.LabelFor(Function(model) model.TypeofAccount, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    &nbsp; &nbsp;
                    <div Class="col-md-10" style="border:2px solid #fff;width:300px;margin-left:15px;">
                @Html.RadioButton("TypeofAccount", "teacher", New With {.htmlAttributes = New With {.class = "form-control"}})
                        TEACHER ACCOUNT
            <br />
                @Html.RadioButton("TypeofAccount", "student", New With {.htmlAttributes = New With {.class = "form-control"}})
                        STUDENT ACCOUNT
            <br />
                @Html.ValidationMessageFor(Function(model) model.TypeofAccount, "", New With {.class = "text-danger"})
                    </div>
                </div>@<br/>
                @<table id="" border="0" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col width="200" />
                        <col width="200" />
                    </colgroup>
                    <tr>
                        <td>
                                @Html.LabelFor(Function(model) model.Firstname, htmlAttributes:=New With {.class = "control-label col-md-10"})
                        </td>
                        <td>
                                @Html.EditorFor(Function(model) model.Firstname, New With {.htmlAttributes = New With {.class = "form-control"}})
                                <br />
                                @Html.ValidationMessageFor(Function(model) model.Firstname, "", New With {.class = "text-danger"})
                        </td>
                    </tr>

                     <tr>
                         <td>
                             @Html.LabelFor(Function(model) model.Lastname, htmlAttributes:=New With {.class = "control-label col-md-10"})
                         </td>
                         <td>
                             @Html.EditorFor(Function(model) model.Lastname, New With {.htmlAttributes = New With {.class = "form-control"}})
                             <br />
                             @Html.ValidationMessageFor(Function(model) model.Lastname, "", New With {.class = "text-danger"})
                         </td>
                     </tr>
                    
                     <tr>
                         <td>
                             @Html.LabelFor(Function(model) model.Username, htmlAttributes:=New With {.class = "control-label col-md-2"})
                         </td>
                         <td>
                             @Html.EditorFor(Function(model) model.Username, New With {.htmlAttributes = New With {.class = "form-control"}})
                             <br />
                             @Html.ValidationMessageFor(Function(model) model.Username, "", New With {.class = "text-danger"})
                         </td>
                     </tr>

                     @*<div class="form-group">
            @Html.LabelFor(Function(model) model.Password, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.PasswordFor(Function(model) model.Password, New With {.htmlAttributes = New With {.class = "form-control"}})
                <br />
                @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
            </div>
        </div>*@
                     <tr>
                         <td>
                             @Html.Label("Password:", htmlAttributes:=New With {.class = "control-label col-md-2"})
                         </td>
                         <td>
                             @Html.Label("teacher/student", New With {.htmlAttributes = New With {.class = "form-control"}})
                         </td>
                     </tr>
                    
                     <tr>
                         <td>
                             @Html.Label("SCHOOL:", htmlAttributes:=New With {.class = "control-label col-md-2"})
                         </td>
                         <td>
                             @Html.EditorFor(Function(model) model.SelectedSchool, New With {.htmlAttributes = New With {.class = "form-control"}})
                         </td>
                     </tr>
                    
                </table>


                @*@<div class="form-group">
                    @Html.LabelFor(Function(model) model.School, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.School, New With {.htmlAttributes = New With {.class = "form-control"}})
                        <br />
                        @Html.ValidationMessageFor(Function(model) model.School, "", New With {.class = "text-danger"})
                    </div>*@

                 @*@Html.LabelFor(Function(model) model.SchoolsList, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                     @*<div class="col-md-10">
                @Html.DropDownListFor(Function(model) model.SelectedSchool, Model.SchoolsList)
                <br />
                @Html.ValidationMessageFor(Function(model) model.SchoolsList, "", New With {.class = "text-danger"})
            </div>*@
            '</div>


                @<div class="form-group">
                    <div class="col-md-offset-2 col-md-10" style="margin-top:50px;">
                        <input type="submit" value="CREATE ACCOUNT" class="btn btn-default" />
                        <input type="button" value="BACK TO MAIN" class="btn btn-default" onclick="location.href='@Url.Action("Index", "Home")'" /><br/>
                        <div class="field-validation-error">
                            @*@if String.IsNullOrEmpty(@ViewBag.StatusMessage)=true
                                @Html.Raw(ViewBag.StatusMessage.Replace(System.Environment.NewLine, "<br/>"))
                            else
                                @ViewBag.StatusMessage
                            end if*@           
                            @ViewBag.StatusMessage   
                        </div>
                    </div>
                </div>
        End Using
    </div>
    <div class="col-sm-3" style="margin-top:-50px;">
        <strong>ACCOUNT TASKS</strong><br /><br />
        <ul id="AccountTasks" Class="sf-menu sf-vertical" style="width:200px;">
            @*width here changes that of superfish menu*@
            <li>
                <a href="@Url.Action("ActivateAccounts", "Home")">ACTIVATE ACCOUNTS</a>
            </li>
            <li>
                <a href="@Url.Action("BulkCreateAccounts", "Home")">MULTIPLE ACCOUNTS</a>
            </li>
        </ul>
        @*@Using (Html.BeginForm("BulkCreateAccounts", "Home", FormMethod.Post))
            @<input type="submit" value="CREATE BULK ACCOUNTS" Class="btn btn-default" onclick="location.href='@Url.Action("BulkCreateAccounts", "Home")'" />
        End Using*@
    </div>
</div>
@*<div>
    @Html.ActionLink("Back to List", "Index")
</div>*@
