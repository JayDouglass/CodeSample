<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StartTask.aspx.vb" Inherits="AjaxJobProgress.StartTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <script type="text/javascript">
        /* example of javascript used to start task */
        $(document).ready(function () {
            $(".button-print-licenses").click(function () {
                StartPrintLicenses();
            });
        });

        function StartPrintLicenses() {
            $(".hide-while-processing").hide();
            $("#statusMessage").html("Requesting ...");
            $("#task-status").show();
            AsyncTasks.StartPrintLicensesTask(StartPrintLicenses_Complete, function (error) { jqAlert(error.message, 'Task Failed', true, false); });
        }

        function StartPrintLicenses_Complete(result) {
            GetStatus(result)
        }

        function GetStatus(taskId) {
            AsyncTasks.GetStatus(taskId, GetStatus_Complete(taskId), function (error) { jqAlert(error.message, 'Task Failed', true, false); });
        }

        function GetStatus_Complete(taskId) {
            return function (result) {
                $("#progressbar").progressbar({
                    value: result.PercentComplete
                });
                $("#statusMessage").html(result.Message);
                if (result.Finished == true) { // if finished, stop AJAX calls                 
                    window.setTimeout(function () { // display alert, wait a little to let UI render last update
                        // jqAlert('Finished making license PDFs', 'Task Completed', false, true);
                        alert("Finished making license PDFs");
                        window.location.reload();
                    }, 500);

                } else { // task not finished, make another AJAX call to get status
                    window.setTimeout(function () { GetStatus(taskId); }, 500);
                }
            }
        }
    </script>
</body>
</html>
