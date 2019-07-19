<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_WXSQInfoAddEdit.aspx.cs" Inherits="Admin_TJ_WXSQInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_WXSQInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" /> 
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">微信授权信息编辑</div>
         <table class="gridtable"> 
            <tr><th>单位</th><td><input id="inputCompID" runat="server" type="text"  onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="p80" readonly="readonly" /></td></tr>
            <tr><th>子级</th><td><input id="inputSid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="p20" /></td></tr>
            <tr><th>微信APPID</th><td><input id="inputWX_Appid" runat="server" type="text" maxlength="25" class="p60" /></td></tr>
            <tr><th>微信APPSecret</th><td><input id="inputWX_Appsecret" runat="server" type="text" maxlength="25" class="p60" /></td></tr>
            <tr><th>跳转路径</th><td><input id="inputWX_Redirect_url" runat="server" type="text" maxlength="50" class="p80" /></td></tr>
            <tr><th>处理路径</th><td><input id="inputWX_CL_url" runat="server" type="text" maxlength="50" class="p80" /></td></tr>
            <tr><th>授权模式</th><td>
                <asp:RadioButtonList ID="RBL_Scope" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem>snsapi_base</asp:ListItem>
                    <asp:ListItem>snsapi_userinfo</asp:ListItem>
                </asp:RadioButtonList>
                </td></tr>
            <tr><th>是否关注</th><td>
                <asp:CheckBox ID="CheckBox_GZ" runat="server" /></td></tr>
            <tr><th>路径一致</th><td>
                <asp:CheckBox ID="CheckBoxOwnUrl" runat="server" /></td></tr>
             <tr>
                 <th>备注</th>
                 <td>
                     <input id="input_remarks" runat="server" class="p80" type="text" />
                 </td>  
             </tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <asp:HiddenField runat="server" ID="hf_compid"/>  
</form> 
    <script src="../include/js/jquery-2.1.1.min.js"></script>
     <script src="../include/js/UploadImage.js"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</body>
</html>
