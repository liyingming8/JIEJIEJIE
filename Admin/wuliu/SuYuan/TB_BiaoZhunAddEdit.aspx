<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_BiaoZhunAddEdit.aspx.cs" Inherits="Admin_TB_BiaoZhunAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_BiaoZhun</title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table>
           
                    <tr><td>标准名称</td><td><input id="inputBiaoZhunname" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            
                    <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="100" /></td><td></td></tr>
           
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
    </body>
</html>