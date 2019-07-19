<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoViewNew.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoViewNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title> 
        <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem"><span>起始时间</span></div>
                <div class="topitem">
                    <asp:TextBox ID="TextBox_RukuDateBegin" runat="server" Width="70px" CssClass="inputsearch"></asp:TextBox><cc1:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" />
                </div>
                <div class="topitem"><span>至</span></div>
                <div class="topitem">
                    <asp:TextBox ID="TextBox_RukuDateEnd" runat="server" Width="70px" CssClass="inputsearch"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                </div>
       <%--         <div class="topitem"><span>经销商</span></div>
                <div class="topitem">
                    <input class="inputsearch" style="width: 250px;" runat="server" placeholder="全部经销商" id="inputagentid" /><input id="hd_agentid" runat="server" type="hidden" /> 
                </div>
                <div class="topitem"><span>产品</span></div>
                <div class="topitem">
                    <input type="text" id="input_ProInfo" runat="server" class="inputsearch" style="width: 200px;"/>
                    <input  type="hidden" id="hd_proid" runat="server"/> 
                </div>--%>
            <%--    <div class="topitem"><span>货仓</span></div>
                <div class="topitem">
                    <asp:DropDownList ID="ComboBox_StoreHouse" runat="server" AppendDataBoundItems="true" DataTextField="StoreHouseName" DataValueField="STID">
                        <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>--%>
                <%--<div class="topitem"><asp:Button ID="Button_Search" runat="server" Text="查 询" OnClick="Button_Search_Click"   ToolTip="点击此按钮，查询量大的情况下会比较慢，可考虑缩小起止时间，多次查询" /></div>--%>
                <div class="topitem">
                    <asp:Button ID="Button_Search" runat="server" Text="查 询" CssClass="btn btn-warning btnyd" OnClick="Button_Search_Click" ToolTip="分页显示仅供查看，数据无法被导出" /></div>
                <div class="topitem">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" Text="导出EXCEL" OnClick="Button1_Click" Enabled="False" ToolTip="需要导出数据时，请点击【查询】按钮【数据量大时会有些延时】" /></div>
            </div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView_RukuInfo" Width="100%" runat="server" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="GridView_RukuInfo_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="发货批次">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>合计：</FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StoreHouseName" HeaderText="发货库房" />
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Products_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="件数">
                            <ItemTemplate>
                                <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_jianshu_ft" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="发货时间">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("FHDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货经销商">
                            <ItemTemplate>
                                <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="发货人">
                            <ItemTemplate>
                                <asp:Label ID="LabelFHR" runat="server" Text='<%# Bind("FHUserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
            </div>
        </div> 
        <script type="text/javascript" src="../../../include/js/jquery.min.js"></script>
        <script type="text/javascript" src="../../../include/js/UploadImage.js"></script>
        <script type="text/javascript" src="../../../js/jquery.easyui.min.js"></script>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
    </form>
</body>
</html>
