<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UserJFlab.aspx.cs" Inherits="TJ_UserJFlab" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <style type="text/css">
            .auto-style1 {
                BACKGROUND: #e6eff8;
                LINE-HEIGHT: 120%;
                height: 30px;
            }
        </style>
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div> 
                <table  class="tdbg">
                    <tr runat="server" id="Cuser"> 
                        <td >
                            <br />
                            <asp:DropDownList ID="DDLField" runat="server" Height="21px" Width="91px">

                                <asp:ListItem Value="OwnUserID">会员昵称</asp:ListItem>

                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" />
                            <br />
                        </td>
                       
                    </tr>
                    <tr>
                        <td>积分时间： 
                            <asp:TextBox ID="DateBegin" runat="server" Height="19px"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="DateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                            至
                            <asp:TextBox ID="DateEnd" runat="server" Height="19px"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="DateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>

                            ;;;
                    
                            <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>

                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　
                                      AutoGenerateColumns="False" DataKeyNames="ID" 
                                      
                                      OnRowDeleting="GridView1_RowDeleting" 
                                       OnRowDataBound="GridView1_RowDataBound"
                                      CssClass="GridViewStyle" PageSize="18">
                            <Columns>
                                <asp:TemplateField HeaderText="会员昵称">

                                    <ItemTemplate>
                                        <asp:Label ID="L1" runat="server" Text='<%# buser.GetList(int.Parse(Eval("UserID").ToString())).NickName %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="标签序号">
                                    <ItemTemplate>
                                        <asp:Label ID="L2" runat="server" Text='<%# Bind("Label") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
                                <asp:TemplateField HeaderText="获得积分">
                                    <ItemTemplate>
                                        <asp:Label ID="L3" runat="server" Text='<%# Bind("jifen") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="积分类型">
                                    <ItemTemplate>
                                        <asp:Label ID="L4" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                         
                                <asp:TemplateField HeaderText="积分时间">
                                    <ItemTemplate>
                                        <asp:Label ID="L5" runat="server" Text='<%# Bind("JFTime", "{0:yyyy-MM-dd }") %>'></asp:Label>
                               
                                    </ItemTemplate>
                                </asp:TemplateField>
                          
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            
                            <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView></div>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
            <asp:HiddenField ID="HF_UserID" runat="server" />
        </form>
    </body>
</html>