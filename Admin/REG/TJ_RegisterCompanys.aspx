<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterCompanys.aspx.cs" Inherits="Admin_REG_TJ_RegisterCompanys" %>
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
                                             <asp:ListItem Value="CompName">单位名称</asp:ListItem>
                                             <asp:ListItem Value="LegalPerson">法人</asp:ListItem>
                                             <asp:ListItem Value="Address">地址</asp:ListItem>
                                             <asp:ListItem Value="TelNumber">电话</asp:ListItem>
                                             <asp:ListItem Value="FaxNumber">传真</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_RegisterCompanysAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                              AutoGenerateColumns="False" DataKeyNames="CompID"
                              onpageindexchanging="GridView1_PageIndexChanging"   
                              onrowcancelingedit="GridView1_RowCancelingEdit"  
                              onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                              onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                              CssClass="GridViewStyle" PageSize="18" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="CompID" DataNavigateUrlFormatString="TJ_RegisterCompanysAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="公司类别">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelCompTypeID" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("CompTypeID").ToString(), false, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="公司名称">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelCompName" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品类别">                    
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("ProductTypeID").ToString(), false, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="法人">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelLegalPerson" runat="server" 
                                           Text='<%# Bind("LegalPerson") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="地址">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="城市">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelCTID" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("CTID").ToString(), true, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="传真">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelFaxNumber" runat="server" Text='<%# Bind("FaxNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电话">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelTelNumber" runat="server" Text='<%# Bind("TelNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电子邮箱">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelZhuCeZiJin" runat="server" Text='<%# Bind("EMail") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="注册资金">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelEMail" runat="server" Text='<%# Bind("ZhuCeZiJin") %>'></asp:Label>万
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="注册日期">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelRegisterDate" runat="server" 
                                           Text='<%# Bind("RegisterDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐号">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelAccountNumber" runat="server" 
                                           Text='<%# Bind("AccountNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
            <asp:HiddenField ID="HF_TempAgentCompIDString" runat="server" />
            <asp:HiddenField ID="HF_TempChildCompIDString" runat="server" />
        </form>
    </body>
</html>