<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_brandpriceAddEdit.aspx.cs" Inherits="CRM_tj_crm_brandpriceAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_crm_brandprice</title>
    <style type="text/css">
        .auto-style1 {
            height: 45px;
        }
    </style>
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
            <tr><th>所属品牌</th><td>
                <asp:DropDownList ID="ddl_brandinfo" runat="server" DataTextField="brandname" DataValueField="id">
                </asp:DropDownList>
                </td></tr>
            <tr><th>经销商级别</th><td>
                <asp:DropDownList ID="DDL_CGrade" runat="server" AppendDataBoundItems="True" DataTextField="gradename" DataValueField="id">
                    <asp:ListItem Value="0">不限</asp:ListItem>
                </asp:DropDownList>
                </td></tr>
            <tr><th>预定价格(￥)</th><td><input id="inputorignalprice" runat="server"  type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /><asp:DropDownList ID="ddlunit" runat="server" DataTextField="unitname" DataValueField="id">
                </asp:DropDownList>
                </td></tr>
             <tr>
                 <th>起订数量</th>
                 <td>
                     <input id="input_startvalue" style="text-align: center;" runat="server" type="text" />
                 </td>
             </tr>
             <tr>
                 <th>奖励额度(￥)</th>
                 <td><input id="inputcommissionvalue" style="text-align: center;"  runat="server" value="0" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td>
             </tr>
            <tr><th class="auto-style1">父级返点</th><td class="auto-style1"><input id="inputparentrcommissionpercent"  value="0" runat="server" style="text-align: center;" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" /></td></tr>
             <tr>
                 <th>返点级数</th>
                 <td>
                     <input id="inputcommisonlevelnum" value="0" runat="server" style="text-align: center;" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" />
                 </td>
             </tr>
            <tr><th>备注</th><td><input id="inputremarks" value="无" runat="server" type="text" maxlength="25" /></td></tr>
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
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
