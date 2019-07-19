<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_rewardprogramAddEdit.aspx.cs" Inherits="CRM_tj_crm_rewardprogramAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_rewardprogram</title>
　　<link href="../include/windows.css" rel="stylesheet" /> 
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="editpageback">
    <div class="toptitle">层级返利</div>
         <table class="gridtable">  
            <tr><th>子级</th><td>
                <asp:DropDownList ID="ddl_childgid" DataValueField="id" DataTextField="gradename" runat="server"></asp:DropDownList></td><td></td></tr>  
             <tr><th>父级</th><td>
                <asp:DropDownList ID="ddl_parentgid" DataValueField="id" DataTextField="gradename" runat="server"></asp:DropDownList></td><td></td></tr>
            <tr><th>奖励类型</th><td>
                <asp:DropDownList ID="ddl_rewardtype" DataTextField="name" DataValueField="id" runat="server"></asp:DropDownList></td><td></td></tr>
            <tr><th>金额</th><td>￥<input id="inputrewardnum" runat="server" type="text"  /></td><td></td></tr>
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
