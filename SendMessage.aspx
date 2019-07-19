<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="SendMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #msg {
            width: 323px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" runat="server" placeholder="电话号码" id="input_phone"/><br />
            <input type="text" runat="server" placeholder="客户" id="input_terminal"/>
            <br />
            <textarea id="msg" rows="2" runat="server">您好!您的资料已经通过审核,请用当前手机号码登录,初始密码为本手机号码后6位,谢谢!</textarea><br />
            <asp:Button runat="server" ID="btn_sendmsg" OnClick="btn_sendmsg_Click" Text="发送"/>
        </div>
    </form>
</body>
</html>
