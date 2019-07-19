<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_XF_ERP_OrderDetailAddEdit.aspx.cs" Inherits="Admin_TJ_XF_ERP_OrderDetailAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_XF_ERP_OrderDetail</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">TJ_XF_ERP_OrderDetail</div>
         <table class="gridtable">
            <tr><th>id</th><td><input id="inputid" runat="server" type="text" maxlength="2" /></td><td></td></tr>
            <tr><th>odid</th><td><input id="inputodid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>materialcode</th><td><input id="inputmaterialcode" runat="server" type="text" maxlength="50" /></td><td></td></tr>
            <tr><th>prodname</th><td><input id="inputprodname" runat="server" type="text" maxlength="50" /></td><td></td></tr>
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
