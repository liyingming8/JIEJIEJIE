<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_JXS_Win_Terminal.aspx.cs" Inherits="Admin_TJ_Activity_JXS_Win_Terminal" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="cc2" Namespace="BL.Controls.ComboBox" Assembly="BL.Controls" %>
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
                            <asp:DropDownList runat="server" ID="ddl_departid" DataTextField="department" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddl_departid_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="topitem">
                            <cc2:ComboBox runat="server" ID="ddl_terminal" DataTextField="CompName" Width="250px" DataValueField="CompID" AppendDataBoundItems="True" AutoPostBack="True" OnComboBoxChanged="ddl_terminal_OnComboBoxChanged" RenderMode="ComboBoxSearch">
                                <asp:ListItem Value="0" Selected="True">全部终端店</asp:ListItem>
                            </cc2:ComboBox>
                            <%--<input id="inputjxs" runat="server" class="inputsearch" style="width: 250px;" placeholder="全部终端店..." readonly="readonly" />--%>
                        </div> 
                        <div class="topitem">
                            <asp:DropDownList ID="ddl_gettype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_gettype_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">全部类型</asp:ListItem>
                                <asp:ListItem Value="1">扫码确认</asp:ListItem>
                                <asp:ListItem Value="4">消费者返利</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="topitem"><span>包含</span></div>
                        <div class="topitem">
                             </div>
                        <div class="topitem">
                            <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                        </div>
                        <div class="topitem">
                            <asp:Button runat="server" ID="btn_refresh" CssClass="btn btn-fresh btnyd" Text="刷新" OnClick="btn_refresh_Click" />
                        </div>
                        <div class="topitem">
                            <asp:Button runat="server" ID="btn_createexcel" Style="text-decoration: underline; cursor: pointer;" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click"/>
                        </div>
                 </div>
            </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div style="overflow-x: auto">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id,remarks"
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
                                <%--<asp:TemplateField HeaderText="prizeintro">
                            <ItemTemplate>
                                <asp:Label ID="Labelprizeintro" runat="server" Text='<%# Bind("prizeintro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="获取日期">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelgettm" runat="server" Text='<%# Convert.ToDateTime(Eval("gettm")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="确认时间">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelconfirmtm" runat="server" Text='<%# Bind("confirmtm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="已领取">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" Enabled="False" ID="ckb_islq" Checked='<%# Convert.ToBoolean(Eval("islq")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="linkremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True">
                                    <ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" />
                                </asp:CommandField>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="12" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
                    </div>
                </div>
            </ContentTemplate>     
        </asp:UpdatePanel> 
                <input id="hdcompid" type="hidden" runat="server" />
                <input id="hdagid" type="hidden" runat="server" /> 
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    </form>
</body>
</html>
