<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register_entry_form.aspx.cs" Inherits="AspNetDataHandler.Register.register" %>
<style>
    .centerdiv {
        position:fixed;
        top: 30%;
        left: 45%;
        width: 23%;
        height:auto;
        float: left;
        margin-top: -9em; 
        margin-left: -15em; 
        border: grey 1px solid;
    }

    html {
         overflow-y: hidden;
    }
</style>
<script src="/Scripts/jquery-2.1.1.js"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="header">
        
    </div>
    <script src="/Header/loadheader.js"></script>
    
    <form id="form" runat="server">
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
                            <asp:TextBox Width="170" ID="NameTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Klasse
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" Width="170" ID="KlasseDropDownList" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Paard 1 
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" Width="170" ID="horse1DropDownList" OnSelectedIndexChanged="horse1DropDownList_OnSelectedIndexChanged" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Paard 2 
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" Width="170" ID="horse2DropDownList" OnSelectedIndexChanged="horse2DropDownList_OnSelectedIndexChanged" runat="server"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Paard 3
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" Width="170" ID="horse3DropDownList" OnSelectedIndexChanged="horse3DropDownList_OnSelectedIndexChanged" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            <asp:TextBox Visible="False" Height="100" Width="170" ID="RemarkTextBox" TextMode="MultiLine" ReadOnly="True" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            MP3
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUploader"  runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Inschrijven" ID="Inschrijven" OnClick="Inschrijven_OnClick"/>
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
