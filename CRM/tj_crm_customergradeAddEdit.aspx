<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customergradeAddEdit.aspx.cs" Inherits="crm_tj_crm_customergradeAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_customergrade</title>
　　<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
　　
</head>
<body>
<form id="form1" runat="server">
<div class="editpageback">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
         <table class="gridtable">
            <tr><th>级别名称</th><td><input id="inputgradename" runat="server" type="text"  /></td></tr>
             <tr>
                 <th>排列顺序</th>
                 <td>
                     <input id="inputgradeorder" type="text" runat="server"  />
                 </td> 
             </tr>
             <tr>
                 <th>上级审核</th>
                 <td>
                     <asp:CheckBox ID="ckb_firstcheck" runat="server" />
                 </td>
             </tr>
             <tr>
                 <th>营业执照</th>
                 <td>
                     <asp:CheckBox ID="ckb_blicenses" runat="server" />
                 </td>
             </tr>
             <tr>
                 <th>备&nbsp; 注</th>
                 <td>
                     <input id="inputremarks" runat="server" type="text"  />
                 </td>
             </tr>
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
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
