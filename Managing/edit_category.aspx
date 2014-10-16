<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_category.aspx.cs" Inherits="AspNetDataHandler.Managing.edit_category" %>

<style>
    .centerdiv {
        position:fixed;
        top: 35%;
        left: 52%;
        width: 12em;
        height:4em;
        margin-top: -9em; 
        margin-left: -15em; 
        border: grey 1px solid;
    }

    html {
         overflow-y: hidden;
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
    
    <h1>Bewerken</h1>

    <form id="form1" runat="server">
        <div class="centerdiv">
            <table>
                <tr>
                    <td>
                        Naam
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox Width="60" ID="NameTextBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="SaveButton" OnClick="SaveButton_OnClick"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
