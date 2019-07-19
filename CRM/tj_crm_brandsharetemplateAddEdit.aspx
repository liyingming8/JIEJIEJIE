<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_brandsharetemplateAddEdit.aspx.cs" Inherits="CRM_tj_crm_brandsharetemplateAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_brandsharetemplate</title>
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
            <tr><th>序号</th><td><input id="inputid" runat="server" type="text" maxlength="2" /></td></tr>
            <tr><th>品牌</th><td><input id="inputbrandid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
            <tr><th>title</th><td><input id="inputtitle" runat="server" type="text" maxlength="25" /></td></tr>
            <tr><th>innerhtml</th><td><input id="inputinnerhtml" runat="server" type="text" maxlength="1000" /></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
</div>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
