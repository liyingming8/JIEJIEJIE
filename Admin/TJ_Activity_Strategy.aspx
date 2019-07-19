<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_Strategy.aspx.cs" Inherits="Admin_TJ_Activity_Strategy" %>

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
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_Activity_StrategyAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>    ', 580,480,'奖励策略')" /></div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="strategyname">策略名称</asp:ListItem> 
                        <asp:ListItem Value="remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id,createuserid"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="策略名称">
                            <ItemTemplate>
                                <asp:Label ID="Labelstrategyname" runat="server" Text='<%# Bind("strategyname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="策略描述">
                            <ItemTemplate>
                                <asp:Label ID="Labelstrategydiscription" runat="server" Text='<%#Comfrank.GetActivityStrategyDiscription(int.Parse(Eval("id").ToString()),false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="单码限次">
                            <ItemTemplate>
                                <asp:Label ID="Labeltimelimit" runat="server" Text='<%# Bind("timelimit") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="有效">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Enabled="False" ID="ckb_isactive" Checked='<%# Convert.ToBoolean(Eval("isactive")) %>'/> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建人">
                            <ItemTemplate>
                                <asp:Label ID="Labelcreateuserid" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="创建日期">
                            <ItemTemplate>
                                <asp:Label ID="Labelcreatedate" runat="server" Text='<%# Bind("createdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--  
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" />
                        </asp:CommandField> --%>
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
    </form>
</body>
</html>
