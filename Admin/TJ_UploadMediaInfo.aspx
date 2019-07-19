<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UploadMediaInfo.aspx.cs" Inherits="Admin_TJ_UploadMediaInfo" %>
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
                                             <asp:ListItem Value="Introductions">说明</asp:ListItem>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td class="tdbg" ></td><td></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="UPLID"
                              onpageindexchanging="GridView1_PageIndexChanging"  Width="100%"  onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:TemplateField HeaderText="用户">
                            <ItemTemplate>
                                <asp:Label ID="LabelUID" runat="server" Text='<%# buser.GetList(Convert.ToInt32(Eval("UID"))).NickName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="上传时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelUploadDate" runat="server" Text='<%# Bind("UploadDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>            
                        <asp:TemplateField HeaderText="上传文件">                   
                            <ItemTemplate>
                                <img alt="img" width="60px" height="60px" src='<%# Eval("LinkURL").ToString() %>' id="fileimage" />                        
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="文件类型">        
                            <ItemTemplate>
                                <asp:Label ID="LabelMediaType" runat="server" Text='<%# Bind("MediaType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="说明">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIntroductions" runat="server"  Text='<%# Bind("Introductions") %>'  MaxLength="300"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelIntroductions" runat="server" Text='<%# Bind("Introductions") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否显示">
                            <EditItemTemplate>                
                                <asp:CheckBox ID="checkShow" runat="server" Checked='<%# Convert.ToBoolean(Eval("Show")) %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelShow" runat="server" Text='<%# (Convert.ToBoolean(Eval("Show")) ? "显示" : "隐藏") %>'></asp:Label>
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