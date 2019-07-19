<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DjManageAddEdit.aspx.cs" Inherits="Admin_TJ_DjManageAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_DjManage</title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">  
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager> 
                <table class="gridtable">
                    <%--<tr><td>ZjID</td><td><input id="inputZjID" runat="server" type="text" maxlength="2" /></td><td></td></tr>--%>
                    <tr><th>中奖码</th><td><input id="inputZjNumber" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>中奖时间</th><td><asp:TextBox ID="inputZjTime" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="inputZjTime"  ID="CalendarExtender1" runat="server"></cc2:CalendarExtender></td></tr>
                    <tr><th>奖项名称</th><td><input id="inputJxName" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>中奖手机号码</th><td><input id="inputZjPhone" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>销售地区</th><td><input id="inputCpXS" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>查询地区</th><td><input id="inputCpCX" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>兑奖点名称</th><td><input id="inputDjdName" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>兑换时间</th><td><asp:TextBox ID="inputDhTime" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="inputDhTime"  ID="CalendarExtender2" runat="server"></cc2:CalendarExtender></td></tr>
                    <tr><th>是否中奖</th><td><input id="inputDjFlag" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>领奖时间</th><td><asp:TextBox ID="inputLjTime" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="inputLjTime"  ID="CalendarExtender3" runat="server"></cc2:CalendarExtender></td></tr>
                    <tr><th>验证码</th><td><input id="inputZjyzCode" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th></th><td><input id="inputDelFlag" runat="server" type="text" maxlength="1"  value ="1" visible ="false"/></td></tr>
                </table> 
                 <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
    </body>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</html>