<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_do_title.aspx.cs" Inherits="Admin_TJ_do_title" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title></title>
    <link href="../include/windows.css" rel="stylesheet" />
    <style type="text/css">
        .row {
            height: 32px;
            line-height: 32px;
        }
        .alert {
            color: darkorange;
            font-weight: bold;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <div class="row">尊敬的【<%=GetCompName() %>】欢迎您来到结构三维码世界</div>
            <div class="row alert"><asp:Label runat="server" ID="lab_reson"></asp:Label></div>
            <div class="row"></div>
            <div class="row"><input type="button" class="btn btn-warning" value="确定" id="btn_ok" onclick="clearandgo()" /></div>
        </div>
    </form>
    <script src="js/frankprocess.js"></script>
</body>
</html>
