@ModelType List(Of String)
@Using (Html.BeginForm("Index", "CreateClass"))
    @<div class="row" style="margin-top:50px;">
        <div class="col-md-4"></div>
         <div class="col-md-4">
             <h2>ADD A CLASS</h2>
             <table id="" cellpadding="2" cellspacing="2">
                 <tr>
                     <td>SELECT CLASS:</td>
                     <td>
                         <select id="School" name="School">
                             @For Each strSchool In ViewBag.lstClasses
                                 @<option value="option1">@strSchool</option>
                             Next
                         </select><br />  
                    </td>
                 </tr>
                 <tr>
                     <td>
                         No. of credits:
                     </td>
                     <td>@ViewBag.NumCredits</td>
                 </tr>
                 <tr>
                     <td>
                         Start Date:
                     </td>
                     <td>@ViewBag.ClassStart</td>
                 </tr>
                 <tr>
                     <td>
                         End Date:
                     </td>
                     <td>@ViewBag.ClassEnd</td>
                 </tr>
                 <tr>
                     <td>
                         Duration:
                     </td>
                     <td>@ViewBag.ClassDurationMin minutes</td>
                 </tr>
                 <tr>
                     <td>
                         <input type="submit" value="CREATE CLASS" Class="btn btn-default" /><br/>
                         <div> Class have selected: @ViewBag.SelectedClass</div>
                     </td>
                 </tr>
             </table>
    </div>
    <div class="col-md-4"></div>
    </div>

End Using
