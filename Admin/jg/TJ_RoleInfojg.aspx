<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RoleInfojg.aspx.cs" Inherits="Admin_TJ_RoleInfojg" %>
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
                                             <asp:ListItem Value="RID">角色权限ID</asp:ListItem>
                                             <asp:ListItem Value="RoleName">系统角色</asp:ListItem>
                                             <asp:ListItem Value="AuthorMenuInfo">授权菜单</asp:ListItem>
                                             <asp:ListItem Value="Remark">备注</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_RoleInfoAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="RID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="RID" DataNavigateUrlFormatString="TJ_RoleInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="系统角色">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRoleName" runat="server" Text='<%# Bind("RoleName") %>'  MaxLength="40"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="RID" DataNavigateUrlFormatString="SystemAuthorManage.aspx?RID={0}" Text="授权" HeaderText="系统目录" /> 
                        <asp:HyperLinkField DataNavigateUrlFields="RID" DataNavigateUrlFormatString="RoleAuthorManage.aspx?RID={0}" Text="授权" HeaderText="可授权角色" />
                        <asp:HyperLinkField DataNavigateUrlFields="RID" DataNavigateUrlFormatString="CompTypeIDAuthorManage.aspx?RID={0}" Text="授权" HeaderText="授权公司类别" />       
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemark" runat="server"  Text='<%# Bind("Remark") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>