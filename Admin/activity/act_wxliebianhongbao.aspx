<%@ Page Language="C#" AutoEventWireup="true" CodeFile="act_wxliebianhongbao.aspx.cs" Inherits="Admin_activity_act_wxliebianhongbao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" /> 
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="editpageback">
    <table class="gridtable">
        <tr><th>红包金额</th><td>￥<input id="inputminvalue" runat="server" value="1" class="p20"/></td></tr>
        <tr><th>数量上限/天</th><td><input id="limitnumday" value="100" class="p20" runat="server"/></td></tr>  
    </table>
        <div class="bottomdivbutton"><asp:Button runat="server" ID="btn_ok" Text="确定" CssClass="btn btn-warning btnyd" OnClick="btn_ok_Click"/></div>
    </div>
    </form>
</body>
</html>
