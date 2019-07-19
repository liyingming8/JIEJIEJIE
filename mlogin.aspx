<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mlogin.aspx.cs" Inherits="mlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
        <link href="css/bootstrap.min.css" rel="stylesheet" />
        <link href="msite/default.css" rel="stylesheet" />
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container logindiv">
                <div class="row">
                    <div class="col-xs-12 ititle">登录确认</div>
                </div>
                <div class="row">
                    <div class="col-xs-4 itext">用户名:</div>
                    <div class="col-xs-8"><asp:TextBox placeholder="请输入用户名" runat="server" ID="txtLoginName" CssClass="inputtext"></asp:TextBox></div>
                </div>
                <div class="row">
                    <div class="col-xs-4 itext">密   码:</div>
                    <div class="col-xs-8"><asp:TextBox placeholder="请输入登录密码" runat="server" ID="txtPassword" TextMode="Password" CssClass="inputtext"></asp:TextBox></div>
                </div>
                <div class="row">
                    <div class="col-xs-12"><asp:Button ID="botton_login" CssClass="inputbutton" runat="server" Text="登 录" OnClick="botton_login_Click" /></div>             
                </div>
            </div>
        </form>

    </body>
</html>