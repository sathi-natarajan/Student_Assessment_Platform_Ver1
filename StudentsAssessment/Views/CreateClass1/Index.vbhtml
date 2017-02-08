@ModelType StudentsAssessment.CreateClassData
@Code
    ViewData("Title") = "Index"
End Code
<script src="~/Scripts/jquery-1.12.4.min.js"></script>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
    });
    function FillClassData() {
        $('#ClassDuration').val("3.5");
        $('#NumCreditHrs').val("120");
        $('#dtStart').text("10/20/2000");
        $('#dtEnd').text("10/25/2000");
    }
</script>
<div class="row">
     <div class="col-sm-6" style="margin-left:300px;">
         @Using (Html.BeginForm())
             @Html.AntiForgeryToken()
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
             @<table id="tblCreateClass" border="0" cellpadding="2" cellspacing="2">
                <colgroup>
                    <col width="200" />
                </colgroup>
         <tr>
             <td colspan="2" style="text-align:center;"><h2>ADD CLASS</h2></td>
         </tr>
                <tr>
                    <td>
                        @Html.LabelFor(Function(model) model.ClassesList, htmlAttributes:=New With {.class = "control-label col-md-2", .style = "width:100%;"})
                    </td>
                    <td>
                        @Html.DropDownListFor(Function(model) model.SelectedClass, Model.ClassesList, New With {.onchange = "FillClassData();"})
                        @Html.ValidationMessageFor(Function(model) model.SelectedClass, "", New With {.class = "text-danger"})
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;">
                    @*<input type="button" id="Refresh" name="Refresh" value="REFRESH" Class="btn btn-default" onclick="location.href='@Url.Action("RefreshClassInfo", "CreateClass1", New With {.SelClass = 1})'" />*@</td>
                </tr>

                <tr>
                    <td>
                        @Html.LabelFor(Function(model) model.NumCreditHrs, htmlAttributes:=New With {.class = "control-label col-md-2", .style = "width:100%;"})
                    </td>
                    <td>
                        @Html.EditorFor(Function(model) model.NumCreditHrs, New With {.htmlAttributes = New With {.class = "form-control", .style = "width:75px;"}, .type = "text"})
                        @Html.ValidationMessageFor(Function(model) model.NumCreditHrs, "", New With {.class = "text-danger"})
                    </td>
                </tr>

                 <tr>
                     <td>
                         @Html.LabelFor(Function(model) model.dtStart, htmlAttributes:=New With {.class = "control-label col-md-2", .style = "width:100%;"})
                     </td>
                     <td>
                         @*@Html.EditorFor(Function(model) model.dtStart, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                        @*@Html.Editor(ViewBag.dtStart, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                        <div id="dtStart">@Model.dtStart.ToShortDateString </div>
                        
                         @*@If ViewBag.dtStart Is Nothing Then
                             @<div>Click refreh to show data</div>
                         Else
                             @<div>@ViewBag.dtStart</div>
                         End If*@
                         @Html.ValidationMessageFor(Function(model) model.dtStart, "", New With {.class = "text-danger"})
                     </td>
                 </tr>

                   <tr>
                       <td>
                           @Html.LabelFor(Function(model) model.dtEnd, htmlAttributes:=New With {.class = "control-label col-md-2", .style = "width:100%;"})
                       </td>
                       <td>
                           @*@Html.EditorFor(Function(model) model.dtEnd, New With {.htmlAttributes = New With {.class = "form-control"}})*@

                           <div id="dtEnd">@Model.dtEnd.ToShortDateString</div>

                           @*@If ViewBag.dtEnd Is Nothing Then
                                @<div>Click refreh to show data</div>
                               Else
                               @<div>@ViewBag.dtEnd</div>
                           End If*@
                           
                           @Html.ValidationMessageFor(Function(model) model.dtEnd, "", New With {.class = "text-danger"})
                       </td>
                   </tr>

                     <tr>
                          <td>
                              @Html.LabelFor(Function(model) model.ClassDuration, htmlAttributes:=New With {.class = "control-label col-md-2", .style = "width:100%;"})
                          </td>
                         <td>
                             @Html.EditorFor(Function(model) model.ClassDuration, New With {.htmlAttributes = New With {.class = "form-control", .style = "width:75px;"}})
                             @Html.ValidationMessageFor(Function(model) model.ClassDuration, "", New With {.class = "text-danger"})
                         </td>
                     </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <input type="submit" value="Create" class="btn btn-success" />
                            <input type="button" id="Back" name="Back" value="Back" Class="btn btn-success" onclick="location.href='@Url.Action("Index", "Home")'" />

                        </td>
                    </tr>
                    <tr>
                       
                            <td colspan="2">
                         <div class="field-validation-error">
                             @ViewBag.StatusMessage
                        </div>
                       
                            </td>
</tr>
            </table>
         End Using
     </div>    
     <div class="col-sm-6"></div>    
</div>

