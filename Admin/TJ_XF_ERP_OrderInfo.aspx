﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_XF_ERP_OrderInfo.aspx.cs" Inherits="Admin_TJ_XF_ERP_OrderInfo" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
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
<div class="container topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_XF_ERP_OrderInfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'TJ_XF_ERP_OrderInfo')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="id">id</asp:ListItem>
<asp:ListItem Value="erpordercode">erpordercode</asp:ListItem>
<asp:ListItem Value="orderdate">orderdate</asp:ListItem>
<asp:ListItem Value="materialcode">materialcode</asp:ListItem>
<asp:ListItem Value="prodname">prodname</asp:ListItem>
<asp:ListItem Value="agentname">agentname</asp:ListItem>
<asp:ListItem Value="agentcode">agentcode</asp:ListItem>
<asp:ListItem Value="tm">tm</asp:ListItem>
             </asp:DropDownList>
        </div>
     <div class="topitem"><span>包含</span></div>
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
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
                <asp:TemplateField HeaderText="erpordercode">
                    <ItemTemplate>
                        <asp:Label ID="Labelerpordercode" runat="server" Text='<%# Bind("erpordercode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="orderdate">
                    <ItemTemplate>
                        <asp:Label ID="Labelorderdate" runat="server" Text='<%# Bind("orderdate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="materialcode">
                    <ItemTemplate>
                        <asp:Label ID="Labelmaterialcode" runat="server" Text='<%# Bind("materialcode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="prodname">
                    <ItemTemplate>
                        <asp:Label ID="Labelprodname" runat="server" Text='<%# Bind("prodname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="agentname">
                    <ItemTemplate>
                        <asp:Label ID="Labelagentname" runat="server" Text='<%# Bind("agentname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="agentcode">
                    <ItemTemplate>
                        <asp:Label ID="Labelagentcode" runat="server" Text='<%# Bind("agentcode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="tm">
                    <ItemTemplate>
                        <asp:Label ID="Labeltm" runat="server" Text='<%# Bind("tm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" /></asp:CommandField>
</Columns>
<EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
<FooterStyle CssClass="GridViewFooterStyle" />
<RowStyle CssClass="GridViewRowStyle" />
<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView>
<webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle"  PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
</div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>