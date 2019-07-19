<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_User_SystemAddEdit.aspx.cs" Inherits="Admin_TJ_User_SystemAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_User_System</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">TJ_User_System</div>
         <table class="gridtable">
            <tr><th>UserID</th><td><input id="inputUserID" runat="server" type="text" maxlength="2" /></td><td></td></tr>
            <tr><th>ParentID</th><td><input id="inputParentID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>CompID</th><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>RID</th><td><input id="inputRID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>IdentityCode</th><td><input id="inputIdentityCode" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>LoginName</th><td><input id="inputLoginName" runat="server" type="text" maxlength="50" /></td><td></td></tr>
            <tr><th>PassWords</th><td><input id="inputPassWords" runat="server" type="text" maxlength="40" /></td><td></td></tr>
            <tr><th>NickName</th><td><input id="inputNickName" runat="server" type="text" maxlength="20" /></td><td></td></tr>
            <tr><th>SexInfo</th><td><input id="inputSexInfo" runat="server" type="text" maxlength="5" /></td><td></td></tr>
            <tr><th>RegisterDate</th><td><input id="inputRegisterDate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" /></td><td></td></tr>
            <tr><th>IsActived</th><td><input id="inputIsActived" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>FromCityID</th><td><input id="inputFromCityID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>AddressInfo</th><td><input id="inputAddressInfo" runat="server" type="text" maxlength="275" /></td><td></td></tr>
            <tr><th>PostCode</th><td><input id="inputPostCode" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>SystemPermission</th><td><input id="inputSystemPermission" runat="server" type="text" maxlength="1073741823" /></td><td></td></tr>
            <tr><th>IntegralValue</th><td><input id="inputIntegralValue" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20" /></td><td></td></tr>
            <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXGongZhongHao</th><td><input id="inputWXGongZhongHao" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXDengLuYouXiang</th><td><input id="inputWXDengLuYouXiang" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>WXYuanShiID</th><td><input id="inputWXYuanShiID" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>WXNumber</th><td><input id="inputWXNumber" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>WXLeiXing</th><td><input id="inputWXLeiXing" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXRenZhengQingKuang</th><td><input id="inputWXRenZhengQingKuang" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXToken</th><td><input id="inputWXToken" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXSignature</th><td><input id="inputWXSignature" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXTimesStamp</th><td><input id="inputWXTimesStamp" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXOnece</th><td><input id="inputWXOnece" runat="server" type="text" maxlength="25" /></td><td></td></tr>
            <tr><th>WXEchoStrnig</th><td><input id="inputWXEchoStrnig" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>WXIsYanZheng</th><td><input id="inputWXIsYanZheng" runat="server" type="text" maxlength="0" /></td><td></td></tr>
            <tr><th>HeaderImageUrl</th><td><input id="inputHeaderImageUrl" runat="server" type="text" maxlength="250" /></td><td></td></tr>
            <tr><th>AuthorDiscount</th><td><input id="inputAuthorDiscount" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>MobileNumber</th><td><input id="inputMobileNumber" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>WX_Province</th><td><input id="inputWX_Province" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>WX_City</th><td><input id="inputWX_City" runat="server" type="text" maxlength="100" /></td><td></td></tr>
            <tr><th>reg_date</th><td><input id="inputreg_date" runat="server" type="text" maxlength="10" /></td><td></td></tr>
            <tr><th>reg_year</th><td><input id="inputreg_year" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>reg_month</th><td><input id="inputreg_month" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
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
