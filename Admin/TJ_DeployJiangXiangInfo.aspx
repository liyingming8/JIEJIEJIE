<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DeployJiangXiangInfo.aspx.cs" Inherits="Admin_TJ_DeployJiangXiangInfo" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                 <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_DeployJiangXiangInfoAddEdit.aspx?cmd=add', 900, 400, '布奖管理')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <%--<asp:ListItem Value="Remarks">备注</asp:ListItem>--%>
                        <asp:ListItem Value="ChuKuDanHao">出库单号</asp:ListItem>
                    </asp:DropDownList>
                </div>
                 
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
              
               
            </div>

            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="DPJXID" Width="100%"
                  OnRowDeleting="GridView1_RowDeleting" PageSize="20"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="奖项">
                        <ItemTemplate>
                            <asp:Label ID="LabelJxID" runat="server" Text='<%# bjx.GetList(Convert.ToInt32(Eval("JxID"))).JxName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="兑奖点">
                        <ItemTemplate>
                            <asp:Label ID="LabelDjdID" runat="server" Text='<%# bcompany.GetList(Convert.ToInt32(Eval("DjdID"))).CompName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="布奖数量">
                        <ItemTemplate>
                            <asp:Label ID="LabelPreOderNum" runat="server" Text='<%# Bind("PreOderNum") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="经销商">
                        <ItemTemplate>
                            <asp:Label ID="LabelAgentID" runat="server" Text='<%# bagent.GetList(Convert.ToInt32(Eval("AgentID"))).CompName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品">
                        <ItemTemplate>
                            <asp:Label ID="LabelProID" runat="server" Text='<%# bprod.GetList(Convert.ToInt32(Eval("ProID"))).Products_Name %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="出库单号">
                        <ItemTemplate>
                            <asp:Label ID="LabelChuKuDanHao" runat="server" Text='<%# Bind("ChuKuDanHao") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="布奖日期">
                        <ItemTemplate>
                            <asp:Label ID="LabelDeploydate" runat="server" Text='<%# Bind("Deploydate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="布奖人">
                        <ItemTemplate>
                            <asp:Label ID="LabelUserID" runat="server" Text='<%# buser.GetList(Convert.ToInt32(Eval("UserID"))).LoginName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="核销">
                        <ItemTemplate>
                            <asp:Label ID="LabelHeXiao" runat="server" Text='<%# HexiaoValue(Eval("DPJXID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结余">
                        <ItemTemplate>
                            <asp:Label ID="LabelJieYu" runat="server" Text='<%# JieYuValue(Eval("DPJXID").ToString(), Eval("PreOderNum").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle  HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
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
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

        </div>
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
