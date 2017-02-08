@Code
    ViewData("Title") = "Index"
End Code

@ModelType StudentsAssessment.LoginData
<h2>Test2's Index</h2>


<div>Your Information passed:<br/>
    ID: @Html.Label(Model.IDNo)<br/>
    First name: @Html.Label(Model.Firstname)<br />
    Lastname: First name: @Html.Label(Model.Lastname)
</div>