<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_do_CompAdvancePayment.aspx.cs" Inherits="Admin_TJ_CompAdvancePayment" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="container topdiv">   
     <div class="topitem"><input type="button" id="btn_pay" OnServerClick="btn_pay_OnServerClick" value="我要充值" class="btn btn-warning btnyd" runat="server" /></div>
</div>
<div>
<asp:GridView ID="GridView1" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField> 
                <asp:TemplateField HeaderText="预付金额">
                    <ItemTemplate>
                        <asp:Label ID="Labelpayvalue" runat="server" Text='<%# Bind("payvalue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="时间">
                    <ItemTemplate>
                        <asp:Label ID="Labelpaytm" runat="server" Text='<%# Bind("paytm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="付款方式">
                    <ItemTemplate>
                        <asp:Label ID="Labelpaymethod" runat="server" Text='<%# Bind("paymethod") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="确认">
                    <ItemTemplate>
                        <asp:Label ID="Labelisconfirm" runat="server" Text='<%# Bind("isconfirm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
</Columns>
<EmptyDataTemplate>尚未检索到您的预付款记录</EmptyDataTemplate>
<FooterStyle CssClass="GridViewFooterStyle" />
<RowStyle CssClass="GridViewRowStyle" />
<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
</div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>