<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ExceptionQueryInfo.aspx.cs" Inherits="Admin_TJ_ExceptionQueryInfo" %>

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
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="lbcode">标签号码</asp:ListItem> 
                        <asp:ListItem Value="extype">异常类型</asp:ListItem>  
                        <asp:ListItem Value="platform">平台</asp:ListItem>
                        <asp:ListItem Value="queryaddress">地址</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id,issolved" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标签号码">
                            <ItemTemplate>
                                <asp:Label ID="Labellbcode" runat="server" Text='<%# Bind("lbcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查询时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelquerytm" runat="server" Text='<%# Bind("querytm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="原因">
                            <ItemTemplate>
                                <asp:Label ID="Labelextype" runat="server" Text='<%# Bind("extype") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="回复方式">
                            <ItemTemplate>
                                <asp:Label ID="Labelrestype" runat="server" Text='<%# ResponseType(Eval("restype").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="是否处理">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Enabled="False" ID="checkbox_issolved" Checked='<%# Convert.ToBoolean(Eval("issolved")) %>'/> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="平台">
                            <ItemTemplate>
                                <asp:Label ID="Labelplatform" runat="server" Text='<%# Bind("platform") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查询地址">
                            <ItemTemplate>
                                <asp:Label ID="Labelqueryaddress" runat="server" Text='<%# Bind("queryaddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="周边数据">
                            <ItemTemplate>
                                <asp:HyperLink ID="hplink_show" ForeColor="white" runat="server">查看</asp:HyperLink> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" ForeColor="white"></ItemStyle>
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
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"  FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ></webdiyer:AspNetPager>
            </div>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
