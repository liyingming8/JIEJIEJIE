<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JieDan.aspx.cs" Inherits="msite_JieDan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
        <title></title>
        <link href="default.css" rel="stylesheet" />
        <link href="../css/bootstrap.min.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container-fluid"></div>
            <div class="jiedanalert">
                <asp:Label ID="labelalert" runat="server" Text="请务必在12小时之内发货，您确定接单吗？"></asp:Label>
            </div>
            <div>
                <asp:Button ID="Button_OK" CssClass="BottonQuerenJieDan" runat="server" Text="确认接单" OnClick="Button_OK_Click" /></div>
            <asp:HiddenField ID="HF_OrderID" runat="server" />
            <asp:HiddenField ID="HF_Cmd" runat="server" />
        </form>
    </body>
</html>