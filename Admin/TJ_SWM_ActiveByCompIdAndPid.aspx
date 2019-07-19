<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_ActiveByCompIdAndPid.aspx.cs" Inherits="Admin_TJ_SWM_ActiveByCompIdAndPid" %> 
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div class="div_WholePage">
            <div class="topdiv">   
                 <div class="topitem">
                     <span>三维码客户</span> 
                 </div>
                <div class="topitem" id="foradmin" Visible="False" runat="server">
                    <input id="inputcompid" runat="server" class="inputsearch p60" placeholder="点击指定"/>
                </div>
                <div class="topitem">
                    <span>激活码</span>
                </div>
                <div class="topitem">
                    <input id="inputactivelabel" runat="server" class="inputsearch" style="width: 100px;"/>
                </div>
                <div class="topitem">
                    <span>数量</span>
                </div>
                <div class="topitem">
                    <input id="input_totalnm" runat="server" class="inputsearch" style="width: 40px;"/>
                </div>
                <div class="topitem">
                    <span>功能试用期限</span>
                </div>
                <div class="topitem">
                    <input id="inputpermitday" runat="server"  class="inputsearch"  style="width: 50px;"/>天
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="确定激活" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
            </div> 
            <div style="overflow-x:auto"></div>
        </div>
        <input id="hd_compid" runat="server" type="hidden" value="0"/>
        <script src="../include/js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>  
    </form>
</body>
</html>
