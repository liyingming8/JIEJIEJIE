<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_advertisementAddEdit.aspx.cs" Inherits="Admin_tj_js_advertisementAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>tj_js_advertisement</title>
　　<link href="../include/windows.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table class="gridtable">
            <tr><th>所属单位</th><td><input id="txCompID" runat="server" type="text" class="length11" readonly="readonly"  /></td></tr> 
            <tr><th>所属产品</th><td>
                <asp:DropDownList ID="ddl_goodsid" runat="server" DataTextField="name" DataValueField="goodsid">
                </asp:DropDownList>
                </td></tr>
            <tr><th>图片</th><td><input id="inputimg" runat="server" type="text"  /></td></tr> 
            <tr><th>介绍</th><td><textarea id="inputintro" runat="server" type="text" class="length13" rows="3"></textarea></td></tr>
            <tr><th>价格</th><td>￥<input id="inputprice" runat="server" type="text"  class="length2" /></td></tr>
            <tr><th>真实价格</th><td>￥<input id="inputrealprice" runat="server" type="text"  class="length2" /></td></tr>
            <tr><th>位置</th><td><input id="inputposition" runat="server" type="text"  /></td></tr> 
            <tr><th>有效</th><td><asp:CheckBox runat="server" ID="ckbvalid"/></td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <script src="../Admin/include/js/jquery.min.js" type="text/javascript"></script>
     <script src="../Admin/include/js/UploadImage.js" type="text/javascript"></script>
     <script src="../Admin/include/js/jquery.easyui.min.js" type="text/javascript"></script>
</form>
</body>
</html>
