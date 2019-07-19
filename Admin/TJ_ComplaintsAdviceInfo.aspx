<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ComplaintsAdviceInfo.aspx.cs" Inherits="Admin_TJ_ComplaintsAdviceInfo" %>
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
                                             <asp:ListItem Value="CAID">CAID</asp:ListItem>
                                             <asp:ListItem Value="ComplainsAdviceContents">ComplainsAdviceContents</asp:ListItem>
                                             <asp:ListItem Value="PublishDate">PublishDate</asp:ListItem>
                                             <asp:ListItem Value="MobilePhone">MobilePhone</asp:ListItem>
                                             <asp:ListItem Value="Name">Name</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td class="tdbg" >
                                                                                                                                                                                                                                            ;</td><td></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                              AutoGenerateColumns="False" DataKeyNames="CAID"
                              onpageindexchanging="GridView1_PageIndexChanging"   
                              onrowcancelingedit="GridView1_RowCancelingEdit"  
                              onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                              onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                              CssClass="GridViewStyle" PageSize="20" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="CAID" DataNavigateUrlFormatString="TJ_ComplaintsAdviceInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />              
                        <asp:TemplateField HeaderText="原因">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelCID" runat="server" Text='<%# ReturnBaseName(Eval("CID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所在城市">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlaceID" runat="server" Text='<%# ReturnBaseName(Eval("PlaceID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="内容">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelComplainsAdviceContents" runat="server" Text='<%# Bind("ComplainsAdviceContents") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelPublishDate" runat="server" Text='<%# Bind("PublishDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="手机号码">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelMobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="姓名">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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