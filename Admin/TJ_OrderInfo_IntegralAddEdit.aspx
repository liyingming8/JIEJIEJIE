<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_OrderInfo_IntegralAddEdit.aspx.cs" Inherits="Admin_TJ_OrderInfo_IntegralAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_OrderInfo_Integral</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">兑奖操作</div>
         <table class="gridtable">
            <tr><th>编号</th><td> 
                <asp:Label ID="LabelOrderNumber" runat="server"></asp:Label>
                </td>
                <td>提交日期</td>
                <td>
                    <asp:Label ID="inputOrderDate" runat="server" />
                </td>
             </tr>
            <tr><th>奖品</th><td><asp:Label id="inputGoodsID" runat="server"/></td>
                <td>数量</td>
                <td>
                    <asp:Label ID="inputOrderNum" runat="server" />
                </td>
             </tr> 
            <tr><th>收货人</th><td>
                <asp:Label ID="inputCustomerName" runat="server" />
                </td>
                <td>联系电话</td>
                <td>
                    <asp:Label ID="inputCustomerPhone" runat="server" />
                </td>
             </tr>
            <tr><th>详细地址</th><td colspan="3">
                <asp:Label ID="inputAddress" runat="server" maxlength="50" type="text" />
                </td>
             </tr>
             <tr>
                 <th>客户备注</th>
                 <td colspan="3">
                     <asp:Label ID="inputRemarks" runat="server" />
                 </td>
             </tr>
            <tr><th>快递公司</th><td>
                <asp:DropDownList ID="DDL_DeliveryCompID" runat="server" AppendDataBoundItems="True" DataTextField="logisticcompany" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">指定物流公司</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>快递单号</td>
                <td>
                    <input id="inputWuLiuDanHao" runat="server" class="p80" type="text" maxlength="50" />
                </td>
             </tr>
            <tr><th>费用</th><td>
                ￥<input id="inputYunFei" runat="server" type="text" class="p20" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20" />
                </td>
                <td>发货日期</td>
                <td>
                    <input id="inputDeliveryComfirmDate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3"  maxlength="16" />
                </td>
             </tr>
<%--            <tr><th>已收货</th><td>
                <asp:CheckBox ID="ckb_yishouhuo" runat="server" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
             </tr>--%>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
