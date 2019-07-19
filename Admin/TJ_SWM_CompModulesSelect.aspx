<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_CompModulesSelect.aspx.cs" Inherits="Admin_TJ_SWM_CompModulesSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" />
    <style type="text/css">
        .searchbox {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_WholePage">
        <asp:GridView ID="GridView1" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="功能模块">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxpg" Width="100"  CssClass="searchbox" runat="server" Text='<%# Bind("pg") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Labelpg" runat="server" Text='<%# Bind("pg") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="显示">
                    <EditItemTemplate>
                        <asp:CheckBox ID="ckb_show_edit" runat="server"></asp:CheckBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ckb_show" Enabled="False" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True"  >
                <ItemStyle HorizontalAlign="Center" />
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
    </div>
    </form>
</body>
</html>
