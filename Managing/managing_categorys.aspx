<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managing_categorys.aspx.cs" Inherits="AspNetDataHandler.Managing.managing_categorys" %>
<style>
    .centerdiv {
        position:fixed;
        top: 30%;
        left: 50%;
        width:30em;
        height:18em;
        margin-top: -9em; /*set to a negative number 1/2 of your height*/
        margin-left: -15em; /*set to a negative number 1/2 of your width*/
    }


    .hoverHorseItem {
        cursor: pointer;
        background-color: orange;
    }
</style>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script src="/Scripts/jquery-2.1.1.js"></script>
    <body>
    <div id="header">
        
    </div>
    <script src="/Header/loadheader.js"></script>
        
        <h1>Beheren</h1>
         
        <form id="form1" runat="server">
            <div class="centerdiv">
                <table id="input">
                    <tr>
                        <td>
                            <a href="/Managing/edit_category.aspx">Klik hier om een nieuw klasse toe tevoegen</a>
                        </td>
                    </tr>
                </table>
                <table id="categorys">
                    <%=GenerateCategoryTable()%>
                </table>
            </div>
        <script type="text/javascript">
           
            $('div[id=deleteButton]').click(function (data) {
                var $virtualcallback = this;
                //alert(data.currentTarget.getAttribute('guid'));
                var request = $.ajax({
                    url: "/Managing/delete_category.aspx",
                    type: "POST",
                    data: { guid: data.currentTarget.getAttribute('guid') },
                    dataType: "text"
                });

                request.done(function (data) {
                    if (data.toLowerCase() == 'not logged in') {
                        window.location.href = '/login.aspx';
                        return;
                    }
                    var tr = $($virtualcallback).closest('tr');
                        tr.remove();
                });

                request.fail(function (textStatus) {
                    alert("Er is iets misgegaan: " + textStatus);
                });

            });

            $('td[id=categoryItem]').hover(function() {
                $(this).addClass('hoverHorseItem');
            }, function() {
                $(this).removeClass('hoverHorseItem');
            });

            $('td[id=categoryItem]').click(function (data) {
                window.location.href = "/Managing/edit_category.aspx?recordguid=" + data.currentTarget.getAttribute('guid');
            });
        
        </script>
        </form>
    </body>
</html>
