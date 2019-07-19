<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Userjg.aspx.cs" Inherits="Admin_TJ_Userjg" %>
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

                                             <asp:ListItem Value="LoginName">用户名</asp:ListItem>
                                             <%--<asp:ListItem Value="PassWords">PassWords</asp:ListItem>
<asp:ListItem Value="NickName">NickName</asp:ListItem>
<asp:ListItem Value="RegisterDate">RegisterDate</asp:ListItem>
<asp:ListItem Value="IsActived">IsActived</asp:ListItem>
<asp:ListItem Value="SystemPermission">SystemPermission</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>--%>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_UserAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                                      AutoGenerateColumns="False" DataKeyNames="UserID,CompID" onpageindexchanging="GridView1_PageIndexChanging"   
                                      onrowcancelingedit="GridView1_RowCancelingEdit"  
                                      onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                                      onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                                      CssClass="GridViewStyle" PageSize="18" >
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="TJ_UserAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />               
                                <asp:TemplateField HeaderText="所属单位">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCompID" runat="server" Text='<%# GetCompanyNameByID(Eval("CompID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="系统角色">  
                                    <EditItemTemplate>
                                        <asp:DropDownList DataTextField="RoleName" DataValueField="RID" DataSource='<%# GetRoleInfo() %>' ID="ddl_role" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>               
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRID" runat="server" Text='<%# ReturnRoleNameByRID(Eval("RID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>               
                                <asp:TemplateField HeaderText="用户名">                    
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                
                                <asp:TemplateField HeaderText="昵称">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNickName" runat="server" Text='<%# Bind("NickName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="注册时间">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRegisterDate" runat="server" Text='<%# Bind("RegisterDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="激活">  
                                    <EditItemTemplate>
                                        <asp:RadioButtonList ID="radiolist" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="未激活" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="激活" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="试用" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="冻结" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </EditItemTemplate>                  
                                    <ItemTemplate>
                                        <asp:Label ID="LabelIsActived" runat="server" Text='<%# ReturnUserStatu(Eval("IsActived")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="来自">                    
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFromCityID" runat="server" Text='<%# ReturnPlaceByID(Eval("FromCityID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>               
                                <asp:TemplateField HeaderText="备注">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </form>
    </body>
</html>