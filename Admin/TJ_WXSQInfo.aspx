<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_WXSQInfo.aspx.cs" Inherits="Admin_TJ_WXSQInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="container topdiv">
                <div class="topitem">
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_WXSQInfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 760,580,'微信授权')" /></div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="ID">ID</asp:ListItem>
                        <asp:ListItem Value="WX_Appid">WX_Appid</asp:ListItem>
                        <asp:ListItem Value="WX_Appsecret">WX_Appsecret</asp:ListItem>
                        <asp:ListItem Value="WX_Redirect_url">WX_Redirect_url</asp:ListItem>
                        <asp:ListItem Value="WX_CL_url">WX_CL_url</asp:ListItem>
                        <asp:ListItem Value="WX_Scope">WX_Scope</asp:ListItem>
                        <asp:ListItem Value="WX_GZ">WX_GZ</asp:ListItem>
                        <asp:ListItem Value="OwnUrl">OwnUrl</asp:ListItem>
                        <asp:ListItem Value="Remarkes">Remarkes</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位">
                            <ItemTemplate>
                                <asp:Label ID="LabelCompID" runat="server" Text='<%#Commfrank.GetValueByID("nm","TJ_RegisterCompanys","CompID","CompName","",Eval("CompID").ToString())  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="子级">
                            <ItemTemplate>
                                <asp:Label ID="LabelSid" runat="server" Text='<%# Bind("Sid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="微信-Appid">
                            <ItemTemplate>
                                <asp:Label ID="LabelWX_Appid" runat="server" Text='<%# Bind("WX_Appid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="微信-Appsecret">
                            <ItemTemplate>
                                <asp:Label ID="LabelWX_Appsecret" runat="server" Text='<%# Bind("WX_Appsecret") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="跳转路径">
                            <ItemTemplate>
                                <asp:Label ID="LabelWX_Redirect_url" runat="server" Text='<%# Bind("WX_Redirect_url") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="处理路径">
                            <ItemTemplate>
                                <asp:Label ID="LabelWX_CL_url" runat="server" Text='<%# Bind("WX_CL_url") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="授权模式">
                            <ItemTemplate>
                                <asp:Label ID="LabelWX_Scope" runat="server" Text='<%# Bind("WX_Scope") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否关注">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckb_gz" Checked='<%# Convert.ToBoolean(Eval("WX_GZ")) %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="路径一致">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckb_same" Checked='<%# Convert.ToBoolean(Eval("OwnUrl")) %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarkes" runat="server" Text='<%# Bind("Remarkes") %>'></asp:Label>
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
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
            </div>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
