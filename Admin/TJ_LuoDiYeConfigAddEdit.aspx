<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_LuoDiYeConfigAddEdit.aspx.cs" Inherits="Admin_TJ_LuoDiYeConfigAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>落地页面配置</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">  
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
                <table class="gridtable">
                    <tr><th>产品</th><td>
                                       <asp:DropDownList ID="ComboBox_WLProID" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID" AppendDataBoundItems="True">
                                           <asp:ListItem Value="0">指定产品...</asp:ListItem>
                                       </asp:DropDownList>
                                   </td></tr>
                    <tr><th>限查次数</th><td><input id="inputLimitedCheckNum" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="input4" /></td></tr>
                    <tr><th>警告提示</th><td class="input6" colspan="2"><input id="inputAlertContents" runat="server" type="text" maxlength="50"  /></td></tr>
                    <tr><th>显示顶部广告</th><td>
                                           <asp:CheckBox ID="CheckBox_ShowTopAd" runat="server" />
                                       </td></tr>
                    <tr><th>显示身份信息</th><td>
                                           <asp:CheckBox ID="CheckBox_ShowSuYuan" runat="server" />
                                      </td></tr>
                    <tr><th>显示功能模块</th><td>
                                           <asp:CheckBox ID="CheckBox_ShowModules" runat="server" />
                                       </td></tr>
                    <tr><th>显示溯源信息</th><td>
                                           <asp:CheckBox ID="CheckBox_ShowTraceInfo" runat="server" />
                                      </td></tr>
                    <tr><th>备注</th><td colspan="2"><input id="inputRemarks" runat="server" type="text" maxlength="25" class="input300" /></td></tr>
                </table>
                      <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>