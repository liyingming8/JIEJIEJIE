<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_CodeSpan.aspx.cs" Inherits="Admin_TJ_Activity_CodeSpan" %>

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
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_Activity_CodeSpanAddEdit.aspx?acid=<%=Sc.EncryptQueryString(hf_acid.Value)%>&cmd=<%=Sc.EncryptQueryString("add")%>', 680,520,'中奖码范围')" />
                </div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="dingdanbianhao">客户订单编号</asp:ListItem>
                        <asp:ListItem Value="remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
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
                        <asp:TemplateField HeaderText="活动">
                            <ItemTemplate>
                                <asp:Label ID="Labelacid" runat="server" Text='<%#  Eval("activityname").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品">
                            <ItemTemplate>
                                <asp:Label ID="Labelprodid" runat="server" Text='<%# (Eval("prodid").ToString().Trim().Length.Equals(0)||Eval("prodid").ToString().Equals("0"))?"不限":Eval("prodname").ToString()  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="经销商">
                            <ItemTemplate>
                                <asp:Label ID="Labelagentid" runat="server" Text='<%# (Eval("agentid").ToString().Trim().Length.Equals(0)||Eval("agentid").ToString().Trim().Equals("0"))?"不限":Eval("agentname").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="客户订单编号">
                            <ItemTemplate>
                                <asp:Label ID="Labeldingdanbianhao" runat="server" Text='<%# Eval("dingdanbianhao").ToString().Trim().Length.Equals(0)?"不限":Eval("dingdanbianhao").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelfhsdate" runat="server" Text='<%# Bind("fhsdate","{0:yyyy-MM-dd}") %>'></asp:Label>至<asp:Label ID="Labelfhedate" runat="server" Text='<%# Bind("fhedate","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建日期">
                            <ItemTemplate>
                                <asp:Label ID="Labelcreatdate" runat="server" Text='<%# Bind("creatdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建人">
                            <ItemTemplate>
                                <asp:Label ID="Labelcreatuserid" runat="server" Text='<%# Eval("createusername").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
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
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
            </div>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <asp:HiddenField ID="hf_acid" runat="server" />
        <asp:HiddenField ID="hf_activity_name" runat="server" />
    </form>
</body>
</html>
