@Code
    ViewData("Title") = "BulkCreateAccounts"
End Code
 
@ModelType StudentsAssessment.BulkAccounts
@Imports System.Data

    <!--[if !IE]><!-->
<style type="text/css">
    /* Generic Styling, for Desktops/Laptops */
    table {
        width: 100%;
        border-collapse: collapse;
    }
    /* Zebra striping */
    tr:nth-of-type(odd) {
        background: #eee;
    }

    th {
        background: #333;
        color: white;
        font-weight: bold;
    }

    td, th {
        padding: 6px;
        border: 1px solid #ccc;
       text-align: left;
    }
    /*
    Max width before this PARTICULAR table gets nasty
    This Query will take effect for any screen smaller than 760px
    And also iPads specifically.
    */
    @@media only screen And (max-width: 760px), (min-device-width: 768px) And (max-device-width: 1024px) {

        /* Force table to Not be Like tables anymore */
        table, thead, tbody, th, td, tr {
            display: block;
        }

        /* Hide table headers (but Not display none;, For accessibility) */
        thead tr {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }

        tr {
            border: 1px solid #ccc;
        }

        td {
            /* Behave Like a "row" */
            border: none;
            border-bottom: 1px solid #eee;
            position: relative;
            padding-Left: 50%;
        }

            td: before {
                /* Now Like a table header */
                position: absolute;
                /* Top/left values mimic padding */
                top: 6px;
                left: 6px;
                width: 45%;
                padding-right: 10px;
                white-Space: nowrap;
            }

            /*
     Label the data
     */
            td:before {
                Content: attr(Data - title);
            }
    }
</style>

<!--<![endif]-->

@*<h2>CREATE A COLLECTION OF @ViewBag.TypeOfAcct ACCOUNTS AT ONCE!</h2>*@
<h2>CREATE MULTIPLE ACCOUNTS AT ONCE!</h2>
<p>
    @Using (Html.BeginForm("DoBulkCreateAccounts", "Home"))
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
        </div>@<br />
        @<p>
                <input type = "submit" value="CREATE ACCOUNT" Class="btn btn-default" />
                <br/>
                <div class="field-validation-error">@ViewBag.StatusMessage</div>
         </p>
    End Using
   
              
    @If ViewBag.AccountType IsNot Nothing AndAlso String.IsNullOrEmpty(ViewBag.AccountType) = False Then
        @<p>
            Type of account to bulk-create:<strong>@ViewBag.AccountType</strong><br/>            
            <strong>Please follow these steps:</strong><br/>
            <ol>
                <li>Click on "Choose File" button.  An "Open" dialog will display.</li>
                <li>Select an excel file from your desired location<br/>
                The file name and path to it will appear near the "Choose file" button for verification.</li>
                <li>Click on "CREATE ACCTS FROM SELECTED EXCEL FILE" button to start loading the <strong>@ViewBag.AccountType</strong> account data from that file</li>
                <li>You can also click "PREVIEW ACCOUNT DATA" to have a table displayed below it with the information that would be created.
                </li>
            </ol>
        </p>@<br/>
        @<div Class="row">
            <div Class="col-sm-6">
             @Using (Html.BeginForm("ImportExcel", "Home", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
                @Html.AntiForgeryToken()
                @<table>
                    <tr><td>Excel file</td><td><input type="file" id="FileUpload1" name="FileUpload1" /></td></tr>
                    <tr><td></td><td><input type="submit" id="Submit" name="Submit" value="UPLOAD DATA AND CREATE ACCOUNTS" class="btn btn-default" /></td></tr>
                </table>

             End Using
            </div>
            @*<div Class="col-sm-6">
                @Using (Html.BeginForm("PreviewBulkAccounts", "Home"))
                    @<input type = "submit" Class="btn btn-default" value="PREVIEW ACCOUNT DATA">
                End Using
            </div>*@
    <div>
        @ViewBag.StatusMessage
    </div>
            @*<div>
                 <table id="">
                     @If ViewBag.Data IsNot Nothing Then
                         Dim dtDataTable As DataTable = CType(ViewBag.Data, DataTable)
                         @<tr>
                             @For Each column As DataColumn In dtDataTable.Columns
                                 @<td>@column.ColumnName.ToUpper()</td>
                             Next
                         </tr>

                         If dtDataTable.Rows.Count > 0 Then
                             @For Each Row As DataRow In dtDataTable.Rows
                                 @<tr>
                                     @For Each column As DataColumn In dtDataTable.Columns
                                         @<td data-title='@column.ColumnName'>
                                             @Row(column).ToString()
                                         </td>
                                     Next
                                 </tr>
                             Next
                Else
                             Dim iCount As Integer = dtDataTable.Columns.Count
                             @<tr>

                                 <td colspan='@iCount' style="color:red;">No Data Found.</td>
                             </tr>
                         End If
                     Else
                         If ViewBag.Error IsNot Nothing Then
                             @<tr>
                                 <td style="color:red;">
                                     @IIf(ViewBag.Error IsNot Nothing, ViewBag.Error.ToString(), "")
                                 </td>
                             </tr>
                         End If
                     End If
                 </table>
             </div>*@
            </div>
    End If





