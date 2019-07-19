<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterInnerImageInfo.aspx.cs" Inherits="Admin_TJ_RegisterInnerImageInfo" %>
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
                                             <asp:ListItem Value="GoodsIMGID">GoodsIMGID</asp:ListItem>
                                             <asp:ListItem Value="Show">Show</asp:ListItem>
                                             <asp:ListItem Value="ImagePathString">ImagePathString</asp:ListItem>
                                             <asp:ListItem Value="Remarks">Remarks</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_RegisterInnerCompanyImageInfoAddEdit.aspx?cmd=add&RCompID=<%= HF_GoodID.Value %>"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="GoodsIMGID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" EnableModelValidation="True" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="GoodsIMGID" DataNavigateUrlFormatString="TJ_RegisterInnerCompanyImageInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="商盟">
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsID" runat="server" Text='<%# bcompany.GetList(Convert.ToInt32(HF_GoodID.Value)).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>              
                        <asp:ImageField DataImageUrlField="ImagePathString" HeaderText="产品图像" ItemStyle-Width="100px" ControlStyle-Width="100px">                   
                        </asp:ImageField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
            <asp:HiddenField ID="HF_GoodID" runat="server" />
        </form>
    </body>
</html>