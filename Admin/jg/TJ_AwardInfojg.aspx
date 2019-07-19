<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardInfojg.aspx.cs" Inherits="Admin_TJ_AwardInfojg" %>
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
                                             <%--<asp:ListItem Value="AWID">AWID</asp:ListItem>--%>
                                             <asp:ListItem Value="AwardThing">奖品</asp:ListItem>
                                             <%--<asp:ListItem Value="ImageURLString">ImageURLString</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="SImageURLString">奖品图片</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="Contents">Contents</asp:ListItem>--%>

                                             <%--<asp:ListItem Value="Remarks">Remarks</asp:ListItem>--%>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_AwardInfoAddEditjg.aspx?cmd=add"><img title="添加" src="../images/add.png" border="0"></a></td></tr></table>
                   <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="AWID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="AWID" DataNavigateUrlFormatString="TJ_AwardInfoAddEditjg.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="活动">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelAGID" runat="server" Text='<%# ReturnIntegalName(Eval("AGID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所需积分">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelIntegralValue" runat="server" Text='<%# Bind("IntegralValue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖品">                    
                            <ItemTemplate>                        
                                <asp:Label ID="LabelAwardThing" runat="server" Text='<%# Bind("AwardThing") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                
                        <asp:TemplateField HeaderText="奖品图片">
                            <ItemTemplate>
                                <asp:Image ID="imageSmallURLString" runat="server" ImageUrl='<%# Eval("SImageURLString") %>' />                       
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖品介绍">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelContents" runat="server" Text='<%# Bind("Contents") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发布时间">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelPublishDate" runat="server" Text='<%# Bind("PublishDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
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