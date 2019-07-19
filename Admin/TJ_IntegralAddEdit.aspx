<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_IntegralAddEdit.aspx.cs" Inherits="Admin_TJ_IntegralAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_Integral</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="gridtable">
                            <tr><th>活动名称</th><td><input id="inputIntegralName" runat="server" type="text" 
                                                        maxlength="25" class="p80" />  
                                                                                        </td></tr>
                            <tr><th>开始时间</th><td>
                                                 <asp:TextBox ID="inputBeginDate" CssClass="p40" runat="server"></asp:TextBox><cc2:CalendarExtender  ID="CalendarExtender1" TargetControlID="inputBeginDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                                                  </td></tr>
                            <tr><th>结束时间</th><td>
                                                 <asp:TextBox ID="inputEndDate" CssClass="p40" runat="server"></asp:TextBox><cc2:CalendarExtender  ID="CalendarExtender2" TargetControlID="inputEndDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender> 
                                            </td></tr> 
                            <tr>
                                <th>积分平台</th>
                                <td>
                                    <asp:RadioButtonList ID="rbl_platform" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rbl_platform_SelectedIndexChanged">
                                        <asp:ListItem Value="0">自主</asp:ListItem>
                                        <asp:ListItem Value="1">第三方</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <th>分值/元</th>
                                <td>
                                    <input id="input_VLPerYuan" class="p20" readonly="readonly" runat="server" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <th>备注</th>
                                <td>
                                    <input id="inputRemarks" class="p80" runat="server" type="text" maxlength="25" />
                                </td>
                            </tr>
                        </table>
                      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" /></div> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />

                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
        </form>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>