<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_LogisticsCompanyAddEdit.aspx.cs" Inherits="Admin_TJ_LogisticsCompanyAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_LogisticsCompany</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">物流公司信息编辑</div>
         <table class="gridtable">
            <tr><th>公司名称</th><td><input id="inputlogisticcompany" runat="server" class="p80" type="text" maxlength="25" /></td></tr>
            <tr><th>编码</th><td><input id="inputcodestr" runat="server" type="text" class="p20" maxlength="10" /></td></tr>
            <tr><th>查询接口</th><td><input id="inputqueryinterfacestr" class="p80" runat="server" type="text" maxlength="150" /></td></tr>
            <tr><th>备注</th><td><input id="inputremarks" runat="server" class="p80" type="text" maxlength="25" /></td></tr>
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
