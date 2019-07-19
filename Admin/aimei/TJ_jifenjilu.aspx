<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_jifenjilu.aspx.cs" Inherits="Admin_TJ_jifenjilu" %>
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
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="GoodsName">产品名称</asp:ListItem>
                                             <asp:ListItem Value="Descriptions">描述</asp:ListItem>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_GoodsInfoAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" PageSize="18" Width="100%" AutoGenerateColumns="False" DataKeyNames="GoodsID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" EnableModelValidation="True" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="GoodsID" DataNavigateUrlFormatString="TJ_GoodsInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />                
                        <asp:TemplateField HeaderText="所属公司">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelCompID" runat="server" Text='<%# ReturnCompanyName(Eval("CompID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="网点">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelMeshPointID" runat="server" Text='<%# ReturnMeshPointName(Eval("MSPID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="产品类别">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsTypeID" runat="server" Text='<%# ReturnGoodsTypeName(Eval("GoodsTypeID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="产品名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtGoodsName" CssClass="input_1" runat="server" Text='<%# Bind("GoodsName") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsName" runat="server" Text='<%# Bind("GoodsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>             
                        <asp:TemplateField HeaderText="参考价">
                            <EditItemTemplate>
                                ￥<asp:TextBox ID="txtPrice" runat="server" CssClass="input4" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("Price") %>' MaxLength="20"></asp:TextBox>/<asp:TextBox  CssClass="input4" ID="txtSaleUnit" Text='<%# Bind("SaleUnitID") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                ￥<asp:Label ID="LabelPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>/<asp:Label ID="labSaleUnit" Text='<%# Bind("SaleUnitID") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="上架时间">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelBeginSaleDate" runat="server" Text='<%# Bind("BeginSaleDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>       
                        <asp:TemplateField HeaderText="下架时间">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelEndSaleDate" runat="server" Text='<%# Bind("EndSaleDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="首页显示">
                            <EditItemTemplate>
                                <asp:CheckBox ID="eshowhomepage" Checked='<%# Bind("ShowHomePage") %>' runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="showhomepage" Checked='<%# Bind("ShowHomePage") %>' Enabled="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>       
                        <asp:TemplateField HeaderText="推荐">
                            <EditItemTemplate>
                                <asp:CheckBox ID="eRecmmand" Checked='<%# Bind("Recmmand") %>' runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Recmmand" Checked='<%# Bind("Recmmand") %>' Enabled="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>         
                        <asp:TemplateField HeaderText="热销">
                            <EditItemTemplate>
                                <asp:CheckBox ID="eHot" Checked='<%# Bind("Hot") %>' runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Hot" Checked='<%# Bind("Hot") %>' Enabled="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>         
                        <asp:HyperLinkField DataNavigateUrlFields="GoodsID" Text="管理" DataNavigateUrlFormatString="TJ_GoodsImageInfo.aspx?GoodsID={0}" HeaderText="系列图片" />
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" CssClass="input_1" runat="server" Text='<%# Bind("Remarks") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>