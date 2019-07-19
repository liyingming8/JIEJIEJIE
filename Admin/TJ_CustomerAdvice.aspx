<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CustomerAdvice.aspx.cs" Inherits="Admin_TJ_CustomerAdvice" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container topdiv">
            <div class="topitem"><span></span></div>
            <div class="topitem">
                <asp:DropDownList ID="DDLField" runat="server">
                    <asp:ListItem Value="advtitle">主题</asp:ListItem>
                    <asp:ListItem Value="advcontent">内容</asp:ListItem>
                    <asp:ListItem Value="customerphone">客户电话</asp:ListItem>
                    <asp:ListItem Value="customername">客户名称</asp:ListItem>
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
                    <asp:TemplateField HeaderText="主题">
                        <ItemTemplate>
                            <asp:Label ID="Labeladvtitle" runat="server" Text='<%# Bind("advtitle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="内容">
                        <ItemTemplate>
                            <asp:Label ID="Labeladvcontent" runat="server" Text='<%# Bind("advcontent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="电话">
                        <ItemTemplate>
                            <asp:Label ID="Labelcustomerphone" runat="server" Text='<%# Bind("customerphone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="客户姓名">
                        <ItemTemplate>
                            <asp:Label ID="Labelcustomername" runat="server" Text='<%# Bind("customername") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="反馈时间">
                        <ItemTemplate>
                            <asp:Label ID="Labeladtime" runat="server" Text='<%# Bind("adtime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
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
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
