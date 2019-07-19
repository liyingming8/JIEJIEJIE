<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_orderAddEdit.aspx.cs" Inherits="Admin_tj_js_orderAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_js_order</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table class="gridtable">
            <tr><th>id</th><td><input id="inputid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>userid</th><td><input id="inputuserid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>ordernumber</th><td><input id="inputordernumber" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>goodsid</th><td><input id="inputgoodsid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>goodsnum</th><td><input id="inputgoodsnum" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>goodstype</th><td><input id="inputgoodstype" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>shouldpaymoney</th><td><input id="inputshouldpaymoney" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>yunfei</th><td><input id="inputyunfei" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>orderdate</th><td><input id="inputorderdate" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliveryname</th><td><input id="inputdeliveryname" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliveryphone</th><td><input id="inputdeliveryphone" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliveryaddress</th><td><input id="inputdeliveryaddress" runat="server" type="text"  /></td><td></td></tr>
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
