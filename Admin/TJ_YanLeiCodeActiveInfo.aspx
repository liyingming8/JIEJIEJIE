<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_YanLeiCodeActiveInfo.aspx.cs" Inherits="Admin_TJ_YanLeiCodeActiveInfo" %>

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
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_YanLeiCodeActiveInfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>    ', 600,450,'TJ_YanLeiCodeActiveInfo')" />
                </div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="id">id</asp:ListItem>
                        <asp:ListItem Value="tablename">tablename</asp:ListItem>
                        <asp:ListItem Value="acpercent">acpercent</asp:ListItem>
                        <asp:ListItem Value="updatedate">updatedate</asp:ListItem>
                        <asp:ListItem Value="notactivecodespan">notactivecodespan</asp:ListItem>
                        <asp:ListItem Value="remarks">remarks</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <div class="topitem">
                    <asp:Button ID="btn_insert" CssClass="btn btn-warning btnyd" runat="server" Text="导入" OnClick="btn_insert_Click" /></div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="表名">
                            <ItemTemplate>
                                <asp:Label ID="Labeltablename" runat="server" Text='<%# Bind("tablename") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总记录数">
                            <ItemTemplate>
                                <asp:Label ID="Labeltotalnum" runat="server" Text='<%# Bind("totalnum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已激活数量">
                            <ItemTemplate>
                                <asp:Label ID="Labelactivednum" runat="server" Text='<%# Eval("activednum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="激活率">
                            <ItemTemplate>
                                <asp:Label ID="Labelacpercent" runat="server" Text='<%# Convert.ToDecimal(Eval("acpercent"))*100 %>'></asp:Label>%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="更新时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelupdatedate" runat="server" Text='<%# Bind("updatedate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="未激活范围">
                            <ItemTemplate>
                                <asp:Label ID="Labelnotactivecodespan" runat="server" Text='<%# Bind("notactivecodespan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刷新">
                            <ItemTemplate>
                                <asp:Button ID="Button_Fresh" CssClass="btn btn-info btnydinline" CommandName="fresh" CommandArgument='<%# Bind("id") %>' runat="server" Text="刷新" /></ItemTemplate>
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
                 <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  NumericButtonCount="5" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页"></webdiyer:AspNetPager>
            </div>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
