<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerorderinfoAddEdit.aspx.cs" Inherits="CRM_tj_crm_customerorderinfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_customerorderinfo</title>
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
            <tr><th>brandid</th><td><input id="inputbrandid" runat="server" type="text"  /></td></tr>
            <tr><th>ordercustomerid</th><td><input id="inputordercustomerid" runat="server" type="text"  /></td></tr>
            <tr><th>ordernumber</th><td><input id="inputordernumber" runat="server" type="text"  /></td></tr>
            <tr><th>unitname</th><td><input id="inputunitname" runat="server" type="text"  /></td></tr>
            <tr><th>orderdatetime</th><td><input id="inputorderdatetime" runat="server" type="text"  /></td></tr>
            <tr><th>parentcustomerid</th><td><input id="inputparentcustomerid" runat="server" type="text"  /></td></tr>
            <tr><th>totalprice</th><td><input id="inputtotalprice" runat="server" type="text"  /></td></tr>
            <tr><th>ispay</th><td><input id="inputispay" runat="server" type="text"  /></td></tr>
            <tr><th>paydatetime</th><td><input id="inputpaydatetime" runat="server" type="text"  /></td></tr>
            <tr><th>paynumber</th><td><input id="inputpaynumber" runat="server" type="text"  /></td></tr>
            <tr><th>paymethod</th><td><input id="inputpaymethod" runat="server" type="text"  /></td></tr>
            <tr><th>shouhuoren</th><td><input id="inputshouhuoren" runat="server" type="text"  /></td></tr>
            <tr><th>shouhuophonenumber</th><td><input id="inputshouhuophonenumber" runat="server" type="text"  /></td></tr>
            <tr><th>shouhuodizhi</th><td><input id="inputshouhuodizhi" runat="server" type="text"  /></td></tr>
            <tr><th>isfahuo</th><td><input id="inputisfahuo" runat="server" type="text"  /></td></tr>
            <tr><th>kuaididanhao</th><td><input id="inputkuaididanhao" runat="server" type="text"  /></td></tr>
            <tr><th>kuaidicompany</th><td><input id="inputkuaidicompany" runat="server" type="text"  /></td></tr>
            <tr><th>kuaidiquerylink</th><td><input id="inputkuaidiquerylink" runat="server" type="text"  /></td></tr>
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
