<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AspNetDataHandler.login" %> 
<!DOCTYPE html>
<style>
    .centerdiv {
        position:fixed;
        top: 50%;
        left: 50%;
        width:30em;
        height:18em;
        margin-top: -9em; /*set to a negative number 1/2 of your height*/
        margin-left: -15em; /*set to a negative number 1/2 of your width*/
    }
</style>
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
            <asp:Login 
                ID = "LoginControl" 
                runat = "server" 
                OnAuthenticate= "LoginControl_OnAuthenticate" 
                BorderColor="#CCCCCC" 
                BorderStyle="Solid" 
                UserNameLabelText="Username:"
                UserNameRequiredErrorMessage="Username is required." 
                TextBoxStyle-Width="225px" 
                LoginButtonText="Login" 
                TitleText=" " 
                BorderWidth="1px">
                
            </asp:Login>
        </div>
    </form>
</body>
</html>
