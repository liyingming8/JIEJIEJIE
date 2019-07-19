<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_PriceDiscountModeAddEdit.aspx.cs" Inherits="Admin_TJ_SWM_PriceDiscountModeAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SWM_PriceDiscountMode</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">三维码折扣模式</div>
         <table class="gridtable">
            <tr><th>高于数量</th><td>
                <input id="inputshuliang" runat="server" type="text" onafterpaste="if(isNaN(value))execCommand('undo')" onkeyup="if(isNaN(value))execCommand('undo')" />
                </td></tr>
            <tr><th>折扣</th><td><input id="inputdiscount" runat="server" class="p10" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" />%</td></tr>
            <tr><th>介绍</th><td><input id="inputjieshao" runat="server" type="text" class="p80"  maxlength="25" /></td></tr>
            <tr><th>备注</th><td><input id="inputremarks" runat="server" type="text" class="p80" maxlength="25" /></td></tr>
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
