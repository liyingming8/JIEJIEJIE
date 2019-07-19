<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_ZhiJiuInfoAddEdit.aspx.cs" Inherits="Admin_TB_SY_ZhiJiuInfoAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SY_ZhiJiuInfo</title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
       
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
           <div class="editpageback">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                <table  class="gridtable"> 
                    <tr><th>制曲批次</th><td>
                                         <asp:DropDownList ID="DropDownList_zq" DataValueField="ZhiQuID" DataTextField="ZhiQuPiCi" AppendDataBoundItems="true" runat="server" Height="25px" Width="124px">
                                             <asp:ListItem Text="制曲批次..." Selected="True" Value="0"></asp:ListItem> 
                                         </asp:DropDownList>
                                     </td></tr>
                    <tr><th>制酒批次</th><td><input id="inputZhiJiuPiCi" runat="server" type="text"  /></td></tr>
                    <tr><th>制酒车间</th><td><input id="inputZhiJiuCheJian" runat="server" type="text"/></td></tr>
                    <tr><th>制酒班组</th><td><input id="inputZhiJiuBanZu" runat="server" type="text" /></td></tr>
                    <tr><th>制酒时间</th><td>

                                         <asp:TextBox ID="inputZhiJiuShiJian" runat="server"></asp:TextBox>
                                         <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="inputZhiJiuShiJian"></cc2:CalendarExtender>

                                    </td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text"/></td></tr>
                </table>
                <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加"   CssClass="btn btn-warning btnyd"/></div>
 </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>