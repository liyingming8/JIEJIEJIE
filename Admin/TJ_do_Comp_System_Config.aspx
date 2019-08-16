<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_do_Comp_System_Config.aspx.cs" Inherits="Admin_TJ_do_Comp_System_Config" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="编辑">
                        <ItemTemplate>
                            <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="积分汇率">
                        <ItemTemplate>
                            <asp:Label ID="Labeljfhlpy" runat="server" Text='<%# Bind("jfhlpy") %>'></asp:Label><asp:Label ID="Labelunitnm" runat="server" Text='<%# Bind("unitnm") %>'></asp:Label>=￥1.00
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="开通">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="ckb_openpoints" AutoPostBack="True" OnCheckedChanged="ckb_openpoints_OnCheckedChanged" Checked='<%# Convert.ToBoolean(Eval("openpoints")) %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>您尚未进行配置</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div>
        <input type="hidden" id="input_permitgo" runat="server" value="0"/>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="js/frankprocess.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                if ($("#input_permitgo").val() === '0') {
                    setalert('请完成积分商城基础设置');
                } else {
                    setalert('');
                }
            })
        </script>
    </form>
</body>
</html>
