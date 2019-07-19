<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CRM_BrandInfoAddEdit.aspx.cs" Inherits="CRM_TJ_CRM_BrandInfoAddEdit" %>
 <!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TJ_CRM_BrandInfo</title>
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
            <tr><th>品牌名称</th><td><input id="inputBrandName" runat="server" type="text"  maxlength="25" /></td>
             </tr>
            <tr>
                 <th>类别</th>
                <td>
                    <asp:DropDownList ID="ComboBoxBrandType" runat="server" RenderMode="ComboBox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><th>简介</th><td>
                <textarea id="inputShortDescription" runat="server" cols="50" maxlength="100" name="S2" rows="2"></textarea></td>
             </tr> 
            <tr>
                 <th>显示</th>
                <td>
                    <asp:CheckBox ID="checkboxIsVisible" runat="server" />
                </td>
            </tr>
            <tr><th>描述</th><td colspan="3">
                <textarea id="TextAreaDescription" cols="80" runat="server" name="S1" rows="8"></textarea></td></tr> 
            <tr><th>LOGO</th><td colspan="3"><table style="border: none;border-collapse:collapse;"><tr><td>
                                                   <asp:Image ID="Image_Logo" Width="120px" runat="server" ImageUrl="~/CRM/images/NoPic.gif" /> 
                                               </td><td>
                                                        <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" 
                                                                src="../CRM/Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" 
                                                                style="vertical-align: text-bottom" width="250"></iframe>
                                                    </td></tr></table></td> 
             </tr>
            <tr><th>备注</th><td colspan="3"><input id="inputRemarks" runat="server" class="length13" type="text" maxlength="25" /></td>
             </tr>
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
    <asp:HiddenField ID="HF_LogoImage" runat="server" />
</form>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script> 
</body>
</html>
