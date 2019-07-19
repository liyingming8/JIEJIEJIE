<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemConfig.aspx.cs" Inherits="CRM_SystemConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <table class="gridtable">
                <tr>
                    <td style="padding: 10px;">系统LOGO</td>
                    <td>
                        <table style="border: 1px solid #eee; border-collapse: collapse; margin-bottom: 10px;">
                            <tr>
                                <td style="padding: 10px;">
                                    <asp:Image ID="Image_Logo" Height="80" runat="server" ImageUrl="~/CRM/images/NoPic.gif" />
                                </td>
                                <td>
                                    <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                                        src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200"
                                        style="vertical-align: text-bottom" width="250"></iframe>
                                </td>
                                <td style="padding: 10px; color: #ff0000;">图像类型:JPG或PNG，宽高为:200px*200px</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px;">首页顶部图</td>
                    <td>
                        <table style="border: 1px solid #eee; border-collapse: collapse; margin-bottom: 10px;">
                            <tr>
                                <td style="padding: 10px;">
                                    <asp:Image ID="Image_ShouYe_Top" Height="80" runat="server" ImageUrl="~/CRM/images/NoPic.gif" />
                                </td>
                                <td>
                                    <iframe id="I2" frameborder="0" height="23" name="I1" scrolling="no"
                                        src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_ShouYe_Top&amp;TargetHd=HF_Image_ShouYe_Top&amp;imgMaxSize=200"
                                        style="vertical-align: text-bottom" width="250"></iframe>
                                </td>
                                <td style="padding: 10px; color: #ff0000;">图像类型:JPG或PNG，宽高为:640px*320px</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px;">授权单位</td>
                    <td>
                        <input id="input_authorcompanyname" runat="server" type="text" /></td>
                </tr>
                <tr>
                    <td style="padding: 10px;">分享名称</td>
                    <td>
                        <input id="input_sharename" type="text" runat="server" /></td>
                </tr>
                <tr>
                    <td style="padding: 10px;">公章图像</td>
                    <td> <table style="border: 1px solid #eee; border-collapse: collapse; margin-bottom: 10px;">
                            <tr>
                                <td style="padding: 10px;">
                                    <asp:Image ID="Image_SignPic" Height="80" runat="server" ImageUrl="~/CRM/images/NoPic.gif" />
                                </td>
                                <td>
                                    <iframe id="I2" frameborder="0" height="23" name="I1" scrolling="no"
                                        src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_SignPic&amp;TargetHd=HF_Image_SignPic&amp;imgMaxSize=200"
                                        style="vertical-align: text-bottom" width="250"></iframe>
                                </td>
                                <td style="padding: 10px; color: #ff0000;">图像类型:PNG，宽高为:400px*400px</td>
                            </tr>
                        </table></td>
                </tr>
                <tr>
                    <td style="padding: 10px;">登录页背景</td>
                    <td><table style="border: 1px solid #eee; border-collapse: collapse; margin-bottom: 10px;">
                            <tr>
                                <td style="padding: 10px;">
                                    <asp:Image ID="img_loginbackurl" Height="100" runat="server" ImageUrl="~/CRM/images/NoPic.gif" />
                                </td>
                                <td>
                                    <iframe id="I2" frameborder="0" height="23" name="I1" scrolling="no"
                                        src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=img_loginbackurl&amp;TargetHd=hf_loginbackurl&amp;imgMaxSize=200"
                                        style="vertical-align: text-bottom" width="250"></iframe>
                                </td>
                                <td style="padding: 10px; color: #ff0000;">图像类型:JPG，宽高为:750px*1334px</td>
                            </tr>
                        </table></td>
                </tr>
            </table>
        </div>
        <div style="margin: 15px 0 0 90px">
            <asp:Button runat="server" ID="btn_ok" Text="确 定" CssClass="btn btn-warning btnyd" OnClick="btn_ok_Click" /></div>
        <asp:HiddenField ID="HF_LogoImage" runat="server" />
        <asp:HiddenField ID="HF_Image_SignPic" runat="server" />
        <asp:HiddenField ID="HF_Image_ShouYe_Top" runat="server" />
        <asp:HiddenField ID="hf_loginbackurl" runat="server" />
    </form>
    <script src="../include/js/jquery.min.js"></script>
    <script src="../include/js/UploadImage.js"></script>
</body>
</html>
