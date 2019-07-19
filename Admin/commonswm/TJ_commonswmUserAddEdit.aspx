<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_commonswmUserAddEdit.aspx.cs" Inherits="Admin_commonswm_TJ_commonswmUserAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_User</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/windows.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>所属单位</th>
                            <td>
                                <input id="txCompID" runat="server" class="length11" type="text" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                            <th>系统角色</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_RID" runat="server" DataTextField="RoleName"
                                    DataValueField="RID">
                                </asp:DropDownList>
                                <asp:Button ID="Button2" runat="server" Text="角色功能" OnClick="Button2_Click" />
                        </tr>
                        <tr>
                            <th>登录名</th>
                            <td>
                                <input id="inputLoginName" runat="server" type="text" maxlength="25" class="input7" />
                            </td>
                        </tr>
                        <tr>
                            <th>密码</th>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox_PassWords" runat="server" TextMode="Password"></asp:TextBox></td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_ModifyAsDefault" runat="server" Text="修改成初始密码：123456" Visible="False" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>昵称</th>
                            <td>
                                <input id="inputNickName" runat="server" type="text" maxlength="20" /></td>
                        </tr>
                        <tr>
                            <th>手机号码</th>
                            <td>
                                <input id="inputMobileNumber" runat="server" type="text" maxlength="25" class="input7" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" onafterpaste="this.value=this.value.replace(/[^0-9]/g,'')" />
                            </td>

                        </tr>
                        <tr id="trIsActived" runat="server">
                            <th>是否激活</th>
                            <td>
                                <asp:RadioButtonList ID="radiolist_Actived" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="0">未激活</asp:ListItem>
                                    <asp:ListItem Value="1">激活</asp:ListItem>
                                    <asp:ListItem Value="2">试用</asp:ListItem>
                                    <asp:ListItem Value="3">冻结</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>来自</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_From" runat="server" DataTextField="CName"
                                    DataValueField="CID">
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" maxlength="25" type="text" />
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_IsSuperAministrator" runat="server" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="hf_compid" runat="server" />
    </form>
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
