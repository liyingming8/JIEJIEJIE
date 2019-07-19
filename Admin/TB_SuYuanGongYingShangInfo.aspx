﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanGongYingShangInfo.aspx.cs" Inherits="Admin_TB_SuYuanGongYingShangInfo" %>

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
            <div class="topdiv">
                <div class="topitem">
                    <div class="topitem">
                        <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TB_SuYuanGongYingShangInfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'供应商')" />
                    </div>
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="GYSName">供应商名称</asp:ListItem>
                        <asp:ListItem Value="GYSPhoneNum">供应商手机号码</asp:ListItem>
                        <asp:ListItem Value="GYSAddress">供应商地址</asp:ListItem>
                        <asp:ListItem Value="SpID">供应类别</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <input id="inputSearchKeyword" placeholder="请输入查找内容" type="text" runat="server" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                 
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID"
                OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商名称">
                        <ItemTemplate>
                            <asp:Label ID="LabelGYSName" runat="server" Text='<%# Bind("GYSName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应类别">
                        <ItemTemplate>
                            <asp:Label ID="LabelSpID" runat="server" Text='<%# ReturnCNameByIDString(Eval("SpID").ToString().Trim()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="城市">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text=' <%# comfun.ReturnBaseClassName(Eval("CtiyID").ToString(), true, false) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详细地址">
                        <ItemTemplate>
                            <asp:Label ID="LabelGYSAddress" runat="server" Text='<%# Bind("GYSAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商手机号码">
                        <ItemTemplate>
                            <asp:Label ID="LabelGYSPhoneNum" runat="server" Text='<%# Bind("GYSPhoneNum") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle  HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  ></webdiyer:AspNetPager>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
