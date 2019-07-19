<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_Comp_ModulesConfigAddEditForAdmin.aspx.cs" Inherits="Admin_TJ_SWM_Comp_ModulesConfigAddEditForAdmin" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SWM_Comp_ModulesConfig</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
　　<link href="../include/windows.css" rel="stylesheet" /> 
       <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">增加功能模块</div>
         <table class="gridtable">
            <tr><th>功能模块</th><td>
                <asp:RadioButtonList ID="rbl_moudleinfo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:RadioButtonList>
                </td></tr> 
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <asp:HiddenField runat="server" ID="HF_CompID"/>
    <input type="hidden" id="hd_imgpath" runat="server"/>
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/jquery.easyui.min.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
