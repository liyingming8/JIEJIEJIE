<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_userAddEdit.aspx.cs" Inherits="Admin_tj_js_userAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_js_user</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table class="gridtable">
            <tr><th>userid</th><td><input id="inputuserid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>name</th><td><input id="inputname" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>nickname</th><td><input id="inputnickname" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>password</th><td><input id="inputpassword" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>sex</th><td><input id="inputsex" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>cellphone</th><td><input id="inputcellphone" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>telphone</th><td><input id="inputtelphone" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>address</th><td><input id="inputaddress" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>city</th><td><input id="inputcity" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>idcard</th><td><input id="inputidcard" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>postcode</th><td><input id="inputpostcode" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>headpic</th><td><input id="inputheadpic" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>qianming</th><td><input id="inputqianming" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliveryname</th><td><input id="inputdeliveryname" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliveryphone</th><td><input id="inputdeliveryphone" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliveryaddress</th><td><input id="inputdeliveryaddress" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>deliverycity</th><td><input id="inputdeliverycity" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>likes</th><td><input id="inputlikes" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>compid</th><td><input id="inputcompid" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>registerdate</th><td><input id="inputregisterdate" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>wxgongzhonghao</th><td><input id="inputwxgongzhonghao" runat="server" type="text"  /></td><td></td></tr>
            <tr><th>wxopenid</th><td><input id="inputwxopenid" runat="server" type="text"  /></td><td></td></tr>
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
