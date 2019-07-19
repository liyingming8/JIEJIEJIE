<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_APP_EquipAuthorAddEdit.aspx.cs" Inherits="Admin_TJ_APP_EquipAuthorAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_APP_EquipAuthor</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">设备授权</div>
         <table class="gridtable">
            <tr><th>单位</th><td>
                <asp:Label ID="Label_compid" runat="server"></asp:Label>
                </td></tr>
            <tr><th>经销商</th><td>
                <asp:Label ID="Labelagentid" runat="server"></asp:Label>
                </td></tr>
            <tr><th>设备编码</th><td>
                <asp:Label ID="Label_equipcode" runat="server"></asp:Label>
                </td></tr>
            <tr><th>注册日期</th><td>
                <asp:Label ID="Label_registerdate" runat="server"></asp:Label>
                </td></tr>
            <tr><th>备注</th><td><input id="inputrmarks" runat="server" class="p80" type="text" maxlength="25" /></td></tr>
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
