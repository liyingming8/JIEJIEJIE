<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Integral.aspx.cs" Inherits="Admin_TJ_Integral" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_IntegralAddEdit.aspx?cmd=add', 600, 420, '积分活动')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="IntegralName">活动名称</asp:ListItem> 
                        <asp:ListItem Value="Remarks">备注</asp:ListItem> 
                     </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含:</span> </div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
            </div> 
            <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="ITGRID"
                          OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                        <Columns> 
                            <asp:TemplateField HeaderText="活动名称">
                                <ItemTemplate>
                                    <asp:Label ID="LabelIntegralName" runat="server" Text='<%# Bind("IntegralName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开始时间"> 
                                <ItemTemplate>
                                    <asp:Label ID="LabelBeginDate" runat="server" Text='<%# Bind("BeginDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="结束时间"> 
                                <ItemTemplate>
                                    <asp:Label ID="LabelEndDate" runat="server" Text='<%# Bind("EndDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="积分规则">
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkjifen" runat="server">设置</asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="产品">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ToolTip='<%# Eval("ProductsInfoInChinese").ToString() %>' ID="linkselectproduct"><%# string.IsNullOrEmpty(Eval("ProductsInfoInChinese").ToString())?"全部":(Eval("ProductsInfoInChinese").ToString().Length>10?Eval("ProductsInfoInChinese").ToString().Substring(0,10):Eval("ProductsInfoInChinese").ToString()) %></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="积分平台">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lab_jfplatform" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注"> 
                                <ItemTemplate>
                                    <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发布人">
                                <ItemTemplate>
                                    <asp:Label ID="LabelUserID" runat="server" Text='<%# ReturnUserName(Eval("UserID").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True">
                                <ItemStyle  HorizontalAlign="Center" Width="50px" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                        <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                    </asp:GridView></div>
                    <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
