<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_PublishImageInfo.aspx.cs" Inherits="Admin_TJ_PublishImageInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
          <link href="../include/easyui.css" rel="stylesheet" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <%--<asp:ListItem Value="IPAID">IPAID</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="PathString">PathString</asp:ListItem>--%>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><%--<td>相关操作:</td><td><a href="TJ_PublishImageInfoAddEdit.aspx?cmd=add&IFID=<%= HF_IFID.Value %>"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>--%> <td class="td1">相关操作:<input type="button" value="新增" onclick="openWinCenter('TJ_PublishImageInfoAddEdit.aspx?cmd=add', 400, 300, '图片组编辑')" /></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="IPAID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="IPAID" DataNavigateUrlFormatString="TJ_PublishImageInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="咨询">              
                            <ItemTemplate>
                                <asp:Label ID="LabelIFID" runat="server" Text='<%# GetPublishName(Eval("IFID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="图片">                  
                            <ItemTemplate>
                                <asp:Image ID="pubpicture" runat="server" Width="60px" ImageUrl='<%# Eval("SMPathString").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="显示顺序">   
                            <EditItemTemplate>
                                <asp:TextBox ID="txtShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>'></asp:TextBox>
                            </EditItemTemplate>                 
                            <ItemTemplate>
                                <asp:Label ID="LabelShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">     
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
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
                <asp:HiddenField ID="HF_IFID" runat="server" />    </div>
        </form>
         <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>