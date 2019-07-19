<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_Comp_ModulesConfigAddEdit.aspx.cs" Inherits="Admin_TJ_SWM_Comp_ModulesConfigAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_SWM_Comp_ModulesConfig</title>
　　<link href="../include/windows.css" rel="stylesheet" /> 
       <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toptitle">模块信息编辑</div>
         <table class="gridtable">
            <tr><th>模块名称</th><td><input id="inputmdnm" runat="server" type="text" class="p80" maxlength="10" /></td></tr>
            <tr><th>图像</th><td><table><tr><td><img id="img_logo" runat="server" height="60"/></td>
                <td></td>
                </tr><tr><td><input type="button" value="上传图片" class="btn  btn-info btnyd" onclick="openWinCenter('getpic/piccutter.aspx?key=img_logo&hdsv=hd_imgpath&bilv=1', 580, 580, '编辑模块LOGO')"/></td>
                    <td>
                        <asp:Button ID="btn_return" CssClass="btn btn-info btnyd" runat="server" Text="还原" OnClick="btn_return_Click" />
                    </td>
                </tr></table></td></tr>
            <tr><th>显示</th><td><asp:CheckBox ID="ckb_show" runat="server" /></td></tr>
            <tr><th>顺序</th><td><input id="inputshoworder" runat="server" type="text" style="text-align: center;padding: 0;" class="p20" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td></tr>
             <tr runat="server" id="adminrow">
                 <th>链接</th>
                 <td><input id="inputlink" runat="server" class="p80"/></td>
             </tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
  </ContentTemplate>
</asp:UpdatePanel>
     <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
    <input type="hidden" id="hd_imgpath" runat="server"/>
     <script src="../js/jquery-1.7.1.js"></script>
     <script src="../include/js/jquery.easyui.min.js"></script>
     <script src="../include/js/UploadImage.js"></script>
</form>
</body>
</html>
