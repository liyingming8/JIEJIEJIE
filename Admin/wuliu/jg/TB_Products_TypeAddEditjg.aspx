<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_TypeAddEditjg.aspx.cs" Inherits="Admin_TB_Products_TypeAddEditjg" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_Products_Type</title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <table class="gridtable">
                    <tr><td>父级类型</td><td>
                                         <asp:DropDownList ID="ComboBox_ParentID" runat="server">
                                         </asp:DropDownList>
                                     </td><td></td></tr>
                    <tr><td>类型编码</td><td><input id="inputTypeCode" runat="server" type="text" maxlength="30" /></td><td></td></tr>
                    <tr><td>类型名称</td><td><input id="inputTypeName" runat="server" type="text" maxlength="25" /></td><td>
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputTypeName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                    </td></tr>
                    <tr><td></td><td></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
    </body>
</html>