<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_CommonSuyuanAddEdit.aspx.cs" Inherits="Admin_TJ_SWM_CommonSuyuanAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>溯源信息</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
　　<link href="../include/windows.css" rel="stylesheet" />
        <link href="../include/easyui.css" rel="stylesheet" />
　　<script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="editpageback">
    <div class="toptitle">溯源信息编辑</div>
         <table class="gridtable">
            <tr><th>产品</th><td>
                <asp:Label ID="Label_goodsname" runat="server"></asp:Label>
                </td></tr>
            <tr><th>原料</th><td><textarea id="inputmaterials" class="p90" runat="server" type="text" maxlength="100" ></textarea></td></tr>
             <tr>
                 <th>生产日期</th>
                 <td>
                     <input id="inputproddate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="p50"  maxlength="16" />
                 </td>
             </tr>
             <tr>
                 <th>出厂日期</th>
                 <td>
                     <input id="inputoutdate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="p50"  maxlength="16" />
                 </td>
             </tr>
             <tr>
                 <th>质检员</th>
                 <td>
                     <input id="inputcheckuser" runat="server" class="p40" type="text" maxlength="10" />
                 </td>
             </tr>
             <tr>
                 <th>质检日期</th>
                 <td>
                     <input id="inputcheckdate" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="p50" maxlength="16" />
                 </td>
             </tr>
             <tr>
                 <th>质检报告</th>
                 <td><table style="border-style:none; border-color:white; border-width: 0px; padding: 0px; margin: 0px; table-layout: auto">
                     <tr>
                         <td>
                             <asp:Image ID="Image_showlogo" runat="server" ImageUrl="~/Admin/Images/NoPic.gif" Width="120px" />
                         </td>
                         <td><input id="Button_uplogo" type="button" value="上传"  onclick="openWinCenter('getpic/piccutter.aspx?key=Image_showlogo&hdsv=HF_ReportImage&bilv=0.707', 680, 680, '上传质检报告')" /></td>
                     </tr> 
                 </table></td>
             </tr> 
             <tr>
                 <th>生产商</th>
                 <td>
                     <input id="input_procompname" runat="server" class="p75"/>
                 </td>
             </tr>
             <tr>
                 <th>产地</th>
                 <td>
                     <input id="input_proaddress" runat="server" class="p75"/>
                 </td>
             </tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
        </div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
    <asp:HiddenField runat="server" ID="hf_product_code"/>
     <asp:HiddenField runat="server" ID="HF_PID"/>
     <asp:HiddenField runat="server" ID="HF_ProdID"/>
     <asp:HiddenField runat="server" ID="HF_ReportImage"/>
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
