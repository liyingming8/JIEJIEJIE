<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SmallBuJiang.aspx.cs" Inherits="Admin_TB_SmallBuJiang" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TB_SmallBuJiangAddEdit.aspx?cmd=add', 600, 350, '号码段布奖编辑')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <%--<asp:ListItem Value="ID">ID</asp:ListItem>--%>
                        <asp:ListItem Value="Startlabelcode">开始号码</asp:ListItem>
                        <asp:ListItem Value="Endlabelcode">结束号码</asp:ListItem>
                        <%--<asp:ListItem Value="JxID">奖项</asp:ListItem>--%>
                        <asp:ListItem Value="BJDate">布奖时间</asp:ListItem>
                        <%--<asp:ListItem Value="Remarks1"> 产品名称</asp:ListItem>--%>
                        <%--<asp:ListItem Value="Remarks">Remarks</asp:ListItem>
<asp:ListItem Value="Remarks1">Remarks1</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                
                <div class="topitem">
                    <input id="inputSearchKeyword" placeholder="请输入查找内容" type="text" runat="server" class="inputsearch" />
                    日期 :
                            <asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"> </asp:TextBox><cc2:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                    至
                            <asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                <div class="topitem">
                    <asp:Button ID="Button1" runat="server" Text="数据导出Excel" CssClass="btn btn-warning btnyd" onserverclick="BtnSearch1_Click" OnClick="Button1_Click" Width="127px" /></div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager>
                <%--<input type="button" value="删除" class="btn btn-warning" style="display: none" onclick="    openWinCenter('TB_SmallBuJiangdelte.aspx?cmd=delete', 600, 300, '号码段布奖删除')" /> --%>
                
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="ID"
                 PageSize="30" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>

                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <%-- <asp:TemplateField HeaderText="布奖人">
                    <ItemTemplate>
                        <asp:Label ID="LabelUID" runat="server" Text='<%# Bind("UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="开始号码">
                        <ItemTemplate>
                            <asp:Label ID="LabelStartlabelcode" runat="server" Text='<%# Bind("Startlabelcode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束号码">
                        <ItemTemplate>
                            <asp:Label ID="LabelEndlabelcode" runat="server" Text='<%# Eval("Endlabelcode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="奖项">
                        <ItemTemplate>
                            <asp:Label ID="LabelJXID" runat="server" Text='<%# bjx.GetList(Convert.ToInt32(Eval("JxID"))).JxName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label ID="Labelcount" runat="server" Text='<%# Bind("count") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="布奖时间">
                        <ItemTemplate>
                            <asp:Label ID="LabelBJDate" runat="server" Text='<%# Bind("BJDate") %>'></asp:Label>
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
