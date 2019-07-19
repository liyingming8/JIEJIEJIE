<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MyCardAndDetails.aspx.cs" Inherits="Admin_TJ_MyCardAndDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto">
                        <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False"
                            CssClass="GridViewStyle" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="">
                            <Columns>
                                <asp:TemplateField HeaderText="卡片主题">
                                    <ItemTemplate>
                                        <asp:Label ID="AwardThing" runat="server" Text='<%# Bind("AwardThing") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="卡片类型">
                                    <ItemTemplate>
                                        <asp:Label ID="awardtype" runat="server" Text='<%# Bind("awardtype") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="卡片数量">
                                    <ItemTemplate>
                                        <asp:Label ID="num" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
