<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SYJGYS.aspx.cs" Inherits="Admin_TJ_SYJGYS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td>
                            <asp:DropDownList ID="DDLField" runat="server">
                                <%--                            <asp:ListItem Value="id">id</asp:ListItem>
<asp:ListItem Value="chanxianmingcheng">chanxianmingcheng</asp:ListItem>
<asp:ListItem Value="ZhiJianYuanMingCheng">ZhiJianYuanMingCheng</asp:ListItem>
<asp:ListItem Value="BaoZhuangXiaoZu">BaoZhuangXiaoZu</asp:ListItem>
<asp:ListItem Value="shengchanpici">shengchanpici</asp:ListItem>
<asp:ListItem Value="shengchanshijian">shengchanshijian</asp:ListItem>
<asp:ListItem Value="imageurl">imageurl</asp:ListItem>
<asp:ListItem Value="CangkuMingcheng">CangkuMingcheng</asp:ListItem>--%>
                                <asp:ListItem Value="LabelCode">标签序号</asp:ListItem>

                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table> 
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="id"
                               OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>

                        <asp:TemplateField HeaderText="标签序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelLabelCode" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产线名称">
                            <ItemTemplate>
                                <asp:Label ID="Labelchanxianmingcheng" runat="server" Text='<%# Bind("chanxianmingcheng") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="质检员">
                            <ItemTemplate>
                                <asp:Label ID="LabelZhiJianYuanMingCheng" runat="server" Text='<%# Bind("ZhiJianYuanMingCheng") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="包装小组">
                            <ItemTemplate>
                                <asp:Label ID="LabelBaoZhuangXiaoZu" runat="server" Text='<%# Bind("BaoZhuangXiaoZu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="生产批次">
                            <ItemTemplate>
                                <asp:Label ID="Labelshengchanpici" runat="server" Text='<%# Bind("shengchanpici") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="生产时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelshengchanshijian" runat="server" Text='<%# Bind("shengchanshijian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="ID" Text="管理" DataNavigateUrlFormatString="TJ_GoodsImageInfojg.aspx?GoodsID={0}" HeaderText="系列图片" />
                        <%--<asp:TemplateField HeaderText="图片管理">

                        <ItemTemplate>
                            <asp:Label ID="Labelimageurl" runat="server" Text='<%# Bind("imageurl") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelProID" runat="server"  Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓库">
                            <ItemTemplate>
                                <asp:Label ID="LabelCangkuID" runat="server" Text='<%# Bind("CangkuID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%--<asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView></div>
            </div>
        </form>
    </body>
</html>