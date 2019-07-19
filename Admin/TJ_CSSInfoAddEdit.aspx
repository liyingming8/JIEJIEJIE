<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CSSInfoAddEdit.aspx.cs" Inherits="Admin_TJ_CSSInfoAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_CSSInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
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
                            <th>样式名称</th>
                            <td>
                                <input id="inputCSSName" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th>css文件</th>
                            <td>
                                <asp:Label ID="Label_CssFileName" runat="server"></asp:Label><iframe id="I0" frameborder="0" height="23" name="I1" scrolling="no"
                                    src="Attachment.aspx?PicUrl=css&amp;TargetHd=HF_CssURL&amp;imgMaxSize=200"
                                    style="vertical-align: text-bottom" width="250"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <th>显示图片</th>
                            <td colspan="2">
                                <asp:Image ID="ImageCssLogo" runat="server" /><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                                    src="Attachment.aspx?PicUrl=csslogo&amp;TargetImg=ImageCssLogo&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=200"
                                    style="vertical-align: text-bottom" width="250"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <th>Logo文件夹</th>
                            <td colspan="2">
                                <input id="inputLogoDirInfo" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <asp:HiddenField ID="HF_CMD" runat="server" />
                        <asp:HiddenField ID="HF_ID" runat="server" />
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                    <asp:HiddenField ID="HF_ImageURL" runat="server" />
                    <asp:HiddenField ID="HF_CssURL" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script type="text/javascript" src="../include/js/UploadImage.js"></script>
</body>
</html>
