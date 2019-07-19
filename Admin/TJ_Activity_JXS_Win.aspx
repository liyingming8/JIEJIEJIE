<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_JXS_Win.aspx.cs" Inherits="Admin_TJ_Activity_JXS_Win" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <%--<asp:DropDownList runat="server" DataValueField="CompID" DataTextField="CompName" ID="ddl_agentid"/>--%>
                    <input  id="inputjxs" runat="server" class="inputsearch" style="width: 250px;" placeholder="全部经销商..."/>
                     </div>
                <div class="topitem"><span>获取时间</span></div>
                <div class="topitem">
                    <asp:TextBox ID="txt_start" CssClass="inputdatenew" runat="server"></asp:TextBox><span>至</span><asp:TextBox ID="txt_end" runat="server"  CssClass="inputdatenew" ></asp:TextBox>
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">   
                        <asp:ListItem Value="winreason">方式</asp:ListItem> 
                        <asp:ListItem Value="remarks">备注</asp:ListItem> 
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                <div class="topitem">
                    <asp:Button runat="server" ID="btn_refresh" CssClass="btn btn-fresh btnyd" Text="刷新" OnClick="btn_refresh_Click"/>
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="compid">
                            <ItemTemplate>
                                <asp:Label ID="Labelcompid" runat="server" Text='<%# Bind("compid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="经销商">
                            <ItemTemplate>
                                <asp:Label ID="Labelagentid" runat="server" Text='<%# BtjRegister.GetList(int.Parse(Eval("agentid").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="类型">
                            <ItemTemplate>
                                <asp:Label ID="Labelawtypeid" runat="server" Text='<%# BtjAwardType.GetList(int.Parse(Eval("awtypeid").ToString())).awardtype %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="方式">
                            <ItemTemplate>
                                <asp:Label ID="Labelwinreason" runat="server" Text='<%# Bind("winreason") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分值">
                            <ItemTemplate>
                                <asp:Label ID="Labelprizevl" runat="server" Text='<%# Bind("prizevl") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="获取日期">
                            <ItemTemplate>
                                <asp:Label ID="Labelgettm" runat="server" Text='<%# Convert.ToDateTime(Eval("gettm")).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="确认日期">
                            <ItemTemplate>
                                <asp:Label ID="Labelconfirmtm" runat="server" Text='<%# Bind("confirmtm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已领取">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Enabled="False" ID="ckb_islq" Checked='<%# Convert.ToBoolean(Eval("islq")) %>'/> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>  
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
            </div>
        </div>
        <input id="hdcompid" type="hidden" runat="server"/>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
