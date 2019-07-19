<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_LotteryActivityAddEdit.aspx.cs" Inherits="Admin_TJ_LotteryActivityAddEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_LotteryActivity</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
        <div class="editpageback">
            <table class="gridtable">
                <tr>
                    <th>活动名称</th>
                    <td>
                        <input id="inputGoodsInfo" runat="server" type="text" maxlength="50" /></td>
                </tr>
                <tr>
                    <th>开始时间</th>
                    <td>
                        <asp:TextBox ID="inputPublishstartDate" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="inputPublishstartDate" ID="CalendarExtender1" Format="yyyy-MM-dd" runat="server">
                        </cc2:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <th>结束时间</th>
                    <td>
                        <asp:TextBox ID="inputPublishendDate" runat="server"></asp:TextBox><cc2:CalendarExtender TargetControlID="inputPublishendDate" Format="yyyy-MM-dd" ID="CalendarExtender2" runat="server">
                        </cc2:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <th >产品</th>
                    <td colspan="2" style="white-space: initial;">
                        <asp:CheckBoxList ID="CheckBoxList_Product"  DataTextField="Products_Name" DataValueField="Infor_ID" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <th>备注</th>
                    <td>
                        <input id="inputRemarks" runat="server" type="text" maxlength="25" /></td>
                </tr>
            </table>
            <div class="bottomdivbutton">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" /></div>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
