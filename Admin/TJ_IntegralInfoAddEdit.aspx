<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_IntegralInfoAddEdit.aspx.cs" Inherits="Admin_TJ_IntegralInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_IntegralInfo</title>
　　<link href="../include/windows.css" rel="stylesheet" /> 
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">积分规则编辑</div>
         <table class="gridtable">
            <tr><th>积分条件</th><td>
                <asp:DropDownList ID="DDL_IntegralItemID" runat="server" DataTextField="ItemName" DataValueField="ITITID">
                </asp:DropDownList>
                </td></tr>
            <tr><th>分值</th><td><input id="inputIntegralReword" runat="server" class="p20" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20" /></td></tr>
            <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="20" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
    <asp:HiddenField runat="server" ID="HF_ITGRID"/>
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
