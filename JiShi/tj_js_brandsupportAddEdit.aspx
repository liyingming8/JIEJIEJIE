<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_brandsupportAddEdit.aspx.cs" Inherits="Admin_tj_js_brandsupportAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_js_brandsupport</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table class="gridtable">
            <tr><th>jishiid</th><td><input id="inputjishiid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>compid</th><td><input id="inputcompid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>joindate</th><td><input id="inputjoindate" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>permit</th><td><input id="inputpermit" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>comfirmuserid</th><td><input id="inputcomfirmuserid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>confirmdate</th><td><input id="inputconfirmdate" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>refusereson</th><td><input id="inputrefusereson" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>supporname</th><td><input id="inputsupporname" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>jishilogourl</th><td><input id="inputjishilogourl" runat="server" type="text"  /></td><td></td></tr>
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
