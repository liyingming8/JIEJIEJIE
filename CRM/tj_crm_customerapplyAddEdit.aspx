<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerapplyAddEdit.aspx.cs" Inherits="CRM_tj_crm_customerapplyAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_customerapply</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　
</head>
<body>
<form id="form1" runat="server">
<div class="editpageback">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
         <table class="gridtable">
            <tr><th>代理品牌</th><td><input id="inputbrandid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
            <tr><th>objcompid</th><td><input id="inputobjcompid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
            <tr><th>申请日期</th><td><input id="inputapplydate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" maxlength="16" /></td></tr>
            <tr><th>是否允许</th><td><input id="inputispermit" runat="server" type="text" maxlength="0" /></td></tr>
            <tr><th>拒绝原因</th><td><input id="inputrefusereson" runat="server" type="text" maxlength="100" /></td></tr>
            <tr><th>处理时间</th><td><input id="inputhandledate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})"  maxlength="16" /></td></tr>
            <tr><th>处理人</th><td><input id="inputhandleuserid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
            <tr><th>备 注</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" />
          <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
      </div>
  </ContentTemplate>
</asp:UpdatePanel>
</div>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
    
</form>
    <script src="../include/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
     <script src="../js/jquery-1.7.1.js" type="text/javascript"></script>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
