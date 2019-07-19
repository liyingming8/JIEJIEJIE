<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customercommission.aspx.cs" Inherits="CRM_tj_crm_customercommission" %>

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
                <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('tj_crm_customercommissionAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_crm_customercommission')" /></div>--%>
            <div class="topitem"><span></span></div>
            <div class="topitem">
                <asp:DropDownList ID="DDLField" runat="server">
                    <asp:ListItem Value="id">序号</asp:ListItem>
                    <asp:ListItem Value="orderdate">订单日期</asp:ListItem>
                    <asp:ListItem Value="ordertotalprice">订单总金额</asp:ListItem>
                    <asp:ListItem Value="returnvalue">佣金总额</asp:ListItem>
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
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="订单编号">
                        <ItemTemplate>
                            <asp:Label ID="Labelorderid" runat="server" Text='<%# Bind("orderid") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下单客户">
                        <ItemTemplate>
                            <asp:Label ID="Labelordercustomerid" runat="server" Text='<%#  GetCustomerName(Eval("ordercid").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="父级">
                        <ItemTemplate>
                            <asp:Label ID="Labelparentcustomerid" runat="server" Text='<%#  GetCustomerName(Eval("getcid").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下单时间">
                        <ItemTemplate>
                            <asp:Label ID="Labelorderdate" runat="server" Text='<%# Bind("orderdate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="订单总额">
                        <ItemTemplate>
                            <asp:Label ID="Labelordertotalprice" runat="server" Text='<%# Bind("ordertotalprice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="佣金总额">
                        <ItemTemplate>
                            <asp:Label ID="Labelreturnvalue" runat="server" Text='<%# Bind("returnvalue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
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
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  ></webdiyer:AspNetPager>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
