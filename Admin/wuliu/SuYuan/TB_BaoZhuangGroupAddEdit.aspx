<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_BaoZhuangGroupAddEdit.aspx.cs" Inherits="Admin_TB_BaoZhuangGroupAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_BaoZhuangGroup</title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table>
                    <tr><td>bzid</td><td><input id="inputbzid" runat="server" type="text" maxlength="2" /></td><td></td></tr>
                    <tr><td>BZcode</td><td><input id="inputBZcode" runat="server" type="text" maxlength="100" /></td><td></td></tr>
                    <tr><td>BZname</td><td><input id="inputBZname" runat="server" type="text" maxlength="100" /></td><td></td></tr>
                    <tr><td>Compid</td><td><input id="inputCompid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
                    <tr><td>Remarks</td><td><input id="inputRemarks" runat="server" type="text" maxlength="100" /></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
    </body>
</html>