<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DKWXJPinfo.aspx.cs" Inherits="Admin_jg_TJ_DKWXJPinfo" %>
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

                                             <asp:ListItem Value="BoxLabel">标签序号</asp:ListItem>
                                             <%--<asp:ListItem Value="PassWords">PassWords</asp:ListItem>
<asp:ListItem Value="NickName">NickName</asp:ListItem>
<asp:ListItem Value="RegisterDate">RegisterDate</asp:ListItem>
<asp:ListItem Value="IsActived">IsActived</asp:ListItem>
<asp:ListItem Value="SystemPermission">SystemPermission</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>--%>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_UserAddEdit.aspx?cmd=add" style="display: none"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                                      AutoGenerateColumns="False" DataKeyNames="ID,CompID" onpageindexchanging="GridView1_PageIndexChanging"   
                                      onrowcancelingedit="GridView1_RowCancelingEdit"  
                                      onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                                      onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                                      CssClass="GridViewStyle" PageSize="18" >
                            <Columns>             
                                <asp:TemplateField HeaderText="标签序号">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBoxLabel" runat="server" Text='<%# Bind("BoxLabel") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="验证码">  
                            
                                    <ItemTemplate>
                                        <asp:Label ID="LabelYZM" runat="server" Text='<%# Bind("YZM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>               
                                <asp:TemplateField HeaderText="所中奖项">                    
                                    <ItemTemplate>
                                        <asp:Label ID="JXname" runat="server" Text='<%# Bind("JXname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                
                                <asp:TemplateField HeaderText="奖品">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelJPType" runat="server" Text='<%# Bind("JPType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="中奖人">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWXname" runat="server" Text='<%# Bind("WXname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="领取时间">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWXname" runat="server" Text='<%# Bind("LQtime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="备注">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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