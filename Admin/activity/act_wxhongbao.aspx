<%@ Page Language="C#" AutoEventWireup="true" CodeFile="act_wxhongbao.aspx.cs" Inherits="Admin_activity_act_wxhongbao" %>

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
        <tr><th>红包</th><td>￥<input id="inputminvalue" style="display: inline-block;" runat="server" value="1" class="p20"/></td></tr>
        <tr><th>验证码</th><td>
            <asp:CheckBox ID="ckb_yzm" runat="server" />
            </td></tr>  
        <tr><th>限量/天</th><td>
            <input id="inputlimitnumday" runat="server" class="p20" type="text" /></td></tr>  
        <tr><th>模板选择</th><td>
            <asp:RadioButtonList ID="rbl_activity_board" runat="server" DataTextField="ActivityName" DataValueField="ID" RepeatDirection="Horizontal" RepeatLayout="Flow">
            </asp:RadioButtonList>
            </td></tr>  
        <tr><th>&nbsp;</th><td>&nbsp;</td></tr>  
    </table>
        <div class="bottomdivbutton"><asp:Button runat="server" ID="btn_ok" Text="确定" CssClass="btn btn-warning btnyd" OnClick="btn_ok_Click"/></div>
    </div>
    </form>
</body>
</html>
