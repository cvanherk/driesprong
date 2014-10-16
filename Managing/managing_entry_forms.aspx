<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managing_entry_forms.aspx.cs" Inherits="AspNetDataHandler.Managing.managing_entry_forms" %>

<!DOCTYPE html>
<style>
     .queueTable {
         width: 100%;
         border: 1px black solid;
     }

    .hoverAcceptButton {
        cursor: pointer;
    }
</style>
<script src="/Scripts/jquery-2.1.1.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="header"></div>
    <script src="/Header/loadheader.js"></script>
    <form id="form1" runat="server">
        <div>
            <table class="queueTable">
                <%=RenderQueueTable()%>
            </table>
            
            <script>
                var $buttonAccept = $('img[id=buttonAccept]');
                $buttonAccept.hover(function () {
                    $(this).addClass('hoverAcceptButton');
                }, function() {
                    $(this).removeClass('hoverAcceptButton');
                });

                $buttonAccept.click(function (data) {
                    alert(data.currentTarget.getAttribute('guid'));
                    var $inputs = $(this).closest('tr').find(":input"); 
                });
            </script>
        </div>
    </form>
</body>
</html>
