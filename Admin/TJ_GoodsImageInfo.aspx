<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_GoodsImageInfo.aspx.cs" Inherits="Admin_TJ_GoodsImageInfo" %>
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
                <div class="div_Nav">
                <table class="table_Nav" >
                    <tr> 
                         <asp:DropDownList ID="DDLField" runat="server">
                                             <%--<asp:ListItem Value="GoodsIMGID">GoodsIMGID</asp:ListItem>
<asp:ListItem Value="Show">Show</asp:ListItem>
<asp:ListItem Value="ImagePathString">ImagePathString</asp:ListItem>--%>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList></td>
                        <td class="td_Nav">
                 包含 <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputtext" /> <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td>
              <td class="td1">相关操作 :  <input type="button" value="新增"  onclick="openWinCenter('TJ_GoodsImageInfoAddEdit.aspx?cmd=add', 600, 400, '产品上架图片管理')" /></td></tr></table>
                    </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="GoodsIMGID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" EnableModelValidation="True" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="GoodsIMGID" DataNavigateUrlFormatString="TJ_GoodsImageInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="产品">
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsID" runat="server" Text='<%# bgoods.GetList(Convert.ToInt32(HF_GoodID.Value)).GoodsName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="显示">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelShow" runat="server" Text='<%# Bind("Show") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ImageField DataImageUrlField="SMImagePathString" HeaderText="产品图像" ItemStyle-Width="100px" ControlStyle-Width="100px">                   
                        </asp:ImageField>
                        <asp:TemplateField HeaderText="显示顺序">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>'  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
            <script src="../include/js/UploadImage.js" type="text/javascript"></script>
            <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>