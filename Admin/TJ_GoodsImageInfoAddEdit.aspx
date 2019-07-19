<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_GoodsImageInfoAddEdit.aspx.cs" Inherits="Admin_TJ_GoodsImageInfoAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_GoodsImageInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <table class="gridtable">
                <tr>
                    <th>产品</th>
                    <td>
                        <asp:Label ID="Label_GoodsName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>显示</th>
                    <td>
                        <asp:CheckBox ID="CheckBox_Show" runat="server" TextAlign="Left" />
                    </td>
                </tr>
                <tr>
                    <th>产品图片</th>
                    <td colspan="2">
                        <asp:Image ID="Image_GoodPic" runat="server" /><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                            src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_GoodPic&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=200"
                            style="vertical-align: text-bottom" width="250"></iframe>
                    </td>
                </tr>
            </table> 
        </div>
        <div class="bottomdivbutton">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_GoodsID" runat="server" />
        <asp:HiddenField ID="HF_ImageURL" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>    
</body>
</html>
