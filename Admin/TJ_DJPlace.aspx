<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DJPlace.aspx.cs" Inherits="Admin_TJ_DJPlace" %>
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
                        <td><asp:DropDownList ID="DDLField" runat="server" >
                                             <%--<asp:ListItem Value="DjdID">DjdID</asp:ListItem>--%>
                                             <asp:ListItem Value="DjdName">兑奖点名称</asp:ListItem>
                                             <%--<asp:ListItem Value="DjZH">DjZH</asp:ListItem>
<asp:ListItem Value="YanZM">YanZM</asp:ListItem>
<asp:ListItem Value="JxName">JxName</asp:ListItem>
<asp:ListItem Value="LXname">LXname</asp:ListItem>--%>
                                             <asp:ListItem Value="MPhone">联系电话</asp:ListItem>
                                             <%--<asp:ListItem Value="Address">Address</asp:ListItem>
<asp:ListItem Value="DjGrade">DjGrade</asp:ListItem>
<asp:ListItem Value="DelFlag">DelFlag</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>--%>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_DJPlaceAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td><td><asp:Button ID="BtnSearch1" runat="server" Text="导出Excel数据" CssClass="inputbutton" OnClick="BtnSearch2_Click" Width="156px"  /></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="DjdID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="DjdID" DataNavigateUrlFormatString="TJ_DJPlaceAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
               
                        <asp:TemplateField HeaderText="兑奖点名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDjdName" runat="server" Text='<%# Bind("DjdName") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDjdName" runat="server" Text='<%# Bind("DjdName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑奖帐号">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDjZH" runat="server" Text='<%# Bind("DjZH") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDjZH" runat="server" Text='<%# Bind("DjZH") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="验证码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtYanZM" runat="server" Text='<%# Bind("YanZM") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelYanZM" runat="server" Text='<%# Bind("YanZM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖项名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJxName" runat="server" Text='<%# Bind("JxName") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelJxName" runat="server" Text='<%# Bind("JxName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖项数量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJxCount" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("JxCount") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelJxCount" runat="server" Text='<%# Bind("JxCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系人">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLXname" runat="server" Text='<%# Bind("LXname") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelLXname" runat="server" Text='<%# Bind("LXname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系电话">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMPhone" runat="server" Text='<%# Bind("MPhone") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelMPhone" runat="server" Text='<%# Bind("MPhone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="详细地址">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑奖点等级">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDjGrade" runat="server" Text='<%# Bind("DjGrade") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDjGrade" runat="server" Text='<%# Bind("DjGrade") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DelFlag" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDelFlag" runat="server"  MaxLength="2"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDelFlag" runat="server" Text='<%# Bind("DelFlag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PlaceID"  Visible ="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPlaceID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("PlaceID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelPlaceID" runat="server" Text='<%# Bind("PlaceID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CompID" Visible="false"  >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCompID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("CompID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelCompID" runat="server" Text='<%# Bind("CompID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' MaxLength="50"></asp:TextBox>
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
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>