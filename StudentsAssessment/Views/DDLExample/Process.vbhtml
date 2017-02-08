@ModelType StudentsAssessment.DDLExample

Your selections are: @Html.LabelFor(Function(model) model.Username))<br/>
Your school are: @Html.LabelFor(Function(model) model.SelectedSchool)

@*@Using (Html.BeginForm("Process", "DDLExample"))
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
    @<input type="submit" id="Submit" name="Submit" value="OK" Class="btn btn-default" />*@
End Using
