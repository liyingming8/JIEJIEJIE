<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_PrizesAddEdit.aspx.cs" Inherits="Admin_TJ_Activity_PrizesAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_Activity_Prizes</title> 
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">奖项信息编辑</div>
         <table class="gridtable"> 
            <tr><th>奖项类别</th><td>
                <asp:DropDownList ID="ddl_awtype" runat="server" AutoPostBack="True" DataTextField="awardtype" DataValueField="id" OnSelectedIndexChanged="ddl_awtype_SelectedIndexChanged">
                </asp:DropDownList>
                </td><td></td></tr>
            <tr id="jiangpinrow" runat="server"><th>奖品</th><td>
                <asp:RadioButtonList ID="rbl_awid" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" DataTextField="AwardThing" DataValueField="AWID" RepeatColumns="4">
                </asp:RadioButtonList>
                </td><td></td></tr>
            <tr id="wxhbrow" runat="server"><th>红包</th><td>￥<input id="inputwxhbvalue" runat="server" type="text" class="p10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td><td></td></tr>
             <tr id="jifenrow" runat="server"><th>积分</th><td><input id="inputjifen" runat="server" type="text" class="p10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td><td></td></tr>
            <tr><th>比率</th><td><input id="inputpercentvl" runat="server" class="p10" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
            <tr><th>备注</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <input id="inputacid" runat="server" type="hidden"/><asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
    <asp:HiddenField ID="hf_acid" runat="server" />
    <asp:HiddenField ID="hf_acid0" runat="server" />
</form>
</body>
</html>
