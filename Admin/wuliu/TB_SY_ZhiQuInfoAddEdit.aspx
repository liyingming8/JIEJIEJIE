<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_ZhiQuInfoAddEdit.aspx.cs" Inherits="Admin_TB_SY_ZhiQuInfoAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SY_ZhiQuInfo</title>
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
                <table class="gridtable"> 
                    <tr><th>制曲车间</th><td><asp:DropDownList ID="DropDownList_cj" DataValueField="WLID" DataTextField="WorkLineName" AppendDataBoundItems="true" runat="server" Height="25px" Width="124px">
                                             <asp:ListItem Text="车间..." Selected="True" Value="0"></asp:ListItem>
             
                                         </asp:DropDownList></td></tr>
                    <tr><th>制曲批次</th><td><input id="inputZhiQuPiCi" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><th>大麦</th><td><asp:DropDownList ID="DropDownList_dm" DataValueField="YLMingCheng" DataTextField="YLCaiGouDanHao" AppendDataBoundItems="true" runat="server" Height="25px" Width="124px" >
                                           <asp:ListItem Text="批次..." Selected="True" Value="0"></asp:ListItem>
             
                                       </asp:DropDownList></td></tr>
                    <tr><th>高粱</th><td ><asp:DropDownList ID="DropDownList_gl" DataValueField="YLMingCheng" DataTextField="YLCaiGouDanHao" AppendDataBoundItems="true" runat="server" Height="25px" Width="124px">
                                                                                   <asp:ListItem Text="批次..." Selected="True" Value="0"></asp:ListItem>
             
                                                                               </asp:DropDownList></td></tr>
                    <tr><th>豌豆</th><td><asp:DropDownList ID="DropDownList_wd" DataValueField="YLMingCheng" DataTextField="YLCaiGouDanHao" AppendDataBoundItems="true" runat="server" Height="25px" Width="124px" >
                                           <asp:ListItem Text="批次..." Selected="True" Value="0"></asp:ListItem>
             
                                       </asp:DropDownList></td></tr>
                    <tr><th>制曲时间</th><td>
                                         <asp:TextBox ID="inputZhiQuShiJian" runat="server"></asp:TextBox>
                                         <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="inputZhiQuShiJian"></cc2:CalendarExtender>
                                     </td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="15" /></td></tr>
                </table>
                 <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/></div> 
    </ContentTemplate>
                </asp:UpdatePanel> 
      </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>