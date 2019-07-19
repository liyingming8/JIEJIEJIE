﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DKWXJPinfoJGXQ.aspx.cs" Inherits="TJ_DKWXJPinfoJGXQ" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
         
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="../include/easyui.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch1" runat="server">
            <div runat="server" id="iLoading" style="display: none; font-weight: bold; font-size: large; left: 600px; text-transform: capitalize; color: red; font-family: Monospace; position: absolute; top: 20px; background-color: #99ccff;">
                数据正在整理中，请稍候......
            </div>
            <div class="div_WholePage">  
                <div class="topdiv">
                    <div class="topitem"><span>标签号码</span></div> 
                    <div class="topitem"><input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputsearch" /></div>
                    <div class="topitem"><span>日期</span></div>
                    <div class="topitem"><asp:TextBox ID="txtStartDate" runat="server" Width="70px" CssClass="inputsearch"></asp:TextBox>
                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server"  Format="yyyy-MM-dd" TargetControlID="txtStartDate"></cc2:CalendarExtender></div>
                    <div class="topitem"><span>至</span></div>
                    <div class="topitem"><asp:TextBox ID="txtEndDate" runat="server"  Width="70px" CssClass="inputsearch"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtEndDate"></cc2:CalendarExtender></div>
                    <div class="topitem"><asp:Button ID="BtnSearch1" runat="server" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" Text="汇总" /></div>
                    <div class="topitem"><asp:Button ID="Button1" runat="server" CssClass="btn btn-default btnyd" OnClick="BtnExport_Click" Text="数据导出Excel" /></div>
                </div>
                  <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                            </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　
                              AutoGenerateColumns="False" DataKeyNames="ID,CompID" 
                              
                              OnRowDeleting="GridView1_RowDeleting" 
                               OnRowDataBound="GridView1_RowDataBound"
                              CssClass="GridViewStyle" PageSize="18">
                    <Columns>
                        <asp:TemplateField HeaderText="中奖时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelWXname" runat="server" Text='<%# Bind("LQDateTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中奖号码">
                            <ItemTemplate>
                                <asp:Label ID="LabelBoxLabel" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                 
                        <asp:TemplateField HeaderText="兑奖码">

                            <ItemTemplate>
                                <asp:Label ID="Labelzjm" runat="server" Text='<%# Bind("ZJMa") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所中奖项">
                            <ItemTemplate>
                                <asp:Label ID="JXname" runat="server" Text='<%# Bind("JXName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="中奖人微信昵称">
                            <ItemTemplate>
                                <asp:Label ID="LabelWXname" runat="server" Text='<%# Bind("NickName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="手机号码">
                            <ItemTemplate>
                                <asp:Label ID="Labelphone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中奖者地址">
                            <ItemTemplate>
                                <asp:Label ID="ShouHuodizhi" runat="server" Text='<%# Eval("ShouHuodizhi") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="扫码地址">
                            <ItemTemplate>
                                <asp:Label ID="saomadizhi" runat="server" Text='<%# Eval("DetialeAddress") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否领奖">
                            <ItemTemplate>
                                <asp:Label ID="LabelDJflag" runat="server" Text='<%# Eval("DJflag").ToString() == "0" ? "否" : "是" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑奖点">
                            <ItemTemplate>
                                <asp:Label ID="LabelDJdian" runat="server" Text='<%# Eval("DJDianName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑奖时间">
                            <ItemTemplate>
                                <asp:Label ID="Labeltime" runat="server" Text='<%# Bind("DHtime") %>'></asp:Label>
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
            <script type="text/javascript">
                function dispdiv() {
                    if (document.getElementById("iLoading").style.display == "block") {
                        document.getElementById("iLoading").style.display = "none";
                    } else {
                        document.getElementById("iLoading").style.display = "block";
                    }
                }
            </script>
        </form>
    </body>
</html>