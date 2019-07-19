<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ZJLabelCodesmallInfoJGXQ.aspx.cs" Inherits="Admin_TJ_ZJLabelCodesmallInfoJGXQ" %>
<%--<%@ Reference Page="~/Admin/TJ_ZJLabelCodesmallInfo.aspx" %>--%>

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
                        <td>
                            <asp:DropDownList ID="DDLField" runat="server">

                                <asp:ListItem Value="LabelCode">二维码标签序号</asp:ListItem>
                                <asp:ListItem Value="BJShiJian">领取时间</asp:ListItem>
                                <asp:ListItem Value="BoxLabelCode">标签序号</asp:ListItem>

                            </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputtext"  placeholder="请输入查找内容"  />;
                            日期<asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"  ></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                            至<asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>;;
                            ;<asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /><asp:Button ID="Button1" runat="server" Text="数据导出Excel" CssClass="inputbutton" OnClick="BtnExport_Click" Width="128px" />
                            <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                            </asp:ScriptManager>
                        </td>
                        <td>相关操作:</td>
                        <td><a onclick=" openWinCenter('TJ_ZJLabelCodesmallInfoAddEdit.aspx?cmd=add', 600, 300) ">
                                             <img title="添加" src="images/add.png" border="0" /></a></td>
                    </tr>
                </table>
                <div runat ="server"  id="iLoading" style="display: none; font-weight: bold; font-size: large; left: 600px; text-transform: capitalize; color: red; font-family: Monospace; position: absolute; top: 20px; background-color: #99ccff;">
                    数据正在整理中，请稍候......
                </div>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="ZJCDID"
                              OnPageIndexChanging="gvwDesignationName_PageIndexChanging"  OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" PageSize="20">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="labelindex" runat="server" Text='<%# GridView1.PageIndex*GridView1.PageSize + GridView1.Rows.Count + 1 %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="二维码链接序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelLabelCode" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标签序号">
                            <ItemTemplate>
                                <asp:Label ID="labelXiangBiaoCode" Text='<%# Bind("BoxLabelCode") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖项">
                            <ItemTemplate>
                                <asp:Label ID="LabelJxID" runat="server" Text='<%# bjxinfo.GetList(Convert.ToInt32(Eval("JxID"))).JxName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="布奖时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelSaleArea" runat="server" Text='<%# Bind("BJShiJian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="销售区域">
                    <ItemTemplate>
                        <asp:Label ID="LabelSaleArea" runat="server" Text='<%# Bind("SaleArea") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtRemarks" runat="server"  MaxLength="50"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                        <asp:CommandField ShowEditButton="False" />
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
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView></div>
            </div>
        </form>
    </body>
</html>