<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Integraljg.aspx.cs" Inherits="Admin_TJ_Integraljg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                             <%--<asp:ListItem Value="ITGRID">ITGRID</asp:ListItem>--%>
                                             <asp:ListItem Value="IntegralName">活动名称</asp:ListItem>
                                             <%--<asp:ListItem Value="PublishDate">PublishDate</asp:ListItem>
<asp:ListItem Value="BeginDate">BeginDate</asp:ListItem>
<asp:ListItem Value="EndDate">EndDate</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>--%>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_IntegralAddEditjg.aspx?cmd=add"><img title="添加" src="../images/add.png" border="0"></a></td></tr></table>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="ITGRID"
                                      onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="ITGRID" DataNavigateUrlFormatString="TJ_IntegralInfoAddEdit.aspx?ITID={0}" Text="详细" />               
                                <asp:TemplateField HeaderText="活动名称">              
                                    <ItemTemplate>
                                        <asp:Label ID="LabelIntegralName" runat="server" Text='<%# Bind("IntegralName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                          
                                <asp:TemplateField HeaderText="开始时间">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBeginDate" runat="server"  MaxLength="16"></asp:TextBox><cc1:CalendarExtender TargetControlID="txtBeginDate" Format="yyyy-MM-dd" ID="CalendarExtender1" runat="server"></cc1:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBeginDate" runat="server" Text='<%# Bind("BeginDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="结束时间">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEndDate" runat="server"  MaxLength="16"></asp:TextBox><cc1:CalendarExtender TargetControlID="txtEndDate" Format="yyyy-MM-dd" ID="CalendarExtender1" runat="server"></cc1:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelEndDate" runat="server" Text='<%# Bind("EndDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="发布人">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelUserID" runat="server" Text='<%# ReturnUserName(Eval("UserID").ToString()) %>'></asp:Label>
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