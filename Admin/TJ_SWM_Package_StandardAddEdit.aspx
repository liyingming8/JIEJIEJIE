<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_Package_StandardAddEdit.aspx.cs" Inherits="Admin_TJ_SWM_Package_StandardAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SWM_Package_Standard</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="editpageback">
    <div class="toptitle">三维码包装规格</div>
         <table class="gridtable">
            <tr><th>单位</th><td>
                <input id="inputunitnm" runat="server" type="text" class="p20" maxlength="5" />
                （盘、卷）</td></tr>
            <tr><th>数量</th><td><input id="inputquantity" runat="server" class="p20" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
            <tr><th>单枚价格</th><td>￥<input id="inputsingleprice" runat="server" class="p20" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" />元</td></tr>
            <tr><th>合计</th><td>￥<input id="inputtotalprice" runat="server" type="text" class="p20" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" />元</td></tr>
             <tr>
                 <th>备注</th>
                 <td>
                     <input id="inputremark" runat="server" type="text" class="p80" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" />
                 </td>
             </tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
            </div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
