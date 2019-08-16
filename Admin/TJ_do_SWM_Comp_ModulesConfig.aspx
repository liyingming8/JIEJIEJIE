<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_do_SWM_Comp_ModulesConfig.aspx.cs" Inherits="Admin_TJ_do_SWM_Comp_ModulesConfig" %>
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
            <div style="overflow-x: auto;  padding-bottom: 50px;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id,logourl"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img id="img_edit" runat="server" alt="编辑" src="Images/edit.png" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hf_mdid" Value='<%# Bind("mdid") %>' />
                                <asp:Label ID="Labelmdnm" runat="server" Text='<%# Bind("mdnm") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOGO">
                            <ItemTemplate>
                                <img alt="LOGO"  height="50" id="img_logo" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="显示">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="ckb_isshow" OnCheckedChanged="ckb_isshow_OnCheckedChanged" AutoPostBack="True"  Checked='<%# Convert.ToBoolean(Eval("isshow")) %>'/> 
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
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <input type="hidden" id="hdLPH" value="blue" runat="server"/>
    </form>
</body>
</html>
