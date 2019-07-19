<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ActivityBorad_SampleImageAddEdit.aspx.cs" Inherits="Admin_TJ_ActivityBorad_SampleImageAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_ActivityBorad_SampleImage</title>
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="toptitle">模板图像编辑</div>
                <table class="gridtable">
                    <tr>
                        <th>所述模板</th>
                        <td>
                            <asp:Label ID="labelaboardname" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>关键字</th>
                        <td>
                            <input id="inputKeyValue" runat="server" type="text" maxlength="25" /></td>
                    </tr>
                    <tr>
                        <th>图像</th>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image_ShowPic" runat="server" Height="120px" ImageUrl="~/images/nopic.gif" />
                                    </td>
                                    <td>
                                        <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_ShowPic&amp;TargetHd=HF_Image&amp;imgMaxSize=400" style="vertical-align: text-bottom" width="250"></iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th>实例显示</th>
                        <td>
                            <asp:CheckBox ID="CheckBox_ForShow" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>备注</th>
                        <td>
                            <input id="inputRemarks" runat="server" class="p60" type="text" maxlength="25" /></td>
                    </tr>
                </table>
                <div class="bottomdivbutton">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <script src="../js/jquery-1.7.1.js"></script>
        <asp:HiddenField ID="HF_ABID" runat="server" />
        <script src="../include/js/UploadImage.js"></script>
        <asp:HiddenField ID="HF_Image" runat="server" />
    </form>
</body>
</html>
