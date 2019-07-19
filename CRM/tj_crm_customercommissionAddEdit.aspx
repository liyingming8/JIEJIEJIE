<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customercommissionAddEdit.aspx.cs" Inherits="CRM_tj_crm_customercommissionAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_customercommission</title>
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
            <tr><th>id</th><td><input id="inputid" runat="server" type="text" maxlength="2" /></td><td></td></tr>
            <tr><th>orderid</th><td><input id="inputorderid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>ordercustomerid</th><td><input id="inputordercustomerid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>parentcustomerid</th><td><input id="inputparentcustomerid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>orderdate</th><td><input id="inputorderdate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" /></td><td></td></tr>
            <tr><th>ordertotalprice</th><td><input id="inputordertotalprice" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td><td></td></tr>
            <tr><th>returnvalue</th><td><input id="inputreturnvalue" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td><td></td></tr>
            <tr><th>compid</th><td><input id="inputcompid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
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
