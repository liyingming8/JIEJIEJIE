<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_YiChangShenBaoInfoAddEdit.aspx.cs" Inherits="Admin_TJ_YiChangShenBaoInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_YiChangShenBaoInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">异常申报详细</div>
         <table class="gridtable">    
            <tr><th>申报人</th><td><asp:Label runat="server" ID="lab_sbr"></asp:Label></td></tr>
            <tr><th>标签号码</th><td><asp:Label runat="server" ID="lab_labcode"></asp:Label></td></tr>
            <tr><th>图像</th><td><img runat="server" id="yichangimg" src="" width="200"/></td></tr>
            <tr><th>产品</th><td>
                <asp:DropDownList ID="ddl_pid" runat="server" DataTextField="pnm" DataValueField="pid">
                </asp:DropDownList>
                </td></tr>
             <tr><th>是否属实</th><td>
                 <asp:CheckBox ID="ckb_iscorrect" runat="server" Text="是" />
                 </td></tr>
            <tr><th>上传时间</th><td><asp:Label id="lab_sbtm" runat="server" type="text" maxlength="25" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <asp:HiddenField ID="HF_AgentID" runat="server" />
    <asp:HiddenField ID="HF_TerminalID" runat="server" />
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
