<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_LotteryActivityDetail.aspx.cs" Inherits="Admin_TJ_LotteryActivityDetail" %>
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
                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="LADID">LADID</asp:ListItem>
                                             <asp:ListItem Value="GradeName">GradeName</asp:ListItem>
                                             <asp:ListItem Value="AwardName">AwardName</asp:ListItem>
                                             <asp:ListItem Value="AwardPictrureURL">AwardPictrureURL</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_LotteryActivityDetailAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="LADID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="LADID" DataNavigateUrlFormatString="TJ_LotteryActivityDetailAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="LADID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLADID" runat="server"  MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelLADID" runat="server" Text='<%# Bind("LADID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LAID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLAID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("LAID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelLAID" runat="server" Text='<%# Bind("LAID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GradeName">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtGradeName" runat="server"  MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelGradeName" runat="server" Text='<%# Bind("GradeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GradeID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtGradeID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("GradeID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelGradeID" runat="server" Text='<%# Bind("GradeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Numbers">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNumbers" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("Numbers") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelNumbers" runat="server" Text='<%# Bind("Numbers") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AwardName">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAwardName" runat="server"  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAwardName" runat="server" Text='<%# Bind("AwardName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AwardPictrureURL">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAwardPictrureURL" runat="server"  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelAwardPictrureURL" runat="server" Text='<%# Bind("AwardPictrureURL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
              </ContentTemplate>
                   </asp:UpdatePanel> 
     
             </div>

        </form>
    </body>
</html>