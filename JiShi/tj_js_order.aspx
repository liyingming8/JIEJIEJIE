<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_order.aspx.cs" Inherits="Admin_tj_js_order" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('tj_js_orderAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_js_order')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="id">id</asp:ListItem>
<asp:ListItem Value="userid">userid</asp:ListItem>
<asp:ListItem Value="ordernumber">ordernumber</asp:ListItem>
<asp:ListItem Value="goodsid">goodsid</asp:ListItem>
<asp:ListItem Value="goodsnum">goodsnum</asp:ListItem>
<asp:ListItem Value="goodstype">goodstype</asp:ListItem>
<asp:ListItem Value="shouldpaymoney">shouldpaymoney</asp:ListItem>
<asp:ListItem Value="remarks">remarks</asp:ListItem>
<asp:ListItem Value="yunfei">yunfei</asp:ListItem>
<asp:ListItem Value="orderdate">orderdate</asp:ListItem>
<asp:ListItem Value="deliveryname">deliveryname</asp:ListItem>
<asp:ListItem Value="deliveryphone">deliveryphone</asp:ListItem>
<asp:ListItem Value="deliveryaddress">deliveryaddress</asp:ListItem>
             </asp:DropDownList>
        </div>
     
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
</div>
<div>
<div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField>
                <asp:TemplateField HeaderText="userid">
                    <ItemTemplate>
                        <asp:Label ID="Labeluserid" runat="server" Text='<%# Bind("userid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ordernumber">
                    <ItemTemplate>
                        <asp:Label ID="Labelordernumber" runat="server" Text='<%# Bind("ordernumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="goodsid">
                    <ItemTemplate>
                        <asp:Label ID="Labelgoodsid" runat="server" Text='<%# Bind("goodsid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="goodsnum">
                    <ItemTemplate>
                        <asp:Label ID="Labelgoodsnum" runat="server" Text='<%# Bind("goodsnum") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="goodstype">
                    <ItemTemplate>
                        <asp:Label ID="Labelgoodstype" runat="server" Text='<%# Bind("goodstype") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="shouldpaymoney">
                    <ItemTemplate>
                        <asp:Label ID="Labelshouldpaymoney" runat="server" Text='<%# Bind("shouldpaymoney") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="remarks">
                    <ItemTemplate>
                        <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="yunfei">
                    <ItemTemplate>
                        <asp:Label ID="Labelyunfei" runat="server" Text='<%# Bind("yunfei") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="orderdate">
                    <ItemTemplate>
                        <asp:Label ID="Labelorderdate" runat="server" Text='<%# Bind("orderdate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliveryname">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliveryname" runat="server" Text='<%# Bind("deliveryname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliveryphone">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliveryphone" runat="server" Text='<%# Bind("deliveryphone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliveryaddress">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliveryaddress" runat="server" Text='<%# Bind("deliveryaddress") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle  HorizontalAlign="Center" Width="50px" /></asp:CommandField>
</Columns>
<EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
<FooterStyle CssClass="GridViewFooterStyle" />
<RowStyle CssClass="GridViewRowStyle" />
<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
<webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="20" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  ></webdiyer:AspNetPager>
</div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>