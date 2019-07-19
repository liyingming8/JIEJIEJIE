<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_swm_feedback_userinfoAddEdit.aspx.cs" Inherits="Admin_tj_swm_feedback_userinfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_swm_feedback_userinfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">人员信息编辑</div>
         <table class="gridtable">
            <tr><th>登录名</th><td colspan="2"><input id="inputloginname" runat="server" class="length4" type="text" maxlength="25" /></td></tr>
            <tr><th>密码</th><td><input id="inputupsw" runat="server" class="length4" type="password" maxlength="25" /></td><td>
                <asp:CheckBox ID="ckb_default_psw" runat="server" Text="默认：123456" />
                </td></tr> 
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
