<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ActivityAddEdit.aspx.cs" Inherits="Admin_TJ_ActivityAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_Activity</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">活动编辑</div>
         <table class="gridtable"> 
            <tr><th>活动名称</th><td colspan="3"><input id="inputAName" runat="server" type="text" class="p80" maxlength="25" /></td>
             </tr>
            <tr><th>起始日期</th><td colspan="3">
                <input id="inputSTM" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" /><span class="discriptionspan">至</span>
                <input id="inputETM" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" /></td>
             </tr>  
             <tr>
                 <th>活动对象</th>
                 <td colspan="3">
                     <asp:RadioButtonList ID="rbl_faceto" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                         <asp:ListItem Selected="True" Value="1">消费者</asp:ListItem>
                         <asp:ListItem Value="2">经销商</asp:ListItem>
                         <asp:ListItem Value="3">终端店</asp:ListItem>
                     </asp:RadioButtonList>
                 </td>
             </tr> 
             <tr>
                 <th>码类型</th>
                 <td colspan="3">
                     <asp:RadioButtonList ID="rbl_codetype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                         <asp:ListItem Value="3">活动码</asp:ListItem>
                         <asp:ListItem Value="2">瓶码</asp:ListItem>
                         <asp:ListItem Value="1">箱码</asp:ListItem>
                     </asp:RadioButtonList>
                 </td>
             </tr>
             <tr>
                 <th>需输入验证码</th>
                 <td>
                     <asp:CheckBox ID="ckb_yzm" runat="server" />
                 </td>
                 <th>父级奖励</th>
                 <td><div style="display: inline-block; text-align: left;"><asp:CheckBox ID="ckb_to_parent" runat="server" /><input  id="feedbackvalue" onafterpaste="if(isNaN(value))execCommand('undo')" onkeyup="if(isNaN(value))execCommand('undo')"  placeholder="额度" class="length1" runat="server"/></div></td>
             </tr>
            <tr><th>奖励策略</th><td>
                <asp:DropDownList ID="DDL_ASID" runat="server" AppendDataBoundItems="True" DataTextField="strategyname" DataValueField="id">
                    <asp:ListItem Value="0">请指定奖励策略</asp:ListItem>
                </asp:DropDownList>
                </td>
                <th>优先级</th>
                <td>
                    <input id="input_priority" value="0" class="length1" onafterpaste="if(isNaN(value))execCommand('undo')" onkeyup="if(isNaN(value))execCommand('undo')"  runat="server"/>
                </td>
             </tr>    
            <tr><th>备注</th><td colspan="3"><input id="inputRemarks" runat="server" class="p80" type="text" maxlength="25" /></td>
             </tr>
             <tr>
                 <th></th>
                 <td colspan="3"></td>
             </tr>
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
