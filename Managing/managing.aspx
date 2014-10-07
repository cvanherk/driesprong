<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managing.aspx.cs" Inherits="AspNetDataHandler.Managing.managing" %>
<style>
    .centerdiv {
        position:fixed;
        top: 30%;
        left: 55%;
        width: 33em;
        height:18em;
        margin-top: -9em; 
        margin-left: -15em; 
        /*border: grey 1px solid;*/
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
<!DOCTYPE html>
<script src="/Scripts/jquery-2.1.1.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="header">
        
    </div>
    <script src="/Header/loadheader.js"></script>
    <form id="form1" runat="server">
    <div class="centerdiv">
        <table class="headerTable">
            <tr>
                <td>
                    <div id="button" url="/Managing/managing_horses.aspx">
                        Paarden beheren
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="button" url="/Managing/managing_classes.aspx">
                        Klassen beheren
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="button" url="/Managing/managing_entry_forms.aspx">
                        Inschrijf formulieren
                    </div>
                </td>
            </tr>
        </table>
    </div>
        
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
    </form>
</body>
</html>
