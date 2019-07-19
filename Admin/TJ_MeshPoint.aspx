<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MeshPoint.aspx.cs" Inherits="Admin_TJ_MeshPoint" %>
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
                                             <asp:ListItem Value="MeshPointName">网点名称</asp:ListItem>
                                             <asp:ListItem Value="Contracter">联系人</asp:ListItem>
                                             <asp:ListItem Value="AddressInfo">地址</asp:ListItem>
                                             <asp:ListItem Value="Remark">备注</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_MeshPointAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="PID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="PID" DataNavigateUrlFormatString="TJ_MeshPointAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />               
                        <asp:TemplateField HeaderText="所属城市">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlaceID" runat="server" Text='<%# ReturnCityName(Eval("PlaceID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="网点名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelMeshPointName" runat="server" Text='<%# Bind("MeshPointName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系人">
                            <ItemTemplate>
                                <asp:Label ID="LabelContracter" runat="server" Text='<%# Bind("Contracter") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电话">
                            <ItemTemplate>
                                <asp:Label ID="LabelTel" runat="server" Text='<%# Bind("Tel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="传真">
                            <ItemTemplate>
                                <asp:Label ID="LabelFax" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                
                        <asp:TemplateField HeaderText="详细地址">
                            <ItemTemplate>
                                <asp:Label ID="LabelAddressInfo" runat="server" Text='<%# Bind("AddressInfo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="排列顺序">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>' MaxLength="200"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>              
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemark" runat="server"  Text='<%# Bind("Remark") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="PID" Text="管理" DataNavigateUrlFormatString="TJ_MeshPointImageInfo.aspx?MSID={0}" HeaderText="系列图片" />
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