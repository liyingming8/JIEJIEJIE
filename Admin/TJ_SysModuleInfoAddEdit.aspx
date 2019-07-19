<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SysModuleInfoAddEdit.aspx.cs" Inherits="Admin_TJ_SysModuleInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SysModuleInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">展示模块信息</div>
         <table class="gridtable">
           
            <tr><th>展示</th><td><input id="inputModuleName" class="length8" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>顺序</th><td><input id="inputShowOrder" class="length1" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>链接</th><td><input id="inputShowConent" runat="server" type="text" maxlength="50" class="length13" /></td><td></td></tr>
            <tr><th>显示</th><td><asp:CheckBox runat="server" ID="ckb_IsShow"/></td><td></td></tr>
            <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" class="length10" /></td><td></td></tr>
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
