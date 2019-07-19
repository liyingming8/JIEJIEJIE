<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_App_LabelModel_InfoAddEdit.aspx.cs" Inherits="Admin_TJ_App_LabelModel_InfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_App_LabelModel_Info</title>
    <link href="../include/easyui.css" rel="stylesheet" />
　　<link href="../include/windows.css" rel="stylesheet" /> 
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">套标信息编辑</div>
         <table class="gridtable">
            <tr><th>所属公司</th><td><input id="inputcompid" runat="server" type="text" class="p80" maxlength="4" /></td></tr>
            <tr><th>套标关系</th><td>
                <input id="TextB" runat="server" placeholder="大" class="p10" type="text" />
                <input id="TextM" runat="server" placeholder="中" value="0" class="p10" type="text" />
                <input id="TextS" runat="server" placeholder="小" class="p10" type="text" />
                </td></tr>
            <tr><th>备注</th><td><input id="inputremarks" runat="server" type="text" class="p80" maxlength="25" /></td></tr>
             <tr>
                 <th>最后更新人</th>
                 <td>
                     <asp:Label ID="Label_userid" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <th>最后更新时间</th>
                 <td>
                     <asp:Label ID="Label_updatetm" runat="server"></asp:Label>
                 </td>
             </tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" /> 
     <asp:HiddenField ID="HF_CompID" runat="server" /> 
</form>
</body>
</html>
