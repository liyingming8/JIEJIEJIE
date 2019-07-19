<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachment.aspx.cs" Inherits="Admin_commonswm_Attachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>uploader</title>
    </head>
    <body style="margin: 0px;">
        <form id="form1" runat="server" style="margin: 0px; vertical-align: central;">
            <div>
                <asp:FileUpload ID="fuLogo" runat="server" Height="20px" Font-Size="12px" Width="180px" />
                <asp:Button ID="btUploadSave" Height="20px" runat="server" Font-Size="12px" Text="上传" onclick="btUploadSave_Click" />        
            </div>
        </form>
    </body>
</html>