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
                <tr>
                    <td>
                        Naam
                    </td>
                    <td>
                        Klasse
                    </td>
                    <td>
                        Paard 1
                    </td>
                    <td>
                        Paard 2
                    </td>
                    <td>
                        Paard 3
                    </td>
                    <td>
                        Muziek
                    </td>
                    <td>
                        Accepteren
                    </td>
                </tr>
                <%=RenderQueueTable()%>
            </table>
            
            
            <div id="audio">
                
            </div>
            
            <script>
                var $buttonAccept = $('img[id=buttonAccept]');
                $buttonAccept.hover(function () {
                    $(this).addClass('hoverAcceptButton');
                }, function () {
                    $(this).removeClass('hoverAcceptButton');
                });

                $buttonAccept.click(function (data) {
                    alert($(data.currentTarget).closest('tr').attr('guid'));
                    var $inputs = $(this).closest('tr').find(":input");
                });

                $('div[id=playpause]').click(function (data) {
                    var $allbuttons = $('[id=playpause]');

                    switch ($(this)[0].innerText) {
                        case "Play":
                            $(this)[0].innerText = "Pause";
                            var request = $.ajax({
                                url: "/AudioPlayer/audioplayer.aspx",
                                type: "POST",
                                data: { guid: $(data.currentTarget).closest('tr').attr('guid') },
                                dataType: "text"
                            });

                            request.done(function (data) {
                                $('#audio').html(data);
                            });

                            request.fail(function (textStatus) {
                                alert("Er is iets misgegaan: " + textStatus);
                            });
                            break;

                        case "Pause":
                            $(this)[0].innerText = "Play";
                            break;
                    }

                    for (var i = 0; i < $allbuttons.length; i++) {
                        var $obj = $allbuttons[i];
                        if ($obj != this)
                            $obj.innerText = "Play";
                    }
                    
                });

            </script>

        </div>
    </form>
</body>
</html>
