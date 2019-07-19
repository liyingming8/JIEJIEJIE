<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UserAccumulating.aspx.cs" Inherits="Admin_TJ_UserAccumulating" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td>
                            <asp:DropDownList ID="DDLField" runat="server">
                                <asp:ListItem Value="UID">会员</asp:ListItem>
                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                  
                  
                    </tr>
                </table>　
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="UACID"
                                 OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                 
                        <asp:TemplateField HeaderText="会员">

                            <ItemTemplate>
                                <asp:Label ID="LabelUID" runat="server" Text='<%# buser.GetList(int.Parse(Eval("UID").ToString())).NickName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                 
                        <asp:TemplateField HeaderText="积分值">
                            <ItemTemplate>
                                <asp:Label ID="LabelAccumulating" runat="server" Text='<%# Bind("Accumulating") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="详情">
                            <ItemTemplate  >
                                <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# JiageLinkString(Eval("UID").ToString()) %>' Text='详细'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    
                    <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="left" />
                
                </asp:GridView></div>
            </div>
        </form>
    </body>
</html>