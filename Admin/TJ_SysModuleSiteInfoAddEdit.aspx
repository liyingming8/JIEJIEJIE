<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SysModuleSiteInfoAddEdit.aspx.cs" Inherits="Admin_TJ_SysModuleSiteInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SysModuleSiteInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">系统模块流程配置</div>
         <table class="gridtable">  
            <tr><th>系统目录</th><td><asp:DropDownList runat="server" ID="ddl_siteid" AutoPostBack="True" OnSelectedIndexChanged="ddl_siteid_SelectedIndexChanged"/></td><td></td></tr>
            <tr><th>目录名称</th><td><input id="inputSiteName" runat="server" type="text" maxlength="25" /></td><td></td></tr> 
            <tr><th>链接</th><td><input id="inputLinkURL" runat="server" type="text" class="length13" maxlength="50" /></td><td></td></tr> 
            <tr><th>显示顺序</th><td><input id="inputShowOrder" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="length1" /></td><td></td></tr>
            <tr><th>提示内容</th><td><input id="inputShowContent" runat="server" type="text" class="length13" maxlength="50" /></td><td></td></tr> 
            <tr><th>结束</th><td><asp:CheckBox runat="server" ID="ck_IsEnd"/></td><td></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
    <input type="hidden" id="input_smid" runat="server"/>
</form>
</body>
</html>
