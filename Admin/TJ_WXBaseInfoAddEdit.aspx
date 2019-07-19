<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_WXBaseInfoAddEdit.aspx.cs" Inherits="Admin_TJ_WXBaseInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_WXBaseInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" /> 
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">微信相关信息编辑</div>
         <table class="gridtable"> 
            <tr><th>APPID</th><td><input id="inputWX_Appid" runat="server" type="text" class="p60" maxlength="25" /></td></tr>
            <tr><th>AppSecret</th><td><input id="inputWX_Appsecret" runat="server"  class="p60" type="text" maxlength="25" /></td></tr>
            <tr><th>商户号</th><td><input id="inputWX_Payid" runat="server" type="text"  class="p60" maxlength="10" /></td></tr>
            <tr><th>支付秘钥</th><td><input id="inputWX_Paykey" runat="server" type="text"  class="p80" maxlength="25" /></td></tr>
            <tr><th>支付证书</th><td><asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileupload" />
                <asp:Button ID="Button_upload" runat="server" OnClick="Button_upload_Click" Text="上传" />
                </td></tr>   
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <asp:HiddenField ID="hf_certpath" runat="server" />
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
