<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_WXPay375.aspx.cs" Inherits="TB_WXPay375" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">

        <div class="div_WholePage">
            <div class="topdiv">
              
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                         <asp:ListItem Value="LabelCode">标签序号</asp:ListItem>
                                <asp:ListItem Value="UserID">会员昵称</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd"
                        OnClick="BtnSearch0_Click" />
                     <input id="BtDC" runat="server" type="button" class="inputbutton" value="导出EXCEL"
                                onserverclick="BtDC_ServerClick" />
                </div>


            </div>
            <%-- <div class="div_Nav">
                <table class="table_Nav">
                    <tr runat="server" id="Cuser">
                        <td class="td1">
                            <asp:DropDownList ID="DDLField" runat="server" Height="21px" Width="91px">
                                <asp:ListItem Value="LabelCode">标签序号</asp:ListItem>
                                <asp:ListItem Value="UserID">会员昵称</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="td_Nav">包含 :
                            <input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" />
                        </td>
                        <td class="td1">
                            <asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="inputbutton" OnClick="BtnSearch0_Click1" />
                            <input id="BtDC" runat="server" type="button" class="inputbutton" value="导出EXCEL"
                                onserverclick="BtDC_ServerClick" />
                        </td>
                    </tr>

                </table>
            </div>--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto">
                        <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            DataKeyNames="ID" OnPageIndexChanging="gvwDesignationName_PageIndexChanging"
                            OnRowDeleting="GridView1_RowDeleting"
                            OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" PageSize="18">
                            <Columns>
                                <asp:TemplateField HeaderText="订购人">
                                    <ItemTemplate>
                                        <asp:Label ID="L1" runat="server" Text='<%# ReturnUserName(Eval("UserID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="订购产品">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="L2" TextMode="MultiLine" runat="server" Text='<%# ReturnCPInfo(Eval("GoodsID").ToString(),Eval("GoodsNum").ToString())%>'></asp:TextBox>--%>
                                        <textarea runat="server" class="textsy" id="L2"><%# ReturnCPInfo(Eval("GoodsID").ToString(), Eval("GoodsNum").ToString()) %> </textarea>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="收货人">
                                    <ItemTemplate>
                                        <asp:Label ID="L3" runat="server" Text='<%# ReturnAdd(Eval("AddID").ToString(), "nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="收货地址">
                                    <ItemTemplate>
                                        <asp:Label ID="L4" runat="server" Text='<%#ReturnAdd(Eval("AddID").ToString(), "ad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="联系电话">
                                    <ItemTemplate>
                                        <asp:Label ID="L5" runat="server" Text='<%# ReturnAdd(Eval("AddID").ToString(), "ph") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否发货">
                                    <ItemTemplate>
                                        <asp:Label ID="L6" runat="server" Text='<%# Eval("WLFH_Flag").ToString().Equals("1") ? "已发货" : "待发货" %>'></asp:Label>
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
                            <EmptyDataTemplate>
                                尚未检索到数据
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_UserID" runat="server" />
    </form>

    <script>
        window.onload = function () { 
            $(".textsy").each(function() {
                this.setAttribute('style', 'height:' + (this.scrollHeight - 20) + 'px;overflow-y:hidden;');
            });
        } 
    </script>
    <script src="../include/js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script> 
</body>
</html>
