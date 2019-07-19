<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_StoreHousejg.aspx.cs" Inherits="Admin_TB_StoreHousejg" %>
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
                                             <asp:ListItem Value="StoreHouseCode">库房编码</asp:ListItem>
                                             <asp:ListItem Value="StoreHouseName">库房名称</asp:ListItem>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                             <asp:ListItem Value="AddressString">地址</asp:ListItem>
                                             <asp:ListItem Value="Contractor">联系人</asp:ListItem>
                                             <asp:ListItem Value="TelPhoneNumber">电话</asp:ListItem>
                                             <asp:ListItem Value="MobilePhone">手机</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TB_StoreHouseAddEditjg.aspx?cmd=add"><img title="添加" src="../../images/add.png" border="0"></a></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="STID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="STID" DataNavigateUrlFormatString="TB_StoreHouseAddEditjg.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="库房编号">
                            <ItemTemplate>
                                <asp:Label ID="LabelStoreHouseCode" runat="server" Text='<%# Bind("StoreHouseCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="库房名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStoreHouseName" runat="server"  MaxLength="50" Text='<%# Bind("StoreHouseName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelStoreHouseName" runat="server" Text='<%# Bind("StoreHouseName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>        
                        <asp:TemplateField HeaderText="省份城市">           
                            <ItemTemplate>
                                <asp:Label ID="LabelCID" runat="server" Text='<%# commfun.ReturnBaseClassName(Eval("CID").ToString(), true, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:TemplateField HeaderText="地址">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAddressString" runat="server"  MaxLength="200" Text='<%# Bind("AddressString") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAddressString" runat="server" Text='<%# Bind("AddressString") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系人">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtContractor" runat="server"  MaxLength="50" Text='<%# Bind("Contractor") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelContractor" runat="server" Text='<%# Bind("Contractor") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电话">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTelPhoneNumber" runat="server"  MaxLength="50" Text='<%# Bind("TelPhoneNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelTelPhoneNumber" runat="server" Text='<%# Bind("TelPhoneNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="手机">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMobilePhone" runat="server"  MaxLength="50" Text='<%# Bind("MobilePhone") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelMobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>       
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"  MaxLength="50" Text='<%# Bind("Remarks") %>'></asp:TextBox>
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