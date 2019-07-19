<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Comp_RolesAddEdit.aspx.cs" Inherits="Admin_TJ_Comp_RolesAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_Comp_Roles</title>
　　<link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">公司系统功能授权</div>
         <table class="gridtable">
            <tr><th>公司</th><td>
                <asp:Label id="inputcompid" runat="server"/>
                </td></tr>
            <tr><th>功能包</th><td><asp:Label id="inputridstring" runat="server"/>
                <asp:RadioButtonList ID="rdb_list_rolepackage" runat="server" DataTextField="rpackage" DataValueField="id" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:RadioButtonList>
                </td></tr> 
            <tr><th>激活</th><td><asp:CheckBox runat="server" ID="ckb_isactive"/>  </td></tr>
            <tr><th>备注</th><td><input id="inputremarks" runat="server" type="text" class="p80" maxlength="25" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <asp:HiddenField ID="HF_CompID" runat="server" />
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
