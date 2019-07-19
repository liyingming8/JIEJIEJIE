<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MSnsInfo.aspx.cs" Inherits="TJ_MSnsInfo" %>
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
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="FHpici">发货批次</asp:ListItem>
                                             <asp:ListItem Value="DTime">时间</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a  href="TJ_MSnsInfoAddEdit.aspx?cmd=add" ><img title="添加" src="images/add.png" border="0" /></a></td></tr></table>
                     <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="ID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="TJ_MSnsInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />   
                        <asp:TemplateField HeaderText="发货批次">
                            <ItemTemplate>
                                <asp:Label ID="LabelPID" runat="server" Text='<%# Bind("FHpici") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="使用日期">
                            <ItemTemplate>
                                <asp:Label ID="LabelBGType" runat="server" Text='<%# Bind("DTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="使用药物">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtYW" TextMode="MultiLine" Text= '<%# Bind("UseYW") %>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="使用肥料">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtYW1" TextMode="MultiLine" Text= '<%# Bind("UserFL") %>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="防治方式">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtYW2" TextMode="MultiLine" Text= '<%# Bind("StyleFZ") %>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="相关措施">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtYW3" TextMode="MultiLine" Text= '<%# Bind("StyleXG") %>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarke") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>