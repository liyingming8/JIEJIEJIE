<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_OrderInfoAddEdit.aspx.cs" Inherits="Admin_TJ_SWM_OrderInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SWM_OrderInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">TJ_SWM_OrderInfo</div>
         <table class="gridtable">
            <tr><th>id</th><td><input id="inputid" runat="server" type="text" maxlength="2" /></td><td></td></tr>
            <tr><th>ordernumber</th><td><input id="inputordernumber" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>psid</th><td><input id="inputpsid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>orderquantity</th><td><input id="inputorderquantity" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>orderremark</th><td><input id="inputorderremark" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>totalprice</th><td><input id="inputtotalprice" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td><td></td></tr>
            <tr><th>ordercompid</th><td><input id="inputordercompid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>orderuserid</th><td><input id="inputorderuserid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>ordertm</th><td><input id="inputordertm" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" /></td><td></td></tr>
            <tr><th>payconfirm</th><td><input id="inputpayconfirm" runat="server" type="text" maxlength="0" /></td><td></td></tr>
            <tr><th>confirmuserid</th><td><input id="inputconfirmuserid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>isfahuo</th><td><input id="inputisfahuo" runat="server" type="text" maxlength="0" /></td><td></td></tr>
            <tr><th>fahuouserid</th><td><input id="inputfahuouserid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>isactive</th><td><input id="inputisactive" runat="server" type="text" maxlength="0" /></td><td></td></tr>
            <tr><th>ordercompnm</th><td><input id="inputordercompnm" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>orderusernm</th><td><input id="inputorderusernm" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>orderphonenm</th><td><input id="inputorderphonenm" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>paytype</th><td><input id="inputpaytype" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>ispay</th><td><input id="inputispay" runat="server" type="text" maxlength="0" /></td><td></td></tr>
            <tr><th>remarks</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
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
