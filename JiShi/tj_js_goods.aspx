﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_goods.aspx.cs" Inherits="Admin_tj_js_goods" %>
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
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('tj_js_goodsAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_js_goods')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="id">id</asp:ListItem>
<asp:ListItem Value="goodsid">goodsid</asp:ListItem>
<asp:ListItem Value="name">name</asp:ListItem>
<asp:ListItem Value="price">price</asp:ListItem>
<asp:ListItem Value="img">img</asp:ListItem>
<asp:ListItem Value="type">type</asp:ListItem>
<asp:ListItem Value="position">position</asp:ListItem>
<asp:ListItem Value="intro">intro</asp:ListItem>
<asp:ListItem Value="remark">remark</asp:ListItem>
<asp:ListItem Value="realprice">realprice</asp:ListItem>
<asp:ListItem Value="valid">valid</asp:ListItem>
<asp:ListItem Value="compid">compid</asp:ListItem>
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
                <asp:TemplateField HeaderText="goodsid">
                    <ItemTemplate>
                        <asp:Label ID="Labelgoodsid" runat="server" Text='<%# Bind("goodsid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="name">
                    <ItemTemplate>
                        <asp:Label ID="Labelname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="price">
                    <ItemTemplate>
                        <asp:Label ID="Labelprice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="img">
                    <ItemTemplate>
                        <asp:Label ID="Labelimg" runat="server" Text='<%# Bind("img") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="type">
                    <ItemTemplate>
                        <asp:Label ID="Labeltype" runat="server" Text='<%# Bind("type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="position">
                    <ItemTemplate>
                        <asp:Label ID="Labelposition" runat="server" Text='<%# Bind("position") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="intro">
                    <ItemTemplate>
                        <asp:Label ID="Labelintro" runat="server" Text='<%# Bind("intro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="remark">
                    <ItemTemplate>
                        <asp:Label ID="Labelremark" runat="server" Text='<%# Bind("remark") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="realprice">
                    <ItemTemplate>
                        <asp:Label ID="Labelrealprice" runat="server" Text='<%# Bind("realprice") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="valid">
                    <ItemTemplate>
                        <asp:Label ID="Labelvalid" runat="server" Text='<%# Bind("valid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="compid">
                    <ItemTemplate>
                        <asp:Label ID="Labelcompid" runat="server" Text='<%# Bind("compid") %>'></asp:Label>
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