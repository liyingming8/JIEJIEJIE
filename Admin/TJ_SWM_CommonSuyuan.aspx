<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_CommonSuyuan.aspx.cs" Inherits="Admin_TJ_SWM_CommonSuyuan" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv" id="topdiv" runat="server">
                <div class="topitem">
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_SWM_CommonSuyuanAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>&pid=<%=Sc.EncryptQueryString(hdpid.Value)%>&prodid=<%=Sc.EncryptQueryString(hdprodid.Value)%>', 680,600,'溯源信息')" /></div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="prodname">产品名称</asp:ListItem>
                        <asp:ListItem Value="prodnumber">产品编码</asp:ListItem>
                        <asp:ListItem Value="materials">原料</asp:ListItem> 
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="Labelprodname" runat="server" Text='<%# Bind("prodname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品编码">
                            <ItemTemplate>
                                <asp:Label ID="Labelprodnumber" runat="server" Text='<%# Bind("prodnumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="原料">
                            <ItemTemplate>
                                <asp:Label ID="Labelmaterials" runat="server" Text='<%# Bind("materials") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="生产日期">
                            <ItemTemplate>
                                <asp:Label ID="Labelproddate" runat="server" Text='<%# Bind("proddate","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="质检日期">
                            <ItemTemplate>
                                <asp:Label ID="Labelcheckdate" runat="server" Text='<%# Bind("checkdate","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="质检员">
                            <ItemTemplate>
                                <asp:Label ID="Labelcheckuser" runat="server" Text='<%# Bind("checkuser") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="质检报告">
                            <ItemTemplate>
                                <img id="imgreport" height="80" runat="server" src='<%# Bind("checkreport") %>'/> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出厂日期">
                            <ItemTemplate>
                                <asp:Label ID="Labeloutdate" runat="server" Text='<%# Bind("outdate","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
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
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="12" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条" NumericButtonCount="5"></webdiyer:AspNetPager>
            </div>
            <input type="hidden" id="hdpid" runat="server"/>
            <input type="hidden" id="hdprodid" runat="server"/>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
