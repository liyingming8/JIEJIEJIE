<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterInnerAgent.aspx.cs" Inherits="Admin_TJ_RegisterInnerAgent" %>
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
                                             <asp:ListItem Text="名称" Value="CompName"></asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" placeholder="请输入查找内容"  /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_RegisterInnerAgentAddEdit.aspx?cmd=add"><img title="添加" src="../images/add.png" border="0"></a></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                              AutoGenerateColumns="False" DataKeyNames="CompID"
                              onpageindexchanging="GridView1_PageIndexChanging"   
                              onrowcancelingedit="GridView1_RowCancelingEdit"  
                              onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                              onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                              CssClass="GridViewStyle" PageSize="18" >
                    <Columns>
                        <%--<asp:HyperLinkField DataNavigateUrlFields="CompID" Text="生成" DataNavigateUrlFormatString="QrProduce.aspx?ID={0}&tp=smck" Target="_blank" HeaderText="盟友验证QR" />--%>
                        <asp:HyperLinkField DataNavigateUrlFields="CompID" DataNavigateUrlFormatString="TJ_RegisterInnerAgentAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="经销商名称">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelCompName" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>           
                        <asp:TemplateField HeaderText="联系人">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelLegalPerson" runat="server" 
                                           Text='<%# Bind("LegalPerson") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="省份城市">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelCTID" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("CTID").ToString(), true, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="地址">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelAddress" runat="server" Text='<%# ((Eval("Address").ToString().Length > 15) ? Eval("Address").ToString().Substring(0, 14) : Eval("Address").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电话">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelTelNumber" runat="server" Text='<%# Bind("TelNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>     
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>