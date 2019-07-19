<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SiteMap.aspx.cs" Inherits="Admin_TJ_SiteMap" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_SiteMapAddEdit.aspx?cmd=add', 600, 500, '系统目录')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="PageName">目录名称</asp:ListItem>
                        <asp:ListItem Value="LinkPath">链接地址</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <asp:DropDownList runat="server" ID="ddl_parentid"></asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server"  placeholder="全部..."  class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>  
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
           <div style="overflow-x:auto">
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SiteID,PageName"
                              OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" PageSize="20" EnableModelValidation="True" EnableViewState="False" OnRowDeleting="GridView1_RowDeleting">
                            <Columns> 
                                <asp:TemplateField HeaderText="父级目录"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelParentID" runat="server" Text='<%# GetSiteMapName(Eval("ParentID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="目录名称"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPageName" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="链接地址"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLinkPath" runat="server" Text='<%# Bind("LinkPath") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="显示顺序"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelShowOrder" runat="server" Text='<%# Bind("ShowOrder") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="系统LOGO">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" Width="30px" ImageUrl='<%# Eval("LogoName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="所属系统">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSystem" Text='<%# GetSystemType(Eval("SysTypeID").ToString()) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="使用角色">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="linkshowrole">角色</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <%-- <asp:CommandField ShowEditButton="True" />--%>
                                <asp:CommandField ShowDeleteButton="True">
                                    <ItemStyle  HorizontalAlign="Center" Width="50px" />
                                </asp:CommandField>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
                            <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="12"  NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth="" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5"></webdiyer:AspNetPager> 
        </div> 
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>

</body>
</html>
