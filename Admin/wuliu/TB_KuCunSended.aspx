﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_KuCunSended.aspx.cs" Inherits="Admin_TB_KuCunSended" %>
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
                                             <asp:ListItem Value="ID">ID</asp:ListItem>
                                             <asp:ListItem Value="Send_Time">Send_Time</asp:ListItem>
                                             <asp:ListItem Value="Contents">Contents</asp:ListItem>
                                             <asp:ListItem Value="Remarks">Remarks</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TB_KuCunSendedAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="TB_KuCunSendedAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="ID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtID" runat="server"  MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Send_Time">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSend_Time" runat="server"  MaxLength="16"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelSend_Time" runat="server" Text='<%# Bind("Send_Time") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contents">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtContents" runat="server"  MaxLength="200"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelContents" runat="server" Text='<%# Bind("Contents") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CompID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCompID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("CompID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelCompID" runat="server" Text='<%# Bind("CompID") %>'></asp:Label>
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