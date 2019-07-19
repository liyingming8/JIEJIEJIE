<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FenLeiJoinChengJiaoLv.aspx.cs" Inherits="Admin_Join_FenLeiJoinChengJiaoLv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server"></asp:ScriptManager>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
<tr><td style="width: 80px" align="left" class="tdbg">起始日期</td>
    <td align="left" class="tdbg" style="width: 100px"><asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
    </td><td class="tdbg" style="width: 20px;text-align:center;">至</td><td class="tdbg" style="width: 100px"><asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
    </td>
<td class="tdbg">
    <%--<asp:RadioButtonList ID="RBL_JoinBy" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Value="1">按时间</asp:ListItem>
        <asp:ListItem Value="2">地点</asp:ListItem>
    </asp:RadioButtonList>--%>
    <asp:RadioButtonList ID="RBL_Item" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="省份地区" Value="1" Selected="True"></asp:ListItem>
        <asp:ListItem Text="年龄段" Value="2"></asp:ListItem>
        <asp:ListItem Text="性别" Value="3"></asp:ListItem>
    </asp:RadioButtonList>
    </td>
<td class="tdbg"><asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td></tr></table>
    <br />
        <asp:Chart ID="Chart1" runat="server" Palette="BrightPastel" imagetype="Png" BorderDashStyle="Solid"  BackGradientStyle="TopBottom" BorderWidth="1" backcolor="192, 255, 192" BackSecondaryColor="White" BorderColor="26, 59, 105" Width="800px" >
            <BorderSkin BorderColor="Silver" PageColor="Chartreuse" />
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="192, 255, 192" ShadowColor="Transparent" BackGradientStyle="TopBottom">
								<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
								<axisy linecolor="64, 64, 64, 64">
									<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
									<majorgrid linecolor="64, 64, 64, 64" />
								</axisy>
								<axisx linecolor="64, 64, 64, 64">
									<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
									<majorgrid linecolor="64, 64, 64, 64" />
								</axisx>
							</asp:ChartArea>
						</chartareas>
            				<legends>
							<asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
						</legends>
            <borderskin skinstyle="Emboss" />
        </asp:Chart></div>
    </form>
</body>
</html>
