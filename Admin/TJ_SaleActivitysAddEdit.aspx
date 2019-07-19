<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SaleActivitysAddEdit.aspx.cs" Inherits="Admin_TJ_SaleActivitysAddEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_SaleActivitys</title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div> 
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="gridtable" >            
                            <tr><th>产品</th><td>
                                               <asp:CheckBoxList ID="CHKList_Goods" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true">
                                                   <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                               </asp:CheckBoxList>
                                           </td></tr>
                            <tr><th>活动介绍</th><td><input id="inputIntroductions" runat="server" type="text" 
                                                        maxlength="200" size="60" /></td></tr>
                            <tr><th>开始日期</th><td>
                                                 <asp:TextBox ID="TextBoxStartDate" CssClass="date" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="TextBoxStartDate" Format="yyyy-MM-dd" ID="CalendarExtender1" runat="server">
                                                                                                                 </cc2:CalendarExtender>
                                             </td></tr>
                            <tr><th>结束日期</th><td>
                                                 <asp:TextBox ID="TextBox_EndDate" CssClass="date" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="TextBox_EndDate"  Format="yyyy-MM-dd" ID="CalendarExtender2" runat="server">
                                                                                                                </cc2:CalendarExtender>
                                             </td></tr>
                            <tr><th>折扣</th><td><input id="inputDiscount" class="number" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20" /></td></tr>
                            <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="30" /></td></tr>
                        </table>
                        <div class="bottomdivbutton"> <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加"  CssClass="btn btn-warning btnyd" /></div>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>