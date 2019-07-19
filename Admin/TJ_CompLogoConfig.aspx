<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompLogoConfig.aspx.cs" Inherits="Admin_TJ_CompLogoConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div><asp:Image ID="Image_Logo" runat="server" /></div>
                <div><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" 
                             src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=200" 
                             style="vertical-align: text-bottom" width="250"></iframe></div>
                <div>
                    <asp:Button ID="Button1" runat="server" Text="确定" OnClick="Button1_Click" /></div>
                <asp:HiddenField  ID="HF_ImageURL" runat="server"/>
            </div>
        </form>
    </body>
</html>