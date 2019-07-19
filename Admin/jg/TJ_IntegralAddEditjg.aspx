<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_IntegralAddEditjg.aspx.cs" Inherits="Admin_TJ_IntegralAddEditjg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_Integral</title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="../images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> 
                                                         发布积分活动</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="TJ_Integral.aspx"><img title="返回" src="../images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr><td>活动名称</td><td><input id="inputIntegralName" runat="server" type="text" 
                                                        maxlength="25" size="60" /></td><td>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                                                                        ControlToValidate="inputIntegralName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                        </td></tr>
                            <tr><td>开始时间</td><td>
                                                 <asp:TextBox ID="inputBeginDate" runat="server"></asp:TextBox><cc2:CalendarExtender  ID="CalendarExtender1" TargetControlID="inputBeginDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                                             </td><td>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                                  ControlToValidate="inputBeginDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                  </td></tr>
                            <tr><td>结束时间</td><td>
                                                 <asp:TextBox ID="inputEndDate" runat="server"></asp:TextBox><cc2:CalendarExtender  ID="CalendarExtender2" TargetControlID="inputEndDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender></td><td>
                                                                                                                                                                                                                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                                                                                                                                                                                                                                         ControlToValidate="inputEndDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                                                                                                                                                         </td></tr>
                            <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                            <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
        </form>
    </body>
</html>