<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_system_configAddEdit.aspx.cs" Inherits="Admin_tj_crm_system_configAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_system_config</title>
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
            <tr><th>id</th><td><input id="inputid" runat="server" type="text"  /></td></tr>
            <tr><th>compid</th><td><input id="inputcompid" runat="server" type="text"  /></td></tr>
            <tr><th>logourl</th><td><input id="inputlogourl" runat="server" type="text"  /></td></tr>
            <tr><th>hometoppicurl</th><td><input id="inputhometoppicurl" runat="server" type="text"  /></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text"  /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
</div>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
</form>
    　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</body>
</html>
