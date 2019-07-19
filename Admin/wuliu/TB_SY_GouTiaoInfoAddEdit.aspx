<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_GouTiaoInfoAddEdit.aspx.cs" Inherits="Admin_TB_SY_GouTiaoInfoAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SY_GouTiaoInfo</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
      
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
            </asp:ScriptManager>
            <div class="editpageback">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                <table class="gridtable">
                    <tr><th>制酒名称</th><td> <asp:DropDownList ID="ComboBox_ZID" runat="server" DataTextField="ZhiJiuPiCi" 
                                                        DataValueField="ZhiJiuID">
                                          </asp:DropDownList></td></tr>
                    <tr><th>大样酒名称</th><td><input id="inputDaYangJiuMingCheng" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>大样酒批次</th><td><input id="inputDaYangJiuPiCi" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>勾调班组</th><td><input id="inputGouTiaoBanZu" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>勾调时间</th><td>
                                         <asp:TextBox ID="inputGouTiaoShiJian" runat="server"></asp:TextBox>
                                         <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="inputGouTiaoShiJian"></cc2:CalendarExtender>
                                     </td></tr>
                    <tr><th>质检员</th><td><input id="inputZhiJianYuan" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="15" /></td></tr>
           
                </table>　　
               <div class="bottomdivbutton"> <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/></div>
                </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
          <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>