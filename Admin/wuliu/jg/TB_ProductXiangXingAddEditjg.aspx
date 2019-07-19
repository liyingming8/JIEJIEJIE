<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_ProductXiangXingAddEditjg.aspx.cs" Inherits="Admin_TB_ProductXiangXingAddEditjg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_ProductXiangXing</title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12pt; font-weight: bold; color: #3666AA"><img src="../../images/icon.gif" align="middle" style="border-width: 0px;" /> 香型编辑</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="javascript:history.go(-1)"><img title="返回" src="../../images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table>
                <br />
                <table>
                    <tr><td>香型</td><td><input id="inputXiangXing" runat="server" type="text" maxlength="15" /></td><td>
                                                                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputXiangXing" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                   </td></tr>
                    <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
    </body>
</html>