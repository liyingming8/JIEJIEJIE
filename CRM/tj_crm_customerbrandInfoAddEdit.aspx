<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerbrandinfoAddEdit.aspx.cs" Inherits="CRM_tj_crm_customerbrandinfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_customerbrandinfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　
</head>
<body>
<form id="form1" runat="server">
<div class="editpageback">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
         <table class="gridtable">
            <tr><th>序号</th><td><input id="inputid" runat="server" type="text"  /></td></tr>
            <tr><th>customerid</th><td><input id="inputcustomerid" runat="server" type="text"  /></td></tr>
            <tr><th>brandid</th><td><input id="inputbrandid" runat="server" type="text"  /></td></tr>
            <tr><th>startdate</th><td><input id="inputstartdate" runat="server" type="text"  /></td></tr>
            <tr><th>enddate</th><td><input id="inputenddate" runat="server" type="text"  /></td></tr>
            <tr><th>ispermit</th><td><input id="inputispermit" runat="server" type="text"  /></td></tr>
            <tr><th>compid</th><td><input id="inputcompid" runat="server" type="text"  /></td></tr>
            <tr><th>permituserid</th><td><input id="inputpermituserid" runat="server" type="text"  /></td></tr>
            <tr><th>agentlevel</th><td><input id="inputagentlevel" runat="server" type="text"  /></td></tr>
            <tr><th>natureagentleve</th><td><input id="inputnatureagentleve" runat="server" type="text"  /></td></tr>
            <tr><th>sharetemplateid</th><td><input id="inputsharetemplateid" runat="server" type="text"  /></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text"  /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
</div>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     
</form>
    <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
    　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
