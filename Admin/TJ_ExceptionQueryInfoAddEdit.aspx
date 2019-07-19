<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ExceptionQueryInfoAddEdit.aspx.cs" Inherits="Admin_TJ_ExceptionQueryInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_ExceptionQueryInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">异常查询处理</div>
         <table class="gridtable"> 
            <tr><th>标签号码</th><td><label id="inputlbcode" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>查询时间</th><td><label id="inputquerytm" runat="server" type="datetime"  class="length3"  maxlength="16" /></td><td></td></tr>
            <tr><th>原因</th><td><label id="inputextype" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>回复方式</th><td><label id="inputrestype" runat="server" type="text" /></td><td></td></tr>
         <%--   <tr><th>userid</th><td><input id="inputuserid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>--%>
            <tr><th>是否解决</th><td><asp:CheckBox id="checkissolved" runat="server" type="text" maxlength="0" /></td><td></td></tr>
            <tr><th>平台</th><td><label id="inputplatform" runat="server" type="text" maxlength="5" /></td><td></td></tr>
            <tr><th>查询地址</th><td><label id="inputqueryaddress" runat="server" type="text" maxlength="50" /></td><td></td></tr>
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
