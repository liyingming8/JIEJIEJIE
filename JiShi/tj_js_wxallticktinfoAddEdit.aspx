<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_wxallticktinfoAddEdit.aspx.cs" Inherits="Admin_tj_js_wxallticktinfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_js_wxallticktinfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table class="gridtable">
            <tr><th>id</th><td><input id="inputid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>wxappid</th><td><input id="inputwxappid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>wxtickt</th><td><input id="inputwxtickt" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>tickttype</th><td><input id="inputtickttype" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>ticket_expires</th><td><input id="inputticket_expires" runat="server" type="text"  /></td><td></td></tr>
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
