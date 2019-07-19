<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerorderInfo.aspx.cs" Inherits="CRM_tj_crm_customerorderInfo" %>

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
        <div class="topdiv">
          <%--  <div class="topitem">
                <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('tj_crm_customerorderInfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_crm_customerorderInfo')" /></div>--%>
            <div class="topitem"><span></span></div>
            <div class="topitem">
                <asp:DropDownList ID="DDLField" runat="server"> 
                    <asp:ListItem Value="ordernumber">订单编号</asp:ListItem>  
                    <asp:ListItem Value="shouhuoren">收货人</asp:ListItem>
                    <asp:ListItem Value="shouhuophonenumber">收货人电话</asp:ListItem>
                    <asp:ListItem Value="shouhuodizhi">收货地址</asp:ListItem> 
                    <asp:ListItem Value="kuaididanhao">快递单号</asp:ListItem>
                    <asp:ListItem Value="kuaidicompany">快递公司</asp:ListItem> 
                    <asp:ListItem Value="remarks">备注</asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <div class="topitem">
                <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容" /></div>
            <div class="topitem">
                <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
        </div>
        <div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="订单编号">
                        <ItemTemplate>
                            <asp:Label ID="LabelIndex" Text='<%#Eval("orderinfocode") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="下单客户">
                        <ItemTemplate>
                            <asp:Label ID="Labelordercustomerid" runat="server" Text='<%# GetCustomerName(Eval("ordercustomerid").ToString())  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="品牌">
                        <ItemTemplate>
                            <asp:Label ID="Labelbrandid" runat="server" Text='<%# GetBrandName(Eval("brandid").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label ID="Labelordernumber" runat="server" Text='<%# Bind("ordernumber") %>'></asp:Label><asp:Label ID="Labelunitname" runat="server" Text='<%# Bind("unitname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="合计金额">
                        <ItemTemplate>
                            ￥<asp:Label ID="Labeltotalprice" runat="server" Text='<%# Bind("totalprice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下单日期">
                        <ItemTemplate>
                            <asp:Label ID="Labelorderdatetime" runat="server" Text='<%# Bind("orderdatetime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="介绍人">
                        <ItemTemplate>
                            <asp:Label ID="Labelparentcustomerid" runat="server" Text='<%# GetCustomerName(Eval("parentcustomerid").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="是否支付">
                        <ItemTemplate>
                            <asp:Label ID="Labelispay" runat="server" Text='<%# Bind("ispay") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付时间">
                        <ItemTemplate>
                            <asp:Label ID="Labelpaydatetime" runat="server" Text='<%# Bind("paydatetime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付单号">
                        <ItemTemplate>
                            <asp:Label ID="Labelpaynumber" runat="server" Text='<%# Bind("paynumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付方式">
                        <ItemTemplate>
                            <asp:Label ID="Labelpaymethod" runat="server" Text='<%# Bind("paymethod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收货人">
                        <ItemTemplate>
                            <asp:Label ID="Labelshouhuoren" runat="server" Text='<%# Bind("shouhuoren") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收货人电话">
                        <ItemTemplate>
                            <asp:Label ID="Labelshouhuophonenumber" runat="server" Text='<%# Bind("shouhuophonenumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收货地址">
                        <ItemTemplate>
                            <asp:Label ID="Labelshouhuodizhi" runat="server" Text='<%# Bind("shouhuodizhi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <%--    <asp:TemplateField HeaderText="是否发货">
                        <ItemTemplate>
                            <asp:Label ID="Labelisfahuo" runat="server" Text='<%# Bind("isfahuo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="快递单号">
                        <ItemTemplate>
                            <asp:Label ID="Labelkuaididanhao" runat="server" Text='<%# Bind("kuaididanhao") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="快递公司">
                        <ItemTemplate>
                            <asp:Label ID="Labelkuaidicompany" runat="server" Text='<%# Bind("kuaidicompany") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <%--   <asp:TemplateField HeaderText="kuaidiquerylink">
                        <ItemTemplate>
                            <asp:Label ID="Labelkuaidiquerylink" runat="server" Text='<%# Bind("kuaidiquerylink") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle  HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="10" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  ></webdiyer:AspNetPager>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
