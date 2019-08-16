<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompADInfoAddEdit.aspx.cs" Inherits="Admin_TJ_CompADInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_CompADInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" /> 
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>广告位置</th>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxADID" runat="server" DataTextField="ADName" DataValueField="ADID" AutoPostBack="True" OnComboBoxChanged="ComboBoxADID_ComboBoxChanged">
                                                <asp:ListItem Text="请选择..." Value="0"></asp:ListItem>
                                            </asp:DropDownList> 
                                            <asp:Label ID="Label_AdSize" runat="server" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>文件</th>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_Pic" Width="200px" runat="server" /> 
                                            <iframe id="I0" frameborder="0" height="23" name="I1" scrolling="no"
                                                src="Attachment.aspx?PicUrl=adfile&amp;TargetImg=Image_Pic&amp;TargetHd=HF_FilePath&amp;imgMaxSize=102400"
                                                style="vertical-align: text-bottom;color:red" width="250"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>是否有效</th>
                            <td>
                                <asp:CheckBox ID="CheckBox_IsActive" runat="server" Text="是否有效" />
                            </td>
                        </tr>
                        <tr>
                            <th>产品</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_GoodsID" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>外链</th>
                            <td>
                                <input id="inputSpecialURLLink" placeholder="另外跳转到新的网址（可不填）" runat="server" type="text" maxlength="50" class="input6" />
                            </td> 
                        </tr>
                        <tr>
                            <th>说明</th>
                            <td>
                                <input id="inputDiscriptions" placeholder="简单说明（可不填）" runat="server" type="text" maxlength="50" class="input6" />
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" placeholder="简单备注（可以不填）" type="text" maxlength="25" class="input6" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_FilePath" runat="server" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
</body>
</html>
