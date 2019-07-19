<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoKuCunJXC.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoKuCunJXC" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title> 
        <link href="../../../include/MasterPage.css" rel="stylesheet" />  
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div style="background-color: white; border-radius: 6px; border: 1px solid gray; padding: 5px 5px;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                    </asp:ScriptManager>
                    <table style="width:100%">
                        <tr>
                            <td nowrap="noWrap" align="left">起始时间:</td>
                            <td colspan="6" nowrap="noWrap">
                                <asp:TextBox ID="TextBox_RukuDateBegin" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                                至
                                <asp:TextBox ID="TextBox_RukuDateEnd" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" nowrap="nowrap">代理商:</td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ComboBox_DaiLiShangID" runat="server" AppendDataBoundItems="true" DataTextField="CompName" DataValueField="CompID">
                                    <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <%--<td nowrap="nowrap">产 品:</td>
                        <td nowrap="nowrap" >
                            <cc2:ComboBox ID="ComboBox_ProInfo" runat="server" AppendDataBoundItems="true" DataTextField="Products_Name" DataValueField="Infor_ID">
                                <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                            </cc2:ComboBox>
                        </td>
                        <td nowrap="nowrap">货仓:</td>
                        <td nowrap="nowrap">
                            <cc2:ComboBox ID="ComboBox_StoreHouse" runat="server" AppendDataBoundItems="true" DataTextField="StoreHouseName" DataValueField="STID">
                                <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                            </cc2:ComboBox>
                        </td>--%>
                            <td nowrap="nowrap">
                                <asp:Button ID="Button_Search" runat="server" Text="查 询" OnClick="Button_Search_Click" Width="60px" /></td>
                            <td style="width: 180px; text-align: center">
                                <asp:Button ID="Button2" runat="server" Visible="false" Text="经销商发货查询" OnClick="Button2_Click" />
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="inputbutton" Text="导出EXCEL" OnClick="Button1_Click" /></td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%">
                    <tr>
                        <td >
                            <asp:Label ID="Label1" runat="server" Text="发货产品列表:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" valign="top" >
                            <asp:GridView ID="GridView_RukuInfo" Width="100%" runat="server" CssClass="grid" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="GridView_RukuInfo_RowDataBound">
                                <Columns>
                                    <%-- <asp:BoundField HeaderText="代理商ID" DataField="AgentID" />
                                <%-- <asp:TemplateField HeaderText="批次">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>合计：</FooterTemplate>
                            </asp:TemplateField>--%>
                                    <%-- <asp:BoundField DataField="FHDate" HeaderText="发货日期" />--%>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Literal ID="lit" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发往经销商">
                                        <ItemTemplate>
                                            <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(GetCookieCompID())).CompName + "--------->" + bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="产品">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               
                                    <%--   <asp:TemplateField HeaderText="发货时间">

                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("FHDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <%--  <asp:TemplateField HeaderText="发货人">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFHR" runat="server" Text='<%# Bind("FHUserID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="到查询时间截止库存">

                                        <ItemTemplate>
                                            <asp:Label ID="LabYiFaHuokucun" runat="server"  Text='<%# int.Parse(comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "kuncun")) - int.Parse(comm.getcijiDailishangkucun(Eval("AgentID").ToString(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "kuncun")) %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="上级发货数量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_jianshu" runat="server" Text='<%# comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "daohuo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            合计：
                                            <asp:Label ID="Label_jianshu_ft" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="已发货">

                                        <ItemTemplate>
                                            <asp:Label ID="LabYiFaHuo" runat="server" Text='<%# comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "fahuo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="库存剩余">

                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#int.Parse(comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "kuncun")) - int.Parse(comm.getcijiDailishangkucun(Eval("AgentID").ToString(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "kuncun")) + int.Parse(comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "daohuo")) - int.Parse(comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "fahuo")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# JiageLinkString(Eval("AgentID").ToString()) %>' Text='详细'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    尚未查询到满足条件的入库记录! 
                                </EmptyDataTemplate>
                                <RowStyle Wrap="False" Height="28px" />
                                <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                                <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="Left"
                                             VerticalAlign="Middle" Wrap="False" Font-Size="13px" />
                                <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                            </asp:GridView> 
                        </td>
                    </tr> 
                    <tr>
                        <td >
                            <asp:Label ID="Label2" runat="server" Text="产品列表:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" valign="top" >
                            <asp:GridView ID="GridViewSH" Width="100%" runat="server" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="GridViewSH_RukuInfo_RowDataBound">
                                <Columns>
                                    <%-- <asp:TemplateField HeaderText="批次">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>合计：</FooterTemplate>
                            </asp:TemplateField>--%>
                                    <%-- <asp:BoundField DataField="FHDate" HeaderText="发货日期" />--%>
                                    <asp:TemplateField HeaderText="发货商">
                                        <ItemTemplate>
                                            <asp:Label ID="FHsID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("CompID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Products_Name" HeaderText="产品" />
                                    <asp:BoundField DataField="StoreHouseName" HeaderText="库房" />
                                    <asp:TemplateField HeaderText="发货人">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelFHS" runat="server" Text='<%# Bind("FHUserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="总量">
                                        <ItemTemplate>
                                            <asp:Label ID="LFhNum" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            合计：
                                            <asp:Label ID="LallNum" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="我的发货量">

                                        <ItemTemplate>
                                            <asp:Label ID="LMeFaHuo" runat="server" Text='<%# comm.getDailishangkucun(GetCookieCompID(), Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "fahuo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="库存剩余">

                                        <ItemTemplate>
                                            <asp:Label ID="LKC" runat="server" Text='<%# kcsy(int.Parse(Eval("XiangNumber").ToString()), Eval("AgentID").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="详细">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpmlink" runat="server" NavigateUrl='<%# JiageLinkString(Eval("AgentID").ToString()) %>' Text='详细'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    尚未查询到满足条件的入库记录!
                                </EmptyDataTemplate>
                                <RowStyle Wrap="False" Height="28px" />
                                <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                                <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="Left"
                                             VerticalAlign="Middle" Wrap="False" Font-Size="13px" />
                                <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                            </asp:GridView> 
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <script type="text/javascript" src="../../../include/js/UploadImage.js"></script>
    </body>
</html>