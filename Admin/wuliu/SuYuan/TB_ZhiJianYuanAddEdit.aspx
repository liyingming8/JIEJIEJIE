<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_ZhiJianYuanAddEdit.aspx.cs" Inherits="Admin_TB_ZhiJianYuanAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_ZhiJianYuan</title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table>
               
                    <tr>
                        <td>质检员名称</td>
                        <td>
                            <input id="inputZJname" runat="server" type="text" maxlength="100" /></td>
                        <td></td>
                    </tr>
               

                    <tr>
                        <td>联系电话</td>
                        <td>
                            <input id="inputPhone" runat="server" type="text" maxlength="10" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td>
                            <input id="inputRemarks" runat="server" type="text" maxlength="100" /></td>
                        <td></td>
                    </tr>
                
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" /></td>
                        <td>
                            <asp:HiddenField ID="HF_CMD" runat="server" />
                            <asp:HiddenField ID="HF_ID" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </form>
    </body>
</html>