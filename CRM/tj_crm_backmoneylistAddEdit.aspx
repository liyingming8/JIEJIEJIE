<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_backmoneylistAddEdit.aspx.cs" Inherits="CRM_tj_crm_backmoneylistAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_backmoneylist</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">tj_crm_backmoneylist</div>
         <table class="gridtable">
            <tr><th>id</th><td><input id="inputid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>fromuid</th><td><input id="inputfromuid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>touid</th><td><input id="inputtouid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>compid</th><td><input id="inputcompid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>orderid</th><td><input id="inputorderid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>rewardtype</th><td><input id="inputrewardtype" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>moneynum</th><td><input id="inputmoneynum" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>bdate</th><td><input id="inputbdate" runat="server" type="text"  /></td><td></td></tr>
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
