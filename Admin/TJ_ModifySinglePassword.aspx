<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ModifySinglePassword.aspx.cs" Inherits="Admin_TJ_ModifySinglePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
        <link href="../include/windows.css" rel="stylesheet" type="text/css" />
        <link href="../include/easyui.css" rel="stylesheet" />
        <style type="text/css">
            body {
                line-height: 2.2rem;
            }
            input {
                height: 1.8rem;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="gridtable">  
                            <tr>
                                <th>用户名</th>
                                <td>
                                    <asp:Label ID="Label_UserName" runat="server"></asp:Label>
                                </td> 
                            </tr>
                            <tr>
                                <th>原密码</th>
                                <td>
                                    <asp:TextBox ID="TextBox_OldPassword" runat="server" CssClass="input5" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_OldPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td> 
                            </tr>
                            <tr>
                                <th>新密码</th>
                                <td>
                                    <asp:TextBox ID="TextBox_NewPassWord" runat="server" CssClass="input5" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_NewPassWord" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>  
                            </tr>
                            <tr>
                                <th>再输一次</th>
                                <td>
                                    <asp:TextBox ID="TextBox_NewPassWordRepeater" runat="server" CssClass="input5" TextMode="Password"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox_NewPassWord" ControlToValidate="TextBox_NewPassWordRepeater" ErrorMessage="新密码两次输入不一致！"></asp:CompareValidator>
                                </td> 
                            </tr>
                            <tr >
                                <th></th>
                                <td style="padding: 1rem 0.31rem;">
                                    <asp:Button ID="Button_Ok" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button_Ok_Click" Text="确定" />
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