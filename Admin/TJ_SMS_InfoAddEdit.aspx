<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SMS_InfoAddEdit.aspx.cs" Inherits="Admin_TJ_SMS_InfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SMS_Info</title>
　　<link href="../include/windows.css" rel="stylesheet" /> 
       <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">短信发送提醒</div>
         <table class="gridtable"> 
            <tr><th>所属单位</th><td><div style="display:block;"><input id="input_compid" runat="server" class="length10" />
                <asp:DropDownList ID="ddl_departid" DataTextField="department" DataValueField="id" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                </asp:DropDownList></div>
                </td></tr>
             <tr>
                 <th>姓名</th>
                 <td>
                     <input id="inputuname" runat="server" type="text" maxlength="5" class="length5" />
                 </td>
             </tr>
            <tr><th>手机号码</th><td><input id="inputphonenumber" runat="server" type="text" maxlength="15" class="length8" /></td></tr>
            <tr><th>提醒环节</th><td><asp:DropDownList ID="ddl_alert_case" runat="server" DataTextField="CName" DataValueField="CID"></asp:DropDownList></td></tr> 
            <tr><th>备注</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" class="length10" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <input  id="hf_compid" runat="server" type="hidden"/>
     <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
