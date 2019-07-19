<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Agents_Inforjg.aspx.cs" Inherits="Admin_TB_Agents_Inforjg" %>
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
                                             <asp:ListItem Value="CompName">经销商</asp:ListItem>
                                             <asp:ListItem Value="LegalPerson">联系人</asp:ListItem>
                                             <asp:ListItem Value="TelNumber">电话</asp:ListItem>
                                             <asp:ListItem Value="MobilePhoneNumber">手机</asp:ListItem>
                                             <asp:ListItem Value="AllowAreaInfo">授权区域</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><input type="button" value="新增" class="btn btn-warning" onclick="openWinCenter('TB_Agents_InforAddEditjg.aspx?cmd=add', 400, 300, '系统角色')" /></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" PageSize="20"  Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="CompID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="CompID" DataNavigateUrlFormatString="TB_Agents_InforAddEditjg.aspx?cmd=edit&ID={0}" Text="详细" />                               
                        <asp:TemplateField HeaderText="省份城市">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelCID" runat="server" Text='<%# commfun.ReturnBaseClassName(Eval("CTID").ToString(), true, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="经销商名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAgent_Name" runat="server"  Text='<%# Bind("CompName") %>' MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAgent_Name" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编码">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelAgent_Code" runat="server" Text='<%# Bind("Agent_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="地址">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAddressString" runat="server"  Text='<%# Bind("Address") %>'  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAddressString" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系人">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMiddleman" runat="server"  Text='<%# Bind("LegalPerson") %>'  MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelMiddleman" runat="server" Text='<%# Bind("LegalPerson") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电话">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTelephone" runat="server" Text='<%# Bind("TelNumber") %>'  MaxLength="40"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelTelephone" runat="server" Text='<%# Bind("TelNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="手机">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMobiePhone" runat="server" Text='<%# Bind("MobilePhoneNumber") %>'  MaxLength="40"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelMobiePhone" runat="server" Text='<%# Bind("MobilePhoneNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>         
                        <asp:TemplateField HeaderText="授权区域">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAllowAreaInfo" runat="server" Text='<%# Bind("AllowAreaInfo") %>'  MaxLength="200"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAllowAreaInfo" runat="server" Text='<%# Bind("AllowAreaInfo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
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