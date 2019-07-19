<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CY_Product.aspx.cs" Inherits="CY_Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
         <link href="../../include/bootstrap.min.css" rel="stylesheet" /> 
            <link href="../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td style="width: 80px" align="left" class="tdbg"><b>搜索选项：</b></td>
                    <td class="tdbg">
                        <asp:DropDownList ID="DDLField" runat="server">

                            <asp:ListItem Value="ProName">产品名称</asp:ListItem>
                        </asp:DropDownList>
                       包含<input id="inputSearchKeyword" type="text" runat="server" class="inputtext" />&nbsp;<asp:Button ID="BtnSearch0" runat="server" Text="模糊查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                    <td class="tdbg">相关操作:</td>
                
                    <td class="topitem"><input type="button" value="新增" class="btn btn-default btnyd" onclick="openWinCenter('CY_ProductAdd.aspx?cmd=add', 900, 500, '经销商信息')" /></td>
                </tr>
            </table>
                
            <br />
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False" DataKeyNames="ProID"
                 OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="ProID" DataNavigateUrlFormatString="CY_ProductAdd.aspx?cmd=edit&ID={0}" Text="详细" />

                    <%--   <asp:TemplateField>
            <ItemTemplate>
                 <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# XiangXiLinkString(Eval("ID").ToString()) %>' Text="详细"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="产品名称">
                        <ItemTemplate>
                            <asp:Label ID="LName" runat="server" Text='<%# Bind("ProName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品价格">
                        <ItemTemplate>
                            <asp:Label ID="LPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="图片">
                        <ItemTemplate>
                            <asp:Label ID="Limg" runat="server" Text='<%# Bind("ProImg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="显示">
                        <ItemTemplate>
                            <asp:Label ID="Lshow" runat="server" Text='<%# (Eval("UseFlag").ToString().Equals("True")?"显示":"不显示") %>'></asp:Label>
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
            <script src="../include/js/jquery.min.js" type="text/javascript"></script> 
        <script type="text/javascript" src="../include/js/UploadImage.js"></script>
           <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
</body>
</html>
