<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardInfo_ImagesAddEdit.aspx.cs" Inherits="Admin_TJ_AwardInfo_ImagesAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_AwardInfo_Images</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <table class="gridtable">
                    <tr>
                        <th>奖品</th>
                        <td>
                            <asp:Label runat="server" ID="label_awdnm"></asp:Label> 
                        </td>
                    </tr>
                    <tr>
                        <th>显示</th>
                        <td><asp:CheckBox runat="server" ID="ckb_isshow"/></td>
                    </tr>
                    <tr>
                        <th>图片</th>
                        <td>
                            <table style="border-style: none; border-color: white; border-width: 0px; padding: 0px; margin: 0px; table-layout: auto">
                                <tr>
                                    <td>
                                        <asp:Image ID="Image_AwardUrl" runat="server" ImageUrl="~/Admin/Images/NoPic.gif" Width="120px" />
                                    </td>
                                    <td>
                                        <input id="Button_uplogo" type="button" class="btn" value="上传LOGO图" onclick="openWinCenter('getpic/piccutter.aspx?key=Image_AwardUrl&hdsv=HF_ImageUrl&bilv=0.75', 410, 410, '上传奖品图')" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th>备注</th>
                        <td>
                            <input id="inputremarks" runat="server" type="text" class="length10" maxlength="25" /></td>
                    </tr>
                </table>
                <div class="bottomdivbutton">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_ImageUrl" runat="server" />
        <asp:HiddenField runat="server" ID="hd_awid"/>
        <script type="text/javascript" src="../include/js/jquery-2.1.1.min.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
