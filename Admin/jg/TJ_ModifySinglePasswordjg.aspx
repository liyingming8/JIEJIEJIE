<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ModifySinglePasswordjg.aspx.cs" Inherits="Admin_TJ_ModifySinglePasswordjg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 80%;" class="addedittable">
                            <tr>
                                <th colspan="5">修改个人密码</th>
                            </tr>
                            <tr>
                                <td>用户名</td>
                                <td>
                                    <asp:Label ID="Label_UserName" runat="server"></asp:Label>
                                </td>
                                <td>原密码</td>
                                <td>
                                    <asp:TextBox ID="TextBox_OldPassword" runat="server" CssClass="input5" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_OldPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td rowspan="2">
                                    <asp:Button ID="Button_Ok" runat="server" CssClass="inputbutton" OnClick="Button_Ok_Click" Text="确定" />
                                </td>
                            </tr>
                            <tr>
                                <td>新密码</td>
                                <td>
                                    <asp:TextBox ID="TextBox_NewPassWord" runat="server" CssClass="input5" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_NewPassWord" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td>再输一次</td>
                                <td>
                                    <asp:TextBox ID="TextBox_NewPassWordRepeater" runat="server" CssClass="input5" TextMode="Password"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox_NewPassWord" ControlToValidate="TextBox_NewPassWordRepeater" ErrorMessage="新密码两次输入不一致！"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:HiddenField ID="HF_UID" runat="server" />
        </form>
    </body>
</html>