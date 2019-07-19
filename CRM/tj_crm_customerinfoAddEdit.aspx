<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerinfoAddEdit.aspx.cs" Inherits="CRM_tj_crm_customerinfoAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>tj_crm_customerinfo</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>父级</th>
                            <td>
                                <%--<asp:DropDownList ID="ComboBox_parentid" runat="server">
                </asp:DropDownList>--%>
                                <input type="text" id="input_parent_nm" runat="server" /><input id="parentid" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <th>级别</th>
                            <td>
                                <asp:DropDownList ID="ddlgradeorder" runat="server" DataTextField="gradename" DataValueField="id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <td>
                                <input id="tx_customername" runat="server" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>性别</th>
                            <td>
                                <asp:RadioButtonList ID="RBL_SexInfo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                                    <asp:ListItem Value="2">女</asp:ListItem>
                                    <asp:ListItem Value="0">保密</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>微信号</th>
                            <td>
                                <input id="tx_wxusernumber" runat="server" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>手机</th>
                            <td>
                                <input id="txphonenumber" runat="server" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>证件号码</th>
                            <td>
                                <input id="txidcardnumber" runat="server" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>授权号</th>
                            <td>
                                <input id="txauthorcode" runat="server" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>城市/地区</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_Cityid" runat="server" MaxHeight="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>详细地址</th>
                            <td>
                                <input id="txaddressinfo" runat="server" type="text" /></td>
                        </tr>
                        <tr>
                            <th>身份证拍照</th>
                            <td colspan="3">
                                <table style="border-width: 0px; border-style: none; border-collapse: collapse; vertical-align: text-bottom;">
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_Logo" Height="200" runat="server" ImageUrl="~/CRM/images/NoPic.gif" />
                                        </td>
                                        <td>
                                            <iframe id="I1" height="23" name="I1" scrolling="no"
                                                src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200"
                                                style="border-style: none; vertical-align: text-bottom" width="250"></iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_cardBehind" Height="200" runat="server" ImageUrl="~/CRM/images/NoPic.gif" />
                                        </td> 
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="rowreturnpsw" runat="server" visible="False">
                            <th>还原密码</th>
                            <td colspan="3">
                                <asp:CheckBox ID="CheckBox_ReturnPSW" runat="server" />
                            </td>
                        </tr>
                        <tr >
                            <th>是否审核</th>
                            <td colspan="3">
                                <asp:CheckBox ID="CheckBox_Ispermit" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                         <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_LogoImage" runat="server" />
    </form>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    <script src="../include/js/UploadImage.js"></script>
</body>
</html>
