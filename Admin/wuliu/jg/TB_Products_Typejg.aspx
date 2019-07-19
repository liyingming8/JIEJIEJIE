<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_Typejg.aspx.cs" Inherits="Admin_TB_Products_Typejg" %>
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
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="TypeCode">类别编码</asp:ListItem>
                                             <asp:ListItem Value="TypeName">类别名称</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TB_Products_TypeAddEditjg.aspx?cmd=add"><img title="添加" src="../../images/add.png" border="0"></a></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="TypeId"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="TypeId" DataNavigateUrlFormatString="TB_Products_TypeAddEditjg.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="父级">
                            <ItemTemplate>
                                <asp:Label ID="LabelParentID" runat="server" Text='<%# (Eval("ParentID").ToString().Equals("0") ? "" : bll.GetList(Convert.ToInt32(Eval("ParentID").ToString())).TypeName) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="类别编码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTypeCode" runat="server" Text='<%# Bind("TypeCode") %>'> MaxLength="60"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelTypeCode" runat="server" Text='<%# Bind("TypeCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="类别名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTypeName" runat="server" Text='<%# Bind("TypeName") %>'  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                
                        <%--<asp:CommandField ShowEditButton="True" />--%>
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