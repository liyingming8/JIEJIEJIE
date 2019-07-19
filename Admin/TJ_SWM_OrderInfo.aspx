<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_OrderInfo.aspx.cs" Inherits="Admin_TJ_SWM_OrderInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_SWM_OrderInfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'TJ_SWM_OrderInfo')" /></div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="ordernumber">单号</asp:ListItem>
                        <asp:ListItem Value="orderremark">备注</asp:ListItem>
                        <asp:ListItem Value="ordercompnm">下单客户</asp:ListItem>
                        <asp:ListItem Value="orderusernm">下单人</asp:ListItem>
                        <asp:ListItem Value="orderphonenm">联系电话</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单编号">
                            <ItemTemplate>
                                <asp:Label ID="Labelordernumber" runat="server" Text='<%# Bind("ordernumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="包装">
                            <ItemTemplate>
                                <asp:Label ID="Labelpsid" runat="server" Text='<%# Bind("psid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="Labelorderquantity" runat="server" Text='<%# Bind("orderquantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Labelorderremark" runat="server" Text='<%# Bind("orderremark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="合计">
                            <ItemTemplate>
                                <asp:Label ID="Labeltotalprice" runat="server" Text='<%# Bind("totalprice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下单时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelordertm" runat="server" Text='<%# Bind("ordertm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下单客户">
                            <ItemTemplate>
                                <asp:Label ID="Labelordercompnm" runat="server" Text='<%# Bind("ordercompnm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下单人">
                            <ItemTemplate>
                                <asp:Label ID="Labelorderusernm" runat="server" Text='<%# Bind("orderusernm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系电话">
                            <ItemTemplate>
                                <asp:Label ID="Labelorderphonenm" runat="server" Text='<%# Bind("orderphonenm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="支付方式">
                            <ItemTemplate>
                                <asp:Label ID="Labelpaytype" runat="server" Text='<%# Bind("paytype") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否支付">
                            <ItemTemplate>
                                <asp:Label ID="Labelispay" runat="server" Text='<%# Bind("ispay") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="支付确认">
                            <ItemTemplate>
                                <asp:Label ID="Labelpayconfirm" runat="server" Text='<%# Bind("payconfirm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="确认人">
                            <ItemTemplate>
                                <asp:Label ID="Labelconfirmuserid" runat="server" Text='<%# Bind("confirmuserid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否发货">
                            <ItemTemplate>
                                <asp:Label ID="Labelisfahuo" runat="server" Text='<%# Bind("isfahuo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货人">
                            <ItemTemplate>
                                <asp:Label ID="Labelfahuouserid" runat="server" Text='<%# Bind("fahuouserid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否激活">
                            <ItemTemplate>
                                <asp:Label ID="Labelisactive" runat="server" Text='<%# Bind("isactive") %>'></asp:Label>
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
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
            </div>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
