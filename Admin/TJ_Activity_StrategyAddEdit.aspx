<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_StrategyAddEdit.aspx.cs" Inherits="Admin_TJ_Activity_StrategyAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_Activity_Strategy</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">奖励策略编辑</div>
         <table class="gridtable"> 
            <tr><th>策略命名</th><td><input id="inputstrategyname" runat="server" class="p80" type="text" maxlength="25" /></td></tr>
            <tr><th>模式</th><td>
                <asp:RadioButtonList ID="RBL_Mode" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBL_Mode_SelectedIndexChanged">
                    <asp:ListItem Value="1">总体中奖率</asp:ListItem>
                    <asp:ListItem Value="2">包装数量</asp:ListItem>
                    <asp:ListItem Value="3">消费者扫码</asp:ListItem>
                </asp:RadioButtonList>
                </td></tr>
             <tr runat="server" id="rowzongtizhongjiang">
                 <th>中奖率</th>
                 <td>
                     <input id="inputtotalwinrate" class="p10" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /><span class="discriptionspan">%</span>
                 </td>
             </tr> 
            <tr runat="server" id="rowtoself"><th>次数</th><td><input id="inputmaxtimeself" class="p10" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" value="3" /><span class="discriptionspan">扫码多少次后会中奖</span></td></tr>
            <tr runat="server" id="rowtopackage"><th>数量</th><td><input id="inputpackagenum" class="p10" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /><span class="discriptionspan">每件包装内的获奖数量</span></td></tr>
            <tr><th>限次</th><td><input id="inputtimelimit" class="p10" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" value="1" /><span class="discriptionspan">每个码参与奖励的次数</span></td></tr>
            <tr><th>有效</th><td>
                <asp:CheckBox ID="CKB_isactive" Checked="True" runat="server" />
                </td></tr>
            <tr><th>备注</th><td><input id="inputremarks" class="p80" runat="server" type="text" maxlength="25" /></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" />
          <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
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
