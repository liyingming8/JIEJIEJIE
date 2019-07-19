<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_BaseClassAddEdit.aspx.cs" Inherits="Admin_TJ_BaseClassAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_BaseClass</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr><th>父级类型</th><td><asp:DropDownList ID="ComboBox_ParentID" runat="server">
                                </asp:DropDownList></td></tr>
                        <tr><th>类型名称</th><td><input id="inputCName" runat="server" type="text" maxlength="40" /></td></tr>
                        <tr><th>排列顺序</th><td><input id="inputShowOrder" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" value="0" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr> 
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField runat="server" ID="HF_CMD" />
        <asp:HiddenField runat="server" ID="HF_ID" />
        <asp:HiddenField runat="server" ID="HF_ParentID" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
