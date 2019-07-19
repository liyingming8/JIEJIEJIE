﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_PublishImageInfoAddEdit.aspx.cs" Inherits="Admin_TJ_PublishImageInfoAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_PublishImageInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th></th>
                            <td>
                                <asp:Label ID="Label_IFID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>图片</th>
                            <td colspan="2">
                                <asp:Image ID="Image_GoodPic" runat="server" /><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                                    src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_GoodPic&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=3000"
                                    style="vertical-align: text-bottom" width="250"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text" maxlength="50" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_ImageURL" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_IFID" runat="server" />
    </form>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
