<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_JPinfoAddEdit.aspx.cs" Inherits="Admin_TJ_JPinfoAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_JPinfo</title>
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
                    <%--<tr><td></td><td><input id="inputJPID" runat="server" type="text" maxlength="2" /></td><td></td></tr>--%>
                    <tr><th>奖项编号</th><td><input id="inputJxID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>兑奖点名称</th><td><input id="inputDjdName" runat="server" type="text" maxlength="50" /></td></tr>
                    <tr><th>奖品名称</th><td><input id="inputJpName" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>配发数量</th><td><input id="inputPFCount" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>兑换数量</th><td><input id="inputDHCount" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>核销数量</th><td><input id="inputHXCount" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>剩余数量</th><td><input id="inputSYCount" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>创建时间</th><td> <asp:TextBox ID="inputCreatTime" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="inputCreatTime"  ID="CalendarExtender1" runat="server"></cc2:CalendarExtender></td></tr>
                    <%--<tr><td>compID</td><td><input id="inputcompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>--%>
                    <tr><th></th><td><input id="inputDelFlag" runat="server" type="text" maxlength="5"  value ="1" visible ="false" /></td></tr>
                </table>
                      <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>