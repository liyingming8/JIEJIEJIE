<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_JPinfo.aspx.cs" Inherits="Admin_TJ_JPinfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <%--<asp:ListItem Value="JPID">JPID</asp:ListItem>--%>
                                             <asp:ListItem Value="DjdName">兑奖点名称</asp:ListItem>
                                             <asp:ListItem Value="JpName">奖品名称</asp:ListItem>
                                             <%--<asp:ListItem Value="CreatTime">CreatTime</asp:ListItem>
<asp:ListItem Value="DelFlag">DelFlag</asp:ListItem>--%>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputtext" placeholder="请输入查找内容" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_JPinfo.aspx"><img title="返回" src="images/back.png" border="0"></a></td><td><asp:Button ID="BtnSearch1" runat="server" Text="导出Excel数据" CssClass="inputbutton" OnClick="BtnSearch2_Click" Width="156px"  /></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="JPID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>

               
                        <asp:TemplateField HeaderText="JxID" Visible ="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJxID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("JxID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelJxID" runat="server" Text='<%# Bind("JxID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑奖点名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDjdName" runat="server" Text='<%# Bind("DjdName") %>'  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDjdName" runat="server" Text='<%# Bind("DjdName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖品名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJpName" runat="server"  Text='<%# Bind("JpName") %>'  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelJpName" runat="server" Text='<%# Bind("JpName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="配发数量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPFCount" runat="server"  onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("PFCount") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelPFCount" runat="server" Text='<%# Bind("PFCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑换数量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDHCount" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("DHCount") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDHCount" runat="server" Text='<%# Bind("DHCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="核销数量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHXCount" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("HXCount") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelHXCount" runat="server" Text='<%# Bind("HXCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="剩余数量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSYCount" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("SYCount") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelSYCount" runat="server" Text='<%# Bind("SYCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建时间">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCreatTime" runat="server"  Text='<%# Bind("CreatTime") %>'  MaxLength="16"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelCreatTime" runat="server" Text='<%# Bind("CreatTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="compID" Visible ="false"  >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtcompID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("compID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelcompID" runat="server" Text='<%# Bind("compID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DelFlag" Visible ="false" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDelFlag" runat="server"  MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDelFlag" runat="server" Text='<%# Bind("DelFlag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>