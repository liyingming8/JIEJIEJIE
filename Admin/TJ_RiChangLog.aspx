<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RiChangLog.aspx.cs" Inherits="Admin_TJ_RiChangLog" %>

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

                                <asp:ListItem Value="BiaoTi">标题</asp:ListItem>

                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                        <td>相关操作:</td>
                        <td><a onclick=" openWinCenter('TJ_RiChangLogAddEdit.aspx?cmd=add', 800, 500) ">
                                             <img title="添加" src="images/add.png" border="0" /></a></td>
                    </tr>
                </table> 
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="ID"
                               OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <%--<asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="openWinCenter('TJ_RiChangLogAddEdit.aspx?cmd=edit&ID={0}',800,500)" Text="详细" />--%> 
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# XiangXiLinkString(Eval("ID").ToString()) %>' Text="详细"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标题">
                            <ItemTemplate>
                                <asp:Label ID="LabelBiaoTi" runat="server" Text='<%# Bind("BiaoTi") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="内容">
                            <ItemTemplate>
                                <asp:Label ID="LabelNeiRong" runat="server" Text='<%# Bind("NeiRong") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提交时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelTiJiaoTime" runat="server" Text='<%# Convert.ToDateTime(Eval("TiJiaoTime")).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提交人">
                            <ItemTemplate>
                                <asp:Label ID="LabeluserID" runat="server" Text='<%# buser.GetList(Convert.ToInt32(Eval("UserID").ToString())).LoginName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="是否紧急">
                    <ItemTemplate>
                        <asp:Label ID="LabelIsjinJi" runat="server" Text='<%# Bind("IsjinJi") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                        <%--   <asp:CommandField ShowEditButton="True" />--%>
                        <asp:CommandField ShowDeleteButton="True" />
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