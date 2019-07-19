<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CustomerAdviceAddEdit.aspx.cs" Inherits="Admin_TJ_CustomerAdviceAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_CustomerAdvice</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">TJ_CustomerAdvice</div>
         <table class="gridtable">
            <tr><th>id</th><td><input id="inputid" runat="server" type="text" maxlength="2" /></td><td></td></tr>
            <tr><th>advtitle</th><td><input id="inputadvtitle" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>advcontent</th><td><input id="inputadvcontent" runat="server" type="text" maxlength="250" /></td><td></td></tr>
            <tr><th>customerphone</th><td><input id="inputcustomerphone" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>customername</th><td><input id="inputcustomername" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>adtime</th><td><input id="inputadtime" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" /></td><td></td></tr>
            <tr><th>userid</th><td><input id="inputuserid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
