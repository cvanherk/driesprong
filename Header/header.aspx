<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header.aspx.cs" Inherits="AspNetDataHandler.header" %>

<style>
    .headerdiv {
        /*width: 400px ;
        margin-left: auto ;
        margin-right: auto ;*/
    }

    .headerTable tr td {
        width: 150px;
    }

    .hover {
        background-color: #b7c3c2 !important;
        color: blue !important;
        cursor: pointer;
    }
    #button {
        text-align: center;   
        margin: 0 auto;
        border: grey 1px solid;
        font-family: helvetica;
        white-space: nowrap;
        width:100%;
    }

    hr { 
        display: block;
        margin-top: 0.5em;
        margin-bottom: 0.5em;
        margin-left: auto;
        margin-right: auto;
        border-style: inset;
        border-width: 1px;
    }
</style>
<script src="/Scripts/jquery-2.1.1.js"></script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="headerdiv">
            <table class="headerTable">
                <tr>
                    <td>
                        <%=HeaderMessage()%>
                    </td>
                    <td></td>
                    <td>
                        <div id="button" url="/Managing/managing.aspx">
                            Beheren 
                        </div>
                    </td>
                    <td></td>
                    <td>
                        <div id="button" url="/Register/register_entry_form.aspx">
                            Inschrijven
                        </div>
                    </td>
                </tr>
            </table>
            <hr />
            <script>
                var $button = $('div[id=button]');
                $button.hover(function () {
                    $(this).addClass('hover');
                }, function () {
                    $(this).removeClass('hover');
                });

                $button.click(function (data) {
                    window.location.href = data.currentTarget.getAttribute('url');
                });

        </script>
        </div>
        
    </form>
</body>
</html>
