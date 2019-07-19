<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_brandpicturesAddEdit.aspx.cs" Inherits="CRM_tj_crm_brandpicturesAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_brandpictures</title>
　　<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="editpageback">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
         <table class="gridtable">
            <tr><th>品牌</th><td>
                <asp:Label ID="LabelBrandName" runat="server"></asp:Label>
                </td></tr>
            <tr><th>图像</th><td><table style="border: none;border-collapse:collapse;"><tr><td>
                                                   <asp:Image ID="Image_Logo" Height="80" runat="server" ImageUrl="~/CRM/images/NoPic.gif" /> 
                                               </td><td>
                                                        <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" 
                                                                src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" 
                                                                style="vertical-align: text-bottom" width="250"></iframe>
                                                    </td></tr></table></td></tr>
            <tr><th>描述</th><td>
                <input id="inputremarks" runat="server" class="length13"  type="text" />
                </td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
</div>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <asp:HiddenField ID="hf_bid" runat="server" />
    <asp:HiddenField ID="HF_LogoImage" runat="server" />
</form>
    <script src="../js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js "type="text/javascript"></script>
    <script src="../include/js/My97DatePicker/WdatePicker.js "type="text/javascript"></script>
</body>
</html>
