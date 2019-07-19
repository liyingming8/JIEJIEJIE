<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_ZhiJianYuan.aspx.cs" Inherits="Admin_TB_ZhiJianYuan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td>
                            <asp:DropDownList ID="DDLField" runat="server">
                           
                                <asp:ListItem Value="ZJname">质检员名称</asp:ListItem>
                            
                        
                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                        <td>相关操作:</td>
                        <td><a onclick=" openWinCenter('TB_ZhiJianYuanAddEdit.aspx?cmd=add', 600, 300) ">
                                             <img title="添加" src="../../images/add.png" border="0" /></a></td>
                    </tr>
                </table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ZJID"
                               OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# XiangXiLinkString(Eval("ZJID").ToString()) %>' Text="详细"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编码">
                            <ItemTemplate>
                                <asp:Label ID="LabelZJID" runat="server" Text='<%# Bind("ZJID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="质检员名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelZJname" runat="server" Text='<%# Bind("ZJname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系电话">
                            <ItemTemplate>
                                <asp:Label ID="LabelPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

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