<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoKuCunNew.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoKuCunNew" %>  
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
           <div class="div_WholePage">
                <div class="div_Nav">
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
                                <asp:Button ID="Button_Search" runat="server" Text="信息查询" OnClick="Button_Search_Click" Width="150px" /></td>
                            <td>       <asp:Button ID="Button1" runat="server" CssClass="inputbutton" Text="导出EXCEL" OnClick="Button1_Click" /></td>                            <td style="width: 180px; text-align: center">  
                                <asp:Button ID="Button2" runat="server" Text="单个经销商发货查询" OnClick="Button2_Click" Visible="False"  />
                            </td>

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
                            <asp:GridView ID="GridView_RukuInfo" Width="90%" runat="server" CssClass="grid"  CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Small" AutoGenerateColumns="False" OnRowDataBound="GridView_RukuInfo_RowDataBound" Visible="False">
                                <Columns> 
                                    <asp:TemplateField HeaderText="发货商" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="labelCompID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("CompID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="发往经销商" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="labelProduct" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="发货日期">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelFHR" runat="server" Text='<%#Convert.ToDateTime(Eval("FHDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="总量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            合计：
                                            <asp:Label ID="Label_jianshu_ft" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="下级经销商已发货">

                                        <ItemTemplate>
                                            <asp:Label ID="LabYiFaHuo" runat="server" Text='<%# comm.getDailishangkucun(Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="库存剩余">

                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# kcsy(int.Parse(Eval("XiangNumber").ToString()), Eval("AgentID").ToString()) %>'></asp:Label>
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
                            <br /> 
                             <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Both" Width="90%" >
                                 <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" Width ="94%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both" >
                                     <AlternatingRowStyle BackColor="White" />
                                     <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                     <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                     <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                     <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                     <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                     <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                     <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                     <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                     <SortedDescendingHeaderStyle BackColor="#820000" />
                                 </asp:GridView></div>
                                 <br />
                                 <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                 <br />
                                 <br />
                             </asp:Panel>

                        </td>
                    </tr>

                    <tr>
                        <td >
                            <asp:Label ID="Label2" runat="server" Text="产品列表:" Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" valign="top" >
                            <asp:GridView ID="GridViewSH" Width="90%" runat="server" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Small" AutoGenerateColumns="False" OnRowDataBound="GridViewSH_RukuInfo_RowDataBound" Visible="False">
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
                                            <asp:Label ID="LallNum" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="我的发货量">

                                        <ItemTemplate>
                                            <asp:Label ID="LMeFaHuo" runat="server" Text='<%# comm.getDailishangkucun(Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text) %>'></asp:Label>
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