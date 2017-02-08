@ModelType StudentsAssessment.DDLExample

@*This Viewbag.RenderForm thing is a workaround.  See DDLExample controller for comments*@
@If ViewBag.RenderForm = 1 Then
    @Using (Html.BeginForm("Process", "DDLExample"))
        @Html.AntiForgeryToken()
        @<div Class="form-group">
            @Html.LabelFor(Function(model) model.Username, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div Class="col-md-10">
                @Html.PasswordFor(Function(model) model.Username, New With {.htmlAttributes = New With {.class = "form-control"}})
                <br />
                @Html.ValidationMessageFor(Function(model) model.Username, "", New With {.class = "text-danger"})
            </div>
        </div>

        @<div Class="form-group">
            @Html.LabelFor(Function(model) model.SchoolsList, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div Class="col-md-10">
                @Html.DropDownListFor(Function(model) model.SelectedSchool, Model.SchoolsList, New With {.htmlAttributes = New With {.class = "form-control"}})
                <br />
                @Html.ValidationMessageFor(Function(model) model.SchoolsList, "", New With {.class = "text-danger"})
            </div>
        </div>
        @<input type="submit" id="Submit" name="Submit" value="OK" Class="btn btn-default" />
    End Using
Else
    @<div>Your selections are: </div>@Html.LabelFor(Function(model) model.Username)@<br/>
    @<div>Your school is:</div> @Html.EditorFor(Function(model) model.SelectedSchool)
    @<input type="button" id="Back" name="Back" value="Back" Class="btn btn-default" onclick="location.href='@Url.Action("Index", "DDLExample")'" />
End If

