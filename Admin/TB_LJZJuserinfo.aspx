<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_LJZJuserinfo.aspx.cs" Inherits="TB_LJZJuserinfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
       <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
       <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div>
                <table  class="tdbg">
                    <tr>  
                        <td >
                            <asp:DropDownList ID="DDLField" runat="server">

                                <asp:ListItem Value="ProID">产品</asp:ListItem>
                                <asp:ListItem Value="BoxLabel">标签序号</asp:ListItem>
                                <asp:ListItem Value="JXname">奖项</asp:ListItem>
                                <asp:ListItem Value="JPInfo">奖品</asp:ListItem> 
                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" placeholder="请输入查找内容" type="text" runat="server" class="inputtext" />
                        </td>

                        <td align="left" >日期</td>
                        <td align="left" class="tdbg" style="width: 100px">
                            <asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                        </td>
                        <td class="tdbg" style="width: 20px; text-align: center;">至</td>
                        <td class="tdbg" style="width: 100px">
                            <asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                        </td>

                        <td>
                            <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" />

                        </td>

                        <td>
                            <input id="BtDC" runat="server" type="button" class="inputbutton" value="导出EXCEL" onserverclick="BtDC_ServerClick" />
                        </td>
                        <td>相关操作:</td>
                        <td><a style="display: none">
                                             <img title="添加" src="images/add.png" border="0"></a></td>
                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　
                                      AutoGenerateColumns="False" DataKeyNames="ID,CompID" 
                                      
                                      OnRowDeleting="GridView1_RowDeleting" 
                                       OnRowDataBound="GridView1_RowDataBound"
                                      CssClass="GridViewStyle" PageSize="18">
                            <Columns>
                                <asp:TemplateField HeaderText="中奖号码">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBoxLabel" runat="server" Text='<%# Bind("BoxLabel") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="所中奖项">
                                    <ItemTemplate>
                                        <asp:Label ID="JXname" runat="server" Text='<%# Bind("JXname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="产品名称">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCpid" runat="server" Text='<%# proname(Eval("BoxLabel").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="奖品">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelJPType" runat="server" Text='<%# Bind("JPInfo") %>'></asp:Label>
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

                            </Columns>
                            <PagerTemplate>
                                当前第:
                                <%--//((GridView)Container.NamingContainer)就是为了得到当前的控件--%>
                                <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView) Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                                页/共:
                                <%--//得到分页页面的总数--%>
                                <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView) Container.NamingContainer).PageCount %>"></asp:Label>
                                页
                                <%--//如果该分页是首分页，那么该连接就不会显示了.同时对应了自带识别的命令参数CommandArgument--%>
                                <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                                                Visible='<%#((GridView) Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                                                CommandName="Page" Visible='<%# ((GridView) Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                                <%--      //如果该分页是尾页，那么该连接就不会显示了--%>
                                <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                                                Visible='<%# ((GridView) Container.NamingContainer).PageIndex != ((GridView) Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                                Visible='<%# ((GridView) Container.NamingContainer).PageIndex != ((GridView) Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                                转到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="30px" Text='<%# ((GridView) Container.Parent.Parent).PageIndex + 1 %>' />页
                                <%--//这里将CommandArgument即使点击该按钮e.newIndex 值为3--%>
                                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                                                CommandName="Page" Text="GO" />
                            </PagerTemplate>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            
                            <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView></div>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </form>
    </body>
</html>